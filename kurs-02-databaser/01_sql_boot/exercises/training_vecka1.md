# Träningsuppgifter: Databaser — Vecka 1

> **Tema:** Introduktion databaser, terminologi, UML och databasdesign  
> **Moduler:** 01 — Intro databaser, 07 — UML och design

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan en databas och Excel?

a. De är samma sak — båda lagrar data i tabeller<br>
b. Excel fungerar för miljontals rader, databaser bara för hundratals<br>
c. En databas hanterar miljontals rader, flera användare samtidigt, komplexa sökningar och relationer mellan tabeller<br>
d. Excel kan koppla ihop tabeller, databaser kan inte

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En databas hanterar miljontals rader, flera användare samtidigt, komplexa sökningar och relationer mellan tabeller

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Båda lagrar data, men en databas är designad för mycket större datamängder och samtidiga användare
  - ❌ **b) Omvänt** - FEL: Det är tvärtom — Excel blir långsam vid tusentals rader, databaser hanterar miljontals
  - ✅ **c) Databasen är kraftfullare** - **RÄTT**: Databaser är byggda för storskalighet — SQL-frågor, JOINs, transaktioner, ACID, och flera användare samtidigt
  - ❌ **d) Excel kan koppla ihop** - FEL: Excel har begränsad relationshantering (t.ex. VLOOKUP) men långt ifrån en relationsdatabas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad kallas varje rad i en databastabell?

a. En kolumn<br>b. En post eller rad<br>c. En databas<br>d. En relation

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En post eller rad

  **Förklaringar:**

  - ❌ **a) En kolumn** - FEL: Kolumn är en egenskap (t.ex. namn, ålder). Det är raderna som innehåller data
  - ✅ **b) En post eller rad** - **RÄTT**: I en tabell representerar varje rad en post — en person, en order, en produkt. Varje kolumn är en egenskap hos den posten
  - ❌ **c) En databas** - FEL: Databasen är hela systemet med alla tabeller
  - ❌ **d) En relation** - FEL: Relation är kopplingen mellan tabeller, inte en rad i en tabell
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Hur lång tid tar det att söka igenom 400 000 personer utan databas?

a. Några minuter<br>b. Cirka 46 dagar<br>c. 1 timme<br>d. 1 sekund

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Cirka 46 dagar

  **Förklaringar:**

  - ❌ **a) Några minuter** - FEL: Med pappersarkiv tar varje mapp ca 10 sekunder att bläddra igenom
  - ✅ **b) 46 dagar** - **RÄTT**: 400 000 × 10 sekunder = 4 000 000 sekunder = ~46 dagar. Med SQL tar samma sökning < 1 sekund
  - ❌ **c) 1 timme** - FEL: Med papper tar det veckor
  - ❌ **d) 1 sekund** - FEL: Det är vad en databas klarar, inte papper
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad betyder SQL?

a. Simple Query Language<br>b. Structured Query Language<br>c. System Query Library<br>d. Standard Question Language

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Structured Query Language

  **Förklaringar:**

  - ❌ **a) Simple** - FEL: Nära men fel. SQL är strukturerat, inte bara "enkelt"
  - ✅ **b) Structured Query Language** - **RÄTT**: SQL är standardiserat frågespråk för relationsdatabaser. Uttalas "ess-queue-ell" eller "sequel"
  - ❌ **c) System Query Library** - FEL: Det är inte ett bibliotek, det är ett språk
  - ❌ **d) Standard Question Language** - FEL: Heter "Query" (fråga), inte "Question"
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vilken SQL-sats använder du för att hämta data från en tabell?

a. INSERT<br>b. SELECT<br>c. UPDATE<br>d. DELETE

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SELECT

  **Förklaringar:**

  - ❌ **a) INSERT** - FEL: INSERT lägger till ny data, den hämtar inte
  - ✅ **b) SELECT** - **RÄTT**: `SELECT * FROM personer` — den vanligaste SQL-satsen. `*` betyder "alla kolumner"
  - ❌ **c) UPDATE** - FEL: UPDATE ändrar befintlig data
  - ❌ **d) DELETE** - FEL: DELETE tar bort data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad gör `WHERE` i en SQL-fråga?

a. Sorterar resultatet<br>b. Filtrerar vilka rader som ska visas<br>c. Kopplar ihop två tabeller<br>d. Räknar antalet rader

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Filtrerar vilka rader som ska visas

  **Förklaringar:**

  - ❌ **a) Sorterar** - FEL: Det gör `ORDER BY`
  - ✅ **b) Filtrerar** - **RÄTT**: `SELECT * FROM personer WHERE stad = 'Göteborg'` — bara personer i Göteborg visas
  - ❌ **c) Kopplar ihop** - FEL: Det gör `JOIN`
  - ❌ **d) Räknar** - FEL: Det gör `COUNT()`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en Primary Key (primärnyckel)?

a. Den vanligaste kolumnen i tabellen<br>b. En unik identifierare för varje rad i en tabell<br>c. En kolumn som får innehålla NULL-värden<br>d. Ett index som gör sökningar snabbare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En unik identifierare för varje rad i en tabell

  **Förklaringar:**

  - ❌ **a) Vanligaste kolumnen** - FEL: Primary Key handlar om unikhet, inte popularitet
  - ✅ **b) Unik identifierare** - **RÄTT**: Varje rad har ett unikt ID — t.ex. `personId` 1, 2, 3. Det gör att du alltid kan hitta exakt rätt rad
  - ❌ **c) Får innehålla NULL** - FEL: En Primary Key kan ALDRIG vara NULL — den måste alltid ha ett värde
  - ❌ **d) Ett index** - FEL: En PK skapar automatiskt ett index, men själva begreppet är "unik identifierare"
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är en Foreign Key (främmande nyckel)?

a. Samma sak som Primary Key<br>b. En kolumn som refererar till en Primary Key i en annan tabell<br>c. En kolumn som innehåller utländsk data<br>d. Ett sätt att radera data

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En kolumn som refererar till en Primary Key i en annan tabell

  **Förklaringar:**

  - ❌ **a) Samma som PK** - FEL: PK är unik för tabellen, FK refererar till en annan tabells PK
  - ✅ **b) Referens till annan tabell** - **RÄTT**: I en tabell Order kan `CustomerId` vara Foreign Key som pekar på `Customer.Id`. Det är så relationer skapas
  - ❌ **c) Utländsk data** - FEL: Ingenting med länder att göra — "foreign" betyder "från en annan tabell"
  - ❌ **d) Radera data** - FEL: FK förhindrar faktiskt oavsiktlig radering (restriction)
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad kallas det när du ritar upp tabeller och deras relationer i ett diagram?

a. Ett flödesschema<br>b. Ett UML-klassdiagram för databaser<br>c. Ett Gantt-schema<br>d. En mockup

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett UML-klassdiagram för databaser

  **Förklaringar:**

  - ❌ **a) Flödesschema** - FEL: Flödesscheman visar processer och steg, inte datastrukturer
  - ✅ **b) UML-klassdiagram** - **RÄTT**: UML-klassdiagram visar tabeller som klasser med attribut, och streck mellan dem visar relationer (1 till många, etc.)
  - ❌ **c) Gantt-schema** - FEL: Gantt är för tidsplanering av projekt
  - ❌ **d) Mockup** - FEL: Mockup är för användargränssnitt, inte databaser
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är skillnaden mellan relationsdatabaser och dokumentdatabaser?

a. Relationsdatabaser använder tabeller, dokumentdatabaser använder JSON-liknande dokument<br>
b. De är samma sak<br>
c. Dokumentdatabaser använder SQL, relationsdatabaser gör inte det<br>
d. Relationsdatabaser är alltid snabbare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Relationsdatabaser använder tabeller, dokumentdatabaser använder JSON-liknande dokument

  **Förklaringar:**

  - ✅ **a) Tabeller vs dokument** - **RÄTT**: SQL-databaser (MySQL, SQLite) använder tabeller med rader/kolumner. MongoDB (dokumentdatabas) använder JSON-liknande dokument. Olika verktyg för olika behov
  - ❌ **b) Samma sak** - FEL: Olika sätt att lagra och strukturera data
  - ❌ **c) Omvänt** - FEL: Relationsdatabaser använder SQL. MongoDB använder inte SQL utan ett eget frågespråk
  - ❌ **d) Alltid snabbare** - FEL: Beror på användningsområde — dokumentdatabaser kan vara snabbare för vissa typer av data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
