# C4 varainte: Tienda con Inventario 

# Nivel 1 - Contexto 
````mermaid
graph TB
    Vendedor["👤 Vendedor<br/>Registra ventas, consulta stock"]
    Administrador["👤 Administrador<br/>Gestiona inventario, precios y reportes"]
    Cliente["👤 Cliente<br/>Explora catálogo, consulta disponibilidad, compra"]

    Sistema["🏪 Sistema de Tienda con Inventario<br/>Registra ventas, controla stock,<br/>emite avisos y genera reportes"]

    Pasarela["💳 Pasarela de Pagos<br/>(externo)"]
    ProveedorNotif["📧 Proveedor de Notificaciones<br/>(email / SMS / WhatsApp)"]

    Vendedor -->|"Registra ventas,<br/>consulta stock"| Sistema
    Administrador -->|"Ajusta stock y precios,<br/>consulta reportes"| Sistema
    Cliente -->|"Consulta productos,<br/>realiza compras"| Sistema

    Sistema -->|"Procesa pagos"| Pasarela
    Sistema -->|"Envía avisos de stock"| ProveedorNotif
````
# Nivel 2 - Contenedores

````mermaid
graph TB
    %% ============ ACTORES ============
    Vendedor["👤 Vendedor"]
    Administrador["👤 Administrador"]
    Cliente["👤 Cliente"]

    %% ============ SISTEMA ============
    subgraph Sistema["🏪 Sistema de Tienda con Inventario"]

        %% --- Frontend / UI ---
        UI["🖥️ <b>Interfaz de Tienda</b><br/><i>Tecnología: .NET (Consola / Web)</i><br/><br/>• Registro de ventas<br/>• Consulta de catálogo<br/>• Panel de administración"]

        %% --- Aplicación principal ---
        App["⚙️ <b>Aplicación de Tienda</b><br/><i>Tecnología: .NET</i><br/><br/>• Lógica de negocio<br/>• Gestión de ventas<br/>• Gestión de catálogo<br/>• Control de usuarios y roles"]

        %% --- Módulo de avisos con FUSIÓN ---
        subgraph ModuloAvisos["🎯 Módulo de Avisos de Stock<br/>(FUSIÓN Observer + Factory)"]

            subgraph PatronObserver["🔔 Observer"]
                Sujeto["<b>Inventario</b><br/>(Sujeto)<br/>Mantiene lista de interesados"]
                ContratoObs["<b>IInteresadoEnStock</b><br/>(Contrato)"]
                ObsConcretos["<b>Observadores</b><br/>• AvisoAlCliente<br/>• AvisoAlAdministrador<br/>• RegistroDeAuditoria"]
            end

            subgraph PatronFactory["🏭 Factory"]
                Fabrica["<b>FabricaDeAvisos</b><br/>Decide qué tipo de aviso crear"]
                ContratoAviso["<b>IAviso</b><br/>(Contrato)"]
                AvisosConcretos["<b>Avisos concretos</b><br/>• AvisoCliente<br/>• AvisoAdministrador<br/>• AvisoAuditoria"]
            end

            Fusion["🔗 <b>Punto de fusión</b><br/>Los observadores piden a la fábrica<br/>el aviso correcto y lo envían"]
        end

        %% --- Módulo de reportes ---
        Reportes["📊 <b>Módulo de Reportes</b><br/><i>Tecnología: .NET</i><br/><br/>• Ventas del período<br/>• Productos más vendidos<br/>• Ranking por vendedor"]

        %% --- Persistencia ---
        BD[("🗄️ <b>Base de Datos</b><br/><i>Tecnología: SQL Server / PostgreSQL</i><br/><br/>Tablas:<br/>• Productos<br/>• Inventario<br/>• Ventas<br/>• DetalleOrden<br/>• Usuarios<br/>• Pagos")]
    end

    %% ============ SISTEMAS EXTERNOS ============
    Pasarela["💳 Pasarela de Pagos<br/>(externo)"]
    ProveedorNotif["📧 Proveedor de Notificaciones<br/>(externo)"]

    %% ============ RELACIONES ============
    Vendedor --> UI
    Administrador --> UI
    Cliente --> UI

    UI -->|"Envía comandos<br/>(registrar venta,<br/>ajustar stock)"| App

    App -->|"Consulta y persiste<br/>productos, ventas,<br/>usuarios"| BD

    App -->|"Dispara eventos<br/>de cambio de stock"| Sujeto
    Sujeto -.->|"Notifica a<br/>interesados suscritos"| ObsConcretos
    ObsConcretos -->|"Solicita el aviso<br/>correcto"| Fabrica
    Fabrica -->|"Devuelve<br/>IAviso"| ObsConcretos
    ObsConcretos -->|"Envía el aviso<br/>al canal"| ProveedorNotif

    App -->|"Solicita datos<br/>agregados"| Reportes
    Reportes -->|"Consulta"| BD

    App -->|"Procesa pagos"| Pasarela
    Pasarela -->|"Confirma<br/>transacción"| App

    %% ============ ESTILOS ============
    classDef actor fill:#e1f5ff,stroke:#0288d1,stroke-width:2px
    classDef sistema fill:#fff3e0,stroke:#f57c00,stroke-width:2px
    classDef modulo fill:#fff9c4,stroke:#f9a825,stroke-width:3px
    classDef patron fill:#c8e6c9,stroke:#2e7d32,stroke-width:2px
    classDef externo fill:#f3e5f5,stroke:#7b1fa2,stroke-width:2px
    classDef bd fill:#e0f2f1,stroke:#00695c,stroke-width:2px

    class Vendedor,Administrador,Cliente actor
    class UI,App,Reportes sistema
    class ModuloAvisos modulo
    class Sujeto,ContratoObs,ObsConcretos,Fabrica,ContratoAviso,AvisosConcretos,Fusion patron
    class Pasarela,ProveedorNotif externo
    class BD bd
````
