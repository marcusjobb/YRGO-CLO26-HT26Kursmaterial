# Dictionary — genomgång

Den här artikeln bygger på live-demo från lektion v39.  
Vi gick igenom hur ett Dictionary skapas, hur man slår upp och loopar, och introducerade LINQ-sortering.

---

## Skapa och fylla ett Dictionary

```csharp
Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);
poäng.Add("Alex", 42);
```

`string` är nyckeltypen — det vi söker på.  
`int` är värdetypen — det vi lagrar.

Varje nyckel måste vara unik. Försöker du `Add` med en nyckel som redan finns kraschar programmet.

---

## Slå upp ett värde

```csharp
Console.WriteLine(poäng["Bella"]);  // 87
Console.WriteLine(poäng["Alex"]);   // 42
```

Om nyckeln inte finns kastar C# ett `KeyNotFoundException` och programmet kraschar. Använd alltid `ContainsKey` innan du slår upp en nyckel du inte är säker på finns:

```csharp
if (poäng.ContainsKey("Jenny"))
{
    Console.WriteLine(poäng["Jenny"]);
}
else
{
    Console.WriteLine("Jenny har inte poängsatts än");
}
```

---

## Bryta ut logik till en metod

Istället för att upprepa if/else-blocket varje gång vi vill visa ett poäng bröt vi ut det till en metod — precis som vi gjorde med `StringHelper.MyReplace` i repetitionsartikeln.

```csharp
VisaPoäng("Carlos");
VisaPoäng("Jenny");
VisaPoäng("Alex");
```

Anropen i Main är nu korta och läsbara. Detaljerna sitter i metoden.

### Metoden med Console-färger

```csharp
void VisaPoäng(string namn)
{
    string text = "";
    if (poäng.ContainsKey(namn))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        text = namn + " har " + poäng[namn] + " poäng";
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        text = namn + " har inte poängsatts än";
    }

    Console.WriteLine(text);
    Console.ResetColor(); // återgå till vanlig textfärg
}
```

`Console.ForegroundColor` sätter textfärgen. `ConsoleColor` är en enum — `Yellow`, `Red`, `Green` osv.  
`Console.ResetColor()` är viktig: om du glömmer den behåller konsolen den satta färgen för all efterföljande utskrift.

---

## Loopa med foreach och KeyValuePair

```csharp
foreach (KeyValuePair<string, int> info in poäng)
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}
```

`KeyValuePair<string, int>` är den typ du faktiskt loopar över i ett Dictionary. Den har två properties:
- `.Key` — nyckeln (här: spelarens namn)
- `.Value` — värdet (här: poängen)

`var` fungerar också och är kortare — kompilatorn räknar ut typen åt dig:

```csharp
foreach (var info in poäng)
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}
```

### Interpolation vs konkatenering

I koden ovan används `$"..."` — stränginterpolation. I `VisaPoäng` används `+` — konkatenering. Båda ger samma resultat:

```csharp
// Konkatenering
text = namn + " har " + poäng[namn] + " poäng";

// Interpolation — lättare att läsa
text = $"{namn} har {poäng[namn]} poäng";
```

Föredra interpolation — det är tydligare vad som är fast text och vad som är variabel.

---

## Sortering med LINQ

LINQ är ett tillägg till C# som ger samlingar kraftfulla sortering- och filtreringsmetoder. Vi introducerade det kort — mer om LINQ i nästa kurs.

**Sorterat på nyckel (A→Ö):**

```csharp
foreach (KeyValuePair<string, int> info in poäng.OrderBy(x => x.Key))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}
```

**Sorterat på värde (störst först):**

```csharp
foreach (var info in poäng.OrderByDescending(x => x.Value))
{
    Console.WriteLine($"{info.Key} har {info.Value} poäng");
}
```

`OrderBy` sorterar stigande. `OrderByDescending` sorterar fallande.  
`x => x.Key` är en lambda — "för varje element, sortera på dess Key". Samma `=>` som i `ForEach`.

---

## Hela koden från lektionen

```csharp
Console.WriteLine("Hello, Dictionary!");

Dictionary<string, int> poäng = new Dictionary<string, int>();
poäng.Add("Bella", 87);
poäng.Add("Carlos", 65);
poäng.Add("Alex", 42);

Console.WriteLine(poäng["Bella"]);
Console.WriteLine(poäng["Alex"]);

if (poäng.ContainsKey("Jenny"))
    Console.WriteLine(poäng["Jenny"]);
else
    Console.WriteLine("Jenny har inte poängsatts än");

Console.WriteLine("----------------------------------------------");
VisaPoäng("Carlos");
VisaPoäng("Jenny");
VisaPoäng("Alex");
VisaPoäng("Bella");
VisaPoäng("Betty");

Console.WriteLine("----------------------------------------------");
foreach (KeyValuePair<string, int> info in poäng)
    Console.WriteLine($"{info.Key} har {info.Value} poäng");

Console.WriteLine("Sorterad på namn");
foreach (var info in poäng.OrderBy(x => x.Key))
    Console.WriteLine($"{info.Key} har {info.Value} poäng");

Console.WriteLine("Sorterad på poäng");
foreach (var info in poäng.OrderByDescending(x => x.Value))
    Console.WriteLine($"{info.Key} har {info.Value} poäng");

void VisaPoäng(string namn)
{
    string text = "";
    if (poäng.ContainsKey(namn))
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        text = $"{namn} har {poäng[namn]} poäng";
    }
    else
    {
        Console.ForegroundColor = ConsoleColor.Red;
        text = $"{namn} har inte poängsatts än";
    }
    Console.WriteLine(text);
    Console.ResetColor();
}
```
