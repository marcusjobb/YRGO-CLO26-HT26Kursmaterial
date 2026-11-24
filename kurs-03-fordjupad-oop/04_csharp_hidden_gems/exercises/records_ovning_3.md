# Rabatt med with

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🔴

**Tid:** ca 20 minuter.

## Uppgift

Använd `record Product(string Name, decimal Price)`. Skapa en metod som returnerar en kopia av produkten med 10 % lägre pris, med `with`. Originalet ska vara oförändrat.

Fyll i det som står `/* din kod */`.

```csharp
Product Discount(Product product)
{
    /* din kod:
       gör en kopia av product
       sätt priset till 90 % av det gamla priset
       returnera kopian */
}

var original = new Product("Kaffe", 40m);
var sale = Discount(original);
Console.WriteLine($"{original.Price} → {sale.Price}");

record Product(string Name, decimal Price);
```

Exempeloutput:

```text
40 → 36,0
```

Talet skrivs med komma eftersom datorn har svenska inställningar. Med engelska inställningar blir det `36.0`.

<details markdown="1">
<summary>Tips 1: ändra inte originalet</summary>

En positionell record går inte att ändra efter att den skapats. Därför gör du en kopia där bara priset är nytt.

</details>

<details markdown="1">
<summary>Tips 2: with</summary>

Med `with` skapar du en kopia och ändrar de egenskaper du anger inom klamrar, till exempel `product with { Price = ... }`.

</details>

<details markdown="1">
<summary>Tips 3: räkna ut 10 % rabatt</summary>

10 % lägre pris är samma sak som 90 % av priset. Multiplicera med `0.9m`. Glöm inte `m`, annars blir det en `double` och den går inte att blanda med `decimal`. Det nya priset blir `product.Price * 0.9m`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
Product Discount(Product product) => product with { Price = product.Price * 0.9m };

var original = new Product("Kaffe", 40m);
var sale = Discount(original);
Console.WriteLine($"{original.Price} → {sale.Price}");   // 40 → 36,0

record Product(string Name, decimal Price);
```

</details>
