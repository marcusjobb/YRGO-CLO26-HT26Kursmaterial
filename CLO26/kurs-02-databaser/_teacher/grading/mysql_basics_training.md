# Träningsuppgifter: Training: MySQL Basics

🟢


## Instruktioner

Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt. 
Klicka på 'Visa svar' för att se det rätta svaret och förklaringar för alla alternativ.

### Fråga 1

Vad är MySQL?

a. Ett programmeringsspråk<br>
b. Ett relationsdatabassystem (RDBMS) som använder SQL<br>
c. My Structured Query Language - mitt eget SQL!<br>
d. En ORM för .NET<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett relationsdatabassystem (RDBMS) som använder SQL


  **Förklaringar:**

  - ❌ **a) Ett programmeringsspråk** - FEL: MySQL är ett databassystem, inte ett språk
  - ✅ **b) Ett relationsdatabassystem (RDBMS) som använder SQL** - **RÄTT**: MySQL är en open-source relationsdatabas som ägs av Oracle
  - ❌ **c) My Structured Query Language - mitt eget SQL!** - FEL: MySQL används av WordPress, Facebook, Twitter och miljontals webbplatser!
  - ❌ **d) En ORM för .NET** - FEL: MySQL är en databas, inte en ORM
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är skillnaden mellan SQLite och MySQL?

a. Ingen skillnad<br>
b. SQLite är en fil, MySQL är en server med klient-server-arkitektur<br>
c. SQLite är snabbare<br>
d. SQLite är för stora system, MySQL för små!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SQLite är en fil, MySQL är en server med klient-server-arkitektur


  **Förklaringar:**

  - ❌ **a) Ingen skillnad** - FEL: Stor skillnad i arkitektur och användningsområden
  - ✅ **b) SQLite är en fil, MySQL är en server med klient-server-arkitektur** - **RÄTT**: SQLite perfekt för utveckling, MySQL för produktion med många användare
  - ❌ **c) SQLite är snabbare** - FEL: MySQL hanterar concurrent users bättre
  - ❌ **d) SQLite är för stora system, MySQL för små!** - FEL: Tvärtom! MySQL är produktionsdatabas, SQLite är utvecklingsdatabas!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vilken standard MySQL-port används?

a. 3000<br>
b. 1433<br>
c. 3306<br>
d. 8080 - som alla webbservrar!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 3306


  **Förklaringar:**

  - ❌ **a) 3000** - FEL: 3000 är vanlig för Node.js-appar, inte MySQL
  - ❌ **b) 1433** - FEL: 1433 är SQL Server:s port
  - ✅ **c) 3306** - **RÄTT**: MySQL lyssnar på port 3306 som standard
  - ❌ **d) 8080 - som alla webbservrar!** - FEL: 3306 är MySQL:s signaturnummer!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är Pomelo i Entity Framework Core?

a. Ett ORM<br>
b. En frukt som smakar databas!<br>
c. En databas<br>
d. Community-driven MySQL provider för EF Core<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Community-driven MySQL provider för EF Core


  **Förklaringar:**

  - ❌ **a) Ett ORM** - FEL: Pomelo är en provider till ORM (EF Core)
  - ❌ **b) En frukt som smakar databas!** - FEL: Pomelo är den rekommenderade MySQL-providern för EF Core!
  - ❌ **c) En databas** - FEL: Pomelo är en NuGet-paket provider
  - ✅ **d) Community-driven MySQL provider för EF Core** - **RÄTT**: Pomelo.EntityFrameworkCore.MySql är bättre än Oracles officiella
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Hur konfigurerar du MySQL i EF Core?

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder options)
{
    var connectionString = "Server=localhost;Database=heroesdb;User=root;Password=secret;";
    options._____(connectionString, ServerVersion.AutoDetect(connectionString));
}
```

a. UseDatabase!<br>
b. UseSqlServer<br>
c. UseMySQL<br>
d. UseMySql<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** UseMySql


  **Förklaringar:**

  - ❌ **a) UseDatabase!** - FEL: UseMySql specificerar vilken databas-typ vi använder!
  - ❌ **b) UseSqlServer** - FEL: UseSqlServer är för SQL Server, inte MySQL
  - ❌ **c) UseMySQL** - FEL: Versaler spelar roll - det är UseMySql
  - ✅ **d) UseMySql** - **RÄTT**: UseMySql konfigurerar Pomelo MySQL provider
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad gör ServerVersion.AutoDetect()?

```csharp
options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
```

a. Sätter vilken version av EF Core som används<br>
b. Hittar närmaste MySQL-server!<br>
c. Frågar MySQL-servern vilken version den kör<br>
d. Uppdaterar MySQL automatiskt<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Frågar MySQL-servern vilken version den kör


  **Förklaringar:**

  - ❌ **a) Sätter vilken version av EF Core som används** - FEL: Det är MySQL server-version, inte EF version
  - ❌ **b) Hittar närmaste MySQL-server!** - FEL: AutoDetect optimerar SQL-generering baserat på MySQL-version!
  - ✅ **c) Frågar MySQL-servern vilken version den kör** - **RÄTT**: AutoDetect kopplar upp och läser server-version automatiskt
  - ❌ **d) Uppdaterar MySQL automatiskt** - FEL: AutoDetect läser version, uppdaterar inte
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vilken datatyp använder MySQL för auto-increment?

a. SERIAL - som PostgreSQL!<br>
b. AUTO_INCREMENT på INT<br>
c. IDENTITY<br>
d. AUTOINCREMENT<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** AUTO_INCREMENT på INT


  **Förklaringar:**

  - ❌ **a) SERIAL - som PostgreSQL!** - FEL: AUTO_INCREMENT gör att Id ökar automatiskt vid INSERT!
  - ✅ **b) AUTO_INCREMENT på INT** - **RÄTT**: MySQL använder AUTO_INCREMENT attribute
  - ❌ **c) IDENTITY** - FEL: IDENTITY är SQL Server-syntax
  - ❌ **d) AUTOINCREMENT** - FEL: Det är SQLite-syntax med ett ord
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är InnoDB i MySQL?

a. Standard storage engine som stödjer transactions och foreign keys<br>
b. En databas i databasen!<br>
c. En MySQL-klient<br>
d. Ett backup-verktyg<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Standard storage engine som stödjer transactions och foreign keys


  **Förklaringar:**

  - ✅ **a) Standard storage engine som stödjer transactions och foreign keys** - **RÄTT**: InnoDB är default engine med ACID-stöd
  - ❌ **b) En databas i databasen!** - FEL: InnoDB = motorn som lagrar data med transaction-support!
  - ❌ **c) En MySQL-klient** - FEL: InnoDB är databasmotor, inte klient
  - ❌ **d) Ett backup-verktyg** - FEL: InnoDB är storage engine, inte backup
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad är utf8mb4 i MySQL?

```sql
CREATE TABLE Heroes (
    Name VARCHAR(100) CHARACTER SET utf8mb4
);
```

a. En komprimeringsmetod<br>
b. UTF-8 encoding som stödjer emojis och alla Unicode-tecken<br>
c. En äldre encoding<br>
d. UTF-8 mega byte 4!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** UTF-8 encoding som stödjer emojis och alla Unicode-tecken


  **Förklaringar:**

  - ❌ **a) En komprimeringsmetod** - FEL: utf8mb4 är character encoding, inte komprimering
  - ✅ **b) UTF-8 encoding som stödjer emojis och alla Unicode-tecken** - **RÄTT**: utf8mb4 är full 4-byte UTF-8, stödjer 😀 emojis
  - ❌ **c) En äldre encoding** - FEL: utf8mb4 är modernare än utf8 (3-byte)
  - ❌ **d) UTF-8 mega byte 4!** - FEL: mb4 = multi-byte 4 - stödjer alla emojis och specialtecken! 🦸
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Hur ser en MySQL connection string ut?

```csharp
var connStr = "Server=___;Database=___;User=___;Password=___;";
```

a. Data Source=heroesdb.db<br>
b. Server=(localdb)\\mssqllocaldb<br>
c. Just localhost!<br>
d. Server=localhost;Database=heroesdb;User=root;Password=secret;<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Server=localhost;Database=heroesdb;User=root;Password=secret;


  **Förklaringar:**

  - ❌ **a) Data Source=heroesdb.db** - FEL: Det är SQLite-format, inte MySQL
  - ❌ **b) Server=(localdb)\\mssqllocaldb** - FEL: Det är SQL Server LocalDb-format
  - ❌ **c) Just localhost!** - FEL: MySQL behöver Server, Database, User och Password!
  - ✅ **d) Server=localhost;Database=heroesdb;User=root;Password=secret;** - **RÄTT**: Standard MySQL connection string format
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Vad är SHOW TABLES; i MySQL?

a. Skapar tabeller<br>
b. Kommando som listar alla tabeller i aktuell databas<br>
c. Visar innehållet i tabeller<br>
d. SELECT * FROM TABLES!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kommando som listar alla tabeller i aktuell databas


  **Förklaringar:**

  - ❌ **a) Skapar tabeller** - FEL: SHOW visar, CREATE skapar
  - ✅ **b) Kommando som listar alla tabeller i aktuell databas** - **RÄTT**: SHOW TABLES är MySQL-specifikt kommando
  - ❌ **c) Visar innehållet i tabeller** - FEL: SHOW TABLES visar namn, inte innehåll
  - ❌ **d) SELECT * FROM TABLES!** - FEL: SHOW TABLES = lista tabellnamn i databasen!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad gör DESCRIBE i MySQL?

```sql
DESCRIBE Heroes;
```

a. Visar tabellstruktur (kolumner, typer, nycklar)<br>
b. Returnerar data från tabellen<br>
c. Beskriver vad tabellen används till<br>
d. Berättar en saga om tabellen!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Visar tabellstruktur (kolumner, typer, nycklar)


  **Förklaringar:**

  - ✅ **a) Visar tabellstruktur (kolumner, typer, nycklar)** - **RÄTT**: DESCRIBE är shortcut för SHOW COLUMNS FROM
  - ❌ **b) Returnerar data från tabellen** - FEL: DESCRIBE visar schema, inte data
  - ❌ **c) Beskriver vad tabellen används till** - FEL: DESCRIBE visar teknisk struktur, inte användning
  - ❌ **d) Berättar en saga om tabellen!** - FEL: DESCRIBE visar Id INT, Name VARCHAR, etc - tabellens DNA!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 13

Vad är skillnaden mellan VARCHAR och TEXT i MySQL?

a. VARCHAR har max-längd, TEXT kan vara mycket längre<br>
b. VARCHAR är för variabler!<br>
c. Ingen skillnad<br>
d. TEXT är snabbare<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** VARCHAR har max-längd, TEXT kan vara mycket längre


  **Förklaringar:**

  - ✅ **a) VARCHAR har max-längd, TEXT kan vara mycket längre** - **RÄTT**: VARCHAR max 65,535, TEXT kan vara 65,535 tecken per rad
  - ❌ **b) VARCHAR är för variabler!** - FEL: VARCHAR(100) = max 100 tecken, TEXT = stor text!
  - ❌ **c) Ingen skillnad** - FEL: TEXT är för längre text, VARCHAR för kortare
  - ❌ **d) TEXT är snabbare** - FEL: VARCHAR är ofta snabbare för kortare texter
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 14

Vad gör LIMIT i MySQL?

```sql
SELECT * FROM Heroes LIMIT 10;
```

a. Begränsar antal returnerade rader<br>
b. Sätter max storlek på tabellen<br>
c. Filtrerar data<br>
d. Stoppar efter 10 sekunder!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Begränsar antal returnerade rader


  **Förklaringar:**

  - ✅ **a) Begränsar antal returnerade rader** - **RÄTT**: LIMIT 10 returnerar max 10 rader
  - ❌ **b) Sätter max storlek på tabellen** - FEL: LIMIT påverkar query-resultat, inte tabellstorlek
  - ❌ **c) Filtrerar data** - FEL: LIMIT begränsar antal, WHERE filtrerar
  - ❌ **d) Stoppar efter 10 sekunder!** - FEL: LIMIT = de första 10 raderna, perfekt för paginering!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 15

Hur gör du paginering i MySQL?

```sql
SELECT * FROM Heroes LIMIT ___ OFFSET ___;
```
För sida 3 med 10 items per sida:

a. LIMIT 10 OFFSET 20<br>
b. LIMIT 10 OFFSET 10!<br>
c. LIMIT 3 OFFSET 10<br>
d. LIMIT 30 OFFSET 0<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** LIMIT 10 OFFSET 20


  **Förklaringar:**

  - ✅ **a) LIMIT 10 OFFSET 20** - **RÄTT**: Sida 3 = hoppa över 20 (2*10), hämta 10
  - ❌ **b) LIMIT 10 OFFSET 10!** - FEL: Sida 1: OFFSET 0, Sida 2: OFFSET 10, Sida 3: OFFSET 20!
  - ❌ **c) LIMIT 3 OFFSET 10** - FEL: LIMIT är items per sida, OFFSET är hur många som hoppas över
  - ❌ **d) LIMIT 30 OFFSET 0** - FEL: Detta hämtar de första 30, inte sida 3
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 16

Vad är TIMESTAMP i MySQL?

a. Tiden för senaste query<br>
b. En stämpel på databasen!<br>
c. Datatyp för datum och tid som auto-uppdateras<br>
d. En Unix timestamp<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Datatyp för datum och tid som auto-uppdateras


  **Förklaringar:**

  - ❌ **a) Tiden för senaste query** - FEL: TIMESTAMP är en kolumntyp
  - ❌ **b) En stämpel på databasen!** - FEL: TIMESTAMP DEFAULT CURRENT_TIMESTAMP = auto CreatedAt!
  - ✅ **c) Datatyp för datum och tid som auto-uppdateras** - **RÄTT**: TIMESTAMP kan auto-sätta vid INSERT/UPDATE
  - ❌ **d) En Unix timestamp** - FEL: TIMESTAMP är MySQL-typ, inte bara Unix epoch
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 17

Vad gör ON UPDATE CURRENT_TIMESTAMP?

```sql
CREATE TABLE Posts (
    UpdatedAt TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
);
```

a. Sätter timestamp vid INSERT<br>
b. Kräver manuell uppdatering<br>
c. Uppdaterar automatiskt timestamp när raden ändras<br>
d. Uppdaterar hela databasen!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Uppdaterar automatiskt timestamp när raden ändras


  **Förklaringar:**

  - ❌ **a) Sätter timestamp vid INSERT** - FEL: ON UPDATE triggar vid UPDATE, inte INSERT
  - ❌ **b) Kräver manuell uppdatering** - FEL: CURRENT_TIMESTAMP sätts automatiskt
  - ✅ **c) Uppdaterar automatiskt timestamp när raden ändras** - **RÄTT**: Auto-updaterande 'modified at' kolumn
  - ❌ **d) Uppdaterar hela databasen!** - FEL: UpdatedAt-kolumnen sätts automatiskt vid varje UPDATE!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 18

Vad är DEFAULT i MySQL?

```sql
CREATE TABLE Heroes (
    PowerLevel INT DEFAULT 50
);
```

a. Maximum-värde<br>
b. Minimum-värde<br>
c. Den vanligaste hjälten!<br>
d. Standardvärde om inget anges vid INSERT<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Standardvärde om inget anges vid INSERT


  **Förklaringar:**

  - ❌ **a) Maximum-värde** - FEL: DEFAULT är standardvärde, inte maximum
  - ❌ **b) Minimum-värde** - FEL: DEFAULT är standardvärde, inte minimum
  - ❌ **c) Den vanligaste hjälten!** - FEL: Om du inte anger PowerLevel blir det automatiskt 50!
  - ✅ **d) Standardvärde om inget anges vid INSERT** - **RÄTT**: DEFAULT ger kolumnen ett default-värde
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 19

Vad är ENUM i MySQL?

```sql
CREATE TABLE Users (
    Role ENUM('admin', 'user', 'guest')
);
```

a. Endless Numbers Unlimited!<br>
b. Ett nummer<br>
c. En foreign key<br>
d. Datatyp som begränsar värden till en specifik lista<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Datatyp som begränsar värden till en specifik lista


  **Förklaringar:**

  - ❌ **a) Endless Numbers Unlimited!** - FEL: ENUM = bara 'admin', 'user' eller 'guest' - inget annat!
  - ❌ **b) Ett nummer** - FEL: ENUM är enumeration, inte nummer
  - ❌ **c) En foreign key** - FEL: ENUM är datatyp, inte relation
  - ✅ **d) Datatyp som begränsar värden till en specifik lista** - **RÄTT**: ENUM tillåter bara fördefinierade värden
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 20

Hur kontrollerar du MySQL server-version från kommandoraden?

a. mysql --version eller SELECT VERSION();<br>
b. mysql -v<br>
c. CHECK VERSION!<br>
d. SHOW VERSION;<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** mysql --version eller SELECT VERSION();


  **Förklaringar:**

  - ✅ **a) mysql --version eller SELECT VERSION();** - **RÄTT**: Båda visar MySQL version
  - ❌ **b) mysql -v** - FEL: -v är verbose, inte version (använd --version)
  - ❌ **c) CHECK VERSION!** - FEL: mysql --version från shell, SELECT VERSION(); från MySQL!
  - ❌ **d) SHOW VERSION;** - FEL: Det är SELECT VERSION(); i SQL
</details>


## Sammanfattning

Du har nu genomgått 20 träningsfrågor om training: mysql basics. 
Dessa frågor täcker viktiga koncept som du behöver känna till för att lyckas i kursen.


**Tips för fortsatt lärande:**

- Gå igenom frågorna igen om du hade svårt med några

- Testa att skriva egen kod för att förstärka koncepten

- Diskutera svåra frågor med klasskamrater eller lärare
