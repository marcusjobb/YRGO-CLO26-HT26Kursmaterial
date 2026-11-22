# Extension methods

Ibland vill du lägga till en metod på en typ som du inte äger, till exempel `string` från .NET. Du kan inte ändra klassen, men en extension method låter dig skriva metoden så att den ser ut som en vanlig metod på typen.

## Ett exempel

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

`this` före första parametern är det som gör metoden till en extension. Anropet ser ut som `"hej".Shout()`, men i praktiken anropas metoden med strängen som första argument.

## Regler

- Klassen som innehåller metoden måste vara `static`.
- Metoden måste vara `static`.
- Namnrymden måste vara importerad, annars syns metoden inte.

## Extension members (C# 14)

Från C# 14 kan en extension också vara en egenskap, genom ett `extension`-block:

```csharp
public static class DateOnlyExtensions
{
    extension(DateOnly date)
    {
        public bool IsWeekend => date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday;
    }
}
```

Egenskapen används utan parenteser, som `date.IsWeekend`. Den kan vara get only, och det är ofta det rätta när värdet följer av det andra värdet.

## När bör du låta bli?

En extension method ska göra något som naturligt hör till typen. Om metoden egentligen hör till en annan klass, eller gömmer viktig logik, skapa en vanlig klass i stället. Extension methods är bra för hjälpmetoder, inte för att dölja beslut.

## Sammanfattning

- Extension methods lägger till metoder på befintliga typer.
- `this` i första parametern gör det till en extension.
- Använd dem för hjälpmetoder som hör till typen.

## Prova själv

Gör `extension_methods_ovning.md`, `extension_methods_ovning_2.md`, `extension_methods_ovning_3.md`, `extension_methods_ovning_4.md` och `time_only_ovning.md`.
