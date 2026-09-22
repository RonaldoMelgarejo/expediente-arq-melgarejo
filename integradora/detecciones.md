# Detección de las 3 violaciones SOLID

**Nombre:** Ronaldo Pablo Melgarejo Cardozo

__Violación 1 — SRP (Responsabilidad Unica)__

**Dónde:** Existe la violación en la clase GestorDeEstadias, en el método RegistrarSalida.

**Por qué:** la clase esta sobre cargada ya que tiene cuatro razones para cambiar: 1. Calcula la tarifa, 2. Guardar en la BD, 3. Imprimir el ticket, 4. Envía mensajes por WhatsApp; Donde cualquier modificación solicitada por alguna área obligará a modificar esta misma clase, lo que incrementa el riesgo de afectar al resto de las funcionalidades.


__Violación 2 — OCP (Abierto a extension y cerrado a modificacion)__

**Dónde:** Existe la violación en el método RegistrarSalida en el switch (tipoVehiculo) de la clase GestorDeEstadias

**Por qué:** el switch puede crecer con cada tipo nuevo de vehículo, hoy tiene 3 tipos auto, moto, residente, si mañana aparece bicicleta o camión, hay que abrir esta clase y agregar un case nuevo, por lo cual la clase esta abierta a modificación y no cumpliría con el principio de cerrada a modificaciones.


__Violación 3 — DIP (Inversion de Dependencias)__

**Dónde:** La violación esta en la clase GestorDeEstadias en el método RegistrarSalida, en las líneas: 
   var baseDeDatos = new BaseDeDatosParqueo(); y var whatsapp = new WhatsAppDelEdificio();

**Por qué:** la clase crea sus propias dependencias utilizando new, por lo que queda atada a una implementación concreta. Si cambia la base de datos o el canal de notificación, es obligatorio modificar la clase.
