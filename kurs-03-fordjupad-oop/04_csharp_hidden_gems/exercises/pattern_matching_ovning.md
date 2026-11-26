# Pattern matching: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv en metod `string Grade(int score)` med `switch`-uttryck för poäng 0–100: 90 och uppåt är A, 75 och uppåt B, 50 och uppåt C, annars F.
2. Lägg till en `when` som ger felmeddelandet "ogiltig" för poäng under 0 eller över 100.

Fyll i det som står `/* din kod */`.

```csharp
string Grade(int score) => score switch
{
    /* din kod (uppgift 2):
       om poängen är under 0 eller över 100, ge "ogiltig" */

    /* din kod (uppgift 1):
       90 eller mer ger "A"
       75 eller mer ger "B"
       50 eller mer ger "C"
       allt annat ger "F" */
};

int[] scores = { 95, 80, 60, 30, -5, 120 };
foreach (var score in scores)
{
    Console.WriteLine($"{score} poäng: {Grade(score)}");
}
```

Exempeloutput:

```text
95 poäng: A
80 poäng: B
60 poäng: C
30 poäng: F
-5 poäng: ogiltig
120 poäng: ogiltig
```

<details markdown="1">
<summary>Tips 1: ordningen spelar roll</summary>

Ett `switch`-uttryck testar raderna uppifrån och ned. Den första raden som passar vinner. Fundera på vilken rad som måste komma först för att -5 och 120 inte ska bli "F" eller "A".

</details>

<details markdown="1">
<summary>Tips 2: relationsmönster</summary>

Du kan jämföra direkt i ett mönster, utan `if`. Skriv till exempel `>= 90` till vänster om `=>`. Sist lägger du `_`, som fångar allt som är kvar.

</details>

<details markdown="1">
<summary>Tips 3: ogiltiga poäng</summary>

Uppgiften nämner `when`, men det går också att kombinera mönster med `or`. Den första raden kan se ut så här:

```csharp
< 0 or > 100 => "ogiltig",
```

Med `when` blir det `_ when score < 0 || score > 100 => "ogiltig",`. Båda fungerar.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string Grade(int score) => score switch
{
    < 0 or > 100 => "ogiltig",
    >= 90 => "A",
    >= 75 => "B",
    >= 50 => "C",
    _ => "F"
};

int[] scores = { 95, 80, 60, 30, -5, 120 };
foreach (var score in scores)
{
    Console.WriteLine($"{score} poäng: {Grade(score)}");
}
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
