# IsWeekend på DateTime

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en extension method `IsWeekend(this DateTime date)` som returnerar `true` för lördag och söndag. Använd den på några datum du själv väljer.

Fyll i det som står `/* din kod */`.

```csharp
Console.WriteLine(new DateTime(2026, 11, 21).IsWeekend());  // True (lördag)
Console.WriteLine(new DateTime(2026, 11, 22).IsWeekend());  // True (söndag)
Console.WriteLine(new DateTime(2026, 11, 23).IsWeekend());  // False (måndag)

public static class DateExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        /* din kod:
           ta reda på vilken veckodag datumet är
           returnera sant om det är lördag eller söndag */
    }
}
```

Exempeloutput:

```text
True
True
False
```

<details markdown="1">
<summary>Tips 1: vilken dag är det?</summary>

Ett `DateTime` har egenskapen `DayOfWeek`. Den talar om vilken veckodag datumet är.

</details>

<details markdown="1">
<summary>Tips 2: jämför med en enum</summary>

Veckodagarna finns som värden i enumen `DayOfWeek`, till exempel `DayOfWeek.Saturday`. Du kan jämföra med `==`.

</details>

<details markdown="1">
<summary>Tips 3: två villkor</summary>

Det räcker att en av jämförelserna är sann. Använd `||` mellan dem: `date.DayOfWeek == DayOfWeek.Saturday || ...`. Hela uttrycket blir ett `bool` som du kan returnera direkt.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
Console.WriteLine(new DateTime(2026, 11, 21).IsWeekend());  // True (lördag)
Console.WriteLine(new DateTime(2026, 11, 22).IsWeekend());  // True (söndag)
Console.WriteLine(new DateTime(2026, 11, 23).IsWeekend());  // False (måndag)

public static class DateExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday
            || date.DayOfWeek == DayOfWeek.Sunday;
    }
}
```

</details>
