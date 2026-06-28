---

title: Hitta det största elementet i en array
author: Marcus Ackre Medina
type: article
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/datastrukturer/arrayexercises/ovning2.md"
description: "Skriv en metod som tar emot en array av heltal och returnerar det största elementet i arrayen."
tags: ["array", "csharp", "datastrukturer", "elementet", "git", "hitta", "ovning2", "största"]
week_fit: []
---

# Hitta det största elementet i en array

🟢


#### Beskrivning av övningen

Skriv en metod som tar emot en array av heltal och returnerar det största elementet i arrayen.

Det är bra om du kan lösa uppgiften utan att använda LINQ. Om du vill kan du också lösa uppgiften med hjälp av LINQ. Det är dock inte ett krav. Men det är alltid bra att vara bekant med LINQ. Även om du helst inte vill använda LINQ i denna uppgift kan det vara bra att lösa uppgiften med hjälp av LINQ också för att se hur det kan göras.

Om du löser det utan Linq har du lärt dig algorithmiskt tänkande. Om du löser det med LINQ har du lärt dig att använda LINQ. Båda är bra att kunna. Men i detta fall rekommenderar jag att du försöker lösa det utan LINQ först.

#### Kodmall

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public static int FindLargestElement(int[] numbers)
{
    // Implementera kod här
}

// Exempelanvändning
int[] numbers = { 5, 8, 2, 11, 3 };
int largest = FindLargestElement(numbers);
Console.WriteLine(largest);
```

#### Förväntad output

```
11
```

#### Facit

<details><summary>Klicka här för att se facit</summary>

```csharp
public static int FindLargestElement(int[] numbers)
{
    int largest = numbers[0];
    for (int i = 1; i < numbers.Length; i++)
    {
        if (numbers[i] > largest)
        {
            largest = numbers[i];
        }
    }
    return largest;
}
```

OBS! Detta kan också lösas med hjälp av LINQ:

```csharp
public static int FindLargestElement(int[] numbers)
{
    return numbers.Max();
}
```

</details>

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
