---

title: Kontext
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/CLO22/Docs/entityframework/kontext/index.md"
description: "Yay, dags att lära oss om databas-kontexten! En databaskontext är en klass som ärver från DbContext och används för att kommunicera med databasen med hjälp av Entity Framework Core."
tags: ["csharp", "databaser", "design-patterns", "entity-framework", "git", "kontext", "oop", "sql"]
week_fit: []
---

# Kontext

🟢


Yay, dags att lära oss om databas-kontexten! En databaskontext är en klass som ärver från DbContext och används för att kommunicera med databasen med hjälp av Entity Framework Core.

## TL;DR

En databaskontext är en klass som ärver från DbContext och används för att kommunicera med databasen med hjälp av Entity Framework Core.

## Beskrivning

En DBContext innehåller en samling av entiteter. Vad är då en entitet? Jo, en entitet är helt enkelt en klass som representerar en tabell i databasen. Varje entitet har egenskaper som motsvarar kolumnerna i tabellen.

## Skapa en kontext för SQL Server

```csharp
public class MyDbContext : DbContext
{
    string connString = "Server=localhost;Database=MyDatabase;User Id=sa;Password=Password123;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(connString);
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}
```

## Skapa en kontext för SQLite

```csharp
public class MyDbContext : DbContext
{
    string connString = "Data Source=MyDatabase.db";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(connString);
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}
```

## Skapa en kontext för MySQL (med Pomelo)

```csharp
public class MyDbContext : DbContext
{
    string connString = "Server=localhost;Database=MyDatabase;User Id=sa;Password=Password123;";

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var version = new MySqlServerVersion(new Version(8, 0, 21)); // Sätt senaste versionen av MySQL
        optionsBuilder.UseMySql(connString, version);
    }

    public DbSet<Blog> Blogs { get; set; }
    public DbSet<Post> Posts { get; set; }
}
```

Awesome! Nu har vi sett exempel på hur man skapar en kontext för olika databassystem. I varje exempel skapar vi en ny klass som ärver från DbContext och konfigurerar anslutningssträngen i OnConfiguring-metoden.

I SQL Server-exemplet använder vi `UseSqlServer`-metoden för att ange att vi vill använda SQL Server som databassystem.

I SQLite-exemplet använder vi `UseSqlite`-metoden för att ange att vi vill använda SQLite som databassystem.

I MySQL-exemplet använder vi `UseMySql`-metoden från Pomelo.EntityFrameworkCore.MySql-paketet för att ange att vi vill använda MySQL som databassystem. Vi specificerar också vilken version av MySQL vi använder.

Nu är vi redo att skapa och interagera med våra databaskontexter. Najs jobbat!

## Obligatorisk Dad-joke

Varför blev Entity Frameworks DbContext så populär på stand-up scenen?

För att den hade en enastående "Connection" med publiken och kunde hantera många "Entities" samtidigt!

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
