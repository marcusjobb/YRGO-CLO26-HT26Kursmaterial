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

## GDPR i vardagen — hushållsundantaget

GDPR gäller **inte** för rent privat behandling (Art. 2.2c).

```
✅ Privat adressbok på din telefon
✅ Anteckningar om familjen i ett privat dokument
✅ Fotomapp på din dator som bara du ser

❌ Dela datan med ett företag (Ancestry, Google, Apple)
❌ Publicera på en öppen webbsida
❌ Använda i en app du gör tillgänglig för andra
```

**Regeln:** Så fort något lämnar din privata sfär — gäller GDPR.

---

## Databas med familj och kompisar

Får man bygga en databas med familjemedlemmar och vänner?

**Privat (hushållsundantaget):**
> "Ja, en kontaktlista som bara du ser — det är OK."

**Men tänk på det här:**
- Lagrar du hälsoinformation? → Känslig uppgift, Art. 9
- Synkar den till molnet? → Datan lämnar din kontroll
- Delar du med en app? → Appen behandlar andras uppgifter

**Familjeträd:**
- Deceased (döda) personer → GDPR skyddar bara **levande** personer
- Levande släktingar → de kan kräva att du tar bort deras uppgifter
- Publicerat online → hushållsundantaget gäller **inte** längre

---

## Foton på sociala medier — vad säger lagen?

**IMY (Integritetsskyddsmyndigheten) är tydliga:**

```
Privat grupp eller direktmeddelande
→ Troligen OK under hushållsundantaget

Öppet Instagram-konto / Facebook utan sekretess
→ Offentlig publicering = utanför hushållsundantaget
→ Du behöver laglig grund för att publicera
```

De flesta använder **berättigat intresse** — men det måste vägas mot
den fotograferades rätt till privatliv.

**Vad krävs i praktiken:**
- Samtycke från de i bilden, **eller**
- Tydlig berättigad grund som väger tyngre än deras intresse

*Källa: IMY — Personuppgifter på nätet (2023)*

---

## Google Photos — biometrisk data (Art. 9)

När Google Photos grupperar bilder på samma person använder de
**ansiktsigenkänning** — det är **biometrisk data**.

Biometrisk data = känslig uppgift (Art. 9).

```
Du godkänner → Google får behandla DITT ansikte
Men de andra i bilderna?
→ De har inte samtyckt till att Google behandlar deras ansikten
```

**I EU/Sverige:**
- "Ansiktsgrupperingen" är **opt-in** — Google frågar dig
- IMY har fått klagomål om just detta (pågående utredningar)
- Att ladda upp ett foto ≠ samtycke för ansiktsigenkänning av alla i bilden

**Principen:** Du kan ge samtycke för din egen data — **inte** andras.

---

## Diskussionsfrågor — vad säger lagen egentligen?

Jobba i par, 5 minuter. Inga facit — diskutera och ta ställning.

1. **Kompisappen:** Du bygger en app som lagrar namn och födelsedag på dina vänner. Du synkar den till din privata iCloud. Gäller GDPR?

2. **Familjeträdet:** Du publicerar ett familjeträd på en öppen webbsida. Din levande farmor finns med. Vad har hon för rättigheter?

3. **Festfotot:** Du lägger upp ett foto från en fest på din öppna Instagram. Tre kompisar syns. Krävs deras samtycke?

4. **Skolprojektet:** Du bygger en app med en databas över kursdeltagare som skoluppgift. Appen läggs upp på GitHub. Vad måste du tänka på?

**Research-fråga:** Sök på IMY:s webbplats — vad är Dataskyddsombudet (DPO) och när krävs ett sådant?

---

## Fotoalbum med kompisar online

**Publikt album — alla kan se det:**
→ Nej, inte utan samtycke från alla i bilderna.
Bilder är personuppgifter. Hushållsundantaget gäller inte vid offentlig publicering.

**Privat album — bara kompisarna ser det:**
→ Troligen OK, men du litar på plattformen.
Plattformens servrar behandlar bilderna — de är inte längre bara "dina".
Välj en tjänst med GDPR-avtal och tydlig datapolicy.

**Tumregel:** Kan en okänd se det? → Samtycke krävs.

---

## GDPR i praktiken — snabbfrågor

**En kund vill bli anonymiserad en vecka efter köpet. OK?**
→ Delvis. Kontaktuppgifter: radera. Bokföringsdata: sparas 7 år (lag).
Kunden kan inte kräva att du bryter mot bokföringslagen.

**Anonymiserad kund vill ha garantireparation. Hur?**
→ Kunden visar sitt kvitto. Du kan verifiera köpet utan personuppgifter i systemet.
Anonymisering tar bort deras konto — inte deras rätt till garanti.

**Kund anonymiserades, vill nu lämna tillbaka varan. Möjligt?**
→ Ja, med kvitto. Men automatiken är borta — det hanteras manuellt.
Informera kunden *innan* anonymisering att framtida ärenden kräver kvitto.

**En okänd ringer och frågar vad du har sparat om kund X.**
→ Utlämnas inte. Tredje part utan samtycke = brott mot Art. 5.

**En kund vill ha utdrag över all data ni har om dem. Måste ni ge det?**
→ Ja. Art. 15 — rätten till tillgång. 30 dagar på sig att leverera.
Allt: namn, adress, orderhistorik, loggar, cookies — om det kan kopplas till personen.

*Längre förklaringar: `notes/gdpr_praktik_scenarier.md`*

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
