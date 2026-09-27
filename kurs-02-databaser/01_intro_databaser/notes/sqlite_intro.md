# Introduktion till SQLite

## Vad är SQLite?

SQLite är en lättviktig, filbaserad relationsdatabas. Till skillnad från MySQL och PostgreSQL kräver den ingen separat serverprocess — hela databasen lagras i en enda fil på disk.

Det gör SQLite till det enklaste sättet att komma igång med en riktig relationsdatabas. Ingen installation, ingen konfiguration, inga tjänster att starta.

---

## Huvudegenskaper

- **Serverless** — ingen separat process, ingen konfiguration
- **Självständig** — hela databasen är en enda `.db`-fil
- **Portabel** — filen kan kopieras, mailas, versionshanteras
- **ACID-kompatibel** — pålitlig även vid krasch (se notes/transaktioner.md)
- **Plattformsoberoende** — Windows, Mac, Linux, Android, iOS

SQLite är troligen den mest använda databasen i världen. Firefox lagrar bokmärken i SQLite. Android och iOS-appar använder SQLite för lokal datalagring. Det är inte en leksaksdatabas — det är ett genomtänkt verktyg för rätt situationer.

---

## Rätt verktyg för rätt situation

SQLite passar när:
- En person eller app skriver åt gången
- Databasen ska följa med applikationen — inte ligga på en separat server
- Du utvecklar och testar — noll friktion

SQLite passar inte när:
- Tusentals användare skriver parallellt
- Databasen ska nås från flera servrar samtidigt

Det är inte en svaghet — det är en designbeslut. Vet du varför SQLite inte passar, vet du när du ska välja MySQL eller PostgreSQL istället.

---

## Skapa en databas i C#

```csharp
var connectionString = "Data Source=shop.db";
using var connection = new SqliteConnection(connectionString);
connection.Open();
```

Filen `shop.db` skapas automatiskt i projektmappen om den inte redan finns. Det är hela setupen.

---

## Skapa en tabell

```csharp
var cmd = connection.CreateCommand();
cmd.CommandText = """
    CREATE TABLE IF NOT EXISTS böcker (
        id         INTEGER PRIMARY KEY AUTOINCREMENT,
        titel      TEXT    NOT NULL,
        författare TEXT    NOT NULL,
        år         INTEGER
    );
""";
cmd.ExecuteNonQuery();
```

`IF NOT EXISTS` gör att du kan köra samma kod flera gånger utan att krascha. `AUTOINCREMENT` räknar upp `id` automatiskt — du behöver aldrig ange det manuellt.

---

## Lägg till data

```csharp
var insert = connection.CreateCommand();
insert.CommandText = """
    INSERT INTO böcker (titel, författare, år)
    VALUES ('Pragmatic Programmer', 'Hunt & Thomas', 1999);
""";
insert.ExecuteNonQuery();
```

`ExecuteNonQuery()` används för operationer som inte returnerar rader: INSERT, UPDATE, DELETE.

---

## Läs data

```csharp
var select = connection.CreateCommand();
select.CommandText = "SELECT * FROM böcker";

using var reader = select.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["id"]}: {reader["titel"]}");
}
```

`ExecuteReader()` returnerar en reader som du stegar igenom rad för rad med `reader.Read()`. Kolumnvärden hämtas med kolumnnamn som index: `reader["titel"]`.

---

## DB Browser for SQLite

DB Browser for SQLite är ett gratis GUI-verktyg som låter dig se och redigera din `.db`-fil visuellt. Ladda ner på **sqlitebrowser.org** (se installationsguiden).

Använd det parallellt med koden — öppna `shop.db` i DB Browser och se exakt vad din kod gör mot databasen. Det är det snabbaste sättet att felsöka och förstå vad som faktiskt händer.

---

## Verktygslåda

| Metod | Används för |
|-------|-------------|
| `ExecuteNonQuery()` | INSERT, UPDATE, DELETE, CREATE TABLE |
| `ExecuteReader()` | SELECT — rader att läsa |
| `ExecuteScalar()` | SELECT som returnerar ett enda värde (t.ex. COUNT) |

---

## Öva

1. Skapa en ny konsollapp och lägg till NuGet-paketet `Microsoft.Data.Sqlite`.
2. Skapa en databas `bibliotek.db` med tabellen `böcker`.
3. Lägg till tre böcker med INSERT.
4. Skriv ut alla böcker med SELECT och ExecuteReader.
5. Öppna `bibliotek.db` i DB Browser och verifiera att datan stämmer.
