# Tentafrågor — Git fördjupning och Markdown

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — Merge-konflikter

Vad är en merge-konflikt och när uppstår den?

A) Den uppstår när två grenar har ändrat samma del av en fil och Git inte kan avgöra vilken version som gäller
B) Den uppstår alltid när man kör `git merge`, oavsett vad som ändrats
C) Den uppstår när man försöker pusha till en gren man inte har rättigheter till
D) Den uppstår när datorn är för trött och behöver en paus 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt — Git kan inte automatiskt välja mellan två olika ändringar på samma ställe och behöver din hjälp.
**B)** Fel — om grenarna inte berört samma kod sker sammanslagningen utan problem.
**C)** Fel — det är ett behörighetsproblem, inte en merge-konflikt.
**D)** Git vilar aldrig, men du kan behöva det efter att ha löst en riktig konflikt.

</details>

---

## Fråga 2 — git pull

Vad gör kommandot `git pull`?

A) Det laddar upp dina lokala ändringar till fjärrservern
B) Det hämtar senaste ändringarna från fjärrservern och slår ihop dem med din lokala gren
C) Det skapar en ny gren baserad på den senaste versionen
D) Det tar bort alla dina lokala filer och börjar om från noll 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Fel — det är `git push` som skickar dina ändringar till fjärrservern.
**B)** Rätt — `git pull` är en kombination av `git fetch` (hämta) och `git merge` (slå ihop).
**C)** Fel — nya grenar skapar man med `git branch` eller `git checkout -b`.
**D)** Det vore ett väldigt dåligt versionshanteringssystem — men nu vet du att det inte funkar så.

</details>

---

## Fråga 3 — Syftet med Markdown

Vad är syftet med Markdown?

A) Att skriva kod som körs direkt i webbläsaren
B) Att formatera text med enkel syntax som sedan kan renderas till HTML eller annat format
C) Att ersätta CSS för webbsidor
D) Att markera ner priset på mjukvaruutveckling 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Fel — Markdown är inte ett programmeringsspråk och körs inte som kod.
**B)** Rätt — Markdown låter dig skriva lättläst text med enkla tecken som `#`, `**` och `-` som sedan omvandlas till formaterad text.
**C)** Fel — CSS styr utseende på webbsidor; Markdown handlar om textstruktur.
**D)** Tyvärr — konsultpriserna påverkas inte av hur bra man är på Markdown.

</details>

---

## Fråga 4 — Fetstil i Markdown

Hur gör man text fet (bold) i Markdown?

A) `__text__` med enkla understreck på varje sida
B) `**text**` med dubbla asterisker på varje sida
C) `<b>text</b>` med HTML-taggar
D) Man skriver med VERSALER och hoppas på det bästa 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Fel — enkla understreck (`_text_`) ger kursiv stil, inte fet. Dubbla understreck (`__text__`) ger fet stil i vissa tolkare, men `**` är standardsättet.
**B)** Rätt — `**text**` är det vanligaste och mest portabla sättet att skriva fetstil i Markdown.
**C)** Fel — HTML-taggar fungerar i vissa Markdown-tolkare, men det är inte Markdown-syntax.
**D)** VERSALER GER BARA KÄNSLAN AV ATT NÅGON SKRIKER — inte fetstil.

</details>

---

## Fråga 5 — README.md

Vad är syftet med en README.md-fil i ett repo?

A) Den innehåller alla hemliga lösenord och API-nycklar för projektet
B) Den är en introduktionstext som förklarar vad projektet är, hur man kör det och vad man behöver veta
C) Den är en logg över alla commits som automatiskt uppdateras av Git
D) Den är obligatorisk för att Git ska fungera överhuvudtaget 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Fel — och om din README faktiskt innehåller lösenord: ändra dem nu, de är exponerade för alla.
**B)** Rätt — README.md är projektets välkomstsida. Den visas automatiskt på GitHub och berättar för andra (och ditt framtida jag) vad projektet gör.
**C)** Fel — commit-loggen hanteras av Git internt och visas med `git log`.
**D)** Fel — Git fungerar utan README.md, men ett repo utan en är som ett paket utan innehållsförteckning.

</details>

---
