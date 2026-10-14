---
marp: true
theme: nion_dark
paginate: true
title: SQL Repetition — från CREATE till VIEW
description: Genomgång av SQL från grunden: databaser, tabeller, CRUD och views
---

<!-- _class: lead -->
<!-- _paginate: false -->

# SQL Repetition

Från CREATE till VIEW — spelbutiken som databas

---

## Det vi går igenom idag

1. **Skapa databas** — CREATE DATABASE, USE
2. **Skapa tabeller** — CREATE TABLE, datatyper, nycklar
3. **CRUD Data** — INSERT, SELECT, UPDATE, DELETE
4. **CRUD Tabeller** — ALTER TABLE, DROP TABLE
5. **Views** — CREATE VIEW, SELECT, DROP VIEW

> Vi kör mot **SQLite** (och noterar var MySQL skiljer sig). Samma SQL — nästan.

---

## Skapa databas

**MySQL:**

```sql
CREATE DATABASE spelbutik;
USE spelbutik;
```

**SQLite:**

```bash
# Databasen skapas automatiskt när du öppnar filen
sqlite3 spelbutik.db
```

> SQLite: ingen `CREATE DATABASE` — filen är databasen. Öppnar du `spelbutik.db` är du inne.

📖 [sqlite_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sqlite_intro.md) · [intro_databaser.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/intro_databaser.md)

---

## Skapa tabell — Games

```sql
CREATE TABLE Games (
    GameId    INTEGER PRIMARY KEY,
    Title     VARCHAR(100) NOT NULL,
    Genre     VARCHAR(50),
    Price     DECIMAL(6,2) NOT NULL,
    Stock     INTEGER      NOT NULL DEFAULT 0
);
```

Varje rad i `Games` är ett spel. `GameId` identifierar det unikt.

📖 [sqlite_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sqlite_intro.md)

---

## Datatyper — snabbreferens

| Typ | Vad det lagrar | Exempel |
|---|---|---|
| `INTEGER` / `INT` | Heltal | 42, -7, 0 |
| `VARCHAR(n)` | Text upp till n tecken | "The Witcher 3" |
| `TEXT` | Obegränsad text | Beskrivningar |
| `DECIMAL(p,s)` | Decimaltal, p siffror varav s decimaler | 299.99 |
| `DATE` | Datum | 2026-10-14 |
| `BOOLEAN` | Sant/falskt | TRUE / FALSE |

> SQLite lagrar allt som text, heltal eller float internt — men förstår dessa typnamn ändå. MySQL är striktare.

---

## PRIMARY KEY, NOT NULL och DEFAULT

```sql
CREATE TABLE Games (
    GameId  INTEGER PRIMARY KEY,   -- unikt, aldrig NULL, identifierar raden
    Title   VARCHAR(100) NOT NULL, -- måste ha ett värde
    Stock   INTEGER NOT NULL DEFAULT 0  -- måste ha värde, men 0 om inget anges
);
```

- `PRIMARY KEY` — varje rad får ett unikt id
- `NOT NULL` — fältet får inte vara tomt
- `DEFAULT` — används om inget värde anges vid INSERT

> MySQL: lägg till `AUTO_INCREMENT` på GameId så sköter databasen numreringen.
> SQLite: `INTEGER PRIMARY KEY` är automatiskt auto-increment.

---

## FOREIGN KEY — Orders kopplas till Games

```sql
CREATE TABLE Orders (
    OrderId      INTEGER PRIMARY KEY,
    GameId       INTEGER NOT NULL,
    CustomerName VARCHAR(100) NOT NULL,
    Quantity     INTEGER NOT NULL DEFAULT 1,
    OrderDate    DATE NOT NULL,

    FOREIGN KEY (GameId) REFERENCES Games(GameId)
);
```

`GameId` i `Orders` pekar på `GameId` i `Games`. Du kan inte lägga en order på ett spel som inte finns.

> SQLite: FOREIGN KEY-kontroll är avstängd som standard. Aktivera med `PRAGMA foreign_keys = ON;`

---

## INSERT — lägg in data

**En rad:**

```sql
INSERT INTO Games (Title, Genre, Price, Stock)
VALUES ('The Witcher 3', 'RPG', 299.99, 15);
```

**Flera rader på en gång:**

```sql
INSERT INTO Games (Title, Genre, Price, Stock)
VALUES
    ('Minecraft', 'Sandbox', 199.00, 42),
    ('Celeste', 'Platformer', 89.00, 8),
    ('Hollow Knight', 'Action', 69.00, 20);
```

> `GameId` anger vi inte — databasen sätter det automatiskt.

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## SELECT — hämta data

```sql
-- Allt
SELECT * FROM Games;

-- Specifika kolumner
SELECT Title, Price FROM Games;

-- Med filter
SELECT Title, Price FROM Games
WHERE Genre = 'RPG';

-- Flera villkor
SELECT Title, Price FROM Games
WHERE Genre = 'RPG' AND Price < 400;
```

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## SELECT — sortera, begränsa, söka

**Sortera:**

```sql
SELECT Title, Price FROM Games
ORDER BY Price DESC;   -- billigast: ASC
```

**Begränsa antal:**

```sql
SELECT Title FROM Games
ORDER BY Price DESC
LIMIT 3;
```

**Söka med mönster:**

```sql
SELECT * FROM Games
WHERE Title LIKE '%Knight%';  -- innehåller "Knight"
```

> `%` = noll eller fler tecken. `_` = exakt ett tecken.

---

## UPDATE — ändra data

```sql
UPDATE Games
SET Price = 249.00
WHERE Title = 'The Witcher 3';
```

Uppdatera flera fält samtidigt:

```sql
UPDATE Games
SET Price = 149.00, Stock = 5
WHERE GameId = 3;
```

> **⚠️ Aldrig UPDATE utan WHERE.**
> `UPDATE Games SET Price = 0` nollställer priset på **alla** spel i butiken.

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## DELETE — ta bort data

```sql
DELETE FROM Games
WHERE GameId = 4;
```

Ta bort alla spel ur ett genre:

```sql
DELETE FROM Games
WHERE Genre = 'Sandbox';
```

> **⚠️ Aldrig DELETE utan WHERE.**
> `DELETE FROM Games` raderar hela spelkatalogen. Ingen ångra-knapp.

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## ALTER TABLE — ändra tabellstruktur

**Lägg till en kolumn:**

```sql
-- MySQL
ALTER TABLE Games ADD COLUMN Rating DECIMAL(3,1);

-- SQLite
ALTER TABLE Games ADD COLUMN Rating DECIMAL(3,1);
```

**Döp om en kolumn:**

```sql
-- MySQL
ALTER TABLE Games RENAME COLUMN Rating TO AverageRating;
```

> **SQLite-begränsning:** `RENAME COLUMN` fungerar från SQLite 3.25+. Att ta bort en kolumn (`DROP COLUMN`) stöds från 3.35+. Äldre versioner kräver att du skapar en ny tabell och kopierar data.

---

## DROP TABLE — ta bort en tabell

```sql
DROP TABLE Orders;
```

Med säkerhetskontroll (fel inte om tabellen redan saknas):

```sql
DROP TABLE IF EXISTS Orders;
```

> DROP TABLE tar bort tabellen **och all data** i den — permanent. Tänk efter innan.
> Ta bort i rätt ordning: `Orders` (som refererar) innan `Games` (som refereras).

---

## VIEW — vad är det?

En **vy** är en sparad SELECT-fråga som beter sig som en tabell.

Varför använda views?
- Slipp skriva samma komplexa JOIN varje gång
- Visa bara de kolumner en användare behöver
- Ger ett "fönster" in i data utan att kopiera den

> Data lagras **inte** i vyn — bara frågan sparas. Kör du mot vyn kör den frågan på nytt.

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## CREATE VIEW

```sql
CREATE VIEW BestSellers AS
SELECT Title, Genre, Price, Stock
FROM Games
WHERE Stock > 10
ORDER BY Price DESC;
```

Använd den precis som en tabell:

```sql
SELECT * FROM BestSellers;

SELECT Title, Price FROM BestSellers
WHERE Genre = 'RPG';
```

---

## CREATE VIEW — med JOIN

En vy som kombinerar `Orders` och `Games`:

```sql
CREATE VIEW OrderDetails AS
SELECT
    o.OrderId,
    o.CustomerName,
    g.Title,
    g.Price,
    o.Quantity,
    (g.Price * o.Quantity) AS TotalPrice,
    o.OrderDate
FROM Orders o
INNER JOIN Games g ON o.GameId = g.GameId;
```

```sql
SELECT * FROM OrderDetails
WHERE CustomerName = 'Anna';
```

---

## DROP VIEW

```sql
DROP VIEW BestSellers;
```

Med säkerhetskontroll:

```sql
DROP VIEW IF EXISTS OrderDetails;
```

> En vy kan alltid återskapas — den innehåller ingen egen data.

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Nu kör ni

Skapa spelbutiksdatabasen från grunden.
Lägg in spel, lägg in ordrar, bygg en vy.

Penna och whiteboard innan IDE:t. Rita schemat först. 🎮

---

## Allt du behöver — i repot

**Notes — läs efter lektionen (eller under):**

| Ämne | Fil |
|------|-----|
| Vad är en databas? | [intro_databaser.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/intro_databaser.md) |
| SQL-frågor: SELECT, WHERE, ORDER BY | [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md) |
| SQLite — filbaserad databas | [sqlite_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sqlite_intro.md) |
| SQL från C# med ADO.NET | [ado_net_sqlite.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_net_sqlite.md) |
| Transaktioner och ACID | [transaktioner.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/transaktioner.md) |
| SQLite, MySQL, PostgreSQL — vilket väljer du? | [databastyper.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/databastyper.md) |

> Alla notes finns under `kurs-02-databaser/01_intro_databaser/notes/` i GitHub-repot.
