# 02 Sql Hardcore — Programmeringstermer

## SELECT
SQL-kommando för att hämta data: `SELECT * FROM Customer WHERE City = 'Göteborg';`

## INSERT
SQL-kommando för att lägga till data: `INSERT INTO Product (Name, Price) VALUES ('Kaffe', 29);`

## UPDATE
SQL-kommando för att ändra data: `UPDATE Product SET Price = 35 WHERE Id = 1;`

## DELETE
SQL-kommando för att ta bort data: `DELETE FROM Product WHERE Id = 1;`

## WHERE
Filter som begränsar vilka rader som påverkas: `WHERE Price > 100 AND Stock > 0`

## JOIN
Kombinera data från flera tabeller: INNER JOIN, LEFT JOIN, RIGHT JOIN.

## ORDER BY
Sortera resultat: `ORDER BY Price DESC, Name ASC`

## GROUP BY
Gruppera rader för aggregering: `SELECT City, COUNT(*) FROM Customer GROUP BY City`

## LIKE
Mönstersökning: `WHERE Name LIKE 'Anna%'` (% = valfria tecken)

## Aggregeringsfunktion
Funktion som beräknar på flera rader: COUNT, SUM, AVG, MIN, MAX.

## Alias
Tillfälligt namn på tabell eller kolumn: `SELECT p.Name FROM Product AS p`

