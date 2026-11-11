# Kurs 2 Databaser — Tentatermer och repetition

Alla centrala begrepp från kurs 02. Gå igenom dessa och se om du kan förklara varje term med egna ord och ett konkret exempel.

---

## SQL-grunder

## SELECT
Hämta data ur en tabell: `SELECT kolumn FROM tabell WHERE villkor ORDER BY kolumn LIMIT n`

## WHERE
Filtrera rader baserat på villkor. Körs INNAN GROUP BY. Kan inte filtrera på aggregat (använd HAVING).

## ORDER BY
Sortera resultatet. `ASC` = stigande (standard), `DESC` = fallande. `ORDER BY Price DESC LIMIT 5` = de 5 dyraste.

## LIMIT
Begränsa antalet returnerade rader: `LIMIT 10` = max 10 rader.

## LIKE
Mönstermatchning: `%` = valfritt antal tecken, `_` = exakt ett tecken. `WHERE Name LIKE 'M%'` = börjar med M.

## IN
Kontrollera om värde finns i lista: `WHERE Category IN ('Mat', 'Dryck')` — kortform för flera OR-villkor.

## BETWEEN
Intervallkontroll (inklusive): `WHERE Price BETWEEN 50 AND 200`

## IS NULL / IS NOT NULL
Kontrollera om ett värde saknas: `WHERE Phone IS NULL`. Kan inte använda `= NULL` — det fungerar inte i SQL.

---

## JOIN

## INNER JOIN
Returnerar bara rader som har matchning i BÅDA tabeller. `FROM Orders INNER JOIN Customers ON Orders.CustomerId = Customers.Id`

## LEFT JOIN
Returnerar alla rader från vänster tabell + matchande rader från höger. Rader utan match får NULL på höger sidas kolumner.

## RIGHT JOIN
Alla från höger + matchande från vänster. Används sällan — byt tabellordning och använd LEFT JOIN istället.

## ON
Anger kopplingen vid JOIN: `ON tabell1.ForeignKey = tabell2.PrimaryKey`

## Alias
Kortnamn för tabell eller kolumn: `FROM Customers c LEFT JOIN Orders o ON c.Id = o.CustomerId`

---

## Aggregat

## COUNT
Räkna rader. `COUNT(*)` = alla rader, `COUNT(kolumn)` = rader där kolumn inte är NULL.

## SUM
Summera numeriska värden: `SUM(Price)` = totalpris.

## AVG
Beräkna medelvärde: `AVG(Price)` = genomsnittspris.

## MIN / MAX
Minsta respektive största värde i en kolumn.

## GROUP BY
Gruppera rader med lika värden för att sedan aggregera: `GROUP BY Category` ger en rad per kategori.

## HAVING
Filtrera grupper efter GROUP BY. Kan använda aggregatfunktioner. `HAVING COUNT(*) > 5` = bara grupper med fler än 5 rader.

## WHERE vs HAVING
WHERE filtrerar enskilda rader INNAN gruppering. HAVING filtrerar grupper EFTER gruppering.

---

## DDL — Data Definition Language

## CREATE TABLE
Skapa en ny tabell med kolumner och constraints.

## ALTER TABLE
Ändra befintlig tabell: lägg till kolumn (`ADD COLUMN`), ändra kolumn (`MODIFY COLUMN`), ta bort kolumn (`DROP COLUMN`).

## DROP TABLE
Ta bort en tabell permanent (all data raderas).

## PRIMARY KEY
Unik identifierare för varje rad i tabellen. Kan inte vara NULL.

## FOREIGN KEY
Referens till primärnyckeln i en annan tabell. Skapar en relation och tvingar referentiell integritet.

## AUTO_INCREMENT
Automatiskt ökande heltal — används ofta för primärnycklar.

## NOT NULL
Constraint som hindrar NULL-värden i en kolumn.

## DEFAULT
Standardvärde som används om inget värde anges vid INSERT.

## UNIQUE
Constraint som kräver att alla värden i kolumnen är unika.

## INDEX
Datastruktur som snabbar upp WHERE-sökningar på bekostnad av extra lagringsutrymme och något långsammare INSERTs.

## ON DELETE CASCADE / SET NULL / RESTRICT
Beteende när en föräldrarad raderas. CASCADE raderar barnraderna, SET NULL sätter NULL, RESTRICT hindrar radering.

---

## DML — Data Manipulation Language

## INSERT INTO
Lägg till en eller flera rader: `INSERT INTO tabell (kol1, kol2) VALUES (val1, val2)`

## UPDATE
Ändra befintliga rader. ALLTID med WHERE — annars uppdateras alla rader.

## DELETE FROM
Ta bort rader. ALLTID med WHERE — annars raderas alla rader. `DELETE FROM tabell` utan WHERE = tom tabell.

---

## Normalisering

## 1NF
Atomiska värden — varje cell innehåller ett värde (ingen lista). Inga upprepande kolumner.

## 2NF
Alla icke-nyckelkolumner är beroende av HELA primärnyckeln. Gäller vid sammansatt nyckel — inga partiella beroenden.

## 3NF
Inga transitiva beroenden. Icke-nyckelkolumner ska bero på primärnyckeln, inte på andra icke-nyckelkolumner.

## Redundans
Samma information lagrad på flera ställen. Problem: uppdatering på ett ställe riskerar inkonsistens.

## Partiellt beroende
En kolumn beror på del av en sammansatt nyckel, inte hela. Elimineras i 2NF.

## Transitivt beroende
A → B → C, där C beror på B snarare än direkt på primärnyckeln A. Elimineras i 3NF.

---

## Transaktioner

## BEGIN / START TRANSACTION
Startar en transaktion. Alla efterföljande operationer är en enhet.

## COMMIT
Bekräfta och spara alla ändringar i transaktionen permanent.

## ROLLBACK
Ångra alla ändringar sedan BEGIN. Databasen återgår till tillståndet före transaktionen.

## ACID
Fyra egenskaper som garanterar databasens integritet:
- **Atomicity** — allt eller inget
- **Consistency** — databasen förblir konsekvent
- **Isolation** — transaktioner stör inte varandra
- **Durability** — committad data överlever krasch

## Deadlock
Två transaktioner väntar på varandra — ingendera kan fortsätta. Databassystemet löser det genom att rulla tillbaka en av dem.

---

## Säkerhet

## SQL Injection
Attack där skadlig SQL-kod injiceras via användarinput. Exempelinput: `Robert'); DROP TABLE Students;--`. Uppstår när input konkateneras direkt i SQL-strängar.

## Parameteriserade frågor
Skydd mot SQL injection. Parametern `@name` ersätts säkert — behandlas som data, aldrig som kod. `cmd.Parameters.AddWithValue("@name", input)`

## EF Core och injection
EF Core LINQ-frågor är säkra som standard — EF genererar parametriserade frågor. Farligt: `FromSqlRaw($"... {input}")` — använd `FromSqlInterpolated` eller LINQ.

## Minsta behörighet
Ge databasanvändare bara de rättigheter de faktiskt behöver. App-användare: SELECT, INSERT, UPDATE, DELETE. Aldrig root i produktion.

## GRANT / REVOKE
SQL-kommandon för att ge och ta bort rättigheter: `GRANT SELECT ON databas.* TO 'user'@'host'`

## DCL
Data Control Language — GRANT och REVOKE.

---

## Backup

## Full backup
Komplett kopia av hela databasen. Enkel restore men tar mest tid och plats.

## Differentiell backup
Allt sedan senaste full backup. Snabbare att skapa, men restore kräver senaste full + denna.

## Inkrementell backup
Allt sedan senaste backup (oavsett typ). Minst data — men restore kräver full + alla inkrementella i ordning.

## Point-in-Time Recovery
Återställning till en exakt tidpunkt med hjälp av transaktionsloggar.

## 3-2-1-regeln
3 kopior av data · 2 olika lagringsmedia · 1 kopia offsite.

## mysqldump
MySQL-verktyg: `mysqldump -u root -p databas > backup.sql`. Restore: `mysql -u root -p databas < backup.sql`

## pg_dump
PostgreSQL: `pg_dump -U postgres databas > backup.sql`. Anpassat format (snabbare): `-Fc`. Restore: `pg_restore`.

## SQLite-backup
Kopiera `.db`-filen: `cp database.db backup.db`. Säkert vid aktiv databas: `sqlite3 database.db ".backup 'backup.db'"`

## BACKUP DATABASE
SQL Server: `BACKUP DATABASE HeroesDb TO DISK = 'backup.bak' WITH COMPRESSION`. Restore: `RESTORE DATABASE`.

---

## ER-diagram

## Entitet
En sak som representeras i databasen — en tabell: Kund, Produkt, Order.

## Attribut
Egenskap hos en entitet — en kolumn: Namn, Pris, Datum.

## Relation
Koppling mellan entiteter: 1:1 (en person har ett pass), 1:N (en kund har många ordrar), N:M (en order har många produkter och en produkt finns i många ordrar).

## N:M-relation
Kräver en kopplingstabell (junction table/bryggtabell): `OrderRows(OrderId, ProductId, Quantity)`.

---

## MongoDB (NoSQL)

## Dokument
Enhet i MongoDB — motsvarar en rad i SQL. Lagras som JSON/BSON.

## Collection
Samling av dokument — motsvarar en tabell i SQL.

## find()
Hämta dokument: `db.heroes.find({ name: "Iron Man" })`

## insertOne() / insertMany()
Lägg till dokument: `db.heroes.insertOne({ name: "Thor", power: "Mjölner" })`

## updateOne() / updateMany()
Uppdatera dokument: `db.heroes.updateOne({ name: "Thor" }, { $set: { power: "Stormbreaker" } })`

## deleteOne() / deleteMany()
Ta bort dokument: `db.heroes.deleteOne({ name: "Thanos" })`

## Schema-less
MongoDB kräver inget fast schema — dokument i samma collection kan ha olika fält. Flexibelt men riskerar inkonsistens.
