# DDL, DML och DCL — Referensguide

## Översikt

SQL-språket delas in i fyra underkategorier baserat på vad kommandona gör:

| Kategori | Fullt namn | Vad det gör |
|----------|-------------|-------------|
| **DDL** | Data Definition Language | Skapar och ändrar databasstruktur |
| **DML** | Data Manipulation Language | Hanterar data i tabellerna |
| **DCL** | Data Control Language | Styr behörigheter och säkerhet |
| **TCL** | Transaction Control Language | Hanterar transaktioner |

---

## DDL — Data Definition Language

DDL används för att **definiera strukturen** — databaser, tabeller, index, vyer.

### CREATE

```sql
-- Skapa databas
CREATE DATABASE webshop;

-- Skapa tabell
CREATE TABLE Product (
    ProductID   INT PRIMARY KEY AUTO_INCREMENT,
    Name        VARCHAR(100) NOT NULL,
    Price       DECIMAL(10,2) NOT NULL,
    Description TEXT,
    Stock       INT DEFAULT 0,
    CategoryID  INT,
    CreatedAt   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID)
);

-- Skapa index
CREATE INDEX idx_product_name ON Product(Name);

-- Skapa vy (virtuell tabell, sparar en SELECT)
CREATE VIEW ActiveProducts AS
SELECT * FROM Product WHERE Stock > 0;
```

### ALTER

```sql
-- Lägg till kolumn
ALTER TABLE Product ADD COLUMN Color VARCHAR(20);

-- Ändra kolumn
ALTER TABLE Product MODIFY COLUMN Price DECIMAL(12,2);

-- Byt namn på kolumn
ALTER TABLE Product RENAME COLUMN Color TO MainColor;

-- Ta bort kolumn
ALTER TABLE Product DROP COLUMN MainColor;

-- Lägg till FK
ALTER TABLE Product ADD FOREIGN KEY (CategoryID)
    REFERENCES Category(CategoryID);

-- Lägg till UNIQUE
ALTER TABLE Product ADD UNIQUE (Name);
```

### DROP

```sql
-- Ta bort tabell (raderar ALLT — data + struktur)
DROP TABLE Product;

-- Ta bort databas (varning!)
DROP DATABASE webshop;

-- Ta bort index
DROP INDEX idx_product_name ON Product;

-- Ta bort vy
DROP VIEW ActiveProducts;
```

### TRUNCATE

```sql
-- Töm tabellen på data, behåll strukturen
TRUNCATE TABLE Product;

-- Skillnad mot DELETE:
-- TRUNCATE = snabb, kan inte ROLLBACK (i MySQL), återställer AUTO_INCREMENT
-- DELETE   = långsam, kan ROLLBACK, påverkar inte AUTO_INCREMENT
```

---

## DML — Data Manipulation Language

DML används för att **arbeta med data** i tabellerna.

### INSERT

```sql
-- En rad med specifika kolumner
INSERT INTO Product (Name, Price, Stock) VALUES ('Kaffekopp', 129.00, 50);

-- Flera rader
INSERT INTO Product (Name, Price, Stock) VALUES
    ('Mugg 330ml', 89.00, 100),
    ('Tefat', 49.00, 75);

-- Infoga från annan tabell
INSERT INTO ProductArchive (SELECT * FROM Product WHERE Stock = 0);
```

### SELECT

```sql
-- Allt
SELECT * FROM Product;

-- Specifika kolumner
SELECT Name, Price FROM Product;

-- Filtrera
SELECT * FROM Product WHERE Price > 100 AND Stock > 0;

-- Sortera
SELECT * FROM Product ORDER BY Price DESC;

-- Begränsa
SELECT * FROM Product LIMIT 10 OFFSET 20;  -- Sid 3, 10 per sida

-- Gruppera
SELECT CategoryID, COUNT(*) AS Antal, AVG(Price) AS Medelpris
FROM Product
GROUP BY CategoryID
HAVING Antal > 5;

-- Join
SELECT p.Name, c.CategoryName
FROM Product p
JOIN Category c ON p.CategoryID = c.CategoryID;
```

### UPDATE

```sql
-- Uppdatera en rad
UPDATE Product SET Price = 149.00 WHERE ProductID = 1;

-- Uppdatera flera fält
UPDATE Product SET Price = Price * 1.1, UpdatedAt = NOW()
WHERE CategoryID = 1;

-- Utan WHERE = ALLA rader påverkas!
-- UPDATE Product SET Price = 0;  ← Farligt!
```

### DELETE

```sql
-- Ta bort en rad
DELETE FROM Product WHERE ProductID = 1;

-- Ta bort flera
DELETE FROM Product WHERE Stock = 0 AND UpdatedAt < '2025-01-01';

-- Ta bort ALLT (varning!)
-- DELETE FROM Product;
```

---

## DCL — Data Control Language

DCL används för att **styra behörigheter**.

### GRANT

```sql
-- Ge alla rättigheter på en databas
GRANT ALL PRIVILEGES ON webshop.* TO 'anna'@'localhost';

-- Ge specifika rättigheter
GRANT SELECT, INSERT, UPDATE ON webshop.Orders TO 'support'@'localhost';

-- Ge med möjlighet att vidarebefordra
GRANT ALL ON webshop.* TO 'admin'@'localhost' WITH GRANT OPTION;

-- Ge på kolumnnivå
GRANT SELECT (Name, Email) ON webshop.Customer TO 'marketing'@'localhost';
```

### REVOKE

```sql
-- Ta bort rättigheter
REVOKE DELETE ON webshop.Products FROM 'anna'@'localhost';

-- Ta bort alla
REVOKE ALL PRIVILEGES ON webshop.* FROM 'anna'@'localhost';

-- Spara ändringarna
FLUSH PRIVILEGES;
```

### Användare och Roller

```sql
-- Skapa användare
CREATE USER 'anna'@'localhost' IDENTIFIED BY 'säkert_lösen';

-- Skapa roll (MySQL 8+)
CREATE ROLE 'read_only';
GRANT SELECT ON webshop.* TO 'read_only';
GRANT 'read_only' TO 'anna'@'localhost';
SET DEFAULT ROLE ALL TO 'anna'@'localhost';

-- Ta bort användare
DROP USER 'anna'@'localhost';
```

---

## TCL — Transaction Control Language

```sql
-- Starta transaktion
START TRANSACTION;

-- Spara en återställningspunkt
SAVEPOINT before_update;

-- Utför operationer
UPDATE Account SET Balance = Balance - 500 WHERE ID = 1;
UPDATE Account SET Balance = Balance + 500 WHERE ID = 2;

-- Ångra till savepoint (om något gick fel)
ROLLBACK TO SAVEPOINT before_update;

-- Ångra allt i transaktionen
ROLLBACK;

-- Bekräfta alla ändringar
COMMIT;
```

---

## Snabbreferenskort

| Kommando | Kategori | Vad | Risk |
|----------|----------|-----|------|
| `CREATE DATABASE` | DDL | Skapa databas | Låg |
| `CREATE TABLE` | DDL | Skapa tabell | Låg |
| `ALTER TABLE` | DDL | Ändra tabell | Medium |
| `DROP TABLE` | DDL | Ta bort tabell | **Hög** |
| `TRUNCATE` | DDL | Töm tabell | **Hög** |
| `SELECT` | DML | Hämta data | Ingen |
| `INSERT` | DML | Lägg till data | Låg |
| `UPDATE` | DML | Uppdatera | **Hög (glöm WHERE!)** |
| `DELETE` | DML | Ta bort | **Hög (glöm WHERE!)** |
| `GRANT` | DCL | Ge rättigheter | Medium |
| `REVOKE` | DCL | Ta bort rättigheter | Medium |
| `COMMIT` | TCL | Bekräfta | Låg |
| `ROLLBACK` | TCL | Ångra | Låg |
