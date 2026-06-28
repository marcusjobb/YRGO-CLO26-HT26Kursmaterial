---

title: Beslutsfattande i kod – Bussbiljetter
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Beslutsfattande i kod - Bussbiljetter.md"
description: "I den här övningen ska vi skapa en klass som hjälper oss att räkna ut hur många bussbiljetter och av vilken typ."
tags: ["arrays", "beskrivning:", "conditions", "csharp", "exercise", "facit", "if", "switch", "vasttrafik"]
week_fit: []
---

| Bussbiljetter |
| --- |


# Beskrivning:

🟡


I den här övningen ska vi skapa en klass som hjälper oss att räkna ut hur många bussbiljetter och av vilken typ.

# Kursplanstermer som berörs av uppgiften:

| Mål | Vad du ska lära dig |
| --- | --- |
| Kunskap om innehållet i .NET-biblioteket | Innehållet i .NET-biblioteket. |
| Kunskaper kring typer, variabler, operationer, uttryck, villkorssatser och loopar inom programmering. | Typer, variabler, uttryck, villkorssatser och loopar används inom programmering. |
| Kunskap kring namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program. | Namngivning och kodstruktur av klasser, metoder och variabler i objektorienterade program används. |
| Lösa problem i ett datalogiskt sammanhang. | Lösa problem i datalogiska sammanhang genom att exempelvis bryta ner problemen. |
| Utveckla program med en tydligt objektorienterad struktur. | Utveckla program med en tydligt objektorienterad struktur. |
| Förstå och använda sig av datastrukturer inom programmering. | Att använda datastrukturer i sin mjukvaruutveckling. |
| Planera, designa och implementera gränssnitt utifrån användaren. | Att planera, designa och implementera gränssnitt utifrån användaren. |
| Utveckla felfria fristående program. | Att skapa felfria fristående program. |


# Pseudokod:

- Ange antal resor och zon

- Loop tills antal resor <=0

- Om Zonpris * antal resor är större än

- Årsavgift -> räkna på årsavgift och dra av 365 resor från listan

- 90 dagarsavgift -> räkna på 90 dagars avgift och dra av 90 dagar på antal resor

- 30 dagarsavgift -> räkna på 30 dagars avgift och dra av 30 dagar på resan

- Annars kör på dagsresor


Tänk på att antal resor ska multipliceras med 2 (tur och retur) och när man lägger till biljetter i köplistan och drar av antal dagar från antal resor som ska göras, ska även detta räknas två gånger.


# Projektinstruktioner:

Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att samarbeta.

# Kodning:

Vi börjar med att skapa ett c#, .net konsolprojekt.

Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

## Bussbiljettväljare

Ibland behöver vi hjälpa våra användare att fatta beslut, det kan vara enkla saker som hur mycket man kan göra av vad, och vad som är det bästa alternativet med tanke på mängden. Vi ska titta på två exempel, den ena handlar om priset för en tågresa och den andra handlar om vad man ska betala på gymmet.

Dessa uppgifter löser man enklast med lite multiplikationer och en del if-satser. Switch-satser fungerar också bra om man kör version 8 av C# som stödjer operatorer för jämförelser. Västtrafiks biljetter kostar enligt (april 2020).

|  | 1 zon | 2 zoner | 3 zoner |
| --- | --- | --- | --- |
| Enkelbiljett | 34 kr | 68 kr | 102 kr |
| 30 dagar | 795 kr | 1 195 kr | 1 825 kr |
| 90 dagar | 2 145 kr | 3 225 kr | 4 930 kr |
| 365 dagar | 7 950 kr | 11 950 kr | 18 250 kr |

Reser man över 3 zoner blir priser samma som 3 zoner.

När vi räknar utgår vi från att resorna är tur och retur på en dag, alltså gånger 2. 
Alltså en resa = en dag = en resa tur och retur.

| Exempel |
| --- |
| // För att hitta billigaste priset kan vi // lägga det i en array och sortera arrayen double[] sorted = new[] { 10, 20, 5, 7}; Array.Sort(sorted); // arrayen blir [0]=5,[1]=7,[2]=10,[3]=20 |


### Västtrafik

| Tips |
| --- |
| public class TicketChooser {         // Den här klassen kommer att behöva ett enum för att      // skilja biljetterna åt.     public enum Biljetter     {         Enkelbiljett,         TrettioDagar,         NittioDagar,         TrehundrasextiofemDagar,         Error     }        // Ett sätt att hålla koll på priser per zon   // kan vi lägga dem i en array som är publik   // för klassen och dess användare   public double[] DayPrice {get;set;} = new double[] { 34, 68, 102 };   public double[] MonthPrice {get;set;} = new double[] { 795, 1195, 1825 };   public double[] TrimesterPrice {get;set;} = new double[] { 2145, 3225, 4930 };   public double[] YearPrice {get;set;} = new double[] { 7950, 11950, 18250 }; } |

Här ska du skapa metoden för att hantera beslutsfattandet. Här ska du kolla priset per biljett enligt antal zoner man ska resa.

Tänk på att zonen inte får vara mindre än 1 eller större än 3. mindre än 1 zon räknas som 1, mer än 3 zoner räknas som 3. Om priset för antal resor överstiger månadskostnad då är det bättre att föreslå månadskostnad, och dra av 30 resdagar från antalet resor, kolla sedan vilka biljetter som krävs för resterande resor.

Gör likadant med 90 dagars och ettårsbiljetter. Spara biljetterna i en array eller in en lista, som sedan returneras när metoden är klar.

| Västtrafik |
| --- |
| internal static class Program {     static void Main()     {         var test=new  TicketChooser();         var des = new TicketChooser();         var plannedTrips = 32;         var zones = 2;         double sum = 0;         foreach (var biljett in des.Västtrafik(zones, plannedTrips))         {             double price = 0;             switch (biljett)             {                 case TicketChooser.Biljetter.Enkelbiljett:                       price= des.DayPrice[zones - 1]; break;                 case TicketChooser.Biljetter.TrettioDagar:                       price= des.MonthPrice[zones - 1]; break;                 case TicketChooser.Biljetter.NittioDagar:                       price = des.TrimesterPrice[zones - 1]; break;                 case TicketChooser.Biljetter.TrehundrasextiofemDagar:                       price = des.YearPrice[zones - 1]; break;                 case TicketChooser.Biljetter.Error:                       price = des.DayPrice[zones - 1]; break;                 default:                       price = 0; break;             }             sum += price;             Console.WriteLine(biljett + " " + price);         }         Console.WriteLine("Summa : " + sum);     } }  public class TicketChooser {     public Biljetter [] Västtrafik(int zones = 1, int tripsPlanned = 1)     {         // Skriv din kod här     } } |


| Förväntad output:  TrettioDagar 1195 Enkelbiljett 68 Enkelbiljett 68 Enkelbiljett 68 Enkelbiljett 68 Summa : 1467 |
| --- |


# Facit

## Västtrafik

Här ska du skapa metoden för att hantera beslutsfattandet. Här ska du kolla priset per biljett enligt antal zoner man ska resa.

Tänk på att zonen inte får vara mindre än 1 eller större än 3. mindre än 1 zon räknas som 1, mer än 3 zoner räknas som 3. Om priset för antal resor överstiger månadskostnad då är det bättre att föreslå månadskostnad, och dra av 30 resdagar från antalet resor, kolla sedan vilka biljetter som krävs för resterande resor.

Gör likadant med 90 dagars och ettårsbiljetter. Spara biljetterna i en array eller in en lista, som sedan returneras när metoden är klar.

Tänk på att tur och retur bara ska räknas på enkelbiljetter!

| Västtrafik |
| --- |
| public Biljetter [] Västtrafik(int zones = 1, int tripsPlanned = 1) {     // Förbereder data     int multiplier = 2; // för att räkna tur och retur     List<Biljetter> biljetter = new List<Biljetter>();     // tripsPlanned *= multiplier; // tur och retur          // Kontrollerar data     if (zones < 1) zones = 1; // får inte vara mindre än 1 zon     if (zones > 3) zones = 3; // mer än tre zoner ger samma pris som tre     zones--; // för att fungera med arrayen          // Bearbetar data     while (tripsPlanned > 0)     {         // Förbereder data         var sumDay = DayPrice[zones] * tripsPlanned * multiplier;          var multi = Math.Ceiling(((double)tripsPlanned / 30));         var sumMonth = MonthPrice[zones] * multi;          multi = Math.Ceiling((double)tripsPlanned / 90);         var sumThreeMonths = TrimesterPrice[zones] * multi;          multi = Math.Ceiling((double)tripsPlanned / 365);         var sumYear = YearPrice[zones] * multi;                  double[] sorted = new[] { sumDay, sumMonth, sumThreeMonths, sumYear };         Array.Sort(sorted);                  // kontrollerar data         if (sorted[0] == sumDay)         {             // bearbetar data             for (int i = 0; i < multiplier; i++)             {                 biljetter.Add(Biljetter.Enkelbiljett);             }             tripsPlanned -= multiplier;        }         else if (sorted[0] == sumMonth)         {             // bearbetar data             biljetter.Add(Biljetter.TrettioDagar);              tripsPlanned -= 30;         }         else if (sorted[0] == sumThreeMonths)         {             // bearbetar data             biljetter.Add(Biljetter.NittioDagar);              tripsPlanned -= 90;         }         else if (sorted[0] == sumYear)         {             // bearbetar data             biljetter.Add(Biljetter.TrehundrasextiofemDagar);              tripsPlanned -= 365;         }         else         {             biljetter.Add(Biljetter.Error);             break;         }     }     // Presenterar resultatet     return biljetter.ToArray(); } |


Testa att köra programmet och passa på att pusha ditt projekt till Git!

När du är klar med projektet, pusha allting till Github!

# Sammanfattning:

## Vad har vi lärt oss av detta exempel?


## Vad kan göras bättre?

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
