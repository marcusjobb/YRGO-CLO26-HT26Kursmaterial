---

title: 3. Skapa och hantera SQLite-databaser i C#
author: Marcus Ackre Medina
type: lecture
topic: oop
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/3_oop_adv/lectures/01_oop_sqlite/3_create_and_manage_sqlite_databases_in_c.md"
description: "Övergripande frågeställning: Hur kan vi effektivt skapa, strukturera och hantera SQLite-databaser i C# för att bygga robusta och skalbara applikationer?"
tags: ["and", "create", "csharp", "databases", "hantera", "installation", "manage", "oop", "skapa", "sqlite"]
week_fit: []
---

# 3. Skapa och hantera SQLite-databaser i C#

🔴


Övergripande frågeställning: Hur kan vi effektivt skapa, strukturera och hantera SQLite-databaser i C# för att bygga robusta och skalbara applikationer?

Glöm inte att du måste ha SQLite nugets installerat för att kunna köra kodexemplen nedan. De heter `System.Data.SQLite` och `System.Data.SQLite.Core`.

## 3.1 Skapa en ny SQLite-databas med C#

I SQLite skapas en ny databas enkelt genom att ansluta till en fil som ännu inte existerar. Den inbyggda SQLite-drivrutinen i C# skapar då automatiskt databasfilen. Detta förenklar processen jämfört med andra databashanterare som kräver explicita kommandon för att skapa en databas.

Syntax för att skapa en ny SQLite-databas:

```csharp
string connectionString = "Data Source=newdatabase.db";
using (var connection = new SQLiteConnection(connectionString))
{
    connection.Open();
}
```

Komplett kodexempel:

```csharp
using System;
using System.Data.SQLite;

class CreateDatabaseExample
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=newdatabase.db";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            var meta = connection.ServerVersion;
            Console.WriteLine("The SQLite version is " + meta);
            Console.WriteLine("A new database has been created.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

Detta exempel skapar en ny SQLite-databas och skriver ut information om den skapade anslutningen. Det demonstrerar hur enkelt det är att initiera en ny databas med SQLite i C#. Observera användningen av `using`-satsen för att säkerställa att databaskopplingen stängs korrekt.

## 3.2 Skapa tabeller med SQL via C#

När databasen är skapad, är nästa steg att skapa tabeller för att lagra data. Detta görs genom att exekvera SQL-kommandon via C#. Vi använder CREATE TABLE-satser för att definiera strukturen på våra tabeller.

Syntax för att skapa en tabell:

```csharp
string sql = "CREATE TABLE IF NOT EXISTS tablename (id INTEGER PRIMARY KEY, name TEXT NOT NULL)";
using (var command = new SQLiteCommand(sql, connection))
{
    command.ExecuteNonQuery();
}
```

Komplett kodexempel:

```csharp
using System;
using System.Data.SQLite;

class CreateTableExample
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=newdatabase.db";
        string sql = "CREATE TABLE IF NOT EXISTS employees (" +
                     "id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                     "name TEXT NOT NULL, " +
                     "position TEXT NOT NULL, " +
                     "salary REAL" +
                     ")";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
                Console.WriteLine("Table created successfully");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
```

Detta exempel skapar en "employees"-tabell med kolumner för id, namn, position och lön. Det visar hur man definierar olika datatyper och använder SQLite-specifika funktioner som `AUTOINCREMENT`. Användningen av `IF NOT EXISTS` förhindrar fel om tabellen redan finns, vilket gör koden mer robust.

## 3.3 Felhantering och användning av `using`-satsen

Korrekt felhantering är avgörande för att bygga robusta databasapplikationer. C#'s `using`-sats är särskilt användbar för att hantera databasresurser, eftersom den automatiskt stänger resurser som `SQLiteConnection` och `SQLiteCommand`.

Syntax för `using`-satsen med SQLite i C#:

```csharp
using (var connection = new SQLiteConnection(connectionString))
{
    using (var command = new SQLiteCommand(sql, connection))
    {
        // Databasoperationer
    }
}
```

Komplett kodexempel med utökad felhantering:

```csharp
using System;
using System.Data.SQLite;

class DatabaseOperationsExample
{
    static void Main(string[] args)
    {
        string connectionString = "Data Source=newdatabase.db";
        string sql = "INSERT INTO employees (name, position, salary) VALUES (@name, @position, @salary)";

        using (var connection = new SQLiteConnection(connectionString))
        {
            connection.Open();
            using (var command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@name", "John Doe");
                command.Parameters.AddWithValue("@position", "Manager");
                command.Parameters.AddWithValue("@salary", 50000.0);
                command.ExecuteNonQuery();

                Console.WriteLine("Data inserted successfully");
            }
        }
        catch (SQLiteException e)
        {
            Console.WriteLine("SQL Error: " + e.Message);
        }
        catch (Exception e)
        {
            Console.WriteLine("Unexpected error: " + e.Message);
        }
    }
}
```

Detta exempel demonstrerar användningen av `SQLiteCommand` för säker datainsättning, `using`-satsen för automatisk resurshantering, och detaljerad felhantering för SQL-specifika problem. `SQLiteCommand` används för att förhindra SQL-injektion och förbättra prestanda vid upprepade exekveringar.

### Integration i Visual Studio och VSCode

För att arbeta med SQLite-databaser i Visual Studio eller VSCode kan du använda följande tillvägagångssätt:

1. **Visual Studio**:

   - Installera `System.Data.SQLite`-paketet via NuGet Package Manager.
   - Installera `System.Data.SQLite.Core`-paketet för .NET Core-projekt.
   - Skapa en ny C#-konsolapplikation och lägg till `using System.Data.SQLite;` i dina kodfiler.
   - Använd SQLite-databasen enligt exemplen ovan och kör din kod direkt i Visual Studio.

2. **VSCode**:
   - Installera .NET SDK och skapa en ny konsolapplikation med `dotnet new console`.
   - Lägg till `System.Data.SQLite` med `dotnet add package System.Data.SQLite`.
   - Följ samma kodexempel som ovan och kör applikationen med `dotnet run`.

## Sammanfattning och reflektion

Nyckelpoänger:

- SQLite-databaser skapas enkelt genom att ansluta till en ny fil.
- Tabeller skapas med SQL-kommandon exekverade via C#.
- Korrekt felhantering och resurshantering är kritiska för robusta applikationer.
- `SQLiteCommand` bör användas för säker datamanipulation och förbättrad prestanda.

Reflektionsövning: Tänk på en applikation du vill utveckla. Hur skulle du strukturera databasen för denna applikation? Vilka tabeller skulle du behöva, och vilka relationer skulle finnas mellan dem? Överväg hur du kan använda SQLite's unika egenskaper för att optimera din databasdesign.

---

## Övningsuppgifter

1. Skapa en C#-applikation som skapar en ny SQLite-databas kallad "library.db". I denna databas, skapa en tabell kallad "books" med kolumnerna id (INTEGER PRIMARY KEY), title (TEXT), author (TEXT), och publication_year (INTEGER). Lägg till felhantering och skriv ut lämpliga meddelanden vid framgång eller misslyckande.

2. Utöka den föregående applikationen för att lägga till några böcker i "books"-tabellen. Använd `SQLiteCommand` för att säkert infoga data. Lägg sedan till funktionalitet för att visa alla böcker i tabellen.

3. Skapa en ny tabell kallad "borrowers" i "library.db" med kolumnerna id (INTEGER PRIMARY KEY), name (TEXT), och email (TEXT). Implementera funktioner för att lägga till nya låntagare och visa alla låntagare. Använd transaktioner för att säkerställa att alla operationer lyckas eller misslyckas som en enhet.

4. Implementera en funktion som låter en låntagare låna en bok. Skapa en ny tabell kallad "loans" som kopplar samman böcker och låntagare, med kolumnerna loan_id (INTEGER PRIMARY KEY), book_id (INTEGER), borrower_id (INTEGER), loan_date (TEXT), och return_date (TEXT). Använd foreign key constraints för att upprätthålla referensintegritet. Implementera även en funktion för att returnera en bok och uppdatera return_date.

5. Skapa en avancerad sökfunktion som låter användaren söka efter böcker baserat på titel, författare, eller publiceringsår. Implementera funktionalitet för att sortera sökresultaten. Använd indexering på relevanta kolumner för att optimera sökprestandan. Lägg även till felhantering för att hantera ogiltiga sökkriterier eller databasfel på ett användarvänligt sätt.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
