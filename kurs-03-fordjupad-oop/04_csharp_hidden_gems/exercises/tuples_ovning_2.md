# Räkna jämna och udda

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv `(int evens, int odds) CountEvenAndOdd(int[] numbers)` som returnerar två värden. Använd deconstruction när du anropar den.

Fyll i det som står `/* din kod */`.

```csharp
(int evens, int odds) CountEvenAndOdd(int[] numbers)
{
    /* din kod:
       starta två räknare på noll
       gå igenom varje tal
         om talet är jämnt, öka jämna
         annars, öka udda
       returnera båda räknarna */
}

var (e, o) = CountEvenAndOdd(new[] { 1, 2, 3, 4, 5 });
Console.WriteLine($"Jämna: {e}, udda: {o}");
```

Exempeloutput:

```text
Jämna: 2, udda: 3
```

<details markdown="1">
<summary>Tips 1: två räknare</summary>

Du behöver hålla koll på två saker samtidigt. Skapa två variabler som börjar på 0, en för jämna tal och en för udda.

</details>

<details markdown="1">
<summary>Tips 2: jämnt eller udda?</summary>

Ett tal är jämnt om resten blir 0 när du delar med 2. I C# skriver du det med `%`, till exempel `n % 2 == 0`.

</details>

<details markdown="1">
<summary>Tips 3: loop och retur</summary>

Använd `foreach (int n in numbers)` och öka rätt räknare inne i loopen med `if` och `else`. När loopen är klar returnerar du båda med `return (evens, odds);`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
(int evens, int odds) CountEvenAndOdd(int[] numbers)
{
    int evens = 0, odds = 0;
    foreach (int n in numbers)
    {
        if (n % 2 == 0) evens++;
        else odds++;
    }
    return (evens, odds);
}

var (e, o) = CountEvenAndOdd(new[] { 1, 2, 3, 4, 5 });
Console.WriteLine($"Jämna: {e}, udda: {o}");   // Jämna: 2, udda: 3
```

</details>
