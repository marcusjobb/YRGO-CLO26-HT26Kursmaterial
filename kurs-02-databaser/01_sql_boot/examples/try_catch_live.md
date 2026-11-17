# Try-Catch Live

Vad kan gå fel? Det visar det här programmet.

---

## Steg 1 — Fråga användaren om deras ålder

```csharp
Console.WriteLine("Hello, TryCatch!");

Console.WriteLine("Ange din ålder!");
```

Enkelt nog. Men nästa rad är farlig.

---

## Steg 2 — Parsa input med try-catch

```csharp
try
{
    // När ni arbetar mot extern input som ni kan kontrollera
    // Exempelvis input från användaren, eller databas, eller API
    // eller skrivaren, hårddisken etc. Så använd try-catch
    int age = int.Parse(Console.ReadLine());
    Console.WriteLine($"Du är {age * 7} hundår");
}
catch (FormatException)
{
    Console.WriteLine("Ange siffror ditt pucko!");
}
catch (Exception e)
{
    Console.WriteLine("Felet är:" + e.Message);
    throw;
}
```

`int.Parse` kraschar om användaren skriver "tjugo" istället för 20. `FormatException` fångar exakt det felet — vi kan ge ett vettigt meddelande istället för att programmet dör.

`catch (Exception e)` med `throw` fångar allt annat oväntat och kastar det vidare — vi loggade felet men vill ändå att programmet ska märka att något gick riktigt fel.

---

## Steg 3 — TryParse som alternativ

```csharp
Console.WriteLine("Skriv en siffra");
var input = Console.ReadLine();
if (int.TryParse(input, out var result))
{
    Console.WriteLine(result);
}
else
{
    Console.WriteLine("Ange siffror ditt pucko!");
}
```

`TryParse` kraschar aldrig — den returnerar `true` eller `false` och lägger värdet i `result` om det gick. Bra när du vill hantera felet inline utan ett try-catch-block.

Skillnaden:
- `Parse` → kasta undantag om det går fel
- `TryParse` → returnera false om det går fel

---

## Hela koden

```csharp
Console.WriteLine("Hello, TryCatch!");

Console.WriteLine("Ange din ålder!");

// Vad kan gå fel?
try
{
    // När ni arbetar mot extern input som ni kan kontrollera
    // Exempelvis input från användaren, eller databas, eller API
    // eller skrivaren, hårddisken etc. Så använd try-catch
    int age = int.Parse(Console.ReadLine());
    Console.WriteLine($"Du är {age * 7} hundår");
}
catch (FormatException)
{
    Console.WriteLine("Ange siffror ditt pucko!");
}
catch (Exception e)
{
    Console.WriteLine("Felet är:" + e.Message);
    throw;
}

Console.WriteLine("Skriv en siffra");
var input = Console.ReadLine();
if (int.TryParse(input, out var result))
{
    Console.WriteLine(result);
}
else
{
    Console.WriteLine("Ange siffror ditt pucko!");
}
```
