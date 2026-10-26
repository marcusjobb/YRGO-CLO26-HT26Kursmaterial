---
marp: true
theme: nion-dark
paginate: true
---

# Repetition — Databaser

**Kurs 2 · v.44 · Inför tenta**

Marcus Ackre Medina · YRGO · CLO26

---

## Tre block

1. **Databas & Tabell** — skapa, radera, strukturera
2. **SQL CRUD** — hämta, lägga till, ändra, ta bort
3. **C# mot databasen** — objekt, metoder, säkerhet

---

<!-- ===== BLOCK 1 ===== -->

## Vad gör den här koden?

```sql
CREATE DATABASE shopdb;

USE shopdb;

CREATE TABLE users (
    id   INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    city VARCHAR(100)
);
```

---

## Svar

```sql
CREATE DATABASE shopdb;   -- skapar en ny databas
USE shopdb;               -- väljer den som aktiv
CREATE TABLE users (...)  -- skapar tabellen users i shopdb
```

`PRIMARY KEY` — unikt ID per rad  
`AUTO_INCREMENT` — databasen räknar upp automatiskt  
`NOT NULL` — fältet måste ha ett värde

---

## Vad gör den här koden?

```sql
DROP TABLE users;

DROP DATABASE shopdb;
```

---

## Svar

```sql
DROP TABLE users;      -- raderar tabellen users (och all data i den)
DROP DATABASE shopdb;  -- raderar hela databasen
```

> ⚠️ Ingen ångra-knapp. `DROP` är permanent.

---

<!-- ===== BLOCK 2 ===== -->

## Vad gör den här koden?

```sql
SELECT name, city
FROM users
WHERE city = 'Göteborg'
ORDER BY name;
```

---

## Svar

Hämtar kolumnerna `name` och `city` från `users`  
— men bara raderna där `city` är `'Göteborg'`  
— sorterade på `name` i stigande ordning.

`SELECT *` hade hämtat alla kolumner.

---

## Vad gör den här koden?

```sql
INSERT INTO users (name, city)
VALUES ('Emilia', 'Göteborg');

UPDATE users
SET city = 'Malmö'
WHERE id = 3;
```

---

## Svar

```sql
INSERT INTO users (name, city)
VALUES ('Emilia', 'Göteborg');
-- Lägger till en ny rad
```

```sql
UPDATE users
SET city = 'Malmö'
WHERE id = 3;
-- Uppdaterar bara raden med id = 3
```

> ⚠️ `UPDATE` utan `WHERE` — uppdaterar **alla** rader.

---

## Vad gör den här koden?

```sql
DELETE FROM users
WHERE id = 5;
```

Och vad händer om du skriver:

```sql
DELETE FROM users;
```

---

## Svar

```sql
DELETE FROM users WHERE id = 5;
-- Tar bort raden med id = 5
```

```sql
DELETE FROM users;
-- Tar bort ALLA rader i tabellen
-- Tabellen finns kvar, men är tom
```

> Alltid `WHERE` vid `DELETE` och `UPDATE`.

---

## Vad gör den här koden?

```sql
BEGIN;

UPDATE accounts SET balance = balance - 500 WHERE id = 1;
UPDATE accounts SET balance = balance + 500 WHERE id = 2;

COMMIT;
```

Och vad händer om något går fel?

---

## Svar

**Transaktion** — antingen sparas *båda* eller *ingen*.

`BEGIN` startar transaktionen.  
`COMMIT` sparar allt — men du kör det manuellt.  
`ROLLBACK` ångrar allt sedan `BEGIN` — också manuellt.

I ren SQL väljer **du** vilket kommando du kör.  
Det finns ingen automatisk felhantering — det löses i C# med `try/catch`.

---

## Vad gör den här koden i MySQL?

```sql
DELIMITER //
CREATE PROCEDURE TransferMoney()
BEGIN
    DECLARE EXIT HANDLER FOR SQLEXCEPTION
        ROLLBACK;

    START TRANSACTION;
    UPDATE accounts SET balance = balance - 500 WHERE id = 1;
    UPDATE accounts SET balance = balance + 500 WHERE id = 2;
    COMMIT;
END //
DELIMITER ;
```

---

## Svar

```sql
DELIMITER //        -- byt ; tillfälligt så MySQL inte avbryter i förtid
CREATE PROCEDURE    -- sparar koden som en funktion i databasen
BEGIN ... END       -- procedurkroppen
DECLARE EXIT HANDLER FOR SQLEXCEPTION
    ROLLBACK;       -- SQL-fel uppstår → rulla tillbaka automatiskt
START TRANSACTION;  -- starta transaktion
COMMIT;             -- spara om allt gick bra
DELIMITER ;         -- återställ ; som avslutare
```

MySQL-specifikt — SQLite har inga stored procedures.  
I C# löser `try/catch` samma sak utan procedure.

---

<!-- ===== BLOCK 3 ===== -->

## Vad gör de här objekten?

```csharp
var conn = new MySqlConnection(connectionString);
conn.Open();

var cmd = new MySqlCommand("SELECT * FROM users", conn);
```

---

## Svar

```csharp
MySqlConnection  // öppnar anslutningen till databasen
MySqlCommand     // håller SQL-frågan och skickar den
```

`conn.Open()` — måste anropas innan du kan köra frågor  
`connectionString` — innehåller server, databas, användare, lösenord

---

## Vad är skillnaden?

```csharp
cmd.ExecuteNonQuery();

cmd.ExecuteScalar();

var reader = cmd.ExecuteReader();
```

---

## Svar

| Metod | Returnerar | Används till |
|-------|-----------|-------------|
| `ExecuteNonQuery()` | antal påverkade rader | INSERT, UPDATE, DELETE |
| `ExecuteScalar()` | ett enda värde | COUNT(), MAX(), SUM() |
| `ExecuteReader()` | `MySqlDataReader` | SELECT — läser rad för rad |

---

## Vad är problemet?

```csharp
string input = Console.ReadLine();

string sql = "SELECT * FROM users WHERE name = '" + input + "'";
var cmd = new MySqlCommand(sql, conn);
```

---

## Svar

**SQL injection** — en angripare kan manipulera frågan.

Skriv in: `' OR '1'='1`  
→ SQL körs som: `WHERE name = '' OR '1'='1'`  
→ returnerar **alla** rader

**Fix:**

```csharp
string sql = "SELECT * FROM users WHERE name = @name";
var cmd = new MySqlCommand(sql, conn);
cmd.Parameters.AddWithValue("@name", input);
```

`MySqlParameter` behandlar input som ett värde — aldrig som SQL.

---

## Sammanfattning

| Område | Kom ihåg |
|--------|---------|
| `CREATE TABLE` | Definiera kolumner + typer + PRIMARY KEY |
| `DROP` | Permanent — ingen ångra |
| `UPDATE` / `DELETE` | Alltid `WHERE` |
| Transaktion | `BEGIN` → `COMMIT` eller `ROLLBACK` |
| `ExecuteNonQuery` | INSERT / UPDATE / DELETE |
| `ExecuteScalar` | Ett värde — COUNT, MAX |
| `MySqlDataReader` | SELECT — läser rad för rad |
| SQL injection | Alltid `MySqlParameter` — aldrig strängkonkatenering |

---

## Lycka till på onsdag 🎓

Tentan: **onsdag 28 okt 09:00–12:00**

Inga hjälpmedel.
