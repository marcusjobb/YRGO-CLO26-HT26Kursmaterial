# Antal i en lista som kan vara null

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en metod `int CountItems(List<string>? items)` som returnerar antalet, eller 0 om listan är `null`. Använd `?.` och `??`. Ingen `if`.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
int CountItems(List<string>? items) => items?.Count ?? 0;
```

</details>
