# Kapitel 2 – MySQL: den klassiska serversidan

🟢


## 1. Vad är MySQL?

MySQL är en **serverdatabas** – till skillnad från SQLite finns det en process som körs och lyssnar på nätverksanrop. MySQL är en av världens mest använda databaser.

### Fördelar
✅ Hanterar många samtidiga användare
✅ Kraftfull och skalbar
✅ Open source (gratis)
✅ Stort community och många verktyg
✅ Fungerar bra med PHP (LAMP-stack)

### Nackdelar
❌ Kräver server (lokal eller fjärr)
❌ Mer komplex setup än SQLite
❌ Kräver underhåll (backups, uppdateringar)

### Användningsområden
- Webbapplikationer
- Content Management Systems (WordPress, Drupal)
- E-handelssystem
- När flera användare behöver tillgång samtidigt

---

## 2. Installation av verktyg och Docker

### 2.1 Installera Docker Desktop

1. Ladda ner Docker Desktop från https://www.docker.com/products/docker-desktop
2. Installera med standardinställningar
3. Starta Docker Desktop
4. Verifiera: Öppna terminal och kör `docker --version`

### 2.2 Starta MySQL + phpMyAdmin med `docker-compose`

Skapa en fil `docker-compose.yml`:

```yaml
version: '3.8'

services:
  mysql:
    image: mysql:8.0
    container_name: mysql_dev
    environment:
      MYSQL_ROOT_PASSWORD: rootpassword
      MYSQL_DATABASE: myapp
      MYSQL_USER: appuser
      MYSQL_PASSWORD: apppassword
    ports:
      - "3306:3306"
    volumes:
      - mysql_data:/var/lib/mysql

  phpmyadmin:
    image: phpmyadmin:latest
    container_name: phpmyadmin_dev
    environment:
      PMA_HOST: mysql
      PMA_PORT: 3306
    ports:
      - "8080:80"
    depends_on:
      - mysql

volumes:
  mysql_data:
```

Starta containers:

```bash
docker-compose up -d
```

**Förklaring:**
- `mysql_dev`: MySQL-server på port 3306
- `phpmyadmin_dev`: Webbaserat admin-gränssnitt på http://localhost:8080
- `mysql_data`: Volym som lagrar data persistent (överlever omstart)

### 2.3 Kontrollera att phpMyAdmin fungerar

1. Öppna webbläsare
2. Gå till http://localhost:8080
3. Logga in:
   - **Server:** `mysql`
   - **Username:** `appuser`
   - **Password:** `apppassword`

Du bör se databasen `myapp` i vänstermenyn.

---

## 3. Kom igång med MySQL-shell eller phpMyAdmin

### Använda phpMyAdmin

1. Klicka på databasen `myapp`
2. Gå till fliken "SQL"
3. Kör SQL-kommandon direkt

### Använda MySQL-shell (CLI)

```bash
docker exec -it mysql_dev mysql -u appuser -p
# Ange lösenord: apppassword
```

Nu är du inne i MySQL-shellen:

```sql
USE myapp;

CREATE TABLE Customers (
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(20)
);

SHOW TABLES;

INSERT INTO Customers (Name, Email, Phone)
VALUES ('Anna Andersson', 'anna@example.com', '0701234567');

SELECT * FROM Customers;
```

Avsluta med `EXIT;`

---

## 4. Anslutning med C#

### Installera NuGet-paket

```bash
dotnet add package MySql.Data
# Eller för Entity Framework:
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

### Basic connection (ADO.NET)

```csharp
using MySql.Data.MySqlClient;

string connectionString = "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;";

using (var connection = new MySqlConnection(connectionString))
{
    connection.Open();

    var command = connection.CreateCommand();
    command.CommandText = "SELECT * FROM Customers";

    using (var reader = command.ExecuteReader())
    {
        while (reader.Read())
        {
            var id = reader.GetInt32("CustomerId");
            var name = reader.GetString("Name");
            var email = reader.GetString("Email");

            Console.WriteLine($"{id}: {name} ({email})");
        }
    }
}
```

### Parametriserade queries

```csharp
command.CommandText = "INSERT INTO Customers (Name, Email, Phone) VALUES (@name, @email, @phone)";
command.Parameters.AddWithValue("@name", "Erik Svensson");
command.Parameters.AddWithValue("@email", "erik@example.com");
command.Parameters.AddWithValue("@phone", "0702345678");
command.ExecuteNonQuery();
```

---

## 5. Entity Framework Core – installation och migrering

### Installera paket

```bash
dotnet add package Pomelo.EntityFrameworkCore.MySql
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
        string connectionString = "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );
    }
}
```

### Skapa migrations

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

EF Core skapar automatiskt `Customers`-tabellen i MySQL!

---

## 6. CRUD-exempel (SQL + C#)

### CREATE (INSERT)

**SQL:**
```sql
INSERT INTO Customers (Name, Email, Phone)
VALUES ('Lisa Larsson', 'lisa@example.com', '0701111111');
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
SELECT * FROM Customers WHERE Email LIKE '%@example.com';
```

**C# med EF Core:**
```csharp
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Where(c => c.Email.EndsWith("@example.com"))
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

## 7. Backup och export (`mysqldump`)

### Backup via Docker

```bash
docker exec mysql_dev mysqldump -u appuser -p myapp > backup.sql
# Ange lösenord när du blir tillfrågad
```

Detta skapar en `.sql`-fil med all data och struktur.

### Återställ från backup

```bash
docker exec -i mysql_dev mysql -u appuser -papppassword myapp < backup.sql
```

### Export via phpMyAdmin

1. Gå till http://localhost:8080
2. Välj databasen `myapp`
3. Klicka på fliken "Export"
4. Välj "Quick" eller "Custom"
5. Klicka "Go"

En `.sql`-fil laddas ner.

### Import via phpMyAdmin

1. Gå till fliken "Import"
2. Välj din `.sql`-fil
3. Klicka "Go"

---

## 8. TL;DR – Snabböversikt

| Vad                | Hur                                      |
|--------------------|------------------------------------------|
| Installera         | Docker + `docker-compose.yml`            |
| Starta             | `docker-compose up -d`                   |
| Admin-gränssnitt   | http://localhost:8080 (phpMyAdmin)       |
| NuGet-paket        | `Pomelo.EntityFrameworkCore.MySql`       |
| Connection string  | `Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;` |
| CRUD               | EF Core eller ADO.NET                    |
| Backup             | `mysqldump` eller phpMyAdmin export      |

---

## 9. Vanliga fel och lösningar

### "Unable to connect to any of the specified MySQL hosts"
**Problem:** MySQL-servern körs inte.
**Lösning:** Kör `docker-compose up -d` och vänta 10 sekunder.

### "Access denied for user"
**Problem:** Fel lösenord eller användare.
**Lösning:** Kontrollera connection string och `docker-compose.yml`.

### "Unknown database 'myapp'"
**Problem:** Databasen skapades inte.
**Lösning:** Kontrollera `MYSQL_DATABASE` i `docker-compose.yml` eller skapa manuellt:
```sql
CREATE DATABASE myapp;
```

### EF migrations fungerar inte
**Problem:** Fel connection string eller server inte startad.
**Lösning:** Verifiera med `docker ps` att containern körs.

### Port 3306 redan används
**Problem:** Annan MySQL-instans kör på samma port.
**Lösning:** Ändra porten i `docker-compose.yml`:
```yaml
ports:
  - "3307:3306"
```
Uppdatera connection string till `Port=3307`.

---

## 10. Sammanfattning

Du har nu lärt dig:
✅ Vad MySQL är och när du ska använda det
✅ Installera MySQL med Docker
✅ Använda phpMyAdmin
✅ Ansluta från C# med ADO.NET och EF Core
✅ CRUD-operationer
✅ Ta backup med `mysqldump`

**Nästa steg:** Kapitel 3 – LocalDB och SQL Server, Microsofts databas!

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
