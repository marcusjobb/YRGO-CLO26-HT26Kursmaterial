# Extension methods

## Burken som inte går att öppna

Anders vill att `"latte".ToTitle()` ska ge `Latte`. Första idén: en egen klass som ärver från `string`, och så lägger han till metoden där.

Funkar det? Gissa först. Fundera lite på det innan du läser vidare.

## Försök 1: ärv från string

```csharp
class MinString : string
{
    public string ToTitle() { /* ... */ }
}
```

```
error CS0509: 'MinString': cannot derive from sealed type 'string'
```

`string` är **sealed**, förseglad. Ingen får ärva från den.

## Fäst något nytt på det som redan finns

Du kan inte öppna burken, men du kan klicka fast ett nytt verktyg på utsidan. Det är en extension method.

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

**Typen efter `this` är typen metoden läggs till på.** Returtypen är en helt annan sak.

## Krav

- Klassen måste vara `static`.
- Metoden måste vara `static`.
- Namnrymden måste vara importerad av den som ska använda vår extension.

## Nu har alla strängar Anders metod

```csharp
public static string ToTitle(this string text)
{
    return char.ToUpper(text[0]) + text.Substring(1);
}

Console.WriteLine("latte".ToTitle());       // Latte
Console.WriteLine("kladdkaka".ToTitle());   // Kladdkaka
```

Anders skrev metoden en gång. Nu kan varje `string` i programmet använda den. Ingen behövde ändra .NET.

## Summan av kardemumman

- Extension methods lägger till metoder på befintliga typer.
- Det görs med `this` i första parametern.
- Använd det för hjälpmetoder, inte för att gömma logik.

## Proffsens tips: kolla ingredienserna först

Vad händer med `"".ToTitle()`? Det finns ingen första bokstav att göra stor, så metoden kraschar med `IndexOutOfRangeException`.

```csharp
public static string ToTitle(this string text)
{
    if (string.IsNullOrEmpty(text))
        return text;
    return char.ToUpper(text[0]) + text.Substring(1);
}
```

Kladdkakan igen: kolla inputen först, använd den sen. Nu ger `"".ToTitle()` tillbaka en tom sträng.

## Proffsens tips: använd med måtta

**Bra:** koden blir lättare att läsa. `"latte".ToTitle()` säger mer än `Helpers.ToTitle("latte")`, och du slipper skapa en ny typ.

**Men:** många extension methods gör det svårt att hålla reda på vad som gör vad. `Where`, `Select`, `OrderBy` och `Count` på en lista är alla extension methods från LINQ (Language Integrated Query). Du ser inte var de kommer ifrån.

**Tumregel:** lägg dem i en klass med tydligt namn, till exempel `StringExtensions`. Kan du inte beskriva vad metoden gör med ett ord? Gör den till en vanlig metod.

## Prova själv

Gör `extension_methods_ovning.md`, `extension_methods_ovning_2.md`, `extension_methods_ovning_3.md`, `extension_methods_ovning_4.md` och `time_only_ovning.md`.
