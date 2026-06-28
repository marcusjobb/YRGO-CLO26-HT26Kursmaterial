---

title: Övning 2: MongoDB från C# - Todo App
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 3
language: mixed
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/mongodb/mongodb_exercise_2.md"
description: "Efter den här övningen kommer du att kunna:"
tags: ["csharp", "databaser", "exercise", "från", "installation", "mongodb", "ssh", "todo", "visual-studio", "övning"]
week_fit: []
---

# Övning 2: MongoDB från C# - Todo App

🔴


## 🎯 Mål med övningen

Efter den här övningen kommer du att kunna:

- Installera och använda MongoDB.Driver i C#
- Ansluta till MongoDB från en .NET-applikation
- Mappa C#-klasser till MongoDB-dokument
- Utföra CRUD-operationer från kod
- Hantera asynkrona MongoDB-operationer

## 🧩 Uppgiften

Ni ska bygga en **Todo-app** med C# och MongoDB.

Appen ska kunna:
- Lägga till todos
- Lista alla todos
- Markera todos som klara
- Ta bort todos
- Filtrera todos (visa bara oklara, eller bara klara)

## 🚀 Kom igång: Skapa Projektet

### Steg 1: Skapa Console App

```bash
dotnet new console -n MongoTodoApp
cd MongoTodoApp
```

### Steg 2: Installera MongoDB Driver

```bash
dotnet add package MongoDB.Driver
```

Detta installerar MongoDB.Driver NuGet-paketet.

### Steg 3: Verifiera Installation

Öppna `MongoTodoApp.csproj` och kolla att detta finns:

```xml
<ItemGroup>
  <PackageReference Include="MongoDB.Driver" Version="2.x.x" />
</ItemGroup>
```

## 📝 Del 1: Skapa Todo-Modellen

Skapa en fil: `Todo.cs`

```csharp
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MongoTodoApp;

public class Todo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("title")]
    public string Title { get; set; } = string.Empty;

    [BsonElement("description")]
    public string? Description { get; set; }

    [BsonElement("isCompleted")]
    public bool IsCompleted { get; set; } = false;

    [BsonElement("createdAt")]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [BsonElement("tags")]
    public List<string> Tags { get; set; } = new();
}
```

### 🧠 Förklaring

- **`[BsonId]`**: Markerar detta som MongoDB's `_id` fält
- **`[BsonRepresentation(BsonType.ObjectId)]`**: Konverterar ObjectId till string
- **`[BsonElement("title")]`**: Mappar C#-property till MongoDB-field (valfritt, men bra för naming conventions)

## 🔌 Del 2: Anslut Till MongoDB

I `Program.cs`:

```csharp
using MongoDB.Driver;
using MongoTodoApp;

// Anslut till MongoDB (ändra connection string om ni kör Atlas)
var connectionString = "mongodb://localhost:27017";
var client = new MongoClient(connectionString);

// Hämta databas
var database = client.GetDatabase("todo_db");

// Hämta collection
var todosCollection = database.GetCollection<Todo>("todos");

Console.WriteLine("Ansluten till MongoDB! ✅");
```

### 🧪 Testa

Kör:

```bash
dotnet run
```

Om det står **"Ansluten till MongoDB! ✅"** funkar det!

## ✅ Del 3: CREATE - Lägg Till Todos

Lägg till följande i `Program.cs`:

```csharp
// Skapa en ny todo
var newTodo = new Todo
{
    Title = "Lär dig MongoDB",
    Description = "Gå igenom övningarna och testa queries",
    Tags = new List<string> { "programmering", "databaser" }
};

// Spara i MongoDB
await todosCollection.InsertOneAsync(newTodo);
Console.WriteLine($"Todo skapad med ID: {newTodo.Id}");
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

### 🧪 Testa

Kör `dotnet run` igen.

Öppna **MongoDB Compass** och kolla `todo_db` → `todos`.

Ser ni ert dokument? 🎉

## 📖 Del 4: READ - Lista Todos

Lägg till:

```csharp
// Hämta alla todos
var allTodos = await todosCollection.Find(_ => true).ToListAsync();

Console.WriteLine("\n📋 Alla Todos:");
foreach (var todo in allTodos)
{
    var status = todo.IsCompleted ? "✅" : "⬜";
    Console.WriteLine($"{status} {todo.Title}");
    if (!string.IsNullOrEmpty(todo.Description))
    {
        Console.WriteLine($"   {todo.Description}");
    }
    Console.WriteLine($"   Skapad: {todo.CreatedAt:yyyy-MM-dd HH:mm}");
    if (todo.Tags.Any())
    {
        Console.WriteLine($"   Tags: {string.Join(", ", todo.Tags)}");
    }
    Console.WriteLine();
}
```

### 🧪 Testa

Kör `dotnet run`.

Ni borde se alla todos listade snyggt!

## 🔄 Del 5: UPDATE - Markera Som Klar

Lägg till:

```csharp
// Hitta en todo och markera som klar
var filter = Builders<Todo>.Filter.Eq(t => t.Title, "Lär dig MongoDB");
var update = Builders<Todo>.Update.Set(t => t.IsCompleted, true);

var result = await todosCollection.UpdateOneAsync(filter, update);
Console.WriteLine($"✅ {result.ModifiedCount} todo markerad som klar!");
```

### 🧠 Förklaring

- **`Builders<Todo>.Filter`**: Skapar filter (motsvarar MongoDB query)
- **`Builders<Todo>.Update`**: Skapar update-operation
- **`UpdateOneAsync`**: Uppdaterar första matchande dokument

### 🧪 Testa

Kör igen och se att din todo nu har ✅ istället för ⬜!

## 🗑️ Del 6: DELETE - Ta Bort Todo

Lägg till:

```csharp
// Ta bort en todo
var deleteFilter = Builders<Todo>.Filter.Eq(t => t.Title, "Lär dig MongoDB");
var deleteResult = await todosCollection.DeleteOneAsync(deleteFilter);
Console.WriteLine($"🗑️ {deleteResult.DeletedCount} todo borttagen!");
```

## 🎨 Del 7: Skapa En Enkel Meny

Nu gör vi appen interaktiv!

Ersätt all kod i `Program.cs` med detta:

```csharp
using MongoDB.Driver;
using MongoTodoApp;

var connectionString = "mongodb://localhost:27017";
var client = new MongoClient(connectionString);
var database = client.GetDatabase("todo_db");
var todosCollection = database.GetCollection<Todo>("todos");

while (true)
{
    Console.WriteLine("\n=== 📝 TODO APP ===");
    Console.WriteLine("1. Lägg till todo");
    Console.WriteLine("2. Visa alla todos");
    Console.WriteLine("3. Markera som klar");
    Console.WriteLine("4. Ta bort todo");
    Console.WriteLine("5. Avsluta");
    Console.Write("\nVälj (1-5): ");

    var choice = Console.ReadLine();

    switch (choice)
    {
        case "1":
            await AddTodo(todosCollection);
            break;
        case "2":
            await ShowTodos(todosCollection);
            break;
        case "3":
            await CompleteTodo(todosCollection);
            break;
        case "4":
            await DeleteTodo(todosCollection);
            break;
        case "5":
            Console.WriteLine("Hejdå! 👋");
            return;
        default:
            Console.WriteLine("❌ Ogiltigt val!");
            break;
    }
}

static async Task AddTodo(IMongoCollection<Todo> collection)
{
    Console.Write("\nTitel: ");
    var title = Console.ReadLine() ?? "";

    Console.Write("Beskrivning (valfritt): ");
    var description = Console.ReadLine();

    Console.Write("Tags (separera med komma): ");
    var tagsInput = Console.ReadLine() ?? "";
    var tags = tagsInput.Split(',', StringSplitOptions.RemoveEmptyEntries)
                        .Select(t => t.Trim())
                        .ToList();

    var todo = new Todo
    {
        Title = title,
        Description = description,
        Tags = tags
    };

    await collection.InsertOneAsync(todo);
    Console.WriteLine("✅ Todo skapad!");
}

static async Task ShowTodos(IMongoCollection<Todo> collection)
{
    var todos = await collection.Find(_ => true).ToListAsync();

    if (!todos.Any())
    {
        Console.WriteLine("\n📭 Inga todos hittades!");
        return;
    }

    Console.WriteLine("\n📋 Dina Todos:");
    foreach (var todo in todos)
    {
        var status = todo.IsCompleted ? "✅" : "⬜";
        Console.WriteLine($"\n{status} {todo.Title}");
        if (!string.IsNullOrEmpty(todo.Description))
            Console.WriteLine($"   📝 {todo.Description}");
        if (todo.Tags.Any())
            Console.WriteLine($"   🏷️ {string.Join(", ", todo.Tags)}");
        Console.WriteLine($"   📅 {todo.CreatedAt:yyyy-MM-dd HH:mm}");
    }
}

static async Task CompleteTodo(IMongoCollection<Todo> collection)
{
    await ShowTodos(collection);

    Console.Write("\nVilken todo vill du markera som klar? (titel): ");
    var title = Console.ReadLine();

    var filter = Builders<Todo>.Filter.Eq(t => t.Title, title);
    var update = Builders<Todo>.Update.Set(t => t.IsCompleted, true);

    var result = await collection.UpdateOneAsync(filter, update);

    if (result.ModifiedCount > 0)
        Console.WriteLine("✅ Todo markerad som klar!");
    else
        Console.WriteLine("❌ Todo hittades inte!");
}

static async Task DeleteTodo(IMongoCollection<Todo> collection)
{
    await ShowTodos(collection);

    Console.Write("\nVilken todo vill du ta bort? (titel): ");
    var title = Console.ReadLine();

    var filter = Builders<Todo>.Filter.Eq(t => t.Title, title);
    var result = await collection.DeleteOneAsync(filter);

    if (result.DeletedCount > 0)
        Console.WriteLine("🗑️ Todo borttagen!");
    else
        Console.WriteLine("❌ Todo hittades inte!");
}
```

### 🧪 Testa

Kör `dotnet run` och testa alla funktioner!

## 🕵️‍♂️ Hur testar ni att det funkar?

- **Lägg till todos** med olika titlar och tags
- **Visa alla** och bekräfta att de dyker upp
- **Markera som klara** och se att ✅ visas
- **Ta bort** och bekräfta att de försvinner
- **Öppna Compass** och kolla att ändringarna syns där också

## 🤔 Diskussion i paret

Snacka ihop er!

1. **Hur kändes MongoDB.Driver jämfört med Entity Framework?**
   - Enklare eller krångligare?
   - Mer eller mindre kod?

2. **Vad händer om ni försöker markera en todo som inte finns?**
   - Kraschar appen?
   - Hur hanterar koden detta?

3. **Embedded vs Referenced:**
   - Just nu är `Tags` en array i todo-dokumentet (embedded).
   - Vad hade hänt om ni hade en separat `tags`-collection?
   - Vilka för- och nackdelar?

## 🔥 BONUS: Advanced Features

### Bonus 1: Filtrera På Tags

Lägg till en meny-option för att visa bara todos med en specifik tag:

```csharp
static async Task ShowTodosByTag(IMongoCollection<Todo> collection)
{
    Console.Write("\nVilken tag vill du filtrera på? ");
    var tag = Console.ReadLine();

    var filter = Builders<Todo>.Filter.AnyEq(t => t.Tags, tag);
    var todos = await collection.Find(filter).ToListAsync();

    // Visa todos...
}
```

### Bonus 2: Sök Med Text

Skapa ett text index:

```csharp
var keys = Builders<Todo>.IndexKeys.Text(t => t.Title).Text(t => t.Description);
await todosCollection.Indexes.CreateOneAsync(new CreateIndexModel<Todo>(keys));
```

Sök:

```csharp
var filter = Builders<Todo>.Filter.Text("MongoDB");
var results = await todosCollection.Find(filter).ToListAsync();
```

### Bonus 3: Sortera Todos

Visa nyaste först:

```csharp
var todos = await collection
    .Find(_ => true)
    .SortByDescending(t => t.CreatedAt)
    .ToListAsync();
```

### Bonus 4: Använd Atlas Connection String

Om ni vill ansluta till MongoDB Atlas istället för Docker:

```csharp
var connectionString = "mongodb+srv://username:password@cluster.mongodb.net/";
```

Ersätt `username`, `password` och `cluster` med era riktiga värden.

## 💭 Reflektionsfrågor

1. **Vad var lättare med MongoDB än EF?**

2. **Vad var svårare?**

3. **Saknar ni migrations?** Eller är det skönt att slippa?

4. **Hur skulle ni bygga en Todo-app med både användare OCH todos?**
   - Embedded eller referenced?
   - En collection eller två?

<details>
<summary>💡 Klicka här för extra tips och best practices</summary>

## Connection String Best Practices

**ALDRIG** hårdkoda connection strings!

Använd istället `appsettings.json`:

```json
{
  "MongoDb": {
    "ConnectionString": "mongodb://localhost:27017",
    "DatabaseName": "todo_db"
  }
}
```

Eller environment variables:

```csharp
var connectionString = Environment.GetEnvironmentVariable("MONGO_CONNECTION_STRING")
    ?? "mongodb://localhost:27017";
```

## Dependency Injection Pattern

Skapa en `MongoDbService`:

```csharp
public class MongoDbService
{
    private readonly IMongoDatabase _database;

    public MongoDbService(string connectionString, string databaseName)
    {
        var client = new MongoClient(connectionString);
        _database = client.GetDatabase(databaseName);
    }

    public IMongoCollection<T> GetCollection<T>(string collectionName)
    {
        return _database.GetCollection<T>(collectionName);
    }
}
```

Användning:

```csharp
var mongoService = new MongoDbService("mongodb://localhost:27017", "todo_db");
var todosCollection = mongoService.GetCollection<Todo>("todos");
```

## Error Handling

Lägg till try-catch:

```csharp
try
{
    await todosCollection.InsertOneAsync(newTodo);
    Console.WriteLine("✅ Todo skapad!");
}
catch (MongoException ex)
{
    Console.WriteLine($"❌ MongoDB fel: {ex.Message}");
}
```

## Async Best Practices

Använd **alltid** async/await med MongoDB:

```csharp
// ✅ Bra
var todos = await collection.Find(_ => true).ToListAsync();

// ❌ Dåligt (blockerar tråden)
var todos = collection.Find(_ => true).ToList();
```

## Index För Prestanda

Om ni ofta söker på `IsCompleted`:

```csharp
var indexKeys = Builders<Todo>.IndexKeys.Ascending(t => t.IsCompleted);
await todosCollection.Indexes.CreateOneAsync(new CreateIndexModel<Todo>(indexKeys));
```

Nu blir queries på completed/uncompleted todos mycket snabbare!

## Compound Queries

Hitta alla oklara todos med en specifik tag:

```csharp
var filter = Builders<Todo>.Filter.And(
    Builders<Todo>.Filter.Eq(t => t.IsCompleted, false),
    Builders<Todo>.Filter.AnyEq(t => t.Tags, "viktigt")
);

var todos = await collection.Find(filter).ToListAsync();
```

## Projection (Välj Bara Vissa Fält)

Om ni bara behöver titel och status:

```csharp
var projection = Builders<Todo>.Projection
    .Include(t => t.Title)
    .Include(t => t.IsCompleted)
    .Exclude(t => t.Id);

var todos = await collection
    .Find(_ => true)
    .Project<Todo>(projection)
    .ToListAsync();
```

Detta sparar bandbredd och minne!

## Embedded Users

Om ni vill lägga till användarinfo:

```csharp
public class Todo
{
    // ... existing properties ...

    [BsonElement("createdBy")]
    public UserInfo? CreatedBy { get; set; }
}

public class UserInfo
{
    public string UserId { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
}
```

Användning:

```csharp
var todo = new Todo
{
    Title = "Fix bug",
    CreatedBy = new UserInfo
    {
        UserId = "user_123",
        Username = "Marcus"
    }
};
```

## Referenced Users

Alternativt, referera bara user ID:

```csharp
public class Todo
{
    // ... existing properties ...

    [BsonElement("userId")]
    public string UserId { get; set; } = string.Empty;
}
```

Hämta användarinfo separat när du behöver det.

**När använda vilket?**
- **Embedded**: Om användarinfo sällan ändras
- **Referenced**: Om användarinfo uppdateras ofta (namn, email, etc.)

</details>

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
