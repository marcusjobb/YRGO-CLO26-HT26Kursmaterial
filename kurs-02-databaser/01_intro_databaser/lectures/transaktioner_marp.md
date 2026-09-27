---
marp: true
theme: nion-dark
paginate: true
---

# Transaktioner och ACID
## Allt eller ingenting

<!-- Transaktioner är ett av de viktigaste koncepten i databaser — och ett som dyker upp på jobbet direkt. Bankexemplet är klassiskt av en anledning: det gör det omedelbart konkret vad som händer om man inte har transaktioner. -->

---

# Problemet utan transaktioner

Du ska flytta 100 kr från konto A till konto B:

1. Dra 100 kr från konto A ✅
2. *Strömavbrott* 💥
3. Lägg till 100 kr på konto B ❌

Pengarna är borta. Ingen av kontona stämmer.

<!-- Skriv detta på tavlan som ett flöde. Fråga klassen: "Vad hade hänt om banken inte hade transaktioner?" Låt dem svara innan du går vidare. -->

---

# Lösningen: transaktionen

En transaktion grupperar operationer till **ett block**.

- Antingen lyckas **alla** operationer → **COMMIT**
- Eller misslyckas någon → **ROLLBACK** av allt

```
BEGIN
  dra 100 kr från konto A
  lägg 100 kr på konto B
COMMIT  ← eller ROLLBACK om något gick fel
```

<!-- Poängtera: databasen garanterar att antingen händer båda sakerna, eller ingen av dem. Det är kärnan. -->

---

# ACID — fyra garantier

**A** — Atomicity (Atomäritet)  
**C** — Consistency (Konsistens)  
**I** — Isolation (Isolering)  
**D** — Durability (Hållbarhet)

<!-- ACID är ett acronym som beskriver vad en databas lovar dig när du använder transaktioner. SQLite är ACID-kompatibel — det är en av anledningarna till att den är pålitlig trots att den är enkel. -->

---

# A — Atomicity

**Allt eller ingenting.**

Om en operation i transaktionen misslyckas rullas hela transaktionen tillbaka — som om ingen av operationerna körts.

```
BEGIN
  INSERT INTO order (...)       ✅
  UPDATE lager SET antal = ...  ❌ — fel!
ROLLBACK — ordern försvinner också
```

<!-- Det är "atom" i fysikbemärkelsen — odelbart. Du kan inte ha halva en transaktion committad. -->

---

# C — Consistency

**Databasen förblir i ett giltigt tillstånd.**

Transaktionen får inte bryta mot regler i databasen — NOT NULL, UNIQUE, FOREIGN KEY — varken före eller efter.

Om en regel bryts: automatisk rollback.

<!-- Consistency skyddar databasens integritet. Det är databasens lagar som aldrig får brytas. -->

---

# I — Isolation

**Transaktioner ser inte varandras halvfärdiga arbete.**

Medan du håller på: andra transaktioner ser gamla värden, inte dina ändringar.

Utan isolation: person B läser ett halvfärdigt saldo medan person A håller på att flytta pengar.

<!-- Isolation är det som gör att du kan ha tusentals parallella användare utan att de stör varandra. Det är också det komplicerade att implementera — men databasen hanterar det åt dig. -->

---

# D — Durability

**Committade ändringar överlever allt.**

När `COMMIT` är klart: data är skriven till disk. Strömavbrott, krasch, omstart — ändringarna finns kvar.

SQLite skriver till en `.db`-fil. Den filen är din garanti.

<!-- Durability är det som skiljer en databas från ett program med variabler i minnet. En variabel försvinner vid krasch. En committad transaktion gör det inte. -->

---

# SQLite och ACID

SQLite är **fullt ACID-kompatibel** — trots att det bara är en fil.

Den använder en journal-fil (`.db-journal` eller WAL) som säkerhetskopierar ändringar innan de skrivs permanent.

```
shop.db          ← din databas
shop.db-journal  ← temporär, tas bort efter COMMIT
```

<!-- Om en .db-journal-fil finns kvar efter en krasch: SQLite läser den vid uppstart och rullar tillbaka transaktionen automatiskt. Det är robust design. -->

---

# Transaktioner i C# med SQLite

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

    transaction.Commit();
}
catch
{
    transaction.Rollback();
}
```

<!-- Gå igenom koden rad för rad. Fråga: "Var i den här koden händer atomiciteten?" Svaret: i try/catch-blocket — Commit() eller Rollback(). -->

---

# Sammanfattning

| Bokstav | Egenskap | Garanti |
|---------|----------|---------|
| A | Atomicity | Allt eller ingenting |
| C | Consistency | Regler bryts aldrig |
| I | Isolation | Transaktioner stör inte varandra |
| D | Durability | Committade data överlever krasch |

SQLite håller alla fyra löften — i en enda fil.

<!-- Avsluta med att låta dem repetera ACID utan att titta: "Stäng ner, förklara ACID för grannen med ett eget exempel." 2 minuter, sedan en frivillig redovisar. -->
