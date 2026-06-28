---

title: Lösningsförslag Lektion 7
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/SQL/Övningar/Lösningsförslag - Lektion 7.sql"
description: "/* 1. Välj alla kolumner från employees som heter Markovitch i efternamn och"
tags: ["7.sql", "databaser", "exercise", "full", "join", "lektion", "lösningsförslag", "outer", "sql", "test"]
week_fit: []
---
```sql
USE employees;

# FULL OUTER JOIN och UNION, UNION ALL
/* 1. Välj alla kolumner från employees som heter Markovitch i efternamn och 
alla department managers */
SELECT 
    *
FROM
    employees AS e
        LEFT JOIN
    dept_manager AS dm ON employees.emp_no = dept_manager.emp_no
WHERE
    e.last_name = 'Markovitch' 
UNION ALL SELECT # UNION ALL SELECT
    *
FROM
    employees e
        RIGHT JOIN
    dept_manager dm ON e.emp_no = dm.emp_no
ORDER BY dept_no DESC;
/* När vi använder UNION får vi 204 rader och med UNION ALL returneras 205 rader. 
UNION tar bort duplikatet som är emp_no 110022 Margareta Markovitch som är chef på d001 och därför
förkommer i båda tabeller.*/


# VIEW
/* 2. Dept_emp innehåller vilken avdelning varje anställda jobbar på och i vilket tidsrum den anställda har jobbat på den avdelningen. 
Flera anställde dyker upp flera gånger för att de har jobbat på olika avdelningar.  
Tex emp_no 10010 dyker upp två gånger.*/
SELECT 
    *
FROM
    dept_emp;

/*För att verifiera att detta faktiskt är anledningen grupperar man efter emp_no, beräknar antal gånger en anställd förekommer och 
skriver ut alla som förekommer mer än en gång. */
/* Error message 1055 ONLY_FULL_GROUP_BY. Aggregate function på select kolumnera som har olika värden*/
SELECT 
    emp_no, from_date, to_date, COUNT(emp_no) AS num
FROM
    dept_emp
GROUP BY emp_no
HAVING num > 1;

/* MIN för att få minsta datum, max för att få största. 31.000 anställda */
SELECT 
    emp_no, MIN(from_date), MAX(to_date), COUNT(emp_no) AS num
FROM
    dept_emp
GROUP BY emp_no
HAVING num > 1;

/* Vill visa ebart senaste kontrakten för varje anställda. Använda VIEW*/
CREATE OR REPLACE VIEW v_dept_emp_latest AS
    SELECT 
        emp_no, MAX(from_date) AS from_date, MAX(to_date) AS to_date
    FROM
        dept_emp
    GROUP BY emp_no;

/* Enbart köra SELECT får vi vad VIEW visar*/
SELECT 
    emp_no, MAX(from_date) AS from_date, MAX(to_date) AS to_date
FROM
    dept_emp
GROUP BY emp_no;

SELECT 
    *
FROM
    employees.v_dept_emp_latest;

    
/* 3. Skapa en view som hämtar genomsnittlig lön för alla chefer registrerade i databasen. 
Avrunda till två decimaler. Skriv sedan ut denna view. */ 
CREATE OR REPLACE VIEW v_manager_avg_salary AS
    SELECT 
        ROUND(AVG(salary), 2) AS avg_salary_manager
    FROM
        salaries s
            JOIN
        dept_manager m ON s.emp_no = m.emp_no;
        
SELECT 
    *
FROM
    employees.v_manager_avg_salary;
    
    
# PROCEDURE
/* 4. Skriva ut de först 1000 raderna av employees databasen och spara det i en procedure */
DROP PROCEDURE IF EXISTS select_employees;  # OBS select_employees() ger inga paranteser här

DELIMITER $$
CREATE PROCEDURE select_employees()
BEGIN
	SELECT * FROM employees
    LIMIT 1000;
END $$
DELIMITER ;

CALL employees.select_employees();
/* Vi kan också ta fram vid att trycka på schemas - stored procedures - och blixten
Klicka vertygen för att få strukturen på procedure */


/* 5. Skapa en procedure kallad avg_salary som ger genomsnittslönen till alla anställda.
Kalla sedan på avg_salary. */
DROP PROCEDURE IF EXISTS avg_salary;

DELIMITER $$
CREATE PROCEDURE avg_salary()
BEGIN
	SELECT 
		AVG(salary)
	FROM
		salaries;
END $$
DELIMITER ;

CALL employees.avg_salary();

/* 6. Skapa en procedure som returnerar namn, lön och start och slutdatum för kontraktet 
till en specifik anställd man väljer. */
DROP PROCEDURE IF EXISTS emp_salary;

DELIMITER $$
CREATE PROCEDURE emp_salary(IN p_emp_no INT)
BEGIN
	SELECT 
		e.first_name, e.last_name, s.salary, s.from_date, s.to_date
	FROM
		employees e
			JOIN
        salaries s ON e.emp_no = s.emp_no # Obs denna = är för JOIN
	WHERE e.emp_no = p_emp_no;  # Obs denna = är för input parameter
END $$
DELIMITER ;
/* Hämta löneinformation om anställd nummer 11300 */
CALL employees.emp_salary(11300);


/* 7. Skapa en procedure som ger genomsnittslönen till en anställd.*/
DROP PROCEDURE IF EXISTS emp_avg_salary;

DELIMITER $$
CREATE PROCEDURE emp_avg_salary(IN p_emp_no INT)
BEGIN
	SELECT 
		e.first_name, e.last_name, AVG(s.salary)
	FROM
		employees e
			JOIN
        salaries s ON e.emp_no = s.emp_no # Obs denna = är för JOIN
	WHERE e.emp_no = p_emp_no;  # Obs denna = är för input parameter
END $$
DELIMITER ;
/* Hämta genomsnittslön till anställd nummer 11300 */
CALL employees.emp_avg_salary(11300);

/* 8. Skapa en procedure som ger genomsnittslönen till en anställd och spara den i en output parameter.*/
DROP PROCEDURE IF EXISTS emp_avg_salary_out;

DELIMITER $$
CREATE PROCEDURE emp_avg_salary_out(IN p_emp_no INT, OUT p_avg_salary DECIMAL(10,2))
BEGIN
	SELECT 
		AVG(s.salary)
	INTO p_avg_salary
    FROM
		employees e
			JOIN
        salaries s ON e.emp_no = s.emp_no # Obs denna = är för JOIN
	WHERE e.emp_no = p_emp_no;  # Obs denna = är för input parameter
END $$
DELIMITER ;
/* Hämta genomsnittslön till anställd nummer 11300 */
SET @p_avg_salary = 0;
CALL employees.emp_avg_salary_out(11300, @p_avg_salary);
SELECT @p_avg_salary;


/* 9. Skapa en procedure som heter emp_info som har input parametrar first_name och last_name 
och som har output parameter emp_no.*/
DROP PROCEDURE IF EXISTS emp_info;

DELIMITER $$
CREATE PROCEDURE emp_info(IN p_first_name VARCHAR(255), IN p_last_name VARCHAR(255), OUT p_emp_no INT)
BEGIN
	SELECT
		e.emp_no
	INTO p_emp_no 
    FROM
		employees e
	WHERE e.first_name = p_first_name
		AND e.last_name = p_last_name;
END$$
DELIMITER ;

# Vilket anställningsnummer har Ramzi Erde?
SET @p_emp_no = 0;
CALL employees.emp_info('Ramzi', 'Erde', @p_emp_no);
SELECT @p_emp_no;


# INDEX. 
/* 10. Jämföra duration tid i action output för att se att det går snabbare. (Kör om första SELECT)*/
DROP INDEX i_hire_date ON employees;

SELECT 
    *
FROM
    employees
WHERE
    hire_date > '2000-01-01';
    
CREATE INDEX i_hire_date ON employees(hire_date);

/* 11. Välj alla anställda som har namnet Georgi Facello */
SELECT 
    *
FROM
    employees
WHERE
    first_name = 'Georgi'
        AND last_name = 'Facello';
        
CREATE INDEX i_composite ON employees(first_name, last_name);

# Visa index
SHOW INDEX FROM employees FROM employees;


/* 12. Välj alla rader från salaries tabellen med anställda som tjänar mer än 89 000. 
Skapa sedan en idex på kolumnen salary i den tabellen och kolla om körningstiden är snabbare. */
SELECT 
    *
FROM
    salaries
WHERE
    salary > 89000;

CREATE INDEX i_salary ON salaries(salary);

SELECT 
    *
FROM
    salaries
WHERE
    salary > 89000;
    
DROP VIEW employees.current_dept_emp;
    
USE employees;

SELECT 
    *
FROM
    employees
WHERE
    last_name = 'Markovitch' 
UNION SELECT 
    *
FROM
    dept_manager;


# VARIABLE
/* 13. Spara informationen från emp_avg_salary_out i en variabel v_avg_salary för anställd nummer 11300.*/
SET @v_avg_salary = 0;
CALL employees.emp_avg_salary_out(11300, @v_avg_salary);
SELECT @v_avg_salary;


/* 14. I Uppgift 9 skapades en procedure emp_info. 
Skapa en variabel v_emp_no som sparar informationen om Aruna Journel. */
SET @v_emp_no = 0;
CALL emp_info('Aruna', 'Journel', @v_emp_no);
SELECT @v_emp_no;


# Function
/* 15. Skapa en funktion som ger genomsnittslönen till en anställd och spara den i en output parameter.*/
DROP FUNCTION IF EXISTS f_emp_avg_salary;

DELIMITER $$
CREATE FUNCTION f_emp_avg_salary(p_emp_no INT) RETURNS DECIMAL(10,2)
DETERMINISTIC
BEGIN
	
    DECLARE v_avg_salary DECIMAL(10,2);
	
    SELECT 
		AVG(s.salary)
	INTO v_avg_salary
    FROM
		employees e
			JOIN
        salaries s ON e.emp_no = s.emp_no # Obs denna = är för JOIN
	WHERE e.emp_no = p_emp_no;  # Obs denna = är för input parameter
    RETURN v_avg_salary;
END $$
DELIMITER ;

/* Hämta genomsnittslön till anställd nummer 11300 */
SELECT f_emp_avg_salary(11300);


/* 16. Skapa en funktion kallad emp_info som tar som input för- och efternamn av an anställd och ger som 
outputlönen till det senaste kontraktet denna anställda har. Välj sedan denna funktionen. */
DELIMITER $$
CREATE FUNCTION emp_info(p_first_name varchar(255), p_last_name varchar(255)) RETURNS decimal(10,2)
DETERMINISTIC 
BEGIN
	DECLARE v_max_from_date date;
    DECLARE v_salary decimal(10,2);
SELECT 
    MAX(from_date)
INTO v_max_from_date FROM
    employees e
        JOIN
    salaries s ON e.emp_no = s.emp_no
WHERE
    e.first_name = p_first_name
        AND e.last_name = p_last_name;
SELECT 
    s.salary
INTO v_salary FROM
    employees e
        JOIN
    salaries s ON e.emp_no = s.emp_no
WHERE
    e.first_name = p_first_name
        AND e.last_name = p_last_name
        AND s.from_date = v_max_from_date;
                RETURN v_salary;
END$$

DELIMITER ;

SELECT EMP_INFO('Aruna', 'Journel');
```
