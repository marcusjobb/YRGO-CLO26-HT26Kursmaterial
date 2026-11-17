# Git — Versionshantering

## Vad är Git och varför behöver du det?

Git är ett versionshanteringssystem som håller koll på varje ändring du gör i
dina filer. Tänk på det som en tidsmaskin — du kan när som helst gå tillbaka och
se hur koden såg ut igår, förra veckan eller när projektet startade.

Utan Git slutar det ofta med mappar som ser ut såhär:

```
projekt/
├── Program.cs
├── Program_gammal.cs
├── Program_fungerar.cs
├── Program_final.cs
└── Program_final_RIKTIG.cs
```

Med Git slipper du det helt. Det finns alltid bara en version av varje fil, och
hela historiken sparas automatiskt.

---

## De tre områdena

För att förstå Git behöver du känna till tre begrepp:

**Working Directory** är mappen på din dator där du redigerar filer. Det är här
du kodar som vanligt.

**Staging Area** är en förberedelseplats. Innan du sparar ändringar väljer du
vilka filer som ska ingå i nästa sparning — det kallas att "stagea" filer med
`git add`.

**Repository** är den faktiska historiken. Varje gång du kör `git commit` sparas
en permanent ögonblicksbild av alla stagade filer.

```
Working Directory  →  git add  →  Staging Area  →  git commit  →  Repository
(du kodar här)                   (förbereds)                      (sparad historik)
```

GitHub är ett fjärrrepository — en kopia av din historik som ligger online.
Du synkar med `git push` (ladda upp) och `git pull` (ladda ner).

---

## Installera Git

Ladda ner från **git-scm.com**. Under installationen:

- Välj **Git Bash** som standardterminal — vi använder alltid Git Bash
- Sätt default branch till **main**
- Allt annat kan vara default

Vi använder Git Bash för alla terminalkommandon. Det fungerar likadant på
Windows, Mac och Linux, vilket betyder att du kan följa alla guider och
hjälpa klasskamrater oavsett vilket operativsystem ni kör.

---

## Konfigurera Git

Öppna Git Bash och kör dessa kommandon en gång. De berättar för Git vem du är
och sparas globalt på din dator:

```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
git config --global init.defaultBranch main
git config --global core.autocrlf false
```

Den sista raden — `core.autocrlf false` — är viktig. Windows och Linux hanterar
radbrytningar på olika sätt. Den inställningen hindrar Git från att konvertera
dem och förstöra filer när du samarbetar med andra.

Verifiera att det sparades:
```bash
git config --list
```

---

## SSH-nyckel — din digitala nyckelring

Detta behöver du tänka om om du ska använda mer än ett github inloggning på din dator. Annars kan du lämna det till "den dagen den sorgen" avdelningen.

När du ansluter till GitHub kan du antingen använda lösenord varje gång eller
sätta upp en SSH-nyckel. En SSH-nyckel är ett par av filer — en privat som
bara finns på din dator, och en publik som du lägger på GitHub. När du
ansluter kollar GitHub om din privata nyckel matchar den publika. Inget
lösenord behövs.

**Skapa en nyckel:**
```bash
ssh-keygen -t ed25519 -C "din@email.com"
# Tryck Enter tre gånger för att använda standardinställningar
```

**Visa den publika nyckeln:**
```bash
cat ~/.ssh/id_ed25519.pub
```

Kopiera hela utskriften (börjar med `ssh-ed25519`). Gå till
**GitHub → Settings → SSH and GPG keys → New SSH key** och klistra in.

**Testa att det fungerar:**
```bash
ssh -T git@github.com
```

Du ska se: `Hi dittnamn! You've successfully authenticated`

---

## Dagliga kommandon

Dessa kommandon använder du varje dag:

```bash
git status              # Se vad som har ändrats
git add filnamn.cs      # Förbered en specifik fil
git add .               # Förbered alla ändrade filer
git commit -m "Meddelande som beskriver vad du gjort"
git push                # Skicka till GitHub
git pull                # Hämta senaste från GitHub
```

**Ritualen:** Kör alltid `git status` innan du gör något annat. Det ger dig
en tydlig bild av läget.

---

## Bra commit-meddelanden

Ett commit-meddelande ska förklara *vad* du gjort — inte *hur*. Koden visar
hur, meddelandet förklarar varför och vad.

```bash
# Bra
git commit -m "Lägg till validering av e-postadress"
git commit -m "Fixa krasch när lista är tom"
git commit -m "Dela upp UserService i mindre metoder"

# Dåligt — säger ingenting
git commit -m "fix"
git commit -m "uppdaterat"
git commit -m "asdf"
```

Commit-historiken är din dokumentation. Om du behöver hitta när en bugg
introducerades, eller förstå varför ett beslut togs för tre månader sedan,
är bra meddelanden guld värda.

---

## .gitignore — vad som aldrig ska committas

Skapa alltid en `.gitignore`-fil i projektets rot **innan** första commit.
Den talar om för Git vilka filer som ska ignoreras helt:

```
# C# bygg-filer (genereras automatiskt — behövs inte i repot)
bin/
obj/

# IDE-filer (personliga inställningar)
.vs/
*.user

# Hemligheter — ALDRIG committa dessa
.env
secrets.json
```

Varför är detta viktigt? `bin/` och `obj/` kan vara hundratals megabyte och
genereras om automatiskt. `.env` kan innehålla lösenord och API-nycklar som
om de hamnar på GitHub kan missbrukas inom minuter av automatiserade bots.

---

## Klona ett repo

```bash
# Hämta ett befintligt repo från GitHub
git clone git@github.com:användarnamn/repo-namn.git

# Gå in i mappen
cd repo-namn
```

SSH-adressen hittar du på GitHub under **Code → SSH**.

---

## Starta ett nytt repo

```bash
# Gå till din projektmapp i Git Bash
cd /c/Users/dittnamn/Documents/projekt

# Initiera Git
git init

# Skapa repot på GitHub först (github.com → New repository)
# Lägg sedan till GitHub som remote
git remote add origin git@github.com:dittnamn/repo-namn.git

# Lägg till alla filer, commit och push
git add .
git commit -m "Initial commit"
git push -u origin main
```

`-u origin main` behövs bara första gången. Efteråt räcker `git push`.

---

## När servern har ändringar du inte har

Det händer att GitHub har kod du inte har lokalt — till exempel om du redigerat
en fil direkt på GitHub, eller om någon annan pushat ändringar.

Om du försöker pusha direkt får du felmeddelandet:
```
! [rejected] main -> main (fetch first)
```

Lösningen: **pull innan push**

```bash
git add .
git commit -m "Mina ändringar"
git pull          # Hämta vad som finns på servern
git push          # Nu fungerar det
```

Ännu bättre vana — pull *innan* du börjar jobba:

```bash
git pull          # Starta alltid med att hämta senaste
# ...gör dina ändringar...
git add .
git commit -m "Mina ändringar"
git push
```

Om det uppstår en konflikt när du pullar — se [`git_konflikter.md`](git_konflikter.md).

---

## Vanliga problem och lösningar

**"Permission denied (publickey)"**
SSH-nyckeln är inte kopplad till GitHub. Gå igenom SSH-stegen igen och
kontrollera att du lade till rätt nyckel (den som börjar med `ssh-ed25519`).

**"src refspec main does not match any"**
Du har inte gjort en commit ännu. Kör `git add .` och `git commit` innan `git push`.

**"Updates were rejected" / "fetch first"**
GitHub har ändringar som du inte har lokalt. Kör `git pull` och lös eventuella
konflikter, sedan `git push`. Se [`git_konflikter.md`](git_konflikter.md) om det krånglar.

**"nothing to commit, working tree clean"**
Ingenting har ändrats sedan senaste commit. Det är inte ett fel — det är bra!

---

## Vill du läsa mer?

- **Pro Git** (gratis bok): git-scm.com/book — den bästa resursen som finns
- **Interaktiv övning**: learngitbranching.js.org — lär dig branching visuellt
- **GitHub Docs**: docs.github.com — officiell dokumentation
