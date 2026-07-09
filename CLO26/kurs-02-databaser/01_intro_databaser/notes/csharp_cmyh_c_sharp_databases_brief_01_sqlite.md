# Kapitel 1 – SQLite: din första filbaserade databas

🟢


## 1. Vad är SQLite?

SQLite är en **filbaserad relationsdatabas** – hela databasen lagras i en enda fil (`.db` eller `.sqlite`).

### Fördelar
✅ Ingen server behövs
✅ Perfekt för små applikationer och prototyper
✅ Enkel att distribuera (kopiera bara filen)
✅ Inbyggd i många programmeringsspråk och ramverk
✅ Ingen konfiguration

### Nackdelar
❌ Inte lämplig för stora system med många samtidiga användare
❌ Begränsad prestanda jämfört med serverdatabaser
❌ Ingen nätverksåtkomst (lokal fil)

### Användningsområden
- Mobilappar (iOS, Android)
- Desktopapplikationer
- Prototyper och utveckling
- Embedded systems
- Testmiljöer

---

## 2. Installation av verktyg och Docker

### 2.1 Installera DB Browser for SQLite

**DB Browser** är ett grafiskt verktyg för att arbeta med SQLite-databaser.

1. Gå till https://sqlitebrowser.org/
2. Ladda ner för Windows
3. Installera med standardinställningar
4. Starta programmet

Alternativt kan du använda **SQLite CLI** (kommandorad), men DB Browser är enklare för nybörjare.

### 2.2 Placera databasen i My Documents med `Path.Combine()`

I C# är det bäst att lagra databasen i användarens dokumentmapp istället för att hårdkoda sökvägar.

```csharp
using System;
using System.IO;

string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string dbPath = Path.Combine(documentsPath, "MinApp", "app.db");

// Skapa mapp om den inte finns
Directory.CreateDirectory(Path.GetDirectoryName(dbPath)!);

string connectionString = $"Data Source={dbPath}";
```

**Varför är detta bra?**
- Fungerar på alla Windows-användare (inte bara dig)
- Fungerar om programmet installeras
- Användaren kan enkelt hitta och ta backup på databasen

### 2.3 (Valfritt) SQLite-container för Docker

SQLite behöver egentligen ingen Docker-container eftersom det är filbaserat, men för fullständighetens skull:

```dockerfile
# Dockerfile (sällan använt för SQLite)
FROM alpine:latest
RUN apk add --no-cache sqlite
CMD ["sqlite3"]
```

**Rekommendation:** Använd SQLite direkt utan Docker.

---

## 3. Kom igång i DB Browser

### Skapa en ny databas

1. Öppna DB Browser
2. File → New Database
3. Spara som `myapp.db` i valfri mapp
4. Du får en dialogruta för att skapa första tabellen

### Skapa en tabell

Klicka "Create Table" och ange:

**Tabellnamn:** `Customers`

**Kolumner:**
| Namn        | Typ     | Primärnyckel | Auto-increment | Not NULL |
|-------------|---------|--------------|----------------|----------|
| CustomerId  | INTEGER | ✅           | ✅             | ✅       |
| Name        | TEXT    |              |                | ✅       |
| Email       | TEXT    |              |                | ✅       |
| Phone       | TEXT    |              |                |          |

Klicka "OK".

### Lägg till data

1. Gå till fliken "Browse Data"
2. Välj tabellen `Customers`
3. Klicka "New Record"
4. Fyll i fälten
5. Klicka "Write Changes"

### Kör SQL-queries

Gå till fliken "Execute SQL" och testa:

```sql
SELECT * FROM Customers;

INSERT INTO Customers (Name, Email, Phone)
VALUES ('Anna Andersson', 'anna@example.com', '0701234567');

UPDATE Customers
SET Phone = '0709999999'
WHERE CustomerId = 1;

DELETE FROM Customers
WHERE CustomerId = 2;
```

---

## 4. Anslutning med C#

### Installera NuGet-paket

```bash
dotnet add package Microsoft.Data.Sqlite
```

### Basic connection

```csharp
using Microsoft.Data.Sqlite;

string connectionString = "Data Source=myapp.db";

using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM Customers";

    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var id = reader.GetInt32(0);
            var name = reader.GetString(1);
            var email = reader.GetString(2);

            Console.WriteLine($"{id}: {name} ({email})");
        }
    }
}
```

### Parametriserade queries (VIKTIGT för säkerhet!)

```csharp
command.CommandText = "INSERT INTO Customers (Name, Email) VALUES (@name, @email)";
command.Parameters.AddWithValue("@name", "Erik Svensson");
command.Parameters.AddWithValue("@email", "erik@example.com");
command.ExecuteNonQuery();
```

**Använd ALDRIG string concatenation för SQL!**
```csharp
// ❌ DÅLIGT - SQL injection-risk!
string sql = $"SELECT * FROM Users WHERE Username = '{username}'";

// ✅ BRA - Parametriserad query
command.CommandText = "SELECT * FROM Users WHERE Username = @username";
command.Parameters.AddWithValue("@username", username);
```

---

## 5. Entity Framework Core – paket, context och migrations

### Installera EF Core-paket

```bash
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef
```

### Skapa modeller

```csharp
public class Customer
{
    public int CustomerId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }
}
```

### Skapa DbContext

```csharp
using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbPath = Path.Combine(documentsPath, "MinApp", "app.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}
```

### Skapa och köra migrations

```bash
# Skapa migration
dotnet ef migrations add InitialCreate

# Uppdatera databasen
dotnet ef database update
```

EF Core skapar automatiskt tabeller baserat på dina modeller!

---

## 6. CRUD-exempel (SQL + C#)

### CREATE (INSERT)

**SQL:**
```sql
INSERT INTO Customers (Name, Email, Phone)
VALUES ('Lisa Larsson', 'lisa@example.com', '0701111111');
```

**C# med ADO.NET:**
```csharp
using (var connection = new SqliteConnection(connectionString))
{
    connection.Open();
    var command = connection.CreateCommand();
    command.CommandText = "INSERT INTO Customers (Name, Email, Phone) VALUES (@name, @email, @phone)";
    command.Parameters.AddWithValue("@name", "Lisa Larsson");
    command.Parameters.AddWithValue("@email", "lisa@example.com");
    command.Parameters.AddWithValue("@phone", "0701111111");
    command.ExecuteNonQuery();
}
```

**C# med EF Core:**
```csharp
using (var context = new AppDbContext())
{
    var customer = new Customer
    {
        Name = "Lisa Larsson",
        Email = "lisa@example.com",
        Phone = "0701111111"
    };

    context.Customers.Add(customer);
    context.SaveChanges();
}
```

### READ (SELECT)

**SQL:**
```sql
SELECT * FROM Customers WHERE Email LIKE '%example.com';
```

**C# med EF Core:**
```csharp
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Where(c => c.Email.EndsWith("example.com"))
        .ToList();

    foreach (var customer in customers)
    {
        Console.WriteLine($"{customer.Name} - {customer.Email}");
    }
}
```

### UPDATE

**SQL:**
```sql
UPDATE Customers SET Phone = '0709999999' WHERE CustomerId = 1;
```

**C# med EF Core:**
```csharp
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(1);
    if (customer != null)
    {
        customer.Phone = "0709999999";
        context.SaveChanges();
    }
}
```

### DELETE

**SQL:**
```sql
DELETE FROM Customers WHERE CustomerId = 3;
```

**C# med EF Core:**
```csharp
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(3);
    if (customer != null)
    {
        context.Customers.Remove(customer);
        context.SaveChanges();
    }
}
```

---

## 7. Backup och återställning

### Backup
SQLite är en fil – kopiera bara filen!

```csharp
// C# backup
string sourcePath = Path.Combine(documentsPath, "MinApp", "app.db");
string backupPath = Path.Combine(documentsPath, "MinApp", $"app_backup_{DateTime.Now:yyyyMMdd_HHmmss}.db");

File.Copy(sourcePath, backupPath);
```

### Återställning
Kopiera backup-filen tillbaka till originalplatsen.

### Export till SQL
I DB Browser:
1. File → Export → Database to SQL file
2. Spara som `.sql`

### Import från SQL
1. File → Import → Database from SQL file

---

## 8. TL;DR – Snabböversikt

| Vad                | Hur                                      |
|--------------------|------------------------------------------|
| Installera verktyg | DB Browser for SQLite                    |
| NuGet-paket        | `Microsoft.Data.Sqlite` + EF Core        |
| Connection string  | `Data Source=myapp.db`                   |
| Skapa tabell       | EF migrations eller SQL i DB Browser     |
| CRUD               | ADO.NET eller EF Core                    |
| Backup             | Kopiera `.db`-filen                      |

---

## 9. Vanliga fel och lösningar

### "SQLite Error 1: 'no such table'"
**Problem:** Tabellen finns inte.
**Lösning:** Kör migrations (`dotnet ef database update`) eller skapa tabellen manuellt.

### "database is locked"
**Problem:** Två anslutningar försöker skriva samtidigt.
**Lösning:** Använd `using`-statements för att stänga connections ordentligt.

### Filen skapas inte
**Problem:** Fel sökväg eller saknade rättigheter.
**Lösning:** Kontrollera `dbPath` och använd `Directory.CreateDirectory()`.

### EF migrations fungerar inte
**Problem:** `dotnet-ef` inte installerat.
**Lösning:** `dotnet tool install --global dotnet-ef`

---

## 10. Sammanfattning

Du har nu lärt dig:
✅ Vad SQLite är och när du ska använda det
✅ Installera DB Browser
✅ Skapa databaser och tabeller
✅ Ansluta från C# med ADO.NET
✅ Använda Entity Framework Core med SQLite
✅ CRUD-operationer
✅ Ta backup

**Nästa steg:** Kapitel 2 – MySQL, där vi går från filbaserad till serverdatabas!

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
