# SetDefaults: fyll i det som saknas

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🔴

**Tid:** ca 30 minuter.

## Uppgift

Skriv en extension method `SetDefaults(this object obj)` som går igenom alla properties och fyller i dem som är `null`:

- En `string` som saknas blir `"okänd"`.
- En `int?` som saknas blir `0`.
- Det som redan är ifyllt rörs inte.

Testa med `new Customer { Name = "Ada" }`. Förväntat resultat: `Ada, okänd, 0`.

Tips: `property.PropertyType == typeof(string)` kollar vilken typ en property har, och `property.SetValue(obj, värde)` sätter den.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var customer = new Customer { Name = "Ada" };
customer.SetDefaults();
Console.WriteLine($"{customer.Name}, {customer.Phone}, {customer.Age}");   // Ada, okänd, 0

public static class ObjectExtensions
{
    public static void SetDefaults(this object obj)
    {
        // Tänk på att lägga till Try Catch
        foreach (var property in obj.GetType().GetProperties())
        {
            if (property.GetValue(obj) != null)
                continue;                          // redan ifyllt, rör inte

            if (property.PropertyType == typeof(string))
                property.SetValue(obj, "okänd");   // text som saknas blir "okänd"
            else if (property.PropertyType == typeof(int?))
                property.SetValue(obj, 0);         // tal som saknas blir 0
        }
    }
}
```

</details>
