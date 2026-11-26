# Beskriv ett objekt

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv `string Describe(object? o)` som returnerar:
- "heltal: n" om värdet är ett heltal
- "text med n tecken" om det är en sträng
- "tomt" om det är `null`
- "okänt" för allt annat

Fyll i det som står `/* din kod */`.

```csharp
string Describe(object? o) => o switch
{
    /* din kod:
       om o är null, ge "tomt"
       om o är ett heltal, ge "heltal: " och talet
       om o är en sträng, ge "text med " och antal tecken
       allt annat ger "okänt" */
};

Console.WriteLine(Describe(42));
Console.WriteLine(Describe("Hej på dig"));
Console.WriteLine(Describe(null));
Console.WriteLine(Describe(3.5));
```

Exempeloutput:

```text
heltal: 42
text med 10 tecken
tomt
okänt
```

<details markdown="1">
<summary>Tips 1: ett switch-uttryck på typen</summary>

Ett `switch`-uttryck kan kolla vilken typ ett värde har. Varje rad är ett mönster, sedan `=>` och sedan det som ska returneras.

</details>

<details markdown="1">
<summary>Tips 2: typmönster med variabel</summary>

Skriver du `int n` som mönster händer två saker. Raden passar bara om `o` är en `int`, och då får du talet i variabeln `n`. Samma sak fungerar med `string s`, och då kan du använda `s.Length`.

</details>

<details markdown="1">
<summary>Tips 3: null och resten</summary>

`null` är ett eget mönster. `_` fångar allt som inte passat någon annan rad, så den ska stå sist. Två av raderna kan se ut så här:

```csharp
null => "tomt",
int n => $"heltal: {n}",
```

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
string Describe(object? o) => o switch
{
    null => "tomt",
    int n => $"heltal: {n}",
    string s => $"text med {s.Length} tecken",
    _ => "okänt"
};

Console.WriteLine(Describe(42));
Console.WriteLine(Describe("Hej på dig"));
Console.WriteLine(Describe(null));
Console.WriteLine(Describe(3.5));
```

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
