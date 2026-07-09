---
marp: true
theme: default
class: invert
paginate: true
---

# UML och ERD för Databasdesign

**Kurs:** Databashantering och -design
**Modul:** 07 — UML och databasdesign

---

## Vad ska vi lära oss idag?

- **UML** — Unified Modeling Language, klassdiagram för databaser
- **ERD** — Entity Relationship Diagram, entitetsmodellering
- **Relationer** — 1:1, 1:N, N:M
- **Från ERD till SQL** — omvandla modell till tabeller

---

## UML vs ERD

| Aspekt | UML Klassdiagram | ERD (Entity Relationship) |
|--------|-----------------|--------------------------|
| Ursprung | Programvaruutveckling | Databasdesign |
| Enhet | Klass | Entitet |
| Relationer | Association, arv | 1:1, 1:N, N:M |
| Syfte | Beskriva systemets struktur | Modellera data och relationer |

**Båda** används för att visualisera datastrukturer — UML är bredare, ERD är specialiserat för databaser.

---

## Entiteter och Attribut

En **entitet** = något vi vill lagra data om (en "tabell" i databasen).

```
┌──────────────────┐
│     Customer      │
├──────────────────┤
│ CustomerID (PK)   │
│ FirstName         │
│ LastName          │
│ Email             │
│ Phone             │
└──────────────────┘
```

- **PK** = Primary Key (unik identifierare)
- Varje attribut = en kolumn i tabellen

---

## Relationer — 1:1 (En-till-en)

En post i Tabell A har **exakt en** matchande post i Tabell B.

```
┌──────────┐       ┌──────────────┐
│   User   │ 1───1 │   Profile    │
├──────────┤       ├──────────────┤
│ UserID   │       │ ProfileID    │
│ Username │       │ UserID (FK)  │
│ Email    │       │ Bio          │
└──────────┘       │ Avatar       │
                   └──────────────┘
```

**SQL:** FK med UNIQUE-constraint i Profile-tabellen.

---

## Relationer — 1:N (En-till-många)

En post i Tabell A har **många** poster i Tabell B.

```
┌──────────┐       ┌──────────────┐
│ Customer │ 1───N │    Order     │
├──────────┤       ├──────────────┤
│ CustID   │       │ OrderID      │
│ Name     │       │ CustID (FK)  │
└──────────┘       │ OrderDate    │
                   │ Total        │
                   └──────────────┘
```

**SQL:** FK i Order-tabellen som pekar på Customer.

---

## Relationer — N:M (Många-till-många)

En post i Tabell A har **många** i Tabell B — och tvärtom. Kräver en **kopplingstabell**.

```
┌──────────┐       ┌──────────────────┐       ┌──────────┐
│ Student  │       │ StudentCourse     │       │  Course  │
├──────────┤       ├──────────────────┤       ├──────────┤
│ StuID    │1───N  │ StudentID (FK)   │N───1  │ CourseID │
│ Name     │       │ CourseID (FK)    │       │ Title    │
└──────────┘       └──────────────────┘       └──────────┘
```

**SQL:** Kopplingstabellen har två FK:ar — en till varje tabell.

---

## Arv i UML — Entity Hierarchy

```
┌──────────────┐
│   Animal     │  ← Abstrakt basklass
├──────────────┤
│ AnimalID     │
│ Name         │
│ Age          │
└──────────────┘
       △
       │
┌──────┴──────┐
│             │
┌─────────┐  ┌─────────┐
│  Dog    │  │  Cat    │
├─────────┤  ├─────────┤
│ Breed   │  │ Indoor  │
│ BarkLvl │  │ ClawStr │
└─────────┘  └─────────┘
```

**Mappningsstrategier:** Table-per-hierarchy (TPH), Table-per-type (TPT), Table-per-concrete (TPC)

---

## Från ERD till SQL

ERD → Relationsschema → SQL

```sql
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    FirstName VARCHAR(50) NOT NULL,
    LastName VARCHAR(50) NOT NULL,
    Email VARCHAR(100) UNIQUE
);

CREATE TABLE Order (
    OrderID INT PRIMARY KEY AUTO_INCREMENT,
    CustomerID INT NOT NULL,
    OrderDate DATE NOT NULL,
    FOREIGN KEY (CustomerID) REFERENCES Customer(CustomerID)
);
```

---

## Normalisering — kort repetition

| Nivå | Krav |
|------|------|
| **1NF** | Atomära värden, ingen repetition |
| **2NF** | Uppfyller 1NF + alla icke-nyckel-attribut beror på HELA primärnyckeln |
| **3NF** | Uppfyller 2NF + inga transitiva beroenden |

UML/ERD hjälper dig att nå 3NF genom att visualisera relationerna.

---

## Praktiskt exempel: Bibliotek

```
┌──────────┐       ┌──────────────┐       ┌──────────┐
│  Member  │1───N  │    Loan      │N───1  │   Book   │
├──────────┤       ├──────────────┤       ├──────────┤
│ MemberID │       │ LoanID       │       │ BookID   │
│ Name     │       │ MemberID(FK) │       │ Title    │
│ Email    │       │ BookID(FK)   │       │ ISBN     │
└──────────┘       │ LoanDate     │       │ AuthorID │
                   │ ReturnDate   │       └──────────┘
                   └──────────────┘
```

Member lånar böcker (1:N). Book skriven av Author (N:1→Author-tabell).

---

## Verktyg för UML/ERD

- **draw.io** / diagrams.net — gratis, online
- **MySQL Workbench** — Reverse engineer → ERD
- **Lucidchart** — professionellt, samarbete
- **PlantUML** — kodbaserad UML (`.puml`-filer)
- **DBDiagram.io** — snabba ERD via text

---

## Sammanfattning

- ✅ UML klassdiagram = systemets struktur
- ✅ ERD = databasens entiteter och relationer
- ✅ Relationer: 1:1, 1:N, N:M
- ✅ N:M kräver kopplingstabell
- ✅ ERD → SQL är en rak översättning
- ➡️ Nästa: DDL och DML — skapa och manipulera data

---
