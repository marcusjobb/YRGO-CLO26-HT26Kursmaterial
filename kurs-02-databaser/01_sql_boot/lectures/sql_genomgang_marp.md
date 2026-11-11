---
marp: true
theme: default
paginate: true
title: Vad gjorde vi egentligen?
description: Visuell genomgång av DQL och DML, det ni använde i SQL Island och SQL Murder Mystery
style: |
  section {
    font-size: 26px;
  }
  section.lead {
    text-align: center;
    justify-content: center;
  }
  section.lead h1 {
    font-size: 64px;
  }
  table {
    font-size: 22px;
  }
  th {
    background: #2d3e50;
    color: #fff;
  }
  td strong {
    color: #d35400;
  }
  td:has(strong) {
    background: #fdebd0;
  }
  code {
    font-size: 0.9em;
  }
  blockquote {
    font-size: 22px;
    border-left: 6px solid #e67e22;
    color: #555;
  }
---

<!-- _class: lead -->
<!-- _paginate: false -->

# Vad gjorde vi egentligen?

Ni har rymt från **SQL Island** 🏝️ och löst mordet i **SQL City** 🕵️

Nu tittar vi på vad som hände under huven.
En tabell. Fem rader. Ett steg i taget.

---

## Ni har redan gjort det svåra

- Ni hittade en slaktare, samlade föremål och köpte ett svärd
- Ni förhörde vittnen, följde en bilplåt och hittade hjärnan bakom mordet

Allt det gjorde ni med **ett fåtal SQL-kommandon** i olika kombinationer.

Idag ger vi dem **namn** och tittar på **exakt vad som händer** med raderna när de körs.

> 🏝️ och 🕵️ på bilderna visar var ni använde kommandot i spelen.

---

## SQL:s fem familjer

| | Familj | Frågan den svarar på | Kommandon |
|---|---|---|---|
| 🔍 | **DQL** | *Vad finns?* | `SELECT` |
| ✏️ | **DML** | *Ändra innehållet* | `INSERT`, `UPDATE`, `DELETE` |
| 🏗️ | **DDL** | *Bygg strukturen* | `CREATE`, `ALTER`, `DROP` |
| 🔑 | **DCL** | *Vem får göra vad?* | `GRANT`, `REVOKE` |
| 🛟 | **TCL** | *Spara eller ångra?* | `BEGIN`, `COMMIT`, `ROLLBACK` |

I spelen använde ni **DQL** och **DML**. De andra tre fanns där hela tiden, bakom kulisserna.

---

## 🔍 DQL: Data Query Language

**Läser data. Ändrar ingenting.**

```sql
SELECT name, gold FROM person WHERE job = 'baker';
```

- Ett enda kommando, `SELECT`, men med många delar: `WHERE`, `ORDER BY`, `GROUP BY`, `JOIN`...
- Du kan köra det hur många gånger som helst. Inget går sönder.
- Det är det du skriver **mest** som utvecklare.

> 🏝️🕵️ Det mesta ni gjorde i båda spelen: leta, filtrera, räkna och koppla ihop.

> Vissa böcker räknar `SELECT` som en del av DML. Det är samma kommando, bara en annan indelning.

---

## ✏️ DML: Data Manipulation Language

**Ändrar innehållet, alltså raderna i tabellerna.**

```sql
INSERT INTO person (name, gold) VALUES ('Fia', 0);
UPDATE person SET gold = gold + 100 WHERE id = 6;
DELETE FROM person WHERE id = 6;
```

- Tabellerna finns kvar. Det är **raderna** som läggs till, ändras eller tas bort.
- Det finns ingen ångra-knapp, och `WHERE` avgör vilka rader som påverkas.
- **Tänk efter före.**

> 🏝️ Ni plockade upp föremål, bytte namn, fick ett svärd och besegrade Dirty Dieter.

---

## 🏗️ DDL: Data Definition Language

**Bygger själva strukturen: tabeller, kolumner och regler.**

```sql
CREATE TABLE village (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);
ALTER TABLE village ADD COLUMN chief INTEGER;
DROP TABLE village;
```

- DML ändrar **vad som står** i tabellen. DDL ändrar **hur tabellen ser ut**.
- `DROP TABLE` tar bort tabellen **och** all data i den.

> 🏝️🕵️ Någon skrev `CREATE TABLE inhabitant ...` innan ni kom till ön. Tabellerna fanns redan när ni började.

---

## 🔑 DCL: Data Control Language

**Bestämmer vem som får göra vad.**

```sql
GRANT SELECT ON person TO elev;
REVOKE DELETE ON person FROM elev;
```

- Användare och roller får **behörigheter**: läsa, ändra, ta bort...
- En rapportanvändare får kanske bara `SELECT`, medan en admin får allt.
- Principen heter **least privilege**: ge bara den behörighet som behövs.

> SQLite, som spelen använder, har ingen DCL. Där är det filen som styr åtkomsten. I SQL Server, PostgreSQL och MySQL är DCL en viktig del av säkerheten.

---

## 🛟 TCL: Transaction Control Language

**Grupperar ändringar så att de sparas eller ångras tillsammans.**

```sql
BEGIN TRANSACTION;
UPDATE account SET balance = balance - 100 WHERE id = 1;
UPDATE account SET balance = balance + 100 WHERE id = 2;
COMMIT;      -- spara båda
-- eller: ROLLBACK;   -- ångra båda
```

- **Allt eller inget.** Pengarna ska inte kunna försvinna halvvägs.
- `COMMIT` sparar och `ROLLBACK` ångrar allt sedan `BEGIN`.
- Det här är ångra-knappen som DML saknar.

> Vi återkommer till TCL i slutet, under "Den gyllene regeln".

---

## Vår tabell: `person`

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

- En **rad** = en person
- En **kolumn** = en egenskap
- `id` är **primärnyckel**: unik för varje rad
- `NULL` = *inget värde*. David bor inte i någon by.

> 🏝️ Samma idé som `inhabitant` i SQL Island, bara mindre.

---

<!-- _class: lead -->

# Del 1: DQL
## Att läsa data med `SELECT`

---

## `SELECT *`: visa allt

```sql
SELECT * FROM person;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

`*` betyder **alla kolumner**. Bra när du utforskar en ny tabell.

> 🏝️ Ert allra första kommando: `SELECT * FROM inhabitant;`

---

## Välja kolumner: före

```sql
SELECT name, gold FROM person;
```

| id | **name** | job | **gold** | village_id |
|---|---|---|---|---|
| 1 | **Anna** | baker | **120** | 1 |
| 2 | **Bertil** | smith | **300** | 2 |
| 3 | **Cissi** | baker | **80** | 1 |
| 4 | **David** | pilot | **500** | NULL |
| 5 | **Eva** | merchant | **250** | 2 |

Vi vill bara ha **name** och **gold**.

---

## Välja kolumner: efter

```sql
SELECT name, gold FROM person;
```

| name | gold |
|---|---|
| Anna | 120 |
| Bertil | 300 |
| Cissi | 80 |
| David | 500 |
| Eva | 250 |

Samma **rader**, färre **kolumner**.
`SELECT` väljer kolumner. Nu ska vi välja rader.

---

## `WHERE`: före

```sql
SELECT * FROM person
WHERE gold >= 250;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| **2** | **Bertil** | **smith** | **300** | **2** |
| 3 | Cissi | baker | 80 | 1 |
| **4** | **David** | **pilot** | **500** | **NULL** |
| **5** | **Eva** | **merchant** | **250** | **2** |

Fetstil = raden uppfyller villkoret.

---

## `WHERE`: efter

```sql
SELECT * FROM person
WHERE gold >= 250;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 2 | Bertil | smith | 300 | 2 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

5 rader in, **3 rader ut**.
`WHERE` är ett filter: varje rad testas, och bara de där villkoret är **sant** följer med.

> 🕵️ Brottsplatsrapporten: `WHERE date = 20180115 AND type = 'murder' AND city = 'SQL City'`

---

## Jämförelser i `WHERE`

| Operator | Betyder | Exempel |
|---|---|---|
| `=` | lika med | `job = 'baker'` |
| `<>` eller `!=` | inte lika med | `job <> 'baker'` |
| `>` `<` | större / mindre än | `gold > 100` |
| `>=` `<=` | större / mindre än eller lika med | `gold >= 250` |

**Text** skrivs inom **enkla** citattecken: `'baker'`
**Tal** skrivs utan: `250`

> `'Baker'` och `'baker'` är inte alltid samma sak. Skriv texten exakt som den står i tabellen.

---

## Text: `WHERE job = 'baker'`

```sql
SELECT * FROM person
WHERE job = 'baker';
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| **1** | **Anna** | **baker** | **120** | **1** |
| 2 | Bertil | smith | 300 | 2 |
| **3** | **Cissi** | **baker** | **80** | **1** |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

---

## Text: efter

```sql
SELECT * FROM person
WHERE job = 'baker';
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 3 | Cissi | baker | 80 | 1 |

> 🏝️ Hungrig på ön: `WHERE job = 'butcher'`. Sedan letade ni bagare att jobba hos.

---

## `AND`: båda måste stämma

```sql
SELECT * FROM person
WHERE job = 'baker' AND gold > 100;
```

| id | name | job | gold | baker? | gold > 100? |
|---|---|---|---|---|---|
| **1** | **Anna** | **baker** | **120** | ✅ | ✅ |
| 2 | Bertil | smith | 300 | ❌ | ✅ |
| 3 | Cissi | baker | 80 | ✅ | ❌ |
| 4 | David | pilot | 500 | ❌ | ✅ |
| 5 | Eva | merchant | 250 | ❌ | ✅ |

**Resultat:** bara Anna. Cissi är bagare men har för lite guld.

---

## `OR`: minst ett måste stämma

```sql
SELECT * FROM person
WHERE job = 'baker' OR job = 'pilot';
```

| id | name | job | gold | baker? | pilot? |
|---|---|---|---|---|---|
| **1** | **Anna** | **baker** | **120** | ✅ | ❌ |
| 2 | Bertil | smith | 300 | ❌ | ❌ |
| **3** | **Cissi** | **baker** | **80** | ✅ | ❌ |
| **4** | **David** | **pilot** | **500** | ❌ | ✅ |
| 5 | Eva | merchant | 250 | ❌ | ❌ |

**Resultat:** Anna, Cissi, David

---

## `IN`: kortare `OR`

```sql
SELECT * FROM person
WHERE job IN ('baker', 'pilot');
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |

Exakt samma resultat som förra bilden.
`IN (...)` = "något av de här värdena".

> 🏝️ En vänlig handlare eller köpman: `job IN ('dealer', 'merchant')`

---

## ⚠️ Fällan: `AND` före `OR`

**Mål:** bagare eller piloter med **mer än 100 guld**.

```sql
WHERE job = 'baker' OR job = 'pilot' AND gold > 100   -- ❌
```

SQL läser `AND` först, som gånger före plus i matte:

```sql
WHERE job = 'baker' OR (job = 'pilot' AND gold > 100)
```

| name | job | gold | |
|---|---|---|---|
| Anna | baker | 120 | |
| **Cissi** | **baker** | **80** | 😱 ska inte vara med |
| David | pilot | 500 | |

---

## ✅ Lösningen: parenteser

```sql
SELECT * FROM person
WHERE (job = 'baker' OR job = 'pilot') AND gold > 100;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 4 | David | pilot | 500 | NULL |

> **Vana:** blandar du `AND` och `OR`, sätt alltid parenteser, även när du tror att det inte behövs.

> 🏝️ Många lösningar på nätet glömmer parenteserna i frågan om handlare och köpmän. I spelet råkar det ge rätt svar ändå. Gjorde du likadant?

---

## `LIKE`: när du bara vet en del

```sql
SELECT * FROM person
WHERE name LIKE '%a';
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| **1** | **Anna** | **baker** | **120** | **1** |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| **5** | **Eva** | **merchant** | **250** | **2** |

`%a` = vad som helst, sedan **a** på slutet

---

## `LIKE`: jokertecken

| Mönster | Betyder | Matchar |
|---|---|---|
| `'A%'` | börjar med A | Anna |
| `'%a'` | slutar med a | Anna, Eva |
| `'%i%'` | innehåller i | Bertil, Cissi, David |
| `'_va'` | ett valfritt tecken + va | Eva |

- `%` = **noll eller fler** tecken
- `_` = **exakt ett** tecken
- Med jokertecken skriver du `LIKE`, **inte** `=`

> 🏝️ Alla smeder: `LIKE '%smith'` · 🕵️ Plåten *innehöll* H42W: `LIKE '%H42W%'`

---

## `BETWEEN`: ett intervall

```sql
SELECT * FROM person
WHERE gold BETWEEN 100 AND 300;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| **1** | **Anna** | **baker** | **120** | **1** |
| **2** | **Bertil** | **smith** | **300** | **2** |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| **5** | **Eva** | **merchant** | **250** | **2** |

Samma som `gold >= 100 AND gold <= 300`. **Gränserna räknas med**, så Bertil med 300 är med.

> 🕵️ Mellan 5'5" och 5'7": `height BETWEEN 65 AND 67`

---

## `NULL`: inget värde

```sql
SELECT * FROM person
WHERE village_id IS NULL;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 4 | David | pilot | 500 | NULL |

Och tvärtom: `WHERE village_id IS NOT NULL` ger de fyra andra.

> 🏝️ Föremål utan ägare: `SELECT * FROM item WHERE owner IS NULL;`

---

## ⚠️ Fällan: `= NULL`

```sql
SELECT * FROM person
WHERE village_id = NULL;   -- ❌
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| | | | | |

**0 rader.** Inget fel, bara tomt.

`NULL` betyder *okänt*. Är okänt lika med okänt? Svaret är **okänt**, inte sant.
Därför: **`IS NULL`**, aldrig `= NULL`.

---

## `ORDER BY`: sortera

```sql
SELECT name, gold FROM person
ORDER BY gold DESC;
```

| Före: name | gold | | Efter: name | gold |
|---|---|---|---|---|
| Anna | 120 | ➡️ | **David** | **500** |
| Bertil | 300 | | Bertil | 300 |
| Cissi | 80 | | Eva | 250 |
| David | 500 | | Anna | 120 |
| Eva | 250 | | Cissi | 80 |

`ASC` = stigande (standard) · `DESC` = fallande
Utan `ORDER BY` har resultatet **ingen garanterad ordning**.

---

## `LIMIT`: bara de första

```sql
SELECT name, gold FROM person
ORDER BY gold DESC
LIMIT 3;
```

| name | gold |
|---|---|
| David | 500 |
| Bertil | 300 |
| Eva | 250 |

"De tre rikaste".

> 🏝️ Den rikaste bagaren: `ORDER BY gold DESC`
> 🕵️ Sista huset på gatan: `ORDER BY address_number DESC LIMIT 1`

> SQL Server skriver istället `SELECT TOP 3 name, gold FROM ...`

---

## `DISTINCT`: ta bort dubbletter

```sql
SELECT DISTINCT job FROM person;
```

| Före: job | | Efter: job |
|---|---|---|
| baker | ➡️ | baker |
| smith | | smith |
| ~~baker~~ | | pilot |
| pilot | | merchant |
| merchant | | |

Perfekt för att se **vilka värden som finns** i en kolumn.

> 🕵️ Walkthroughen: `SELECT DISTINCT type FROM crime_scene_report;`

---

## `AS`: döp om en kolumn i resultatet

```sql
SELECT name AS namn, gold AS guld
FROM person
WHERE job = 'baker';
```

| namn | guld |
|---|---|
| Anna | 120 |
| Cissi | 80 |

Tabellen ändras **inte**. Bara rubriken i resultatet byter namn.

---

## Aggregatfunktioner: många rader blir ett värde

```sql
SELECT COUNT(*), SUM(gold), AVG(gold), MIN(gold), MAX(gold)
FROM person;
```

| gold |
|---|
| 120 |
| 300 |
| 80 |
| 500 |
| 250 |

⬇️

| COUNT(\*) | SUM(gold) | AVG(gold) | MIN(gold) | MAX(gold) |
|---|---|---|---|---|
| 5 | 1250 | 250 | 80 | 500 |

---

## Aggregat + `WHERE`

```sql
SELECT COUNT(*) FROM person
WHERE job = 'baker';
```

| id | name | job |
|---|---|---|
| **1** | **Anna** | **baker** |
| **3** | **Cissi** | **baker** |

⬇️

| COUNT(\*) |
|---|
| 2 |

`WHERE` filtrerar **först**, sedan räknar `COUNT`.

> 🏝️ "Hur många kvinnor bor i Onionville?"

---

## `GROUP BY`: före

```sql
SELECT job, COUNT(*), SUM(gold)
FROM person
GROUP BY job;
```

Tänk dig att raderna läggs i **högar**, en hög per yrke:

| hög | name | job | gold |
|---|---|---|---|
| 🟦 | Anna | baker | 120 |
| 🟦 | Cissi | baker | 80 |
| 🟩 | Bertil | smith | 300 |
| 🟨 | David | pilot | 500 |
| 🟥 | Eva | merchant | 250 |

---

## `GROUP BY`: efter

```sql
SELECT job, COUNT(*), SUM(gold)
FROM person
GROUP BY job;
```

| | job | COUNT(\*) | SUM(gold) |
|---|---|---|---|
| 🟦 | baker | 2 | 200 |
| 🟩 | smith | 1 | 300 |
| 🟨 | pilot | 1 | 500 |
| 🟥 | merchant | 1 | 250 |

En rad **per hög**. Aggregaten räknas inom varje hög.

> Du kan inte välja `name` här. Vilket namn skulle högen 🟦 visa, Anna eller Cissi?

> 🏝️ Snittguld per yrke. Det var så ni såg att slaktarna var rikast.

---

## `HAVING`: filtrera högarna

```sql
SELECT job, COUNT(*)
FROM person
GROUP BY job
HAVING COUNT(*) > 1;
```

| job | COUNT(\*) | > 1? |
|---|---|---|
| **baker** | **2** | ✅ |
| smith | 1 | ❌ |
| pilot | 1 | ❌ |
| merchant | 1 | ❌ |

**Resultat:** `baker | 2`

---

## `WHERE` eller `HAVING`?

| | `WHERE` | `HAVING` |
|---|---|---|
| Filtrerar | **rader** | **grupper** |
| När | **före** `GROUP BY` | **efter** `GROUP BY` |
| Kan använda `COUNT`, `SUM`... | ❌ | ✅ |

```sql
SELECT job, SUM(gold)
FROM person
WHERE gold < 400          -- 1. bort med David (500) innan högarna läggs
GROUP BY job
HAVING SUM(gold) > 200;   -- 2. bort med baker (200) efteråt
```

**Resultat:** `merchant | 250` och `smith | 300`

> 🕵️ "Gick på konserten **3 gånger**" → `HAVING COUNT(*) = 3`

---

<!-- _class: lead -->

# Två tabeller
## `JOIN`

---

## En tabell till: `village`

| person.id | name | village_id | | village.id | name |
|---|---|---|---|---|---|
| 1 | Anna | **1** | | **1** | Lökby |
| 2 | Bertil | **2** | | **2** | Gurkby |
| 3 | Cissi | **1** | | 3 | Morotsby |
| 4 | David | NULL | | | |
| 5 | Eva | **2** | | | |

- `person.village_id` **pekar på** `village.id`
- Det kallas en **främmande nyckel** (foreign key)
- Ingen bor i Morotsby. David bor ingenstans.

---

## `JOIN`: koppla ihop

```sql
SELECT person.name, village.name
FROM person
JOIN village ON person.village_id = village.id;
```

| person.name | village.name |
|---|---|
| Anna | Lökby |
| Bertil | Gurkby |
| Cissi | Lökby |
| Eva | Gurkby |

`ON` säger **hur** raderna hör ihop.

> 🏝️ Vilken by bor Dirty Dieter i? Byns namn fanns i `village`, Dieter fanns i `inhabitant`.

---

## Vart tog David vägen? 🕵️

| person.name | village_id | finns i village? |
|---|---|---|
| Anna | 1 | ✅ Lökby |
| Bertil | 2 | ✅ Gurkby |
| Cissi | 1 | ✅ Lökby |
| **David** | **NULL** | ❌ |
| Eva | 2 | ✅ Gurkby |

En vanlig `JOIN` (**INNER JOIN**) tar bara med rader som har en **match i båda** tabellerna.
Morotsby saknas också, eftersom ingen bor där.

> 🕵️ **En JOIN är också ett filter.** Joe Germuska hade rätt gymkort men inget körkort. Därför försvann han i JOIN mot `drivers_license`.

---

## `LEFT JOIN`: behåll alla till vänster

```sql
SELECT person.name, village.name
FROM person
LEFT JOIN village ON person.village_id = village.id;
```

| person.name | village.name |
|---|---|
| Anna | Lökby |
| Bertil | Gurkby |
| Cissi | Lökby |
| **David** | **NULL** |
| Eva | Gurkby |

Alla rader från tabellen **till vänster** (`person`) följer med. Saknas en match blir det `NULL`.

---

## Alias för tabeller

```sql
SELECT p.name, v.name AS village
FROM person AS p
JOIN village AS v ON p.village_id = v.id
WHERE v.name = 'Lökby';
```

| name | village |
|---|---|
| Anna | Lökby |
| Cissi | Lökby |

- `p` och `v` är **smeknamn** för tabellerna
- Behövs när två tabeller har en kolumn med samma namn (här `name`)
- Gör långa JOIN-kedjor läsbara

> 🕵️ Mördaren: gym → incheckning → person → körkort, fyra tabeller i en kedja

---

## JOIN + aggregat + GROUP BY

```sql
SELECT v.name, COUNT(*) AS invanare, SUM(p.gold) AS guld
FROM person AS p
JOIN village AS v ON p.village_id = v.id
GROUP BY v.name;
```

| name | invanare | guld |
|---|---|---|
| Gurkby | 2 | 550 |
| Lökby | 2 | 200 |

> 🏝️ "Hur mycket guld finns i Cucumbertown?" Svaret var 8980.

---

## Subquery: en fråga inuti en fråga

**Vilka har mer guld än snittet?**

```sql
SELECT name, gold FROM person
WHERE gold > (SELECT AVG(gold) FROM person);
```

1. Den inre frågan körs först: `AVG(gold)` = **250**
2. Sedan blir det `WHERE gold > 250`

| name | gold |
|---|---|
| Bertil | 300 |
| David | 500 |

---

## I vilken ordning läser SQL frågan?

Du **skriver** i den här ordningen... men databasen **kör** i den här:

| Skriv | Kör |
|---|---|
| 1. `SELECT` | 5. |
| 2. `FROM` / `JOIN` | **1.** hämta och koppla tabeller |
| 3. `WHERE` | **2.** filtrera rader |
| 4. `GROUP BY` | **3.** lägg i högar |
| 5. `HAVING` | **4.** filtrera högar |
| 6. `ORDER BY` | **6.** sortera |
| 7. `LIMIT` | **7.** klipp |

Därför kan `WHERE` inte använda `COUNT(*)`, eftersom högarna inte finns än.

---

<!-- _class: lead -->

# Del 2: DML
## Att ändra data

⚠️ Härifrån ändras tabellen på riktigt

---

## `INSERT`: före

```sql
INSERT INTO person (name, job, gold, village_id)
VALUES ('Främling', NULL, 0, 1);
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

---

## `INSERT`: efter

```sql
INSERT INTO person (name, job, gold, village_id)
VALUES ('Främling', NULL, 0, 1);
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |
| **6** | **Främling** | **NULL** | **0** | **1** |

Vi skrev aldrig `id`. Databasen **räknar upp det själv**.

> 🏝️ Så registrerades ni som `'Stranger'` och fick `personid` 20.

---

## `UPDATE`: före

```sql
UPDATE person
SET gold = gold + 100
WHERE id = 6;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| ... | | | | |
| **6** | **Främling** | **NULL** | **0** | **1** |

- `SET` = **vad** ska ändras
- `WHERE` = **vilka rader**

---

## `UPDATE`: efter

```sql
UPDATE person
SET gold = gold + 100
WHERE id = 6;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| ... | | | | |
| 6 | Främling | NULL | **100** | 1 |

`gold = gold + 100` läser det **gamla** värdet och skriver tillbaka det **nya**.
`gold = 100` hade skrivit över, oavsett vad som stod där.

> 🏝️ Betalt för ringen och tekannan: `SET gold = gold + 120`

---

## Flera kolumner på en gång

```sql
UPDATE person
SET name = 'Fia', job = 'baker'
WHERE id = 6;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 6 | **Fia** | **baker** | 100 | 1 |

> **Filtrera på `id` när du ändrar.** Ett namn kan finnas flera gånger, men ett `id` är unikt.

> 🏝️ Ni bytte namn från Stranger: `WHERE personid = 20`

---

## ⚠️ `UPDATE` utan `WHERE`

```sql
UPDATE person
SET gold = 0;   -- glömde WHERE...
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | **0** | 1 |
| 2 | Bertil | smith | **0** | 2 |
| 3 | Cissi | baker | **0** | 1 |
| 4 | David | pilot | **0** | NULL |
| 5 | Eva | merchant | **0** | 2 |
| 6 | Fia | baker | **0** | 1 |

**Alla** blev fattiga. Ingen varning, ingen ångra-knapp. 💸

---

## `DELETE`: före

```sql
DELETE FROM person
WHERE id = 6;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |
| ~~6~~ | ~~Fia~~ | ~~baker~~ | ~~100~~ | ~~1~~ |

---

## `DELETE`: efter

```sql
DELETE FROM person
WHERE id = 6;
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

`DELETE` tar bort **hela rader**.

> 🏝️ Dirty Dieter och Dirty Diane: `DELETE FROM inhabitant WHERE name = 'Dirty Dieter'`

---

## ⚠️ `DELETE` utan `WHERE`

```sql
DELETE FROM person;   -- glömde WHERE...
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| | | | | |

Tabellen finns kvar, men den är **tom**. 🫠

---

## Den gyllene regeln

**1. Skriv `SELECT` med ditt `WHERE` först**

```sql
SELECT * FROM person WHERE id = 6;
```

**2. Ser raderna rätt ut? Byt då bara början**

```sql
DELETE FROM person WHERE id = 6;
```

Samma `WHERE` → samma rader. Du vet vad du ändrar **innan** du ändrar det.

> 🛟 **TCL** som skyddsnät: `BEGIN` → din ändring → `SELECT` för att kontrollera → `COMMIT` om det blev rätt, `ROLLBACK` om det blev fel.

---

## Bonus 🕵️: hur visste spelet att ni hade rätt?

```sql
INSERT INTO solution VALUES (1, 'Jeremy Bowers');
SELECT value FROM solution;
```

- Ni lämnade svaret med en **`INSERT`**, alltså DML
- Tabellen `solution` har en **trigger**: SQL som körs automatiskt vid varje `INSERT`
- Triggern jämför svaret med facit och skriver tillbaka ett meddelande

Facit stod aldrig i klartext i databasen. Det var sparat som `hex('Jeremy Bowers')`.

---

<!-- _class: lead -->

# Sammanfattning

---

## Det ni gjorde → vad det heter

| I spelet | Kommando |
|---|---|
| 🏝️ Tittade runt på ön | `SELECT *` |
| 🏝️ Letade slaktare, smeder och piloter | `WHERE`, `LIKE`, `IN` |
| 🏝️ Plockade upp föremål, sålde och bytte namn | `UPDATE` |
| 🏝️ Räknade invånare och guld per by | `JOIN`, `COUNT`, `SUM`, `GROUP BY` |
| 🏝️ Besegrade Dieter och Diane | `DELETE` |
| 🕵️ Hittade sista huset på gatan | `ORDER BY ... DESC LIMIT 1` |
| 🕵️ Följde gymkort → person → körkort | flera `JOIN` i kedja |
| 🕵️ Tre konsertbesök i december | `BETWEEN`, `GROUP BY`, `HAVING` |
| 🕵️ Lämnade svaret | `INSERT` + trigger |

---

## Fusklapp: DQL (1/2), hitta rader

| Vill du... | Skriv |
|---|---|
| se allt | `SELECT * FROM t` |
| välja kolumner | `SELECT a, b FROM t` |
| filtrera rader | `WHERE villkor` |
| kombinera villkor | `AND`, `OR`, `(...)` |
| matcha en lista | `IN ('x', 'y')` |
| matcha en del av text | `LIKE '%x%'` |
| intervall | `BETWEEN 1 AND 10` |
| saknat värde | `IS NULL` / `IS NOT NULL` |

---

## Fusklapp: DQL (2/2), forma resultatet

| Vill du... | Skriv |
|---|---|
| sortera | `ORDER BY a DESC` |
| begränsa | `LIMIT n` |
| unika värden | `SELECT DISTINCT a` |
| döpa om en kolumn | `SELECT a AS namn` |
| räkna, summera... | `COUNT`, `SUM`, `AVG`, `MIN`, `MAX` |
| per grupp | `GROUP BY a` + `HAVING` |
| två tabeller | `JOIN t2 ON t1.x = t2.y` |
| fråga i en fråga | `WHERE a > (SELECT ...)` |

---

## Fusklapp: DML

| Vill du... | Skriv |
|---|---|
| lägga till en rad | `INSERT INTO t (a, b) VALUES (1, 'x')` |
| ändra rader | `UPDATE t SET a = 1 WHERE id = 5` |
| ta bort rader | `DELETE FROM t WHERE id = 5` |

**Tre vanor:**

1. `SELECT` med samma `WHERE` **innan** `UPDATE`/`DELETE`
2. Filtrera på **`id`**, inte på namn, när du ändrar
3. **Parenteser** när du blandar `AND` och `OR`

---

## Olika databaser, små skillnader

| | SQLite (Island, Murder Mystery) | SQL Server |
|---|---|---|
| De första n raderna | `LIMIT 3` | `SELECT TOP 3 ...` |
| Text | `'enkla citattecken'` | `'enkla citattecken'` |
| `LIKE` och stora/små bokstäver | skiljer **inte** (för a–z) | beror på *collation* |
| Räkna upp id | `INTEGER PRIMARY KEY` | `IDENTITY(1,1)` |

Grunderna är desamma överallt. Det är detaljerna som skiljer.

---

<!-- _class: lead -->

# Ni kan redan det här 🏝️🕵️

Ni rymde från en ö och löste ett mord med SQL.
Allt på de här bilderna har ni **redan gjort**.

Nu vet ni också vad det heter och varför det fungerar.
