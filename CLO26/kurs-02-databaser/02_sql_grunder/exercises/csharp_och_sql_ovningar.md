---

title: C# och SQL övningar (updaterad)
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 2
language: mixed
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/C# och SQL övningar (updaterad).md"
description: "Följande uppgifter kräver en del tänkande och kodande så jag föreslår att ni gör det i era grupper, så kommer vi att kolla på resultatet tillsammans på torsdag."
tags: ["ado-net", "connection-string", "crud", "databaser", "datatable", "exercise", "klasser", "mockaroo", "sql", "övningar"]
week_fit: []
---

# C# + SQL Övningar

🟡



Följande uppgifter kräver en del tänkande och kodande så jag föreslår att ni gör det i era grupper, så kommer vi att kolla på resultatet tillsammans på torsdag.

Här nedan följer några uppgifter för att lektionen ska smälta in lite mer.


Hälsningar

Marcus

# Uppgift 1 – Skapa klassen Databas.cs

För att enklast kunna samarbeta med databasen ska vi skapa en klass som vi kallar Databas.cs

I denna kommer vi att spara alla metoder vi kan behöva för framtida databasanvändande. En bra programmerare en lat programmerare, och slipper man skriva om kod så sparar man sig själv en massa tid.

Det är alltid bra att utgå från en klass som hanterar all databaskommunikation, på så sätt slipper man upprepa koden. Precis som när vi normaliserar (förenklar och delar upp) en databas, så förenklar vi vår kod. En klass till att hantera allt som har med databasen att göra.

One class to rule them all

- Öppna Visual Studio

- Skapa ett projekt  (Kalla den vad du vill)

- I den skapa klassen Databas.cs

- Skapa en kallad en property kallad ConnectionString

- Skapa en property kallad DatabaseName

- I constructorn eller i propertyns defaultvärde anger du 
@"Data Source=.\SQLExpress;Integrated Security=true;database={0}" 
till din ConnectionString
och valfri databasnamn till DatabaseName propertyn

Den hör klassen kommer vi att använda som grund för de här övningarna.


# Uppgift 2 – Metoder för att skicka och ta emot information

Vi ska nu skapa metoder för att kunna använda databasen, som vi såg i Star Trek episoden med Kirk och SQL så vet vi att det finns olika sätt att kommunicera med databasen.

ExecuteNonQuery() kör SQL koden och returnerar en long med antal påverkade rader

SQLAdapter objektet hämtar informationen från databasen till ett DataTable objekt.

- Skapa en metod (ex ExecuteSQL) som tar emot SQL string och parametrar och som exekverar din SQL kod och returnerar ingenting 
När du kör din SQL kod, använd ExecuteNonQuery.
(Metoden ExecuteNonQuery() returnerar egentligen en long med antalet rader som påverkats)

- Skapa en metod (ex GetDataTable) som tar emot SQL string och parametrar och som exekverar din SQL kod och returnerar en DataTable.

Tänk på att metoderna som pratar med databasen ska använda sig av propertyn för ConnectionString så att vi är säkra på att alla metoderna kopplar sig till samma databas.

När du ska använda din connection string, glöm inte att skriva exempelvis 
var conString = string.Format(ConnectionString, Database);

Detta fungerar på samma sätt som när man skriver

```
Console.WriteLine("Hälsningsfras: {0}","Hej");
```

String format kommer att ersätta {0} i din ConnectionString med databasens namn, så slipper du bråka med din string.

Använd nu din nya sträng conString som ConnectionString när du kopplar dig till databasen.


# Uppgift 3 – Testkörning

Nu har vi metoder för kommunikation med databasen. Då ska vi testa lite.

I program.main()

- Skapa en instans av din databasklass

- Ställ in din DatabaseName till att köra mot databasen Population

- Använd dina metoder för att hämta en lista på personer över 30 år och under 50 år

- Skriv ut listan på Consolen


# Uppgift 4 – Gräver lite i systemet

Förutom de databaser vi skapar så finns det en systemdatabas som ligger och gömmer sig i SQL-Serverns vrår. Tabellen finns inte riktigt som det ser ut, det är en virtuell tabell som används för att göra livet enklare för databaskodare. Denna har en del trevliga vyer som vi kan titta på.

En sådan vy är Sys.Database_files.

Vyn talar om för oss var databasfilerna finns för den databasen vi är kopplade emot. För som sagt, hela databasen är egentligen en stor fil. Vi kan få fram en lista på filerna genom att enkelt fråga Sys.Database_files om information.


- Skapa en instans av din databasklass

- Kör följande fråga:  SELECT physical_name, size FROM sys.database_files

- Skiv ut resultatet på Consolen

- Skapa en metod i din klass som returnerar filnamnen

Nu vet du i alla fall var filerna finns ifall du skulle behöva en backupp din databas. (det finns dock andra sätt att göra backupp på)


# Uppgift 5 – Lite planering vore inte helt fel

På Mockaroo fixar vi en lista som vi ska läsa ner som CSV för att själv kunna hantera alla INSERTS. Men den här tabellen kommer att generera många uppredande rader.

Vad är ett bra sätt att dela upp det på? Diskutera gärna i grupp hur en sådan här tabell skulle kunna delas upp för att göra databasen så liten som möjligt.


- Hur många tabeller blir det ungefär?

- Behöver alla Primary Key?

- Rita gärna ett diagram med data

- Blir allt 1-1 kopplingar?

- Behövs en kopplingstabell / Relationstabell?


# Uppgift 6 – Gräver lite mer i systemet

Vi fortsätter med att använda oss av databasklassen för nästa fråga.

Nu vill vi veta vilka databaser som finns i servern.

- Fråga databasklassen: SELECT name FROM sys.databases

- Skriv ut listan på Consolen

- Skapa en metod i databasklassen som hämtar listan

Nu vet du hur du får reda på vilka databaser som finns i din lista.


# Uppgift 7 – Skapa en databas

Nu ska vi skapa databaser från koden.

CREATE DATABASE  dbnamn; -- skapar en databas kort och gott, allt den behöver är filnamnet.

CREATE TABLE är lite krångligare. 
Man anger namn på tabellen och sedan parenteser, i parentesen anger man kolumnnamn och typ sedan anger man regler som gäller.

Id int PRIMARY KEY IDENTITY (1,1) , -- Skapar kolumn Id som är Primary Key, börjar på 1 och ökar med 1
kolumnnamn typ NOT NULL,  -- värde som inte får vara null
kolumnnamn typ UNIQUE, – värde som måste vara unik
FOREIGN KEY (valfrikolumn) REFERENCES tabell (tabellensId) -- skapar länk till Foreign Key i annan tabell

- Genom din databasklass, skapa en ny Databas med namnet Humans

- CREATE DATABASE Humans;

- Byt DatabaseName till att använda databasen Humans

- Genom din databasklass, skapa en ny Tabell med namnet People

```
Exempel:
CREATE TABLE People (
    ID int NOT NULL Identity (1,1),
    lastName varchar(255),
    firstName varchar(255),
    address varchar(255),
    city varchar(255),
    shoeSize int 
);
```

- Lägg till kolumnen age i tabellen
ALTER TABLE People
ADD age int;

- Ta nu bort kolumnen ShoeSize
ALTER TABLE People
DROP COLUMN shoeSize;

- För framtida bruk, skapa metoder i din databasklass som tar emot parametrar

- CreateDatabase (string databasnamn)

- CreateTable (string table, string field)

- AlterTable (string table, string field)

- DropDatabase(string database)

- DropTable(string table)


# Uppgift 8 – Testdata!

Nu behöver vi testadata… och då tar vi en runda till Mockaroo

Hämta data för att fylla tabellen People


# Uppgift 9 – Klasser och SQL


- Skapa en klass kallad Person

- I den skapar du nu properties för Id, LastName, FirstName, Address, City, Age

- Skapa en klass kallad People

- I People skapa nu CRUD metoder som hanterar People objekt
exempelvis 
public void Create(Person person), 
public Person Read(string name), 
public void Update(Person person), 
public void Delete(Person person)
Metoderna ska ta emot en Person objekt och lägga in dessa värden i SQL kod + parametrar för att spara, radera, söka eller uppdatera tabellen People. Read ska returnera en Person objekt eller null.

- Skapa en List() metod som returnerar alla rader med personer

- Testa de olika metoderna

- Skriv ut resultatet på Consolen

Några datatyper som används av MsSQL och hur man tolkar dem i C#

| SQL Server Database Engine type | .NET Framework type |
| --- | --- |
| bigint | Int64 |
| binary | Byte[] |
| bit | Boolean |
| char | String |
| datetime | DateTime |
| decimal | Decimal |
| float | Double |
| image | Byte[] |
| int | Int32 |
| money | Decimal |
| nchar | String |
| numeric | Decimal |
| nvarchar | String |
| real | Single |
| smalldatetime | DateTime |
| smallint | Int16 |
| smallmoney | Decimal |
| timestamp | Byte[] |
| tinyint | Byte |
| uniqueidentifier | Guid |
| varbinary | Byte[] |
| varchar | String |

Källa: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql-server-data-type-mappings?redirectedfrom=MSDN


# Lärdomar

Nu har ni du en klass som hanterar din kommunikation med databasen, den här klassen kan du återanvända för att slippa skriva om koden.

I övningarna har du fått lära dig

- Hur man skapar och tar bort databaser.

- Hur man skapar, ändrar och tar bort tabeller och kolumner.

- Hur man läser in från en DataTable och sparar värdet i en property

- Hur man läser properties och omvandlar till SQL parametrar

- CRUD metoder

Har du fått ordning på allt detta så har du allt du behöver för kommande Inlämning 1

Huvudsaken är att du nu kan kommunicera med databasen via din klass och slipper därmed allt hanterande av databas, och kan därmed fokusera dig på C#.

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
