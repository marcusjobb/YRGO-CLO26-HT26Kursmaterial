# Lärarfacit — Arraycopy

## Lösning med Array.Copy

```csharp
using System;

public class MainClass
{
    public static void Main(string[] args)
    {
        int[] heltal = { 5, 2, 7, 1, 9, 3, 8, 4, 6, 10 };

        Console.Write("Siffror: ");
        PrintArray(heltal);

        int[] femForsta = new int[5];
        Array.Copy(heltal, femForsta, 5);

        Console.Write("De fem första talen: ");
        PrintArray(femForsta);
    }

    private static void PrintArray(int[] array)
    {
        foreach (int num in array)
            Console.Write(num + " ");
        Console.WriteLine();
    }
}
```

## Alternativ lösning — for-loop

```csharp
int[] femForsta = new int[5];
for (int i = 0; i < 5; i++)
    femForsta[i] = heltal[i];
```

## Pedagogisk poäng

`Array.Copy` är bekvämt men inte magiskt — det gör exakt samma sak som for-loopen.
Visa båda versionerna och låt dem diskutera vilket de föredrar och varför.
Poängtera att `Array.Copy` inte kopierar ett objekt djupt — om arrayen innehöll objekt (referenstyper) skulle båda arrayerna peka på samma instanser.
