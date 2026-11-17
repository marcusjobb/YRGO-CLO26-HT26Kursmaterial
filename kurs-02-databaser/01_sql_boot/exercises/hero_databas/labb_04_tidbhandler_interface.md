# Labb 4 — TiDBHandler + Interface 🟡

## Vad du ska bygga

`SQLiteHandler` funkar bra. Nu vill vi också kunna lagra heroes i molnet, i TiDB Serverless (en gratis MySQL-kompatibel molndatabas). Problemet: TiDB-handlern kommer att se nästan identisk ut. Samma metoder, nästan samma kod. Det luktar kopiering. Lösningen är ett interface.

*Förutsättning: du har `SQLiteHandler` och `Hero`-klassen från labb 3.*

---

## ⚠️ TiDB-lösenord i appsettings.json

Du har läst connectionstring från `appsettings.json` sedan labb 1, och det gör vi alltid. Nu innehåller den dessutom en riktig hemlighet: TiDB-lösenordet. Kontrollera att `appsettings.json` finns i `.gitignore` innan du lägger in lösenordet — annars kan det läcka vid nästa commit.

---

## Steg

1. Skapa ett konto på [tidbcloud.com](https://tidbcloud.com) och starta ett gratis Serverless-kluster. Lägg connectionstring i `appsettings.json`. Nyckeln `Database` väljer vilken som används, och `CreateDb` innehåller databasspecifik SQL för `CreateDB`:
   ```json
   {
     "ConnectionStrings": {
       "Sqlite": "Data Source=heroes.db",
       "TiDB": "Server=HOST;Port=4000;Database=heroes;User=USER;Password=LÖSENORD;SslMode=Required"
     },
     "CreateDb": {
       "Sqlite": "",
       "TiDB": "CREATE DATABASE IF NOT EXISTS heroes"
     },
     "Database": "Sqlite"
   }
   ```
   Uppdatera `appsettings.example.json` med platshållare för TiDB-connectionstringen.

   *Har du inte hunnit skapa något TiDB-konto? Du kan testa mot MySQL i Docker från föreläsningen. Connectionstring: `Server=localhost;Port=3306;Database=heroes;User=root;Password=DITT_LÖSENORD;SslMode=None;AllowPublicKeyRetrieval=True`.*

2. Lägg till NuGet-paketet för MySQL:
   ```
   dotnet add package MySqlConnector
   ```

3. Skapa `TiDBHandler` med exakt samma metoder som `SQLiteHandler`. Två saker skiljer sig av nödvändighet:
   - SQL-dialekten: `AUTO_INCREMENT` i stället för `AUTOINCREMENT`, `VARCHAR(255)` i stället för `TEXT`, och id:t får du från `cmd.LastInsertedId`
   - `CreateDB`: här är `CREATE DATABASE` ett riktigt steg, och databasen finns kanske inte än. Du måste därför ansluta *utan* databasnamn (`MySqlConnectionStringBuilder` med `Database = ""`)

4. Jämför de två klasserna — vad är identiskt? Vad skiljer? Notera det i en kommentar eller i din README. Fundera särskilt: `CreateDB` finns på `SQLiteHandler` men gör ingenting där. Vad säger det om vår abstraktion?

5. Skapa interface `IDbHandler`:
   ```csharp
   public interface IDbHandler
   {
       void CreateDB(string sql);
       void DropTable();
       void CreateTable(Hero hero);
       void Insert(Hero hero);
       void Delete(int id);
       void Delete(string name);
       void Update(Hero hero);
       Hero? Find(string name);
       List<Hero> Search(string token);
   }
   ```
   Låt både `SQLiteHandler` och `TiDBHandler` implementera `IDbHandler`

6. I `Program.cs`: läs vilken databas som ska användas från `appsettings.json` (nyckeln `"Database"`) och skapa rätt handler:
   ```csharp
   var provider = config["Database"] ?? "Sqlite";
   var connectionString = config.GetConnectionString(provider)!;
   IDbHandler handler = provider == "TiDB"
       ? new TiDBHandler(connectionString)
       : new SQLiteHandler(connectionString);
   handler.CreateDB(config[$"CreateDb:{provider}"] ?? "");
   ```
   Nu byter du databas genom att ändra ett värde i `appsettings.json` — resten av koden rör du inte.

7. Kör programmet mot SQLite. Byt till TiDB (`"Database": "TiDB"`). Kör igen. Resultatet ska vara identiskt.

## Förväntad output

*(Samma utskrift som i labb 3, oavsett vilken databas som valts)*

```
=== Databas skapad ===
=== Tabell skapad från Hero-klass ===

=== Infoga heroes ===
Bella inlagd (id: 1)
Carlos inlagd (id: 2)
Zara inlagd (id: 3)

=== Find: Zara ===
Hero { Id = 3, Name = Zara, Age = 22, City = Stockholm }

=== Search: ar ===
Hero { Id = 2, Name = Carlos, Age = 34, City = Göteborg }
Hero { Id = 3, Name = Zara, Age = 22, City = Stockholm }

=== Update: Bella ===
Bella bor nu i Lund.

=== Find: Batman ===
Hittade ingen hero med det namnet.

=== Delete: Carlos ===
Carlos borttagen. Kvar: 2 heroes.
```

## Kriterier

**G**
- `IDbHandler`-interfacet finns och implementeras av båda handlers
- Programmet kör mot SQLite
- Att byta från `"Sqlite"` till `"TiDB"` i `appsettings.json` gör att programmet kör mot TiDB utan kodändringar
- TiDB-lösenordet finns inte i git

**VG**
- Du kan förklara vad ett interface är och varför det behövs här
- Du kan peka på vilka rader i `SQLiteHandler` och `TiDBHandler` som skiljer sig, och varför
- Du kan förklara varför `Program.cs` inte längre bryr sig om vilken databas som används
- Du kan förklara varför `CreateDB` är ett exempel på att abstraktionen läcker
