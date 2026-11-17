# Labb 2 — SQLiteHandler 🟢

## Vad du ska bygga

Labb 1 fungerade — men `Program.cs` var ett enda kaos av SQL-strängar. Nu lyfter vi ut all databaslogik till en egen klass: `SQLiteHandler`. `Program.cs` ska inte längre veta hur SQL fungerar, bara vad den vill göra.

*Förutsättning: du har NuGet-paketen och `appsettings.json` från labb 1.*

## Steg

1. Fortsätt i samma projekt eller skapa ett nytt (samma NuGet-paket och `appsettings.json`-setup som labb 1)

2. Skapa klassen `SQLiteHandler` med konstruktor som tar en `connectionString`

3. Implementera följande metoder:

   | Method | Parametrar | Beskrivning |
   |--------|-----------|-------------|
   | `CreateDB` | `string sql` | Kör SQL mot databasen om `sql` inte är tom. SQLite skapar filen själv vid första anslutning, så här skickar vi en tom sträng. |
   | `CreateTable` | `string sql` | Skapar en tabell från en SQL-sträng |
   | `DropTable` | `string table` | Tar bort tabellen om den finns, så att programmet kan köras om |
   | `Insert` | `string table, string[] values` | Infogar en rad (värdena i kolumnernas ordning, utan `id`) |
   | `Delete` | `string table, int id` | Tar bort en rad via id |
   | `Delete` | `string table, string name` | Tar bort en rad via namn |
   | `Update` | `string table, int id, string[] values` | Uppdaterar en rad via id |
   | `Update` | `string table, string name, string[] values` | Uppdaterar en rad via namn |
   | `Find` | `string name` | Returnerar en hero som en sträng, eller `null` om ingen hittas |
   | `Search` | `string token` | Returnerar en lista med matchande namn |

4. Tabellnamn och kolumnnamn kan inte skickas som SQL-parametrar. Fundera: hur skyddar du dig mot att någon skickar in `"heroes; DROP TABLE heroes"` som tabellnamn?

5. I `Program.cs`: läs connectionstring från `appsettings.json`, skapa en instans av `SQLiteHandler` och anropa metoderna. `Program.cs` ska inte innehålla någon SQL förutom `CREATE TABLE`-strängen som du skickar in.

6. Lägg märke till `Insert("heroes", ["Bella", "28", "Malmö"])`. Allt är strängar, och ordningen måste gissas. Vad händer om du byter plats på två värden? Spara frågan till labb 3.

## Förväntad output

```
=== Databas skapad ===
=== Tabell skapad ===

=== Infoga heroes ===
Bella inlagd.
Carlos inlagd.
Zara inlagd.

=== Find: Bella ===
Hittade: Bella, 28 år, Malmö

=== Search: ar ===
Carlos
Zara

=== Update: Bella bor nu i Lund ===
Uppdaterad.
Hittade: Bella, 28 år, Lund

=== Update via id: Zara fyller 23 ===
Hittade: Zara, 23 år, Stockholm

=== Delete: Carlos ===
Borttagen.
Hittade: ingen hero med det namnet

=== Delete via id: Zara ===
Borttagen.
Hittade: ingen hero med det namnet
```

## Kriterier

**G**
- `SQLiteHandler` innehåller alla metoder i tabellen ovan
- `Program.cs` innehåller ingen SQL förutom `CREATE TABLE`-strängen
- Alla operationer fungerar korrekt och programmet kan köras om med samma utskrift

**VG**
- Metoder är välnamngivna och gör exakt en sak
- Tabellnamn kontrolleras innan de byggs in i SQL, och värden skickas som parametrar
- Du kan förklara varför det är bättre att samla SQL-logiken i en klass
- Överlagring av `Delete` och `Update` används korrekt
