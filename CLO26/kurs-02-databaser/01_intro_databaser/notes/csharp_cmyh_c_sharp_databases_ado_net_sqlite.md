---

title: ADO.NET och SQLite i C#
author: Marcus Ackre Medina
type: article
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/databases/ado_net_sqlite.md"
description: "ADO.NET är Microsofts databasåtkomstteknologi för .NET-applikationer. När du använder ADO.NET med SQLite får du direktkontroll över databasanslutningar, kommandon och datamappning - det 'manuella arbe"
tags: ["ado", "ado.net", "csharp", "databaser", "design-patterns", "entity-framework", "git", "net", "oop", "sql"]
week_fit: []
---

# ADO.NET och SQLite i C#

🟢


ADO.NET är Microsofts databasåtkomstteknologi för .NET-applikationer. När du använder ADO.NET med SQLite får du direktkontroll över databasanslutningar, kommandon och datamappning - det "manuella arbetet" som senare förenklas av ORM-verktyg som Entity Framework.

## TL;DR

- **ADO.NET** = low-level databasåtkomst i .NET
- **System.Data.SQLite** = SQLite-provider för ADO.NET
- **Connection** → **Command** → **Reader/NonQuery** = grundflödet
- **Använd parametrar** mot SQL injection
- **using statements** för automatisk resurshantering
- **Manuell mappning** från DataReader till objekt

## Innehållsförteckning

- [Vad är ADO.NET?](#vad-är-adonet)
- [Installation och setup](#installation-och-setup)
- [Grundläggande koncept](#grundläggande-koncept)
- [Connection management](#connection-management)
- [Exekvera SQL-kommandon](#exekvera-sql-kommandon)
- [Läsa data med DataReader](#läsa-data-med-datareader)
- [CRUD-operationer](#crud-operationer)
- [Parameteriserade queries](#parameteriserade-queries)
- [Transactions](#transactions)
- [Error handling](#error-handling)
- [Best practices](#best-practices)
- [Jämförelse med Entity Framework](#jämförelse-med-entity-framework)

## Vad är ADO.NET?

### Definition

ADO.NET (ActiveX Data Objects .NET) är Microsofts dataåtkomstteknologi som ger lågnivåkontroll över databasoperationer.

**Fördelar:**

- Full kontroll över SQL
- Bättre prestanda (mindre overhead)
- Lättare att optimera queries
- Bra för läsning av stora datamängder

**Nackdelar:**

- Mer boilerplate-kod
- Manuell objektmappning
- Svårare att underhålla
- Lätt att göra misstag

### ADO.NET-komponenter

```
┌─────────────────┐
│  Connection     │ ← Anslutning till databas
└────────┬────────┘
         │
┌────────▼────────┐
│  Command        │ ← SQL-kommando att köra
└────────┬────────┘
         │
    ┌────▼────┐
    │ Reader  │ ← Läsa resultat (SELECT)
    └─────────┘
    ┌─────────┐
    │ NonQuery│ ← Inget resultat (INSERT/UPDATE/DELETE)
    └─────────┘
```

## Installation och setup

### NuGet-paket

```xml
<!-- I .csproj -->
<ItemGroup>
  <PackageReference Include="System.Data.SQLite" Version="1.0.118" />
</ItemGroup>
```

Eller via Package Manager Console:

```bash
dotnet add package System.Data.SQLite
```

### Projekt-struktur

```
MyApp/
├── MyApp.csproj
├── Program.cs
├── Models/
│   └── Student.cs
├── Data/
│   ├── DatabaseService.cs
│   └── StudentRepository.cs
└── Database/
    └── school.db
```

### Grundläggande using statements

```csharp
using System.Data.SQLite;
using System.Data;
```

## Grundläggande koncept

### Connection String

Connection string definierar hur man ansluter till databasen:

```csharp
// Relativ sökväg
string connectionString = "Data Source=school.db;Version=3;";

// Absolut sökväg
string connectionString = @"Data Source=C:\MyApp\Database\school.db;Version=3;";

// Med extra inställningar
string connectionString = @"
    Data Source=school.db;
    Version=3;
    Pooling=true;
    Max Pool Size=100;
    Foreign Keys=True;";
```

### SQLiteConnection

```csharp
// Skapa anslutning
using var connection = new SQLiteConnection(connectionString);

// Öppna anslutning
connection.Open();

// Kontrollera status
if (connection.State == ConnectionState.Open)
{
    Console.WriteLine("Ansluten till databas!");
}

// Anslutningen stängs automatiskt när using-blocket avslutas
```

### SQLiteCommand

```csharp
using var connection = new SQLiteConnection(connectionString);
connection.Open();

// Skapa kommando
using var command = new SQLiteCommand(connection);
command.CommandText = "SELECT COUNT(*) FROM Students";

// Exekvera kommando
object result = command.ExecuteScalar();
Console.WriteLine($"Antal studenter: {result}");
```

## Connection management

### Manuell hantering (UNDVIK)

```csharp
// ❌ Dåligt - kan läcka connections
SQLiteConnection connection = new SQLiteConnection(connectionString);
connection.Open();
// ... gör något
connection.Close(); // Glöms lätt!
```

### Using statement (REKOMMENDERAT)

```csharp
// ✅ Bra - stänger automatiskt
using (var connection = new SQLiteConnection(connectionString))
{
    connection.Open();
    // ... gör något
} // Stängs automatiskt här
```

### Using declaration (C# 8+)

```csharp
// ✅ Ännu bättre - kortare syntax
using var connection = new SQLiteConnection(connectionString);
connection.Open();
// ... gör något
// Stängs automatiskt i slutet av metoden
```

### Connection pooling

```csharp
// Connection pooling är aktiverat by default
string connectionString = @"
    Data Source=school.db;
    Version=3;
    Pooling=true;              // Default: true
    Max Pool Size=100;         // Default: 100
    Min Pool Size=0;           // Default: 0
    Connection Timeout=30;";   // Sekunder att vänta
```

## Exekvera SQL-kommandon

### ExecuteNonQuery - INSERT, UPDATE, DELETE

```csharp
using var connection = new SQLiteConnection(connectionString);
connection.Open();

// INSERT
string insertSql = @"
    INSERT INTO Students (FirstName, LastName, Email)
    VALUES ('Anna', 'Andersson', 'anna@example.com')";

using var command = new SQLiteCommand(insertSql, connection);
int rowsAffected = command.ExecuteNonQuery();
Console.WriteLine($"{rowsAffected} rad(er) påverkade");
```

### ExecuteScalar - Enkelt värde

```csharp
using var connection = new SQLiteConnection(connectionString);
connection.Open();

// Räkna studenter
string countSql = "SELECT COUNT(*) FROM Students";
using var command = new SQLiteCommand(countSql, connection);
long count = (long)command.ExecuteScalar();
Console.WriteLine($"Antal studenter: {count}");

// Hämta senaste ID
string lastIdSql = "SELECT last_insert_rowid()";
using var idCommand = new SQLiteCommand(lastIdSql, connection);
long lastId = (long)idCommand.ExecuteScalar();
```

### ExecuteReader - Flera rader

```csharp
using var connection = new SQLiteConnection(connectionString);
connection.Open();

string selectSql = "SELECT FirstName, LastName, Email FROM Students";
using var command = new SQLiteCommand(selectSql, connection);
using var reader = command.ExecuteReader();

while (reader.Read())
{
    string firstName = reader.GetString(0);  // Index
    string lastName = reader.GetString(1);
    string email = reader.GetString(2);

    Console.WriteLine($"{firstName} {lastName} - {email}");
}
```

## Läsa data med DataReader

### Grundläggande läsning

```csharp
using var connection = new SQLiteConnection(connectionString);
connection.Open();

string sql = "SELECT StudentId, FirstName, LastName, Email FROM Students";
using var command = new SQLiteCommand(sql, connection);
using var reader = command.ExecuteReader();

while (reader.Read())
{
    // Läs med index (0-baserat)
    int id = reader.GetInt32(0);
    string firstName = reader.GetString(1);

    // Eller med kolumnnamn (långsammare)
    int id2 = reader.GetInt32(reader.GetOrdinal("StudentId"));
    string firstName2 = reader.GetString(reader.GetOrdinal("FirstName"));
}
```

### Hantera NULL-värden

```csharp
while (reader.Read())
{
    int id = reader.GetInt32(0);
    string firstName = reader.GetString(1);

    // Kontrollera NULL
    string? middleName = reader.IsDBNull(2)
        ? null
        : reader.GetString(2);

    // Eller med GetValue + cast
    string? lastName = reader.GetValue(3) as string;
}
```

### Mappa till objekt

```csharp
// Model
public class Student
{
    public int StudentId { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; }
}

// Mappning
public List<Student> GetAllStudents()
{
    var students = new List<Student>();

    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    string sql = "SELECT StudentId, FirstName, LastName, Email, CreatedDate FROM Students";
    using var command = new SQLiteCommand(sql, connection);
    using var reader = command.ExecuteReader();

    while (reader.Read())
    {
        students.Add(new Student
        {
            StudentId = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Email = reader.GetString(3),
            CreatedDate = DateTime.Parse(reader.GetString(4))
        });
    }

    return students;
}
```

## CRUD-operationer

### Create - INSERT

```csharp
public int CreateStudent(string firstName, string lastName, string email)
{
    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    // Sätt in data
    string insertSql = @"
        INSERT INTO Students (FirstName, LastName, Email)
        VALUES (@firstName, @lastName, @email)";

    using var command = new SQLiteCommand(insertSql, connection);
    command.Parameters.AddWithValue("@firstName", firstName);
    command.Parameters.AddWithValue("@lastName", lastName);
    command.Parameters.AddWithValue("@email", email);

    command.ExecuteNonQuery();

    // Hämta det nya ID:t
    command.CommandText = "SELECT last_insert_rowid()";
    long newId = (long)command.ExecuteScalar();

    return (int)newId;
}
```

### Read - SELECT

```csharp
public Student? GetStudentById(int studentId)
{
    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    string sql = @"
        SELECT StudentId, FirstName, LastName, Email, CreatedDate
        FROM Students
        WHERE StudentId = @id";

    using var command = new SQLiteCommand(sql, connection);
    command.Parameters.AddWithValue("@id", studentId);

    using var reader = command.ExecuteReader();

    if (reader.Read())
    {
        return new Student
        {
            StudentId = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Email = reader.GetString(3),
            CreatedDate = DateTime.Parse(reader.GetString(4))
        };
    }

    return null;
}
```

### Update - UPDATE

```csharp
public bool UpdateStudentEmail(int studentId, string newEmail)
{
    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    string sql = @"
        UPDATE Students
        SET Email = @email
        WHERE StudentId = @id";

    using var command = new SQLiteCommand(sql, connection);
    command.Parameters.AddWithValue("@email", newEmail);
    command.Parameters.AddWithValue("@id", studentId);

    int rowsAffected = command.ExecuteNonQuery();
    return rowsAffected > 0;
}
```

### Delete - DELETE

```csharp
public bool DeleteStudent(int studentId)
{
    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    string sql = "DELETE FROM Students WHERE StudentId = @id";

    using var command = new SQLiteCommand(sql, connection);
    command.Parameters.AddWithValue("@id", studentId);

    int rowsAffected = command.ExecuteNonQuery();
    return rowsAffected > 0;
}
```

## Parameteriserade queries

### Varför parametrar?

**Utan parametrar (FARLIGT!):**

```csharp
// ❌ SQL INJECTION-risk!
string email = userInput;
string sql = $"SELECT * FROM Students WHERE Email = '{email}'";

// Om userInput = "'; DROP TABLE Students;--"
// Körs: SELECT * FROM Students WHERE Email = ''; DROP TABLE Students;--'
```

**Med parametrar (SÄKERT!):**

```csharp
// ✅ Säkert mot SQL injection
string sql = "SELECT * FROM Students WHERE Email = @email";
using var command = new SQLiteCommand(sql, connection);
command.Parameters.AddWithValue("@email", userInput);
// SQL-motorn hanterar escaping automatiskt
```

### Olika sätt att lägga till parametrar

```csharp
using var command = new SQLiteCommand(sql, connection);

// Metod 1: AddWithValue (enklast)
command.Parameters.AddWithValue("@firstName", "Anna");

// Metod 2: Skapa parameter explicit
var param = new SQLiteParameter("@lastName", DbType.String);
param.Value = "Andersson";
command.Parameters.Add(param);

// Metod 3: Parameter collection initializer
command.Parameters.AddRange(new[]
{
    new SQLiteParameter("@firstName", "Anna"),
    new SQLiteParameter("@lastName", "Andersson")
});
```

### Parametrar med olika datatyper

```csharp
string sql = @"
    INSERT INTO Students (FirstName, Age, GPA, IsActive, EnrollmentDate)
    VALUES (@firstName, @age, @gpa, @isActive, @enrollmentDate)";

using var command = new SQLiteCommand(sql, connection);

// String
command.Parameters.AddWithValue("@firstName", "Anna");

// Integer
command.Parameters.AddWithValue("@age", 22);

// Double
command.Parameters.AddWithValue("@gpa", 3.75);

// Boolean (lagras som INTEGER 0/1 i SQLite)
command.Parameters.AddWithValue("@isActive", true);

// DateTime (konverteras till TEXT i SQLite)
command.Parameters.AddWithValue("@enrollmentDate", DateTime.Now);

command.ExecuteNonQuery();
```

### NULL-hantering

```csharp
string? middleName = null;

// NULL-värde
command.Parameters.AddWithValue("@middleName",
    middleName ?? (object)DBNull.Value);

// Eller med conditional
if (middleName != null)
    command.Parameters.AddWithValue("@middleName", middleName);
else
    command.Parameters.AddWithValue("@middleName", DBNull.Value);
```

## Transactions

### Grundläggande transaction

```csharp
using var connection = new SQLiteConnection(_connectionString);
connection.Open();

using var transaction = connection.BeginTransaction();

try
{
    // Operation 1
    string sql1 = "INSERT INTO Students (FirstName, LastName) VALUES ('Anna', 'Svensson')";
    using var cmd1 = new SQLiteCommand(sql1, connection, transaction);
    cmd1.ExecuteNonQuery();

    // Operation 2
    string sql2 = "UPDATE Courses SET EnrolledCount = EnrolledCount + 1 WHERE CourseId = 101";
    using var cmd2 = new SQLiteCommand(sql2, connection, transaction);
    cmd2.ExecuteNonQuery();

    // Allt gick bra - commit
    transaction.Commit();
    Console.WriteLine("Transaction lyckades!");
}
catch (Exception ex)
{
    // Något gick fel - rollback
    transaction.Rollback();
    Console.WriteLine($"Transaction misslyckades: {ex.Message}");
    throw;
}
```

### Transaction med isolation level

```csharp
using var connection = new SQLiteConnection(_connectionString);
connection.Open();

// SQLite stödjer främst Serializable och ReadCommitted
using var transaction = connection.BeginTransaction(IsolationLevel.Serializable);

try
{
    // Dina operationer här
    transaction.Commit();
}
catch
{
    transaction.Rollback();
    throw;
}
```

### Savepoints

```csharp
using var connection = new SQLiteConnection(_connectionString);
connection.Open();

using var transaction = connection.BeginTransaction();

try
{
    // Operation 1
    var cmd1 = new SQLiteCommand("INSERT INTO Students ...", connection, transaction);
    cmd1.ExecuteNonQuery();

    // Skapa savepoint
    var saveCmd = new SQLiteCommand("SAVEPOINT sp1", connection, transaction);
    saveCmd.ExecuteNonQuery();

    try
    {
        // Operation 2 (riskabel)
        var cmd2 = new SQLiteCommand("UPDATE ...", connection, transaction);
        cmd2.ExecuteNonQuery();
    }
    catch
    {
        // Rollback till savepoint (behåller operation 1)
        var rollbackCmd = new SQLiteCommand("ROLLBACK TO sp1", connection, transaction);
        rollbackCmd.ExecuteNonQuery();
    }

    transaction.Commit();
}
catch
{
    transaction.Rollback();
    throw;
}
```

## Error handling

### Try-Catch med SQLite

```csharp
public void SafeDatabaseOperation()
{
    try
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();

        string sql = "INSERT INTO Students (Email) VALUES (@email)";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@email", "duplicate@example.com");

        command.ExecuteNonQuery();
    }
    catch (SQLiteException ex)
    {
        // SQLite-specifika fel
        switch (ex.ErrorCode)
        {
            case 19: // CONSTRAINT violation
                Console.WriteLine("Constraint fel (t.ex. duplicate email)");
                break;
            case 5: // Database locked
                Console.WriteLine("Databasen är låst");
                break;
            default:
                Console.WriteLine($"SQLite fel {ex.ErrorCode}: {ex.Message}");
                break;
        }
    }
    catch (Exception ex)
    {
        // Andra fel
        Console.WriteLine($"Oväntat fel: {ex.Message}");
    }
}
```

### Retry-logik för locked database

```csharp
public void ExecuteWithRetry(string sql, int maxRetries = 3)
{
    int attempts = 0;

    while (attempts < maxRetries)
    {
        try
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();

            using var command = new SQLiteCommand(sql, connection);
            command.ExecuteNonQuery();

            return; // Lyckades!
        }
        catch (SQLiteException ex) when (ex.ErrorCode == 5) // Database locked
        {
            attempts++;
            if (attempts >= maxRetries)
                throw;

            Thread.Sleep(100 * attempts); // Exponential backoff
        }
    }
}
```

## Best practices

### 1. Repository pattern

```csharp
public interface IStudentRepository
{
    Student? GetById(int id);
    List<Student> GetAll();
    int Create(Student student);
    bool Update(Student student);
    bool Delete(int id);
}

public class StudentRepository : IStudentRepository
{
    private readonly string _connectionString;

    public StudentRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public Student? GetById(int id)
    {
        using var connection = new SQLiteConnection(_connectionString);
        connection.Open();

        string sql = "SELECT * FROM Students WHERE StudentId = @id";
        using var command = new SQLiteCommand(sql, connection);
        command.Parameters.AddWithValue("@id", id);

        using var reader = command.ExecuteReader();
        if (reader.Read())
        {
            return MapToStudent(reader);
        }

        return null;
    }

    private Student MapToStudent(SQLiteDataReader reader)
    {
        return new Student
        {
            StudentId = reader.GetInt32(reader.GetOrdinal("StudentId")),
            FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
            LastName = reader.GetString(reader.GetOrdinal("LastName")),
            Email = reader.GetString(reader.GetOrdinal("Email"))
        };
    }
}
```

### 2. Connection factory

```csharp
public interface IDbConnectionFactory
{
    SQLiteConnection CreateConnection();
}

public class SQLiteConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SQLiteConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public SQLiteConnection CreateConnection()
    {
        var connection = new SQLiteConnection(_connectionString);
        connection.Open();

        // Aktivera foreign keys
        using var cmd = new SQLiteCommand("PRAGMA foreign_keys = ON", connection);
        cmd.ExecuteNonQuery();

        return connection;
    }
}
```

### 3. Async operations

```csharp
public async Task<List<Student>> GetAllStudentsAsync()
{
    var students = new List<Student>();

    using var connection = new SQLiteConnection(_connectionString);
    await connection.OpenAsync();

    string sql = "SELECT * FROM Students";
    using var command = new SQLiteCommand(sql, connection);
    using var reader = await command.ExecuteReaderAsync();

    while (await reader.ReadAsync())
    {
        students.Add(MapToStudent(reader));
    }

    return students;
}

public async Task<int> CreateStudentAsync(Student student)
{
    using var connection = new SQLiteConnection(_connectionString);
    await connection.OpenAsync();

    string sql = @"
        INSERT INTO Students (FirstName, LastName, Email)
        VALUES (@firstName, @lastName, @email)";

    using var command = new SQLiteCommand(sql, connection);
    command.Parameters.AddWithValue("@firstName", student.FirstName);
    command.Parameters.AddWithValue("@lastName", student.LastName);
    command.Parameters.AddWithValue("@email", student.Email);

    await command.ExecuteNonQueryAsync();

    command.CommandText = "SELECT last_insert_rowid()";
    var result = await command.ExecuteScalarAsync();
    return Convert.ToInt32(result);
}
```

## Jämförelse med Entity Framework

### ADO.NET (Manuellt)

```csharp
// Mer kod, mer kontroll
public List<Student> GetStudentsByClass(string className)
{
    var students = new List<Student>();

    using var connection = new SQLiteConnection(_connectionString);
    connection.Open();

    string sql = @"
        SELECT s.StudentId, s.FirstName, s.LastName, s.Email
        FROM Students s
        INNER JOIN Enrollments e ON s.StudentId = e.StudentId
        INNER JOIN Classes c ON e.ClassId = c.ClassId
        WHERE c.ClassName = @className";

    using var command = new SQLiteCommand(sql, connection);
    command.Parameters.AddWithValue("@className", className);

    using var reader = command.ExecuteReader();
    while (reader.Read())
    {
        students.Add(new Student
        {
            StudentId = reader.GetInt32(0),
            FirstName = reader.GetString(1),
            LastName = reader.GetString(2),
            Email = reader.GetString(3)
        });
    }

    return students;
}
```

### Entity Framework (ORM)

```csharp
// Mindre kod, mindre kontroll
public List<Student> GetStudentsByClass(string className)
{
    return _context.Students
        .Where(s => s.Enrollments.Any(e => e.Class.ClassName == className))
        .ToList();
}
```

### När använda vilket?

**ADO.NET:**

- Komplexa SQL-queries med optimeringar
- Bulk-operationer med stora datamängder
- Behöver maximal prestanda
- Legacy-kod eller specifika krav

**Entity Framework:**

- Standard CRUD-operationer
- Snabb utveckling
- Mindre projekt
- När maintainability är viktigare än prestanda

## Sammanfattning

ADO.NET ger dig full kontroll över databasinteraktioner men kräver mer kod och noggrannhet:

✅ **Fördelar:**

- Maximal prestanda
- Full SQL-kontroll
- Transparent databasåtkomst
- Inget ORM-overhead

❌ **Nackdelar:**

- Mer boilerplate-kod
- Manuell objektmappning
- Lätt att göra säkerhetsmisstag
- Mindre maintainable

**Nyckelpunkter:**

1. Använd alltid `using` statements
2. Parametrisera ALLA queries
3. Hantera NULL-värden korrekt
4. Använd transactions för kritiska operationer
5. Implementera error handling
6. Överväg Repository pattern för struktur

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
