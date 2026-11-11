# Tentafrågor — ORM, NoSQL och entiteter

Öva inför tentan. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — ORM

Vad är en ORM?

A) Ett program som tar backup på databasen
B) Ett verktyg som kopplar ihop objekt i C# med tabeller i en databas (Object-Relational Mapping)
C) Ett program skrivet i Python 🐍
D) Ett språk för att skriva SQL-frågor

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Backup är något annat. En ORM handlar om att koppla objekt till tabeller.
**B)** Rätt. EF Core är en ORM. Du jobbar med klasser och objekt, och ORM:en sköter tabellerna.
**C)** Orm betyder snake på svenska, men i programmering står ORM för Object-Relational Mapping.
**D)** Det är SQL självt. En ORM skriver ofta SQL åt dig.

</details>

---

## Fråga 2 — NoSQL

Vad är NoSQL?

A) En databas som bara fungerar i datorns minne
B) En databas som är sur på SQL 😠
C) Databaser som inte bygger på tabeller och rader, till exempel dokumentdatabaser som MongoDB
D) En gammal sorts databas som ingen använder längre

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Det är en minnesdatabas. NoSQL kan lagras både på disk och i minnet.
**B)** Inte sur, bara annorlunda.
**C)** Rätt. NoSQL är ett samlingsnamn för databaser som lagrar data på andra sätt än tabeller, till exempel som dokument.
**D)** Tvärtom, NoSQL används mycket idag.

</details>

---

## Fråga 3 — Entitet

Vad är en entitet?

A) En metod som sparar data i databasen
B) En sak i systemet som vi vill spara och hålla reda på, till exempel Guest. I C# är den oftast en klass
C) En kolumn i en tabell
D) En sorts spöke i koden 👻

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Metoder gör saker. En entitet är en sak.
**B)** Rätt. I Hotel Grandioso är Guest, Room och Booking entiteter. I databasen blir de tabeller.
**C)** Kolumnen är en property. Entiteten är hela tabellen.
**D)** Spöken hör till Halloween, inte till databaser.

</details>

---

## Fråga 4 — Dokument

Vad är ett dokument i NoSQL?

A) Ett SQL-kommando som hämtar data
B) En tabell med många kolumner
C) En post med data, ofta JSON-liknande, med nycklar och värden
D) Ett brev med frimärke ✉️

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** SQL-kommandon är frågor, inte posterna som sparas.
**B)** Det är SQL-världen. Ett dokument har inga fasta kolumner.
**C)** Rätt. Ett dokument i MongoDB liknar JSON: nyckel och värde, som `"Name": "Mira"`.
**D)** Posten är snabb, men inte så här.

</details>

---

## Fråga 5 — Dokument och rad

Vad motsvarar ett NoSQL-dokument i SQL?

A) En rad (row) i en tabell
B) En kolumn
C) Ett kvitto på hotellet 🧾
D) En tabell

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Ett dokument motsvarar en rad, och ett objekt i C# motsvarar båda.
**B)** En kolumn motsvarar ett fält eller en property.
**C)** Kvittot behåller vi, men det är inte svaret.
**D)** En tabell motsvarar en collection i NoSQL.

</details>

---

## Fråga 6 — Collection

Vad kallas en tabell i NoSQL (MongoDB)?

A) Collection
B) Column
C) Row
D) Bokhylla 📚

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. En collection är en samling dokument, ungefär som en tabell är en samling rader.
**B)** Column är en kolumn i SQL.
**C)** Row är en rad i SQL.
**D)** Fin bild, men fel ord. Det heter collection.

</details>
