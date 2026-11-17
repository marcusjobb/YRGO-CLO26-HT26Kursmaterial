# Normalisering

🟢


Normalisering är processen att organisera data i en databas för att minimera redundans och beroenden. Genom att följa normaliseringsregler skapar vi databaser som är effektiva, konsekventa och lätta att underhålla. Denna guide täcker alla fem normalformer (1NF, 2NF, 3NF, BCNF, 4NF, 5NF) - där de tre första täcker 95% av praktiska databaser, medan de högre normalformerna är användbara i specifika situationer.

## Innehållsförteckning

- [Introduktion](#introduktion)
- [Varför normalisera](#varför-normalisera)
- [Problem med onormaliserad data](#problem-med-onormaliserad-data)
- [Nyckelkoncept](#nyckelkoncept)
- [Första normalformen (1NF)](#första-normalformen-1nf)
- [Andra normalformen (2NF)](#andra-normalformen-2nf)
- [Tredje normalformen (3NF)](#tredje-normalformen-3nf)
- [Normaliseringsprocessen steg-för-steg](#normaliseringsprocessen-steg-för-steg)
- [Praktiskt exempel: Bokhandel](#praktiskt-exempel-bokhandel)
- [När ska man INTE normalisera](#när-ska-man-inte-normalisera)
- [Ytterligare normalformer](#ytterligare-normalformer)
  - [Boyce-Codd Normal Form (BCNF)](#boyce-codd-normal-form-bcnf)
  - [Fourth Normal Form (4NF)](#fourth-normal-form-4nf)
  - [Fifth Normal Form (5NF)](#fifth-normal-form-5nf)
- [Externa resurser](#externa-resurser)
- [Slutsats](#slutsats)
- [TL;DR](#tldr)

## Introduktion

Föreställ dig ett Excel-ark där samma kundinformation upprepas på varje rad för varje beställning. Vad händer när en kund byter adress? Du måste uppdatera hundratals rader! Vad händer om du skriver fel på några rader? Nu har samma kund flera olika adresser i systemet.

Detta är exakt de problem normalisering löser. Edgar F. Codd introducerade normaliseringskonceptet 1970 som en del av den relationella databasmodellen. Hans arbete revolution

erade hur vi strukturerar data och är fortfarande fundamentalt idag.

**Normalisering handlar om:**

1. **Eliminera redundans** - Samma data lagras bara EN gång
2. **Säkerställa integritet** - Data förblir konsekvent
3. **Optimera för uppdateringar** - Ändringar görs på ETT ställe
4. **Förhindra anomalier** - Inga konstiga bieffekter vid CRUD-operationer

## Varför normalisera

### Lagringsutrymme

**Onormaliserad databas:**

```
Orders tabell (1,000 beställningar från 100 kunder):
- Varje rad innehåller: OrderId, CustomerName, CustomerEmail, CustomerAddress, CustomerPhone, ...
- Total storlek: ~500 KB
```

**Normaliserad databas:**

```
Customers tabell (100 kunder):
- CustomerName, CustomerEmail, CustomerAddress, CustomerPhone

Orders tabell (1,000 beställningar):
- OrderId, CustomerId (bara en referens!)

Total storlek: ~120 KB (76% mindre!)
```

### Dataintegritet

**Scenario:** En kund flyttar och byter e-post.

**Onormaliserad:**

- Måste uppdatera 50 rader (alla kundens beställningar)
- Risk: Glömmer bort några → kunden har nu OLIKA adresser i systemet
- Resultat: Vilken är korrekt? Ingen vet!

**Normaliserad:**

- Uppdatera 1 rad i Customers-tabellen
- Alla beställningar refererar automatiskt till nya data
- Resultat: Alltid konsekvent!

### Prestanda för skrivoperationer

**Onormaliserad:**

```sql
-- Uppdatera kundadress = uppdatera 50 rader
UPDATE Orders
SET CustomerAddress = 'Nya gatan 123'
WHERE CustomerName = 'Anna Andersson';
-- 50 disk-skrivningar!
```

**Normaliserad:**

```sql
-- Uppdatera kundadress = uppdatera 1 rad
UPDATE Customers
SET Address = 'Nya gatan 123'
WHERE CustomerName = 'Anna Andersson';
-- 1 disk-skrivning!
```

### Flexibilitet

**Onormaliserad:** Vad händer om vi vill lägga till en "preferred payment method" för kunder?

- Måste lägga till kolumn i Orders
- Måste fylla i samma värde för alla kundens 50 beställningar
- Vad händer med gamla beställningar?

**Normaliserad:**

- Lägg till kolumn i Customers
- Fyll i ETT värde
- Alla beställningar ser automatiskt nya data via foreign key

## Problem med onormaliserad data

### Insertion Anomalies (Insättningsanomalier)

**Problem:** Kan inte lägga till viss information utan att lägga till orelaterad information.

**Exempel:**

```
Orders tabell (onormaliserad):
OrderId | CustomerName | CustomerEmail      | ProductName | Price
--------|----------|---------------|---------|---
1       | Anna         | anna@example.com  | Bok         | 199
```

**Scenario:** Vi vill lägga till en ny kund i systemet (så vi kan skicka nyhetsbrev), men kunden har inte beställt något än.

**Problem:** KAN INTE! Orders-tabellen kräver en beställning för att lagra kundinformation. Vi måste skapa en "dummy order" vilket är konstigt och fel.

**Lösning (normaliserad):**

```sql
-- Separata tabeller
CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY,
    Name TEXT,
    Email TEXT
);

-- Nu kan vi lägga till kunder utan beställningar!
INSERT INTO Customers (Name, Email) VALUES ('Anna', 'anna@example.com');
```

### Update Anomalies (Uppdateringsanomalier)

**Problem:** Måste uppdatera samma information på flera ställen.

**Exempel:**

```
Orders (onormaliserad):
OrderId | CustomerName | CustomerEmail       | Product
--------|----------|----------------|-----
1       | Anna         | anna@example.com   | Bok
2       | Anna         | anna@example.com   | Penna
3       | Anna         | anna@eggsample.com | Sudd  ← GLÖMDE UPPDATERA!
```

Anna bytte e-post, men vi glömde uppdatera en rad. Nu har hon två e-postadresser i systemet - vilken är korrekt?

**Lösning (normaliserad):**

```sql
Customers:
CustomerId | Name | Email
-----------|---|--------------
1          | Anna | anna@example.com  ← Uppdateras HÄR

Orders:
OrderId | CustomerId | Product
--------|--------|----
1       | 1          | Bok
2       | 1          | Penna
3       | 1          | Sudd
```

Alla beställningar refererar till SAMMA kunddata via CustomerId!

### Deletion Anomalies (Raderingsanomalier)

**Problem:** Radering av en post tar med sig orelaterad information.

**Exempel:**

```
Orders (onormaliserad):
OrderId | CustomerName | CustomerEmail      | Product
--------|----------|---------------|-----
1       | Anna         | anna@example.com  | Bok
```

**Scenario:** Kunden returnerar boken, vi raderar beställningen.

**Problem:** Vi förlorar ALL kundinformation! Namn, e-post, allt borta. Vi kan inte ens skicka ett "hoppas vi ses snart"-mail.

**Lösning (normaliserad):**

```sql
-- Radera beställning
DELETE FROM Orders WHERE OrderId = 1;

-- Kundinformation finns kvar i Customers!
SELECT * FROM Customers WHERE CustomerId = 1;
-- Anna's data finns fortfarande
```

## Nyckelkoncept

Innan vi dyker in i normalformerna måste vi förstå några grundläggande koncept.

### Primärnyckel (Primary Key)

En kolumn (eller kombination av kolumner) som UNIKT identifierar varje rad.

**Exempel:**

```sql
CREATE TABLE Students (
    StudentId INTEGER PRIMARY KEY,  ← Primärnyckel
    PersonalNumber TEXT UNIQUE,
    Name TEXT
);
```

**Egenskaper:**

- ✓ Unikt för varje rad
- ✓ Aldrig NULL
- ✓ Bör aldrig ändras

**Typer:**

- **Simple Key**: En kolumn (StudentId)
- **Composite Key**: Flera kolumner tillsammans

```sql
CREATE TABLE CourseEnrollments (
    StudentId INTEGER,
    CourseId INTEGER,
    PRIMARY KEY (StudentId, CourseId)  ← Sammansatt nyckel
);
```

### Främmande nyckel (Foreign Key)

En kolumn som refererar till primärnyckeln i en annan tabell.

```sql
CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY,
    CustomerId INTEGER,  ← Främmande nyckel
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
```

### Funktionellt beroende (Functional Dependency)

**Definition:** Kolumn B är funktionellt beroende av kolumn A om värdet av A unikt bestämmer värdet av B.

**Notation:** `A → B` (läs: "A bestämmer B")

**Exempel:**

```
StudentId → Name
StudentId → Email
StudentId → DateOfBirth
```

Om vi känner StudentId (t.ex. 1234), kan vi unikt bestämma studentens namn, e-post och födelsedatum.

**Viktigt:**

```
Name → StudentId  ← INTE sant!
```

Flera studenter kan heta "Anna Andersson", så namn bestämmer INTE unikt StudentId.

### Partiellt beroende (Partial Dependency)

När ett attribut är beroende av BARA EN DEL av en sammansatt primärnyckel.

**Exempel:**

```
Primärnyckel: (StudentId, CourseId)

StudentId, CourseId → Grade       ← Fullt beroende (behöver båda)
StudentId → StudentName            ← Partiellt beroende (behöver bara StudentId)
CourseId → CourseName              ← Partiellt beroende (behöver bara CourseId)
```

Detta är DÅLIGT och bryts av 2NF!

### Transitivt beroende (Transitive Dependency)

När ett icke-nyckel-attribut är beroende av ett annat icke-nyckel-attribut.

**Exempel:**

```
StudentId → DepartmentId → DepartmentName

StudentId är primärnyckel
DepartmentId är INTE primärnyckel
DepartmentName beror på DepartmentId (inte direkt på StudentId)
```

Detta är DÅLIGT och bryts av 3NF!

## Första normalformen (1NF)

### Definition

En tabell är i 1NF om:

1. **Alla kolumner innehåller atomära värden** (inga listor, komma-separerade värden)
2. **Varje kolumn innehåller värden av samma typ**
3. **Varje kolumn har ett unikt namn**
4. **Ordningen på rader spelar ingen roll**

### Problem: Multi-värdes attribut

**DÅLIGT** (Bryter mot 1NF):

```
Students:
StudentId | Name  | Courses
----------|---|------------------
1         | Anna  | Math,Physics,Chemistry  ← FLERA värden i EN kolumn!
2         | Bengt | Math,Biology
```

**Varför dåligt?**

- Hur söker vi efter alla som läser "Math"? `LIKE '%Math%'` fångar även "Mathematics"!
- Vad om en kurs innehåller kommatecken i namnet?
- Hur lägger vi till/tar bort en kurs?
- Hur sorterar vi på kurs?

**LÖSNING 1: Repetera rader**

```
Students_Courses (1NF):
StudentId | Name  | Course
----------|---|------
1         | Anna  | Math
1         | Anna  | Physics
1         | Anna  | Chemistry
2         | Bengt | Math
2         | Bengt | Biology
```

✓ Nu i 1NF: Varje cell har ETT värde
✗ Men: Namnet upprepas (redundans)

**LÖSNING 2: Separata tabeller (bättre)**

```sql
CREATE TABLE Students (
    StudentId INTEGER PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE Courses (
    CourseId INTEGER PRIMARY KEY,
    CourseName TEXT NOT NULL
);

-- Many-to-Many kopplingstabell
CREATE TABLE StudentCourses (
    StudentId INTEGER,
    CourseId INTEGER,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
```

### Problem: Grupperade attribut

**DÅLIGT** (Bryter mot 1NF):

```
Students:
StudentId | Name  | Phone1      | Phone2      | Phone3
----------|---|---------|---------|---------
1         | Anna  | 0701234567  | 0812345678  | NULL
2         | Bengt | 0731234567  | NULL        | NULL
```

**Varför dåligt?**

- Vad om någon har 4 telefonnummer?
- Slösar utrymme för de med bara 1 nummer
- Svårt att söka "alla telefonnummer för Anna"

**LÖSNING:**

```sql
CREATE TABLE Students (
    StudentId INTEGER PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE PhoneNumbers (
    PhoneId INTEGER PRIMARY KEY,
    StudentId INTEGER NOT NULL,
    PhoneNumber TEXT NOT NULL,
    Type TEXT,  -- 'Mobile', 'Home', 'Work'
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId)
);
```

### Problem: Komplexa objekt

**DÅLIGT** (Bryter mot 1NF):

```
Orders:
OrderId | CustomerData
--------|--------------------------------
1       | {"name":"Anna","email":"anna@..."}  ← JSON/XML i kolumn!
```

**LÖSNING:**

```sql
CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY,
    Name TEXT,
    Email TEXT
);

CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY,
    CustomerId INTEGER,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);
```

### Sammanfattning 1NF

**Regler:**

- ✓ Ett värde per cell
- ✓ Ingen JSON, XML, komma-separerade listor
- ✓ Inga "Phone1, Phone2, Phone3"-kolumner

**För att uppnå 1NF:**

1. Dela upp multi-värdes kolumner till separata rader/tabeller
2. Dela upp grupperade attribut till separata tabeller
3. Extrahera komplexa objekt till egna tabeller

## Andra normalformen (2NF)

### Definition

En tabell är i 2NF om:

1. **Den är i 1NF**
2. **Alla icke-nyckel-attribut är FULLT funktionellt beroende av HELA primärnyckeln**

**Med andra ord:** Inga partiella beroenden!

### När är 2NF relevant?

Endast när du har en **sammansatt primärnyckel** (composite key). Om primärnyckeln är EN kolumn är tabellen automatiskt i 2NF (förutsatt att den är i 1NF).

### Problem: Partiella beroenden

**DÅLIGT** (Bryter mot 2NF):

```
CourseEnrollments:
StudentId | CourseId | StudentName | CourseName | Grade
----------|------|---------|--------|---
1         | 101      | Anna        | Math       | A
1         | 102      | Anna        | Physics    | B
2         | 101      | Bengt       | Math       | A

Primärnyckel: (StudentId, CourseId)

Beroenden:
StudentId, CourseId → Grade         ← OK (fullt beroende)
StudentId → StudentName              ← PROBLEM! (partiellt beroende)
CourseId → CourseName                ← PROBLEM! (partiellt beroende)
```

**Varför dåligt?**

- StudentName upprepas för varje kurs studenten tar
- CourseName upprepas för varje student som tar kursen
- Om en kurs byter namn måste vi uppdatera ALLA enrollments

**LÖSNING (2NF):**

```sql
-- Dela upp i tre tabeller

CREATE TABLE Students (
    StudentId INTEGER PRIMARY KEY,
    StudentName TEXT NOT NULL
);

CREATE TABLE Courses (
    CourseId INTEGER PRIMARY KEY,
    CourseName TEXT NOT NULL
);

CREATE TABLE Enrollments (
    StudentId INTEGER,
    CourseId INTEGER,
    Grade TEXT,
    PRIMARY KEY (StudentId, CourseId),
    FOREIGN KEY (StudentId) REFERENCES Students(StudentId),
    FOREIGN KEY (CourseId) REFERENCES Courses(CourseId)
);
```

Nu:

- StudentName lagras EN gång per student
- CourseName lagras EN gång per kurs
- Grade beror på BÅDE StudentId OCH CourseId (korrekt!)

### Praktiskt exempel: Orderrader

**DÅLIGT** (Bryter mot 2NF):

```
OrderLines:
OrderId | ProductId | ProductName  | ProductPrice | Quantity | LineTotal
--------|-------|----------|----------|------|------
1       | 101       | Laptop       | 9999         | 2        | 19998
1       | 102       | Mouse        | 299          | 1        | 299
2       | 101       | Laptop       | 9999         | 1        | 9999

Primärnyckel: (OrderId, ProductId)

Beroenden:
OrderId, ProductId → Quantity        ← OK
OrderId, ProductId → LineTotal       ← OK
ProductId → ProductName              ← PROBLEM!
ProductId → ProductPrice             ← PROBLEM!
```

**Varför dåligt?**

- ProductName och ProductPrice upprepas för varje order
- Om produktpriset ändras måste vi uppdatera alla gamla orders (historiskt felaktigt!)

**LÖSNING (2NF):**

```sql
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    ProductName TEXT NOT NULL,
    CurrentPrice REAL NOT NULL
);

CREATE TABLE OrderLines (
    OrderId INTEGER,
    ProductId INTEGER,
    Quantity INTEGER NOT NULL,
    PriceAtOrderTime REAL NOT NULL,  ← Spara historiskt pris!
    LineTotal REAL NOT NULL,
    PRIMARY KEY (OrderId, ProductId),
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
```

**Notera:** Vi sparar `PriceAtOrderTime` i OrderLines för att bevara historik - vad betalade kunden FAKTISKT? Detta är korrekt även om produktpriset ändras senare!

### Sammanfattning 2NF

**Regler:**

- ✓ Är i 1NF
- ✓ Alla icke-nyckel-attribut beror på HELA primärnyckeln

**För att uppnå 2NF:**

1. Identifiera sammansatta primärnycklar
2. Hitta attribut som bara beror på DEL av nyckeln
3. Flytta dessa attribut till separata tabeller

**Snabbtest:** Har tabellen en enkel primärnyckel (en kolumn)? Då är den automatiskt i 2NF!

## Tredje normalformen (3NF)

### Definition

En tabell är i 3NF om:

1. **Den är i 2NF**
2. **Inga transitiva beroenden** (icke-nyckel-attribut får inte bero på andra icke-nyckel-attribut)

### Problem: Transitiva beroenden

**DÅLIGT** (Bryter mot 3NF):

```
Employees:
EmployeeId | Name  | DepartmentId | DepartmentName | DepartmentLocation
-----------|---|----------|------------|---------------
1          | Anna  | 10           | IT             | Building A
2          | Bengt | 10           | IT             | Building A
3          | Cecilia| 20          | HR             | Building B

Primärnyckel: EmployeeId

Beroenden:
EmployeeId → DepartmentId              ← OK
EmployeeId → Name                      ← OK
DepartmentId → DepartmentName          ← PROBLEM! (transitivt)
DepartmentId → DepartmentLocation      ← PROBLEM! (transitivt)

Transitivt beroende:
EmployeeId → DepartmentId → DepartmentName
```

**Varför dåligt?**

- DepartmentName och DepartmentLocation upprepas för varje anställd i avdelningen
- Om avdelningen flyttar måste vi uppdatera ALLA anställda
- Om vi raderar sista anställda i en avdelning förlorar vi avdelningsinformation

**LÖSNING (3NF):**

```sql
CREATE TABLE Departments (
    DepartmentId INTEGER PRIMARY KEY,
    DepartmentName TEXT NOT NULL,
    DepartmentLocation TEXT NOT NULL
);

CREATE TABLE Employees (
    EmployeeId INTEGER PRIMARY KEY,
    Name TEXT NOT NULL,
    DepartmentId INTEGER NOT NULL,
    FOREIGN KEY (DepartmentId) REFERENCES Departments(DepartmentId)
);
```

Nu:

- Avdelningsinformation lagras EN gång
- Anställda refererar bara till DepartmentId
- Uppdatera avdelning = uppdatera EN rad

### Praktiskt exempel: Produkter och kategorier

**DÅLIGT** (Bryter mot 3NF):

```
Products:
ProductId | ProductName | CategoryId | CategoryName | CategoryDescription
----------|---------|--------|----------|----------------
1         | Laptop      | 10         | Electronics  | Tech devices
2         | Mouse       | 10         | Electronics  | Tech devices
3         | Desk        | 20         | Furniture    | Office furniture

Transitivt beroende:
ProductId → CategoryId → CategoryName
ProductId → CategoryId → CategoryDescription
```

**LÖSNING (3NF):**

```sql
CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY,
    CategoryName TEXT NOT NULL,
    CategoryDescription TEXT
);

CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    ProductName TEXT NOT NULL,
    CategoryId INTEGER NOT NULL,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
```

### Praktiskt exempel: Beställningar och leveransstatus

**DÅLIGT** (Bryter mot 3NF):

```
Orders:
OrderId | CustomerId | ShippingStatusCode | ShippingStatusDescription
--------|--------|---------------|---------------------
1       | 101        | SHP               | Shipped
2       | 102        | DLV               | Delivered
3       | 103        | SHP               | Shipped

Transitivt beroende:
OrderId → ShippingStatusCode → ShippingStatusDescription
```

**LÖSNING (3NF):**

```sql
CREATE TABLE ShippingStatuses (
    StatusCode TEXT PRIMARY KEY,
    StatusDescription TEXT NOT NULL
);

INSERT INTO ShippingStatuses VALUES
    ('PND', 'Pending'),
    ('SHP', 'Shipped'),
    ('DLV', 'Delivered'),
    ('RTN', 'Returned');

CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY,
    CustomerId INTEGER NOT NULL,
    ShippingStatusCode TEXT NOT NULL,
    FOREIGN KEY (ShippingStatusCode) REFERENCES ShippingStatuses(StatusCode)
);
```

### Sammanfattning 3NF

**Regler:**

- ✓ Är i 2NF
- ✓ Inga transitiva beroenden

**För att uppnå 3NF:**

1. Identifiera kolumner som beror på andra icke-nyckel-kolumner
2. Flytta dessa till separata tabeller
3. Referera med foreign keys

**Snabbtest:** Fråga för varje kolumn: "Kan jag slå upp detta värde från en annan kolumn i samma tabell?" Om ja → bryter mot 3NF!

## Normaliseringsprocessen steg-för-steg

### Steg 1: Identifiera onormaliserad data

**Exempel: Bokhandel (unnormalized)**

```
Orders:
OrderId | OrderDate | CustomerName | CustomerEmail    | CustomerPhone | ProductName | ProductAuthor | ProductISBN | ProductPrice | Quantity | TotalPrice
--------|-------|----------|-------------|-----------|---------|-----------|---------|----------|------|--------
1       | 2024-01-01| Anna         | anna@example.com| 0701111111    | Database Book| John Smith   | 123456      | 399          | 2        | 798
1       | 2024-01-01| Anna         | anna@example.com| 0701111111    | SQL Guide   | Jane Doe      | 789012      | 299          | 1        | 299
2       | 2024-01-02| Bengt        | bengt@example.com| 0702222222   | Database Book| John Smith   | 123456      | 399          | 1        | 399
```

**Problem identifierade:**

- ❌ Multi-värdes data (flera produkter per order → flera rader)
- ❌ Redundans (kundinfo upprepas)
- ❌ Redundans (produktinfo upprepas)
- ❌ Update anomalies (om Anna byter e-post?)
- ❌ Deletion anomalies (om vi raderar order 2 förlorar vi Bengt)

### Steg 2: Applicera 1NF

**Dela upp i logiska entiteter:**

```
Orders:
OrderId | OrderDate | CustomerName | CustomerEmail     | CustomerPhone | ProductName  | ProductAuthor | ProductISBN | ProductPrice | Quantity
--------|-------|----------|--------------|-----------|----------|-----------|---------|----------|------
1       | 2024-01-01| Anna         | anna@example.com | 0701111111    | Database Book| John Smith    | 123456      | 399          | 2
1       | 2024-01-01| Anna         | anna@example.com | 0701111111    | SQL Guide    | Jane Doe      | 789012      | 299          | 1
2       | 2024-01-02| Bengt        | bengt@example.com| 0702222222    | Database Book| John Smith    | 123456      | 399          | 1
```

✓ Nu i 1NF: Varje cell har atomära värden
✗ Men: Fortfarande mycket redundans

### Steg 3: Applicera 2NF

**Identifiera entiteter och separera:**

```sql
-- Kunder (Customer data upprepas inte längre)
CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    Phone TEXT
);

-- Produkter (Product data upprepas inte längre)
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName TEXT NOT NULL,
    Author TEXT,
    ISBN TEXT UNIQUE,
    Price REAL NOT NULL
);

-- Orders (huvudtabell)
CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderDate TEXT NOT NULL,
    CustomerId INTEGER NOT NULL,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId)
);

-- OrderLines (kopplingstabell för order-produkt many-to-many)
CREATE TABLE OrderLines (
    OrderLineId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    PriceAtOrderTime REAL NOT NULL,  -- Historiskt pris
    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId)
);
```

✓ Nu i 2NF: Ingen redundans för kunder eller produkter
✓ Kundinfo lagras EN gång
✓ Produktinfo lagras EN gång

### Steg 4: Applicera 3NF

**Kolla efter transitiva beroenden:**

I vårt exempel är vi redan i 3NF! Men låt oss säga att vi har:

```sql
Products:
ProductId | ProductName   | Author      | AuthorCountry
----------|-----------|---------|-----------
1         | Database Book | John Smith  | USA
2         | SQL Guide     | Jane Doe    | UK

Transitivt beroende:
ProductId → Author → AuthorCountry
```

**Lösning:**

```sql
CREATE TABLE Authors (
    AuthorId INTEGER PRIMARY KEY AUTOINCREMENT,
    AuthorName TEXT NOT NULL,
    Country TEXT
);

CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductName TEXT NOT NULL,
    AuthorId INTEGER NOT NULL,
    ISBN TEXT UNIQUE,
    Price REAL NOT NULL,
    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId)
);
```

✓ Nu i 3NF: Inga transitiva beroenden

### Steg 5: Verifiera design

**Checklist:**

- ✅ 1NF: Alla värden atomära
- ✅ 2NF: Inga partiella beroenden
- ✅ 3NF: Inga transitiva beroenden
- ✅ Primärnycklar på alla tabeller
- ✅ Foreign keys för relationer
- ✅ Constraints (NOT NULL, UNIQUE)

## Praktiskt exempel: Bokhandel

### Fullständigt normaliserat schema

```sql
-- ====================================
-- CUSTOMERS
-- ====================================
CREATE TABLE Customers (
    CustomerId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT UNIQUE NOT NULL,
    Phone TEXT,
    Address TEXT,
    City TEXT,
    PostalCode TEXT,
    Country TEXT DEFAULT 'Sweden',
    RegisteredDate TEXT DEFAULT (date('now')),

    CHECK (Email LIKE '%@%.%')
);

-- ====================================
-- AUTHORS
-- ====================================
CREATE TABLE Authors (
    AuthorId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    BirthYear INTEGER,
    Country TEXT,
    Biography TEXT
);

-- ====================================
-- PUBLISHERS
-- ====================================
CREATE TABLE Publishers (
    PublisherId INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Country TEXT,
    Website TEXT
);

-- ====================================
-- PRODUCTS (Books)
-- ====================================
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    ISBN TEXT UNIQUE NOT NULL,
    AuthorId INTEGER NOT NULL,
    PublisherId INTEGER NOT NULL,
    PublicationYear INTEGER,
    Pages INTEGER,
    Language TEXT DEFAULT 'Swedish',
    CurrentPrice REAL NOT NULL,
    StockQuantity INTEGER DEFAULT 0,

    FOREIGN KEY (AuthorId) REFERENCES Authors(AuthorId),
    FOREIGN KEY (PublisherId) REFERENCES Publishers(PublisherId),

    CHECK (CurrentPrice >= 0),
    CHECK (StockQuantity >= 0),
    CHECK (Pages > 0)
);

-- ====================================
-- CATEGORIES
-- ====================================
CREATE TABLE Categories (
    CategoryId INTEGER PRIMARY KEY AUTOINCREMENT,
    CategoryName TEXT UNIQUE NOT NULL,
    Description TEXT
);

-- ====================================
-- PRODUCT-CATEGORY (Many-to-Many)
-- ====================================
CREATE TABLE ProductCategories (
    ProductId INTEGER,
    CategoryId INTEGER,
    PRIMARY KEY (ProductId, CategoryId),
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId) ON DELETE CASCADE,
    FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId) ON DELETE CASCADE
);

-- ====================================
-- ORDERS
-- ====================================
CREATE TABLE Orders (
    OrderId INTEGER PRIMARY KEY AUTOINCREMENT,
    CustomerId INTEGER NOT NULL,
    OrderDate TEXT DEFAULT (datetime('now')),
    Status TEXT DEFAULT 'Pending',
    ShippingAddress TEXT,
    ShippingCity TEXT,
    ShippingPostalCode TEXT,
    TotalAmount REAL,

    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),

    CHECK (Status IN ('Pending', 'Confirmed', 'Shipped', 'Delivered', 'Cancelled'))
);

-- ====================================
-- ORDER LINES
-- ====================================
CREATE TABLE OrderLines (
    OrderLineId INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    PriceAtOrderTime REAL NOT NULL,  -- Historiskt pris!
    LineTotal REAL NOT NULL,

    FOREIGN KEY (OrderId) REFERENCES Orders(OrderId) ON DELETE CASCADE,
    FOREIGN KEY (ProductId) REFERENCES Products(ProductId),

    CHECK (Quantity > 0),
    CHECK (PriceAtOrderTime >= 0),
    CHECK (LineTotal >= 0)
);

-- ====================================
-- REVIEWS
-- ====================================
CREATE TABLE Reviews (
    ReviewId INTEGER PRIMARY KEY AUTOINCREMENT,
    ProductId INTEGER NOT NULL,
    CustomerId INTEGER NOT NULL,
    Rating INTEGER NOT NULL,
    ReviewText TEXT,
    ReviewDate TEXT DEFAULT (date('now')),

    FOREIGN KEY (ProductId) REFERENCES Products(ProductId) ON DELETE CASCADE,
    FOREIGN KEY (CustomerId) REFERENCES Customers(CustomerId),

    CHECK (Rating >= 1 AND Rating <= 5),
    UNIQUE (ProductId, CustomerId)  -- En recension per kund per produkt
);

-- ====================================
-- INDEX FÖR PRESTANDA
-- ====================================
CREATE INDEX idx_products_author ON Products(AuthorId);
CREATE INDEX idx_products_publisher ON Products(PublisherId);
CREATE INDEX idx_products_isbn ON Products(ISBN);
CREATE INDEX idx_orders_customer ON Orders(CustomerId);
CREATE INDEX idx_orders_date ON Orders(OrderDate);
CREATE INDEX idx_orderlines_order ON OrderLines(OrderId);
CREATE INDEX idx_orderlines_product ON OrderLines(ProductId);
CREATE INDEX idx_reviews_product ON Reviews(ProductId);
CREATE INDEX idx_reviews_customer ON Reviews(CustomerId);

-- ====================================
-- VYER FÖR VANLIGA QUERIES
-- ====================================
CREATE VIEW ProductsWithDetails AS
SELECT
    p.ProductId,
    p.Title,
    p.ISBN,
    a.Name AS AuthorName,
    pub.Name AS PublisherName,
    p.PublicationYear,
    p.CurrentPrice,
    p.StockQuantity,
    GROUP_CONCAT(c.CategoryName, ', ') AS Categories,
    COALESCE(AVG(r.Rating), 0) AS AverageRating,
    COUNT(r.ReviewId) AS ReviewCount
FROM Products p
JOIN Authors a ON p.AuthorId = a.AuthorId
JOIN Publishers pub ON p.PublisherId = pub.PublisherId
LEFT JOIN ProductCategories pc ON p.ProductId = pc.ProductId
LEFT JOIN Categories c ON pc.CategoryId = c.CategoryId
LEFT JOIN Reviews r ON p.ProductId = r.ProductId
GROUP BY p.ProductId;

CREATE VIEW OrderSummary AS
SELECT
    o.OrderId,
    o.OrderDate,
    c.Name AS CustomerName,
    c.Email AS CustomerEmail,
    o.Status,
    COUNT(ol.OrderLineId) AS ItemCount,
    SUM(ol.LineTotal) AS TotalAmount
FROM Orders o
JOIN Customers c ON o.CustomerId = c.CustomerId
LEFT JOIN OrderLines ol ON o.OrderId = ol.OrderId
GROUP BY o.OrderId;
```

### Fördelar med normaliserad design

**1. Dataintegritet**

```sql
-- Ändra författarens land
UPDATE Authors SET Country = 'Canada' WHERE AuthorId = 1;
-- Alla böcker av författaren visar automatiskt rätt land!
```

**2. Lagringsutrymme**

```
Onormaliserad: 10,000 orderrader × 500 bytes = 5 MB
Normaliserad:
  - Customers: 1,000 × 200 bytes = 200 KB
  - Products: 500 × 150 bytes = 75 KB
  - Orders: 2,000 × 100 bytes = 200 KB
  - OrderLines: 10,000 × 50 bytes = 500 KB
  Total: 975 KB (80% mindre!)
```

**3. Flexibilitet**

```sql
-- Lägg till ny kategori
INSERT INTO Categories (CategoryName, Description)
VALUES ('Science Fiction', 'Futuristic and speculative fiction');

-- Koppla produkt till kategori
INSERT INTO ProductCategories (ProductId, CategoryId)
VALUES (1, (SELECT CategoryId FROM Categories WHERE CategoryName = 'Science Fiction'));
```

## När ska man INTE normalisera

Normalisering är inte alltid rätt svar. Här är scenarier där denormalisering kan vara korrekt.

### 1. Read-Heavy applikationer

**Scenario:** E-handelssida som visar produktlistor (99% reads, 1% writes)

**Problem med full normalisering:**

```sql
-- Hämta produkter med författare, kategori, recensioner
SELECT
    p.Title,
    a.Name AS Author,
    c.CategoryName,
    AVG(r.Rating) AS Rating
FROM Products p
JOIN Authors a ON p.AuthorId = a.AuthorId
JOIN ProductCategories pc ON p.ProductId = pc.ProductId
JOIN Categories c ON pc.CategoryId = c.CategoryId
LEFT JOIN Reviews r ON p.ProductId = r.ProductId
GROUP BY p.ProductId;

-- 4 JOINs = långsamt!
```

**Denormaliserad lösning:**

```sql
-- Lägg till beräknade kolumner
CREATE TABLE Products (
    ProductId INTEGER PRIMARY KEY,
    Title TEXT NOT NULL,
    AuthorName TEXT NOT NULL,  -- Denormaliserat!
    CategoryNames TEXT,         -- Denormaliserat!
    AverageRating REAL,         -- Denormaliserat!
    ReviewCount INTEGER         -- Denormaliserat!
);

-- Nu: enkelt SELECT utan JOINs
SELECT Title, AuthorName, CategoryNames, AverageRating
FROM Products
WHERE CategoryNames LIKE '%Science Fiction%';

-- 100x snabbare!
```

**Trade-off:** Måste uppdatera denormaliserad data när författare/kategorier/recensioner ändras (triggers eller application logic).

### 2. Rapportering och Analytics

**Scenario:** Monthly sales reports

**Problem:** JOINs över miljontals rader är långsamma

**Lösning: Materialiserad vy eller separat rapporteringsdatabas**

```sql
CREATE TABLE MonthlySalesReport (
    ReportId INTEGER PRIMARY KEY,
    Year INTEGER,
    Month INTEGER,
    ProductId INTEGER,
    ProductName TEXT,
    CategoryName TEXT,
    TotalQuantitySold INTEGER,
    TotalRevenue REAL,
    GeneratedAt TEXT
);

-- Uppdatera månadsvis via scheduled job
```

### 3. Historiska poster

**Scenario:** Vi måste bevara exakt hur data såg ut vid en specifik tidpunkt.

**Exempel: Fakturor**

```sql
-- INTE referera till Customers-tabellen!
CREATE TABLE Invoices (
    InvoiceId INTEGER PRIMARY KEY,
    InvoiceDate TEXT,

    -- Spara kunddata direkt (snapshot)
    CustomerName TEXT NOT NULL,
    CustomerAddress TEXT NOT NULL,
    CustomerEmail TEXT NOT NULL,

    -- Om kunden byter namn/adress påverkar det inte gamla fakturor!
);
```

### 4. Prestanda-kritiska queries

**Scenario:** Landing page måste ladda under 100ms

**Lösning: Caching-tabell**

```sql
CREATE TABLE ProductCache (
    ProductId INTEGER PRIMARY KEY,
    FullHTML TEXT,  -- Pre-rendererad HTML!
    LastUpdated TEXT
);

-- Uppdatera när produkt ändras
-- Läs direkt från cache = super snabbt
```

### Sammanfattning: När denormalisera

✓ **Denormalisera när:**

- Read-to-write ratio > 100:1
- Prestanda är kritiskt
- Data är historisk (snapshots)
- Rapportering/analytics
- Kan hantera eventual consistency

✗ **INTE denormalisera när:**

- Data uppdateras ofta
- Stark konsistens krävs
- Flera system delar samma data
- Du inte har monitoring för inkonsistens

## Ytterligare normalformer

De flesta databaser stannar vid 3NF, men det finns fler normalformer.

### Boyce-Codd Normal Form (BCNF)

**Definition:** En strängare version av 3NF. Varje determinant måste vara en candidate key.

**När 3NF inte räcker:**

```
CourseSchedule:
StudentId | CourseId | Instructor
----------|------|--------
1         | Math101  | Dr. Smith
2         | Math101  | Dr. Smith
1         | Phys101  | Dr. Jones

Beroenden:
StudentId, CourseId → Instructor  ← OK för 3NF
Instructor → CourseId             ← PROBLEM för BCNF!
```

En instruktör undervisar bara en kurs, så Instructor bestämmer CourseId. Men Instructor är inte en candidate key!

**BCNF lösning:**

```sql
CREATE TABLE Instructors (
    Instructor TEXT PRIMARY KEY,
    CourseId INTEGER NOT NULL
);

CREATE TABLE Enrollments (
    StudentId INTEGER,
    Instructor TEXT,
    PRIMARY KEY (StudentId, Instructor),
    FOREIGN KEY (Instructor) REFERENCES Instructors(Instructor)
);
```

### Fourth Normal Form (4NF)

**Definition:** En tabell är i 4NF om:

1. **Den är i BCNF**
2. **Den har inga multi-valued dependencies**

#### Vad är Multi-Valued Dependencies?

Ett multi-valued dependency (MVD) uppstår när ett attribut är oberoende associerat med två eller flera andra attribut.

**Notation:** `A →→ B` (läs: "A multi-determines B")

**Problem: Independent Multi-Valued Dependencies**

Föreställ dig att vi vill lagra information om lärare, deras kurser, och deras telefonnummer:

**DÅLIGT** (Bryter mot 4NF):

```
Teacher_Info:
TeacherId | TeacherName | Course      | PhoneNumber
----------|---------|---------|---------
1         | Dr. Smith   | Math        | 070-1111111
1         | Dr. Smith   | Math        | 070-2222222  ← Samma kurs, annat nummer
1         | Dr. Smith   | Physics     | 070-1111111  ← Annan kurs, samma nummer
1         | Dr. Smith   | Physics     | 070-2222222  ← Kartesisk produkt!
2         | Dr. Jones   | Chemistry   | 070-3333333
2         | Dr. Jones   | Biology     | 070-3333333

Beroenden:
TeacherId →→ Course        (en lärare kan ha FLERA kurser)
TeacherId →→ PhoneNumber   (en lärare kan ha FLERA nummer)
```

**Problem:**

- Course och PhoneNumber är **oberoende** av varandra
- Dr. Smith har 2 kurser och 2 telefonnummer → 2×2 = **4 rader** (kartesisk produkt!)
- Om Dr. Smith får ett tredje telefonnummer måste vi lägga till 2 rader (en för varje kurs)
- Enorm redundans och anomalier

**Varför dåligt konkret:**

```sql
-- Dr. Smith får nytt nummer
-- Måste lägga till ETT nummer för VARJE kurs!
INSERT INTO Teacher_Info VALUES (1, 'Dr. Smith', 'Math', '070-4444444');
INSERT INTO Teacher_Info VALUES (1, 'Dr. Smith', 'Physics', '070-4444444');

-- Dr. Smith slutar undervisa Math
-- Måste radera flera rader (en för varje telefonnummer!)
DELETE FROM Teacher_Info WHERE TeacherId = 1 AND Course = 'Math';
-- Nu förlorade vi ALLA telefonnummer för Math-kursen!
```

**LÖSNING (4NF):**

Dela upp multi-valued attributes i separata tabeller:

```sql
-- Lärare bas-information
CREATE TABLE Teachers (
    TeacherId INTEGER PRIMARY KEY,
    TeacherName TEXT NOT NULL
);

-- Kurser (oberoende)
CREATE TABLE TeacherCourses (
    TeacherId INTEGER,
    Course TEXT,
    PRIMARY KEY (TeacherId, Course),
    FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId)
);

-- Telefonnummer (oberoende)
CREATE TABLE TeacherPhones (
    TeacherId INTEGER,
    PhoneNumber TEXT,
    PRIMARY KEY (TeacherId, PhoneNumber),
    FOREIGN KEY (TeacherId) REFERENCES Teachers(TeacherId)
);
```

**Resultat:**

```
Teachers:
TeacherId | TeacherName
----------|--------
1         | Dr. Smith
2         | Dr. Jones

TeacherCourses:
TeacherId | Course
----------|------
1         | Math
1         | Physics
2         | Chemistry
2         | Biology

TeacherPhones:
TeacherId | PhoneNumber
----------|---------
1         | 070-1111111
1         | 070-2222222
2         | 070-3333333
```

Nu:

- 2 kurser + 2 nummer = **4 rader totalt** (inte 4 rader i en tabell!)
- Lägg till nummer: **1 rad**
- Ta bort kurs: **1 rad**
- Ingen kartesisk produktexplosion!

#### Praktiskt exempel: Anställda och kompetenser

**DÅLIGT** (Bryter mot 4NF):

```
Employee_Skills:
EmployeeId | EmployeeName | Skill        | Certification
-----------|----------|----------|-----------
1          | Anna         | C#           | Azure Dev
1          | Anna         | C#           | SQL Expert    ← Samma skill, annan cert
1          | Anna         | Python       | Azure Dev     ← Annan skill, samma cert
1          | Anna         | Python       | SQL Expert    ← Kartesisk produkt!
2          | Bengt        | Java         | AWS Dev

MVD:
EmployeeId →→ Skill
EmployeeId →→ Certification
(Skills och Certifications är oberoende!)
```

**Problem:**

- Anna har 2 skills och 2 certifieringar → 4 rader
- Lägg till en tredje skill → måste lägga till 2 rader (en för varje certification)

**LÖSNING (4NF):**

```sql
CREATE TABLE Employees (
    EmployeeId INTEGER PRIMARY KEY,
    EmployeeName TEXT NOT NULL
);

CREATE TABLE EmployeeSkills (
    EmployeeId INTEGER,
    Skill TEXT,
    PRIMARY KEY (EmployeeId, Skill),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);

CREATE TABLE EmployeeCertifications (
    EmployeeId INTEGER,
    Certification TEXT,
    PRIMARY KEY (EmployeeId, Certification),
    FOREIGN KEY (EmployeeId) REFERENCES Employees(EmployeeId)
);
```

#### Hur identifiera 4NF-brott?

**Frågor att ställa:**

1. Har tabellen två eller fler kolumner med multi-valued data?
2. Är dessa multi-valued kolumner **oberoende** av varandra?
3. Ser du en "kartesisk produkt" i data (många upprepningar)?

**Exempel på berättande tecken:**

```
-- Om du ser detta mönster:
A | B1 | C1
A | B1 | C2
A | B2 | C1
A | B2 | C2  ← Varje kombination av B och C för samma A!

-- Då har du troligen MVD: A →→ B och A →→ C
```

#### C# Class Design - Arraydilemmat! 🎯

**Viktigt för C#-utvecklare:** När du designar klasser med arrayer/listor är det lätt att skapa 4NF-brott!

**DÅLIG C# design:**

```csharp
public class Teacher {
    public int TeacherId { get; set; }
    public string Name { get; set; }
    public List<string> Courses { get; set; }        // ← Multi-valued!
    public List<string> PhoneNumbers { get; set; }   // ← Multi-valued!
}

// Vad händer i databasen?
// Hur lagrar vi två OBEROENDE listor?

// Alternativ 1: Serialisera till JSON (DÅLIGT!)
TeacherId | Name      | Courses              | PhoneNumbers
----------|-------|-----------------|---------------
1         | Dr. Smith | ["Math","Physics"]   | ["070-1111111","070-2222222"]

// Alternativ 2: Kartesisk produkt (4NF-brott!)
TeacherId | Name      | Course  | PhoneNumber
----------|-------|-----|---------
1         | Dr. Smith | Math    | 070-1111111
1         | Dr. Smith | Math    | 070-2222222
1         | Dr. Smith | Physics | 070-1111111
1         | Dr. Smith | Physics | 070-2222222
```

**BRA C# design (normaliserad):**

```csharp
public class Teacher {
    public int TeacherId { get; set; }
    public string Name { get; set; }
    // Inga arrayer/listor av oberoende data!
}

public class TeacherCourse {
    public int TeacherId { get; set; }
    public string Course { get; set; }
}

public class TeacherPhone {
    public int TeacherId { get; set; }
    public string PhoneNumber { get; set; }
}

// SQL tabeller:
CREATE TABLE Teachers (TeacherId, Name);
CREATE TABLE TeacherCourses (TeacherId, Course);
CREATE TABLE TeacherPhones (TeacherId, PhoneNumber);
```

**Tumregel:** Om din C#-klass har **två eller fler listor** av **oberoende data** → du behöver troligen separata tabeller i databasen!

#### När är 4NF viktig?

**Använd 4NF när:**

- ✓ Du har oberoende multi-valued attribut
- ✓ Data växer (kartesisk produkt blir dyr)
- ✓ Du uppdaterar en dimension ofta (t.ex. lägga till telefonnummer)

**4NF är INTE viktig när:**

- ✗ Multi-valued attribut är **beroende** (då är 3NF tillräckligt)
- ✗ Små dataset (< 1000 rader)
- ✗ Read-only data

#### Sammanfattning 4NF

**Regel:**

- ✓ Är i BCNF
- ✓ Inga oberoende multi-valued dependencies

**För att uppnå 4NF:**

1. Identifiera oberoende multi-valued attribut
2. Dela upp i separata tabeller
3. Behåll foreign keys till bas-tabellen

**Snabbtest:** Ser du en "kartesisk produkt" i dina rader? (Samma primärnyckel med alla kombinationer av värden) → Troligen 4NF-brott!

### Fifth Normal Form (5NF)

**Definition:** En tabell är i 5NF (också kallad **Project-Join Normal Form, PJNF**) om:

1. **Den är i 4NF**
2. **Den kan inte delas upp i mindre tabeller utan att förlora information**

5NF handlar om **join dependencies** - situationer där en tabell kan rekonstrueras genom att JOIN:a flera mindre tabeller, och denna uppdelning eliminerar redundans.

#### Vad är Join Dependencies?

En join dependency uppstår när en tabell kan delas upp i flera tabeller som, när de JOIN:as, ger tillbaka exakt samma information utan redundans eller förlust.

**Problem: Komplexa ternära relationer**

**DÅLIGT** (Bryter mot 5NF):

```
Agent_Company_Product:
AgentId | CompanyId | ProductId
--------|-------|------
1       | 100       | 500       ← Agent 1 kan sälja Product 500 för Company 100
1       | 100       | 501       ← Agent 1 kan sälja Product 501 för Company 100
1       | 101       | 500       ← Agent 1 kan sälja Product 500 för Company 101
2       | 100       | 500
2       | 101       | 501

Affärsregler:
- En agent kan representera flera företag
- Ett företag kan sälja flera produkter
- En agent kan sälja en produkt OM OCH ENDAST OM:
  1. Agenten representerar företaget
  2. Företaget säljer produkten
```

**Varför dåligt?**

Om vi vill lägga till att Agent 1 kan sälja Product 502 för Company 100, och Company 100 redan säljer Product 502, måste vi lägga till en rad. Men vad händer om vi har 10 agenter för Company 100? Vi måste lägga till 10 rader!

**Redundans:**

```
Om:
- Agent 1 representerar Company 100
- Company 100 säljer Products [500, 501, 502]

Då måste vi ha:
1, 100, 500
1, 100, 501
1, 100, 502  ← Redundant! Denna kombination är implicit given av de andra relationerna
```

**LÖSNING (5NF):**

Dela upp i tre binära relationer:

```sql
-- Vilka agenter representerar vilka företag?
CREATE TABLE AgentCompanies (
    AgentId INTEGER,
    CompanyId INTEGER,
    PRIMARY KEY (AgentId, CompanyId)
);

-- Vilka företag säljer vilka produkter?
CREATE TABLE CompanyProducts (
    CompanyId INTEGER,
    ProductId INTEGER,
    PRIMARY KEY (CompanyId, ProductId)
);

-- Vilka agenter kan sälja vilka produkter?
CREATE TABLE AgentProducts (
    AgentId INTEGER,
    ProductId INTEGER,
    PRIMARY KEY (AgentId, ProductId)
);
```

**Resultat:**

```
AgentCompanies:          CompanyProducts:          AgentProducts:
AgentId | CompanyId      CompanyId | ProductId    AgentId | ProductId
--------|----        ----------|------    --------|------
1       | 100            100       | 500          1       | 500
1       | 101            100       | 501          1       | 501
2       | 100            100       | 502          2       | 500
2       | 101            101       | 500          2       | 501
                         101       | 501
```

**För att ta reda på "Kan Agent 1 sälja Product 500 för Company 100?":**

```sql
SELECT ac.AgentId, cp.CompanyId, ap.ProductId
FROM AgentCompanies ac
JOIN CompanyProducts cp ON ac.CompanyId = cp.CompanyId
JOIN AgentProducts ap ON ac.AgentId = ap.AgentId AND cp.ProductId = ap.ProductId
WHERE ac.AgentId = 1 AND cp.CompanyId = 100 AND ap.ProductId = 500;
```

**Fördelar:**

- Lägg till agent till företag: **1 rad** (i AgentCompanies)
- Lägg till produkt till företag: **1 rad** (i CompanyProducts)
- Lägg till produkt till agent: **1 rad** (i AgentProducts)
- Ingen redundans!

#### Praktiskt exempel: Kurser, Lärare, Böcker

**DÅLIGT** (Bryter mot 5NF):

```
Course_Teacher_Book:
CourseId | TeacherId | BookId
---------|-------|----
CS101    | 1         | 500    ← CS101 taught by Teacher 1 using Book 500
CS101    | 1         | 501    ← CS101 taught by Teacher 1 using Book 501
CS101    | 2         | 500    ← CS101 taught by Teacher 2 using Book 500
CS101    | 2         | 501    ← CS101 taught by Teacher 2 using Book 501

Affärsregler:
- En kurs kan undervisas av flera lärare
- En kurs använder flera böcker
- En lärare kan använda flera böcker
- Men: Vilka böcker som används beror på KURSEN, inte läraren!
```

**Problem:**

Om CS101 lägger till Book 502, och vi har 5 lärare som undervisar CS101, måste vi lägga till 5 rader!

**LÖSNING (5NF):**

```sql
-- Vilka lärare undervisar vilka kurser?
CREATE TABLE CourseTeachers (
    CourseId TEXT,
    TeacherId INTEGER,
    PRIMARY KEY (CourseId, TeacherId)
);

-- Vilka böcker används i vilka kurser?
CREATE TABLE CourseBooks (
    CourseId TEXT,
    BookId INTEGER,
    PRIMARY KEY (CourseId, BookId)
);

-- Vilka lärare använder vilka böcker? (om relevant)
CREATE TABLE TeacherBooks (
    TeacherId INTEGER,
    BookId INTEGER,
    PRIMARY KEY (TeacherId, BookId)
);
```

**Data:**

```
CourseTeachers:           CourseBooks:
CourseId | TeacherId      CourseId | BookId
---------|----        ---------|----
CS101    | 1              CS101    | 500
CS101    | 2              CS101    | 501
```

**För att ta reda på "Vilka böcker använder Teacher 1 för CS101?":**

```sql
SELECT ct.TeacherId, cb.BookId
FROM CourseTeachers ct
JOIN CourseBooks cb ON ct.CourseId = cb.CourseId
WHERE ct.TeacherId = 1 AND ct.CourseId = 'CS101';
```

#### Hur identifiera 5NF-brott?

**Frågor att ställa:**

1. Har du en tabell med 3+ kolumner som alla är i primärnyckeln?
2. Kan informationen rekonstrueras genom att JOIN:a mindre tabeller?
3. Ser du redundans när du lägger till ny data i en dimension?

**Berättande tecken:**

```
-- Om du ser detta mönster i en ternär relation:
A1 | B1 | C1
A1 | B1 | C2
A1 | B2 | C1
A1 | B2 | C2  ← Alla kombinationer för samma A!

-- OCH affärsregeln är: "A relaterar till B, A relaterar till C, B relaterar till C"
-- DÅ har du troligen join dependency → 5NF-brott
```

#### När är 5NF viktig?

**Använd 5NF när:**

- ✓ Du har komplexa ternära (eller högre) relationer
- ✓ Relationerna kan delas upp utan informationsförlust
- ✓ Du ser "kombinationsexplosion" när du lägger till data

**5NF är INTE viktig när:**

- ✗ Relationen är fundamentalt ternär (kan inte delas upp utan att förlora mening)
- ✗ Dataset är litet
- ✗ Prestanda är kritiskt (JOIN:s kan bli dyra)

#### Praktiskt exempel: Leverantörer, Delar, Projekt

**Klassiskt 5NF-exempel:**

```
Supplier_Part_Project:
SupplierId | PartId | ProjectId
-----------|----|------
S1         | P1     | J1        ← Supplier S1 levererar Part P1 till Project J1
S1         | P2     | J1
S2         | P1     | J2

Affärsregler:
- Om Supplier S1 levererar Part P1
- OCH Part P1 används i Project J1
- OCH Supplier S1 jobbar med Project J1
- DÅ: S1 levererar P1 till J1 (implicit!)
```

**LÖSNING (5NF):**

```sql
CREATE TABLE SupplierParts (
    SupplierId TEXT,
    PartId TEXT,
    PRIMARY KEY (SupplierId, PartId)
);

CREATE TABLE PartProjects (
    PartId TEXT,
    ProjectId TEXT,
    PRIMARY KEY (PartId, ProjectId)
);

CREATE TABLE SupplierProjects (
    SupplierId TEXT,
    ProjectId TEXT,
    PRIMARY KEY (SupplierId, ProjectId)
);
```

**Rekonstruera original (lossless join):**

```sql
SELECT sp.SupplierId, pp.PartId, spr.ProjectId
FROM SupplierParts sp
JOIN PartProjects pp ON sp.PartId = pp.PartId
JOIN SupplierProjects spr ON sp.SupplierId = spr.SupplierId
    AND pp.ProjectId = spr.ProjectId;
```

#### Sammanfattning 5NF

**Regel:**

- ✓ Är i 4NF
- ✓ Kan inte delas upp i mindre tabeller utan informationsförlust
- ✓ Alla join dependencies är implicerade av candidate keys

**För att uppnå 5NF:**

1. Identifiera komplexa ternära (eller högre) relationer
2. Analysera om relationen kan delas upp i binära relationer
3. Verifiera att lossless join fungerar (ingen informationsförlust)
4. Dela upp i mindre tabeller

**Snabbtest:** Har du en "all-key" tabell (alla kolumner i primärnyckeln) med 3+ kolumner? Kan du dela upp den i mindre tabeller och JOIN:a tillbaka? → Troligen 5NF-möjlighet!

#### Varning: Överdriven normalisering

**5NF kan vara för mycket:**

```sql
-- 5NF: 3 tabeller, 3 JOINs för varje query
SELECT sp.SupplierId, pp.PartId, spr.ProjectId
FROM SupplierParts sp
JOIN PartProjects pp ON sp.PartId = pp.PartId
JOIN SupplierProjects spr ON sp.SupplierId = spr.SupplierId
    AND pp.ProjectId = spr.ProjectId;

-- 4NF/3NF: 1 tabell, 0 JOINs
SELECT SupplierId, PartId, ProjectId
FROM SupplierPartProject;
```

**När INTE använda 5NF:**

- Queries blir för komplexa (3+ JOINs)
- Prestanda är kritiskt
- Data ändras sällan
- Utvecklare har svårt att förstå designen

**Praktisk rekommendation:**

- **Fokusera på 3NF** för de flesta applikationer
- **Använd BCNF** vid behov för strängare integritet
- **Använd 4NF** när du har oberoende multi-valued dependencies (t.ex. listor i C#-klasser)
- **Använd 5NF** ENDAST när:
  - Du har komplexa ternära relationer
  - Redundansen är betydande
  - Prestanda är inte kritiskt
  - Teamet förstår designen

**I praktiken:** De flesta produktionsdatabaser stannar vid 3NF eller BCNF. 4NF är användbart för specifika fall. 5NF är sällan nödvändigt och kan göra systemet svårare att förstå och långsammare att query:a.

## Externa resurser

### Interaktiva tutorials

1. **SQLZoo Normalization**

   - URL: https://sqlzoo.net/wiki/Data_normalization
   - ✓ Interaktiva övningar med direktfeedback
   - ✓ Öva på att identifiera normalformer
   - ✓ Praktiska exempel med SQL-queries
   - **Rekommenderas starkt** för hands-on lärande

2. **W3Schools Database Normalization**
   - URL: https://www.w3schools.in/dbms/database-normalization/
   - ✓ Enkel förklaring med tydliga exempel
   - ✓ Bra för snabb översikt
   - ✓ Täcker 1NF, 2NF, 3NF, BCNF
   - Perfekt för nybörjare

### Fördjupning

1. **Database System Concepts** (Silberschatz, Korth, Sudarshan)

   - Kapitel 7: Relational Database Design
   - Akademisk standardreferens
   - Djupdykning i normaliseringsteori

2. **Mike Chapple's Normalization Guide**

   - URL: https://www.thoughtco.com/database-normalization-basics-1019735
   - Praktisk guide med verkliga exempel

3. **TutorialsPoint DBMS Normalization**
   - URL: https://www.tutorialspoint.com/dbms/database_normalization.htm
   - Steg-för-steg genomgång
   - Många exempel och övningar

### Video-tutorials

1. **Lucid Software: Database Normalization**

   - YouTube: "Database Normalization - 1NF, 2NF, 3NF, BCNF, 4NF and 5NF"
   - Visuell förklaring med diagram
   - ~15 minuter, täcker alla normalformer

2. **freeCodeCamp: Database Design Course**
   - YouTube: Full kurs om databasdesign
   - Inkluderar normalisering i kontext
   - ~4 timmar

### Verktyg för att öva

1. **dbdiagram.io**

   - URL: https://dbdiagram.io/home
   - Öva på att designa normaliserade scheman
   - Generera SQL direkt från diagram

2. **SQLFiddle**
   - URL: http://sqlfiddle.com/
   - Testa normaliserade vs onormaliserade design
   - Jämför prestanda

## Slutsats

Normalisering är en av de viktigaste färdigheterna inom databasdesign. Genom att följa normaliseringsreglerna skapar vi databaser som är:

- **Effektiva** - Minimal redundans = mindre lagring
- **Konsekventa** - Uppdatera på ett ställe = ingen inkonsistens
- **Skalbara** - Växer utan omstrukturering
- **Underhållbara** - Tydlig struktur = lättare att förstå

**Processen:**

1. **1NF** - Atomära värden, inga listor
2. **2NF** - Eliminera partiella beroenden
3. **3NF** - Eliminera transitiva beroenden

**Best practices:**

- Normalisera först, denormalisera endast när prestanda kräver det
- Dokumentera varför du denormaliserar
- Använd triggers eller application logic för att hålla denormaliserad data synkad
- Testa båda approach med realistisk data

**Kom ihåg:** Normalisering är ett verktyg, inte ett mål. Målet är en databas som fungerar bra för ditt specifika användningsfall!

## TL;DR

**Normalisering = Organisera data effektivt**

**Problem med dålig design:**

- Redundans (samma data upprepas)
- Anomalier (konstiga bieffekter vid CRUD)
- Slöseri med lagring
- Långsamma uppdateringar

**Lösning: Normalformer**

**1NF:**

- ✓ Ett värde per cell
- ✗ Inga komma-separerade listor
- ✗ Inga "Phone1, Phone2, Phone3"

**2NF:**

- ✓ Är i 1NF
- ✓ Alla attribut beror på HELA primärnyckeln
- ✗ Inga partiella beroenden

**3NF:**

- ✓ Är i 2NF
- ✓ Inga transitiva beroenden
- ✗ Icke-nyckel-attribut får inte bero på andra icke-nyckel-attribut

**BCNF:**

- ✓ Är i 3NF
- ✓ Varje determinant är en candidate key
- ✗ Strängare version av 3NF

**4NF:**

- ✓ Är i BCNF
- ✓ Inga oberoende multi-valued dependencies
- ✗ Inga kartesiska produkter av oberoende listor
- ⚠️ **Viktigt för C#:** Två oberoende listor i en klass → två tabeller i databasen!

**5NF:**

- ✓ Är i 4NF
- ✓ Kan inte delas upp utan informationsförlust
- ✗ Inga join dependencies
- ⚠️ Sällan behövs, komplex för ternära relationer

**Process:**

1. Identifiera entiteter
2. Dela upp multi-värdes attribut (1NF)
3. Eliminera partiella beroenden (2NF)
4. Eliminera transitiva beroenden (3NF)
5. Överväg BCNF för strängare integritet
6. Överväg 4NF om du har oberoende listor (C# class design!)
7. Överväg 5NF endast för komplexa ternära relationer

**När INTE normalisera:**

- Read-heavy applikationer (99% reads)
- Prestanda-kritiska queries
- Historiska snapshots
- Rapportering/analytics

**Praktiska rekommendationer:**

- **De flesta databaser:** 3NF är tillräckligt
- **Strängare integritet:** BCNF
- **C# klasser med listor:** Tänk 4NF!
- **Komplexa relationer:** 5NF (sällan)

**Externa resurser:**

- https://sqlzoo.net/wiki/Data_normalization
- https://www.w3schools.in/dbms/database-normalization/

**Tumregel:** Normalisera först till 3NF, denormalisera endast när mätbar prestanda kräver det! 📊

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
