# Konsol — färg, position och cursor

🟡

## 🧠 Syfte

Terminalen kan mer än skriva text. Med några rader kod kan du ändra bakgrundsfärg, styra exakt var text skrivs ut, och dölja markören — det är grunden till allt från enkla menyer till animerade konsolspel. Det här kapitlet visar hur.

## 💻 Koden

```csharp
using System;

class KonsolDemo
{
    static void Main(string[] args)
    {
        // Bakgrundsfärg — OBS: måste rensa skärmen efteråt
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.Clear(); // Bakgrunden fyller hela fönstret FÖRST här

        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("Gul text på mörkblå bakgrund!");

        Console.ResetColor(); // Återställ till standard

        // Flytta markören till rad 5, kolumn 10 (x=10, y=5)
        Console.SetCursorPosition(10, 5);
        Console.WriteLine("Jag skrivs ut på rad 5, kolumn 10");

        // Dölj markören (bra i animationer och spel)
        Console.CursorVisible = false;

        Console.SetCursorPosition(0, 8);
        Console.ResetColor();
        Console.WriteLine("Tryck Enter för att avsluta...");
        Console.ReadLine();

        Console.CursorVisible = true; // Visa markören igen
    }
}
```

## 📋 Förklaring

### Console.Clear()

Rensar hela terminalfönstret och återställer markören till position (0, 0).

```csharp
Console.Clear();
```

Används vanligtvis för att uppdatera en animation eller byta skärm.

---

### Console.BackgroundColor och Console.ForegroundColor

Sätter bakgrunds- och textfärg för all text som skrivs ut *efter* att du ändrat dem.

```csharp
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.ForegroundColor = ConsoleColor.Yellow;
Console.WriteLine("Hej!");
```

**Viktig detalj om bakgrundsfärgen:** Att sätta `BackgroundColor` påverkar bara nya tecken — terminalbakgrunden förblir oförändrad. Vill du att *hela fönstret* ska få den nya bakgrundsfärgen måste du anropa `Console.Clear()` *efter* att du satt färgen.

```csharp
// Fel ordning — bakgrunden i fönstret ändras inte
Console.Clear();
Console.BackgroundColor = ConsoleColor.DarkBlue;

// Rätt ordning — hela fönstret fylls med blå bakgrund
Console.BackgroundColor = ConsoleColor.DarkBlue;
Console.Clear();
```

Tillgängliga färger finns i `ConsoleColor`-enumen: `Black`, `DarkBlue`, `DarkGreen`, `DarkCyan`, `DarkRed`, `DarkMagenta`, `DarkYellow`, `Gray`, `DarkGray`, `Blue`, `Green`, `Cyan`, `Red`, `Magenta`, `Yellow`, `White`.

---

### Console.ResetColor()

Återställer både `BackgroundColor` och `ForegroundColor` till terminalens standardfärger.

```csharp
Console.ForegroundColor = ConsoleColor.Red;
Console.WriteLine("Röd text");
Console.ResetColor();
Console.WriteLine("Normal text igen");
```

Glöm aldrig att återställa — annars påverkar dina färger all text som skrivs ut efter programmet avslutas.

---

### Console.SetCursorPosition(x, y)

Flyttar markören till en specifik position i terminalfönstret.

```csharp
Console.SetCursorPosition(kolumn, rad);
```

- **x** = kolumn (horisontellt, räknas från vänster, börjar på 0)
- **y** = rad (vertikalt, räknas från toppen, börjar på 0)

```csharp
Console.SetCursorPosition(20, 10);
Console.WriteLine("Mitt på skärmen");
```

Det här gör det möjligt att rita enkla gränssnitt och skriva ut text på precis rätt plats utan att skriva ut en massa tomma rader.

---

### Console.CursorVisible

En bool-egenskap som styr om markören syns i terminalfönstret.

```csharp
Console.CursorVisible = false; // Dölj markören
Console.CursorVisible = true;  // Visa markören igen
```

Användbart i animationer och spel där en blinkande markör stör upplevelsen. Kom ihåg att sätta den till `true` igen när programmet avslutas — annars försvinner markören i terminalen efteråt.

## 📚 Sammanfattning

| Metod/Egenskap | Vad den gör |
|---|---|
| `Console.Clear()` | Rensar skärmen, återställer markören till (0,0) |
| `Console.BackgroundColor` | Sätter bakgrundsfärgen för nya tecken |
| `Console.ForegroundColor` | Sätter textfärgen |
| `Console.ResetColor()` | Återställer till terminalens standardfärger |
| `Console.SetCursorPosition(x, y)` | Flyttar markören till angiven kolumn och rad |
| `Console.CursorVisible` | `false` döljer markören, `true` visar den |

**Kom ihåg:** Sätt `BackgroundColor` *innan* du anropar `Clear()` om du vill fylla hela fönstret med bakgrundsfärgen.

## 😄 Obligatoriskt pappaskämt

Varför gillar programmerare mörkt läge? För att ljuset lockar buggar!

---
Nu vet du hur terminalen egentligen fungerar. Testa, experimentera och hitta ditt eget sätt att använda det.
