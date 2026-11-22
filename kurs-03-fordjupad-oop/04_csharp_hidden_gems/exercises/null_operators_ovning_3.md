# Cache med ??=

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv en klass `Cache` med en property `List<string>? items`. Metoden `Add(string item)` ska skapa listan första gången den används, och sedan lägga till objektet. Använd `??=`.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
class Cache
{
    private List<string>? items;

    public void Add(string item)
    {
        items ??= new List<string>();
        items.Add(item);
    }

    public int Count => items?.Count ?? 0;
}
```

</details>
