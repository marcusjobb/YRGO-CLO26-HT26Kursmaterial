---
marp: true
theme: nion-dark
paginate: true
---

# SQLite
## En hel databas i en enda fil

<!-- SQLite är starten för hela databaskursen. Det är enkelt att komma igång, kräver noll konfiguration, och är ändå en riktig, produktionsgodkänd databas. Firefox, Android, iOS — alla använder SQLite. -->

---

# Vad är SQLite?

En **relationsdatabas** — men utan server.

Hela databasen bor i **en fil** på disk: `shop.db`, `users.db`, `whatever.db`.

Ingen installation. Ingen serverprocess. Ingen konfiguration.

```
shop.db  ← det är hela databasen
```

<!-- Visa filen i filutforskaren eller terminalen. Det är ganska häftigt att hela databasen är en fil som du kan kopiera, flytta, och bifoga till ett mejl. -->

---

# Varför SQLite?

- **Serverless** — inget att starta, inget att konfigurera
- **Portabel** — databasen följer med appen
- **Fullt ACID-kompatibel** — pålitlig även vid krasch
- **Används överallt** — Firefox, Android, iOS, Raspberry Pi

> SQLite är troligen den mest använda databasen i världen.

<!-- Det brukar överraska folk. De tänker "MySQL är den riktiga", men SQLite körs på miljarder enheter. Det är inte en leksaksdatabas — det är ett genomtänkt verktyg för rätt situationer. -->

---

# Rätt verktyg för rätt situation

SQLite passar när:
- En person (eller app) skriver åt gången
- Databasen ska följa med koden — inte ligga på en server
- Du utvecklar och testar — noll friktion att komma igång

SQLite passar **inte** när:
- Tusentals användare skriver **samtidigt**
- Databasen ska nås från flera servrar

<!-- Inga verktyg är universella. SQLite-begränsningarna är välkända och enkla att förstå — det är inte en svaghet, det är en designbeslut. -->

---

# Skapa en databas

```csharp
// Filen skapas automatiskt om den inte finns
var connectionString = "Data Source=shop.db";
using var connection = new SqliteConnection(connectionString);
connection.Open();
```

Det är allt. Filen `shop.db` skapas i projektmappen.

Öppna den med **DB Browser for SQLite** och du ser databasen direkt.

<!-- Live-demo här: skapa en enkel konsollapp, kör den, visa att shop.db dyker upp. Öppna i DB Browser. Två fönster sida vid sida — kod och databas. -->

---

# Skapa en tabell

```csharp
var cmd = connection.CreateCommand();
cmd.CommandText = """
    CREATE TABLE IF NOT EXISTS böcker (
        id      INTEGER PRIMARY KEY AUTOINCREMENT,
        titel   TEXT    NOT NULL,
        författare TEXT NOT NULL,
        år      INTEGER
    );
""";
cmd.ExecuteNonQuery();
```

`IF NOT EXISTS` — kör om igen utan att krascha.

<!-- Förklara PRIMARY KEY AUTOINCREMENT — id räknas upp automatiskt, du behöver inte tänka på det. TEXT och INTEGER är SQLites två vanligaste typer. -->

---

# Lägg till data

```csharp
var insert = connection.CreateCommand();
insert.CommandText = """
    INSERT INTO böcker (titel, författare, år)
    VALUES ('Pragmatic Programmer', 'Hunt & Thomas', 1999);
""";
insert.ExecuteNonQuery();
```

Kör i DB Browser efteråt — raden finns där.

<!-- Visa i DB Browser att raden dök upp. Fråga: "Vad händer om vi kör INSERT igen?" — ett till exemplar läggs till, med ett nytt id. AUTOINCREMENT garanterar unikhet. -->

---

# Läs data

```csharp
var select = connection.CreateCommand();
select.CommandText = "SELECT * FROM böcker";

using var reader = select.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine($"{reader["id"]}: {reader["titel"]}");
}
```

`ExecuteReader()` returnerar rader en i taget — `reader.Read()` stegar framåt.

<!-- ExecuteReader är för SELECT, ExecuteNonQuery är för INSERT/UPDATE/DELETE. Poängtera att Reader måste stängas — därav using. -->

---

# DB Browser for SQLite

Ditt visuella fönster in i databasen.

- Se tabeller och data utan att skriva SQL
- Kör SQL-frågor och se resultat direkt
- Perfekt för att felsöka — se vad din kod faktiskt gör

```
Kod → skriver till shop.db → DB Browser visar resultatet
```

Nedladdning: **sqlitebrowser.org**

<!-- Öppna DB Browser parallellt med VS Code under hela kursen. Det är det snabbaste sättet att förstå vad koden gör mot databasen. -->

---

# Sammanfattning

- SQLite = en fil, ingen server, ACID-kompatibel
- `SqliteConnection` + `CreateCommand()` + `ExecuteNonQuery()` / `ExecuteReader()`
- DB Browser = visuellt felsökningsverktyg
- Passar perfekt för den här kursen — och för många riktiga projekt

Nästa steg: **Entity Framework** — C# pratar med databasen på riktigt.

<!-- EF Core genererar SQL åt dig — men du förstår det bara om du vet hur SQL fungerar. Därför kör vi raw SQL först. -->
