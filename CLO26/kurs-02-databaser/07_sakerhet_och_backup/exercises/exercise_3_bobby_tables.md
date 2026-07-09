# Övning 3: Bobby Tables - SQL Injection 💉

🔴

**Fort Knox Protocol - Del 3**

## Mål
Förstå SQL Injection genom att både ATTACKERA och FÖRSVARA dig mot världens vanligaste webbsårbarhet. Du kommer lära dig varför EF Core är så viktigt för säkerhet!

## Förutsättningar
- Övning 2 klar (Heroes på MySQL)
- Grundläggande förståelse för SQL

## Tidsåtgång
45-60 minuter

⚠️ **VARNING:** Du kommer köra riktiga SQL Injection-attacker i denna övning. Gör detta ENDAST i din egen testmiljö!

---

## Del 1: Möt Bobby Tables

### Steg 1: Läs klassikern

Öppna: https://xkcd.com/327/

**Bilden visar:**
- Mamma: "We named him Robert'); DROP TABLE Students;--"
- Skola: "Oh, I hope you're not planning to use that name in a database..."

**Vad händer:**
Skolans system kör troligen:

```sql
INSERT INTO Students (Name) VALUES ('Robert'); DROP TABLE Students;--');
```

**Resultat:**
1. `Robert'` stänger strängen
2. `);` avslutar INSERT
3. `DROP TABLE Students;` raderar tabellen!
4. `--` kommenterar bort resten

**Hela student-tabellen: BORTA!** 😱

✅ **Checkpoint 1:** Du förstår konceptet!

---

## Del 2: Sårbar App (Gör INTE detta i produktion!)

Nu ska du bygga en MEDVETET sårbar app för att förstå problemet.

### Steg 1: Skapa nytt projekt

```bash
mkdir SqlInjectionDemo
cd SqlInjectionDemo
dotnet new console
```

### Steg 2: Lägg till MySQL-paket

```bash
dotnet add package MySqlConnector
```

### Steg 3: Skapa sårbar kod

Öppna `Program.cs` och skriv:

```csharp
using MySqlConnector;

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║  SÅRBAR HJÄLTE-SÖKARE (GÖR EJ I PROD!)  ║");
Console.WriteLine("╚══════════════════════════════════════════╝\n");

Console.Write("Sök hjälte (ange hjältenamn): ");
var heroName = Console.ReadLine() ?? "";

var connectionString = "Server=localhost;Database=heroesdb;" +
                      "User=root;Password=SuperSecret123;";

using var connection = new MySqlConnection(connectionString);
connection.Open();

// ⚠️ FARLIG KOD - ALDRIG GÖR DETTA!
var sql = $"SELECT * FROM Heroes WHERE HeroName = '{heroName}'";
Console.WriteLine($"\n🔍 SQL: {sql}\n");

try
{
    var command = new MySqlCommand(sql, connection);
    var reader = command.ExecuteReader();

    Console.WriteLine("═══════════════════════════════════════");
    Console.WriteLine("RESULTAT:");
    Console.WriteLine("═══════════════════════════════════════");

    var found = false;
    while (reader.Read())
    {
        found = true;
        Console.WriteLine($"🦸 {reader["HeroName"]} - {reader["RealName"]}");
        Console.WriteLine($"📞 {reader["Phone"]}");
        Console.WriteLine();
    }

    if (!found)
    {
        Console.WriteLine("❌ Ingen hjälte hittad!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"💥 ERROR: {ex.Message}");
}
```

### Steg 4: Lägg till data (om du inte har någon)

Koppla in i MySQL och lägg till några hjältar:

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb
```

```sql
INSERT INTO Heroes (RealName, HeroName, Phone) VALUES
('Tony Stark', 'Iron Man', '555-STARK'),
('Peter Parker', 'Spider-Man', '555-WEB'),
('Thor Odinson', 'Thor', '555-THUNDER');
```

```sql
EXIT;
```

✅ **Checkpoint 2:** Sårbar app skapad!

---

## Del 3: Testa Normal Användning

### Steg 1: Kör appen normalt

```bash
dotnet run
```

```
Sök hjälte (ange hjältenamn): Iron Man
```

**Förväntat resultat:**
```
🔍 SQL: SELECT * FROM Heroes WHERE HeroName = 'Iron Man'

═══════════════════════════════════════
RESULTAT:
═══════════════════════════════════════
🦸 Iron Man - Tony Stark
📞 555-STARK
```

**SÅ HÄR FUNGERAR DET!** Normal användning, inga problem.

✅ **Checkpoint 3:** Normal användning funkar!

---

## Del 4: Attack #1 - Data Exfiltration

Nu blir du hackare! 😈

### Steg 1: OR-injection

Kör appen igen:

```bash
dotnet run
```

```
Sök hjälte (ange hjältenamn): Thanos' OR '1'='1
```

**Vad händer?**

```
🔍 SQL: SELECT * FROM Heroes WHERE HeroName = 'Thanos' OR '1'='1'
```

**Resultat:**
```
═══════════════════════════════════════
RESULTAT:
═══════════════════════════════════════
🦸 Iron Man - Tony Stark
📞 555-STARK

🦸 Spider-Man - Peter Parker
📞 555-WEB

🦸 Thor - Thor Odinson
📞 555-THUNDER
```

**ALLA hjältar returnerade!** 😱

### Steg 2: Förklaring

Injected SQL:
```sql
SELECT * FROM Heroes WHERE HeroName = 'Thanos' OR '1'='1'
```

**Vad händer:**
- `'Thanos'` matchar ingen hjälte (false)
- `OR` gör att hela WHERE-klausulen är true om någon del är true
- `'1'='1'` är ALLTID true
- **Resultat:** Alla rader returneras!

**Security Impact:** Hackaren kan se all data utan att känna till någon hjälte!

✅ **Checkpoint 4:** OR-injection funkar (tyvärr!)!

---

## Del 5: Attack #2 - UNION-injection

Nu blir det mer advanced!

### Steg 1: UNION SELECT

Kör appen:

```bash
dotnet run
```

```
Sök hjälte (ange hjältenamn): x' UNION SELECT Id, Street, City, 'Hacked' FROM Addresses--
```

**Vad händer?**

```
🔍 SQL: SELECT * FROM Heroes WHERE HeroName = 'x' UNION SELECT Id, Street, City, 'Hacked' FROM Addresses--'
```

**Resultat:** Du ser adresser istället för hjältar!

### Steg 2: Förklaring

**UNION** låter dig kombinera två SELECT-queries:

```sql
SELECT * FROM Heroes WHERE HeroName = 'x'  -- Hittar inget
UNION
SELECT Id, Street, City, 'Hacked' FROM Addresses  -- Läser adresser!
-- -- kommenterar bort den sista '
```

**Security Impact:** Hackaren kan läsa VILKEN tabell som helst i databasen!

✅ **Checkpoint 5:** UNION-injection funkar!

---

## Del 6: Attack #3 - Bobby Tables (VARNING!)

⚠️ **VARNING:** Denna attack RADERAR VERKLIGEN TABELLEN! Gör BARA om du är beredd att återställa!

### Steg 1: Backup först!

```bash
docker exec mysql-heroes mysqldump -uroot -pSuperSecret123 heroesdb \
  > before_bobby_tables.sql
```

**NU HAR DU FALLSKÄRM!**

### Steg 2: Kör attacken

```bash
dotnet run
```

```
Sök hjälte (ange hjältenamn): Bobby'); DROP TABLE Heroes;--
```

**Vad händer?**

```
🔍 SQL: SELECT * FROM Heroes WHERE HeroName = 'Bobby'); DROP TABLE Heroes;--'

💥 ERROR: Table 'heroesdb.heroes' doesn't exist
```

**TABELLEN ÄR BORTA!** 😱😱😱

### Steg 3: Verifiera katastrofen

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb
```

```sql
SHOW TABLES;
```

**Heroes finns INTE i listan!**

```sql
EXIT;
```

### Steg 4: Återställ från backup

```bash
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < before_bobby_tables.sql
```

**Kolla:**

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**Tabellen tillbaka!** 😌

✅ **Checkpoint 6:** Bobby Tables-attack utförd och återställd!

---

## Del 7: Försvar #1 - Parametriserade Queries

Nu ska du FIXA sårbarheten!

### Steg 1: Skapa säker version

Skapa `ProgramSafe.cs`:

```csharp
using MySqlConnector;

Console.WriteLine("╔══════════════════════════════════════════╗");
Console.WriteLine("║   SÄKER HJÄLTE-SÖKARE (PARAMETRISERAD)  ║");
Console.WriteLine("╚══════════════════════════════════════════╝\n");

Console.Write("Sök hjälte (ange hjältenamn): ");
var heroName = Console.ReadLine() ?? "";

var connectionString = "Server=localhost;Database=heroesdb;" +
                      "User=root;Password=SuperSecret123;";

using var connection = new MySqlConnection(connectionString);
connection.Open();

// ✅ SÄKER KOD - Parametriserad query
var sql = "SELECT * FROM Heroes WHERE HeroName = @heroName";
Console.WriteLine($"\n🔍 SQL: {sql}");
Console.WriteLine($"📌 Parameter @heroName = '{heroName}'\n");

try
{
    var command = new MySqlCommand(sql, connection);
    command.Parameters.AddWithValue("@heroName", heroName);  // <-- KRITISK RAD!

    var reader = command.ExecuteReader();

    Console.WriteLine("═══════════════════════════════════════");
    Console.WriteLine("RESULTAT:");
    Console.WriteLine("═══════════════════════════════════════");

    var found = false;
    while (reader.Read())
    {
        found = true;
        Console.WriteLine($"🦸 {reader["HeroName"]} - {reader["RealName"]}");
        Console.WriteLine($"📞 {reader["Phone"]}");
        Console.WriteLine();
    }

    if (!found)
    {
        Console.WriteLine("❌ Ingen hjälte hittad!");
    }
}
catch (Exception ex)
{
    Console.WriteLine($"💥 ERROR: {ex.Message}");
}
```

### Steg 2: Testa normal användning

```bash
dotnet run ProgramSafe.cs
```

```
Sök hjälte (ange hjältenamn): Iron Man
```

**Resultat:** Fungerar perfekt!

### Steg 3: Testa OR-injection

```bash
dotnet run ProgramSafe.cs
```

```
Sök hjälte (ange hjältenamn): Thanos' OR '1'='1
```

**Resultat:**
```
❌ Ingen hjälte hittad!
```

**SKYDDET FUNGERAR!** Injection-strängen behandlas som DATA, inte kod!

### Steg 4: Testa Bobby Tables

```bash
dotnet run ProgramSafe.cs
```

```
Sök hjälte (ange hjältenamn): Bobby'); DROP TABLE Heroes;--
```

**Resultat:**
```
❌ Ingen hjälte hittad!
```

**Tabellen finns kvar!** Verifiera:

```bash
docker exec -it mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  -e "SELECT COUNT(*) FROM Heroes;"
```

**SÄKERT!** 🎉

✅ **Checkpoint 7:** Parametriserade queries skyddar!

---

## Del 8: Försvar #2 - EF Core

EF Core gör detta AUTOMATISKT!

### Steg 1: Testa Heroes-appen

Kör din Heroes_ef_live app:

```bash
cd ../Heroes_ef_live
dotnet run
```

Välj: `4` (Lista hjältar)

### Steg 2: Ändra för testning

Tillfällig ändring i `Program.cs` för att testa injection:

```csharp
static void SearchHero(HeroContext db)
{
    Console.Write("\nSök hjältenamn: ");
    var search = Console.ReadLine() ?? "";

    var heroes = db.Heroes
        .Where(h => h.HeroName == search)  // <-- EF använder parametrar automatiskt!
        .ToList();

    foreach (var hero in heroes)
    {
        Console.WriteLine($"🦸 {hero.HeroName} - {hero.RealName}");
    }

    if (!heroes.Any())
    {
        Console.WriteLine("❌ Ingen hjälte hittad!");
    }
}
```

Lägg till i menyn och testa:

```
Sök hjältenamn: Thanos' OR '1'='1
```

**Resultat:**
```
❌ Ingen hjälte hittad!
```

**EF Core skyddar automatiskt!** 🛡️

### Steg 3: Kolla generated SQL (optional)

Aktivera logging i HeroContext.cs:

```csharp
protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
{
    var connectionString = "Server=localhost;Database=heroesdb;" +
                          "User=root;Password=SuperSecret123;";

    optionsBuilder
        .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString))
        .LogTo(Console.WriteLine, LogLevel.Information);  // <-- Visar SQL!
}
```

Kör igen och se:

```
info: Microsoft.EntityFrameworkCore.Database.Command[20101]
      Executed DbCommand (5ms) [Parameters=[@__search_0='Thanos\' OR \'1\'=\'1' (Size = 4000)], CommandType='Text', CommandTimeout='30']
      SELECT `h`.`Id`, `h`.`HeroName`, `h`.`RealName`, `h`.`Phone`
      FROM `Heroes` AS `h`
      WHERE `h`.`HeroName` = @__search_0
```

**Se!** `@__search_0='Thanos\' OR \'1\'=\'1'` - hela injection-strängen är en PARAMETER!

✅ **Checkpoint 8:** EF Core is your friend!

---

## 🟢 Uppgift 1: Exploatera Sårbar App (Basic)

Använd den sårbara appen (`Program.cs`) och hitta injection-strängar för:

1. **Lista alla hjältar som börjar på 'S':**
   - Hint: `S%' OR HeroName LIKE '%`

2. **Få reda på hur många hjältar som finns:**
   - Hint: `x' UNION SELECT COUNT(*), 'x', 'x', 'x' FROM Heroes--`

3. **Lista alla tabellnamn i databasen:**
   - Hint: `x' UNION SELECT table_name, 'x', 'x', 'x' FROM information_schema.tables WHERE table_schema='heroesdb'--`

Testa varje injection och dokumentera resultatet!

---

## 🟡 Uppgift 2: Bygg Injection Detector (Advanced)

Skapa en funktion som detekterar potentiella SQL injection-försök:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public static bool IsPotentialSqlInjection(string input)
{
    var dangerousPatterns = new[]
    {
        "'",
        "--",
        ";",
        "/*",
        "*/",
        "xp_",
        "sp_",
        "DROP",
        "INSERT",
        "UPDATE",
        "DELETE",
        "UNION",
        "OR 1=1"
    };

    // Din kod här: Returnera true om någon pattern hittas
    // ...
}
```

Testa med:
- `Iron Man` (ska returnera false)
- `Thanos' OR '1'='1` (ska returnera true)
- `Bobby'); DROP TABLE Heroes;--` (ska returnera true)

**Fråga:** Är detta tillräckligt skydd? Varför/varför inte?

---

## 🔴 Uppgift 3: Whitelist Input Validation (Pro)

Parametriserade queries skyddar mot SQL injection, men vi kan också validera input!

Skapa en säker hjälte-sökare som:

1. **Endast tillåter bokstäver, siffror, mellanslag och bindestreck**
2. **Max 50 tecken**
3. **Loggar alla rejected inputs till fil**

```csharp
public static bool IsValidHeroName(string input)
{
    if (string.IsNullOrWhiteSpace(input) || input.Length > 50)
        return false;

    // Din kod här: Regex för tillåtna tecken
    // Hint: ^[a-zA-Z0-9 -]+$
    // ...
}

public static void LogSuspiciousInput(string input)
{
    // Din kod här: Logga till fil med timestamp
    // ...
}
```

Testa med både normal input och injection-försök!

---

## 🎯 Bonus: Prepared Statement Benchmark

Jämför prestanda mellan:

1. **String concatenation** (osäker)
2. **Parametriserade queries** (säker)

```csharp
var sw = Stopwatch.StartNew();

for (int i = 0; i < 1000; i++)
{
    // Kör query
}

sw.Stop();
Console.WriteLine($"Tid: {sw.ElapsedMilliseconds}ms");
```

**Hypotes:** Parametriserade queries är snabbare tack vare query plan caching!

Testa och dokumentera resultat!

---

## Sammanfattning

Du har nu:
- ✅ Förstått SQL Injection via Bobby Tables
- ✅ Byggt en medvetet sårbar app
- ✅ Genomfört OR-injection attack
- ✅ Genomfört UNION-injection attack
- ✅ Genomfört Bobby Tables attack (DROP TABLE)
- ✅ Lärt dig parametriserade queries
- ✅ Sett hur EF Core skyddar automatiskt
- ✅ (Bonus) Byggt injection detector och input validation

**Nästa steg:** Övning 4 - Read-Only User!

---

## Troubleshooting

### Problem: "Table doesn't exist" efter Bobby Tables

**Lösning:**
Återställ från backup:

```bash
docker exec -i mysql-heroes mysql -uroot -pSuperSecret123 heroesdb \
  < before_bobby_tables.sql
```

### Problem: Injection funkar inte i säker version

**Lösning:**
Det är MENINGEN! Parametriserade queries skyddar.

### Problem: "Parameter already defined"

**Lösning:**
Du har lagt till samma parameter två gånger:

```csharp
command.Parameters.AddWithValue("@heroName", heroName);
command.Parameters.AddWithValue("@heroName", anotherValue);  // DUBBLETT!
```

Ta bort dubbletten!

---

## Diskussionsfrågor

1. **Varför kallas det "Injection"?**
   - Vi "injicerar" kod i SQL-strängen

2. **Kan man få SQL Injection via EF Core LINQ?**
   - Nej! LINQ är säkert per design (använder parametrar)

3. **När är det OK att använda FromSqlRaw?**
   - Endast med parametriserade queries:
     ```csharp
     db.Heroes.FromSqlInterpolated($"SELECT * FROM Heroes WHERE HeroName = {name}")
     ```

4. **Hur kan man skydda sig förutom parametriserade queries?**
   - Input validation
   - Whitelist-filter
   - Principle of Least Privilege (begränsade users)
   - Web Application Firewall (WAF)

Diskutera i grupp!

---

## Pappaskämt-paus 😄

**Fråga:** Varför gick Bobby Tables till skolan?
**Svar:** För att DROP out! (Ba dum tss!)

**Fråga:** Vad sa hackaren när SQL Injection inte funkade?
**Svar:** "Det var en parametriserad motgång!"

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
