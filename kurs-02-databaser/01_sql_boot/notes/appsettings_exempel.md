# Exempel på appsettings.json

🟢

Hur du strukturerar connection strings för olika databaser, och hur du läser dem i C#.

---

## Struktur för alla databaser

```json
{
  "ConnectionStrings": {
    "SQLite": "Data Source=app.db",

    "MySQL": "Server=localhost;Port=3306;Database=myapp;User=appuser;Password=apppassword;",

    "LocalDB": "Server=(localdb)\\MSSQLLocalDB;Database=MyApp;Integrated Security=true;",

    "SQLServer": "Server=localhost,1433;Database=MyApp;User Id=sa;Password=YourStrong!Passw0rd;TrustServerCertificate=true;",

    "MongoDB": "mongodb://admin:adminpassword@localhost:27017",

    "MongoDBAtlas": "mongodb+srv://username:password@cluster.mongodb.net/myapp"
  },

  "DatabaseSettings": {
    "Provider": "SQLite",
    "DatabaseName": "myapp"
  },

  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.EntityFrameworkCore": "Warning"
    }
  }
}
```

---

## Använda i C#

```csharp
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("SQLite")!;
string provider = config["DatabaseSettings:Provider"]!;
```

---

## Med Dependency Injection (ASP.NET Core)

```csharp
// Program.cs
services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(Configuration.GetConnectionString("SQLite")));
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
