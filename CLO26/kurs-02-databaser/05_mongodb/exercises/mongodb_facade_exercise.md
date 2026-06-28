---

title: MongoDB Facade Pattern - Övning
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/mongodb/mongodb_facade_exercise.md"
description: "Skapa en Facade-klass som förenklar MongoDB-operationer och gör dem återanvändbara."
tags: ["csharp", "databaser", "exercise", "facade", "git", "mongodb", "pattern", "visual-studio", "övning"]
week_fit: []
---

# MongoDB Facade Pattern - Övning

🟢


## Mål
Skapa en Facade-klass som förenklar MongoDB-operationer och gör dem återanvändbara.

**Progressiv approach:**
1. **Först:** Hård typad Facade specifik för `Person`
2. **Sen:** Generisk Facade som fungerar för alla modeller

## Varför Facade?

Istället för att upprepa samma MongoDB-kod överallt:
```csharp
var client = new MongoClient(connectionString);
var database = client.GetDatabase("Person");
var collection = database.GetCollection<Person>("Person");
var result = await collection.Find(_ => true).ToListAsync();
```

Vill vi kunna skriva:
```csharp
var facade = new PersonFacade(connectionString);
var result = await facade.GetAllAsync();
```

---

## 🤔 Varför async/await?

### MongoDB.Driver är 100% asynkron

**Du har inget val!**
- Alla MongoDB-metoder heter `*Async` (InsertOneAsync, FindAsync, etc.)
- Inga synkrona alternativ finns i moderna versioner
- MongoDB-drivern är designad för asynkron kod från grunden

### Vad betyder async/await?

**Synkront (blocking):**
```csharp
// Du ringer ett samtal - kan inte göra annat medan du pratar
var data = HämtaFrånDatabas(); // Programmet står still här!
Console.WriteLine(data);
```

**Asynkront (non-blocking):**
```csharp
// Du skickar SMS - kan göra annat medan du väntar på svar
var data = await HämtaFrånDatabasAsync(); // Programmet kan göra annat!
Console.WriteLine(data);
```

### Fördelar med async

1. **Bättre prestanda** - Tråden blockeras inte
2. **Modern standard** - Alla nya .NET API:er använder async
3. **Skalbarhet** - Kan hantera fler samtidiga användare

### Exempel: MongoDB utan async går inte!

```csharp
// ❌ Finns inte i MongoDB.Driver
var persons = collection.Find(_ => true).ToList();

// ✅ Måste använda async
var persons = await collection.Find(_ => true).ToListAsync();
```

### Kom ihåg i din Facade

Alla metoder måste vara `async` och returnera `Task<T>`:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
// ✅ Rätt
public async Task<List<Person>> GetAllAsync()
{
    return await _collection.Find(_ => true).ToListAsync();
}

// ❌ Fel - försök att undvika async
public List<Person> GetAll()
{
    return _collection.Find(_ => true).ToListAsync().Result; // Farligt!
}
```

**Varför är `.Result` farligt?**
- Kan orsaka deadlocks
- Blockerar tråden (dålig prestanda)
- Anti-pattern i modern C#

---

# STEG 1: Hård Typad Facade (Person)

## Del 1A: Planering - PersonFacade

### Vad ska vår Facade kunna göra?

**CRUD-operationer:**
- Create: Lägg till ett dokument
- Read: Hämta alla, hämta ett, sök med filter
- Update: Uppdatera ett dokument
- Delete: Ta bort ett dokument

**Flexibilitet:**
- Kunna byta databas
- Kunna byta collection
- Fungera för vilken typ som helst (generisk)

### Vilka konstruktorer behöver vi för PersonFacade?

**Alternativ 1: Ta emot connectionstring**
```csharp
public PersonFacade(string connectionString)
```

**Alternativ 2: Ta emot färdig client**
```csharp
public PersonFacade(MongoClient client)
```

**Vi hårdkodar database och collection-namn eftersom det är specifikt för Person!**

## Del 1B: PersonFacade - Klasstruktur

### Skapa den hårda klassen

```csharp
public class PersonFacade
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<Person> _collection;

    // TODO: Lägg till konstruktorer här

    // TODO: Lägg till CRUD-metoder här
}
```

**Fördelar med hård typning:**
- Enkel att förstå
- Ingen generics-komplexitet
- Perfekt för att lära sig Facade-pattern
- Kan returnera `Person` direkt utan casting

## Del 1C: PersonFacade - Implementera konstruktorer

### Uppgift 1C.1: Konstruktor med connectionstring

```csharp
public PersonFacade(string connectionString)
{
    // TODO: Skapa MongoClient
    // TODO: Hämta database "Person"
    // TODO: Hämta collection "Person"
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public PersonFacade(string connectionString)
{
    _client = new MongoClient(connectionString);
    _database = _client.GetDatabase("Person");
    _collection = _database.GetCollection<Person>("Person");
}
```
</details>

### Uppgift 1C.2: Konstruktor med befintlig client

```csharp
public PersonFacade(MongoClient client)
{
    // TODO: Använd client som skickas in
    // TODO: Hämta database "Person"
    // TODO: Hämta collection "Person"
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public PersonFacade(MongoClient client)
{
    _client = client;
    _database = _client.GetDatabase("Person");
    _collection = _database.GetCollection<Person>("Person");
}
```
</details>

### Uppgift 1C.3: DRY med constructor chaining

Kan du undvika kodduplicering mellan de två konstruktorerna?

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public PersonFacade(string connectionString)
    : this(new MongoClient(connectionString))
{
}

public PersonFacade(MongoClient client)
{
    _client = client;
    _database = _client.GetDatabase("Person");
    _collection = _database.GetCollection<Person>("Person");
}
```

**Förklaring:** Den första konstruktorn skapar en MongoClient och skickar vidare till den andra konstruktorn.
</details>

## Del 1D: PersonFacade - CREATE-metoder

### Uppgift 1D.1: InsertAsync

```csharp
public async Task InsertAsync(Person person)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task InsertAsync(Person person)
{
    await _collection.InsertOneAsync(person);
}
```
</details>

**Test:**
```csharp
var facade = new PersonFacade(connectionString);
var luke = new Person
{
    Id = Guid.NewGuid().ToString(),
    first_name = "Luke",
    last_name = "Skywalker"
};
await facade.InsertAsync(luke);
```

### Uppgift 1D.2: InsertManyAsync

```csharp
public async Task InsertManyAsync(IEnumerable<Person> persons)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task InsertManyAsync(IEnumerable<Person> persons)
{
    await _collection.InsertManyAsync(persons);
}
```
</details>

## Del 1E: PersonFacade - READ-metoder

### Uppgift 1E.1: GetAllAsync

```csharp
public async Task<List<Person>> GetAllAsync()
{
    // TODO: Implementera
    // Hint: Find(_ => true)
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<Person>> GetAllAsync()
{
    return await _collection.Find(_ => true).ToListAsync();
}
```
</details>

### Uppgift 1E.2: GetByFirstNameAsync

Person-specifik sökning:
```csharp
public async Task<List<Person>> GetByFirstNameAsync(string firstName)
{
    // TODO: Implementera
    // Hint: Builders<Person>.Filter.Eq(p => p.first_name, firstName)
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<Person>> GetByFirstNameAsync(string firstName)
{
    var filter = Builders<Person>.Filter.Eq(p => p.first_name, firstName);
    return await _collection.Find(filter).ToListAsync();
}
```
</details>

### Uppgift 1E.3: GetByEmailAsync

```csharp
public async Task<Person?> GetByEmailAsync(string email)
{
    // TODO: Implementera
    // Hint: FirstOrDefaultAsync
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<Person?> GetByEmailAsync(string email)
{
    var filter = Builders<Person>.Filter.Eq(p => p.email, email);
    return await _collection.Find(filter).FirstOrDefaultAsync();
}
```
</details>

### Uppgift 1E.4: GetByCarAsync

```csharp
public async Task<List<Person>> GetByCarAsync(string car)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<Person>> GetByCarAsync(string car)
{
    var filter = Builders<Person>.Filter.Eq(p => p.car, car);
    return await _collection.Find(filter).ToListAsync();
}
```
</details>

**Fördel med hård typning:** Vi kan skapa specifika metoder för Person!

## Del 1F: PersonFacade - UPDATE-metoder

### Uppgift 1F.1: UpdateEmailAsync

Person-specifik uppdatering:
```csharp
public async Task<UpdateResult> UpdateEmailAsync(string firstName, string newEmail)
{
    // TODO: Skapa filter för first_name
    // TODO: Skapa update för email
    // TODO: Kör UpdateOneAsync
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<UpdateResult> UpdateEmailAsync(string firstName, string newEmail)
{
    var filter = Builders<Person>.Filter.Eq(p => p.first_name, firstName);
    var update = Builders<Person>.Update.Set(p => p.email, newEmail);
    return await _collection.UpdateOneAsync(filter, update);
}
```
</details>

**Test:**
```csharp
var result = await facade.UpdateEmailAsync("Luke", "luke@jedi.com");
Console.WriteLine($"Modified: {result.ModifiedCount}");
```

### Uppgift 1F.2: UpdateCarAsync

```csharp
public async Task<UpdateResult> UpdateCarAsync(
    string email,
    string car,
    string carModel,
    int carYear)
{
    // TODO: Implementera
    // Uppdatera flera fält samtidigt
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<UpdateResult> UpdateCarAsync(
    string email,
    string car,
    string carModel,
    int carYear)
{
    var filter = Builders<Person>.Filter.Eq(p => p.email, email);
    var update = Builders<Person>.Update
        .Set(p => p.car, car)
        .Set(p => p.car_model, carModel)
        .Set(p => p.car_year, carYear);
    return await _collection.UpdateOneAsync(filter, update);
}
```
</details>

### Uppgift 1F.3: ReplacePersonAsync

```csharp
public async Task<ReplaceOneResult> ReplacePersonAsync(string email, Person newPerson)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<ReplaceOneResult> ReplacePersonAsync(string email, Person newPerson)
{
    var filter = Builders<Person>.Filter.Eq(p => p.email, email);
    return await _collection.ReplaceOneAsync(filter, newPerson);
}
```
</details>

## Del 1G: PersonFacade - DELETE-metoder

### Uppgift 1G.1: DeleteByEmailAsync

```csharp
public async Task<DeleteResult> DeleteByEmailAsync(string email)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<DeleteResult> DeleteByEmailAsync(string email)
{
    var filter = Builders<Person>.Filter.Eq(p => p.email, email);
    return await _collection.DeleteOneAsync(filter);
}
```
</details>

### Uppgift 1G.2: DeleteByFirstNameAsync

```csharp
public async Task<DeleteResult> DeleteByFirstNameAsync(string firstName)
{
    // TODO: Implementera
    // Hint: DeleteManyAsync för att ta bort alla med samma förnamn
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<DeleteResult> DeleteByFirstNameAsync(string firstName)
{
    var filter = Builders<Person>.Filter.Eq(p => p.first_name, firstName);
    return await _collection.DeleteManyAsync(filter);
}
```
</details>

---

# STEG 2: Generisk Facade (Alla modeller)

Nu när du har en fungerande PersonFacade, refaktorera den till att vara generisk!

## Del 2A: Från PersonFacade till MongoFacade&lt;T&gt;

### Jämförelse

**Innan (Hård typad):**
```csharp
public class PersonFacade
{
    private readonly IMongoCollection<Person> _collection;

    public PersonFacade(string connectionString)
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase("Person");
        _collection = _database.GetCollection<Person>("Person");
    }

    public async Task<List<Person>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}
```

**Efter (Generisk):**
```csharp
public class MongoFacade<T> where T : class
{
    private readonly IMongoCollection<T> _collection;

    public MongoFacade(string connectionString, string databaseName, string collectionName)
    {
        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(databaseName);
        _collection = _database.GetCollection<T>(collectionName);
    }

    public async Task<List<T>> GetAllAsync()
    {
        return await _collection.Find(_ => true).ToListAsync();
    }
}
```

### Vad har ändrats?

1. **`Person` → `T`**: Alla hårdkodade typer byts ut
2. **Hårdkodade namn → parametrar**: Database och collection blir parametrar
3. **`where T : class`**: Säkerställer att T är en referenstyp

## Del 2B: Konvertera PersonFacade

### Uppgift 2B.1: Gör klassen generisk

```csharp
public class MongoFacade<T> where T : class
{
    private readonly MongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IMongoCollection<T> _collection;

    // TODO: Uppdatera konstruktorer
}
```

### Uppgift 2B.2: Uppdatera konstruktorer

```csharp
public MongoFacade(string connectionString, string databaseName, string collectionName)
    : this(new MongoClient(connectionString), databaseName, collectionName)
{
}

public MongoFacade(MongoClient client, string databaseName, string collectionName)
{
    // TODO: Ta bort hårdkodade "Person" och använd parametrar
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public MongoFacade(string connectionString, string databaseName, string collectionName)
    : this(new MongoClient(connectionString), databaseName, collectionName)
{
}

public MongoFacade(MongoClient client, string databaseName, string collectionName)
{
    _client = client;
    _database = _client.GetDatabase(databaseName);
    _collection = _database.GetCollection<T>(collectionName);
}
```
</details>

### Uppgift 2B.3: Konvertera alla metoder

**Före:**
```csharp
public async Task InsertAsync(Person person) { ... }
public async Task<List<Person>> GetAllAsync() { ... }
public async Task<Person?> GetByEmailAsync(string email) { ... }
```

**Efter:**
```csharp
public async Task InsertAsync(T document) { ... }
public async Task<List<T>> GetAllAsync() { ... }
public async Task<T?> FindOneAsync(FilterDefinition<T> filter) { ... }
```

**⚠️ Problem:** Vi kan inte ha `GetByEmailAsync` längre - varför?
**Svar:** Inte alla modeller har `email`-fält!

**Lösning:** Gör metoder mer generiska med filter.

## Del 2C: Generiska sökmetoder

### Uppgift 2C.1: FindAsync

```csharp
public async Task<List<T>> FindAsync(FilterDefinition<T> filter)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<T>> FindAsync(FilterDefinition<T> filter)
{
    return await _collection.Find(filter).ToListAsync();
}
```
</details>

### Uppgift 2C.2: FindOneAsync

```csharp
public async Task<T?> FindOneAsync(FilterDefinition<T> filter)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<T?> FindOneAsync(FilterDefinition<T> filter)
{
    return await _collection.Find(filter).FirstOrDefaultAsync();
}
```
</details>

### Uppgift 2C.3: FindByPropertyAsync (helper)

```csharp
public async Task<List<T>> FindByPropertyAsync<TProperty>(
    Expression<Func<T, TProperty>> property,
    TProperty value)
{
    var filter = Builders<T>.Filter.Eq(property, value);
    return await FindAsync(filter);
}
```

**Användning:**
```csharp
// För Person
var facade = new MongoFacade<Person>(connectionString, "Person", "Person");
var lukes = await facade.FindByPropertyAsync(p => p.first_name, "Luke");

// För Product
var productFacade = new MongoFacade<Product>(connectionString, "Store", "Products");
var electronics = await productFacade.FindByPropertyAsync(p => p.category, "Electronics");
```

## Del 2D: Generiska UPDATE-metoder

### Uppgift 2D.1: UpdateOneAsync

```csharp
public async Task<UpdateResult> UpdateOneAsync(
    FilterDefinition<T> filter,
    UpdateDefinition<T> update)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<UpdateResult> UpdateOneAsync(
    FilterDefinition<T> filter,
    UpdateDefinition<T> update)
{
    return await _collection.UpdateOneAsync(filter, update);
}
```
</details>

### Uppgift 2D.2: UpdateManyAsync

```csharp
public async Task<UpdateResult> UpdateManyAsync(
    FilterDefinition<T> filter,
    UpdateDefinition<T> update)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<UpdateResult> UpdateManyAsync(
    FilterDefinition<T> filter,
    UpdateDefinition<T> update)
{
    return await _collection.UpdateManyAsync(filter, update);
}
```
</details>

### Uppgift 2D.3: ReplaceOneAsync

```csharp
public async Task<ReplaceOneResult> ReplaceOneAsync(
    FilterDefinition<T> filter,
    T replacement)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<ReplaceOneResult> ReplaceOneAsync(
    FilterDefinition<T> filter,
    T replacement)
{
    return await _collection.ReplaceOneAsync(filter, replacement);
}
```
</details>

## Del 2E: Generiska DELETE-metoder

### Uppgift 2E.1: DeleteOneAsync

```csharp
public async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<DeleteResult> DeleteOneAsync(FilterDefinition<T> filter)
{
    return await _collection.DeleteOneAsync(filter);
}
```
</details>

### Uppgift 2E.2: DeleteManyAsync

```csharp
public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<DeleteResult> DeleteManyAsync(FilterDefinition<T> filter)
{
    return await _collection.DeleteManyAsync(filter);
}
```
</details>

---

# STEG 3: Testa båda varianterna

## Del 3A: Test PersonFacade (hård typad)

```csharp
var personFacade = new PersonFacade(connectionString);

// Enkelt och intuitivt
await personFacade.InsertAsync(new Person { ... });
var persons = await personFacade.GetByFirstNameAsync("Luke");
await personFacade.UpdateEmailAsync("Luke", "luke@jedi.com");
```

**Fördelar:**
- Tydliga metodnamn
- Ingen generics-komplexitet
- IDE autocomplete visar Person-specifika metoder

**Nackdelar:**
- Måste skapa ny Facade för varje modell (ProductFacade, OrderFacade, etc.)
- Kodduplicering mellan facades

## Del 3B: Test MongoFacade&lt;T&gt; (generisk)

```csharp
var personFacade = new MongoFacade<Person>(connectionString, "Person", "Person");

// Mer generiskt men flexibelt
await personFacade.InsertAsync(new Person { ... });
var filter = Builders<Person>.Filter.Eq(p => p.first_name, "Luke");
var persons = await personFacade.FindAsync(filter);

// Funkar även för andra modeller!
var productFacade = new MongoFacade<Product>(connectionString, "Store", "Products");
await productFacade.InsertAsync(new Product { ... });
```

**Fördelar:**
- En klass för alla modeller
- Ingen kodduplicering
- Lätt att underhålla

**Nackdelar:**
- Mer komplex syntax med Builders
- Kräver förståelse för generics

## Del 3C: Skapa flera modeller

### Product-modell

```csharp
public class Product
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string MongoId { get; set; }

    [BsonElement("id")]
    public string Id { get; set; }

    public string name { get; set; }
    public string category { get; set; }
    public decimal price { get; set; }
    public int stock { get; set; }
}
```

### Test med Product

```csharp
var productFacade = new MongoFacade<Product>(connectionString, "Store", "Products");

var laptop = new Product
{
    Id = Guid.NewGuid().ToString(),
    name = "Lightsaber",
    category = "Weapons",
    price = 9999.99m,
    stock = 5
};

await productFacade.InsertAsync(laptop);

var weapons = await productFacade.FindByPropertyAsync(p => p.category, "Weapons");
```

**🎯 Poäng:** Samma Facade fungerar för alla modeller!

---

# STEG 4: Bonusfunktioner

## Del 4A: CountAsync

```csharp
public async Task<long> CountAsync(FilterDefinition<T>? filter = null)
{
    // TODO: Implementera
    // Hint: filter ??= Builders<T>.Filter.Empty
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<long> CountAsync(FilterDefinition<T>? filter = null)
{
    filter ??= Builders<T>.Filter.Empty;
    return await _collection.CountDocumentsAsync(filter);
}
```
</details>

### Uppgift 4A.2: ExistsAsync

```csharp
public async Task<bool> ExistsAsync(FilterDefinition<T> filter)
{
    // TODO: Implementera
    // Hint: Använd CountAsync
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<bool> ExistsAsync(FilterDefinition<T> filter)
{
    var count = await CountAsync(filter);
    return count > 0;
}
```
</details>

### Uppgift 4A.3: FindWithPagingAsync

```csharp
public async Task<List<T>> FindWithPagingAsync(
    FilterDefinition<T> filter,
    int page,
    int pageSize)
{
    // TODO: Implementera
    // Hint: Skip och Limit
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<T>> FindWithPagingAsync(
    FilterDefinition<T> filter,
    int page,
    int pageSize)
{
    return await _collection
        .Find(filter)
        .Skip((page - 1) * pageSize)
        .Limit(pageSize)
        .ToListAsync();
}
```
</details>

## Del 4B: Sökning med Regex

```csharp
public async Task<List<T>> SearchAsync(
    Expression<Func<T, string>> property,
    string searchTerm)
{
    // TODO: Implementera
}
```

<details>
<summary>💡 Lösningsförslag</summary>

```csharp
public async Task<List<T>> SearchAsync(
    Expression<Func<T, string>> property,
    string searchTerm)
{
    var filter = Builders<T>.Filter.Regex(property, searchTerm);
    return await FindAsync(filter);
}
```
</details>

**Test:**
```csharp
var gmailUsers = await facade.SearchAsync(p => p.email, "gmail");
```

---

# STEG 5: Komplett Testprogram

## Del 5A: Test PersonFacade (Hård typad)

```csharp
using MongoDB.Driver;

var connectionString = File.ReadAllText(
    Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
        "databaser",
        "MongoDB.txt"
    )
).Trim();

Console.WriteLine("=== TESTING PersonFacade (Hård typad) ===\n");

var personFacade = new PersonFacade(connectionString);

// CREATE
var leia = new Person
{
    Id = Guid.NewGuid().ToString(),
    first_name = "Leia",
    last_name = "Organa",
    email = "leia@rebellion.com",
    gender = "Female",
    car = "Speeder",
    car_model = "74-Z",
    car_year = 1983,
    app = "RebelChat"
};
await personFacade.InsertAsync(leia);
Console.WriteLine("✅ Created Leia");

// READ - specifik metod
var leias = await personFacade.GetByFirstNameAsync("Leia");
Console.WriteLine($"✅ Found {leias.Count} person(s) named Leia");

// UPDATE - specifik metod
await personFacade.UpdateEmailAsync("Leia", "leia@alliance.com");
Console.WriteLine("✅ Updated Leia's email");

// DELETE - specifik metod
await personFacade.DeleteByEmailAsync("leia@alliance.com");
Console.WriteLine("✅ Deleted Leia");
```

## Del 5B: Test MongoFacade&lt;T&gt; (Generisk)

```csharp
Console.WriteLine("\n=== TESTING MongoFacade<T> (Generisk) ===\n");

var facade = new MongoFacade<Person>(connectionString, "Person", "Person");

// CREATE
var han = new Person
{
    Id = Guid.NewGuid().ToString(),
    first_name = "Han",
    last_name = "Solo",
    email = "han@smuggler.com",
    gender = "Male",
    car = "Millennium Falcon",
    car_model = "YT-1300",
    car_year = 1977,
    app = "SmugglersNet"
};
await facade.InsertAsync(han);
Console.WriteLine("✅ Created Han");

// READ - generisk metod
var all = await facade.GetAllAsync();
Console.WriteLine($"✅ Total persons: {all.Count}");

// SEARCH - generisk med property
var hans = await facade.FindByPropertyAsync(p => p.first_name, "Han");
Console.WriteLine($"✅ Found {hans.Count} person(s) named Han");

// UPDATE - generisk metod
var filter = Builders<Person>.Filter.Eq(p => p.first_name, "Han");
var update = Builders<Person>.Update.Set(p => p.app, "RebelApp");
await facade.UpdateOneAsync(filter, update);
Console.WriteLine("✅ Updated Han's app");

// DELETE - generisk metod
await facade.DeleteOneAsync(filter);
Console.WriteLine("✅ Deleted Han");

// COUNT
var count = await facade.CountAsync();
Console.WriteLine($"✅ Total documents: {count}");
```

## Del 5C: Test med flera modeller (Generisk)

```csharp
Console.WriteLine("\n=== TESTING Med Product-modell ===\n");

var productFacade = new MongoFacade<Product>(connectionString, "Store", "Products");

var lightsaber = new Product
{
    Id = Guid.NewGuid().ToString(),
    name = "Blue Lightsaber",
    category = "Weapons",
    price = 9999.99m,
    stock = 3
};

await productFacade.InsertAsync(lightsaber);
Console.WriteLine("✅ Created Lightsaber product");

var weapons = await productFacade.FindByPropertyAsync(p => p.category, "Weapons");
Console.WriteLine($"✅ Found {weapons.Count} weapon(s)");

var productFilter = Builders<Product>.Filter.Eq(p => p.name, "Blue Lightsaber");
await productFacade.DeleteOneAsync(productFilter);
Console.WriteLine("✅ Deleted Lightsaber");
```

---

# STEG 6: Reflektion och Diskussion

## Jämför PersonFacade vs MongoFacade&lt;T&gt;

### PersonFacade (Hård typad)

**Fördelar:**
- ✅ Enkel att förstå och använda
- ✅ Tydliga metodnamn (`GetByFirstNameAsync`, `UpdateEmailAsync`)
- ✅ Perfekt för att lära sig Facade-pattern
- ✅ Ingen generics-komplexitet
- ✅ IDE autocomplete visar alla Person-metoder

**Nackdelar:**
- ❌ Måste skapa ny klass för varje modell
- ❌ Mycket kodduplicering
- ❌ Svårt att underhålla (ändringar måste göras överallt)

### MongoFacade&lt;T&gt; (Generisk)

**Fördelar:**
- ✅ En klass för alla modeller
- ✅ Ingen kodduplicering
- ✅ Lätt att underhålla
- ✅ Flexibel och kraftfull

**Nackdelar:**
- ❌ Mer komplex syntax
- ❌ Kräver förståelse för generics
- ❌ Builders-syntax kan vara svår initialt

## Diskussionsfrågor

1. **När skulle du välja PersonFacade över MongoFacade&lt;T&gt;?**
   - Litet projekt med få modeller?
   - Lärande och förståelse?
   - Team med begränsad C#-erfarenhet?

2. **När skulle du välja MongoFacade&lt;T&gt;?**
   - Många modeller (10+)?
   - Stort professionellt projekt?
   - Behöver flexibilitet?

3. **Hur kan vi förbättra felhanteringen?**
4. **Skulle du lägga till logging? Hur?**
5. **Hur skulle du testa dessa facades?**
6. **Vilken approach föredrar du själv? Varför?**

## VG-utmaning: Lägg till fler features

### 1. Transaction-support
```csharp
public async Task<T> WithTransactionAsync<T>(
    Func<IClientSessionHandle, Task<T>> action)
{
    using var session = await _client.StartSessionAsync();
    session.StartTransaction();
    try
    {
        var result = await action(session);
        await session.CommitTransactionAsync();
        return result;
    }
    catch
    {
        await session.AbortTransactionAsync();
        throw;
    }
}
```

### 2. Soft delete
Istället för att ta bort dokument, markera dem som borttagna:
```csharp
public async Task SoftDeleteAsync(FilterDefinition<T> filter)
{
    var update = Builders<T>.Update.Set("IsDeleted", true);
    await UpdateManyAsync(filter, update);
}

public async Task<List<T>> GetAllActiveAsync()
{
    var filter = Builders<T>.Filter.Ne("IsDeleted", true);
    return await FindAsync(filter);
}
```

### 3. Bulk operations
```csharp
public async Task BulkWriteAsync(IEnumerable<WriteModel<T>> requests)
{
    await _collection.BulkWriteAsync(requests);
}
```

### 4. Aggregation pipeline
```csharp
public async Task<List<TResult>> AggregateAsync<TResult>(
    PipelineDefinition<T, TResult> pipeline)
{
    return await _collection.Aggregate(pipeline).ToListAsync();
}
```

## Pro-tips

- **Separera Facade från business logic** - Facade är bara för databasaccess
- **Skapa en IMongoFacade interface** - Lättare att mocka i tester
- **Dependency Injection** - Registrera som service i ASP.NET Core
- **Connection pooling** - MongoClient är trådsäker, återanvänd samma instans
- **Lazy loading** - Överväg att inte ansluta förrän första operationen

## Sammanfattning

En bra MongoDB Facade ska:
- ✅ Förenkla CRUD-operationer
- ✅ Vara generisk och återanvändbar
- ✅ Hantera både connectionString och MongoClient
- ✅ Ge flexibilitet att byta database/collection
- ✅ Kapsla in MongoDB-specifik logik
- ✅ Vara enkel att testa och underhålla

Nu kör vi! 🚀

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
