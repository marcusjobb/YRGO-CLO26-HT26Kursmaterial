---
marp: true
theme: nion_dark
paginate: true
title: GDPR och databaser
description: Vad GDPR kräver av oss som bygger system med personuppgifter — och hur databasdesign och normalisering hänger ihop med lagen.
---

<!-- _class: lead -->
<!-- _paginate: false -->

# GDPR och databaser 🔒

Lagen om personuppgifter — och vad den kräver av oss

*Marcus Ackre Medina · CLO26 · v.43*

---

## Vad är GDPR?

**General Data Protection Regulation** — EU:s dataskyddsförordning, gäller sedan 2018.

Den reglerar hur **personuppgifter** får samlas in, lagras och användas.

Som utvecklare är det **ditt ansvar** att systemen du bygger följer lagen.
"Jag visste inte" är inte ett juridiskt argument.

---

## Vad är en personuppgift?

Allt som direkt eller indirekt kan identifiera en levande person.

```
Direkta:          Indirekta (kombinerat):
──────────────    ─────────────────────────
Namn              IP-adress + tidpunkt
E-postadress      Postnummer + ålder + kön
Personnummer      Användarnamn
Telefonnummer     Enhets-ID
Foto              Platsdata
```

**Tumregel:** Om du kan koppla det till en specifik person — är det en personuppgift.

---

## Sex principer (Art. 5)

GDPR kräver att personuppgifter hanteras enligt dessa principer:

| Princip | Vad det betyder i praktiken |
|---------|----------------------------|
| **Laglig grund** | Du måste ha en anledning att lagra datan |
| **Ändamålsbegränsning** | Samla bara det du faktiskt behöver |
| **Dataminimering** | Lagra inte mer än nödvändigt |
| **Riktighet** | Håll datan korrekt och uppdaterad |
| **Lagringsbegränsning** | Radera när du inte behöver den längre |
| **Integritet och konfidentialitet** | Skydda mot obehörig åtkomst |

---

## Laglig grund — sex alternativ

Du måste ha **minst ett** av dessa för att lagra personuppgifter:

1. **Samtycke** — personen har aktivt godkänt
2. **Avtal** — nödvändigt för att fullfölja ett kontrakt
3. **Rättslig förpliktelse** — lagen kräver det (t.ex. bokföring)
4. **Skydda vitala intressen** — nödsituationer
5. **Allmänt intresse** — myndighetsuppgifter
6. **Berättigat intresse** — avvägt mot den registrerades intresse

De flesta applikationer vi bygger använder **samtycke** eller **avtal**.

---

## Vad GDPR kräver av din databas

```sql
-- ❌ Lagra aldrig mer än du behöver
CREATE TABLE Kunder (
    KundId    INT PRIMARY KEY,
    Namn      TEXT,
    Email     TEXT,
    Personnr  TEXT,   -- Behöver du verkligen detta?
    Religion  TEXT,   -- Känslig uppgift — kräver extra skydd
    Hälsadata TEXT    -- Känslig uppgift — kräver extra skydd
);

-- ✅ Lagra bara det som behövs för ändamålet
CREATE TABLE Kunder (
    KundId  INT PRIMARY KEY,
    Namn    TEXT NOT NULL,
    Email   TEXT NOT NULL UNIQUE
);
```

---

## Känsliga uppgifter (Art. 9)

Dessa kräver **explicit samtycke** och extra skydd:

- Hälsotillstånd och medicinska uppgifter
- Politiska åsikter
- Religiös övertygelse
- Etniskt ursprung
- Sexuell läggning
- Genetiska och biometriska data
- Fackmedlemskap

**Djursjukhuset:** Diagnos och medicinering för ett djur är inte känsliga uppgifter.
Men ägarens hälsotillstånd — om det dök upp — vore det.

---

## Rätten att bli glömd (Art. 17)

En användare kan kräva att du raderar alla uppgifter om dem.

```sql
-- Rätt: radera kunden och allt kopplat (cascade)
DELETE FROM Kunder WHERE KundId = 42;
-- Med ON DELETE CASCADE tas ordrar, adresser etc. bort automatiskt

-- Fel: bara dölja i UI men behålla i databasen
UPDATE Kunder SET Aktiv = 0 WHERE KundId = 42;
-- ☝ Uppgifterna finns kvar — det räcker inte
```

**Undantag:** Bokföring, rättsliga krav. Då anonymiserar du istället.

---

## Anonymisering vs pseudonymisering

```
Anonymisering:
  Namn: "Anna Andersson"  →  borttaget för alltid
  Går inte att återskapa — faller utanför GDPR

Pseudonymisering:
  Namn: "Anna Andersson"  →  AnvId: a3f9c2
  Kan återkopplas med en separat nyckel
  Fortfarande personuppgift — skyddas av GDPR
```

**Anonymisering** är den enda metoden som gör att GDPR inte längre gäller.

---

## Normalisering och GDPR

En normaliserad databas **underlättar** GDPR-efterlevnad.

**Utan normalisering:**
```
Köparen Anna Andersson, anna@mail.com, 073-111 2233 finns på 47 rader.
Rätten att bli glömd = uppdatera 47 rader.
Risk att missa någon.
```

**Med normalisering:**
```sql
DELETE FROM Kunder WHERE KundId = 1;
-- Anna försvinner från hela systemet via CASCADE.
-- En rad. Noll risk att missa.
```

Redundans är inte bara ett tekniskt problem — det är ett juridiskt problem.

---

## Säkerhet i praktiken

```sql
-- Lagra aldrig lösenord i klartext
-- ❌
INSERT INTO Anvandare (Email, Losenord) VALUES ('anna@mail.com', 'katt123');

-- ✅ Lagra alltid en hash (görs i appkoden, inte i SQL)
INSERT INTO Anvandare (Email, LosenordHash) VALUES ('anna@mail.com', '$2b$12$...');
```

Andra åtgärder:
- Kryptera känsliga kolumner
- Begränsa databas-access (minsta möjliga rättigheter)
- Logga vem som läser/skriver personuppgifter
- Ha en backup-strategi med kryptering

---

## Privacy by design

GDPR kräver att dataskydd byggs **in från start** — inte läggs till i efterhand.

Ställ frågorna **innan** du skapar tabellen:

1. Varför behöver vi den här uppgiften?
2. Hur länge behöver vi den?
3. Vem ska ha tillgång till den?
4. Hur raderar vi den när vi inte behöver den längre?

Om du inte kan svara på frågorna — lagra inte uppgiften.

---

## Sammanfattning

| Krav | Lösning i databasen |
|------|---------------------|
| Dataminimering | Lagra bara nödvändiga kolumner |
| Rätten att bli glömd | ON DELETE CASCADE + testa det |
| Riktighet | Uppdatera, normalisera — en källa för sanning |
| Integritet | Kryptering, hashing, minimal access |
| Lagringsbegränsning | Automatisk radering efter X tid |

*Nästa: GDPR-övning med ett verkligt scenario.*
