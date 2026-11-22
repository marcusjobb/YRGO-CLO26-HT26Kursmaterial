# Produkter och lika värden

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skapa `record Product(string Name, decimal Price)`. Skapa två produkter med samma namn och pris och jämför dem. Skapa sedan en lista med produkter och använd `Contains` med ett nytt objekt med samma värden.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
record Product(string Name, decimal Price);

var a = new Product("Kaffe", 39m);
var b = new Product("Kaffe", 39m);
Console.WriteLine(a == b);   // True

var list = new List<Product> { a };
Console.WriteLine(list.Contains(b));   // True
```

</details>
