---
marp: true
theme: nion-dark
paginate: true
---

# SQL — Lek med databaser
## Din första fråga till databasen

---

# Vad är SQL?

**SQL** = Structured Query Language

Du ställer frågor till databasen på ett språk den förstår.

```sql
SELECT * FROM kunder WHERE stad = 'Göteborg';
```

> "Ge mig alla kunder som bor i Göteborg."

---

# Öppna din lekplats

👉 **sqliteonline.com**

- Ingen installation
- Fungerar i webbläsaren
- Riktig SQLite — samma motor vi använder i C#

Klicka **Demo DB** uppe till vänster så laddas en färdig databas.

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

---

# Prova: Hämta allt

```sql
SELECT * FROM customers;
```

`*` betyder **alla kolumner**.

**Vad ser du?**
- Hur många rader?
- Vilka kolumner finns?

---

# Prova: Välj kolumner

```sql
SELECT CustomerName, City, Country
FROM customers;
```

Bara de kolumner du vill ha — inget mer.

---

# Prova: Filtrera med WHERE

```sql
SELECT CustomerName, City
FROM   customers
WHERE  Country = 'Germany';
```

**Uppdrag:** Hämta alla kunder från France.

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

---

# Prova: Begränsa med LIMIT

```sql
SELECT CustomerName
FROM   customers
ORDER BY CustomerName
LIMIT 5;
```

De fem första kunderna i bokstavsordning.

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

---

# Uppdrag 🎯

Klara de här utan att googla — bara prova dig fram:

1. Vilka kunder bor i London?
2. Hur många länder finns representerade? *(tips: `COUNT(DISTINCT Country)`)*
3. Lista de 10 dyraste produkterna *(tabellen heter `products`)*
4. Vilken anställd heter mest ovanligt? *(tabellen heter `employees`)*

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
