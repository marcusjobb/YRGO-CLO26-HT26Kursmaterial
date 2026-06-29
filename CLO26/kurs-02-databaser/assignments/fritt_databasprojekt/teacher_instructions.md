# Instruktioner för lärare – Fritt databasprojekt

## Översikt

Denna uppgift låter studenterna välja ämne, frontend och databasbackend själva. Målet är att efterlikna en verklig utvecklingssituation där en developer måste göra arkitekturval baserat på projektets behov.

## Tre olika backends – olika svårighetsgrad

| Backend | Svårighet | Typiskt för |
|---------|-----------|-------------|
| SQLite | Lätt–Medel | Student som vill fokusera på SQL och enkel filbaserad lagring |
| EF Core + LocalDB | Medel | Student som vill lära sig ORM och LINQ |
| MongoDB | Medel–Svår | Student som är nyfiken på NoSQL och molntjänster |

## Bedömning

### För G

- ≥3 tabeller med logiska relationer
- Full CRUD
- Fungerande UI (console eller grafiskt)
- README med screenshots
- Felhantering

### För VG

Kräver minst 2 av VG-funktionerna. Repository-mönster med interface är starkt rekommenderat som bas för VG.

## Vanliga fallgropar att titta efter

- Allt i Program.cs / MainWindow.xaml.cs
- Ingen felhantering (app kraschar vid felaktig input)
- Hårdkodade sökvägar
- SQL-injektion (SQLite-alternativet)
- Ingen användning av `.Include()` för relationer i EF Core
- MongoDB: alla dokument i en enda collection

## Att tänka på

- **GitHub Classroom** fungerar bra för denna uppgift – skapa ett template-repo
- **MongoDB Atlas free tier** kräver registrering – påminn studenterna att göra detta i förväg
- **LocalDB** kräver SQL Server LocalDB installerat (ingår i Visual Studio)
- **SQLite** kräver inget extra – bara en NuGet-package
- Studenterna bör få godkänna sitt ämnesval med läraren innan de börjar
