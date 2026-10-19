# Tentafrågor — Intro Databaser

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Vad är en databas?

Vad stämmer bäst på en databas?

A) En organiserad samling data som kan sökas, sorteras och uppdateras
B) Ett program för att skapa kalkylblad med makrofunktioner
C) En textfil med kommaseparerade värden (CSV)
D) En hård disk med extra snabbt minne 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. En databas är en organiserad samling strukturerad data som lagras elektroniskt och kan sökas och uppdateras effektivt.
**B)** Det är ett kalkylprogram (Excel/Google Sheets), inte en databas.
**C)** CSV är ett filformat — enkelt, men saknar sökfunktioner, relationer och transaktioner.
**D)** Hård disk med snabbt minne är hårdvara — inte ett databassystem.

</details>

---

## Fråga 2 — DBMS

Vad är ett DBMS?

B) En programvara som hanterar databasen och kommunicerar med applikationen
A) En specifik SQL-syntax som används i MySQL
C) En fil på disken där tabeller lagras
D) Ett granskningsverktyg för att kontrollera att SQL-koden är korrekt 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. DBMS (Database Management System) är programvaran som hanterar databasen — du pratar aldrig direkt med filerna, alltid via DBMS:et.
**A)** SQL-syntax tillhör SQL-standarden, inte ett DBMS specifikt.
**C)** Databasfilen är vad DBMS:et hanterar — det är inte DBMS:et självt.
**D)** Det låter rimligt, men DBMS hanterar data, inte kodgranskning.

</details>

---

## Fråga 3 — Primary Key

Vad är en Primary Key?

C) En kolumn som unikt identifierar varje rad i tabellen och aldrig kan vara NULL
A) En kolumn som lagrar krypterade lösenord för inloggning
B) Den första kolumnen i tabellen, oavsett vad den innehåller
D) En speciell nyckel på tangentbordet för att öppna databasen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. Primary Key identifierar varje rad unikt — inga dubbletter, aldrig NULL.
**A)** Lösenord lagras inte i Primary Key-kolumner — det vore ett allvarligt säkerhetsproblem.
**B)** Det är inte positionen i tabellen som avgör — det är en explicit deklaration.
**D)** Det är inte en fysisk nyckel.

</details>

---

## Fråga 4 — Foreign Key

Vad gör en Foreign Key?

A) Den pekar på Primary Key i en annan tabell och skapar en relation
C) Den krypterar data i tabellen för extra säkerhet
B) Den förhindrar alla INSERT-operationer om tabellen är full
D) Den låser tabellen när en annan användare arbetar i den 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Foreign Key refererar till Primary Key i en annan tabell och garanterar referensintegritet.
**C)** Kryptering är en separat funktion, inte vad Foreign Key gör.
**B)** Foreign Key handlar om relationer, inte om tabellens storlek.
**D)** Låsning av tabeller hanteras av transaktioner, inte av Foreign Key.

</details>

---

## Fråga 5 — CRUD

Vad står CRUD för?

D) Create, Read, Update, Delete — de fyra grundoperationerna mot data
A) Connect, Run, Use, Deploy — stegen för att sätta upp en databas
B) Copy, Repeat, Undo, Delete — tangentbordsgenvägar i SQL-klienten
C) Customer, Record, User, Database — vanliga tabellnamn i affärssystem 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: D**

**D)** Rätt. CRUD = Create (INSERT), Read (SELECT), Update (UPDATE), Delete (DELETE). Alla system du bygger gör CRUD.
**A)** Det är en påhittad förkortning.
**B)** Också påhittad.
**C)** Rolig, men fel.

</details>

---

## Fråga 6 — SQLite vs MySQL

Vad skiljer SQLite från MySQL?

B) SQLite lagrar hela databasen i en enda fil och kräver ingen server, MySQL kräver en server
A) SQLite använder NoSQL, MySQL använder SQL
C) SQLite är snabbare än MySQL för alla typer av applikationer
D) SQLite är gjord av Google och MySQL av Microsoft 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. SQLite är filbaserad och serverless — perfekt för appar och utveckling. MySQL kräver en serverprocess och hanterar många samtidiga användare bättre.
**A)** Båda använder SQL — SQLite är ett RDBMS precis som MySQL.
**C)** MySQL och PostgreSQL är generellt snabbare vid hög belastning med många samtidiga användare.
**D)** SQLite skapades av D. Richard Hipp, MySQL av MySQL AB (numera Oracle).

</details>

---

## Fråga 7 — Tabell och rad

Vad är en rad (record/row) i en databas?

A) En enstaka post i tabellen — en konkret instans av det tabellen beskriver
B) En horisontell lista över vilka kolumner som finns i tabellen
C) En SQL-fråga som returnerar data från tabellen
D) En kommaseparerad textrad i en CSV-exportfil 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. En rad = en post = en konkret instans. I Customer-tabellen är varje kund en rad.
**B)** Det beskriver tabellens schema (kolumndefinitionerna), inte en rad.
**C)** Det är en query, inte en rad.
**D)** Det är ett filformat, inte ett databaskoncept.

</details>

---

## Fråga 8 — SQL

Vad är SQL?

C) Standardspråket för att kommunicera med en relationsdatabas
A) Ett programmeringsspråk för att bygga webbapplikationer
B) En typ av databas utan tabeller, likt MongoDB
D) Det hemliga lösenordet till alla MySQL-databaser 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**C)** Rätt. SQL = Structured Query Language. Du SELECT:ar, INSERT:ar, UPDATE:ar och DELETE:ar med det.
**A)** SQL är inte ett webbprogrammeringsspråk — det är ett frågespråk för databaser.
**B)** SQL används i relationsdatabaser med tabeller — inte i NoSQL-system.
**D)** Nej.

</details>

---

## Fråga 9 — RDBMS

Vilket av följande är ett RDBMS?

B) PostgreSQL
A) MongoDB
C) Redis
D) Excel (med VBA-makron aktiverade) 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**B)** Rätt. PostgreSQL är ett RDBMS — data lagras i tabeller med definierade relationer.
**A)** MongoDB är en dokumentdatabas (NoSQL), inte ett RDBMS.
**C)** Redis är en key-value-databas (NoSQL).
**D)** Excel med makron är fortfarande ett kalkylprogram.

</details>

---

## Fråga 10 — Kolumn och datatyp

Vad bestämmer en kolumns datatyp?

A) Vilken typ av värden som får lagras i kolumnen — t.ex. INT, VARCHAR, DATE
C) Hur bred kolumnen visas i SQL-klienten
B) Ordningen kolumnen hamnar i när du gör SELECT *
D) Hur stor hårddisk-plats som reserveras per bokstav i kolumnnamnet 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Datatypen avgör vad som får lagras: `INT` för heltal, `VARCHAR(100)` för text upp till 100 tecken, `DATE` för datum, `DECIMAL` för decimaltal.
**C)** Visningsbredd i klienten är inställning i klienten, inte i datatypen.
**B)** SELECT * returnerar kolumner i den ordning de definierades i tabellen — det har inget med datatypen att göra.
**D)** Kolumnnamnet tar inte upp plats i datalagringen.

</details>
