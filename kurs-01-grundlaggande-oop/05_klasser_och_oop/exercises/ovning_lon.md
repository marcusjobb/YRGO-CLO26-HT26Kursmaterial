# Övning — Lönesystemet

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Yrkeshögskolan NionIT behöver ett enkelt lönesystem. Det finns tre typer av personal — och alla beräknas olika. Löneformeln i sig ska aldrig ändras, men hur tillägget räknas ut beror på rollen.

---

## Del 1 — Basklassen `Anställd`

Skapa klassen `Anställd` med:

**Properties (publik get, protected set):**
- `Namn` (string)
- `Grundlön` (double)

**Konstruktor** som tar namn och grundlön

**Låst metod — ändra aldrig den här:**
```csharp
public void SkrivUtLön()
{
    Console.WriteLine($"{Namn}: {Grundlön + Tillägg():F0} kr/mån");
}
```

**Virtual metod:**
```csharp
public virtual double Tillägg() => 0;
```

---

## Del 2 — Tre subklasser

### `Lärare`
- Konstruktor tar namn och grundlön
- Tillägg: **8% av grundlönen** (kompetenspåslag)

### `Konsult`
- Konstruktor tar namn, grundlön och timmar per månad (int)
- Tillägg: **timmar × 150 kr** (konsultarvode)

### `TillfälligFöreläsare`
- Konstruktor tar namn och antal föreläsningar (int)
- Grundlön = 0 (ingen fast lön)
- Tillägg: **föreläsningar × 3500 kr** (per tillfälle)

---

## Del 3 — Sätt ihop i Main

```csharp
Anställd[] personal = {
    new Lärare("Anna Lindqvist", 38000),
    new Lärare("Björn Eriksson", 41000),
    new Konsult("Maria Chen", 35000, 20),
    new TillfälligFöreläsare("Dr. Svensson", 3)
};

foreach (var p in personal)
{
    p.SkrivUtLön();
}
```

**Förväntad output:**
```
Anna Lindqvist: 41040 kr/mån
Björn Eriksson: 44280 kr/mån
Maria Chen: 38000 kr/mån
Dr. Svensson: 10500 kr/mån
```

---

## Frågor — svara som kommentarer i koden

1. `SkrivUtLön()` är inte virtual — vad händer om du försöker override:a den i `Lärare`?
2. Varför är `Grundlön` protected och inte private? Vad skulle hända om den var private?
3. Lägg till `class Rektor : Anställd` med fast tillägg 12 000 kr. Hur många rader behövde du skriva?
4. Kan du lägga alla fyra typerna i en `List<Anställd>` och loopa igenom dem? Testa.

---

## Utmaning 🔴

Lägg till metoden `Presentera()` i `Anställd`:

```csharp
public void Presentera()
{
    Console.WriteLine($"{Namn} ({GetType().Name}) — grundlön: {Grundlön:F0} kr");
}
```

Anropa både `Presentera()` och `SkrivUtLön()` för varje anställd i loopen.  
Förklara med egna ord (kommentar): vad gör `GetType().Name` och varför är det användbart här?

---

<details>
<summary>Lösningsförslag</summary>

```csharp
class Anställd
{
    public string Namn { get; protected set; }
    public double Grundlön { get; protected set; }

    public Anställd(string namn, double grundlön)
    {
        Namn = namn;
        Grundlön = grundlön;
    }

    public void SkrivUtLön()
    {
        Console.WriteLine($"{Namn}: {Grundlön + Tillägg():F0} kr/mån");
    }

    public virtual double Tillägg() => 0;
}

class Lärare : Anställd
{
    public Lärare(string namn, double grundlön) : base(namn, grundlön) { }
    public override double Tillägg() => Grundlön * 0.08;
}

class Konsult : Anställd
{
    private int _timmar;
    public Konsult(string namn, double grundlön, int timmar) : base(namn, grundlön)
    {
        _timmar = timmar;
    }
    public override double Tillägg() => _timmar * 150;
}

class TillfälligFöreläsare : Anställd
{
    private int _föreläsningar;
    public TillfälligFöreläsare(string namn, int föreläsningar) : base(namn, 0)
    {
        _föreläsningar = föreläsningar;
    }
    public override double Tillägg() => _föreläsningar * 3500;
}
```

</details>
