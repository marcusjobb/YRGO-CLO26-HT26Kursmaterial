# Träningsuppgifter: Fördjupad OOP — Vecka 1

> **Tema:** Databaskopplingar och ramverk  
> **Modul:** 01 — Databaskopplingar

## Instruktioner
Välj det bästa svaret. Klicka på 'Visa svar' för att se rätt svar och förklaringar.

### Fråga 1

Vad är Repository Pattern?

a. Ett sätt att lagra filer i databasen<br>b. Ett abstraktionslager mellan databas och affärslogik som centraliserar dataåtkomst<br>c. En metod för att skapa backup av databasen<br>d. Ett sätt att kryptera data

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett abstraktionslager mellan databas och affärslogik som centraliserar dataåtkomst

  **Förklaringar:**

  - ❌ **a) Lagra filer** - FEL: Repository handlar om mönster för dataåtkomst, inte fillagring
  - ✅ **b) Abstraktionslager** - **RÄTT**: Repository pattern döljer databaskomplexiteten bakom ett interface. Affärslogiken anropar `IRepository.GetAllAsync()` utan att veta om det är SQL Server, MongoDB eller en lista i minnet
  - ❌ **c) Backup** - FEL: Ingenting med backup att göra
  - ❌ **d) Kryptering** - FEL: Repository handlar om arkitektur, inte säkerhet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vilka fördelar ger Repository Pattern?

a. Snyggare kod, färre rader, snabbare exekvering<br>b. Enklare testning, konsekvent gränssnitt, förbättrad kodstruktur, möjlighet att byta datakälla<br>c. Automatisk backup och återställning<br>d. Mindre minnesanvändning

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Enklare testning, konsekvent gränssnitt, förbättrad kodstruktur, möjlighet att byta datakälla

  **Förklaringar:**

  - ❌ **a) Färre rader** - FEL: Repository kan kräva *mer* kod initialt, men blir lättare att underhålla
  - ✅ **b) Testbarhet, struktur, flexibilitet** - **RÄTT**: Du kan mocka repositoryt i tester, byta från SQL Server till SQLite utan att ändra affärslogik, och all dataåtkomst är på ett ställe
  - ❌ **c) Automatisk backup** - FEL: Repository handlar inte om backup
  - ❌ **d) Mindre minne** - FEL: Ingenting med minnesoptimering att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är typiskt för ett `IRepository<T, TId>` interface?

a. Metoder som GetById, GetAll, Add, Update, Delete med generiska typer<br>b. Specifika metoder för varje entitetstyp<br>c. Bara Get-metoder<br>d. Metoder som kör SQL direkt

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Metoder som GetById, GetAll, Add, Update, Delete med generiska typer

  **Förklaringar:**

  - ✅ **a) Generiska CRUD-metoder** - **RÄTT**: `IRepository<Product, int>` ger dig `GetByIdAsync(1)`, `GetAllAsync()`, `AddAsync(product)`, etc. för alla entiteter — oavsett om det är Product, User eller Order
  - ❌ **b) Specifika metoder per entitet** - FEL: Poängen med generiska repositoryt är att metoderma är återanvändbara. Specifika metoder läggs i konkreta repositoryn
  - ❌ **c) Bara Get** - FEL: Även Add, Update, Delete är standard
  - ❌ **d) SQL direkt** - FEL: Repositoryt kan innehålla SQL internt, men interfacet exponerar inte det
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad betyder `TEntity` i `IRepository<TEntity, TKey>`?

a. En specifik klass som Product eller User — bestäms när repositoryt skapas<br>b. Vilken datatyp som helst<br>c. En tabell i databasen<br>d. En interface-typ

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En specifik klass som Product eller User — bestäms när repositoryt skapas

  **Förklaringar:**

  - ✅ **a) Generisk parameter** - **RÄTT**: `IRepository<Product, int>` = TEntity är Product, TKey är int. `IRepository<User, Guid>` = TEntity är User, TKey är Guid. Samma interface, olika typer
  - ❌ **b) Vilken typ som helst** - FEL: Oftast begränsad med `where TEntity : class`
  - ❌ **c) En tabell** - FEL: TEntity är en C#-klass (entitet), inte en databastabell (även om de ofta motsvarar varandra)
  - ❌ **d) Ett interface** - FEL: TEntity är en klass, inte ett interface
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är fördelen med att använda `async/await` i ett repository?

a. Det gör koden långsammare men säkrare<br>b. Det låter tråden arbeta med annat medan databasen svarar — bättre skalbarhet<br>c. Det är obligatoriskt i .NET<br>d. Det gör att databasen svarar snabbare

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Det låter tråden arbeta med annat medan databasen svarar — bättre skalbarhet

  **Förklaringar:**

  - ❌ **a) Långsammare** - FEL: async/await är inte långsammare — det är *effektivare* under väntan
  - ✅ **b) Släpper tråden** - **RÄTT**: När du anropar `await _dbSet.ToListAsync()` släpps tråden medan databasen jobbar. Tråden kan då hantera andra anrop. Detta gör att servern kan hantera fler samtidiga användare
  - ❌ **c) Obligatoriskt** - FEL: Du kan skriva synkron kod, men async rekommenderas för I/O-operationer
  - ❌ **d) Databasen svarar snabbare** - FEL: async/await påverkar inte databasens svarstid, bara hur applikationen väntar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad är en `UnitOfWork`?

a. En enskild databasfråga<br>b. Ett mönster som grupperar flera repository-operationer i en transaktion<br>c. En måttenhet för arbete<br>d. En metod för att mäta prestanda

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster som grupperar flera repository-operationer i en transaktion

  **Förklaringar:**

  - ❌ **a) Enskild fråga** - FEL: UnitOfWork hanterar *flera* operationer som en helhet
  - ✅ **b) Transaktionshantering** - **RÄTT**: Säg att du skapar en order och uppdaterar lagersaldo — båda måste lyckas eller båda misslyckas. UnitOfWork använder en databastransaktion: `CommitAsync()` om allt går bra, `RollbackAsync()` om något går fel
  - ❌ **c) Måttenhet** - FEL: Ingenting med fysik eller mått att göra
  - ❌ **d) Prestandamätning** - FEL: UnitOfWork handlar om dataintegritet, inte prestanda
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad är en `DbSet<T>` i Entity Framework Core?

a. En tabell i databasen representerad som en samling i C#<br>b. En inställning för databasanslutning<br>c. En SQL-fråga<br>d. En typ av lista

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En tabell i databasen representerad som en samling i C#

  **Förklaringar:**

  - ✅ **a) Representerar en tabell** - **RÄTT**: `public DbSet<Product> Products { get; set; }` i din DbContext — EF Core mappar automatiskt Products-tabellen till DbSet<Product>. Du kan sedan göra LINQ-frågor mot den
  - ❌ **b) Anslutningsinställning** - FEL: Anslutningen konfigureras i OnConfiguring, inte med DbSet
  - ❌ **c) SQL-fråga** - FEL: DbSet är en representation av data, inte en fråga
  - ❌ **d) En lista** - FEL: Liknar en lista (du kan LINQa på den) men den är kopplad till databasen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad är fördelen med Dependency Injection (DI) i kombination med Repository Pattern?

a. Du slipper installera NuGet-paket<br>b. Du kan byta implementation (t.ex. SQL Server → SQLite) utan att ändra affärslogik<br>c. Det gör kodinjektioner omöjliga<br>d. Det krävs ingen konfiguration

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Du kan byta implementation utan att ändra affärslogik

  **Förklaringar:**

  - ❌ **a) Slipper NuGet** - FEL: DI påverkar inte vilka paket du använder
  - ✅ **b) Utbytbar implementation** - **RÄTT**: `services.AddScoped<IProductRepository, SqlProductRepository>()` — för att byta till MongoDB skapar du `MongoProductRepository : IProductRepository` och ändrar en rad i DI-konfigurationen. Affärslogiken märker inget
  - ❌ **c) Förhindrar injektioner** - FEL: DI handlar om beroendehantering, inte säkerhet
  - ❌ **d) Ingen konfiguration** - FEL: DI kräver konfiguration i Program.cs eller Startup.cs
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad är SQLite?

a. En molnbaserad databas<br>b. En filbaserad databas som inte kräver en separat server — perfekt för utveckling och mobila appar<br>c. En Microsoft SQL-server<br>d. Ett grafiskt verktyg för databaser

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En filbaserad databas som inte kräver en separat server

  **Förklaringar:**

  - ❌ **a) Molnbaserad** - FEL: SQLite är lokalt, inte i molnet
  - ✅ **b) Filbaserad, serverlös** - **RÄTT**: SQLite lagrar hela databasen i en enda `.db`-fil. Du behöver inte installera eller starta någon server. Används ofta för utveckling, mobila appar och inbyggda system
  - ❌ **c) Microsoft SQL-server** - FEL: SQL Server är Microsofts serverbaserade databas. SQLite är en annan produkt
  - ❌ **d) Grafiskt verktyg** - FEL: SQLite är en databasmotor, inget grafiskt verktyg
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad är migrations i Entity Framework Core?

a. Att flytta data från en databas till en annan<br>b. Ett sätt att versionhantera databasschemat så att det hålls synkat med C#-klasserna<br>c. Att flytta C#-kod till databasen<br>d. Att byta molnleverantör

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett sätt att versionhantera databasschemat så att det hålls synkat med C#-klasserna

  **Förklaringar:**

  - ❌ **a) Flytta data** - FEL: Migrations hanterar *struktur* (tabeller, kolumner), inte data
  - ✅ **b) Versionhantera databasschemat** - **RÄTT**: När du lägger till en property i en C#-klass kör du `dotnet ef migrations add AddEmailToUser`. EF genererar automatiskt `ALTER TABLE Users ADD Email TEXT`. Migrations är spårbara i git
  - ❌ **c) Flytta kod till databasen** - FEL: C#-koden stannar i C#. Migrations skapar SQL-kommandon som ändrar databasen
  - ❌ **d) Byta molnleverantör** - FEL: Ingenting med moln att göra
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>
