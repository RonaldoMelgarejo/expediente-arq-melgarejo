# Clase sobre cargada

Clase OrdenVenta:
* Almacena los datos de la venta (idOrden, fecha, estado, total)
* Calcula el total
* Transición de Estado

1. ANTES (H1: Clase Sobrecargada)
```mermaid
classDiagram
    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +double total
        +calcularTotal() double
        +cambiarEstado(String nuevoEstado)
        +guardarEnBaseDatos() bool
    }
````
2. DESPUÉS (H2: Aplicando SRP)
```mermaid
classDiagram
    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +List~DetalleOrden~ detalles
    }

    class CalculadorDeVenta {
        +calcularTotal(OrdenVenta orden) double
    }

    class GestorDeEstadoVenta {
        +cambiarEstado(OrdenVenta orden, String nuevoEstado)
    }

    class RepositorioDeOrdenVenta {
        +guardar(OrdenVenta orden)
        +buscar(String idOrden) OrdenVenta
        +actualizar(OrdenVenta orden)
        +eliminar(String idOrden)
    }

    class DetalleOrden {
        +int cantidad
        +double precioUnitario
        +double subtotal
        +calcularSubtotal() double
    }

    OrdenVenta "1" *-- "1..*" DetalleOrden : contiene
    CalculadorDeVenta ..> OrdenVenta : calcula
    GestorDeEstadoVenta ..> OrdenVenta : gestiona estado
    RepositorioDeOrdenVenta ..> OrdenVenta : persiste
````

**Quien pedira cambios?**
* OrdenVenta: Finanzas / Contabilidad
* RegistroDeVentas: Infraestructura / TI (DBA)
