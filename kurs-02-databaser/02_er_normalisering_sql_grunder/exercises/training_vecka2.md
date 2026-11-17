# Träningsuppgifter: Databaser — Vecka 2

> **Tema:** SQL grunder — CRUD och relationsdatabaser  
> **Modul:** 02 — SQL hardcore

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad står CRUD för?

a. Create, Read, Update, Delete<br>b. Copy, Run, Update, Drop<br>c. Create, Remove, Use, Data<br>d. Connect, Read, Upload, Delete

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Create, Read, Update, Delete

  **Förklaringar:**

  - ✅ **a) Create, Read, Update, Delete** - **RÄTT**: De fyra grundoperationerna i alla databaser. I SQL: INSERT (Create), SELECT (Read), UPDATE (Update), DELETE (Delete)
  - ❌ **b) Copy, Run...** - FEL: Inget av dessa är SQL-kommandon
  - ❌ **c) Create, Remove...** - FEL: Remove är inte ett SQL-kommando (DELETE är det)
  - ❌ **d) Connect, Read...** - FEL: Connect är inte en CRUD-operation
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Hur lägger du till en ny rad i en tabell?

a. `ADD INTO Heroes VALUES ('Clark', 'Kent')`<br>b. `INSERT INTO Heroes (name, lastName) VALUES ('Clark', 'Kent')`<br>c. `PUT INTO Heroes ('Clark', 'Kent')`<br>d. `CREATE ROW Heroes ('Clark', 'Kent')`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `INSERT INTO Heroes (name, lastName) VALUES ('Clark', 'Kent')`

  **Förklaringar:**

  - ❌ **a) ADD INTO** - FEL: ADD används för att lägga till kolumner (ALTER TABLE ADD), inte rader
  - ✅ **b) INSERT INTO ... VALUES** - **RÄTT**: INSERT skapar en ny rad. Kolumnnamn efter tabell, värden i samma ordning efter VALUES. Text med enkelfnuttar
  - ❌ **c) PUT INTO** - FEL: PUT finns inte i SQL
  - ❌ **d) CREATE ROW** - FEL: CREATE används för databaser och tabeller, inte rader
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är `VARCHAR(50)`?

a. En funktion som räknar tecken<br>b. En datatyp för text med max 50 tecken<br>c. Ett tal med 50 decimaler<br>d. Ett unikt ID

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En datatyp för text med max 50 tecken

  **Förklaringar:**

  - ❌ **a) Räknar tecken** - FEL: Det är en datatyp, inte en funktion
  - ✅ **b) Text med max 50 tecken** - **RÄTT**: `VARCHAR` = Variable Character. 50 = maxlängd. Sparar bara platsen texten faktiskt använder, inte hela 50
  - ❌ **c) 50 decimaler** - FEL: För decimaltal används DECIMAL eller FLOAT
  - ❌ **d) Unikt ID** - FEL: För unika IDn används INTEGER PRIMARY KEY auto_increment
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad gör `ORDER BY ålder DESC`?

a. Sorterar i stigande ordning (yngst först)<br>b. Visar bara de över 18 år<br>c. Sorterar i fallande ordning (äldst först)<br>d. Väljer slumpmässig ordning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Sorterar i fallande ordning (äldst först)

  **Förklaringar:**

  - ❌ **a) Stigande** - FEL: `ASC` (ascending) ger stigande ordning. `DESC` är motsatsen
  - ❌ **b) Filtrerar ålder** - FEL: Det gör `WHERE`, inte `ORDER BY`
  - ✅ **c) Fallande (äldst först)** - **RÄTT**: `ORDER BY` sorterar resultatet. `DESC` = descending = fallande. Utan DESC sorteras det som standard i stigande ordning
  - ❌ **d) Slumpmässig** - FEL: ORDER BY sorterar deterministiskt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad blir resultatet av `SELECT COUNT(heroId) FROM Heroes`?

a. En lista på alla hjältar<br>b. Antalet rader i Heroes-tabellen<br>c. Hjälten med högst ID<br>d. Summan av alla ID-nummer

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Antalet rader i Heroes-tabellen

  **Förklaringar:**

  - ❌ **a) Lista på hjältar** - FEL: `SELECT *` ger listan. `COUNT()` ger bara antalet
  - ✅ **b) Antalet rader** - **RÄTT**: `COUNT()` är en aggregatfunktion som räknar antalet rader. `COUNT(*)` räknar alla, `COUNT(kolumn)` räknar rader där kolumnen inte är NULL
  - ❌ **c) Högst ID** - FEL: Det gör `MAX(heroId)`
  - ❌ **d) Summan** - FEL: Det gör `SUM(heroId)`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad händer om du kör `DELETE FROM Heroes` utan WHERE?

a. Ingenting — SQL kräver WHERE<br>b. Alla rader i tabellen raderas<br>c. Bara första raden raderas<br>d. Tabellen raderas

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Alla rader i tabellen raderas

  **Förklaringar:**

  - ❌ **a) Kräver WHERE** - FEL: SQL tillåter DELETE utan WHERE — och det är precis därför det är farligt
  - ✅ **b) Alla rader raderas** - **RÄTT**: `DELETE FROM Heroes` utan WHERE tar bort ALL data. Tabellen finns kvar men är tom. Använd ALLTID WHERE!
  - ❌ **c) Bara första** - FEL: Utan WHERE påverkas ALLA rader
  - ❌ **d) Tabellen raderas** - FEL: Tabellen raderas med `DROP TABLE Heroes`
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Hur skapar du en ny tabell?

a. `NEW TABLE Heroes (...)`<br>b. `ADD TABLE Heroes (...)`<br>c. `CREATE TABLE Heroes (...)`<br>d. `MAKE TABLE Heroes (...)`

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** `CREATE TABLE Heroes (...)`

  **Förklaringar:**

  - ❌ **a) NEW TABLE** - FEL: NEW finns inte i SQL
  - ❌ **b) ADD TABLE** - FEL: ADD används för kolumner, inte tabeller
  - ✅ **c) CREATE TABLE** - **RÄTT**: `CREATE TABLE Heroes (heroId INTEGER PRIMARY KEY, name VARCHAR(50), ...)` — inom parentes anges alla kolumner med datatyper
  - ❌ **d) MAKE TABLE** - FEL: MAKE finns inte i SQL
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad betyder `auto_increment`?

a. Databasen ökar automatiskt värdet med 1 för varje ny rad<br>b. Värdet ökar med 1 varje gång du läser det<br>c. Kolumnen kan innehålla flera värden<br>d. Den gör att talet alltid är jämnt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Databasen ökar automatiskt värdet med 1 för varje ny rad

  **Förklaringar:**

  - ✅ **a) Automatisk räknare** - **RÄTT**: Perfekt för Primary Key — du behöver aldrig ange IDt själv. Första raden får 1, andra 2, etc.
  - ❌ **b) Ökar vid läsning** - FEL: Ökar bara vid INSERT, inte SELECT
  - ❌ **c) Flera värden** - FEL: En kolumn har alltid ett värde per rad
  - ❌ **d) Jämnt tal** - FEL: Ingenting med jämna/udda tal att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad gör `UPDATE Heroes SET email = 'ny@mail.com' WHERE heroId = 1`?

a. Skapar en ny hjälte med email ny@mail.com<br>b. Ändrar email för hjälten med ID 1<br>c. Lägger till en kolumn för email<br>d. Tar bort hjälten med ID 1

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ändrar email för hjälten med ID 1

  **Förklaringar:**

  - ❌ **a) Skapar ny hjälte** - FEL: Det gör INSERT, inte UPDATE
  - ✅ **b) Ändrar befintlig rad** - **RÄTT**: UPDATE ändrar data i existerande rader. SET anger vilken kolumn som ska ändras och till vad. WHERE avgör vilken rad
  - ❌ **c) Lägger till kolumn** - FEL: Det gör `ALTER TABLE ... ADD COLUMN`
  - ❌ **d) Tar bort** - FEL: Det gör DELETE
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är `DISTINCT` bra för?

a. Att ta bort dubbletter från resultatet<br>b. Att göra sökningen snabbare<br>c. Att sortera i unik ordning<br>d. Att skapa unika index

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att ta bort dubbletter från resultatet

  **Förklaringar:**

  - ✅ **a) Ta bort dubbletter** - **RÄTT**: `SELECT DISTINCT name FROM Heroes` — om tre hjältar heter Bruce får du bara Bruce en gång
  - ❌ **b) Snabbare** - FEL: DISTINCT kan göra sökningen långsammare (måste jämföra rader)
  - ❌ **c) Sortera unikt** - FEL: Sortering är ORDER BY. DISTINCT tar bara bort dubbletter
  - ❌ **d) Skapa index** - FEL: Index skapas med CREATE INDEX
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Vad gör en Foreign Key i en tabell?

a. Gör att kolumnen måste vara unik<br>b. Skapar en länk till en Primary Key i en annan tabell och hindrar ogiltiga värden<br>c. Gör att tabellen inte kan raderas<br>d. Automatiskt ifyllnad av data

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skapar en länk till en Primary Key i en annan tabell och hindrar ogiltiga värden

  **Förklaringar:**

  - ❌ **a) Unik kolumn** - FEL: Det är UNIQUE-constraint. FK kan ha dubbletter (flera ordrar kan ha samma CustomerId)
  - ✅ **b) Länk till PK + skydd** - **RÄTT**: `FOREIGN KEY (heroId) REFERENCES Heroes(heroId)` — du kan inte sätta heroId = 999 om ingen hjälte har det IDt. Skyddar dataintegriteten
  - ❌ **c) Förhindrar radering** - FEL: FK förhindrar radering AV REFERERAD data, inte själva tabellen
  - ❌ **d) Auto-fyllnad** - FEL: Det gör auto_increment/default values
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad innebär NOT NULL i en kolumn?

a. Kolumnen får vara tom<br>b. Kolumnen måste ha ett värde — NULL är inte tillåtet<br>c. Kolumnen kan bara innehålla tal<br>d. Kolumnen raderas om den är tom

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kolumnen måste ha ett värde — NULL är inte tillåtet

  **Förklaringar:**

  - ❌ **a) Får vara tom** - FEL: Det är tvärtom — `NOT NULL` betyder att den INTE får vara tom
  - ✅ **b) Måste ha ett värde** - **RÄTT**: `name VARCHAR(50) NOT NULL` — du kan inte skapa en hjälte utan namn. Databasen nekar INSERT om name saknas
  - ❌ **c) Bara tal** - FEL: NOT NULL handlar om NULL vs icke-NULL, inte datatyp
  - ❌ **d) Radera om tom** - FEL: Databasen säger bara nej vid INSERT/UPDATE, den raderar inte automatiskt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
