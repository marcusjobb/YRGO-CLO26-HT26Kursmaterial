# Tentafrågor — refaktorering, kodkvalitet och Kanban

Öva inför tentan. Varje fråga har fyra alternativ: ett rätt, ett lite roligt och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Kanban

Varför ska du begränsa antalet kort i Working?

A) Så att alla kort hinner bli Done på en dag

B) Så att det syns när något fastnar, till exempel i Test

C) För att Kanban inte fungerar utan tak 🙃

D) Så att Backlog blir tom snabbare


<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Det är inte målet. Målet är ett jämnt flöde.

**B)** Rätt. Begränsningen gör flaskhalsar synliga.

**C)** Kanban kan fungera utan ett fast tak, men begränsningen ger ett tydligt flöde.

**D)** Backlog påverkas inte av begränsningen i Working på det sättet.

</details>

---

## Fråga 2 — Code smells

Vad är ett code smell?

A) Kod som är skriven i ett språk som inte längre används

B) Kod som luktar bränt efter en lång kodgranskning ☕

C) Ett tecken på att koden kan bli bättre, även om den fungerar

D) Kod som kraschar varje gång den körs


<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Det kan vara ett problem, men det är inte vad ett smell betyder.

**B)** Ha ha, men nej. Det är ingen teknisk term.

**C)** Rätt. Koden kan fungera och ändå vara svår att läsa och ändra.

**D)** Det är en bugg. Ett smell fungerar ofta, men är svårt att förstå eller ändra.

</details>

---

## Fråga 3 — Extract Method

Vad gör Extract Method?

A) Flyttar en del av en metod till en ny metod med ett beskrivande namn

B) Tar bort metoder som inte används

C) Flyttar koden till en annan dator 🖥️

D) Kopierar en metod till en annan klass


<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. Koden blir lättare att läsa, och beteendet ska vara detsamma.

**B)** Det kallas att ta bort död kod. Extract Method lägger till en ny metod.

**C)** Förvirrande, men det är inte vad metoden gör.

**D)** Det är inte en refaktorering, och det skapar dubbelkod, som är ett smell.

</details>

---

## Fråga 4 — SoC

Vad är skillnaden mellan SRP och SoC?

A) SoC är när man delar upp kaffet och kakan 🍰

B) SRP gäller en klass, SoC gäller hur programmets delar är uppdelade, till exempel gränssnitt, logik och data

C) SoC betyder att alla klasser ska ha samma ansvar

D) SRP och SoC betyder samma sak


<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Gott, men inte rätt svar.

**B)** Rätt. SRP är på klassnivå, och SoC är på programnivå.

**C)** Tvärtom: SoC handlar om att skilja ansvar från varandra.

**D)** De är närbesläktade, men de verkar på olika nivåer.

</details>

---

## Fråga 5 — DRY

Vad betyder DRY?

A) Varje kunskap ska finnas på ett enda ställe

B) Dokumentera allt i kommentarer

C) Repetera varje lektion två gånger 🔁

D) Skriv tester innan du skriver koden


<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. När regeln finns på två ställen glöms den ena när den andra ändras.

**B)** Kommentarer är viktiga, men DRY handlar om upprepning av kod och kunskap.

**C)** Bra för inlärning, men det är inte vad DRY betyder.

**D)** Det är Test-Driven Development, inte DRY.

</details>
