# SQL från C# till databasen — en rymdmanga

Det finns två sätt att prata med en databas från C#. Antingen *skickar* du något (skapa, ändra, radera) och bryr dig inte om svaret, eller så *frågar* du efter data och vill ha den tillbaka.

Besättningen på fraktskeppet **Kometen** visar hur båda sätten fungerar.

| | | |
|---|---|---|
| ![Sora](manga/sora.jpg) | **Kapten Sora** | fattar besluten |
| ![Yuki](manga/yuki.jpg) | **Yuki** | kommunikation, öppnar och stänger kanaler |
| ![Kenta](manga/kenta.jpg) | **Kenta** | ingenjör, bygger kommandona |
| ![Hoshi](manga/hoshi.jpg) | **Doktor Hoshi** | analytiker, tar emot och kontrollerar data |
| ![Raiko](manga/raiko.jpg) | **Raiko** | pirat, inte någon vän |
| ![Mamo](manga/mamo.jpg) | **Mamo** | handlare med en kappa full av fickor |

> Koden använder NuGet-paketet `Microsoft.Data.Sqlite`, samma som i [SQLite i C#](../examples/2026-10-07_sqlite_i_csharp.md). Där står hur du skapar projektet och installerar paketet.

## Kapitel 1 — Piratmetoden: kommunikation utan förväntat svar

![Sora](manga/sora.jpg)

**Sora:** Ett skepp närmar sig! Yuki, anropa det!

```csharp
using var conn = new SqliteConnection("Data Source=Space.db");
```

![Yuki](manga/yuki.jpg)

**Yuki:** Öppnar kommunikationskanalen!

```csharp
conn.Open();
```

![Raiko](manga/raiko.jpg)

**Raiko:** Hahaha! Lämna över lasten, annars blir ni rymdskrot!

```csharp
// (Det här var ingen förhandling)
```

![Sora](manga/sora.jpg)

**Sora:** Kenta, förbered för strid!

```csharp
string sql = "DELETE FROM SpaceShips WHERE Ship = @ship";
```

![Kenta](manga/kenta.jpg)

**Kenta:** Skölden uppe! Laddar kanonerna!

```csharp
using var cmd = new SqliteCommand(sql, conn);
```

![Sora](manga/sora.jpg)

**Sora:** Sikta på piratskeppet!

```csharp
cmd.Parameters.AddWithValue("@ship", "PirateShip");
```

![Sora](manga/sora.jpg)

**Sora:** ELD!

```csharp
int rows = cmd.ExecuteNonQuery();
```

![Hoshi](manga/hoshi.jpg)

**Hoshi:** *justerar glasögonen* Kontrollerar att piratskeppet är borta.

```csharp
if (rows == 1)
{
    Console.WriteLine("Bekräftat. Ett piratskepp mindre.");
}
```

`ExecuteNonQuery()` returnerar **antalet rader som påverkades**. Om Hoshi får `0` har vi missat, för då fanns det inget skepp med det namnet.

## Kapitel 2 — Handlarmetoden: kommunikation som returnerar en DataTable

![Hoshi](manga/hoshi.jpg)

**Hoshi:** Ett skepp närmar sig. Jag förbereder minnesplats för informationsutbyte.

```csharp
DataTable dt = new DataTable(); // kräver using System.Data;
```

![Sora](manga/sora.jpg)

**Sora:** Yuki, anropa skeppet!

```csharp
using var conn = new SqliteConnection("Data Source=Space.db");
```

![Yuki](manga/yuki.jpg)

**Yuki:** Öppnar kommunikationskanalen!

```csharp
conn.Open();
```

![Mamo](manga/mamo.jpg)

**Mamo:** Fufufu... jag har precis det ni söker, kapten.

```csharp
// (Han har det alltid. Frågan är bara vad det kostar.)
```

![Sora](manga/sora.jpg)

**Sora:** Visa vad du har.

```csharp
string sql = "SELECT * FROM MerchantItems WHERE Merchant = @merchant";
```

![Yuki](manga/yuki.jpg)

**Yuki:** Förbereder sökning av varor.

```csharp
using var cmd = new SqliteCommand(sql, conn);
```

![Yuki](manga/yuki.jpg)

**Yuki:** Skickar över sökparametrar.

```csharp
cmd.Parameters.AddWithValue("@merchant", "Mamo");
```

![Hoshi](manga/hoshi.jpg)

**Hoshi:** Förbereder överföring av hans data till oss.

```csharp
using var reader = cmd.ExecuteReader();
```

![Hoshi](manga/hoshi.jpg)

**Hoshi:** Tar emot information.

```csharp
dt.Load(reader);
```

![Yuki](manga/yuki.jpg)

**Yuki:** All data är mottagen. Stänger kanalen.

```csharp
return dt;
```

![Kenta](manga/kenta.jpg)

**Kenta:** Eh... varför står det en låda med "GRATISPROV" i lastrummet?

![Mamo](manga/mamo.jpg)

**Mamo:** *redan långt borta* Fufufu...

`ExecuteReader()` öppnar strömmen av rader, och `dt.Load(reader)` lägger dem i en `DataTable` som du sedan kan loopa igenom:

```csharp
foreach (DataRow row in dt.Rows)
{
    Console.WriteLine($"{row["Item"]} {row["Price"]}");
}
```

## Kapitel 3 — Parametern som hette fel

![Kenta](manga/kenta.jpg)

**Kenta:** Kapten! Programmet kraschade!

```csharp
string sql = "SELECT * FROM MerchantItems WHERE Merchant = @name";
// ...
cmd.Parameters.AddWithValue("@merchant", "Mamo");
```

```
System.InvalidOperationException: Must add values for the following parameters: @name
```

![Hoshi](manga/hoshi.jpg)

**Hoshi:** Logiskt. SQL-satsen frågar efter `@name`, men vi skickade `@merchant`. Namnen måste vara **exakt samma** på båda ställena.

Felmeddelandet talar om vilken parameter i SQL-satsen som saknar värde. Läs det, så vet du var du ska leta.

> I [SQLite i C#](../examples/2026-10-07_sqlite_i_csharp.md) skriver vi parametrarna som `$name`. `Microsoft.Data.Sqlite` förstår både `@`, `$` och `:`, så välj ett av dem och håll fast vid det.

## Sammanfattning

**Piratmetoden** använder du när du ska *manipulera* data (skapa, ändra eller radera) och inte behöver ta emot några rader:

- Du kör `ExecuteNonQuery()`, och den returnerar antalet påverkade rader.
- Vill du ha id:t på en rad du nyss har lagt in (`INSERT`) frågar du efter det med `SELECT last_insert_rowid()` och `ExecuteScalar()`. Det visas i [SQLite i C#, avsnitt 8](../examples/2026-10-07_sqlite_i_csharp.md#8-vilket-id-fick-vi).

**Handlarmetoden** använder du när du *söker* data och vill ha den tillbaka:

- Du kör `ExecuteReader()` och lägger resultatet i en `DataTable` med `dt.Load(reader)`.
- En `DataTable` kan innehålla flera rader (`dt.Rows`). Varje `DataRow` är **en rad** i resultatet.
- Om `dt.Rows.Count == 0` hittade databasen inget som matchade din sökning.

**Använd alltid parametrar**, oavsett om värdet är text eller en siffra. Det skyddar mot SQL injection, och du slipper tänka på citattecken och decimaltecken.

## Testa själv

Vill du köra exemplen behöver databasen tabellerna och lite data. Kör den här koden en gång innan:

```csharp
using var setup = new SqliteConnection("Data Source=Space.db");
setup.Open();
using var c = setup.CreateCommand();
c.CommandText = """
    CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL);
    INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip');

    CREATE TABLE IF NOT EXISTS MerchantItems (
        Merchant TEXT NOT NULL,
        Item     TEXT NOT NULL,
        Price    INTEGER
    );
    INSERT INTO MerchantItems VALUES
        ('Mamo', 'Hyperdriftskristall', 500),
        ('Mamo', 'Snabbnudlar (12-pack)', 8),
        ('Mamo', 'Gratisprov', 0),
        ('Okänd handlare', 'Begagnad robotarm', 120);
    """;
c.ExecuteNonQuery();
```

Handlarmetoden ger då tre rader. Den sista är förstås ett gratisprov.
