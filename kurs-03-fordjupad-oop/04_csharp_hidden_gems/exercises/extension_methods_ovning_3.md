# IsWeekend på DateTime

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en extension method `IsWeekend(this DateTime date)` som returnerar `true` för lördag och söndag. Använd den på några datum du själv väljer.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public static class DateExtensions
{
    public static bool IsWeekend(this DateTime date)
    {
        return date.DayOfWeek == DayOfWeek.Saturday
            || date.DayOfWeek == DayOfWeek.Sunday;
    }
}

Console.WriteLine(new DateTime(2026, 11, 21).IsWeekend());  // true (lördag)
```

</details>
