# Lösningsnyckel — Bil-klassen

Facit till `exercises/ovning_car.md`.

---

## Steg 1 — Klassen med `Beskriv()`

```csharp
class Car
{
    public string Märke { get; private set; }
    public string Modell { get; private set; }
    public int Årsmodell { get; private set; }
    public int Hastighet { get; private set; }

    public Car(string märke, string modell, int årsmodell)
    {
        Märke = märke;
        Modell = modell;
        Årsmodell = årsmodell;
        Hastighet = 0;
    }

    public void Beskriv()
    {
        Console.WriteLine($"{Märke} {Modell} ({Årsmodell}) — hastighet: {Hastighet} km/h");
    }

    static void Main()
    {
        Car bil1 = new Car("Volvo", "V60", 2022);
        Car bil2 = new Car("Tesla", "Model 3", 2023);

        bil1.Beskriv();
        bil2.Beskriv();
    }
}
```

Förväntad output:
```
Volvo V60 (2022) — hastighet: 0 km/h
Tesla Model 3 (2023) — hastighet: 0 km/h
```

---

## Steg 2 — Full lösning med `Accelerera()` och `Bromsa()`

```csharp
class Car
{
    public string Märke { get; private set; }
    public string Modell { get; private set; }
    public int Årsmodell { get; private set; }
    public int Hastighet { get; private set; }

    public Car(string märke, string modell, int årsmodell)
    {
        Märke = märke;
        Modell = modell;
        Årsmodell = årsmodell;
        Hastighet = 0;
    }

    public void Beskriv()
    {
        Console.WriteLine($"{Märke} {Modell} ({Årsmodell}) — hastighet: {Hastighet} km/h");
    }

    // ökar hastigheten
    public void Accelerera(int ökning)
    {
        Hastighet += ökning;
        Console.WriteLine($"{Märke} accelererar...");
    }

    // minskar hastigheten — aldrig under 0
    public void Bromsa(int minskning)
    {
        Hastighet -= minskning;
        if (Hastighet < 0)
        {
            Hastighet = 0;
        }
        Console.WriteLine($"{Märke} bromsar...");
    }

    static void Main()
    {
        Car bil1 = new Car("Volvo", "V60", 2022);
        Car bil2 = new Car("Tesla", "Model 3", 2023);

        bil1.Beskriv();
        bil2.Beskriv();

        Console.WriteLine();

        bil1.Accelerera(60);
        bil2.Accelerera(80);

        Console.WriteLine();

        bil1.Beskriv();
        bil2.Beskriv();

        Console.WriteLine();

        bil1.Bromsa(20);

        Console.WriteLine();

        bil1.Beskriv();
        bil2.Beskriv();
    }
}
```

Förväntad output:
```
Volvo V60 (2022) — hastighet: 0 km/h
Tesla Model 3 (2023) — hastighet: 0 km/h

Volvo accelererar...
Tesla accelererar...

Volvo V60 (2022) — hastighet: 60 km/h
Tesla Model 3 (2023) — hastighet: 80 km/h

Volvo bromsar...

Volvo V60 (2022) — hastighet: 40 km/h
Tesla Model 3 (2023) — hastighet: 80 km/h
```

---

## Pedagogiska noteringar

- Steg 1 speglar föreläsningsexemplet direkt — bygg förtroende innan nytt läggs till
- `Bromsa()` introducerar ett enkelt villkor (clamp till 0) — bra diskussionsstart
- Studerande som är tidigt klara kan uppmanas prova att skapa en tredje bil och köra den i botten med `Bromsa()`
