# Räkna jämna och udda

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv `(int evens, int odds) CountEvenAndOdd(int[] numbers)` som returnerar två värden. Använd deconstruction när du anropar den.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
(int evens, int odds) CountEvenAndOdd(int[] numbers)
{
    int evens = 0, odds = 0;
    foreach (int n in numbers)
    {
        if (n % 2 == 0) evens++;
        else odds++;
    }
    return (evens, odds);
}

var (e, o) = CountEvenAndOdd(new[] { 1, 2, 3, 4, 5 });
Console.WriteLine($"Jämna: {e}, udda: {o}");   // Jämna: 2, udda: 3
```

</details>
