---

title: Övning: Dagboksapplikation med Clean Code & SRP 📔
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/ado/5_diary_app_clean_code.md"
description: "Vi ska bygga en dagboksapplikation där du skriver dina dagliga anteckningar. Men istället för att bara 'få det att fungera', ska vi göra det **rätt** från början!"
tags: ["app", "clean", "clean-code", "csharp", "dagboksapplikation", "diary", "exercise", "installation", "ssh", "visual-studio"]
week_fit: []
---

# Övning: Dagboksapplikation med Clean Code & SRP 📔

🟢


## Din uppgift

Vi ska bygga en dagboksapplikation där du skriver dina dagliga anteckningar. Men istället för att bara "få det att fungera", ska vi göra det **rätt** från början!

Du kommer lära dig:
- **SRP (Single Responsibility Principle)** - En metod gör EN sak
- **Clean Code** - Läsbar, begriplig kod
- **Metodisk uppbyggnad** - Steg för steg i pedagogisk ordning

Vi bygger funktionerna i denna ordning:
1. **Setup** → Skapa databas och tabell
2. **Create** → Lägg till dagboksinlägg (med automatiskt datum!)
3. **Read All** → Lista alla inlägg
4. **Search** → Sök bland inlägg
5. **Update** → Redigera ett inlägg
6. **Delete** → Ta bort ett inlägg

## Teknikförklaring

### SRP - Single Responsibility Principle

Varje metod ska ha **EN ansvarsområde**:

**DÅLIGT** ❌ (gör för mycket):
```csharp
static void DoEverything()
{
    // Öppna databas
    // Ta input från användaren
    // Validera input
    // Spara i databas
    // Visa resultat
    // Logga fel
    // Stäng databas
}
```

**BRA** ✅ (varje metod gör EN sak):
```csharp
static void CreateEntry() { /* Hanterar skapande */ }
static string GetUserInput() { /* Hämtar input */ }
static bool ValidateInput(string input) { /* Validerar */ }
static void SaveToDatabase(Entry entry) { /* Sparar */ }
static void ShowMessage(string msg) { /* Visar meddelande */ }
```

### Clean Code Principer

1. **Beskrivande namn** - `GetUserInput()` inte `Get()`
2. **Små metoder** - Max 20-30 rader
3. **En abstraktionsnivå** - Inte blanda låg och hög nivå
4. **Kommentarer när det behövs** - Förklara VARFÖR, inte VAD

### Automatiskt datum med DateTime

```csharp
DateTime now = DateTime.Now;
string datum = now.ToString("yyyy-MM-dd");        // 2025-10-03
string tid = now.ToString("HH:mm:ss");            // 14:35:22
string fullformat = now.ToString("yyyy-MM-dd HH:mm");  // 2025-10-03 14:35
```

---

## Komplett kod-skelett

Här är hela grundstrukturen. Fyll i metoderna en i taget!

```csharp
using System;
using System.IO;
using System.Data.SQLite;
using System.Collections.Generic;

// Klass för att representera ett dagboksinlägg
class DiaryEntry
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string CreatedAt { get; set; }
    public string UpdatedAt { get; set; }
}

class DiaryApp
{
    private static string connectionString = "";

    static void Main()
    {
        SetupDatabase();
        CreateDiaryTable();
        RunMainMenu();
    }

    // ==================== SETUP ====================

    static void SetupDatabase()
    {
        // TODO: Implementera
        // Variabler du behöver:
        string documentsPath;
        string dbFolder;
        string dbPath;

        // Din kod här...
    }

    static void CreateDiaryTable()
    {
        // TODO: Implementera
        // Variabler du behöver:
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;

        // Din kod här...
    }

    // ==================== MAIN MENU ====================

    static void RunMainMenu()
    {
        // TODO: Implementera
        // Variabler du behöver:
        bool running;
        string choice;

        // Din kod här...
    }

    // ==================== CREATE ====================

    static void CreateNewEntry()
    {
        // TODO: Implementera
        // Variabler du behöver:
        string title;
        string content;
        string timestamp;

        // Din kod här...
    }

    static string GetInputWithPrompt(string prompt)
    {
        // TODO: Implementera
        // Variabler du behöver:
        string input;

        // Din kod här...
    }

    static string GetCurrentTimestamp()
    {
        // TODO: Implementera
        // Variabler du behöver:
        DateTime now;
        string timestamp;

        // Din kod här...
    }

    static void SaveEntry(string title, string content, string timestamp)
    {
        // TODO: Implementera
        // Variabler du behöver:
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;

        // Din kod här...
    }

    // ==================== READ ====================

    static void ShowAllEntries()
    {
        // TODO: Implementera
        // Variabler du behöver:
        List<DiaryEntry> entries;

        // Din kod här...
    }

    static List<DiaryEntry> GetAllEntries()
    {
        // TODO: Implementera
        // Variabler du behöver:
        List<DiaryEntry> entries;
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;
        SQLiteDataReader reader;

        // Din kod här...
    }

    static DiaryEntry CreateEntryFromReader(SQLiteDataReader reader)
    {
        // TODO: Implementera
        // Variabler du behöver:
        DiaryEntry entry;

        // Din kod här...
    }

    static void DisplayEntry(DiaryEntry entry)
    {
        // TODO: Implementera
        // Använd Console.WriteLine för att visa inlägget snyggt

        // Din kod här...
    }

    // ==================== SEARCH ====================

    static void SearchEntries()
    {
        // TODO: Implementera
        // Variabler du behöver:
        string searchTerm;
        List<DiaryEntry> results;

        // Din kod här...
    }

    static List<DiaryEntry> SearchInDatabase(string searchTerm)
    {
        // TODO: Implementera
        // Variabler du behöver:
        List<DiaryEntry> results;
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;
        SQLiteDataReader reader;

        // Din kod här...
    }

    // ==================== UPDATE ====================

    static void UpdateEntry()
    {
        // TODO: Implementera
        // Variabler du behöver:
        string idInput;
        int id;
        DiaryEntry entry;
        string newTitle;
        string newContent;

        // Din kod här...
    }

    static DiaryEntry GetEntryById(int id)
    {
        // TODO: Implementera
        // Variabler du behöver:
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;
        SQLiteDataReader reader;
        DiaryEntry entry;

        // Din kod här...
    }

    static void UpdateInDatabase(int id, string newTitle, string newContent)
    {
        // TODO: Implementera
        // Variabler du behöver:
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;
        string timestamp;

        // Din kod här...
    }

    // ==================== DELETE ====================

    static void DeleteEntry()
    {
        // TODO: Implementera
        // Variabler du behöver:
        string idInput;
        int id;
        DiaryEntry entry;
        string confirm;

        // Din kod här...
    }

    static void DeleteFromDatabase(int id)
    {
        // TODO: Implementera
        // Variabler du behöver:
        SQLiteConnection connection;
        string sql;
        SQLiteCommand command;

        // Din kod här...
    }
}
```

---

## Steg 1: Setup - Skapa databas och tabell

### Metod 1: SetupDatabase()

**Ansvar:** Skapa databas-filen och sätta connection string

```csharp
static void SetupDatabase()
{
    string documentsPath;
    string dbFolder;
    string dbPath;

    // TODO:
    // 1. Hämta Dokument-mappen med Environment.GetFolderPath
    // 2. Skapa sökväg till "databases" mappen med Path.Combine
    // 3. Skapa mappen med Directory.CreateDirectory
    // 4. Skapa sökväg till "my_diary.db" filen
    // 5. Skapa filen om den inte finns med SQLiteConnection.CreateFile
    // 6. Sätt connectionString = "Data Source=" + dbPath
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void SetupDatabase()
{
    string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    string dbFolder = Path.Combine(documentsPath, "databases");
    Directory.CreateDirectory(dbFolder);

    string dbPath = Path.Combine(dbFolder, "my_diary.db");

    if (!File.Exists(dbPath))
    {
        SQLiteConnection.CreateFile(dbPath);
        Console.WriteLine($"📁 Databas skapad: {dbPath}");
    }

    connectionString = $"Data Source={dbPath}";
}
```

</details>

---

### Metod 2: CreateDiaryTable()

**Ansvar:** Skapa diary_entries tabell om den inte finns

```csharp
static void CreateDiaryTable()
{
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;

    // TODO:
    // 1. Skapa connection med connectionString (använd using!)
    // 2. Öppna connection
    // 3. Skapa SQL för CREATE TABLE IF NOT EXISTS diary_entries med kolumner:
    //    - id (INTEGER PRIMARY KEY AUTOINCREMENT)
    //    - title (TEXT NOT NULL)
    //    - content (TEXT NOT NULL)
    //    - created_at (TEXT NOT NULL)
    //    - updated_at (TEXT)
    // 4. Skapa command och kör ExecuteNonQuery
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void CreateDiaryTable()
{
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = @"
            CREATE TABLE IF NOT EXISTS diary_entries (
                id INTEGER PRIMARY KEY AUTOINCREMENT,
                title TEXT NOT NULL,
                content TEXT NOT NULL,
                created_at TEXT NOT NULL,
                updated_at TEXT
            )";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("✅ Dagbokstabell redo!");
        }
    }
}
```

</details>

---

## Steg 2: Create - Lägg till inlägg

### Metod 3: RunMainMenu()

**Ansvar:** Visa meny och hantera användarval

```csharp
static void RunMainMenu()
{
    bool running;
    string choice;

    // TODO:
    // 1. Sätt running = true
    // 2. Loopa medan running är true
    // 3. Skriv ut meny med Console.WriteLine
    // 4. Läs val med Console.ReadLine
    // 5. Använd switch för att anropa rätt metod
    // 6. Case "6": sätt running = false
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void RunMainMenu()
{
    bool running = true;

    while (running)
    {
        Console.WriteLine("\n╔══════════════════════════╗");
        Console.WriteLine("║    MIN DAGBOK 📔         ║");
        Console.WriteLine("╚══════════════════════════╝");
        Console.WriteLine("1. Skriv nytt inlägg");
        Console.WriteLine("2. Visa alla inlägg");
        Console.WriteLine("3. Sök inlägg");
        Console.WriteLine("4. Redigera inlägg");
        Console.WriteLine("5. Ta bort inlägg");
        Console.WriteLine("6. Avsluta");
        Console.Write("\nVälj: ");

        string choice = Console.ReadLine();

        switch (choice)
        {
            case "1":
                CreateNewEntry();
                break;
            case "2":
                ShowAllEntries();
                break;
            case "3":
                SearchEntries();
                break;
            case "4":
                UpdateEntry();
                break;
            case "5":
                DeleteEntry();
                break;
            case "6":
                running = false;
                Console.WriteLine("\n👋 Hej då! Dina minnen är sparade.");
                break;
            default:
                Console.WriteLine("❌ Ogiltigt val!");
                break;
        }
    }
}
```

</details>

---

### Metod 4: GetInputWithPrompt()

**Ansvar:** Visa en prompt och hämta input från användare

```csharp
static string GetInputWithPrompt(string prompt)
{
    string input;

    // TODO:
    // 1. Skriv ut prompt med Console.Write (inte WriteLine!)
    // 2. Läs input med Console.ReadLine
    // 3. Returnera input
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static string GetInputWithPrompt(string prompt)
{
    Console.Write(prompt);
    return Console.ReadLine();
}
```

</details>

---

### Metod 5: GetCurrentTimestamp()

**Ansvar:** Skapa en formaterad timestamp

```csharp
static string GetCurrentTimestamp()
{
    DateTime now;
    string timestamp;

    // TODO:
    // 1. Hämta DateTime.Now
    // 2. Formatera till "yyyy-MM-dd HH:mm" med ToString
    // 3. Returnera timestamp
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static string GetCurrentTimestamp()
{
    return DateTime.Now.ToString("yyyy-MM-dd HH:mm");
}
```

</details>

---

### Metod 6: SaveEntry()

**Ansvar:** Spara ett inlägg i databasen

```csharp
static void SaveEntry(string title, string content, string timestamp)
{
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;

    // TODO:
    // 1. Skapa connection med using
    // 2. Öppna connection
    // 3. Skapa SQL: INSERT INTO diary_entries (title, content, created_at) VALUES (@title, @content, @timestamp)
    // 4. Skapa command med using
    // 5. Lägg till parametrar med AddWithValue
    // 6. Kör ExecuteNonQuery
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void SaveEntry(string title, string content, string timestamp)
{
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = @"INSERT INTO diary_entries (title, content, created_at)
                      VALUES (@title, @content, @timestamp)";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@title", title);
            command.Parameters.AddWithValue("@content", content);
            command.Parameters.AddWithValue("@timestamp", timestamp);

            command.ExecuteNonQuery();
        }
    }
}
```

</details>

---

### Metod 7: CreateNewEntry()

**Ansvar:** Koordinera skapande av nytt inlägg

```csharp
static void CreateNewEntry()
{
    string title;
    string content;
    string timestamp;

    // TODO:
    // 1. Skriv ut "📝 NYTT DAGBOKSINLÄGG"
    // 2. Hämta title med GetInputWithPrompt("Rubrik: ")
    // 3. Hämta content med GetInputWithPrompt("Innehåll: ")
    // 4. Hämta timestamp med GetCurrentTimestamp()
    // 5. Spara med SaveEntry(title, content, timestamp)
    // 6. Skriv ut bekräftelse
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void CreateNewEntry()
{
    Console.WriteLine("\n📝 NYTT DAGBOKSINLÄGG");

    string title = GetInputWithPrompt("Rubrik: ");
    string content = GetInputWithPrompt("Innehåll: ");
    string timestamp = GetCurrentTimestamp();

    SaveEntry(title, content, timestamp);

    Console.WriteLine($"\n✅ Inlägg sparat! ({timestamp})");
}
```

</details>

---

## Steg 3: Read All - Visa alla inlägg

### Metod 8: CreateEntryFromReader()

**Ansvar:** Skapa ett DiaryEntry-objekt från en DataReader

```csharp
static DiaryEntry CreateEntryFromReader(SQLiteDataReader reader)
{
    DiaryEntry entry;

    // TODO:
    // 1. Skapa ett nytt DiaryEntry-objekt
    // 2. Sätt Id = reader.GetInt32(0)
    // 3. Sätt Title = reader.GetString(1)
    // 4. Sätt Content = reader.GetString(2)
    // 5. Sätt CreatedAt = reader.GetString(3)
    // 6. Sätt UpdatedAt = reader.IsDBNull(4) ? null : reader.GetString(4)
    // 7. Returnera entry
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static DiaryEntry CreateEntryFromReader(SQLiteDataReader reader)
{
    return new DiaryEntry
    {
        Id = reader.GetInt32(0),
        Title = reader.GetString(1),
        Content = reader.GetString(2),
        CreatedAt = reader.GetString(3),
        UpdatedAt = reader.IsDBNull(4) ? null : reader.GetString(4)
    };
}
```

</details>

---

### Metod 9: GetAllEntries()

**Ansvar:** Hämta alla inlägg från databasen

```csharp
static List<DiaryEntry> GetAllEntries()
{
    List<DiaryEntry> entries;
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;
    SQLiteDataReader reader;

    // TODO:
    // 1. Skapa en ny List<DiaryEntry>
    // 2. Skapa connection med using
    // 3. Öppna connection
    // 4. Skapa SQL: SELECT * FROM diary_entries ORDER BY created_at DESC
    // 5. Skapa command med using
    // 6. Skapa reader med ExecuteReader() (använd using!)
    // 7. Loopa med while (reader.Read())
    // 8. För varje rad: entries.Add(CreateEntryFromReader(reader))
    // 9. Returnera entries
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static List<DiaryEntry> GetAllEntries()
{
    List<DiaryEntry> entries = new List<DiaryEntry>();

    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = "SELECT * FROM diary_entries ORDER BY created_at DESC";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    entries.Add(CreateEntryFromReader(reader));
                }
            }
        }
    }

    return entries;
}
```

</details>

---

### Metod 10: DisplayEntry()

**Ansvar:** Visa ett enskilt inlägg på ett snyggt sätt

```csharp
static void DisplayEntry(DiaryEntry entry)
{
    // TODO:
    // 1. Skriv ut en snygg ram med Console.WriteLine
    // 2. Visa Id, datum, titel, innehåll
    // 3. Om UpdatedAt inte är null, visa det också
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void DisplayEntry(DiaryEntry entry)
{
    Console.WriteLine($"┌─ ID: {entry.Id} ─────────────────────────");
    Console.WriteLine($"│ 📅 {entry.CreatedAt}");
    Console.WriteLine($"│ 📝 {entry.Title}");
    Console.WriteLine($"│");
    Console.WriteLine($"│ {entry.Content}");

    if (entry.UpdatedAt != null)
    {
        Console.WriteLine($"│ ✏️  Redigerad: {entry.UpdatedAt}");
    }

    Console.WriteLine("└────────────────────────────────────────\n");
}
```

</details>

---

### Metod 11: ShowAllEntries()

**Ansvar:** Visa alla dagboksinlägg

```csharp
static void ShowAllEntries()
{
    List<DiaryEntry> entries;

    // TODO:
    // 1. Skriv ut "📚 ALLA DAGBOKSINLÄGG"
    // 2. Hämta alla inlägg med GetAllEntries()
    // 3. Om entries.Count == 0, skriv "(Din dagbok är tom)" och return
    // 4. Loopa genom entries med foreach
    // 5. För varje entry, anropa DisplayEntry(entry)
    // 6. Skriv ut totalt antal
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void ShowAllEntries()
{
    Console.WriteLine("\n📚 ALLA DAGBOKSINLÄGG\n");

    List<DiaryEntry> entries = GetAllEntries();

    if (entries.Count == 0)
    {
        Console.WriteLine("(Din dagbok är tom)");
        return;
    }

    foreach (var entry in entries)
    {
        DisplayEntry(entry);
    }

    Console.WriteLine($"\nTotalt: {entries.Count} inlägg");
}
```

</details>

---

## Steg 4: Search - Sök inlägg

### Metod 12: SearchInDatabase()

**Ansvar:** Söka i databasen efter titel eller innehåll

```csharp
static List<DiaryEntry> SearchInDatabase(string searchTerm)
{
    List<DiaryEntry> results;
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;
    SQLiteDataReader reader;

    // TODO:
    // 1. Skapa en ny List<DiaryEntry> för results
    // 2. Skapa connection med using
    // 3. Öppna connection
    // 4. Skapa SQL med LIKE: WHERE title LIKE @search OR content LIKE @search
    // 5. Skapa command med using
    // 6. Lägg till parameter @search med värde: "%" + searchTerm + "%"
    // 7. Skapa reader med ExecuteReader() (using!)
    // 8. Loopa och lägg till i results
    // 9. Returnera results
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static List<DiaryEntry> SearchInDatabase(string searchTerm)
{
    List<DiaryEntry> results = new List<DiaryEntry>();

    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = @"SELECT * FROM diary_entries
                      WHERE title LIKE @search OR content LIKE @search
                      ORDER BY created_at DESC";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@search", $"%{searchTerm}%");

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    results.Add(CreateEntryFromReader(reader));
                }
            }
        }
    }

    return results;
}
```

</details>

---

### Metod 13: SearchEntries()

**Ansvar:** Koordinera sökning

```csharp
static void SearchEntries()
{
    string searchTerm;
    List<DiaryEntry> results;

    // TODO:
    // 1. Skriv ut "🔍 SÖK I DAGBOKEN"
    // 2. Hämta searchTerm med GetInputWithPrompt
    // 3. Kolla om searchTerm är tom/null, om ja: visa fel och return
    // 4. Sök med SearchInDatabase(searchTerm)
    // 5. Om results.Count == 0, visa meddelande och return
    // 6. Skriv ut antal träffar
    // 7. Loopa och visa varje result med DisplayEntry
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void SearchEntries()
{
    Console.WriteLine("\n🔍 SÖK I DAGBOKEN");

    string searchTerm = GetInputWithPrompt("Sök efter: ");

    if (string.IsNullOrWhiteSpace(searchTerm))
    {
        Console.WriteLine("❌ Du måste skriva något att söka efter!");
        return;
    }

    List<DiaryEntry> results = SearchInDatabase(searchTerm);

    if (results.Count == 0)
    {
        Console.WriteLine($"\n😔 Inga inlägg hittades för '{searchTerm}'");
        return;
    }

    Console.WriteLine($"\n✅ Hittade {results.Count} inlägg:\n");

    foreach (var entry in results)
    {
        DisplayEntry(entry);
    }
}
```

</details>

---

## Steg 5: Update - Redigera inlägg

### Metod 14: GetEntryById()

**Ansvar:** Hämta ett specifikt inlägg baserat på ID

```csharp
static DiaryEntry GetEntryById(int id)
{
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;
    SQLiteDataReader reader;
    DiaryEntry entry;

    // TODO:
    // 1. Skapa connection med using
    // 2. Öppna connection
    // 3. Skapa SQL: SELECT * FROM diary_entries WHERE id = @id
    // 4. Skapa command med using
    // 5. Lägg till parameter @id
    // 6. Skapa reader med ExecuteReader() (using!)
    // 7. Om reader.Read() returnerar true: returnera CreateEntryFromReader(reader)
    // 8. Annars returnera null
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static DiaryEntry GetEntryById(int id)
{
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = "SELECT * FROM diary_entries WHERE id = @id";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", id);

            using (SQLiteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    return CreateEntryFromReader(reader);
                }
            }
        }
    }

    return null;
}
```

</details>

---

### Metod 15: UpdateInDatabase()

**Ansvar:** Uppdatera ett inlägg i databasen

```csharp
static void UpdateInDatabase(int id, string newTitle, string newContent)
{
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;
    string timestamp;

    // TODO:
    // 1. Skapa connection med using
    // 2. Öppna connection
    // 3. Skapa SQL: UPDATE diary_entries SET title = @title, content = @content, updated_at = @timestamp WHERE id = @id
    // 4. Skapa command med using
    // 5. Lägg till parametrar för title, content, timestamp (GetCurrentTimestamp!), id
    // 6. Kör ExecuteNonQuery
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void UpdateInDatabase(int id, string newTitle, string newContent)
{
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = @"UPDATE diary_entries
                      SET title = @title,
                          content = @content,
                          updated_at = @timestamp
                      WHERE id = @id";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@title", newTitle);
            command.Parameters.AddWithValue("@content", newContent);
            command.Parameters.AddWithValue("@timestamp", GetCurrentTimestamp());
            command.Parameters.AddWithValue("@id", id);

            command.ExecuteNonQuery();
        }
    }
}
```

</details>

---

### Metod 16: UpdateEntry()

**Ansvar:** Koordinera uppdatering av inlägg

```csharp
static void UpdateEntry()
{
    string idInput;
    int id;
    DiaryEntry entry;
    string newTitle;
    string newContent;

    // TODO:
    // 1. Skriv ut "✏️ REDIGERA INLÄGG"
    // 2. Visa alla med ShowAllEntries()
    // 3. Hämta idInput med GetInputWithPrompt
    // 4. Försök parsa till int med TryParse, om misslyckas: visa fel och return
    // 5. Hämta entry med GetEntryById(id)
    // 6. Om entry är null: visa fel och return
    // 7. Visa nuvarande innehåll med DisplayEntry(entry)
    // 8. Hämta newTitle och newContent (tips: visa nuvarande i prompten)
    // 9. Om tom input, använd gamla värdet
    // 10. Uppdatera med UpdateInDatabase(id, newTitle, newContent)
    // 11. Visa bekräftelse
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void UpdateEntry()
{
    Console.WriteLine("\n✏️  REDIGERA INLÄGG");

    ShowAllEntries();

    string idInput = GetInputWithPrompt("\nAnge ID på inlägg att redigera: ");

    if (!int.TryParse(idInput, out int id))
    {
        Console.WriteLine("❌ Ogiltigt ID!");
        return;
    }

    DiaryEntry entry = GetEntryById(id);

    if (entry == null)
    {
        Console.WriteLine($"❌ Inget inlägg med ID {id} hittades!");
        return;
    }

    Console.WriteLine("\n📋 Nuvarande innehåll:");
    DisplayEntry(entry);

    Console.WriteLine("Skriv nytt värde eller tryck ENTER för att behålla:\n");
    string newTitle = GetInputWithPrompt($"Ny rubrik [{entry.Title}]: ");
    string newContent = GetInputWithPrompt($"Nytt innehåll [{entry.Content}]: ");

    if (string.IsNullOrWhiteSpace(newTitle))
        newTitle = entry.Title;

    if (string.IsNullOrWhiteSpace(newContent))
        newContent = entry.Content;

    UpdateInDatabase(id, newTitle, newContent);

    Console.WriteLine("\n✅ Inlägg uppdaterat!");
}
```

</details>

---

## Steg 6: Delete - Ta bort inlägg

### Metod 17: DeleteFromDatabase()

**Ansvar:** Ta bort ett inlägg från databasen

```csharp
static void DeleteFromDatabase(int id)
{
    SQLiteConnection connection;
    string sql;
    SQLiteCommand command;

    // TODO:
    // 1. Skapa connection med using
    // 2. Öppna connection
    // 3. Skapa SQL: DELETE FROM diary_entries WHERE id = @id
    // 4. Skapa command med using
    // 5. Lägg till parameter @id
    // 6. Kör ExecuteNonQuery
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void DeleteFromDatabase(int id)
{
    using (SQLiteConnection connection = new SQLiteConnection(connectionString))
    {
        connection.Open();

        string sql = "DELETE FROM diary_entries WHERE id = @id";

        using (SQLiteCommand command = new SQLiteCommand(sql, connection))
        {
            command.Parameters.AddWithValue("@id", id);
            command.ExecuteNonQuery();
        }
    }
}
```

</details>

---

### Metod 18: DeleteEntry()

**Ansvar:** Koordinera borttagning av inlägg

```csharp
static void DeleteEntry()
{
    string idInput;
    int id;
    DiaryEntry entry;
    string confirm;

    // TODO:
    // 1. Skriv ut "🗑️ TA BORT INLÄGG"
    // 2. Visa alla med ShowAllEntries()
    // 3. Hämta idInput med GetInputWithPrompt
    // 4. Försök parsa till int, om misslyckas: visa fel och return
    // 5. Hämta entry med GetEntryById(id)
    // 6. Om entry är null: visa fel och return
    // 7. Visa vad som ska tas bort med DisplayEntry
    // 8. Fråga om bekräftelse (ja/nej)
    // 9. Om inte "ja": visa "Avbrutet!" och return
    // 10. Ta bort med DeleteFromDatabase(id)
    // 11. Visa bekräftelse
}
```

<details>
<summary>💡 Klicka för lösning</summary>

```csharp
static void DeleteEntry()
{
    Console.WriteLine("\n🗑️  TA BORT INLÄGG");

    ShowAllEntries();

    string idInput = GetInputWithPrompt("\nAnge ID på inlägg att ta bort: ");

    if (!int.TryParse(idInput, out int id))
    {
        Console.WriteLine("❌ Ogiltigt ID!");
        return;
    }

    DiaryEntry entry = GetEntryById(id);

    if (entry == null)
    {
        Console.WriteLine($"❌ Inget inlägg med ID {id} hittades!");
        return;
    }

    Console.WriteLine("\n⚠️  Du är på väg att ta bort:");
    DisplayEntry(entry);

    string confirm = GetInputWithPrompt("Är du säker? (ja/nej): ");

    if (confirm.ToLower() != "ja")
    {
        Console.WriteLine("Avbrutet!");
        return;
    }

    DeleteFromDatabase(id);

    Console.WriteLine("\n✅ Inlägg borttaget!");
}
```

</details>

---

## Metodöversikt - Sorterat efter ansvar

### 🔧 Setup & Configuration (2 metoder)
1. `SetupDatabase()` - Skapa databas-fil
2. `CreateDiaryTable()` - Skapa tabell

### 🎯 Main Flow (1 metod)
3. `RunMainMenu()` - Huvudmeny

### 🛠️ Utility (2 metoder)
4. `GetInputWithPrompt()` - Hämta input
5. `GetCurrentTimestamp()` - Skapa timestamp

### ➕ Create Operations (2 metoder)
6. `CreateNewEntry()` - Koordinera skapande
7. `SaveEntry()` - Spara i DB

### 📖 Read Operations (4 metoder)
8. `CreateEntryFromReader()` - Konvertera DB → objekt
9. `GetAllEntries()` - Hämta alla från DB
10. `DisplayEntry()` - Formatera visning
11. `ShowAllEntries()` - Visa alla

### 🔍 Search Operations (2 metoder)
12. `SearchInDatabase()` - Sök i DB
13. `SearchEntries()` - Koordinera sökning

### ✏️ Update Operations (3 metoder)
14. `GetEntryById()` - Hämta specifikt inlägg
15. `UpdateInDatabase()` - Uppdatera i DB
16. `UpdateEntry()` - Koordinera uppdatering

### 🗑️ Delete Operations (2 metoder)
17. `DeleteFromDatabase()` - Ta bort från DB
18. `DeleteEntry()` - Koordinera borttagning

**Totalt: 18 små, fokuserade metoder! 🎉**

---

## Clean Code Principer som används

### 1. Single Responsibility (SRP) ✅
- Varje metod har ETT ansvar
- `GetInputWithPrompt()` - Bara input
- `DisplayEntry()` - Bara visning
- `SaveEntry()` - Bara sparande

### 2. DRY (Don't Repeat Yourself) ✅
- `GetInputWithPrompt()` används överallt
- `DisplayEntry()` används i 4 olika metoder
- `GetCurrentTimestamp()` används i create och update

### 3. Beskrivande namn ✅
- `CreateNewEntry()` inte `DoStuff()`
- `GetEntryById()` inte `Get()`
- `DeleteFromDatabase()` inte `Remove()`

### 4. Små metoder ✅
- Varje metod är 5-30 rader
- Lätt att förstå och testa

### 5. Separation of Concerns ✅
- UI-logik (Console) i egna metoder
- Databas-logik isolerad
- Business logik (koordinering) separat

---

## Att fundera på

- Varför är det bra att ha många små metoder istället för få stora?
- Hur gör SRP koden lättare att testa?
- Vilka metoder skulle du kunna återanvända i ett annat projekt?
- Hur skulle du lägga till kategorier/taggar till dagboksinlägg?
- Varför använder vi en `DiaryEntry`-klass istället för lösa parametrar?

---

## Utmaningar (om du vill gå vidare!)

1. **Export till fil** - Skapa `ExportToTextFile()` som sparar alla inlägg

2. **Kategorier** - Lägg till kategori-fält (arbete, privat, resor)

3. **Stämningsläge** - Lägg till emoji/mood för varje dag (😊😐😢)

4. **Statistik** - Visa hur många inlägg per månad/år

5. **Backup** - Skapa automatisk backup av databasen

6. **Felhantering** - Lägg till try-catch runt databas-operationer

---

## Sammanfattning

✅ **En metod = Ett ansvar** - SRP i praktiken
✅ **Återanvändning** - DRY (Don't Repeat Yourself)
✅ **Beskrivande namn** - Självdokumenterande kod
✅ **Små metoder** - Max 30 rader
✅ **DiaryEntry-klass** - Tydlig data-struktur
✅ **Automatiskt datum** - Ingen manuell input!

**Kod som är lätt att förstå, underhålla och utöka! 🎉**

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
