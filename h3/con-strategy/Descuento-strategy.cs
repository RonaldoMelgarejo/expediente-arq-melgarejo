using System;

public interface IEstrategiaDescuento
{
    double CalcularDescuento(double monto, int cantidad);
}

public class DescuentoSinPromocion : IEstrategiaDescuento
{
    public double CalcularDescuento(double monto, int cantidad)
    {
        return 0;
    }
}

public class DescuentoPorVolumen : IEstrategiaDescuento
{
    public double CalcularDescuento(double monto, int cantidad)
    {
        if (cantidad >= 10)
            return monto * 0.10;
        
        return 0;    
    }
}

public class DescuentoPromocional : IEstrategiaDescuento
{
    public double CalcularDescuento(double monto, int cantidad)
    {
        return monto * 0.15;
    }
}

public class CalculadorDeDescuento
{
    private readonly IEstrategiaDescuento _estrategia;

    public CalculadorDeDescuento(IEstrategiaDescuento estrategia)
    {
        _estrategia = estrategia;
    }

    public double Calcular(double monto, int cantidad)
    {
        return _estrategia.CalcularDescuento(monto, cantidad);
    }
}

public static class Demo
{
    public static void Main()
    {
        double monto = 1500;
        int cantidad = 12;

        Console.WriteLine(
            $"[SIN PROMOCIÓN] Descuento: " +
            $"{new CalculadorDeDescuento(new DescuentoSinPromocion()).Calcular(monto, cantidad):0.00} Bs"
        );

        Console.WriteLine(
            $"[POR VOLUMEN]   Descuento: " +
            $"{new CalculadorDeDescuento(new DescuentoPorVolumen()).Calcular(monto, cantidad):0.00} Bs"
        );

        Console.WriteLine(
            $"[PROMOCIONAL]   Descuento: " +
            $"{new CalculadorDeDescuento(new DescuentoPromocional()).Calcular(monto, cantidad):0.00} Bs"
        );
    }
}
