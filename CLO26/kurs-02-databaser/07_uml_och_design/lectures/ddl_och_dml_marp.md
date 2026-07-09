---
marp: true
theme: default
class: invert
paginate: true
---

# DDL och DML — Bygg och Manipulera Databaser

**Kurs:** Databashantering och -design
**Modul:** 07 — UML och databasdesign

---

## Vad ska vi lära oss idag?

- **DDL** — Data Definition Language (CREATE, ALTER, DROP)
- **DML** — Data Manipulation Language (SELECT, INSERT, UPDATE, DELETE)
- **Constraints** — begränsningar för dataintegritet
- **Index** — snabba upp sökningar
- **Transaktioner** — ACID och COMMIT/ROLLBACK

---

## SQL-språkets indelning

```
SQL
├── DDL (Data Definition Language)
│   ├── CREATE  ─ skapa tabeller/databaser
│   ├── ALTER   ─ ändra struktur
│   ├── DROP    ─ ta bort
│   └── TRUNCATE ─ töm data (behåll struktur)
│
├── DML (Data Manipulation Language)
│   ├── SELECT ─ hämta data
│   ├── INSERT ─ lägg till
│   ├── UPDATE ─ uppdatera
│   └── DELETE ─ ta bort
│
├── DCL (Data Control Language)      ← Nästa föreläsning
└── TCL (Transaction Control Language)
```

---

## DDL — CREATE

Skapa en databas:

```sql
CREATE DATABASE webshop;
USE webshop;
```

Skapa en tabell:

```sql
CREATE TABLE Product (
    ProductID   INT PRIMARY KEY AUTO_INCREMENT,
    Name        VARCHAR(100) NOT NULL,
    Price       DECIMAL(10,2) NOT NULL,
    Stock       INT DEFAULT 0,
    CategoryID  INT,
    CreatedAt   TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);
```

---

## DDL — Datatyper

| Datatyp | Beskrivning | Exempel |
|---------|-------------|---------|
| `INT` | Heltal | 42, -5 |
| `DECIMAL(10,2)` | Decimaltal | 1499.50 |
| `VARCHAR(50)` | Text (max 50 tecken) | "Marcus" |
| `CHAR(10)` | Text (exakt 10 tecken) | "2026-06-30" |
| `DATE` | Datum | '2026-06-30' |
| `DATETIME` | Datum + tid | '2026-06-30 14:30:00' |
| `BOOLEAN` | Sant/falskt | TRUE / FALSE |
| `TEXT` | Lång text (articles, beskrivningar) | |
| `BLOB` | Binär data (bilder, filer) | |

---

## DDL — Constraints

Begränsningar som skyddar dataintegriteten:

```sql
CREATE TABLE Customer (
    CustomerID  INT PRIMARY KEY,              -- Unik, NOT NULL
    Email       VARCHAR(100) UNIQUE,          -- Inga dubbletter
    Name        VARCHAR(50) NOT NULL,          -- Måste finnas
    Age         INT CHECK (Age >= 18),         -- Måste vara 18+
    Country     VARCHAR(50) DEFAULT 'Sweden',  -- Standardvärde
    CategoryID  INT REFERENCES Category(CategoryID)  -- FK
);
```

- **PRIMARY KEY** = UNIQUE + NOT NULL
- **FOREIGN KEY** = referens till annan tabell
- **CHECK** = villkor som måste uppfyllas
- **DEFAULT** = standardvärde om inget anges

---

## DDL — ALTER och DROP

Ändra befintlig tabell:

```sql
-- Lägg till kolumn
ALTER TABLE Product ADD COLUMN Description TEXT;

-- Ändra datatyp
ALTER TABLE Product MODIFY COLUMN Price DECIMAL(12,2);

-- Ta bort kolumn
ALTER TABLE Product DROP COLUMN Description;

-- Lägg till FK
ALTER TABLE Product ADD FOREIGN KEY (CategoryID)
    REFERENCES Category(CategoryID);

-- Ta bort tabell (varning!)
DROP TABLE Product;

-- Töm tabell (behåller struktur)
TRUNCATE TABLE Product;
```

---

## DML — INSERT

Lägg till data:

```sql
-- En rad
INSERT INTO Product (Name, Price, Stock, CategoryID)
VALUES ('Kaffe brygg', 299.00, 50, 1);

-- Flera rader samtidigt
INSERT INTO Product (Name, Price, Stock, CategoryID) VALUES
    ('Espressokopp 6-pack', 199.00, 30, 1),
    ('Mugg 330ml', 89.00, 100, 1),
    ('Termoskanna 1L', 449.00, 20, 2);

-- Utelämna AUTO_INCREMENT-kolumner — de sätts automatiskt
```

---

## DML — SELECT

Hämta data:

```sql
-- Alla kolumner, alla rader
SELECT * FROM Product;

-- Specifika kolumner
SELECT Name, Price FROM Product;

-- Filtrera
SELECT * FROM Product WHERE Price < 200;

-- Sortera
SELECT * FROM Product ORDER BY Price DESC;

-- Begränsa
SELECT * FROM Product LIMIT 5;

-- Beräkna
SELECT COUNT(*), AVG(Price), MAX(Price) FROM Product;
```

---

## DML — UPDATE och DELETE

Uppdatera och ta bort data:

```sql
-- Uppdatera (GLÖM INTE WHERE!)
UPDATE Product SET Price = 249.00 WHERE ProductID = 1;

-- Uppdatera flera fält
UPDATE Product
SET Price = 349.00, Stock = 25
WHERE ProductID = 3;

-- Ta bort (GLÖM INTE WHERE!)
DELETE FROM Product WHERE ProductID = 4;

-- Ta bort ALLT (var försiktig!)
-- DELETE FROM Product;
```

⚠️ **Alltid WHERE på UPDATE och DELETE** — annars påverkas ALLA rader!

---

## TCL — Transaktioner

ACID = Atomicity, Consistency, Isolation, Durability

```sql
START TRANSACTION;

UPDATE Account SET Balance = Balance - 500 WHERE AccountID = 1;
UPDATE Account SET Balance = Balance + 500 WHERE AccountID = 2;

-- Om allt gick bra:
COMMIT;

-- Om något gick fel:
-- ROLLBACK;
```

Antingen båda uppdateringarna genomförs — eller ingen. Pengar försvinner inte.

---

## Index — snabba upp sökningar

```sql
-- Skapa index
CREATE INDEX idx_product_name ON Product(Name);

-- Unikt index (förhindrar dubbletter)
CREATE UNIQUE INDEX idx_product_sku ON Product(SKU);

-- Composite index (flera kolumner)
CREATE INDEX idx_product_category ON Product(CategoryID, Name);
```

**När använda index:**
- Kolumner som används i WHERE, JOIN, ORDER BY
- **Inte** på kolumner som sällan söks på
- Index snabbar upp SELECT men saktar ner INSERT/UPDATE

---

## Prova själv

1. Skapa en tabell `Employee` med kolumner: EmployeeID, FirstName, LastName, Email, Salary, DepartmentID
2. Lägg till 5 anställda
3. Hitta alla som tjänar över 40000
4. Öka lönen med 10% för en specifik anställd
5. Skapa ett index på Email

---

## Sammanfattning

- ✅ DDL = CREATE, ALTER, DROP (struktur)
- ✅ DML = SELECT, INSERT, UPDATE, DELETE (data)
- ✅ Constraints skyddar dataintegriteten
- ✅ Transaktioner (ACID) = COMMIT / ROLLBACK
- ✅ Index snabbar upp SELECT
- ⚠️ WHERE på UPDATE/DELETE — annars förlorar du data!
- ➡️ Nästa: DCL och säkerhet

---
