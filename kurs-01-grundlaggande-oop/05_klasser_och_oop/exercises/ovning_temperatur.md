# Övning — Termometern 🌡️

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Göteborgs väderstation har fått ett nytt problem: de amerikanska turisterna begriper sig inte på Celsius. Stationen behöver visa temperaturen i båda enheterna — och de har anlitat dig.

Du ska bygga en digital termometer som vet var den sitter och kan presentera temperaturen på båda sidor av Atlanten.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med ett privat fält och en publik property
- [ ] Förstår hur man lagrar ett värde och returnerar en beräknad version av det
- [ ] Kan skapa ett objekt med konstruktor och anropa metoder på det

---

## Uppgift

Skapa en klass `Termometer` med:

**Privat fält:**
- `_temperaturCelsius` (double)

**Property** (publik get, privat set):
- `Plats` (string) — sätts i konstruktorn

**Konstruktor** som tar in `plats` (string) och `startTemp` (double)

**Metoder:**
- `SättTemperatur(double celsius)` — uppdaterar det privata fältet
- `HämtaFahrenheit()` — returnerar temperaturen i Fahrenheit
- `VisaAvläsning()` — skriver ut plats, Celsius och Fahrenheit

**I `Main()`:**
- Skapa minst två termometrar på olika platser
- Sätt temperaturer och anropa `VisaAvläsning()` på varje

---

## Tips

**Celsius → Fahrenheit:**

```csharp
double fahrenheit = celsius * 9.0 / 5.0 + 32;
```

**Formatera decimaler i utskrift:**

```csharp
Console.WriteLine($"{värde:F1}");  // en decimal, t.ex. 22,5
```

---

## Exempeloutput

```
📍 Göteborg: 18,0°C (64,4°F)
📍 Sälen: -12,0°C (10,4°F)
📍 Göteborg: 25,5°C (77,9°F)
```

---

## Att fundera på

- Varför lagrar vi temperaturen som Celsius internt och konverterar i `HämtaFahrenheit()`? Vad hade hänt om vi gjort tvärtom?
- `Plats` har `private set` — vad betyder det att stationen inte kan byta plats efter att den skapats?
- Vad händer om du skickar in `-300` som temperatur — borde klassen bry sig om det?

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Termometer
{
    private double _temperaturCelsius;

    public string Plats { get; private set; }

    public Termometer(string plats, double startTemp)
    {
        Plats = plats;
        _temperaturCelsius = startTemp;
    }

    public void SättTemperatur(double celsius)
    {
        _temperaturCelsius = celsius;
    }

    public double HämtaFahrenheit()
    {
        return _temperaturCelsius * 9.0 / 5.0 + 32;
    }

    public void VisaAvläsning()
    {
        Console.WriteLine($"📍 {Plats}: {_temperaturCelsius:F1}°C ({HämtaFahrenheit():F1}°F)");
    }
}

class Program
{
    static void Main()
    {
        Termometer göteborg = new Termometer("Göteborg", 18.0);
        Termometer sälen = new Termometer("Sälen", -12.0);

        göteborg.VisaAvläsning();
        sälen.VisaAvläsning();

        göteborg.SättTemperatur(25.5);
        göteborg.VisaAvläsning();
    }
}
```

`_temperaturCelsius` är privat — ingen utifrån kan råka sätta temperaturen till `double.MaxValue`. All uppdatering går via `SättTemperatur()`, som kan byggas ut med validering när behovet uppstår.

</details>
