# Övning — JOINs i klassrummet 🐾

🟢 Grundnivå · Kör i SQLite Browser med `djursjukhus_seed.sql`

Öppna filen `examples/djursjukhus_seed.sql` i SQLite Browser och importera databasen.
Skriv sedan SQL-frågorna nedan. Du ska inte gissa — data finns i filen, kör och kontrollera.

---

## Uppgift 1 — Lista alla djur med ägarnamn

Visa djurets namn, art och ägarens namn.
Sortera på ägarens namn.

### Förväntad output

```
Djur  | Art  | Agare
──────┼──────┼──────
Pluto | Hund | Sara
Ronja | Katt | Sara
Misse | Katt | Johan
Fido  | Hund | Lena
```

---

## Uppgift 2 — Alla besök med djurnamn och datum

Visa datum, djurets namn och orsaken till besöket.
Sortera på datum, äldst först.

### Förväntad output

```
Datum       | Djur  | Orsak
────────────┼───────┼────────────
2026-09-01  | Pluto | Klöv
2026-09-03  | Misse | Klöv
2026-09-08  | Pluto | Vaccination
2026-09-12  | Ronja | Vaccination
2026-09-15  | Fido  | Kontroll
2026-10-01  | Misse | Kontroll
```

---

## Uppgift 3 — Vilken ägare har flest djur?

Visa ägarens namn och antal djur. Sortera fallande.

### Förväntad output

```
Agare | AntalDjur
──────┼──────────
Sara  | 2
Johan | 1
Lena  | 1
```

---

## Uppgift 4 — Hitta djur utan besök

Använd LEFT JOIN. Visa namn på alla djur som **inte** haft ett enda besök.
(Med nuvarande data: inga — men frågan ska fungera om vi lägger till ett.)

### Tips

```sql
WHERE b.BesokId IS NULL
```

---

## Utmanande fråga

Visa fullständig besökshistorik: ägarnamn, djurnamn, art, datum och orsak.
Tre tabeller, två JOINs.
