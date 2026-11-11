# Inlämningsuppgift 1

🟡



Ni ska skapa relationsdatabas i SQL. Databasen ska planeras och designas och sedan ska SQL användas för att skapa tabeller och ställa frågor mot databasen.

Scenario: Ett företag som hyr ut film ska ha en databas om kunderna. Förslag på vad en sådan databas kan innehålla är kundinformation, album, artist, låtar och genre.


# För att få godkänt skall ni:

- Bestämma och planera innehållet i en databas med ursprungsläge enligt ovan.

- Databasen ska innehålla minst tre tabeller.

- Databasen ska planeras med diagram (relational schemas).

- Det ska finnas kopplingar mellan åtminstone två av tabellerna (ett-till-många-förhållande).  Kopplingar ska skapas som primary och foreign keys.

- Tabeller ska sedan skapas i SQL från den planerade databasen. (ex CREATE DATABASE, CREATE TABLE data_type constraints, USE, INSERT INTO.. VALUES..)

- SQL-frågor (queries) ska ställas till databasen. Någon query kan till exempel visa, lägga till, uppdatera eller ta bort data. (ex ALTER TABLE.. ADD, DROP, MODIFY, RENAME)

- Flera SQL-frågor ska ställas till databasen ihop med SELECT.. FROM.. WHERE.. . Ni får välja vilka som passar ihop med databasen och förslag på kommandon som kan användas är

- Operators: AND, OR, NOT, IN, LIKE, IS NULL, BETWEEN

- Comparison operators: = < > >= <= !=

- Det ska finnas en kortfattad beskrivning av databasens innehåll och syfte.

- Ni får använda redan existerande data.

- Kommentera koden! Vad för query ställs till databasen, alltså vilken information önskar man få ut?


# Utöver kraven för godkänt så skall ni för VG:

- Databasen ska innehålla minst fem tabeller där det är kopplingar mellan tre av tabellerna.

- SQL queries ska ge en tydlig bild över databasen syfte och innehåll genom att få ut relevant data och någon form av statistik. Till exempel kan man använda

- Aggregate functions COUNT, SUM, MIN, MAX, AVG

- Sortera och gruppera data: SORT BY, GROUP BY, AS, HAVING

- Hämta ut information av fler tabeller samtidigt: INNER JOIN, LEFT/RIGHT JOIN,


# Täckte kursplansmål:

- Sätta upp en SQL miljö.

- Designa en databas och använda SQL för att skapa tabeller och köra frågor och sökningar mot databasen.

- Analys och optimering av SQL frågor.

- Index och samtidighet.


# Deadline och inlämning:

- Lämnas in senast söndag 25/10 kl 23.59 i PingPong.

- En zippad fil ska lämnas in som innehåller tre filar:

- Den ena skapar databasen (SQL fil).

- Den andra där alla queries till databasen ställs (SQL fil).

- I tredje filen skall beskrivningen av databasen samt relational schema finnas (text dokument)

- Den zippade filen skall namnges med namn, vilken inlämnningsuppgift och betygsönskan. Ex Eva_Hegnar_inlämning1_VG.zip
