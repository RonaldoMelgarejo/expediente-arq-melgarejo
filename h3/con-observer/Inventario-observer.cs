using System;
using System.Collections.Generic;

public interface IObservadorStock
{
    void Actualizar(Inventario inventario);
}

public class Inventario
{
    public string IdInventario { get; set; }
    public int CantidadDisponible { get; set; }
    public int StockMinimo { get; set; }

    private readonly List<IObservadorStock> _observadores = new();

    public Inventario(
        string idInventario,
        int cantidadDisponible,
        int stockMinimo)
    {
        IdInventario = idInventario;
        CantidadDisponible = cantidadDisponible;
        StockMinimo = stockMinimo;
    }

    public void Suscribir(IObservadorStock observador)
    {
        _observadores.Add(observador);
    }

    public void ActualizarStock(int nuevaCantidad)
    {
        int cantidadAnterior = CantidadDisponible;
        CantidadDisponible = nuevaCantidad;

        if (cantidadAnterior > StockMinimo &&
            CantidadDisponible <= StockMinimo)
        {
            foreach (var observador in _observadores)
            {
                observador.Actualizar(this);
            }
        }
    }
}

public class AvisoCliente : IObservadorStock
{
    public void Actualizar(Inventario inventario)
    {
        Console.WriteLine(
            $"[CLIENTE] Stock bajo: {inventario.CantidadDisponible} unidades."
        );
    }
}

public class AvisoAdministrador : IObservadorStock
{
    public void Actualizar(Inventario inventario)
    {
        Console.WriteLine(
            $"[ADMINISTRADOR] ALERTA: Se alcanzó el stock mínimo de {inventario.StockMinimo} unidades."
        );
    }
}

public static class Demo
{
    public static void Main()
    {
        var inventario = new Inventario(
            "I001",
            10,
            5
        );

        inventario.Suscribir(
            new AvisoCliente()
        );

        inventario.Suscribir(
            new AvisoAdministrador()
        );

        inventario.ActualizarStock(0);
    }
}
