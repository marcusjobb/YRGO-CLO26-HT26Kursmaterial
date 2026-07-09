# EF Core — Övningar

## Förutsättningar

- .NET 8 SDK installerat
- SQLite (kräver ingen installation)
- Bekantskap med CRUD-begrepp

## Övning 1: Skapa databas med Code-First

Skapa en konsolapplikation och en `BloggContext` med dessa entiteter:

```csharp
public class Blogg()
{
    public int Id { get; set; }
    public string? Titel { get; set; }
    public DateTime Skapad { get; set; }
    public List<Inlagg> Inlagg { get; set; } = new();
}

public class Inlagg()
{
    public int Id { get; set; }
    public string? Rubrik { get; set; }
    public string? Innehall { get; set; }
    public int BloggId { get; set; }
    public Blogg? Blogg { get; set; }
}
```

**Instruktioner:**
1. Skapa en konsolapp: `dotnet new console -n EFCoreOvn`
2. Lägg till paket: `dotnet add package Microsoft.EntityFrameworkCore.Sqlite`
3. Skapa `BloggContext` med `DbSet<Blogg>` och `DbSet<Inlagg>`
4. Konfigurera SQLite i `OnConfiguring` med `Data Source=blogg.db`
5. Skapa och kör en migration
6. Verifiera att databasen skapats

**Tips:** Använd `EnsureCreated()` för enkel testning, men migrationer för riktiga projekt.

---

## Övning 2: CRUD mot databasen

I samma projekt, lägg till kod i `Main` som:

1. **Create:** Skapa två bloggar med 2-3 inlägg var
2. **Read:** Hämta och skriv ut alla bloggar med deras inlägg
3. **Update:** Ändra titeln på en blogg
4. **Delete:** Ta bort ett inlägg

**Krav:**
- Använd `SaveChangesAsync()` och `async Main`
- Hantera `DbUpdateException` vid fel
- Använd `Include()` för att ladda inlägg tillsammans med blogg

---

## Övning 3: Migrationer

Lägg till en `Kategori`-egenskap på `Inlagg`:

```csharp
public string? Kategori { get; set; }
```

1. Skapa en migration: `dotnet ef migrations add AddKategori`
2. Granska den genererade koden i `Migrations/`-mappen
3. Uppdatera databasen: `dotnet ef database update`
4. Verifiera att kolumnen finns i databasen (använd t.ex. DB Browser for SQLite)

**Frågor:**
- Vad händer om du glömmer köra `database update`?
- Varför är migrationer bättre än `EnsureCreated()`?

---

## Övning 4: Filtrering och sökning

Bygg vidare. Implementera dessa queries:

1. Hämta alla inlägg från en specifik blogg (filtrera på `BloggId`)
2. Hämta inlägg som innehåller ett specifikt ord i rubriken
3. Hämta bloggar som har minst 2 inlägg
4. Sortera bloggar efter `Skapad`-datum (senaste först)

**Tips:** Använd LINQ: `Where()`, `OrderByDescending()`, `Count()`.

---

## Övning 5: Reflektion

Svara på frågorna i en kommentar i `Program.cs`:

1. Vad är fördelen med EF Core jämfört med ren ADO.NET?
2. När skulle du välja SQLite istället för MySQL/MSSQL?
3. Vad är skillnaden mellan Eager Loading och Lazy Loading?
4. Varför är migrationer viktiga i ett team?
