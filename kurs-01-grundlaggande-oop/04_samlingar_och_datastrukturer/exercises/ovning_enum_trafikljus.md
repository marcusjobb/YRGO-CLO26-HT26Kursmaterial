# Övning — Trafikljuset

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Stadsingenjören i Göteborg behöver ett program för att simulera ett trafikljus.
Systemet ska visa rätt instruktion till föraren beroende på vilket ljus som lyser.
Trafiken på Avenyn är kaotisk nog som det är — ett stavfel i en sträng ska inte göra det värre.

---

## Vad gäller för den här övningen

- [ ] Kan deklarera en enum utanför klassen
- [ ] Kan använda enum-värden i en switch-sats
- [ ] Förstår varför enum är säkrare än en sträng för fasta alternativ

---

## Uppgift

Skapa enumen `Trafikljus` utanför klassen:

```csharp
enum Trafikljus { Röd, Gul, Grön }
```

**Metod `VisaInstruktion(Trafikljus ljus)`**  
Använd en switch-sats. Skriv ut rätt instruktion:

- `Röd` → `"Stanna! Rött ljus."`
- `Gul` → `"Var beredd att stanna. Gult ljus."`
- `Grön` → `"Kör! Grönt ljus."`

Anropa metoden med alla tre värden i `Main()`.

---

## Exempeloutput

```
Stanna! Rött ljus.
Var beredd att stanna. Gult ljus.
Kör! Grönt ljus.
```

---

## Klar snabbt? Utmaning 🔴

Simulera en full trafikljuscykel med `Thread.Sleep(1000)`.  
Skriv `"Byt!"` mellan varje ljusbyte. Loopa tre gånger.

<details>
<summary>Tips</summary>

`Thread.Sleep(1000)` pausar programmet i 1 sekund. Du behöver ingen using — det räcker med `System.Threading.Thread.Sleep(1000)` eller lägg till `using System.Threading;` överst.

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
enum Trafikljus { Röd, Gul, Grön }

class Program
{
    static void Main()
    {
        VisaInstruktion(Trafikljus.Röd);
        VisaInstruktion(Trafikljus.Gul);
        VisaInstruktion(Trafikljus.Grön);
    }

    static void VisaInstruktion(Trafikljus ljus)
    {
        switch (ljus)
        {
            case Trafikljus.Röd:
                Console.WriteLine("Stanna! Rött ljus.");
                break;
            case Trafikljus.Gul:
                Console.WriteLine("Var beredd att stanna. Gult ljus.");
                break;
            case Trafikljus.Grön:
                Console.WriteLine("Kör! Grönt ljus.");
                break;
        }
    }
}
```

</details>
