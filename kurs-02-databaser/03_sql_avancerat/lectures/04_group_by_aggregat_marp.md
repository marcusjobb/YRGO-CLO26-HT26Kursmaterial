---
marp: true
theme: nion_dark
paginate: true
title: GROUP BY och aggregatfunktioner
description: Räkna, summera och gruppera data med GROUP BY, COUNT, SUM, AVG, MAX och MIN. Intro inför onsdagens fördjupning.
---

<!-- _class: lead -->
<!-- _paginate: false -->

# GROUP BY och aggregat 📊

Räkna, summera, hitta mönster

*Marcus Ackre Medina · CLO26 · v.43*

---

## Vad är ett aggregat?

En funktion som tar **många rader** och returnerar **ett värde**.

```sql
SELECT COUNT(*) FROM Djur;        -- hur många djur finns det?
SELECT MAX(Alder) FROM Agare;     -- vem är äldst?
SELECT AVG(Alder) FROM Agare;     -- vad är medelåldern?
```

Utan GROUP BY räknar du hela tabellen på en gång.

---

## De fem aggregatfunktionerna

| Funktion | Vad den gör |
|----------|-------------|
| `COUNT(*)` | Räknar antal rader |
| `COUNT(kolumn)` | Räknar rader där kolumnen inte är NULL |
| `SUM(kolumn)` | Summerar värden |
| `AVG(kolumn)` | Beräknar medelvärde |
| `MAX(kolumn)` | Hittar det högsta värdet |
| `MIN(kolumn)` | Hittar det lägsta värdet |

---

## GROUP BY — dela upp i grupper

```sql
-- Hur många djur av varje art?
SELECT Art, COUNT(*) AS Antal
FROM Djur
GROUP BY Art;
```

```
Art  | Antal
─────┼──────
Hund | 2
Katt | 2
```

Utan `GROUP BY` hade vi fått **ett** svar för hela tabellen.
Med `GROUP BY Art` får vi **ett svar per art**.

---

## JOIN + GROUP BY tillsammans

```sql
-- Hur många besök har varje ägare haft totalt?
SELECT a.Namn, COUNT(b.BesokId) AS AntalBesok
FROM Agare a
JOIN Djur d   ON a.AgarId = d.AgarId
JOIN Besok b  ON d.DjurId = b.DjurId
GROUP BY a.AgarId, a.Namn
ORDER BY AntalBesok DESC;
```

```
Namn  | AntalBesok
──────┼───────────
Sara  | 3
Johan | 2
Lena  | 1
```

---

## HAVING — filtrera på aggregat

`WHERE` filtrerar **rader**. `HAVING` filtrerar **grupper**.

```sql
-- Bara ägare med fler än ett besök
SELECT a.Namn, COUNT(b.BesokId) AS AntalBesok
FROM Agare a
JOIN Djur d  ON a.AgarId = d.AgarId
JOIN Besok b ON d.DjurId = b.DjurId
GROUP BY a.AgarId, a.Namn
HAVING COUNT(b.BesokId) > 1;
```

```
Namn  | AntalBesok
──────┼───────────
Sara  | 3
Johan | 2
```

---

## WHERE vs HAVING — sida vid sida

```sql
-- WHERE: filtrera INNAN gruppering (rader)
SELECT Art, COUNT(*) AS Antal
FROM Djur
WHERE Art != 'Fisk'       -- ta bort rader först
GROUP BY Art;

-- HAVING: filtrera EFTER gruppering (grupper)
SELECT Art, COUNT(*) AS Antal
FROM Djur
GROUP BY Art
HAVING COUNT(*) > 1;      -- ta bort grupper med för få
```

**Minnesregel:** `WHERE` jobbar på rader. `HAVING` jobbar på grupper.

---

## Vanligaste misstaget

```sql
-- ❌ Du kan inte använda aggregat i WHERE
SELECT Art, COUNT(*) AS Antal
FROM Djur
WHERE COUNT(*) > 1         -- KRASCHAR
GROUP BY Art;

-- ✅ Använd HAVING istället
SELECT Art, COUNT(*) AS Antal
FROM Djur
GROUP BY Art
HAVING COUNT(*) > 1;
```

---

## Ordningen spelar roll

SQL körs i den här ordningen — inte i den ordning du skriver:

```
FROM → JOIN → WHERE → GROUP BY → HAVING → SELECT → ORDER BY
```

Det är därför `WHERE` inte kan se aggregat — de finns inte än när `WHERE` körs.

---

## Prova själv — djursjukhuset

Kör mot `djursjukhus_seed_tidb.sql` i TiDB Cloud SQL Editor:

1. Hur många besök av varje orsak finns det?
2. Vilken ägare har flest besök totalt (via sina djur)?
3. Visa bara de orsaker som förekommit mer än en gång
4. Vilken månad hade flest besök? (`MONTH(Datum)`)

> TiDB är MySQL-kompatibelt — `strftime()` är SQLite-syntax och funkar inte här.

*Onsdagens lektion fördjupar aggregat med handledning och hero-databasen.*
