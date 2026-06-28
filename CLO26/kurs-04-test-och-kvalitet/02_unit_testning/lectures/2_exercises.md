---

title: 2. Mutation testing: Koncept och tillämpning
author: Marcus Ackre Medina
type: lecture
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/4_test/lectures/09_mutation_testing_and_property_based_testing/2_exercises.md"
description: "Mutation testing övningsuppgifter"
tags: ["csharp", "koncept", "mutation", "testing", "testing:", "tillämpning", "visual-studio"]
week_fit: []
---

# 2. Mutation testing: Koncept och tillämpning

🟢


Exercises:

Mutation testing övningsuppgifter

1. Grundläggande mutantidentifiering

Beskrivning: Identifiera potentiella mutanter i en given kodsnutt.

Instruktioner:
a) Granska följande C#-kod:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public bool IsEvenAndPositive(int number)
{
    return number > 0 && number % 2 == 0;
}
```

b) Lista minst tre möjliga mutationer som kunde skapas från denna kod.
c) För varje mutation, beskriv kort hur den skulle påverka funktionens beteende.

Förväntat resultat: En lista med minst tre mutationer och deras potentiella effekter på kodens funktion.

2. Konfigurering av Stryker.NET

Beskrivning: Skapa en grundläggande konfigurationsfil för Stryker.NET.

Instruktioner:
a) Öppna en texteditor.
b) Skapa en ny fil med namnet "stryker-config.json".
c) Skriv en JSON-konfiguration som:

- Använder HTML och progress som rapportörer.
- Anger "./tests/MyProject.Tests.csproj" som test-projekt.
d) Spara filen.

Förväntat resultat: En korrekt formaterad stryker-config.json fil som kan användas för att köra Stryker.NET.

3. Analys av mutationstestresultat

Beskrivning: Tolka resultaten från en Stryker.NET-körning och föreslå förbättringar.

Instruktioner:
a) Studera följande utdrag från en Stryker.NET-rapport:

```
Mutation testing report:
Total mutants: 100
Killed mutants: 85
Survived mutants: 12
Timeout mutants: 2
Ignored mutants: 1
Mutation score: 85.00%
```

b) Beräkna procentandelen överlevande mutanter.
c) Baserat på dessa resultat, föreslå minst två konkreta åtgärder för att förbättra testsviten.
d) Förklara varför timeout-mutanter kan uppstå och hur de bör hanteras.

Förväntat resultat: En kort analys av rapporten med beräkningar, förbättringsförslag och en förklaring av timeout-mutanter.

4. Hantering av ekvivalenta mutanter

Beskrivning: Identifiera och diskutera en ekvivalent mutant.

Instruktioner:
a) Granska följande original kod och dess mutant:

Original:

```csharp
public int Absolute(int x)
{
    return x < 0 ? -x : x;
}
```

Mutant:

```csharp
public int Absolute(int x)
{
    return x <= 0 ? -x : x;
}
```

b) Förklara varför denna mutant är ekvivalent med originalkoden.
c) Diskutera utmaningarna med att automatiskt upptäcka sådana ekvivalenta mutanter.
d) Föreslå en strategi för att hantera ekvivalenta mutanter i ett större projekt.

Förväntat resultat: En analys av den ekvivalenta mutanten, en diskussion om utmaningarna, och ett förslag på hanteringsstrategi.

5. Integrering av mutation testing i CI/CD

Beskrivning: Planera integrationen av mutation testing i en CI/CD-pipeline.

Instruktioner:
a) Skissa en enkel CI/CD-pipeline för ett .NET-projekt som inkluderar följande steg:

- Kodbygge
- Enhetstester
- Kodtäckningsanalys
b) Lägg till ett steg för mutation testing med Stryker.NET i denna pipeline.
c) Beskriv när mutation testing bör köras i förhållande till andra steg.
d) Diskutera potentiella utmaningar med att inkludera mutation testing i CI/CD och föreslå lösningar.

Förväntat resultat: En skiss över en CI/CD-pipeline med mutation testing integrerat, tillsammans med en kort beskrivning av ordning, utmaningar och lösningar.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
