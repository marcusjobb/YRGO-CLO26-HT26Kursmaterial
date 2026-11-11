---
marp: true
theme: nion-dark
paginate: true
---

# 🦸 Hero-databasen

### Från kaos till elegant kod — steg för steg

---

## Det här passet

Du ska bygga samma program **sju gånger**.

Varje labb är en förbättring av föregående.  
I slutet har du ett system som kan byta databas utan att röra ett enda tecken i koden.

---

## Labb 1 — Kaos

```csharp
// Allt SQL ligger rakt i Program.cs
using var connection = new SqliteConnection(connectionString);
connection.Open();
var cmd = connection.CreateCommand();
cmd.CommandText = "INSERT INTO heroes (name, age, city) VALUES (@name, @age, @city)";
cmd.Parameters.AddWithValue("@name", "Bella");
// ...och så vidare, rad efter rad
```

Det funkar. Det är också omöjligt att hålla koll på.

---

## Labb 2 — En klass tar hand om SQL

```csharp
var handler = new SQLiteHandler(connectionString);
handler.Insert("heroes", ["Bella", "28", "Malmö"]);
```

`Program.cs` vet inte längre hur SQL fungerar. Bara vad den vill göra.

Men: `["Bella", "28", "Malmö"]` — vilken ordning är rätt?  
Lätt att göra fel. Lätt att missa ett värde.

---

## Labb 3 — Objekt istället för strängar

```csharp
var hero = new Hero { Name = "Bella", Age = 28, City = "Malmö" };
handler.Insert(hero);

var found = handler.Find("Bella");
Console.WriteLine(found); // Hero { Id = 1, Name = Bella, Age = 28, City = Malmö }
```

Nu kan kompilatorn hjälpa dig. Inget mer gissande på ordning.

---

## Labb 4 — Interfacet (facade)

```csharp
IDbHandler handler = provider == "TiDB"
    ? new TiDBHandler(connectionString)
    : new SQLiteHandler(connectionString);
```

Byt rad i `appsettings.json`:
```json
"Database": "TiDB"
```

Kör. Samma output. Ingen kodändring.

---

## Varför är det här viktigt?

```
Labb 1  → Raw SQL i main         (det funkar, men det är kaos)
Labb 2  → Handler-klass          (separera ansvar)
Labb 3  → Hero-objekt            (typsäkerhet, reflection)
Labb 4  → Interface              (byt databas utan att röra kod)
Labb 5  → Generics               (fungerar med vilken klass som helst)
Labb 6  → Dependency Injection   (testar utan riktig databas)
Labb 7  → Fler servrar           (valfri fördjupning)
```

Varje steg löser ett verkligt problem från steget innan.

---

## Vad behöver du?

- .NET 8 eller 9
- `Microsoft.Data.Sqlite` (NuGet)
- `Microsoft.Extensions.Configuration.Json` (NuGet)
- MySQL i Docker (från tidigare lektion) — behövs i labb 4

**Starta med labb 1.** Läs hela instruktionen innan du kodar.

---

## Viktigt från dag ett

`appsettings.json` innehåller din connectionstring.  
Den ska **aldrig** pushas till GitHub.

```
# .gitignore
appsettings.json
*.db
bin/
obj/
```

Checka in `appsettings.example.json` istället.  
Det berättar för andra vad som behövs, utan att läcka hemligheter.

---

## Kör!

Filer finns i repot under:  
`kurs-02-databaser/01_intro_databaser/exercises/hero_databas/`

Fastnar du? Fråga. Det är meningen att det ska vara svårt.

🟢 Börja med labb 1.
