# Databaser — varför finns de, och vad kan de?

## En vanlig tisdag på CSI: Göteborg

Kriminalinspektör Lindqvist tittar upp från sitt skrivbord och ber om en lista:

> "Alla män, 25–35 år, brunt hår, hockeyfrilla, utstående öron, minst 195 centimeter lång, skonummer 43."

Det är en rimlig begäran. Men hur löser du den om du inte har en databas?

---

## Utan databas: pappersarkivet

Du börjar gå igenom pappersarkivet manuellt. En mapp per person. Tio sekunder per mapp. Göteborg har 400 000 folkbokförda.

400 000 × 10 sekunder = ungefär **46 dagar**.

Det är naturligtvis inte rimligt för ett brott som kräver ett snabbt svar.

---

## Med databas: under en sekund

Med en databas ställer du en fråga:

```sql
SELECT *
FROM personer
WHERE kön       = 'man'
  AND ålder     BETWEEN 25 AND 35
  AND hårfärg   = 'brun'
  AND frisyr    = 'hockeyfrilla'
  AND öron      = 'utstående'
  AND längd     >= 195
  AND skonummer = 43;
```

Svarstid: under en sekund. Samma sökning, oavsett om det är 400 000 eller 40 miljoner poster.

---

## Vad är en databas?

En databas är ett organiserat sätt att lagra information så att man kan söka, filtrera och kombinera den snabbt. Tänk på det som ett pappersarkiv — men ett som aldrig behöver bläddras igenom manuellt.

| Utan databas | Med databas |
|-------------|------------|
| Pappersarkiv | Strukturerade tabeller |
| Manuell sökning | SQL-fråga |
| 46 dagar | Under en sekund |
| En person åt gången | Tusentals parallellt |

---

## Tre grundbegrepp

En databas organiserar data i **tabeller**. En tabell liknar ett kalkylark:

| id | namn  | ålder | stad     |
|----|-------|-------|----------|
| 1  | Anna  | 28    | Göteborg |
| 2  | Björn | 34    | Malmö    |

Varje **rad** representerar ett objekt — i det här fallet en person. Varje **kolumn** är en egenskap — namn, ålder, stad.

De tre begreppen att hålla fast vid: **tabell**, **rad**, **kolumn**.

---

## Den enklaste SQL-frågan

```sql
SELECT * FROM personer;
```

Läs det som en mening: *Välj allt från tabellen personer.*

- `SELECT` — vad vill du se?
- `*` — allting (alla kolumner)
- `FROM` — varifrån?
- `personer` — tabellens namn

Det är hela strukturen för en grundläggande fråga. Allt annat du lär dig är varianter och tillägg.

---

## Filtrera med WHERE

```sql
SELECT * FROM personer
WHERE stad = 'Göteborg';
```

Nu får du bara personerna i Göteborg. `WHERE` är databasens sätt att ta emot ett villkor — ungefär som en `if`-sats i C#.

---

## Kombinera villkor

```sql
SELECT * FROM personer
WHERE stad = 'Göteborg'
  AND ålder BETWEEN 20 AND 30;
```

`AND` kräver att båda villkoren är sanna. `BETWEEN 20 AND 30` inkluderar gränsvärdena — alltså 20 och 30 är med.

---

## Välj specifika kolumner

```sql
SELECT namn, ålder
FROM personer
WHERE stad = 'Göteborg';
```

Istället för `*` namnger du exakt vilka kolumner du vill ha. I en riktig databas med hundra kolumner och miljoner rader gör det stor skillnad — både för prestanda och läsbarhet.

---

## Varför inte bara Excel?

Excel är bra för hundratals rader och en person som arbetar åt gången. En databas är byggd för:

- Miljontals rader utan att tappa hastighet
- Flera användare som läser och skriver *samtidigt*
- Komplexa sökningar och kopplingar mellan flera tabeller
- Åtkomst från nätverk och applikationer — utan att öppna en fil

---

## Vad vi bygger den här kursen

Kursen går igenom hela stacken för hur en applikation pratar med en databas:

```
Databaser
├── SQL — ställa frågor och lagra data
├── Normalisering — designa rätt från början
├── Entity Framework — C# pratar med databasen
├── MongoDB — när tabeller inte räcker
└── Säkerhet & backup — när allt gått fel
```

---

## Tänk igenom det här

Du driver en pizzeria med 50 beställningar per dag. Efter ett år vill du veta vilken pizza som säljs mest på fredagkvällar i november.

Hur hade du löst det *utan* databas? Vad hade du behövt ha sparat från dag ett?

Fundera på det innan nästa lektion — du ska kunna sätta ord på det.

---

## Träna vidare

| Resurs | Vad |
|--------|-----|
| [W3Schools SQL Intro](https://www.w3schools.com/sql/sql_intro.asp) | Introduktion till vad SQL är och varför det används |
| [SQLZoo SELECT Basics](https://sqlzoo.net/wiki/SELECT_basics) | Interaktiva SELECT-övningar direkt i webbläsaren |
