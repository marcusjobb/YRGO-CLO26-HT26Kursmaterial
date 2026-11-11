# Workshop: Denormalisering för AI/ML Träningsdata (SQLite-version)

🟢


## Scenario

Ni jobbar för **WildShop AB** igen, men nu med ett nytt uppdrag! Efter att ni räddade databasen igår vill företaget nu använda sin data för Machine Learning. De vill förutsäga:

1. **Churn Prediction**: Vilka kunder kommer sluta handla?
2. **Product Recommendations**: Vilka produkter ska rekommenderas till vilka kunder?
3. **Sales Forecasting**: Hur mycket kommer de sälja nästa månad?

**Problemet**: Era perfekt normaliserade tabeller från igår är INTE lämpliga för ML-algoritmer!

**Uppdraget**: Transformera den normaliserade databasen till denormaliserade träningsset.

**OBS**: Denna version använder SQLite-syntax!

---

## SQLite Setup

```sql
-- Aktivera foreign keys
PRAGMA foreign_keys = ON;

-- Kontrollera SQLite-version (Window functions kräver 3.25+)
SELECT sqlite_version();
```

---

## Del 1: Förstå Målet (30 min)

### Uppgift 1.1: Analysera ML-krav

För varje ML-uppgift, besvara:

**Churn Prediction (Kommer kunden sluta handla?)**

1. Vad är en "observation" (rad i träningsdata)?
   - [ ] En order
   - [ ] En kund
   - [ ] En produkt
   - [ ] Något annat?

2. Vilka features (kolumner) skulle vara användbara?
   - Exempel: Antal ordrar, dagar sedan senaste köp, total spenderad summa...
   - Lista minst 10 features!

3. Vad är target-variabeln (vad ska förutsägas)?
   - [ ] Kommer handla igen inom 30 dagar (Ja/Nej)
   - [ ] Kommer handla igen inom 90 dagar (Ja/Nej)
   - [ ] Antal framtida ordrar (0, 1, 2, ...)
   - [ ] Något annat?

**Er analys här (skriv ner era svar):**
```
Observation: _______________________
Features: _________________________
Target: ___________________________
```

### Lösningsförslag (Dolt)

<details>
<summary>Klicka för lösning</summary>

**Churn Prediction:**

**Observation**: En kund (EN rad per kund)

**Features** (exempel på 15+ användbara):
1. `TotalOrders` - Antal totala ordrar
2. `TotalSpent` - Total spenderad summa
3. `AvgOrderValue` - Genomsnittligt ordervärde
4. `DaysSinceLastOrder` - Dagar sedan senaste köp
5. `DaysSinceRegistration` - Dagar sedan registrering
6. `OrderFrequency` - Ordrar per månad
7. `FavoriteCategory` - Mest köpta kategorin
8. `CategoryDiversity` - Antal olika kategorier köpta
9. `HasReturned` - Har gjort retur? (boolean)
10. `PreferredPaymentMethod` - Vanligaste betalmetod
11. `PreferredShippingMethod` - Vanligaste leveransmetod
12. `AvgDaysBetweenOrders` - Genomsnittlig tid mellan ordrar
13. `LastOrderValue` - Värde på senaste ordern
14. `IsPremiumCustomer` - Spenderat > 10,000 kr
15. `CategoryElectronicsCount` - Antal elektronikköp
16. `CategoryCampingCount` - Antal campingköp

**Target**: `WillChurnIn30Days` (INTEGER 0/1) - baserat på om de har order inom 30 dagar framåt

**Varför denna struktur?**
- EN rad = EN kund = EN träningsexempel
- Alla features på samma rad (inga JOINs behövs senare)
- Numeriska värden (lätt för algoritmer att hantera)

</details>

---

## Del 2: Skapa Features från Normaliserad Data (90 min)

Nu ska ni använda er normaliserade databas från igår för att skapa denormaliserade träningsdata.

### Uppgift 2.1: Grundläggande Aggregering

**Mål**: Skapa en tabell med kunddata + aggregerad orderstatistik

```sql

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
CREATE TABLE Customer_Features AS
SELECT
    c.CustomerID,
    c.Name,
    c.Email,
    c.RegistrationDate,

    -- Aggregerade features
    COUNT(DISTINCT o.OrderID) as TotalOrders,
    COALESCE(SUM(o.TotalAmount), 0) as TotalSpent,
    COALESCE(AVG(o.TotalAmount), 0) as AvgOrderValue,

    -- Tidsdata (SQLite använder julianday för datumberäkningar)
    MIN(o.OrderDate) as FirstOrderDate,
    MAX(o.OrderDate) as LastOrderDate,

    -- DaysSinceLastOrder: Dagar sedan senaste order
    COALESCE(
        CAST((julianday('now') - julianday(MAX(o.OrderDate))) AS INTEGER),
        999
    ) as DaysSinceLastOrder,

    -- CustomerLifetimeDays: Dagar mellan första och senaste order
    COALESCE(
        CAST((julianday(MAX(o.OrderDate)) - julianday(MIN(o.OrderDate))) AS INTEGER),
        0
    ) as CustomerLifetimeDays,

    -- DaysSinceRegistration
    CAST((julianday('now') - julianday(c.RegistrationDate)) AS INTEGER) as DaysSinceRegistration

FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID, c.Name, c.Email, c.RegistrationDate;
```

**SQLite Datum-Funktioner:**
- `julianday('now')` - Dagens datum som Julian Day Number
- `julianday(date_column)` - Konvertera datum till JDN
- `CAST(... AS INTEGER)` - Konvertera till heltal (dagar)

**Er uppgift**: Verifiera att tabellen skapades korrekt:

```sql
-- Kontrollera struktur
PRAGMA table_info(Customer_Features);

-- Se exempel
SELECT * FROM Customer_Features LIMIT 5;

-- Kontrollera statistik
SELECT
    COUNT(*) as TotalCustomers,
    AVG(TotalOrders) as AvgOrdersPerCustomer,
    AVG(DaysSinceLastOrder) as AvgDaysSinceLastOrder
FROM Customer_Features;
```

---

### Uppgift 2.2: Kategori-Features (One-Hot Encoding)

**Mål**: Lägg till features för varje produktkategori

Först, kolla vilka kategorier som finns:

```sql
SELECT DISTINCT Category FROM Products ORDER BY Category;
```

Skapa sedan en utökad version av Customer_Features:

```sql
-- Ta bort gamla tabellen
DROP TABLE IF EXISTS Customer_Features;

-- Skapa ny med kategori-features
CREATE TABLE Customer_Features AS
SELECT
    c.CustomerID,
    c.Name,
    c.Email,
    c.RegistrationDate,

    -- Grundläggande aggregering
    COUNT(DISTINCT o.OrderID) as TotalOrders,
    COALESCE(SUM(o.TotalAmount), 0) as TotalSpent,
    COALESCE(AVG(o.TotalAmount), 0) as AvgOrderValue,

    -- Tidsdata
    MIN(o.OrderDate) as FirstOrderDate,
    MAX(o.OrderDate) as LastOrderDate,
    COALESCE(
        CAST((julianday('now') - julianday(MAX(o.OrderDate))) AS INTEGER),
        999
    ) as DaysSinceLastOrder,
    COALESCE(
        CAST((julianday(MAX(o.OrderDate)) - julianday(MIN(o.OrderDate))) AS INTEGER),
        0
    ) as CustomerLifetimeDays,
    CAST((julianday('now') - julianday(c.RegistrationDate)) AS INTEGER) as DaysSinceRegistration,

    -- Kategori-features (Quantity) - Anpassa efter era kategorier!
    SUM(CASE WHEN p.Category = 'Camping' THEN od.Quantity ELSE 0 END) as QtyCamping,
    SUM(CASE WHEN p.Category = 'Vandring' THEN od.Quantity ELSE 0 END) as QtyVandring,
    SUM(CASE WHEN p.Category = 'Klättring' THEN od.Quantity ELSE 0 END) as QtyKlattring,
    SUM(CASE WHEN p.Category = 'Cykling' THEN od.Quantity ELSE 0 END) as QtyCykling,
    SUM(CASE WHEN p.Category = 'Skidor' THEN od.Quantity ELSE 0 END) as QtySkidor,
    SUM(CASE WHEN p.Category = 'Skor' THEN od.Quantity ELSE 0 END) as QtySkor,

    -- Kategori-features (Spent Amount)
    SUM(CASE WHEN p.Category = 'Camping' THEN od.Subtotal ELSE 0 END) as SpentCamping,
    SUM(CASE WHEN p.Category = 'Vandring' THEN od.Subtotal ELSE 0 END) as SpentVandring,
    SUM(CASE WHEN p.Category = 'Klättring' THEN od.Subtotal ELSE 0 END) as SpentKlattring,
    SUM(CASE WHEN p.Category = 'Cykling' THEN od.Subtotal ELSE 0 END) as SpentCykling,
    SUM(CASE WHEN p.Category = 'Skidor' THEN od.Subtotal ELSE 0 END) as SpentSkidor,
    SUM(CASE WHEN p.Category = 'Skor' THEN od.Subtotal ELSE 0 END) as SpentSkor,

    -- Diversity metric
    COUNT(DISTINCT p.Category) as CategoryDiversity

FROM Customers c
LEFT JOIN Orders o ON c.CustomerID = o.CustomerID
LEFT JOIN OrderDetails od ON o.OrderID = od.OrderID
LEFT JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CustomerID, c.Name, c.Email, c.RegistrationDate;
```

**Uppgift**: Anpassa kategorierna efter era produkter och verifiera:

```sql
SELECT
    CustomerID,
    Name,
    TotalOrders,
    QtyCamping,
    QtyVandring,
    CategoryDiversity
FROM Customer_Features
WHERE TotalOrders > 0
LIMIT 10;
```

---

### Uppgift 2.3: RFM-Analys (Recency, Frequency, Monetary)

RFM är en klassisk metod för kundsegmentering.

**SQLite Window Functions**: Kräver SQLite 3.25 eller senare!

```sql
-- Skapa vy med RFM-scores
CREATE VIEW Customer_Features_RFM AS
SELECT
    *,
    -- RFM Scores (1-5, där 5 är bäst)
    -- SQLite NTILE syntax
    NTILE(5) OVER (ORDER BY DaysSinceLastOrder DESC) as R_Score,
    NTILE(5) OVER (ORDER BY TotalOrders ASC) as F_Score,
    NTILE(5) OVER (ORDER BY TotalSpent ASC) as M_Score,

    -- Kombinerad RFM-score som text
    (NTILE(5) OVER (ORDER BY DaysSinceLastOrder DESC) || '' ||
     NTILE(5) OVER (ORDER BY TotalOrders ASC) || '' ||
     NTILE(5) OVER (ORDER BY TotalSpent ASC)) as RFM_Score,

    -- Segment baserat på RFM
    CASE
        WHEN NTILE(5) OVER (ORDER BY DaysSinceLastOrder DESC) >= 4
         AND NTILE(5) OVER (ORDER BY TotalOrders ASC) >= 4
         AND NTILE(5) OVER (ORDER BY TotalSpent ASC) >= 4
        THEN 'Champions'

        WHEN NTILE(5) OVER (ORDER BY DaysSinceLastOrder DESC) >= 3
         AND NTILE(5) OVER (ORDER BY TotalOrders ASC) >= 3
        THEN 'Loyal'

        WHEN NTILE(5) OVER (ORDER BY DaysSinceLastOrder DESC) <= 2
        THEN 'At Risk'

        ELSE 'Regular'
    END as CustomerSegment

FROM Customer_Features;

-- Testa vyn
SELECT
    CustomerSegment,
    COUNT(*) as Count,
    AVG(TotalSpent) as AvgSpent
FROM Customer_Features_RFM
GROUP BY CustomerSegment
ORDER BY AvgSpent DESC;
```

**Alternativ för äldre SQLite-versioner (utan NTILE):**

Om ni får fel med NTILE, använd denna manual metod:

```sql
-- Manuell RFM-score beräkning för äldre SQLite
CREATE VIEW Customer_Features_RFM_Manual AS
SELECT
    cf.*,
    -- R_Score: Dela in i 5 grupper baserat på ranking
    CASE
        WHEN DaysSinceLastOrder <= (SELECT DaysSinceLastOrder FROM Customer_Features ORDER BY DaysSinceLastOrder LIMIT 1 OFFSET (SELECT COUNT(*)/5 FROM Customer_Features)) THEN 5
        WHEN DaysSinceLastOrder <= (SELECT DaysSinceLastOrder FROM Customer_Features ORDER BY DaysSinceLastOrder LIMIT 1 OFFSET (SELECT COUNT(*)*2/5 FROM Customer_Features)) THEN 4
        WHEN DaysSinceLastOrder <= (SELECT DaysSinceLastOrder FROM Customer_Features ORDER BY DaysSinceLastOrder LIMIT 1 OFFSET (SELECT COUNT(*)*3/5 FROM Customer_Features)) THEN 3
        WHEN DaysSinceLastOrder <= (SELECT DaysSinceLastOrder FROM Customer_Features ORDER BY DaysSinceLastOrder LIMIT 1 OFFSET (SELECT COUNT(*)*4/5 FROM Customer_Features)) THEN 2
        ELSE 1
    END as R_Score,

    -- F_Score och M_Score på liknande sätt...
    -- (förenkla genom att använda percentiler istället)

    CASE
        WHEN TotalOrders >= 10 THEN 5
        WHEN TotalOrders >= 7 THEN 4
        WHEN TotalOrders >= 4 THEN 3
        WHEN TotalOrders >= 2 THEN 2
        ELSE 1
    END as F_Score,

    CASE
        WHEN TotalSpent >= 10000 THEN 5
        WHEN TotalSpent >= 5000 THEN 4
        WHEN TotalSpent >= 2000 THEN 3
        WHEN TotalSpent >= 500 THEN 2
        ELSE 1
    END as M_Score

FROM Customer_Features cf;
```

---

## Del 3: Skapa Target-Variabel (45 min)

Nu behöver vi skapa vad modellen ska förutsäga!

### Uppgift 3.1: Temporal Split

**VIKTIGT**: För tidsseriedata måste vi dela data KRONOLOGISKT, inte slumpmässigt!

**Strategi:**
1. **Training Set**: Data t.o.m. 2024-09-30
2. **Test Set**: Data från 2024-10-01 och framåt

```sql
-- Lägg till target-kolumn
ALTER TABLE Customer_Features
ADD COLUMN IsActive INTEGER;  -- SQLite använder INTEGER för BOOLEAN

-- Sätt target baserat på framtida ordrar
UPDATE Customer_Features
SET IsActive = CASE
    WHEN EXISTS (
        SELECT 1
        FROM Orders o
        WHERE o.CustomerID = Customer_Features.CustomerID
          AND o.OrderDate > '2024-09-30'
    ) THEN 1
    ELSE 0
END;

-- Kontrollera class balance
SELECT
    IsActive,
    COUNT(*) as Count,
    CAST(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM Customer_Features) AS REAL) as Percentage
FROM Customer_Features
GROUP BY IsActive;
```

**Uppgift**: Analysera resultatet. Är class balance bra?
- 50/50 är perfekt
- 70/30 är ok
- 90/10 är problematiskt (behöver balansering)

---

## Del 4: Feature Engineering Avancerat (60 min)

### Uppgift 4.1: Derived Features

Skapa beräknade features från existerande:

```sql
-- Lägg till derived features
ALTER TABLE Customer_Features ADD COLUMN SpendPerDay REAL;
ALTER TABLE Customer_Features ADD COLUMN OrdersPerMonth REAL;
ALTER TABLE Customer_Features ADD COLUMN CustomerTier TEXT;
ALTER TABLE Customer_Features ADD COLUMN IsHighValue INTEGER;
ALTER TABLE Customer_Features ADD COLUMN IsRecentlyActive INTEGER;
ALTER TABLE Customer_Features ADD COLUMN IsDiverseBuyer INTEGER;

-- Uppdatera värden
UPDATE Customer_Features
SET
    SpendPerDay = CASE
        WHEN CustomerLifetimeDays > 0 THEN TotalSpent * 1.0 / CustomerLifetimeDays
        ELSE 0
    END,

    OrdersPerMonth = CASE
        WHEN CustomerLifetimeDays > 0 THEN (TotalOrders * 30.0) / CustomerLifetimeDays
        ELSE 0
    END,

    CustomerTier = CASE
        WHEN TotalSpent > 10000 THEN 'VIP'
        WHEN TotalSpent > 5000 THEN 'Premium'
        WHEN TotalSpent > 1000 THEN 'Regular'
        ELSE 'New'
    END,

    IsHighValue = CASE WHEN TotalSpent > 5000 THEN 1 ELSE 0 END,
    IsRecentlyActive = CASE WHEN DaysSinceLastOrder < 30 THEN 1 ELSE 0 END,
    IsDiverseBuyer = CASE WHEN CategoryDiversity >= 3 THEN 1 ELSE 0 END;

-- Verifiera
SELECT
    CustomerID,
    Name,
    CustomerTier,
    SpendPerDay,
    OrdersPerMonth,
    IsHighValue,
    IsRecentlyActive
FROM Customer_Features
LIMIT 10;
```

### Uppgift 4.2: Text Features från Produktbeskrivningar

```sql
-- Skapa tabell med text-baserade features per kund
CREATE TABLE Customer_Text_Features AS
SELECT
    c.CustomerID,

    -- Räkna produkter med specifika nyckelord
    SUM(CASE WHEN p.Name LIKE '%Premium%' THEN 1 ELSE 0 END) as PremiumProductCount,
    SUM(CASE WHEN p.Name LIKE '%Budget%' OR p.Name LIKE '%Basic%' THEN 1 ELSE 0 END) as BudgetProductCount,
    SUM(CASE WHEN p.Name LIKE '%Pro%' THEN 1 ELSE 0 END) as ProProductCount,

    -- Genomsnittlig produktnamn-längd
    AVG(LENGTH(p.Name)) as AvgProductNameLength,

    -- Genomsnittligt produktpris
    AVG(p.Price) as AvgProductPrice,

    -- Har köpt Limited Edition?
    MAX(CASE WHEN p.Name LIKE '%Limited%' OR p.Name LIKE '%Edition%' THEN 1 ELSE 0 END) as HasBoughtLimitedEdition

FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
JOIN OrderDetails od ON o.OrderID = od.OrderID
JOIN Products p ON od.ProductID = p.ProductID
GROUP BY c.CustomerID;

-- Kolla resultatet
SELECT * FROM Customer_Text_Features LIMIT 10;
```

---

## Del 5: Export och Validation (45 min)

### Uppgift 5.1: Final Feature Table

Kombinera ALLA features till en final tabell:

```sql
CREATE TABLE ML_Training_Data AS
SELECT
    cf.*,
    rfm.R_Score,
    rfm.F_Score,
    rfm.M_Score,
    rfm.CustomerSegment,
    tf.PremiumProductCount,
    tf.BudgetProductCount,
    tf.AvgProductNameLength,
    tf.HasBoughtLimitedEdition
FROM Customer_Features cf
LEFT JOIN Customer_Features_RFM rfm ON cf.CustomerID = rfm.CustomerID
LEFT JOIN Customer_Text_Features tf ON cf.CustomerID = tf.CustomerID;

-- Visa struktur
PRAGMA table_info(ML_Training_Data);

-- Räkna features
SELECT COUNT(*) as TotalFeatures
FROM pragma_table_info('ML_Training_Data');

-- Visa exempel
SELECT * FROM ML_Training_Data LIMIT 5;
```

### Uppgift 5.2: Data Quality Checks

```sql
-- 1. Kontrollera NULL-värden (SQLite-variant)
SELECT
    SUM(CASE WHEN TotalOrders IS NULL THEN 1 ELSE 0 END) as NullTotalOrders,
    SUM(CASE WHEN TotalSpent IS NULL THEN 1 ELSE 0 END) as NullTotalSpent,
    SUM(CASE WHEN IsActive IS NULL THEN 1 ELSE 0 END) as NullIsActive,
    SUM(CASE WHEN R_Score IS NULL THEN 1 ELSE 0 END) as NullRScore
FROM ML_Training_Data;

-- 2. Kontrollera outliers
-- OBS: SQLite har inte inbyggd STDDEV i alla versioner, så vi beräknar manuellt
WITH stats AS (
    SELECT
        MIN(TotalSpent) as MinSpent,
        MAX(TotalSpent) as MaxSpent,
        AVG(TotalSpent) as AvgSpent,
        -- Standard deviation: sqrt(avg(x^2) - avg(x)^2)
        SQRT(AVG(TotalSpent * TotalSpent) - AVG(TotalSpent) * AVG(TotalSpent)) as StdDevSpent
    FROM ML_Training_Data
)
SELECT
    'TotalSpent' as Feature,
    MinSpent as Min,
    MaxSpent as Max,
    AvgSpent as Avg,
    StdDevSpent as StdDev
FROM stats;

-- För TotalOrders
WITH stats AS (
    SELECT
        MIN(TotalOrders) as MinOrders,
        MAX(TotalOrders) as MaxOrders,
        AVG(TotalOrders) as AvgOrders,
        SQRT(AVG(TotalOrders * TotalOrders) - AVG(TotalOrders) * AVG(TotalOrders)) as StdDevOrders
    FROM ML_Training_Data
)
SELECT
    'TotalOrders' as Feature,
    MinOrders as Min,
    MaxOrders as Max,
    AvgOrders as Avg,
    StdDevOrders as StdDev
FROM stats;

-- 3. Kontrollera class balance
SELECT
    IsActive,
    COUNT(*) as Count,
    ROUND(COUNT(*) * 100.0 / (SELECT COUNT(*) FROM ML_Training_Data), 2) as Percentage
FROM ML_Training_Data
GROUP BY IsActive;

-- 4. Kontrollera extrema outliers (> 3 standard deviations)
WITH stats AS (
    SELECT
        AVG(TotalSpent) as AvgSpent,
        SQRT(AVG(TotalSpent * TotalSpent) - AVG(TotalSpent) * AVG(TotalSpent)) as StdDevSpent
    FROM ML_Training_Data
)
SELECT
    CustomerID,
    Name,
    TotalSpent,
    ROUND((TotalSpent - stats.AvgSpent) / stats.StdDevSpent, 2) as ZScore
FROM ML_Training_Data, stats
WHERE ABS((TotalSpent - stats.AvgSpent) / stats.StdDevSpent) > 3;
```

### Uppgift 5.3: Export till CSV (SQLite-metod)

**Metod 1: SQLite Command Line**

```bash
# I terminalen (inte i SQL)
sqlite3 wildshop.db

# Sedan i SQLite prompt
.mode csv
.headers on
.output ml_training_data.csv
SELECT * FROM ML_Training_Data;
.output stdout
```

**Metod 2: Från Python**

```python
import sqlite3
import csv

# Anslut till databas
conn = sqlite3.connect('wildshop.db')
cursor = conn.cursor()

# Hämta data
cursor.execute("SELECT * FROM ML_Training_Data")
rows = cursor.fetchall()

# Hämta kolumnnamn
column_names = [description[0] for description in cursor.description]

# Skriv till CSV
with open('ml_training_data.csv', 'w', newline='', encoding='utf-8') as csvfile:
    writer = csv.writer(csvfile)
    writer.writerow(column_names)
    writer.writerows(rows)

print(f"Exported {len(rows)} rows to ml_training_data.csv")
conn.close()
```

**Metod 3: DB Browser for SQLite (GUI)**

1. Öppna databasen i DB Browser
2. Högerklicka på tabellen `ML_Training_Data`
3. Välj "Export to CSV"
4. Välj destination och filnamn

---

## Del 6: Produkt-Associationer (Bonus) (60 min)

### Uppgift 6.1: "Kunder som köpte X köpte också Y"

```sql
CREATE TABLE Product_Associations AS
SELECT
    od1.ProductID as Product_A,
    p1.Name as Product_A_Name,
    od2.ProductID as Product_B,
    p2.Name as Product_B_Name,
    COUNT(DISTINCT od1.OrderID) as CoOccurrences,

    -- Support: Hur ofta förekommer paret?
    CAST(COUNT(DISTINCT od1.OrderID) * 100.0 / (
        SELECT COUNT(DISTINCT OrderID) FROM OrderDetails
    ) AS REAL) as Support_Percentage,

    -- Confidence: P(B|A)
    CAST(COUNT(DISTINCT od1.OrderID) * 100.0 / (
        SELECT COUNT(DISTINCT OrderID)
        FROM OrderDetails
        WHERE ProductID = od1.ProductID
    ) AS REAL) as Confidence_A_to_B

FROM OrderDetails od1
JOIN OrderDetails od2 ON od1.OrderID = od2.OrderID
    AND od1.ProductID < od2.ProductID  -- Undvik dubbletter
JOIN Products p1 ON od1.ProductID = p1.ProductID
JOIN Products p2 ON od2.ProductID = p2.ProductID
GROUP BY od1.ProductID, p1.Name, od2.ProductID, p2.Name
HAVING CoOccurrences >= 5  -- Minst 5 gemensamma köp
ORDER BY CoOccurrences DESC;

-- Se resultat
SELECT * FROM Product_Associations LIMIT 20;
```

### Uppgift 6.2: Rekommendationer baserat på produkt

```sql
-- Om en kund just köpt "Tält 4-person", vad ska rekommenderas?
SELECT
    pa.Product_B_Name as RecommendedProduct,
    pa.CoOccurrences as TimesBoughtTogether,
    pa.Confidence_A_to_B as ConfidenceScore
FROM Product_Associations pa
JOIN Products p ON pa.Product_A = p.ProductID
WHERE p.Name = 'Tält 4-person'
ORDER BY pa.Confidence_A_to_B DESC
LIMIT 5;
```

---

## Del 7: Reflektion och Dokumentation (30 min)

### Uppgift 7.1: Feature List Dokumentation

Skapa en tabell med alla era features:

| Feature Name | Beskrivning | Datatyp | SQLite-specifikt |
|--------------|-------------|---------|------------------|
| TotalOrders | Antal ordrar per kund | INTEGER | - |
| DaysSinceLastOrder | Dagar sedan senaste köp | INTEGER | julianday() |
| R_Score | RFM Recency Score (1-5) | INTEGER | NTILE() eller manuell |
| SpendPerDay | Spenderat per dag | REAL | Derived feature |

### Uppgift 7.2: SQLite-specifika Utmaningar

Dokumentera vilka SQLite-specifika anpassningar ni gjorde:

1. **Datum-hantering**:
   - Använd `julianday()` istället för `DATEDIFF()`
   - TEXT datatyp för datum (ISO 8601)

2. **Boolean-värden**:
   - INTEGER (0/1) istället för BOOLEAN

3. **Standard deviation**:
   - Manuell beräkning: `SQRT(AVG(x*x) - AVG(x)*AVG(x))`

4. **Window functions**:
   - NTILE() fungerar i SQLite 3.25+
   - Alternativ: Manuell percentil-beräkning

5. **Export**:
   - `.mode csv` i SQLite CLI
   - Python script för mer kontroll

---

## SQLite Performance Tips

### Indexering för Snabbare Queries

```sql
-- Lägg till index på kolumner som används i WHERE, JOIN, GROUP BY
CREATE INDEX idx_orders_customerid_date ON Orders(CustomerID, OrderDate);
CREATE INDEX idx_orderdetails_productid ON OrderDetails(ProductID);
CREATE INDEX idx_products_category ON Products(Category);

-- Kontrollera query plan
EXPLAIN QUERY PLAN
SELECT
    c.CustomerID,
    COUNT(o.OrderID) as TotalOrders
FROM Customers c
JOIN Orders o ON c.CustomerID = o.CustomerID
GROUP BY c.CustomerID;
```

### Analyze och Optimize

```sql
-- Uppdatera statistik för query optimizer
ANALYZE;

-- Komprimera och optimera databas
VACUUM;

-- Kontrollera databasstorlek
SELECT page_count * page_size as DatabaseSize FROM pragma_page_count(), pragma_page_size();
```

---

## Inlämning

### Vad ska lämnas in:

1. **SQL-script**: `feature_engineering_sqlite.sql` med ALLA queries
2. **CSV Export**: `ml_training_data.csv` (eller screenshot)
3. **SQLite databas**: `wildshop.db` (om <10MB)
4. **Dokumentation**: Markdown eller PDF med:
   - Feature list med SQLite-anpassningar
   - Data quality report
   - Reflektion över SQLite-specifika utmaningar

### Deadline:

**Fredag 16:00** - Ladda upp på Canvas

### Bedömning:

- ✅ **Godkänd**:
  - Minst 15 features
  - Korrekt target-variabel
  - Fungerande export
  - SQLite-syntax korrekt

- ✅ **Väl Godkänd**:
  - 20+ features
  - RFM-analys korrekt (NTILE eller manual)
  - Product associations
  - Utförlig dokumentation med SQLite-jämförelser

---

## Resurser

- [SQLite Window Functions](https://www.sqlite.org/windowfunctions.html)
- [SQLite Date and Time Functions](https://www.sqlite.org/lang_datefunc.html)
- [SQLite Math Functions](https://www.sqlite.org/lang_mathfunc.html)
- [SQLite Performance Tuning](https://www.sqlite.org/queryplanner.html)

---

## Tips för SQLite Feature Engineering

1. **Kontrollera SQLite-version**: `SELECT sqlite_version();`
2. **Använd julianday()**: För alla datumberäkningar
3. **CAST för precision**: `CAST(x AS REAL)` för division
4. **COALESCE för NULL**: Hantera NULL-värden explicit
5. **Transactions**: `BEGIN; ... COMMIT;` för stora operationer
6. **PRAGMA optimize**: Kör efter stora ändringar

**Lycka till med er feature engineering i SQLite! 🚀**

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
