---

title: Träningsuppgifter: Training: ADO.NET och C# till Databas
author: Marcus Ackre Medina
type: exam
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exam/adonet_training.md"
description: "Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt."
tags: ["ado.net", "adonet", "csharp", "databas", "git", "ssh", "syntax", "till", "training", "training:"]
week_fit: []
---

# Träningsuppgifter: Training: ADO.NET och C# till Databas

🟢


## Instruktioner

Välj det bästa svaret för varje fråga. Varje fråga har flera alternativ där ett är korrekt.
Klicka på 'Visa svar' för att se det rätta svaret och förklaringar för alla alternativ.

### Fråga 1

Vad är ADO.NET?

a. Manuella databasverktyget - du gör allt själv!<br>
b. Microsofts low-level API för databasåtkomst i .NET<br>
c. En ORM som Entity Framework<br>
d. Ett databassystem<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Microsofts low-level API för databasåtkomst i .NET


  **Förklaringar:**

  - ❌ **a) Manuella databasverktyget - du gör allt själv!** - FEL: Du får göra allt manuellt, men också full kontroll!
  - ✅ **b) Microsofts low-level API för databasåtkomst i .NET** - **RÄTT**: ADO.NET ger direkt kontroll över SQL
  - ❌ **c) En ORM som Entity Framework** - FEL: ADO.NET är lägre nivå än ORM
  - ❌ **d) Ett databassystem** - FEL: Det är ett API, inte en databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 2

Vad är Connection String?

```csharp
string connectionString = "Data Source=school.db;Version=3;";
```

a. Databasens adress och inställningar - som GPS-koordinater till data<br>
b. Namnet på databasen<br>
c. Instruktioner för hur man ansluter till databasen<br>
d. Lösenordet till databasen<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Instruktioner för hur man ansluter till databasen


  **Förklaringar:**

  - ❌ **a) Databasens adress och inställningar - som GPS-koordinater till data** - FEL: Perfekt analogi - visar vägen till databasen!
  - ❌ **b) Namnet på databasen** - FEL: Det är en del av connection string
  - ✅ **c) Instruktioner för hur man ansluter till databasen** - **RÄTT**: Innehåller sökväg, version, pooling etc.
  - ❌ **d) Lösenordet till databasen** - FEL: Connection string kan innehålla lösenord, men är mer än så
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 3

Varför ska man ALLTID använda using statement med databas-objekt?

```csharp
using var connection = new SQLiteConnection(connectionString);
```

a. using är städaren - stänger dörren när du är klar<br>
b. För att koden ser proffsig ut<br>
c. För att automatiskt stänga och frigöra resurser<br>
d. Det är obligatoriskt i C#<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** För att automatiskt stänga och frigöra resurser


  **Förklaringar:**

  - ❌ **a) using är städaren - stänger dörren när du är klar** - FEL: using städar upp efter dig automatiskt!
  - ❌ **b) För att koden ser proffsig ut** - FEL: Ser bra ut OCH fungerar bra!
  - ✅ **c) För att automatiskt stänga och frigöra resurser** - **RÄTT**: using garanterar Dispose() anropas
  - ❌ **d) Det är obligatoriskt i C#** - FEL: Inte obligatoriskt, men starkt rekommenderat
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 4

Vad gör ExecuteNonQuery()?

```csharp
command.CommandText = "INSERT INTO Students (FirstName) VALUES ('Anna')";
int rows = command.ExecuteNonQuery();
```

a. Returnerar ett värde<br>
b. Returnerar resultatdata<br>
c. Kör INSERT/UPDATE/DELETE och säger hur många rader som ändrades<br>
d. Returnerar antal påverkade rader<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar antal påverkade rader


  **Förklaringar:**

  - ❌ **a) Returnerar ett värde** - FEL: Det är ExecuteScalar()
  - ❌ **b) Returnerar resultatdata** - FEL: Det är ExecuteReader()
  - ❌ **c) Kör INSERT/UPDATE/DELETE och säger hur många rader som ändrades** - FEL: NonQuery = ingen data tillbaka, bara antal!
  - ✅ **d) Returnerar antal påverkade rader** - **RÄTT**: ExecuteNonQuery för INSERT/UPDATE/DELETE
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 5

Vad gör ExecuteScalar()?

```csharp
command.CommandText = "SELECT COUNT(*) FROM Students";
object result = command.ExecuteScalar();
```

a. Returnerar första kolumnen i första raden<br>
b. Kör en query utan resultat<br>
c. Returnerar alla rader<br>
d. Hämtar ett enda värde - perfekt för COUNT, MAX, MIN<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar första kolumnen i första raden


  **Förklaringar:**

  - ✅ **a) Returnerar första kolumnen i första raden** - **RÄTT**: ExecuteScalar för enkla värden
  - ❌ **b) Kör en query utan resultat** - FEL: Det är ExecuteNonQuery()
  - ❌ **c) Returnerar alla rader** - FEL: Det är ExecuteReader()
  - ❌ **d) Hämtar ett enda värde - perfekt för COUNT, MAX, MIN** - FEL: Perfekt för aggregatfunktioner!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 6

Vad gör ExecuteReader()?

```csharp
using var reader = command.ExecuteReader();
while (reader.Read())
{
    // Läs data
}
```

a. Returnerar ett enda värde<br>
b. Läser en rad<br>
c. Som en bok - läs rad för rad med Read()<br>
d. Returnerar flera rader som kan itereras<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Returnerar flera rader som kan itereras


  **Förklaringar:**

  - ❌ **a) Returnerar ett enda värde** - FEL: Det är ExecuteScalar()
  - ❌ **b) Läser en rad** - FEL: Kan läsa många rader med while-loop
  - ❌ **c) Som en bok - läs rad för rad med Read()** - FEL: reader.Read() bläddrar framåt!
  - ✅ **d) Returnerar flera rader som kan itereras** - **RÄTT**: ExecuteReader för SELECT-queries
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 7

Varför ska man ALLTID använda parametrar istället för strängkonkatenering?

```csharp
// FEL
string sql = $"SELECT * FROM Users WHERE Email = '{email}'";

// RÄTT
string sql = "SELECT * FROM Users WHERE Email = @email";
command.Parameters.AddWithValue("@email", email);
```

a. Bobby Tables kan inte droppa tabeller med parametrar!<br>
b. Det är snabbare<br>
c. Skydd mot SQL Injection-attacker<br>
d. Det ser snyggare ut<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skydd mot SQL Injection-attacker


  **Förklaringar:**

  - ❌ **a) Bobby Tables kan inte droppa tabeller med parametrar!** - FEL: Parametrar = SQL Injection-försäkring!
  - ❌ **b) Det är snabbare** - FEL: Säkerhet är huvudskälet, inte prestanda
  - ✅ **c) Skydd mot SQL Injection-attacker** - **RÄTT**: Parametrar escapar farliga tecken automatiskt
  - ❌ **d) Det ser snyggare ut** - FEL: Ser bra ut OCH är säkert
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 8

Hur läser du data från en DataReader med index?

```csharp
while (reader.Read())
{
    int id = reader.GetInt32(___);
    string name = reader.GetString(___);
}
```

a. 1 och 2<br>
b. id" och "name<br>
c. 0 och 1 - kolumner är noll-indexerade!<br>
d. 0 och 1<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 0 och 1


  **Förklaringar:**

  - ❌ **a) 1 och 2** - FEL: Kolumner börjar på 0, inte 1
  - ❌ **b) id" och "name** - FEL: Det är kolumnnamn, inte index
  - ❌ **c) 0 och 1 - kolumner är noll-indexerade!** - FEL: Precis som arrays!
  - ✅ **d) 0 och 1** - **RÄTT**: DataReader är noll-indexerad
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 9

Hur kontrollerar du om ett värde är NULL i DataReader?

```csharp
while (reader.Read())
{
    string? email = _____ ? null : reader.GetString(2);
}
```

a. reader.IsNull(2)<br>
b. reader.IsDBNull(2)<br>
c. reader.GetString(2) == null<br>
d. IsDBNull(index) - kolla före GetString() annars kraschar det!<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** reader.IsDBNull(2)


  **Förklaringar:**

  - ❌ **a) reader.IsNull(2)** - FEL: Metoden heter IsDBNull, inte IsNull
  - ✅ **b) reader.IsDBNull(2)** - **RÄTT**: IsDBNull kontrollerar NULL före läsning
  - ❌ **c) reader.GetString(2) == null** - FEL: GetString() kastar exception vid NULL
  - ❌ **d) IsDBNull(index) - kolla före GetString() annars kraschar det!** - FEL: NULL-kontroll är obligatorisk vid nullable-kolumner!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 10

Vad gör en Transaction?

```csharp
using var transaction = connection.BeginTransaction();
try {
    // Operationer
    transaction.Commit();
} catch {
    transaction.Rollback();
}
```

a. Gör queryn snabbare<br>
b. Allt eller inget - både INSERT och UPDATE måste lyckas, annars ångras båda<br>
c. Krypterar datan<br>
d. Säkerställer att flera operationer lyckas tillsammans eller ångras alla<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Säkerställer att flera operationer lyckas tillsammans eller ångras alla


  **Förklaringar:**

  - ❌ **a) Gör queryn snabbare** - FEL: Handlar om dataintegritet, inte prestanda
  - ❌ **b) Allt eller inget - både INSERT och UPDATE måste lyckas, annars ångras båda** - FEL: Perfekt för banköverföringar: ta från A OCH lägg till i B!
  - ❌ **c) Krypterar datan** - FEL: Transaction handlar inte om säkerhet
  - ✅ **d) Säkerställer att flera operationer lyckas tillsammans eller ångras alla** - **RÄTT**: Transaction = ACID-garanti
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 11

Hur hämtar du det senaste auto-increment ID efter INSERT?

```csharp
command.CommandText = "INSERT INTO Students (Name) VALUES (@name)";
command.ExecuteNonQuery();

// Hämta ID?
```

a. SELECT MAX(Id) FROM Students<br>
b. command.LastInsertedId<br>
c. SELECT last_insert_rowid()<br>
d. last_insert_rowid() - SQLites magiska ID-hämtare<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** SELECT last_insert_rowid()


  **Förklaringar:**

  - ❌ **a) SELECT MAX(Id) FROM Students** - FEL: Farligt vid concurrent inserts
  - ❌ **b) command.LastInsertedId** - FEL: Ingen sådan property finns
  - ✅ **c) SELECT last_insert_rowid()** - **RÄTT**: SQLites säkra sätt att få senaste ID
  - ❌ **d) last_insert_rowid() - SQLites magiska ID-hämtare** - FEL: Thread-safe och pålitlig!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 12

Vad är Repository Pattern?

```csharp
public interface IStudentRepository
{
    Student? GetById(int id);
    List<Student> GetAll();
    int Create(Student student);
}
```

a. Ett mönster för att separera databaslogik från business logic<br>
b. En ORM<br>
c. Databaslogiken får eget rum - separation of concerns!<br>
d. Ett databasprogram<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett mönster för att separera databaslogik från business logic


  **Förklaringar:**

  - ✅ **a) Ett mönster för att separera databaslogik från business logic** - **RÄTT**: Repository = databasabstraktionslager
  - ❌ **b) En ORM** - FEL: Repository kan användas med ADO.NET eller ORM
  - ❌ **c) Databaslogiken får eget rum - separation of concerns!** - FEL: Håller koden organiserad och testbar!
  - ❌ **d) Ett databasprogram** - FEL: Det är ett designmönster
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 13

Vad är skillnaden mellan AddWithValue och Add för parametrar?

```csharp
// Metod 1
command.Parameters.AddWithValue("@name", "Anna");

// Metod 2
var param = new SQLiteParameter("@name", DbType.String);
param.Value = "Anna";
command.Parameters.Add(param);
```

a. AddWithValue = snabbversion, Add = explicit typkontroll<br>
b. Ingen skillnad<br>
c. Add är snabbare<br>
d. AddWithValue är enklare, Add ger mer kontroll över datatyp<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** AddWithValue är enklare, Add ger mer kontroll över datatyp


  **Förklaringar:**

  - ❌ **a) AddWithValue = snabbversion, Add = explicit typkontroll** - FEL: Använd AddWithValue för enkelhet, Add för precision!
  - ❌ **b) Ingen skillnad** - FEL: Add ger explicit typkontroll
  - ❌ **c) Add är snabbare** - FEL: Minimal prestandaskillnad
  - ✅ **d) AddWithValue är enklare, Add ger mer kontroll över datatyp** - **RÄTT**: AddWithValue gissar typ, Add specificerar
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 14

Hur hanterar du NULL-värden vid INSERT med parametrar?

```csharp
string? middleName = null;
command.Parameters.AddWithValue("@middleName", _____);
```

a. middleName ?? (object)DBNull.Value<br>
b. middleName ?? DBNull.Value - konvertera C# null till DB NULL<br>
c. middleName<br>
d. null<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** middleName ?? (object)DBNull.Value


  **Förklaringar:**

  - ✅ **a) middleName ?? (object)DBNull.Value** - **RÄTT**: Konvertera C# null till DBNull.Value
  - ❌ **b) middleName ?? DBNull.Value - konvertera C# null till DB NULL** - FEL: Två olika NULL-typer måste mappas!
  - ❌ **c) middleName** - FEL: C# null blir inte automatiskt DB NULL
  - ❌ **d) null** - FEL: Måste vara DBNull.Value för databas
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 15

Vad är async/await för i databasoperationer?

```csharp
public async Task<List<Student>> GetAllStudentsAsync()
{
    await connection.OpenAsync();
    using var reader = await command.ExecuteReaderAsync();
    // ...
}
```

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

a. Förhindrar att UI fryser under databasoperationer<br>
b. Gör queryn snabbare<br>
c. Det är obligatoriskt för databaser<br>
d. Async = UI fortsätter fungera medan databasen jobbar<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Förhindrar att UI fryser under databasoperationer


  **Förklaringar:**

  - ✅ **a) Förhindrar att UI fryser under databasoperationer** - **RÄTT**: async låter UI-tråden fortsätta medan DB arbetar
  - ❌ **b) Gör queryn snabbare** - FEL: Förbättrar responsiveness, inte SQL-prestanda
  - ❌ **c) Det är obligatoriskt för databaser** - FEL: Valfritt men rekommenderat för UI-appar
  - ❌ **d) Async = UI fortsätter fungera medan databasen jobbar** - FEL: Användaren kan klicka medan data laddas!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 16

Vad är Connection Pooling?

```csharp
string connectionString = "Data Source=db.db;Pooling=true;Max Pool Size=100;";
```

a. Pooling = återvinning av connections - öppna inte nya hela tiden!<br>
b. Återanvänder databasanslutningar istället för att skapa nya<br>
c. En backup-mekanism<br>
d. Krypterar connections<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Återanvänder databasanslutningar istället för att skapa nya


  **Förklaringar:**

  - ❌ **a) Pooling = återvinning av connections - öppna inte nya hela tiden!** - FEL: Öppna/stäng är dyrt, pooling återanvänder!
  - ✅ **b) Återanvänder databasanslutningar istället för att skapa nya** - **RÄTT**: Pooling sparar tid genom att återanvända
  - ❌ **c) En backup-mekanism** - FEL: Handlar om prestandaoptimering
  - ❌ **d) Krypterar connections** - FEL: Pooling handlar inte om säkerhet
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 17

Hur mappar du en DataReader-rad till ett objekt?

```csharp
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; }
}

while (reader.Read())
{
    var student = new Student
    {
        StudentId = _____,
        FirstName = _____
    };
}
```

a. reader.GetInt32(0) och reader.GetString(1)<br>
b. reader[0] och reader[1]<br>
c. reader.StudentId och reader.FirstName<br>
d. GetInt32(0) och GetString(1) - typade läsningar<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** reader.GetInt32(0) och reader.GetString(1)


  **Förklaringar:**

  - ✅ **a) reader.GetInt32(0) och reader.GetString(1)** - **RÄTT**: Typade getters för säker läsning
  - ❌ **b) reader[0] och reader[1]** - FEL: Returnerar object, kräver casting
  - ❌ **c) reader.StudentId och reader.FirstName** - FEL: DataReader har inga sådana properties
  - ❌ **d) GetInt32(0) och GetString(1) - typade läsningar** - FEL: GetInt32/GetString är säkrare än casting!
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 18

Vad returnerar ExecuteNonQuery() vid DELETE om ingen rad matchas?

```csharp
command.CommandText = "DELETE FROM Students WHERE StudentId = 9999";
int rows = command.ExecuteNonQuery();
```

a. 0 rader påverkade - inget fel, bara inget hände<br>
b. -1<br>
c. 0<br>
d. Kastar exception<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 0


  **Förklaringar:**

  - ❌ **a) 0 rader påverkade - inget fel, bara inget hände** - FEL: Använd returvärdet för att kolla om något hände!
  - ❌ **b) -1** - FEL: Negativa värden returneras inte
  - ✅ **c) 0** - **RÄTT**: ExecuteNonQuery returnerar antal påverkade rader
  - ❌ **d) Kastar exception** - FEL: Ingen rad matchad är inte ett fel
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 19

Hur hanterar du SQLiteException för constraint violations?

```csharp
try
{
    // Database operation
}
catch (SQLiteException ex)
{
    if (ex.ErrorCode == ___)
    {
        Console.WriteLine("Duplicate email!");
    }
}
```

a. 19 = CONSTRAINT violation - som duplicate email eller foreign key<br>
b. 19<br>
c. 500<br>
d. 404<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** 19


  **Förklaringar:**

  - ❌ **a) 19 = CONSTRAINT violation - som duplicate email eller foreign key** - FEL: Kolla ErrorCode för specifik felhantering!
  - ✅ **b) 19** - **RÄTT**: ErrorCode 19 = CONSTRAINT-fel i SQLite
  - ❌ **c) 500** - FEL: HTTP-felkod för server error
  - ❌ **d) 404** - FEL: Det är HTTP-felkod, inte SQLite
</details>

<div style="text-align: center; margin: 2em 0;"><img src="separator_cool.png" alt="Separator" style="max-width: 400px; height: auto;"></div>

### Fråga 20

När ska du använda ADO.NET istället för Entity Framework?

a. För alla småprojekt<br>
b. Aldrig, EF är alltid bättre<br>
c. ADO.NET när du vill optimera varje query - EF när du vill koda snabbt<br>
d. När du behöver maximal prestanda och full SQL-kontroll<br>

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** När du behöver maximal prestanda och full SQL-kontroll


  **Förklaringar:**

  - ❌ **a) För alla småprojekt** - FEL: EF är ofta bättre även för små projekt
  - ❌ **b) Aldrig, EF är alltid bättre** - FEL: ADO.NET har sina användningsområden
  - ❌ **c) ADO.NET när du vill optimera varje query - EF när du vill koda snabbt** - FEL: Rätt verktyg för rätt jobb!
  - ✅ **d) När du behöver maximal prestanda och full SQL-kontroll** - **RÄTT**: ADO.NET för optimering, EF för produktivitet
</details>


## Sammanfattning

Du har nu genomgått 20 träningsfrågor om training: ado.net och c# till databas.
Dessa frågor täcker viktiga koncept som du behöver känna till för att lyckas i kursen.


**Tips för fortsatt lärande:**

- Gå igenom frågorna igen om du hade svårt med några

- Testa att skriva egen kod för att förstärka koncepten

- Diskutera svåra frågor med klasskamrater eller lärare
