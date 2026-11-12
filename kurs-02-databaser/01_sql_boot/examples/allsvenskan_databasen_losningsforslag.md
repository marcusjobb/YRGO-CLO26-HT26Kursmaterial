# Allsvenskan-databasen — Lösningsförslag

Referenslösning för `allsvenskan_databasen.md`.

---

## Uppgift 1 — Skapa tabellerna

```sql
CREATE TABLE clubs (
    id      INTEGER PRIMARY KEY,
    name    TEXT NOT NULL,
    city    TEXT,
    founded INTEGER
);

CREATE TABLE players (
    id      INTEGER PRIMARY KEY,
    name    TEXT NOT NULL,
    position TEXT,
    age     INTEGER,
    club_id INTEGER,
    FOREIGN KEY (club_id) REFERENCES clubs(id)
);
```

## Uppgift 2 — Seeda databasen

```sql
INSERT INTO clubs (id, name, city, founded) VALUES
    (1, 'Malmö FF',      'Malmö',     1910),
    (2, 'Hammarby IF',   'Stockholm', 1889),
    (3, 'IFK Göteborg',  'Göteborg',  1904),
    (4, 'AIK',           'Solna',     1891);

INSERT INTO players (id, name, position, age, club_id) VALUES
    (1,  'Erik Svensson',      'Forward',    24, 1),
    (2,  'Jonas Berg',         'Midfielder', 31, 1),
    (3,  'Sara Lindqvist',     'Defender',   27, 1),
    (4,  'Mohammed Al-Hassan', 'Goalkeeper', 29, 2),
    (5,  'Linus Karlsson',     'Forward',    22, 2),
    (6,  'Anna Ström',         'Midfielder', 34, 2),
    (7,  'Pedro Silva',        'Defender',   26, 3),
    (8,  'Kristofer Nyman',    'Forward',    19, 3),
    (9,  'Ingrid Holmberg',    'Midfielder', 28, 3),
    (10, 'Viktor Åberg',       'Goalkeeper', 33, 4),
    (11, 'Fatima Osei',        'Forward',    21, 4),
    (12, 'David Melin',        'Defender',   30, 4);
```

## Uppgift 3 — Äldst till yngst

```sql
SELECT name, age
FROM players
ORDER BY age DESC;
```

## Uppgift 4 — Spelare i Malmö FF

```sql
SELECT p.name, p.position
FROM players p
JOIN clubs c ON p.club_id = c.id
WHERE c.name = 'Malmö FF';
```

## Uppgift 5 — Antal spelare per klubb

```sql
SELECT c.name, COUNT(p.id) AS antal_spelare
FROM clubs c
JOIN players p ON p.club_id = c.id
GROUP BY c.name;
```

## Uppgift 6 — Yngste spelaren

```sql
SELECT name, age
FROM players
ORDER BY age ASC
LIMIT 1;
```

## Uppgift 7 — Ny spelare (exempel)

```sql
INSERT INTO players (id, name, position, age, club_id)
VALUES (13, 'Maria Hansson', 'Forward', 25, 1);
```

## Uppgift 8 — Ta bort spelare 5

```sql
DELETE FROM players WHERE id = 5;
```

## Uppgift 9 — Uppdatera Hammarby IF:s stad

```sql
UPDATE clubs SET city = 'Solna' WHERE name = 'Hammarby IF';

SELECT * FROM clubs;
```

## Uppgift 10 — Medelålder per klubb 🔴

```sql
SELECT c.name, AVG(p.age) AS medelalder
FROM clubs c
JOIN players p ON p.club_id = c.id
GROUP BY c.name;
```

## Uppgift 11 — Störst trupp 🔴

```sql
SELECT c.name, COUNT(p.id) AS antal
FROM clubs c
JOIN players p ON p.club_id = c.id
GROUP BY c.name
ORDER BY antal DESC
LIMIT 1;
```

## Uppgift 12 — Veteraner 🔴

```sql
SELECT p.name, p.age, c.name AS klubb
FROM players p
JOIN clubs c ON p.club_id = c.id
WHERE p.age >= 30;
```

---

**Mappning mot inlämningen (Diskografi):**

| Övning | Inlämning (motsvarar) |
|--------|-----------------------|
| 3 — ORDER BY age | Sortera album efter år |
| 4 — JOIN + WHERE | Låtar på ett specifikt album |
| 5 — COUNT + GROUP BY | Antal låtar per album |
| 6 — ORDER BY + LIMIT | Längsta låten |
| 7 — INSERT | Lägg till ny rad |
| 8 — DELETE | Ta bort en rad |
| 9 — UPDATE | Uppdatera en rad |
| 10 — AVG + GROUP BY | Snittlängd per album |
| 11 — Störst trupp | Albumet med flest låtar |
| 12 — JOIN + WHERE (ålder) | Låtar längre än 4 min med albumtitel |
