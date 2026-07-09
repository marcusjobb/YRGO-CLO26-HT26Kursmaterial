# Array Övningar 📊

🟡


## Övning 1: Grundläggande Array (Lätt)

Fyll i koden nedan för att skapa en array med 5 superhjältar och skriva ut alla namnen med deras position.

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
using System;

class Program
{
    static void Main(string[] args)
    {
        // TODO: Skapa en array med 5 superhjältar
        string[] heroes = // Din kod här

        // TODO: Loopa genom arrayen och skriv ut position och namn
        for (int i = 0; i < heroes.Length; i++)
        {
            // Din kod här
        }

        // TODO: Skriv ut totala antalet hjältar
        // Din kod här
    }
}
```

### Förväntad Output:
```
Position 0: Iron Man
Position 1: Spider-Man
Position 2: Thor
Position 3: Captain America
Position 4: Hulk
Antal hjältar: 5
```

<details>
<summary>💡 Tips 1: Skapa Array</summary>

- Använd `new string[] { "name1", "name2", "name3", "name4", "name5" }`
- Eller `new string[5]` och tilldela värden separat
- Kom ihåg att arrays är 0-indexerade

</details>

<details>
<summary>💡 Tips 2: Loopa och Skriva Ut</summary>

- Använd `Console.WriteLine($"Position {i}: {heroes[i]}")`
- `{i}` visar positionen, `{heroes[i]}` visar hjältens namn
- Loop-villkoret är `i < heroes.Length`

</details>

<details>
<summary>💡 Tips 3: Totala Antalet</summary>

- Använd `heroes.Length` för att få arrayens storlek
- Skriv: `Console.WriteLine($"Antal hjältar: {heroes.Length}")`

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        // Skapa en array med 5 superhjältar
        string[] heroes = new string[] {
            "Iron Man",
            "Spider-Man",
            "Thor",
            "Captain America",
            "Hulk"
        };

        // Loopa genom arrayen och skriv ut position och namn
        for (int i = 0; i < heroes.Length; i++)
        {
            Console.WriteLine($"Position {i}: {heroes[i]}");
        }

        // Skriv ut totala antalet hjältar
        Console.WriteLine($"Antal hjältar: {heroes.Length}");
    }
}
```

</details>

---

## Övning 2: Array Manipulation (Medium)

Komplettera koden för att skapa en array med 10 slumptal, hitta största och minsta talet, samt beräkna genomsnittet.

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int[] numbers = new int[10];

        // TODO: Fyll arrayen med slumptal mellan 1-100
        for (int i = 0; i < numbers.Length; i++)
        {
            // Din kod här
        }

        // TODO: Skriv ut alla tal på en rad
        Console.Write("Tal: ");
        for (int i = 0; i < numbers.Length; i++)
        {
            // Din kod här (komma mellan tal utom sista)
        }
        Console.WriteLine();

        // TODO: Initiera max, min och sum variabler
        int max = // Din kod här
        int min = // Din kod här
        int sum = 0;

        // TODO: Loopa och hitta max, min, summera
        for (int i = 0; i < numbers.Length; i++)
        {
            // Din kod här för max
            // Din kod här för min
            // Din kod här för sum
        }

        // TODO: Beräkna genomsnitt och skriv ut resultat
        double average = // Din kod här

        Console.WriteLine($"Största tal: {max}");
        Console.WriteLine($"Minsta tal: {min}");
        Console.WriteLine($"Genomsnitt: {average:F1}");
    }
}
```

### Förväntad Output:
```
Tal: 45, 78, 23, 89, 12, 56, 67, 34, 91, 28
Största tal: 91
Minsta tal: 12
Genomsnitt: 52.3
```

<details>
<summary>💡 Tips 1: Slumptal</summary>

- `random.Next(1, 101)` ger tal mellan 1-100
- Tilldela direkt: `numbers[i] = random.Next(1, 101)`

</details>

<details>
<summary>💡 Tips 2: Skriva ut med Komman</summary>

- Skriv talet först: `Console.Write(numbers[i])`
- Lägg till komma om inte sista: `if (i < numbers.Length - 1) Console.Write(", ")`

</details>

<details>
<summary>💡 Tips 3: Max, Min, Genomsnitt</summary>

- Initiera med första elementet: `numbers[0]`
- Jämför i loop: `if (numbers[i] > max) max = numbers[i]`
- Genomsnitt: `(double)sum / numbers.Length`

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```csharp
using System;

class Program
{
    static void Main(string[] args)
    {
        Random random = new Random();
        int[] numbers = new int[10];

        // Fyll arrayen med slumptal mellan 1-100
        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(1, 101);
        }

        // Skriv ut alla tal på en rad
        Console.Write("Tal: ");
        for (int i = 0; i < numbers.Length; i++)
        {
            Console.Write(numbers[i]);
            if (i < numbers.Length - 1) Console.Write(", ");
        }
        Console.WriteLine();

        // Initiera max, min och sum variabler
        int max = numbers[0];
        int min = numbers[0];
        int sum = 0;

        // Loopa och hitta max, min, summera
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] > max) max = numbers[i];
            if (numbers[i] < min) min = numbers[i];
            sum += numbers[i];
        }

        // Beräkna genomsnitt och skriv ut resultat
        double average = (double)sum / numbers.Length;

        Console.WriteLine($"Största tal: {max}");
        Console.WriteLine($"Minsta tal: {min}");
        Console.WriteLine($"Genomsnitt: {average:F1}");
    }
}
```

</details>

---

## Övning 3: Array Sökning med Metoder (Svår)

Skapa en klass med metoder för att söka i arrays. Fyll i de saknade delarna.

```csharp
using System;
using System.Collections.Generic;

class ArraySearcher
{
    // TODO: Implementera metod som hittar alla positioner för ett värde
    public static List<int> FindAllPositions(int[] array, int searchValue)
    {
        List<int> positions = new List<int>();

        // Din kod här - loopa och lägg till positioner

        return positions;
    }

    // TODO: Implementera metod som skriver ut sökresultat
    public static void PrintSearchResult(int[] array, int searchValue, List<int> positions)
    {
        Console.WriteLine($"Söker efter: {searchValue}");

        if (positions.Count == 0)
        {
            // Din kod här - meddelande när inget hittas
        }
        else
        {
            // Din kod här - visa positioner och antal
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] array = { 5, 3, 8, 3, 1, 9, 3, 7, 2 };

        Console.WriteLine($"Array: [{string.Join(", ", array)}]");

        // TODO: Sök efter värdet 3
        int searchValue1 = 3;
        List<int> positions1 = // Din kod här
        ArraySearcher.PrintSearchResult(array, searchValue1, positions1);

        Console.WriteLine();

        // TODO: Sök efter värdet 10 (som inte finns)
        int searchValue2 = 10;
        List<int> positions2 = // Din kod här
        ArraySearcher.PrintSearchResult(array, searchValue2, positions2);
    }
}
```

### Förväntad Output:
```
Array: [5, 3, 8, 3, 1, 9, 3, 7, 2]
Söker efter: 3
Hittade 3 på positionerna: 1, 3, 6
Totalt antal förekomster: 3

Söker efter: 10
Värdet 10 hittades inte i arrayen.
```

<details>
<summary>💡 Tips 1: FindAllPositions Metod</summary>

- Loopa: `for (int i = 0; i < array.Length; i++)`
- Jämför: `if (array[i] == searchValue)`
- Lägg till position: `positions.Add(i)`

</details>

<details>
<summary>💡 Tips 2: PrintSearchResult Metod</summary>

- Ingen träff: `Console.WriteLine($"Värdet {searchValue} hittades inte i arrayen.")`
- Träffar: Använd `string.Join(", ", positions)` för positionslistan
- Visa antal: `positions.Count`

</details>

<details>
<summary>💡 Tips 3: Main Metod</summary>

- Anropa metod: `ArraySearcher.FindAllPositions(array, searchValue1)`
- Samma för båda sökningar, ändra bara searchValue

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```csharp
using System;
using System.Collections.Generic;

class ArraySearcher
{
    // Implementera metod som hittar alla positioner för ett värde
    public static List<int> FindAllPositions(int[] array, int searchValue)
    {
        List<int> positions = new List<int>();

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] == searchValue)
            {
                positions.Add(i);
            }
        }

        return positions;
    }

    // Implementera metod som skriver ut sökresultat
    public static void PrintSearchResult(int[] array, int searchValue, List<int> positions)
    {
        Console.WriteLine($"Söker efter: {searchValue}");

        if (positions.Count == 0)
        {
            Console.WriteLine($"Värdet {searchValue} hittades inte i arrayen.");
        }
        else
        {
            Console.WriteLine($"Hittade {searchValue} på positionerna: {string.Join(", ", positions)}");
            Console.WriteLine($"Totalt antal förekomster: {positions.Count}");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        int[] array = { 5, 3, 8, 3, 1, 9, 3, 7, 2 };

        Console.WriteLine($"Array: [{string.Join(", ", array)}]");

        // Sök efter värdet 3
        int searchValue1 = 3;
        List<int> positions1 = ArraySearcher.FindAllPositions(array, searchValue1);
        ArraySearcher.PrintSearchResult(array, searchValue1, positions1);

        Console.WriteLine();

        // Sök efter värdet 10 (som inte finns)
        int searchValue2 = 10;
        List<int> positions2 = ArraySearcher.FindAllPositions(array, searchValue2);
        ArraySearcher.PrintSearchResult(array, searchValue2, positions2);
    }
}
```
</details>

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
