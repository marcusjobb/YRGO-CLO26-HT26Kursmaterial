# Constraints

🟢


<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. TOC
{:toc}

</details>

## TL;DR

Constraints i SQL används för att begränsa datan i en tabell. Det finns två typer av constraints: primary key och foreign key. Primary key används för att identifiera unika värden för varje rad i en tabell, medan foreign key används för att koppla ihop tabeller genom att begränsa värdena till de som finns i en annan tabell.

## När du läst detta ska du kunna

- Förstå vad constraints är i SQL.
- Känna till skillnaden mellan primary key och foreign key.
- Använda constraints för att skapa kopplingar mellan tabeller.

## Introduktion

Constraints i SQL är regler som kan tillämpas på tabeller för att begränsa datan som kan lagras i dem. Genom att använda constraints kan vi säkerställa dataintegritet och upprätthålla relationer mellan tabeller.

I denna artikel kommer vi att fokusera på två typer av constraints: primary key och foreign key.

## Beskrivning

### Primary Key

Primary Key är ett unikt värde för varje rad i en tabell. Det används för att identifiera och söka efter specifika rader i tabellen. En tabell kan ha endast en primary key, och den kan inte vara null. Det är också möjligt att ha en primary key som är en kombination av flera kolumner, vilket kallas en sammansatt primary key.

### Foreign Key

Foreign key är ett värde i en tabell som är kopplat till ett värde i en annan tabell. Det används för att skapa relationer mellan tabeller genom att begränsa vilka värden som kan lagras i den främmande nyckelkolumnen. En tabell kan ha flera foreign keys, men varje foreign key kan bara referera till en unik primary key i en annan tabell. Precis som primary key kan inte heller foreign key vara null, och den kan också vara en sammansatt nyckel.

## Fördelar

Genom att använda constraints i SQL kan vi:

- Säkerställa unika värden för varje rad i en tabell med hjälp av primary key.
- Skapa relationer och referenser mellan tabeller genom foreign key.
- Bevara dataintegritet genom att begränsa vilka värden som kan lagras i tabellernas kolumner.
- Optimera frågor genom att utnyttja constraints för att utföra snabba sökningar och kopplingar mellan tabeller.

## Begränsningar

Några viktiga begränsningar att vara medveten om när man använder constraints är:

- Varje tabell kan ha endast en primary key och flera foreign keys.
- Primary key och foreign key kan inte vara null.
- Om en tabell refererar till en annan tabell med en foreign key, måste värdet i foreign key-kolumnen finnas som en primary key i den andra tabellen.
- Vid användning av sammansatta nycklar måste kombinationen

 av värden vara unik för varje rad i tabellen.

## Användningsområden

Constraints används i en mängd olika situationer i databasdesign och datahantering. Några vanliga användningsområden inkluderar:

- Säkerställa att varje användare i en användartabell har en unik användaridentitet genom att använda primary key.
- Skapa referenser och relationer mellan tabeller, till exempel mellan en order och en kundtabell, genom foreign key.
- Begränsa vilka värden som kan lagras i en kolumn, till exempel att bara tillåta numeriska värden eller en viss längd på en sträng.

## Exempelkod

Följande exempelkod visar hur du kan använda constraints för att skapa tabeller och koppla dem ihop med hjälp av primary key och foreign key i SQL:

```sql
CREATE TABLE Person (
    PersonID int NOT NULL PRIMARY KEY,
    LastName varchar(255) NOT NULL,
    FirstName varchar(255),
    Address varchar(255),
    City varchar(255)
);

CREATE TABLE Orders (
    OrderID int NOT NULL PRIMARY KEY,
    OrderDate date NOT NULL,
    PersonID int NOT NULL,
    FOREIGN KEY (PersonID) REFERENCES Person(PersonID)
);
```

I det här exemplet skapas en tabell "Person" med en primary key-kolumn "PersonID" och andra kolumner för personens namn, adress och stad. Sedan skapas en tabell "Orders" med en primary key-kolumn "OrderID", en kolumn "OrderDate" och en foreign key-kolumn "PersonID" som refererar till "Person"-tabellen.

För att koppla ihop tabellerna och visa information från båda tabellerna kan vi använda följande SQL-fråga:

```sql
SELECT * FROM Person
INNER JOIN Orders ON Person.PersonID = Orders.PersonID;
```

Denna fråga hämtar alla rader från både "Person" och "Orders" tabellerna där primary key-värdet i "Person" matchar foreign key-värdet i "Orders".

## Slutsats

Constraints i SQL är kraftfulla verktyg som används för att begränsa datan i tabeller och skapa relationer mellan dem. Genom att använda primary key och foreign key kan vi upprätthålla dataintegritet och skapa välstrukturerade databaser. Genom att förstå och använda constraints på rätt sätt kan vi optimera våra databasdesigner och förbättra effektiviteten i våra SQL-frågor.

## Termtabell

- Constraints: Regler som används för att begränsa datan i SQL-tabeller.
- Primary Key: Unikt värde för varje rad i en tabell som används för att identifiera och söka efter rader.
- Foreign Key: Värde i en tabell som är kopplat till ett värde i en annan tabell för att skapa relationer och referenser.
- Dataintegritet: Säkerställer att datan i en databas är korrekt och konsekvent.

## Obligatorisk Dad-joke

Varför gillar databaser att festa?
För att de har massor av "tables" att dansa på! 🕺💃

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
