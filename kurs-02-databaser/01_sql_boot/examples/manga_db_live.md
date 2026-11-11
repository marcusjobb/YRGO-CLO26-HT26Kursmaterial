# MangaDB Live — SQLite från C# i fyra funktioner

**Verktyg:** .NET 10, NuGet-paketet `Microsoft.Data.Sqlite`

Det här är livekodningen där besättningen på **Kometen** möter pirater och handlare. Koden är delad i fyra funktioner: en som skapar databasen, en som raderar ett skepp, en som hämtar handlarens varor, och en som städar upp efteråt.

---

## 1. Ingångspunkten — sätt ihop delarna

Vi börjar med att definiera var databasen ska ligga, och sedan anropar vi de fyra funktionerna i ordning.

```csharp
using Microsoft.Data.Sqlite; // NuGet: Microsoft.Data.Sqlite
using System.Data;           // för DataTable

Console.WriteLine("Hello, MangaDB!");

// Sökvägen till databasfilen — ändra den här raden för att flytta databasen
const string db = "Data Source=Space.db";

SetupSpace();   // 1. Skapa tabeller och fyll på med data
Pirates();      // 2. Radera piratskeppet (ExecuteNonQuery)
Merchant();     // 3. Hämta handlarens varor (ExecuteReader + DataTable)
DeleteSpace();  // 4. Ta bort databasfilen när vi är klara
```

Nu kan vi läsa programmet uppifrån och förstå flödet utan att behöva gå in i varje funktion.

Lägg märke till att `db` är definierad på ett ställe — ändrar vi sökvägen här så följer alla funktioner med automatiskt.

---

## 2. SetupSpace — skapa tabeller och lägg in testdata

Innan vi kan köra Pirates och Merchant måste databasen ha tabeller och rader. Det är SetupSpace:s jobb.

```csharp
void SetupSpace()
{
    // Öppna (eller skapa) databasfilen — SQLite skapar filen om den inte finns
    using var setup = new SqliteConnection(db);
    setup.Open();

    // CreateCommand ger oss ett tomt kommandoobjekt kopplat till den här anslutningen
    using var c = setup.CreateCommand();

    // Ett kommando = ett SQL-statement — Microsoft.Data.Sqlite kör ett i taget
    c.CommandText = "CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL)";
    c.ExecuteNonQuery(); // ExecuteNonQuery = vi skickar något, förväntar oss inget svar

    // Vi återanvänder samma kommandoobjekt — bara CommandText byts ut
    c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
    c.ExecuteNonQuery();

    // Raw string literal """ ... """ låter oss skriva SQL på flera rader utan escape-tecken
    c.CommandText = """
        CREATE TABLE IF NOT EXISTS MerchantItems (
            Merchant TEXT NOT NULL,
            Item     TEXT NOT NULL,
            Price    INTEGER           -- INTEGER är SQLites heltaltyp
        )
        """;
    c.ExecuteNonQuery();

    c.CommandText = """
        INSERT INTO MerchantItems VALUES
            ('Mamo', 'Hyperdriftskristall', 500),
            ('Mamo', 'Snabbnudlar (12-pack)', 8),
            ('Mamo', 'Gratisprov', 0),
            ('Okänd handlare', 'Begagnad robotarm', 120)
        """;
    c.ExecuteNonQuery();
}
```

`CREATE TABLE IF NOT EXISTS` gör att programmet inte kraschar om vi råkar köra det två gånger — tabellen skapas bara om den inte redan finns.

---

## 3. Pirates — radera ett skepp med parameter

Nu drar piratskeppet. Vi skickar ett DELETE-kommando och kontrollerar att exakt en rad raderades.

```csharp
void Pirates()
{
    // Varje funktion öppnar sin egen anslutning — det är vanlig praxis
    // using-nyckelordet stänger anslutningen automatiskt när funktionen är klar
    using var conn = new SqliteConnection(db);
    conn.Open();

    // SQL-satsen med en platshållare — @ship ersätts med det riktiga värdet nedan
    // Vi skriver ALDRIG in värdet direkt i strängen (SQL injection-risk)
    string sql = "DELETE FROM SpaceShips WHERE Ship = @ship";

    // SqliteCommand kopplar SQL-satsen till anslutningen
    using var cmd = new SqliteCommand(sql, conn);

    // Här binder vi @ship till det faktiska värdet vi vill radera
    // Databasen behandlar det som data, aldrig som kod
    cmd.Parameters.AddWithValue("@ship", "PirateShip");

    // ExecuteNonQuery returnerar antalet rader som påverkades
    // Om vi får 0 tillbaka hittades inget skepp med det namnet
    int deletedRows = cmd.ExecuteNonQuery();

    if (deletedRows == 1)
    {
        Console.WriteLine("Bekräftat. Ett piratskepp mindre.");
    }
    else
    {
        // 0 rader = skeppet fanns inte, >1 = vi raderade för mycket (borde inte hända)
        Console.WriteLine($"Oväntat resultat: {deletedRows} rader raderades.");
    }
}
```

Parametern `@ship` kan heta vad som helst så länge den heter **exakt samma sak** i SQL-satsen och i `AddWithValue`. Bokstav för bokstav, inklusive `@`.

---

## 4. Merchant — hämta varor med SELECT och DataTable

Handlaren Mamo har varor. Vi hämtar dem med SELECT och lagrar resultatet i en DataTable.

```csharp
void Merchant()
{
    // DataTable är en minnestabellsom håller raderna vi hämtar från databasen
    // Vi behöver System.Data för att använda DataTable och DataRow
    DataTable dt = new DataTable();

    using var conn = new SqliteConnection(db);
    conn.Open();

    // DISTINCT = hämta bara unika kombinationer (skyddar mot dubbletter om setup körts två gånger)
    string sql = "SELECT DISTINCT * FROM MerchantItems WHERE Merchant = @merchant";

    using var cmd = new SqliteCommand(sql, conn);

    // Parametern @merchant skickas separat, precis som i Pirates()
    cmd.Parameters.AddWithValue("@merchant", "Mamo");

    // ExecuteReader öppnar en dataström — den läser en rad i taget från databasen
    using var reader = cmd.ExecuteReader();

    // dt.Load kopierar hela läsaren in i minnet på en gång
    // Efter det kan vi stänga anslutningen och ändå ha kvar alla rader
    dt.Load(reader);

    // dt.Rows är en samling DataRow-objekt — en per rad i resultatet
    foreach (DataRow row in dt.Rows)
    {
        // Vi når kolumnvärdena via kolumnnamnet som en sträng — skiftlägeskänsligt
        Console.WriteLine($"{row["Item"]} kostar {row["Price"]} krediter");
    }
}
```

Om `dt.Rows.Count == 0` returnerade sökningen inga rader — antingen finns inte handlaren eller så är tabellen tom.

---

## 5. DeleteSpace — städa upp databasfilen

Vi tar bort filen när vi är klara, så att nästa körning börjar från noll.

```csharp
void DeleteSpace()
{
    // Vi behöver ta ut filsökvägen ur connection string-formatet
    // "Data Source=Space.db" → "Space.db"
    string filename = db.Replace("Data Source=", "");

    if (File.Exists(filename))
    {
        File.Delete(filename);
        Console.WriteLine($"Databasen {filename} är borttagen.");
    }
}
```

I ett riktigt program tar vi aldrig bort databasen — det här finns bara för att demos ska starta rent varje gång.

---

## Hela koden

```csharp
using Microsoft.Data.Sqlite;
using System.Data;

Console.WriteLine("Hello, MangaDB!");

const string db = "Data Source=Space.db";

SetupSpace();
Pirates();
Merchant();
DeleteSpace();

void SetupSpace()
{
    using var setup = new SqliteConnection(db);
    setup.Open();
    using var c = setup.CreateCommand();

    c.CommandText = "CREATE TABLE IF NOT EXISTS SpaceShips (Ship TEXT NOT NULL)";
    c.ExecuteNonQuery();

    c.CommandText = "INSERT INTO SpaceShips VALUES ('Kometen'), ('PirateShip')";
    c.ExecuteNonQuery();

    c.CommandText = """
        CREATE TABLE IF NOT EXISTS MerchantItems (
            Merchant TEXT NOT NULL,
            Item     TEXT NOT NULL,
            Price    INTEGER
        )
        """;
    c.ExecuteNonQuery();

    c.CommandText = """
        INSERT INTO MerchantItems VALUES
            ('Mamo', 'Hyperdriftskristall', 500),
            ('Mamo', 'Snabbnudlar (12-pack)', 8),
            ('Mamo', 'Gratisprov', 0),
            ('Okänd handlare', 'Begagnad robotarm', 120)
        """;
    c.ExecuteNonQuery();
}

void Pirates()
{
    using var conn = new SqliteConnection(db);
    conn.Open();

    string sql = "DELETE FROM SpaceShips WHERE Ship = @ship";
    using var cmd = new SqliteCommand(sql, conn);
    cmd.Parameters.AddWithValue("@ship", "PirateShip");

    int deletedRows = cmd.ExecuteNonQuery();

    if (deletedRows == 1)
        Console.WriteLine("Bekräftat. Ett piratskepp mindre.");
    else
        Console.WriteLine($"Oväntat resultat: {deletedRows} rader raderades.");
}

void Merchant()
{
    DataTable dt = new DataTable();

    using var conn = new SqliteConnection(db);
    conn.Open();

    string sql = "SELECT DISTINCT * FROM MerchantItems WHERE Merchant = @merchant";
    using var cmd = new SqliteCommand(sql, conn);
    cmd.Parameters.AddWithValue("@merchant", "Mamo");

    using var reader = cmd.ExecuteReader();
    dt.Load(reader);

    foreach (DataRow row in dt.Rows)
    {
        Console.WriteLine($"{row["Item"]} kostar {row["Price"]} krediter");
    }
}

void DeleteSpace()
{
    string filename = db.Replace("Data Source=", "");
    if (File.Exists(filename))
    {
        File.Delete(filename);
        Console.WriteLine($"Databasen {filename} är borttagen.");
    }
}
```
