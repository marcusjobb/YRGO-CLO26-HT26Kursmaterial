# Reflection: vad består objektet av?

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen. Reflection är en demo i bildspelet, så den här övningen är ett första smakprov.

**Nivå:** 🟢

**Tid:** ca 20 minuter.

## Uppgift

Utgå från Anders kund:

```csharp
class Customer
{
    public string? Name { get; set; }
    public string? Phone { get; set; }
    public int? Age { get; set; }
}
```

1. Skapa en `Customer` med namnet "Ada".
2. Skriv ut klassens namn med `GetType().Name`.
3. Skriv ut namnet på varje property med `GetProperties()`.

Tips: `GetType()` ger typen, och `GetProperties()` ger en lista där varje post har en `Name`.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var ada = new Customer { Name = "Ada" };

Console.WriteLine(ada.GetType().Name);   // Customer

foreach (var property in ada.GetType().GetProperties())
{
    Console.WriteLine(property.Name);
}
// Name
// Phone
// Age
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
