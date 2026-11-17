# Inlämning 2 — Din filmdatabas som app

**Individuell uppgift**

---

## Bakgrunden

I inlämning 1 designade och byggde du en diskografi-databas för hand. Den här gången är
databasen given — schemat och skapandet av tabeller får du färdigt nedan. Din uppgift är att
bygga en **C#-applikation** som pratar med den, inte att designa den.

Fokus ligger på **kod som pratar med en databas genom ett rent lager**, inte på databasdesign —
det övade du redan på i inlämning 1.

Du ska bygga en konsolapp med en meny där användaren kan bläddra och söka bland filmer och deras
rollistor — och du ska använda **Facade-mönstret** för att dölja hur databasen faktiskt funkar.

---

## Vad är ett Facade-mönster?

En Facade är ett lager som gömmer komplexitet bakom en enkel yta.

```
Meny (Program.cs)
    ↓ anropar
FilmFacade
    ↓ anropar
SQLite-queries
    ↓ pratar med
filmdatabas.db
```

`Program.cs` vet inte att det finns en databas. Den vet bara att den kan anropa
`facade.GetAllMovies()` och få tillbaka en lista.

```csharp
// Utan facade — logik och databas blandas ihop i menyn
var connection = new SqliteConnection("Data Source=filmdatabas.db");
connection.Open();
var command = connection.CreateCommand();
command.CommandText = "SELECT * FROM movies ORDER BY release_year";
// ... i menykoden

// Med facade — menyn frågar, facade svarar
var movies = facade.GetAllMovies();
foreach (var movie in movies)
    Console.WriteLine($"{movie.Year} — {movie.Title}");
```

---

## Databasen (given — samma för alla)

Schema:

```sql
CREATE TABLE Director (
    Id INTEGER PRIMARY KEY,
    Name TEXT NOT NULL
);

CREATE TABLE Movie (
    Id INTEGER PRIMARY KEY,
    DirectorId INTEGER NOT NULL,
    Title TEXT NOT NULL,
    ReleaseYear INTEGER NOT NULL,
    FOREIGN KEY (DirectorId) REFERENCES Director(Id)
);

CREATE TABLE Role (
    Id INTEGER PRIMARY KEY,
    MovieId INTEGER NOT NULL,
    CharacterName TEXT NOT NULL,
    ActorName TEXT NOT NULL,
    FOREIGN KEY (MovieId) REFERENCES Movie(Id)
);
```

Du väljer själv **vilken regissör och filmografi** databasen ska innehålla — precis som du
valde din egen artist i inlämning 1. Fyll `Director`, `Movie` och `Role` med minst 5 filmer och
minst 3 roller per film. Exemplen nedan använder Christopher Nolans filmografi, men det är bara
ett exempel — hitta på ditt eget om du vill.

---

## Krav

### G — Godkänt

Konsolappen ska ha en meny med minst dessa alternativ:

```
=== Din Regissörs Filmografi ===
1. Visa alla filmer
2. Visa rollista för en film
3. Sök film på titel
4. Avsluta
```

- Alla databasanrop går genom en `FilmFacade`-klass
- Databasen är SQLite, enligt schemat ovan
- Appen kraschar inte om användaren matar in fel

### VG — Väl godkänt

Allt i G, plus:

- [ ] Lägg till en roll i en film via menyn (INSERT)
- [ ] Ta bort en roll via menyn (DELETE)
- [ ] Facade-klassen har separata metoder per operation — inga "gör allt"-metoder
- [ ] Du kan muntligt förklara varför Facade-mönstret används och vad som hade hänt utan det

---

## Förväntad körning

```
=== Christopher Nolans Filmografi ===
1. Visa alla filmer
2. Visa rollista för en film
3. Sök film på titel
4. Lägg till roll         (VG)
5. Ta bort roll           (VG)
6. Avsluta

Val: 1

Filmer i ordning:
  2000 — Memento              (3 roller)
  2005 — Batman Begins        (4 roller)
  2006 — The Prestige         (3 roller)
  2008 — The Dark Knight      (4 roller)
  2010 — Inception            (5 roller)
  ...

Val: 2
Ange filmtitel: Inception

Rollista för Inception:
  Dom Cobb        spelas av Leonardo DiCaprio
  Arthur          spelas av Joseph Gordon-Levitt
  Ariadne         spelas av Elliot Page
  ...

Val: 3
Ange del av titel: dark

Träffar:
  2008 — The Dark Knight
  2012 — The Dark Knight Rises
```

---

## Kom igång

Skapa projektet:

```bash
dotnet new console -n FilmdatabasApp
cd FilmdatabasApp
dotnet add package Microsoft.Data.Sqlite
```

---

## Startstruktur (given — kopiera rakt in)

Alla filer nedan är färdiga att använda. Du ska **inte** ändra strukturen — bara fylla i SQL-frågorna och resultatmappningen på de markerade ställena (`// ← DIN SQL HÄR` och `// ← MAPPA RESULTATET HÄR`).

### Movie.cs

```csharp
public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; } = "";
    public int ReleaseYear { get; set; }
    public string DirectorName { get; set; } = "";
    public int RoleCount { get; set; }
}
```

### Role.cs

```csharp
public class Role
{
    public int Id { get; set; }
    public string CharacterName { get; set; } = "";
    public string ActorName { get; set; } = "";
}
```

### Program.cs

```csharp
var facade = new FilmFacade("filmdatabas.db");

while (true)
{
    Console.WriteLine("\n=== Din Regissörs Filmografi ===");
    Console.WriteLine("1. Visa alla filmer");
    Console.WriteLine("2. Visa rollista för en film");
    Console.WriteLine("3. Sök film på titel");
    Console.WriteLine("4. Avsluta");
    Console.Write("\nVal: ");
    var val = Console.ReadLine();

    if (val == "1")
    {
        var filmer = facade.GetAllMovies();
        foreach (var f in filmer)
            Console.WriteLine($"  {f.ReleaseYear} — {f.Title} ({f.RoleCount} roller)");
    }
    else if (val == "2")
    {
        Console.Write("Ange filmtitel: ");
        var titel = Console.ReadLine() ?? "";
        var roller = facade.GetRolesByMovie(titel);
        foreach (var r in roller)
            Console.WriteLine($"  {r.CharacterName} spelas av {r.ActorName}");
    }
    else if (val == "3")
    {
        Console.Write("Ange del av titel: ");
        var sokord = Console.ReadLine() ?? "";
        var träffar = facade.SearchMovies(sokord);
        foreach (var f in träffar)
            Console.WriteLine($"  {f.ReleaseYear} — {f.Title}");
    }
    else if (val == "4")
    {
        break;
    }
}
```

### FilmFacade.cs

```csharp
using Microsoft.Data.Sqlite;

public class FilmFacade
{
    private readonly string _connectionString;

    public FilmFacade(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
    }

    public List<Movie> GetAllMovies()
    {
        var result = new List<Movie>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = // ← DIN SQL HÄR
            // Tips: JOIN Movie med Director, COUNT roller per film, ORDER BY ReleaseYear
            "";

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Movie
            {
                // ← MAPPA RESULTATET HÄR
                // Tips: reader.GetInt32(0), reader.GetString(1), osv.
            });
        }

        return result;
    }

    public List<Role> GetRolesByMovie(string movieTitle)
    {
        var result = new List<Role>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = // ← DIN SQL HÄR
            // Tips: JOIN Role med Movie WHERE Movie.Title = @title
            "";
        command.Parameters.AddWithValue("@title", movieTitle);

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Role
            {
                // ← MAPPA RESULTATET HÄR
            });
        }

        return result;
    }

    public List<Movie> SearchMovies(string titlePart)
    {
        var result = new List<Movie>();

        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        using var command = connection.CreateCommand();
        command.CommandText = // ← DIN SQL HÄR
            // Tips: WHERE Movie.Title LIKE @search — glöm inte %-tecknen runt sökordet
            "";
        command.Parameters.AddWithValue("@search", $"%{titlePart}%");

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            result.Add(new Movie
            {
                // ← MAPPA RESULTATET HÄR
            });
        }

        return result;
    }
}
```

---

## Inlämning

GitHub-repo med:

1. Komplett C#-projekt som går att köra
2. Din `filmdatabas.db` inkluderad (eller `seed.sql` för att skapa och fylla den utifrån schemat ovan)
3. `README.md` med: hur man kör projektet och en kort förklaring av din Facade

**Deadline:** annonseras på Classroom
