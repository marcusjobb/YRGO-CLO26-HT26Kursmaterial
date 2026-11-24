# Antal i en lista som kan vara null

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Nivå:** 🟡

**Tid:** ca 20 minuter.

## Uppgift

Skriv en metod `int CountItems(List<string>? items)` som returnerar antalet, eller 0 om listan är `null`. Använd `?.` och `??`. Ingen `if`.

Fyll i det som står `/* din kod */`.

```csharp
int CountItems(List<string>? items) =>
    /* din kod:
       hämta antalet i listan om den finns
       finns ingen lista, ge 0 */;

var fruits = new List<string> { "äpple", "banan", "päron" };
var empty = new List<string>();
List<string>? missing = null;

Console.WriteLine($"Frukter: {CountItems(fruits)}");
Console.WriteLine($"Tom lista: {CountItems(empty)}");
Console.WriteLine($"Ingen lista: {CountItems(missing)}");
```

Exempeloutput:

```text
Frukter: 3
Tom lista: 0
Ingen lista: 0
```

<details markdown="1">
<summary>Tips 1: två olika nollor</summary>

En tom lista och en lista som är `null` är inte samma sak. Den tomma listan finns, den har bara 0 saker i sig. Den som är `null` finns inte alls. Båda ska ge 0 här.

</details>

<details markdown="1">
<summary>Tips 2: Count på något som kan vara null</summary>

`items.Count` kraschar om `items` är `null`. Med `items?.Count` får du `null` i stället för ett fel. Typen blir då `int?`, alltså ett heltal som kan saknas.

</details>

<details markdown="1">
<summary>Tips 3: gör om int? till int</summary>

Metoden ska returnera `int`, inte `int?`. Lägg `??` efter `items?.Count` och skriv vad som ska gälla när värdet saknas.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
int CountItems(List<string>? items) => items?.Count ?? 0;

var fruits = new List<string> { "äpple", "banan", "päron" };
var empty = new List<string>();
List<string>? missing = null;

Console.WriteLine($"Frukter: {CountItems(fruits)}");
Console.WriteLine($"Tom lista: {CountItems(empty)}");
Console.WriteLine($"Ingen lista: {CountItems(missing)}");
```

</details>
