# Träningsuppgifter: Vecka 1 — Verktyg och Git

## Instruktioner

Välj det bästa svaret för varje fråga. Klicka på 'Visa svar' för att se det rätta svaret och förklaringar.

### Fråga 1

Vilket Git-kommando visar vilka filer som har ändrats?

a. git show<br>
b. git log<br>
c. git status<br>
d. git diff

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** git status

  **Förklaringar:**

  - ❌ **a) git show** - FEL: `git show` visar detaljer om en specifik commit
  - ❌ **b) git log** - FEL: `git log` visar commit-historiken, inte pågående ändringar
  - ✅ **c) git status** - **RÄTT**: `git status` ska vara det första du kör. Den visar ändrade filer, vilka som är stagade och vilken branch du är på
  - ❌ **d) git diff** - FEL: `git diff` visar *exakt vad* som ändrats rad för rad, medan `git status` bara visar *vilka filer* som ändrats
</details>

---

### Fråga 2

Vad är rätt ordning i Git-ritualen?

a. commit → add → push → status<br>
b. status → add → commit → push<br>
c. push → commit → add → status<br>
d. add → push → commit → status

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** status → add → commit → push

  **Förklaringar:**

  - ❌ **a) commit → add → push** - FEL: Du måste adda innan du committar, inte tvärtom
  - ✅ **b) status → add → commit → push** - **RÄTT**: Kolla läget (`status`), förbered filer (`add`), spara ögonblicksbild (`commit`), skicka till GitHub (`push`)
  - ❌ **c) push → commit → add** - FEL: Du kan inte pusha något som inte är committat
  - ❌ **d) add → push → commit** - FEL: Du måste committa innan du pushar
</details>

---

### Fråga 3

Vad gör `git add .` ?

a. Lägger till alla ändrade filer i staging area<br>
b. Skapar en commit<br>
c. Tar bort alla ändringar<br>
d. Pushar all kod till GitHub

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Lägger till alla ändrade filer i staging area

  **Förklaringar:**

  - ✅ **a) Alla ändrade filer till staging** - **RÄTT**: `git add .` förbereder alla ändrade och nya filer i aktuell mapp för nästa commit
  - ❌ **b) Skapar en commit** - FEL: Det gör `git commit`
  - ❌ **c) Tar bort ändringar** - FEL: `git add` förbereder ändringar, den tar inte bort dem
  - ❌ **d) Pushar kod** - FEL: `git push` skickar till GitHub, `git add` förbereder lokalt
</details>

---

### Fråga 4

Vad är ett bra commit-meddelande?

a. "fix"<br>
b. "asdf"<br>
c. "Lägg till validering av e-postadress"<br>
d. "grejer"

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** "Lägg till validering av e-postadress"

  **Förklaringar:**

  - ❌ **a) "fix"** - FEL: Säger inget om *vad* som fixades
  - ❌ **b) "asdf"** - FEL: Helt meningslöst — om tre månader kommer du inte ha en aning om vad den commiten gjorde
  - ✅ **c) "Lägg till validering av e-postadress"** - **RÄTT**: Beskriver VAD som gjorts. Använd presens, aktivt verb
  - ❌ **d) "grejer"** - FEL: Alldeles för vagt — vad för grejer?
</details>

---

### Fråga 5

Vad ska en `.gitignore`-fil innehålla?

a. Instruktioner för hur man ignorerar git-kommandon<br>
b. Mappar och filer som aldrig ska committas, som `bin/` och `obj/`<br>
c. Lista över vem som får pusha till repot<br>
d. Alla .cs-filer i projektet

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Mappar och filer som aldrig ska committas, som `bin/` och `obj/`

  **Förklaringar:**

  - ❌ **a) Instruktioner för git** - FEL: `.gitignore` är inte en kommandofil, den listar bara mönster för filer att ignorera
  - ✅ **b) Mappar och filer att ignorera** - **RÄTT**: `bin/` och `obj/` kan vara hundratals MB och genereras om automatiskt — de ska aldrig i repot. `.env` och `secrets.json` kan innehålla lösenord
  - ❌ **c) Vem som får pusha** - FEL: Det kontrolleras via GitHub-inställningar, inte `.gitignore`
  - ❌ **d) Alla .cs-filer** - FEL: `.gitignore` listar vad som ska *ignoreras*, inte vad som ska spåras
</details>

---

### Fråga 6

Vad är Git Bash?

a. En IDE för C#<br>
b. En terminal som fungerar likadant på Windows, Mac och Linux<br>
c. En version av Visual Studio<br>
d. Ett GitHub-konto

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** En terminal som fungerar likadant på Windows, Mac och Linux

  **Förklaringar:**

  - ❌ **a) En IDE** - FEL: Git Bash är en terminal, inte en IDE. IDE:n är Visual Studio eller Rider
  - ✅ **b) En terminal som är samma överallt** - **RÄTT**: Git Bash ger dig samma kommandon (`ls`, `cd`, `pwd`) oavsett operativsystem. Det gör att alla instruktioner fungerar för alla
  - ❌ **c) En version av VS** - FEL: Visual Studio och Git Bash är helt olika verktyg
  - ❌ **d) Ett GitHub-konto** - FEL: Git Bash är programvara på din dator, inte ett konto
</details>

---

### Fråga 7

Vad gör `git clone`?

a. Skapar en kopia av ett repo från GitHub till din dator<br>
b. Skapar en ny branch<br>
c. Tar bort ett repo från GitHub<br>
d. Kopierar en fil i ditt repo

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Skapar en kopia av ett repo från GitHub till din dator

  **Förklaringar:**

  - ✅ **a) Hämtar repo från GitHub** - **RÄTT**: `git clone git@github.com:användarnamn/repo.git` laddar ner hela repot med all historik
  - ❌ **b) Skapar en branch** - FEL: Det gör `git branch`
  - ❌ **c) Tar bort repo** - FEL: `git clone` hämtar, inte tar bort
  - ❌ **d) Kopierar en fil** - FEL: `git clone` kopierar hela repot, inte en enskild fil
</details>

---

### Fråga 8

Vad är rätt kommando för att verifiera att .NET SDK är installerat?

a. dotnet --version<br>
b. dotnet --check<br>
c. net --version<br>
d. sdk --version

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** dotnet --version

  **Förklaringar:**

  - ✅ **a) dotnet --version** - **RÄTT**: Kör i Git Bash och du bör se `10.x.x` eller nyare
  - ❌ **b) dotnet --check** - FEL: Det kommandot finns inte
  - ❌ **c) net --version** - FEL: Kommandot heter `dotnet`, inte `net`
  - ❌ **d) sdk --version** - FEL: Kommandot heter `dotnet`, inte `sdk`
</details>

---

### Fråga 9

Vad gör git pull?

a. Skickar din kod till GitHub<br>b. Hämtar senaste ändringarna från GitHub<br>c. Tar bort din lokala kod<br>d. Skapar ett nytt repo

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Hämtar senaste ändringarna från GitHub

  **Förklaringar:**

  - ❌ **a) Skickar kod till GitHub** - FEL: Det gör `git push`
  - ✅ **b) Hämtar från GitHub** - **RÄTT**: `git pull` laddar ner andras commits som du inte har lokalt. Bra vana: kör `git pull` *innan* du börjar jobba
  - ❌ **c) Tar bort kod** - FEL: `git pull` tar inte bort något, den lägger till nya ändringar
  - ❌ **d) Skapar nytt repo** - FEL: Det gör du på GitHub.com eller med `git init`
</details>

---

### Fråga 10

Vad är en merge-konflikt?

a. När Git kraschar<br>
b. När Git inte kan avgöra vilken version av en fil som ska gälla<br>
c. När din dator inte har internet<br>
d. När du glömt spara en fil

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** När Git inte kan avgöra vilken version av en fil som ska gälla

  **Förklaringar:**

  - ❌ **a) Git kraschar** - FEL: En konflikt är inte en krasch — Git ber dig fatta ett beslut
  - ✅ **b) Git vet inte vilken version som gäller** - **RÄTT**: Det händer när två personer (eller du själv på två ställen) ändrat samma rad i samma fil. Git markerar konflikten och du väljer själv vad slutresultatet ska bli
  - ❌ **c) Ingen internet** - FEL: Konflikter uppstår lokalt vid merge/pull, inte vid internetproblem
  - ❌ **d) Glömt spara** - FEL: Git jobbar med sparade filer, osparade ändringar påverkar inte Git
</details>

---

### Fråga 11

Vad ser du om du kör `git commit -m "fix"`?

a. Ett bra commit-meddelande som alla förstår<br>
b. Ett dåligt commit-meddelande som inte säger vad som ändrades<br>
c. Ingenting — kommandot fungerar inte<br>
d. En varning från Git

<details>
  <summary>Visa svar</summary>

  **Rätt svar:** Ett dåligt commit-meddelande som inte säger vad som ändrades

  **Förklaringar:**

  - ❌ **a) Bra meddelande** - FEL: "fix" är ett av de sämsta meddelandena. Vad fixades? Varför?
  - ✅ **b) Dåligt meddelande** - **RÄTT**: "fix" säger ingenting om vad commiten innehåller. Om tre månader har du ingen aning om vad "fix" betyder
  - ❌ **c) Fungerar inte** - FEL: Kommandot fungerar tekniskt — Git accepterar vilken text som helst. Det är bara dålig praxis
  - ❌ **d) En varning** - FEL: Git varnar inte för dåliga meddelanden. Det är upp till dig som utvecklare att skriva bra sådana
</details>

---
