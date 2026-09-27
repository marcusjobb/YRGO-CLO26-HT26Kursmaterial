# SQL — din första fråga till databasen

## Vad är SQL?

SQL (Structured Query Language) är det språk du använder för att kommunicera med en databas. Det är inte ett programmeringsspråk i vanlig mening — det är ett *frågespråk*. Du beskriver **vad** du vill ha, inte **hur** datorn ska hämta det. Det är databasens jobb att räkna ut det effektivaste sättet.

En SQL-fråga kan se ut så här:

```sql
SELECT * FROM kunder WHERE stad = 'Göteborg';
```

Läs det som en mening: *Välj allt från tabellen kunder, där staden är Göteborg.*

---

## Din lekplats: sqliteonline.com

Första gången behöver du ingen installation. Gå till **sqliteonline.com** och klicka **Demo DB** uppe till vänster. En färdig databas laddas med tabeller för kunder, anställda och produkter — riktiga data att öva på direkt i webbläsaren.

Det är riktig SQLite — samma motor vi sedan använder i C#.

---

## Grundstrukturen

Alla grundläggande SQL-frågor följer samma mönster:

```sql
SELECT  vad_du_vill_ha
FROM    vilket_bord
WHERE   vilket_villkor;
```

Tre nyckelord att börja med: `SELECT`, `FROM`, `WHERE`.

---

## Hämta allt från en tabell

```sql
SELECT * FROM customers;
```

`*` betyder *alla kolumner*. Du ser alla rader och alla fält i tabellen `customers`. Det är praktiskt för utforskning, men i produktion vill du sällan hämta allt — det är dyrt och ger mer data än du behöver.

---

## Välj specifika kolumner

```sql
SELECT CustomerName, City, Country
FROM customers;
```

Nu hämtar du bara de kolumner du faktiskt behöver. I en riktig databas med hundra kolumner och miljoner rader gör det stor skillnad för prestanda.

---

## Filtrera med WHERE

```sql
SELECT CustomerName, City
FROM   customers
WHERE  Country = 'Germany';
```

`WHERE` fungerar som ett filter. Du anger ett villkor — databasen returnerar bara de rader som uppfyller det. Textsträngar skrivs med enkelfnuttar i SQLite.

**Vanligaste misstaget:** dubbelfnuttar eller glömda citattecken runt strängar.

---

## Sortera med ORDER BY

```sql
SELECT CustomerName, City
FROM   customers
ORDER BY CustomerName;
```

Utan `ORDER BY` garanterar databasen ingen särskild ordning — aldrig lita på att rader kommer ut sorterade av sig självt. Med `DESC` sorterar du i fallande ordning:

```sql
ORDER BY CustomerName DESC;
```

`ASC` (stigande) är standard och behöver inte skrivas ut.

---

## Begränsa med LIMIT

```sql
SELECT CustomerName
FROM   customers
ORDER BY CustomerName
LIMIT 5;
```

`LIMIT` stoppar resultatet vid ett visst antal rader. Kombinationen `ORDER BY` + `LIMIT` är klassisk för att hämta exempelvis de tio senaste ordererna eller de fem billigaste produkterna.

---

## Räkna med COUNT

```sql
SELECT COUNT(*) FROM customers;
```

`COUNT()` räknar rader. Med `DISTINCT` räknar du bara unika värden:

```sql
SELECT COUNT(DISTINCT Country) FROM customers;
```

Det ger antalet unika länder i tabellen — inte hur många kunder det finns totalt.

---

## Sammanfattning: de sex grunderna

| Nyckelord  | Vad det gör |
|------------|-------------|
| `SELECT`   | Väljer kolumner |
| `FROM`     | Väljer tabell |
| `WHERE`    | Filtrerar rader efter villkor |
| `ORDER BY` | Sorterar resultatet |
| `LIMIT`    | Begränsar antal returnerade rader |
| `COUNT()`  | Räknar rader |

---

## Nästa steg

Nästa lektion skapar vi egna tabeller med `CREATE TABLE` och lägger in data med `INSERT`. Men innan dess — öva i Demo DB tills de sex nyckelorden sitter automatiskt.

---

## Träna vidare

| Resurs | Vad |
|--------|-----|
| [sqliteonline.com](https://sqliteonline.com) | Kör SQL direkt i webbläsaren mot Demo DB — ingen installation |
| [SQLZoo SELECT Basics](https://sqlzoo.net/wiki/SELECT_basics) | Interaktiva SELECT-övningar med facit |
| [SQLZoo SELECT from WORLD](https://sqlzoo.net/wiki/SELECT_from_WORLD_Tutorial) | Träna WHERE och jämförelser mot världsdata |
| [W3Schools SQL SELECT](https://www.w3schools.com/sql/sql_select.asp) | Snabbreferens för SELECT-syntax |
| [W3Schools SQL WHERE](https://www.w3schools.com/sql/sql_where.asp) | WHERE med exempel och operatorer |
| [W3Schools SQL ORDER BY](https://www.w3schools.com/sql/sql_orderby.asp) | Sortering och ASC/DESC |
