# Fel i en asynkron metod

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en async-metod som kastar ett undantag efter en fördröjning. Anropa den med `await` i en `try`/`catch` och skriv ut felmeddelandet.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
async Task FailAfterDelay()
{
    await Task.Delay(500);
    throw new InvalidOperationException("Något gick fel");
}

try
{
    await FailAfterDelay();
}
catch (InvalidOperationException ex)
{
    Console.WriteLine($"Fångade: {ex.Message}");
}
```

</details>
