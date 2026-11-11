# Övning — Avengers-registret

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

S.H.I.E.L.D. behöver ett system för att hålla koll på sina aktiva hjältar.
Nick Fury har tid för kod, men inte för dålig kod.

Din uppgift är att bygga ett enkelt hjälteregister med en array.

---

## Vad gäller för den här övningen

- [ ] Kan skapa och initiera en array
- [ ] Kan loopa igenom en array och skriva ut varje element
- [ ] Kan söka i en array och hitta ett specifikt element
- [ ] Förstår vad `array.Length` är

---

## Uppgift

Skapa en array med sex hjältar: `"Iron Man"`, `"Captain America"`, `"Thor"`, `"Black Widow"`, `"Hulk"`, `"Hawkeye"`.

**Metod 1: `VisaAlla(string[] roster)`**
Skriv ut alla hjältar med deras position (börja räkna från 1, inte 0).

**Metod 2: `HittaHjalte(string[] roster, string namn)`**
Sök igenom arrayen. Om hjälten finns: skriv ut positionen. Om inte: skriv ut att de inte hittades.

**Metod 3: `AntalHjaltar(string[] roster)`**
Skriv ut hur många hjältar som finns i registret.

Anropa alla tre metoderna i `Main()`.

---

## Exempeloutput

```
Tillgängliga hjältar:
1. Iron Man
2. Captain America
3. Thor
4. Black Widow
5. Hulk
6. Hawkeye

Söker efter Thor...
Thor hittades på position 3.

Söker efter Loki...
Loki finns inte i registret.

Antal hjältar: 6
```

---

## Klar snabbt? Utmaning 🔴

Lägg till en fjärde metod `VisaOmvänt(string[] roster)` som skriver ut listan baklänges.

<details>
<summary>Tips</summary>

Börja loopen på `roster.Length - 1` och räkna nedåt till och med `0`.

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
class Program
{
    static void Main()
    {
        string[] roster = { "Iron Man", "Captain America", "Thor", "Black Widow", "Hulk", "Hawkeye" };

        VisaAlla(roster);
        Console.WriteLine();

        HittaHjalte(roster, "Thor");
        HittaHjalte(roster, "Loki");
        Console.WriteLine();

        AntalHjaltar(roster);
    }

    static void VisaAlla(string[] roster)
    {
        Console.WriteLine("Tillgängliga hjältar:");
        for (int i = 0; i < roster.Length; i++)
            Console.WriteLine($"{i + 1}. {roster[i]}");
    }

    static void HittaHjalte(string[] roster, string namn)
    {
        Console.WriteLine($"Söker efter {namn}...");
        for (int i = 0; i < roster.Length; i++)
        {
            if (roster[i] == namn)
            {
                Console.WriteLine($"{namn} hittades på position {i + 1}.");
                return;
            }
        }
        Console.WriteLine($"{namn} finns inte i registret.");
    }

    static void AntalHjaltar(string[] roster)
    {
        Console.WriteLine($"Antal hjältar: {roster.Length}");
    }
}
```

</details>
