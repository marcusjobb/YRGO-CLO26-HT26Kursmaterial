---
title: Inlämning 1 — Din favoritartists diskografi (SQL)
author: Marcus Ackre Medina
type: assignment
topic: databaser
difficulty: 2
language: sql
status: new
marcus_voice: true
tags: ["sql", "databaser", "normalisering", "joins", "crud", "individuell"]
---

# Inlämning 1 — Din favoritartists diskografi

**Individuell uppgift**

---

## Bakgrunden

Du är ett obotligt musiknörd som bestämt dig för att katalogisera en artists hela diskografi i en riktig databas. Inga mer Excel-ark. Inga mer anteckningsböcker. Bara ren, normaliserad SQL.

Välj en artist du faktiskt gillar. Det spelar ingen roll vem — men du ska kunna rabbla albumtitlar och låtar utan att googla. Det märks om du bryr dig.

*(Exempeldatan i det här dokumentet använder Taylor Swift. Du behöver inte följa exemplet.)*

---

## Vad du ska bygga

En SQL-databas med artistens diskografi. Den ska innehålla:

- Minst **3 album** med minst **3 låtar** vardera
- Minst **3 tabeller** med korrekta relationer
- **Primary keys** och **foreign keys** på alla kopplingar
- Normaliserad till **minst 3NF**
- Ett **ER-diagram** (relationsschema) som visar din design

---

## Föreslagna tabeller

```sql
artists (id, name, genre, country, debut_year)
albums  (id, title, release_year, artist_id)
songs   (id, title, duration_seconds, track_number, album_id)
```

Du får utöka med fler tabeller om du vill — till exempel `tours` eller `collaborations`.

---

## Queries du ska kunna köra

Skriv och spara alla queries i en `.sql`-fil som du lämnar in.

### G-krav

```sql
-- 1. Lista alla album av artisten, sorterade efter år
SELECT title, release_year
FROM albums
ORDER BY release_year;

-- 2. Lista alla låtar på ett specifikt album
SELECT s.title, s.duration_seconds
FROM songs s
JOIN albums a ON s.album_id = a.id
WHERE a.title = 'Folklore';

-- 3. Hur många låtar finns det på varje album?
SELECT a.title, COUNT(s.id) AS antal_latar
FROM albums a
JOIN songs s ON s.album_id = a.id
GROUP BY a.title;

-- 4. Vilken är den längsta låten?
SELECT title, duration_seconds
FROM songs
ORDER BY duration_seconds DESC
LIMIT 1;

-- 5. Lista alla låtar sorterade alfabetiskt
SELECT title FROM songs ORDER BY title;
```

### VG-krav (utöver G)

```sql
-- 6. Snittlängd på låtar per album
SELECT a.title, AVG(s.duration_seconds) AS snitt_sekunder
FROM albums a
JOIN songs s ON s.album_id = a.id
GROUP BY a.title;

-- 7. Albumet med flest låtar
SELECT a.title, COUNT(s.id) AS antal
FROM albums a
JOIN songs s ON s.album_id = a.id
GROUP BY a.title
ORDER BY antal DESC
LIMIT 1;

-- 8. Alla låtar längre än 4 minuter (240 sekunder), med albumtitel
SELECT s.title, a.title AS album, s.duration_seconds
FROM songs s
JOIN albums a ON s.album_id = a.id
WHERE s.duration_seconds > 240;
```

---

## Krav

### G — Godkänt

- [ ] Minst 3 tabeller med primary keys och foreign keys
- [ ] Minst 3 album med minst 3 låtar vardera
- [ ] Normaliserad till 3NF
- [ ] ER-diagram finns med i inlämningen
- [ ] Queries 1–5 körs och ger korrekt resultat
- [ ] Koden är inlämnad som `.sql`-fil

### VG — Väl godkänt

Allt i G, plus:

- [ ] Queries 6–8 körs och ger korrekt resultat
- [ ] Du kan muntligt förklara varför du valde din tabellstruktur
- [ ] Du kan förklara vad som händer i en JOIN
- [ ] Databasen har minst ett many-to-many-förhållande (t.ex. låt ↔ genre, artist ↔ tour)

---

## Inlämning

Lämna in en `.zip` eller länk till GitHub-repo som innehåller:

1. `schema.sql` — CREATE TABLE-satser
2. `data.sql` — INSERT-satser med din data
3. `queries.sql` — alla queries numrerade
4. `er_diagram.png` (eller `.pdf`) — ditt relationsschema

**Deadline:** Fredag v2 (exakt datum annonseras på Classroom)

---

*Lycka till. Och om du väljer Taylor Swift — All Too Well (10 minute version) är 621 sekunder.*
