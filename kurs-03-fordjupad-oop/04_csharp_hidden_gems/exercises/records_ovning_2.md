# Produkter och lika värden

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🟡

**Tid:** ca 20 minuter.

## Uppgift

Skapa `record Product(string Name, decimal Price)`. Skapa två produkter med samma namn och pris och jämför dem. Skapa sedan en lista med produkter och använd `Contains` med ett nytt objekt med samma värden.

Fyll i det som står `/* din kod */`.

```csharp
var a = new Product("Kaffe", 39m);
var b = new Product("Kaffe", 39m);
/* din kod:
   skriv ut om a och b är lika */

var list = new List<Product> { a };
/* din kod:
   skriv ut om listan innehåller b */

/* din kod:
   deklarera en record Product med namn och pris */
```

Exempeloutput:

```text
True
True
```

<details markdown="1">
<summary>Tips 1: recorden står sist</summary>

Med top-level statements ska typer som `record` ligga sist i filen, efter koden som körs. Själva deklarationen är en rad: `record Product(string Name, decimal Price);`

</details>

<details markdown="1">
<summary>Tips 2: lika värden</summary>

`a` och `b` är två olika objekt i minnet. För en vanlig klass skulle `a == b` bli `False`. En record jämför i stället värdena i egenskaperna.

</details>

<details markdown="1">
<summary>Tips 3: Contains</summary>

`Contains` använder samma jämförelse som recorden. Skriv ut resultatet av `a == b` och sedan `list.Contains(b)` med `Console.WriteLine`. Båda ska ge `True`, fast `b` aldrig lades in i listan.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var a = new Product("Kaffe", 39m);
var b = new Product("Kaffe", 39m);
Console.WriteLine(a == b);   // True

var list = new List<Product> { a };
Console.WriteLine(list.Contains(b));   // True

record Product(string Name, decimal Price);
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
