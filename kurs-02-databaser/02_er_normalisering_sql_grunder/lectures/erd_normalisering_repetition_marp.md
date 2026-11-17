---
marp: true
theme: nion_dark
paginate: true
title: ERD & Normalisering — från kaos till tabeller
description: Visuell genomgång av hur man går från platt data till normaliserade tabeller och ERD
---

<!-- _class: lead -->
<!-- _paginate: false -->

# Del 2 — ERD & Normalisering

Från ett enda kort till tre tabeller

---

## Vi börjar med en låda

<div style="border: 3px solid #e67e22; border-radius: 12px; padding: 24px; font-size: 22px; margin-top: 16px;">

**📦 Order 1001**

Kalle Svensson · Södra Gatan 23 · Myskoby · Sverige

🎮 The Witcher 3 · 299 kr · 2026-10-17

</div>

Allt på ett ställe. Ser rimligt ut — tills det kommer fler ordrar.

---

## Fler ordrar dyker upp

<div style="border: 3px solid #e67e22; border-radius: 12px; padding: 18px; font-size: 19px; margin-bottom: 12px;">

**Order 1001** · Kalle Svensson · Södra Gatan 23 · Myskoby · Sverige · The Witcher 3 · 299 kr

</div>

<div style="border: 3px solid #e67e22; border-radius: 12px; padding: 18px; font-size: 19px; margin-bottom: 12px;">

**Order 1002** · <span style="background:#c0392b;color:#fff;padding:2px 6px;border-radius:4px;">Kalle Svensson · Södra Gatan 23 · Myskoby · Sverige</span> · Minecraft · 199 kr

</div>

<div style="border: 3px solid #e67e22; border-radius: 12px; padding: 18px; font-size: 19px;">

**Order 1003** · Anna Karlsson · Norra Vägen 7 · Storstad · Sverige · <span style="background:#c0392b;color:#fff;padding:2px 6px;border-radius:4px;">Minecraft · 199 kr</span>

</div>

> Rött = upprepat. Kalle syns två gånger. Minecraft syns två gånger.

---

## Vad händer nu?

**Scenario 1:** Kalle Svensson byter adress till Östra Allén 5.

→ Du måste leta upp **alla hans ordrar** och uppdatera adressen i varje rad.

**Scenario 2:** The Witcher 3 sätts på rea — nytt pris: 99 kr.

→ Du måste hitta **varje rad** som har det spelet och ändra priset.
→ Missar du en rad? Nu har databasen två olika priser på samma spel.

**Scenario 3:** Du vill ta bort ett spel ur katalogen.

→ Raderar du raden tar du också bort ordern — och kunduppgifterna med den.

> Det här kallas **uppdateringsanomalier**. Normalisering löser dem.

---

## Lösningen: bryt upp i separata lådor

<div style="display: flex; gap: 20px; margin-top: 24px; font-size: 20px;">

<div style="border: 3px solid #3498db; border-radius: 10px; padding: 16px; flex: 1;">

**👤 Kund**

KundId
Namn
Adress
Stad
Land

</div>

<div style="border: 3px solid #2ecc71; border-radius: 10px; padding: 16px; flex: 1;">

**🎮 Spel**

SpelId
Titel
Pris

</div>

<div style="border: 3px solid #9b59b6; border-radius: 10px; padding: 16px; flex: 1;">

**🛒 Order**

OrderId
KundId →
SpelId →
Datum

</div>

</div>

> `→` betyder: pekar på en annan tabell. Kalle's data finns **på ett enda ställe**.

---

## Pilar — vad betyder de?

<div style="display: flex; gap: 24px; margin-top: 20px; font-size: 20px; align-items: flex-start;">

<div style="flex: 1;">

```
KUND ──────── ORDER
  1              Många
```

En kund kan lägga **många** ordrar.
En order tillhör **en** kund.

</div>

<div style="flex: 1;">

```
SPEL ──────── ORDER
  1              Många
```

Ett spel kan dyka upp i **många** ordrar.
En order gäller **ett** spel.

</div>

</div>

> Det kallas **1:N-relation** (en-till-många).
> En kund = 1. Kundens ordrar = N (ett godtyckligt antal).

---

## ERD — Entity Relationship Diagram

<div style="display:flex; align-items:center; justify-content:center; gap:12px; margin-top:20px; font-size:18px;">

<div style="border:3px solid #3498db; border-radius:10px; padding:14px 18px; min-width:130px;">

**👤 KUND**
<hr style="border-color:#3498db; margin:6px 0"/>
🔑 KundId
Namn
Adress
Stad
Land

</div>

<div style="text-align:center; font-size:26px; color:#aaa;">
  <div>1</div>
  <div>──────</div>
  <div style="font-size:20px; color:#e67e22;">lägger</div>
  <div>──────</div>
  <div>N</div>
</div>

<div style="border:3px solid #9b59b6; border-radius:10px; padding:14px 18px; min-width:130px;">

**🛒 ORDER**
<hr style="border-color:#9b59b6; margin:6px 0"/>
🔑 OrderId
🔗 KundId
🔗 SpelId
Datum

</div>

<div style="text-align:center; font-size:26px; color:#aaa;">
  <div>N</div>
  <div>──────</div>
  <div style="font-size:20px; color:#e67e22;">gäller</div>
  <div>──────</div>
  <div>1</div>
</div>

<div style="border:3px solid #2ecc71; border-radius:10px; padding:14px 18px; min-width:130px;">

**🎮 SPEL**
<hr style="border-color:#2ecc71; margin:6px 0"/>
🔑 SpelId
Titel
Pris

</div>

</div>

> 🔑 = primärnyckel · 🔗 = främmande nyckel (pekar på annan tabell)

---

## Från ERD till tabeller — Kund

```sql
CREATE TABLE Kund (
    KundId  INTEGER PRIMARY KEY,
    Namn    TEXT    NOT NULL,
    Adress  TEXT,
    Stad    TEXT,
    Land    TEXT    DEFAULT 'Sverige'
);
```

Kalle finns nu **en gång**. Byter han adress — en enda UPDATE.

```sql
UPDATE Kund SET Adress = 'Östra Allén 5' WHERE KundId = 1;
```

Alla hans ordrar pekar fortfarande rätt — vi ändrade inget i dem.

📖 [intro_databaser.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/intro_databaser.md)

---

## Från ERD till tabeller — Spel

```sql
CREATE TABLE Spel (
    SpelId  INTEGER PRIMARY KEY,
    Titel   TEXT    NOT NULL,
    Pris    REAL    NOT NULL
);
```

The Witcher 3 finns **en gång**. Ny prissättning — en enda UPDATE.

```sql
UPDATE Spel SET Pris = 99.00 WHERE Titel = 'The Witcher 3';
```

Alla ordrar som innehåller det spelet speglar direkt det nya priset — via `JOIN`.

📖 [sqlite_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sqlite_intro.md)

---

## Från ERD till tabeller — Order

```sql
CREATE TABLE Order_ (
    OrderId  INTEGER PRIMARY KEY,
    KundId   INTEGER NOT NULL,
    SpelId   INTEGER NOT NULL,
    Datum    TEXT    NOT NULL,
    FOREIGN KEY (KundId) REFERENCES Kund(KundId),
    FOREIGN KEY (SpelId) REFERENCES Spel(SpelId)
);
```

Kalle köper Minecraft:

```sql
INSERT INTO Order_ (KundId, SpelId, Datum) VALUES (1, 2, '2026-10-17');
```

Ingen adress. Inget pris. Bara pekarna. Allt annat hämtas via JOIN.

📖 [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md)

---

## JOIN — peka och hämta

Nu ligger data på tre ställen. `JOIN` sätter ihop bitarna via nycklarna.

```sql
FROM Order_ o
INNER JOIN Kund k ON o.KundId = k.KundId
INNER JOIN Spel s ON o.SpelId = s.SpelId
```

- `INNER JOIN` tar bara rader som har en match i **båda** tabellerna
- `ON` säger *hur* de hänger ihop — vilken nyckel som pekar på vad
- En `JOIN` är också ett **filter**: ordrar utan giltig kund syns inte

> **LEFT JOIN** — tar med **alla** rader från vänster tabell, `NULL` om ingen match:
> `FROM Kund k LEFT JOIN Order_ o ON o.KundId = k.KundId`
> — visar kunder som aldrig lagt en order

---

## Hämta allt med JOIN

```sql
SELECT
    k.Namn,
    k.Adress,
    s.Titel,
    s.Pris,
    o.Datum
FROM Order_ o
INNER JOIN Kund k ON o.KundId = k.KundId
INNER JOIN Spel s ON o.SpelId = s.SpelId;
```

**Resultat:**

| Namn | Adress | Titel | Pris | Datum |
|------|--------|-------|------|-------|
| Kalle Svensson | Södra Gatan 23 | The Witcher 3 | 299 | 2026-10-17 |
| Kalle Svensson | Södra Gatan 23 | Minecraft | 199 | 2026-10-17 |
| Anna Karlsson | Norra Vägen 7 | Minecraft | 199 | 2026-10-17 |

> Ser ut som lådan vi började med — men nu lagras det smart.

---

## Vad vi vann

| Förut (flat) | Nu (normaliserat) |
|---|---|
| Kalle's adress på varje orderrad | Kalle's adress på ett ställe |
| Ändra pris = uppdatera alla rader | Ändra pris = en UPDATE i Spel |
| Ta bort spel = förstör orderhistorik | Spel och ordrar är separata |
| Svårt att ställa frågor | JOIN ger oss allt vi behöver |

> En bra databasdesign gör framtida frågor möjliga — inte bara de du tänkte på idag.

---

<!-- _class: lead -->
<!-- _paginate: false -->

# Rita innan du kodar

Papper → Lådor → Pilar → SQL

Det tar 5 minuter och sparar 2 timmar av omskrivning.

---

## Resurser

| Ämne | Fil |
|------|-----|
| Vad är en databas? | [intro_databaser.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/intro_databaser.md) |
| SQL-frågor och JOIN | [sql_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sql_intro.md) |
| SQLite — CREATE TABLE | [sqlite_intro.md](https://github.com/marcusjobb/YRGO-CLO26-HT26Kursmaterial/blob/main/kurs-02-databaser/01_intro_databaser/notes/sqlite_intro.md) |

> Normalisering fördjupas nästa vecka — det vi gjort idag är intuitionen bakom det.
