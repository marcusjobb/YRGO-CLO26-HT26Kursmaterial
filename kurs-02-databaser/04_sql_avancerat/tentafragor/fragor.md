# Tentafrågor — SQL Avancerat

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Subquery

Vad gör en subquery?

A) En SELECT-sats inuti en annan SELECT-sats — innerfrågan körs först och resultatet används av ytterfrågan
B) En fråga som körs automatiskt vid varje INSERT
C) En sparad fråga som körs på ett schema, likt ett cronjob
D) En fråga som dubblerar en tabell för testning 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Subquery = en fråga inuti en fråga. Klassiskt exempel: `WHERE Price > (SELECT AVG(Price) FROM Product)`.
**B)** Det beskriver en Trigger, inte en subquery.
**C)** Det beskriver ett schemalagt jobb, inte en subquery.
**D)** Det är inte vad en subquery gör.

</details>

---

## Fråga 2 — CTE

Vad är en CTE och hur definieras den?

C) En tillfällig, namngiven resultatmängd som definieras med `WITH namn AS (...)` och bara existerar under en query
A) En permanent vy som sparas i databasen med `CREATE VIEW`
B) En tabell som skapas automatiskt av databasen vid komplexa JOIN-operationer
D) En metod för att komprimera stora queries till en rad 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. `WITH AktiveKunder AS (SELECT ...) SELECT * FROM AktiveKunder` — CTE:n lever bara för den queryn.
**A)** En vy skapas med `CREATE VIEW` och är permanent — CTE är tillfällig.
**B)** Databasen skapar inte automatiska tabeller vid JOIN.
**D)** CTE gör kod mer läslig, men inte kortare.

</details>

---

## Fråga 3 — Transaktion

Vad är en transaktion?

B) En grupp SQL-operationer som behandlas som en odelbar enhet — antingen lyckas ALLA eller rullar ALLA tillbaka
A) En enda SELECT-sats som returnerar data
C) En automatisk säkerhetskopia som körs varje gång data ändras
D) En betalning i ett e-handelssystem 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. START TRANSACTION → operationer → COMMIT (eller ROLLBACK om något gick fel). Klassiskt exempel: banköverföring.
**A)** En SELECT är en enstaka operation, inte en transaktion.
**C)** Transaktioner handlar om atomicitet, inte säkerhetskopior.
**D)** Det är en affärsmässig transaktion — databasbegreppet är vidare.

</details>

---

## Fråga 4 — ACID

Vad står A i ACID för?

A) Atomicity — transaktionen är odelbar: antingen lyckas allt eller misslyckas allt
C) Authorization — transaktionen kontrollerar behörigheter
B) Availability — databasen är alltid tillgänglig
D) Automatic — transaktionen startar automatiskt vid varje query 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. ACID = Atomicity, Consistency, Isolation, Durability. Atomicity = allt eller inget.
**C)** Authorization är en säkerhetsfunktion, inte en ACID-egenskap.
**B)** Availability tillhör CAP-teoremet, inte ACID.
**D)** Transaktioner startar med START TRANSACTION, inte automatiskt.

</details>

---

## Fråga 5 — View (vy)

Vad är en vy (VIEW)?

D) En sparad SELECT-sats som ser ut som en tabell — data lagras inte i vyn, bara frågan
A) En kopia av tabelldata som uppdateras var 10:e minut
B) En tabell med inbyggd kryptering för känsliga kolumner
C) En sorterad version av en tabell, sparad för snabbare åtkomst 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**D)** Rätt. En vy är bara en sparad fråga. Varje gång du frågar vyn körs den underliggande SELECT-satsen.
**A)** Det beskriver en materialiserad vy (som inte alla databaser stöder) — vanliga vyer lagrar ingen data.
**B)** Kryptering är en separat funktion, inte vad en vy gör.
**C)** En vy sorterar inte data automatiskt utan att du anger ORDER BY.

</details>

---

## Fråga 6 — Stored Procedure

Varför använder man en Stored Procedure?

B) För att spara och återanvända en samling SQL-satser med ett namn, köra dem med CALL och eventuellt ta parametrar
A) För att permanent blockera en tabell för andra användare
C) För att automatiskt skapa backuper när data läggs till
D) För att kunna skriva SQL i Python istället för i databasen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. Stored Procedures är databasfunktioner: `CREATE PROCEDURE GetProducts(IN catId INT) BEGIN ... END;` → `CALL GetProducts(3);`
**A)** Stored Procedures låser inte tabeller — det hanteras av transaktioner.
**C)** Det beskriver en Trigger, inte en Stored Procedure.
**D)** Stored Procedures skrivs i SQL (eller PL/SQL etc.), inte i Python.

</details>

---

## Fråga 7 — Trigger

Vad är en Trigger?

A) En automatisk handling som körs när en specifik händelse sker i databasen — INSERT, UPDATE eller DELETE på en tabell
B) En funktion som körs manuellt av DBA:n vid behov
C) Ett schemalagt jobb som körs varje natt
D) En knapp i SQL-klienten som kör den senaste frågan igen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. En Trigger är ett automatiskt svar på en databashändelse. Exempel: logga varje prisändring med AFTER UPDATE ON Product.
**B)** Triggers körs automatiskt, inte manuellt.
**C)** Det beskriver ett schemalagt jobb (t.ex. cron eller SQL Server Agent).
**D)** Nej.

</details>

---

## Fråga 8 — Window Function

Vad skiljer en window function från GROUP BY?

C) Window function beräknar ett värde per rad utan att kollapsa resultatet. GROUP BY kollapsar rader till en rad per grupp
A) Window functions är snabbare än GROUP BY på stora tabeller
B) Window functions kan bara användas med COUNT och SUM
D) GROUP BY är en äldre syntax och ersätts av OVER-klausulen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Med GROUP BY + COUNT försvinner de individuella raderna — du ser bara en rad per grupp. Med RANK() OVER (PARTITION BY ...) behåller varje rad sin identitet och får dessutom ett beräknat värde.
**A)** Prestanda är inte den principiella skillnaden.
**B)** Window functions fungerar med SUM, AVG, RANK, ROW_NUMBER, LAG, LEAD och fler.
**D)** GROUP BY och OVER-klausulen löser olika problem — de ersätter inte varandra.

</details>

---

## Fråga 9 — Index

När bör du INTE skapa ett index?

B) På en Boolean-kolumn (t.ex. IsActive med bara true/false) — index hjälper lite när antalet unika värden är litet
A) På en kolumn du söker på ofta i WHERE
C) På en Foreign Key-kolumn
D) På kolumner du JOIN:ar på 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. Index är effektiva när de har hög kardinalitet (många unika värden). En Boolean-kolumn har bara 2 möjliga värden — databasen kan lika gärna scanna hela tabellen.
**A)** Kolumner i WHERE är klassiska kandidater för index.
**C)** Foreign Keys bör ofta indexeras för att JOIN-operationer ska gå snabbt.
**D)** JOIN-kolumner är bra kandidater för index.

</details>

---

## Fråga 10 — ER-diagram

Vad representerar ett ER-diagram?

A) En visuell bild av databasens tabeller, kolumner och relationer mellan dem
B) En lista på alla SQL-queries som körts mot databasen
C) En graf över databasprestanda över tid
D) En bild som visar hur data rör sig mellan server och klient 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Entity Relationship Diagram visar entiteter (tabeller), deras attribut (kolumner) och hur de är kopplade (1-till-många, många-till-många etc.).
**B)** Det är en query-logg, inte ett ER-diagram.
**C)** Det är ett prestandadiagram, inte ett ER-diagram.
**D)** Det är ett nätverksdiagram eller sekvensdiagram, inte ett ER-diagram.

</details>
