# Exempel — Överlagring (method overloading)

Samma metodnamn, olika signaturer — kompilatorn väljer rätt version baserat på argumenten du skickar in.  
En signatur = metodnamn + parametertyper. Returtypen räknas **inte**.

---

## Hälsning — olika sätt att hälsa

Fyra versioner av `Hälsa()` med olika parametrar.

```csharp
class Hälsning
{
    public void Hälsa(string namn)              // Hälsa(string)
    {
        Console.WriteLine($"Hej {namn}");
    }

    public void Hälsa(string namn, string titel)  // Hälsa(string, string)
    {
        Console.WriteLine($"Goddag, {titel} {namn}");
    }

    public void Hälsa(int antal)               // Hälsa(int) — helt annan typ
    {
        for (int i = 0; i < antal; i++)
        {
            Console.WriteLine("Hej!");
        }
    }

    public void Hälsa()                        // Hälsa() — inga parametrar
    {
        Console.WriteLine("Hej på dig!");
    }
}
```

**Användning:**

```csharp
Hälsning h = new();
h.Hälsa("Marcus");           // Hälsa(string)
h.Hälsa("Marcus", "Mr.");    // Hälsa(string, string)
h.Hälsa(3);                  // Hälsa(int)
h.Hälsa();                   // Hälsa()
```

**Output:**

```
Hej Marcus
Goddag, Mr. Marcus
Hej!
Hej!
Hej!
Hej på dig!
```

---

## Calculator — samma operation, olika typer

Kompilatorn väljer version baserat på om argumenten är `int` eller `double`.

```csharp
class Calculator
{
    public void Add(int x, int y)           // Add(int, int)
    {
        // OBS: + med string gör strängkonkatenering, inte addition
        // "2" + " " + "3" → "2 3", inte 5
        Console.WriteLine(x + " " + y);
    }

    public void Add(double x, int y)        // Add(double, int)
    {
        Console.WriteLine(x + " " + y);
    }

    public void Add(double x, double y)     // Add(double, double)
    {
        Console.WriteLine(x + " " + y);
    }
}
```

**Användning:**

```csharp
Calculator calc = new();
calc.Add(2, 3);         // Add(int, int)
calc.Add(2.5, 3);       // Add(double, int)
calc.Add(2.5, 3.5);     // Add(double, double)
```

**Output:**

```
2 3
2,5 3
2,5 3,5
```

> `x + " " + y` — så fort en `string` dyker upp i kedjan behandlas resten som konkatenering, inte matematik.

---

## Geometri — rektangel med ett eller två mått

En parameter = kvadrat. Två parametrar = bredd och höjd.

```csharp
class Geometri
{
    public void Rectangle(int w)           // Rectangle(int) — alla sidor lika
    {
        Console.WriteLine($"All sides: {w}");
    }

    public void Rectangle(int w, int h)    // Rectangle(int, int) — bredd och höjd
    {
        Console.WriteLine($"Width: {w}");
        Console.WriteLine($"Height: {h}");
    }
}
```

**Användning:**

```csharp
Geometri geo = new();
geo.Rectangle(10);      // Rectangle(int)
geo.Rectangle(2, 4);    // Rectangle(int, int)
```

**Output:**

```
All sides: 10
Width: 2
Height: 4
```

---

## Sammanfattning

| Klass | Vad överlagringen visar |
|-------|------------------------|
| `Hälsning` | Olika antal och typer av parametrar — samma metodnamn |
| `Calculator` | `int` vs `double` — kompilatorn väljer närmaste matchning |
| `Geometri` | Färre parametrar = rimligt standardbeteende (kvadrat) |

> Överlagring är inte arv — klasserna här är helt fristående.  
> Det är kompilatorn, inte runtime, som väljer vilken version som körs.
