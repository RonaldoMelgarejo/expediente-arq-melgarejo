# Detecciones - Ferreteria "El tornillo"
__Nombre: Ronaldo Pablo Melgarejo Cardozo__

__Violación 1: I (ISP - Segregacion de Interfaces)__

- Vive en IEmpleadoDeFerreteria en los metodos AutorizarVentaAlPorMayor, AjustarPrecio, VerReporteDeCompras que implementa la clase Vendedor.
- Es una violación porque la interfaz gorda IEmpleadoDeFerreteria obliga al Vendedor a implementar los métodos que no cumplen que son AutorizarVentaAlPorMayor, AjustarPrecio, VerReporteDeCompras, ya que estas lanzan excepciones porque el vendedor no puede hacer eso, bajo el contrato que tiene.

__Violación 2: O (OCP - Abierto a extension y cerrado a modificacion)__
- Vive en la clase GestorDePedidos, en el metodo ProcesarPedido porque contiene un switch
- Es una violacion porque usa un switch donde puede crecer por nuevos tipoCliente, la cual si mañana llega un nuevo tipo de cliente como mayorista o tal vez otro, tenemos que modificar el switch agregando un case nuevo. por lo cual la clase no esta cerrada a modificaciones.

__Violación 3: D (DIP - Inversion de Dependencias)__
- Vive en la clase GestorDePedidos, ene el método ProcesarPedido
- Es una violación porque la clase crea sus propias dependencias con new BaseDeDatosMySql y new CorreoSmtp, por lo cual esta atada a esta implementación concreta, donde si cambia la base de datos o el correo hay que modificar la clase.

__Violación 4: S (SRP - Responsabilidad Unica)__
- Vive en la clase GestorDePedidos en el método ProcesarPedido
- Es una violación porque esta clase esta sobre cargada ya que tiene al menos cuatro responsabilidades, como calcular descuentos, guardar en la base de datos, imprime el comprobante y envía correos, si cambia la lógica de descuento o el formato de comprobante hay que tocar la clase.

