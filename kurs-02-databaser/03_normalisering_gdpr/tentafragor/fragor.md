# Tentafrågor — Normalisering och GDPR

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Vad är normalisering?

Vad är syftet med normalisering?

B) Att strukturera databasen för att minimera onödig duplicering och minska risken för inkonsekvenser
A) Att komprimera databasen för snabbare lagring
C) Att omvandla alla textvärden till gemener
D) Att säkerställa att alla tabeller har exakt lika många rader 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. Normalisering handlar om att organisera data logiskt — ett faktum på ett ställe, inga onödiga kopior.
**A)** Komprimering är en lagringsteknisk fråga, inte normalisering.
**C)** Gemener/versaler är formateringsfrågor, inget med normalisering att göra.
**D)** Antal rader per tabell styrs av data, inte av normalisering.

</details>

---

## Fråga 2 — 1NF

Vilket av dessa bryter mot 1NF?

A) En Products-cell innehåller värdet "Kaffe, Te, Juice" (kommaseparerad lista)
C) En tabell har en kolumn med NULL-värden
B) Två rader i tabellen har samma pris
D) Tabellen saknar en Primary Key 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. 1NF kräver atomära värden — en sak per cell. En kommaseparerad lista i en cell bryter mot det. Lösningen är separata rader.
**C)** NULL-värden är tillåtna i SQL (om kolumnen tillåter det) och bryter inte mot 1NF.
**B)** Dubbletter i icke-nyckelkolumner är OK — det bryter inte mot 1NF.
**D)** Saknad Primary Key är ett designproblem men tekniskt sett inte ett brott mot 1NF.

</details>

---

## Fråga 3 — 2NF

När är 2NF relevant?

C) När tabellen har en sammansatt primärnyckel (av flera kolumner) och en kolumn beror bara på en del av nyckeln
A) Alltid — alla tabeller måste uppfylla 2NF oavsett nyckelstruktur
B) När tabellen har fler än 10 kolumner
D) När det finns fler än en Foreign Key i tabellen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. 2NF är bara relevant när primärnyckeln är sammansatt. Om nyckeln är enkel (en enda kolumn) uppfylls alltid 2NF automatiskt.
**A)** Tabeller med enkel primärnyckel behöver inte analyseras för 2NF.
**B)** Antal kolumner avgör inte 2NF.
**D)** Antal Foreign Keys avgör inte 2NF.

</details>

---

## Fråga 4 — 3NF

Vad är ett transitivt beroende?

B) När kolumn C beror på kolumn B, och B beror på primärnyckeln A — så att C beror indirekt på A via B
A) När två tabeller har Foreign Key-koppling till varandra ömsesidigt
C) När en kolumn kan ha NULL-värden i vissa rader men inte i andra
D) När samma data läggs in i databasen av två användare samtidigt 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. Transitivt beroende: A → B → C. Klassiskt exempel: CustomerId → ZipCode → City. Lösning: flytta ZipCode+City till en egen tabell.
**A)** Det är ett cirkulärt beroende, inte ett transitivt.
**C)** Det beskriver NULL-hantering, inte beroenden.
**D)** Det är ett concurrency-problem, inte ett normaliseringsproblem.

</details>

---

## Fråga 5 — Redundans

En databas lagrar stadsnamnet "Göteborg" i varje kundrad istället för i en separat postnummertabell. Vad kallas detta?

A) Redundans — onödig duplicering av samma data på flera ställen
B) Denormalisering — ett medvetet val för ökad prestanda
C) Kaskaduppdatering — en teknik för att hålla data synkroniserat
D) Index-uppblåsning — ett prestandaproblem i sökmotorn 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Redundans = samma faktum på flera ställen. Om Göteborg byter namn (teoretiskt) måste du uppdatera tusentals rader.
**B)** Denormalisering är ett *medvetet* beslut med motivering — slumpmässig duplicering är bara redundans.
**C)** Kaskaduppdatering är en funktion för Foreign Keys, inte ett namn på problemet.
**D)** Index-uppblåsning är ett eget prestandaproblem och inte kopplat till detta.

</details>

---

## Fråga 6 — GDPR: vad är en personuppgift?

Vilket av följande är INTE en personuppgift enligt GDPR?

C) Produktpriset 29,90 kr för ett kaffemärke
A) En persons IP-adress
B) En persons foto
D) En persons personnummer 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Produktpriset är information om en produkt, inte om en person — det är alltså inte en personuppgift.
**A)** IP-adress är en personuppgift eftersom den kan kopplas till en specifik person eller enhet.
**B)** Foto är en personuppgift — det kan identifiera en person direkt.
**D)** Personnummer är en direkt identifierare och en klassisk personuppgift.

</details>

---

## Fråga 7 — GDPR: raderingsrätten

En kund begär radering av sina uppgifter. Vad räcker det att göra?

B) Det räcker inte att ta bort kunden ur Users-tabellen — du måste hitta och radera/pseudonymisera all deras data i hela systemet
A) Ta bort deras rad ur Users-tabellen så är du klar
C) Anonymisera deras e-postadress genom att byta ut @ mot [at]
D) Skicka ett mejl som bekräftar att du noterat begäran — det uppfyller GDPR 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. Raderingsrätten kräver att DU hittar all data om personen — orders, loggar, backuper, analytiksystem. Det räcker inte att ta bort en rad i en tabell.
**A)** Data om personen finns troligen på fler ställen: ordrar, loggar, nyhetsbrev, etc.
**C)** `anna[at]example.com` är fortfarande en personuppgift — det är inte pseudonymisering eller anonymisering.
**D)** En bekräftelse är inte radering — du måste faktiskt radera.

</details>

---

## Fråga 8 — Dataminimering

Vad innebär dataminimering under GDPR?

A) Du får bara samla in den data du faktiskt behöver för ditt angivna syfte
C) Du ska komprimera data för att spara lagringsutrymme
B) Du ska begränsa antalet rader i en tabell för att hålla databasen liten
D) Du ska dölja känslig data bakom kryptering i alla kolumner 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Dataminimering = samla bara in det du behöver. Behöver din webbutik verkligen kundens födelsedag för att skicka ett paket?
**C)** Komprimering är ett tekniskt lagringskoncept, inte GDPR-dataminimering.
**B)** Antal rader är inte vad GDPR-dataminimering handlar om.
**D)** Kryptering skyddar data men är inte dataminimering.

</details>

---

## Fråga 9 — Samtycke

Vilket av dessa är ett giltigt samtycke enligt GDPR?

B) En förinbockad ruta som användaren aktivt måste bocka ur för att avböja
A) En förbockad ruta som användaren aktivt bockar i för att acceptera
C) En text i användarvillkoren som säger att användaren accepterar genom att besöka webbplatsen
D) En pop-up med texten "Genom att andas godkänner du vår integritetspolicy" 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Samtycke måste vara ett aktivt, frivilligt val. Att aktivt bocka i en ruta = giltigt samtycke.
**B)** Förinbockad ruta är INTE giltigt samtycke — personen har inte aktivt valt.
**C)** Att bara besöka en webbplats räknas inte som samtycke till databehandling.
**D)** ... nej.

</details>

---

## Fråga 10 — Pseudonymisering vs anonymisering

Vad är skillnaden mellan pseudonymisering och anonymisering?

C) Pseudonymiserad data kan kopplas tillbaka till en person via en separat nyckel. Anonymiserad data kan det inte
A) Pseudonymisering krypterar data, anonymisering tar bort den
B) De är synonymer — GDPR behandlar dem identiskt
D) Anonymisering är tillåtet, pseudonymisering är förbjudet under GDPR 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Pseudonymisering: data ersätts med alias men kopplingen kan återupprättas. Det är fortfarande personuppgifter under GDPR. Anonymisering: kopplingen är permanent bruten — inte längre personuppgifter.
**A)** Kryptering är en teknik — pseudonymisering handlar om att ersätta identifierare.
**B)** GDPR behandlar dem olika — pseudonymiserad data omfattas fortfarande av GDPR.
**D)** Båda är tillåtna och uppmuntras av GDPR.

</details>
