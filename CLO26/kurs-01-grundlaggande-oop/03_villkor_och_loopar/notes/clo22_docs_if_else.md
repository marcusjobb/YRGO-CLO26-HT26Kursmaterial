---

title: Else
author: Marcus Ackre Medina
type: article
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/if/else.md"
description: "Else' är ett nyckelord som används i programmeringsspråket C# för att skapa en alternativ väg i en kodblockstruktur. Det ger möjlighet att utföra en annan sekvens av kod om en 'if'-sats utvärderas som"
tags: ["conditions", "csharp", "else", "git"]
week_fit: []
---

# Else

🟢


## Beskrivning

"Else" är ett nyckelord som används i programmeringsspråket C# för att skapa en alternativ väg i en kodblockstruktur. Det ger möjlighet att utföra en annan sekvens av kod om en "if"-sats utvärderas som falsk. Med andra ord kan "else" användas för att hantera fall där "if"-villkoret inte är uppfyllt, och programmet kan utföra en annan uppsättning instruktioner istället.

När en "if"-sats utvärderas som sann, utförs de kodinstruktioner som ligger inuti "if"-blocket. Om villkoret i "if"-satsen däremot utvärderas som falskt, kommer programmet att hoppa över kodblocket inom "if" och istället utföra kodblocket inom "else".

För att använda "else" måste det alltid följas av en "if". Det är också viktigt att notera att "else" alltid kommer i slutet av en "if"-sats och kan inte användas utanför den.

## Exempel

Här är ett exempel som visar hur "else" kan användas för att kontrollera om en person är myndig eller inte baserat på deras ålder:

```csharp
int age = 18;
if (age >= 18)
{
    Console.WriteLine("Du är myndig");
}
else
{
    Console.WriteLine("Du är inte myndig");
}
```

I detta exempel är "age" satt till 18. Eftersom 18 är större än eller lika med 18, utvärderas "if"-villkoret som sant, och programmet skriver ut "Du är myndig". Om vi ändrar värdet på "age" till 17 kommer "if"-villkoret att utvärderas som falskt, och programmet skriver ut "Du är inte myndig".

Med hjälp av "else" kan vi låta programmet välja olika vägar att följa beroende på villkoren.

## Sammanfattning

"Else" är ett viktigt nyckelord inom C# som ger möjlighet att skapa en alternativ väg i kodblocket baserat på ett "if"-villkor. Om "if"-satsen utvärderas som sann utförs de kodinstruktioner som ligger inuti "if"-blocket, och om villkoret utvärderas som falskt utförs istället kodinstruktionerna inom "else"-blocket. Det ger programmerare möjlighet att hantera olika fall och göra beslut i sina program.

## Termer och förklaringar

| Term     | Förklaring                                                                                                                                          |
| -------- | --------------------------------------------------------------------------------------------------------------------------------------------------- |
| Else     | Ett nyckelord i C# som används för att skapa en alternativ väg i kodblocket när "if"-villkoret är falskt.                                           |
| If       | Ett nyckelord i C# som används för att skapa en villkorsbaserad kodblockstruktur. Om villkoret är sant, utförs kodinstruktionerna inom "if"-blocket |
| Kodblock | En grupp av kodinstruktioner som är grupperade tillsammans.                                                                                         |
| Villkor  | En logisk fråga som kan utvärderas som antingen sant eller falskt.                                                                                  |

## Obligatorisk Dad-joke

Varför gillar programmerare att använda "else"?

För att de inte gillar att vara i "if-nite"! 😄

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
