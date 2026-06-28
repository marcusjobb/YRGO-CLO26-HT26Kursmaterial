---

title: Dictionary
author: Marcus Ackre Medina
type: article
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/datastrukturer/dictionary.md"
description: "<details open markdown='block'>"
tags: ["csharp", "datastrukturer", "dictionary", "git", "oop"]
week_fit: []
---

# Dictionary

🟢


<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. TOC
{:toc}

</details>

## Beskrivning

En Dictionary är en datastruktur som tillåter oss att lagra och hämta värden baserat på nycklar. Den fungerar på ett liknande sätt som en telefonbok där vi kan slå upp ett namn (nyckel) för att få fram ett telefonnummer (värde). Dictionary är en kraftfull datastruktur inom programmering som erbjuder snabb åtkomst och effektiva sökningar.

## Exempel

Här är ett exempel på hur man kan använda en Dictionary i C#:

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        // Skapa en Dictionary för att lagra hjältarnas namn och deras hemvärld
        Dictionary<string, string> heroes = new Dictionary<string, string>();

        // Lägg till värden i Dictionary
        heroes["Superman"] = "DC";
        heroes["Spider-Man"] = "Marvel";
        heroes["Batman"] = "DC";

        // Hämta hemvärlden för en specifik hjälte
        string supermanHomeWorld = heroes["Superman"];
        Console.WriteLine("Hemvärld för Superman: " + supermanHomeWorld);

        // Uppdatera hemvärlden för en hjälte
        heroes["Spider-Man"] = "DC";

        // Kontrollera om en hjälte finns i Dictionary
        if (heroes.ContainsKey("Batman"))
        {
            Console.WriteLine("Batman finns i Dictionary.");
        }

        // Ta bort en hjälte från Dictionary
        heroes.Remove("Superman");

        // Loopa genom alla hjältarnas namn och hemvärldar i Dictionary
        foreach (var kvp in heroes)
        {
            string heroName = kvp.Key;
            string homeWorld = kvp.Value;
            Console.WriteLine("Hjälte: " + heroName + ", Hemvärld: " + homeWorld);
        }
    }
}
```

Detta kodexempel skapar en Dictionary med nycklar av typen string och värden av typen int. Vi lägger till några värden i Dictionary, hämtar och uppdaterar värden, kontrollerar om en nyckel finns och tar bort värden från Dictionary. Till sist loopar vi igenom alla nycklar och värden i Dictionary och skriver ut dem.

Detta är bara ett grundläggande exempel på hur man kan använda Dictionary i C#. Det finns mycket mer att utforska och lära sig om denna kraftfulla datastruktur.

## Slutsats

Dictionary är en värdefull datastruktur som tillåter oss att lagra och hämta värden baserat på nycklar. Det erbjuder snabb åtkomst och effektiva sökningar, vilket gör det till en populär datastruktur inom programmering. Genom att använda Dictionary kan vi effektivt hantera och organisera data i våra program.

För att lära dig mer om Dictionary och hur det kan användas i C#-programmering, rekommenderas att utforska dokumentationen och exempelkod på Microsofts officiella webbplats eller andra resurser som erbjuder fördjupad information om ämnet.

## TL;DR

En Dictionary är en datastruktur som låter oss lagra och hämta värden baserat på nycklar. Det ger snabb åtkomst och effektiva sökningar, vilket gör det till ett kraftfullt verktyg inom programmering.

## Obligatorisk dad-joke

Varför älskar programmerare att använda dictionaries?

För att de alltid vill ha en "key" till framgång!

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
