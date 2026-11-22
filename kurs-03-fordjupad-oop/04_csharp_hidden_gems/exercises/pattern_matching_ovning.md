# Pattern matching: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv en metod `string Grade(int score)` med `switch`-uttryck för poäng 0–100: 90 och uppåt är A, 75 och uppåt B, 50 och uppåt C, annars F.
2. Lägg till en `when` som ger felmeddelandet "ogiltig" för poäng under 0 eller över 100.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string Grade(int score) => score switch
{
    < 0 or > 100 => "ogiltig",
    >= 90 => "A",
    >= 75 => "B",
    >= 50 => "C",
    _ => "F"
};
```

</details>
