# Arrayer — livekodning 2026-09-23

Från vanliga endimensionella arrayer till en tvådimensionell matris, och de olika sätten man kan loopa igenom dem.

## Endimensionella arrayer

```csharp
// vanliga en dimensions arrayer
int[] nummer = { 1, 2, 3, 4, 6 };
int[] nummer2 = new int[10];
bool[] booleans = new bool[10];
```

Tre sätt att skapa en array: `{ 1, 2, 3, 4, 6 }` fyller den direkt med värden och bestämmer storleken automatiskt (5 platser). `new int[10]` skapar en array med 10 platser, alla satta till standardvärdet för `int` (0). `new bool[10]` gör samma sak för `bool` — standardvärdet där är `false`.

## Tvådimensionell array

```csharp
int[,] matrix = new int[10, 10];

matrix[3, 4] = 9;
matrix[3, 5] = 1;
```

`int[,]` (kommatecken inuti hakparenteserna) är en riktig matris — 10 rader gånger 10 kolumner, 100 platser totalt, alla `int`. Man adresserar en cell med två index: `matrix[rad, kolumn]`.

## Loopa med foreach

```csharp
foreach (int num in nummer)
{
    Console.Write(num + ",");
}
Console.WriteLine();
foreach (int num in nummer2)
{
    Console.Write(num + ",");
}
Console.WriteLine();
foreach (bool value in booleans)
{
    Console.Write(value + ",");
}
Console.WriteLine();
```

`foreach` går igenom **varje element** i tur och ordning, utan att du behöver hålla reda på index själv. Bra när du bara vill läsa värden, inte när du behöver veta *var* i arrayen du är.

## Loopa en matris med for

```csharp
for (int x = 0; x < 10; x++)
{
    Console.Write($"Row {x} : ");
    for (int y = 0; y < 10; y++)
    {
        Console.Write(matrix[x, y] + ",");
    }
    Console.WriteLine();
}
```

En tvådimensionell array loopas med **två** kapslade `for`-loopar — den yttre går genom raderna (`x`), den inre går genom kolumnerna i just den raden (`y`). Till skillnad från `foreach` vet du här exakt vilken position du är på, vilket krävs för att skriva `matrix[x, y]`.

**Varför `for` här och inte `foreach`?** `foreach` skulle gå igenom alla 100 värden i turordning, men du hade tappat vilken rad/kolumn varje värde satt i — omöjligt att gruppera utskriften per rad. `for` med explicit index ger dig kontrollen att bryta av med `Console.WriteLine()` efter varje rad.
