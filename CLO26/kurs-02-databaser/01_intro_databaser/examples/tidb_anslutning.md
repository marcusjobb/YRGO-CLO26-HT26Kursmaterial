# Anslut till TiDB Cloud

Minsta möjliga program som ansluter till TiDB Cloud och bekräftar att det fungerar.

---

## Installera paketet

```bash
dotnet add package MySql.Data
```

---

## Anslut och verifiera

```csharp
using var connection = new MySqlConnection(connectionString);
connection.Open();
Console.WriteLine($"✅ Ansluten! Serverversion: {connection.ServerVersion}");
```

Om du ser serverversionen är anslutningen uppe.

---

## Skapa en tabell och lägg in data

```csharp
var cmd = connection.CreateCommand();
cmd.CommandText = @"
    CREATE TABLE IF NOT EXISTS hello (
        id   INT AUTO_INCREMENT PRIMARY KEY,
        text VARCHAR(100)
    );
    INSERT INTO hello (text) VALUES ('Hej från C#!');
";
cmd.ExecuteNonQuery();
Console.WriteLine("Tabell skapad och rad inlagd.");
```

---

## Läs tillbaka datan

```csharp
cmd.CommandText = "SELECT * FROM hello";
using var reader = cmd.ExecuteReader();
while (reader.Read())
    Console.WriteLine($"  {reader.GetInt32("id")}: {reader.GetString("text")}");
```

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
    Console.WriteLine($"✅ Ansluten! Serverversion: {connection.ServerVersion}");

    var cmd = connection.CreateCommand();
    cmd.CommandText = @"
        CREATE TABLE IF NOT EXISTS hello (
            id   INT AUTO_INCREMENT PRIMARY KEY,
            text VARCHAR(100)
        );
        INSERT INTO hello (text) VALUES ('Hej från C#!');
    ";
    cmd.ExecuteNonQuery();
    Console.WriteLine("Tabell skapad och rad inlagd.");

    cmd.CommandText = "SELECT * FROM hello";
    using var reader = cmd.ExecuteReader();
    while (reader.Read())
        Console.WriteLine($"  {reader.GetInt32("id")}: {reader.GetString("text")}");
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
