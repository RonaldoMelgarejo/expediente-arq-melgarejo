# Diagrama 
__1. H1 ANTES__
````mermaid
classDiagram
    class Usuario {
        +String id
        +String nombre
        +String email
        +String rol
        +autenticar() bool
    }

    class Producto {
        +String idProducto
        +String nombre
        +String descripcion
        +double precioBase
        +String categoria
        +actualizarPrecio(double nuevoPrecio)
    }

    class Inventario {
        +String idInventario
        +int cantidadDisponible
        +int stockMinimo
        +String ubicacion
        +actualizarStock(int cantidad)
        +verificarDisponibilidad() bool
    }

    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +double total
        +List~DetalleOrden~ detalles
        +calcularTotal() double
        +cambiarEstado(String nuevoEstado)
        +guardar()
        +procesarPago()
    }

    class DetalleOrden {
        +int cantidad
        +double precioUnitario
        +double subtotal
        +calcularSubtotal() double
    }

    class Pago {
        +String idPago
        +Date fechaPago
        +double monto
        +String metodoPago
        +String estadoTransaccion
        +procesarPago() bool
    }

    class RepositorioOrdenVentaMySQL {
        +guardar(OrdenVenta orden)
    }



    Producto "1" -- "1" Inventario : gestiona
    Usuario "1" -- "*" OrdenVenta : realiza / procesa
    OrdenVenta "1" *-- "1..*" DetalleOrden : contiene
    DetalleOrden "*" -- "1" Producto : referencia a
    OrdenVenta "1" -- "1" Pago : liquida con

    OrdenVenta --> RepositorioOrdenVentaMySQL : new

````
__Problemas identificados en el H1__
* En el diseño inicial se identificó que algunas clases concentraban demasiadas responsabilidades, especialmente OrdenVenta, que además de representar la venta se encargaba de realizar cálculos, controlar estados y gestionar aspectos relacionados con la persistencia.
* También se observó que ciertas decisiones del comportamiento del sistema podían terminar dependiendo de condiciones o switch según el tipo o estado de la venta. Esto hacía que, ante la necesidad de agregar un nuevo comportamiento, fuera necesario modificar código que ya funcionaba.
* Otro problema estaba relacionado con las dependencias directas hacia implementaciones concretas. Al utilizar objetos específicos, como un repositorio de una determinada base de datos, desde la lógica del negocio, el sistema quedaba más ligado a una tecnología particular.
* Además, los contratos de algunas operaciones podían ser demasiado amplios para los diferentes tipos de usuarios. Por ejemplo, un vendedor no necesariamente necesita realizar las mismas operaciones que un administrador, por lo que obligarlos a depender de una misma interfaz generaría métodos que algunos roles no utilizarían.
* Finalmente, también se identificó el riesgo de establecer relaciones de herencia o contratos que obligaran a determinadas clases a implementar comportamientos que realmente no les corresponden. Esto podría provocar excepciones, métodos sin uso o comportamientos especiales que contradijeran lo esperado del contrato original.

 
__Estos problemas hacían que el diseño inicial fuera más difícil de mantener, extender y probar, por lo que la aplicación de los principios SOLID permitió organizar mejor las responsabilidades y reducir el acoplamiento entre las diferentes partes del sistema.__

__2. H2 DESPUES (SOLID)__

````mermaid
classDiagram
    class Usuario {
        +String id
        +String nombre
        +String email
        +String rol
        +autenticar() bool
    }

    class Categoria {
        +String idCategoria
        +String nombre
        +String descripcion
    }

    class Producto {
        +String idProducto
        +String nombre
        +String descripcion
        +double precioBase
        +actualizarPrecio(double nuevoPrecio)
    }

    class Inventario {
        +String idInventario
        +int cantidadDisponible
        +int stockMinimo
        +String ubicacion
        +actualizarStock(int cantidad)
        +verificarDisponibilidad() bool
    }

    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +List~DetalleOrden~ detalles
    }

    class DetalleOrden {
        +int cantidad
        +double precioUnitario
        +double subtotal
        +calcularSubtotal() double
    }

    class Pago {
        +String idPago
        +Date fechaPago
        +double monto
        +String metodoPago
        +String estadoTransaccion
    }

    class CalculadorDeVenta {
        +calcularTotal(OrdenVenta orden) double
    }

    class GestorDeEstadoVenta {
        +cambiarEstado(OrdenVenta orden, String nuevoEstado)
    }

    class EstadoVenta {
        <<interface>>
        +cambiar(OrdenVenta orden)
    }

    class EstadoCarrito {
        +cambiar(OrdenVenta orden)
    }

    class EstadoConfirmada {
        +cambiar(OrdenVenta orden)
        +procesarPago(OrdenVenta orden)
        +anular(OrdenVenta orden)
    }

    class EstadoPagada {
        +cambiar(OrdenVenta orden)
        +entregar(OrdenVenta orden)
    }

    class ProcesablePago {
        <<interface>>
        +procesarPago(OrdenVenta orden)
    }

    class Entregable {
        <<interface>>
        +entregar(OrdenVenta orden)
    }

    class Anulable {
        <<interface>>
        +anular(OrdenVenta orden)
    }

    class IRegistrador {
        <<interface>>
        +registrarVenta()
    }

    class IAjustadorStock {
        <<interface>>
        +ajustarStock()
    }

    class IAjustadorPrecio {
        <<interface>>
        +ajustarPrecio()
    }

    class Vendedor {
        +registrarVenta()
    }

    class Administrador {
        +ajustarStock()
        +ajustarPrecio()
    }

    class IRepositorioDeOrdenVenta {
        <<interface>>
        +guardar(OrdenVenta orden)
    }

    class RepositorioOrdenVentaMySQL {
        +guardar(OrdenVenta orden)
    }

    class RepositorioOrdenVentaPostgreSQL {
        +guardar(OrdenVenta orden)
    }

    class RepositorioOrdenVentaMemoria {
        +guardar(OrdenVenta orden)
    }

    Usuario "1" -- "*" OrdenVenta : realiza / procesa

    Categoria "1" -- "*" Producto : clasifica

    Producto "1" -- "1" Inventario : gestiona

    OrdenVenta "1" *-- "1..*" DetalleOrden : contiene

    DetalleOrden "*" -- "1" Producto : referencia a

    OrdenVenta "1" -- "1" Pago : liquida con

    CalculadorDeVenta ..> OrdenVenta : calcula
    GestorDeEstadoVenta ..> OrdenVenta : gestiona

    OrdenVenta --> EstadoVenta : utiliza

    EstadoVenta <|.. EstadoCarrito
    EstadoVenta <|.. EstadoConfirmada
    EstadoVenta <|.. EstadoPagada

    ProcesablePago <|.. EstadoConfirmada
    Entregable <|.. EstadoPagada
    Anulable <|.. EstadoConfirmada

    Usuario <|-- Vendedor
    Usuario <|-- Administrador

    IRegistrador <|.. Vendedor
    IAjustadorStock <|.. Administrador
    IAjustadorPrecio <|.. Administrador

    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaMySQL
    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaPostgreSQL
    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaMemoria

    OrdenVenta --> IRepositorioDeOrdenVenta : depende de
````
__H2 Aplicando principios SOLID__
* Se dividió la clase OrdenVenta, separando el cálculo, gestión de estados y persistencia para que cada clase tenga una sola responsabilidad (SRP).
* Se reemplazó la lógica basada en decisiones por tipo mediante estados concretos que implementan EstadoVenta, permitiendo agregar nuevos comportamientos sin modificar los existentes (OCP).
* Se redujeron los contratos de los estados a comportamientos comunes y se separaron capacidades como pago, entrega y anulación para evitar sustituciones incorrectas (LSP).
* Se dividieron las operaciones de los usuarios en interfaces pequeñas según sus capacidades, evitando que vendedor y administrador dependan de métodos que no necesitan (ISP).
* Se eliminó la dependencia directa de OrdenVenta hacia una implementación concreta de base de datos mediante la interfaz IRepositorioDeOrdenVenta (DIP), además las implementaciones MySQL, PostgreSQL y memoria pueden conectarse al mismo contrato sin modificar la lógica del negocio.

__El resultado es una arquitectura con responsabilidades separadas, extensible y con dependencias dirigidas hacia abstracciones.__

