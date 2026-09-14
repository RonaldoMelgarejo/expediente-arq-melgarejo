using System;

namespace Adapter.Despues;

public class Pago
{
    public string IdPago { get; set; } = "";
    public DateTime FechaPago { get; set; }
    public double Monto { get; set; }
    public string MetodoPago { get; set; } = "";
    public string EstadoTransaccion { get; set; } = "";
}

public class ConversorDolarExterno
{
    public double Convertir(
        double monto,
        string origen,
        string destino)
    {
        if (origen == "USD" && destino == "BOB")
            return monto * 6.96;

        return 0;
    }
}

public class ConversorEuroExterno
{
    public decimal CambiarMoneda(
        decimal cantidad,
        string moneda)
    {
        if (moneda == "EUR")
            return cantidad * 8.10m;

        return 0;
    }
}

public interface IConvertidorMoneda
{
    double ConvertirABolivianos(double monto);
}

public class AdaptadorDolar : IConvertidorMoneda
{
    private readonly ConversorDolarExterno _conversor = new();

    public double ConvertirABolivianos(double monto)
    {
        return _conversor.Convertir(
            monto,
            "USD",
            "BOB"
        );
    }
}

public class AdaptadorEuro : IConvertidorMoneda
{
    private readonly ConversorEuroExterno _conversor = new();

    public double ConvertirABolivianos(double monto)
    {
        decimal resultado =
            _conversor.CambiarMoneda(
                Convert.ToDecimal(monto),
                "EUR"
            );

        return Convert.ToDouble(resultado);
    }
}

public class ProcesadorDePago
{
    private readonly IConvertidorMoneda _convertidor;

    public ProcesadorDePago(IConvertidorMoneda convertidor)
    {
        _convertidor = convertidor;
    }

    public void RegistrarPago(
        Pago pago,
        double monto,
        string moneda)
    {
        double montoBOB =
            _convertidor.ConvertirABolivianos(monto);

        pago.Monto = montoBOB;
        pago.EstadoTransaccion = "Procesada";

        Console.WriteLine(
            $"[PAGO] {pago.IdPago} — " +
            $"{montoBOB:F2} BOB " +
            $"(venía como {monto:F2} {moneda})"
        );
    }
}

public class Program
{
    public static void Main()
    {
        var pagoDolar = new Pago
        {
            IdPago = "P001",
            FechaPago = DateTime.Now,
            MetodoPago = "Dólares"
        };

        var procesadorDolar =
            new ProcesadorDePago(
                new AdaptadorDolar()
            );

        procesadorDolar.RegistrarPago(
            pagoDolar,
            100,
            "USD"
        );

        var pagoEuro = new Pago
        {
            IdPago = "P002",
            FechaPago = DateTime.Now,
            MetodoPago = "Euros"
        };

        var procesadorEuro =
            new ProcesadorDePago(
                new AdaptadorEuro()
            );

        procesadorEuro.RegistrarPago(
            pagoEuro,
            50,
            "EUR"
        );
    }
}
