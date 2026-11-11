# Tentafrågor — Databaser: fil, minne och server

Öva inför tentan. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Fildatabas

Vad är en fildatabas, till exempel SQLite?

A) En databas i datorns minne som försvinner vid omstart
B) En databas för filmer 🎬
C) En databas som lagras i en fil på disken, utan egen server
D) En databas som körs på en server i molnet

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Det är en minnesdatabas.
**B)** Fin tanke, men fil står för filen på disken.
**C)** Rätt. SQLite ligger i en `.db`-fil som programmet läser direkt.
**D)** Det är en databasserver.

</details>

---

## Fråga 2 — Minnesdatabas

Vad händer med datan i en minnesdatabas, som EF Core InMemory, när programmet stängs?

A) Den skickas till databasservern
B) Den försvinner
C) Den sparas automatiskt i en fil
D) Den går på semester 🏖️ och kommer tillbaka

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** En minnesdatabas har ingen server att skicka till.
**B)** Rätt. Datan finns bara så länge programmet körs. Bra för tester, dåligt för data du vill behålla.
**C)** Det är en fildatabas som gör det.
**D)** Den kommer inte tillbaka.

</details>

---

## Fråga 3 — Databasserver

Vad kännetecknar en databasserver, till exempel SQL Server eller MySQL?

A) Ett eget program som körs separat och som flera klienter kan koppla upp sig mot
B) Datorn där servitören sitter 🍽️
C) Databasen ligger som en fil bredvid ditt program
D) Den fungerar bara i datorns minne

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Programmen är klienter som pratar med servern, ofta över nätverk.
**B)** Servrar i matsalen och servrar i datorhallen är olika servrar.
**C)** Det är en fildatabas.
**D)** Servrar sparar normalt data på disk.

</details>

---

## Fråga 4 — Connection string

Vad innehåller en connection string?

A) En lista över alla tabeller
B) Uppgifter om hur programmet kopplar upp sig mot databasen, till exempel server, databas och inloggning
C) Själva datan i databasen
D) Ett snöre som håller datorn ihop 🧵

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Tabellerna finns i databasen, inte i connection string.
**B)** Rätt. Den berättar var databasen finns och hur du kommer in.
**C)** Datan ligger i databasen, inte i anslutningssträngen.
**D)** Det finns bara i sagor.

</details>

---

## Fråga 5 — Normalisering

Vad är normalisering?

A) Att dela upp data i flera tabeller så att samma sak inte lagras om och om igen
B) Att göra alla namn med stor bokstav
C) Att ta backup av databasen
D) Att få alla poster att bete sig normalt 🙂

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. En gäst som bor flera gånger ska ligga i en tabell, inte skrivas om vid varje bokning.
**B)** Det är formatering, inte normalisering.
**C)** Backup har ett annat syfte.
**D)** Normala poster, inte normalisering.

</details>
