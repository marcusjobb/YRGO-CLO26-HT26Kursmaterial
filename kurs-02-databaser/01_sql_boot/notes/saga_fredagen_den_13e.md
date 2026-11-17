# Fredagen den 13:e

🟡

*En till saga om en databas. Den här gången får du inte läsa sagan först.*

---

Det finns en skog utanför Lindvik som ingen går genom efter mörkrets inbrott.

Det står ingen skylt om det. Det behövs inte. Alla som har bott i Lindvik tillräckligt länge vet det ändå, på samma sätt som man vet att man inte ska titta in i en mörk källare, eller svara när någon säger ens namn bakom en stängd dörr. Man vet det i magen, inte i huvudet.

Kian hade bara bott i Lindvik i två år.

Fredagen den trettonde hängde fullmånen över trädtopparna som en lampa någon hade glömt att släcka. Kian hade läst att fullmåne var romantiskt. Han hade läst att en picknick vid sjön var ännu mer romantiskt. Han hade inte läst något om skogen.

Fyra personer gick in mellan träden den kvällen. Lite senare var de fem.

Ingen av dem har velat berätta vad som hände. Det enda vi har är det databasen minns, och databasen minns bara siffror.

---

## Det databasen minns

Här är öarna. Id-numren är blandade med flit, så att listorna inte avslöjar något.

**Personer**

| id | Namn |
|---|---|
| 1 | Valentina |
| 2 | Kian |
| 3 | Varelsen |
| 4 | Noor |
| 5 | Elias |

Ja, Varelsen står i persontabellen. I databasen är alla som gör något en person. Databasen dömer ingen.

**Händelser**

| id | Händelse | Preposition |
|---|---|---|
| 1 | hör | |
| 2 | ger | till |
| 3 | springer | |
| 4 | bjuder in | |
| 5 | knackar på | |
| 6 | packar | |
| 7 | följer efter | |
| 8 | låser | |
| 9 | tappar | |
| 10 | ser | |
| 11 | öppnar | |
| 12 | följer med | |
| 13 | äter | |

**Saker**

| id | Sak |
|---|---|
| 1 | stugans dörr |
| 2 | en skugga mellan träden |
| 3 | en picknickkorg |
| 4 | genom skogen |
| 5 | en mobiltelefon |
| 6 | fullmånen |
| 7 | ett ljud från skogen |
| 8 | en kanelbulle |
| 9 | gänget |

Och här är kartan, alltså det som faktiskt hände. Bara siffror.

| Ordning | Person1 | Händelse | Sak | Person2 |
|---|---|---|---|---|
| 1 | 2 | 6 | 3 | |
| 2 | 2 | 4 | | 4 |
| 3 | 5 | 12 | | 2 |
| 4 | 1 | 12 | | 2 |
| 5 | 4 | 10 | 6 | |
| 6 | 2 | 9 | 5 | |
| 7 | 1 | 10 | 2 | |
| 8 | 3 | 7 | 9 | |
| 9 | 5 | 1 | 7 | |
| 10 | 4 | 3 | 4 | |
| 11 | 3 | 3 | 4 | |
| 12 | 5 | 8 | 1 | |
| 13 | 3 | 5 | 1 | |
| 14 | 4 | 11 | 1 | |
| 15 | 3 | 2 | 5 | 2 |
| 16 | 4 | 4 | | 3 |
| 17 | 3 | 13 | 8 | |

Du *kan* sitta och slå upp varje siffra för hand. Men det är just det databasen är till för.

## Två nyheter jämfört med första sagan

Om du har läst [En liten saga om en databas](en_liten_saga_om_en_databas.md) känner du igen öarna och broarna. Två saker är nya.

**Sak får vara tom.** "Kian bjuder in Noor" har ingen sak, bara en person. Därför får `item_id` vara `NULL`, och därför behövs en `LEFT JOIN` mot `item` också.

**Prepositionen bor i `verb`.** I första sagan stod det alltid "till" före mottagaren. Det fungerar för "ger", men inte för allt. Ordet hör ihop med *verbet*, inte med händelsen. Varje gång någon "ger" något är det "till" någon.

Men varför står "på" i "knackar på" och "in" i "bjuder in" inne i själva verbet, när "till" har en egen kolumn? Testet är var ordet hamnar i meningen:

- **Står ordet alltid direkt efter verbet** hör det till verbet. "Grannen *knackar på* fönstret". Ordet kan inte flyttas, och det ändrar betydelsen: att knacka på är något annat än att bara knacka. Därför ligger det i `name`.
- **Introducerar ordet mottagaren** hamnar det efter saken. "Grannen ger en ficklampa *till* någon". Det behövs bara när det finns en mottagare. Därför ligger det i `preposition`, och frågan skriver bara ut det när `receiver_id` har ett värde.

Det här är **tredje normalformen** i praktiken. Den säger att varje kolumn bara ska bero på tabellens nyckel, och inte på någon annan kolumn. Prepositionen beror inte på *händelsen*, utan på *verbet*, alltså ska den stå i verbets tabell.

Hade den stått i kartan hade vi fått skriva "till" på varje rad där någon ger något. Då är vi tillbaka på **ändringsproblemet**: samma sak på många ställen, och glömmer man ett blir det fel. Båda begreppen förklaras närmare i [Det här kallas normalisering](en_liten_saga_om_en_databas.md#det-här-kallas-normalisering) i första sagan.

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
    (1, 'Valentina'), (2, 'Kian'), (3, 'Varelsen'), (4, 'Noor'), (5, 'Elias');

INSERT INTO verb (id, name, preposition) VALUES
    ( 1, 'hör', NULL),          ( 2, 'ger', 'till'),
    ( 3, 'springer', NULL),     ( 4, 'bjuder in', NULL),
    ( 5, 'knackar på', NULL),   ( 6, 'packar', NULL),
    ( 7, 'följer efter', NULL), ( 8, 'låser', NULL),
    ( 9, 'tappar', NULL),       (10, 'ser', NULL),
    (11, 'öppnar', NULL),       (12, 'följer med', NULL),
    (13, 'äter', NULL);

INSERT INTO item (id, name) VALUES
    (1, 'stugans dörr'), (2, 'en skugga mellan träden'),
    (3, 'en picknickkorg'), (4, 'genom skogen'),
    (5, 'en mobiltelefon'), (6, 'fullmånen'),
    (7, 'ett ljud från skogen'), (8, 'en kanelbulle'),
    (9, 'gänget');

INSERT INTO event (step, doer_id, verb_id, item_id, receiver_id) VALUES
    ( 1, 2,  6, 3, NULL),
    ( 2, 2,  4, NULL, 4),
    ( 3, 5, 12, NULL, 2),
    ( 4, 1, 12, NULL, 2),
    ( 5, 4, 10, 6, NULL),
    ( 6, 2,  9, 5, NULL),
    ( 7, 1, 10, 2, NULL),
    ( 8, 3,  7, 9, NULL),
    ( 9, 5,  1, 7, NULL),
    (10, 4,  3, 4, NULL),
    (11, 3,  3, 4, NULL),
    (12, 5,  8, 1, NULL),
    (13, 3,  5, 1, NULL),
    (14, 4, 11, 1, NULL),
    (15, 3,  2, 5, 2),
    (16, 4,  4, NULL, 3),
    (17, 3, 13, 8, NULL);
```

## Innan du kör: lek detektiv

Titta bara på siffrorna en stund innan du låter databasen berätta.

- Vilket `doer_id` dyker upp oftast? Slå upp vem det är.
- Steg 13 är `3, 5, 1`. Slå upp de tre siffrorna. Passande, för en fredag den trettonde.
- Sak nummer 5 dyker upp två gånger. Vem har den första gången, och vem har den andra?

Har du en teori? Bra. Nu tar vi reda på om den stämmer.

## Låt databasen berätta

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

Det är samma fråga som i första sagan, med två tillägg:

- `LEFT JOIN item`, eftersom en händelse kan sakna sak
- `COALESCE(' ' || v.preposition, '')` sätter in prepositionen om verbet har en. Annars blir det ingenting. Den yttre `COALESCE` gör att hela delen försvinner när det inte finns någon mottagare.

Kör frågan. Läs resultatet uppifrån och ned, en rad i taget. Fuska inte genom att titta på sista raden först.

<details>
<summary>🌕 Spoiler: så här slutade natten (öppna först när du har kört frågan)</summary>

| step | sentence |
|---|---|
| 1 | Kian packar en picknickkorg. |
| 2 | Kian bjuder in Noor. |
| 3 | Elias följer med Kian. |
| 4 | Valentina följer med Kian. |
| 5 | Noor ser fullmånen. |
| 6 | Kian tappar en mobiltelefon. |
| 7 | Valentina ser en skugga mellan träden. |
| 8 | Varelsen följer efter gänget. |
| 9 | Elias hör ett ljud från skogen. |
| 10 | Noor springer genom skogen. |
| 11 | Varelsen springer genom skogen. |
| 12 | Elias låser stugans dörr. |
| 13 | Varelsen knackar på stugans dörr. |
| 14 | Noor öppnar stugans dörr. |
| 15 | Varelsen ger en mobiltelefon till Kian. |
| 16 | Noor bjuder in Varelsen. |
| 17 | Varelsen äter en kanelbulle. |

Varelsen jagade dem aldrig. Den hade hittat Kians telefon i mossan och försökte bara lämna tillbaka den. 📱

Ledtråden fanns där hela tiden, i steg 6. En liten, oskyldig rad mitt i allt det otäcka. Precis som i en riktig rysare: det viktigaste gömmer sig i det man inte bryr sig om.

</details>

## Övningar

### 🟢 Övning 1: Vem syns mest?

Vem gör flest saker under natten? Svara med en fråga, inte genom att räkna för hand.

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
| Varelsen | 5 |
| Noor | 4 |
| Elias | 3 |
| Kian | 3 |
| Valentina | 2 |

Varelsen är huvudpersonen i den här sagan, oavsett vad gänget tyckte.

</details>

### 🟢 Övning 2: Följ telefonen

Visa alla händelser där mobiltelefonen är med: i vilket steg, vem och vad.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT e.step, d.name, v.name
FROM event AS e
JOIN person AS d ON e.doer_id = d.id
JOIN verb   AS v ON e.verb_id = v.id
JOIN item   AS i ON e.item_id = i.id
WHERE i.name = 'en mobiltelefon'
ORDER BY e.step;
```

| step | name | name |
|---|---|---|
| 6 | Kian | tappar |
| 15 | Varelsen | ger |

Två rader som tillsammans är hela gåtans lösning.

</details>

### 🟡 Övning 3: Varelsens natt

Visa allt Varelsen gör, i ordning. Läs bara de raderna. Hur ser natten ut från Varelsens håll?

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```sql
SELECT e.step, v.name, i.name
FROM event AS e
JOIN person AS d    ON e.doer_id = d.id
JOIN verb   AS v    ON e.verb_id = v.id
LEFT JOIN item AS i ON e.item_id = i.id
WHERE d.name = 'Varelsen'
ORDER BY e.step;
```

| step | name | name |
|---|---|---|
| 8 | följer efter | gänget |
| 11 | springer | genom skogen |
| 13 | knackar på | stugans dörr |
| 15 | ger | en mobiltelefon |
| 17 | äter | en kanelbulle |

Från Varelsens håll är det en helt vanlig kväll: den springer efter några som har tappat något, lämnar tillbaka det och blir bjuden på fika. Samma data, olika perspektiv. Det är ofta så det är med data.

</details>

### 🔴 Övning 4: Ett mörkare slut

Ändra **en enda siffra** i kartan så att natten slutar betydligt otäckare. Kör sagan igen.

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

Ett förslag är att byta vad Varelsen äter i steg 17:

```sql
UPDATE event
SET item_id = 3
WHERE step = 17;
```

Nu slutar sagan med "Varelsen äter en picknickkorg." Fortfarande inte farligt, men definitivt mer oroande.

Lägg märke till hur lite som behövdes. En siffra ändrade hela slutet, eftersom alla texter bor på sina öar och kartan bara pekar.

</details>

### 🔴 Övning 5: Var ska prepositionen bo?

Någon tycker att prepositionen borde ligga i `event` i stället för i `verb`, så att man kan välja fritt för varje händelse. Vad skulle det innebära? Finns det fall där det faktiskt vore bättre?

<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

Om prepositionen låg i `event` skulle "till" behöva skrivas på varje rad där någon ger något. Det är samma upprepning som i den allra första tabellen, och samma ändringsproblem. Glömmer man den på en rad blir det "Varelsen ger en mobiltelefon Kian".

Så länge prepositionen beror på verbet och inget annat hör den hemma i `verb`. Det är tredje normalformen.

Men det finns verb där prepositionen faktiskt beror på sammanhanget. Man kan både "springa *till* någon" och "springa *från* någon". Då beror prepositionen på *händelsen*, och då är det helt rätt att den ligger i `event`. Normalisering handlar inte om att följa en regel blint. Det handlar om att fråga: **vad beror det här egentligen på?**

</details>

---

[Tillbaka till En liten saga om en databas](en_liten_saga_om_en_databas.md) · [Nästa saga: Folklore](saga_folklore.md)

*Alla personer, varelser och händelser i den här sagan är uppdiktade. Ingen varelse, picknickkorg eller kanelbulle kom till skada.*

*Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com*
