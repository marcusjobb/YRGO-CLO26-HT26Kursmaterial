# SQL-genomgång: vad gjorde vi egentligen?

🟢

Du har rymt från en ö och löst ett mord, båda gångerna med SQL.

I **SQL Island** hittade du en slaktare, samlade föremål, köpte ett svärd och besegrade Dirty Dieter. I **SQL Murder Mystery** förhörde du vittnen, följde en bilplåt genom fyra tabeller och hittade hjärnan bakom mordet.

Det gjorde du med ett fåtal kommandon i olika kombinationer. Nu ska vi ge dem namn och titta på exakt vad som händer med raderna när de körs.

Hängde du med i spelen? Då kan du redan det mesta här. Den här texten hjälper dig att förstå *varför* det fungerade.

## TL;DR

- SQL delas in i fem familjer: **DQL** (läsa), **DML** (ändra), **DDL** (bygga), **DCL** (behörigheter) och **TCL** (spara eller ångra)
- `SELECT` väljer **kolumner** och `WHERE` väljer **rader**
- `NULL` jämförs med `IS NULL`, aldrig med `= NULL`
- `AND` binder hårdare än `OR`, så sätt parenteser
- `GROUP BY` lägger rader i högar och `HAVING` filtrerar högarna
- En vanlig `JOIN` är också ett **filter**: rader utan match försvinner
- Kör alltid `SELECT` med samma `WHERE` innan du kör `UPDATE` eller `DELETE`

## Innehållsförteckning

- [SQL:s fem familjer](#sqls-fem-familjer)
- [Övningsdata](#övningsdata)
- [DQL: läsa data](#dql-läsa-data)
- [Två tabeller: JOIN](#två-tabeller-join)
- [I vilken ordning kör databasen frågan?](#i-vilken-ordning-kör-databasen-frågan)
- [DML: ändra data](#dml-ändra-data)
- [Vanliga misstag](#vanliga-misstag)
- [Sammanfattning](#sammanfattning)
- [Övningar](#övningar)

---

## SQL:s fem familjer

| | Familj | Frågan den svarar på | Kommandon |
|---|---|---|---|
| 🔍 | **DQL**, Data Query Language | *Vad finns?* | `SELECT` |
| ✏️ | **DML**, Data Manipulation Language | *Ändra innehållet* | `INSERT`, `UPDATE`, `DELETE` |
| 🏗️ | **DDL**, Data Definition Language | *Bygg strukturen* | `CREATE`, `ALTER`, `DROP` |
| 🔑 | **DCL**, Data Control Language | *Vem får göra vad?* | `GRANT`, `REVOKE` |
| 🛟 | **TCL**, Transaction Control Language | *Spara eller ångra?* | `BEGIN`, `COMMIT`, `ROLLBACK` |

I spelen använde du **DQL** och **DML**. De andra tre fanns där hela tiden, bakom kulisserna.

![SQL:s fem familjer: DQL, DML, DDL, DCL och TCL](diagrams/sql_fem_familjer.png)

### 🔍 DQL: läser, ändrar ingenting

```sql
SELECT name, gold FROM person WHERE job = 'baker';
```

`SELECT` är ett enda kommando, men med många delar: `WHERE`, `ORDER BY`, `GROUP BY`, `JOIN`... Du kan köra det hur många gånger som helst utan att något går sönder. Det är också det du kommer att skriva **mest** som utvecklare.

Vissa böcker räknar `SELECT` som en del av DML. Det är samma kommando, bara en annan indelning.

### ✏️ DML: ändrar raderna

```sql
INSERT INTO person (name, gold) VALUES ('Fia', 0);
UPDATE person SET gold = gold + 100 WHERE id = 6;
DELETE FROM person WHERE id = 6;
```

Tabellerna finns kvar. Det är **raderna** som läggs till, ändras eller tas bort. Det finns ingen ångra-knapp, och det är `WHERE` som avgör vilka rader som påverkas.

### 🏗️ DDL: bygger strukturen

```sql
CREATE TABLE village (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

ALTER TABLE village ADD COLUMN chief INTEGER;

DROP TABLE village;
```

DML ändrar **vad som står** i tabellen. DDL ändrar **hur tabellen ser ut**. Och `DROP TABLE` tar bort tabellen *och* all data i den.

I spelen hade någon redan skrivit `CREATE TABLE inhabitant ...` innan du kom till ön.

### 🔑 DCL: vem får göra vad?

```sql
GRANT SELECT ON person TO elev;
REVOKE DELETE ON person FROM elev;
```

Användare och roller får **behörigheter**. En rapportanvändare får kanske bara läsa, medan en admin får göra allt. Principen heter **least privilege**: ge bara den behörighet som faktiskt behövs.

SQLite har ingen DCL. Där är det filen på disken som styr åtkomsten. I SQL Server, PostgreSQL och MySQL är DCL en viktig del av säkerheten.

### 🛟 TCL: allt eller inget

```sql
BEGIN TRANSACTION;
UPDATE account SET balance = balance - 100 WHERE id = 1;
UPDATE account SET balance = balance + 100 WHERE id = 2;
COMMIT;
```

Tänk dig att strömmen går mellan de två `UPDATE`. Hundra kronor har lämnat konto 1 men aldrig kommit fram till konto 2.

En transaktion gör att **båda** ändringarna sparas, eller **ingen** av dem. `COMMIT` sparar och `ROLLBACK` ångrar allt sedan `BEGIN`.

Det här är ångra-knappen som DML saknar. Vi återkommer till den när vi pratar om `DELETE`.

---

## Övningsdata

All SQL i den här texten körs mot två små tabeller. Kör skriptet i DB Browser for SQLite (fliken *Execute SQL*) så kan du testa allt själv.

```sql
CREATE TABLE village (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE person (
    id         INTEGER PRIMARY KEY,
    name       TEXT NOT NULL,
    job        TEXT,
    gold       INTEGER NOT NULL DEFAULT 0,
    village_id INTEGER REFERENCES village(id)
);

INSERT INTO village (id, name) VALUES
    (1, 'Lökby'),
    (2, 'Gurkby'),
    (3, 'Morotsby');

INSERT INTO person (name, job, gold, village_id) VALUES
    ('Anna',   'baker',    120, 1),
    ('Bertil', 'smith',    300, 2),
    ('Cissi',  'baker',     80, 1),
    ('David',  'pilot',    500, NULL),
    ('Eva',    'merchant', 250, 2);
```

Så här ser `person` ut:

| id | name | job | gold | village_id |
|---|---|---|---|---|
| 1 | Anna | baker | 120 | 1 |
| 2 | Bertil | smith | 300 | 2 |
| 3 | Cissi | baker | 80 | 1 |
| 4 | David | pilot | 500 | NULL |
| 5 | Eva | merchant | 250 | 2 |

En **rad** är en person och en **kolumn** är en egenskap. `id` är **primärnyckeln**, alltså unik för varje rad. `NULL` betyder *inget värde*: David bor inte i någon by.

Det är samma idé som `inhabitant` i SQL Island, bara mindre.

---

## DQL: läsa data

### `SELECT`: vilka kolumner?

```sql
SELECT * FROM person;
```

`*` betyder alla kolumner. Det var ditt allra första kommando i SQL Island, och det är rätt första steg när du utforskar en okänd tabell.

Vill du bara se vissa kolumner räknar du upp dem:

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

![SELECT name, gold: kolumnerna name och gold är markerade i tabellen](diagrams/sql_select_kolumner.png)

Samma **rader**, färre **kolumner**. `SELECT` väljer kolumner. Nu ska vi välja rader.

### `WHERE`: vilka rader?

Tänk dig `WHERE` som en **dörrvakt**. Varje rad kommer fram till dörren en i taget. Dörrvakten tittar på villkoret och svarar ja eller nej. Bara de rader som får ett ja kommer in i resultatet.

```sql
SELECT * FROM person
WHERE gold >= 250;
```

Dörrvakten frågar varje rad: "Har du minst 250 guld?"

| id | name | gold | Får komma in? |
|---|---|---|---|
| 1 | Anna | 120 | ❌ |
| 2 | Bertil | 300 | ✅ |
| 3 | Cissi | 80 | ❌ |
| 4 | David | 500 | ✅ |
| 5 | Eva | 250 | ✅ |

![WHERE gold >= 250: Bertil, David och Eva är markerade](diagrams/sql_where_fore.png)

Fem rader in, **tre rader ut**: Bertil, David och Eva.

![Resultatet av WHERE gold >= 250: tre rader](diagrams/sql_where_efter.png)

Det var så du hittade brottsplatsrapporten i Murder Mystery:

```sql
WHERE date = 20180115 AND type = 'murder' AND city = 'SQL City'
```

### Jämförelser

| Operator | Betyder | Exempel |
|---|---|---|
| `=` | lika med | `job = 'baker'` |
| `<>` eller `!=` | inte lika med | `job <> 'baker'` |
| `>` `<` | större / mindre än | `gold > 100` |
| `>=` `<=` | större / mindre än eller lika med | `gold >= 250` |

**Text** skrivs inom **enkla** citattecken (`'baker'`), och **tal** skrivs utan (`250`). Skriv texten exakt som den står i tabellen. `'Baker'` och `'baker'` är inte alltid samma sak.

### `AND`, `OR` och `IN`

Med `AND` måste **båda** villkoren vara sanna:

```sql
SELECT * FROM person
WHERE job = 'baker' AND gold > 100;
```

Resultatet är bara Anna. Cissi är bagare, men har för lite guld.

![AND: bara Anna uppfyller båda villkoren](diagrams/sql_and.png)

Med `OR` räcker det att **ett** av villkoren är sant:

```sql
SELECT * FROM person
WHERE job = 'baker' OR job = 'pilot';
```

![OR: Anna, Cissi och David uppfyller minst ett villkor](diagrams/sql_or.png)

Resultatet är Anna, Cissi och David.

`IN` är ett kortare sätt att skriva många `OR` på samma kolumn:

```sql
SELECT * FROM person
WHERE job IN ('baker', 'pilot');
```

Det är exakt samma resultat. Det var så du hittade en vänlig handlare eller köpman i SQL Island: `job IN ('dealer', 'merchant')`.

### ⚠️ Fällan: `AND` före `OR`

Säg att du vill ha bagare eller piloter som har **mer än 100 guld**:

```sql
-- ❌ Ser rätt ut, men är fel
WHERE job = 'baker' OR job = 'pilot' AND gold > 100
```

SQL läser `AND` före `OR`, precis som gånger kommer före plus i matte. Databasen läser alltså frågan så här:

```sql
WHERE job = 'baker' OR (job = 'pilot' AND gold > 100)
```

![Fällan: utan parenteser smiter Cissi med 80 guld in](diagrams/sql_fallan_and_or.png)

Alla bagare kommer med, oavsett guld, och därför smiter Cissi med sina 80 guld in. Lösningen är parenteser:

```sql
-- ✅ Säger exakt det du menar
WHERE (job = 'baker' OR job = 'pilot') AND gold > 100
```

Nu blir resultatet Anna och David.

Många lösningar till SQL Island på nätet glömmer just de här parenteserna i frågan om handlare och köpmän. I spelet råkar det ge rätt svar ändå. Gjorde du likadant? Då vet du nu varför det fungerade, och varför det inte alltid gör det.

### `LIKE`: när du bara vet en del

```sql
SELECT * FROM person
WHERE name LIKE '%a';
```

![LIKE '%a': Anna och Eva är markerade](diagrams/sql_like.png)

| Mönster | Betyder | Matchar |
|---|---|---|
| `'A%'` | börjar med A | Anna |
| `'%a'` | slutar med a | Anna, Eva |
| `'%i%'` | innehåller i | Bertil, Cissi, David |
| `'_va'` | ett valfritt tecken, sedan va | Eva |

- `%` betyder **noll eller fler** tecken
- `_` betyder **exakt ett** tecken
- Med jokertecken skriver du `LIKE`, inte `=`

Du använde båda varianterna i spelen. I SQL Island var det `LIKE '%smith'` för att hitta alla sorters smeder. I Murder Mystery *innehöll* bilplåten H42W, alltså `LIKE '%H42W%'`.

### `BETWEEN`: ett intervall

```sql
SELECT * FROM person
WHERE gold BETWEEN 100 AND 300;
```

![BETWEEN 100 AND 300: Anna, Bertil och Eva är markerade](diagrams/sql_between.png)

Det är samma sak som `gold >= 100 AND gold <= 300`. **Gränserna räknas med**, så Bertil med exakt 300 är med. Resultatet är Anna, Bertil och Eva.

I Murder Mystery var personen mellan 5'5" och 5'7": `height BETWEEN 65 AND 67`.

### `NULL`: inget värde

```sql
SELECT * FROM person
WHERE village_id IS NULL;
```

Det ger David. Och tvärtom: `IS NOT NULL` ger de fyra andra.

Men varför inte `= NULL`? Prova:

```sql
-- ❌ Ger 0 rader. Inget felmeddelande, bara tomt.
SELECT * FROM person
WHERE village_id = NULL;
```

![WHERE village_id = NULL ger 0 rader](diagrams/sql_null_fallan.png)

`NULL` betyder *okänt*. Är okänt lika med okänt? Svaret är **okänt**, inte sant. Och dörrvakten släpper bara in rader där svaret är *sant*.

I SQL Island letade du föremål utan ägare: `WHERE owner IS NULL`.

### `ORDER BY` och `LIMIT`

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

![ORDER BY gold DESC: före och efter sortering](diagrams/sql_order_by.png)

`ASC` betyder stigande ordning och är standard. `DESC` betyder fallande. Utan `ORDER BY` har resultatet **ingen garanterad ordning**, även om det ofta ser ut att ha det.

`LIMIT` klipper resultatet efter ett visst antal rader. I SQL Server skriver du i stället `SELECT TOP 3 name, gold FROM ...`.

I Murder Mystery bodde det första vittnet i "det sista huset på gatan". SQL har inget "sista", så du översatte det till `ORDER BY address_number DESC LIMIT 1`.

### `DISTINCT`: unika värden

```sql
SELECT DISTINCT job FROM person;
```

![DISTINCT job: dubbletten baker tas bort](diagrams/sql_distinct.png)

Det ger `baker`, `smith`, `pilot` och `merchant`, där den andra `baker` har plockats bort. Det här är perfekt för att se vilka värden som finns i en kolumn innan du skriver ditt `WHERE`.

### `AS`: döp om i resultatet

```sql
SELECT name AS namn, gold AS guld
FROM person;
```

Tabellen ändras inte. Det är bara rubrikerna i resultatet som byter namn.

### Aggregatfunktioner: många rader blir ett värde

```sql
SELECT COUNT(*), SUM(gold), AVG(gold), MIN(gold), MAX(gold)
FROM person;
```

| COUNT(\*) | SUM(gold) | AVG(gold) | MIN(gold) | MAX(gold) |
|---|---|---|---|---|
| 5 | 1250 | 250 | 80 | 500 |

![Fem guldvärden blir ett värde per aggregatfunktion](diagrams/sql_aggregat.png)

Fem rader blev en. Kombinerar du med `WHERE` filtreras raderna **först**, och sedan räknas de:

```sql
SELECT COUNT(*) FROM person
WHERE job = 'baker';   -- 2
```

I SQL Island var det "Hur många kvinnor bor i Onionville?"

### `GROUP BY`: lägg raderna i högar

Tänk dig att du sorterar tvätt. Alla vita plagg i en hög, alla svarta i en annan, alla färgade i en tredje. Sedan räknar du hur många plagg som ligger i varje hög.

Det är precis vad `GROUP BY` gör.

```sql
SELECT job, COUNT(*), SUM(gold)
FROM person
GROUP BY job;
```

![GROUP BY job: raderna läggs i färgade högar](diagrams/sql_group_by_fore.png)

Först läggs raderna i högar, en per yrke:

| hög | name | job | gold |
|---|---|---|---|
| 🟦 | Anna | baker | 120 |
| 🟦 | Cissi | baker | 80 |
| 🟩 | Bertil | smith | 300 |
| 🟨 | David | pilot | 500 |
| 🟥 | Eva | merchant | 250 |

Sedan blir varje hög **en rad** i resultatet:

| job | COUNT(\*) | SUM(gold) |
|---|---|---|
| baker | 2 | 200 |
| merchant | 1 | 250 |
| pilot | 1 | 500 |
| smith | 1 | 300 |

![GROUP BY job: en rad per hög](diagrams/sql_group_by_efter.png)

Varför kan du inte lägga till `name` i `SELECT` här? Tänk på tvätthögen. Om du frågar "vilket plagg är den vita högen?" finns inget svar. Högen är många plagg. På samma sätt vet databasen inte om högen 🟦 ska visa Anna eller Cissi.

**Regeln:** i en grupperad fråga får du bara välja kolumner som du grupperar på, eller aggregat.

Det var så du såg att slaktarna var rikast på SQL Island: snittguld per yrke.

### `HAVING`: filtrera högarna

```sql
SELECT job, COUNT(*)
FROM person
GROUP BY job
HAVING COUNT(*) > 1;
```

Resultatet är `baker | 2`. Det är den enda högen med mer än en person.

![HAVING COUNT(*) > 1: bara högen baker är kvar](diagrams/sql_having.png)

`WHERE` och `HAVING` ser ut att göra samma sak, men de jobbar vid olika tillfällen:

| | `WHERE` | `HAVING` |
|---|---|---|
| Filtrerar | **rader** | **högar** |
| När | **före** `GROUP BY` | **efter** `GROUP BY` |
| Kan använda `COUNT`, `SUM`... | ❌ | ✅ |

Här är ett exempel där båda behövs:

```sql
SELECT job, SUM(gold)
FROM person
WHERE gold < 400          -- 1. David (500) kommer aldrig in i någon hög
GROUP BY job
HAVING SUM(gold) > 200;   -- 2. Bagarnas hög (200) plockas bort efteråt
```

Resultatet är `merchant | 250` och `smith | 300`.

Murder Mystery krävde just `HAVING`: personen hade gått på konserten **exakt tre gånger**, alltså `HAVING COUNT(*) = 3`.

### Subquery: en fråga inuti en fråga

Vilka har mer guld än snittet?

```sql
SELECT name, gold FROM person
WHERE gold > (SELECT AVG(gold) FROM person);
```

Den inre frågan körs först och ger 250. Sedan blir det i praktiken `WHERE gold > 250`, och resultatet är Bertil och David.

---

## Två tabeller: `JOIN`

### Främmande nycklar

| id | name | village_id |
|---|---|---|
| 1 | Anna | **1** |
| 2 | Bertil | **2** |
| 3 | Cissi | **1** |
| 4 | David | NULL |
| 5 | Eva | **2** |

| id | name |
|---|---|
| **1** | Lökby |
| **2** | Gurkby |
| 3 | Morotsby |

![person.village_id pekar på village.id](diagrams/sql_village_tabell.png)

`person.village_id` **pekar på** `village.id`. Det kallas en **främmande nyckel** (foreign key). Ingen bor i Morotsby, och David bor ingenstans.

### `JOIN ... ON`

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

I SQL Island ville du veta vilken by Dirty Dieter bodde i. Byns namn fanns i `village` och Dieter fanns i `inhabitant`, och `villageid` var bron mellan dem.

**Gammal stil:**

```sql
-- Kommatecken i FROM, kopplingen i WHERE
SELECT person.name, village.name
FROM person, village
WHERE person.village_id = village.id;
```

**Modern stil:**

```sql
-- Kopplingen i ON, filtren i WHERE
SELECT person.name, village.name
FROM person
JOIN village ON person.village_id = village.id;
```

Båda ger exakt samma resultat. SQL Island visade den gamla stilen, och du kommer att stöta på den i äldre kod.

Den moderna stilen håller isär *hur tabellerna hör ihop* och *vilka rader du vill ha*. Glömmer du kopplingsvillkoret i den gamla stilen får du **alla kombinationer** av rader, en så kallad kartesisk produkt. Fem personer gånger tre byar blir femton rader nonsens.

### 💡 Vart tog David vägen?

Titta på resultatet igen. David är borta. Morotsby också.

En vanlig `JOIN` (egentligen `INNER JOIN`) tar bara med rader som har en **match i båda** tabellerna. David har `NULL` och matchar ingen by. Morotsby har ingen invånare.

**En JOIN är alltså också ett filter.**

![Vart tog David vägen? INNER JOIN tar bara rader med match](diagrams/sql_join_filter.png)

Det är precis det som hände i Murder Mystery. Joe Germuska hade rätt sorts gymkort och var på gymmet rätt dag. Men han hade inget körkort i databasen, och därför försvann han tyst i `JOIN` mot `drivers_license`. Kvar blev bara Jeremy Bowers.

Om du inte visste om det här kunde du ha trott att du hade filtrerat bort Joe med bilplåten. I själva verket gjorde din `JOIN` det åt dig.

### `LEFT JOIN`: behåll alla till vänster

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
| David | NULL |
| Eva | Gurkby |

![LEFT JOIN: David är med, med NULL som by](diagrams/sql_left_join.png)

Alla rader från tabellen **till vänster** (`person`, den som står i `FROM`) följer med. Saknas en match fylls det i med `NULL`.

### Alias

```sql
SELECT p.name, v.name AS village
FROM person AS p
JOIN village AS v ON p.village_id = v.id
WHERE v.name = 'Lökby';
```

`p` och `v` är smeknamn för tabellerna. De behövs när två tabeller har kolumner med samma namn (här `name`), och de gör långa kedjor läsbara.

När du hittade mördaren i Murder Mystery kopplade du fyra tabeller: gymmedlem, incheckning, person och körkort. Utan alias hade den frågan blivit en vägg av text.

### JOIN + GROUP BY

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

Det var "Hur mycket guld finns i Cucumbertown?" på SQL Island.

### Clean Code: SQL

> Så här skriver du SQL som går att läsa om ett halvår.

```sql
-- ❌ Fungerar, men läs den här om ett halvår...
select * from person p, village v where p.village_id=v.id and v.name='Lökby' and p.gold>100 or p.job='pilot'
```

```sql
-- ✅ Samma fråga, men nu syns tanken (och felet med AND/OR försvann)
SELECT p.name, p.gold, v.name AS village
FROM person AS p
JOIN village AS v ON p.village_id = v.id
WHERE v.name = 'Lökby'
  AND (p.gold > 100 OR p.job = 'pilot');
```

Kör båda och jämför. Den andra ger bara Anna. Den första ger Anna, och dessutom **David tre gånger**, en gång per by.

Vad hände? `OR p.job = 'pilot'` utan parenteser gör att piloten släpps igenom oavsett vad som står före. Då gäller inte heller kopplingen `p.village_id = v.id` för honom, och kommatecken-JOIN parar ihop honom med *alla* byar. Två små fel som förstärker varandra, och båda syns direkt när frågan är snyggt formaterad.

- **Nyckelord med versaler** (`SELECT`, `WHERE`), och namn på tabeller och kolumner med gemener
- **En del per rad:** `SELECT`, `FROM`, `JOIN`, `WHERE` och `GROUP BY` får varsin rad
- **Välj kolumner du behöver** i stället för `*` i riktig kod
- **`JOIN ... ON`** i stället för kommatecken
- **Parenteser** så fort du blandar `AND` och `OR`
- **Korta men begripliga alias:** `p` för `person` är bra, `t1` och `t2` är det inte

> 💬 *Det här är hur jag brukar formatera SQL. Har ditt team en annan stil som är lika läsbar? Kör på den. Det viktiga är att alla i projektet skriver likadant.*

---

## I vilken ordning kör databasen frågan?

Det här är kanske den viktigaste insikten i hela texten. Du **skriver** en fråga i en ordning, men databasen **kör** den i en annan:

| Du skriver | Databasen kör |
|---|---|
| 1. `SELECT` | 5. välj kolumner |
| 2. `FROM` / `JOIN` | **1.** hämta och koppla tabeller |
| 3. `WHERE` | **2.** filtrera rader |
| 4. `GROUP BY` | **3.** lägg i högar |
| 5. `HAVING` | **4.** filtrera högar |
| 6. `ORDER BY` | **6.** sortera |
| 7. `LIMIT` | **7.** klipp |

![Skrivordning och körordning för en SQL-fråga](diagrams/sql_korordning.png)

Nu går det att förklara saker som annars känns godtyckliga:

- **Varför kan inte `WHERE` använda `COUNT(*)`?** När `WHERE` körs (steg 2) finns inga högar än. De skapas i steg 3.
- **Varför försvinner rader i en `JOIN`?** Kopplingen sker i steg 1, innan ditt `WHERE` ens har börjat.
- **Varför kan `ORDER BY` ofta använda ett alias från `SELECT`, men `WHERE` kan det inte?** `ORDER BY` körs efter `SELECT`, men `WHERE` körs före.

---

## DML: ändra data

⚠️ Härifrån ändras tabellen på riktigt.

### `INSERT`: lägg till en rad

```sql
INSERT INTO person (name, job, gold, village_id)
VALUES ('Främling', NULL, 0, 1);
```

| id | name | job | gold | village_id |
|---|---|---|---|---|
| ... | | | | |
| **6** | **Främling** | **NULL** | **0** | **1** |

![INSERT: en sjätte rad med id 6 har lagts till](diagrams/sql_insert_efter.png)

Vi skrev aldrig `id`. Databasen räknar upp det själv. Kolumnerna och värdena paras ihop **i den ordning** du skriver dem.

Det var precis så SQL Island registrerade dig som `'Stranger'` och gav dig `personid` 20.

### `UPDATE`: ändra rader

```sql
UPDATE person
SET gold = gold + 100
WHERE id = 6;
```

`SET` säger **vad** som ska ändras och `WHERE` säger **vilka rader**.

![UPDATE: Främlings guld ändras från 0 till 100](diagrams/sql_update_efter.png)

`gold = gold + 100` läser det gamla värdet och skriver tillbaka det nya. `gold = 100` hade skrivit över värdet, oavsett vad som stod där innan. När du sålde ringen och tekannan på SQL Island var det `SET gold = gold + 120`.

Du kan ändra flera kolumner på en gång:

```sql
UPDATE person
SET name = 'Fia', job = 'baker'
WHERE id = 6;
```

Filtrera på `id` när du ändrar data. Ett namn kan finnas flera gånger, men ett `id` är unikt. När du bytte namn från Stranger var det `WHERE personid = 20`.

### `DELETE`: ta bort rader

```sql
DELETE FROM person
WHERE id = 6;
```

![DELETE WHERE id = 6: raden Fia stryks](diagrams/sql_delete_fore.png)

`DELETE` tar bort **hela rader**. Vill du bara tömma en kolumn är det `UPDATE ... SET kolumn = NULL` du är ute efter.

Dirty Dieter och Dirty Diane försvann med `DELETE FROM inhabitant WHERE name = 'Dirty Dieter'`.

### ⚠️ Utan `WHERE`

```sql
UPDATE person SET gold = 0;   -- alla blir fattiga
DELETE FROM person;           -- tabellen blir tom
```

![UPDATE utan WHERE: alla får 0 guld](diagrams/sql_update_utan_where.png)

Ingen varning och ingen ångra-knapp. Det här är ett klassiskt sätt att förstöra en produktionsdatabas.

### Den gyllene regeln

1. **Skriv `SELECT` med ditt `WHERE` först**

   ```sql
   SELECT * FROM person WHERE id = 6;
   ```

2. **Ser raderna rätt ut? Byt då bara början**

   ```sql
   DELETE FROM person WHERE id = 6;
   ```

Samma `WHERE` ger samma rader. Du vet vad du ändrar **innan** du ändrar det.

Vill du ha ett skyddsnät till kan du använda TCL:

```sql
BEGIN TRANSACTION;
DELETE FROM person WHERE id = 6;
SELECT * FROM person;   -- ser det rätt ut?
COMMIT;                 -- ja, spara
-- ROLLBACK;            -- nej, ångra allt
```

### Bonus: hur visste Murder Mystery att du hade rätt?

```sql
INSERT INTO solution VALUES (1, 'Jeremy Bowers');
SELECT value FROM solution;
```

Du lämnade ditt svar med en `INSERT`, alltså DML. Tabellen `solution` har en **trigger**, SQL som körs automatiskt vid varje `INSERT`. Triggern jämför ditt svar med facit och skriver tillbaka ett meddelande.

Facit stod aldrig i klartext i databasen. Det var sparat som `hex('Jeremy Bowers')`, så att ingen kunde fuska med `SELECT * FROM solution`.

---

## Vanliga misstag

| Misstag | Vad händer? | Gör så här i stället |
|---|---|---|
| `WHERE x = NULL` | 0 rader, inget felmeddelande | `WHERE x IS NULL` |
| `AND` och `OR` utan parenteser | Fel rader smiter med | Sätt parenteser |
| `"baker"` med dubbla citattecken | Tolkas som ett kolumnnamn i många databaser | `'baker'` med enkla citattecken |
| `name = 'Annabel'` när du bara vet förnamnet | 0 rader | `name LIKE 'Annabel%'` |
| `WHERE COUNT(*) > 1` | Felmeddelande | `HAVING COUNT(*) > 1` |
| En kolumn i `SELECT` som inte finns i `GROUP BY` | Felmeddelande, eller ett slumpvis värde i SQLite | Gruppera på kolumnen, eller använd ett aggregat |
| Rader "försvinner" efter en `JOIN` | Inner join tar bara rader med match | `LEFT JOIN` om du vill behålla dem |
| `UPDATE` eller `DELETE` utan `WHERE` | Alla rader påverkas | `SELECT` med samma `WHERE` först |
| Förlita sig på ordningen utan `ORDER BY` | Ordningen kan ändras när som helst | Skriv `ORDER BY` |

---

## Sammanfattning

**Det du gjorde i spelen, och vad det heter:**

| I spelet | Kommando |
|---|---|
| 🏝️ Tittade runt på ön | `SELECT *` |
| 🏝️ Letade slaktare, smeder och piloter | `WHERE`, `LIKE`, `IN` |
| 🏝️ Plockade upp föremål, sålde och bytte namn | `UPDATE` |
| 🏝️ Registrerades som Stranger och fick ett svärd | `INSERT` |
| 🏝️ Räknade invånare och guld per by | `JOIN`, `COUNT`, `SUM`, `GROUP BY` |
| 🏝️ Besegrade Dieter och Diane | `DELETE` |
| 🕵️ Hittade sista huset på gatan | `ORDER BY ... DESC LIMIT 1` |
| 🕵️ Följde gymkort → person → körkort | flera `JOIN` i kedja |
| 🕵️ Tre konsertbesök i december | `BETWEEN`, `GROUP BY`, `HAVING` |
| 🕵️ Lämnade svaret | `INSERT` + trigger |

**Tre vanor att ta med dig:**

1. `SELECT` med samma `WHERE` **innan** du kör `UPDATE` eller `DELETE`
2. Filtrera på **`id`**, inte på namn, när du ändrar data
3. **Parenteser** när du blandar `AND` och `OR`

---

## Övningar

Använd övningsdatan ovan. Kör skriptet igen om du har ändrat i tabellerna.

### 🟢 Övning 1: De rika

Visa namn och guld för alla som har mer än 100 guld, sorterat med den rikaste först.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT name, gold
FROM person
WHERE gold > 100
ORDER BY gold DESC;
```

Resultatet är David (500), Bertil (300), Eva (250) och Anna (120). Cissi har bara 80 och stoppas av dörrvakten.

</details>

### 🟢 Övning 2: Bokstaven i

Vilka personer har bokstaven `i` någonstans i namnet?

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT name
FROM person
WHERE name LIKE '%i%';
```

Resultatet är Bertil, Cissi och David. `%` på båda sidor betyder "var som helst i namnet".

</details>

### 🟢 Övning 3: Gurkby

Hur många personer bor i Gurkby? Använd byns **namn**, inte dess id.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT COUNT(*)
FROM person AS p
JOIN village AS v ON p.village_id = v.id
WHERE v.name = 'Gurkby';
```

Resultatet är 2. Namnet finns bara i `village`, så du behöver en `JOIN` för att kunna filtrera på det.

</details>

### 🟡 Övning 4: Rika yrken

Vilka yrken har tillsammans mer än 250 guld? Visa yrket och summan.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT job, SUM(gold) AS total
FROM person
GROUP BY job
HAVING SUM(gold) > 250;
```

Resultatet är `pilot | 500` och `smith | 300`. Bagarna har 200 och köpmannen exakt 250, och 250 är inte *mer än* 250.

Villkoret handlar om högar, inte enskilda rader, och därför är det `HAVING` och inte `WHERE`.

</details>

### 🟡 Övning 5: Spökbyn

Vilken by har ingen invånare alls?

*Tips: vilken sorts JOIN behåller rader som saknar match?*

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT v.name
FROM village AS v
LEFT JOIN person AS p ON p.village_id = v.id
WHERE p.id IS NULL;
```

Resultatet är Morotsby.

`LEFT JOIN` behåller alla byar, och en by utan invånare får `NULL` i alla kolumner från `person`. Sedan plockar `WHERE p.id IS NULL` fram just de raderna.

Lägg märke till att `village` står till vänster här. Det är byarna vi vill behålla.

</details>

### 🟡 Övning 6: Snitt per by

Visa snittguldet per by. David, som inte bor i någon by, ska också vara med.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT v.name AS village, AVG(p.gold) AS snittguld
FROM person AS p
LEFT JOIN village AS v ON p.village_id = v.id
GROUP BY v.name;
```

| village | snittguld |
|---|---|
| NULL | 500 |
| Gurkby | 275 |
| Lökby | 100 |

Nu står `person` till vänster, eftersom det är *personerna* som ska behållas. David hamnar i en egen hög med `NULL` som bynamn.

</details>

### 🔴 Övning 7: Bonus till bagarna

Alla bagare i Lökby ska få 50 guld extra. Gör det på ett säkert sätt, så att du hinner kontrollera resultatet innan ändringen sparas. Använd byns **namn**, inte dess id.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
-- 1. Vilka rader gäller det?
SELECT *
FROM person
WHERE job = 'baker'
  AND village_id = (SELECT id FROM village WHERE name = 'Lökby');

-- 2. Ändra inuti en transaktion
BEGIN TRANSACTION;

UPDATE person
SET gold = gold + 50
WHERE job = 'baker'
  AND village_id = (SELECT id FROM village WHERE name = 'Lökby');

-- 3. Kontrollera
SELECT name, gold FROM person;

-- 4. Spara (eller ROLLBACK om något blev fel)
COMMIT;
```

Anna går från 120 till 170 och Cissi från 80 till 130.

Här samlas allt: den gyllene regeln (`SELECT` först), en subquery som översätter bynamn till id, `gold = gold + 50` i stället för att skriva över, och TCL som skyddsnät.

</details>

### 🔴 Övning 8: Rikast i varje by

Vem är rikast i varje by? Visa byn, namnet och guldet.

*Tips: en subquery kan referera till raden som den yttre frågan just tittar på.*

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT v.name AS village, p.name, p.gold
FROM person AS p
JOIN village AS v ON p.village_id = v.id
WHERE p.gold = (
    SELECT MAX(p2.gold)
    FROM person AS p2
    WHERE p2.village_id = p.village_id
);
```

| village | name | gold |
|---|---|---|
| Lökby | Anna | 120 |
| Gurkby | Bertil | 300 |

Det här kallas en **korrelerad subquery**. Den inre frågan körs en gång *per rad* i den yttre frågan, och `p.village_id` pekar på just den raden. För Anna frågar den "vad är max i Lökby?", och för Bertil "vad är max i Gurkby?".

Varför fungerar det inte med bara `GROUP BY v.name` och `MAX(p.gold)`? Då får du det högsta guldet per by, men inte *vem* som har det. Kommer du ihåg tvätthögen? Högen vet inte vilket plagg den är.

</details>

---

Du har redan rymt från en ö och löst ett mord. Nu vet du också vad allt heter och varför det fungerar. Det är skillnaden mellan att kunna följa ett recept och att kunna laga mat. Snyggt jobbat! 💪

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*

---

📖 **Läs mer i C#-boken**  
→ [SQL-intro](https://marcusjobb.github.io/programmering-csharp/sql/sql-intro/)
