# Records

Många klasser i ett program är bara behållare för data. De ska inte ändras efter att de skapats, och två objekt med samma data ska anses lika. En `record` gör just det med mycket kortare kod.

## En rad, en klass

```csharp
public record Customer(string Name, int Age);

var a = new Customer("Ada", 36);
var b = new Customer("Ada", 36);
Console.WriteLine(a == b);          // True
```

Två records är lika om alla deras värden är lika. För en vanlig klass jämförs i stället om det är samma objekt i minnet.

## Oföränderlig kopia med with

```csharp
var older = a with { Age = 37 };
Console.WriteLine(a.Age);           // 36. a är oförändrad.
```

`with` skapar en ny kopia där några värden ändrats. Den ursprungliga posten finns kvar. Det gör records lätta att resonera om, eftersom ett värde inte ändras under dina fötter.

## Samma jämförelse, två sätt

En vanlig klass jämför referenser med `==`. En `record` jämför innehåll. Samma kod ger alltså olika svar beroende på vilken typ du använder. Det är viktigt att veta när du väljer.

## När är en record rätt?

- Data som bara beskriver något, till exempel en bokning eller en produkt.
- Data som ska jämföras på innehåll, till exempel i en lista eller en dictionary.
- Data som inte ska ändras efter att den skapats.

Om objektet har ett beteende som ändras över tid, till exempel en kundvagn som får nya varor, är en vanlig klass ofta bättre.

## Sammanfattning

- En `record` är en klass för data, med värdelikhet.
- `with` gör en kopia med ändrade värden.
- Använd den för data som inte ska ändras.

## Prova själv

Gör `records_ovning.md`, `records_ovning_2.md` och `records_ovning_3.md`.
