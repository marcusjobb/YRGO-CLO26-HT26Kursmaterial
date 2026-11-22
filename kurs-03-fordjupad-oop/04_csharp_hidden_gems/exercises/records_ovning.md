# Records: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skapa `record Booking(string Guest, DateTime CheckIn, int Nights)`.
2. Skapa två bokningar med samma värden och jämför dem med `==`.
3. Gör en ny bokning med `with` som förlänger vistelsen med en natt.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
public record Booking(string Guest, DateTime CheckIn, int Nights);

var a = new Booking("Ada", new DateTime(2026, 11, 20), 2);
var b = new Booking("Ada", new DateTime(2026, 11, 20), 2);
Console.WriteLine(a == b);                 // True
var longer = a with { Nights = 3 };
```

</details>
