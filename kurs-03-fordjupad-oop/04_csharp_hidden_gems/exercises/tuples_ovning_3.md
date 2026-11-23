# Billigaste produkten

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Du har en lista med `(string Name, decimal Price)`. Skriv en metod som returnerar namnet och priset på den billigaste produkten, som en tuple.

Fyll i det som står `/* din kod */`.

```csharp
var products = new List<(string Name, decimal Price)>
{
    ("Kaffe", 39m), ("Bulle", 25m), ("Te", 19m)
};

(string Name, decimal Price) Cheapest(List<(string Name, decimal Price)> items)
{
    /* din kod:
       sortera items på pris, lägst först
       returnera den första */
}

var (name, price) = Cheapest(products);
Console.WriteLine($"{name} kostar {price} kr");
```

Exempeloutput:

```text
Te kostar 19 kr
```

<details markdown="1">
<summary>Tips 1: varje produkt är en tuple</summary>

Varje element i listan är redan en tuple med `Name` och `Price`. Du kan alltså returnera ett element direkt, utan att bygga en ny tuple.

</details>

<details markdown="1">
<summary>Tips 2: sortera på pris</summary>

LINQ-metoden `OrderBy` sorterar en lista. Du talar om vad den ska sortera på med en lambda, till exempel `p => p.Price`.

</details>

<details markdown="1">
<summary>Tips 3: ta den första</summary>

Efter sorteringen ligger den billigaste först. Hämta den med `First()`. Hela metoden kan skrivas på en rad: `return items.OrderBy(...).First();`

</details>

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
