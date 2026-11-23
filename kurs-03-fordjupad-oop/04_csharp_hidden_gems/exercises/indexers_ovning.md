# Indexers: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Lägg till en kontroll i indexern: om index är utanför hyllan ska du kasta `ArgumentOutOfRangeException`.
2. Testa med index 5 och -1.

Fyll i det som står `/* din kod */`.

```csharp
var shelf = new Shelf();
shelf[0] = "Clean Code";
Console.WriteLine(shelf[0]);

foreach (int index in new[] { 5, -1 })
{
    try
    {
        Console.WriteLine(shelf[index]);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine($"Index {index} finns inte på hyllan.");
    }
}

class Shelf
{
    private readonly string[] books = new string[5];

    public string this[int index]
    {
        get => books[CheckIndex(index)];
        set => books[CheckIndex(index)] = value;
    }

    private int CheckIndex(int index)
    {
        /* din kod:
           om index är mindre än 0 eller för stort för hyllan,
             kasta ArgumentOutOfRangeException
           annars returnera index */
    }
}
```

Exempeloutput:

```text
Clean Code
Index 5 finns inte på hyllan.
Index -1 finns inte på hyllan.
```

<details markdown="1">
<summary>Tips 1: var sitter kontrollen?</summary>

Både `get` och `set` anropar `CheckIndex`. Då räcker det att skriva kontrollen på ett ställe, och den gäller överallt där hyllan används.

</details>

<details markdown="1">
<summary>Tips 2: vilka index är giltiga?</summary>

Hyllan har 5 platser, med index 0 till 4. Använd `books.Length` i stället för att skriva 5, så fungerar koden även om hyllan blir större.

</details>

<details markdown="1">
<summary>Tips 3: kasta undantaget</summary>

Skriv ett `if` med `index < 0 || index >= books.Length`. Inuti skriver du `throw new ArgumentOutOfRangeException(nameof(index));`. Efter `if`-satsen returnerar du `index`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
var shelf = new Shelf();
shelf[0] = "Clean Code";
Console.WriteLine(shelf[0]);

foreach (int index in new[] { 5, -1 })
{
    try
    {
        Console.WriteLine(shelf[index]);
    }
    catch (ArgumentOutOfRangeException)
    {
        Console.WriteLine($"Index {index} finns inte på hyllan.");
    }
}

class Shelf
{
    private readonly string[] books = new string[5];

    public string this[int index]
    {
        get => books[CheckIndex(index)];
        set => books[CheckIndex(index)] = value;
    }

    private int CheckIndex(int index)
    {
        if (index < 0 || index >= books.Length)
            throw new ArgumentOutOfRangeException(nameof(index));
        return index;
    }
}
```

</details>
