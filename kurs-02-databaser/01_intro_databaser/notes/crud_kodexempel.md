# CRUD-kodexempel

🟢

Färdiga kodexempel för Create, Read, Update och Delete med EF Core och MongoDB.Driver.

---

## Entity Framework Core (SQL-databaser)

### CREATE

```csharp
using (var context = new AppDbContext())
{
    var entity = new Customer { Name = "Test", Email = "test@example.com" };
    context.Customers.Add(entity);
    context.SaveChanges();
}
```

### READ — alla

```csharp
using (var context = new AppDbContext())
{
    var customers = context.Customers.ToList();
}
```

### READ — filtrerad

```csharp
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Where(c => c.Email.EndsWith("@example.com"))
        .OrderBy(c => c.Name)
        .ToList();
}
```

### READ — med relationer

```csharp
using (var context = new AppDbContext())
{
    var customers = context.Customers
        .Include(c => c.Orders)
        .ThenInclude(o => o.OrderItems)
        .ToList();
}
```

### UPDATE

```csharp
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(1);
    if (customer != null)
    {
        customer.Email = "newemail@example.com";
        context.SaveChanges();
    }
}
```

### DELETE

```csharp
using (var context = new AppDbContext())
{
    var customer = context.Customers.Find(1);
    if (customer != null)
    {
        context.Customers.Remove(customer);
        context.SaveChanges();
    }
}
```

---

## MongoDB.Driver

### Setup

```csharp
var client = new MongoClient("mongodb://localhost:27017");
var database = client.GetDatabase("myapp");
var collection = database.GetCollection<Customer>("customers");
```

### CREATE

```csharp
var customer = new Customer { Name = "Test", Email = "test@example.com" };
await collection.InsertOneAsync(customer);
```

### READ — alla

```csharp
var customers = await collection.Find(_ => true).ToListAsync();
```

### READ — filtrerad

```csharp
var customers = await collection
    .Find(c => c.Email.EndsWith("@example.com"))
    .SortBy(c => c.Name)
    .ToListAsync();
```

### READ — med Filter Builder

```csharp
var filter = Builders<Customer>.Filter.Eq(c => c.Email, "test@example.com");
var customer = await collection.Find(filter).FirstOrDefaultAsync();
```

### UPDATE

```csharp
var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
var update = Builders<Customer>.Update.Set(c => c.Email, "newemail@example.com");
await collection.UpdateOneAsync(filter, update);
```

### UPDATE — lägg till i array

```csharp
var addUpdate = Builders<Customer>.Update.Push(c => c.Tags, "newtag");
await collection.UpdateOneAsync(filter, addUpdate);
```

### DELETE

```csharp
var filter = Builders<Customer>.Filter.Eq(c => c.Id, customerId);
await collection.DeleteOneAsync(filter);
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
