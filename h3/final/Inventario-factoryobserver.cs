using System;
using System.Collections.Generic;

namespace FusionFinal;

// FACTORY — productos concretos de aviso

public interface IAviso
{
    void Enviar(string mensaje, string destinatario);
}

public class AvisoCliente : IAviso
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine(
            $"[CLIENTE] {mensaje} — dirigido a {destinatario}"
        );
}

public class AvisoAdministrador : IAviso
{
    public void Enviar(string mensaje, string destinatario)
        => Console.WriteLine(
            $"[ADMINISTRADOR] {mensaje} — dirigido a {destinatario}"
        );
}

// FACTORY — ventanilla única para crear avisos

public static class FabricaDeAvisos
{
    public static IAviso Crear(string tipo) => tipo switch
    {
        "cliente" => new AvisoCliente(),
        "administrador" => new AvisoAdministrador(),
        _ => throw new ArgumentException(
            $"Tipo de aviso desconocido: {tipo}"
        )
    };
}

// OBSERVER — contrato para los interesados en cambios de stock

public interface IInteresadoEnStock
{
    void CuandoCambiaStock(
        string producto,
        int stockActual,
        int stockMinimo
    );
}

// OBSERVADORES

public class AvisoAlCliente : IInteresadoEnStock
{
    public void CuandoCambiaStock(
        string producto,
        int stockActual,
        int stockMinimo)
    {
        IAviso aviso = FabricaDeAvisos.Crear("cliente");

        string mensaje;

        if (stockActual == 0)
        {
            mensaje =
                $"El producto {producto} está agotado. " +
                "No hay unidades disponibles.";
        }
        else
        {
            mensaje =
                $"El producto {producto} tiene disponibilidad limitada. " +
                $"Quedan {stockActual} unidades.";
        }

        aviso.Enviar(mensaje, "clientes interesados");
    }
}

public class AvisoAlAdministrador : IInteresadoEnStock
{
    public void CuandoCambiaStock(
        string producto,
        int stockActual,
        int stockMinimo)
    {
        IAviso aviso = FabricaDeAvisos.Crear("administrador");

        string mensaje;

        if (stockActual == 0)
        {
            mensaje =
                $"El producto {producto} está AGOTADO. " +
                "Se requiere reposición inmediata.";
        }
        else if (stockActual < stockMinimo)
        {
            mensaje =
                $"El producto {producto} está por debajo del stock mínimo. " +
                $"Stock actual: {stockActual} unidades. " +
                $"Stock mínimo: {stockMinimo}.";
        }
        else
        {
            mensaje =
                $"El producto {producto} alcanzó el stock mínimo de " +
                $"{stockMinimo} unidades.";
        }

        aviso.Enviar(mensaje, "administrador de inventario");
    }
}

// inventario que anuncia los cambios

public class Inventario
{
    private readonly List<IInteresadoEnStock> _interesados = new();

    public string Producto { get; }
    public int StockActual { get; private set; }
    public int StockMinimo { get; }

    public Inventario(
        string producto,
        int stockActual,
        int stockMinimo)
    {
        Producto = producto;
        StockActual = stockActual;
        StockMinimo = stockMinimo;
    }

    public void Suscribir(IInteresadoEnStock interesado)
        => _interesados.Add(interesado);

    public void ActualizarStock(int nuevoStock)
    {
        int stockAnterior = StockActual;

        StockActual = nuevoStock;

        Console.WriteLine(
            $"[INVENTARIO] {Producto}: " +
            $"{stockAnterior} -> {StockActual}"
        );

        bool cruzoStockMinimo =
            stockAnterior > StockMinimo &&
            StockActual <= StockMinimo;

        bool seAgoto =
            stockAnterior > 0 &&
            StockActual == 0;

        if (cruzoStockMinimo || seAgoto)
        {
            Console.WriteLine(
                $"[INVENTARIO] Evento detectado: avisando a " +
                $"{_interesados.Count} interesados"
            );

            foreach (var interesado in _interesados)
            {
                interesado.CuandoCambiaStock(
                    Producto,
                    StockActual,
                    StockMinimo
                );
            }
        }
    }
}

// DEMO

public static class Demo
{
    public static void Main()
    {
        var inventario = new Inventario(
            "Laptop Lenovo",
            10,
            5
        );

        inventario.Suscribir(new AvisoAlCliente());
        inventario.Suscribir(new AvisoAlAdministrador());

        Console.WriteLine("=== FUSIÓN FACTORY + OBSERVER ===");
        Console.WriteLine();

        Console.WriteLine("-- stock normal --");
        inventario.ActualizarStock(8);

        Console.WriteLine();

        Console.WriteLine("-- alcanza el stock mínimo --");
        inventario.ActualizarStock(5);

        Console.WriteLine();

        Console.WriteLine("-- producto agotado --");
        inventario.ActualizarStock(0);

        Console.WriteLine();

        Console.WriteLine("-- se repone el producto --");
        inventario.ActualizarStock(15);

        Console.WriteLine();

        Console.WriteLine("-- vuelve a alcanzar el mínimo --");
        inventario.ActualizarStock(5);
    }
}
