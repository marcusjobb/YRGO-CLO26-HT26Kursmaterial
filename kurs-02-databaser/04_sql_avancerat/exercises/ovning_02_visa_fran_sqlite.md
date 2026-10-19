# Övning 2 — Visa låtar från SQLite 🟢

Menyn fungerar. Nu kopplar vi på databasen — men vi börjar enkelt: bara visa data.

---

## Uppgift

Skapa ett program som ansluter till en SQLite-databas och visar alla låtar i en tabell i konsolen.

Du behöver:
1. Skapa en databas `musik.db` med en tabell `Låtar`
2. Stoppa in tre testlåtar direkt i koden (så databasen inte är tom)
3. Hämta och visa alla låtar

### Tabellstruktur

```sql
CREATE TABLE IF NOT EXISTS Låtar (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Titel TEXT NOT NULL,
    Längd INTEGER
);
```

### Krav

- Skapa tabellen om den inte finns (`CREATE TABLE IF NOT EXISTS`)
- Stoppa in tre testlåtar med INSERT (bara om tabellen är tom — annars fyller du den varje gång)
- Hämta alla låtar och skriv ut dem snyggt

---

## Förväntad output

```
=== Låtlista ===
ID  Titel                    Längd
--- ------------------------ -----
1   Bohemian Rhapsody        354
2   Hotel California         391
3   Stairway to Heaven       482
```

---

## Tips

```csharp
// Kontrollera om tabellen är tom innan du seedar
command.CommandText = "SELECT COUNT(*) FROM Låtar";
long antal = (long)command.ExecuteScalar()!;
if (antal == 0)
{
    // Stoppa in testdata här
}
```

```csharp
// Formatera kolumner snyggt
Console.WriteLine($"{reader["Id"],-4}{reader["Titel"],-25}{reader["Längd"],5}");
```

---

## Utmanande frågor

- 💡 Vad händer om du kör programmet två gånger utan att kolla `COUNT(*)`?
- 💡 Vad är skillnaden mellan `ExecuteNonQuery`, `ExecuteScalar` och `ExecuteReader`?
- 💡 Var hamnar filen `musik.db` på din dator?
