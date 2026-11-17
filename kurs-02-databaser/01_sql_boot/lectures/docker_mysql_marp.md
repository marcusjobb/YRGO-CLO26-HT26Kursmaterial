---
marp: true
theme: default
paginate: true
title: Docker + MySQL
description: Från lokal fil till serverbaserad databas — MySQL i en Docker-container med phpMyAdmin och C#
style: |
  section {
    font-size: 26px;
  }
  section.lead {
    text-align: center;
    justify-content: center;
  }
  section.lead h1 {
    font-size: 64px;
  }
  section.lead p {
    font-size: 28px;
  }
  table {
    font-size: 22px;
  }
  th {
    background: #2d3e50;
    color: #fff;
  }
  td strong {
    color: #d35400;
  }
  td:has(strong) {
    background: #fdebd0;
  }
  code {
    font-size: 0.9em;
  }
  blockquote {
    font-size: 22px;
    border-left: 6px solid #e67e22;
    color: #555;
  }
---

<!-- _class: lead -->
<!-- _paginate: false -->

# Docker + MySQL

SQLite lagrar data i en fil.
MySQL lever i en server.
Docker gör det enkelt att starta den.

---

## Var är vi nu?

```
Kurs 02 — Databaser
├── ✅ Databastyper
├── ✅ SQL-grunder (SELECT, INSERT, UPDATE, DELETE)
├── ✅ SQLite — filbaserad databas
├── ✅ Normalisering
└── 👉 MySQL — serverdatabas i Docker     ← idag
```

---

## SQLite vs MySQL

| | **SQLite** | **MySQL** |
|---|---|---|
| **Var** | En fil på disk | En process som körs |
| **Uppstart** | Omedelbart | Startas med Docker |
| **Användare** | En åt gången | Många samtidigt |
| **Passar för** | Lokalt, prototyper | Webb, backend, produktion |
| **Konfiguration** | Ingen | docker-compose.yml |

> Båda pratar SQL. Syntaxen är nästan identisk.

---

## Vad är Docker?

En container är som en **isolerad dator i datorn**.

```
Din dator
└── Docker
    ├── Container: mysql_dev   ← MySQL 8.0, port 3306
    └── Container: phpmyadmin  ← webbgränssnitt, port 8080
```

Inga systemförändringar. Ta bort containern → inget kvar.

---

## Installera Docker Desktop

1. Ladda ner: **docker.com/products/docker-desktop**
2. Installera med standardinställningar
3. Starta Docker Desktop
4. Verifiera:

```bash
docker --version
```

Du ska se något som: `Docker version 27.x.x`

---

## docker-compose.yml

Skapa en ny mapp, lägg in den här filen:

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
    ports:
      - "8080:80"
    depends_on:
      - mysql

volumes:
  mysql_data:
```

---

## Starta

```bash
docker-compose up -d
```

`-d` = detached — körs i bakgrunden

Vänta ~10 sekunder, sedan:

```bash
docker ps
```

Du ska se **mysql_dev** och **phpmyadmin_dev** med status `Up`.

---

## phpMyAdmin

Öppna **http://localhost:8080**

- **Username:** `appuser`
- **Password:** `apppassword`

Du ser databasen `myapp` i vänstermenyn.

Fliken **SQL** → kör kommandon direkt i webbläsaren.

---

## Skapa en tabell i phpMyAdmin

```sql
CREATE TABLE Customers (
    CustomerId INT AUTO_INCREMENT PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(20)
);

INSERT INTO Customers (Name, Email, Phone)
VALUES ('Anna Andersson', 'anna@example.com', '0701234567');

SELECT * FROM Customers;
```

---

## NuGet-paket

```bash
# ADO.NET
dotnet add package MySql.Data

# Entity Framework Core
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef
```

---

## Ansluta från C#

```csharp
using MySql.Data.MySqlClient;

string connectionString =
    "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;";

using var connection = new MySqlConnection(connectionString);
connection.Open();

var cmd = connection.CreateCommand();
cmd.CommandText = "SELECT * FROM Customers";

using var reader = cmd.ExecuteReader();
while (reader.Read())
    Console.WriteLine($"{reader.GetInt32("CustomerId")}: {reader.GetString("Name")}");
```

---

## DbContext med EF Core

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        string cs = "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;";
        options.UseMySql(cs, ServerVersion.AutoDetect(cs));
    }
}
```

```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

---

## Stanna och städa

```bash
# Stoppa containers (data bevaras)
docker-compose down

# Stoppa OCH radera all data
docker-compose down -v
```

> `down -v` tar bort volymen `mysql_data` — databasen är borta. Använd när du vill börja om.

---

## Vanliga fel

| Fel | Lösning |
|-----|---------|
| `Unable to connect` | Kör `docker-compose up -d`, vänta 10 sek |
| `Access denied` | Kontrollera lösenord i connection string |
| Port 3306 används | Ändra till `"3307:3306"` i compose-filen |

---

## Sammanfattning

```
SQLite    → en fil, perfekt för lokal dev
MySQL     → en server, hanterar många användare
Docker    → startar servern utan att installera något
phpMyAdmin → webbgränssnitt för att inspektera databasen
EF Core   → samma kod som med SQLite, annat paket
```

> Syntaxen i SQL är nästan identisk — det är infrastrukturen som skiljer.

---

<!-- _class: lead -->

# Hands-on

Skapa `docker-compose.yml`, starta containers,
logga in i phpMyAdmin och skapa er första tabell.

**Ni vet redan SQL — nu har ni en riktig server att prata med.**
