# Hemövning — JOINs och aggregat 🏠

🟡 Mellannivå · Kör i SQLite Browser med `djursjukhus_seed.sql`

Dessa övningar är svårare än klassrumsversionerna.
Du ska kombinera JOIN, GROUP BY, HAVING och ibland subqueries.

---

## Uppgift 1 — Ägare med flera djur av samma art

Lista ägare som har **mer än ett djur av samma art**.
Visa ägarnamn, art och antal.

### Förväntad output

```
Agare | Art  | Antal
──────┼──────┼──────
Sara  | Katt | 1
```

*(Med nuvarande data: Sara har en hund och en katt — inga upprepningar.
Lägg till ett djur för att testa: `INSERT INTO Djur (Namn, Art, AgarId) VALUES ('Rex', 'Hund', 1);`)*

---

## Uppgift 2 — Senaste besöket per djur

Visa varje djurs **senaste** besöksdatum och orsak.
Sortera på senaste datum, nyast överst.

```
Djur  | Art  | SenasteBesok | Orsak
──────┼──────┼──────────────┼──────────
Misse | Katt | 2026-10-01   | Kontroll
Fido  | Hund | 2026-09-15   | Kontroll
Ronja | Katt | 2026-09-12   | Vaccination
Pluto | Hund | 2026-09-08   | Vaccination
```

---

## Uppgift 3 — Ägare som bara haft vaccinationsbesök

Lista ägare vars **alla** besök (via deras djur) har orsaken `Vaccination`.

### Tips

En approach: hitta ägare som INTE haft några icke-vaccinationsbesök.

---

## Uppgift 4 — Utöka databasen och testa

Lägg till en fjärde tabell: `Veterinar` med namn och specialitet.
Koppla varje besök till en veterinär.

```sql
-- Skapa och koppla
CREATE TABLE Veterinar (
    VetId       INTEGER PRIMARY KEY,
    Namn        TEXT NOT NULL,
    Specialitet TEXT NOT NULL
);

ALTER TABLE Besok ADD COLUMN VetId INTEGER REFERENCES Veterinar(VetId);
```

Seed med tre veterinärer och uppdatera besöken.
Skriv sedan en fråga som visar hur många besök varje veterinär haft, per art.

---

## Uppgift 5 — GDPR-reflektion (skriftlig)

Djursjukhusets databas lagrar ägarnas namn, ålder och telefonnummer.

Svara i kommentarer i din SQL-fil:

1. Vilken laglig grund har djursjukhuset för att lagra ägaruppgifter?
2. Vad händer om en ägare kräver att bli raderad — men deras djur fortfarande är aktiva patienter?
3. Hur länge bör besökshistoriken sparas?
