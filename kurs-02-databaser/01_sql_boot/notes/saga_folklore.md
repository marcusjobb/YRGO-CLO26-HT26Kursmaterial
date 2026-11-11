# Folklore

🟡

*En till saga om en databas, den här gången med musik. Inte heller den här får du läsa först.*

> 🎵 [Lyssna på låtarna medan du kör frågorna](https://open.spotify.com/playlist/6zwgrPSAVTaFsuj9VFm5B5)

---

På albumet *folklore* från 2020 finns tre låtar som hänger ihop: **cardigan**, **august** och **betty**. Var för sig är de tre låtar. Tillsammans är de en berättelse om en sommar och tre tonåringar, berättad från tre olika håll.

Varje låt vet bara en del av sanningen. Den som sjunger vet vad hen själv har sett och känt, men inte vad de andra vet.

En databas fungerar på samma sätt. Den vet inte vad som är sant. Den vet bara vad någon har lagt in. Här har vi lagt in det som händer i låtarna, så gott vi kan tolka dem, som siffror.

Fyra personer. Fjorton händelser. Hur slutar det?

---

## Det databasen vet

**Personer**

| id | Namn |
|---|---|
| 1 | Inez |
| 2 | Augustine |
| 3 | James |
| 4 | Betty |

**Händelser**

| id | Händelse | Preposition |
|---|---|---|
| 1 | berättar | för |
| 2 | undviker | |
| 3 | är tillsammans med | |
| 4 | ångrar | |
| 5 | tillbringar | med |
| 6 | hör | |
| 7 | bjuder in | utom |
| 8 | ser | |
| 9 | dyker upp på | |
| 10 | behåller | |
| 11 | förlåter | |
| 12 | byter | |
| 13 | ber om | |

**Saker**

| id | Sak |
|---|---|
| 1 | förlåtelse |
| 2 | ett rykte |
| 3 | hela klassen |
| 4 | sommaren |
| 5 | den gamla koftan |
| 6 | Bettys fest |
| 7 | allt |
| 8 | klassrum |
| 9 | minnet av augusti |

**Kartan**

| Ordning | Person1 | Händelse | Sak | Person2 |
|---|---|---|---|---|
| 1 | 3 | 3 | | 4 |
| 2 | 3 | 5 | 4 | 2 |
| 3 | 2 | 10 | 9 | |
| 4 | 1 | 6 | 2 | |
| 5 | 1 | 1 | 7 | 4 |
| 6 | 4 | 2 | | 3 |
| 7 | 4 | 12 | 8 | |
| 8 | 4 | 7 | 3 | 3 |
| 9 | 3 | 4 | 7 | |
| 10 | 3 | 9 | 6 | |
| 11 | 3 | 13 | 1 | |
| 12 | 4 | 8 | | 3 |
| 13 | 4 | 11 | | 3 |
| 14 | 4 | 10 | 5 | |

Schemat är detsamma som i [Fredagen den 13:e](saga_fredagen_den_13e.md): sak får vara tom, och prepositionen bor i `verb`. Har du inte läst den sagan än står det där varför.

## Bygg databasen

> 🗄️ **Kör skripten mot SQLite**, t.ex. i DB Browser for SQLite eller på [sqliteonline.com](https://sqliteonline.com/). De är skrivna för SQLite och fungerar inte rakt av i andra databaser. I SQL Server sätter man till exempel ihop text med `+` eller `CONCAT` i stället för `||`, och `LOWER`, `TRIM` och `DROP TABLE IF EXISTS` kan fungera lite annorlunda.

Skapa en **ny** databas och kör skriptet:

```sql
DROP TABLE IF EXISTS event;
DROP TABLE IF EXISTS person;
DROP TABLE IF EXISTS verb;
DROP TABLE IF EXISTS item;

CREATE TABLE person (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE verb (
    id          INTEGER PRIMARY KEY,
    name        TEXT NOT NULL,
    preposition TEXT
);

CREATE TABLE item (
    id   INTEGER PRIMARY KEY,
    name TEXT NOT NULL
);

CREATE TABLE event (
    id          INTEGER PRIMARY KEY,
    step        INTEGER NOT NULL,
    doer_id     INTEGER NOT NULL REFERENCES person(id),
    verb_id     INTEGER NOT NULL REFERENCES verb(id),
    item_id     INTEGER REFERENCES item(id),
    receiver_id INTEGER REFERENCES person(id)
);

INSERT INTO person (id, name) VALUES
    (1, 'Inez'), (2, 'Augustine'), (3, 'James'), (4, 'Betty');

INSERT INTO verb (id, name, preposition) VALUES
    ( 1, 'berättar', 'för'),          ( 2, 'undviker', NULL),
    ( 3, 'är tillsammans med', NULL), ( 4, 'ångrar', NULL),
    ( 5, 'tillbringar', 'med'),       ( 6, 'hör', NULL),
    ( 7, 'bjuder in', 'utom'),        ( 8, 'ser', NULL),
    ( 9, 'dyker upp på', NULL),       (10, 'behåller', NULL),
    (11, 'förlåter', NULL),           (12, 'byter', NULL),
    (13, 'ber om', NULL);

INSERT INTO item (id, name) VALUES
    (1, 'förlåtelse'), (2, 'ett rykte'), (3, 'hela klassen'),
    (4, 'sommaren'), (5, 'den gamla koftan'), (6, 'Bettys fest'),
    (7, 'allt'), (8, 'klassrum'), (9, 'minnet av augusti');

INSERT INTO event (step, doer_id, verb_id, item_id, receiver_id) VALUES
    ( 1, 3,  3, NULL, 4),
    ( 2, 3,  5, 4, 2),
    ( 3, 2, 10, 9, NULL),
    ( 4, 1,  6, 2, NULL),
    ( 5, 1,  1, 7, 4),
    ( 6, 4,  2, NULL, 3),
    ( 7, 4, 12, 8, NULL),
    ( 8, 4,  7, 3, 3),
    ( 9, 3,  4, 7, NULL),
    (10, 3,  9, 6, NULL),
    (11, 3, 13, 1, NULL),
    (12, 4,  8, NULL, 3),
    (13, 4, 11, NULL, 3),
    (14, 4, 10, 5, NULL);
```

## Innan du kör: lek detektiv

- Steg 1 är `3, 3, –, 4`. Vilka är tillsammans när sagan börjar?
- Steg 2 har mottagaren `2`. Vem tillbringar James sommaren med?
- Sak nummer 7, "allt", dyker upp två gånger. Vem gör vad med den?
- Vem står som Person1 på den sista raden?

## Låt databasen berätta

Samma fråga som i [Fredagen den 13:e](saga_fredagen_den_13e.md). Det är hela poängen: när strukturen är rätt kan samma fråga berätta vilken saga som helst.

```sql
SELECT e.step,
       d.name || ' ' || v.name
              || COALESCE(' ' || i.name, '')
              || COALESCE(COALESCE(' ' || v.preposition, '') || ' ' || r.name, '')
              || '.' AS sentence
FROM event AS e
JOIN person AS d      ON e.doer_id = d.id
JOIN verb   AS v      ON e.verb_id = v.id
LEFT JOIN item   AS i ON e.item_id = i.id
LEFT JOIN person AS r ON e.receiver_id = r.id
ORDER BY e.step;
```

Lyssna gärna på låtarna medan du läser resultatet rad för rad.

<details>
<summary>🎸 Spoiler: så här slutade sommaren (öppna först när du har kört frågan)</summary>

| step | sentence |
|---|---|
| 1 | James är tillsammans med Betty. |
| 2 | James tillbringar sommaren med Augustine. |
| 3 | Augustine behåller minnet av augusti. |
| 4 | Inez hör ett rykte. |
| 5 | Inez berättar allt för Betty. |
| 6 | Betty undviker James. |
| 7 | Betty byter klassrum. |
| 8 | Betty bjuder in hela klassen utom James. |
| 9 | James ångrar allt. |
| 10 | James dyker upp på Bettys fest. |
| 11 | James ber om förlåtelse. |
| 12 | Betty ser James. |
| 13 | Betty förlåter James. |
| 14 | Betty behåller den gamla koftan. |

En sak att veta: **slutet är öppet i låtarna.** Det går att tolka på flera sätt, och det är inte säkert att Betty förlåter James. Raderna 13 och 14 är *vår* tolkning. Det leder oss direkt till övning 5.

</details>

## Övningar

### 🟢 Övning 1: Vems berättelse?

Vem gör flest saker i sagan? Vem gör minst?

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT p.name, COUNT(e.id) AS antal
FROM person AS p
LEFT JOIN event AS e ON e.doer_id = p.id
GROUP BY p.name
ORDER BY antal DESC, p.name;
```

| name | antal |
|---|---|
| Betty | 6 |
| James | 5 |
| Inez | 2 |
| Augustine | 1 |

Augustine gör bara en enda sak i databasen, trots att hon har en hel låt. Mer om det i övning 4.

</details>

### 🟢 Övning 2: Rykten sprids

Visa alla händelser där Inez är inblandad, som den som gör något.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT e.step, v.name, i.name, r.name AS till
FROM event AS e
JOIN person AS d      ON e.doer_id = d.id
JOIN verb   AS v      ON e.verb_id = v.id
LEFT JOIN item   AS i ON e.item_id = i.id
LEFT JOIN person AS r ON e.receiver_id = r.id
WHERE d.name = 'Inez'
ORDER BY e.step;
```

| step | name | name | till |
|---|---|---|---|
| 4 | hör | ett rykte | NULL |
| 5 | berättar | allt | Betty |

Två rader räckte för att förändra allt.

</details>

### 🟡 Övning 3: Bjöd Betty in James?

Någon skriver en enklare version av sagofrågan som struntar i prepositionen:

```sql
SELECT e.step, d.name || ' ' || v.name || ' ' || i.name || ' ' || r.name AS sentence
FROM event AS e
JOIN person AS d ON e.doer_id = d.id
JOIN verb   AS v ON e.verb_id = v.id
JOIN item   AS i ON e.item_id = i.id
JOIN person AS r ON e.receiver_id = r.id
WHERE e.step = 8;
```

Vad blir meningen? Varför är det fel, och vad säger det om var prepositionen ska bo?

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

Resultatet blir "Betty bjuder in hela klassen James". Det låter som att hon bjuder in honom, men det är precis tvärtom!

Ordet "utom" är det som bär hela betydelsen, och det finns bara i `verb`-tabellen. Prepositionen beror på verbet: när Betty "bjuder in" någon i den här sagan är det alltid alla *utom* någon. Därför ska den stå där, en gång, och inte upprepas på varje rad i kartan.

Ett litet ord i fel tabell, eller ett som glöms bort i en fråga, kan alltså vända på hela betydelsen.

</details>

### 🟡 Övning 4: Augustines perspektiv

Augustine gör bara en sak i databasen. Visa alla händelser där hon finns med, oavsett om hon gör något eller är mottagare.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT e.step,
       d.name || ' ' || v.name
              || COALESCE(' ' || i.name, '')
              || COALESCE(COALESCE(' ' || v.preposition, '') || ' ' || r.name, '')
              || '.' AS sentence
FROM event AS e
JOIN person AS d      ON e.doer_id = d.id
JOIN verb   AS v      ON e.verb_id = v.id
LEFT JOIN item   AS i ON e.item_id = i.id
LEFT JOIN person AS r ON e.receiver_id = r.id
WHERE e.doer_id = 2 OR e.receiver_id = 2
ORDER BY e.step;
```

| step | sentence |
|---|---|
| 2 | James tillbringar sommaren med Augustine. |
| 3 | Augustine behåller minnet av augusti. |

Två rader, men det är hela hennes låt. En databas sparar *vad som hände*, men inte hur det kändes. Det är därför samma sommar kan vara en hel låt för en person och en enda rad för databasen.

</details>

### 🔴 Övning 5: Skriv ditt eget slut

Slutet är öppet i låtarna. Skriv om det! Förlåter Betty inte James? Ändra raderna 13 och 14 med `UPDATE`, eller ta bort dem med `DELETE`, och lägg till egna händelser med `INSERT`. Behöver du nya verb eller saker lägger du till dem på deras öar.

Kör sagan igen och se hur ditt slut blir.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

Ett förslag, där Betty går vidare:

```sql
UPDATE event SET verb_id = 2 WHERE step = 13;

INSERT INTO item (name) VALUES ('ett nytt kapitel');
INSERT INTO verb (name, preposition) VALUES ('börjar', NULL);

UPDATE event
SET verb_id = (SELECT id FROM verb WHERE name = 'börjar'),
    item_id = (SELECT id FROM item WHERE name = 'ett nytt kapitel')
WHERE step = 14;
```

Nu slutar sagan med:

| step | sentence |
|---|---|
| 13 | Betty undviker James. |
| 14 | Betty börjar ett nytt kapitel. |

Lägg märke till att du inte behövde skriva om en enda mening. Du ändrade bara siffror på kartan, och lade till en ny ö-rad för det verb och den sak som inte fanns förut. Resten av sagan är orörd.

</details>

---

[Tillbaka till En liten saga om en databas](en_liten_saga_om_en_databas.md) · [Föregående saga: Fredagen den 13:e](saga_fredagen_den_13e.md)

*Personerna kommer från Taylor Swifts låtar på albumet folklore (2020). Händelserna är vår egen tolkning av låtarna, och sagan har ingen koppling till artisten.*

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*
