# Träningsuppgifter: Databaser — Vecka 3

> **Tema:** SQL avancerat, normalisering och säkerhet  
> **Moduler:** 04 — SQL avancerat, 03 — Normalisering & GDPR, 07 — Säkerhet & backup

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är skillnaden mellan INNER JOIN och LEFT JOIN?

a. De är samma sak<br>b. INNER JOIN visar bara matchningar, LEFT JOIN visar allt från vänster tabell även om ingen matchning finns<br>c. LEFT JOIN är snabbare än INNER JOIN<br>d. INNER JOIN använder WHERE, LEFT JOIN använder ON

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** INNER JOIN visar bara matchningar, LEFT JOIN visar allt från vänster tabell även om ingen matchning finns

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Olika resultat — INNER inkluderar bara matchande rader, LEFT inkluderar alla från vänster
  - ✅ **b) INNER vs LEFT** - **RÄTT**: INNER JOIN: "Visa bara studenter MED kurser". LEFT JOIN: "Visa ALLA studenter, även de UTAN kurser" (de får NULL för kursen)
  - ❌ **c) Snabbare** - FEL: Beror på data och index, inte på JOIN-typen
  - ❌ **d) WHERE vs ON** - FEL: Båda använder ON för JOIN-villkoret
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad blir resultatet av ett LEFT JOIN om en student inte har några kurser?

a. Studenten visas inte alls<br>b. Studenten visas med NULL i kurs-kolumnerna<br>c. Programmet kraschar<br>d. Studenten får en tom rad

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Studenten visas med NULL i kurs-kolumnerna

  **Förklaringar:**

  - ❌ **a) Visas inte** - FEL: Det är INNER JOIN-beteende
  - ✅ **b) NULL i kurs-kolumner** - **RÄTT**: LEFT JOIN säger "behåll alla från vänster". Om höger tabell saknar matchning sätts NULL. Perfekt för "studenter utan kurser" eller "kunder utan ordrar"
  - ❌ **c) Kraschar** - FEL: NULL är ett giltigt värde, inget som kraschar
  - ❌ **d) Tom rad** - FEL: Raden finns, men kurs-kolumnerna har värdet NULL
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad gör GROUP BY i en SQL-fråga?

a. Sorterar data i grupper<br>b. Grupperar rader med samma värde för att kunna använda aggregatfunktioner<br>c. Skapar grupper av användare<br>d. Tar bort dubbletter

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Grupperar rader med samma värde för att kunna använda aggregatfunktioner

  **Förklaringar:**

  - ❌ **a) Sorterar** - FEL: Sortering gör ORDER BY. GROUP BY grupperar för aggregering
  - ✅ **b) Grupperar för aggregat** - **RÄTT**: `SELECT Category, COUNT(*) FROM Products GROUP BY Category` — räknar antalet produkter per kategori. GROUP BY slår ihop alla rader med samma kategori-värde
  - ❌ **c) Användargrupper** - FEL: Det är en SQL-funktion, inget med användare att göra
  - ❌ **d) Tar bort dubbletter** - FEL: Det gör DISTINCT
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad är skillnaden mellan WHERE och HAVING?

a. De är samma sak, bara olika syntax<br>b. WHERE filtrerar rader före GROUP BY, HAVING filtrerar grupper efter GROUP BY<br>c. HAVING är snabbare än WHERE<br>d. WHERE används bara med SELECT

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** WHERE filtrerar rader före GROUP BY, HAVING filtrerar grupper efter GROUP BY

  **Förklaringar:**

  - ❌ **a) Samma sak** - FEL: Helt olika — WHERE kan inte använda aggregatfunktioner, HAVING kan
  - ✅ **b) WHERE före, HAVING efter** - **RÄTT**: Körningsordning: FROM → WHERE → GROUP BY → HAVING → SELECT → ORDER BY. WHERE filtrerar först, HAVING filtrerar de grupperade resultaten
  - ❌ **c) Snabbare** - FEL: WHERE filtrerar tidigare och kan vara mer effektivt, men HAVING har ett annat syfte
  - ❌ **d) Bara med SELECT** - FEL: WHERE fungerar även med UPDATE och DELETE
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är normalisering?

a. Att göra data mer normalt och vanligt<br>b. Att organisera data för att minska redundans och förbättra dataintegritet<br>c. Att skapa backup av databasen<br>d. Att konvertera data till ett standardformat

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Att organisera data för att minska redundans och förbättra dataintegritet

  **Förklaringar:**

  - ❌ **a) Göra data normalt** - FEL: Ingenting med "normalt" att göra — normalisering är en teknisk process
  - ✅ **b) Minska redundans och förbättra integritet** - **RÄTT**: Normalisering delar upp data i flera tabeller för att slippa upprepning. Exempel: istället för att spara "IT, John Doe" på varje anställdrad, skapa en Departments-tabell
  - ❌ **c) Backup** - FEL: Backup är en annan sak
  - ❌ **d) Standardformat** - FEL: Det handlar om databasdesign, inte formatkonvertering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad krävs för att en tabell ska vara i första normalform (1NF)?

a. Alla kolumner måste vara sorterade<br>b. Varje kolumn innehåller atomära (odelbara) värden och inga upprepande grupper av kolumner<br>c. Alla rader måste vara unika<br>d. Det måste finnas en Foreign Key

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Varje kolumn innehåller atomära (odelbara) värden och inga upprepande grupper av kolumner

  **Förklaringar:**

  - ❌ **a) Sorterade** - FEL: Sortering har inget med normalformer att göra
  - ✅ **b) Atomära värden, inga upprepande grupper** - **RÄTT**: Tabellen `Order(OrderID, Product1, Product2, Product3)` uppfyller INTE 1NF. Istället: `Order(OrderID, Product)` med en rad per produkt. En kolumn får inte innehålla "Bok, Penna, Linjal" — dela upp
  - ❌ **c) Unika rader** - FEL: Det är Primary Key, inte 1NF
  - ❌ **d) Foreign Key** - FEL: FK är för relationer, inte för 1NF
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

En tabell har kolumnerna: EmployeeID, Name, Department, DepartmentHead. Varför uppfyller den INTE tredje normalform (3NF)?

a. Den har för många kolumner<br>b. DepartmentHead är beroende av Department, inte av EmployeeID (transitivt beroende)<br>c. Den saknar Primary Key<br>d. Den har NULL-värden

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** DepartmentHead är beroende av Department, inte av EmployeeID (transitivt beroende)

  **Förklaringar:**

  - ❌ **a) För många kolumner** - FEL: Antal kolumner har inget med 3NF att göra
  - ✅ **b) Transitivt beroende** - **RÄTT**: DepartmentHead beror på Department, som beror på EmployeeID. Om IT byter chef måste du uppdatera ALLA anställda i IT. Lösning: skapa en Departments-tabell
  - ❌ **c) Saknar PK** - FEL: Även med PK bryts 3NF av transitiva beroenden
  - ❌ **d) NULL-värden** - FEL: NULL är OK i 3NF
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är SQL Injection?

a. Att injicera SQL-kod i en databas via databasens administrationsverktyg<br>b. En attack där användaren matar in SQL-kod via ett formulärfält som körs mot databasen<br>c. Att skapa en SQL-fråga i C#-kod<br>d. En metod för att optimera SQL-frågor

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En attack där användaren matar in SQL-kod via ett formulärfält som körs mot databasen

  **Förklaringar:**

  - ❌ **a) Via admin-verktyg** - FEL: SQL Injection sker via applikationens input, inte admin-verktyg
  - ✅ **b) Användaren injicerar SQL** - **RÄTT**: Om du skriver `"SELECT * FROM Users WHERE name='" + användarnamn + "'"` och användaren skriver `admin' --` ändras frågan till `SELECT * FROM Users WHERE name='admin' --'` — kommentaren tar bort lösenordskollen!
  - ❌ **c) SQL i C#** - FEL: Det är bara vanlig programmering, inte en attack
  - ❌ **d) Optimering** - FEL: SQL Injection är en säkerhetsrisk, inte en optimeringsteknik
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Hur skyddar Entity Framework Core mot SQL Injection?

a. Det skyddar inte — du måste själv vara försiktig<br>b. EF Core använder parametriserade queries automatiskt när du använder LINQ<br>c. EF Core blockerar all SQL-kod<br>d. EF Core krypterar alla queries

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** EF Core använder parametriserade queries automatiskt när du använder LINQ

  **Förklaringar:**

  - ❌ **a) Skyddar inte** - FEL: EF Core har inbyggt skydd — så länge du använder LINQ
  - ✅ **b) Parametriserade queries** - **RÄTT**: `db.Heroes.FirstOrDefault(h => h.HeroName == input)` genererar `SELECT * FROM Heroes WHERE HeroName = @p0` — användarens input blir en parameter, inte SQL-kod. Även om input innehåller `' OR '1'='1` tolkas det som TEXT, inte SQL
  - ❌ **c) Blockerar all SQL** - FEL: EF Core tillåter raw SQL via FromSqlRaw, men då måste du vara försiktig
  - ❌ **d) Krypterar** - FEL: Kryptering är något annat. EF Core parametriserar, vilket separerar data från kod
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är principen om minsta behörighet (Least Privilege Principle)?

a. Alla användare ska ha root-behörighet för att fungera<br>b. Varje användare ska bara ha de rättigheter som absolut behövs — inget mer<br>c. Bara utvecklare ska ha tillgång till databasen<br>d. Användare ska ha mer rättigheter än de behöver för framtida behov

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Varje användare ska bara ha de rättigheter som absolut behövs — inget mer

  **Förklaringar:**

  - ❌ **a) Root för alla** - FEL: Det är VÄRSTA praxis — om en app hackas har hackaren full kontroll
  - ✅ **b) Minsta möjliga rättigheter** - **RÄTT**: App-användaren får bara SELECT, INSERT, UPDATE, DELETE — inte CREATE/DROP. Read-only användaren får bara SELECT. Root används bara för setup och migrationer
  - ❌ **c) Bara utvecklare** - FEL: Databasen ska användas av appar, inte bara utvecklare
  - ❌ **d) Extra rättigheter** - FEL: Ge mer rättigheter vid behov, börja med så lite som möjligt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Vad gör mysqldump?

a. Skapar en ny databas<br>b. Tar bort gammal data<br>c. Skapar en backup-fil med all SQL som krävs för att återskapa en databas<br>d. Optimerar databasens prestanda

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skapar en backup-fil med all SQL som krävs för att återskapa en databas

  **Förklaringar:**

  - ❌ **a) Skapa databas** - FEL: Det gör CREATE DATABASE
  - ❌ **b) Ta bort data** - FEL: Det gör DELETE/DROP
  - ✅ **c) Backup** - **RÄTT**: `mysqldump -uroot -pSuperSecret123 heroesdb > backup.sql` skapar en fil med CREATE TABLE + INSERT-satser för all data. Återställ med: `mysql ... < backup.sql`
  - ❌ **d) Optimera** - FEL: mysqldump är för backup, inte optimering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad är 3-2-1 regeln för backup?

a. 3 backups, 2 dagar mellan varje, 1 gång i veckan<br>b. 3 kopior av data, 2 olika media, 1 offsite<br>c. 3 utvecklare, 2 testmiljöer, 1 produktion<br>d. 3 databaser, 2 servrar, 1 backup

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 3 kopior av data, 2 olika media, 1 offsite

  **Förklaringar:**

  - ❌ **a) Tidsschema** - FEL: Regeln handlar om antal kopior, inte tid
  - ✅ **b) 3 kopior, 2 media, 1 offsite** - **RÄTT**: 3 kopior (original + 2 backups). 2 olika media (t.ex. hårddisk + moln). 1 offsite (separat geografisk plats). Om kontoret brinner finns backup någon annanstans
  - ❌ **c) Team-storlek** - FEL: Ingenting med utvecklingsteam att göra
  - ❌ **d) Servrar** - FEL: Handlar om kopior, inte antal servrar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
