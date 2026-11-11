# Från kaos till kvitto — normalisering, ordrar och GDPR

## Läringsmål

Efter den här artikeln ska du kunna:

- Identifiera problemen som uppstår när kunddata lagras direkt i ordertabellen
- Bryta ut ett ordersystem till 3NF steg för steg med motivering
- Förklara varför en välnormaliserad databas kan förstöra orderhistoriken
- Designa en kvittolösning som skyddar historisk data
- Resonera kring vad som måste bevaras och vad som måste raderas när GDPR kräver att en kund tas bort

---

## Utgångspunkten: arvsystemet

Du har tagit över en liten webbutik som säljer böcker och leksaker. Föregångaren var pragmatisk — "det funkade ju" — och lade allt i en enda tabell. Nu ska systemet växa, och det är dags att titta under huven.

Det här är tabellen du ärver:

| OrderId | KundNamn | KundEmail | KundAdress | KundStad | KundPostnr | KundVisa | ProduktNamn | Kategori | Antal | Pris | Datum |
|---|---|---|---|---|---|---|---|---|---|---|---|---|
| 1 | Anna Karlsson | anna@mail.se | Kungsgatan 5 | Göteborg | 41117 | 1234 | Lär dig Python | Bok | 1 | 299 | 2024-01-15 |
| 2 | Anna Karlsson | anna@mail.se | Kungsgatan 5 | Göteborg | 41117 | 1234 | LEGO City 60388 | Leksak | 2 | 499 | 2024-02-03 |
| 3 | Björn Svensson | bjorn@mail.se | Storgatan 12 | Stockholm | 11357 | 5678 | Lär dig Python | Bok | 1 | 299 | 2024-02-10 |
| 4 | Anna Karlsson | anna@mail.se | Ladugårdsvägen 3 | Göteborg | 41652 | 1234 | Minecraft LEGO | Leksak | 1 | 649 | 2024-11-20 |

Titta noga på rad 1 och rad 4. Samma Anna — men med två olika adresser. Hon flyttade i november. Systemet lade in hennes nya adress på ny order men de gamla ordrarna har kvar den gamla.

Nu vet vi egentligen inte längre om adresserna på order 1 och 2 är korrekta. Var det till Kungsgatan paketen skickades? Troligen. Men systemet kan inte bekräfta det.

Det är ett symptom på ett djupare strukturproblem. Låt oss räkna fler:

---

### Problem 1: Kunddata upprepas (uppdateringsanomali)

Anna finns på tre rader. Om hon ändrar e-postadress behöver vi uppdatera alla tre — och det förutsätter att vi hittar alla tre. Det finns inget i designen som garanterar att vi gör det.

Vad händer om systemet uppdaterar email på rad 1 och 2, men kraschar innan rad 4? Nu har Anna två e-postadresser i systemet. Vilken är rätt? Det vet inte databasen.

### Problem 2: Produktdata upprepas (uppdateringsanomali)

"Lär dig Python" kostar 299 kr på rad 1 och 3. Om förlaget höjer priset till 349 kr och vi uppdaterar det i tabellen — påverkas gamla ordrar? Kanske. Kanske inte. Det beror på hur uppdateringen skrivs, och det är ett bräckligt system.

### Problem 3: Kategori beror inte på ordern (transitivt beroende)

"Bok" på rad 1 och 3 är en egenskap hos produkten — inte hos den specifika ordern. Om vi beslutar att byta kategorin "Bok" till "Litteratur" i hela systemet, måste vi nu hitta alla rader i Ordertabellen som nämner "Bok". Det är inte orderdata. Det ska inte ligga i ordertabellen.

### Problem 4: Radering river sönder historia (raderingsanomali)

"Lär dig Python" slutar säljas. Vi raderar produkten. Men vad händer med order 1 och 3? De pekar på en produkt som inte längre finns. Vi har tre val:
- Foreign key-constraint blockerar raderingen (produkten kan inte raderas)
- Kaskadradering tar bort orderraderna med (historik försvinner)
- NULL sätts in (ordern blir ofullständig och meningslös)

Inget av dem är rätt. Det är ett designproblem, inte ett databasoperationsproblem.

Det här är **0NF** — allt är på ett ställe, ingenting är strukturerat.

---

## Steg 1 → 2NF/3NF: Bryt ut Kund

Det mest uppenbara steget: en kund är ett eget koncept och ska finnas på exakt ett ställe.

**Varför?** Inte bara för att undvika upprepning — utan för att separera _vem Anna är_ från _vad Anna har köpt_. Kunduppgifter och orderhistorik är två olika saker med olika livscykler. En kund kan uppdatera sin adress utan att det ska påverka gamla ordrar (eller egentligen borde det inte, men det återkommer vi till).

**Tabell: Kund**

| KundId | Namn | Email | Adress | Stad | Postnr | VisaSista4 |
|---|---|---|---|---|---|---|
| 1 | Anna Karlsson | anna@mail.se | Ladugårdsvägen 3 | Göteborg | 41652 | 1234 |
| 2 | Björn Svensson | bjorn@mail.se | Storgatan 12 | Stockholm | 11357 | 5678 |

Vi lagrar bara den _nuvarande_ adressen. Kungsgatan 5 är borta — Anna bor där inte längre.

**Order uppdateras** — kundkolumnerna ersätts med ett KundId:

| OrderId | KundId | ProduktNamn | Kategori | Antal | Pris | Datum |
|---|---|---|---|---|---|---|
| 1 | 1 | Lär dig Python | Bok | 1 | 299 | 2024-01-15 |
| 2 | 1 | LEGO City 60388 | Leksak | 2 | 499 | 2024-02-03 |
| 3 | 2 | Lär dig Python | Bok | 1 | 299 | 2024-02-10 |
| 4 | 1 | Minecraft LEGO | Leksak | 1 | 649 | 2024-11-20 |

Bättre. Men vi har nu introducerat ett nytt problem — ett som var osynligt i den gamla designen.

Om vi visar orderhistorik för Anna och hämtar hennes adress från Kund-tabellen, visar vi Ladugårdsvägen 3 — den adress hon bor på _idag_. Vi vet inte längre var order 1 levererades.

Det är inte ett normaliseringsfel. Det är en konsekvens av att normalisering är optimerad för _aktuell_ data, inte _historisk_ data. Vi löser det separat. Fortsätt läsa.

---

## Steg 2 → 3NF: Bryt ut Produkt och Kategori

Produktdata upprepas på flera ordrar och bär ett transitivt beroende: `OrderId → ProduktNamn → Kategori`. Kategorin tillhör produkten — inte den enskilda ordern.

**Varför är det ett problem att Kategori sitter i Order?**

Tänk dig att vi vill räkna "hur mycket har vi sålt i kategorin Bok det senaste halvåret?" Med nuvarande design skannar vi alla ordrar och filtrerar på texten "Bok". Det fungerar — tills någon stavar fel ("book"), tills en kategori byter namn, eller tills samma produkt dyker upp under två olika kategorier beroende på vilken anställd som lade in ordern.

En kategori ska vara ett faktum som definieras en gång och refereras därifrån.

**Tabell: Kategori**

| KategoriId | Namn |
|---|---|
| 1 | Bok |
| 2 | Leksak |

**Tabell: Produkt**

| ProduktId | Namn | Pris | KategoriId | Aktiv |
|---|---|---|---|---|
| 1 | Lär dig Python | 349 | 1 | true |
| 2 | LEGO City 60388 | 499 | 2 | true |
| 3 | Minecraft LEGO | 649 | 2 | true |

Notera: `Lär dig Python` kostar nu 349 kr — priset ändrades. Det är rätt att produkten har sitt _nuvarande_ pris. Men order 1 och 3 gjordes för 299 kr.

Därför kan vi inte bara lägga in `ProduktId` i Order och hämta priset från Produkt. Vi behöver en mellanliggande tabell som lagrar priset _vid köptillfället_.

---

## Steg 3 → 3NF: Skapa Orderrad med pris vid köp

En order kan ha flera produkter. Det kan inte representeras korrekt med en enda rad per order — det är samma upprepningsproblem vi löste i hundartikeln. Vi behöver en kopplingstabell.

**Tabell: Order** — nu renodlad till att bara handla om ordern som händelse:

| OrderId | KundId | Datum |
|---|---|---|
| 1 | 1 | 2024-01-15 |
| 2 | 1 | 2024-02-03 |
| 3 | 2 | 2024-02-10 |
| 4 | 1 | 2024-11-20 |

**Tabell: Orderrad** — en rad per produkt per order, med priset låst vid köptillfället:

| OrderradId | OrderId | ProduktId | Antal | PrisVidKop |
|---|---|---|---|---|
| 1 | 1 | 1 | 1 | 299 |
| 2 | 2 | 2 | 2 | 499 |
| 3 | 3 | 1 | 1 | 299 |
| 4 | 4 | 3 | 1 | 649 |

`PrisVidKop` är inte ett normaliseringsbrott. Det är en _snapshot_ — ett avsiktligt lagrat faktum om hur världen såg ut vid ett specifikt tillfälle. Produkten kostar 349 idag. Kunden betalade 299. Båda är sanna, men om olika saker.

Nu är systemet i **3NF**. Varje tabell har en tydlig primärnyckel. Inga transitiva beroenden. Kunddata ändras utan att påverka ordrar. Produktdata ändras utan att påverka betalda priser.

Men problemet med historiken kvarstår.

---

## Problemet: normalisering lever i nuet

Låt oss testa tre verkliga scenarier mot vår välnormaliserade databas.

### Scenario A: Anna byter adress

Anna flyttar. Vi uppdaterar `KundId = 1` med den nya adressen. Korrekt.

Men om vi nu visar orderhistorik och hämtar adressen från Kund-tabellen visar vi den nya adressen — även på order 1, 2 och 3 som levererades till den gamla adressen. Om Anna ringer och säger att hon aldrig fick paketet för order 1 — till vilken adress levererades det? Vi vet inte längre.

### Scenario B: En produkt byter namn

Förlaget byter titel. "Lär dig Python" heter nu "Python för nybörjare". Vi uppdaterar Produkt-tabellen.

Nu visar Orderrad 1 och 3 (via JOIN mot Produkt) det nya namnet — fastän kunden har ett fysiskt kvitto hemma som säger "Lär dig Python". Om de returnerar boken och hävdar att de aldrig fått rätt produkt — har de fel, men systemet stöder deras påstående.

### Scenario C: En produkt raderas

"Lär dig Python" fasas ut ur sortimentet. `ProduktId = 1` raderas.

Orderrad 1 och 3 pekar nu på ett `ProduktId` som inte existerar. Databasen blockerar antingen raderingen (om vi har en foreign key-constraint) eller låter orderraderna hänga i luften med ett ogiltigt id. Vi har fastnat.

**Vad är det egentliga problemet?**

En order är inte ett nuläge — den är ett historiskt faktum. "Anna köpte Lär dig Python för 299 kr den 15 januari 2024" är sant oavsett vad som händer med produkten, priset eller Anna efteråt. En normaliserad databas lagrar aktuell information på ett enda ställe och låter allt referera dit. Men om det som refereras förändras förändras historiken med.

En fysisk butik löser det här med ett papper — kvittot. Vi behöver en digital motsvarighet.

---

## Lösningen: Kvittotabellen — en snapshot i tid

Ett kvitto är en fryst bild av verkligheten vid ett specifikt tillfälle. Det ändras aldrig. Det är inte normaliserat på samma sätt som resten av databasen — och det är avsiktligt.

Precis som en papperslapp på en hylla inte ändras när priset ändras i kassasystemet, ska ett digitalt kvitto inte ändras när kunddata eller produktdata förändras.

Vi skapar kvittot vid köptillfället och lagrar allt som text:

**Tabell: Kvitto**

| KvittoId | OrderId | KundNamn | KundAdress | KundEmail | Totalbelopp | KvittoDatum |
|---|---|---|---|---|---|---|
| 1 | 1 | Anna Karlsson | Kungsgatan 5, 41117 Göteborg | anna@mail.se | 299 | 2024-01-15 |
| 2 | 2 | Anna Karlsson | Kungsgatan 5, 41117 Göteborg | anna@mail.se | 998 | 2024-02-03 |
| 3 | 3 | Björn Svensson | Storgatan 12, 11357 Stockholm | bjorn@mail.se | 299 | 2024-02-10 |
| 4 | 4 | Anna Karlsson | Ladugårdsvägen 3, 41652 Göteborg | anna@mail.se | 649 | 2024-11-20 |

**Tabell: Kvittorad**

| KvittoradId | KvittoId | ProduktNamn | Kategori | Antal | Enhetspris |
|---|---|---|---|---|---|
| 1 | 1 | Lär dig Python | Bok | 1 | 299 |
| 2 | 2 | LEGO City 60388 | Leksak | 2 | 499 |
| 3 | 3 | Lär dig Python | Bok | 1 | 299 |
| 4 | 4 | Minecraft LEGO | Leksak | 1 | 649 |

`KundAdress`, `ProduktNamn` och `Kategori` är inte foreign keys — de är text, kopierade in från källtabellerna vid köptillfället. De ändras aldrig.

Nu testar vi scenarierna:

**Anna byter adress:** Kvitto 1 och 2 visar fortfarande "Kungsgatan 5" — för det var hennes adress när köpen gjordes. Kvitto 4 visar "Ladugårdsvägen 3" — för hon hade redan flyttat.

**Produkten byter namn:** Kvittoraderna visar "Lär dig Python" — för det var titeln vid köptillfället. Inget ändras i Kvittotabell när Produkt-tabellen uppdateras.

**Produkten raderas:** Kvittoraderna är text. Det finns ingen foreign key till Produkt. Raderingen blockeras inte och historiken är intakt.

**Kvittotabellen innehåller med flit redundans.** Det är inte ett fel — det är en funktion.

---

## Sedan knackar GDPR på dörren

Allt verkar löst. Men ett par år passerar och GDPR påminner om en grundläggande regel:

> **Artikel 5(1)(e) — lagringsbegränsning:** Personuppgifter ska inte lagras längre än vad som är nödvändigt för de ändamål för vilka de behandlas.

En kund som inte handlat på tre år ska kunna begära att bli bortglömd. Vi raderar dem från Kund-tabellen.

Men de finns kvar i Kvitto-tabellen. `KundNamn`, `KundAdress`, `KundEmail` — allt som kan identifiera en person, fryst i tid.

Ska vi radera kvittona också?

**Det beror på varför vi har dem.** Och det är den viktiga frågan.

### Vad behöver vi kvittot till?

Kvittot finns för att:
- Kunna hantera garantiärenden och reklamationer
- Uppfylla bokföringskrav (Sverige: 7 års arkivplikt för räkenskapsinformation)
- Ge kunden möjlighet att referera till ett köp via sitt eget ordernummer

För dessa ändamål behöver vi:
- Att köpet genomfördes — OrderId, datum, totalbelopp
- Vad som köptes och för hur mycket — Kvittorad
- Möjlighet att identifiera kunden _om de hör av sig med sitt kvitto_ — OrderId räcker som referens (kunden har det i sin email eller utskrift)

Vi behöver **inte**:
- Kundens namn sparad i kvittot (om kunden hör av sig ger de oss sitt ordernummer)
- Kundens adress (leveransen är avklarad)
- Kundens email (vi kan inte mejla en borttagen kund)

### Anonymisering — att ta bort det personliga utan att förstöra historiken

Vi raderar inte kvittoraderna. Vi **anonymiserar** personuppgifterna i dem:

**Kvitto efter anonymisering av Anna:**

| KvittoId | OrderId | KundNamn | KundAdress | KundEmail | Totalbelopp | KvittoDatum |
|---|---|---|---|---|---|---|
| 1 | 1 | [Borttagen kund] | NULL | NULL | 299 | 2024-01-15 |
| 2 | 2 | [Borttagen kund] | NULL | NULL | 998 | 2024-02-03 |

Transaktionen är dokumenterad. Beloppet, datumet och vad som köptes är intakt — det är vad bokföringen kräver. Men personuppgifterna är borta — det är vad GDPR kräver.

**Vad är "personuppgift"?**

Enligt GDPR är en personuppgift varje uppgift som direkt eller indirekt kan identifiera en fysisk person. Det inkluderar:
- Namn
- Adress
- E-postadress
- Kortnummer (även sista fyra siffror i kombination med namn)
- OrderId ensamt — nej, det är ett opakt tal utan kontext

Om Anna hör av sig med OrderId 1 och sitt fysiska kvitto kan vi matcha dem mot transaktionen och hjälpa med garantin — utan att vi behöver ha lagrat hennes namn i tre år extra.

### Den viktiga balansen

GDPR och bokföringskrav drar i olika riktningar:
- GDPR säger: radera personuppgifter när de inte längre behövs
- Bokföringslagen säger: bevara räkenskapsinformation i 7 år

Kvittolösningen med anonymisering löser spänningen: vi bevarar det ekonomiska faktumet (beloppet, vad som köptes, när), men raderar det personliga (vem som köpte det).

---

## Slutresultatet — databasdiagram

Klistra in koden på [dbdiagram.io](https://dbdiagram.io) för att se diagrammet visuellt.

```
Table Kund
{
  id integer [pk]
  namn string
  email string
  adress string
  stad string
  postnummer string
  visaSista4 string
}

Table Kategori
{
  id integer [pk]
  namn string
}

Table Produkt
{
  id integer [pk]
  namn string
  pris decimal
  kategoriId integer
  aktiv boolean
}

Table Order
{
  id integer [pk]
  kundId integer
  datum datetime
}

Table Orderrad
{
  id integer [pk]
  orderId integer
  produktId integer
  antal integer
  prisVidKop decimal
}

Table Kvitto
{
  id integer [pk]
  orderId integer
  kundNamn string
  kundAdress string
  kundEmail string
  totalbelopp decimal
  kvittoDatum datetime
}

Table Kvittorad
{
  id integer [pk]
  kvittoId integer
  produktNamn string
  kategori string
  antal integer
  enhetspris decimal
}

Ref: "Produkt"."kategoriId" > "Kategori"."id"
Ref: "Order"."kundId" > "Kund"."id"
Ref: "Orderrad"."orderId" > "Order"."id"
Ref: "Orderrad"."produktId" > "Produkt"."id"
Ref: "Kvitto"."orderId" > "Order"."id"
Ref: "Kvittorad"."kvittoId" > "Kvitto"."id"
```

---

## Vad kan vi nu fråga?

**Alla ordrar och totalsumma för kund 1:**

```sql
SELECT o.id AS OrderId, o.Datum, SUM(or2.PrisVidKop * or2.Antal) AS Totalt
FROM [Order] o
JOIN Orderrad or2 ON or2.OrderId = o.id
WHERE o.KundId = 1
GROUP BY o.id, o.Datum;
```

**Vad köpte en kund och vad kostade det när de köpte?**

```sql
SELECT p.Namn AS Produkt, or2.PrisVidKop, or2.Antal, o.Datum
FROM [Order] o
JOIN Orderrad or2 ON or2.OrderId = o.id
JOIN Produkt p ON or2.ProduktId = p.id
WHERE o.KundId = 1;
```

**Kvitto för order 1 — även om produkten raderats efteråt:**

```sql
SELECT k.KundNamn, k.KvittoDatum, kr.ProduktNamn, kr.Antal, kr.Enhetspris
FROM Kvitto k
JOIN Kvittorad kr ON kr.KvittoId = k.id
WHERE k.OrderId = 1;
```

**Hur mycket omsatte kategorin Bok det senaste halvåret?**

Vi kan fråga mot Orderrad (aktuella data) — inte Kvittorad (snapshot-data):

```sql
SELECT SUM(or2.PrisVidKop * or2.Antal) AS Omsattning
FROM Orderrad or2
JOIN Produkt p ON or2.ProduktId = p.id
JOIN Kategori kat ON p.KategoriId = kat.id
JOIN [Order] o ON or2.OrderId = o.id
WHERE kat.Namn = 'Bok'
  AND o.Datum >= DATEADD(month, -6, GETDATE());
```

---

## Sammanfattning

| Steg | Vad hände | Varför det matterats |
|---|---|---|
| 0 | En tabell med allt | Upprepning, inkonsistens, trasiga referenser |
| 1 | Kund bröts ut | En kund ska finnas på ett ställe — uppdateringsanomali eliminerad |
| 2 | Produkt + Kategori bröts ut | Transitiva beroenden borta — kategori tillhör produkt, inte order |
| 3 | Orderrad skapades med PrisVidKop | Priset vid köp lagras separat från nuvarande pris |
| — | Problem: normalisering förstör historiken | Kund byter adress → gamla ordrar visar ny adress |
| 4 | Kvitto och Kvittorad skapades | Snapshot av verkligheten vid köptillfället — ändras aldrig |
| — | Problem: GDPR kräver borttagning av kunder | Personuppgifter finns kvar i Kvitto |
| 5 | Anonymisering av kvitton vid GDPR-radering | Personuppgifter rensas, transaktionsdata bevaras för bokföring |

**Det centrala insikten:** Normalisering och historik är inte samma problem. Normalisering håller aktuell data konsekvent och utan upprepning. Historisk data behöver frysas vid skapelsetillfället — normalisering är fel verktyg för det. Kvittotabellen är lösningen på det andra problemet, inte ett undantag från det första.
