# Övning — Kafé Nionit

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Kafé Nionit har precis öppnat på Campus Mölndal.
Kassören behöver ett system: kunden väljer en dryck, systemet slår upp priset och skriver ut ett kvitto.
Menyn lagras i ett Dictionary — det gör det enkelt att lägga till nya drycker utan att ändra logiken.

---

## Vad gäller för den här övningen

- [ ] Kan kombinera enum med Dictionary som nyckeltyp
- [ ] Kan skriva en metod som slår upp ett värde i ett Dictionary
- [ ] Kan formatera ett kvitto med Console.WriteLine och string interpolation

---

## Uppgift

Skapa enumen `Dryck` utanför klassen:

```csharp
enum Dryck { Kaffe, Te, Choklad, Juice, Smoothie }
```

Skapa ett `Dictionary<Dryck, decimal>` med priser i `Main()`:

| Dryck    | Pris   |
|----------|--------|
| Kaffe    | 29 kr  |
| Te       | 25 kr  |
| Choklad  | 35 kr  |
| Juice    | 39 kr  |
| Smoothie | 55 kr  |

**Metod `decimal HamtaPris(Dictionary<Dryck, decimal> meny, Dryck val)`**  
Returnerar priset för vald dryck.

**Metod `void SkrivKvitto(Dryck val, decimal pris)`**  
Skriver ut ett kvitto i det här formatet:

```
--- Kvitto ---
Dryck:   Kaffe
Pris:    29,00 kr
Tack och välkommen!
```

Testa tre olika drycker i `Main()`. Lämna en tom rad mellan kvittona.

---

## Exempeloutput

```
--- Kvitto ---
Dryck:   Kaffe
Pris:    29,00 kr
Tack och välkommen!

--- Kvitto ---
Dryck:   Smoothie
Pris:    55,00 kr
Tack och välkommen!

--- Kvitto ---
Dryck:   Te
Pris:    25,00 kr
Tack och välkommen!
```

---

## Klar snabbt? Utmaning 🔴

Lägg till `int antal` som parameter till `SkrivKvitto()`.  
Beräkna totalpriset och skriv ut det:

```
--- Kvitto ---
Dryck:   Kaffe
Antal:   2
Pris:    2 x 29,00 kr = 58,00 kr
Tack och välkommen!
```

<details>
<summary>Tips</summary>

`decimal totalt = pris * antal;`  
Formatera decimal med två decimaler: `$"{totalt:F2} kr"`.  
`Dryck.Kaffe.ToString()` ger strängen `"Kaffe"` — använd det i kvittot.

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
enum Dryck { Kaffe, Te, Choklad, Juice, Smoothie }

class Program
{
    static void Main()
    {
        var meny = new Dictionary<Dryck, decimal>
        {
            { Dryck.Kaffe,    29m },
            { Dryck.Te,       25m },
            { Dryck.Choklad,  35m },
            { Dryck.Juice,    39m },
            { Dryck.Smoothie, 55m }
        };

        SkrivKvitto(Dryck.Kaffe,    HamtaPris(meny, Dryck.Kaffe));
        Console.WriteLine();
        SkrivKvitto(Dryck.Smoothie, HamtaPris(meny, Dryck.Smoothie));
        Console.WriteLine();
        SkrivKvitto(Dryck.Te,       HamtaPris(meny, Dryck.Te));
    }

    static decimal HamtaPris(Dictionary<Dryck, decimal> meny, Dryck val)
    {
        return meny[val];
    }

    static void SkrivKvitto(Dryck val, decimal pris)
    {
        Console.WriteLine("--- Kvitto ---");
        Console.WriteLine($"Dryck:   {val}");
        Console.WriteLine($"Pris:    {pris:F2} kr");
        Console.WriteLine("Tack och välkommen!");
    }
}
```

</details>
