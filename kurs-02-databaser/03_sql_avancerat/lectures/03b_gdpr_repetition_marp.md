---
marp: true
theme: nion-dark
paginate: true
title: GDPR — snabbrepetition
description: 10-minutersrepetition av GDPR inför onsdagens fördjupning
---

<!-- _class: lead -->
<!-- _paginate: false -->

# GDPR — snabbrepetition 🔒

*Marcus Ackre Medina · CLO26 · v.43*

---

## Sex principer — kan du dem?

Personuppgifter ska vara:

1. **Lagliga och transparenta** — du behöver en laglig grund
2. **Ändamålsbegränsade** — samla bara för ett specifikt syfte
3. **Minimerade** — bara det du faktiskt behöver
4. **Korrekta** — håll dem uppdaterade
5. **Lagringsminimerade** — radera när du inte längre behöver dem
6. **Säkra** — skydda mot obehörig åtkomst

---

## Vad är en personuppgift?

Allt som kan kopplas till en levande person:

✅ Namn · e-post · personnr · IP-adress · foto  
✅ Hälsouppgifter · politiska åsikter · biometri ← **känsliga (Art. 9)**

❌ Anonymiserad data — går inte längre att koppla till en person

> Pseudonymisering (t.ex. krypterat personnr) är **fortfarande** personuppgift.

---

## Vad kräver GDPR av din databas?

```sql
-- ❌ Lagra bara det du behöver
Lösenord TEXT,          -- klartext → kryptera med bcrypt
Hälsotillst TEXT,       -- känslig uppgift → särskild laglig grund krävs
FöddÅr INTEGER,         -- behöver du hela åldern, eller räcker det?

-- ✅ Designa för dataminimering från dag ett
PasswordHash TEXT,      -- aldrig klartext
SkapadDatum DATE,       -- när ska raden raderas?
```

**Rätten att bli glömd (Art. 17):** användaren kan kräva att du raderar deras data.  
Din databas måste kunna göra det — utan att ta ner hela systemet.

---

## Snabbfrågor — räck upp handen

1. Du bygger en gymapp. Behöver du lagra personnummer?
2. En användare vill bli raderad. Du tar bort raden i `Users` — klart?
3. Du loggar alla IP-adresser i en tabell. Är det okej?
4. `appsettings.json` med connectionstring pushas till GitHub. GDPR-problem?

---

<!-- _class: lead -->

## En mening var

Vad är **dataminimering**?

*(Prata med den bredvid dig — 60 sekunder)*
