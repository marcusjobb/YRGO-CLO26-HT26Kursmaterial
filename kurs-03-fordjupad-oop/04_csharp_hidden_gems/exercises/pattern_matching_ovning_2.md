# Beskriv ett objekt

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv `string Describe(object o)` som returnerar:
- "heltal: n" om värdet är ett heltal
- "text med n tecken" om det är en sträng
- "tomt" om det är `null`
- "okänt" för allt annat

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string Describe(object o) => o switch
{
    null => "tomt",
    int n => $"heltal: {n}",
    string s => $"text med {s.Length} tecken",
    _ => "okänt"
};
```

</details>
