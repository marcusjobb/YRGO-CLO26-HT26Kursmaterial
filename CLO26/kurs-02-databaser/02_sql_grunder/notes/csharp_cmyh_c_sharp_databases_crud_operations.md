---

title: CRUD-operationer
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/crud_operations.md"
description: "CRUD-operationer utgör grunden för all databasinteraktion och representerar de fyra grundläggande funktionerna för persistent lagring: Create (skapa), Read (läsa), Update (uppdatera) och Delete (ta bo"
tags: ["crud", "crud-operationer", "csharp", "databaser", "git", "operations", "sql"]
week_fit: []
---

# CRUD-operationer

🔴


CRUD-operationer utgör grunden för all databasinteraktion och representerar de fyra grundläggande funktionerna för persistent lagring: Create (skapa), Read (läsa), Update (uppdatera) och Delete (ta bort). Dessa operationer motsvarar SQL-kommandona INSERT, SELECT, UPDATE och DELETE, och tillsammans ger de utvecklare all funktionalitet som behövs för att hantera data i relationsdatabaser.

## Innehållsförteckning

- [Introduktion](#introduktion)
- [Create - Skapa data](#create---skapa-data)
- [Read - Läsa data](#read---läsa-data)
- [Update - Uppdatera data](#update---uppdatera-data)
- [Delete - Ta bort data](#delete---ta-bort-data)
- [Avancerade CRUD-tekniker](#avancerade-crud-tekniker)
- [Transaktioner och CRUD](#transaktioner-och-crud)
- [CRUD med relationer](#crud-med-relationer)
- [Prestanda och optimering](#prestanda-och-optimering)
- [Säkerhet i CRUD-operationer](#säkerhet-i-crud-operationer)
- [Praktiska exempel](#praktiska-exempel)
- [Slutsats](#slutsats)
- [TL;DR](#tldr)

## Introduktion

CRUD-operationer är det universella språket för databasmanipulation. Oavsett vilket databassystem eller programmeringsspråk du använder, kommer du alltid att arbeta med dessa fyra grundläggande operationer:

- **Create (INSERT)**: Lägga till ny data
- **Read (SELECT)**: Hämta och visa data
- **Update (UPDATE)**: Modifiera befintlig data
- **Delete (DELETE)**: Ta bort data

Dessa operationer bildar ryggraden i alla databasdrivna applikationer, från enkla webbsidor till komplexa enterprise-system.

## Create - Skapa data

CREATE-operationer använder SQL-kommandot INSERT för att lägga till ny data i databastabeller.

### Grundläggande INSERT

```sql
-- Skapa tabell först
CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY,
    Username VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE,
    CreatedDate DATE DEFAULT CURRENT_DATE
);

-- Enkel INSERT
INSERT INTO Users (Username, Email)
VALUES ('anna_dev', 'anna@example.com');

-- INSERT med alla kolumner specificerade
INSERT INTO Users (UserId, Username, Email, CreatedDate)
VALUES (1, 'erik_admin', 'erik@example.com', '2024-01-15');
```

### Flera rader samtidigt

```sql
-- Sätt in flera användare i en operation
INSERT INTO Users (Username, Email) VALUES
    ('lisa_designer', 'lisa@example.com'),
    ('johan_tester', 'johan@example.com'),
    ('sara_pm', 'sara@example.com');
```

### INSERT med SELECT (kopiering av data)

```sql
-- Skapa backup-tabell
CREATE TABLE Users_Backup AS SELECT * FROM Users WHERE 1=0; -- Tom kopia av struktur

-- Kopiera aktiva användare till backup
INSERT INTO Users_Backup (Username, Email, CreatedDate)
SELECT Username, Email, CreatedDate
FROM Users
WHERE CreatedDate >= '2024-01-01';
```

### Hantering av AUTO_INCREMENT/PRIMARY KEY

```sql
-- SQLite med AUTOINCREMENT
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2)
);

-- Sätt in utan att specificera ID
INSERT INTO Products (ProductName, Price)
VALUES ('Laptop', 15999.99);

-- Hämta senast skapade ID
SELECT last_insert_rowid(); -- SQLite
```

## Read - Läsa data

READ-operationer använder SELECT för att hämta data från databaser. Detta är ofta den mest använda operationen.

### Grundläggande SELECT

```sql
-- Hämta alla kolumner och rader
SELECT * FROM Users;

-- Hämta specifika kolumner
SELECT Username, Email FROM Users;

-- Hämta med villkor
SELECT Username, Email
FROM Users
WHERE CreatedDate >= '2024-01-01';
```

### Filtrering med WHERE

```sql
-- Exakta matchningar
SELECT * FROM Users WHERE Username = 'anna_dev';

-- Textmönster
SELECT * FROM Users WHERE Email LIKE '%@gmail.com';
SELECT * FROM Users WHERE Username LIKE 'a%'; -- Börjar med 'a'

-- Numeriska jämförelser
SELECT * FROM Products WHERE Price BETWEEN 1000 AND 5000;
SELECT * FROM Products WHERE Price >= 500;

-- Datum/tid filtrering
SELECT * FROM Users WHERE CreatedDate > '2024-01-01';
SELECT * FROM Users WHERE CreatedDate = CURRENT_DATE;

-- Kombinerade villkor
SELECT * FROM Users
WHERE CreatedDate >= '2024-01-01'
AND Email LIKE '%@company.com';

-- Negation
SELECT * FROM Users WHERE Email NOT LIKE '%@spam.com';
SELECT * FROM Products WHERE Price IS NOT NULL;
```

### Sortering och begränsning

```sql
-- Sortera resultat
SELECT Username, Email FROM Users ORDER BY Username ASC;
SELECT ProductName, Price FROM Products ORDER BY Price DESC;

-- Flera sorteringskolumner
SELECT Username, Email, CreatedDate
FROM Users
ORDER BY CreatedDate DESC, Username ASC;

-- Begränsa antal resultat
SELECT * FROM Products ORDER BY Price DESC LIMIT 5; -- Dyraste 5 produkterna

-- Pagination med OFFSET
SELECT * FROM Users
ORDER BY UserId
LIMIT 10 OFFSET 20; -- Hoppa över 20, hämta nästa 10
```

### Aggregatfunktioner

```sql
-- Räkna rader
SELECT COUNT(*) FROM Users;
SELECT COUNT(*) AS TotalProducts FROM Products WHERE Price > 1000;

-- Matematiska funktioner
SELECT
    AVG(Price) AS AveragePrice,
    MIN(Price) AS CheapestPrice,
    MAX(Price) AS MostExpensive,
    SUM(Price) AS TotalValue
FROM Products;

-- Gruppering
SELECT
    LEFT(Email, INSTR(Email, '@') - 1) AS Domain,
    COUNT(*) AS UserCount
FROM Users
WHERE Email IS NOT NULL
GROUP BY LEFT(Email, INSTR(Email, '@') - 1)
HAVING COUNT(*) > 1;
```

## Update - Uppdatera data

UPDATE-operationer modifierar befintlig data i databastabeller.

### Grundläggande UPDATE

```sql
-- Uppdatera en specifik rad
UPDATE Users
SET Email = 'anna.developer@newcompany.com'
WHERE Username = 'anna_dev';

-- Uppdatera flera kolumner
UPDATE Products
SET Price = 13999.99, ProductName = 'Gaming Laptop'
WHERE ProductId = 1;
```

### Villkorlig uppdatering

```sql
-- Uppdatera baserat på flera villkor
UPDATE Products
SET Price = Price * 0.9  -- 10% rabatt
WHERE Price > 5000 AND ProductName LIKE '%Laptop%';

-- Uppdatera med beräkning
UPDATE Users
SET Username = LOWER(Username)
WHERE CreatedDate < '2024-01-01';

-- Uppdatera med subquery
UPDATE Products
SET Price = (
    SELECT AVG(Price) * 1.1
    FROM Products AS p2
    WHERE p2.CategoryId = Products.CategoryId
)
WHERE Price IS NULL;
```

### Säker uppdatering

```sql
-- ALLTID kontrollera först vad som kommer att uppdateras
SELECT * FROM Products WHERE Price > 10000;

-- Sedan kör uppdateringen
UPDATE Products
SET Price = Price * 0.95
WHERE Price > 10000;

-- Kontrollera resultatet
SELECT * FROM Products WHERE Price > 9500;
```

### UPDATE med JOIN (om stöds)

```sql
-- Uppdatera baserat på data från annan tabell
UPDATE Products
SET Price = Price * 1.1
WHERE CategoryId IN (
    SELECT CategoryId
    FROM Categories
    WHERE CategoryName = 'Premium'
);
```

## Delete - Ta bort data

DELETE-operationer tar bort data från databastabeller.

### Grundläggande DELETE

```sql
-- Ta bort specifik rad
DELETE FROM Users WHERE UserId = 5;

-- Ta bort baserat på villkor
DELETE FROM Products WHERE Price < 100;

-- Ta bort med textfilter
DELETE FROM Users WHERE Email LIKE '%@temporaryemail.com';
```

### Villkorlig borttagning

```sql
-- Ta bort gamla poster
DELETE FROM Users WHERE CreatedDate < '2023-01-01';

-- Ta bort baserat på flera villkor
DELETE FROM Products
WHERE Price < 50
AND ProductName LIKE '%deprecated%';

-- Ta bort med subquery
DELETE FROM Users
WHERE UserId IN (
    SELECT UserId FROM UserSessions
    WHERE LastLogin < '2023-01-01'
);
```

### Säker borttagning

```sql
-- ALLTID kontrollera först vad som kommer tas bort
SELECT * FROM Users WHERE CreatedDate < '2023-01-01';
SELECT COUNT(*) FROM Users WHERE CreatedDate < '2023-01-01';

-- Sedan ta bort
DELETE FROM Users WHERE CreatedDate < '2023-01-01';

-- Kontrollera resultatet
SELECT COUNT(*) FROM Users;
```

### Soft delete vs Hard delete

```sql
-- Lägg till kolumn för soft delete
ALTER TABLE Users ADD COLUMN IsDeleted BOOLEAN DEFAULT FALSE;

-- Soft delete (markera som borttagen)
UPDATE Users
SET IsDeleted = TRUE
WHERE UserId = 5;

-- Hämta endast aktiva användare
SELECT * FROM Users WHERE IsDeleted = FALSE OR IsDeleted IS NULL;

-- Hard delete (faktisk borttagning)
DELETE FROM Users WHERE IsDeleted = TRUE AND CreatedDate < '2023-01-01';
```

## Avancerade CRUD-tekniker

### UPSERT (INSERT eller UPDATE)

```sql
-- SQLite UPSERT syntax
INSERT INTO Users (UserId, Username, Email)
VALUES (1, 'admin', 'admin@example.com')
ON CONFLICT(UserId) DO UPDATE SET
    Email = excluded.Email,
    Username = excluded.Username;

-- Alternativ med INSERT OR REPLACE
INSERT OR REPLACE INTO Users (UserId, Username, Email)
VALUES (1, 'admin', 'newemail@example.com');
```

### Batch-operationer

```sql
-- Batch INSERT
INSERT INTO Products (ProductName, Price, CategoryId) VALUES
    ('Product A', 299.99, 1),
    ('Product B', 399.99, 1),
    ('Product C', 199.99, 2),
    ('Product D', 599.99, 2);

-- Batch UPDATE
UPDATE Products
SET Price = CASE
    WHEN CategoryId = 1 THEN Price * 1.1
    WHEN CategoryId = 2 THEN Price * 1.05
    ELSE Price
END;
```

### Returning clausul (om stöds)

```sql
-- Returnera data från INSERT (PostgreSQL syntax som exempel)
INSERT INTO Users (Username, Email)
VALUES ('new_user', 'new@example.com')
RETURNING UserId, CreatedDate;

-- SQLite alternativ - använd triggers eller separata SELECT
```

## Transaktioner och CRUD

### Grundläggande transaktioner

```sql
-- Start transaktion
BEGIN TRANSACTION;

-- CRUD-operationer
INSERT INTO Orders (UserId, TotalAmount) VALUES (1, 299.99);
UPDATE Products SET Stock = Stock - 1 WHERE ProductId = 5;
INSERT INTO OrderItems (OrderId, ProductId, Quantity)
VALUES (last_insert_rowid(), 5, 1);

-- Bekräfta ändringar
COMMIT;

-- Eller rulla tillbaka vid fel
-- ROLLBACK;
```

### Felhantering i transaktioner

```sql
BEGIN TRANSACTION;

-- Kontrollera att produkten finns i lager
SELECT Stock FROM Products WHERE ProductId = 5;

-- Om Stock > 0, fortsätt
INSERT INTO Orders (UserId, TotalAmount) VALUES (1, 299.99);

-- Kontrollera att ordern skapades
SELECT * FROM Orders WHERE OrderId = last_insert_rowid();

-- Om allt OK
COMMIT;
-- Annars ROLLBACK;
```

## CRUD med relationer

### Foreign Key-hantering

```sql
-- Skapa tabeller med relationer
CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY,
    CategoryName VARCHAR(50) NOT NULL
);

CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    CategoryId INTEGER,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);

-- Sätt in med relationer
INSERT INTO Categories (CategoryName) VALUES ('Electronics');
INSERT INTO Products (ProductName, CategoryId)
VALUES ('Smartphone', 1);
```

### Kaskaderande operationer

```sql
-- UPDATE med CASCADE
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    ProductName VARCHAR(100) NOT NULL,
    CategoryId INTEGER,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
        ON UPDATE CASCADE  -- Uppdatera automatiskt
        ON DELETE SET NULL -- Sätt till NULL vid borttagning
);
```

## Prestanda och optimering

### Index för snabbare CRUD

```sql
-- Skapa index för ofta använda kolumner
CREATE INDEX idx_users_email ON Users(Email);
CREATE INDEX idx_products_price ON Products(Price);
CREATE INDEX idx_users_created ON Users(CreatedDate);

-- Compound index för flera kolumner
CREATE INDEX idx_products_category_price ON Products(CategoryId, Price);
```

### Optimerade frågor

```sql
-- Använd specifika kolumner istället för *
SELECT Username, Email FROM Users WHERE UserId = 1;

-- Använd LIMIT för stora dataset
SELECT * FROM Products ORDER BY Price DESC LIMIT 100;

-- Använd EXISTS istället för IN för stora subqueries
SELECT * FROM Users u
WHERE EXISTS (
    SELECT 1 FROM Orders o WHERE o.UserId = u.UserId
);
```

## Säkerhet i CRUD-operationer

### Parameteriserade frågor

```sql
-- FARLIGT - SQL Injection risk
-- "SELECT * FROM Users WHERE Username = '" + userInput + "'"

-- SÄKERT - Använd parametrar (syntax varierar per språk/driver)
-- "SELECT * FROM Users WHERE Username = ?"
```

### Behörighetshantering

```sql
-- Skapa användare med begränsade rättigheter
-- CREATE USER 'app_user'@'localhost' IDENTIFIED BY 'strong_password';
-- GRANT SELECT, INSERT, UPDATE ON mydb.Users TO 'app_user'@'localhost';
-- GRANT DELETE ON mydb.Sessions TO 'app_user'@'localhost';
```

### Validering innan CRUD

```sql
-- Validera data innan insert
SELECT
    CASE
        WHEN LENGTH(Username) < 3 THEN 'Username too short'
        WHEN Email NOT LIKE '%@%.%' THEN 'Invalid email'
        ELSE 'Valid'
    END AS ValidationResult
FROM (SELECT 'ab' AS Username, 'invalid-email' AS Email);
```

## Praktiska exempel

### Komplett e-handelssystem CRUD

```sql
-- Skapa tabeller
CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName VARCHAR(100) NOT NULL,
    Price DECIMAL(10,2) NOT NULL,
    Stock INTEGER DEFAULT 0,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    OrderDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    Status VARCHAR(20) DEFAULT 'pending',
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- CREATE - Lägg till ny kund och order
BEGIN TRANSACTION;

INSERT INTO Customers (FirstName, LastName, Email)
VALUES ('Anna', 'Andersson', 'anna@example.com');

INSERT INTO Products (ProductName, Price, Stock)
VALUES ('Laptop Pro', 15999.99, 10);

INSERT INTO Orders (CustomerId)
VALUES (last_insert_rowid());

COMMIT;

-- READ - Hämta orderhistorik för kund
SELECT
    c.FirstName + ' ' + c.LastName AS CustomerName,
    o.OrderId,
    o.OrderDate,
    o.Status
FROM Orders o
JOIN Customers c ON o.CustomerId = c.CustomerId
WHERE c.Email = 'anna@example.com'
ORDER BY o.OrderDate DESC;

-- UPDATE - Uppdatera orderstatus
UPDATE Orders
SET Status = 'shipped'
WHERE OrderId = 1 AND Status = 'pending';

-- UPDATE - Minska lagersaldo efter försäljning
UPDATE Products
SET Stock = Stock - 1
WHERE ProductId = 1 AND Stock > 0;

-- DELETE - Ta bort gamla, avbrutna ordrar
DELETE FROM Orders
WHERE Status = 'cancelled'
AND OrderDate < DATE('now', '-6 months');

-- READ - Kontrollera lagerstatuß
SELECT
    ProductName,
    Price,
    Stock,
    CASE
        WHEN Stock = 0 THEN 'Out of Stock'
        WHEN Stock < 10 THEN 'Low Stock'
        ELSE 'In Stock'
    END AS StockStatus
FROM Products
ORDER BY Stock ASC;
```

### Användarhantering med soft delete

```sql
-- Skapa användartabell med soft delete
CREATE TABLE Users (
    UserId INTEGER PRIMARY KEY AUTOINCREMENT,
    Username VARCHAR(50) UNIQUE NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL,
    IsActive BOOLEAN DEFAULT TRUE,
    IsDeleted BOOLEAN DEFAULT FALSE,
    CreatedDate DATETIME DEFAULT CURRENT_TIMESTAMP,
    LastLogin DATETIME,
    DeletedDate DATETIME
);

-- CREATE - Ny användare
INSERT INTO Users (Username, Email)
VALUES ('johndoe', 'john@example.com');

-- READ - Endast aktiva användare
SELECT Username, Email, CreatedDate
FROM Users
WHERE IsDeleted = FALSE;

-- UPDATE - Inaktivera användare
UPDATE Users
SET IsActive = FALSE
WHERE Username = 'johndoe';

-- UPDATE - Logga in användare
UPDATE Users
SET LastLogin = CURRENT_TIMESTAMP
WHERE Username = 'johndoe' AND IsActive = TRUE;

-- DELETE - Soft delete
UPDATE Users
SET IsDeleted = TRUE, DeletedDate = CURRENT_TIMESTAMP
WHERE Username = 'johndoe';

-- DELETE - Permanent borttagning av gamla soft-deleted användare
DELETE FROM Users
WHERE IsDeleted = TRUE
AND DeletedDate < DATE('now', '-2 years');
```

## Slutsats

CRUD-operationer är fundamentala för all databasinteraktion och utgör grunden för datahantering i applikationer. Genom att behärska CREATE (INSERT), READ (SELECT), UPDATE och DELETE kan utvecklare effektivt hantera data i relationsdatabaser. Viktigt att komma ihåg är säkerhetsaspekter som parameteriserade frågor, prestandaoptimering genom index och lämpliga transaktioner, samt att alltid testa och validera data innan operationer utförs.

## TL;DR

CRUD = **C**reate (INSERT), **R**ead (SELECT), **U**pdate (UPDATE), **D**elete (DELETE). **CREATE**: `INSERT INTO table (columns) VALUES (data)` lägger till ny data. **READ**: `SELECT columns FROM table WHERE condition` hämtar data, använd WHERE för filtrering, ORDER BY för sortering, LIMIT för pagination. **UPDATE**: `UPDATE table SET column=value WHERE condition` ändrar data - ALLTID använd WHERE. **DELETE**: `DELETE FROM table WHERE condition` tar bort data - kontrollera med SELECT först. Använd transaktioner (BEGIN/COMMIT/ROLLBACK) för flera operationer. Soft delete (markera IsDeleted=TRUE) istället för hard delete för säkerhet. Parameteriserade frågor mot SQL injection. Index för prestanda.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
