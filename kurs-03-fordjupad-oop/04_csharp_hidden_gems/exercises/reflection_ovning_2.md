# ToDebugString: alla värden på en rad

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🟡

**Tid:** ca 20 minuter.

## Uppgift

Skriv en extension method `ToDebugString(this object obj)` som returnerar en rad med alla properties på formen `Name = Ada, Phone = saknas, Age = saknas`. Om ett värde är `null` ska det stå `saknas`. Använd `GetType()`, `GetProperties()`, `GetValue` och `??`.

Testa med `Customer` från förra övningen. Prova sedan på en annan klass du själv hittar på. Samma metod ska fungera på båda.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var ada = new Customer { Name = "Ada" };
Console.WriteLine(ada.ToDebugString());   // Name = Ada, Phone = saknas, Age = saknas

public static class ObjectExtensions
{
    public static string ToDebugString(this object obj)
    {
        // Tänk på att lägga till Try Catch
        var parts = obj.GetType().GetProperties()
            .Select(p => $"{p.Name} = {p.GetValue(obj) ?? "saknas"}");
        return string.Join(", ", parts);
    }
}
```

Metoden vet inget om `Customer`. Därför fungerar den på vilken klass som helst.

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
