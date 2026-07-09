# Träningsuppgifter: Training: SQL Injection & Security

🟢


## Instruktioner

Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt. 
Klicka på 'Visa svar' för att se det rätta svaret och förklaringar för alla alternativ.

### Fråga 1

Vad är SQL Injection?

a. Ett sätt att optimera SQL-queries<br>
b. En attack där skadlig SQL-kod injiceras via användarinput<br>
c. Ett verktyg för databasadministration<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En attack där skadlig SQL-kod injiceras via användarinput


  **Förklaringar:**

  - ❌ **a) Ett sätt att optimera SQL-queries** - FEL: SQL Injection är en attack, inte optimering
  - ✅ **b) En attack där skadlig SQL-kod injiceras via användarinput** - **RÄTT**: SQL Injection är världens vanligaste webbsårbarhet
  - ❌ **c) Ett verktyg för databasadministration** - FEL: Det är en säkerhetsrisk, inte ett verktyg
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vilket klassiskt XKCD-exempel visar SQL Injection?

```sql
INSERT INTO Students (Name) VALUES ('Robert'); DROP TABLE Students;--');
```

a. Bobby Tables (Robert'); DROP TABLE Students;--)<br>
b. Little Johnny<br>
c. SQL Sam<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Bobby Tables (Robert'); DROP TABLE Students;--)


  **Förklaringar:**

  - ✅ **a) Bobby Tables (Robert'); DROP TABLE Students;--)** - **RÄTT**: xkcd.com/327 - "We named him Robert'); DROP TABLE Students;--"
  - ❌ **b) Little Johnny** - FEL: Det är Bobby Tables som är den klassiska
  - ❌ **c) SQL Sam** - FEL: Det heter Bobby Tables
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vilken kod är SÅRBAR för SQL Injection?

```csharp
// A
var sql = $"SELECT * FROM Users WHERE Email = '{email}'";

// B
var sql = "SELECT * FROM Users WHERE Email = @email";
command.Parameters.AddWithValue("@email", email);
```

a. Båda är säkra<br>
b. B - Parametriserad query<br>
c. A - String interpolation<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** A - String interpolation


  **Förklaringar:**

  - ❌ **a) Båda är säkra** - FEL: A är farligt sårbar
  - ❌ **b) B - Parametriserad query** - FEL: Parametrar skyddar mot SQL Injection
  - ✅ **c) A - String interpolation** - **RÄTT**: String concatenation/interpolation med user input är farligt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad skriver följande kod ut om användaren anger: `admin' OR '1'='1`?

```csharp
var username = Console.ReadLine(); // admin' OR '1'='1
var sql = $"SELECT * FROM Users WHERE Username = '{username}'";
// SQL blir: SELECT * FROM Users WHERE Username = 'admin' OR '1'='1'
```

a. Bara admin-användaren<br>
b. ALLA användare (eftersom '1'='1' är alltid sant)<br>
c. Ingenting - queryn failar<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** ALLA användare (eftersom '1'='1' är alltid sant)


  **Förklaringar:**

  - ❌ **a) Bara admin-användaren** - FEL: OR '1'='1' gör att alla användare returneras
  - ✅ **b) ALLA användare (eftersom '1'='1' är alltid sant)** - **RÄTT**: OR-injection returnerar alla rader när villkoret är sant
  - ❌ **c) Ingenting - queryn failar** - FEL: SQL är syntaktiskt korrekt och körs
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Hur skyddar parametriserade queries mot SQL Injection?

```csharp
command.CommandText = "SELECT * FROM Users WHERE Email = @email";
command.Parameters.AddWithValue("@email", "test' OR '1'='1");
```

a. Blockerar alla specialtecken<br>
b. Parametervärden behandlas som data, inte kod<br>
c. Kollar att input är säker<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Parametervärden behandlas som data, inte kod


  **Förklaringar:**

  - ❌ **a) Blockerar alla specialtecken** - FEL: Parametrar tillåter specialtecken men escapar dem
  - ✅ **b) Parametervärden behandlas som data, inte kod** - **RÄTT**: Parametrar escapas automatiskt - injektionen blir bara text
  - ❌ **c) Kollar att input är säker** - FEL: Parametrar gör input säker genom escaping
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad gör Entity Framework för att skydda mot SQL Injection?

a. Blockerar alla specialtecken<br>
b. Använder alltid parametriserade queries automatiskt<br>
c. Krypterar all input<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Använder alltid parametriserade queries automatiskt


  **Förklaringar:**

  - ❌ **a) Blockerar alla specialtecken** - FEL: EF parametriserar, blockerar inte tecken
  - ✅ **b) Använder alltid parametriserade queries automatiskt** - **RÄTT**: LINQ-queries genererar säkra SQL-satser med parametrar
  - ❌ **c) Krypterar all input** - FEL: EF parametriserar queries, krypterar inte
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vilken EF Core-metod är FARLIG om den används fel?

```csharp
var heroes = db.Heroes
    .FromSqlRaw($"SELECT * FROM Heroes WHERE Name = '{name}'")
    .ToList();
```

a. FromSqlRaw med string interpolation<br>
b. ToList()<br>
c. Where()<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** FromSqlRaw med string interpolation


  **Förklaringar:**

  - ✅ **a) FromSqlRaw med string interpolation** - **RÄTT**: FromSqlRaw + string interpolation = SQL Injection-risk!
  - ❌ **b) ToList()** - FEL: ToList() är säker, problemet är FromSqlRaw
  - ❌ **c) Where()** - FEL: LINQ Where() är säker
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Hur använder du FromSqlRaw SÄKERT i EF Core?

```csharp
var name = "admin' OR '1'='1";
var heroes = db.Heroes
    .FromSqlRaw(_____, name)
    .ToList();
```

a. SELECT * FROM Heroes WHERE Name = '" + name + "'<br>
b. $"SELECT * FROM Heroes WHERE Name = '{name}'<br>
c. SELECT * FROM Heroes WHERE Name = {0}<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SELECT * FROM Heroes WHERE Name = {0}


  **Förklaringar:**

  - ❌ **a) SELECT * FROM Heroes WHERE Name = '" + name + "'** - FEL: String concatenation är farligt!
  - ❌ **b) $"SELECT * FROM Heroes WHERE Name = '{name}'** - FEL: String interpolation i FromSqlRaw är farligt!
  - ✅ **c) SELECT * FROM Heroes WHERE Name = {0}** - **RÄTT**: Använd placeholders {0}, {1} etc för parametrar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad är FromSqlInterpolated i EF Core?

```csharp
var heroes = db.Heroes
    .FromSqlInterpolated($"SELECT * FROM Heroes WHERE Name = {name}")
    .ToList();
```

a. Samma som FromSqlRaw<br>
b. Snabbare än FromSqlRaw<br>
c. Säker version som automatiskt parametriserar interpolated strings<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Säker version som automatiskt parametriserar interpolated strings


  **Förklaringar:**

  - ❌ **a) Samma som FromSqlRaw** - FEL: FromSqlInterpolated är säkrare
  - ❌ **b) Snabbare än FromSqlRaw** - FEL: Handlar om säkerhet, inte prestanda
  - ✅ **c) Säker version som automatiskt parametriserar interpolated strings** - **RÄTT**: FromSqlInterpolated gör string interpolation säker
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad händer om Bobby Tables-attacken lyckas?

```sql
INSERT INTO Students (Name) VALUES ('Robert'); DROP TABLE Students;--');
```

a. Students-tabellen raderas permanent<br>
b. Bara namnet 'Robert' läggs till<br>
c. SQL-queryn failar<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Students-tabellen raderas permanent


  **Förklaringar:**

  - ✅ **a) Students-tabellen raderas permanent** - **RÄTT**: DROP TABLE tar bort hela tabellen och all data
  - ❌ **b) Bara namnet 'Robert' läggs till** - FEL: Attacken exekverar både INSERT och DROP
  - ❌ **c) SQL-queryn failar** - FEL: Om systemet är sårbart körs båda kommandona
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Vad betyder '--' i SQL Injection-attacker?

```sql
SELECT * FROM Users WHERE Username = 'admin'--' AND Password = 'x'
```

a. Ett minus-tecken<br>
b. En operator<br>
c. SQL-kommentar som ignorerar resten av queryn<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SQL-kommentar som ignorerar resten av queryn


  **Förklaringar:**

  - ❌ **a) Ett minus-tecken** - FEL: -- är kommentar-syntax i SQL
  - ❌ **b) En operator** - FEL: -- startar en kommentar
  - ✅ **c) SQL-kommentar som ignorerar resten av queryn** - **RÄTT**: -- kommenterar bort lösenordskollen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vilken attack använder UNION för att läsa andra tabeller?

```sql
SELECT Name FROM Products WHERE Id = 1 UNION SELECT Password FROM Users
```

a. OR-injection<br>
b. UNION-based SQL Injection<br>
c. Comment injection<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** UNION-based SQL Injection


  **Förklaringar:**

  - ❌ **a) OR-injection** - FEL: OR-injection använder OR, inte UNION
  - ✅ **b) UNION-based SQL Injection** - **RÄTT**: UNION låter attackerare kombinera resultat från andra tabeller
  - ❌ **c) Comment injection** - FEL: Det är UNION-attack
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 13

Vad är stored procedures och hjälper de mot SQL Injection?

a. Backup-metod<br>
b. Fördefinierad SQL i databasen - hjälper om parametriserad<br>
c. Alltid 100% säkra mot injection<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Fördefinierad SQL i databasen - hjälper om parametriserad


  **Förklaringar:**

  - ❌ **a) Backup-metod** - FEL: Stored procedures är SQL-funktioner, inte backup
  - ✅ **b) Fördefinierad SQL i databasen - hjälper om parametriserad** - **RÄTT**: Stored procedures med parametrar är säkra
  - ❌ **c) Alltid 100% säkra mot injection** - FEL: Bara om de använder parametrar korrekt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 14

Vilken princip är VIKTIGAST för att förhindra SQL Injection?

a. ALDRIG lita på användarinput - använd alltid parametrar<br>
b. Använd bara SELECT-queries<br>
c. Kryptera all data<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** ALDRIG lita på användarinput - använd alltid parametrar


  **Förklaringar:**

  - ✅ **a) ALDRIG lita på användarinput - använd alltid parametrar** - **RÄTT**: Treat all input as hostile är grundregeln
  - ❌ **b) Använd bara SELECT-queries** - FEL: SELECT kan också utnyttjas för data exfiltration
  - ❌ **c) Kryptera all data** - FEL: Kryptering skyddar inte mot SQL Injection
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 15

Vad är Whitelist-validering?

```csharp
if (!Regex.IsMatch(input, @"^[a-zA-Z0-9]+$"))
    throw new Exception("Invalid input");
```

a. Tillåt bara godkända tecken/värden<br>
b. Kryptera input<br>
c. Blockera kända attacker<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Tillåt bara godkända tecken/värden


  **Förklaringar:**

  - ✅ **a) Tillåt bara godkända tecken/värden** - **RÄTT**: Whitelist = explicit lista av tillåtna värden
  - ❌ **b) Kryptera input** - FEL: Whitelist validerar format, krypterar inte
  - ❌ **c) Blockera kända attacker** - FEL: Det är blacklist, inte whitelist
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 16

Varför är blacklist-validering osäker?

```csharp
if (input.Contains("'") || input.Contains("--"))
    throw new Exception("Invalid");
```

a. Omöjligt att förutse alla attack-varianter<br>
b. För långsam<br>
c. Blockerar för mycket<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Omöjligt att förutse alla attack-varianter


  **Förklaringar:**

  - ✅ **a) Omöjligt att förutse alla attack-varianter** - **RÄTT**: Hackare hittar alltid nya sätt att kringgå blacklist
  - ❌ **b) För långsam** - FEL: Prestanda är inte huvudproblemet
  - ❌ **c) Blockerar för mycket** - FEL: Problemet är att den blockerar för LITE
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 17

Vad är Least Privilege Principle för databassäkerhet?

a. Ge bara de rättigheter som absolut behövs<br>
b. Använd alltid root/admin-konto<br>
c. Kryptera all data<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ge bara de rättigheter som absolut behövs


  **Förklaringar:**

  - ✅ **a) Ge bara de rättigheter som absolut behövs** - **RÄTT**: App-user ska bara kunna SELECT/INSERT/UPDATE, inte DROP TABLE
  - ❌ **b) Använd alltid root/admin-konto** - FEL: Motsatsen - använd ALDRIG root för app!
  - ❌ **c) Kryptera all data** - FEL: Handlar om behörigheter, inte kryptering
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 18

Vad gör prepared statements i ADO.NET?

```csharp
var cmd = new SqlCommand("SELECT * FROM Users WHERE Email = @email", conn);
cmd.Parameters.AddWithValue("@email", userInput);
```

a. Separerar SQL-kod från data och förhindrar injection<br>
b. Gör queryn snabbare<br>
c. Krypterar queryn<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Separerar SQL-kod från data och förhindrar injection


  **Förklaringar:**

  - ✅ **a) Separerar SQL-kod från data och förhindrar injection** - **RÄTT**: Prepared statements = parametriserade queries
  - ❌ **b) Gör queryn snabbare** - FEL: Prestanda är en bonus, säkerhet är huvudsyftet
  - ❌ **c) Krypterar queryn** - FEL: Prepared statements parametriserar, krypterar inte
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 19

Vilket påstående om LINQ och SQL Injection är SANT?

a. LINQ-queries i EF Core är automatiskt säkra<br>
b. LINQ kan vara sårbara för injection<br>
c. LINQ kräver manuell sanitering<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** LINQ-queries i EF Core är automatiskt säkra


  **Förklaringar:**

  - ✅ **a) LINQ-queries i EF Core är automatiskt säkra** - **RÄTT**: LINQ genererar parametriserade queries
  - ❌ **b) LINQ kan vara sårbara för injection** - FEL: Standard LINQ är säker
  - ❌ **c) LINQ kräver manuell sanitering** - FEL: LINQ hanterar säkerhet automatiskt
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 20

Vad är Web Application Firewall (WAF)?

a. Ett virus-program<br>
b. Ett lager som filtrerar HTTP-requests för attack-mönster<br>
c. En databas-firewall<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett lager som filtrerar HTTP-requests för attack-mönster


  **Förklaringar:**

  - ❌ **a) Ett virus-program** - FEL: WAF är specifikt för webbattacker
  - ✅ **b) Ett lager som filtrerar HTTP-requests för attack-mönster** - **RÄTT**: WAF kan blockera SQL Injection-försök
  - ❌ **c) En databas-firewall** - FEL: WAF är för webbapplikationer, inte bara databaser
</details>


## Sammanfattning

Du har nu genomgått 20 träningsfrågor om training: sql injection & security. 
Dessa frågor täcker viktiga koncept som du behöver känna till för att lyckas i kursen.


**Tips för fortsatt lärande:**

- Gå igenom frågorna igen om du hade svårt med några

- Testa att skriva egen kod för att förstärka koncepten

- Diskutera svåra frågor med klasskamrater eller lärare
