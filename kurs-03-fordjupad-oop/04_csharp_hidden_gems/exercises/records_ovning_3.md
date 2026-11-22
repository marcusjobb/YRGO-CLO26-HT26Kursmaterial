# Rabatt med with

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Använd `record Product(string Name, decimal Price)`. Skapa en metod som returnerar en kopia av produkten med 10 % lägre pris, med `with`. Originalet ska vara oförändrat.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
Product Discount(Product product) => product with { Price = product.Price * 0.9m };

var original = new Product("Kaffe", 40m);
var sale = Discount(original);
Console.WriteLine($"{original.Price} → {sale.Price}");   // 40 → 36,0
```

</details>
