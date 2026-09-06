# Herencia Mentirosa
La Herencia mentirosa detectada es EstadoVenta promete operaciones específicas que algunos estados no pueden realizar.
El Problema: Los hijos tendrían que rechazar operaciones, devolver errores o lanzar excepciones.

EstadoVenta debe contener solamente lo que absolutamente todos los estados pueden cumplir.

Si EstadoVenta promete: + cambiar(OrdenVenta orden), entonces todos los estados deberían poder cumplir correctamente ese contrato.
Si tenemos EstadoVenta 
1. ANTES (H1)
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

    EstadoVenta <|.. EstadoCarrito
    EstadoVenta <|.. EstadoConfirmada
    EstadoVenta <|.. EstadoPagada
````
2. DESPUES (H2)
````mermaid
classDiagram
    class EstadoVenta {
        <<interface>>
        +cambiar(OrdenVenta orden)
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

    EstadoVenta <|.. EstadoCarrito
    EstadoVenta <|.. EstadoConfirmada
    EstadoVenta <|.. EstadoPagada

    ProcesablePago <|.. EstadoConfirmada
    Entregable <|.. EstadoPagada
    Anulable <|.. EstadoConfirmada

````
