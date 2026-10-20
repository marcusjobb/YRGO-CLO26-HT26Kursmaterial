# SQL Injection, lösenordshashning och UPDATE

> **Vad demonstrerar detta?**
> SQL injection · lösenordshashning · saltning · UPDATE · parametriserade frågor
>
> Vi bygger en enkel inloggning i tre steg — och visar att hashning **inte** skyddar mot SQL injection.

---

## Del 1 — Naiv inloggning (lösenord i klartext)

Tabellen ser ut så här. Lösenordet lagras i klartext — aldrig göra detta på riktigt.

```sql
CREATE TABLE users (
    id   INTEGER PRIMARY KEY AUTOINCREMENT,
    username TEXT NOT NULL,
    password TEXT NOT NULL
);

INSERT INTO users (username, password) VALUES ('admin', 'hemligt123');
```

En naiv C#-inloggning bygger SQL-strängen direkt från användarinput:

```csharp
string username = Console.ReadLine(); // användaren skriver in detta
string password = Console.ReadLine();

// ⚠️ ALDRIG göra såhär — input sys ihop direkt i SQL-strängen
string sql = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";
```

Nu kan angriparen skriva in: `' OR '1'='1` som användarnamn och lösenord.

SQL-strängen blir då:

```sql
SELECT * FROM users WHERE username = '' OR '1'='1' AND password = '' OR '1'='1'
-- '1'='1' är alltid sant → inloggning lyckas utan rätt lösenord
```

---

## Del 2 — Vi hashar lösenordet. Är vi säkra nu?

Vi saltar med `1337` och hashar med SHA-256 innan vi sparar lösenordet.
Salt = ett extra värde vi slänger in för att göra hash-resultatet unikt och svårare att knäcka.

```csharp
using System.Security.Cryptography;
using System.Text;

static string HashaMedSalt(string lösenord, string salt)
{
    // Slå ihop salt + lösenord, sedan hasha hela strängen
    string saltadSträng = salt + lösenord;
    byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltadSträng));
    return Convert.ToHexString(bytes).ToLower();
}

string salt     = "1337"; // i verkligheten: unikt per användare, lagrat i databasen
string hashad   = HashaMedSalt("hemligt123", salt);

Console.WriteLine(hashad);
// → något i stil med: 7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d906
```

Vi uppdaterar nu lösenordet i databasen med UPDATE:

```sql
-- Vi byter ut klartextlösenordet mot det hashade
UPDATE users
SET password = '7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d906'
WHERE username = 'admin';
```

Och inloggningen hashar lösenordet innan jämförelse:

```csharp
// Hasha det inmatade lösenordet med samma salt innan vi jämför
string inmatat  = Console.ReadLine();
string testad   = HashaMedSalt(inmatat, "1337");

string sql = $"SELECT * FROM users WHERE username = '{username}' AND password = '{testad}'";
```

Nu testar angriparen igen med `' OR '1'='1`. SQL-strängen blir:

```sql
SELECT * FROM users WHERE username = '' OR '1'='1' AND password = 'a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3'
-- username-delen: '' OR '1'='1' → sant
-- Inloggning lyckas ÄNDÅ — hashning hjälpte inte ett dugg mot injektionen
```

Lägg märke till: hashningen skyddade lösenordet *om* databasen hade läckt — men den stoppade inte injektionen alls. Det är två separata hot, och båda behöver lösas.

---

## Del 3 — Den riktiga lösningen: parametriserade frågor

Parametriserade frågor skickar input och SQL separat — databasen behandlar aldrig input som SQL-kod.

```csharp
using Microsoft.Data.Sqlite;

// Hasha det inmatade lösenordet
string testad = HashaMedSalt(inmatat, "1337");

using var connection = new SqliteConnection("Data Source=demo.db");
connection.Open();

using var cmd = connection.CreateCommand();

// @ framför variabelnamnet = parameter, inte inbakad sträng
cmd.CommandText = "SELECT * FROM users WHERE username = @user AND password = @pwd";
cmd.Parameters.AddWithValue("@user", username);
cmd.Parameters.AddWithValue("@pwd", testad);

using var reader = cmd.ExecuteReader();

if (reader.Read())
    Console.WriteLine("Inloggad!");
else
    Console.WriteLine("Fel användarnamn eller lösenord.");
```

Angriparen skriver in `' OR '1'='1` igen. Databasen hanterar det nu som en *textsträng*, inte SQL — och hittar ingen användare med det namnet. Inloggning misslyckades.

---

## Hela koden

```csharp
using System.Security.Cryptography;
using System.Text;
using Microsoft.Data.Sqlite;

// ── Hjälpmetod: salta + hasha lösenordet ──────────────────────────────────
static string HashaMedSalt(string lösenord, string salt)
{
    string saltadSträng = salt + lösenord;
    byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(saltadSträng));
    return Convert.ToHexString(bytes).ToLower();
}

const string SALT = "1337"; // ⚠️ i produktion: unikt per användare, lagrat i databasen

// ── Databas: skapa tabell + lägg in admin med hashad lösenord ─────────────
using var connection = new SqliteConnection("Data Source=demo.db");
connection.Open();

using (var setup = connection.CreateCommand())
{
    setup.CommandText = """
        CREATE TABLE IF NOT EXISTS users (
            id       INTEGER PRIMARY KEY AUTOINCREMENT,
            username TEXT NOT NULL,
            password TEXT NOT NULL
        );
        """;
    setup.ExecuteNonQuery();
}

string hashad = HashaMedSalt("hemligt123", SALT);

// UPDATE: byt ut lösenordet (eller INSERT om tabellen är tom)
using (var upsert = connection.CreateCommand())
{
    upsert.CommandText = """
        INSERT INTO users (username, password) VALUES (@user, @pwd)
        ON CONFLICT(username) DO UPDATE SET password = @pwd;
        """;
    upsert.Parameters.AddWithValue("@user", "admin");
    upsert.Parameters.AddWithValue("@pwd", hashad);
    upsert.ExecuteNonQuery();
}

// ── Inloggning med parametriserade frågor ─────────────────────────────────
Console.Write("Användarnamn: ");
string username = Console.ReadLine()!;

Console.Write("Lösenord: ");
string inmatat = Console.ReadLine()!;

string testad = HashaMedSalt(inmatat, SALT);

using var cmd = connection.CreateCommand();
cmd.CommandText = "SELECT * FROM users WHERE username = @user AND password = @pwd";
cmd.Parameters.AddWithValue("@user", username);
cmd.Parameters.AddWithValue("@pwd", testad);

using var reader = cmd.ExecuteReader();

if (reader.Read())
    Console.WriteLine("✅ Inloggad!");
else
    Console.WriteLine("❌ Fel användarnamn eller lösenord.");
```

> **NuGet som krävs:** `Microsoft.Data.Sqlite`
>
> **Kom ihåg:** `1337` är ett skämtsalt för demoändamål.
> I produktion: använd ett unikt, slumpmässigt salt per användare (t.ex. `Guid.NewGuid().ToString()`) och lagra det i databasen bredvid lösenords-hashen. Ännu bättre: byt SHA-256 mot `BCrypt.Net` eller `Argon2`.
