# ADO.NET — objekt och användning

Snabbreferens för de viktigaste ADO.NET-objekten. Varje objekt gör en sak — lär dig vad den saken är.

---

## ConfigurationBuilder / IConfiguration

Läser in appsettings.json så du kan hämta connection strings och andra inställningar.

```cs
using Microsoft.Extensions.Configuration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

string connectionString = config.GetConnectionString("DefaultConnection")!;
```

> Utropstecknet (`!`) säger till kompilatorn att du vet att värdet inte är null. Se till att nyckeln faktiskt finns i filen.

---

## MySqlConnection

Representerar anslutningen till databasen. Utan den — ingen databas.

```cs
using MySql.Data.MySqlClient;

using var connection = new MySqlConnection(connectionString);
connection.Open();

// Gör saker med databasen här...

// connection.Close() körs automatiskt när using-blocket avslutas
```

> `using var` är viktigt. En databasanslutning är en begränsad resurs — stäng den alltid, även om något går fel.

---

## MySqlCommand

Representerar en SQL-fråga eller ett kommando som ska köras mot databasen.

```cs
string sql = "SELECT * FROM Heroes WHERE Level > @minLevel";
using var command = new MySqlCommand(sql, connection);
```

### MySqlParameter — skydda mot SQL injection

Bygg aldrig SQL-strängar med + och användardata. Använd parametrar.

```cs
// FEL — sårbart för SQL injection
string sql = "SELECT * FROM Heroes WHERE Name = '" + userInput + "'";

// RÄTT — använd parametrar
string sql = "SELECT * FROM Heroes WHERE Name = @name";
using var command = new MySqlCommand(sql, connection);
command.Parameters.AddWithValue("@name", userInput);
```

> En parameter skickas separat från SQL-texten — databasen behandlar den aldrig som kod, bara som data.

---

## ExecuteNonQuery

Kör ett kommando som **inte returnerar rader** — INSERT, UPDATE, DELETE. Returnerar antal påverkade rader.

```cs
string sql = "INSERT INTO Heroes (Name, Level) VALUES (@name, @level)";
using var command = new MySqlCommand(sql, connection);
command.Parameters.AddWithValue("@name", "Aria");
command.Parameters.AddWithValue("@level", 5);

int rowsAffected = command.ExecuteNonQuery();
Console.WriteLine($"{rowsAffected} rad(er) påverkades.");
```

> Om `rowsAffected` är 0 hittades inget att ändra — inte ett fel, men värt att kolla.

---

## ExecuteScalar

Kör en fråga och returnerar **ett enda värde** — första kolumnen på första raden. Perfekt för COUNT, MAX, SUM.

```cs
string sql = "SELECT COUNT(*) FROM Heroes";
using var command = new MySqlCommand(sql, connection);

int count = Convert.ToInt32(command.ExecuteScalar());
Console.WriteLine($"Antal hjältar: {count}");
```

> Resultatet är `object?` — casta det alltid till rätt typ med `Convert.ToInt32`, `ToString()`, eller liknande.

---

## ExecuteReader / MySqlDataReader

Kör en SELECT och låter dig läsa rad för rad. Minsnålt — hämtar en rad i taget, inte allt på en gång.

```cs
string sql = "SELECT Id, Name, Level FROM Heroes";
using var command = new MySqlCommand(sql, connection);
using var reader = command.ExecuteReader();

while (reader.Read())
{
    int id       = reader.GetInt32("Id");
    string name  = reader.GetString("Name");
    int level    = reader.GetInt32("Level");

    Console.WriteLine($"{id}: {name} (nivå {level})");
}
```

> `reader.Read()` returnerar `true` så länge det finns fler rader. När alla är lästa returnerar den `false` och loopen avslutas.

Hämta värde om kolumnen kan vara null:

```cs
string? bio = reader.IsDBNull("Bio") ? null : reader.GetString("Bio");
```

---

## DataTable + MySqlDataAdapter

Laddar ett helt resultset in i minnet som en tabell. Praktiskt när du vill jobba med data utan aktiv databasanslutning.

```cs
string sql = "SELECT * FROM Heroes";
using var adapter = new MySqlDataAdapter(sql, connection);

DataTable table = new DataTable();
adapter.Fill(table);

Console.WriteLine($"{table.Rows.Count} rader laddade.");
```

> `MySqlDataAdapter` öppnar och stänger anslutningen automatiskt under `Fill()`.

---

## DataRow / DataColumn

Ger dig tillgång till enskilda rader och kolumner i en `DataTable`.

```cs
foreach (DataRow row in table.Rows)
{
    string name = row["Name"].ToString()!;
    int level   = Convert.ToInt32(row["Level"]);

    Console.WriteLine($"{name} är nivå {level}");
}
```

Loopa kolumnerna om du vill se schemat:

```cs
foreach (DataColumn col in table.Columns)
{
    Console.WriteLine($"{col.ColumnName} ({col.DataType.Name})");
}
```

> Åtkomst via kolumnnamn (`row["Name"]`) är mer läsbar än index (`row[0]`) och håller bättre när schemat ändras.

---

## Transaction

Samlar flera kommandon i ett block — antingen **lyckas alla** eller **ångras alla**. Kritiskt när operationer beror på varandra.

```cs
using var connection = new MySqlConnection(connectionString);
connection.Open();

using var transaction = connection.BeginTransaction();

try
{
    var cmd1 = new MySqlCommand(
        "UPDATE Accounts SET Balance = Balance - @amount WHERE Id = @from",
        connection, transaction);
    cmd1.Parameters.AddWithValue("@amount", 500);
    cmd1.Parameters.AddWithValue("@from", 1);
    cmd1.ExecuteNonQuery();

    var cmd2 = new MySqlCommand(
        "UPDATE Accounts SET Balance = Balance + @amount WHERE Id = @to",
        connection, transaction);
    cmd2.Parameters.AddWithValue("@amount", 500);
    cmd2.Parameters.AddWithValue("@to", 2);
    cmd2.ExecuteNonQuery();

    transaction.Commit();
    Console.WriteLine("Transaktion genomförd.");
}
catch (Exception ex)
{
    transaction.Rollback();
    Console.WriteLine($"Något gick fel — allt ångrades: {ex.Message}");
}
```

> Utan transaktion kan ett halvt misslyckat block lämna databasen i ett inkonsekvent tillstånd. Använd alltid transaktioner när operationer hänger ihop.

---

Entity Framework Core och LINQ har egna quicksheets.
