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

## Fråga 6 — Rubrik i Markdown

Hur skapar man en rubrik på nivå 2 (H2) i Markdown?

A) `## Rubriktext`
B) `<h2>Rubriktext</h2>`
C) `**Rubriktext**`
D) Man skriver rubriken och hoppas att den ser viktig ut av sig själv 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**A)** Rätt. `##` ger H2. Ett `#` ger H1 (sidan/dokumentets titel), `##` ger H2 (sektion), `###` ger H3 (undersektion).
**B)** HTML-syntax fungerar i vissa tolkare men är inte Markdown — undvik det när Markdown räcker.
**C)** `**text**` ger fetstil, inte en rubrik.
**D)** Texten ser viktig ut men renderas som vanlig brödtext — ingen H2.

</details>

---

## Fråga 7 — Punktlista i Markdown

Hur skapar man en punktlista med tre varor i Markdown?

A) Skriv varorna med radbrytning — Markdown tolkar varje ny rad som ett listitem
B) Börja varje rad med `-` (eller `*`) följt av ett mellanslag
C) Omge listan med `<ul>` och `</ul>` och varje item med `<li>`
D) Lista dem med semikolon: `Äpple; Bröd; Mjölk` 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Vanliga radbrytningar i Markdown slås ihop till ett stycke — de skapar inte en lista.
**B)** Rätt. Varje rad som börjar med `- ` eller `* ` blir ett listitem:
```
- Äpple
- Bröd
- Mjölk
```
**C)** HTML fungerar men är inte Markdown-syntax.
**D)** Semikolon ger ingen lista — bara en lång rad text med semikolon.

</details>

---

## Fråga 8 — Vad ska ett commit-meddelande innehålla?

Vad är ett bra commit-meddelande?

A) Datum och klockslag — Git vet ändå inte vad som ändrades
B) En kort, beskrivande mening om vad som ändrades och gärna varför — ex: `Lägg till validering av HP-värde`
C) En lång detaljerad roman om alla tankar bakom ändringen
D) `asdfgh` — bara något så att Git är nöjt 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Git lägger till datum automatiskt — det behöver du inte skriva.
**B)** Rätt. En bra commit-meddelande är kort (under 72 tecken) och berättar vad som ändrades: `Lägg till TakeDamage-metod på Monster`. Ännu bättre: lägg till *varför* om det inte är uppenbart.
**C)** Commit-meddelanden ska vara korta — en lång berättelse hör hemma i PR-beskrivningen, inte i meddelandet.
**D)** `asdfgh` är det vanligaste misstaget på kl 17:58 en fredag. Resistera frestelsen.

</details>

---

## Fråga 9 — .gitignore

Vad är syftet med en `.gitignore`-fil?

A) Den listar filer som Git ska radera automatiskt
B) Den berättar för Git vilka filer och mappar som inte ska spåras eller committas
C) Den är en logg över filer som inte fick plats i senaste commit
D) Den är en fil man ignorerar — den spelar ingen roll 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**A)** Git raderar ingenting baserat på `.gitignore` — filer ignoreras bara, de tas inte bort.
**B)** Rätt. `.gitignore` håller byggartefakter (`bin/`, `obj/`, `.vs/`) och känslig information (`.env`) borta från repot. Filer som redan är committade ignoreras inte automatiskt — de måste tas bort manuellt.
**C)** Det finns ingen sådan logg i Git.
**D)** Den spelar stor roll — ett C#-projekt utan `.gitignore` fyller repot med tusentals genererade filer.

</details>

---

## Fråga 10 — ordningen add → commit → push

I vilken ordning kör man kommandon för att spara och skicka sina ändringar till GitHub?

A) `git push` → `git commit` → `git add`
B) `git commit` → `git add` → `git push`
C) `git add` → `git commit` → `git push`
D) `git yeet` — det borde finnas ett sådant kommando 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**A)** Du kan inte pusha innan du har committat — och du kan inte committa utan att ha stagat.
**B)** Fel ordning — man måste stage:a filer med `add` innan `commit` kan inkludera dem.
**C)** Rätt. `git add filnamn` → `git commit -m "meddelande"` → `git push`. Stage → snapshota → skicka.
**D)** `git yeet` finns inte i standard-Git — men det vore ett populärt alias.

</details>

---
