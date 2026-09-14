using System;
using System.Collections.Generic;

namespace Builder.Despues;

public class OrdenVenta
{
    public string IdOrden { get; init; } = "";
    public DateTime Fecha { get; init; }
    public string Estado { get; init; } = "";
    public string Producto { get; init; } = "";
    public int Cantidad { get; init; }
    public double Total { get; init; }

    public void Describir()
        => Console.WriteLine(
            $"[TIENDA] Orden {IdOrden} | " +
            $"Producto: {Producto} | " +
            $"Cantidad: {Cantidad} | " +
            $"Total: {Total} | " +
            $"Estado: {Estado}"
        );
}

public class ArmadorDeOrdenVenta
{
    private string _idOrden = "";
    private DateTime _fecha;
    private string _estado = "";
    private string _producto = "";
    private int _cantidad;
    private double _total;

    public ArmadorDeOrdenVenta ConId(string id)
    {
        _idOrden = id;
        return this;
    }

    public ArmadorDeOrdenVenta ConFecha(DateTime fecha)
    {
        _fecha = fecha;
        return this;
    }

    public ArmadorDeOrdenVenta ConEstado(string estado)
    {
        _estado = estado;
        return this;
    }

    public ArmadorDeOrdenVenta ConProducto(string producto)
    {
        _producto = producto;
        return this;
    }

    public ArmadorDeOrdenVenta ConCantidad(int cantidad)
    {
        _cantidad = cantidad;
        return this;
    }

    public ArmadorDeOrdenVenta ConTotal(double total)
    {
        _total = total;
        return this;
    }

    public OrdenVenta Construir()
    {
        if (string.IsNullOrEmpty(_idOrden))
            throw new InvalidOperationException(
                "Falta el identificador de la orden."
            );

        if (string.IsNullOrEmpty(_estado))
            throw new InvalidOperationException(
                "Falta el estado de la orden."
            );

        if (string.IsNullOrEmpty(_producto))
            throw new InvalidOperationException(
                "Falta el producto."
            );

        if (_cantidad <= 0)
            throw new InvalidOperationException(
                "La cantidad debe ser mayor a cero."
            );

        if (_total <= 0)
            throw new InvalidOperationException(
                "El total debe ser mayor a cero."
            );

        return new OrdenVenta
        {
            IdOrden = _idOrden,
            Fecha = _fecha,
            Estado = _estado,
            Producto = _producto,
            Cantidad = _cantidad,
            Total = _total
        };
    }
}

public class Program
{
    public static void Main()
    {
        var orden = new ArmadorDeOrdenVenta()
            .ConId("ORD001")
            .ConFecha(DateTime.Now)
            .ConEstado("carrito")
            .ConProducto("Taladro eléctrico")
            .ConCantidad(2)
            .ConTotal(900)
            .Construir();

        orden.Describir();

        try
        {
            new ArmadorDeOrdenVenta()
                .ConEstado("carrito")
                .ConProducto("Taladro eléctrico")
                .Construir();
        }
        catch (InvalidOperationException e)
        {
            Console.WriteLine($"[GUARDIÁN] {e.Message}");
        }
    }
}
