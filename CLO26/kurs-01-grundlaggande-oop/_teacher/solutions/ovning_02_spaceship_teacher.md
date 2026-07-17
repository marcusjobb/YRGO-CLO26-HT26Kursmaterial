# Lösningsnyckel — Spaceship-klassen

Facit till `exercises/ovning_spaceship.md`.

---

## Steg 1 — Klassen med `Presentera()`

```csharp
class Spaceship
{
    public string Namn { get; private set; }
    public string Kapten { get; private set; }
    public int MaxBesättning { get; private set; }
    public double Bränsle { get; private set; }
    public bool ÄrAktivt { get; private set; }

    public Spaceship(string namn, string kapten, int maxBesättning)
    {
        Namn = namn;
        Kapten = kapten;
        MaxBesättning = maxBesättning;
        Bränsle = 100.0;
        ÄrAktivt = true;
    }

    public void Presentera()
    {
        Console.WriteLine($"{Namn} | Kapten: {Kapten} | Bränsle: {(int)Bränsle}% | Besättning: {MaxBesättning}");
    }

    static void Main()
    {
        Spaceship skepp1 = new Spaceship("Millennium Falcon", "Han Solo", 6);
        Spaceship skepp2 = new Spaceship("Enterprise", "Kirk", 430);

        skepp1.Presentera();
        skepp2.Presentera();
    }
}
```

Förväntad output:
```
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
Enterprise | Kapten: Kirk | Bränsle: 100% | Besättning: 430
```

---

## Steg 2 — Full lösning med `Flyg()` och `Tanka()`

```csharp
class Spaceship
{
    public string Namn { get; private set; }
    public string Kapten { get; private set; }
    public int MaxBesättning { get; private set; }
    public double Bränsle { get; private set; }
    public bool ÄrAktivt { get; private set; }

    public Spaceship(string namn, string kapten, int maxBesättning)
    {
        Namn = namn;
        Kapten = kapten;
        MaxBesättning = maxBesättning;
        Bränsle = 100.0;
        ÄrAktivt = true;
    }

    public void Presentera()
    {
        Console.WriteLine($"{Namn} | Kapten: {Kapten} | Bränsle: {(int)Bränsle}% | Besättning: {MaxBesättning}");
    }

    // flyger och förbrukar bränsle — aldrig under 0
    public void Flyg(double bränsleförbrukning)
    {
        Bränsle -= bränsleförbrukning;
        if (Bränsle < 0)
        {
            Bränsle = 0;
        }
        Console.WriteLine($"{Namn} flyger... Bränsle kvar: {(int)Bränsle}%");
    }

    // tankar upp — aldrig över 100
    public void Tanka(double mängd)
    {
        Bränsle += mängd;
        if (Bränsle > 100.0)
        {
            Bränsle = 100.0;
        }
        Console.WriteLine($"Tankar {Namn}...");
    }

    static void Main()
    {
        Spaceship skepp1 = new Spaceship("Millennium Falcon", "Han Solo", 6);
        Spaceship skepp2 = new Spaceship("Enterprise", "Kirk", 430);

        skepp1.Presentera();
        skepp2.Presentera();

        Console.WriteLine();

        skepp1.Flyg(25);
        skepp2.Flyg(10);

        Console.WriteLine();

        skepp1.Tanka(25);
        skepp1.Presentera();
    }
}
```

Förväntad output:
```
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
Enterprise | Kapten: Kirk | Bränsle: 100% | Besättning: 430

Millennium Falcon flyger... Bränsle kvar: 75%
Enterprise flyger... Bränsle kvar: 90%

Tankar Millennium Falcon...
Millennium Falcon | Kapten: Han Solo | Bränsle: 100% | Besättning: 6
```

---

## Steg 3 (🔴 utmaning) — `Docka()`

```csharp
// lägg till i Spaceship-klassen
public void Docka(Spaceship annatSkepp)
{
    Console.WriteLine($"{Namn} dockar med {annatSkepp.Namn}.");
}
```

Anrop i `Main()`:
```csharp
skepp1.Docka(skepp2);
```

Output:
```
Millennium Falcon dockar med Enterprise.
```

---

## Pedagogiska noteringar

- `double`-fältet `Bränsle` är ett litet steg upp från `int` — bra introduktion till flyttal utan att det känns tungt
- Steg 3 introducerar objekt som parameter, ett viktigt mönster. Låt starka studerande prova — gå inte igenom det med hela gruppen om de flesta inte kommit dit
- `ÄrAktivt` används inte i övningen — det finns där som en naturlig diskussionspunkt ("vad händer om vi stänger av skeppet?")
