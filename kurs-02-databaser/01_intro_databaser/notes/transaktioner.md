# Transaktioner och ACID

## Problemet utan transaktioner

Tänk dig en banköverföring: 100 kr ska flyttas från konto A till konto B. Det kräver två operationer — dra från A, lägg till på B.

Vad händer om strömmen går ut exakt mellan de två operationerna? Pengarna är dragna från A men aldrig insatta på B. Ingen av kontona stämmer, och ingenting indikerar att något gick fel.

Det är problemet som transaktioner löser.

---

## Vad är en transaktion?

En transaktion är ett block av databasoperationer som behandlas som **en enda enhet**. Antingen lyckas alla operationer och ändringarna sparas — eller misslyckas någon och allt rullas tillbaka som om inget hänt.

```
BEGIN
  dra 100 kr från konto A
  lägg 100 kr på konto B
COMMIT  ← allt lyckades, spara
```

Om något går fel:

```
ROLLBACK  ← ingenting sparas, databasen är oförändrad
```

---

## ACID — fyra garantier

ACID beskriver vad en databas lovar dig när du använder transaktioner. SQLite är fullt ACID-kompatibel.

### A — Atomicity (Atomäritet)

Allt eller ingenting. Om en operation i transaktionen misslyckas rullas hela transaktionen tillbaka — som om ingen av operationerna körts.

### C — Consistency (Konsistens)

Databasen förblir i ett giltigt tillstånd. Transaktionen får inte bryta mot regler — NOT NULL, UNIQUE, FOREIGN KEY. Om en regel bryts: automatisk rollback.

### I — Isolation (Isolering)

Transaktioner ser inte varandras halvfärdiga arbete. Medan du håller på ser andra transaktioner gamla värden, inte dina pågående ändringar. Det gör att tusentals parallella användare kan arbeta utan att störa varandra.

### D — Durability (Hållbarhet)

När `COMMIT` är klart är data skriven till disk. Strömavbrott, krasch, omstart — ändringarna finns kvar.

---

## SQLite och ACID

SQLite är ACID-kompatibel trots att det bara är en fil. Den använder en journal-fil som säkerhetskopierar ändringar innan de skrivs permanent:

```
shop.db          ← din databas
shop.db-journal  ← temporär säkerhetskopia, tas bort efter COMMIT
```

Om en `.db-journal`-fil finns kvar efter en krasch läser SQLite den vid uppstart och rullar tillbaka transaktionen automatiskt.

---

## Transaktioner i C# med SQLite

```csharp
using var connection = new SqliteConnection("Data Source=shop.db");
connection.Open();

using var transaction = connection.BeginTransaction();
try
{
    var cmd1 = connection.CreateCommand();
    cmd1.CommandText = "UPDATE konton SET saldo = saldo - 100 WHERE id = 1";
    cmd1.ExecuteNonQuery();

    var cmd2 = connection.CreateCommand();
    cmd2.CommandText = "UPDATE konton SET saldo = saldo + 100 WHERE id = 2";
    cmd2.ExecuteNonQuery();

    transaction.Commit();   // båda lyckades — spara
}
catch
{
    transaction.Rollback(); // något gick fel — rulla tillbaka allt
}
```

`BeginTransaction()` startar transaktionen. `Commit()` sparar. `Rollback()` i catch-blocket ser till att databasen förblir oförändrad om något kastar ett undantag.

---

## Sammanfattning

| Bokstav | Egenskap | Garanti |
|---------|----------|---------|
| A | Atomicity | Allt eller ingenting |
| C | Consistency | Regler bryts aldrig |
| I | Isolation | Transaktioner stör inte varandra |
| D | Durability | Committade data överlever krasch |

---

## Öva

Skapa en tabell `bank_accounts` med kolumnerna `id`, `kontonummer` och `saldo`. Skriv en C#-transaktion som:

1. Drar 200 kr från konto `'1001'`
2. Lägger 200 kr på konto `'1002'`
3. Rullar tillbaka om saldot på `'1001'` skulle bli negativt

Verifiera i DB Browser att saldona stämmer efter körningen.
