# Övning — Subqueries i diskografin

🔴 Utmaning — **VG-nivå** *(kursplanen: "Hantera avancerade SQL-frågor med användning av t.ex subfrågor och JOIN över flera tabeller")*

## Bakgrunden

Samma diskografi-databas som i Inlämning 1 (Artist, Album, Song). Den här gången löser du
uppgifterna med **subqueries** — inte JOIN, inte flera separata SELECT du kombinerar för hand.

Schema (samma som i inlämningen):

```sql
Artist(Id, Name)
Album(Id, ArtistId, Title, ReleaseYear)
Song(Id, AlbumId, Title, DurationSeconds)
```

En subquery är en SELECT inuti en annan SELECT. Innerfrågan körs först och ger ett resultat
som ytterfrågan sedan filtrerar eller jämför mot. Se `termer/sql_avancerat.md` för grundbegreppet
innan du börjar — den här övningen är där du faktiskt använder det.

---

## Uppgift 1 — Album längre än snittet (icke-korrelerad subquery)

Hitta alla album som har **fler låtar** än vad ett album har i genomsnitt.

**Krav:**
- Använd en subquery i `WHERE`-satsen som räknar ut genomsnittligt antal låtar per album
- Ingen GROUP BY-hjälpvariabel utanför frågan — allt ska ske i en (1) SQL-sats

**Exempeloutput** (siffror är exempel, inte facit):

```
Album med fler låtar än snittet:
  1989 (Taylor's Version) — 21 låtar
  Red (Taylor's Version)  — 30 låtar
```

> Tips: du behöver räkna låtar per album i en subquery, och genomsnittet av *den* subqueryn i en till.

---

## Uppgift 2 — Artister utan album efter 2020 (NOT IN)

Lista alla artister i tabellen som **inte** har släppt något album efter år 2020.

**Krav:**
- Använd `WHERE ArtistId NOT IN (subquery)`
- Fundera på vad som händer om subqueryn kan returnera `NULL` — testa det och skriv ner vad du märker

**Exempeloutput:**

```
Artister utan album efter 2020:
  Fleetwood Mac
  ABBA
```

---

## Uppgift 3 — Längsta låten per artist (korrelerad subquery)

För varje artist: hämta titeln på deras **längsta** låt.

**Krav:**
- Den inre subqueryn måste referera till den yttre frågans rad (en *korrelerad* subquery) — det är skillnaden mot uppgift 1 och 2
- `WHERE Song.DurationSeconds = (SELECT MAX(...) ... WHERE Album.ArtistId = Artist.Id)`-mönstret är precis det du ska landa i

**Exempeloutput:**

```
Längsta låt per artist:
  Taylor Swift  — All Too Well (10 Minute Version)  (10:13)
  ABBA          — Thank You For The Music            (3:50)
```

> Tips: en korrelerad subquery körs en gång *per rad* i ytterfrågan, inte en gång totalt. Det är därför den kan referera till ytterfrågans tabell.

---

## Uppgift 4 — EXISTS istället för IN

Skriv om uppgift 2 (artister utan album efter 2020) så att den använder `NOT EXISTS` istället för `NOT IN`.

**Krav:**
- Resultatet ska bli identiskt med uppgift 2
- Skriv en kommentarsrad ovanför frågan: när är `EXISTS` att föredra framför `IN`? (en mening räcker)

---

## Utmanande frågor (för dig som vill djupare)

1. Går uppgift 3 (längsta låten per artist) att lösa med ett `JOIN` + `GROUP BY` istället för en korrelerad subquery? Skriv den versionen också — vilken är lättare att läsa?
2. En subquery i `WHERE` jämfört med en subquery i `FROM` (en "derived table") — vad är skillnaden i när de körs och vad de returnerar?
3. Kan du skriva uppgift 1 med en CTE (`WITH ... AS`) istället för en subquery i `WHERE`? Jämför läsbarheten.

> ⏱️ **15-minutersregeln:** Fastnar du i mer än 15 minuter på en korrelerad subquery — fråga klassen, en AI eller läraren. Kämpa inte ensam längre än så.

---

*Facit finns hos läraren.*
