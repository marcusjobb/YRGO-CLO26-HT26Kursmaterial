# ORM — Programmeringstermer

## Object-Relational Mapping (ORM)
Teknik som översätter mellan objektorienterad programmering (C#) och relationsdatabaser (SQL). Låter dig arbeta med databasen som om den vore vanliga C#-objekt.

## Entity
En C#-klass som mappas till en databastabell. Varje instans av entiteten motsvarar en rad i tabellen. Exempel: Klassen `Customer` mappas till tabellen `Customers`.

## DbContext
Huvudklassen i EF Core som representerar en session med databasen. Innehåller `DbSet<T>`-egenskaper för varje tabell och hanterar anslutning, frågor, ändringsspårning och transaktioner.

```csharp
public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; }
}
```

## DbSet
Representerar en tabell i databasen. Används för att utföra CRUD-operationer via LINQ.

```csharp
context.Customers.Where(c => c.Name == "Anna").ToList();
```

## Navigation Property
En egenskap i en entity som pekar på relaterade entiteter. Exempel: `Customer.Orders` är en samling av ordrar som tillhör en kund.

## Change Tracker
Mekanism i EF Core som håller koll på ändringar i entiteter. När du anropar `SaveChanges()` vet EF Core exakt vad som behöver uppdateras, läggas till eller tas bort.

## Lazy Loading
Strategi där relaterad data laddas automatiskt först när navigation propertyn accessas. Kan orsaka N+1-problemet om man inte är försiktig.

## Eager Loading
Strategi där relaterad data laddas i samma query med `Include()` och `ThenInclude()`. Ger en databasroundtrip men laddar alltid allt.

## Migration
Versionshantering för databasschema. En migration beskriver vad som ändrats (nya tabeller, kolumner, etc.) och hur man ångrar det.

```bash
Add-Migration AddEmailToCustomer
Update-Database
```

## N+1 Problem
Prestandaproblem där en loop genererar en query för varje iteration. Lösning: använd `Include()` (Eager Loading) istället för Lazy Loading i loopar.

## Provider (Database Provider)
EF Core-drivrutin för en specifik databas: `UseSqlite()`, `UseMySql()`, `UseSqlServer()`, `UseInMemory()`.

## SaveChanges
Metod som skickar alla spårade ändringar till databasen i en transaktion.

## AsNoTracking
Instruktion till EF Core att inte spåra entiteter. Ger bättre prestanda för read-only queries.
