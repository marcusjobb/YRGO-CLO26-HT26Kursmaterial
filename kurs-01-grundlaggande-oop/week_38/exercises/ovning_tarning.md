# Övning — Tärningen 🎲

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Lasse och Emma spelar ett brädspel. Tärningen försvann under soffan för tre rundor sedan. Emma påstår att de kan lösa det med kod. Lasse är skeptisk men nyfiken.

Du ska bygga tärningen.

---

## Vad gäller för den här övningen

- [ ] Kan skriva en klass med ett privat fält och en publik metod
- [ ] Förstår varför `Random` ska vara `static`
- [ ] Kan skapa ett objekt med `new` och anropa en metod

---

## Uppgift

Skapa en klass `Tärning` med:

**Privat statiskt fält:**
- `_random` — en `static Random` (delas av alla tärningsobjekt)

**Metod:**
- `Kasta()` — returnerar ett slumpmässigt tal mellan 1 och 6 (inklusive) och skriver ut resultatet

**I `Main()`:**
- Skapa en tärning
- Kasta den tre gånger
- Skriv ut summan

---

## Exempeloutput

```
Tärning 1: 4
Tärning 2: 2
Tärning 3: 6
Summan: 12
```

---

## Att fundera på

- Varför är `_random` deklarerad som `static`?
- Vad skulle hända om den inte var det?
- Hur skulle du ändra koden för att simulera en T20 (tjugosidig tärning)?

---

## Klar snabbt? Utmaning 🔴

Skapa tre separata tärningsobjekt istället för att kasta samma tre gånger.  
Resultatet ska bli detsamma — men varför fungerar det ändå, trots tre separata objekt?

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Tärning
{
    private static Random _random = new Random();

    public int Kasta()
    {
        int resultat = _random.Next(1, 7);
        Console.WriteLine($"Tärning: {resultat}");
        return resultat;
    }
}

class Program
{
    static void Main()
    {
        Tärning tärning = new Tärning();
        int summa = 0;

        for (int i = 1; i <= 3; i++)
        {
            summa += tärning.Kasta();
        }

        Console.WriteLine($"Summan: {summa}");
    }
}
```

`_random` är `static` för att alla tärningsobjekt delar samma slumpgenerator — annars riskerar du att få samma tal om objekten skapas tätt inpå varandra.

</details>

## Extra övning

Om du får för dig att bygga en riktig RPG-miljö och behöver T6, T12 och T20 — hur löser du det?

Det finns två vägar. Båda funkar. Bara en av dem är snygg.

<details>
<summary>Lösning 1 — en klass per tärning</summary>

**Fördel:** snygga klassnamn  
**Nackdel:** bryter mot DRY — tre klasser med identisk kod

```csharp
class TärningT6
{
    private static Random _random = new Random();
    public int Värde { get; private set; }

    public int Kasta()
    {
        Värde = _random.Next(1, 7);
        return Värde;
    }
}

class TärningT12
{
    private static Random _random = new Random();
    public int Värde { get; private set; }

    public int Kasta()
    {
        Värde = _random.Next(1, 13);
        return Värde;
    }
}

class TärningT20
{
    private static Random _random = new Random();
    public int Värde { get; private set; }

    public int Kasta()
    {
        Värde = _random.Next(1, 21);
        return Värde;
    }
}

class Program
{
    static void Main()
    {
        TärningT6 t6   = new TärningT6();
        TärningT12 t12 = new TärningT12();
        TärningT20 t20 = new TärningT20();

        Console.WriteLine($"T6:  {t6.Kasta()}");
        Console.WriteLine($"T12: {t12.Kasta()}");
        Console.WriteLine($"T20: {t20.Kasta()}");
    }
}
```

</details>

<details>
<summary>Lösning 2 — en klass, många tärningar</summary>

**Fördel:** en klass, inga upprepningar  
**Nackdel:** lite fler parametrar

```csharp
class Tärning
{
    private static Random _random = new Random();
    private int _max;
    public int Värde { get; private set; }

    public Tärning(int maxVärde)
    {
        _max = maxVärde;
    }

    public int Kasta()
    {
        Värde = _random.Next(1, _max + 1); // +1 så att max faktiskt kan rullas
        return Värde;
    }
}

class Program
{
    static void Main()
    {
        Tärning t6  = new Tärning(6);
        Tärning t12 = new Tärning(12);
        Tärning t20 = new Tärning(20);

        Console.WriteLine($"T6:  {t6.Kasta()}");
        Console.WriteLine($"T12: {t12.Kasta()}");
        Console.WriteLine($"T20: {t20.Kasta()}");
    }
}
```

</details>

Men det är overkill 😄