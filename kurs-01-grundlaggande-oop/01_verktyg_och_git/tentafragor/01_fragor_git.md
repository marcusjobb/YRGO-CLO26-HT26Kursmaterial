# Tentafrågor — Git och verktyg

Öva inför tentan. Varje fråga har fyra alternativ — ett rätt, ett lite roligt, och två som verkar rimliga men inte stämmer.

---

## Fråga 1 — git add

Vad gör kommandot `git add`?

C) Det sparar ändringarna permanent i historiken
A) Det förbereder (stagear) filer för nästa commit
B) Det laddar upp filerna till GitHub
D) Det skapar en backup på skrivbordet 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**C)** Det är vad `git commit` gör — `git add` är steget innan, det väljer ut vad som ska ingå.
**A)** Rätt. `git add` lägger filer i "staging area", en sorts förberedelsezon inför commit.
**B)** Det är `git push` som skickar commits till en fjärr-repo, inte `git add`.
**D)** Git rör inte skrivbordet — men det hade varit ett intressant filsystem.

</details>

---

## Fråga 2 — commit vs push

Vad är skillnaden mellan `git commit` och `git push`?

C) `commit` och `push` gör samma sak men `push` är snabbare
B) `commit` sparar ändringarna lokalt, `push` skickar dem till fjärr-repot
A) `commit` skickar till GitHub, `push` skickar till GitLab
D) `push` skickar koden till Marcus direkt 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**C)** De gör helt olika saker — `commit` skapar en snapshot lokalt, `push` synkar med servern.
**B)** Rätt. Du kan göra hundra commits offline och sedan pusha alla på en gång.
**A)** Båda kommandon fungerar mot vilken Git-server som helst — GitHub, GitLab, Gitea, du väljer.
**D)** Marcus kollar GitHub som alla andra — det finns ingen direktlänk till hans inkorg.

</details>

---

## Fråga 3 — vad är ett repository?

Vad är ett repository (repo)?

B) En mapp med källkod, utan versionshistorik
C) En samling commits som Git håller koll på, lokalt eller på en server
A) En automatisk backup som skapas varje gång du sparar en fil
D) En speciell typ av USB-minne för kod 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**B)** En vanlig mapp är inte ett repo — det krävs att Git är initierat (`.git`-mappen måste finnas).
**C)** Rätt. Ett repo är hela projektets historik: alla commits, grenar och taggar.
**A)** Det är inte hur Git fungerar — du bestämmer själv när du committar, inget sker automatiskt.
**D)** Kul idé, men nej. Repo är ett begrepp, inte en hårdvara.

</details>

---

## Fråga 4 — git status

Vad visar `git status`?

B) Vilka commits som finns i historiken
A) Skillnaden (diff) mellan två filer
C) Vilka filer som är ändrade, stagade eller ospårade sedan senaste commit
D) En statusrapport om hur det går för projektet, med omdömen 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: C**

**B)** Det är `git log` som visar commit-historiken.
**A)** Det är `git diff` som visar exakt vad som ändrats rad för rad.
**C)** Rätt. `git status` ger en tydlig översikt: vad är stageat, vad är ändrat men inte stageat, och vad vet Git inte om än.
**D)** Git är opartisk — den ger inga betyg, bara fakta.

</details>

---

## Fråga 5 — git clone

Vad händer när du klonar ett repo med `git clone`?

C) Du skapar en ny tom mapp och kopplar den till GitHub
B) Du laddar ner repot med hela dess historik till din dator
A) Du kopierar bara senaste versionen av filerna, utan historik
D) Du kör `git yolo` i bakgrunden 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: B**

**C)** Det är `git init` + `git remote add` som sätter upp en ny tom koppling — inte clone.
**B)** Rätt. `git clone` hämtar hela repot: alla filer, alla commits, alla grenar.
**A)** Det stämmer inte — du får hela historiken på köpet, det är en av Gits styrkor.
**D)** `git yolo` är tyvärr inte ett riktigt kommando. Ännu.

</details>

---

## Fråga 6 — .gitignore

Vad är syftet med en `.gitignore`-fil?

C) Den listar vilka filer Git ska ta bort automatiskt
A) Den talar om för Git vilka filer och mappar som inte ska spåras
B) Den innehåller inloggningsuppgifter till GitHub
D) Den ignorerar alla commits gjorda på en måndag 😄

<details>
<summary>Visa svar och förklaring</summary>

**Rätt svar: A**

**C)** Git tar inte bort filer på eget bevåg — `.gitignore` gör att Git helt enkelt inte ser dem.
**A)** Rätt. Typiska exempel är `bin/`, `obj/`, `.env` och andra filer som inte ska in i repot.
**B)** Inloggningsuppgifter ska absolut inte vara i repot alls — varken i `.gitignore` eller någon annan fil.
**D)** Git är tyvärr blind för veckodagar. Måndagscommits gäller lika fullt.

</details>

---
