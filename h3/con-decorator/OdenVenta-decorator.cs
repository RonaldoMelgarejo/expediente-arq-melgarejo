using System;

namespace Decorator.Despues
{
    public interface IOrdenVenta
    {
        void Mostrar(string idOrden);
        double CalcularTotal();
    }

    public class OrdenVentaBase : IOrdenVenta
    {
        private readonly double total;

        public OrdenVentaBase(double total)
        {
            this.total = total;
        }

        public void Mostrar(string idOrden)
        {
            Console.WriteLine(
                "[ORDEN] " + idOrden +
                " | Total base: " + total.ToString("0.00") + " Bs"
            );
        }

        public double CalcularTotal()
        {
            return total;
        }
    }

    public abstract class CapaDeOrdenVenta : IOrdenVenta
    {
        protected readonly IOrdenVenta Interno;

        protected CapaDeOrdenVenta(IOrdenVenta interno)
        {
            Interno = interno;
        }

        public abstract void Mostrar(string idOrden);

        public abstract double CalcularTotal();
    }

    public class ConDescuento : CapaDeOrdenVenta
    {
        public ConDescuento(IOrdenVenta interno)
            : base(interno)
        {
        }

        public override void Mostrar(string idOrden)
        {
            Interno.Mostrar(idOrden);
            Console.WriteLine("   -> descuento del 10%");
        }

        public override double CalcularTotal()
        {
            return Interno.CalcularTotal() * 0.90;
        }
    }

    public class ConCostoEnvio : CapaDeOrdenVenta
    {
        public ConCostoEnvio(IOrdenVenta interno)
            : base(interno)
        {
        }

        public override void Mostrar(string idOrden)
        {
            Interno.Mostrar(idOrden);
            Console.WriteLine("   -> costo de envío: 25 Bs");
        }

        public override double CalcularTotal()
        {
            return Interno.CalcularTotal() + 25;
        }
    }

    public static class Demo
    {
        public static void Main()
        {
            IOrdenVenta completa =
                new ConCostoEnvio(
                    new ConDescuento(
                        new OrdenVentaBase(1000)));

            completa.Mostrar("ORD001");

            Console.WriteLine(
                "Total final: " +
                completa.CalcularTotal().ToString("0.00") +
                " Bs"
            );

            Console.WriteLine();

            Console.WriteLine(
                "-- otra combinación --"
            );

            IOrdenVenta sencilla =
                new ConCostoEnvio(
                    new OrdenVentaBase(1000));

            sencilla.Mostrar("ORD002");

            Console.WriteLine(
                "Total final: " +
                sencilla.CalcularTotal().ToString("0.00") +
                " Bs"
            );
        }
    }
}
