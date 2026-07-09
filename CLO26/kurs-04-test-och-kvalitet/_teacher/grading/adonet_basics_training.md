# Träningsuppgifter: Training: ADO.NET Basics - SqlConnection, SqlCommand & DataReader

🟢


## Instruktioner

Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt. 
Klicka på 'Visa svar' för att se det rätta svaret och förklaringar för alla alternativ.

### Fråga 1

Vad är ADO.NET?

a. Ett databassystem<br>
b. Microsofts low-level API för databasåtkomst i .NET<br>
c. En ORM som Entity Framework<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Microsofts low-level API för databasåtkomst i .NET


  **Förklaringar:**

  - ❌ **a) Ett databassystem** - FEL: ADO.NET är ett API, inte själva databasen
  - ✅ **b) Microsofts low-level API för databasåtkomst i .NET** - **RÄTT**: ADO.NET ger direkt kontroll över SQL och databasoperationer
  - ❌ **c) En ORM som Entity Framework** - FEL: ADO.NET är lägre nivå än ORM - du skriver SQL själv
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är en Connection String?

a. Lösenordet till databasen<br>
b. En sträng med instruktioner för hur man ansluter till databasen<br>
c. Namnet på tabellen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En sträng med instruktioner för hur man ansluter till databasen


  **Förklaringar:**

  - ❌ **a) Lösenordet till databasen** - FEL: Lösenord kan ingå men connection string är mer än så
  - ✅ **b) En sträng med instruktioner för hur man ansluter till databasen** - **RÄTT**: Innehåller server, databas, autentisering etc.
  - ❌ **c) Namnet på tabellen** - FEL: Connection string pekar på databas, inte tabell
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Vad är SqlConnection?

```csharp
using var connection = new SqlConnection(connectionString);
connection.Open();
```

a. En tabell i databasen<br>
b. En SQL-query<br>
c. Representerar en anslutning till SQL Server-databasen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Representerar en anslutning till SQL Server-databasen


  **Förklaringar:**

  - ❌ **a) En tabell i databasen** - FEL: SqlConnection är anslutningen till hela databasen
  - ❌ **b) En SQL-query** - FEL: SqlConnection är anslutningen, inte queryn
  - ✅ **c) Representerar en anslutning till SQL Server-databasen** - **RÄTT**: SqlConnection hanterar själva databasanslutningen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Varför ska man ALLTID använda using med SqlConnection?

```csharp
using var connection = new SqlConnection(connectionString);
// Koden här
// connection.Dispose() anropas automatiskt
```

a. Gör koden snabbare<br>
b. Det är obligatoriskt i C#<br>
c. För att automatiskt stänga och frigöra databasanslutningen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** För att automatiskt stänga och frigöra databasanslutningen


  **Förklaringar:**

  - ❌ **a) Gör koden snabbare** - FEL: Handlar om resurshantering, inte hastighet
  - ❌ **b) Det är obligatoriskt i C#** - FEL: Inte obligatoriskt men starkt rekommenderat
  - ✅ **c) För att automatiskt stänga och frigöra databasanslutningen** - **RÄTT**: using garanterar att Dispose() anropas även vid exception
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad är SqlCommand?

```csharp
var command = new SqlCommand("SELECT * FROM Heroes", connection);
```

a. Resultatet från en query<br>
b. Representerar en SQL-sats som ska exekveras<br>
c. En databasanslutning<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Representerar en SQL-sats som ska exekveras


  **Förklaringar:**

  - ❌ **a) Resultatet från en query** - FEL: SqlCommand kör queryn, resultatet kommer från reader/scalar
  - ✅ **b) Representerar en SQL-sats som ska exekveras** - **RÄTT**: SqlCommand innehåller SQL och exekverar den mot databasen
  - ❌ **c) En databasanslutning** - FEL: Det är SqlConnection som är anslutningen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Hur sätter du SQL-texten i ett SqlCommand?

```csharp
var command = new SqlCommand();
command._____ = "SELECT * FROM Heroes";
```

a. SQL<br>
b. Query<br>
c. CommandText<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** CommandText


  **Förklaringar:**

  - ❌ **a) SQL** - FEL: Det heter CommandText, inte SQL
  - ❌ **b) Query** - FEL: Det heter CommandText, inte Query
  - ✅ **c) CommandText** - **RÄTT**: CommandText property innehåller SQL-strängen
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Vad gör ExecuteNonQuery()?

```csharp
command.CommandText = "INSERT INTO Heroes (Name) VALUES ('Spider-Man')";
int rows = command.ExecuteNonQuery();
```

a. Kör INSERT/UPDATE/DELETE och returnerar antal påverkade rader<br>
b. Returnerar ett enskilt värde<br>
c. Returnerar data från databasen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Kör INSERT/UPDATE/DELETE och returnerar antal påverkade rader


  **Förklaringar:**

  - ✅ **a) Kör INSERT/UPDATE/DELETE och returnerar antal påverkade rader** - **RÄTT**: ExecuteNonQuery för queries som INTE returnerar data
  - ❌ **b) Returnerar ett enskilt värde** - FEL: Det är ExecuteScalar() som returnerar ett värde
  - ❌ **c) Returnerar data från databasen** - FEL: Det är ExecuteReader() som returnerar data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Vad gör ExecuteScalar()?

```csharp
command.CommandText = "SELECT COUNT(*) FROM Heroes";
object result = command.ExecuteScalar();
int count = Convert.ToInt32(result);
```

a. Returnerar alla rader<br>
b. Kör INSERT/UPDATE/DELETE<br>
c. Returnerar första kolumnen i första raden<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar första kolumnen i första raden


  **Förklaringar:**

  - ❌ **a) Returnerar alla rader** - FEL: Det är ExecuteReader() som returnerar många rader
  - ❌ **b) Kör INSERT/UPDATE/DELETE** - FEL: Det är ExecuteNonQuery() för modifierande queries
  - ✅ **c) Returnerar första kolumnen i första raden** - **RÄTT**: ExecuteScalar för att hämta ett enskilt värde
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Vad gör ExecuteReader()?

```csharp
using var reader = command.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader["HeroName"]);
}
```

a. Returnerar ett enskilt värde<br>
b. Returnerar ett SqlDataReader-objekt för att läsa flera rader<br>
c. Kör UPDATE-satser<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar ett SqlDataReader-objekt för att läsa flera rader


  **Förklaringar:**

  - ❌ **a) Returnerar ett enskilt värde** - FEL: Det är ExecuteScalar() för enskilt värde
  - ✅ **b) Returnerar ett SqlDataReader-objekt för att läsa flera rader** - **RÄTT**: ExecuteReader för SELECT-queries som returnerar många rader
  - ❌ **c) Kör UPDATE-satser** - FEL: Det är ExecuteNonQuery() för modifierande queries
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad returnerar reader.Read()?

```csharp
while (reader.Read())
{
    // Läs data här
}
```

a. true om det finns fler rader, false när slut<br>
b. Data från raden<br>
c. Antal kolumner<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** true om det finns fler rader, false när slut


  **Förklaringar:**

  - ✅ **a) true om det finns fler rader, false när slut** - **RÄTT**: Read() flyttar till nästa rad OCH returnerar bool
  - ❌ **b) Data från raden** - FEL: Read() returnerar bool, inte data
  - ❌ **c) Antal kolumner** - FEL: Read() returnerar bool för om det finns mer data
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Hur läser du data från SqlDataReader med index?

```csharp
while (reader.Read())
{
    int id = reader.GetInt32(___);
    string name = reader.GetString(___);
}
```
Om SELECT är: SELECT Id, Name FROM Heroes

a. 0 och 1<br>
b. Id" och "Name<br>
c. 1 och 2<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 0 och 1


  **Förklaringar:**

  - ✅ **a) 0 och 1** - **RÄTT**: DataReader är noll-indexerad - första kolumnen är 0
  - ❌ **b) Id" och "Name** - FEL: Det är kolumnnamn, inte index
  - ❌ **c) 1 och 2** - FEL: Index börjar på 0, inte 1
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Hur läser du data med kolumnnamn istället för index?

```csharp
while (reader.Read())
{
    string name = reader[_____].ToString();
}
```

a. Name<br>
b. 0<br>
c. Name<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Name


  **Förklaringar:**

  - ✅ **a) Name** - **RÄTT**: reader["kolumnnamn"] returnerar object som måste castas
  - ❌ **b) 0** - FEL: Det är index, inte kolumnnamn
  - ❌ **c) Name** - FEL: Kolumnnamn måste vara i quotes
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 13

Vad är skillnaden mellan reader[0] och reader.GetInt32(0)?

```csharp
object value1 = reader[0];           // Returnerar object
int value2 = reader.GetInt32(0);     // Returnerar int
```

a. GetInt32 returnerar typat värde, [0] returnerar object<br>
b. Ingen skillnad<br>
c. GetInt32 är snabbare<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** GetInt32 returnerar typat värde, [0] returnerar object


  **Förklaringar:**

  - ✅ **a) GetInt32 returnerar typat värde, [0] returnerar object** - **RÄTT**: GetInt32/GetString är type-safe, indexer returnerar object
  - ❌ **b) Ingen skillnad** - FEL: Stor skillnad - en är typad, en är object
  - ❌ **c) GetInt32 är snabbare** - FEL: Prestanda är liknande, fördelen är type safety
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 14

Hur kontrollerar du om ett värde är NULL i DataReader?

```csharp
while (reader.Read())
{
    string? email = reader._____(2) ? null : reader.GetString(2);
}
```

a. IsNull<br>
b. IsDBNull<br>
c. == null<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** IsDBNull


  **Förklaringar:**

  - ❌ **a) IsNull** - FEL: Metoden heter IsDBNull, inte IsNull
  - ✅ **b) IsDBNull** - **RÄTT**: IsDBNull(index) kontrollerar NULL före läsning
  - ❌ **c) == null** - FEL: GetString kastar exception vid NULL, måste kolla IsDBNull först
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 15

Varför ska man ALLTID använda parametrar istället för string concatenation?

```csharp
// FARLIGT
var sql = $"SELECT * FROM Users WHERE Email = '{email}'";

// SÄKERT
var sql = "SELECT * FROM Users WHERE Email = @email";
command.Parameters.AddWithValue("@email", email);
```

a. Det är obligatoriskt<br>
b. Skydd mot SQL Injection-attacker<br>
c. Det är snabbare<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skydd mot SQL Injection-attacker


  **Förklaringar:**

  - ❌ **a) Det är obligatoriskt** - FEL: Inte obligatoriskt men absolut nödvändigt för säkerhet
  - ✅ **b) Skydd mot SQL Injection-attacker** - **RÄTT**: Parametrar escapar automatiskt och behandlar input som data
  - ❌ **c) Det är snabbare** - FEL: Säkerhet är huvudskälet, inte prestanda
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 16

Hur lägger du till en parameter i SqlCommand?

```csharp
command.CommandText = "SELECT * FROM Heroes WHERE Name = @name";
command.Parameters._____("@name", "Spider-Man");
```

a. Insert<br>
b. AddWithValue<br>
c. Add<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** AddWithValue


  **Förklaringar:**

  - ❌ **a) Insert** - FEL: Metoden heter AddWithValue
  - ✅ **b) AddWithValue** - **RÄTT**: AddWithValue är enklaste sättet att lägga till parameter
  - ❌ **c) Add** - FEL: Add finns men AddWithValue är enklare för basic parameters
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 17

Vad är en SqlTransaction?

```csharp
using var transaction = connection.BeginTransaction();
try {
    // Flera operationer här
    transaction.Commit();
} catch {
    transaction.Rollback();
}
```

a. En typ av SQL-query<br>
b. Säkerställer att flera operationer lyckas tillsammans eller ångras alla<br>
c. Gör querys snabbare<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Säkerställer att flera operationer lyckas tillsammans eller ångras alla


  **Förklaringar:**

  - ❌ **a) En typ av SQL-query** - FEL: Transaction är ett koncept för att gruppera queries
  - ✅ **b) Säkerställer att flera operationer lyckas tillsammans eller ångras alla** - **RÄTT**: Transaction = ACID-garanti för flera SQL-satser
  - ❌ **c) Gör querys snabbare** - FEL: Handlar om dataintegritet, inte prestanda
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 18

Hur hanterar du NULL-värden när du INSERT:ar med parametrar?

```csharp
string? middleName = null;
command.Parameters.AddWithValue("@middleName", _____);
```

a. middleName ?? (object)DBNull.Value<br>
b. null<br>
c. middleName<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** middleName ?? (object)DBNull.Value


  **Förklaringar:**

  - ✅ **a) middleName ?? (object)DBNull.Value** - **RÄTT**: C# null måste konverteras till DBNull.Value för databas
  - ❌ **b) null** - FEL: Måste vara DBNull.Value för databas-NULL
  - ❌ **c) middleName** - FEL: C# null blir inte automatiskt DB NULL
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 19

Vad är connection pooling?

```csharp
var connStr = "Server=localhost;Database=db;Pooling=true;Max Pool Size=100;";
```

a. Återanvänder databasanslutningar istället för att skapa nya<br>
b. Krypterar anslutningar<br>
c. En backup-mekanism<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Återanvänder databasanslutningar istället för att skapa nya


  **Förklaringar:**

  - ✅ **a) Återanvänder databasanslutningar istället för att skapa nya** - **RÄTT**: Pooling sparar tid genom att återanvända connections
  - ❌ **b) Krypterar anslutningar** - FEL: Pooling handlar om återanvändning, inte säkerhet
  - ❌ **c) En backup-mekanism** - FEL: Pooling är prestandaoptimering, inte backup
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 20

När ska du använda async/await med ADO.NET?

```csharp
await connection.OpenAsync();
using var reader = await command.ExecuteReaderAsync();
```

a. Det är obligatoriskt för databaser<br>
b. Gör SQL-queries snabbare<br>
c. För att förhindra att UI fryser under databasoperationer<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** För att förhindra att UI fryser under databasoperationer


  **Förklaringar:**

  - ❌ **a) Det är obligatoriskt för databaser** - FEL: Async är valfritt men rekommenderat för UI-appar
  - ❌ **b) Gör SQL-queries snabbare** - FEL: Förbättrar responsiveness, inte själva query-hastigheten
  - ✅ **c) För att förhindra att UI fryser under databasoperationer** - **RÄTT**: Async låter UI-tråden fortsätta medan DB arbetar
</details>


## Sammanfattning

Du har nu genomgått 20 träningsfrågor om training: ado.net basics - sqlconnection, sqlcommand & datareader. 
Dessa frågor täcker viktiga koncept som du behöver känna till för att lyckas i kursen.


**Tips för fortsatt lärande:**

- Gå igenom frågorna igen om du hade svårt med några

- Testa att skriva egen kod för att förstärka koncepten

- Diskutera svåra frågor med klasskamrater eller lärare
