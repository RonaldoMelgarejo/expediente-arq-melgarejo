# New Peligroso

__¿dónde tu negocio fabrica sus detalles?__
El problema aparece cuando OrdenVenta se encarga directamente de guardar sus datos en una base de datos concreta.

1. ANTES (H1)
````mermaid
classDiagram
    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +List~DetalleOrden~ detalles
        +guardar()
    }

    class RepositorioOrdenVentaMySQL {
        +guardar(OrdenVenta orden)
    }

    class MySQL {
        +conectar()
        +ejecutar()
    }

    OrdenVenta --> RepositorioOrdenVentaMySQL : crea con new
    RepositorioOrdenVentaMySQL --> MySQL
````
2. DESPUES (H2)
````mermaid
classDiagram
    class OrdenVenta {
        +String idOrden
        +Date fecha
        +String estado
        +List~DetalleOrden~ detalles
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

    OrdenVenta --> IRepositorioDeOrdenVenta : depende de

    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaMySQL
    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaPostgreSQL
    IRepositorioDeOrdenVenta <|.. RepositorioOrdenVentaMemoria
````

__¿Podría probar esta regla sin el detalle real conectado?__
Puedo utilizar RepositorioOrdenVentaMemoria, que implementa IRepositorioDeOrdenVenta, para probar el comportamiento sin conectar una base de datos real.
