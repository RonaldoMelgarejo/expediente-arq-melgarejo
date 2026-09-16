// Elijo la situacion 2: Cálculo de multa por día de atraso
// Patrón: Strategy
// Solucion: Ronaldo Pablo Melgarejo Cardozo

using System;

namespace Parcial2.Biblioteca
{
    // EL CONTRATO: toda regla de multa sabe calcularse
    public interface IReglaDeMulta
    {
        decimal Calcular(int diasDeAtraso);
    }

    // Una clase por tipo de socio, cada una sabe su fórmula y nada más

    // Socio infantil: no paga multa, solo bloquea nuevos préstamos
    public class MultaInfantil : IReglaDeMulta
    {
        public decimal Calcular(int diasDeAtraso)
        {
            return 0m;
        }
    }

    // Socio adulto: 2 Bs por día de atraso
    public class MultaAdulto : IReglaDeMulta
    {
        public decimal Calcular(int diasDeAtraso)
        {
            return diasDeAtraso * 2.00m;
        }
    }

    // Socio tercera edad: 1 Bs por día, con tope de 20 Bs
    public class MultaTerceraEdad : IReglaDeMulta
    {
        public decimal Calcular(int diasDeAtraso)
        {
            return Math.Min(diasDeAtraso * 1.00m, 20.00m);
        }
    }

    // La calculadora recibe la regla y la aplica
    public class CalculadoraDeMultas
    {
        private readonly IReglaDeMulta _regla;

        // Inyección de dependencias: la regla entra por constructor
        public CalculadoraDeMultas(IReglaDeMulta regla)
        {
            _regla = regla;
        }

        public decimal Calcular(int diasDeAtraso)
        {
            return _regla.Calcular(diasDeAtraso);
        }
    }

    public static class Demo
    {
        public static void Main()
        {
            Console.WriteLine("=== BIBLIOTECA MUNICIPAL - CÁLCULO DE MULTAS ===\n");

            int dias = 5;

            // Socio infantil
            var calcInfantil = new CalculadoraDeMultas(new MultaInfantil());
            Console.WriteLine("[INFANTIL]   " + (dias) + " días de atraso: " + calcInfantil.Calcular(dias).ToString("0.00") + " Bs (solo bloqueo)");

            // Socio adulto
            var calcAdulto = new CalculadoraDeMultas(new MultaAdulto());
            Console.WriteLine("[ADULTO]   " + (dias) + " días de atraso: " + calcAdulto.Calcular(dias).ToString("0.00") + " Bs");

            // Socio tercera edad
            var calcTercera = new CalculadoraDeMultas(new MultaTerceraEdad());
            Console.WriteLine("[TERCERA EDAD]   " + (dias) + " días de atraso: " + calcTercera.Calcular(dias).ToString("0.00") + " Bs");
        }
    }
}
