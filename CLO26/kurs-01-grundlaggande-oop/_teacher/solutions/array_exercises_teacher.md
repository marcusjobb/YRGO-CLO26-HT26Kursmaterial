# Lärarfacit — Array Övningar

## Övning 1 — Grundläggande array

```csharp
string[] heroes = { "Iron Man", "Spider-Man", "Thor", "Captain America", "Hulk" };

for (int i = 0; i < heroes.Length; i++)
    Console.WriteLine($"Position {i}: {heroes[i]}");

Console.WriteLine($"Antal hjältar: {heroes.Length}");
```

## Övning 2 — Array manipulation med slumptal

```csharp
Random random = new Random();
int[] numbers = new int[10];

for (int i = 0; i < numbers.Length; i++)
    numbers[i] = random.Next(1, 101);

Console.Write("Tal: ");
for (int i = 0; i < numbers.Length; i++)
{
    Console.Write(numbers[i]);
    if (i < numbers.Length - 1) Console.Write(", ");
}
Console.WriteLine();

int max = numbers[0];
int min = numbers[0];
int sum = 0;

for (int i = 0; i < numbers.Length; i++)
{
    if (numbers[i] > max) max = numbers[i];
    if (numbers[i] < min) min = numbers[i];
    sum += numbers[i];
}

double average = (double)sum / numbers.Length;
Console.WriteLine($"Största tal: {max}");
Console.WriteLine($"Minsta tal: {min}");
Console.WriteLine($"Genomsnitt: {average:F1}");
```

## Övning 3 — Sökning med metoder

```csharp
public static List<int> FindAllPositions(int[] array, int searchValue)
{
    List<int> positions = new List<int>();
    for (int i = 0; i < array.Length; i++)
        if (array[i] == searchValue) positions.Add(i);
    return positions;
}

public static void PrintSearchResult(int[] array, int searchValue, List<int> positions)
{
    Console.WriteLine($"Söker efter: {searchValue}");
    if (positions.Count == 0)
        Console.WriteLine($"Värdet {searchValue} hittades inte i arrayen.");
    else
    {
        Console.WriteLine($"Hittade {searchValue} på positionerna: {string.Join(", ", positions)}");
        Console.WriteLine($"Totalt antal förekomster: {positions.Count}");
    }
}
```

## Pedagogisk poäng

Övning 2: initiera `max = numbers[0]` och `min = numbers[0]` — bättre än `int.MinValue`/`MaxValue` här eftersom arrayen garanterat har element. Visa hur initialisering av jämförelsevärde är ett designval.

Övning 3: `List<int>` för returvärde är snyggt — metoden vet inte hur anroparen vill använda positionerna, så den returnerar data och låter anroparen bestämma vad som ska skrivas ut. Bra separation.
