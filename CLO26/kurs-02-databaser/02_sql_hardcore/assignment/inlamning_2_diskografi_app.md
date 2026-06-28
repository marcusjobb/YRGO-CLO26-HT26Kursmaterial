---
title: Inlämning 2 — Din diskografi som app (SQLite + Facade)
author: Marcus Ackre Medina
type: assignment
topic: databaser
difficulty: 3
language: csharp
status: new
marcus_voice: true
tags: ["sqlite", "csharp", "facade", "databaser", "console", "crud", "individuell"]
---

# Inlämning 2 — Din diskografi som app

**Individuell uppgift**

---

## Bakgrunden

I inlämning 1 designade du en databas och fyllde den med data. Nu ska du bygga en C#-applikation som pratar med den databasen.

Samma data. Samma schema. Men nu via kod.

Du ska bygga en konsolapp med en meny där användaren kan bläddra och söka i diskografin — och du ska använda **Facade-mönstret** för att dölja hur databasen faktiskt funkar.

---

## Vad är ett Facade-mönster?

En Facade är ett lager som gömmer komplexitet bakom en enkel yta.

```
Meny (Program.cs)
    ↓ anropar
MusicFacade
    ↓ anropar
SQLite-queries
    ↓ pratar med
diskografi.db
```

`Program.cs` vet inte att det finns en databas. Den vet bara att den kan anropa `facade.GetAllAlbums()` och få tillbaka en lista.

```csharp
// Utan facade — logik och databas blandas ihop i menyn
var connection = new SqliteConnection("Data Source=music.db");
connection.Open();
var command = connection.CreateCommand();
command.CommandText = "SELECT * FROM albums ORDER BY release_year";
// ... i menykoden

// Med facade — menyn frågar, facade svarar
var albums = facade.GetAllAlbums();
foreach (var album in albums)
    Console.WriteLine($"{album.Year} — {album.Title}");
```

---

## Krav

### G — Godkänt

Konsolappen ska ha en meny med minst dessa alternativ:

```
=== Din Artists Diskografi ===
1. Visa alla album
2. Visa låtar på ett album
3. Sök låt på titel
4. Avsluta
```

- Alla databasanrop går genom en `MusicFacade`-klass
- Databasen är SQLite (samma schema som inlämning 1)
- Appen kraschar inte om användaren matar in fel

### VG — Väl godkänt

Allt i G, plus:

- [ ] Lägg till ett album eller en låt via menyn (INSERT)
- [ ] Ta bort en låt via menyn (DELETE)
- [ ] Facade-klassen har separata metoder per operation — inga "gör allt"-metoder
- [ ] Du kan muntligt förklara varför Facade-mönstret används och vad som hade hänt utan det

---

## Förväntad körning

```
=== Taylor Swift Diskografi ===
1. Visa alla album
2. Visa låtar på ett album
3. Sök låt på titel
4. Lägg till låt        (VG)
5. Ta bort låt          (VG)
6. Avsluta

Val: 1

Album i ordning:
  2006 — Taylor Swift (14 låtar)
  2008 — Fearless (13 låtar)
  2010 — Speak Now (14 låtar)
  2012 — Red (16 låtar)
  2014 — 1989 (13 låtar)
  ...

Val: 2
Ange albumtitel: Folklore

Låtar på Folklore:
  1. the 1               (3:30)
  2. cardigan            (3:59)
  3. the last great american dynasty (3:50)
  ...
```

---

## Kom igång

Skapa projektet:

```bash
dotnet new console -n DiskografiApp
cd DiskografiApp
dotnet add package Microsoft.Data.Sqlite
```

Grundstruktur att utgå från:

```csharp
// MusicFacade.cs
public class MusicFacade
{
    private readonly string _connectionString;

    public MusicFacade(string dbPath)
    {
        _connectionString = $"Data Source={dbPath}";
    }

    public List<Album> GetAllAlbums()
    {
        // din kod
    }

    public List<Song> GetSongsByAlbum(string albumTitle)
    {
        // din kod
    }

    public List<Song> SearchSongs(string titlePart)
    {
        // din kod
    }
}
```

---

## Inlämning

GitHub-repo med:

1. Komplett C#-projekt som går att köra
2. Din `diskografi.db` inkluderad (eller `seed.sql` för att skapa den)
3. `README.md` med: hur man kör projektet och en kort förklaring av din Facade

**Deadline:** Fredag v2 (exakt datum annonseras på Classroom)
