Övningar – Lektion 4


Vi kommer bland annat använda Salary-databasen vi har jobbat med de två sista övningarna. Om ni inte har skapat hela databasen med värden i tabeller i SQL finns den på Teams under V2 och Lösningsförslag övningar – Lektion 2.sql


ALTER TABLE

- Lägga till en ny kolumn adress med datatypen CHAR i tabellen companies.


- Ändra datatypen till adress till VARCHAR(255).


- Ändra namnet på kolumnen adress i tabellen companies till company_adress.


- Radera kolumnen company_adress.


- W3schools har bra tutorias med tillhörande exempel och övningar man löser online. Gå igenom för ALTER TABLE samt DROP TABLE.

- https://www.w3schools.com/sql/sql_alter.asp

- https://www.w3schools.com/sql/sql_drop_table.asp


SELECT

- Välj alla kolumner i tabellen companies.


- Välj förnamn och efternamn på kunderna i tabellen customers.


- https://www.w3schools.com/sql/sql_select.asp


WHERE & comparison operators

- I tabellen sales, välj alla försäljningar gjord av kunder med customer_id lika med 1.


- I tabellen items, välj alla item med unit_price_usd störra än 100.


- Välj alla kunder har haft kalgomål.


- https://www.w3schools.com/sql/sql_where.asp


WHERE & AND, OR, NOT operators

- Skriv all försäljningsinformation från när Kevin har köpt en stol.


- Få information om artiklar (items) som är lampa eller skrivbord.


- Från tabellen sales, välj alla försäljningar utom den gjord av customer_id lika med 1.


- https://www.w3schools.com/sql/sql_and_or.asp


WHERE & IN

- Välj från tabellen sales där item code antingen är A_1, B_1, C_1 eller D_1.


- https://www.w3schools.com/sql/sql_in.asp


WHERE & LIKE & wildcards

- Från tabellen om companies, välj företagsnamn samt telefonnummer till de företagen som telefonnummer börjar med '+1(202)'.


- https://www.w3schools.com/sql/sql_like.asp


- https://www.w3schools.com/sql/sql_wildcards.asp


IS NULL

- https://www.w3schools.com/sql/sql_null_values.asp


WHERE & BETWEEN

- Välj från tabellen sales alla försäljningar gjorde mellan 2017-01-01 och 2017-07-01.


- https://www.w3schools.com/sql/sql_between.asp


SELECT & Aggregate functions COUNT, SUM, MIN, MAX, AVG

- Hur manga varor har ett pris större eller lika med 150 USD? Man kan använda (*) här.


- Vad är högsta antal klagomål en kund har gett?


- Vad är genomsnittspriset på föremålen som säljs?


- Vad är genomsnittspriset på föremålen lampa, skrivbord och stol?


- https://www.w3schools.com/sql/sql_count_avg_sum.asp


- https://www.w3schools.com/sql/sql_min_max.asp


SELECT & DISTINCT

- Hur många olika föremål (item) är det i tabellen items?


- Hur många olika customer_id är det i sales databasen?


- https://www.w3schools.com/sql/sql_distinct.asp

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
