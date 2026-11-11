# Mira i en låda (MongoDB)

Det här är läsversionen av måndagens eftermiddag. Vi tittar på ett annat sätt att spara data: en dokumentdatabas. Målet idag är att förstå skillnaden, inte att bli expert.

## Två sätt att förvara saker

En SQL-databas är som en byrå. Varje låda har fack med namn, och allt måste passa i facken. Det bestäms i förväg, med en migration.

En dokumentdatabas är som en hylla med kartonger. Varje kartong är ett **dokument**. Du lägger i det som behövs, och två kartonger behöver inte innehålla samma saker.

MongoDB är den vanligaste dokumentdatabasen.

## Ni kan redan läsa ett dokument

Ett dokument i MongoDB ser ut nästan som JSON-filen från förra veckan:

```json
{
  "_id": "6730f1c2a4b5c6d7e8f90123",
  "Name": "Mira",
  "Level": 2,
  "Items": ["Svärd", "Karta", "Lykta"]
}
```

Lägg märke till listan `Items`. Den ligger **inne i** dokumentet. I en SQL-databas hade den blivit en egen tabell med en främmande nyckel, precis som uppdragen i förra lektionen.

## Samma hjälte, två världar

| | EF Core + SQLite | MongoDB |
|---|---|---|
| Sparar | rader i tabeller | dokument i samlingar |
| Struktur | bestäms i förväg (migration) | bestäms när du sparar |
| Listor | egen tabell + främmande nyckel | direkt i dokumentet |
| Bra när | data hänger ihop och måste stämma | data varierar och ska läsas snabbt |

Ingen av dem är bättre. De är bra på olika saker. Ett bokningssystem där inget får dubbelbokas passar bäst i SQL. En spelares inventarie, som ser olika ut för varje spelare, passar bra i ett dokument.

## Koden

Klassen behöver ett `Id` av typen `ObjectId`. MongoDB skapar det åt dig:

```csharp
class Hero
{
    public ObjectId Id { get; set; }                 // MongoDB skapar Id åt dig
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public List<string> Items { get; set; } = new(); // en lista ryms direkt i dokumentet
}
```

Det finns ingen `DbContext` och ingen migration. Du ansluter och sparar direkt:

```csharp
// Anslut till MongoDB som körs i Docker på din dator
// Tänk på att lägga till Try Catch
var client = new MongoClient("mongodb://localhost:27017");
IMongoDatabase db = client.GetDatabase("game");
IMongoCollection<Hero> heroes = db.GetCollection<Hero>("heroes");

// SPARA: inget schema, ingen migration. Dokumentet sparas som det är.
var mira = new Hero
{
    Name = "Mira",
    Level = 2,
    Items = new List<string> { "Svärd", "Karta", "Lykta" }
};
heroes.InsertOne(mira);
Console.WriteLine($"Sparade {mira.Name} med Id {mira.Id}");
```

Att hitta, ändra och ta bort känns som LINQ:

```csharp
// LÄSA: samma LINQ-känsla som i EF
List<Hero> found = heroes.Find(h => h.Level >= 2).ToList();
foreach (Hero hero in found)
{
    Console.WriteLine($"{hero.Name}, nivå {hero.Level}, bär: {string.Join(", ", hero.Items)}");
}

// ÄNDRA: sätt ett nytt värde på ett fält
heroes.UpdateOne(h => h.Id == mira.Id, Builders<Hero>.Update.Set(h => h.Level, 3));

// TA BORT
heroes.DeleteOne(h => h.Id == mira.Id);
Console.WriteLine($"Hjältar kvar: {heroes.CountDocuments(h => true)}");
```

Det finns ingen `SaveChanges()`. Varje anrop sparas direkt i databasen.

## Starta MongoDB

MongoDB är en server. Det enklaste är att köra den i Docker:

```bash
docker run -d --name mongo -p 27017:27017 mongo
dotnet add package MongoDB.Driver
```

Överst i `Program.cs` behöver du `using MongoDB.Bson;` och `using MongoDB.Driver;`.

## Sammanfattning

1. En SQL-databas har tabeller och relationer, och strukturen bestäms i förväg.
2. MongoDB sparar dokument, och listor ryms inne i dokumentet.
3. Hotel Grandioso byggs med EF Core, eftersom bokningar måste stämma.

## Hela koden

```csharp
using MongoDB.Bson;
using MongoDB.Driver;

// Anslut till MongoDB som körs i Docker på din dator
// Tänk på att lägga till Try Catch
var client = new MongoClient("mongodb://localhost:27017");
IMongoDatabase db = client.GetDatabase("game");
IMongoCollection<Hero> heroes = db.GetCollection<Hero>("heroes");

// SPARA: inget schema, ingen migration. Dokumentet sparas som det är.
var mira = new Hero
{
    Name = "Mira",
    Level = 2,
    Items = new List<string> { "Svärd", "Karta", "Lykta" }
};
heroes.InsertOne(mira);
Console.WriteLine($"Sparade {mira.Name} med Id {mira.Id}");

// LÄSA: samma LINQ-känsla som i EF
List<Hero> found = heroes.Find(h => h.Level >= 2).ToList();
foreach (Hero hero in found)
{
    Console.WriteLine($"{hero.Name}, nivå {hero.Level}, bär: {string.Join(", ", hero.Items)}");
}

// ÄNDRA: sätt ett nytt värde på ett fält
heroes.UpdateOne(h => h.Id == mira.Id, Builders<Hero>.Update.Set(h => h.Level, 3));

// Vilka bär en karta?
List<Hero> withMap = heroes.Find(h => h.Items.Contains("Karta")).ToList();
Console.WriteLine($"Med karta: {withMap.Count}");

// TA BORT
heroes.DeleteOne(h => h.Id == mira.Id);
Console.WriteLine($"Hjältar kvar: {heroes.CountDocuments(h => true)}");

class Hero
{
    public ObjectId Id { get; set; }                 // MongoDB skapar Id åt dig
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public List<string> Items { get; set; } = new(); // en lista ryms direkt i dokumentet
}
```
