---

title: Lösningsförslag Lektion 6
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/SQL/Övningar/Lösningsförslag - Lektion 6.sql"
description: "e.emp_no, e.first_name, e.last_name, dm.dept_no"
tags: ["6.sql", "databaser", "exercise", "inner", "join", "left", "lektion", "lösningsförslag", "right", "sql"]
week_fit: []
---
```sql
USE employees;

# LEFT JOIN VS INNER JOIN VS RIGHT JOIN
# LEFT JOIN
SELECT 
    e.emp_no, e.first_name, e.last_name, dm.dept_no
FROM
    employees e
        LEFT JOIN
    dept_manager dm ON e.emp_no = dm.emp_no
WHERE
    e.last_name = 'Markovitch'
ORDER BY dm.dept_no DESC;

# INNER JOIN
SELECT 
    e.emp_no, e.first_name, e.last_name, dm.dept_no
FROM
    employees e
        INNER JOIN
    dept_manager dm ON e.emp_no = dm.emp_no
WHERE
    e.last_name = 'Markovitch'
ORDER BY dm.dept_no DESC;

# RIGHT JOIN
SELECT
    e.emp_no, e.first_name, e.last_name, dm.dept_no
FROM
    employees e
        RIGHT JOIN
    dept_manager dm ON e.emp_no = dm.emp_no
WHERE
    e.last_name = 'Markovitch'
ORDER BY dm.dept_no DESC;

# CROSS JOIN
/* JOIN alla chefer (managers) och vilka avdelningar de hypotetisk kan vara chefer på.
Lägg märke till att alla avdelningsnummer har blivit kopplat till alla chefer.*/
SELECT 
    *
FROM
    dept_manager;

SELECT 
    dm.*, d.*
FROM
    dept_manager dm
        CROSS JOIN
    departments d
ORDER BY dm.emp_no , d.dept_no;

/* CROSS JOIN är det samma som att använda SELECT.. FROM flera tabeller*/
SELECT 
    dm.*, d.*
FROM
    dept_manager dm,
    departments d
ORDER BY dm.emp_no , d.dept_no;


/* Använd CROSS JOIN för att ge en lista med alla möjliga kombinationer mellan chefer
från dept_manager tabellen och avdelning nummer 9 från departments */
 SELECT 
    dm.*, d.*
FROM
    departments d
        CROSS JOIN
    dept_manager dm
WHERE
    d.dept_no = 'd009'
ORDER BY d.dept_name;


# SELF JOIN  
SELECT 
    m1.*, m2.*
FROM
    dept_manager m1
        JOIN
    dept_manager m2 ON m1.dept_no = m2.dept_no
WHERE
    m1.emp_no != m2.emp_no
        AND m1.dept_no = m2.dept_no
ORDER BY m1.dept_no;


# JOIN & WHERE
/*Vill se emp_no, för, efternamn för anställde som lön högre än 145000. JOIN två tabeller. 
Baserad på vilken information vill vi hämta data? WHERE Salary>145000 */
SELECT 
    e.emp_no, e.first_name, e.last_name, s.salary
FROM
    employees e
        JOIN
    salaries s ON e.emp_no = s.emp_no
WHERE
    salary > 145000
ORDER BY salary;

/* Välj för och efternamn, anställningsdatum och jobbtitel på alla anställda som har 
 förnamn Margareta och efternamn Makrovitch */
SELECT 
    e.emp_no, e.first_name, e.last_name, e.hire_date, t.title
FROM
    employees e
        JOIN
    titles t ON e.emp_no = t.emp_no
WHERE
    e.first_name = 'Margareta'
        AND e.last_name = 'Markovitch';

# JOIN flera tabeller  
/* Till alla department managers, skriv en lista med first name, last name, hire date, 
date promoted to managers and departent name.   
Tips är att kolla på relational schema för employees först. */
SELECT 
    e.first_name,
    e.last_name,
    e.hire_date,
    dm.from_date,
    d.dept_name
FROM
    employees e
        JOIN
    dept_manager dm ON e.emp_no = dm.emp_no
        JOIN
    departments d ON dm.dept_no = d.dept_no;

/* Ge en lista med alla avdelningar och genomsnittslön till alla chefer. 
Lägg märke till att det inte är en direkt key relation mellan de två tabellerna 
och att man kan använda andra gemensamma kolumner för att koppla ihop tabellerna */
SELECT 
    d.dept_name, AVG(s.salary) AS average_salary
FROM
    departments d
        JOIN
    dept_manager dm ON d.dept_no = dm.dept_no
        JOIN
    salaries s ON dm.emp_no = s.emp_no
GROUP BY d.dept_name
ORDER BY average_salary DESC;

# Aggregate functions med JOINS
/* Genomsnittslön för alla män och kvinnor i företaget */
SELECT 
    e.gender, AVG(salary) AS average_salary
FROM
    employees e
        JOIN
    salaries s ON e.emp_no = s.emp_no
GROUP BY e.gender;

# Subqueries
/* Hämta information om alla department managers som blev anställda 
mellan 1990-01-01 och 1995-01-01 */
SELECT 
    *
FROM
    dept_manager
WHERE
    emp_no IN (SELECT 
            emp_no
        FROM
            employees
        WHERE
            hire_date BETWEEN '1990-01-01' AND '1995-01-01');

/* Välj all information från employees som har jobbtitel assistant engineer */
SELECT 
    *
FROM
    employees e
WHERE
    EXISTS( SELECT 
            *
        FROM
            titles t
        WHERE
            t.emp_no = e.emp_no
                AND title = 'Assistant Engineer');
                
SELECT 
    emp_no, first_name, last_name, salary
FROM
    (SELECT 
        emp_no, first_name, last_name
    FROM
        employees
    WHERE
        hire_date BETWEEN '1998-01-01' AND '1999-01-01'
    LIMIT 20) AS emp_search
        INNER JOIN
    (SELECT 
        emp_no, salary
    FROM
        salaries) AS salary USING (emp_no);
```
