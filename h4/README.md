# C4 varainte: Tienda con Inventario 

# Nivel 1 - Contexto 
````mermaid
flowchart TB

    vendedor["👤 Vendedor<br>(registra y procesa ventas)"]
    admin["👤 Administrador<br>(gestiona productos, precios y stock)"]
    cliente["👤 Cliente<br>(consulta productos y recibe avisos)"]

    sistema["🛒 SISTEMA DE TIENDA CON INVENTARIO<br>Gestiona ventas, productos, inventario<br>y pagos"]

    correo["📧 Servicio de correo<br>(sistema externo)"]
    pasarela["💳 Pasarela de pagos<br>(sistema externo)"]

    vendedor -->|"registra ventas"| sistema
    admin -->|"gestiona productos y existencias"| sistema
    cliente -->|"consulta productos y realiza compras"| sistema

    sistema -->|"envía comprobantes y avisos"| correo
    correo -->|"entrega notificaciones"| cliente

    sistema -->|"solicita pagos"| pasarela
    pasarela -->|"devuelve resultado del pago"| sistema
````
# Nivel 2 - Contenedores

````mermaid
flowchart TB

    vendedor["👤 Vendedor"]
    admin["👤 Administrador"]
    cliente["👤 Cliente"]

    subgraph sistema["🛒 SISTEMA DE TIENDA CON INVENTARIO"]

        web["🌐 Aplicación web<br>C# <br>Ventas, catálogo, inventario y consultas"]

        backend["⚙️ Backend de tienda<br>C# <br>Ventas, productos, inventario y pagos"]

        bd[("🗄️ Base de datos<br>SQL<br>Productos, categorías, inventarios,<br>órdenes, detalles y pagos")]

        notificaciones["🔔 Servicio de notificaciones<br>C#<br>Gestiona avisos de stock mínimo<br>y notificaciones a usuarios"]
    end

    pagos["💳 Pasarela de pagos<br>(externa)"]
    correo["📧 Servicio de correo<br>(externo)"]

    vendedor -->|"registra ventas"| web
    admin -->|"gestiona productos y stock"| web
    cliente -->|"consulta productos y compras"| web

    web -->|"solicita operaciones"| backend

    backend -->|"consulta y almacena"| bd

    backend -->|"notifica stock mínimo"| notificaciones
    notificaciones -->|"envía avisos"| correo

    backend -->|"procesa pagos"| pagos
    pagos -->|"resultado del pago"| backend
````
