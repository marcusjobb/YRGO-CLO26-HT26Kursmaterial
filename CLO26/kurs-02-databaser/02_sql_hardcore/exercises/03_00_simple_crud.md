---

title: Enkel CRUD med Entity Framework Core
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/ef/03_00_simple_crud.md"
description: "En grundläggande övning för att förstå Create, Read, Update, Delete operationer med EF Core."
tags: ["core", "crud", "csharp", "databaser", "enkel", "entity", "exercise", "framework", "installation", "simple"]
week_fit: []
---

# Enkel CRUD med Entity Framework Core

🔴


En grundläggande övning för att förstå Create, Read, Update, Delete operationer med EF Core.

## Mål

- Skapa ett enkelt bibliotekssystem
- Öva på alla CRUD-operationer
- Förstå hur SaveChanges fungerar
- Hantera användarin put från konsolen

## Projekt: Enkelt Bibliotek

Vi bygger ett system som håller koll på böcker.

## Steg 1 – Setup (5 min)

```bash
dotnet new console -n SimpleLibrary
cd SimpleLibrary
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

## Steg 2 – Skapa Book-entitet (5 min)

`Book.cs`:

```csharp
public class Book
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Author { get; set; } = string.Empty;
    public int Year { get; set; }
    public bool IsAvailable { get; set; } = true;
}
```

## Steg 3 – DbContext (5 min)

`LibraryContext.cs`:

```csharp
using Microsoft.EntityFrameworkCore;

public class LibraryContext : DbContext
{
    public DbSet<Book> Books => Set<Book>();

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlite("Data Source=library.db");
    }
}
```

## Steg 4 – Migrations (3 min)

```bash
dotnet ef migrations add Initial
dotnet ef database update
```

## Steg 5 – CREATE (Lägg till böcker) (10 min)

`Program.cs`:

```csharp
using SimpleLibrary;

using var db = new LibraryContext();

void AddBook()
{
    Console.Write("Titel: ");
    var title = Console.ReadLine() ?? "";

    Console.Write("Författare: ");
    var author = Console.ReadLine() ?? "";

    Console.Write("År: ");
    int.TryParse(Console.ReadLine(), out int year);

    var book = new Book
    {
        Title = title,
        Author = author,
        Year = year,
        IsAvailable = true
    };

    db.Books.Add(book);
    db.SaveChanges();

    Console.WriteLine($"✅ Bok tillagd med Id: {book.Id}");
}

// Testa
AddBook();
```

## Steg 6 – READ (Visa böcker) (10 min)

```csharp
void ListAllBooks()
{
    var books = db.Books
        .OrderBy(b => b.Title)
        .ToList();

    Console.WriteLine($"\n📚 Böcker i biblioteket ({books.Count}):\n");

    foreach (var book in books)
    {
        var status = book.IsAvailable ? "Tillgänglig" : "Utlånad";
        Console.WriteLine($"{book.Id}. {book.Title} av {book.Author} ({book.Year}) - {status}");
    }
}

void FindBook()
{
    Console.Write("Sök titel: ");
    var search = Console.ReadLine() ?? "";

    var books = db.Books
        .Where(b => b.Title.Contains(search))
        .ToList();

    Console.WriteLine($"\nHittade {books.Count} bok/böcker:");
    foreach (var book in books)
    {
        Console.WriteLine($"- {book.Title} av {book.Author}");
    }
}
```

## Steg 7 – UPDATE (Uppdatera bok) (10 min)

```csharp
void LoanBook()
{
    Console.Write("Ange bok-Id: ");
    int.TryParse(Console.ReadLine(), out int id);

    var book = db.Books.Find(id);

    if (book == null)
    {
        Console.WriteLine("❌ Boken finns inte!");
        return;
    }

    if (!book.IsAvailable)
    {
        Console.WriteLine("❌ Boken är redan utlånad!");
        return;
    }

    book.IsAvailable = false;
    db.SaveChanges();

    Console.WriteLine($"✅ {book.Title} är nu utlånad");
}

void ReturnBook()
{
    Console.Write("Ange bok-Id: ");
    int.TryParse(Console.ReadLine(), out int id);

    var book = db.Books.Find(id);

    if (book == null)
    {
        Console.WriteLine("❌ Boken finns inte!");
        return;
    }

    book.IsAvailable = true;
    db.SaveChanges();

    Console.WriteLine($"✅ {book.Title} är återlämnad");
}
```

## Steg 8 – DELETE (Radera bok) (5 min)

```csharp
void DeleteBook()
{
    Console.Write("Ange bok-Id att radera: ");
    int.TryParse(Console.ReadLine(), out int id);

    var book = db.Books.Find(id);

    if (book == null)
    {
        Console.WriteLine("❌ Boken finns inte!");
        return;
    }

    Console.Write($"Är du säker på att radera '{book.Title}'? (ja/nej): ");
    var confirm = Console.ReadLine()?.ToLower();

    if (confirm == "ja")
    {
        db.Books.Remove(book);
        db.SaveChanges();
        Console.WriteLine("✅ Boken raderad");
    }
    else
    {
        Console.WriteLine("Avbrutet");
    }
}
```

## Steg 9 – Meny (10 min)

Komplett program med meny:

```csharp
bool running = true;

while (running)
{
    Console.WriteLine("\n=== BIBLIOTEKET ===");
    Console.WriteLine("1. Lägg till bok");
    Console.WriteLine("2. Visa alla böcker");
    Console.WriteLine("3. Sök bok");
    Console.WriteLine("4. Låna ut bok");
    Console.WriteLine("5. Återlämna bok");
    Console.WriteLine("6. Radera bok");
    Console.WriteLine("0. Avsluta");
    Console.Write("> ");

    switch (Console.ReadLine())
    {
        case "1": AddBook(); break;
        case "2": ListAllBooks(); break;
        case "3": FindBook(); break;
        case "4": LoanBook(); break;
        case "5": ReturnBook(); break;
        case "6": DeleteBook(); break;
        case "0": running = false; break;
        default: Console.WriteLine("Ogiltigt val"); break;
    }
}
```

## Utmaning 1 – Statistik

Lägg till en menyoption som visar statistik:

```csharp
void ShowStatistics()
{
    var total = db.Books.Count();
    var available = db.Books.Count(b => b.IsAvailable);
    var loaned = total - available;

    Console.WriteLine("\n📊 Statistik:");
    Console.WriteLine($"Totalt antal böcker: {total}");
    Console.WriteLine($"Tillgängliga: {available}");
    Console.WriteLine($"Utlånade: {loaned}");

    // Mest populära författare
    var topAuthor = db.Books
        .GroupBy(b => b.Author)
        .OrderByDescending(g => g.Count())
        .FirstOrDefault();

    if (topAuthor != null)
    {
        Console.WriteLine($"Mest förekommande författare: {topAuthor.Key} ({topAuthor.Count()} böcker)");
    }
}
```

## Utmaning 2 – Uppdatera bokinfo

```csharp
void UpdateBook()
{
    Console.Write("Ange bok-Id: ");
    int.TryParse(Console.ReadLine(), out int id);

    var book = db.Books.Find(id);
    if (book == null)
    {
        Console.WriteLine("❌ Boken finns inte!");
        return;
    }

    Console.WriteLine($"\nNuvarande info: {book.Title} av {book.Author}");

    Console.Write("Ny titel (lämna tom för oförändrad): ");
    var title = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(title))
        book.Title = title;

    Console.Write("Ny författare (lämna tom för oförändrad): ");
    var author = Console.ReadLine();
    if (!string.IsNullOrWhiteSpace(author))
        book.Author = author;

    db.SaveChanges();
    Console.WriteLine("✅ Boken uppdaterad");
}
```

## Utmaning 3 – Sortering

Lägg till sortering i lista:

```csharp
void ListBooksSorted()
{
    Console.WriteLine("Sortera efter:");
    Console.WriteLine("1. Titel");
    Console.WriteLine("2. Författare");
    Console.WriteLine("3. År");
    Console.Write("> ");

    var books = Console.ReadLine() switch
    {
        "1" => db.Books.OrderBy(b => b.Title).ToList(),
        "2" => db.Books.OrderBy(b => b.Author).ThenBy(b => b.Title).ToList(),
        "3" => db.Books.OrderByDescending(b => b.Year).ToList(),
        _ => db.Books.ToList()
    };

    foreach (var book in books)
    {
        Console.WriteLine($"{book.Title} av {book.Author} ({book.Year})");
    }
}
```

## Vad lärde du dig?

✅ **CREATE** – `Add()` + `SaveChanges()`
✅ **READ** – `ToList()`, `Find()`, `FirstOrDefault()`, `Where()`
✅ **UPDATE** – Ändra property + `SaveChanges()`
✅ **DELETE** – `Remove()` + `SaveChanges()`

✅ EF Core spårar ändringar automatiskt
✅ `SaveChanges()` skickar allt till databasen på en gång
✅ `Find()` är snabbast för att hämta via Id

## Nästa steg

- Lägg till [betyg/recensioner](link-to-relations.md) på böcker (relationer!)
- Prova [superhero-övningen](03_04_heroes.md) för avancerade relationer
- Läs om [LINQ queries](link-to-linq.md) för mer avancerade sökningar

## Vanliga misstag

**❌ Glömma SaveChanges:**

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
book.Title = "Ny titel";
// Glömde db.SaveChanges() – ingenting sparas!
```

**❌ Inte kolla om null:**

```csharp
var book = db.Books.Find(999);  // Finns inte
book.Title = "Test";  // NullReferenceException!
```

**✅ Rätt:**

```csharp
var book = db.Books.Find(999);
if (book != null)
{
    book.Title = "Test";
    db.SaveChanges();
}
```

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
