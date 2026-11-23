# Tre samtidiga väntanden

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgift

Skriv tre metoder som var och en returnerar en sträng efter en fördröjning. Kör dem samtidigt med `Task.WhenAll` och skriv ut alla resultat.

Fyll i det som står `/* din kod */`.

```csharp
async Task<string> Fetch(string name, int ms)
{
    await Task.Delay(ms);
    return $"{name} klar";
}

/* din kod:
   starta Fetch för A (1000 ms), B (800 ms) och C (1200 ms) på en gång
   vänta tills alla tre är klara
   spara svaren i results */

foreach (var r in results)
    Console.WriteLine(r);
```

Exempeloutput (allt skrivs ut på en gång, efter ca 1,2 sekunder):

```text
A klar
B klar
C klar
```

<details markdown="1">
<summary>Tips 1: starta först, vänta sedan</summary>

Om du skriver `await` framför varje anrop för sig, väntar de på varandra. Då tar det 3 sekunder. Du vill i stället starta alla tre först och vänta en gång.

</details>

<details markdown="1">
<summary>Tips 2: Task.WhenAll</summary>

`Task.WhenAll` tar emot flera uppgifter och blir klar när den sista är klar. Den långsammaste (C, 1200 ms) bestämmer hur lång tid det tar.

</details>

<details markdown="1">
<summary>Tips 3: nästan hela raden</summary>

Skriv `var results = await Task.WhenAll(...);` och lägg de tre anropen av `Fetch` inuti parentesen, åtskilda med komman. `results` blir en `string[]` i samma ordning som du skrev anropen.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
async Task<string> Fetch(string name, int ms)
{
    await Task.Delay(ms);
    return $"{name} klar";
}

var results = await Task.WhenAll(
    Fetch("A", 1000),
    Fetch("B", 800),
    Fetch("C", 1200));

foreach (var r in results)
    Console.WriteLine(r);
```

</details>
