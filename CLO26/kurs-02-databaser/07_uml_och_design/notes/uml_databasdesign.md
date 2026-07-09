# UML för Databasdesign

## Vad är UML?

UML (Unified Modeling Language) är ett standardiserat modelleringsspråk för att visualisera, specificera, konstruera och dokumentera system. För databasdesign använder vi främst **klassdiagram** för att beskriva entiteter och deras relationer — precis som ERD (Entity Relationship Diagrams) men med fler uttrycksmöjligheter.

## Klassdiagram för Databaser

Ett klassdiagram visar:
- **Entiteter/klasser** — vad vi vill lagra data om (Customer, Product, Order)
- **Attribut** — vad varje entitet har (Name, Price, Email)
- **Relationer** — hur entiteter hänger ihop (Customer → Order)
- **Multiplicitet** — 1:1, 1:N, N:M
- **Arv** — är-hierarkier (Dog extends Animal)

### Multiplicitet (kardinalitet)

| Notation | Betydelse | Exempel |
|----------|-----------|---------|
| `1` | Exakt en | En Customer har en Profile |
| `0..1` | Noll eller en | En Employee har 0 eller 1 Manager |
| `*` | Noll till många | En Customer har 0 till många Orders |
| `1..*` | En till många | En Order måste ha minst 1 OrderItem |
| `0..*` | Noll till många | Samma som `*` |

### Relationssymboler

| Symbol | Betydelse |
|--------|-----------|
| `———` | Association (1:1 eller 1:N) |
| `—◇—` | Aggregation (del-helhet, svag koppling) |
| `—◆—` | Composition (del-helhet, stark koppling) |
| `—▷—` | Arv (extends) |
| `—•—` | Navigerbarhet |

## Exempel: Bibliotekssystem

```
┌──────────┐       ┌──────────────┐       ┌──────────┐
│  Member  │1───N  │    Loan      │N───1  │   Book   │
├──────────┤       ├──────────────┤       ├──────────┤
│ -ID: int │       │ -ID: int     │       │ -ID: int │
│ -Name    │       │ -MemberID    │       │ -Title   │
│ -Email   │       │ -BookID      │       │ -ISBN    │
│ +Borrow()│       │ -LoanDate    │       │ +GetInfo │
└──────────┘       │ -ReturnDate  │       └──────────┘
                   └──────────────┘
```

- `-` = private (bara inom klassen)
- `+` = public (synlig utåt)
- Relationen Member → Loan = 1:N (en medlem kan ha många lån)
- Relationen Loan → Book = N:1 (många lån kan referera samma bok)

## Från UML till SQL

Varje klass = en tabell. Varje attribut = en kolumn. Varje association = FOREIGN KEY.

**Klass: Customer**
```sql
CREATE TABLE Customer (
    CustomerID INT PRIMARY KEY AUTO_INCREMENT,
    Name VARCHAR(100) NOT NULL,
    Email VARCHAR(100) UNIQUE NOT NULL
);
```

**Association (1:N):** Customer har många Orders → FK i Order
```sql
ALTER TABLE Order ADD FOREIGN KEY (CustomerID)
    REFERENCES Customer(CustomerID);
```

**Association (N:M):** Student ←→ Course → kopplingstabell
```sql
CREATE TABLE StudentCourse (
    StudentID INT,
    CourseID INT,
    PRIMARY KEY (StudentID, CourseID),
    FOREIGN KEY (StudentID) REFERENCES Student(StudentID),
    FOREIGN KEY (CourseID) REFERENCES Course(CourseID)
);
```

## Arv i UML → SQL

Vid arv (t.ex. Dog extends Animal) finns tre strategier:

1. **TPH (Table-per-Hierarchy):** En tabell för ALLA klasser, med en `Discriminator`-kolumn. Snabbast, men kolumner blir NULL för subklasser som inte har dem.

2. **TPT (Table-per-Type):** En tabell per klass. Bas-attribut i Animal, utökade attribut i Dog. Kräver JOIN. Renast design.

3. **TPC (Table-per-Concrete):** En tabell per konkret klass. Dog får alla Animal-kolumner + egna. Duplicering av basattribut.

EF Core defaultar TPH — ofta rätt val för de flesta applikationer.

## Verktyg

| Verktyg | Typ | Pris |
|---------|-----|------|
| draw.io | Online/offline | Gratis |
| PlantUML | Kod-baserad | Gratis |
| MySQL Workbench | EER-diagram | Gratis |
| Lucidchart | Online | Freemium |
| dbdiagram.io | Online ERD | Freemium |
| Visual Studio | Class Designer | Ingår i VS |

## Länkar

- [UML 2.5 Specification](https://www.omg.org/spec/UML/)
- [PlantUML Class Diagrams](https://plantuml.com/class-diagram)
- [dbdiagram.io](https://dbdiagram.io)
