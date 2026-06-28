---

title: Övningsuppgifter – Lektion 6
author: Marcus Ackre Medina
type: exercise
topic: sql
difficulty: 2
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Övningar – Lektion 6.md"
description: "- Gå in på SQLZoo och gör övningar och quiz för att träna på SELECT och JOIN  https://sqlzoo.net/wiki/SQL_Tutorial"
tags: ["aggregate-functions", "cross-join", "exercise", "inner-join", "join", "left-join", "right-join", "sql", "subqueries"]
week_fit: []
---

Övningsuppgifter – Lektion 6

- Gå in på SQLZoo och gör övningar och quiz för att träna på SELECT och JOIN  https://sqlzoo.net/wiki/SQL_Tutorial


LEFT JOIN VS INNER JOIN VS RIGHT JOIN

- Join employees och dept_manager för att ge en lista alla anställda som heter 'Markovitch' i efternamn. Listan borde innehålla kolumnerna emp_no, fist_name, last_name och dept_no. Innehåller resultatet en chef (manager) med det namnet? Testa sortera (ORDER BY) för att se detta lättare. Ändra mellan att använda INNER, LEFT, RIGHT JOIN och se skillnaden.

CROSS JOIN

- JOIN alla chefer (managers) och vilka avdelningar de hypotetisk kan vara chefer på. Lägg märke till att alla avdelningsnummer har blivit kopplat till alla chefer.

- CROSS JOIN är det samma som att använda SELECT.. FROM flera tabeller. Testa använda FROM med flera tabeller i uppgift 3.

- Använd CROSS JOIN för att ge en lista med alla möjliga kombinationer mellan chefer från dept_manager tabellen och avdelning nummer 9 från departments.

JOINS & WHERE

- Gör en lista med emp_no, för, efternamn för anställde som har lön högre än 145 000. JOIN två tabeller. Baserad på vilken information vill vi hämta data? WHERE Salary > 145 000.

- Välj för och efternamn, anställningsdatum och jobbtitel på alla anställda som har förnamn Margareta och efternamn Makrovitch

JOIN flera tabeller

- Till alla department managers, skriv en lista med first name, last name, hire date, date promoted to managers and departent name.  Tips är att kolla på relational schema för employees först.

- Ge en lista med alla avdelningar och genomsnittslön till alla chefer. Lägg märke till att det inte är en direkt key relation mellan de två tabellerna och att man kan använda andra gemensamma kolumner för att koppla ihop tabellerna.

Aggregate functions med JOINS.

- Hitta genomsnittslön för alla män och kvinnor i företaget.

SUBQUERIES

- Hämta information om alla department managers som blev anställda mellan 1990-01-01 och 1995-01-01.

- Välj all information från employees som har jobbtitel assistant engineer.

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
