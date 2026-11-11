# Code-First Dagbok med Entity Framework

🟢


## Introduktion

I den här övningen kommer vi att skapa en enkel dagboksapplikation som låter användare skapa och visa dagboksinlägg från konsolen. Vi kommer att använda Entity Framework och Code-First tillvägagångssättet för att skapa och hantera databasen.

## Förutsättningar

Innan vi börjar, se till att du har följande installerat på din dator:

- Visual Studio eller annan C#-kompatibel utvecklingsmiljö
- .NET Core SDK

## Steg 1: Skapa projektet

1. Öppna Visual Studio och skapa ett nytt konsolprojekt.
2. Välj .NET Core som målplattform och ange ett lämpligt namn för projektet.

## Steg 2: Installera Entity Framework

1. Högerklicka på projektet i Lösarexplorer och välj "Hantera NuGet-paket".
2. Sök efter "Microsoft.EntityFrameworkCore" och installera paketet.
3. Sök också efter "Microsoft.EntityFrameworkCore.Design" och installera det.

## Steg 3: Skapa databasmodeller

1. Skapa en ny klassfil `DiaryEntry.cs` och lägg till följande kod:

```csharp
using System.ComponentModel.DataAnnotations;

public class DiaryEntry
{
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    public string Content { get; set; }
}
```

## Steg 4: Skapa en DbContext

1. Skapa en ny klassfil `DiaryContext.cs` och lägg till följande kod:

```csharp
using Microsoft.EntityFrameworkCore;

public class DiaryContext : DbContext
{
    public DbSet<DiaryEntry> DiaryEntries { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=diary.db");
    }
}
```

## Steg 5: Skapa metoder för att visa och skapa inlägg

1. Öppna filen `Program.cs` och uppdatera koden enligt följande:

```csharp
using System;

namespace DiaryApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Välkommen till din dagbok!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("Välj en handling:");
                Console.WriteLine("1. Visa alla inlägg");
                Console.WriteLine("2. Skapa ett nytt inlägg");
                Console.WriteLine("0. Avsluta");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowAllEntries();
                        break;
                    case "2":
                        CreateNewEntry();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Ogiltigt val. Försök igen.");
                        break;
                }
            }
        }

        static void ShowAllEntries()
        {
            // Hämta alla dagboksinlägg från databasen
            using (var context = new DiaryContext())
            {
                var entries = context.DiaryEntries;

                Console.WriteLine("Dina dagboksinlägg:");
                foreach (

var entry in entries)
                {
                    Console.WriteLine($"Id: {entry.Id}, Titel: {entry.Title}");
                    Console.WriteLine($"Innehåll: {entry.Content}");
                    Console.WriteLine();
                }
            }
        }

        static void CreateNewEntry()
        {
            Console.WriteLine("Ange en titel för ditt inlägg:");
            string title = Console.ReadLine();

            Console.WriteLine("Skriv ditt inlägg:");
            string content = Console.ReadLine();

            // Skapa ett nytt DiaryEntry-objekt med användarens angivna titel och innehåll
            var entry = new DiaryEntry
            {
                Title = title,
                Content = content
            };

            using (var context = new DiaryContext())
            {
                // Lägg till det nya inlägget i DiaryEntries-uppsättningen
                context.DiaryEntries.Add(entry);

                // Spara ändringarna i databasen
                context.SaveChanges();

                Console.WriteLine("Inlägget har sparats.");
            }
        }
    }
}
```

## Steg 6: Kör applikationen

1. Tryck på F5 eller klicka på Start-knappen för att köra applikationen.
2. Följ menyvalen för att visa alla inlägg eller skapa ett nytt inlägg i dagboken.

## Sammanfattning

Grattis! Du har nu skapat en enkel dagboksapplikation som använder en Code-First databas. Du kan visa befintliga inlägg och skapa nya inlägg direkt från konsolen. Du kan också utöka applikationen med fler funktioner och anpassningar baserat på dina behov.

Fortsätt utforska Entity Framework och Code-First-approachen för att bygga mer komplexa och kraftfulla databasapplikationer.

## Termer

| Term              | Definition                                                                                                    |
| ----------------- | ------------------------------------------------------------------------------------------------------------- |
| Code-First        | Ett tillvägagångssätt för att skapa en databas genom att definiera databasmodeller i koden.                   |
| Data Source       | En relationsdatabas som används för att lagra data.                                                           |
| Database-first    | Ett tillvägagångssätt för att skapa en databas genom att definiera databasmodeller i koden.                   |
| DbContext         | En klass som representerar en session med databasen.                                                          |
| DbSet             | En uppsättning av databasobjekt som kan användas för att hämta och spara data.                                |
| Entity Framework  | Ett objektrelationellt kartläggningsbibliotek som används för att hantera databasobjekt i .NET-applikationer. |
| Migration         | En databasförändring som kan tillämpas på en databas.                                                         |
| Migrationer       | En databasförändring som kan tillämpas på en databas.                                                         |
| Migrationsskript  | En databasförändring som kan tillämpas på en databas.                                                         |
| Migrationsverktyg | Ett verktyg som används för att skapa och hantera databasmigrationer.                                         |
| Modell            | En klass som representerar en databasentitet.                                                                 |
| Modellering       | En klass som representerar en databasentitet.                                                                 |
| ORM               | Ett objektrelationellt kartläggningsbibliotek som används för att hantera databasobjekt i .NET-applikationer. |
| SQL               | Ett programmeringsspråk som används för att kommunicera med databaser.                                        |
| SQL Server        | En relationsdatabas som används för att lagra data.                                                           |
| SQLite            | En relationsdatabas som används för att lagra data.                                                           |

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
