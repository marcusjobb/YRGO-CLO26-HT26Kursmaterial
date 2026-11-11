# Mira flyttar in i en databas (EF Core)

Det här är läsversionen av måndagens första lektion. Förra veckan sparade vi Mira i en JSON-fil. Idag flyttar hon in i en riktig databas, och vi skriver ingen SQL själva.

## Varför inte bara en fil?

En JSON-fil fungerar bra för en hjälte. Med tusen hjältar blir det jobbigt:

- Ska du ändra **en** hjälte måste hela filen läsas in och skrivas om.
- Ska du hitta alla med mer än 50 i hälsa måste du läsa **alla** först.
- Skriver två program samtidigt skriver det ena över det andra.

En databas är byggd för just det här. Den kan söka, ändra en enda rad och hålla ordning när flera skriver samtidigt.

## EF Core är tolken

EF Core (Entity Framework Core) står mellan din C#-kod och databasen. Du arbetar med vanliga objekt, och EF Core översätter till SQL.

```mermaid
flowchart LR
    A["C#-objekt<br/>new Hero { Name = &quot;Mira&quot; }"] -->|EF Core översätter| B["Tabell Heroes<br/>Id 1 · Mira · 100 · 1"]
```

Det finns tre delar:

| Del | Vad den gör |
|-----|-------------|
| `Hero` | Klassen. Den blir en tabell. |
| `GameContext` | Kontakten med databasen. |
| `game.db` | Själva databasen. SQLite sparar allt i en vanlig fil. |

## Först: verktygslådan (NuGet-paket)

EF Core följer inte med .NET från början. Vi hämtar det som NuGet-paket. **NuGet** är färdiga kodpaket som andra har skrivit, ungefär som appar i en appbutik. Du lägger till dem i ditt projekt med ett kommando.

Skapa projektet och lägg till paketen innan du skriver någon kod:

```bash
dotnet new console -n HeroDb
cd HeroDb
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet tool install --global dotnet-ef     # en gång per dator
```

- `Microsoft.EntityFrameworkCore.Sqlite` är EF Core, och den del som pratar med SQLite-databaser.
- `Microsoft.EntityFrameworkCore.Design` behövs för att kunna skapa migrationer.
- `dotnet-ef` är verktyget som ger dig kommandot `dotnet ef`. Det installeras en gång per dator, inte per projekt. Står det att verktyget redan är installerat är allt som det ska.

Vill du kontrollera att paketen kom med? Öppna `HeroDb.csproj`. Där ska det finnas två rader som börjar med `<PackageReference`.

## Klassen och kontexten

```csharp
class Hero
{
    public int Id { get; set; }               // Id blir primärnyckel automatiskt
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Level { get; set; }
}

class GameContext : DbContext
{
    public DbSet<Hero> Heroes => Set<Hero>();   // tabellen Heroes

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=game.db");
}
```

En property som heter `Id` blir automatiskt nyckeln i tabellen. `DbSet<Hero>` är tabellen. `UseSqlite` säger vilken databas och vilken fil.

Klasserna ska ligga **under** koden i `Program.cs`. Står de ovanför får du felet CS8803.

## Receptet

Paketen är på plats och klasserna är skrivna. Nu gör du ritningen, en gång per projekt:

```bash
dotnet ef migrations add Init              # ritningen av databasen
dotnet run
```

En **migration** är en ritning: "så här ska tabellerna se ut". EF Core skapar den genom att läsa dina klasser. Den hamnar i mappen `Migrations/`. Ändrar du en klass gör du en ny migration med ett nytt namn.

## Öppna databasen

```csharp
using Microsoft.EntityFrameworkCore;

// Öppna databasen. Migrate() skapar filen game.db första gången
// och lägger till nya tabeller när du har gjort en ny migration.
using var db = new GameContext();
db.Database.Migrate();
```

`Migrate()` bygger databasen efter ritningen när programmet startar. Därför behöver du inget extra kommando för att uppdatera databasen.

## Spara

Att spara är två steg. Först lägger du till objektet, sedan sparar du:

```csharp
// SPARA: lägg till ett objekt och spara
var mira = new Hero { Name = "Mira", Health = 100, Level = 1 };
db.Heroes.Add(mira);
db.SaveChanges();
Console.WriteLine($"Sparade {mira.Name} med Id {mira.Id}");
```

Lägg märke till att du aldrig sätter `Id`. Databasen ger Mira nummer 1, och EF Core skriver tillbaka det i objektet.

## Titta in i databasen med DB Browser

Du behöver inte lita blint på EF Core. Du kan titta in i databasen själv med programmet **DB Browser for SQLite**. Det är gratis och finns för Windows, Mac och Linux på [sqlitebrowser.org](https://sqlitebrowser.org).

Var ligger filen? Koden säger `UseSqlite("Data Source=game.db")`. Det är en sökväg utan mapp, så filen hamnar i den mapp programmet körs från:

- Kör du `dotnet run` i projektmappen hamnar `game.db` i mappen `HeroDb`.
- Startar du med knappen i Visual Studio hamnar den i `bin/Debug/net10.0/`.

Öppna filen med **Open Database** och titta:

1. Fliken **Database Structure** visar tabellen `Heroes` och dess kolumner `Id`, `Name`, `Health` och `Level`. Där finns också tabellen `__EFMigrationsHistory`. I den skriver EF Core upp vilka migrationer som redan är körda, till exempel `Init`.
2. Fliken **Browse Data** visar raderna. Välj tabellen `Heroes` och tryck uppdatera. Syns Mira?

Prova gärna igen efter att du ändrat och tagit bort hjältar nedan. Tryck uppdatera i **Browse Data** varje gång.

Två vänliga tips:

- Stäng databasen i DB Browser (**Close Database**) innan du kör programmet efter en ny migration. Annars kan filen vara låst.
- Ändra inte data i DB Browser medan programmet körs. Har du ändrat något: tryck **Write Changes** eller **Revert Changes**, så att inget hänger kvar.

## Läsa, ändra och ta bort

Att läsa alla hjältar ser ut som en vanlig lista:

```csharp
// LÄSA: hämta alla hjältar
foreach (Hero hero in db.Heroes.ToList())
{
    Console.WriteLine($"{hero.Id}: {hero.Name}, {hero.Health} hälsa, nivå {hero.Level}");
}
```

För att ändra hämtar du objektet, ändrar det som vanligt och sparar:

```csharp
// HITTA OCH ÄNDRA: ändra objektet och spara igen
Hero? found = db.Heroes.Find(mira.Id);
if (found != null)
{
    found.Level = 2;
    db.SaveChanges();
    Console.WriteLine($"{found.Name} är nu nivå {found.Level}");
}
```

`Find` letar med nyckeln. Hittas ingen får du `null`, och därför kollar vi med `if`. Titta i **Browse Data** i DB Browser och tryck uppdatera. Står Mira på nivå 2 nu?

Att ta bort är också två steg:

```csharp
// TA BORT
db.Heroes.Remove(mira);
db.SaveChanges();
Console.WriteLine($"Hjältar kvar: {db.Heroes.Count()}");
```

## LINQ blir SQL

Du kan använda LINQ precis som på listor. EF Core gör om det till SQL:

```csharp
// FILTRERA: LINQ blir SQL
var strong = db.Heroes.Where(h => h.Health >= 50);
Console.WriteLine(strong.ToQueryString());
```

```text
SELECT "h"."Id", "h"."Health", "h"."Level", "h"."Name"
FROM "Heroes" AS "h"
WHERE "h"."Health" >= 50
```

Det är samma sorts SQL som i databaskursen. Den här gången skrev EF Core den åt dig.

## Sammanfattning

1. En klass blir en tabell, och `Id` blir nyckeln.
2. `DbContext` är kontakten och `DbSet` är tabellen.
3. Ny eller ändrad klass: `dotnet ef migrations add <Namn>`.
4. `Add`, `Find` och `Remove`, och sedan alltid `SaveChanges()`.

## Hela koden

```csharp
using Microsoft.EntityFrameworkCore;

// Öppna databasen. Migrate() skapar filen game.db första gången
// och lägger till nya tabeller när du har gjort en ny migration.
using var db = new GameContext();
db.Database.Migrate();

// SPARA: lägg till ett objekt och spara
var mira = new Hero { Name = "Mira", Health = 100, Level = 1 };
db.Heroes.Add(mira);
db.SaveChanges();
Console.WriteLine($"Sparade {mira.Name} med Id {mira.Id}");

// LÄSA: hämta alla hjältar
foreach (Hero hero in db.Heroes.ToList())
{
    Console.WriteLine($"{hero.Id}: {hero.Name}, {hero.Health} hälsa, nivå {hero.Level}");
}

// HITTA OCH ÄNDRA: ändra objektet och spara igen
Hero? found = db.Heroes.Find(mira.Id);
if (found != null)
{
    found.Level = 2;
    db.SaveChanges();
    Console.WriteLine($"{found.Name} är nu nivå {found.Level}");
}

// FILTRERA: LINQ blir SQL
var strong = db.Heroes.Where(h => h.Health >= 50);
Console.WriteLine(strong.ToQueryString());

// TA BORT
db.Heroes.Remove(mira);
db.SaveChanges();
Console.WriteLine($"Hjältar kvar: {db.Heroes.Count()}");

class Hero
{
    public int Id { get; set; }               // Id blir primärnyckel automatiskt
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Level { get; set; }
}

class GameContext : DbContext
{
    public DbSet<Hero> Heroes => Set<Hero>();   // tabellen Heroes

    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite("Data Source=game.db");
}
```
