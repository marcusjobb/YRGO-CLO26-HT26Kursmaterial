---
marp: true
theme: nion-dark
paginate: true
---

# Databaser
## Lektion 1 — Varför finns de, och vad kan de?

---

# En vanlig tisdag på CSI: Göteborg

Kriminalinspektör Lindqvist tittar upp från sitt skrivbord.

> "Jag behöver en lista på alla män,  
> 25–35 år,  
> brunt hår,  
> hockeyfrilla,  
> utstående öron,  
> minst 195 centimeter lång,  
> skonummer 43."

---

# Hur löser vi det *utan* en databas?

**Alternativ 1: Pappersarkiv**
- Gå igenom varje mapp manuellt
- Kryssa för om personen matchar
- Repetera för 400 000 folkbokförda i Göteborg

---

# Hur lång tid tar det?

En mapp tar 10 sekunder att bläddra igenom.

400 000 mappar × 10 sekunder = **46 dagar**

---

# Alternativ 2: En databas

```sql
SELECT *
FROM personer
WHERE kön      = 'man'
  AND ålder   BETWEEN 25 AND 35
  AND hårfärg = 'brun'
  AND frisyr  = 'hockeyfrilla'
  AND öron    = 'utstående'
  AND längd   >= 195
  AND skonummer = 43;
```

Svarstid: **< 1 sekund**

---

# Vad är en databas?

En databas är ett organiserat sätt att lagra information
så att man kan söka, filtrera och kombinera den snabbt.

| Utan databas | Med databas |
|-------------|------------|
| Pappersarkiv | Strukturerad tabell |
| Manuell sökning | SQL-fråga |
| 46 dagar | < 1 sekund |
| En person åt gången | Tusentals parallellt |

---

# Tre grundbegrepp

**Tabell** — ett blad med rader och kolumner

| id | namn | ålder | stad |
|----|------|-------|------|
| 1  | Anna | 28    | Göteborg |
| 2  | Björn | 34   | Malmö |

**Rad** — en person (eller ett objekt)

**Kolumn** — en egenskap (namn, ålder, stad)

---

# Den enklaste frågan

```sql
SELECT * FROM personer;
```

`SELECT` — vad vill du se?  
`*` — allt  
`FROM` — varifrån?  
`personer` — tabellens namn

---

# Filtrera med WHERE

```sql
SELECT * FROM personer
WHERE stad = 'Göteborg';
```

Nu får du bara personerna i Göteborg.

---

# Kombinera villkor

```sql
SELECT * FROM personer
WHERE stad = 'Göteborg'
  AND ålder BETWEEN 20 AND 30;
```

`AND` — båda villkoren måste stämma  
`BETWEEN 20 AND 30` — inklusive 20 och 30

---

# Välj specifika kolumner

```sql
SELECT namn, ålder
FROM personer
WHERE stad = 'Göteborg';
```

Du behöver inte hämta allt — bara det du faktiskt behöver.

---

# Varför inte bara Excel?

Excel fungerar för hundratals rader.

En databas fungerar för:
- Miljontals rader
- Flera användare samtidigt
- Komplexa sökningar utan att du öppnar en fil
- Kopplingar mellan flera tabeller

---

# Vad vi bygger den här kursen

```
Databaser
├── SQL — ställa frågor och lagra data
├── Normalisering — designa rätt från början
├── Entity Framework — C# pratar med databasen
├── MongoDB — när tabeller inte räcker
└── Säkerhet & backup — när allt gått fel
```

---

# Övning direkt

Fundera på det här — skriv ner ett svar:

> Du driver en pizzeria med 50 beställningar per dag.
> Efter ett år vill du veta vilken pizza som säljs mest
> på fredagkvällar i november.
>
> Hur skulle du ha löst det *utan* databas?
> Vad hade du behövt ha sparat från dag ett?

---

# Nästa lektion

Vi installerar och kör en riktig databas.

Du skriver din första `SELECT`, `INSERT`, `UPDATE` och `DELETE`.

Det tar 45 minuter och fungerar på din dator idag.
