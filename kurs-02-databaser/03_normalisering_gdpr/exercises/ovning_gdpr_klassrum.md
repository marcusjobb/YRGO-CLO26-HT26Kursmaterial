# Övning — GDPR i klassrummet 🔒

🟢 Grundnivå · Diskussion + SQL

Jobba i par. Ni ska inte googla lagtext — använd det ni just gick igenom.

---

## Scenario: Gymappen

Ett gym har byggt en app för att boka pass. Databasen ser ut så här:

```sql
CREATE TABLE Medlemmar (
    MedlemId    INTEGER PRIMARY KEY,
    Namn        TEXT,
    Email       TEXT,
    Telefon     TEXT,
    Personnr    TEXT,
    Hälsotillst TEXT,   -- t.ex. "hjärtproblem, diabetes"
    Lösenord    TEXT,   -- klartext
    Skapad      TEXT    -- datum för registrering
);

CREATE TABLE Bokningar (
    BokId     INTEGER PRIMARY KEY,
    MedlemId  INTEGER,
    Pass      TEXT,
    Datum     TEXT,
    Frånvaro  TEXT     -- anledning om de inte dök upp
);
```

---

## Uppgift 1 — Hitta problemen

Lista **minst fyra** GDPR-problem i databasen ovan.
Koppla varje problem till rätt princip (dataminimering, integritet, etc.)

Skriv i ett kodblock eller i punktform.

---

## Uppgift 2 — Designa om

Skriv en förbättrad `CREATE TABLE` för `Medlemmar`.
Ta bort det som inte behövs. Byt ut det som är fel.

Motivera varje val med en kommentar i SQL:

```sql
CREATE TABLE Medlemmar (
    -- förklara varje kolumn här
);
```

---

## Uppgift 3 — Rätten att bli glömd

En medlem vill raderas. Skriv SQL som tar bort **all** data om personen.
Tänk på vad som händer med deras bokningar.

```sql
-- Skriv er lösning här
```

**Diskutera:** Finns det något ni måste behålla trots allt? (Tänk bokföring.)

---

## Utmanande fråga

Gymmet vill börja lagra om en person kom till passet eller inte — och varför de uteblev om de inte kom.

Är det lagligt? Vilken laglig grund krävs? Vad behöver de göra innan de kan lagra det?
