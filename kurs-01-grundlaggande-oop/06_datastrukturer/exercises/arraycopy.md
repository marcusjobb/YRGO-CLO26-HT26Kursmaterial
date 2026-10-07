# Plocka ut en del av en array och skapa en ny array av det.

🟢


I denna övning ska vi skapa en array med 10 heltal och sedan plocka ut de fem första talen från den ursprungliga arrayen för att skapa en ny array.

## Instruktioner

1. Skapa en array med 10 heltal.
2. Skapa en ny array som innehåller de fem första talen i den första arrayen.
3. Skriv ut den nya arrayen.

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

## Kodexempel

```csharp
using System;

public class MainClass
{
    public static void Main(string[] args)
    {
        // Skapa en array med 10 heltal
        int[] heltal = { 5, 2, 7, 1, 9, 3, 8, 4, 6, 10 };

        // Skriv ut den ursprungliga arrayen
        Console.Write("Siffror: ");
        PrintArray(heltal);

        // Skapa en ny array som innehåller de fem första talen i den första arrayen
        int[] femForsta = new int[5];
        Array.Copy(heltal, femForsta, 5); // Visst är det snyggt att det finns en inbyggd metod för detta?

        // Skriv ut den nya arrayen
        Console.Write("De fem första talen: ");
        PrintArray(femForsta);
    }

    private static void PrintArray(int[] array)
    {
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
```

## Resultat

```text
Siffror: 5 2 7 1 9 3 8 4 6 10
De fem första talen: 5 2 7 1 9
```

I det här exemplet skapar vi en array med 10 heltal och därefter kopierar vi de fem första talen till en ny array. Den ursprungliga arrayen och den nya arrayen skrivs sedan ut.

Kom ihåg att det här bara är ett exempel på hur du kan lösa uppgiften. Det finns flera sätt att plocka ut en del av en array i C#.

### Facit

<details>
    <summary>Klicka här för att se facit</summary>

```csharp
using System;

public class MainClass
{
    public static void Main(string[] args)
    {
        // Skapa en array med 10 heltal
        int[] heltal = { 5, 2, 7, 1, 9, 3, 8, 4, 6, 10 };

        // Skriv ut den ursprungliga arrayen
        Console.Write("Siffror: ");
        PrintArray(heltal);

        // Skapa en ny array som innehåller de fem första talen i den första arrayen
        int[] femForsta = new int[5];
        Array.Copy(heltal, femForsta, 5);

        // Skriv ut den nya arrayen
        Console.Write("De fem första talen: ");
        PrintArray(femForsta);
    }

    private static void PrintArray(int[] array)
    {
        foreach (int num in array)
        {
            Console.Write(num + " ");
        }
        Console.WriteLine();
    }
}
```

</details>

I facit-lösningen ovan skapar vi först en array med 10 heltal. Sedan skriver vi ut den ursprungliga arrayen genom att använda hjälpmetoden `PrintArray`. Därefter skapar vi en ny array `femForsta` med en storlek på 5 och kopierar de fem första talen från den ursprungliga arrayen till den nya arrayen med hjälp av `Array.Copy`-metoden. Slutligen skriver vi ut den nya arrayen genom att använda `PrintArray` igen.

Det här är ett av flera sätt att lösa uppgiften — `Array.Copy` är bekvämt, men du kan också lösa det med en vanlig for-loop. Prova gärna båda.
