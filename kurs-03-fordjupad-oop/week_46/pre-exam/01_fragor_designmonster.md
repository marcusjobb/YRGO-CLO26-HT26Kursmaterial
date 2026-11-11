# Tentafrågor — designmönster och UML

Öva inför tentan. En fråga per mönster, och en om UML. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Builder

Vad är Builder bra för?

A) Att bygga sandslott på stranden 🏖️ och se hur länge de står
B) Att bygga ett objekt med många egenskaper steg för steg, där steg kan utelämnas
C) Att bestämma vilken klass som ska skapas utifrån ett värde
D) Att bara tillåta ett enda objekt av en klass

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Sandslott är en byggare, men inte ett designmönster.
**B)** Rätt. Rätt. Mira får sina egenskaper en mening i taget, och det som inte sägs får ett rimligt värde.
**C)** Det är Factory. Builder bygger ett objekt, Factory väljer vilket objekt.
**D)** Det är Singleton. Builder har inget med antal objekt att göra.

</details>

---

## Fråga 2 — Observer

När används Observer?

A) När ett objekt ska meddela flera andra objekt när något har hänt, utan att känna till dem i förväg
B) När en klass ska byta beteende under körning
C) När ett objekt ska byggas steg för steg
D) När din dator ska bli sur på dig 😠

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Rätt. Hero säger till, och varje lyssnare bestämmer själv vad den gör med det.
**B)** Det är Strategy. Observer handlar om att meddela, inte om att byta beteende.
**C)** Det är Builder. Observer handlar om meddelanden, inte om att bygga objekt.
**D)** Datorer blir inte sura. Observer är bara ett mönster.

</details>

---

## Fråga 3 — UML

Vad visar ett klassdiagram som ett ERD inte visar?

A) Vilka tabeller som finns i databasen
B) Metoder, alltså vad klassen kan göra
C) Vilken färg klassen har när den är glad 🌈
D) Hur många rader varje tabell har

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det visar ett ERD. Tabeller är det ett ERD är till för.
**B)** Rätt. Rätt. En tabell har bara data, och ett ERD visar därför inga metoder.
**C)** Klasser är inte glada, och färger ingår inte i UML-notationen.
**D)** Varken klassdiagram eller ERD visar antal rader. Det är data, inte struktur.

</details>

---

## Fråga 4 — Factory

Vad gör en Factory?

A) Samlar skapandet av objekt på ett ställe, så att anroparen inte behöver veta vilken klass som används
B) Låter ett objekt notifiera många andra när något ändras
C) Serverar gröt åt hjältarna 🥣 på kvällen
D) Bygger ett komplext objekt steg för steg med en byggare

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Rätt. Monster skapas i en fabrik, och resten av programmet beställer dem.
**B)** Det är Observer. Factory skapar objekt, den meddelar ingen.
**C)** Gröt är en fabrik, men inte en Factory i programmeringsmening.
**D)** Det är Builder. Factory väljer vilken klass som skapas, Builder bygger ihop ett objekt.

</details>

---

## Fråga 5 — Strategy

Vad är poängen med Strategy-mönstret?

A) Att Mira bestämmer sig för att byta frisyr 💇 när hon blir tråkig
B) Att se till att det bara finns ett enda objekt av en klass i hela programmet
C) Att byta beteende genom att byta objekt, utan att ändra klassen som använder det
D) Att skapa objekt utan att anroparen vet vilken klass som används

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Lika omväxlande som en Strategy, men inte samma sak.
**B)** Det är Singleton. Strategy handlar om att byta beteende, inte om att begränsa antal objekt.
**C)** Rätt. Rätt. Mira byter vapen med en enda rad, och Hero ändras inte.
**D)** Det är Factory. Strategy handlar om vilket sätt något görs på, inte om att skapa objekt.

</details>
