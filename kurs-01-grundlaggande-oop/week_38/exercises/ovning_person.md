# Övning — Personen 🧍

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Receptionen på ett litet hotell skriver för hand på varje välkomstkort: "Välkommen, [Förnamn] [Efternamn]!" Det tar tid och det blir ofta fel. Du ska automatisera det.

Hotellet vill ha ett program som tar emot ett förnamn och ett efternamn och kan skriva ut ett välkomsthälsning på ett konsekvent sätt.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med privata fält och en property
- [ ] Förstår hur en property kan kombinera data från flera fält

---

## Uppgift

Skapa en klass `Person` med:

**Privata fält:**
- `_förnamn` (string)
- `_efternamn` (string)

**Property** (publik get, privat set):
- `FullständigtNamn` — returnerar `_förnamn` och `_efternamn` ihopsatta med ett mellanslag

**Konstruktor** som tar in förnamn och efternamn

**Metod:**
- `Välkomna()` — skriver ut `"Välkommen, [FullständigtNamn]!"`

**I `Main()`:**
- Skapa tre gäster — en av dem är `Jack Leopards`
- Välkomna varje gäst

---

## Exempeloutput

```
Välkommen, Anna Svensson!
Välkommen, Jack Leopards!
Välkommen, Priya Nilsson!
```

---

## Tips

`FullständigtNamn` kan antingen sättas i konstruktorn eller beräknas varje gång via en expression-bodied property:

```csharp
public string FullständigtNamn => $"{_förnamn} {_efternamn}";
```

Båda fungerar — välj det som känns tydligast.

---

## Klar snabbt? Utmaning 🔴

Hotellet vill också kunna skriva ut ett kort med initialer: "A.S." för Anna Svensson.  
Lägg till en **andra property** `Initialer` som returnerar första bokstaven av varje namn med punkt emellan.  
Inget nytt fält — bara en property som arbetar med de som redan finns.

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Person
{
    private string _förnamn;
    private string _efternamn;

    public string FullständigtNamn => $"{_förnamn} {_efternamn}";

    public Person(string förnamn, string efternamn)
    {
        _förnamn = förnamn;
        _efternamn = efternamn;
    }

    public void Välkomna()
    {
        Console.WriteLine($"Välkommen, {FullständigtNamn}!");
    }
}

class Program
{
    static void Main()
    {
        Person gäst1 = new Person("Anna", "Svensson");
        Person gäst2 = new Person("Jack", "Leopards");
        Person gäst3 = new Person("Priya", "Nilsson");

        gäst1.Välkomna();
        gäst2.Välkomna();
        gäst3.Välkomna();
    }
}
```

**Utmaning — Initialer:**

```csharp
public string Initialer => $"{_förnamn[0]}.{_efternamn[0]}.";
```

</details>
