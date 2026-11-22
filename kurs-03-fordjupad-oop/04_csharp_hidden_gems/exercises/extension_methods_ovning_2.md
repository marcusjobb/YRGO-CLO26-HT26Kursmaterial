# WordCount på string

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en extension method `WordCount(this string text)` som returnerar antalet ord. Tom sträng ger 0. Testa med "hej där världen" och med en tom sträng.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public static class StringExtensions
{
    public static int WordCount(this string text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return 0;
        return text.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
    }
}

Console.WriteLine("hej där världen".WordCount());   // 3
Console.WriteLine("".WordCount());                  // 0
```

</details>
