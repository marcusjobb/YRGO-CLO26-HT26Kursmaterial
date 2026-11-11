# Labb 5 — Generics 🔴

## Vad du ska bygga

Heroes ska nu ha husdjur. En `Pet` liknar en `Hero`: ett objekt med properties, samma CRUD-operationer, samma sökbehov. Om vi skapar `SQLitePetHandler` och `TiDBPetHandler` kopierar vi allt vi redan gjort. Det är inte en lösning, det är ett problem. Svaret är generics: `IDbHandler<T>`.

*Förutsättning: du har `IDbHandler` och båda handlers från labb 4.*

## Steg

1. Skapa klassen `Pet` med properties: `int Id`, `string Name`, `string Type`, `int HeroId`

2. Lägg till en property `List<Pet> Pets` i `Hero`-klassen. `ToString()` på `Hero` ska fortfarande skriva ut bara `Id`, `Name`, `Age` och `City`.

3. Uppdatera `IDbHandler` till att bli generisk. Två metoder är nya jämfört med labb 4: `FindBy` (hitta rader via valfri kolumn) behövs för att hämta en heros husdjur, och `CreateDB`/`DropTable` följer med från tidigare:
   ```csharp
   public interface IDbHandler<T>
   {
       void CreateDB(string sql);
       void DropTable();
       void CreateTable(T model);
       void Insert(T model);
       void Delete(int id);
       void Delete(string name);
       void Update(T model);
       T? Find(string name);
       List<T> FindBy(string column, object value);
       List<T> Search(string token);
   }
   ```

4. Uppdatera `SQLiteHandler` till `SQLiteHandler<T>` och `TiDBHandler` till `TiDBHandler<T>`. Du behöver `where T : new()` så att handlern kan skapa objekt av raderna. Använd reflection för att generera SQL från `T`s properties (samma princip som `CreateTable` i labb 3). Tabellens namn är klassnamnet i gemener (`hero`, `pet`).

5. **Bara enkla typer blir kolumner:** `int`, `string`, `double`, `bool` och `DateTime`. `Hero.Pets` är en `List<Pet>` och ska hoppas över. *Diskussion: varför kan vi inte lägga en lista i en kolumn? Hur ser relationen mellan hero och pet ut i databasen?*

6. `FindBy(string column, object value)` får inte skicka in kolumnnamnet rakt i SQL. Kontrollera att kolumnen finns bland `T`s properties först. Kolumnnamn kan inte vara SQL-parametrar.

7. I `Program.cs`: skapa en handler för Hero och en för Pet:
   ```csharp
   IDbHandler<Hero> heroHandler = new SQLiteHandler<Hero>(connectionString);
   IDbHandler<Pet> petHandler = new SQLiteHandler<Pet>(connectionString);
   ```

8. Kör: skapa tabellerna, infoga heroes, infoga husdjur kopplade till rätt hero, hämta en hero och ladda dess husdjur med `FindBy`:
   ```csharp
   hero.Pets = petHandler.FindBy(nameof(Pet.HeroId), hero.Id);
   ```
   Du gör det för hand i `Program.cs`, och det är avsiktligt. Lägg märke till hur snabbt den raden kommer att behövas på fler ställen. Det löser vi i labb 6.

9. Husdjuren: Mjao (Katt, tillhör Bella), Woff (Hund, tillhör Bella) och Sture (Orm, tillhör Carlos).

## Förväntad output

```
=== Databas skapad ===
=== Tabeller skapade ===

=== Heroes ===
Bella inlagd (id: 1)
Carlos inlagd (id: 2)

=== Pets ===
Mjao inlagd (id: 1, heroId: 1)
Woff inlagd (id: 2, heroId: 1)
Sture inlagd (id: 3, heroId: 2)

=== Find: Bella ===
Hero { Id = 1, Name = Bella, Age = 28, City = Malmö }
  Pet: Mjao (Katt)
  Pet: Woff (Hund)

=== Search pets: o ===
Mjao, Katt (heroId: 1)
Woff, Hund (heroId: 1)

=== Delete: Sture ===
Sture borttagen. Carlos har nu 0 husdjur.

=== Delete via id: Carlos ===
Carlos borttagen. Find ger: ingen hero
```

Sökningen `o` matchar bara husdjurens *namn*: Mjao och Woff innehåller ett `o`, Sture gör det inte.

## Kriterier

**G**
- `IDbHandler<T>` implementeras av `SQLiteHandler<T>`
- Separat handler-instans för `Hero` och `Pet`
- Insert, Find, FindBy och Search fungerar för båda typerna
- `List<Pet>` blir ingen kolumn

**VG**
- `TiDBHandler<T>` fungerar på samma sätt som `SQLiteHandler<T>` (byt klass i `Program.cs`, samma utskrift)
- `CreateTable` genererar rätt SQL för både `Hero` och `Pet` via reflection
- `FindBy` kontrollerar kolumnnamnet mot `T`s properties
- Du kan förklara varför generics är bättre än att skriva `HeroHandler` och `PetHandler` separat
- Du kan beskriva vad `<T>` innebär och vad som händer vid kompilering
- Du kan peka på vad som fortfarande är kopierat mellan `SQLiteHandler<T>` och `TiDBHandler<T>`, och föreslå hur man kunde minska det
