# Nordbanken — säkert inloggningssystem™

Ett helt säkert inloggningssystem för Nordbanken. Inget kan gå fel.

---

## Inloggningsformuläret

```csharp
Console.WriteLine("╔═══════════════════════════════╗");
Console.WriteLine("║    NORDBANKEN SÄKER INLOGG™   ║");
Console.WriteLine("║    Skyddad sedan 1987          ║");
Console.WriteLine("╚═══════════════════════════════╝");
Console.WriteLine();
Console.Write("Användarnamn: ");
string username = Console.ReadLine()!;
Console.Write("Lösenord: ");
string password = Console.ReadLine()!;
```

Studerande skriver in ett namn och ett lösenord. Verkar helt rimligt.

---

## Kontrollen mot databasen

```csharp
string query = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";
```

Vi stoppar in det användaren skrivit direkt i SQL-frågan. Smidigt.

```csharp
var cmd = connection.CreateCommand();
cmd.CommandText = query;

using var reader = cmd.ExecuteReader();

if (reader.HasRows)
{
    Console.WriteLine();
    Console.WriteLine("✅ Inloggning lyckades! Välkommen!");
    Console.WriteLine($"   Kontonummer: 4716-XXXX-XXXX-1337");
    Console.WriteLine($"   Saldo: 2 847 391 kr");
}
else
{
    Console.WriteLine();
    Console.WriteLine("❌ Fel användarnamn eller lösenord.");
}
```

Om raden finns i databasen → inloggad. Annars → nekad. Enkelt.

---

## Testdata

```sql
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) NOT NULL,
    password VARCHAR(50) NOT NULL
);

INSERT INTO users (username, password) VALUES
('anna', 'hemligt123'),
('erik', 'lösenord456');
```

Testkonton om ni vill prova med rätt uppgifter.

---

## Hela koden

```csharp
using MySql.Data.MySqlClient;

string connectionString =
    "Server=gateway01.eu-central-1.prod.aws.tidbcloud.com;" +
    "Port=4000;" +
    "Database=test;" +
    "Uid=<användarnamn>;" +
    "Pwd=<lösenord>;" +
    "SslMode=Required;";

try
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();

    // Skapa tabellen om den inte finns
    var setup = connection.CreateCommand();
    setup.CommandText = @"
        CREATE TABLE IF NOT EXISTS users (
            id INT AUTO_INCREMENT PRIMARY KEY,
            username VARCHAR(50) NOT NULL,
            password VARCHAR(50) NOT NULL
        );
        INSERT IGNORE INTO users (username, password) VALUES
        ('anna', 'hemligt123'),
        ('erik', 'lösenord456');
    ";
    setup.ExecuteNonQuery();

    // Inloggning
    Console.WriteLine("╔═══════════════════════════════╗");
    Console.WriteLine("║    NORDBANKEN SÄKER INLOGG™   ║");
    Console.WriteLine("║    Skyddad sedan 1987          ║");
    Console.WriteLine("╚═══════════════════════════════╝");
    Console.WriteLine();
    Console.Write("Användarnamn: ");
    string username = Console.ReadLine()!;
    Console.Write("Lösenord: ");
    string password = Console.ReadLine()!;

    // ⚠️ Sårbar query — stoppa aldrig in användarinput direkt i SQL
    string query = $"SELECT * FROM users WHERE username = '{username}' AND password = '{password}'";

    var cmd = connection.CreateCommand();
    cmd.CommandText = query;

    using var reader = cmd.ExecuteReader();

    if (reader.HasRows)
    {
        Console.WriteLine();
        Console.WriteLine("✅ Inloggning lyckades! Välkommen!");
        Console.WriteLine($"   Kontonummer: 4716-XXXX-XXXX-1337");
        Console.WriteLine($"   Saldo: 2 847 391 kr");
    }
    else
    {
        Console.WriteLine();
        Console.WriteLine("❌ Fel användarnamn eller lösenord.");
    }
}
catch (MySqlException ex)
{
    Console.WriteLine($"Databasfel: {ex.Message}");
}
catch (Exception ex)
{
    Console.WriteLine($"Fel: {ex.Message}");
}
```
