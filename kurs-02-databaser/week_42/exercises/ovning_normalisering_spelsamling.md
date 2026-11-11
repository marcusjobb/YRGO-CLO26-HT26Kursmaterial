# Övning — Normalisering: Spelsamlingen

🟡

Samma steg som orderövningen, men nu med en spelsamlingsdatabas. Det finns fler saker att hitta och fixa den här gången.

## Utgångspunkt — den platta tabellen

```
spel_id | spel_titel    | genre_1 | genre_2 | plattform | tillverkare | tillverkare_land
--------|---------------|---------|---------|-----------|-------------|----------------
101     | The Witcher 3 | RPG     | Action  | PC        | CD Projekt  | Polen
102     | Minecraft     | Sandbox |         | PC        | Mojang      | Sverige
103     | The Witcher 3 | RPG     | Action  | Xbox      | CD Projekt  | Polen
104     | FIFA 24       | Sport   |         | PC        | EA Sports   | USA
```

---

## Steg 1 — Identifiera problemen

Lista **minst fyra problem** med den här tabellen.

> **Tips:** The Witcher 3 dyker upp två gånger. Det finns tomma genre-celler. Och vad händer om CD Projekt byter namn?

---

## Steg 2 — 1NF: ett värde per cell

Genrerna ligger i `genre_1` och `genre_2`. Det bryter mot 1NF.

Visa hur tabellen ser ut efter att du åtgärdat 1NF-problemen. Det kommer bli fler rader.

---

## Steg 3 — 2NF: bryt ut upprepningarna

Spelet The Witcher 3 upprepas för varje plattform det finns på. Tillverkaren upprepas för varje spel de gjort.

Bryt ut `Spel`, `Plattform` och `Tillverkare` till egna tabeller. Visa hur tabellerna ser ut med exempeldata.

Tänk på: relationen mellan Spel och Plattform är N:M — ett spel finns på många plattformar, en plattform har många spel. Hur löser du det?

---

## Steg 4 — 3NF: kontrollera transitiva beroenden

Är `tillverkare_land` ett transitivt beroende? Förklara vad det innebär och hur du åtgärdar det.

---

## Steg 5 — Skriv SQL

Skriv `CREATE TABLE` för slutresultatet. Du ska ha tabellerna `Spel`, `Plattform`, `Tillverkare` och `Genre`, plus mellantabellerna `SpelPlattform` och `SpelGenre`.

```sql
-- Skriv din SQL här
```

> **Tips:** Mellantabeller har en sammansatt primärnyckel:
> ```sql
> PRIMARY KEY (spel_id, plattform_id)
> ```
