# 🧙‍♂️ Trolldomsmission: Lär dig variabler med Harry Potter!

## 🎯 Mål med övningen
Lär dig om grundläggande variabler i C# och hur man lagrar information för att använda den i programmet.

## 🧩 Äventyrsbeskrivning
I ett av de mer mystiska rummen på Hogwarts, ringer ett magiskt meddelande. Professor McGonagall har upptäckt att någon stjäl böcker från biblioteket! Du, Harry, och dina vänner Hermione och Ron behöver snabbt skapa ett program för att hålla koll på de stulna böckerna. Problemet? Ni behöver lagra boktitlar för att kunna hjälpa bibliotekarien Madam Pince!

## 🚀 Kom igång: Dags att kalla på magin!
```csharp
using System;

namespace HogwartsLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            // Vi behöver en variabel för att lagra titeln på den stulna boken
            // Här kan vi använda en string för text
            // Skapa en string variabel som heter "stolenBookTitle" och sätt värdet till "Harry Potter och De vises sten"
<details>
<summary>💡 Tips</summary>

```csharp
            string stolenBookTitle = "Harry Potter och De vises sten";
```

</details>

            // Nu behöver vi en variabel för att lagra antalet böcker som har stulits
            // Vi använder en int för heltal
            // Skapa en int variabel som heter "stolenBooksCount" och sätt värdet till 1
<details>
<summary>💡 Tips</summary>

```csharp
            int stolenBooksCount = 1;
```

</details>

            // Nu ska vi spara den magiska informationen som vi har
            // Använd Console.WriteLine för att skriva ut titeln på den stulna boken
            // Vi behöver visa titeln och antalet böcker
            // Använd interpolerad sträng för att sammanfoga text och variabler
<details>
<summary>💡 Tips</summary>

```csharp
            Console.WriteLine($"Titt på den stulna boken: {stolenBookTitle}, Antal stulna böcker: {stolenBooksCount}");
```

</details>
        }
    }
}
```

## ✅ Förväntat resultat
När programmet körs ska det visa: 
```
Titt på den stulna boken: Harry Potter och De vises sten, Antal stulna böcker: 1
```

## 💡 Lärdomar
Idag har du lärt dig hur man skapar och använder variabler i C#! Genom att lagra boktitlar och räkna stulna böcker kan du lättare organisera information för magiska uppdrag. Variabler är grunden i programmering och hjälper oss att göra koden mer flexibel och lättläst. Tänk på att alltid namnge variabler som beskriver deras syfte, så som "stolenBookTitle", för att följa god programmeringspraxis.