# Async och await: frivillig övning

**Frivillig.** Gör den om du vill träna mer. Den är inte obligatorisk, och den ingår inte i bedömningen.

**Tid:** ca 20 minuter.

## Uppgifter

1. Skriv tre metoder som var och en väntar en sekund och sedan returnerar ett tal.
2. Kör dem efter varandra med `await`. Mät tiden.
3. Kör dem samtidigt med `Task.WhenAll`. Mät tiden igen.

Fyll i det som står `/* din kod */`.

```csharp
using System.Diagnostics;

async Task<int> GetA()
{
    /* din kod (steg 1):
       vänta en sekund
       returnera 1 */
}

async Task<int> GetB()
{
    /* din kod (steg 1):
       vänta en sekund
       returnera 2 */
}

async Task<int> GetC()
{
    /* din kod (steg 1):
       vänta en sekund
       returnera 3 */
}

var sw = Stopwatch.StartNew();
/* din kod (steg 2):
   anropa GetA, GetB och GetC en i taget med await
   spara svaren i a, b och c */
Console.WriteLine($"Efter varandra: summa {a + b + c}, tid {sw.Elapsed.TotalSeconds:F1} s");

sw.Restart();
/* din kod (steg 3):
   starta alla tre metoderna på en gång
   vänta tills alla är klara
   spara svaren i en array som heter results */
Console.WriteLine($"Samtidigt: summa {results.Sum()}, tid {sw.Elapsed.TotalSeconds:F1} s");
```

Exempeloutput (hela körningen tar ca 4 sekunder):

```text
Efter varandra: summa 6, tid 3,0 s
Samtidigt: summa 6, tid 1,0 s
```

Står det `3.0` i stället för `3,0` hos dig? Det beror bara på datorns språkinställning.

<details markdown="1">
<summary>Tips 1: vänta utan att låsa programmet</summary>

Du behöver ett sätt att vänta utan att låsa programmet. Det finns en färdig metod i klassen `Task` som väntar ett visst antal millisekunder.

</details>

<details markdown="1">
<summary>Tips 2: vänta en sekund</summary>

`await Task.Delay(1000);` väntar en sekund. Efter den raden skriver du `return` med ditt tal.

</details>

<details markdown="1">
<summary>Tips 3: efter varandra eller samtidigt</summary>

Efter varandra: skriv tre rader, en per metod, till exempel `int a = await GetA();`.

Samtidigt: anropa alla tre metoderna inuti `Task.WhenAll(...)` och sätt `await` framför hela anropet. Du får tillbaka en `int[]`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

Steg 2 tar ungefär tre sekunder. Steg 3 tar ungefär en sekund, eftersom de väntar samtidigt.

```csharp
using System.Diagnostics;

async Task<int> GetA()
{
    await Task.Delay(1000);
    return 1;
}

async Task<int> GetB()
{
    await Task.Delay(1000);
    return 2;
}

async Task<int> GetC()
{
    await Task.Delay(1000);
    return 3;
}

var sw = Stopwatch.StartNew();
int a = await GetA();
int b = await GetB();
int c = await GetC();
Console.WriteLine($"Efter varandra: summa {a + b + c}, tid {sw.Elapsed.TotalSeconds:F1} s");

sw.Restart();
int[] results = await Task.WhenAll(GetA(), GetB(), GetC());
Console.WriteLine($"Samtidigt: summa {results.Sum()}, tid {sw.Elapsed.TotalSeconds:F1} s");
```

</details>
