# INNER JOIN och LEFT JOIN 🔗

🟢


**Koppla ihop tabeller och få helhetsbild**

*Marcus Ackre Medina*
*Campus Mölndal - CLO25*
---

## Dagens agenda

1. **Varför JOIN?** - Problem med separata tabeller
2. **INNER JOIN** - Matcha på båda sidor
3. **LEFT JOIN** - Behåll allt från vänster
4. **RIGHT och FULL JOIN** - Andra varianter
5. **JOIN + GROUP BY** - Kraftfulla rapporter
6. **Best practices** - Undvik vanliga misstag

---

## Problemet utan JOIN

**Två separata tabeller:**

```sql
-- Students tabell
SELECT * FROM Students;
-- StudentId | FirstName | LastName
-- 1         | Anna      | Svensson
-- 2         | Erik      | Larsson

-- Enrollments tabell
SELECT * FROM Enrollments;
-- EnrollmentId | StudentId | CourseCode
-- 101          | 1         | CS101
-- 102          | 1         | CS102
```

**Hur kopplar vi ihop dem?** 🤔

---

## Lösningen: JOIN!

**JOIN = limmet som binder ihop tabellerna**

```sql
SELECT s.FirstName, s.LastName, e.CourseCode
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

**Resultat:**
| FirstName | LastName | CourseCode |
|-----------|----------|------------|
| Anna      | Svensson | CS101      |
| Anna      | Svensson | CS102      |

---

## Vad är INNER JOIN?

**INNER JOIN = visa bara matchningar som finns i BÅDA tabellerna**

- Om student saknar kurser → visas INTE
- Om kurs saknar student → visas INTE
- Bara där det finns en matchning → visas

**Tänk:** "Ge mig bara där båda sidor matchar"

---

## INNER JOIN - Syntax

```sql
SELECT kolumner
FROM tabell1 AS alias1
INNER JOIN tabell2 AS alias2
ON alias1.kolumn = alias2.kolumn;
```

**Viktiga delar:**
- `AS alias` - förkorta tabellnamn (s, e, o, etc.)
- `ON` - vilken kolumn kopplar tabellerna?
- `tabell1.kolumn` - specificera vilken tabell

---

## INNER JOIN - Praktiskt exempel

```sql
SELECT
    s.StudentId,
    s.FirstName,
    s.LastName,
    e.CourseCode,
    e.Grade
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
WHERE e.Grade >= 4
ORDER BY s.LastName, e.CourseCode;
```

**Resultat:** Bara studenter som HAR kurser och fått betyg ≥ 4

---

## Vad är LEFT JOIN?

**LEFT JOIN = visa allt från vänster tabell + matchningar från höger**

- Alla från vänster tabell visas ALLTID
- Om ingen matchning från höger → NULL
- Perfekt för "visa alla, även de som saknar..."

**Tänk:** "Ge mig alla från vänster, även om höger saknas"

---

## LEFT JOIN - Syntax

```sql
SELECT kolumner
FROM tabell1 AS alias1
LEFT JOIN tabell2 AS alias2
ON alias1.kolumn = alias2.kolumn;
```

**Skillnad mot INNER JOIN:**
- INNER → bara matchningar
- LEFT → alla från vänster + matchningar

---

## LEFT JOIN - Praktiskt exempel

```sql
SELECT
    s.StudentId,
    s.FirstName,
    s.LastName,
    e.CourseCode
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
ORDER BY s.StudentId;
```

**Resultat:**
| StudentId | FirstName | LastName | CourseCode |
|-----------|-----------|----------|------------|
| 1         | Anna      | Svensson | CS101      |
| 1         | Anna      | Svensson | CS102      |
| 2         | Erik      | Larsson  | NULL       |

---

## INNER JOIN vs LEFT JOIN

**INNER JOIN:**
```sql
-- Bara studenter MED kurser
SELECT s.FirstName, e.CourseCode
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

**LEFT JOIN:**
```sql
-- ALLA studenter, även UTAN kurser
SELECT s.FirstName, e.CourseCode
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

---

## Hitta poster som SAKNAR matchning

**LEFT JOIN + WHERE NULL = "visa det som INTE matchar"**

```sql
-- Studenter utan kurser
SELECT
    s.StudentId,
    s.FirstName,
    s.LastName
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
WHERE e.EnrollmentId IS NULL;
```

**Resultat:** Bara studenter som INTE är anmälda till någon kurs

---

## RIGHT JOIN och FULL JOIN

**RIGHT JOIN:**
- Motsatsen till LEFT JOIN
- Behåll allt från HÖGER tabell
- SQLite har INTE stöd för RIGHT JOIN

**FULL OUTER JOIN:**
- Behåll allt från BÅDA tabellerna
- SQLite har INTE stöd för FULL JOIN
- Använd `UNION` för att återskapa

---

## JOIN med flera tabeller

**Kedja flera JOINs:**

```sql
SELECT
    s.FirstName,
    s.LastName,
    c.CourseName,
    t.TeacherName
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
INNER JOIN Courses AS c ON e.CourseCode = c.CourseCode
INNER JOIN Teachers AS t ON c.TeacherId = t.TeacherId;
```

**Tips:** Börja med huvudtabell, lägg till en JOIN i taget

---

## JOIN + GROUP BY = Rapporter

**Kombinera för kraftfull analys:**

```sql
SELECT
    s.StudentId,
    s.FirstName,
    s.LastName,
    COUNT(e.CourseCode) AS CourseCount,
    ROUND(AVG(e.Grade), 2) AS AvgGrade
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
GROUP BY s.StudentId, s.FirstName, s.LastName
ORDER BY CourseCount DESC;
```

**Resultat:** Antal kurser + snittbetyg per student

---

## Praktiskt exempel - Kundrapport

**Kunder och deras ordrar:**

```sql
SELECT
    c.CustomerId,
    c.CustomerName,
    COUNT(o.OrderId) AS TotalOrders,
    SUM(o.TotalAmount) AS TotalSpent,
    ROUND(AVG(o.TotalAmount), 2) AS AvgOrder
FROM Customers AS c
LEFT JOIN Orders AS o ON c.CustomerId = o.CustomerId
GROUP BY c.CustomerId, c.CustomerName
ORDER BY TotalSpent DESC;
```

**LEFT JOIN säkerställer att alla kunder visas, även utan ordrar**

---

## Praktiskt exempel - Produkter per kategori

**Kategoristatistik:**

```sql
SELECT
    cat.CategoryName,
    COUNT(p.ProductId) AS ProductCount,
    ROUND(AVG(p.Price), 2) AS AvgPrice,
    MIN(p.Price) AS CheapestProduct,
    MAX(p.Price) AS MostExpensive
FROM Categories AS cat
LEFT JOIN Products AS p ON cat.CategoryId = p.CategoryId
GROUP BY cat.CategoryId, cat.CategoryName
HAVING COUNT(p.ProductId) > 0
ORDER BY ProductCount DESC;
```

---

## Alias - Varför och hur?

**Utan alias (långt och rörigt):**
```sql
SELECT Students.FirstName, Enrollments.CourseCode
FROM Students
INNER JOIN Enrollments ON Students.StudentId = Enrollments.StudentId;
```

**Med alias (kort och tydligt):**
```sql
SELECT s.FirstName, e.CourseCode
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

**Vanliga alias:** s, e, o, c, p, t

---

## WHERE vs ON vid JOIN

**ON = JOIN-villkor (hur tabeller kopplas)**
```sql
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
```

**WHERE = filtrera resultat EFTER join**
```sql
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
WHERE e.Grade >= 4
```

**OBS:** WHERE på NULL-kolumn efter LEFT JOIN tar bort "vänster-rader"!

---

## Vanliga fallgropar ⚠️

### 1. Ambiguous column error

```sql
-- ❌ FEL: Vilken StudentId?
SELECT StudentId, FirstName
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;

-- ✅ RÄTT: Specificera tabell
SELECT s.StudentId, s.FirstName
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

---

## Vanliga fallgropar ⚠️

### 2. WHERE tar bort LEFT JOIN-rader

```sql
-- ❌ FEL: WHERE e.CourseCode tar bort studenter utan kurser
SELECT s.FirstName, e.CourseCode
FROM Students AS s
LEFT JOIN Enrollments AS e ON s.StudentId = e.StudentId
WHERE e.CourseCode IS NOT NULL;  -- Gör LEFT JOIN till INNER JOIN!

-- ✅ RÄTT: Flytta till ON eller acceptera NULL
SELECT s.FirstName, e.CourseCode
FROM Students AS s
LEFT JOIN Enrollments AS e
ON s.StudentId = e.StudentId AND e.CourseCode IS NOT NULL;
```

---

## Vanliga fallgropar ⚠️

### 3. Dubblerade rader utan DISTINCT/GROUP BY

```sql
-- Problem: Student med 3 kurser = 3 rader
SELECT s.FirstName, COUNT(*)
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;

-- Lösning: GROUP BY
SELECT s.FirstName, COUNT(e.CourseCode) AS CourseCount
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId
GROUP BY s.StudentId, s.FirstName;
```

---

## Self JOIN - Jämför inom samma tabell

**Hitta mentorer och mentees:**

```sql
SELECT
    mentor.EmployeeName AS Mentor,
    mentee.EmployeeName AS Mentee
FROM Employees AS mentor
INNER JOIN Employees AS mentee ON mentor.EmployeeId = mentee.MentorId
ORDER BY mentor.EmployeeName;
```

**Samma tabell joinad mot sig själv!**

---

## CROSS JOIN - Kartesisk produkt

**Alla kombinationer (använd med försiktighet!):**

```sql
SELECT
    s.FirstName,
    c.CourseName
FROM Students AS s
CROSS JOIN Courses AS c;
```

**Resultat:** Om 30 studenter × 10 kurser = 300 rader!

**Användning:** Schema-generering, rapportmallar

---

## Tips & Best Practices 💡

1. **Indexera foreign keys** - snabbare JOINs
2. **Använd tydliga alias** - s, e, o, c
3. **Börja med INNER JOIN** - lägg till LEFT vid behov
4. **Undvik SELECT *** - välj kolumner explicit
5. **Testa stegvis** - en JOIN i taget
6. **Kommentera komplexa JOINs** - hjälp framtida-dig

---

## Prestanda tips 🚀

**Slow query:**
```sql
-- Saknar index på StudentId
SELECT s.FirstName, e.CourseCode
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

**Fast query:**
```sql
-- Med index
CREATE INDEX idx_enrollments_student ON Enrollments(StudentId);

SELECT s.FirstName, e.CourseCode
FROM Students AS s
INNER JOIN Enrollments AS e ON s.StudentId = e.StudentId;
```

---

## Övning 1 - Grön 🟢

**Uppgift:**
Lista alla kurser och de studenter som går dem.
Visa CourseCode, CourseName, StudentName.

**Tips:**
- Använd INNER JOIN mellan Courses och Enrollments
- Lägg till ytterligare JOIN till Students
- Sortera på CourseCode

---

## Övning 2 - Gul 🟡

**Uppgift:**
Visa alla studenter och antal kurser de går.
Inkludera även studenter utan kurser.

**Tips:**
- Använd LEFT JOIN
- Använd COUNT() och GROUP BY
- Sortera på antal kurser, fallande

---

## Övning 3 - Röd 🔴

**Uppgift:**
Skapa en rapport som visar:
- Kursnamn
- Antal anmälda studenter
- Genomsnittsbetyg
- Endast kurser med fler än 5 studenter
- Sorterat på högst snittbetyg först

**Tips:** Kombinera JOIN, GROUP BY, HAVING, och AVG()

---

## Sammanfattning

✅ **INNER JOIN** - bara matchningar från båda tabellerna
✅ **LEFT JOIN** - allt från vänster + matchningar från höger
✅ **JOIN + GROUP BY** - kraftfulla rapporter
✅ **Alias** - gör koden lättläst (s, e, o)
✅ **ON vs WHERE** - olika syften vid JOIN
✅ **Indexering** - viktigt för prestanda

**Nästa gång:** Entity Framework - JOINs i C#!

---

## Frågor? 🤔

**Resurser:**
- Handbok: `C-Sharp/databases/sql/joins.md`
- SQLite dokumentation: https://sqlite.org/lang_select.html
- Övningar i kursmaterialet

**Dad joke:**
Varför gillar databasen parmiddagar?
*För att den får joina två tabeller åt gången.* 😄

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
