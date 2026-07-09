# DML — Programmeringstermer

## Data Manipulation Language (DML)
Den del av SQL som används för att hantera data i tabeller: SELECT, INSERT, UPDATE, DELETE.

## SELECT
Hämtar data från en eller flera tabeller. Det vanligaste SQL-kommandot.

```sql
SELECT Name, Price FROM Product WHERE Price > 100 ORDER BY Price DESC;
```

## INSERT
Lägger till nya rader i en tabell.

```sql
INSERT INTO Product (Name, Price) VALUES ('Kaffekopp', 129.00);
```

## UPDATE
Ändrar befintliga rader i en tabell. EXTREMT VIKTIGT med WHERE — annars uppdateras ALLT.

```sql
UPDATE Product SET Price = 149.00 WHERE ProductID = 1;
```

## DELETE
Tar bort rader från en tabell. EXTREMT VIKTIGT med WHERE — annars försvinner ALLT.

```sql
DELETE FROM Product WHERE ProductID = 1;
```

## WHERE
Filter som begränsar vilka rader som påverkas. Används med SELECT, UPDATE, DELETE.

## JOIN
Kombinerar rader från två eller fler tabeller baserat på en relaterad kolumn.

- **INNER JOIN** — bara matchande rader
- **LEFT JOIN** — alla rader från vänstra tabellen, NULL från högra om ingen match
- **RIGHT JOIN** — tvärtom
- **FULL JOIN** — alla rader från båda

## GROUP BY
Grupperar rader efter en kolumn och används med aggregeringsfunktioner (COUNT, SUM, AVG, MAX, MIN).

```sql
SELECT CategoryID, COUNT(*) FROM Product GROUP BY CategoryID;
```

## HAVING
Filter för grupperade data (som WHERE men efter GROUP BY).

## ORDER BY
Sorterar resultatet — ASC (stigande), DESC (fallande).

## LIMIT / OFFSET
Begränsar antalet returnerade rader. OFFSET används för paginering.

```sql
SELECT * FROM Product LIMIT 10 OFFSET 20;  -- Sida 3
```

## Aggregeringsfunktion
Funktion som beräknar ett värde från flera rader: COUNT, SUM, AVG, MAX, MIN.

## Subquery
En SELECT inuti en annan SELECT. Används för komplexa frågor.

```sql
SELECT * FROM Product WHERE Price > (SELECT AVG(Price) FROM Product);
```

## LIKE
Mönstersökning med wildcards: `%` (valfria tecken), `_` (exakt ett tecken).

```sql
SELECT * FROM Product WHERE Name LIKE '%kaffe%';
```
