---
marp: true
theme: nion-dark
paginate: true
footer: "© 2025–2026 Nion Education. Får inte användas av andra skolor utan tillstånd."
---

# Extension methods

**Vecka 48 · Hellweek · Hidden gems**

---

## Lägg till en metod på en befintlig typ

```csharp
public static class StringExtensions
{
    public static string Shout(this string text)
    {
        return text.ToUpper() + "!";
    }
}

Console.WriteLine("hej".Shout());   // HEJ!
```

`this` före parametern gör att metoden ser ut som en vanlig metod på `string`.

---

## Krav

- Klassen måste vara `static`.
- Metoden måste vara `static`.
- Namnrymden måste vara importerad.

---

## Wow: en metod på något du inte äger

Du kan lägga till `Shout()` på `string`, även om `string` kommer från .NET. Ingen behöver ändra biblioteket.

---

## Sammanfattning

- Extension methods lägger till metoder på befintliga typer.
- Det görs med `this` i första parametern.
- Använd det för hjälpmetoder, inte för att gömma logik.
