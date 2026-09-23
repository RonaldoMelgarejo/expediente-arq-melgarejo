# Patrón aplicado — Observer

**Nombre:** Ronaldo Melgarejo

__Patrón aplicado: Observer__

**Por qué Observer y no otro:**
- **Strategy** sería si hubiera varias formas de calcular el aviso. No es el caso: el aviso es uno solo.
- **Factory** sería si hubiera que crear distintos tipos de avisos. Acá no hay decisión de creación.
- **Decorator** sería si hubiera que agregar capas al aviso. Acá el aviso es plano.
- **Observer** es el correcto: la estadía publica un evento estadia-larga, y los interesados se suscriben a escucharlo.


**Código corto del diseño:**

```csharp
public interface IObservadorDeEstadias
{
    void CuandoEstadiaLarga(Estadia estadia);
}

public class ServicioDeAvisos
{
    private readonly List<IObservadorDeEstadias> _observadores = new();

    public void Suscribir(IObservadorDeEstadias o) => _observadores.Add(o);

    public void PublicarEstadiaLarga(Estadia estadia)
    {
        if (!estadia.EsLarga()) return;
        foreach (var o in _observadores)
            o.CuandoEstadiaLarga(estadia);
    }
}

public class AvisoAlDuenio : IObservadorDeEstadias
{
    public void CuandoEstadiaLarga(Estadia estadia)
        => Console.WriteLine($"[AVISO] 📱 Estimado dueño de {estadia.Placa}: su vehículo lleva más de 24 h.");
}
```

__Justificación__

**Por qué ESE patrón:**
- El requerimiento habla de avisar cuando ocurre un evento de mas de 24 h.
- El que anuncia no debe conocer a los que escuchan como al dueño, seguridad, etc..
- Los interesados pueden cambiar con el tiempo, hoy solo el dueño, mañana quizás varios.
- Cada interesado nuevo debe poder suscribirse sin tocar el código del anunciador.

**Qué pasa SIN Observer:**
- La estadía tendría que conocer al dueño, a seguridad y acoplándose a todos.
- Cada interesado nuevo obligaría a abrir la clase Estadia.
- Viola OCP + DIP a la vez.

**Con Observer:**
- La estadía **publica** el evento; los interesados **se suscriben**.
- Un interesado nuevo = **una clase nueva**, sin tocar al anunciador.
- Se puede probar con un observador falso.
- Respeta OCP + DIP.
