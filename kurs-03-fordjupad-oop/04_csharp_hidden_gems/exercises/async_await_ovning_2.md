# Tre samtidiga väntanden

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv tre metoder som var och en returnerar en sträng efter en fördröjning. Kör dem samtidigt med `Task.WhenAll` och skriv ut alla resultat.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
async Task<string> Fetch(string name, int ms)
{
    await Task.Delay(ms);
    return $"{name} klar";
}

var results = await Task.WhenAll(
    Fetch("A", 1000),
    Fetch("B", 800),
    Fetch("C", 1200));

foreach (var r in results)
    Console.WriteLine(r);
```

</details>
