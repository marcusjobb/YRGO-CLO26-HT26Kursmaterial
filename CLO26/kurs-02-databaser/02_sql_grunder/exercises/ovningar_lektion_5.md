---

title: Övningsuppgifter – Lektion 5
author: Marcus Ackre Medina
type: exercise
topic: sql
difficulty: 2
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Övningar - Lektion 5.md"
description: "ptdeptÖvningsuppgifter – Lektion 5"
tags: ["aggregate-functions", "as", "exercise", "group-by", "having", "join", "order-by", "select", "sql"]
week_fit: []
---

ptdeptÖvningsuppgifter – Lektion 5


För dessa övningsuppgifter behöver vi employees.sql databasen som finns att ladda hem i Teams.

Som förra veckan är det bra träning att gå in på https://www.w3schools.com/sql/ för varje avsnitt och kolla igenom exempel och göra tillhörande övningar.


Relational schema av employees.sql


- Ladda hem, öppna och kör employees.sql databasen i MySQL. Skapa en ny SQL fil som ni gör dagens övningsuppgifter i. För att rätt databas skall användas börja filen med att skriva

USE employees;


- Studera relational schema av employees databasen ovanför samt SQL filen där databasen skapas. Se på tabellen dept_emp. Lägg märke till hur emp_no och dept_no kombineras till en primary key. Detta markeras i relational schema med att fler kolumner i samma tabell är understrukna. Sedan är också emp_no en foreign key och dept_no en foreign key. Detta markeras med (FK). Finns det flera tillfällen av detta i databasen?


AND & OR

- Hämta en lista med alla kvinnliga anställda och som har ett förnamn som antingen är Kellie eller Aruna.

- (Hint: använd tabellen employees och kolumnerna gender och first_name )


ORDER BY

- Välj all data från employees tabellen och sortera den efter hire_date i fallande ordning.


- Skriv ut first_name, last_name och hire_date från tabellen employees för alla som anställdes  mellan 1998-01-01 och 1999-01-01. Sortera efter hire_date.


GROUP BY

- Från titles tabellen, skapa en lista med alla olika jobb som finns.


- Från titles tabellen, skapa en lista med alla olika jobb som finns och hur många anställda som har denna titeln.

- (Hint: använd COUNT())


- Från employees tabellen, skapa en lista med alla kvinnliga anställdas olika förnamn.

- (Hint: använd WHERE)


AS

- Gör som i uppgift 6 och skapa en lista med olika jobb och hur många anställda som har den titeln. Döpa om kolumnen med antal anställa till no_emps_same_title.


Lite svårare uppgifter där vi kombinerar AS, WHERE, GROUP BY, ORDER BY och aggregate functions

- Skriv en query som skriver ut två kolumner från tabellen salaries. Första kolumnen skall ha lön (salary) högre än 100 000. Den andra kolumnen skall visa antal anställda som har de olika lönerna och skall döpas om till emps_with_same_salary. Till sist skall outputen sorteras efter första kolumnen salary.


- Från employees tabellen, skriv ut två kolumner. En som innehåller alla olika förnamn till kvinnliga anställda och en kolumn med namn females_same_first_name som visar hur många kvinnliga anställda som har samma förnamn. Sortera listan efter förnamn.


HAVING

- Använd tabellen titles. Skapa en lista med två kolumner title och COUNT(emp_no). Listan skall innehålla antal anställda med de olika arbetstitlarna. Med andra ord använd GROUP BY title. Ge namnet count_title till kolumnen med antal COUNT(emp_no). Sortera efter COUNT(emp_no) så att titeln med minst antal är överst.

- Gör exakt samma som ovan men nu enbart med de titlarna med mer än 90 000 anställda.


Lite svårare uppgift om HAVING och WHERE

- Det finns nio olika avdelningar i employees databasen. De nio olika avdelningarna har var sin kod under kolumnnamnet dept_no. För att se vilka de är kan man använda

SELECT * FROM departments;

I tabellen dept_emp visas alla anställningar på alla avdelningar och vilket tidsintervall den anställda har varit på avdelningen. Skriver man ut alla rader

SELECT * FROM dept_emp;

får man 331 603 rader alltså finns det så många anställningar på alla avdelningar. Skriver man ut de unika anställningsnumren

SELECT DISTINCT emp_no FROM dept_emp;

får man i stället 300 024 rader. Samma person kan ha varit anställd på olika avdelningar och då fått en ny rad i tabellen.

Använd tabellen dept_emp. Välj anställningsnummer (emp_no) på alla individer som har varit anställda på mer än en avdelning (dept_no) efter datumet (from_date) 2000-01-01.


INNER & LEFT JOIN

- I tabellen dept_manager hittar man information om vilka avdelningar de olika cheferna är ansvarig för (dept_no). I tabellen employees hittar man personlig information om alla medarbetare, inklusive cheferna. Skapa en lista med information om alla chefers anställningsnummer, förnamn och efternamn samt vilken avdelning de är ansvarig för.


- Join employees och dept_manager för att ge en lista alla anställda som heter 'Markovitch' i efternamn. Listan borde innehålla kolumnerna emp_no, fist_name, last_name och dept_no. Innehåller resultatet en chef (manager) med det namnet? Testa sortera (ORDER BY) för att se detta lättare.

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
