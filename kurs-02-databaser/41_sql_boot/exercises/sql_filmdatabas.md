# SQL-filmdatabas

🟢

Sex korta uppgifter där du skapar en filmdatabas från grunden. Gör dem i DB Browser for SQLite: skriv SQL-koden i fliken **Execute SQL** och kör den.

## Uppgift 1 — Skapa databasen och tabellen

Skapa en ny databas som heter `IMDB.db`. Skapa sedan en tabell som heter `Movies` med kolumnerna `Id`, `Title`, `Language` och `Year`, och lägg in de här fyra raderna:

| Id | Title | Language | Year |
|---|---|---|---|
| 1 | Blade Runner | English | 1982 |
| 2 | Jönssonligan | Swedish | 1981 |
| 3 | Interstellar | English | 2014 |
| 4 | The Wolf of Wall Street | English | 2013 |

Skriv också en `SELECT`-sats som skriver ut hela tabellen.

## Uppgift 2 — 2000-talet

Skriv en `SELECT`-sats som skriver ut namnen på alla filmer som kom ut på 2000-talet.

## Uppgift 3 — Räkna

Skriv en `SELECT`-sats som räknar hur många filmer som är på engelska.

> **Tips:** `COUNT(Language)`

## Uppgift 4 — Ny Jönssonliga

År 2020 kom det en ny Jönssonligan-film. Uppdatera raden med Jönssonligan så att utgivningsåret blir 2020.

## Uppgift 5 — Betyg

Lägg till en ny kolumn i tabellen `Movies` som heter `Rating`. Den får inte vara `NULL` och har standardvärdet `0`.

Uppdatera sedan alla rader så att filmerna får olika betyg, och skriv en `SELECT`-sats som skriver ut hela tabellen.

## Uppgift 6 — Bra engelska filmer

Skriv en `SELECT`-sats som skriver ut namnen på alla filmer som har ett betyg över 2 och är på engelska, sorterade efter namn.
