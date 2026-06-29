# Arsène Lupin och Bankvalvets Gåta

## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Skapa variabler av olika datatyper (`string`, `int`, `double`)
- Använda operatorer (`*`, `+`, `-`, `+=`, `-=`, `%`, `/`)
- Lösa steg-för-steg matematiska beräkningar med variabler
- Förstå hur komplexa beräkningar byggs upp från enkla delar

---

## 🧩 Mysteriet

Den berömde gentleman-tjuven Arsène Lupin står inför sin största utmaning hittills: Banque de Paris's legendariska valv. Valvet skyddas inte bara av tjocka stålväggar, utan av en genialisk matematisk gåta som måste lösas för att låsa upp dörren.

Gåtan kräver att Lupin beräknar den **exakta kombinationen** genom att lösa en serie matematiska problem. Hans fickur tickar - han har endast **15 minuter** innan vakterna kommer tillbaka.

## 🚀 Kom igång: Gåtans första del (tänk själv!)

```csharp
using System;

namespace LupinHeist
{
    class Program
    {
        static void Main(string[] args)
        {
            // Gentleman-tjuvens namn (text)
            // Vi lagrar namn för att skapa personlig upplevelse genom koden
            // Detta gör programmet mer läsbart och maintainerbart
            // Skapa en string variabel som heter "thiefName" och sätt värdet till "Arsène Lupin"
```

<details>
<summary>💡 Tips</summary>

```csharp
            string thiefName = "Arsène Lupin";
```

</details>

```csharp
            // Tiden kvar (minuter)
            // Resource management: vi spårar tid som en begränsad resurs
            // I verkliga system: session timeout, cache expiry, deadline tracking
            // Skapa en int variabel som heter "timeRemaining" och sätt värdet till 15
```

<details>
<summary>💡 Tips</summary>

```csharp
            int timeRemaining = 15;
```

</details>

```csharp
            Console.WriteLine("🎩 " + thiefName + " står inför bankvalvets gåta...");
            Console.WriteLine("⏰ Tid kvar: " + timeRemaining + " minuter");
            Console.WriteLine();

            // FÖRSTA GÅTAN: Grundtalet
            // Bankvalvets första siffra baseras på årtalet banken grundades
            // Fundamentala värden: ofta kommer business logic från historiska data
            // Skapa en int variabel som heter "bankFoundedYear" och sätt värdet till 1887
```

<details>
<summary>💡 Tips</summary>

```csharp
            int bankFoundedYear = 1887;
```

</details>

```csharp
            Console.WriteLine("🏛️  Första ledtråden: Banken grundades år " + bankFoundedYear);

            // Beräkna summan av siffrorna i årtalet (1+8+8+7)
            // Digital root calculation: ett vanligt mönster för att "komprimera" stora tal
            // Vi delar upp problemet i mindre delar för att undvika mänskliga fel
            // Skapa en int variabel som heter "digitSum" och beräkna: 1+8+8+7
```

<details>
<summary>💡 Tips</summary>

```csharp
            int digitSum = 1 + 8 + 8 + 7;
```

</details>

```csharp
            Console.WriteLine("🔢 Summa av siffror: " + digitSum);

            // ANDRA GÅTAN: Mystiska faktorn
            // Lupins lyckonum multiplicerat med första resultatet
            // Magic numbers undviks genom att ge dem meningsfulla namn
            // Skapa en int variabel som heter "lupinLuckyNumber" och sätt värdet till 13
```

<details>
<summary>💡 Tips</summary>

```csharp
            int lupinLuckyNumber = 13;
```

</details>

```csharp
            // Första kombinationssiffran
            // Composed calculations: bygger komplexa beräkningar från enkla delar
            // Detta gör koden testbar och felsökningsvänlig
            // Skapa en int variabel som heter "firstDigit" och beräkna: digitSum * lupinLuckyNumber
```

<details>
<summary>💡 Tips</summary>

```csharp
            int firstDigit = digitSum * lupinLuckyNumber;
```

</details>

```csharp
            Console.WriteLine("🎯 Lupins lyckotal: " + lupinLuckyNumber);
            Console.WriteLine("🔐 Första kombinationssiffran: " + firstDigit);
            Console.WriteLine();

            // TREDJE GÅTAN: Valvets vikt
            // Valvdörren väger exakt detta antal ton
            // Domain-specific constants: vissa värden kommer från fysiska begränsningar
            // Skapa en double variabel som heter "vaultWeightTons" och sätt värdet till 12.5
```

<details>
<summary>💡 Tips</summary>

```csharp
            double vaultWeightTons = 12.5;
```

</details>

```csharp
            // Konvertera till kilogram för beräkningar
            // Unit conversion: ofta behöver vi standardisera enheter för beräkningar
            // 1 ton = 1000 kg - vi hårdkodar denna konstant eftersom den aldrig ändras
            // Skapa en double variabel som heter "vaultWeightKg" och beräkna: vaultWeightTons * 1000
```

<details>
<summary>💡 Tips</summary>

```csharp
            double vaultWeightKg = vaultWeightTons * 1000;
```

</details>

```csharp
            Console.WriteLine("⚖️  Valvets vikt: " + vaultWeightTons + " ton (" + vaultWeightKg + " kg)");

            // FJÄRDE GÅTAN: Säkerhetsfaktorn
            // Ta resten när första siffran delas med valvets vikt i kg
            // Modulo operations: används för cykliska beräkningar, hash functions, checksums
            // Skapa en double variabel som heter "securityFactor" och beräkna: firstDigit % vaultWeightKg
```

<details>
<summary>💡 Tips</summary>

```csharp
            double securityFactor = firstDigit % vaultWeightKg;
```

</details>

```csharp
            Console.WriteLine("🔒 Säkerhetsfaktor (modulo): " + securityFactor);

            // FEMTE GÅTAN: Slutlig kombination
            // Dela säkerhetsfaktorn med Lupins ålder och avrunda nedåt
            // Age-based calculations: vanligt i försäkring, pension, access control
            // Skapa en int variabel som heter "lupinAge" och sätt värdet till 35
```

<details>
<summary>💡 Tips</summary>

```csharp
            int lupinAge = 35;
```

</details>

```csharp
            // Beräkna den slutgiltiga koden
            // Floor division: vi vill ha heltalsdelen för kombinationslås
            // Cast operations: konverterar mellan datatyper när nödvändigt
            // Skapa en int variabel som heter "finalCode" och beräkna: (int)(securityFactor / lupinAge)
```

<details>
<summary>💡 Tips</summary>

```csharp
            int finalCode = (int)(securityFactor / lupinAge);
```

</details>

```csharp
            Console.WriteLine("👤 Lupins ålder: " + lupinAge + " år");
            Console.WriteLine("🎯 SLUTLIG KOD: " + finalCode);
            Console.WriteLine();

            // Spåra tidsförbrukning
            // Performance monitoring: håll koll på resursanvändning
            // Skapa en int variabel som heter "timeUsed" och sätt värdet till 8
```

<details>
<summary>💡 Tips</summary>

```csharp
            int timeUsed = 8;
```

</details>

```csharp
            // Beräkna återstående tid
            // Resource depletion tracking: kritiskt i resurshanteringssystem
            // Skapa en int variabel som heter "timeLeft" och beräkna: timeRemaining - timeUsed
```

<details>
<summary>💡 Tips</summary>

```csharp
            int timeLeft = timeRemaining - timeUsed;
```

</details>

```csharp
            Console.WriteLine("⏱️  Tid använd för gåtan: " + timeUsed + " minuter");
            Console.WriteLine("⏰ Tid kvar: " + timeLeft + " minuter");
            Console.WriteLine();
            Console.WriteLine("🎩✨ " + thiefName + " har knäckt koden!");
        }
    }
}
```

## ✅ Förväntat resultat efter första gåtan

```
🎩 Arsène Lupin står inför bankvalvets gåta...
⏰ Tid kvar: 15 minuter

🏛️  Första ledtråden: Banken grundades år 1887
🔢 Summa av siffror: 24
🎯 Lupins lyckotal: 13
🔐 Första kombinationssiffran: 312

⚖️  Valvets vikt: 12.5 ton (12500 kg)
🔒 Säkerhetsfaktor (modulo): 312
👤 Lupins ålder: 35 år
🎯 SLUTLIG KOD: 8

⏱️  Tid använd för gåtan: 8 minuter
⏰ Tid kvar: 7 minuter

🎩✨ Arsène Lupin har knäckt koden!
```

---

## ⚔️ Valvinbrottet och flykt (nu vet du hur det funkar!)

Nu när Lupin har knäckt den första koden fortsätter äventyret. Valvet har **flera lås** som kräver ytterligare beräkningar:
- **Sekundärt lås**: Baserat på antalet diamanter i valvet
- **Tertärt lås**: Guldsaldon från olika konton
- **Nödlås**: Tidbaserad algoritm för säker flykt

Fortsätt med resten av koden:

```csharp
            // VALVINBROTTET PÅBÖRJAS
            Console.WriteLine("🚪 Första låset öppnat! Lupin går in i valvet...");
            
            // Diamanter i valvet
            int diamondCount = 247;
            double diamondValue = 50000.0; // Per diamant i kronor
            double totalDiamondValue = diamondCount * diamondValue;
            
            Console.WriteLine("💎 Diamanter funna: " + diamondCount + " stycken");
            Console.WriteLine("💰 Total värde diamanter: " + totalDiamondValue + " kr");

            // Sekundärt lås - baserat på diamantantal
            int secondaryCode = diamondCount % 100; // Tar de två sista siffrorna
            timeLeft -= 3; // Ytterligare 3 minuter förbrukas
            
            Console.WriteLine("🔐 Sekundärt lås kod: " + secondaryCode);
            Console.WriteLine("⏰ Tid kvar: " + timeLeft + " minuter");

            // Guldsaldon från tre stora konton
            double account1 = 1500000.75;
            double account2 = 2300000.25; 
            double account3 = 980000.50;
            double totalGold = account1 + account2 + account3;
            
            // Tertärt lås - baserat på genomsnittligt saldo
            double averageAccount = totalGold / 3;
            int tertiaryCode = (int)(averageAccount / 100000); // Dividerat med 100k, avrundat nedåt
            
            Console.WriteLine("🏅 Totalt guld i valv: " + totalGold + " kr");
            Console.WriteLine("📊 Genomsnittligt konto: " + averageAccount + " kr");
            Console.WriteLine("🔐 Tertärt lås kod: " + tertiaryCode);

            // FLYKTEN
            timeLeft -= 2; // Sista låset tar 2 minuter
            int escapeTime = 2; // Tid för flykt
            int finalTimeLeft = timeLeft - escapeTime;
            
            Console.WriteLine();
            Console.WriteLine("🏃‍♂️ " + thiefName + " flyr från valvet!");
            Console.WriteLine("⏱️  Total tid använd: " + (timeRemaining - finalTimeLeft) + " minuter");
            Console.WriteLine("⏰ Tid kvar vid flykt: " + finalTimeLeft + " minuter");
            
            // Slutlig sammanfattning
            double totalHeistValue = totalDiamondValue + totalGold;
            Console.WriteLine();
            Console.WriteLine("🎩✨ KUPP GENOMFÖRT!");
            Console.WriteLine("💎 Diamanter: " + totalDiamondValue + " kr");
            Console.WriteLine("🏅 Guld: " + totalGold + " kr"); 
            Console.WriteLine("💰 Total byte: " + totalHeistValue + " kr");
            Console.WriteLine("👑 " + thiefName + " försvinner in i natten...");
        }
    }
}
```

## ✅ Fullständigt förväntat resultat

```
🎩 Arsène Lupin står inför bankvalvets gåta...
⏰ Tid kvar: 15 minuter

🏛️  Första ledtråden: Banken grundades år 1887
🔢 Summa av siffror: 24
🎯 Lupins lyckotal: 13
🔐 Första kombinationssiffran: 312

⚖️  Valvets vikt: 12.5 ton (12500 kg)
🔒 Säkerhetsfaktor (modulo): 312
👤 Lupins ålder: 35 år
🎯 SLUTLIG KOD: 8

⏱️  Tid använd för gåtan: 8 minuter
⏰ Tid kvar: 7 minuter

🎩✨ Arsène Lupin har knäckt koden!
🚪 Första låset öppnat! Lupin går in i valvet...
💎 Diamanter funna: 247 stycken
💰 Total värde diamanter: 12350000 kr
🔐 Sekundärt lås kod: 47
⏰ Tid kvar: 4 minuter
🏅 Totalt guld i valv: 4780001.5 kr
📊 Genomsnittligt konto: 1593333.8333333333 kr
🔐 Tertärt lås kod: 15

🏃‍♂️ Arsène Lupin flyr från valvet!
⏱️  Total tid använd: 13 minuter
⏰ Tid kvar vid flykt: 2 minuter

🎩✨ KUPP GENOMFÖRT!
💎 Diamanter: 12350000 kr
🏅 Guld: 4780001.5 kr
💰 Total byte: 17130001.5 kr
👑 Arsène Lupin försvinner in i natten...
```

## 💡 Lärdomar

**Första gåtan lärde dig:**
- **Steg-för-steg problemlösning**: Bryta ner komplexa beräkningar i hanterbara delar
- **Modulo operation (`%`)**: För cykliska beräkningar och checksums
- **Type casting**: Konvertera mellan `double` och `int` för olika beräkningar
- **Digital root calculation**: Komprimera stora tal till enkla värden

**Valvinbrottet och flykten lärde dig:**
- **Resource tracking**: Hålla koll på tid som begränsad resurs
- **Financial calculations**: Hantera stora belopp och genomsnitt
- **Multi-step algorithms**: Flera låskoder baserade på olika beräkningar
- **Real-world applications**: Hur matematik används i säkerhetssystem

Arsène Lupin visar att programmering är som att lösa en serie sammankopplade gåtor - varje del byggs på den föregående, och precision är avgörande för framgång! 🎩✨