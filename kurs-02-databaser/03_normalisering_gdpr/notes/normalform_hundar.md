# Från Excelfil till riktig databas — hundar, adresser och normalformer

## Läringsmål

Efter den här artikeln ska du kunna:

- Förklara varför en-tabells-lösningar orsakar problem i praktiken
- Identifiera upprepande grupper och transitiva beroenden
- Bryta ut en tabell steg för steg med rätt motivering
- Koppla varje steg till rätt normalform (1NF, 3NF)

---

## Varför normaliserar vi överhuvudtaget?

Innan vi börjar hacka sönder tabeller är det värt att förstå vad vi faktiskt försöker lösa.

En databas är inte ett Excel-ark. En databas är ett system som ska kunna svara på frågor, hålla data konsekvent och växa utan att kollapsa under sin egen vikt. Excel är fantastiskt för att räkna på ett dataset en gång. En databas ska hantera att tusentals rader läggs till, ändras och raderas — varje dag — utan att skapa inkonsekvens.

Det finns ett begrepp som heter **dataintegritet**: att data är korrekt, konsekvent och att vi kan lita på den. Normalisering är det viktigaste verktyget vi har för att upprätthålla dataintegritet.

Det finns tre klassiska fiender mot dataintegritet som alla uppstår när vi _inte_ normaliserar:

- **Uppdateringsanomali** — för att ändra ett faktum måste vi uppdatera det på många ställen. Missar vi ett ställe är data inkonsekvent.
- **Insättningsanomali** — vi kan inte lägga till data utan att lägga till annan, orelaterad data samtidigt.
- **Raderingsanomali** — om vi raderar en rad försvinner information om flera olika saker på en gång.

Vi kommer att se alla tre i vår Excelfil.

---

## Utgångspunkten: föregångarens Excelfil

Du har precis fått ansvar för att modernisera ett hundägarregister. Föregångaren var noggrann — allt finns sparat. Men när du öppnar filen ser du det här:

| PersonId | Förnamn | Efternamn | Gata | Husnr | Postnummer | Ort | Lan | Land | HundNamn | HundRas | RasUrsprung | PappaNamn | MammaNamn |
|---|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 1 | Anna | Karlsson | Kungsgatan | 5 | 41117 | Göteborg | Västra Götaland | Sverige | Fido | Labrador | England | — | — |
| 1 | Anna | Karlsson | Kungsgatan | 5 | 41117 | Göteborg | Västra Götaland | Sverige | Bella | Border Collie | Skottland | Fido | Molly |
| 2 | Björn | Svensson | Drottninggatan | 12 | 41104 | Göteborg | Västra Götaland | Sverige | Rex | Schäfer | Tyskland | — | — |
| 3 | Cecilia | Lindqvist | Storgatan | 3 | 11357 | Stockholm | Stockholm | Sverige | Kula | Golden Retriever | Skottland | — | Bella |

Anna finns på rad 1 **och** rad 2. Exakt samma adressinformation, exakt samma person — hon har bara två hundar.

Det här är inte ett märkligt misstag. Det är det naturliga resultatet av att tänka i "en rad per händelse" — en person med en hund är en rad, en person med två hundar är två rader. I ett enkelt Excel-ark fungerar det. Men i ett system som ska växa, ändras och vara tillförlitligt i år — det håller inte.

Låt oss räkna problemen:

---

### Problem 1: Uppdateringsanomali

Anna byter adress. För att hålla databasen konsekvent måste vi uppdatera **alla** rader där Anna finns. Om vi hittar rad 1 men inte rad 2 — eller om systemet kraschar mitt i — har vi plötsligt en Anna med två adresser. Ingen vet vilken som är rätt.

Och hur vet vi hur många rader Anna finns på? Det vet vi inte förrän vi sökt igenom hela tabellen.

### Problem 2: Insättningsanomali

Vi vill lägga till information om att Border Collies ursprungligen comes from Skottland. Men för att lägga till den informationen måste vi antingen hitta en befintlig hund av den rasen, eller skapa en hund som inte existerar. Rasen kan inte existera i systemet utan att en hund bär på den.

### Problem 3: Raderingsanomali

Cecilia slutar vara hundägare och tas bort ur registret. Vi raderar hennes rad. Men nu försvinner också all information om att "Kula" existerade — och att Golden Retrievers ursprung är Skottland. En radering av kunddata drar med sig djurdata och rasdata i fallet.

### Problem 4: Trasiga referenser

Bellas PappaNamn är "Fido" och MammaNamn är "Molly". Det är bara text. Systemet kan inte kontrollera att en hund med det namnet existerar. Molly finns inte ens i tabellen. Om vi vill ställa frågan "vilka hundar har Bella som förälder?" kan vi inte göra det — vi har bara en textsträng att söka i, och texten kan vara felstavad, inkonsekvent eller saknas helt.

Det här är vad man kallar **nollte normalform (0NF)** — eller Excelhögans form. Allt finns, ingenting är strukturerat.

---

## Steg 1 → 1NF: Ta bort upprepande grupper

Den första normalformen handlar om en grundläggande princip:

> **1NF:** Varje kolumn ska innehålla ett enda, odelbart (atomiskt) värde. Inga upprepande grupper.

**Vad betyder "atomiskt"?** Ett värde är atomiskt om det inte kan delas upp i meningsfulla delar i databassammanhang. "Göteborg" är atomiskt — det är ett stadsnamn. "Göteborg, Stockholm" i en enda cell är _inte_ atomiskt — det är två värden packade ihop.

**Vad är "upprepande grupper"?** Det är när samma typ av information dyker upp på flera rader för samma entitet. Anna upprepas med ny hunddata — det är en upprepande grupp.

Kränkningar av 1NF ser ut som:
- Kommaseparerade värden i en cell: `"Fido, Bella, Rex"`
- Kolumner som `Hund1`, `Hund2`, `Hund3` — vad gör vi när någon skaffar en fjärde?
- Dubletterade rader för samma entitet med olika värden i en kolumn

Lösningen i alla fall är densamma: separera entiteterna i egna tabeller och koppla dem med relationer.

Vi skapar tre tabeller:

---

### Tabell: Person

Den person som finns i systemet, en gång.

| PersonId | Förnamn | Efternamn | Gata | Husnr | Postnummer | Ort | Lan | Land |
|---|---|---|---|---|---|---|---|---|
| 1 | Anna | Karlsson | Kungsgatan | 5 | 41117 | Göteborg | Västra Götaland | Sverige |
| 2 | Björn | Svensson | Drottninggatan | 12 | 41104 | Göteborg | Västra Götaland | Sverige |
| 3 | Cecilia | Lindqvist | Storgatan | 3 | 11357 | Stockholm | Stockholm | Sverige |

Anna finns nu på exakt en rad. Om hon byter adress uppdaterar vi den enda raden. Klart.

---

### Tabell: Hund

Varje hund som existerar i systemet, en gång.

| HundId | Namn | Ras | RasUrsprung | PappaId | MammaId |
|---|---|---|---|---|---|
| 1 | Fido | Labrador | England | NULL | NULL |
| 2 | Bella | Border Collie | Skottland | 1 | 5 |
| 3 | Rex | Schäfer | Tyskland | NULL | NULL |
| 4 | Kula | Golden Retriever | Skottland | NULL | 2 |
| 5 | Molly | Border Collie | Skottland | NULL | NULL |

Lägg märke till att Molly (id 5) nu faktiskt existerar som en post. Hon behöver inte längre vara en textsträng som kan vara felstavad — hon är en riktig entitet som databasen kan verifiera.

`PappaId` och `MammaId` pekar på `HundId` i **samma tabell**. Bella (id 2) har pappa Fido (id 1) och mamma Molly (id 5). Det kallas en **självrefererande relation** och vi återkommer till det längre ner.

---

### Tabell: HundPerson (kopplingstabell)

Kopplingen "vem äger vilken hund" är en **många-till-många-relation** — en person kan ha flera hundar, och i teorin kan en hund ha flera ägare (gemensam hund). Sådana relationer kräver en egen tabell.

| PersonId | HundId |
|---|---|
| 1 | 1 |
| 1 | 2 |
| 2 | 3 |
| 3 | 4 |

Nu äger Anna (PersonId 1) hundarna Fido (HundId 1) och Bella (HundId 2). Om Anna skaffar en tredje hund lägger vi till en rad i HundPerson — utan att röra Person-tabellen alls.

Vi är i **1NF**. Upprepande grupper är borta. Problemet med raderingsanomalin är löst — vi kan ta bort Cecilia utan att Kula försvinner.

Men titta noga på Person-tabellen igen. Det finns fortfarande problem gömda där.

---

## Steg 2: Förstå transitiva beroenden

Innan vi går vidare behöver vi ett nytt begrepp: **transitivt beroende**.

Tänk på det som en kedja av orsak och verkan:

> A bestämmer B, och B bestämmer C — men vi har lagrat A, B och C i samma tabell som om de alla tillhör A direkt.

I vår Person-tabell händer det här:

- `PersonId` bestämmer `Postnummer` — rimligt, en person bor på ett postnummer
- `Postnummer` bestämmer `Ort` — postnummer 41117 är alltid Göteborg
- `Postnummer` bestämmer `Lan` — 41117 är alltid i Västra Götaland
- `Lan` bestämmer `Land` — Västra Götaland är alltid i Sverige

`Ort` och `Lan` tillhör egentligen `Postnummer`, inte `PersonId`. Orten är ett attribut på postnumret — inte på personen. Det faktum att Anna bor i Göteborg följer av att hon bor på postnummer 41117 — inte av vem Anna är.

**Varför är det ett problem?**

Tänk om Göteborgs stad och en kranskommunen slår ihop och ett par postnummer byter Lan-tillhörighet. Vi måste nu uppdatera varje enskild rad i Person-tabellen för varje person som bor i de postnumren. Det är ett uppdateringsproblem — samma typ av problem vi löste i steg 1, men nu med geografidata.

Och tänk om vi vill fråga "vilka postnummer finns i Västra Götaland?" utan transitiva beroenden måste vi söka igenom alla personers adresser. Det är ineffektivt och beroende av att alla personer faktiskt har ett värde i Lan-kolumnen.

**Tredje normalformen (3NF)** eliminerar det här:

> **3NF:** Alla kolumner ska bero direkt på primärnyckeln — inte på andra icke-nyckelkolumner.

Vi bryter ut steg för steg. Varje steg är litet, men var och ett har en klar motivering.

---

## Steg 3 → 3NF: Bryt ut Land

Texten "Sverige" upprepas på varje rad i Person-tabellen. Det har ingenting med personen att göra — ett land är ett eget faktum i världen.

**Vad händer om vi inte bryter ut Land?**
Om Sverige skulle byta namn (det händer inte, men principen gäller) eller om vi vill lägga till ett nytt attribut på landet (t.ex. valuta, landskod) måste vi ändra Person-tabellen eller lägga till kolumner. Det är inte Person-tabellens ansvar att veta vad som gäller om ett land.

**Tabell: Land**

| LandId | Namn |
|---|---|
| 1 | Sverige |
| 2 | England |
| 3 | Skottland |
| 4 | Tyskland |

Vi lägger till fler länder efter behov — utan att röra Person. Om vi vill lägga till "valuta" på varje land gör vi det i Land-tabellen. Person vet bara sitt LandId.

**Person uppdateras** — kolumnen `Land` ersätts med `LandId`:

| PersonId | Förnamn | Efternamn | Gata | Husnr | Postnummer | Ort | Lan | LandId |
|---|---|---|---|---|---|---|---|---|
| 1 | Anna | Karlsson | Kungsgatan | 5 | 41117 | Göteborg | Västra Götaland | 1 |
| 2 | Björn | Svensson | Drottninggatan | 12 | 41104 | Göteborg | Västra Götaland | 1 |
| 3 | Cecilia | Lindqvist | Storgatan | 3 | 11357 | Stockholm | Stockholm | 1 |

Texten "Sverige" finns nu på exakt ett ställe. Men Ort och Lan upprepas fortfarande.

---

## Steg 4 → 3NF: Bryt ut Ort

`Postnummer → Ort` är fortfarande ett transitivt beroende. "Göteborg" i Person-tabellen är inte ett faktum om Anna — det är ett faktum om postnumret 41117.

**Vad händer om vi inte bryter ut Ort?**
Björn och Anna bor båda i Göteborg, på olika postnummer. Om vi vill veta alla unika orter i systemet måste vi scanna alla personrader och filtrera dubbletter. Det är inte fel, men det är ineffektivt och det beror på att vi _tur att alla orts-poster är korrekt ifyllda. Ett stavfel i ett datum ändrar inte ett postnummer, men ett stavfel i "Göteborg" (t.ex. "Gothenburg" eller "Götheborg") skapar en ny "ort" i systemet.

**Tabell: Ort**

| OrtId | Namn | LandId |
|---|---|---|
| 1 | Göteborg | 1 |
| 2 | Stockholm | 1 |

Varje ort kopplas direkt till ett land. Om vi vill veta i vilket land en stad finns är det ett enkelt uppslag, inte en sökning genom alla personrader.

**Tabell: Postnummer** — nu med en koppling till Ort:

| PostnummerID | Kod | OrtId |
|---|---|---|
| 1 | 41117 | 1 |
| 2 | 41104 | 1 |
| 3 | 11357 | 2 |

Postnummer 41117 och 41104 är båda i Göteborg (OrtId 1). Om det finns 200 postnummer i Göteborg är det 200 rader i Postnummer-tabellen — men bara en rad i Ort-tabellen som säger "Göteborg".

**Person uppdateras** — Ort, Lan och Land ersätts med ett enda postnummer-id:

| PersonId | Förnamn | Efternamn | Gata | Husnr | PostnummerID |
|---|---|---|---|---|---|
| 1 | Anna | Karlsson | Kungsgatan | 5 | 1 |
| 2 | Björn | Svensson | Drottninggatan | 12 | 2 |
| 3 | Cecilia | Lindqvist | Storgatan | 3 | 3 |

Nu kan vi fråga "vilka postnummer finns i Göteborg?" och "hur många unika städer finns i systemet?" utan att gräva i persondata.

---

## Steg 5 → 3NF: Bryt ut Gata

En gata hör hemma i ett postnummerområde. "Kungsgatan" i Person-tabellen är inte ett faktum om Anna — det är ett faktum om en geografisk plats.

**Varför bryta ut gatan?**
Det finns tre skäl.

Första skälet: om 500 personer bor på Kungsgatan lagras texten "Kungsgatan" 500 gånger. Det är slöseri, och det innebär att en stavningsändring kräver 500 uppdateringar.

Andra skälet: en gata kan i verkligheten sträcka sig över flera postnummer. Möjligen börjar Storgatan med postnummer 11357 och slutar med 11459. Om gatan lagras som text i Person är det omöjligt att representera den korrekt.

Tredje skälet: med en separat Gata-tabell kan vi ställa frågor direkt om gator — hur många gator finns i ett postnummer? Vilka postnummer delar en gata? Det är omöjligt utan en riktig gatupost.

**Tabell: Gata**

| GataId | Namn | PostnummerID |
|---|---|---|
| 1 | Kungsgatan | 1 |
| 2 | Drottninggatan | 2 |
| 3 | Storgatan | 3 |

**Person — slutversion:**

| PersonId | Förnamn | Efternamn | GataId | Husnr |
|---|---|---|---|---|
| 1 | Anna | Karlsson | 1 | 5 |
| 2 | Björn | Svensson | 2 | 12 |
| 3 | Cecilia | Lindqvist | 3 | 3 |

Personens adress är nu ett enda id. Anna bor på GataId 1, husnummer 5. Allt annat — gatunamn, postnummer, stad, land — hänger ihop via relationer. Om Anna byter adress till Storgatan 7 ändrar vi `GataId` och `Husnr` på en enda rad. Ingenting annat påverkas.

---

## Steg 6 → 3NF: Bryt ut Ras från Hund

Samma typ av transitivt beroende finns i Hund-tabellen.

"Labrador kommer från England" är ett faktum om rasen — inte om den specifika hunden Fido. Det spelar ingen roll om Fido är den gladaste, snabbaste eller tjuraste labradoren som någonsin levt — han är fortfarande från England i den meningen att hans ras härstammar därifrån.

Beroendet ser ut så här: `HundId → Ras → RasUrsprung`. Ursprungslandet är ett attribut på rasen, inte på hunden.

**Vad händer om vi inte bryter ut det?**
Om vi har 200 labradorer i systemet lagras texten "England" 200 gånger. Om rasonforumationen behöver kompletteras — kanske vi vill lägga till en "officiell FCI-grupp" eller en medellivslängd — måste vi lägga till kolumner i Hund-tabellen och fylla i samma värde för alla 200 labradorer.

**Tabell: Ras**

| RasId | Namn | UrsprungslandId |
|---|---|---|
| 1 | Labrador | 2 |
| 2 | Border Collie | 3 |
| 3 | Schäfer | 4 |
| 4 | Golden Retriever | 3 |

Notera att `UrsprungslandId` pekar på samma **Land-tabell** vi skapade i steg 3. En tabell kan refereras av hur många andra tabeller som helst — det är en central styrka i relationsmodellen. Skottland finns en gång i Land-tabellen och används av både Border Collie och Golden Retriever.

**Hund — slutversion:**

| HundId | Namn | RasId | PappaId | MammaId |
|---|---|---|---|---|
| 1 | Fido | 1 | NULL | NULL |
| 2 | Bella | 2 | 1 | 5 |
| 3 | Rex | 3 | NULL | NULL |
| 4 | Kula | 4 | NULL | 2 |
| 5 | Molly | 2 | NULL | NULL |

---

## Självrefererande relationer — en djupdykning

`PappaId` och `MammaId` pekar båda på `HundId` i **samma tabell**. Det kallas en **självrefererande relation**, och det är ett elegant och kraftfullt mönster.

Varför gör vi det här i stället för att skapa separata tabeller för "föräldrahundar"?

Därför att en föräldrahund är exakt samma sorts entitet som barnahunden. Fido är en hund. Molly är en hund. Det faktum att Fido är Bellas pappa gör inte Fido till en annan _typ_ av sak — han är fortfarande en hund i hundregistret.

Om vi skapade en separat `ForäldraHund`-tabell skulle vi behöva duplicera all hunddata dit. Det är precis det vi försöker undvika.

Med den självrefererande relationen kan vi nu göra saker som var omöjliga i Excel-filen:

```sql
-- Vem är Bellas föräldrar?
SELECT pappa.Namn AS Pappa, mamma.Namn AS Mamma
FROM Hund h
LEFT JOIN Hund pappa ON h.PappaId = pappa.id
LEFT JOIN Hund mamma ON h.MammaId = mamma.id
WHERE h.Namn = 'Bella';
```

```sql
-- Vilka hundar är barn till Bella?
SELECT Namn FROM Hund WHERE PappaId = 2 OR MammaId = 2;
```

```sql
-- Vilka hundar är syskon till Bella (delar minst en förälder)?
SELECT DISTINCT syskon.Namn
FROM Hund syskon
JOIN Hund bella ON bella.Namn = 'Bella'
WHERE (syskon.PappaId = bella.PappaId AND bella.PappaId IS NOT NULL)
   OR (syskon.MammaId = bella.MammaId AND bella.MammaId IS NOT NULL)
   AND syskon.id != bella.id;
```

Det tredje exemplet — syskon — hade inte ens gått att formulera mot Excel-filen.

---

## Slutresultatet

Klistra in koden på [dbdiagram.io](https://dbdiagram.io) för att se diagrammet visuellt.

```
Table Person
{
  id integer [pk]
  fName string
  lName string
  gataId integer
  husnummer string
}

Table Gata
{
  id integer [pk]
  namn string
  postnummerId integer
}

Table Postnummer
{
  id integer [pk]
  kod string
  ortId integer
}

Table Ort
{
  id integer [pk]
  namn string
  landId integer
}

Table Land
{
  id integer [pk]
  namn string
}

Table Hund
{
  id integer [pk]
  namn string
  rasId integer
  pappaId integer
  mammaId integer
}

Table HundPerson
{
  personId integer
  hundId integer
}

Table Ras
{
  id integer [pk]
  namn string
  ursprungslandId integer
}

Ref: "Person"."gataId" > "Gata"."id"
Ref: "Gata"."postnummerId" > "Postnummer"."id"
Ref: "Postnummer"."ortId" > "Ort"."id"
Ref: "Ort"."landId" > "Land"."id"
Ref: "Hund"."rasId" > "Ras"."id"
Ref: "Hund"."pappaId" - "Hund"."id"
Ref: "Hund"."mammaId" - "Hund"."id"
Ref: "Ras"."ursprungslandId" > "Land"."id"
Ref: "HundPerson"."personId" > "Person"."id"
Ref: "HundPerson"."hundId" > "Hund"."id"
```

---

## Vad kan vi nu fråga?

En välnormaliserad databas är inte bara snyggare — den gör frågor möjliga som var omöjliga mot Excel-filen.

**Vilka postnummer finns i Göteborg?**

Utan normalisering: skanna alla personers adresser och filtrera på "Göteborg" (text som kan vara felstavad).
Med normalisering: slå upp Ort med namn "Göteborg" och hämta alla Postnummer med det OrtId.

```sql
SELECT p.Kod
FROM Postnummer p
JOIN Ort o ON p.OrtId = o.id
WHERE o.Namn = 'Göteborg';
```

**Hur många hundar bor på Kungsgatan?**

```sql
SELECT COUNT(hp.HundId) AS AntalHundar
FROM Gata g
JOIN Person per ON per.GataId = g.id
JOIN HundPerson hp ON hp.PersonId = per.id
WHERE g.Namn = 'Kungsgatan';
```

**Hur många hundar har varje person i postnummer 41117?**

```sql
SELECT per.Fornamn, per.Efternamn, COUNT(hp.HundId) AS AntalHundar
FROM Person per
JOIN Gata g ON per.GataId = g.id
JOIN Postnummer p ON g.PostnummerId = p.id
JOIN HundPerson hp ON hp.PersonId = per.id
WHERE p.Kod = '41117'
GROUP BY per.id, per.Fornamn, per.Efternamn;
```

**Vilket ursprungsland har hundarna i ett visst postnummer?**

Vi kan inte ens formulera den här frågan mot Excel-filen. Med normalisering är kedjan tydlig: person → hund → ras → land.

```sql
SELECT DISTINCT l.Namn AS Ursprungsland
FROM Hund h
JOIN Ras r ON h.RasId = r.id
JOIN Land l ON r.UrsprungslandId = l.id
JOIN HundPerson hp ON hp.HundId = h.id
JOIN Person per ON hp.PersonId = per.id
JOIN Gata g ON per.GataId = g.id
JOIN Postnummer p ON g.PostnummerId = p.id
WHERE p.Kod = '41117';
```

**Vem äger Bella?**

```sql
SELECT per.Fornamn, per.Efternamn
FROM Person per
JOIN HundPerson hp ON hp.PersonId = per.id
JOIN Hund h ON hp.HundId = h.id
WHERE h.Namn = 'Bella';
```

---

## Sammanfattning

| Steg | Vad bröts ut | Varför det matterats | Normalform |
|---|---|---|---|
| 1 | Hundar till separat tabell + HundPerson | Upprepande grupper — Anna duplicerades för varje hund | 1NF |
| 2 | Land till separat tabell | Länder är egna entiteter, ska inte upprepas i personrader | 3NF |
| 3 | Ort till separat tabell, länkad till Land | Ort beror på postnummer, inte på personen — transitivt beroende | 3NF |
| 4 | Postnummer till separat tabell, länkad till Ort | Postnumret bär ortdata som hör till postnumret, inte personen | 3NF |
| 5 | Gata till separat tabell, länkad till Postnummer | En gata är en geografisk entitet — inte ett attribut på en person | 3NF |
| 6 | Ras till separat tabell med ursprungsland | Rasursprunget är ett faktum om rasen — inte om den enskilda hunden | 3NF |

Varje steg hade samma drivkraft: data ska lagras på ett ställe och refereras därifrån. Aldrig upprepas.

Den enkel tumregeln som håller: **om du behöver ändra ett faktum på mer än ett ställe i databasen, är något inte normaliserat.**
