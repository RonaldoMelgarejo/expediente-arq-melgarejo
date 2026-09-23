// Refactor: Ronaldo Pablo Melgarejo Cardozo

// REFACTOR DE SRP — GestorDeEstadias tenía 4 razones para cambiar.
// Cada responsabilidad se extrae a su propia clase.
// El gestor queda FLACO: solo coordina el flujo.

namespace Integradora.Parqueo.Refactor;

// ---- MODELO ----

public class Estadia
{
    public string Placa { get; }
    public string TipoVehiculo { get; }
    public int Horas { get; }
    public decimal Total { get; }

    public Estadia(string placa, string tipoVehiculo, int horas, decimal total)
    {
        Placa = placa;
        TipoVehiculo = tipoVehiculo;
        Horas = horas;
        Total = total;
    }
}

// ---- RESPONSABILIDAD 1: LA REGLA DE NEGOCIO (cambia si contabilidad lo pide) ----

public class CalculadoraDeTarifa
{
    public decimal Calcular(string tipoVehiculo, int horas)
    {
        decimal tarifaPorHora = tipoVehiculo switch
        {
            "auto"      => 5,
            "moto"      => 3,
            "residente" => 1,
            _           => 5
        };
        return tarifaPorHora * horas;
    }
}

// ---- RESPONSABILIDAD 2: LA PERSISTENCIA (cambia si migra la BD) ----

public class RepositorioDeEstadias
{
    public void Guardar(Estadia estadia)
        => Console.WriteLine($"[BD] INSERT INTO estadias VALUES ('{estadia.Placa}', '{estadia.TipoVehiculo}', {estadia.Horas}, {estadia.Total})");
}

// ---- RESPONSABILIDAD 3: LA PRESENTACIÓN (cambia si el dueño quiere otro ticket) ----

public class ImpresoraDeTickets
{
    public void Imprimir(Estadia estadia)
    {
        Console.WriteLine("----- TICKET DE SALIDA -----");
        Console.WriteLine($"Placa {estadia.Placa}: {estadia.Horas} h como {estadia.TipoVehiculo}");
        Console.WriteLine($"TOTAL: {estadia.Total:0.00} Bs");
    }
}

// ---- RESPONSABILIDAD 4: LA COMUNICACIÓN (cambia si el WhatsApp pasa a otro canal) ----

public class NotificadorDeSalidas
{
    public void Notificar(Estadia estadia)
        => Console.WriteLine($"[WHATSAPP] 📱 Salida registrada: {estadia.Placa}, {estadia.Horas} h, {estadia.Total:0.00} Bs");
}

// ---- EL GESTOR: solo coordina, no ejecuta ----

public class GestorDeEstadias
{
    private readonly CalculadoraDeTarifa _calculadora = new();
    private readonly RepositorioDeEstadias _repositorio = new();
    private readonly ImpresoraDeTickets _impresora = new();
    private readonly NotificadorDeSalidas _notificador = new();

    public void RegistrarSalida(string placa, string tipoVehiculo, int horas)
    {
        decimal total = _calculadora.Calcular(tipoVehiculo, horas);
        var estadia = new Estadia(placa, tipoVehiculo, horas, total);

        _repositorio.Guardar(estadia);
        _impresora.Imprimir(estadia);
        _notificador.Notificar(estadia);
    }
}

public static class Demo
{
    public static void Main()
    {
        new GestorDeEstadias().RegistrarSalida("1234-ABC", "auto", 3);
    }
}
