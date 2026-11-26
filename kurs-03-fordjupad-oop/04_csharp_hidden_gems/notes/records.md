# Records

Anders har två kunder i kön som båda ropar `Latte, 35 kr`. Han jämför beställningarna i koden och får `false`, fast de är identiska. Och när han ska ge en av dem rabatt vill han inte ändra originalet. Vad behöver Anders kunna göra?

Tänk på två kontrakt med exakt samma innehåll. De är lika, eftersom det är innehållet som räknas och inte vilket pappersexemplar du håller i. En `record` tycker likadant: den jämför innehållet, inte vilket exemplar det är.

## En rad, en klass

```csharp
public record Customer(string Name, int Age);

var a = new Customer("Ada", 36);
var b = new Customer("Ada", 36);
Console.WriteLine(a == b);          // True
```

Två objekt är lika om deras värden är lika, inte om de är samma objekt i minnet. En rad räcker för att få en hel klass med konstruktor, properties och jämförelse.

## Records är skrivskyddade

```csharp
var a = new Customer("Ada", 36);
a.Age = 40;   // går inte
```

Kompilatorn svarar så här:

```
error CS8852: Init-only property or indexer 'Customer.Age' can only be assigned in an object initializer, ...
```

Värdena sätts när objektet skapas. Sedan är de låsta. Vill du ha en ändrad version gör du en kopia, och det gör `with`.

## Oföränderlig kopia med with

```csharp
var older = a with { Age = 37 };
Console.WriteLine(a.Age);           // 36. a är oförändrad.
Console.WriteLine(older.Age);       // ger 37
```

`with` är ett måste när du vill ändra något, eftersom du inte kan skriva direkt i en record. Du får en ny kopia där bara det du nämner ändras.

## Samma värden, olika svar

Nu jämför vi en vanlig `class` med en `record` som har exakt samma innehåll.

```csharp
class OrderClass
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

record OrderRecord(string Name, decimal Price);
```

Två typer med samma innehåll. Den ena är en `class`, den andra en `record`. Vi skapar två beställningar av varje och jämför:

```csharp
var c1 = new OrderClass { Name = "Latte", Price = 35 };
var c2 = new OrderClass { Name = "Latte", Price = 35 };
Console.WriteLine(c1 == c2);   // False

var r1 = new OrderRecord("Latte", 35);
var r2 = new OrderRecord("Latte", 35);
Console.WriteLine(r1 == r2);   // True
```

En `class` jämför om det är **samma objekt**. En `record` jämför **innehållet**. Samma kod ger alltså olika svar beroende på vilken typ du använder, och det är bra att veta när du väljer.

## Anders ger rabatt

```csharp
var order = new OrderRecord("Latte", 35);
var rabatt = order with { Price = 30 };

Console.WriteLine(order.Price);    // 35, originalet är oförändrat
Console.WriteLine(rabatt.Price);   // 30
```

`with` gör en kopia där bara det du nämner ändras. Originalbeställningen finns kvar, och rabatten är en ny beställning.

## Proffsens tips: ändra aldrig originalet

Du suddar inte på originalkvittot. Du skriver ut ett nytt. Det är det `with` gör åt dig.

Använd en `record` för data som inte ska ändras: en beställning, ett kvitto, ett svar från ett API (Application Programming Interface, ett gränssnitt mellan program). Behöver du en ändrad version, gör en kopia med `with`.

## Summan av kardemumman

- En `record` är en klass för data.
- Värdena är skrivskyddade när objektet är skapat.
- Den jämförs på innehåll.
- `with` gör en ändrad kopia.

## Prova själv

Gör `records_ovning.md`, `records_ovning_2.md` och `records_ovning_3.md`.

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
