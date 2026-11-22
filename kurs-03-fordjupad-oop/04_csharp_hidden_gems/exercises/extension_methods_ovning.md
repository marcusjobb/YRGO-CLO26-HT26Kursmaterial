# Extension methods: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv en extension method `Percent(this decimal value, int percent)` som returnerar procenten av värdet.
2. Använd den på 200 med 25.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public static class DecimalExtensions
{
    public static decimal Percent(this decimal value, int percent)
    {
        return value * percent / 100m;
    }
}

Console.WriteLine(200m.Percent(25));   // 50
```

</details>
