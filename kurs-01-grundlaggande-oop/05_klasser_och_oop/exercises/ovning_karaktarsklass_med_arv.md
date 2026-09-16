# Övning — Karaktärsklass med arv

> Du löste det här problemet tidigare med en `switch`-sats i
> [`03_villkor_och_loopar/exercises/conditions/character_class.md`](../../03_villkor_och_loopar/exercises/conditions/character_class.md).
> Samma karaktärer, samma stats — men nu gör vi det med klasser och arv.
> Lägg dem sida vid sida och se vad som förändras.

🔴

---

## Bakgrund

I switch-övningen hårdkodade du stats direkt i varje `case`. Det funkade — men vad händer om du vill:

- Lägga till en sjätte karaktärsklass?
- Låta en karaktär attackera en annan?
- Spara en lista med flera karaktärer och skriva ut dem alla?

Med en `switch` sprider du logiken överallt. Med arv bor varje karaktär i sin egen klass och vet vad den ska göra.

---

## Del 1 — Basklass

Skapa en basklass `Karaktär` med dessa properties och en metod:

| Property | Typ |
|----------|-----|
| `Namn` | `string` |
| `Styrka` | `int` |
| `Intelligens` | `int` |
| `Smidighet` | `int` |

Metoden `PresenteraDig()` skriver ut karaktärens namn och stats i samma format som switch-övningens förväntade output.

---

## Del 2 — Underklasser

Skapa en underklass för varje karaktär. Varje underklass ärver från `Karaktär` och sätter sina stats i konstruktorn.

| Klass | Styrka | Intelligens | Smidighet |
|-------|--------|-------------|-----------|
| `Krigar` | 9 | 3 | 5 |
| `Trollkarl` | 2 | 10 | 4 |
| `Lomsk` | 5 | 6 | 9 |
| `Dvarg` | 8 | 5 | 3 |
| `Hobbit` | 3 | 7 | 8 |
| `Manniska` | 4 | 3 | 9 |

---

## Del 3 — Använd dem

Skapa en `List<Karaktär>` med en instans av varje klass.
Iterera listan och anropa `PresenteraDig()` på varje karaktär.

### Förväntad output

```plaintext
Du valde: Krigar
Styrka: 9
Intelligens: 3
Smidighet: 5

Du valde: Trollkarl
Styrka: 2
Intelligens: 10
Smidighet: 4

Du valde: Lömsk
Styrka: 5
Intelligens: 6
Smidighet: 9

Du valde: Dvärg
Styrka: 8
Intelligens: 5
Smidighet: 3

Du valde: Hobbit
Styrka: 3
Intelligens: 7
Smidighet: 8

Du valde: Människa
Styrka: 4
Intelligens: 3
Smidighet: 9
```

---

## Diskutera med din partner

- Vad händer i `List<Karaktär>` när du anropar `PresenteraDig()`? Vilken klass metod körs?
- Hur många rader kod behöver du ändra för att lägga till en sjätte karaktär?
- Jämför med switch-övningen — vad är enklare att underhålla?

---

<details><summary>Tips: basklass</summary>

```csharp
class Karaktär
{
    public string Namn { get; set; }
    public int Styrka { get; set; }
    public int Intelligens { get; set; }
    public int Smidighet { get; set; }

    public void PresenteraDig()
    {
        Console.WriteLine($"Du valde: {Namn}");
        Console.WriteLine($"Styrka: {Styrka}");
        Console.WriteLine($"Intelligens: {Intelligens}");
        Console.WriteLine($"Smidighet: {Smidighet}");
        Console.WriteLine();
    }
}
```

</details>

<details><summary>Tips: underklass</summary>

```csharp
class Krigar : Karaktär
{
    public Krigar()
    {
        Namn = "Krigar";
        Styrka = 9;
        Intelligens = 3;
        Smidighet = 5;
    }
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
class Karaktär
{
    public string Namn { get; set; }
    public int Styrka { get; set; }
    public int Intelligens { get; set; }
    public int Smidighet { get; set; }

    public void PresenteraDig()
    {
        Console.WriteLine($"Du valde: {Namn}");
        Console.WriteLine($"Styrka: {Styrka}");
        Console.WriteLine($"Intelligens: {Intelligens}");
        Console.WriteLine($"Smidighet: {Smidighet}");
        Console.WriteLine();
    }
}

class Krigar : Karaktär
{
    public Krigar() { Namn = "Krigar"; Styrka = 9; Intelligens = 3; Smidighet = 5; }
}

class Trollkarl : Karaktär
{
    public Trollkarl() { Namn = "Trollkarl"; Styrka = 2; Intelligens = 10; Smidighet = 4; }
}

class Lomsk : Karaktär
{
    public Lomsk() { Namn = "Lömsk"; Styrka = 5; Intelligens = 6; Smidighet = 9; }
}

class Dvarg : Karaktär
{
    public Dvarg() { Namn = "Dvärg"; Styrka = 8; Intelligens = 5; Smidighet = 3; }
}

class Hobbit : Karaktär
{
    public Hobbit() { Namn = "Hobbit"; Styrka = 3; Intelligens = 7; Smidighet = 8; }
}

class Manniska : Karaktär
{
    // Politician + sports freak
    public Manniska() { Namn = "Människa"; Styrka = 4; Intelligens = 3; Smidighet = 9; }
}

// Program
var karaktärer = new List<Karaktär>
{
    new Krigar(),
    new Trollkarl(),
    new Lomsk(),
    new Dvarg(),
    new Hobbit(),
    new Manniska()
};

foreach (var k in karaktärer)
{
    k.PresenteraDig();
}
```

</details>
