# Övning — Karaktärsklass

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Du skapar ett litet rollspelssystem i Sagan om Ringen-stil. En spelare väljer ett nummer (1–5) och får sin karaktärsklass med tillhörande stats.

Ingen inläsning från tangentbordet — byt värdet direkt i koden och kör om. Det är fullt tillräckligt nu.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

Deklarera en variabel:

```csharp
int val = 1;
```

Skriv en `switch`-sats som matchar `val` mot karaktärsklasserna nedan och skriver ut namn och stats med `Console.WriteLine`.

| Värde | Klass | Stats |
|-------|-------|-------|
| 1 | Krigar | Styrka: 9, Intelligens: 3, Smidighet: 5 |
| 2 | Trollkarl | Styrka: 2, Intelligens: 10, Smidighet: 4 |
| 3 | Lömsk | Styrka: 5, Intelligens: 6, Smidighet: 9 |
| 4 | Dvärg | Styrka: 8, Intelligens: 5, Smidighet: 3 |
| 5 | Hobbit | Styrka: 3, Intelligens: 7, Smidighet: 8 |

Om värdet inte är 1–5 ska programmet skriva ut: `Ogiltigt val. Välj ett tal mellan 1 och 5.`

---

## Förväntad output för val = 1
```plaintext
Du valde: Krigar
Styrka: 9
Intelligens: 3
Smidighet: 5
```

## Förväntad output för val = 3
```plaintext
Du valde: Lömsk
Styrka: 5
Intelligens: 6
Smidighet: 9
```

## Förväntad output för val = 7
```plaintext
Ogiltigt val. Välj ett tal mellan 1 och 5.
```

---

<details><summary>Tips: grundstruktur för switch</summary>

```csharp
int val = 1;

switch (val)
{
    case 1:
        Console.WriteLine("Du valde: Krigar");
        Console.WriteLine("Styrka: 9");
        // fortsätt med resten av stats
        break;
    case 2:
        // Trollkarl
        break;
    // ... osv
    default:
        Console.WriteLine("Ogiltigt val. Välj ett tal mellan 1 och 5.");
        break;
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int val = 1;

switch (val)
{
    case 1:
        Console.WriteLine("Du valde: Krigar");
        Console.WriteLine("Styrka: 9");
        Console.WriteLine("Intelligens: 3");
        Console.WriteLine("Smidighet: 5");
        break;
    case 2:
        Console.WriteLine("Du valde: Trollkarl");
        Console.WriteLine("Styrka: 2");
        Console.WriteLine("Intelligens: 10");
        Console.WriteLine("Smidighet: 4");
        break;
    case 3:
        Console.WriteLine("Du valde: Lömsk");
        Console.WriteLine("Styrka: 5");
        Console.WriteLine("Intelligens: 6");
        Console.WriteLine("Smidighet: 9");
        break;
    case 4:
        Console.WriteLine("Du valde: Dvärg");
        Console.WriteLine("Styrka: 8");
        Console.WriteLine("Intelligens: 5");
        Console.WriteLine("Smidighet: 3");
        break;
    case 5:
        Console.WriteLine("Du valde: Hobbit");
        Console.WriteLine("Styrka: 3");
        Console.WriteLine("Intelligens: 7");
        Console.WriteLine("Smidighet: 8");
        break;
    default:
        Console.WriteLine("Ogiltigt val. Välj ett tal mellan 1 och 5.");
        break;
}
```

</details>
