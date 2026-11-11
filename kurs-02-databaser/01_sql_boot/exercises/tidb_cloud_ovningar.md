# Övningar — MySQL i molnet med TiDB Cloud

🟢

I de här övningarna ansluter ni mot en riktig MySQL-databas i molnet. Ni jobbar alla mot samma server — det är poängen. Det är så det ser ut i verkligheten.

Connection string får ni av läraren.

Kom ihåg: allt som pratar med databasen ska vara insvept i `try-catch`. Se [felhantering_try_catch.md](../notes/felhantering_try_catch.md).

---

## Övning 1 — Kom igång 🟢

Skapa ett nytt konsolprojekt och installera rätt paket.

```bash
dotnet new console -n TiDBLabb
cd TiDBLabb
dotnet add package MySql.Data
```

Koppla upp dig mot databasen och skriv ut ett bekräftelsemeddelande.

### Förväntad output

```
Ansluten till Nordbanken Cloud™
Serverversion: 8.x.x (eller liknande)
```

**Tips:** `connection.ServerVersion` ger dig versionen.

---

## Övning 2 — Skapa din grupps databas 🟢

Varje grupp skapar sin egen databas i det delade clustret.

```sql
CREATE DATABASE IF NOT EXISTS grupp1;
```

Byt sedan `Database=test` i din connection string mot `Database=grupp1`.

### Förväntad output

```
Databas 'grupp1' skapad (eller finns redan).
Ansluten till grupp1.
```

**Tips:** `ExecuteNonQuery()` kör kommandon som inte returnerar rader.

---

## Övning 3 — Skapa en tabell 🟢

Skapa en tabell för produkter i er butik. Välj ett tema ni gillar — mataffär, spelbutik, rymdstation, vad som helst.

Tabellen ska ha minst:
- `id` (primärnyckel, auto increment)
- `name` (text)
- `price` (decimalt tal)

### Förväntad output

```
Tabell 'products' skapad.
```

---

## Övning 4 — Lägg in data 🟡

Lägg in minst 5 produkter i er tabell med `INSERT`. Gör det i kod, inte i ett GUI.

### Förväntad output

```
5 produkter inlagda.
```

---

## Övning 5 — Hämta och visa data 🟡

Hämta alla produkter och skriv ut dem snyggt i terminalen.

### Förväntad output

```
ID  Namn                Pris
1   Laserkanon Mk. II   4 999 kr
2   Rymdhjälm Basic     1 299 kr
3   Gravitationsbälte   7 500 kr
...
```

---

## Övning 6 — Sök med parameter 🟡

Be användaren skriva in ett maxpris. Hämta alla produkter som kostar mindre än det.

Använd **parametriserad query** — inte string-konkatenering.

### Förväntad output

```
Maxpris: 2000
Produkter under 2 000 kr:
- Rymdhjälm Basic: 1 299 kr
```

---

## Utmanande frågor 🔴

1. Vad händer om två grupper skapar en tabell med samma namn i sitt eget schema? Kan de se varandras data?

2. Ni använder alla samma root-användare. Vad är problemet med det i ett riktigt system?

3. Varför kräver TiDB Cloud `SslMode=Required` men inte lokal Docker?

---

## Lösningsförslag

Lösningar finns i `_teacher/solutions/`.
