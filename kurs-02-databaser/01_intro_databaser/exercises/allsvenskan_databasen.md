# Allsvenskan-databasen

🟡

Du ska bygga en databas för en förenklad version av Allsvenskan. Gör uppgifterna i DB Browser for SQLite.

---

## Uppgift 1 — Skapa tabellerna

Skapa en ny databas som heter `Allsvenskan.db`. Skapa sedan två tabeller: `clubs` och `players`.

**clubs**

| Kolumn | Typ | Notering |
|--------|-----|----------|
| id | INTEGER | PRIMARY KEY |
| name | TEXT | NOT NULL |
| city | TEXT | |
| founded | INTEGER | |

**players**

| Kolumn | Typ | Notering |
|--------|-----|----------|
| id | INTEGER | PRIMARY KEY |
| name | TEXT | NOT NULL |
| position | TEXT | |
| age | INTEGER | |
| club_id | INTEGER | FOREIGN KEY → clubs(id) |

---

## Uppgift 2 — Seeda databasen

Lägg in de här fyra klubbarna:

| id | name | city | founded |
|----|------|------|---------|
| 1 | Malmö FF | Malmö | 1910 |
| 2 | Hammarby IF | Stockholm | 1889 |
| 3 | IFK Göteborg | Göteborg | 1904 |
| 4 | AIK | Solna | 1891 |

Lägg sedan in de här spelarna:

| id | name | position | age | club_id |
|----|------|----------|-----|---------|
| 1 | Erik Svensson | Forward | 24 | 1 |
| 2 | Jonas Berg | Midfielder | 31 | 1 |
| 3 | Sara Lindqvist | Defender | 27 | 1 |
| 4 | Mohammed Al-Hassan | Goalkeeper | 29 | 2 |
| 5 | Linus Karlsson | Forward | 22 | 2 |
| 6 | Anna Ström | Midfielder | 34 | 2 |
| 7 | Pedro Silva | Defender | 26 | 3 |
| 8 | Kristofer Nyman | Forward | 19 | 3 |
| 9 | Ingrid Holmberg | Midfielder | 28 | 3 |
| 10 | Viktor Åberg | Goalkeeper | 33 | 4 |
| 11 | Fatima Osei | Forward | 21 | 4 |
| 12 | David Melin | Defender | 30 | 4 |

---

## Uppgift 3 — Visa alla spelare

Skriv en `SELECT`-sats som listar alla spelares namn och ålder, **sorterat från äldst till yngst**.

---

## Uppgift 4 — Spelare i en specifik klubb

Skriv en `SELECT`-sats som visar alla spelares namn och position som spelar i **Malmö FF**.

> **Tips:** Du behöver en `JOIN` mellan `players` och `clubs`, och ett `WHERE`-villkor på klubbnamnet.

---

## Uppgift 5 — Antal spelare per klubb

Skriv en `SELECT`-sats som visar hur många spelare varje klubb har i sin trupp.

> **Tips:** `COUNT` och `GROUP BY`

---

## Uppgift 6 — Yngste spelaren

Skriv en `SELECT`-sats som hittar den yngsta spelaren i hela ligan. Visa namn och ålder.

> **Tips:** `ORDER BY age` + `LIMIT`

---

## Uppgift 7 — Ny spelare

Lägg till en ny spelare i valfri klubb. Välj namn, position och ålder själv.

---

## Uppgift 8 — Spelarbyten händer

Spelare 5 (Linus Karlsson) lämnar ligan. Ta bort honom ur databasen.

---

## Uppgift 9 — Flytt

Hammarby IF spelar sina hemmamatcher på Friends Arena i Solna. Uppdatera deras stad till `'Solna'`.

Skriv sedan en `SELECT` som visar alla klubbar för att bekräfta att ändringen gick igenom.

---

## Uppgift 10 — Medelålder 🔴

Beräkna medelåldern på spelarna i varje klubb. Visa klubbnamn och medelålder.

> **Tips:** `AVG(age)` + `JOIN` + `GROUP BY`

---

## Uppgift 11 — Störst trupp 🔴

Vilken klubb har flest spelare? Visa klubbnamn och antal.

> **Tips:** Sortera på antal och ta den första raden.

---

## Uppgift 12 — Veteraner 🔴

Lista alla spelare som är **30 år eller äldre**, och visa även vilket lag de spelar i.

Kolumner: spelarens namn, ålder, klubbens namn.
