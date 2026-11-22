# Kontrollera typen innan du använder den

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

1. Skriv en metod `static void PrintLength(dynamic value)` som skriver ut längden om värdet är en sträng.
2. Om det inte är en sträng, skriv ut "Inte text".
3. Anropa den med `"hej"`, `42` och `null`.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
static void PrintLength(dynamic value)
{
    if (value is string text)
        Console.WriteLine($"Längd: {text.Length}");
    else
        Console.WriteLine("Inte text");
}

PrintLength("hej");   // Längd: 3
PrintLength(42);      // Inte text
PrintLength(null);    // Inte text
```

</details>
