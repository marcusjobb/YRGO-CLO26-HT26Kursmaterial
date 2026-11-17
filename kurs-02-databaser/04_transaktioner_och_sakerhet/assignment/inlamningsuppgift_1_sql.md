Inlämningsuppgift 1


Du ska skapa en relationsdatabas i SQL. Databasen ska planeras och designas med diagram. Sedan ska SQL användas för att skapa tabeller och ställa frågor mot databasen. (SQL kod)

Scenariot: Ett företag som hyr ut filmer ska ha en databas om sina kunder och vad de har hyrt. Förslag på vad en sådan databas kan innehålla är kundinformation, filminfo och antal kopior, stjärnskådisar, studio och genre.

(OBS! Fyra, fem records per tabell kommer att behövas för att kunna demonstrera dina queries. Använd gärna git & GitHub för att verisionshantera och hålla reda på koden. Det är dock inget krav.)


För att få godkänt skall du:

- Bestämma och planera innehållet i en databas med ursprungsläge enligt ovan.

- Databasen ska innehålla minst tre tabeller.

- Det ska finnas kopplingar mellan åtminstone två av tabellerna (ett-till-många-förhållande).  Kopplingar ska skapas som primary och foreign keys.


- Databasen skall vara normaliserad till tredje graden (N3)

- Databasen ska planeras med diagram (relational schemas)

- One to many etc ska framgå genom pilarna mellan tabeller men optional, mandatory osv. är inte nödvändigt att visa.


- Tabeller ska sedan skapas i SQL från den planerade databasen. (ex CREATE TABLE data_type constraints, USE, INSERT INTO.. VALUES..)

- SQL-frågor (queries) ska ställas till databasen. Någon query kan till exempel visa, lägga till, uppdatera eller ta bort data. (ex ALTER TABLE.. ADD, DROP, MODIFY, RENAME)

- Flera SQL-frågor ska ställas till databasen ihop med SELECT.. FROM.. WHERE.. . Du får välja vilka som passar ihop med databasen. Förslag på kommandon som kan användas är

- Operators: AND, OR, NOT, IN, LIKE, IS NULL, BETWEEN

- Comparison operators: = < > >= <= !=

- Aggregate functions COUNT, SUM, MIN, MAX, AVG

- Sortera och gruppera data: SORT BY, GROUP BY, AS, HAVING

- Hämta ut information av fler tabeller samtidigt: INNER JOIN, LEFT/RIGHT JOIN,

- Kommentera koden! Vad för query ställs till databasen, alltså vilken information önskar man få ut?


Täckta kursplansmål:

6. Använda databasspråket SQL för att hämta och manipulera data

7. Använda databasspråket SQL för att skapa databas med tabeller med god data-och referensintegritet

8. Tillämpa databasmodellering enligt ER-modell och normalisera databastabeller


Deadline och inlämning:

- Lämnas in senast Måndag 9/11 kl 23.59 i PingPong.

- En zippad fil ska lämnas in som innehåller tre filer:

- Den ena skapar tabellerna och lägger in records(data) (SQL fil).

- Den andra där alla queries till databasen ställs (SQL fil).

- I tredje filen skall relational schema finnas (pdf/png/jpg)
