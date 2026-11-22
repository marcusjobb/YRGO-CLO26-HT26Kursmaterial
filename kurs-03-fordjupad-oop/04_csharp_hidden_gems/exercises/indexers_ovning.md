# Indexers: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Lägg till en kontroll i indexern: om index är utanför hyllan ska du kasta `ArgumentOutOfRangeException`.
2. Testa med index 5 och -1.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public string this[int index]
{
    get => books[CheckIndex(index)];
    set => books[CheckIndex(index)] = value;
}

private int CheckIndex(int index)
{
    if (index < 0 || index >= books.Length)
        throw new ArgumentOutOfRangeException(nameof(index));
    return index;
}
```

</details>
