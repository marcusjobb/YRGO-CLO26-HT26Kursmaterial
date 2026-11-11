---
marp: true
theme: nion-dark
paginate: true
---

# Mira i en låda: MongoDB

**Kurs:** Fördjupad OOP i C#
**Modul:** 01 Spara och ladda data (dokumentdatabas)

Marcus Ackre Medina · YRGO · CLO26

---

## Två sätt att förvara saker

**SQL-databas:** en byrå med lådor. Varje låda har fack med namn. Allt måste passa i facken.

**Dokumentdatabas:** en hylla med kartonger. Varje kartong är ett dokument. Du lägger i det som behövs.

---

## Ni kan redan läsa ett dokument

```json
{
  "_id": "6730f1c2a4b5c6d7e8f90123",
  "Name": "Mira",
  "Level": 2,
  "Items": ["Svärd", "Karta", "Lykta"]
}
```

Minns ni katterna som vi sparade i en JSON-fil förra onsdagen? Där gjorde vi redan CRUD: spara, läsa, ändra och ta bort.
MongoDB sparar nästan exakt så, fast i en riktig databas i stället för i en fil.

Listan `Items` ligger **i** dokumentet. I SQL hade den blivit en egen tabell.

---

## Samma hjälte, två världar

| | EF Core + SQLite | MongoDB |
|---|---|---|
| Sparar | rader i tabeller | dokument i samlingar |
| Struktur | bestäms i förväg (migration) | bestäms när du sparar |
| Listor | egen tabell + främmande nyckel | direkt i dokumentet |
| Bra när | data hänger ihop, måste stämma | data varierar, ska läsas snabbt |

Ingen är bättre. De är bra på olika saker.

---

## Klassen

```csharp
class Hero
{
    public ObjectId Id { get; set; }                 // MongoDB skapar Id åt dig
    public string Name { get; set; } = "";
    public int Level { get; set; }
    public List<string> Items { get; set; } = new(); // en lista ryms direkt i dokumentet
}
```

Ingen `DbContext`. Ingen migration.

---

## Spara och läsa

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
```

---

## Hitta, ändra, ta bort

```csharp
// LÄSA: samma LINQ-känsla som i EF
List<Hero> found = heroes.Find(h => h.Level >= 2).ToList();

// ÄNDRA: sätt ett nytt värde på ett fält
heroes.UpdateOne(h => h.Id == mira.Id, Builders<Hero>.Update.Set(h => h.Level, 3));

// TA BORT
heroes.DeleteOne(h => h.Id == mira.Id);
```

Ingen `SaveChanges()`. Varje anrop sparas direkt.

---

## Starta MongoDB på din dator

```bash
docker run -d --name mongo -p 27017:27017 mongo
dotnet add package MongoDB.Driver
```

Har du inte Docker? Då räcker det att du förstår skillnaden idag. Övningens första del görs med papper och penna.

---

## Det här tar du med dig

1. SQL: tabeller och relationer, struktur i förväg.
2. MongoDB: dokument, listor ryms inne i dokumentet.
3. Hotel Grandioso görs med **EF Core**. Bokningar måste stämma, och därför passar relationer bäst där.

**Nu:** Övning, *Mira i en låda* (kort).
