# Stjärntecken från ett datum

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 30 minuter.

**Förkunskap:** Extension methods och pattern matching med tuples.

Skriv en extension method som tar ett datum och returnerar stjärntecknet.

## Uppgift

1. Skriv en extension method `ZodiacSign(this DateOnly date)` som returnerar ett stjärntecken på svenska, till exempel "Vädur".
2. Läs in ett datum med `DateOnly.Parse(Console.ReadLine())`. Använd formatet `ÅÅÅÅ-MM-DD`.
3. Skriv ut stjärntecknet för datumet.
4. **🔴** Låt användaren skriva in flera datum, ett i taget, tills hen skriver `slut`.

Tips: Ett switch-uttryck på `(date.Month, date.Day)` gör det kort och tydligt.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public static class DateOnlyExtensions
{
    public static string ZodiacSign(this DateOnly date)
    {
        return (date.Month, date.Day) switch
        {
            (3, >= 21) or (4, <= 19) => "Vädur",
            (4, >= 20) or (5, <= 20) => "Oxen",
            (5, >= 21) or (6, <= 20) => "Tvillingarna",
            (6, >= 21) or (7, <= 22) => "Kräftan",
            (7, >= 23) or (8, <= 22) => "Lejonet",
            (8, >= 23) or (9, <= 22) => "Jungfrun",
            (9, >= 23) or (10, <= 22) => "Vågen",
            (10, >= 23) or (11, <= 21) => "Skorpionen",
            (11, >= 22) or (12, <= 21) => "Skytten",
            (12, >= 22) or (1, <= 19) => "Stenbocken",
            (1, >= 20) or (2, <= 18) => "Vattumannen",
            _ => "Fiskarna"
        };
    }
}

Console.Write("Ange ett datum (ÅÅÅÅ-MM-DD): ");
DateOnly date = DateOnly.Parse(Console.ReadLine());
Console.WriteLine($"Stjärntecken: {date.ZodiacSign()}");
```

**🔴 Variant:**

```csharp
while (true)
{
    Console.Write("Datum (eller slut): ");
    string? input = Console.ReadLine();
    if (input == "slut") break;
    Console.WriteLine(DateOnly.Parse(input!).ZodiacSign());
}
```

</details>

## Variant: som egenskap

Från C# 14 (.NET 10) kan en extension också vara en **egenskap** (property), i stället för en metod. Egenskapen ska vara **get only**: stjärntecknet beräknas från datumet, så det finns inget att sätta.

1. Skriv om `ZodiacSign` till en get-only egenskap i ett `extension`-block.
2. Använd den som `date.ZodiacSign` (utan parenteser).
3. Försök att tilldela egenskapen. Vad säger kompilatorn?

<details markdown="1">
<summary>Lösningsförslag: egenskap</summary>

```csharp
public static class DateOnlyExtensions
{
    extension(DateOnly date)
    {
        public string ZodiacSign => (date.Month, date.Day) switch
        {
            (3, >= 21) or (4, <= 19) => "Vädur",
            (4, >= 20) or (5, <= 20) => "Oxen",
            (5, >= 21) or (6, <= 20) => "Tvillingarna",
            (6, >= 21) or (7, <= 22) => "Kräftan",
            (7, >= 23) or (8, <= 22) => "Lejonet",
            (8, >= 23) or (9, <= 22) => "Jungfrun",
            (9, >= 23) or (10, <= 22) => "Vågen",
            (10, >= 23) or (11, <= 21) => "Skorpionen",
            (11, >= 22) or (12, <= 21) => "Skytten",
            (12, >= 22) or (1, <= 19) => "Stenbocken",
            (1, >= 20) or (2, <= 18) => "Vattumannen",
            _ => "Fiskarna"
        };
    }
}

var date = new DateOnly(1990, 5, 1);
Console.WriteLine(date.ZodiacSign);   // Oxen
```

Steg 3: Att tilldela `date.ZodiacSign = "Vädur";` ger ett kompileringsfel. Egenskapen saknar `set`, och det är meningen: stjärntecknet följer av datumet.

</details>
