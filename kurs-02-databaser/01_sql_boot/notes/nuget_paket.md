# NuGet-paket för databaser

🟢

Snabbreferens för vilka paket du behöver beroende på databastyp.

---

## SQLite

```bash
# Grundläggande anslutning
dotnet add package Microsoft.Data.Sqlite

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.Sqlite
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## MySQL

```bash
# Grundläggande anslutning
dotnet add package MySql.Data

# Entity Framework Core
dotnet add package Pomelo.EntityFrameworkCore.MySql
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## SQL Server / LocalDB

```bash
# Grundläggande anslutning
dotnet add package Microsoft.Data.SqlClient

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Design
```

---

## MongoDB

```bash
# MongoDB Driver (INGET EF Core)
dotnet add package MongoDB.Driver
```

---

## EF Core Tools (installeras globalt, en gång)

```bash
# Installera dotnet-ef
dotnet tool install --global dotnet-ef

# Uppdatera dotnet-ef
dotnet tool update --global dotnet-ef

# Verifiera att det fungerar
dotnet ef
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
