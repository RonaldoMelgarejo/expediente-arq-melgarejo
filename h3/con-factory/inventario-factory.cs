using System;

public class Inventario
{
    public string IdInventario { get; set; }
    public int CantidadDisponible { get; set; }
    public int StockMinimo { get; set; }
    public string Ubicacion { get; set; }

    public Inventario(
        string idInventario,
        int cantidadDisponible,
        int stockMinimo,
        string ubicacion)
    {
        IdInventario = idInventario;
        CantidadDisponible = cantidadDisponible;
        StockMinimo = stockMinimo;
        Ubicacion = ubicacion;
    }

    public bool StockBajo()
    {
        return CantidadDisponible > 0 &&
               CantidadDisponible <= StockMinimo;
    }

    public bool StockAgotado()
    {
        return CantidadDisponible == 0;
    }
}

public interface IAviso
{
    void Enviar(Inventario inventario);
}

public class AvisoStockMinimo : IAviso
{
    public void Enviar(Inventario inventario)
    {
        Console.WriteLine(
            $"[STOCK MÍNIMO] El inventario {inventario.IdInventario} " +
            $"tiene {inventario.CantidadDisponible} unidades. " +
            $"Stock mínimo: {inventario.StockMinimo}."
        );
    }
}

public class AvisoStockAgotado : IAviso
{
    public void Enviar(Inventario inventario)
    {
        Console.WriteLine(
            $"[STOCK AGOTADO] El inventario {inventario.IdInventario} " +
            $"no tiene unidades disponibles."
        );
    }
}

public class AvisoReposicion : IAviso
{
    public void Enviar(Inventario inventario)
    {
        Console.WriteLine(
            $"[REPOSICIÓN] El inventario {inventario.IdInventario} " +
            $"requiere reposición. " +
            $"Cantidad actual: {inventario.CantidadDisponible}."
        );
    }
}

public static class FabricaDeAvisos
{
    public static IAviso Crear(string tipo)
    {
        return tipo.ToLower() switch
        {
            "stock-minimo" => new AvisoStockMinimo(),
            "stock-agotado" => new AvisoStockAgotado(),
            "reposicion" => new AvisoReposicion(),
            _ => throw new ArgumentException(
                $"Tipo de aviso desconocido: {tipo}"
            )
        };
    }
}

public class Program
{
    public static void Main()
    {
        Inventario inventario = new Inventario(
            "I001",
            3,
            5,
            "Almacén A"
        );

        Console.WriteLine("=== RF5 - AVISOS DE INVENTARIO ===");
        Console.WriteLine();
        Console.WriteLine($"Inventario: {inventario.IdInventario}");
        Console.WriteLine($"Cantidad disponible: {inventario.CantidadDisponible}");
        Console.WriteLine($"Stock mínimo: {inventario.StockMinimo}");
        Console.WriteLine($"Ubicación: {inventario.Ubicacion}");
        Console.WriteLine();

        if (inventario.StockBajo())
        {
            IAviso aviso = FabricaDeAvisos.Crear("stock-minimo");
            aviso.Enviar(inventario);
        }

        if (inventario.StockAgotado())
        {
            IAviso aviso = FabricaDeAvisos.Crear("stock-agotado");
            aviso.Enviar(inventario);
        }

        IAviso avisoReposicion = FabricaDeAvisos.Crear("reposicion");
        avisoReposicion.Enviar(inventario);
    }
}
