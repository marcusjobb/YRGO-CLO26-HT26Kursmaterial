# Labb 7 — Fler servrar 🔴 (valfri fördjupning)

## Vad du ska bygga

Du har installerat fler databasservrar i Docker. Nu ska de användas. Du skriver en ny handler för en annan server bakom samma `IDbHandler<T>`, och `Program.cs`, `HeroService` och `PetService` ska fungera utan ändringar. Den enda filen utöver den nya handlern som får ändras är den som väljer handler utifrån `appsettings.json`.

Testet är enkelt: **samma körning ska ge identisk utskrift mot varje handler.**

*Förutsättning: du har allt från labb 6, plus minst en av servrarna nedan körande i Docker.*

## Utgångsläge: en fabrik

Registreringen i `Program.cs` använder en fabrik som väljer handler utifrån `"Database"` i `appsettings.json`:

```csharp
public static class HandlerFactory
{
    public static IDbHandler<T> Create<T>(string provider, string connectionString) where T : new() =>
        provider switch
        {
            "Sqlite" => new SQLiteHandler<T>(connectionString),
            "TiDB" => new TiDBHandler<T>(connectionString),
            _ => throw new ArgumentException($"Okänd databas: {provider}")
        };
}
```

```csharp
var dbProvider = config["Database"] ?? "Sqlite";
services.AddSingleton(HandlerFactory.Create<Hero>(dbProvider, connectionString));
services.AddSingleton(HandlerFactory.Create<Pet>(dbProvider, connectionString));
```

Lägg till nya rader i `switch`-uttrycket för varje ny handler. Lägg också till en connectionstring och en `CreateDb`-rad per databas i `appsettings.json`.

## Steg

1. Välj en server:
   - **PostgreSQL** med NuGet-paketet `Npgsql`
   - **SQL Server** med NuGet-paketet `Microsoft.Data.SqlClient` (imagen är stor och kräver att du godkänner Microsofts licensvillkor, `ACCEPT_EULA=Y`)

2. Skriv `PostgresHandler<T>` (eller `SqlServerHandler<T>`) som implementerar `IDbHandler<T>`. Börja med att kopiera `TiDBHandler<T>` och byt ut det som skiljer. Dialektguiden nedan visar vad du ska leta efter.

3. Lägg till handlern i fabriken och ändra `"Database"` i `appsettings.json`.

4. Kör programmet. Jämför utskriften med labb 6, rad för rad.

## Dialektguide

| Sak | SQLite | MySQL / TiDB | PostgreSQL | SQL Server |
|-----|--------|--------------|------------|------------|
| Autoinkrement | `AUTOINCREMENT` | `AUTO_INCREMENT` | `SERIAL` | `IDENTITY(1,1)` |
| Text | `TEXT` | `VARCHAR(255)` | `TEXT` | `NVARCHAR(255)` |
| Citera identifierare | `"namn"` | `` `namn` `` | `"namn"` | `[namn]` |
| Id efter insert | `last_insert_rowid()` | `LastInsertedId` | `RETURNING id` | `OUTPUT INSERTED.id` |
| Sök utan skiftläge | `LIKE` | `LIKE` | **`ILIKE`** (`LIKE` skiljer på versaler) | `LIKE` (beror på collation) |
| `CREATE DATABASE IF NOT EXISTS` | finns inte (filen skapas) | finns | **finns inte**, du får felet "finns redan" | finns inte, kontrollera med `DB_ID` |

Två saker att tänka på:

- Du måste ansluta till en *annan* databas (till exempel `postgres` eller `master`) för att skapa din egen. Den du vill skapa finns ju inte än.
- Skriv aldrig lösenordet i koden eller i en fil som hamnar i git. Det vet du redan.

## 💡 Aha: MariaDB

MariaDB räknas **inte** som en ny handler. MariaDB pratar MySQL-protokollet, så `TiDBHandler<T>` och `MySqlConnector` fungerar oförändrade. Starta en MariaDB-container, lägg till `"MariaDB"` i fabriken så att den pekar på `TiDBHandler<T>`, och kör. Samma utskrift? Vad säger det om skillnaden mellan *databasprodukt* och *protokoll*?

## Rutan om mönsterspråk

Det vi har byggt är ett interface med flera utbytbara implementationer. I mönsterspråk ligger det närmast **Strategy** (utbytbar algoritm bakom ett interface) eller **Adapter** (anpassar olika API:er till ett gemensamt). **Facade** betyder strikt en förenklad ingång framför ett *komplext delsystem*, och det är inte riktigt vad vi gör här. I klassrummet kallar vi det gärna "facade" i vardagligt tal, men du ska känna till skillnaden.

## Förväntad output

*(Identisk med labb 6, oavsett databas)*

```
=== Databas skapad ===
=== Tabeller skapade ===

=== Heroes ===
Bella inlagd (id: 1)
Carlos inlagd (id: 2)

=== Pets ===
Mjao inlagd (id: 1, heroId: 1)
Woff inlagd (id: 2, heroId: 1)
Sture inlagd (id: 3, heroId: 2)

=== HeroService.GetWithPets: Bella ===
Hero { Id = 1, Name = Bella, Age = 28, City = Malmö }
  Pet: Mjao (Katt)
  Pet: Woff (Hund)

=== HeroService.Search: ar ===
Hero { Id = 2, Name = Carlos, Age = 34, City = Göteborg }

=== PetService.Search: o ===
Mjao, Katt (heroId: 1)
Woff, Hund (heroId: 1)

=== PetService.Get: Saknas ===
Hittade inget husdjur med det namnet.
```

## Kriterier

**G**
- En ny handler (PostgreSQL via Npgsql **eller** SQL Server via Microsoft.Data.SqlClient) klarar samma körning oförändrad
- `Program.cs`, `HeroService` och `PetService` är oförändrade från labb 6 (förutom fabriken)
- Utskriften är identisk med labb 6, även vid omkörning

**VG**
- Två nya handlers, **eller** en ny handler plus att du har testat MariaDB-observationen
- En kort skriftlig redogörelse (en halv sida) som tar upp:
  1. **Dialektskillnader du stötte på:** autoinkrement, typnamn, citering av identifierare, `LIKE` mot `ILIKE`, `TOP` mot `LIMIT`
  2. **Var abstraktionen läcker:** till exempel `CreateDB` och connectionstring-formatet
  3. **Varför MongoDB inte passar `IDbHandler<T>` rakt av:** den är inte relationell, och `CreateTable` blir meningslös
