# ⚡ Anakin Skywalker och Kristallgrottan på Ilum

## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Skapa variabler av olika datatyper (`string`, `int`, `double`)
- Förstå hur variabler lagrar och representerar data
- Använda aritmetiska operationer för att beräkna kristallstyrka
- Skriva ut resultat med string concatenation

---

## 🧩 Äventyret

Unge Anakin Skywalker har äntligen kommit till den heliga kristallgrottan på planeten Ilum för att välja sin första ljussabelkristall. Varje kristall har unika egenskaper - färg, styrka och Force-resonans. Som blivande Jedi måste han förstå kristallernas egenskaper för att välja den som passar honom bäst.

Mästare Yoda har lärt honom: **"Kristallen väljer Jedi, den gör. Men förstå dess kraft, du måste."**

## 🚀 Kom igång: Kristallens hemligheter (tänk själv!)

```csharp
using System;

namespace IlumCrystalCave
{
    class Program
    {
        static void Main(string[] args)
        {
            // Anakins namn som ung padawan
            // Vi lagrar namnet för att använda genom hela programmet
            // Detta följer principen att definiera viktiga data en gång
            // Skapa en string variabel för Anakins fullständiga namn
```

<details>
<summary>💡 Tips</summary>

```csharp
            string padawanName = "Anakin Skywalker";
```

</details>

```csharp
            Console.WriteLine("🌌 Välkommen till kristallgrottan på Ilum, " + padawanName + "!");
            Console.WriteLine("⚡ Tid att välja din första ljussabelkristall...");
            Console.WriteLine();

            // Den första kristallens färg
            // Varje kristall har sin unika färg som avspeglar användarens anslutning till Force
            // Lagra kristallens primära färg som text
            // Skapa en string variabel för kristallens färg
```

<details>
<summary>💡 Tips</summary>

```csharp
            string crystalColor = "Blå";
```

</details>

```csharp
            // Kristallens grundstyrka
            // Force-kristaller har olika naturlig styrka från 1-100
            // Denna kraft förstärks av Jedins förmåga att kanalisera Force
            // Skapa en int variabel för kristallens basstyrka
```

<details>
<summary>💡 Tips</summary>

```csharp
            int basePower = 75;
```

</details>

```csharp
            Console.WriteLine("💎 Du har funnit en " + crystalColor.ToLower() + " kristall!");
            Console.WriteLine("⚡ Kristallens grundstyrka: " + basePower + " Force-enheter");
            Console.WriteLine();

            // Anakins Force-potential som ung padawan
            // Hans midichlorian-antal ger honom exceptionell Force-känslighet
            // Detta värde multipliceras med kristallens grundstyrka
            // Skapa en double variabel för Anakins Force-multiplikator
```

<details>
<summary>💡 Tips</summary>

```csharp
            double anakinForcePotential = 2.3;
```

</details>

```csharp
            // Beräkna den totala ljussabelstyrkan
            // När kristall kombineras med Jedins Force-potential skapas verklig makt
            // Denna beräkning avgör ljussabelns slutliga kraft
            // Skapa en double variabel för total styrka genom att multiplicera
```

<details>
<summary>💡 Tips</summary>

```csharp
            double totalSaberPower = basePower * anakinForcePotential;
```

</details>

```csharp
            Console.WriteLine("🌟 " + padawanName + "s Force-potential: x" + anakinForcePotential);
            Console.WriteLine("⚔️  Total ljussabelstyrka: " + totalSaberPower + " enheter!");
            Console.WriteLine();

            // Kristallens speciella egenskaper
            // Olika kristaller ger olika förmågor och känslor
            // Denna information hjälper Jedi att förstå sin kristalls natur
            // Skapa en string variabel för kristallens specialegenskap
```

<details>
<summary>💡 Tips</summary>

```csharp
            string specialAbility = "Förhöjd koncentration";
```

</details>

```csharp
            // Kristallens sällsynthetsvärde (1-10)
            // Sällsynta kristaller är kraftfullare men svårare att behärska
            // Detta påverkar ljussabelns stabilitet och effektivitet
            // Skapa en int variabel för sällsynthetsnivån
```

<details>
<summary>💡 Tips</summary>

```csharp
            int rarity = 7;
```

</details>

```csharp
            Console.WriteLine("✨ Speciell förmåga: " + specialAbility);
            Console.WriteLine("💍 Sällsynthetsnivå: " + rarity + "/10");
            Console.WriteLine();

            // Beräkna kristallens totala värde
            // Kombinerar styrka och sällsynthet för att ge ett helhetsvärde
            // Detta hjälper Jedi att förstå kristallens fulla potential
            // Skapa en double variabel genom att multiplicera total styrka med sällsynthet
```

<details>
<summary>💡 Tips</summary>

```csharp
            double crystalValue = totalSaberPower * rarity;
```

</details>

```csharp
            Console.WriteLine("💰 Kristallens totala värde: " + crystalValue + " galaktiska krediter");
            Console.WriteLine();
            Console.WriteLine("🎯 " + padawanName + ", kristallen har valt dig!");
            Console.WriteLine("⚡ Din " + crystalColor.ToLower() + " ljussabel kommer att lysa starkt i galaxens mörka tider.");
            Console.WriteLine("🌟 Må Force vara med dig, unge Skywalker!");
        }
    }
}
```

## ✅ Förväntat resultat efter kristallvalet

```
🌌 Välkommen till kristallgrottan på Ilum, Anakin Skywalker!
⚡ Tid att välja din första ljussabelkristall...

💎 Du har funnit en blå kristall!
⚡ Kristallens grundstyrka: 75 Force-enheter

🌟 Anakin Skywalkers Force-potential: x2.3
⚔️  Total ljussabelstyrka: 172.5 enheter!

✨ Speciell förmåga: Förhöjd koncentration
💍 Sällsynthetsnivå: 7/10

💰 Kristallens totala värde: 1207.5 galaktiska krediter

🎯 Anakin Skywalker, kristallen har valt dig!
⚡ Din blå ljussabel kommer att lysa starkt i galaxens mörka tider.
🌟 Må Force vara med dig, unge Skywalker!
```

## 💡 Lärdomar

**Variabler lärde dig:**
- **String variabler**: Lagrar text som namn, färger och beskrivningar
- **Int variabler**: Lagrar heltal som styrka och sällsynthetsnivåer  
- **Double variabler**: Lagrar decimaltal för precisa beräkningar
- **Variabelnamn**: Välj beskrivande namn som `crystalColor` istället för `c`

**Beräkningar lärde dig:**
- **Multiplikation**: Kombinera kristallstyrka med Force-potential
- **Mixed operations**: Arbeta med både heltal och decimaltal
- **String methods**: Använd `.ToLower()` för konsekvent textformatering
- **String concatenation**: Kombinera text och variabler för meddelanden

**Programmerartänkande du lärde dig:**
- **Data representation**: Hur verkliga egenskaper (färg, styrka) blir variabler
- **Calculated values**: Skapa nya värden från befintliga data
- **User experience**: Använd beskrivande text för engagerande output
- **Business logic**: Kristallstyrka × Force-potential = Total kraft (som i verkliga applikationer)

**Verkliga tillämpningar:**
- **E-commerce**: Produktegenskaper, priser, kundvärdering
- **Gaming**: Karaktärsstyrka, inventory-system, experience points  
- **Finance**: Grundbelopp, räntor, totalsummor
- **Scientific**: Mätningar, beräkningar, resultatsrapporter

Anakin visar att variabler är som Force - osynliga kraftfält som lagrar information och kan kombineras för att skapa något större än summan av sina delar! ⚔️🌟