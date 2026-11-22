# Property pattern på en record

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Givet `record Order(string Status, int ItemCount)`, skriv `string Message(Order order)` med ett switch-uttryck:
- Status "Klar" och minst en vara: "Skickas"
- Status "Klar" och inga varor: "Tom order"
- Allt annat: "Väntar"

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
record Order(string Status, int ItemCount);

string Message(Order order) => order switch
{
    { Status: "Klar", ItemCount: > 0 } => "Skickas",
    { Status: "Klar" } => "Tom order",
    _ => "Väntar"
};
```

</details>
