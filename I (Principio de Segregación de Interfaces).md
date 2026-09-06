# Contratos de los roles
__Roles__
* __Vendedor:__ Quiere registrar ventas, consultar la disponibilidad de productos y emitir la transacción.
* __Administrador:__ Quiere gestionar el inventario y visualizar reportes de ventas.
* __Cliente:__ Explora el catálogo de productos, consulta la disponibilidad, realiza compras.

1. ANTES (H1)
````mermaid
classDiagram
    class Usuario {
        +String id
        +String nombre
        +String email
        +String rol
        +autenticar() bool
    }

    class IGestionTienda {
        <<interface>>
        +registrarVenta()
        +ajustarStock()
        +ajustarPrecio()
    }

    class Vendedor {
        +registrarVenta()
        +ajustarStock()
        +ajustarPrecio()
    }

    class Administrador {
        +registrarVenta()
        +ajustarStock()
        +ajustarPrecio()
    }

    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +double total
    }

    class Inventario {
        +String idInventario
        +int cantidadDisponible
        +int stockMinimo
        +String ubicacion
        +actualizarStock(int cantidad)
    }

    class Producto {
        +String idProducto
        +String nombre
        +String descripcion
        +double precioBase
        +String categoria
        +actualizarPrecio(double nuevoPrecio)
    }

    Usuario <|-- Vendedor
    Usuario <|-- Administrador

    IGestionTienda <|.. Vendedor
    IGestionTienda <|.. Administrador

    Vendedor --> OrdenVenta : registra
    Administrador --> Inventario : ajusta
    Administrador --> Producto : ajusta
````

2. DESPUES (H2)
````mermaid
classDiagram
    class Usuario {
        +String id
        +String nombre
        +String email
        +String rol
        +autenticar() bool
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

    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +double total
    }

    class Inventario {
        +String idInventario
        +int cantidadDisponible
        +int stockMinimo
        +String ubicacion
        +actualizarStock(int cantidad)
    }

    class Producto {
        +String idProducto
        +String nombre
        +String descripcion
        +double precioBase
        +String categoria
        +actualizarPrecio(double nuevoPrecio)
    }

    Usuario <|-- Vendedor
    Usuario <|-- Administrador

    IRegistrador <|.. Vendedor
    IAjustadorStock <|.. Administrador
    IAjustadorPrecio <|.. Administrador

    Vendedor --> OrdenVenta : registra
    Administrador --> Inventario : ajusta
    Administrador --> Producto : ajusta

````
