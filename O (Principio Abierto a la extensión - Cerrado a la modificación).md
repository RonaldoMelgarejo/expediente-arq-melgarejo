# Switch escondido
__¿Dónde está el switch?__
En el RF3 estado de venta (carrito → confirmada → pagada → entregada / anulada)
1. ANTES (H1 Switch escondido)
```mermaid
classDiagram
    class GestorDeEstadoVenta {
        +cambiarEstado(OrdenVenta orden, String nuevoEstado)
    }
````

2. DESPUES 

````mermaid
classDiagram
    class EstadoVenta {
        <<interface>>
        +cambiar(OrdenVenta orden)
    }

    class EstadoCarrito {
        +cambiar(OrdenVenta orden)
    }

    class EstadoConfirmada {
        +cambiar(OrdenVenta orden)
    }

    class EstadoPagada {
        +cambiar(OrdenVenta orden)
    }

    class OrdenVenta {
        -String idOrden
        -Date fecha
        -String estado
        -List~DetalleOrden~ detalles
    }

    EstadoVenta <|.. EstadoCarrito
    EstadoVenta <|.. EstadoConfirmada
    EstadoVenta <|.. EstadoPagada

    OrdenVenta --> EstadoVenta : utiliza
````

__Cuando el negocio pida el tipo nuevo, ¿qué clase nace y qué clase no se toca?__
Puede agregarse el tipo ENTREGADA, nacería la clase EstadoEntregada y no se tocaría las clases: EstadoCarrito, EstadoConfirmada, EstadoPagada, OrdenVenta
