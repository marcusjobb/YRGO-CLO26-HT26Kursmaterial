---

title: Övning: CRUD - Skapa, Läsa, Uppdatera, Ta bort 📝
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/2_databases/exercises/ado/2_crud_operations.md"
description: "Nu ska vi lära oss de fyra grundläggande operationerna som alla databasapplikationer behöver:"
tags: ["bort", "crud", "csharp", "databaser", "exercise", "läsa,", "operations", "skapa,", "ssh", "uppdatera,"]
week_fit: []
---

# Övning: CRUD - Skapa, Läsa, Uppdatera, Ta bort 📝

🟢


## Din uppgift

Nu ska vi lära oss de fyra grundläggande operationerna som alla databasapplikationer behöver:
- **C**reate - Lägga till ny data
- **R**ead - Läsa och visa data
- **U**pdate - Uppdatera befintlig data
- **D**elete - Ta bort data

Vi bygger vidare på din databas från förra övningen och skapar en enkel meny där användaren kan mata in egna personer!

## Teknikförklaring

### SQL-kommandon du kommer använda

- **INSERT** - Lägger till ny data i tabellen
- **SELECT** - Hämtar data från tabellen
- **UPDATE** - Ändrar befintlig data
- **DELETE** - Tar bort data

### Parametrar i SQL (VIKTIGT! 🔐)

När vi stoppar in användardata i SQL-frågor använder vi **parametrar** istället för att bara limma ihop strängar. Annars kan någon elak person förstöra hela din databas (kallas SQL-injection).

**DÅLIGT** ❌ (ALDRIG göra så här!):
```csharp
string sql = "INSERT INTO Personer (Namn) VALUES ('" + namn + "')";
```

**BRA** ✅ (Använd parametrar!):
```csharp
string sql = "INSERT INTO Personer (Namn) VALUES (@namn)";
command.Parameters.AddWithValue("@namn", namn);
```

### ExecuteReader vs ExecuteScalar vs ExecuteNonQuery

- **ExecuteReader()** - När du vill ha MÅNGA rader tillbaka (SELECT med flera resultat)
- **ExecuteScalar()** - När du vill ha ETT värde tillbaka (t.ex. COUNT)
- **ExecuteNonQuery()** - När du INTE vill ha något tillbaka (INSERT, UPDATE, DELETE)

---

## Steg 1: Bygg vidare på föregående projekt

Du kan antingen fortsätta i ditt projekt från övning 1, eller kopiera databas-funktionerna därifrån.

---

## Kod-skelett

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
using System;
using System.IO;
using System.Data.SQLite;

class Program
{
    // Spara connection string som en statisk variabel så alla metoder kan använda den
    static string connectionString = "";

    static void Main()
    {
        // Sätt upp databasen (använd din kod från förra övningen!)
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbFolderPath = Path.Combine(documentsPath, "databases");
        Directory.CreateDirectory(dbFolderPath);
        string dbPath = Path.Combine(dbFolderPath, "minadata.db");

        if (!File.Exists(dbPath))
        {
            SQLiteConnection.CreateFile(dbPath);
        }

        connectionString = $"Data Source={dbPath}";

        // Skapa tabellen om den inte finns
        CreateTableIfNotExists();

        // Huvudmeny
        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== PERSONREGISTER ===");
            Console.WriteLine("1. Lägg till person");
            Console.WriteLine("2. Visa alla personer");
            Console.WriteLine("3. Uppdatera person");
            Console.WriteLine("4. Ta bort person");
            Console.WriteLine("5. Avsluta");
            Console.Write("\nVälj (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatePerson();
                    break;
                case "2":
                    ReadAllPersons();
                    break;
                case "3":
                    UpdatePerson();
                    break;
                case "4":
                    DeletePerson();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Hej då! 👋");
                    break;
                default:
                    Console.WriteLine("❌ Ogiltigt val, försök igen!");
                    break;
            }
        }
    }

    static void CreateTableIfNotExists()
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Personer (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Namn TEXT NOT NULL,
                    Ålder INTEGER
                )";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    // TODO: Implementera CREATE - Lägg till en ny person
    static void CreatePerson()
    {
        Console.WriteLine("\n--- Lägg till ny person ---");

        // 1. Fråga användaren efter namn
        Console.Write("Namn: ");
        string namn = Console.ReadLine();

        // 2. Fråga användaren efter ålder
        Console.Write("Ålder: ");
        int ålder = int.Parse(Console.ReadLine());

        // 3. Skapa SQL-frågan med PARAMETRAR (inte string concatenation!)
        // Tipset: INSERT INTO Personer (Namn, Ålder) VALUES (@namn, @ålder)

        // 4. Öppna connection, skapa command, lägg till parametrar, kör ExecuteNonQuery()

        // 5. Skriv ut ett bekräftelsemeddelande

        Console.WriteLine("✅ Person tillagd!");
    }

    // TODO: Implementera READ - Visa alla personer
    static void ReadAllPersons()
    {
        Console.WriteLine("\n--- Alla personer ---");

        // 1. Skapa SQL-frågan
        // Tipset: SELECT Id, Namn, Ålder FROM Personer

        // 2. Öppna connection och skapa command

        // 3. Använd ExecuteReader() för att få tillbaka en SQLiteDataReader
        // ExecuteReader() ger oss något som heter en "DataReader"
        // Det är som en pekare som går igenom alla rader i resultatet

        // 4. Loopa igenom resultatet med while (reader.Read())
        // reader.Read() flyttar fram pekaren till nästa rad och returnerar true om det finns fler rader

        // 5. För varje rad, läs ut värdena:
        // - reader.GetInt32(0) ger första kolumnen som int (Id)
        // - reader.GetString(1) ger andra kolumnen som string (Namn)
        // - reader.GetInt32(2) ger tredje kolumnen som int (Ålder)

        // 6. Skriv ut varje person på ett snyggt sätt

        // TIPS: Glöm inte att stänga reader när du är klar!
        // Eller använd 'using' för att det ska hända automatiskt.
    }

    // TODO: Implementera UPDATE - Uppdatera en person
    static void UpdatePerson()
    {
        Console.WriteLine("\n--- Uppdatera person ---");

        // Först visar vi alla personer så användaren ser vilka som finns
        ReadAllPersons();

        // 1. Fråga vilket Id användaren vill uppdatera
        Console.Write("\nAnge Id på personen du vill uppdatera: ");
        int id = int.Parse(Console.ReadLine());

        // 2. Fråga efter nytt namn
        Console.Write("Nytt namn: ");
        string nyttNamn = Console.ReadLine();

        // 3. Fråga efter ny ålder
        Console.Write("Ny ålder: ");
        int nyÅlder = int.Parse(Console.ReadLine());

        // 4. Skapa SQL-frågan med parametrar
        // Tipset: UPDATE Personer SET Namn = @namn, Ålder = @ålder WHERE Id = @id

        // 5. Öppna connection, skapa command, lägg till parametrar, kör ExecuteNonQuery()

        // 6. ExecuteNonQuery() returnerar antalet påverkade rader
        // Om det är 0 betyder det att inget hittades med det Id:t

        Console.WriteLine("✅ Person uppdaterad!");
    }

    // TODO: Implementera DELETE - Ta bort en person
    static void DeletePerson()
    {
        Console.WriteLine("\n--- Ta bort person ---");

        // Visa alla personer först
        ReadAllPersons();

        // 1. Fråga vilket Id användaren vill ta bort
        Console.Write("\nAnge Id på personen du vill ta bort: ");
        int id = int.Parse(Console.ReadLine());

        // 2. Fråga om användaren är säker (säkerhetscheck!)
        Console.Write($"Är du säker på att du vill ta bort person med Id {id}? (ja/nej): ");
        string svar = Console.ReadLine();

        if (svar.ToLower() != "ja")
        {
            Console.WriteLine("Avbrutet!");
            return;
        }

        // 3. Skapa SQL-frågan med parameter
        // Tipset: DELETE FROM Personer WHERE Id = @id

        // 4. Öppna connection, skapa command, lägg till parameter, kör ExecuteNonQuery()

        Console.WriteLine("✅ Person borttagen!");
    }
}
```

---

## Förväntad output (exempel)

```
=== PERSONREGISTER ===
1. Lägg till person
2. Visa alla personer
3. Uppdatera person
4. Ta bort person
5. Avsluta

Välj (1-5): 1

--- Lägg till ny person ---
Namn: Anna Andersson
Ålder: 25
✅ Person tillagd!

=== PERSONREGISTER ===
1. Lägg till person
2. Visa alla personer
3. Uppdatera person
4. Ta bort person
5. Avsluta

Välj (1-5): 1

--- Lägg till ny person ---
Namn: Bengt Bengtsson
Ålder: 42
✅ Person tillagd!

=== PERSONREGISTER ===
1. Lägg till person
2. Visa alla personer
3. Uppdatera person
4. Ta bort person
5. Avsluta

Välj (1-5): 2

--- Alla personer ---
[1] Anna Andersson, 25 år
[2] Bengt Bengtsson, 42 år

=== PERSONREGISTER ===
1. Lägg till person
2. Visa alla personer
3. Uppdatera person
4. Ta bort person
5. Avsluta

Välj (1-5): 3

--- Uppdatera person ---
[1] Anna Andersson, 25 år
[2] Bengt Bengtsson, 42 år

Ange Id på personen du vill uppdatera: 1
Nytt namn: Anna Svensson
Ny ålder: 26
✅ Person uppdaterad!

=== PERSONREGISTER ===
1. Lägg till person
2. Visa alla personer
3. Uppdatera person
4. Ta bort person
5. Avsluta

Välj (1-5): 2

--- Alla personer ---
[1] Anna Svensson, 26 år
[2] Bengt Bengtsson, 42 år
```

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
using System;
using System.IO;
using System.Data.SQLite;

class Program
{
    static string connectionString = "";

    static void Main()
    {
        string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        string dbFolderPath = Path.Combine(documentsPath, "databases");
        Directory.CreateDirectory(dbFolderPath);
        string dbPath = Path.Combine(dbFolderPath, "minadata.db");

        if (!File.Exists(dbPath))
        {
            SQLiteConnection.CreateFile(dbPath);
        }

        connectionString = $"Data Source={dbPath}";
        CreateTableIfNotExists();

        bool running = true;
        while (running)
        {
            Console.WriteLine("\n=== PERSONREGISTER ===");
            Console.WriteLine("1. Lägg till person");
            Console.WriteLine("2. Visa alla personer");
            Console.WriteLine("3. Uppdatera person");
            Console.WriteLine("4. Ta bort person");
            Console.WriteLine("5. Avsluta");
            Console.Write("\nVälj (1-5): ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CreatePerson();
                    break;
                case "2":
                    ReadAllPersons();
                    break;
                case "3":
                    UpdatePerson();
                    break;
                case "4":
                    DeletePerson();
                    break;
                case "5":
                    running = false;
                    Console.WriteLine("Hej då! 👋");
                    break;
                default:
                    Console.WriteLine("❌ Ogiltigt val, försök igen!");
                    break;
            }
        }
    }

    static void CreateTableIfNotExists()
    {
        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = @"
                CREATE TABLE IF NOT EXISTS Personer (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Namn TEXT NOT NULL,
                    Ålder INTEGER
                )";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.ExecuteNonQuery();
            }
        }
    }

    static void CreatePerson()
    {
        Console.WriteLine("\n--- Lägg till ny person ---");

        Console.Write("Namn: ");
        string namn = Console.ReadLine();

        Console.Write("Ålder: ");
        int ålder = int.Parse(Console.ReadLine());

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            // Vi använder @ framför parameternamn i SQL
            string sql = "INSERT INTO Personer (Namn, Ålder) VALUES (@namn, @ålder)";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // AddWithValue kopplar ihop parametern med värdet
                // Detta skyddar mot SQL-injection!
                command.Parameters.AddWithValue("@namn", namn);
                command.Parameters.AddWithValue("@ålder", ålder);

                command.ExecuteNonQuery();
            }
        }

        Console.WriteLine("✅ Person tillagd!");
    }

    static void ReadAllPersons()
    {
        Console.WriteLine("\n--- Alla personer ---");

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT Id, Namn, Ålder FROM Personer";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                // ExecuteReader ger oss en "reader" som kan gå igenom resultatet
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    bool harData = false;

                    // Läs() flyttar fram till nästa rad och returnerar true om det finns mer
                    while (reader.Read())
                    {
                        harData = true;

                        // Hämta värden från kolumnerna (0-indexerat)
                        int id = reader.GetInt32(0);      // Första kolumnen (Id)
                        string namn = reader.GetString(1); // Andra kolumnen (Namn)
                        int ålder = reader.GetInt32(2);   // Tredje kolumnen (Ålder)

                        Console.WriteLine($"[{id}] {namn}, {ålder} år");
                    }

                    if (!harData)
                    {
                        Console.WriteLine("(Inga personer registrerade än)");
                    }
                }
            }
        }
    }

    static void UpdatePerson()
    {
        Console.WriteLine("\n--- Uppdatera person ---");
        ReadAllPersons();

        Console.Write("\nAnge Id på personen du vill uppdatera: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write("Nytt namn: ");
        string nyttNamn = Console.ReadLine();

        Console.Write("Ny ålder: ");
        int nyÅlder = int.Parse(Console.ReadLine());

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "UPDATE Personer SET Namn = @namn, Ålder = @ålder WHERE Id = @id";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@namn", nyttNamn);
                command.Parameters.AddWithValue("@ålder", nyÅlder);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine("❌ Ingen person med det Id:t hittades!");
                    return;
                }
            }
        }

        Console.WriteLine("✅ Person uppdaterad!");
    }

    static void DeletePerson()
    {
        Console.WriteLine("\n--- Ta bort person ---");
        ReadAllPersons();

        Console.Write("\nAnge Id på personen du vill ta bort: ");
        int id = int.Parse(Console.ReadLine());

        Console.Write($"Är du säker på att du vill ta bort person med Id {id}? (ja/nej): ");
        string svar = Console.ReadLine();

        if (svar.ToLower() != "ja")
        {
            Console.WriteLine("Avbrutet!");
            return;
        }

        using (SQLiteConnection connection = new SQLiteConnection(connectionString))
        {
            connection.Open();

            string sql = "DELETE FROM Personer WHERE Id = @id";

            using (SQLiteCommand command = new SQLiteCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    Console.WriteLine("❌ Ingen person med det Id:t hittades!");
                    return;
                }
            }
        }

        Console.WriteLine("✅ Person borttagen!");
    }
}
```

</details>

---

## Viktiga koncept att förstå

### SQL-parametrar (SQL injection-skydd)

När vi använder användarinput i SQL-frågor MÅSTE vi använda parametrar:

```csharp
// Detta är FARLIGT! ❌
string sql = $"INSERT INTO Personer (Namn) VALUES ('{namn}')";
// Om namn = "Robert'); DROP TABLE Personer; --" kan hela tabellen raderas!

// Detta är SÄKERT! ✅
string sql = "INSERT INTO Personer (Namn) VALUES (@namn)";
command.Parameters.AddWithValue("@namn", namn);
// Parametern rensas automatiskt från farlig kod
```

### DataReader-mönstret

```csharp
using (SQLiteDataReader reader = command.ExecuteReader())
{
    while (reader.Read())  // Går till nästa rad, returnerar false när det är slut
    {
        // Hämta data från aktuell rad
        int id = reader.GetInt32(0);        // Kolumnindex 0
        string namn = reader.GetString(1);   // Kolumnindex 1
    }
}
```

### ExecuteNonQuery() returnerar antal påverkade rader

```csharp
int rowsAffected = command.ExecuteNonQuery();
if (rowsAffected == 0)
{
    Console.WriteLine("Inget hittades!");
}
```

---

## Att fundera på

- Vad händer om användaren skriver in text istället för ett tal när programmet frågar efter ålder?
- Varför är det viktigt att använda parametrar istället för string concatenation?
- Vad returnerar `ExecuteNonQuery()` och hur kan vi använda det?
- Prova att öppna din databas i DB Browser for SQLite medan programmet körs - ser du ändringarna?
- Vad händer om två personer har samma namn? Kan vi fortfarande uppdatera dem korrekt?

---

## Utmaningar (om du vill gå vidare!)

1. **Sökfunktion** - Lägg till ett menyval som låter användaren söka efter personer vars namn innehåller en viss text (tipset: `WHERE Namn LIKE '%sökning%'`)

2. **Felhantering** - Lägg till try-catch för att hantera om användaren skriver in felaktig data

3. **Bekräftelse vid uppdatering** - Visa personens nuvarande data innan uppdatering så användaren vet vad som kommer ändras

4. **Sortering** - Låt användaren välja om listan ska sorteras på namn eller ålder (tipset: `ORDER BY Namn ASC`)

---

## Nästa steg

Nu kan du grunderna i CRUD! Nästa övning kommer handla om:
- Att jobba med flera tabeller (relationer)
- JOIN-operationer
- Foreign keys

Bra jobbat! 🎉

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
