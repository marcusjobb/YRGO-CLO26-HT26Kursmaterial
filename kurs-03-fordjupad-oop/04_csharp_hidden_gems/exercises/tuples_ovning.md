# Tuples: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv en metod som tar en lista med heltal och returnerar `(int min, int max)`.
2. Använd deconstruction för att skriva ut min och max.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
(int min, int max) MinMax(List<int> numbers)
{
    return (numbers.Min(), numbers.Max());
}

var (min, max) = MinMax(new List<int> { 4, 9, 2 });
Console.WriteLine($"Min: {min}, max: {max}");
```

</details>
