# Sherlock Holmes och Mysteriet på Baker Street

## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Skapa och använda `for`-loopar för repetitiva uppgifter
- Förstå loop-komponenter: initiering, villkor, iteration
- Använda loop-variabler för räkning och ackumulering
- Kombinera loops med variabler och beräkningar

---

## 🧩 Mysteriet

Det är en dimmig novemberkväll på Baker Street 221B. Sherlock Holmes sitter vid sin skrivbordslåda när Mrs. Hudson kommer instörtande - hennes dyrbara smyckesskrin har blivit stulet! Tjuven har lämnat efter sig en serie ledtrådar som måste undersökas systematiskt.

Holmes vet att **metodisk undersökning** är nyckeln. Han måste granska **varje ledtråd** i ordning, **räkna bevisen**, och **ackumulera information** för att lösa fallet. Som den briljante detektiven han är, använder han sitt **systematiska tillvägagångssätt** - precis som en for-loop!

## 🚀 Kom igång: Den systematiska undersökningen (tänk själv!)

```csharp
using System;

namespace HolmesMystery
{
    class Program
    {
        static void Main(string[] args)
        {
            // Mästardetektivens namn (text)
            // Vi lagrar namn för konsekvent rapportering genom hela undersökningen
            // Detta följer DRY-principen och gör koden maintainerbar
            // Skapa en string variabel som heter "detectiveName" och sätt värdet till "Sherlock Holmes"
```

<details>
<summary>💡 Tips</summary>

```csharp
            string detectiveName = "Sherlock Holmes";
```

</details>

```csharp
            // Totalt antal ledtrådar att undersöka
            // Loop boundaries: vi definierar hur många iterationer vi behöver
            // I verkliga system: antal poster i databas, filer att processa, API calls att göra
            // Skapa en int variabel som heter "totalClues" och sätt värdet till 7
```

<details>
<summary>💡 Tips</summary>

```csharp
            int totalClues = 7;
```

</details>

```csharp
            Console.WriteLine("🔍 " + detectiveName + " påbörjar sin undersökning...");
            Console.WriteLine("📋 Antal ledtrådar att granska: " + totalClues);
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine();

            // Räknare för viktiga bevis
            // Accumulator pattern: vi samlar data genom iterationerna
            // Detta är fundamental för statistik, summering, filtrering
            // Skapa en int variabel som heter "importantEvidence" och sätt värdet till 0
```

<details>
<summary>💡 Tips</summary>

```csharp
            int importantEvidence = 0;
```

</details>

```csharp
            // Misstänksamhetspoäng (ackumuleras för varje ledtråd)
            // Running totals: vanligt mönster för scorekeeping, budget tracking, metrics
            // Skapa en int variabel som heter "suspicionScore" och sätt värdet till 0
```

<details>
<summary>💡 Tips</summary>

```csharp
            int suspicionScore = 0;
```

</details>

```csharp
            // HOLMES' SYSTEMATISKA GRANSKNING
            // For-loop: repetitiv process för att undersöka varje ledtråd
            // Loop structure: initialization; condition; increment
            // Skapa en for-loop som startar från 1 och går till och med totalClues
            // for (int clueNumber = 1; clueNumber <= totalClues; clueNumber++)
```

<details>
<summary>💡 Tips</summary>

```csharp
            for (int clueNumber = 1; clueNumber <= totalClues; clueNumber++)
            {
```

</details>

```csharp
                Console.WriteLine("🔎 Undersöker ledtråd #" + clueNumber + ":");
                
                // Varje ledtråd har olika värden beroende på dess nummer
                // Conditional logic within loops: olika behandling baserat på iteration
                // Business rules: ofta varierar processing baserat på input data
                
                if (clueNumber == 1)
                {
                    Console.WriteLine("   📜 Hotelsebrev med blodfläck - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 15;
                }
                else if (clueNumber == 2)
                {
                    Console.WriteLine("   🚬 Cigarettfimpar - 3 olika märken");
                    suspicionScore += 8;
                }
                else if (clueNumber == 3)
                {
                    Console.WriteLine("   👞 Muddy fotspår storlek 42 - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 12;
                }
                else if (clueNumber == 4)
                {
                    Console.WriteLine("   🕰️  Trasig fickur stannad på 23:47");
                    suspicionScore += 10;
                }
                else if (clueNumber == 5)
                {
                    Console.WriteLine("   💍 Glitterspår från smyckesskrin - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 20;
                }
                else if (clueNumber == 6)
                {
                    Console.WriteLine("   🗝️  Främmande nyckel som inte passar något lås");
                    suspicionScore += 5;
                }
                else if (clueNumber == 7)
                {
                    Console.WriteLine("   📰 Tidningsurklipp om liknande brott - Viktigt!");
                    importantEvidence++;
                    suspicionScore += 18;
                }
                
                // Visa löpande status för varje ledtråd
                // Progress reporting: håll användaren informerad om framsteg
                Console.WriteLine("   📊 Misstanksamhet så här långt: " + suspicionScore + " poäng");
                Console.WriteLine("   🎯 Viktiga bevis hittade: " + importantEvidence + " st");
                Console.WriteLine();
```

<details>
<summary>💡 Tips för att avsluta for-loopen</summary>

```csharp
            }
```

</details>

```csharp
            // HOLMES' ANALYS OCH SLUTSATS
            Console.WriteLine("════════════════════════════════════════");
            Console.WriteLine("🧠 " + detectiveName + "s analys:");
            Console.WriteLine("📋 Totalt antal ledtrådar granskade: " + totalClues);
            Console.WriteLine("🎯 Viktiga bevis funna: " + importantEvidence + " av " + totalClues);
            Console.WriteLine("📊 Total misstanksamhetspoäng: " + suspicionScore);
            
            // Beräkna procentandel viktiga bevis
            // Percentage calculations: vanligt för rapporter och analytics
            // Type casting för precision i divisionsberäkningar
            double evidencePercentage = ((double)importantEvidence / totalClues) * 100;
            Console.WriteLine("📈 Andel viktiga bevis: " + evidencePercentage + "%");
            
            // Holmes' slutsats baserat på ackumulerad data
            // Decision logic: använd samlad data för slutsatser
            if (suspicionScore >= 70 && importantEvidence >= 3)
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("✅ Fallet LÖST! Bevisen pekar tydligt på en inombrottstjuv");
                Console.WriteLine("🔒 Rekommendation: Kontakta Scotland Yard omedelbart");
            }
            else if (suspicionScore >= 40)
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("🤔 Mer undersökning krävs - ledtrådarna är tvetydiga");
                Console.WriteLine("📝 Rekommendation: Samla ytterligare bevis");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("🎩 " + detectiveName + "s slutsats:");
                Console.WriteLine("❌ Otillräckliga bevis för att lösa fallet");
                Console.WriteLine("🔍 Rekommendation: Ny brottsplatsundersökning behövs");
            }
            
            Console.WriteLine();
            Console.WriteLine("🎭 \"Elementary, my dear Watson!\" - " + detectiveName);
        }
    }
}
```

## ✅ Förväntat resultat efter undersökningen

```
🔍 Sherlock Holmes påbörjar sin undersökning...
📋 Antal ledtrådar att granska: 7
════════════════════════════════════════

🔎 Undersöker ledtråd #1:
   📜 Hotelsebrev med blodfläck - Viktigt!
   📊 Misstanksamhet så här långt: 15 poäng
   🎯 Viktiga bevis hittade: 1 st

🔎 Undersöker ledtråd #2:
   🚬 Cigarettfimpar - 3 olika märken
   📊 Misstanksamhet så här långt: 23 poäng
   🎯 Viktiga bevis hittade: 1 st

🔎 Undersöker ledtråd #3:
   👞 Muddy fotspår storlek 42 - Viktigt!
   📊 Misstanksamhet så här långt: 35 poäng
   🎯 Viktiga bevis hittade: 2 st

🔎 Undersöker ledtråd #4:
   🕰️  Trasig fickur stannad på 23:47
   📊 Misstanksamhet så här långt: 45 poäng
   🎯 Viktiga bevis hittade: 2 st

🔎 Undersöker ledtråd #5:
   💍 Glitterspår från smyckesskrin - Viktigt!
   📊 Misstanksamhet så här långt: 65 poäng
   🎯 Viktiga bevis hittade: 3 st

🔎 Undersöker ledtråd #6:
   🗝️  Främmande nyckel som inte passar något lås
   📊 Misstanksamhet så här långt: 70 poäng
   🎯 Viktiga bevis hittade: 3 st

🔎 Undersöker ledtråd #7:
   📰 Tidningsurklipp om liknande brott - Viktigt!
   📊 Misstanksamhet så här långt: 88 poäng
   🎯 Viktiga bevis hittade: 4 st

════════════════════════════════════════
🧠 Sherlock Holmes analys:
📋 Totalt antal ledtrådar granskade: 7
🎯 Viktiga bevis funna: 4 av 7
📊 Total misstanksamhetspoäng: 88
📈 Andel viktiga bevis: 57.142857142857146%

🎩 Sherlock Holmes slutsats:
✅ Fallet LÖST! Bevisen pekar tydligt på en inombrottstjuv
🔒 Rekommendation: Kontakta Scotland Yard omedelbart

🎭 "Elementary, my dear Watson!" - Sherlock Holmes
```

## 💡 Lärdomar

**For-loop strukturen lärde dig:**
- **Loop initialization**: `int clueNumber = 1` - variabeln startvärde
- **Loop condition**: `clueNumber <= totalClues` - när loopen ska fortsätta
- **Loop increment**: `clueNumber++` - hur variabeln ändras efter varje iteration
- **Loop body**: Koden som körs för varje iteration

**Programmeringsmönster du lärde dig:**
- **Accumulator Pattern**: `importantEvidence++` och `suspicionScore += värde`
- **Progress Tracking**: Visa status efter varje iteration
- **Conditional Processing**: Olika logik baserat på loop-variabeln
- **Data Analysis**: Använd ackumulerad data för slutsatser
- **Percentage Calculations**: `((double)importantEvidence / totalClues) * 100`

**Verkliga tillämpningar:**
- **Database Processing**: Gå igenom alla poster systematiskt
- **File Processing**: Behandla filer en i taget
- **Statistical Analysis**: Samla data och beräkna summor/genomsnitt
- **Quality Control**: Granska varje item och räkna fel/framgångar
- **Report Generation**: Producera rapporter baserat på samlad data

Sherlock Holmes visar att for-loops är som systematisk detektivarbete - varje iteration bygger på den föregående tills du har hela bilden! 🕵️‍♂️🔍