# expediente-arq-melgarejo
__Arquitectura de Software__

Ronaldo Melgarejo - **Variante:** Comercio (Tienda con inventario)

## 1. Actores del Sistema
* **Vendedor:** Quiere registrar ventas, consultar la disponibilidad de productos y emitir la transacción.
* **Administrador:** Quiere gestionar el inventario y visualizar reportes de ventas.
* **Cliente:** Explora el catálogo de productos, consulta la disponibilidad, realiza compras.

## 2. Inventario de Módulos (Cajas del Sistema)
1. **Módulo de Seguridad y Autenticación:** Gestiona los roles y permisos de vendedores y administradores.
2. **Módulo de Catálogo e Inventario:** Gestiona la información de productos, procesa las entradas y salidas de almacén, calcula el stock disponible.
3. **Módulo de Ventas:** Procesa el carrito de compras, registra la transacción y actualiza el inventario.
4. **Módulo de Reportes:** Genera consolidados de ventas del período y listas de productos más vendidos.
5. **Módulo de Facturación y Pagos:** Se integra con la pasarela de pagos, registra las transacciones confirmadas, genera los comprobantes de venta y gestiona estados de pago.

## 3. Primer Diagrama de Clases (Receta de 4 Pasos)
```mermaid
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
        +calcularTotal() double
        +cambiarEstado(String nuevoEstado)
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

    %% Relaciones UML
    Producto "1" -- "1" Inventario : gestiona
    Usuario "1" -- "*" OrdenVenta : realiza / procesa
    OrdenVenta "1" *-- "1..*" DetalleOrden : contiene
    DetalleOrden "*" -- "1" Producto : referencia a
    OrdenVenta "1" -- "1" Pago : liquida con
````
## 4. Atributos de Calidad Críticos
* Idoneidad Funcional: El cálculo de montos y el conteo del inventario no deben fallar ni permitir vender productos sin stock disponible.
* Usabilidad: La interfaz de registro de ventas debe responder rápido para operar con agilidad en momentos de alta demanda.






