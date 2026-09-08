# Diagrama de clases del sistema DESPUES de las dos curas
**Nombre: Ronaldo Pablo Melgarejo Cardozo**

```mermaid
classDiagram
    %% CONTRATOS (ISP)
    class IRegistradorDePedidos {
        <<interface>>
        +RegistrarPedido(material string, cantidad int)
    }

    class IAutenticadorDeVentas {
        <<interface>>
        +AutorizarVentaAlPorMayor(material string)
    }

    class IGestorDePrecios {
        <<interface>>
        +AjustarPrecio(material string, nuevoPrecio decimal)
    }

    class IGeneradorDeReportes {
        <<interface>>
        +VerReporteDeCompras()
    }

    %% CONTRATOS (DIP)
    class IRepositorioDePedidos {
        <<interface>>
        +GuardarPedido(cliente string, material string, cantidad int, total decimal)
    }

    class ICanalDeNotificacion {
        <<interface>>
        +Enviar(mensaje string)
    }

    class ICalculadorDeDescuentos {
        <<interface>>
        +Calcular(tipoCliente string, total decimal) decimal
    }

    %% IMPLEMENTACIONES (ISP)
    class Vendedor {
        +RegistrarPedido(material string, cantidad int)
    }

    class Encargado {
        +RegistrarPedido(material string, cantidad int)
        +AutorizarVentaAlPorMayor(material string)
        +AjustarPrecio(material string, nuevoPrecio decimal)
        +VerReporteDeCompras()
    }

    %% IMPLEMENTACIONES (DIP)
    class RepositorioMySQL {
        +GuardarPedido(cliente string, material string, cantidad int, total decimal)
    }

    class NotificadorPorCorreo {
        +Enviar(mensaje string)
    }

    class CalculadorDeDescuentos {
        +Calcular(tipoCliente string, total decimal) decimal
    }

    %% COORDINADOR
    class GestorDePedidos {
        -_repositorio IRepositorioDePedidos
        -_canal ICanalDeNotificacion
        -_calculadorDescuentos ICalculadorDeDescuentos
        +GestorDePedidos(IRepositorioDePedidos, ICanalDeNotificacion, ICalculadorDeDescuentos)
        +ProcesarPedido(cliente string, tipoCliente string, material string, cantidad int, precioUnitario decimal)
    }

    %% RELACIONES
    IRegistradorDePedidos <|.. Vendedor
    IRegistradorDePedidos <|.. Encargado
    IAutenticadorDeVentas <|.. Encargado
    IGestorDePrecios <|.. Encargado
    IGeneradorDeReportes <|.. Encargado

    IRepositorioDePedidos <|.. RepositorioMySQL
    ICanalDeNotificacion <|.. NotificadorPorCorreo
    ICalculadorDeDescuentos <|.. CalculadorDeDescuentos

    GestorDePedidos --> IRepositorioDePedidos
    GestorDePedidos --> ICanalDeNotificacion
    GestorDePedidos --> ICalculadorDeDescuentos

````
