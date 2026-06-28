---

title: Kapitel 5 – MongoDB: dokumentens värld
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/brief/05_mongodb.md"
description: "Välkommen till NoSQL-världen! MongoDB är en **dokumentdatabas** som lagrar data som JSON-liknande dokument istället för tabeller och rader."
tags: ["csharp", "databaser", "design-patterns", "dokumentens", "entity-framework", "git", "java", "kapitel", "mongodb", "mongodb:"]
week_fit: []
---

# Kapitel 5 – MongoDB: dokumentens värld

🟢


## 1. Från SQL till NoSQL

Välkommen till NoSQL-världen! MongoDB är en **dokumentdatabas** som lagrar data som JSON-liknande dokument istället för tabeller och rader.

### SQL vs NoSQL i korthet

| Koncept         | SQL (Relationsdatabaser) | NoSQL (MongoDB)        |
|-----------------|--------------------------|------------------------|
| Datastruktur    | Tabeller och rader       | Collections och dokument |
| Schema          | Fast struktur            | Flexibelt schema       |
| Relationer      | Foreign keys, JOINs      | Embedded documents     |
| Skalning        | Vertikalt (kraftigare server) | Horisontellt (fler servrar) |
| Query-språk     | SQL                      | MongoDB query language |
| Användning      | Strukturerad data        | Flexibel, hierarkisk data |

### När ska du använda MongoDB?

✅ **Använd MongoDB när:**
- Data har varierande struktur över tid
- Du arbetar med JSON-baserade API:er
- Du behöver snabb utveckling utan stela scheman
- Du behöver horisontell skalning
- Data är hierarkisk (t.ex. bloggposter med kommentarer)

❌ **Använd INTE MongoDB när:**
- Du behöver komplexa JOIN-operationer
- Transaktioner över flera dokument är kritiska
- Data är starkt relationell (kunder → ordrar → produkter)
- Du behöver ACID-garantier på legacy-sätt

---

## 2. Installation av verktyg och Docker

### 2.1 Installera Docker och hämta MongoDB-image

```bash
# Hämta MongoDB-imagen
docker pull mongo:latest
```

### 2.2 Starta MongoDB-container

Skapa `docker-compose.yml`:

```yaml
version: '3.8'

services:
  mongodb:
    image: mongo:latest
    container_name: mongodb_dev
    environment:
      MONGO_INITDB_ROOT_USERNAME: admin
      MONGO_INITDB_ROOT_PASSWORD: adminpassword
    ports:
      - "27017:27017"
    volumes:
      - mongo_data:/data/db

volumes:
  mongo_data:
```

Starta containern:

```bash
docker-compose up -d
```

### 2.3 (Alternativ) Skapa konto på MongoDB Atlas

**MongoDB Atlas** är en gratis molnbaserad MongoDB-lösning.

1. Gå till https://www.mongodb.com/cloud/atlas/register
2. Skapa ett konto
3. Skapa en "Free Shared Cluster" (M0)
4. Skapa en databas-användare
5. Lägg till din IP-adress i Network Access
6. Kopiera connection string (ser ut som):
   ```
   mongodb+srv://username:password@cluster.mongodb.net/myapp
   ```

### 2.4 Installera MongoDB Compass

**Compass** är ett grafiskt verktyg för MongoDB (som phpMyAdmin för MySQL).

1. Ladda ner från https://www.mongodb.com/products/compass
2. Installera med standardinställningar
3. Starta Compass
4. Anslut till:
   - **Lokal Docker:** `mongodb://admin:adminpassword@localhost:27017`
   - **Atlas:** Klistra in din Atlas connection string

---

## 3. Kom igång med Mongo Shell eller Compass

### Använda MongoDB Compass

1. Öppna Compass
2. Anslut till `mongodb://admin:adminpassword@localhost:27017`
3. Klicka "Create Database"
   - **Database Name:** `myapp`
   - **Collection Name:** `customers`
4. Klicka på `customers` collection
5. Klicka "ADD DATA" → "Insert Document"
6. Klistra in:

```json
{
  "name": "Anna Andersson",
  "email": "anna@example.com",
  "phone": "0701234567",
  "address": {
    "street": "Storgatan 1",
    "city": "Stockholm",
    "zip": "11122"
  },
  "tags": ["vip", "premium"]
}
```

7. Klicka "Insert"

### Använda Mongo Shell

```bash
docker exec -it mongodb_dev mongosh -u admin -p adminpassword
```

I shellen:

```javascript
use myapp

db.customers.insertOne({
  name: "Erik Svensson",
  email: "erik@example.com",
  phone: "0702345678",
  address: {
    street: "Lillgatan 2",
    city: "Göteborg",
    zip: "41234"
  },
  tags: ["standard"]
})

db.customers.find()

db.customers.updateOne(
  { name: "Anna Andersson" },
  { $set: { phone: "0709999999" } }
)

db.customers.deleteOne({ name: "Erik Svensson" })
```

---

## 4. JSON, XML och CSV – tre sätt att representera data

### 4.1 Vad är JSON och varför Mongo använder det

**JSON (JavaScript Object Notation)** är ett textbaserat format för att representera strukturerad data.

**Exempel:**
```json
{
  "name": "Anna",
  "age": 30,
  "hobbies": ["läsa", "programmera"],
  "address": {
    "city": "Stockholm"
  }
}
```

MongoDB använder JSON-liknande dokument eftersom:
- ✅ Lätt att läsa och skriva
- ✅ Direkt mappning till objekt i programmeringsspråk
- ✅ Stöder nested structures (objekt i objekt)
- ✅ Flexibelt schema

### 4.2 Skillnader och användningsområden

**JSON:**
```json
{
  "id": 1,
  "name": "Anna",
  "email": "anna@example.com"
}
```

**XML:**
```xml
<customer>
  <id>1</id>
  <name>Anna</name>
  <email>anna@example.com</email>
</customer>
```

**CSV:**
```csv
id,name,email
1,Anna,anna@example.com
```

### 4.3 Fördelar och nackdelar per format

| Format | Fördelar | Nackdelar | Användning |
|--------|----------|-----------|------------|
| **JSON** | Lätt att läsa, stöd för nested data, populärt i API:er | Ingen datumstandardisering, ingen schemavalidering | API:er, konfiguration, MongoDB |
| **XML** | Schemavalidering (XSD), metadata, namespaces | Verbose, svårare att läsa | SOAP, konfigfiler, äldre system |
| **CSV** | Enkelt, lätt att öppna i Excel, kompakt | Endast platt data, ingen nesting, svårt att hantera kommatecken | Export/import, datautbyte |

### 4.4 Från JSON till BSON – varför Mongo lagrar binärt

MongoDB lagrar faktiskt inte JSON direkt – den använder **BSON (Binary JSON)**.

**BSON-fördelar:**
- ✅ Snabbare att läsa och skriva
- ✅ Stöd för fler datatyper (Date, ObjectId, Binary)
- ✅ Mindre storlek i många fall
- ✅ Indexering och queries är snabbare

**För dig som utvecklare:**
Du skriver JSON, MongoDB konverterar automatiskt till BSON!

```json
{
  "_id": ObjectId("507f1f77bcf86cd799439011"),
  "createdAt": ISODate("2024-01-15T10:30:00Z")
}
```

---

## 5. JSON-hantering i C#

### 5.1 `System.Text.Json` – serialisering och deserialisering

C# har inbyggt stöd för JSON via `System.Text.Json`.

**Installera inte behövs** – ingår i .NET!

### 5.2 Objekt → JSON

```csharp
using System.Text.Json;

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}

var customer = new Customer
{
    Id = 1,
    Name = "Anna Andersson",
    Email = "anna@example.com"
};

// Konvertera till JSON
string json = JsonSerializer.Serialize(customer);
Console.WriteLine(json);
// Output: {"Id":1,"Name":"Anna Andersson","Email":"anna@example.com"}

// Med snygg formatering
var options = new JsonSerializerOptions { WriteIndented = true };
string prettyJson = JsonSerializer.Serialize(customer, options);
Console.WriteLine(prettyJson);
```

### 5.3 JSON → Objekt

```csharp
string json = """
{
  "Id": 1,
  "Name": "Anna Andersson",
  "Email": "anna@example.com"
}
""";

Customer? customer = JsonSerializer.Deserialize<Customer>(json);

if (customer != null)
{
    Console.WriteLine($"{customer.Name} - {customer.Email}");
}
```

### 5.4 Läsa och skriva filer

**Skriva JSON till fil:**
```csharp
var customers = new List<Customer>
{
    new Customer { Id = 1, Name = "Anna", Email = "anna@example.com" },
    new Customer { Id = 2, Name = "Erik", Email = "erik@example.com" }
};

string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions { WriteIndented = true });
File.WriteAllText("customers.json", json);
```

**Läsa JSON från fil:**
```csharp
string json = File.ReadAllText("customers.json");
List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(json);

foreach (var customer in customers ?? new List<Customer>())
{
    Console.WriteLine(customer.Name);
}
```

**Alternativt med streams (bättre för stora filer):**
```csharp
// Skriva
using (var stream = File.Create("customers.json"))
{
    JsonSerializer.Serialize(stream, customers, new JsonSerializerOptions { WriteIndented = true });
}

// Läsa
using (var stream = File.OpenRead("customers.json"))
{
    List<Customer>? customers = JsonSerializer.Deserialize<List<Customer>>(stream);
}
```

---

## 6. Anslutning med C# och `MongoDB.Driver`

### Installera NuGet-paket

```bash
dotnet add package MongoDB.Driver
```

### Skapa modeller med BSON-attribut

```csharp
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Customer
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("email")]
    public string Email { get; set; } = string.Empty;

    [BsonElement("phone")]
    public string? Phone { get; set; }

    [BsonElement("address")]
    public Address? Address { get; set; }

    [BsonElement("tags")]
    public List<string> Tags { get; set; } = new();
}

public class Address
{
    [BsonElement("street")]
    public string Street { get; set; } = string.Empty;

    [BsonElement("city")]
    public string City { get; set; } = string.Empty;

    [BsonElement("zip")]
    public string Zip { get; set; } = string.Empty;
}
```

### Ansluta till MongoDB

```csharp
using MongoDB.Driver;

// Lokal Docker
string connectionString = "mongodb://admin:adminpassword@localhost:27017";

// MongoDB Atlas
// string connectionString = "mongodb+srv://username:password@cluster.mongodb.net/";

var client = new MongoClient(connectionString);
var database = client.GetDatabase("myapp");
var collection = database.GetCollection<Customer>("customers");
```

---

## 7. Varför EF inte används här

**Entity Framework Core stöder inte MongoDB officiellt.**

Istället använder vi **MongoDB.Driver**, som är det officiella C#-biblioteket från MongoDB Inc.

**Fördelar med MongoDB.Driver:**
- ✅ Fullt stöd för alla MongoDB-features
- ✅ Direkt mappning till BSON
- ✅ Async/await från grunden
- ✅ Officiellt stöd och uppdateringar

**Skillnader från EF Core:**
- Ingen `SaveChanges()` – ändringar sparas direkt
- Använder `Filter` builders istället för LINQ i många fall
- Embedded documents istället för relationer

---

## 8. CRUD-exempel i C#

### CREATE (Insert)

```csharp
var customer = new Customer
{
    Name = "Lisa Larsson",
    Email = "lisa@example.com",
    Phone = "0701111111",
    Address = new Address
    {
        Street = "Kungsgatan 3",
        City = "Malmö",
        Zip = "21122"
    },
    Tags = new List<string> { "new", "premium" }
};

await collection.InsertOneAsync(customer);
Console.WriteLine($"Skapade kund med ID: {customer.Id}");
```

### READ (Find)

```csharp
// Hämta alla
var allCustomers = await collection.Find(_ => true).ToListAsync();

// Hämta en
var customer = await collection.Find(c => c.Email == "anna@example.com").FirstOrDefaultAsync();

// Filtrera
var stockholmCustomers = await collection.Find(c => c.Address!.City == "Stockholm").ToListAsync();

// Med Filter builder
var filter = Builders<Customer>.Filter.Eq(c => c.Email, "anna@example.com");
var result = await collection.Find(filter).FirstOrDefaultAsync();
```

### UPDATE

```csharp
// Uppdatera ett fält
var filter = Builders<Customer>.Filter.Eq(c => c.Email, "anna@example.com");
var update = Builders<Customer>.Update.Set(c => c.Phone, "0709999999");
await collection.UpdateOneAsync(filter, update);

// Lägg till i array
var addTagUpdate = Builders<Customer>.Update.Push(c => c.Tags, "vip");
await collection.UpdateOneAsync(filter, addTagUpdate);

// Uppdatera hela dokumentet
var customer = await collection.Find(c => c.Email == "anna@example.com").FirstOrDefaultAsync();
if (customer != null)
{
    customer.Phone = "0701111111";
    await collection.ReplaceOneAsync(c => c.Id == customer.Id, customer);
}
```

### DELETE

```csharp
// Ta bort ett dokument
var filter = Builders<Customer>.Filter.Eq(c => c.Email, "erik@example.com");
await collection.DeleteOneAsync(filter);

// Ta bort flera
var cityFilter = Builders<Customer>.Filter.Eq(c => c.Address!.City, "Stockholm");
await collection.DeleteManyAsync(cityFilter);
```

---

## 9. Backup och export (`mongodump`, `mongorestore`)

### Backup via Docker

```bash
docker exec mongodb_dev mongodump --username admin --password adminpassword --authenticationDatabase admin --db myapp --out /dump
docker cp mongodb_dev:/dump ./mongo_backup
```

### Återställ backup

```bash
docker cp ./mongo_backup mongodb_dev:/dump
docker exec mongodb_dev mongorestore --username admin --password adminpassword --authenticationDatabase admin --db myapp /dump/myapp
```

### Export till JSON (via Compass)

1. Öppna collection i Compass
2. Klicka "Export Data"
3. Välj JSON format
4. Spara filen

### Import från JSON

1. Klicka "Add Data" → "Import JSON or CSV file"
2. Välj filen
3. Klicka "Import"

---

## 10. TL;DR – Snabböversikt

| Vad                | Hur                                      |
|--------------------|------------------------------------------|
| Installera         | Docker + `docker-compose.yml`            |
| Admin-verktyg      | MongoDB Compass                          |
| NuGet-paket        | `MongoDB.Driver`                         |
| Connection string  | `mongodb://admin:adminpassword@localhost:27017` |
| CRUD               | `MongoDB.Driver` (ej EF Core)            |
| JSON i C#          | `System.Text.Json`                       |
| Backup             | `mongodump` / `mongorestore`             |

---

## 11. Vanliga fel och lösningar

### "MongoConnectionException: Unable to connect"
**Problem:** MongoDB körs inte.
**Lösning:** `docker-compose up -d` och vänta 10 sekunder.

### "Command failed with error 13 (Unauthorized)"
**Problem:** Fel autentiseringsuppgifter.
**Lösning:** Kontrollera username/password i connection string.

### "database/collection does not exist"
**Problem:** Samlingen skapas inte automatiskt.
**Lösning:** MongoDB skapar collections automatiskt vid första insert.

### JSON deserialization fel
**Problem:** JSON-struktur matchar inte C#-klass.
**Lösning:** Kontrollera property-namn och typer. Använd `[BsonElement("namn")]` för att mappa.

### ObjectId serialization problem
**Problem:** `_id` kan inte deserialiseras.
**Lösning:** Använd `string?` med `[BsonRepresentation(BsonType.ObjectId)]`.

---

## 12. Sammanfattning

Du har nu lärt dig:
✅ Skillnaden mellan SQL och NoSQL
✅ Installera MongoDB med Docker
✅ Använda MongoDB Compass
✅ Förstå JSON, XML, CSV och BSON
✅ Hantera JSON i C# med `System.Text.Json`
✅ Ansluta med `MongoDB.Driver`
✅ CRUD-operationer utan EF Core
✅ Ta backup med `mongodump`

**Nästa steg:** Kapitel 6 – Reflektion och jämförelse mellan alla databaser!

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
