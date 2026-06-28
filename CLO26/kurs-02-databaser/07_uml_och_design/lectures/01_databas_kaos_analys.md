---

title: Databas-Kaos: Analysera Dålig Design
author: Marcus Ackre Medina
type: lecture
topic: databaser
difficulty: 1
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/lectures/01_databas_kaos_analys.md"
description: "- Identifiera symptom på dålig databasdesign"
tags: ["analys", "analysera", "databas", "databas-kaos:", "databaser", "design", "dålig", "kaos", "sql", "ssh"]
week_fit: []
---

# Databas-Kaos: Analysera Dålig Design

🟢


---

## Rädda Kaos-Databasen

**Campus Mölndal - Databasdesign**
---

## Dagens Mål

- Identifiera symptom på dålig databasdesign
- Förstå normaliseringsregler och varför de finns
- Analysera ett riktigt "kaos-exempel"
- Lära sig migreringsstrategier
- Skriva SQL för att fixa strukturproblem

---

## Vad är en "Kaos-Databas"?

En databas med:
- ❌ Data som upprepas överallt
- ❌ Uppdateringar som kräver ändringar på många ställen
- ❌ Inkonsistent data (olika värden för samma sak)
- ❌ Borttagningar som förstör relaterad data
- ❌ Omöjliga att söka i effektivt

---

## Verkligt Exempel: Webbshop-Kaos

<div class="mermaid">

graph TD
    A["Order-tabell"] --> B["Problem 1: Upprepning"];
    A --> C["Problem 2: Uppdateringsanomali"];
    A --> D["Problem 3: Borttagningsanomali"];

</div>

```sql
CREATE TABLE Orders (
    OrderID INT,
    CustomerName VARCHAR(100),
    CustomerEmail VARCHAR(100),
    CustomerAddress VARCHAR(200),
    ProductName VARCHAR(100),
    ProductPrice DECIMAL(10,2),
    Quantity INT
);
```

---

## Problem 1: Upprepning (Redundans)

```sql
INSERT INTO Orders VALUES
(1, 'Anna Svensson', 'anna@email.com', 'Storgatan 1',
    'Laptop', 15000, 1),
(2, 'Anna Svensson', 'anna@email.com', 'Storgatan 1',
    'Mus', 299, 2),
(3, 'Anna Svensson', 'anna@email.com', 'Storgatan 1',
    'Tangentbord', 899, 1);
```

**Problem**: Anna's data upprepas 3 gånger!

**Konsekvens**: Slöseri med utrymme, svårt att underhålla

---

## Problem 2: Uppdateringsanomali

Vad händer om Anna byter adress?

```sql
-- Måste uppdatera ALLA rader!
UPDATE Orders
SET CustomerAddress = 'Nygatan 5'
WHERE CustomerName = 'Anna Svensson';
```

**Problem**: Om vi missar en rad blir data inkonsistent

**Risk**: Anna har olika adresser i olika ordrar!

---

## Problem 3: Borttagningsanomali

```sql
-- Ta bort Annas sista order
DELETE FROM Orders WHERE OrderID = 3;
```

**Problem**: Om vi tar bort sista ordern förlorar vi Annas uppgifter helt!

**Konsekvens**: Kunddata försvinner bara för att de inte har aktiva ordrar

---

## Problem 4: Insättningsanomali

Kan vi lägga till en ny produkt utan en order?

```sql

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
-- Detta fungerar INTE!
INSERT INTO Orders (ProductName, ProductPrice)
VALUES ('Skärm', 3500);
```

**Problem**: Vi kan inte spara produkter utan ordrar

**Konsekvens**: Produktkatalog kan inte existera självständigt

---

## Normaliseringsregler: 1NF (Första Normalformen)

**Regel**: Varje cell ska innehålla ETT värde (atomärt)

### ❌ Bryter mot 1NF:
```sql
CREATE TABLE Orders (
    OrderID INT,
    CustomerName VARCHAR(100),
    Products VARCHAR(500)  -- 'Laptop, Mus, Tangentbord'
);
```

### ✅ Följer 1NF:
```sql
-- Separat rad för varje produkt
```

---

## Normaliseringsregler: 2NF (Andra Normalformen)

**Regel**: Inga partiella beroenden (gäller endast sammansatta nycklar)

### ❌ Bryter mot 2NF:
```sql
CREATE TABLE OrderDetails (
    OrderID INT,
    ProductID INT,
    ProductName VARCHAR(100),  -- Beror bara på ProductID!
    Quantity INT,
    PRIMARY KEY (OrderID, ProductID)
);
```

**Problem**: ProductName beror bara på ProductID, inte hela nyckeln

---

## Normaliseringsregler: 3NF (Tredje Normalformen)

**Regel**: Inga transitiva beroenden (icke-nyckel-attribut får inte bero på andra icke-nyckel-attribut)

### ❌ Bryter mot 3NF:
```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    CustomerName VARCHAR(100),  -- Beror på CustomerID!
    CustomerEmail VARCHAR(100)   -- Beror på CustomerID!
);
```

**Lösning**: Flytta kunddata till egen tabell

---

## Räddningsplan: Normaliserad Design

<div class="mermaid">

graph TD
    A["Customers"] --> B["Orders"];
    C["Products"] --> D["OrderDetails"];
    B --> D;

</div>

---

## Räddningsplan: Normaliserad Design

```sql
CREATE TABLE Customers (
    CustomerID INT PRIMARY KEY,
    Name VARCHAR(100),
    Email VARCHAR(100),
    Address VARCHAR(200)
);

CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    Name VARCHAR(100),
    Price DECIMAL(10,2)
);
```

---

## Räddningsplan: Relationer

```sql
CREATE TABLE Orders (
    OrderID INT PRIMARY KEY,
    CustomerID INT,
    OrderDate DATE,
    FOREIGN KEY (CustomerID) REFERENCES Customers(CustomerID)
);

CREATE TABLE OrderDetails (
    OrderID INT,
    ProductID INT,
    Quantity INT,
    PRIMARY KEY (OrderID, ProductID),
    FOREIGN KEY (OrderID) REFERENCES Orders(OrderID),
    FOREIGN KEY (ProductID) REFERENCES Products(ProductID)
);
```

---

## Migrering: Steg 1 - Skapa Nya Tabeller

```sql
-- Skapa normaliserade tabeller
CREATE TABLE Customers ( ... );
CREATE TABLE Products ( ... );
CREATE TABLE Orders_New ( ... );
CREATE TABLE OrderDetails ( ... );
```

**Tips**: Börja med oberoende tabeller (Customers, Products) först!

---

## Migrering: Steg 2 - Migrera Data

```sql
-- Migrera unika kunder
INSERT INTO Customers (Name, Email, Address)
SELECT DISTINCT CustomerName, CustomerEmail, CustomerAddress
FROM Orders;

-- Migrera unika produkter
INSERT INTO Products (Name, Price)
SELECT DISTINCT ProductName, ProductPrice
FROM Orders;
```

---

## Migrering: Steg 3 - Migrera Relationer

```sql
-- Migrera ordrar med nya ID:n
INSERT INTO Orders_New (CustomerID, OrderDate)
SELECT c.CustomerID, o.OrderDate
FROM Orders o
JOIN Customers c ON o.CustomerName = c.Name;

-- Migrera orderdetaljer
INSERT INTO OrderDetails (OrderID, ProductID, Quantity)
SELECT on.OrderID, p.ProductID, o.Quantity
FROM Orders o
JOIN Orders_New on ON ...
JOIN Products p ON o.ProductName = p.Name;
```

---

## Migrering: Steg 4 - Validera och Byt

```sql
-- Kontrollera dataintegrit et
SELECT COUNT(*) FROM Orders;  -- Gamla tabellen
SELECT SUM(Quantity) FROM OrderDetails;  -- Nya strukturen

-- Om allt stämmer: byt namn
DROP TABLE Orders;
ALTER TABLE Orders_New RENAME TO Orders;
```

**VIKTIGT**: Alltid ta backup innan!

---

## Verktyg för Analys

### SQL-frågor för att hitta problem:

```sql
-- Hitta dubbletter (redundans)
SELECT CustomerName, CustomerEmail, COUNT(*)
FROM Orders
GROUP BY CustomerName, CustomerEmail
HAVING COUNT(*) > 1;

-- Hitta inkonsistenser
SELECT CustomerName, COUNT(DISTINCT CustomerEmail) as EmailCount
FROM Orders
GROUP BY CustomerName
HAVING EmailCount > 1;
```

---

## Vanliga Symptom på Kaos

1. **NULL-värden överallt**: Design saknar rätt relationer
2. **Enorma UPDATE-statements**: Data upprepas
3. **Komplexa WHERE-klauser**: Dålig struktur
4. **Långsamma sökningar**: Inga rätt index eller normalisering
5. **Dataförlust vid DELETE**: Saknar referential integrity

---

## Best Practices för Databas-Räddning

1. ✅ **Analysera först**: Dokumentera alla problem
2. ✅ **Rita ER-diagram**: Visualisera rätt struktur
3. ✅ **Migrera i steg**: Testa varje steg
4. ✅ **Behåll gamla data**: Backup och parallell körning
5. ✅ **Validera allt**: Kontrollräkna innan switch
6. ✅ **Använd transaktioner**: ROLLBACK vid problem

---

## Nästa Steg: Workshop

**Praktisk övning**: "Rädda Kaos-Databasen"

1. Analysera en trasig webbshop-databas
2. Identifiera normaliseringsbrott
3. Rita ny ER-design
4. Skriv migreringsscript
5. Testa och validera

**Mål**: Från kaos till kvalitet!

---

## Sammanfattning

- **Kaos-databaser** är dyra att underhålla
- **Normalisering** förhindrar anomalier
- **Migrering** kräver planering och testning
- **SQL-färdigheter** är nyckeln till räddning
- **Nästa lektion**: När vi MEDVETET bryter reglerna (för AI!)

---

## Frågor?

**Tänk på**: Imorgon lär vi oss när det är OK att bryta dessa regler!

*För AI/ML-träningsdata behöver vi ofta denormaliserade strukturer...*

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
