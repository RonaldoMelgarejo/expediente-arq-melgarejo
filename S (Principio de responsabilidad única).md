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
        +double total
        +asociarDetalle(DetalleOrden detalle)
    }

    class CalculadoraDePrecios {
        +calcularSubtotal(DetalleOrden detalle) double
        +calcularImpuestos(double subtotal) double
        +calcularTotal(List~DetalleOrden~ detalles) double
    }

    class RegistroDeVentas {
        +guardar(OrdenVenta orden) bool
        +buscarPorId(String idOrden) OrdenVenta
        +actualizarEstado(String idOrden, String nuevoEstado) bool
    }

    class GestorDeVentas {
        +procesarVenta(OrdenVenta orden) bool
        +cancelarVenta(String idOrden) bool
    }

    %% Relaciones
    GestorDeVentas --> CalculadoraDePrecios : usa para calcular
    GestorDeVentas --> RegistroDeVentas : usa para guardar
    GestorDeVentas --> OrdenVenta : opera sobre
    CalculadoraDePrecios ..> OrdenVenta : lee detalles de
    RegistroDeVentas ..> OrdenVenta : persiste
````

**Quien pedira cambios?**
* OrdenVenta: Finanzas / Contabilidad
* RegistroDeVentas: Infraestructura / TI (DBA)
