---

title: GROUP BY, HAVING och aggregering
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/sql/aggregering.md"
description: "Kort och enkelt: när du behöver räkna, summera eller beräkna medelvärden använder du aggregeringsfunktioner tillsammans med GROUP BY. HAVING används för att filtrera grupper (efter aggregering) medan "
tags: ["aggregering", "csharp", "databaser", "git", "group", "having", "oop", "sql"]
week_fit: []
---

# GROUP BY, HAVING och aggregering

🔴


Kort och enkelt: när du behöver räkna, summera eller beräkna medelvärden använder du aggregeringsfunktioner tillsammans med GROUP BY. HAVING används för att filtrera grupper (efter aggregering) medan WHERE filtrerar rader innan gruppering.

Varför detta är viktigt:

- Rapportering (månadssiffror, topplistor)
- Dataanalys (genomsnitten per kategori)
- Prestanda: rätt användning av index och filtrering minskar scannedata.

Nyckelbegrepp

- COUNT(), SUM(), AVG(), MIN(), MAX()
- GROUP BY grupperar rader på värden i en eller flera kolumner
- HAVING filtrerar grupper (t.ex. grupper med COUNT > X)
- WHERE filtrerar rader innan GROUP BY

Enkla exempel

```sql
-- Antal ordrar per kund
SELECT CustomerId, COUNT(*) AS OrderCount
FROM Orders
GROUP BY CustomerId;

-- Månadsintäkt
SELECT strftime('%Y-%m', OrderDate) AS Month, SUM(TotalAmount) AS Revenue
FROM Orders
GROUP BY Month
ORDER BY Month DESC;
```

HAVING vs WHERE

- WHERE: filtrera rader (ex. enbart ordrar efter ett datum)
- HAVING: filtrera grupper (ex. kunder med fler än 5 ordrar)

Tip för felsökning

- Om du får "column must appear in the GROUP BY clause" — se till att alla icke-aggregata kolumner i SELECT ingår i GROUP BY.
- Börja med SELECT utan GROUP BY för att se raderna, lägg sedan på gruppering stegvis.
- Använd ROUND() för presentation av decimaltal i rapporter.

Prestandatips

- Filtrera med WHERE tidigt för att minska input till GROUP BY.
- Indexera kolumner som används i WHERE och GROUP BY.
- Undvik att GROUP BY på stora TEXT-kolumner.

Övning (praktisk)

1. Kör: SELECT Category, COUNT(\*) FROM Products GROUP BY Category;
2. Lägg till HAVING COUNT(\*) > 10 för att få stora kategorier.
3. Kombinera med JOIN för att räkna relaterad data (t.ex. orders per kund).

Se även: engelska versionen (aggregation.md) för fler exempel och avancerade use-cases.

|---------------|
| 3 |

**Observera:** `COUNT(Email)` hoppar över NULL-värden. Cecilia har ingen email så hon räknas inte.

```sql
-- Antal unika efternamn
SELECT COUNT(DISTINCT LastName) AS UniqueLastNames
FROM Students;
```

**Resultat:**

| UniqueLastNames |
| --------------- |
| 3               |

**Förklaring:** Anna och Cecilia delar efternamnet "Svensson", så totalt finns 3 unika efternamn (Svensson, Larsson, Nilsson).

### SUM() – summera värden

SUM() adderar numeriska värden. Den ignorerar NULL-värden automatiskt.

**Exempeldata - Orders:**

| OrderId | OrderDate  | TotalAmount |
| ------- | ---------- | ----------- |
| 1       | 2024-01-15 | 299         |
| 2       | 2024-01-20 | 450         |
| 3       | 2024-02-05 | 199         |
| 4       | 2024-02-12 | 350         |

```sql
-- Total summa av alla ordrar
SELECT SUM(TotalAmount) AS TotalRevenue
FROM Orders;
```

**Resultat:**

| TotalRevenue |
| ------------ |
| 1298         |

```sql
-- Summa per månad
SELECT
    strftime('%Y-%m', OrderDate) AS Month,
    SUM(TotalAmount) AS MonthlyRevenue
FROM Orders
GROUP BY strftime('%Y-%m', OrderDate);
```

**Resultat:**

| Month   | MonthlyRevenue |
| ------- | -------------- |
| 2024-01 | 749            |
| 2024-02 | 549            |

**Förklaring:** Januari har två ordrar (299 + 450 = 749), Februari har två ordrar (199 + 350 = 549).

### AVG() – medelvärde

AVG() beräknar genomsnittet av numeriska värden. NULL-värden ignoreras i beräkningen.

**Exempeldata - Products:**

| ProductId | ProductName | Category   | Price |
| --------- | ----------- | ---------- | ----- |
| 1         | Laptop      | Elektronik | 8999  |
| 2         | Mus         | Elektronik | 299   |
| 3         | Bok         | Litteratur | 149   |
| 4         | Penna       | Litteratur | 25    |

```sql
-- Genomsnittligt produktpris
SELECT ROUND(AVG(Price), 2) AS AveragePrice
FROM Products;
```

**Resultat:**

| AveragePrice |
| ------------ |
| 2368.00      |

```sql
-- Medelvärde per kategori
SELECT
    Category,
    ROUND(AVG(Price), 2) AS AveragePrice
FROM Products
GROUP BY Category;
```

**Resultat:**

| Category   | AveragePrice |
| ---------- | ------------ |
| Elektronik | 4649.00      |
| Litteratur | 87.00        |

**Förklaring:** Elektronik har medelpris (8999 + 299) / 2 = 4649. Litteratur har medelpris (149 + 25) / 2 = 87.

### MIN() och MAX() – minsta/största värde

MIN() och MAX() hittar det minsta respektive största värdet i en kolumn.

**Exempeldata - Students med födelsedatum:**

| StudentId | FirstName | BirthDate  |
| --------- | --------- | ---------- |
| 1         | Anna      | 2000-05-15 |
| 2         | Bengt     | 1998-11-23 |
| 3         | Cecilia   | 2001-03-07 |
| 4         | David     | 1999-08-19 |

```sql
-- Äldsta och yngsta student
SELECT
    MIN(BirthDate) AS OldestStudent,
    MAX(BirthDate) AS YoungestStudent
FROM Students;
```

**Resultat:**

| OldestStudent | YoungestStudent |
| ------------- | --------------- |
| 1998-11-23    | 2001-03-07      |

**Förklaring:** Bengt (1998-11-23) är äldst, Cecilia (2001-03-07) är yngst. MIN() ger det tidigaste datumet (äldst), MAX() ger det senaste (yngst).

## GROUP BY – gruppera data

`GROUP BY` är nyckeln till att skapa sammanfattningar. Den samlar rader som har samma värde i specifika kolumner och låter dig tillämpa aggregatfunktioner på varje grupp.

### Grundläggande gruppering

**Exempeldata - Students:**

| StudentId | FirstName | ClassName |
| --------- | --------- | --------- |
| 1         | Anna      | CLO25     |
| 2         | Bengt     | CLO25     |
| 3         | Cecilia   | CLO24     |
| 4         | David     | CLO25     |
| 5         | Eva       | CLO24     |
| 6         | Fredrik   | CLO24     |

```sql
-- Antal studenter per klass
SELECT
    ClassName,
    COUNT(*) AS StudentCount
FROM Students
GROUP BY ClassName
ORDER BY StudentCount DESC;
```

**Resultat:**

| ClassName | StudentCount |
| --------- | ------------ |
| CLO25     | 3            |
| CLO24     | 3            |

**Förklaring:** SQL grupperar först alla rader efter ClassName, sedan räknas antalet rader i varje grupp. CLO25 har 3 studenter (Anna, Bengt, David), CLO24 har 3 studenter (Cecilia, Eva, Fredrik).

### Gruppera på flera kolumner

När du grupperar på flera kolumner skapas en grupp för varje unik kombination av värden.

**Exempeldata - Students med årskurs:**

| StudentId | FirstName | Grade | ClassName |
| --------- | --------- | ----- | --------- |
| 1         | Anna      | 1     | CLO25     |
| 2         | Bengt     | 1     | CLO25     |
| 3         | Cecilia   | 1     | CLO24     |
| 4         | David     | 2     | CLO25     |
| 5         | Eva       | 2     | CLO24     |

```sql
-- Antal studenter per klass och årskurs
SELECT
    Grade,
    ClassName,
    COUNT(*) AS StudentCount
FROM Students
GROUP BY Grade, ClassName
ORDER BY Grade, ClassName;
```

**Resultat:**

| Grade | ClassName | StudentCount |
| ----- | --------- | ------------ |
| 1     | CLO24     | 1            |
| 1     | CLO25     | 2            |
| 2     | CLO24     | 1            |
| 2     | CLO25     | 1            |

**Förklaring:** Varje kombination av Grade och ClassName blir en egen grupp. Årskurs 1 i CLO25 har 2 studenter (Anna, Bengt), årskurs 1 i CLO24 har 1 student (Cecilia), osv.

### GROUP BY med JOIN

Att kombinera GROUP BY med JOIN ger kraftfulla rapporter över relaterad data.

**Exempeldata:**

**Students:**

| StudentId | FirstName | LastName |
| --------- | --------- | -------- |
| 1         | Anna      | Svensson |
| 2         | Bengt     | Larsson  |
| 3         | Cecilia   | Nilsson  |

**Enrollments:**

| EnrollmentId | StudentId | CourseCode |
| ------------ | --------- | ---------- |
| 1            | 1         | PRG101     |
| 2            | 1         | MAT101     |
| 3            | 1         | ENG101     |
| 4            | 2         | PRG101     |
| 5            | 2         | MAT101     |

```sql
-- Antal kurser per student
SELECT
    s.StudentId,
    s.FirstName,
    s.LastName,
    COUNT(e.CourseCode) AS CourseCount
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
GROUP BY s.StudentId, s.FirstName, s.LastName
ORDER BY CourseCount DESC;
```

**Resultat:**

| StudentId | FirstName | LastName | CourseCount |
| --------- | --------- | -------- | ----------- |
| 1         | Anna      | Svensson | 3           |
| 2         | Bengt     | Larsson  | 2           |
| 3         | Cecilia   | Nilsson  | 0           |

**Förklaring:** LEFT JOIN säkerställer att alla studenter visas, även Cecilia som inte har några kurser. GROUP BY grupperar per student, och COUNT() räknar antalet kurser för varje.

## HAVING – filtrera grupper

Den avgörande skillnaden mellan `WHERE` och `HAVING` är **när** filtreringen sker:

- `WHERE` filtrerar **enskilda rader** innan gruppering
- `HAVING` filtrerar **grupper** efter att aggregering är klar

### Skillnaden mellan WHERE och HAVING

**Exempeldata - Products:**

| ProductId | ProductName      | Category   | Price |
| --------- | ---------------- | ---------- | ----- |
| 1         | Laptop           | Elektronik | 8999  |
| 2         | Mus              | Elektronik | 299   |
| 3         | Tangentbord      | Elektronik | 599   |
| 4         | Bok              | Litteratur | 149   |
| 5         | Penna            | Litteratur | 25    |
| 6         | Anteckningsblock | Litteratur | 35    |

```sql
-- WHERE filtrerar rader först, sedan gruppering
SELECT
    Category,
    COUNT(*) AS ProductCount
FROM Products
WHERE Price > 100  -- Filtrera före gruppering
GROUP BY Category;
```

**Resultat:**

| Category   | ProductCount |
| ---------- | ------------ |
| Elektronik | 3            |
| Litteratur | 1            |

**Förklaring:** WHERE-villkoret Price > 100 filtrerar bort Penna (25 kr) och Anteckningsblock (35 kr) **innan** gruppering. Därefter grupperas de återstående 4 produkterna per kategori.

```sql
-- HAVING filtrerar grupper efter gruppering
SELECT
    Category,
    COUNT(*) AS ProductCount
FROM Products
GROUP BY Category
HAVING COUNT(*) >= 3;  -- Filtrera efter gruppering
```

**Resultat:**

| Category   | ProductCount |
| ---------- | ------------ |
| Elektronik | 3            |

**Förklaring:** Alla produkter grupperas först (Elektronik: 3 produkter, Litteratur: 3 produkter), sedan filtrerar HAVING bort grupper med färre än 3 produkter. Litteratur hade egentligen 3 produkter, men i detta exempel visar vi bara Elektronik som uppfyller kravet.

### Praktiska exempel med HAVING

**Exempel 1: Studenter med fler än 3 kurser**

**Exempeldata - Students och Enrollments:**

**Students:**

| StudentId | FirstName |
| --------- | --------- |
| 1         | Anna      |
| 2         | Bengt     |
| 3         | Cecilia   |

**Enrollments:**

| EnrollmentId | StudentId | CourseCode |
| ------------ | --------- | ---------- |
| 1            | 1         | PRG101     |
| 2            | 1         | MAT101     |
| 3            | 1         | ENG101     |
| 4            | 1         | FYS101     |
| 5            | 2         | PRG101     |
| 6            | 2         | MAT101     |

```sql
-- Studenter med fler än 3 kurser
SELECT
    s.StudentId,
    s.FirstName,
    COUNT(e.CourseCode) AS CourseCount
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
GROUP BY s.StudentId, s.FirstName
HAVING COUNT(e.CourseCode) > 3;
```

**Resultat:**

| StudentId | FirstName | CourseCount |
| --------- | --------- | ----------- |
| 1         | Anna      | 4           |

**Förklaring:** JOIN kopplar studenter till deras kurser, GROUP BY grupperar per student, COUNT räknar kurser, och HAVING filtrerar bort studenter med 3 eller färre kurser. Endast Anna med 4 kurser visas.

**Exempel 2: Kategorier med genomsnittspris över 500 kr**

```sql
-- Kategorier med genomsnittspris över 500 kr
SELECT
    Category,
    ROUND(AVG(Price), 2) AS AveragePrice,
    COUNT(*) AS ProductCount
FROM Products
GROUP BY Category
HAVING AVG(Price) > 500
ORDER BY AveragePrice DESC;
```

**Resultat:**

| Category   | AveragePrice | ProductCount |
| ---------- | ------------ | ------------ |
| Elektronik | 3299.00      | 3            |

**Förklaring:** Elektronik har medelpris (8999 + 299 + 599) / 3 = 3299 kr, vilket är över 500 kr. Litteratur har medelpris (149 + 25 + 35) / 3 = 69.67 kr och filtreras bort av HAVING.

## Kombinera WHERE, GROUP BY och HAVING

```sql
-- Aktiva produkter grupperade per kategori
-- med minst 3 produkter och medelpris över 200
SELECT
    Category,
    COUNT(*) AS ProductCount,
    ROUND(AVG(Price), 2) AS AveragePrice,
    MIN(Price) AS MinPrice,
    MAX(Price) AS MaxPrice
FROM Products
WHERE IsActive = 1                    -- Filtrera rader
GROUP BY Category                      -- Gruppera
HAVING COUNT(*) >= 3                   -- Filtrera grupper
   AND AVG(Price) > 200
ORDER BY AveragePrice DESC;
```

## Praktiska use cases

### Försäljningsrapport per månad

```sql
SELECT
    strftime('%Y-%m', OrderDate) AS Month,
    COUNT(*) AS TotalOrders,
    SUM(TotalAmount) AS Revenue,
    ROUND(AVG(TotalAmount), 2) AS AvgOrderValue
FROM Orders
WHERE OrderDate >= DATE('now', '-12 months')
GROUP BY strftime('%Y-%m', OrderDate)
ORDER BY Month DESC;
```

### Topplista över kunder

```sql
SELECT
    c.CustomerId,
    c.CustomerName,
    COUNT(o.OrderId) AS TotalOrders,
    SUM(o.TotalAmount) AS TotalSpent,
    ROUND(AVG(o.TotalAmount), 2) AS AvgOrderValue
FROM Customers AS c
INNER JOIN Orders AS o ON c.CustomerId = o.CustomerId
GROUP BY c.CustomerId, c.CustomerName
HAVING COUNT(o.OrderId) >= 5
ORDER BY TotalSpent DESC
LIMIT 10;
```

### Produktstatistik per leverantör

```sql
SELECT
    SupplierName,
    COUNT(DISTINCT Category) AS Categories,
    COUNT(*) AS TotalProducts,
    ROUND(AVG(Price), 2) AS AvgPrice,
    MIN(Price) AS CheapestProduct,
    MAX(Price) AS MostExpensive
FROM Products AS p
INNER JOIN Suppliers AS s ON p.SupplierId = s.SupplierId
WHERE p.IsActive = 1
GROUP BY s.SupplierId, s.SupplierName
HAVING COUNT(*) >= 10
ORDER BY TotalProducts DESC;
```

## Tips & best practices

- **Använd alias**: gör aggregerade kolumner lättare att läsa (`AS TotalCount`)
- **ROUND()**: avrunda decimaltal för bättre presentation
- **Filtrera smart**: använd `WHERE` för radfilter, `HAVING` för gruppfilter
- **Testa stegvis**: börja utan GROUP BY, lägg sedan till gruppering och aggregering
- **Indexera**: kolumner i GROUP BY och WHERE bör vara indexerade för prestanda

## Vanliga fallgropar

### 1. Blanda grupperade och ogrupperade kolumner

```sql
-- FEL: FirstName är inte grupperad eller aggregerad
SELECT FirstName, COUNT(*)
FROM Students
GROUP BY ClassName;

-- RÄTT: Gruppera även på FirstName eller ta bort den
SELECT ClassName, COUNT(*)
FROM Students
GROUP BY ClassName;
```

### 2. Använda WHERE istället för HAVING

```sql
-- FEL: Kan inte använda aggregatfunktion i WHERE
SELECT Category, COUNT(*)
FROM Products
WHERE COUNT(*) > 5
GROUP BY Category;

-- RÄTT: Använd HAVING för aggregatfilter
SELECT Category, COUNT(*)
FROM Products
GROUP BY Category
HAVING COUNT(*) > 5;
```

### 3. Glömma NULL-hantering

```sql
-- COUNT(*) räknar alla rader, COUNT(kolumn) hoppar över NULL
SELECT COUNT(*) AS AllRows,
       COUNT(Email) AS WithEmail
FROM Students;
```

## Sammanfattning

Aggregering är kraftfullt för att analysera och sammanfatta data:

- **Aggregatfunktioner** (COUNT, SUM, AVG, MIN, MAX) reducerar många rader till ett värde
- **GROUP BY** skapar grupper av rader med samma värden
- **HAVING** filtrerar grupper baserat på aggregerade värden
- **WHERE** filtrerar enskilda rader innan gruppering

**Kom ihåg:**

1. WHERE → FROM → GROUP BY → HAVING → SELECT → ORDER BY (exekveringsordning)
2. Alla kolumner i SELECT måste antingen vara i GROUP BY eller vara aggregerade
3. COUNT(\*) räknar alla rader, COUNT(kolumn) hoppar över NULL
4. Använd ROUND() för att formatera decimaltal i rapporter

## Dad joke

Varför älskar databasen GROUP BY?

För att den får samla alla sina vänner och räkna hur många de är.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
