# Tentainfo — Kurs 2: Databaser och SQL

Tentan är skriftlig och täcker det vi gått igenom under kursens gång.
Nedan hittar du en lista på vad du ska kunna — med länkar till materialet om du känner dig osäker.

---

## SQL-grunder

Du ska kunna läsa, förstå och skriva grundläggande SQL-frågor.

**Hämta data**
- `SELECT`, `WHERE`, `ORDER BY`
- `JOIN` — INNER JOIN, LEFT JOIN, RIGHT JOIN
- Aggregatfunktioner: `COUNT`, `SUM`, `AVG`, `MIN`, `MAX`
- `GROUP BY`, `HAVING`

📖 [SQL-genomgång](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_genomgang.md)  
📖 [SQL från C# — rymdmanga](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_fran_csharp_rymdmanga.md)  
📖 [Aggregering](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/04_sql_avancerat/notes/sql_aggregation.md)

**Ändra data**
- `INSERT INTO`, `UPDATE ... SET ... WHERE`, `DELETE FROM ... WHERE`

**Skapa och ändra tabeller**
- `CREATE DATABASE`, `CREATE TABLE`
- `ALTER TABLE` (lägga till/ändra kolumner)
- `DROP TABLE`, `DROP DATABASE`

📖 [CRUD-operationer](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/02_sql_hardcore/notes/crud_operations.md)  
📖 [SQL — tabeller](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/02_sql_hardcore/notes/sql_tabeller.md)

---

## Normalisering

Du ska kunna förklara vad de tre normalformerna innebär och identifiera problem i ett dåligt designat schema.

- **1NF** — inga upprepade grupper, atomära värden
- **2NF** — inga partiella beroenden
- **3NF** — inga transitiva beroenden

📖 [Normalisering](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/03_normalisering_gdpr/notes/normalisering.md)  
📖 [Normalform — order och GDPR](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/03_normalisering_gdpr/notes/normalform_order_gdpr.md)

---

## Säkerhet — SQL Injection

Du ska kunna förklara vad SQL injection är, visa ett exempel på hur det kan utnyttjas, och förklara hur parametriserade frågor skyddar mot det.

📖 [SQL-säkerhet](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/07_sakerhet_och_backup/notes/sql_security.md)

---

## GDPR

Du ska kunna förklara vad GDPR är och varför det spelar roll när man arbetar med databaser och persondata.

- Vad är personuppgifter?
- Varför ska man inte spara mer än nödvändigt?
- Vad händer om man bryter mot GDPR?

📖 [GDPR i praktiken](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/03_normalisering_gdpr/notes/gdpr_praktik_scenarier.md)

---

## ADO.NET — identifiera objekten

Du behöver inte skriva ADO.NET-kod utantill. Däremot ska du kunna **läsa kod och förklara vad varje del gör**.

Dessa objekt ska du känna igen och kunna beskriva:

| Objekt | Exempel | Vad gör det? |
|--------|---------|--------------|
| Connection | `SqliteConnection` | Öppnar anslutningen till databasen |
| Command | `SqliteCommand` | Håller SQL-frågan och kör den mot databasen |
| Parameters | `command.Parameters` | Lägger in värden säkert (skyddar mot SQL injection) |
| `ExecuteNonQuery()` | — | Kör INSERT/UPDATE/DELETE — returnerar antal påverkade rader |
| `ExecuteReader()` | — | Kör SELECT — returnerar rader du kan loopa igenom |
| `DataTable` | — | Lagrar resultatet i minnet som en tabell |

📖 [ADO.NET med SQLite](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_net_sqlite.md)  
📖 [ADO snabbguide](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/ado_quicksheet.md)

---

## Vad är INTE med på denna tenta

- MongoDB och dokumentdatabaser — det kommer i nästa inlämning
- EF Core / ORM

---

## Tips inför tentan

- Öva på att skriva SQL-frågor för hand — utan autocomplete
- Gå igenom normaliserings-exemplen och försök identifiera vilket normalformsproblem varje exempel har
- Förstå skillnaden på `ExecuteNonQuery` och `ExecuteReader` — när använder man vilken?
