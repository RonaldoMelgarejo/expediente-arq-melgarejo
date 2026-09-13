using System;
using System.Collections.Generic;

public class Categoria
{
    public string IdCategoria { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }

    public Categoria(string idCategoria, string nombre, string descripcion)
    {
        IdCategoria = idCategoria;
        Nombre = nombre;
        Descripcion = descripcion;
    }
}

public class Producto
{
    public string IdProducto { get; set; }
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public double PrecioBase { get; set; }

    public Producto(string idProducto, string nombre, string descripcion, double precioBase)
    {
        IdProducto = idProducto;
        Nombre = nombre;
        Descripcion = descripcion;
        PrecioBase = precioBase;
    }
}

public class Inventario
{
    public string IdInventario { get; set; }
    public int CantidadDisponible { get; set; }
    public int StockMinimo { get; set; }
    public string Ubicacion { get; set; }

    public Inventario(string idInventario, int cantidadDisponible, int stockMinimo, string ubicacion)
    {
        IdInventario = idInventario;
        CantidadDisponible = cantidadDisponible;
        StockMinimo = stockMinimo;
        Ubicacion = ubicacion;
    }
}

public class OrdenVenta
{
    public string IdOrden { get; set; }
    public DateTime Fecha { get; set; }
    public string Estado { get; set; }
    public List<DetalleOrden> Detalles { get; set; }

    public OrdenVenta(string idOrden, DateTime fecha, string estado)
    {
        IdOrden = idOrden;
        Fecha = fecha;
        Estado = estado;
        Detalles = new List<DetalleOrden>();
    }

    public void AgregarDetalle(DetalleOrden detalle)
    {
        Detalles.Add(detalle);
    }
}

public class DetalleOrden
{
    public int Cantidad { get; set; }
    public double PrecioUnitario { get; set; }
    public double Subtotal { get; set; }

    public DetalleOrden(int cantidad, double precioUnitario)
    {
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        Subtotal = cantidad * precioUnitario;
    }
}
