# GROUP BY, HAVING och aggregering 📊

🟢


**Räkna, summera och analysera data**

*Marcus Ackre Medina*
*Campus Mölndal - CLO25*
---

## Dagens agenda

1. **Aggregatfunktioner** - COUNT, SUM, AVG, MIN, MAX
2. **GROUP BY** - Gruppera data
3. **HAVING** - Filtrera grupper
4. **WHERE vs HAVING** - När ska man använda vad?
5. **Praktiska exempel** - Rapporter och statistik

---

## Vad är aggregering?

**Aggregering = sammanfatta data**

- Räkna antal poster
- Summera värden
- Beräkna medelvärden
- Hitta min/max-värden
- Gruppera liknande data

**Varför?** För att få insikter ur stora datamängder!

---

## Aggregatfunktioner - COUNT()

**Räkna antal rader**

```sql
-- Totalt antal studenter
SELECT COUNT(*) AS TotalStudents
FROM Students;

-- Antal studenter med email
SELECT COUNT(Email) AS StudentsWithEmail
FROM Students;

-- Antal unika efternamn
SELECT COUNT(DISTINCT LastName) AS UniqueLastNames
FROM Students;
```

**Tips:** `COUNT(*)` räknar alla rader, `COUNT(kolumn)` hoppar över NULL

---

## Aggregatfunktioner - SUM()

**Summera numeriska värden**

```sql
-- Total omsättning
SELECT SUM(TotalAmount) AS TotalRevenue
FROM Orders;

-- Total lagersaldo värde
SELECT SUM(Quantity * Price) AS TotalInventoryValue
FROM Products;
```

**OBS:** SUM() fungerar bara på numeriska kolumner!

---

## Aggregatfunktioner - AVG()

**Beräkna medelvärde**

```sql
-- Genomsnittligt ordervärde
SELECT AVG(TotalAmount) AS AverageOrderValue
FROM Orders;

-- Avrunda till 2 decimaler
SELECT ROUND(AVG(Price), 2) AS AveragePrice
FROM Products;
```

**Tips:** Använd `ROUND()` för snyggare resultat

---

## Aggregatfunktioner - MIN() och MAX()

**Hitta minsta och största värde**

```sql
-- Billigaste och dyraste produkt
SELECT
    MIN(Price) AS CheapestProduct,
    MAX(Price) AS MostExpensive
FROM Products;

-- Äldsta och senaste order
SELECT
    MIN(OrderDate) AS FirstOrder,
    MAX(OrderDate) AS LatestOrder
FROM Orders;
```

---

## GROUP BY - Grundläggande gruppering

**Gruppera rader med samma värde**

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
|-----------|--------------|
| CLO25     | 28           |
| CLO24     | 25           |

---

## GROUP BY - Flera kolumner

**Gruppera på flera nivåer**

```sql
-- Antal studenter per årskurs och klass
SELECT
    Grade,
    ClassName,
    COUNT(*) AS StudentCount
FROM Students
GROUP BY Grade, ClassName
ORDER BY Grade, ClassName;
```

**Resultat blir grupperat på både Grade OCH ClassName**

---

## GROUP BY med flera aggregat

**Kombinera olika aggregatfunktioner**

```sql
SELECT
    Category,
    COUNT(*) AS ProductCount,
    ROUND(AVG(Price), 2) AS AvgPrice,
    MIN(Price) AS MinPrice,
    MAX(Price) AS MaxPrice,
    SUM(Quantity) AS TotalStock
FROM Products
GROUP BY Category
ORDER BY AvgPrice DESC;
```

---

## HAVING - Filtrera grupper

**WHERE filtrerar rader, HAVING filtrerar grupper**

```sql
-- Kategorier med fler än 5 produkter
SELECT
    Category,
    COUNT(*) AS ProductCount
FROM Products
GROUP BY Category
HAVING COUNT(*) > 5;
```

**HAVING används EFTER GROUP BY!**

---

## WHERE vs HAVING

**Viktiga skillnader:**

| WHERE                         | HAVING                        |
|-------------------------------|-------------------------------|
| Filtrerar **rader**           | Filtrerar **grupper**         |
| Används **före** GROUP BY     | Används **efter** GROUP BY    |
| Kan INTE använda aggregat     | Kan använda aggregat          |

---

## WHERE vs HAVING - Exempel

```sql
-- WHERE filtrerar rader FÖRST
SELECT Category, COUNT(*) AS ProductCount
FROM Products
WHERE Price > 100          -- Filtrera före gruppering
GROUP BY Category;

-- HAVING filtrerar grupper EFTER
SELECT Category, COUNT(*) AS ProductCount
FROM Products
GROUP BY Category
HAVING COUNT(*) >= 5;      -- Filtrera efter gruppering
```

---

## Kombinera WHERE och HAVING

**Bäst av båda världar!**

```sql
SELECT
    Category,
    COUNT(*) AS ProductCount,
    ROUND(AVG(Price), 2) AS AvgPrice
FROM Products
WHERE IsActive = 1              -- Filtrera rader först
GROUP BY Category               -- Gruppera
HAVING COUNT(*) >= 3            -- Filtrera grupper
   AND AVG(Price) > 200
ORDER BY AvgPrice DESC;
```

**WHERE → GROUP BY → HAVING → ORDER BY**

---

## Praktiskt exempel - Försäljningsrapport

**Månadsrapport för senaste året**

```sql
SELECT
    strftime('%Y-%m', OrderDate) AS Month,
    COUNT(*) AS TotalOrders,
    SUM(TotalAmount) AS Revenue,
    ROUND(AVG(TotalAmount), 2) AS AvgOrderValue,
    COUNT(DISTINCT CustomerId) AS UniqueCustomers
FROM Orders
WHERE OrderDate >= DATE('now', '-12 months')
GROUP BY strftime('%Y-%m', OrderDate)
ORDER BY Month DESC;
```

---

## Praktiskt exempel - Topplista kunder

**Hitta bästa kunderna**

```sql
SELECT
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

---

## Praktiskt exempel - Produktstatistik

**Analys per leverantör**

```sql
SELECT
    s.SupplierName,
    COUNT(DISTINCT p.Category) AS Categories,
    COUNT(*) AS TotalProducts,
    ROUND(AVG(p.Price), 2) AS AvgPrice,
    MIN(p.Price) AS Cheapest,
    MAX(p.Price) AS MostExpensive
FROM Products AS p
INNER JOIN Suppliers AS s ON p.SupplierId = s.SupplierId
WHERE p.IsActive = 1
GROUP BY s.SupplierId, s.SupplierName
HAVING COUNT(*) >= 10
ORDER BY TotalProducts DESC;
```

---

## Vanliga fallgropar ⚠️

### 1. Blanda grupperade och ogrupperade kolumner

```sql
-- ❌ FEL: FirstName är inte grupperad
SELECT FirstName, COUNT(*)
FROM Students
GROUP BY ClassName;

-- ✅ RÄTT
SELECT ClassName, COUNT(*)
FROM Students
GROUP BY ClassName;
```

---

## Vanliga fallgropar ⚠️

### 2. Använda aggregat i WHERE

```sql
-- ❌ FEL: Kan inte använda COUNT() i WHERE
SELECT Category, COUNT(*)
FROM Products
WHERE COUNT(*) > 5
GROUP BY Category;

-- ✅ RÄTT: Använd HAVING
SELECT Category, COUNT(*)
FROM Products
GROUP BY Category
HAVING COUNT(*) > 5;
```

---

## Vanliga fallgropar ⚠️

### 3. Glömma NULL-hantering

```sql
-- COUNT(*) räknar ALLA rader
-- COUNT(kolumn) hoppar över NULL

SELECT
    COUNT(*) AS AllStudents,
    COUNT(Email) AS StudentsWithEmail,
    COUNT(*) - COUNT(Email) AS StudentsWithoutEmail
FROM Students;
```

---

## Tips & Best Practices 💡

1. **Använd alias** - `AS TotalCount` gör det lättare att läsa
2. **ROUND()** - avrunda decimaltal för bättre presentation
3. **Filtrera smart** - WHERE för rader, HAVING för grupper
4. **Testa stegvis** - börja utan GROUP BY, lägg till efterhand
5. **Indexera** - kolumner i GROUP BY bör vara indexerade

---

## Exekveringsordning i SQL

**SQL körs inte i den ordning du skriver!**

```sql
SELECT Category, COUNT(*) AS Total     -- 5. Välj kolumner
FROM Products                          -- 1. Från tabell
WHERE IsActive = 1                     -- 2. Filtrera rader
GROUP BY Category                      -- 3. Gruppera
HAVING COUNT(*) > 5                    -- 4. Filtrera grupper
ORDER BY Total DESC                    -- 6. Sortera
LIMIT 10;                              -- 7. Begränsa
```

---

## Övning 1 - Grön 🟢

**Uppgift:**
Räkna antal studenter per klass och sortera på flest först

**Tips:**
- Använd `COUNT(*)`
- Gruppera på `ClassName`
- Sortera med `ORDER BY ... DESC`

---

## Övning 2 - Gul 🟡

**Uppgift:**
Visa klasser med fler än 20 studenter och deras genomsnittsålder

**Tips:**
- Beräkna ålder från födelsedatum
- Använd `AVG()` och `ROUND()`
- Filtrera med `HAVING COUNT(*) > 20`

---

## Övning 3 - Röd 🔴

**Uppgift:**
Skapa en månatlig försäljningsrapport för 2024 som visar:
- Månad
- Antal ordrar
- Total omsättning
- Genomsnittligt ordervärde
- Visa endast månader med över 100 ordrar

---

## Sammanfattning

✅ **Aggregatfunktioner** - COUNT, SUM, AVG, MIN, MAX
✅ **GROUP BY** - gruppera data för analys
✅ **HAVING** - filtrera grupper efter aggregering
✅ **WHERE** - filtrera rader före gruppering
✅ **Kombinera** - få kraftfulla rapporter och statistik

**Nästa gång:** JOIN - koppla ihop tabeller!

---

## Frågor? 🤔

**Resurser:**
- Handbok: `C-Sharp/databases/sql/aggregering.md`
- SQLite dokumentation: https://sqlite.org/lang_aggfunc.html
- Övningar i kursmaterialet

**Dad joke:**
Varför älskar databasen GROUP BY?
*För att den får samla alla sina vänner och räkna hur många de är.* 😄

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
