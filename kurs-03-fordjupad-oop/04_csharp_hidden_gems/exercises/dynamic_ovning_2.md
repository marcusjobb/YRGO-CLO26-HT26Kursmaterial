# Dynamiska fält med ExpandoObject

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

1. Skapa ett `dynamic person = new ExpandoObject();` och lägg till `Name` och `Age` på det.
2. Skriv ut båda fälten.
3. Lägg till ett fält till som du inte deklarerat i förväg. Vad händer?

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
using System.Dynamic;

dynamic person = new ExpandoObject();
person.Name = "Ada";
person.Age = 36;
person.City = "Göteborg";   // Nytt fält, inget kompileringsfel
Console.WriteLine($"{person.Name}, {person.Age}, {person.City}");
```

</details>
