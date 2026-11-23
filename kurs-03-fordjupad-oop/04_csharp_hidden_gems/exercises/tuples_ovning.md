# Tuples: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv en metod som tar en lista med heltal och returnerar `(int min, int max)`.
2. Använd deconstruction för att skriva ut min och max.

Fyll i det som står `/* din kod */`.

```csharp
(int min, int max) MinMax(List<int> numbers)
{
    /* din kod 1:
       ta fram det minsta talet i listan
       ta fram det största talet i listan
       returnera båda som ett par */
}

/* din kod 2:
   anropa MinMax med listan 4, 9, 2
   packa upp svaret i två variabler, min och max */
Console.WriteLine($"Min: {min}, max: {max}");
```

Exempeloutput:

```text
Min: 2, max: 9
```

<details markdown="1">
<summary>Tips 1: vad är en tuple?</summary>

En tuple är ett sätt att returnera flera värden på en gång. Returtypen `(int min, int max)` betyder att metoden lämnar tillbaka två heltal som hör ihop.

</details>

<details markdown="1">
<summary>Tips 2: minsta och största talet</summary>

En `List<int>` har färdiga metoder för det här. Titta på `Min()` och `Max()`. Du skapar en tuple genom att skriva värdena inom parentes, till exempel `(a, b)`.

</details>

<details markdown="1">
<summary>Tips 3: packa upp svaret</summary>

Med deconstruction får du två variabler direkt: `var (min, max) = ...`. Till höger skriver du anropet till `MinMax` med en ny lista. I metoden räcker det med en rad: `return (numbers.Min(), ...);`

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
(int min, int max) MinMax(List<int> numbers)
{
    return (numbers.Min(), numbers.Max());
}

var (min, max) = MinMax(new List<int> { 4, 9, 2 });
Console.WriteLine($"Min: {min}, max: {max}");
```

</details>
