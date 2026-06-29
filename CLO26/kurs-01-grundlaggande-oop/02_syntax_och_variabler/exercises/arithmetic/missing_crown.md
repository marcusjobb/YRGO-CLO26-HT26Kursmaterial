# Övning — Den saknade kronan

Alex, Sam och Kim beställer en pizza på Pizzeria Napoli. Den kostar 120 kr. <br>
Ingen har Swish — de betalar kontant. Var och en lägger fram sin 50-lapp. <br>

Kassan tar emot 150 kr och ger tillbaka 30 kr i växel. <br>
De kan inte dela 30 jämnt på tre — de tar 6 kr var och lägger 12 kr i Röda Korsets bössa i kassan. <br>
 <br>
På hemvägen börjar Alex räkna: <br>
 <br>
-"Vi betalade 50 kr var och fick 6 kr tillbaka, alltså betalade vi 44 kr var." <br>
-"44 × 3 = 132 kr." <br>
-"Plus de 12 kronorna vi donerade... 132 + 12 = 144 kr." <br>
-"Men vi lade fram 150 kr. **Var är de sex kronorna?**" <br>

![OMG](../../res/memes/double_counting.jpg)

## Flödeschema

```mermaid
flowchart LR
    A[Definiera variabler] --> B[Beräkna]
    B --> C[Utvärdera]
    C --> D[Korrigera]
    D --> E[Presentera resultat]
```

## Kod

Koden nedan räknar precis som Alex. Kör programmet, se vad som händer — och ta sedan reda på vart de sex kronorna tog vägen.

```csharp
int pizza = 120;
int alex = 50;
int sam = 50;
int kim = 50;
int donation = 0;

Console.WriteLine($"Pizzan kostar {pizza} kr");
Console.WriteLine($"Alex har {alex} kr");
Console.WriteLine($"Sam har {sam} kr");
Console.WriteLine($"Kim har {kim} kr");
Console.WriteLine();

Console.WriteLine($"De betalar {alex + sam + kim} kr");
int växel = (alex + sam + kim) - pizza;

alex -= 50;
sam -= 50;
kim -= 50;

Console.WriteLine($"Kassan ger tillbaka {växel} kr");
Console.WriteLine();

Console.WriteLine("De delar upp växeln:");
alex += 6;
sam += 6;
kim += 6;
växel -= 18;
donation = växel;

Console.WriteLine($"Alex har nu {alex} kr");
Console.WriteLine($"Sam har nu {sam} kr");
Console.WriteLine($"Kim har nu {kim} kr");
Console.WriteLine($"Och donerar {donation} kr till Röda Korset");
Console.WriteLine();

Console.WriteLine("Summa summarum:");
int nettoBetalning = (50 - 6) * 3;
Console.WriteLine($"De betalade 50 - 6 kr var, alltså 44 × 3 = {nettoBetalning} kr");
Console.WriteLine($"Och donerade {donation} kr");
nettoBetalning += donation;
Console.WriteLine($"Summan blir: {nettoBetalning} kr");

if (nettoBetalning != 150)
    Console.WriteLine("Error 404: Kronor not found");
```

### Förväntad output
```plaintext
Pizzan kostar 120 kr
Alex har 50 kr
Sam har 50 kr
Kim har 50 kr

De betalar 150 kr
Kassan ger tillbaka 30 kr

De delar upp växeln:
Alex har nu 6 kr
Sam har nu 6 kr
Kim har nu 6 kr
Och donerar 12 kr till Röda Korset

Summa summarum:
De betalade 50 - 6 kr var, alltså 44 × 3 = 132 kr
Och donerade 12 kr
Summan blir: 144 kr
Error 404: Kronor not found
```

## Utmanande frågor

Vad hände med pengarna? Hur kan summan bli mindre? Var det en skum Pizzabagare eller någon av killarna som försöker lura till sig pengar?
<br>Vad tror du?
<br>Hur skulle du lösa detta dilemma?

Kopiera koden till din Visual Studio och kör den, se om du får samma resultat, ändra i koden om så mycket du vill tills du får fram ett bra resultat.

<details><summary>Tips 1</summary>

```plaintext
Koden fungerar — den kraschar inte.
Men något i beräkningarna stämmer inte.

Rita flödet på papper: vart tar varje krona vägen
från att de lägger fram sina 50-lappar till att
programmet skriver ut summan?
```

</details>

<details><summary>Tips 2</summary>

```plaintext
Titta på den sista beräkningen.

nettoBetalning = 44 × 3 = 132 kr

Vad ingår egentligen i de 132 kronorna?
Bara pizzan, eller något mer?
```

</details>

<details><summary>Tips 3</summary>

```plaintext
132 = 120 (pizza) + 12 (donation)

Donationen är redan inräknad i nettot.
Ska du verkligen addera donation en gång till?
```

</details>

<details><summary>Lösningsförslag</summary>

För att förstå felet behöver vi tänka på vad de 132 kronorna faktiskt representerar.

44 × 3 = 132 är vad de betalade netto — alltså pizzapriset **plus** donationen redan inräknad (120 + 12 = 132). Att sedan addera donationen igen ger 144, inte 150.

Kronan är inte saknad. Matematiken var fel.

Det rätta sättet att kontrollera är att se till att alla pengar är redovisade:

```csharp
// pizza + tillbaka till personerna + donation = totalt betalt
int kontroll = pizza + (alex + sam + kim) + donation;
Console.WriteLine($"Kontroll: {pizza} + {alex + sam + kim} + {donation} = {kontroll} kr");

if (kontroll == 150)
    Console.WriteLine("Alla kronor är redovisade.");
```

Felet heter **double counting** — donationen räknades in två gånger. Det är ett klassiskt logikfel: koden körde utan krasch, men svaret var fel ändå.

Även om koden fungerar kan logiken vara kass.

</details>
