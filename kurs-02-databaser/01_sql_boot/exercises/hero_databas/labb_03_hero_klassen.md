# Labb 3 — Hero-klassen 🟡

## Vad du ska bygga

I labb 2 anropade vi `Insert("heroes", ["Bella", "28", "Malmö"])`. Det fungerar, men det är lätt att blanda ihop ordningen, lätt att missa ett värde och omöjligt att läsa utan att räkna parametrar. Nu introducerar vi en `Hero`-klass och låter `SQLiteHandler` jobba med objekt i stället för lösa strängar.

*Förutsättning: du har `SQLiteHandler` från labb 2.*

## Steg

1. Skapa klassen `Hero` med properties: `int Id`, `string Name`, `int Age`, `string City`. Lägg till en `ToString()`-override som ger exakt formatet i utskriften nedan (`Hero { Id = 3, Name = Zara, Age = 22, City = Stockholm }`).

2. Uppdatera `SQLiteHandler` så att följande metoder tar ett `Hero`-objekt:
   - `Insert(Hero hero)` — istället för `Insert(string table, string[] values)`. Efter insättningen ska `hero.Id` vara satt till radens id (`SELECT last_insert_rowid()`).
   - `Update(Hero hero)` — uppdaterar via `hero.Id`
   - `Find(string name)` — returnerar nu ett `Hero`-objekt (eller `null`)
   - `Search(string token)` — returnerar `List<Hero>`
   - `Delete(int id)` och `Delete(string name)` — oförändrade
   - `DropTable()` — tar bort `hero`-tabellen om den finns

3. Låt `CreateTable` generera sin SQL från ett `Hero`-objekt med reflection:
   ```csharp
   handler.CreateTable(new Hero());
   ```
   Tabellen ska skapas baserat på `Hero`s properties och deras typer (`int` blir `INTEGER`, `string` blir `TEXT`, `Id` blir `INTEGER PRIMARY KEY AUTOINCREMENT`). SQL-strängen får inte vara hårdkodad. Tabellens namn är klassnamnet i gemener: `hero`.

4. Uppdatera `Program.cs` för att använda de nya metoderna. Skapa `Hero`-objekt och skicka in dem.

5. Skriv ut resultatet av `Find` och `Search` med `Console.WriteLine`

## Förväntad output

```
=== Databas skapad ===
=== Tabell skapad från Hero-klass ===

=== Infoga heroes ===
Bella inlagd (id: 1)
Carlos inlagd (id: 2)
Zara inlagd (id: 3)

=== Find: Zara ===
Hero { Id = 3, Name = Zara, Age = 22, City = Stockholm }

=== Search: ar ===
Hero { Id = 2, Name = Carlos, Age = 34, City = Göteborg }
Hero { Id = 3, Name = Zara, Age = 22, City = Stockholm }

=== Update: Bella ===
Bella bor nu i Lund.

=== Find: Batman ===
Hittade ingen hero med det namnet.

=== Delete: Carlos ===
Carlos borttagen. Kvar: 2 heroes.
```

## Kriterier

**G**
- `Hero`-klassen finns med rätt properties
- `Insert`, `Update`, `Find` och `Search` använder `Hero`-objekt
- `CreateTable` genererar SQL från `Hero` med reflection, utan att SQL-strängen är hårdkodad i `Program.cs`

**VG**
- Du kan förklara hur reflection läser `Hero`s properties och varför det förenklar `CreateTable`
- Du kan förklara varför `Insert(hero)` är bättre än `Insert("heroes", ["Bella", "28", "Malmö"])`
- `Find` returnerar `null` när hero inte hittas, och `Program.cs` hanterar det
