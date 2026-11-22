# Billigaste produkten

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Du har en lista med `(string Name, decimal Price)`. Skriv en metod som returnerar namnet och priset på den billigaste produkten, som en tuple.

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var products = new List<(string Name, decimal Price)>
{
    ("Kaffe", 39m), ("Bulle", 25m), ("Te", 19m)
};

(string Name, decimal Price) Cheapest(List<(string Name, decimal Price)> items)
{
    return items.OrderBy(p => p.Price).First();
}

var (name, price) = Cheapest(products);
Console.WriteLine($"{name} kostar {price} kr");   // Te kostar 19 kr
```

</details>
