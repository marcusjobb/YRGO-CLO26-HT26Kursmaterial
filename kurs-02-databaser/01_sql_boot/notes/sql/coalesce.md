# COALESCE: ett reservvärde för NULL

🟡

`NULL` dyker upp överallt: en person utan by, en by utan invånare, en kund som inte har angett något telefonnummer. Och `NULL` smittar. Räknar du med det blir svaret `NULL`, och sätter du ihop text med det försvinner hela texten.

`COALESCE` är verktyget som säger: *om det här saknas, använd det här i stället.*

*Exemplen använder [övningsdatan](ovningsdata.md).*

## Vad gör COALESCE?

`COALESCE` tar två eller fler värden och ger tillbaka **det första som inte är `NULL`**.

```sql
SELECT COALESCE(NULL, NULL, 'tredje', 'fjärde');   -- 'tredje'
SELECT COALESCE('första', 'andra');                -- 'första'
SELECT COALESCE(NULL, NULL);                       -- NULL
```

På svenska:

> *Titta på värdena från vänster till höger. Ge mig det första som finns.*

Är alla `NULL` blir svaret `NULL`. Oftast har man två värden: kolumnen, och ett reservvärde om kolumnen är tom.

Uttalas ungefär "ko-a-LESS". Ordet betyder *smälta samman*.

## Ett reservvärde i resultatet

David bor inte i någon by. Med en `LEFT JOIN` får han `NULL` som bynamn:

```sql
SELECT p.name, COALESCE(v.name, 'ingen by') AS village
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

| name | village |
|---|---|
| Anna | Lökby |
| Bertil | Gurkby |
| Cissi | Lökby |
| David | ingen by |
| Eva | Gurkby |

Tabellen ändras inte. `COALESCE` påverkar bara det som visas i resultatet, precis som `AS`.

## Aggregat efter en LEFT JOIN

Hur mycket guld finns det i varje by?

```sql
SELECT v.name, SUM(p.gold) AS guld
FROM village AS v
LEFT JOIN person AS p ON p.village_id = v.id
GROUP BY v.name;
```

| name | guld |
|---|---|
| Gurkby | 550 |
| Lökby | 200 |
| Morotsby | NULL |

Morotsby har inga invånare, så det finns inget att summera, och `SUM` av ingenting blir `NULL`. Men i en rapport vill man nästan alltid se **0**:

```sql
SELECT v.name, COALESCE(SUM(p.gold), 0) AS guld
FROM village AS v
LEFT JOIN person AS p ON p.village_id = v.id
GROUP BY v.name;
```

| name | guld |
|---|---|
| Gurkby | 550 |
| Lökby | 200 |
| Morotsby | 0 |

Det här är ett av de vanligaste användningsområdena för `COALESCE`, och du kommer att se `COALESCE(SUM(...), 0)` i mycket SQL.

**`COUNT` behöver det inte.** `COUNT(p.id)` ger redan `0` för Morotsby, eftersom att räkna ingenting blir noll. Det är bara `SUM`, `AVG`, `MIN` och `MAX` som ger `NULL` när det inte finns något att räkna med.

### ⚠️ COALESCE inuti ett aggregat ändrar svaret

```sql
SELECT AVG(village_id),                 -- 1.5
       AVG(COALESCE(village_id, 0))     -- 1.2
FROM person;
```

`AVG` hoppar över `NULL`, så det första snittet räknas på fyra personer. I det andra har David fått `0` och räknas med som en femte. Båda kan vara rätt, men de svarar på **olika frågor**. Tänk efter vilken du menar.

`COALESCE(SUM(...), 0)` är oftast det du vill ha. Det byter bara ut slutresultatet. `SUM(COALESCE(...))` ändrar vad som räknas.

## Text som sätts ihop

`||` sätter ihop text, men allt som sätts ihop med `NULL` blir `NULL`:

```sql
SELECT p.name || ' bor i ' || v.name AS sentence
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

| sentence |
|---|
| Anna bor i Lökby |
| Bertil bor i Gurkby |
| Cissi bor i Lökby |
| NULL |
| Eva bor i Gurkby |

Davids mening försvann helt. Med `COALESCE` kan du byta ut **hela den del** som saknas:

```sql
SELECT p.name || COALESCE(' bor i ' || v.name, ' bor ingenstans') AS sentence
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

| sentence |
|---|
| Anna bor i Lökby |
| Bertil bor i Gurkby |
| Cissi bor i Lökby |
| David bor ingenstans |
| Eva bor i Gurkby |

Knepet är att `' bor i ' || v.name` blir `NULL` för David, eftersom `v.name` är `NULL`. Då tar `COALESCE` reservtexten. Du kan alltså använda `COALESCE` på hela uttryck, inte bara på kolumner.

## Flera reservvärden

Tänk dig en kontakttabell där en person kan ha mobilnummer, hemnummer, båda eller inget. (Den tabellen finns inte i övningsdatan, så det här exemplet är bara att läsa.)

```sql
SELECT name, COALESCE(mobile, home_phone, 'saknas') AS phone
FROM contact;
```

Har personen ett mobilnummer används det. Annars hemnumret. Annars texten `saknas`. Ordningen bestämmer vad som är viktigast.

## Tom text är inte NULL

```sql
UPDATE person SET job = '' WHERE id = 4;

SELECT name, COALESCE(job, 'okänt') FROM person WHERE id = 4;
-- David | (tomt)
```

`''` är en tom text, men den **finns**. `COALESCE` ser inget `NULL` och ger tillbaka den tomma texten.

Vill du behandla tom text som saknad kan du kombinera med `NULLIF`, som gör om ett visst värde till `NULL`:

```sql
SELECT name, COALESCE(NULLIF(job, ''), 'okänt') FROM person WHERE id = 4;
-- David | okänt
```

`NULLIF(job, '')` betyder "om `job` är `''`, låtsas att det är `NULL`".

Kör [övningsdatan](ovningsdata.md) igen efteråt, så att David blir pilot igen.

## Olika databaser

| Databas | Med två värden | Med fler värden |
|---|---|---|
| Alla | `COALESCE(a, b)` | `COALESCE(a, b, c, ...)` |
| SQLite, MySQL | `IFNULL(a, b)` | |
| SQL Server | `ISNULL(a, b)` | |
| Oracle | `NVL(a, b)` | |

`COALESCE` är standard-SQL och fungerar överallt. De andra är varje databas egen variant, och de tar bara två värden. Välj `COALESCE`, så fungerar din SQL var du än kör den.

### Clean Code: COALESCE

> Så här gör du reservvärden tydliga.

```sql
-- ❌ Vad betyder 0 här? Ingen by, eller by nummer 0?
SELECT name, COALESCE(village_id, 0) FROM person;
```

```sql
-- ✅ Reservvärdet säger vad det betyder
SELECT p.name, COALESCE(v.name, 'ingen by') AS village
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

- **Välj ett reservvärde som inte kan förväxlas med riktig data.** `0` som id eller `''` som namn ser ut som något, men betyder inget.
- **Samma datatyp** som kolumnen: tal ersätts med tal, och text med text
- **`COALESCE(SUM(...), 0)`** när du vill se noll i stället för `NULL` i en rapport
- **Ge kolumnen ett namn med `AS`**, eftersom `COALESCE(...)` annars blir kolumnrubriken

## Vanliga misstag

| Misstag | Vad händer? | Gör så här i stället |
|---|---|---|
| `COALESCE(village_id, 'ingen')` | Blandar tal och text. SQLite tillåter det, men SQL Server ger ett fel. | Samma typ: `COALESCE(v.name, 'ingen by')` |
| Tro att `COALESCE` fångar tom text | `''` är inte `NULL`, och följer med | `COALESCE(NULLIF(kolumn, ''), 'reserv')` |
| `AVG(COALESCE(x, 0))` när du menar snittet av de som har ett värde | Raderna med `NULL` räknas som 0 och drar ned snittet | `AVG(x)`, som hoppar över `NULL` |
| `COALESCE(COUNT(...), 0)` | Fungerar, men behövs inte, eftersom `COUNT` redan ger 0 | Bara `COUNT(...)` |
| `ISNULL(a, b)` i SQLite | `ISNULL` betyder något annat, eller finns inte | `COALESCE(a, b)` |

## Sammanfattning

- `COALESCE(a, b, ...)` ger det **första värdet som inte är `NULL`**.
- Används för reservvärden i resultatet: `COALESCE(v.name, 'ingen by')`.
- `COALESCE(SUM(...), 0)` ger 0 i stället för `NULL` när det inte finns något att summera. `COUNT` behöver det inte.
- Det kan användas på hela uttryck, t.ex. text som annars hade blivit `NULL`.
- Tom text `''` är inte `NULL`. Använd `NULLIF` om du vill behandla den så.
- `COALESCE` fungerar i alla databaser. `IFNULL`, `ISNULL` och `NVL` är lokala varianter.

## Övningar

Använd [övningsdatan](ovningsdata.md).

### 🟢 Övning 1: Vilket värde?

Vad blir resultatet av de här tre? Svara innan du kör dem.

```sql
SELECT COALESCE(NULL, 'b', 'c');
SELECT COALESCE('a', NULL, 'c');
SELECT COALESCE(NULL, NULL, NULL);
```

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

- `'b'`, det första som inte är `NULL`
- `'a'`, som redan är det första och inte är `NULL`
- `NULL`, eftersom det inte finns något annat att välja

</details>

### 🟢 Övning 2: Var bor alla?

Visa alla personers namn och namnet på byn de bor i. Den som inte bor i någon by ska få texten `hemlös` i stället för `NULL`.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT p.name, COALESCE(v.name, 'hemlös') AS village
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

Det måste vara en `LEFT JOIN`. Med en vanlig `JOIN` hade David försvunnit helt, och då finns det inget `NULL` för `COALESCE` att ersätta.

</details>

### 🟡 Övning 3: Snittguld per by

Visa varje by och snittguldet för dess invånare. Alla tre byar ska vara med, och en by utan invånare ska visa `0`.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT v.name, COALESCE(AVG(p.gold), 0) AS snittguld
FROM village AS v
LEFT JOIN person AS p ON p.village_id = v.id
GROUP BY v.name;
```

| name | snittguld |
|---|---|
| Gurkby | 275 |
| Lökby | 100 |
| Morotsby | 0 |

`COALESCE` ligger **utanför** `AVG`. Det byter bara ut slutresultatet för Morotsby och påverkar inte snittet för de andra byarna.

</details>

### 🔴 Övning 4: En mening för alla

Skriv en fråga som ger en mening per person: `Anna bor i Lökby och har 120 guld.` Den som inte bor i någon by ska få `David bor ingenstans och har 500 guld.`

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT p.name
       || COALESCE(' bor i ' || v.name, ' bor ingenstans')
       || ' och har ' || p.gold || ' guld.' AS sentence
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id;
```

`' bor i ' || v.name` blir `NULL` för David, och då väljer `COALESCE` reservtexten. Resten av meningen sätts ihop som vanligt, eftersom `p.name` och `p.gold` aldrig är `NULL`.

Utan `COALESCE` hade Davids hela mening blivit `NULL`.

</details>

---

Föregående: [Subquery](subquery.md) · [Tillbaka till översikten](index.md) · Nästa: [INSERT](insert.md)

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*
