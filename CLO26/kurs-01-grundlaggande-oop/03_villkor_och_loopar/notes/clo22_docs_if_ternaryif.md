---

title: Ternary if
author: Marcus Ackre Medina
type: article
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/if/ternaryif.md"
description: "<details open markdown='block'>"
tags: ["conditions", "csharp", "git", "ternary", "ternaryif"]
week_fit: []
---

# Ternary if

🟢


<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. [Beskrivning](#beskrivning)
2. [Exempel](#exempel)
3. [Slutsats](#slutsats)
4. [Mer läsning](#mer-läsning)
5. [Termer](#termer)
6. [Obligatorisk dad joke](#obligatorisk-dad-joke)

</details>

## Beskrivning

Ternary if, även känd som conditional operator, är en kompakt syntax i programmeringsspråket C# som tillåter oss att uttrycka en enkel if-sats på en rad. Det ger oss möjlighet att utvärdera ett villkor och välja en av två uttryck beroende på om villkoret är sant eller falskt. Syntaxen för ternary if består av tre delar: villkor, frågetecken och uttryck för sant och falskt. Det är ett kraftfullt verktyg som kan göra koden mer koncis och läsbar.

## Exempel

Här är ett exempel som visar hur man använder ternary if i C#:

```csharp
int age = 18;
string result = age >= 18 ? "Du är myndig" : "Du är inte myndig";
Console.WriteLine(result);
```

I detta exempel tilldelas strängen "Du är myndig" till variabeln "result" om värdet av variabeln "age" är större eller lika med 18. Annars tilldelas strängen "Du är inte myndig". Sedan skrivs värdet av "result" ut till konsolen.

Det är viktigt att notera att ternary if är ett uttryck och kan användas inuti andra uttryck eller tilldelningar. Det ger oss möjlighet att göra kompakt och läsbar kod för att hantera enkla villkor.

En annan användning av ternary if är när vi redan har en boolean-variabel och vi behöver välja mellan två värden baserat på dess värde. Här är ett exempel:

```csharp
bool catIsCute = true;
string result = catIsCute ? "Katten är söt <3" : "Katten är inte söt :(";
Console.WriteLine(result);
```

I detta exempel tilldelas strängen "Katten är söt <3" till variabeln "result" om värdet av "catIsCute" är sant (true). Annars tilldelas strängen "Katten är inte söt :(".

Ternary if är ett verktyg som kan göra koden mer läsbar och koncis i situationer där vi behöver göra en enkel villkorskontroll och tilldela olika värden beroende på resultatet.

## Slutsats

Ternary if, eller conditional operator, är ett användbart verktyg inom programmering som gör det möjligt för oss att uttrycka en enkel if-sats på en rad. Det hjälper oss att göra koden mer koncis och läsbar genom att välja mellan två uttryck beroende på om ett vill

tillstånd är sant eller falskt. Genom att använda ternary if kan vi undvika att skriva en längre if-sats när vi bara behöver hantera enkla villkor.

Ternary if använder följande syntax:

```
villkor ? uttryck om sant : uttryck om falskt
```

Där "villkor" är det uttryck som utvärderas, "?" är frågetecknet som markerar början på ternary if, "uttryck om sant" är det värde eller uttryck som tilldelas om villkoret är sant, och "uttryck om falskt" är det värde eller uttryck som tilldelas om villkoret är falskt.

Det är viktigt att notera att ternary if bara är lämplig för enkla villkor och enkla uttryck. Om du behöver hantera mer komplexa villkor eller flera uttryck kan det vara bättre att använda en vanlig if-sats.

## Mer läsning

- [C# Conditional Operator (?:)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator)
- [C# If-else statement](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else)

## Termer

- Ternary if: En kompakt syntax i C# som tillåter oss att uttrycka en enkel if-sats på en rad.
- Conditional operator: En annan term för ternary if, som beskriver dess användning för att utvärdera och välja mellan två uttryck baserat på ett villkor.

## Obligatorisk dad joke

tillstånd är sant eller falskt. Genom att använda ternary if kan vi undvika att skriva en längre if-sats när vi bara behöver hantera enkla villkor.

Ternary if använder följande syntax:

```
villkor ? uttryck om sant : uttryck om falskt
```

Där "villkor" är det uttryck som utvärderas, "?" är frågetecknet som markerar början på ternary if, "uttryck om sant" är det värde eller uttryck som tilldelas om villkoret är sant, och "uttryck om falskt" är det värde eller uttryck som tilldelas om villkoret är falskt.

Det är viktigt att notera att ternary if bara är lämplig för enkla villkor och enkla uttryck. Om du behöver hantera mer komplexa villkor eller flera uttryck kan det vara bättre att använda en vanlig if-sats.

## Mer läsning

- [C# Conditional Operator (?:)](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/conditional-operator)
- [C# If-else statement](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/if-else)

## Termer

- Ternary if: En kompakt syntax i C# som tillåter oss att uttrycka en enkel if-sats på en rad.
- Conditional operator: En annan term för ternary if, som beskriver dess användning för att utvärdera och välja mellan två uttryck baserat på ett villkor.

## Obligatorisk dad joke

Varför älskar programmerare att använda ternary if?

För att det gör koden kortare och "ter"-rific! 😄

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
