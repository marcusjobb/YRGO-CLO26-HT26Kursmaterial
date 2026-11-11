# Övning 5 — Flytta databaskoden till en egen klass 🟡

CRUD fungerar. Men just nu ligger troligtvis all databaskod i `Program.cs`. Det är dags att städa.

---

## Uppgift

Skapa klassen `LåtRepository` som hanterar all kommunikation med databasen. `Program.cs` ska bara hantera menyn och kalla på metoderna — inget om `SqliteConnection` eller `SqliteCommand` där.

### Klassen ska ha dessa metoder

```csharp
public class LåtRepository
{
    public void SkapaTabellOmSaknas()  { ... }
    public List<Låt> HämtaAlla()       { ... }
    public void LäggTill(string titel, int längd) { ... }
    public Låt? HämtaEfterID(int id)   { ... }
    public void TaBort(int id)          { ... }
}
```

### Modellklassen

Du behöver också en enkel klass för att hålla låtdata:

```csharp
public class Låt
{
    public int Id { get; set; }
    public string Titel { get; set; } = "";
    public int Längd { get; set; }
}
```

### Krav

- `Program.cs` innehåller bara menylogik — inga SQL-anrop
- Anslutningssträngen (`Data Source=musik.db`) finns bara på ett ställe, i klassen
- Alla fem metoderna ska fungera

---

## Förväntad struktur

```
MusicApp/
├── Program.cs          ← bara meny och anrop
├── LåtRepository.cs    ← all databaskod
└── Låt.cs              ← datamodell
```

`Program.cs` kan se ut ungefär så:

```csharp
var repo = new LåtRepository();
repo.SkapaTabellOmSaknas();

bool kör = true;
while (kör)
{
    Console.WriteLine("\n=== Min musikapp ===");
    Console.WriteLine("1. Visa alla låtar");
    Console.WriteLine("2. Lägg till låt");
    Console.WriteLine("3. Ta bort låt");
    Console.WriteLine("4. Avsluta");
    Console.Write("\nVälj: ");

    // ... switch/if för att hantera valet och anropa repo-metoder
}
```

---

## Utmanande frågor

- 💡 Varför är det bra att hålla databaskoden i en separat klass?
- 💡 Vad händer om du i framtiden vill byta från SQLite till en annan databas — var behöver du ändra?
- 💡 Vad är fördelen med att `HämtaAlla()` returnerar `List<Låt>` istället för att skriva ut direkt till konsolen?
