# SQL Repetition — Spelbutiken

🟢 Grundläggande · 🟡 Mellannivå · 🔴 Utmaning

Vi jobbar med en spelbutik under hela passet. Samma databas, samma tabeller — vi bygger vidare steg för steg.

---

## Del 1 — Skapa databasen 🟢

**1.1** 🟢 Skapa databasen och välj den (MySQL):

```sql
CREATE DATABASE spelbutik;
USE spelbutik;
```

> SQLite: hoppa över det här steget — tabellerna skapas direkt i filen.

---

**1.2** 🟢 Skapa tabellen `Games`:

```sql
-- MySQL
CREATE TABLE Games (
    GameId INT PRIMARY KEY AUTO_INCREMENT,
    Title  VARCHAR(100) NOT NULL,
    Genre  VARCHAR(50),
    Price  DECIMAL(6,2) NOT NULL,
    Stock  INT DEFAULT 0
);

-- SQLite
CREATE TABLE Games (
    GameId INTEGER PRIMARY KEY,
    Title  TEXT NOT NULL,
    Genre  TEXT,
    Price  REAL NOT NULL,
    Stock  INTEGER DEFAULT 0
);
```

---

**1.3** 🟢 Skapa tabellen `Orders` med en `FOREIGN KEY`:

```sql
-- MySQL
CREATE TABLE Orders (
    OrderId      INT PRIMARY KEY AUTO_INCREMENT,
    GameId       INT NOT NULL,
    CustomerName VARCHAR(100) NOT NULL,
    Quantity     INT NOT NULL DEFAULT 1,
    OrderDate    DATE,
    FOREIGN KEY (GameId) REFERENCES Games(GameId)
);

-- SQLite
CREATE TABLE Orders (
    OrderId      INTEGER PRIMARY KEY,
    GameId       INTEGER NOT NULL,
    CustomerName TEXT NOT NULL,
    Quantity     INTEGER NOT NULL DEFAULT 1,
    OrderDate    TEXT,
    FOREIGN KEY (GameId) REFERENCES Games(GameId)
);
```

---

**1.4** 🟢 Lägg in fem spel:

```sql
INSERT INTO Games (Title, Genre, Price, Stock) VALUES
    ('The Witcher 3',      'RPG',     249.00, 12),
    ('FIFA 25',            'Sport',   549.00,  5),
    ('Minecraft',          'Sandbox', 199.00, 30),
    ('Elden Ring',         'RPG',     449.00,  8),
    ('Stardew Valley',     'Indie',   129.00, 25);
```

---

## Del 2 — Läsa data 🟢

**2.1** 🟢 Hämta alla spel:

```sql
SELECT * FROM Games;
```

**2.2** 🟢 Hämta bara titel och pris:

```sql
SELECT Title, Price FROM Games;
```

**2.3** 🟢 Hämta alla RPG-spel:

```sql
SELECT * FROM Games WHERE Genre = 'RPG';
```

**2.4** 🟢 Hämta spel som kostar mellan 200 och 500 kr:

```sql
SELECT * FROM Games WHERE Price BETWEEN 200 AND 500;
-- alternativt:
SELECT * FROM Games WHERE Price >= 200 AND Price <= 500;
```

**2.5** 🟢 Sortera alla spel från dyrast till billigast:

```sql
SELECT * FROM Games ORDER BY Price DESC;
```

**2.6** 🟢 Hämta de tre billigaste spelen:

```sql
SELECT * FROM Games ORDER BY Price ASC LIMIT 3;
```

**2.7** 🟢 Sök efter spel vars namn innehåller bokstaven "e":

```sql
SELECT * FROM Games WHERE Title LIKE '%e%';
```

---

## Del 3 — Uppdatera och radera data 🟢🟡

**3.1** 🟢 Höj priset på Minecraft med 50 kr:

```sql
UPDATE Games SET Price = Price + 50 WHERE Title = 'Minecraft';
```

**3.2** 🟢 Sätt lagersaldot på FIFA 25 och Elden Ring till 0 (de är slutsålda):

```sql
UPDATE Games SET Stock = 0 WHERE Title IN ('FIFA 25', 'Elden Ring');
```

**3.3** 🟢 Lägg in en order — Anna köper 2 exemplar av Minecraft (GameId 3):

```sql
INSERT INTO Orders (GameId, CustomerName, Quantity, OrderDate)
VALUES (3, 'Anna Karlsson', 2, '2026-10-17');
```

Lägg in ytterligare två orders så vi har data till senare övningar:

```sql
INSERT INTO Orders (GameId, CustomerName, Quantity, OrderDate)
VALUES (1, 'Björn Svensson', 1, '2026-10-17');

INSERT INTO Orders (GameId, CustomerName, Quantity, OrderDate)
VALUES (3, 'Anna Karlsson', 1, '2026-10-18');
```

**3.4** 🟡 Försök radera Minecraft (GameId 3) — vad händer?

```sql
DELETE FROM Games WHERE GameId = 3;
```

> Det ska misslyckas om FOREIGN KEY är aktiv — det finns orders som refererar till det spelet.
> SQLite: aktivera FK-kontroll med `PRAGMA foreign_keys = ON;` annars tillåts raderingen ändå.
>
> Diskutera: hur hanterar man det i ett riktigt system? (Cascade delete, soft delete, arkivera?)

**3.5** 🟢 Radera ett spel som *inte* har några orders. Lägg först in ett nytt:

```sql
INSERT INTO Games (Title, Genre, Price, Stock) VALUES ('Testspel', 'Övrigt', 9.00, 1);
DELETE FROM Games WHERE Title = 'Testspel';
```

---

## Del 4 — Ändra tabellen 🟡

**4.1** 🟡 Lägg till en kolumn `Rating` i Games:

```sql
-- MySQL och SQLite
ALTER TABLE Games ADD COLUMN Rating DECIMAL(3,1);
```

Sätt ett betyg på ett par spel:

```sql
UPDATE Games SET Rating = 9.5 WHERE Title = 'The Witcher 3';
UPDATE Games SET Rating = 8.2 WHERE Title = 'Elden Ring';
UPDATE Games SET Rating = 9.8 WHERE Title = 'Stardew Valley';
```

**4.2** 🟡 Lägg till en kolumn med ett DEFAULT-värde:

```sql
-- MySQL och SQLite
ALTER TABLE Games ADD COLUMN IsActive BOOLEAN DEFAULT 1;
```

**4.3** 🟡 Byt namn på en kolumn (MySQL):

```sql
-- MySQL 8+
ALTER TABLE Games RENAME COLUMN Stock TO StockCount;

-- SQLite: stöds inte direkt i äldre versioner.
-- Alternativ: skapa ny tabell med rätt namn och kopiera data.
```

> SQLite är konservativ med ALTER TABLE — den tillåter ADD COLUMN och RENAME TABLE, men inte DROP COLUMN eller RENAME COLUMN i äldre versioner.

**4.4** 🟡 Ta bort kolumnen `IsActive` (MySQL):

```sql
-- MySQL 8.0+
ALTER TABLE Games DROP COLUMN IsActive;

-- SQLite: stöds inte direkt — se kommentar ovan.
```

---

## Del 5 — Views 🟡🔴

> En VIEW är en sparad SELECT. Den lagrar inte data — den kör frågan varje gång du väljer från den.

**5.1** 🟡 Skapa en vy med bara spel som finns i lager:

```sql
CREATE VIEW GamesInStock AS
SELECT GameId, Title, Genre, Price, Stock
FROM Games
WHERE Stock > 0;

SELECT * FROM GamesInStock;
```

**5.2** 🟡 Skapa en vy som kombinerar `Games` och `Orders` med JOIN:

```sql
CREATE VIEW OrderDetails AS
SELECT
    o.OrderId,
    o.CustomerName,
    g.Title      AS GameTitle,
    o.Quantity,
    o.OrderDate
FROM Orders o
INNER JOIN Games g ON o.GameId = g.GameId;

SELECT * FROM OrderDetails;
```

**5.3** 🟡 Hämta bara Annas orders från vyn:

```sql
SELECT * FROM OrderDetails WHERE CustomerName = 'Anna Karlsson';
```

**5.4** 🔴 Skapa en vy som summerar hur många exemplar som sålts per spel:

```sql
CREATE VIEW SalesSummary AS
SELECT
    g.Title,
    SUM(o.Quantity) AS TotalSold
FROM Games g
LEFT JOIN Orders o ON g.GameId = o.GameId
GROUP BY g.GameId, g.Title;

SELECT * FROM SalesSummary ORDER BY TotalSold DESC;
```

**5.5** 🟡 Ta bort en vy:

```sql
DROP VIEW IF EXISTS GamesInStock;
```

---

## Del 6 — Blandade utmaningar 🔴

**6.1** 🔴 Hitta det dyraste spelet i varje genre:

```sql
SELECT Genre, Title, Price
FROM Games
WHERE (Genre, Price) IN (
    SELECT Genre, MAX(Price)
    FROM Games
    GROUP BY Genre
);
```

> SQLite: syntaxen `(col1, col2) IN (...)` stöds inte alltid. Alternativ med JOIN:
> ```sql
> SELECT g.Genre, g.Title, g.Price
> FROM Games g
> INNER JOIN (
>     SELECT Genre, MAX(Price) AS MaxPrice
>     FROM Games
>     GROUP BY Genre
> ) m ON g.Genre = m.Genre AND g.Price = m.MaxPrice;
> ```

**6.2** 🔴 Hitta kunder som har lagt mer än en order:

```sql
SELECT CustomerName, COUNT(*) AS AntalOrders
FROM Orders
GROUP BY CustomerName
HAVING COUNT(*) > 1;
```

**6.3** 🔴 Räkna ut totalvärdet av varje order (antal × pris):

```sql
SELECT
    o.OrderId,
    o.CustomerName,
    g.Title,
    o.Quantity,
    g.Price,
    o.Quantity * g.Price AS TotalPris
FROM Orders o
INNER JOIN Games g ON o.GameId = g.GameId
ORDER BY TotalPris DESC;
```

---

## Tips

> **Aldrig UPDATE eller DELETE utan WHERE** — om du glömmer WHERE påverkar du alla rader i tabellen. Inget ångra-knapp finns.

> **SQLite vs MySQL:**
> - SQLite: `INTEGER PRIMARY KEY` (inte `INT`), inget `AUTO_INCREMENT`
> - SQLite: `PRAGMA foreign_keys = ON;` måste köras per session annars ignoreras FK-begränsningar
> - SQLite: `ALTER TABLE` är begränsad — `ADD COLUMN` fungerar, `DROP COLUMN` och `RENAME COLUMN` fungerar bara i nyare versioner

> **Använd IF EXISTS på DROP** för att undvika fel om objektet inte finns:
> ```sql
> DROP TABLE IF EXISTS Games;
> DROP VIEW IF EXISTS GamesInStock;
> ```

> **15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga grannen, sedan AI, sedan läraren. I den ordningen.
