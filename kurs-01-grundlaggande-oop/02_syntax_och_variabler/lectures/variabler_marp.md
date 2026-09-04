---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Variabler och datatyper

### Hur C# minns saker

_Kurs 01 · Vecka 1 · Nion Education_

---

## Vad är en variabel?

En variabel är ett **namn på en plats i minnet**.

När du skriver:

```csharp
int ålder = 25;
```

Händer tre saker:

1. C# bokar en liten plats i datorns minne
2. Den platsen får namnet `ålder`
3. Värdet `25` sparas på den platsen

---

## Minnet — en enkel bild

Tänk dig datorns minne som en lång rad lådor.
Varje låda har en adress — men vi slipper hålla koll på den.
Det är det `ålder` gör åt oss.

```
Minnet:
┌──────────┬──────────┬──────────┬──────────┐
│    25    │          │          │          │
└──────────┴──────────┴──────────┴──────────┘
     ↑
   ålder
```

Skriver du `ålder` i koden — hämtar C# värdet från den lådan.
Skriver du `ålder = 30` — byter du ut vad som ligger i lådan.

---

## Deklaration och tilldelning

```csharp
int ålder = 25;       // deklarera + tilldela på en gång
```

Eller i två steg:

```csharp
int ålder;            // deklarera — lådan finns, men är tom
ålder = 25;           // tilldela — lägg 25 i lådan
```

> 💬 _"I praktiken gör du det på en rad. Men det är bra att veta att det är två separata saker."_

---

## Datatyper — vad ryms i lådan?

Olika typer av data tar olika mycket plats och beter sig olika.
C# vill veta på förhand vad lådan ska innehålla.

| Typ | Vad den håller | Exempel |
|-----|---------------|---------|
| `int` | Heltal | `42`, `-7`, `0` |
| `double` | Decimaltal | `3.14`, `99.9` |
| `string` | Text | `"hej"`, `"Marcus"` |
| `bool` | Sant eller falskt | `true`, `false` |

---

## int — heltal

```csharp
int pris = 799;
int rabatt = 30;
int saldo = -150;      // negativt fungerar också
```

Använd `int` när du räknar med hela saker — kronor, antal, år.

Obs: inga decimaler. `int resultat = 7 / 2;` ger `3`, inte `3.5`.

---

## double — decimaltal

```csharp
double bensinPris = 18.90;
double moms = 0.25;
double distans = 15.5;
```

Använd `double` när du behöver decimaler — priser, avstånd, procentsatser.

```csharp
double kostnad = distans * bensinPris;
// 15.5 × 18.90 = 292.95
```

---

## string — text

```csharp
string namn = "Alex";
string stad = "Göteborg";
string meddelande = "Hej " + namn + "!";   // "Hej Alex!"
```

Text skrivs alltid med **citattecken**. Utan dem tror C# att det är ett variabelnamn.

```csharp
string fel = Alex;    // FEL — C# letar efter en variabel som heter Alex
string rätt = "Alex"; // Rätt — det är texten Alex
```

---

## bool — sant eller falskt

```csharp
bool harRåd = true;
bool ärStudent = false;
```

Ser litet ut — men `bool` är grunden för alla beslut i koden.
Nästa gång: `if (harRåd)` — då börjar det hända saker.

---

## Namnregler

Variabelnamn ska vara **beskrivande**. Koden läses fler gånger än den skrivs.

```csharp
// Dåligt
int x = 799;
int y = 30;

// Bra
int ursprungspris = 799;
int rabattProcent = 30;
```

Regler i C#:
- Börja med liten bokstav: `mittNamn` (camelCase)
- Inga mellanslag, inga å/ä/ö i namn (fungerar men undvik)
- Namn ska berätta vad variabeln innehåller

---

## Kompilatorn — din noggranna kassörska

Koden du skriver är en **inköpslista**. Kompilatorn är kassörskan som jobbar sig igenom den.

```
Inköpslista:
☐ int pris = 799          → bokar låda, lägger in 799
☐ int rabatt = 30         → bokar låda, lägger in 30
☐ int slutpris = pris - rabatt  → räknar 799-30, lägger in 769
☐ Console.WriteLine(...)  → skriver ut på skärmen
```

En vara i taget. Uppifrån och ner. Inga undantag.

> 💬 _"Det betyder att du inte kan använda `slutpris` på rad 3 om du inte deklarerat den på rad 1 eller 2. Listan läses inte baklänges."_

![w:380](../res/memes/skip-a-line.jpeg)

---

## Allt tillsammans

```csharp
string produkt = "Jacka";
int ursprungspris = 799;
int rabattProcent = 30;
int presentkort = 150;
int saldo = 500;

int rabattBelopp = ursprungspris * rabattProcent / 100;
int slutpris = ursprungspris - rabattBelopp - presentkort;
int kvar = saldo - slutpris;

Console.WriteLine($"{produkt} kostar {ursprungspris} kr");
Console.WriteLine($"Efter rabatt och presentkort: {slutpris} kr");
Console.WriteLine($"Kvar på kontot: {kvar} kr");
```

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `streaming_vs_bio.md` — beräkna och jämför  
🟢 `rabattkoden.md` — jackan, rabatten, presentkortet  
🟡 `den_saknade_kronan.md` — hitta felet i logiken

_Ta det steg för steg. Använd tipsen om du fastnar._
