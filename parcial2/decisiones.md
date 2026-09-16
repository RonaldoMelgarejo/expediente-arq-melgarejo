
**Nombre:** Ronaldo Pablo Melgarejo Cardozo
# P2.1 Elegir y justificar
* __Situación 1.:__

Patron: Observer

Por que?: El problema es que el módulo de préstamos conoce a todos los interesados y los llama uno por uno como a al correo, al registro de morosidad y a la pantalla de recepción. Cada vez que aparece un interesado nuevo, hay que abrir el módulo de préstamos y agregar otra llamada.
Con Observer, el módulo de préstamos solo notifica a una lista. Los interesados se suscriben por su cuenta. Si llega el sistema de multas, se suscribe y listo. Si mañana llega otro, también. El módulo no se toca nunca más.

Por qué no otro: Strategy no aplica porque no hay que elegir un algoritmo, hay que avisar a varios. Factory crea objetos, no gestiona suscripciones. Adapter traduce interfaces, no notifica.

* __Situación 2.:__

Patron: Strategy

Por que?: La multa se calcula diferente o distinto según el tipo de socio como infantil no paga, adulto paga 2Bs por día, y tercera edad 1Bs por día. Donde este calculo esta en un if/else dentro del modulo de préstamo y esta copiado en el modulo de reportes. Ademas el consejo municipal cambia las reglas cada año y esto significa que cada cambio requiere modificar múltiples clases y volver a probar todo.
Por lo cual con Strategy, cada regla de cálculo es una clase aparte. El módulo de préstamos recibe la regla por constructor y la aplica sin saber cuál es. Si el concejo agrega un nuevo tipo de socio, creamos una clase nueva y no tocamos nada de lo que ya funcionaba.

Por que no otro: Factory crea objetos, pero no encapsula fórmulas de cálculo. Builder arma objetos paso a paso, no elige reglas. Observer notifica cambios, no calcula multas.

* __Situación 3.:__

Patron: Adapter

Por que?: El Sistema Estatal es un servicio externo que no podemos modificar, sus métodos están en inglés `PushRecord`, `isoDate`, `originCode`, las fechas vienen en otro formato y los códigos no son los nuestros. Si dejamos que nuestro catálogo hable directamente con él, nuestro código se contamina con traducciones por todos lados y si el servicio estatal cambia de versión, hay que modificar nuestro catálogo.
Con Adapter, creamos un traductor en la frontera. Nuestro dominio define su contrato en su idioma como español, fechas locales, códigos propios. El adaptador envuelve al servicio externo y traduce las llamadas. Si el servicio estatal cambia, solo se toca el adaptador. Nuestro catálogo ni se entera.

Por que no otro: Factory crea objetos, no traduce interfaces. Strategy elige algoritmos, no adapta servicios externos. Observer notifica cambios, no integra sistemas.



# P2.3 La Conexión SOLID
La implementación que elegí en el P2.2 es la situación 2 con patrón Strategy

Los principios SOLID que rescata son: OCP (abierto a extensión y cerrado a modificación) y DIP (inversión de dependencias)

El principio a la vista es OCP: en mi código, la `CalculadoraDeMultas` no tiene ningún `if`. Si el concejo agrega un nuevo tipo de socio, solo creo una clase nueva que implemente `IReglaDeMulta` y no toco nada de lo que ya funciona. Eso está en la línea de la interfaz `IReglaDeMulta` y en cómo cada regla es una clase aparte (`MultaInfantil`, `MultaAdulto`, `MultaTerceraEdad`).

También se ve DIP en el constructor: `public CalculadoraDeMultas(IReglaDeMulta regla)`. La calculadora no crea la regla con `new`, la recibe desde afuera. Depende de la abstracción, no de las clases concretas. Por eso puedo cambiar la regla sin abrir la calculadora.
