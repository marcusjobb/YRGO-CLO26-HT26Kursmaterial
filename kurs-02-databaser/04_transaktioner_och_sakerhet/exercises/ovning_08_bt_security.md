# Övning 8 — BT Security 🔴

> *"Welcome to BT Security. Please enter your credentials."*

Du bygger inloggningssystemet till en bank. Riktiga konton, riktig databas — och ett gapande säkerhetshål.

Programmet loopar tills användaren loggar in eller väljer att avsluta. Det ska kännas som ett riktigt system.

---

## Steg 1 — Skapa databasen och seed-data

Nytt konsolprojekt (`BT_Security`) med `Microsoft.Data.Sqlite`.

```csharp
using Microsoft.Data.Sqlite;

var connection = new SqliteConnection("Data Source=banken.db");
connection.Open();

// Skapa kontotabell
var create = connection.CreateCommand();
create.CommandText = """
    CREATE TABLE IF NOT EXISTS Konton (
        Id INTEGER PRIMARY KEY AUTOINCREMENT,
        Username TEXT NOT NULL,
        Password TEXT NOT NULL,
        Roll TEXT NOT NULL
    )
    """;
create.ExecuteNonQuery();

// Lägg in konton om tabellen är tom
var count = connection.CreateCommand();
count.CommandText = "SELECT COUNT(*) FROM Konton";
long antal = (long)(count.ExecuteScalar() ?? 0);

if (antal == 0)
{
    var seed = connection.CreateCommand();
    seed.CommandText = """
        INSERT INTO Konton (Username, Password, Roll) VALUES
        ('kelce', '1387', 'Kund'),
        ('admin', '1337', 'Administratör')
        """;
    seed.ExecuteNonQuery();
    Console.WriteLine("Konton skapade.\n");
}
```

---

## Steg 2 — Inloggningsloopen med strängkonkatenering (sårbar version)

Loopen frågar om username och password. Vid fel: försök igen. Vid rätt: välkommen-meddelande och programmet avslutas.

```csharp
Console.WriteLine("╔══════════════════════════════╗");
Console.WriteLine("║     BT Security Bank         ║");
Console.WriteLine("╚══════════════════════════════╝");
Console.WriteLine();

while (true)
{
    Console.Write("Ange ditt username (eller 'q' för att avsluta): ");
    string username = Console.ReadLine() ?? "";

    if (username.ToLower() == "q") break;

    Console.Write("Ange ditt password: ");
    string password = Console.ReadLine() ?? "";

    // ⚠️ SÅRBAR — strängkonkatenering rakt in i SQL
    var login = connection.CreateCommand();
    login.CommandText =
        "SELECT Id, Username, Roll FROM Konton " +
        "WHERE Username = '" + username + "' AND Password = '" + password + "'";

    using var reader = login.ExecuteReader();

    if (reader.Read())
    {
        string namn = reader["Username"].ToString()!;
        string roll = reader["Roll"].ToString()!;
        Console.WriteLine();
        Console.WriteLine($"✅ Välkommen, {namn}! Du är inloggad som {roll}.");
        break;
    }
    else
    {
        Console.WriteLine("❌ Fel lösenord. Försök igen.\n");
    }
}
```

Testa med:
- username `kelce`, password `1387` → ska fungera
- username `admin`, password `1337` → ska fungera
- username `admin`, password `felfel` → ska misslyckas

---

## Steg 3 — Bobby Tables loggar in

Nu testar du det här som inloggning:

```
username: admin
password: ' OR '1'='1
```

### Vad tror du händer?

...

### Vad som faktiskt händer

```
✅ Välkommen, kelce! Du är inloggad som Kund.
```

Du är inloggad. Utan rätt lösenord. Som fel person.

**Varför?** SQL:en som körs mot databasen ser ut så här:

```sql
SELECT Id, Username, Roll FROM Konton
WHERE Username = 'admin' AND Password = '' OR '1'='1'
```

Operatorprioritet: `AND` körs före `OR`, så det läses som:

```sql
WHERE (Username = 'admin' AND Password = '') OR ('1'='1')
```

Vänster del är falsk (inget konto med tomt lösenord). Höger del är alltid sann.  
Hela villkoret är sant för **varje rad i tabellen** — databasen returnerar alla konton.  
Programmet tar det första den hittar — råkar vara Kelce — och loggar in dig som hen.

Du behövde inte veta lösenordet. Du behövde inte veta vem som finns i systemet. Du behövde bara veta att SQL-injection existerar.

---

## Steg 4 — Fixa det

Ersätt den sårbara login-frågan med parametriserade queries:

```csharp
// Steg 4: Parametriserad query — säker version
var login = connection.CreateCommand();
login.CommandText =
    "SELECT Id, Username, Roll FROM Konton " +
    "WHERE Username = @username AND Password = @password";
login.Parameters.AddWithValue("@username", username);
login.Parameters.AddWithValue("@password", password);
```

Testa nu med `' OR '1'='1` som lösenord igen.

### Vad händer?

```
❌ Fel lösenord. Försök igen.
```

`@username` och `@password` skickas som *data* — aldrig som SQL-kod. Databasen läser `' OR '1'='1` som en textsträng och jämför den mot lösenordet. Ingen match. Ingen inloggning.

---

## Reflektionsfrågor

- 💡 Varför loggade du in som Kelce och inte som admin — trots att du angav `admin` som username?
- 💡 Vad hade hänt om du i stället injicerat i username-fältet: `admin'--`?
- 💡 Räcker det att "filtrera bort citattecken" som säkerhetsåtgärd? Varför inte?
- 💡 Hur hanteras lösenord i riktiga system? (Tips: läs om hashing och `bcrypt`)

