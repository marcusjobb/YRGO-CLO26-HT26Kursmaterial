---
marp: true
theme: nion-dark
paginate: true
---

# SQL — Lek med databaser
## Din första fråga till databasen

<!-- Välkommen till databaskursen. Idag handlar det inte om teori — vi kör direkt. Ingen installation behövs första rundan, vi kör i webbläsaren. -->

---

# Vad är SQL?

**SQL** = Structured Query Language

Du ställer frågor till databasen på ett språk den förstår.

```sql
SELECT * FROM kunder WHERE stad = 'Göteborg';
```

> "Ge mig alla kunder som bor i Göteborg."

<!-- SQL är inte ett programmeringsspråk i vanlig mening — det är ett frågespråk. Du beskriver VAD du vill ha, inte HUR datorn ska hämta det. Det är databasens jobb att räkna ut det effektivaste sättet. -->

---

# Öppna din lekplats

👉 **sqliteonline.com**

- Ingen installation
- Fungerar i webbläsaren
- Riktig SQLite — samma motor vi använder i C#

Klicka **Demo DB** uppe till vänster så laddas en färdig databas.

<!-- Visa på skärmen: öppna sqliteonline.com, klicka Demo DB, visa att tabellerna syns i vänsterspalten. Ge dem 2 minuter att göra samma sak. Kontrollera att alla kommit igång innan du går vidare. -->

---

# Grundstrukturen

```sql
SELECT  vad_du_vill_ha
FROM    vilket_bord
WHERE   vilket_villkor;
```

```sql
SELECT namn, stad
FROM   kunder
WHERE  land = 'Sverige';
```

Tre ord att börja med: `SELECT` · `FROM` · `WHERE`

<!-- Läs meningarna högt med dem: "Välj namn och stad, från tabellen kunder, där landet är Sverige." Det hjälper hjärnan att förstå att SQL är nära vanligt språk. Semikolon på slutet — krävs inte alltid men är god vana. -->

---

# Prova: Hämta allt

```sql
SELECT * FROM customers;
```

`*` betyder **alla kolumner**.

**Vad ser du?**
- Hur många rader?
- Vilka kolumner finns?

<!-- Ge dem 2 minuter. Poängtera att stjärnan är praktisk men dyr i produktion — man vill sällan hämta allt. Det är bra att tidigt vänja sig vid att namnge kolumner explicit. -->

---

# Prova: Välj kolumner

```sql
SELECT CustomerName, City, Country
FROM customers;
```

Bara de kolumner du vill ha — inget mer.

<!-- Visa att resultatet är smalare. Förklara att i en riktig databas med hundra kolumner och miljoner rader gör det stor skillnad att bara hämta det man behöver. -->

---

# Prova: Filtrera med WHERE

```sql
SELECT CustomerName, City
FROM   customers
WHERE  Country = 'Germany';
```

**Uppdrag:** Hämta alla kunder från France.

<!-- Vanligaste misstaget: glöma citattecken runt textsträngar, eller använda fel citattecken. SQLite vill ha enkelfnuttar. Dubbelkolla om de fastnar. Lösning: WHERE Country = 'France' -->

---

# Prova: Sortera med ORDER BY

```sql
SELECT CustomerName, City
FROM   customers
ORDER BY CustomerName;
```

```sql
SELECT CustomerName, City
FROM   customers
ORDER BY CustomerName DESC;
```

`ASC` = stigande (standard) · `DESC` = fallande

<!-- Bra tillfälle att prata om att databaser inte garanterar någon ordning utan ORDER BY. Aldrig lita på att rader kommer ut i "rätt" ordning av sig självt. -->

---

# Prova: Begränsa med LIMIT

```sql
SELECT CustomerName
FROM   customers
ORDER BY CustomerName
LIMIT 5;
```

De fem första kunderna i bokstavsordning.

<!-- LIMIT är väldigt användbart i produktion — paginering, preview, testdata. Kombinationen ORDER BY + LIMIT är klassisk. -->

---

# Räkna med COUNT

```sql
SELECT COUNT(*) FROM customers;
```

```sql
SELECT COUNT(*) FROM customers
WHERE Country = 'Brazil';
```

**Hur många brasilianska kunder finns det?**

<!-- COUNT är den enklaste aggregatfunktionen. Svaret på Brazil-frågan är 9. Fråga dem om de kan lista ut hur man räknar bara unika länder — förberedelse för nästa slide. -->

---

# Uppdrag 🎯

Klara de här utan att googla — bara prova dig fram:

1. Vilka kunder bor i London?
2. Hur många länder finns representerade? *(tips: `COUNT(DISTINCT Country)`)*
3. Lista de 10 dyraste produkterna *(tabellen heter `products`)*
4. Vilken anställd heter mest ovanligt? *(tabellen heter `employees`)*

<!-- Låt dem jobba 10–15 minuter. Gå runt och titta. Om någon fastnar: påminn om att tabellnamnen syns i vänsterspalten. Lösningar: 1. WHERE City='London' 2. COUNT(DISTINCT Country) = 21 3. ORDER BY Price DESC LIMIT 10 4. Fri tolkning — kul diskussion -->

---

# Nästa steg

Nästa lektion: **skapa egna tabeller**

```sql
CREATE TABLE spelare (
    id      INTEGER PRIMARY KEY,
    namn    TEXT,
    poang   INTEGER
);

INSERT INTO spelare (namn, poang) VALUES ('Maja', 42);
```

Men det är imorgon. Idag — leka.

<!-- Avsluta med att visa snabbt hur CREATE TABLE ser ut — inte gå igenom det, bara visa att det finns. Skapar förväntningar inför nästa lektion. -->

---

# Sammanfattning

| Nyckelord | Vad det gör |
|-----------|-------------|
| `SELECT`  | Väljer kolumner |
| `FROM`    | Väljer tabell |
| `WHERE`   | Filtrerar rader |
| `ORDER BY`| Sorterar resultatet |
| `LIMIT`   | Begränsar antal rader |
| `COUNT()` | Räknar rader |

<!-- Be dem stänga laptopen en sekund och berätta för grannen: vad gör SELECT, FROM och WHERE? Retrieval practice — inlärning sker bättre genom att minnas än att läsa om. -->
