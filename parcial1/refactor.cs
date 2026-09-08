// Curar DOS violaciones: elijo  I (ISP) y D (DIP)
// Refactor: Ronaldo Pablo Melgarejo Cardozo

namespace Parcial1.Ferreteria;

// PASO 1: CONTRATOS SEGREGADOS (ISP) dividimos la interfaz gorda en interfaces pequeñas

// Interfaz #1: Solo para registrar pedidos
public interface IRegistradorDePedidos
{
    void RegistrarPedido(string material, int cantidad);
}

// Interfaz #2: Solo para autorizar ventas al por mayor
public interface IAutenticadorDeVentas
{
    void AutorizarVentaAlPorMayor(string material);
}

// Interfaz #3: Solo para gestionar precios
public interface IGestorDePrecios
{
    void AjustarPrecio(string material, decimal nuevoPrecio);
}

// Interfaz #4: Solo para ver reportes
public interface IGeneradorDeReportes
{
    void VerReporteDeCompras();
}

// PASO 2: CONTRATOS PARA INFRAESTRUCTURA (DIP) creamos contratos que el negocio necesita

// contrato para guardar pedidos
public interface IRepositorioDePedidos
{
    void GuardarPedido(string cliente, string material, int cantidad, decimal total);
}

// contrato para notificar
public interface ICanalDeNotificacion
{
    void Enviar(string mensaje);
}

// contrato para descuentos
public interface ICalculadorDeDescuentos
{
    decimal Calcular(string tipoCliente, decimal total);
}

// PASO 3: implementaciones

// --- Implementaciones de ISP ---

// Vendedor SOLO firma lo que puede hacer
public class Vendedor : IRegistradorDePedidos
{
    public void RegistrarPedido(string material, int cantidad)
        => Console.WriteLine($"[VEND] Pedido: {cantidad} x {material}");
}

// Encargado firma TODO lo que puede hacer
public class Encargado : IRegistradorDePedidos, IAutenticadorDeVentas, IGestorDePrecios, IGeneradorDeReportes
{
    public void RegistrarPedido(string material, int cantidad)
        => Console.WriteLine($"[ENC] Pedido: {cantidad} x {material}");

    public void AutorizarVentaAlPorMayor(string material)
        => Console.WriteLine($"[ENC] Venta al por mayor de {material} autorizada");

    public void AjustarPrecio(string material, decimal nuevoPrecio)
        => Console.WriteLine($"[ENC] {material} ahora cuesta {nuevoPrecio:0.00} Bs");

    public void VerReporteDeCompras()
        => Console.WriteLine("[ENC] Reporte de compras del mes");
}

// --- Implementaciones de DIP ---

// Repositorio concreto: MySQL
public class RepositorioMySQL : IRepositorioDePedidos
{
    public void GuardarPedido(string cliente, string material, int cantidad, decimal total)
        => Console.WriteLine($"[MYSQL] INSERT INTO pedidos VALUES ('{cliente}', '{material}', {cantidad}, {total})");
}

// Canal concreto: Correo SMTP
public class NotificadorPorCorreo : ICanalDeNotificacion
{
    public void Enviar(string mensaje)
        => Console.WriteLine($"[SMTP] {mensaje}");
}

// Calculador de descuentos
public class CalculadorDeDescuentos : ICalculadorDeDescuentos
{
    public decimal Calcular(string tipoCliente, decimal total)
    {
        switch (tipoCliente)
        {
            case "particular":
                return 0;
            case "contratista":
                return total * 0.15m;
            case "constructora":
                return total * 0.25m;
            default:
                return 0;
        }
    }
}

// PASO 4: COORDINADOR 

public class GestorDePedidos
{
    private readonly IRepositorioDePedidos _repositorio;
    private readonly ICanalDeNotificacion _canal;
    private readonly ICalculadorDeDescuentos _calculadorDescuentos;

    public GestorDePedidos(
        IRepositorioDePedidos repositorio,
        ICanalDeNotificacion canal,
        ICalculadorDeDescuentos calculadorDescuentos)
    {
        _repositorio = repositorio;
        _canal = canal;
        _calculadorDescuentos = calculadorDescuentos;
    }

    public void ProcesarPedido(string cliente, string tipoCliente, string material, int cantidad, decimal precioUnitario)
    {
        decimal total = cantidad * precioUnitario;

        decimal descuento = _calculadorDescuentos.Calcular(tipoCliente, total);
        decimal totalFinal = total - descuento;

        _repositorio.GuardarPedido(cliente, material, cantidad, totalFinal);

        Console.WriteLine("----- COMPROBANTE -----");
        Console.WriteLine($"{cantidad} x {material}");
        Console.WriteLine($"Cliente: {cliente} ({tipoCliente})");
        Console.WriteLine($"TOTAL: {totalFinal:0.00} Bs");

        _canal.Enviar($"Su pedido de {material} fue registrado, {cliente}");
    }
}

public static class Demo
{
    public static void Correr()
    {
        var gestor = new GestorDePedidos(
            new RepositorioMySQL(),
            new NotificadorPorCorreo(),
            new CalculadorDeDescuentos()
        );
      
        gestor.ProcesarPedido("Marco", "contratista", "Cemento 50kg", 10, 62.00m);
    }
}
