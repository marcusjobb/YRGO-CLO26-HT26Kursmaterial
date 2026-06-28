---

title: Workshop: Rädda Kaos-Databasen
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/01_radda_kaos_databasen.md"
description: "Ni har blivit anlitade som databaskonsulter för att rädda en webbshops databas som är på gränsen till kollaps. Den ursprungliga utvecklaren är borta och databasen har växt vild med dålig design som nu"
tags: ["databasen", "databaser", "exercise", "kaos", "kaos-databasen", "radda", "rädda", "sql", "ssh", "visual-studio"]
week_fit: []
---

# Workshop: Rädda Kaos-Databasen

🟢


## Bakgrund

Ni har blivit anlitade som databaskonsulter för att rädda en webbshops databas som är på gränsen till kollaps. Den ursprungliga utvecklaren är borta och databasen har växt vild med dålig design som nu orsakar problem dagligen.

## Scenario

**WildShop AB** - en onlinebutik för utomhusutrustning - har en databas som innehåller:
- 5,000+ kunder
- 20,000+ ordrar
- 150+ produkter

**Problem de upplever:**
- Uppdateringar tar alldeles för lång tid
- Data blir inkonsistent (samma kund har olika adresser)
- När de tar bort produkter försvinner viktiga försäljningsdata
- Nya produkter kan inte läggas till utan att skapa fake-ordrar

## Del 1: Analysera Kaos-Databasen (45 min)

### Uppgift 1.1: Granska den befintliga strukturen

Här är den nuvarande databas-strukturen:

```sql

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY AUTO_INCREMENT,
    OrderDate DATE,

    -- Kunduppgifter (upprepas för varje order!)
    CustomerName VARCHAR(100),
    CustomerEmail VARCHAR(100),
    CustomerPhone VARCHAR(20),
    CustomerAddress VARCHAR(200),
    CustomerCity VARCHAR(100),
    CustomerZipCode VARCHAR(10),

    -- Produktuppgifter (upprepas för varje order!)
    ProductName VARCHAR(100),
    ProductDescription TEXT,
    ProductCategory VARCHAR(50),
    ProductPrice DECIMAL(10,2),
    ProductSKU VARCHAR(50),

    -- Orderuppgifter
    Quantity INT,
    TotalAmount DECIMAL(10,2),
    PaymentMethod VARCHAR(50),
    ShippingMethod VARCHAR(50),
    OrderStatus VARCHAR(50)
);
```

**Era uppgifter:**

1. **Identifiera normaliseringsbrott**: Vilka normaliseringsregler (1NF, 2NF, 3NF) bryts? Lista alla problem ni hittar.

2. **Dokumentera anomalier**: För varje typ av anomali (insertion, update, deletion), ge konkreta exempel på vad som kan gå fel.

3. **Skapa testdata**: Skriv INSERT-satser som visar problemen i praktiken. Skapa minst:
   - 3 kunder
   - 5 ordrar (med upprepade kunduppgifter)
   - 3 produkter (med upprepade produktuppgifter)

### Lösningsförslag (Dolt - öppna först efter ni försökt själva)

<details>
<summary>Klicka för att se lösning</summary>

**Normaliseringsbrott:**

1. **1NF-brott (Atomära värden)**:
   - Om flera produkter i samma order lagras som kommaseparerad lista

2. **2NF-brott (Partiella beroenden)**:
   - Produktinformation (ProductName, ProductDescription, etc.) beror bara på ProductSKU, inte hela primary key

3. **3NF-brott (Transitiva beroenden)**:
   - CustomerCity och CustomerZipCode beror på CustomerAddress, inte direkt på OrderID
   - ProductCategory beror på ProductName, inte direkt på OrderID

**Anomalier:**

*Insertion Anomaly:*
```sql
-- Kan inte lägga till produkt utan order
INSERT INTO Orders (ProductName, ProductSKU, ProductPrice)
VALUES ('Tält 4-person', 'TENT-4P-001', 3500);
-- MISSLYCKAS: CustomerName, OrderDate etc. är required
```

*Update Anomaly:*
```sql
-- Om Anna byter email måste ALLA hennes ordrar uppdateras
UPDATE Orders
SET CustomerEmail = 'anna.ny@email.com'
WHERE CustomerName = 'Anna Svensson';
-- RISK: Om vi missar en order har Anna olika emails!
```

*Deletion Anomaly:*
```sql
-- Ta bort Annas sista order
DELETE FROM Orders WHERE OrderID = 123;
-- FÖRLUST: All info om Anna försvinner!
```

**Testdata:**
```sql
INSERT INTO Orders VALUES
(1, '2024-01-10',
    'Anna Svensson', 'anna@email.com', '0701234567',
    'Storgatan 1', 'Göteborg', '41301',
    'Tält 4-person', 'Vattentätt familjetält', 'Camping', 3500.00, 'TENT-4P-001',
    1, 3500.00, 'Kort', 'PostNord', 'Levererad'),

(2, '2024-01-15',
    'Anna Svensson', 'anna@email.com', '0701234567',
    'Storgatan 1', 'Göteborg', '41301',
    'Sovsäck -10C', 'Varm vintersongsäck', 'Camping', 899.00, 'SLEEP-W10-002',
    2, 1798.00, 'Kort', 'PostNord', 'Skickad'),

(3, '2024-01-20',
    'Erik Andersson', 'erik@company.se', '0709876543',
    'Kungsgatan 5', 'Stockholm', '11143',
    'Tält 4-person', 'Vattentätt familjetält', 'Camping', 3500.00, 'TENT-4P-001',
    1, 3500.00, 'Faktura', 'DHL', 'Behandlas'),

(4, '2024-01-22',
    'Anna Svensson', 'anna@email.com', '0701234567',
    'Storgatan 1', 'Göteborg', '41301',
    'Ryggsäck 65L', 'Trekkingryggsäck', 'Vandring', 1299.00, 'PACK-65L-003',
    1, 1299.00, 'Swish', 'PostNord', 'Levererad'),

(5, '2024-01-25',
    'Erik Andersson', 'erik@company.se', '0709876543',
    'Kungsgatan 5', 'Stockholm', '11143',
    'Sovsäck -10C', 'Varm vintersovsäck', 'Camping', 899.00, 'SLEEP-W10-002',
    3, 2697.00, 'Faktura', 'DHL', 'Levererad');
```

**Problem demonstrerade:**
- Anna's uppgifter upprepas 3 gånger (redundans)
- Produktinfo upprepas (Tält och Sovsäck förekommer flera gånger)
- Om vi ändrar produktpris måste alla gamla ordrar uppdateras
- Om vi tar bort order 4 förlorar vi inte info om Anna (bra), men om vi tar bort order 1, 2 OCH 4 förlorar vi henne!

</details>

---

## Del 2: Designa Räddningen (60 min)

### Uppgift 2.1: Skapa ER-diagram

Rita ett Entity-Relationship diagram för den nya, normaliserade strukturen.

**Inkludera följande entiteter:**
- Customers
- Products
- Orders
- OrderDetails (kopplingstabell)

**För varje entitet:**
- Lista alla attribut
- Markera primary keys
- Markera foreign keys
- Visa kardinalitet (1:1, 1:N, M:N)

**Tips:** Använd draw.io, Lucidchart, eller penna och papper. Ta foto om ni ritar för hand.

### Uppgift 2.2: Skriv CREATE TABLE-satser

Baserat på ert ER-diagram, skriv SQL för att skapa den normaliserade strukturen.

**Krav:**
- Alla tabeller ska ha lämpliga primary keys
- Använd FOREIGN KEY constraints
- Lämpliga datatyper för varje kolumn
- Lägg till INDEX där det behövs för prestanda

**Mall att utgå från:**

```sql
-- Customers table
CREATE TABLE Customers (
    -- Fyll i...
);

-- Products table
CREATE TABLE Products (
    -- Fyll i...
);

-- Orders table
CREATE TABLE Orders (
    -- Fyll i...
);

-- OrderDetails (kopplingstabell)
CREATE TABLE OrderDetails (
    -- Fyll i...
);
```

### Lösningsförslag (Dolt)

<details>
<summary>Klicka för att se lösning</summary>

```sql
-- Customers table
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    Phone VARCHAR(20),
    Address VARCHAR(200),
    City VARCHAR(100),
    ZipCode VARCHAR(10),
    RegistrationDate DATE DEFAULT CURRENT_DATE,
    INDEX idx_email (Email),
    INDEX idx_name (Name)
);

-- Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY AUTO_INCREMENT,
    SKU VARCHAR(50) NOT NULL UNIQUE,
    Name VARCHAR(100) NOT NULL,
    Description TEXT,
    Category VARCHAR(50),
    Price DECIMAL(10,2) NOT NULL,
    StockQuantity INT DEFAULT 0,
    IsActive BOOLEAN DEFAULT TRUE,
    INDEX idx_sku (SKU),
    INDEX idx_category (Category)
);

-- Orders table
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY AUTO_INCREMENT,
    CustomerID INT NOT NULL,
    OrderDate DATE NOT NULL,
    TotalAmount DECIMAL(10,2) NOT NULL,
    PaymentMethod VARCHAR(50),
    ShippingMethod VARCHAR(50),
    OrderStatus VARCHAR(50) DEFAULT 'Pending',
    ShippingAddress VARCHAR(200),
    ShippingCity VARCHAR(100),
    ShippingZipCode VARCHAR(10),
    CreatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    INDEX idx_customer (CustomerID),
    INDEX idx_date (OrderDate),
    INDEX idx_status (OrderStatus)
);

-- OrderDetails (kopplingstabell för M:N mellan Orders och Products)
CREATE TABLE OrderDetails (
    OrderDetailID INT PRIMARY KEY AUTO_INCREMENT,
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,  -- Pris vid köptillfället (viktigt!)
    Subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID)
        ON DELETE CASCADE
        ON UPDATE CASCADE,
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
        ON DELETE RESTRICT
        ON UPDATE CASCADE,
    INDEX idx_order (OrderID),
    INDEX idx_product (ProductID)
);
```

**Viktiga design-beslut:**

1. **CustomerID vs Email som PK**: Vi använder surrogate key (CustomerID) för stabilitet. Email kan ändras!

2. **UnitPrice i OrderDetails**: Sparar priset vid köptillfället så historiska ordrar inte påverkas av prisändringar.

3. **ON DELETE RESTRICT för Products**: Förhindrar borttagning av produkter som har ordrar (dataintegritet!).

4. **ON DELETE CASCADE för OrderDetails**: Om en order tas bort försvinner dess detaljer (logiskt).

5. **Shipping address i Orders**: Tillåter olika leveransadress än kundens registrerade adress.

6. **Timestamps**: CreatedAt och UpdatedAt för audit trail.

</details>

---

## Del 3: Migrera Data (75 min)

Nu ska ni faktiskt flytta data från den trasiga strukturen till den nya!

### Uppgift 3.1: Skapa migreringsstrategi

Planera i vilken ordning ni ska migrera data. Varför är ordningen viktig?

**Frågor att besvara:**
1. Vilken tabell ska skapas först?
2. Vilken tabell ska populeras först?
3. Hur hanterar ni dubbletter (t.ex. samma kund förekommer flera gånger i gamla Orders-tabellen)?
4. Hur säkerställer ni att ingen data förloras?

### Uppgift 3.2: Skriv migreringsskript

Skriv SQL-satser för att migrera data från gamla `Orders` till nya tabeller.

**Steg 1: Migrera unika kunder**

```sql
-- Extrahera unika kunder från Orders-tabellen
INSERT INTO Customers (Name, Email, Phone, Address, City, ZipCode)
SELECT DISTINCT
    CustomerName,
    CustomerEmail,
    CustomerPhone,
    CustomerAddress,
    CustomerCity,
    CustomerZipCode
FROM Orders
WHERE CustomerEmail IS NOT NULL;  -- Undvik NULL-rader

-- Kontrollera resultat
SELECT COUNT(*) as AntalKunder FROM Customers;
SELECT * FROM Customers LIMIT 5;
```

**Steg 2: Migrera unika produkter**

```sql
-- Era migreringssatser här...
-- Tips: Använd GROUP BY för att få unika produkter
```

**Steg 3: Migrera ordrar**

```sql
-- Era migreringssatser här...
-- Tips: Använd JOIN för att få rätt CustomerID från Customers
```

**Steg 4: Migrera orderdetaljer**

```sql
-- Era migreringssatser här...
-- Tips: Komplexa JOIN för att matcha både OrderID och ProductID
```

### Lösningsförslag (Dolt)

<details>
<summary>Klicka för att se lösning</summary>

```sql
-- STEG 1: Migrera kunder (redan visat ovan)
INSERT INTO Customers (Name, Email, Phone, Address, City, ZipCode)
SELECT DISTINCT
    CustomerName,
    CustomerEmail,
    CustomerPhone,
    CustomerAddress,
    CustomerCity,
    CustomerZipCode
FROM Orders
WHERE CustomerEmail IS NOT NULL;

-- STEG 2: Migrera produkter
INSERT INTO Products (SKU, Name, Description, Category, Price)
SELECT DISTINCT
    ProductSKU,
    ProductName,
    ProductDescription,
    ProductCategory,
    ProductPrice
FROM Orders
WHERE ProductSKU IS NOT NULL;

-- STEG 3: Migrera ordrar
-- Vi behöver en temporär tabell för att hålla koll på mappning gamla -> nya OrderID
CREATE TEMPORARY TABLE OrderMapping (
    OldOrderID INT,
    NewOrderID INT
);

INSERT INTO Orders (CustomerID, OrderDate, TotalAmount, PaymentMethod, ShippingMethod, OrderStatus)
SELECT
    c.CustomerID,
    o.OrderDate,
    o.TotalAmount,
    o.PaymentMethod,
    o.ShippingMethod,
    o.OrderStatus
FROM Orders o
JOIN Customers c ON o.CustomerEmail = c.Email;

-- Spara mappning
INSERT INTO OrderMapping (OldOrderID, NewOrderID)
SELECT o.OrderID, LAST_INSERT_ID()
FROM Orders o;

-- STEG 4: Migrera orderdetaljer
INSERT INTO OrderDetails (OrderID, ProductID, Quantity, UnitPrice, Subtotal)
SELECT
    om.NewOrderID,
    p.ProductID,
    o.Quantity,
    o.ProductPrice,  -- UnitPrice från gamla priset
    o.Quantity * o.ProductPrice
FROM Orders o
JOIN OrderMapping om ON o.OrderID = om.OldOrderID
JOIN Products p ON o.ProductSKU = p.SKU;

-- STEG 5: Validering
SELECT
    'Customers' as Tabell,
    COUNT(*) as AntalRader
FROM Customers
UNION ALL
SELECT 'Products', COUNT(*) FROM Products
UNION ALL
SELECT 'Orders', COUNT(*) FROM Orders
UNION ALL
SELECT 'OrderDetails', COUNT(*) FROM OrderDetails;

-- Kontrollera att totala försäljningssumman stämmer
SELECT
    SUM(TotalAmount) as GammalTotal
FROM Orders;  -- Gamla tabellen

SELECT
    SUM(TotalAmount) as NyTotal
FROM Orders;  -- Nya tabellen

-- Borde vara samma!
```

**Viktiga steg:**

1. **DISTINCT**: Extrahera unika värden från denormaliserad data
2. **JOIN på Email/SKU**: Matcha nya ID:n med gamla data
3. **Temporär mappningstabell**: Håll koll på gamla vs nya OrderID
4. **Validering**: Dubbelkolla att summan stämmer!

</details>

### Uppgift 3.3: Validera migreringen

Skriv SQL-queries för att verifiera att:

1. Antalet unika kunder är korrekt
2. Inga produkter har försvunnit
3. Totala försäljningssumman är oförändrad
4. Alla ordrar har minst en orderrad i OrderDetails

```sql
-- Validering 1: Antal kunder
-- Era queries här...

-- Validering 2: Antal produkter
-- Era queries här...

-- Validering 3: Total försäljning
-- Era queries här...

-- Validering 4: Ordrar utan detaljer (ska vara 0)
-- Era queries här...
```

---

## Del 4: Testa Den Nya Strukturen (30 min)

### Uppgift 4.1: Operationer som nu fungerar

Testa att utföra operationer som var problematiska i den gamla strukturen:

**Test 1: Uppdatera kundinformation**
```sql
-- Ändra Annas email (nu behövs bara EN update!)
UPDATE Customers
SET Email = 'anna.ny@email.com'
WHERE Name = 'Anna Svensson';

-- Verifiera att ALLA hennes ordrar automatiskt pekar på nya emailen
SELECT
    o.OrderID,
    c.Name,
    c.Email,
    o.OrderDate
FROM Orders o
JOIN Customers c ON o.CustomerID = c.CustomerID
WHERE c.Name = 'Anna Svensson';
```

**Test 2: Lägga till produkt utan order**
```sql
-- Nu fungerar detta!
INSERT INTO Products (SKU, Name, Description, Category, Price, StockQuantity)
VALUES ('BOOT-TREK-004', 'Vandringskängor', 'Vattentäta och hållbara', 'Skor', 1899.00, 25);
```

**Test 3: Ta bort kund (ska misslyckas p.g.a. foreign key)**
```sql
-- Detta ska INTE gå (kunden har ordrar)
DELETE FROM Customers WHERE Name = 'Anna Svensson';
-- Förväntat: Error Code 1451 - Cannot delete or update a parent row

-- Men vi kan markera kunden som inaktiv istället:
ALTER TABLE Customers ADD COLUMN IsActive BOOLEAN DEFAULT TRUE;
UPDATE Customers SET IsActive = FALSE WHERE Name = 'Anna Svensson';
```

### Uppgift 4.2: Skapa komplex rapport

Skriv en query som visar:
- Kundnamn
- Antal ordrar
- Total spenderad summa
- Mest köpta produktkategori

```sql
-- Er query här...
-- Tips: Använd GROUP BY, JOIN och subqueries
```

### Lösningsförslag (Dolt)

<details>
<summary>Klicka för att se lösning</summary>

```sql
SELECT
    c.Name as KundNamn,
    c.Email,
    COUNT(DISTINCT o.OrderID) as AntalOrderar,
    SUM(o.TotalAmount) as TotalSpenderat,
    (
        SELECT p.Category
        FROM OrderDetails od
        JOIN Products p ON od.ProductID = p.ProductID
        WHERE od.OrderID IN (
            SELECT OrderID FROM Orders WHERE CustomerID = c.CustomerID
        )
        GROUP BY p.Category
        ORDER BY SUM(od.Quantity) DESC
        LIMIT 1
    ) as FavoriteKategori
FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.Name, c.Email
ORDER BY TotalSpenderat DESC;
```

**Förklaring:**
- **Yttre query**: Grupperar per kund och räknar ordrar + summa
- **Subquery**: Hittar mest köpta kategorin för varje kund
- **LEFT JOIN**: Inkluderar kunder utan ordrar (visar 0)

</details>

---

## Del 5: Reflektion och Dokumentation (20 min)

### Uppgift 5.1: Skriv migrations-dokumentation

Skapa ett dokument (Markdown eller Word) med:

1. **Problembeskrivning**: Vad var fel med gamla strukturen?
2. **Lösningsförklaring**: Hur löser nya designen problemen?
3. **ER-Diagram**: Inkludera ert diagram
4. **Migreringssteg**: Steg-för-steg guide
5. **Validering**: Hur ni verifierade att migreringen lyckades
6. **Rekommendationer**: Vad bör göras framåt?

### Uppgift 5.2: Presentera för gruppen

Förbered en 5-minuters presentation där ni visar:
- Före/efter-jämförelse
- Största förbättringarna
- Utmaningar ni stötte på
- En "wow-moment" query som visar förbättringen

---

## Bonusuppgifter (Om ni har tid över)

### Bonus 1: Lägg till produktrecensioner

Utöka databasen med möjlighet för kunder att lämna recensioner på produkter.

**Krav:**
- En kund kan recensera en produkt en gång
- Recension innehåller: betyg (1-5), text, datum
- Endast kunder som köpt produkten kan recensera

```sql
CREATE TABLE ProductReviews (
    -- Design detta själva!
);
```

### Bonus 2: Implementera triggers

Skapa en trigger som automatiskt uppdaterar `Orders.TotalAmount` när `OrderDetails` ändras.

```sql
DELIMITER //

CREATE TRIGGER update_order_total AFTER INSERT ON OrderDetails
FOR EACH ROW
BEGIN
    -- Er trigger-logik här
END//

DELIMITER ;
```

### Bonus 3: Performance-optimering

1. Analysera långsamma queries med `EXPLAIN`
2. Lägg till index där de behövs
3. Skriv en stored procedure för att hämta kundrapport effektivt

---

## Inlämning

### Vad ska lämnas in:

1. **SQL-script**: `migration.sql` med alla CREATE, INSERT och validationsquerys
2. **ER-Diagram**: Bild eller PDF
3. **Dokumentation**: Markdown eller PDF med reflektion
4. **Testresultat**: Screenshot på validationsquerys som visar att allt stämmer

### Deadeline:

**Imorgon 16:00** - Ladda upp på Canvas

### Bedömning:

- ✅ **Godkänd**: Korrekt normaliserad struktur, fungerande migrering
- ✅ **Väl Godkänd**: + Genomtänkta index, triggers/views, utförlig dokumentation

---

## Resurser

- [MySQL Normalization Tutorial](https://dev.mysql.com/doc/mysql-tutorial-excerpt/8.0/en/)
- [PostgreSQL Foreign Keys](https://www.postgresql.org/docs/current/ddl-constraints.html)
- [Database Design Best Practices](https://www.sqlshack.com/learn-sql-database-design/)

---

## Tips för Framgång

1. **Börja smått**: Testa med lite data först
2. **Använd transactions**: `BEGIN; ... ROLLBACK;` när ni testar
3. **Backup**: Alltid ta backup innan migrering!
4. **Dokumentera**: Skriv ner vad ni gör medan ni gör det
5. **Testa ofta**: Validera efter varje steg
6. **Fråga**: Om ni kör fast, fråga handledare!

**Lycka till med räddningsuppdraget! 🚀**

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
