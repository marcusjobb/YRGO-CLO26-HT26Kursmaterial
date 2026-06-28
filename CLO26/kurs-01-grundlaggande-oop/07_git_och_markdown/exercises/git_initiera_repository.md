---

title: Steg-för-steg: Skapa ditt första Git Repository
author: Marcus Ackre Medina
type: exercise
topic: git
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/0_installation/install_git/exercises/git-initiera-repository.md"
description: "📘 Precis som du skriver ditt namn på en bok du lånar, behöver Git veta vem du är. Detta behöver bara göras **en gång per dator**."
tags: ["ditt", "exercise", "första", "git", "initiera", "repository", "skapa", "steg-för-steg:", "visual-studio"]
week_fit: []
---

# Steg-för-steg: Skapa ditt första Git Repository

🟢


## Innan du börjar - Konfigurera Git

📘 Precis som du skriver ditt namn på en bok du lånar, behöver Git veta vem du är. Detta behöver bara göras **en gång per dator**.

### Sätt ditt användarnamn och email

```bash
# Sätt ditt fullständiga namn
git config --global user.name "Anna Andersson"

# Sätt din email-adress
git config --global user.email "anna.andersson@student.se"
```

### Kontrollera att det blev rätt

```bash
# Visa ditt namn
git config --global user.name

# Visa din email
git config --global user.email

# Visa alla inställningar
git config --list
```

💡 **Varför är detta viktigt?** Varje commit du gör kommer att märkas med detta namn och email. Det hjälper dig och andra att veta vem som gjorde vilka ändringar.

## Steg 1: Skapa projektmapp

### Tänk så här

💬 Vi ska skapa en mapp för vårt projekt och sedan göra den till ett Git-repository:

```plaintext
1. Skapa en vanlig mapp
2. Navigera till mappen  
3. Säg till Git att "övervaka" denna mapp
4. Mappen blir nu ett Git-repository
```

### Praktiskt genomförande

```bash
# Navigera till där du vill skapa projektet
cd Desktop
# eller cd Documents
# eller cd /path/to/where/you/want/project

# Skapa ny projektmapp
mkdir mitt-git-projekt

# Gå in i mappen
cd mitt-git-projekt
```

### Kontrollera var du är

```bash
# Se vilken mapp du är i
pwd

# Lista innehållet (bör vara tomt än så länge)
ls -la
```

## Steg 2: Initiera Git Repository

### Det magiska kommandot

```bash
git init
```

### Vad händer nu?

Git skapar en **dold mapp** som heter `.git` i din projektmapp. Här lagras:
- All historik över dina ändringar
- Information om branches
- Konfiguration för detta projekt
- Allt Git behöver för att hålla koll på ditt projekt

### Kontrollera att det fungerade

```bash
# Se dolda filer (inklusive .git mappen)
ls -la

# Du bör se något som:
# drwxr-xr-x   9 username  staff   288 Jan 15 10:30 .git
```

### Kolla status på ditt nya repository

```bash
git status
```

Du bör se något liknande:
```
On branch main
No commits yet
nothing to commit (create/copy files and use "git add" to track)
```

## Steg 3: Skapa din första fil

### Skapa README-fil

```bash
# Skapa en README-fil
echo "# Mitt Git Projekt" > README.md

# eller med textredigerare
nano README.md
# (skriv innehåll, spara med Ctrl+X)
```

### Lägg till mer innehåll

```bash
# Lägg till beskrivning
echo "" >> README.md
echo "Detta är mitt första Git-projekt där jag lär mig grunderna." >> README.md
echo "" >> README.md  
echo "## Vad jag kommer att lära mig:" >> README.md
echo "- Git init" >> README.md
echo "- Git add" >> README.md
echo "- Git commit" >> README.md
```

### Kolla vad som hänt

```bash
git status
```

Nu ser du att filen är "untracked" - Git vet att den finns men följer inte ändringar ännu.

## Steg 4: Lägg till filer till Git

### Grundläggande add

```bash
# Lägg till specifik fil
git add README.md
```

### Kolla status igen

```bash
git status
```

Nu är filen "staged" - redo att committas! Du ser något som:
```
Changes to be committed:
  (use "git rm --cached <file>..." to unstage)
        new file:   README.md
```

### Lägg till flera filer

```bash
# Skapa fler filer
echo 'console.log("Hello World!");' > script.js
echo '<h1>Min Webbsida</h1>' > index.html

# Lägg till alla filer samtidigt
git add .

# Alternativt, lägg till specifika filer
git add script.js index.html
```

## Steg 5: Gör din första commit

### Tänk så här om commits

💬 En commit är som att ta ett foto av hela ditt projekt vid denna tidpunkt. Foto + beskrivning av vad som ändrats = commit.

### Gör commiten

```bash
git commit -m "Initial commit: Lagt till README, HTML och JavaScript-fil"
```

### Vad händer?

Git skapar en "ögonblicksbild" av alla filer som du lagt till med `git add`. Denna ögonblicksbild:
- Får ett unikt ID (hash)
- Märks med ditt namn, email och tidsstämpel
- Sparar ditt meddelande
- Blir en permanent del av projektets historia

### Kontrollera din commit

```bash
# Se historik
git log

# Kompakt vy
git log --oneline

# Se vad som ändrades i senaste commit
git show
```

## Steg 6: Fortsätt arbeta - Ändra och commit igen

### Modifiera befintlig fil

```bash
echo "## Nästa steg" >> README.md
echo "- Lära mig branches" >> README.md
echo "- Jobba med remote repositories" >> README.md
```

### Se vad som ändrats

```bash
# Kolla status
git status

# Se exakt vad som ändrats
git diff
```

### Commit ändringen

```bash
git add README.md
git commit -m "Lagt till nästa steg i README"
```

## Steg 7: Jobba med flera ändringar

### Gör ändringar i flera filer

```bash
# Ändra JavaScript-filen
echo 'alert("Välkommen till min sida!");' >> script.js

# Ändra HTML-filen
echo '<p>Denna sida är under utveckling.</p>' >> index.html

# Skapa ny fil
echo "TODO: Lägg till styling" > todo.txt
```

### Välj vad som ska committas

```bash
# Kolla vad som ändrats
git status

# Lägg bara till vissa filer
git add README.md script.js

# Commit dessa
git commit -m "Uppdaterat README och lagt till JavaScript-alert"

# Lägg till resten senare
git add .
git commit -m "Lagt till utvecklingsinfo i HTML och skapat todo-lista"
```

## Steg 8: Se din projekthistoria

### Utforska historiken

```bash
# Full historik
git log

# Kompakt vy med alla commits på en rad vardera
git log --oneline

# Se träd-vy (användbart med branches)
git log --graph --oneline

# Se vad som ändrades i varje commit
git log --stat
```

### Jämför olika punkter i tiden

```bash
# Jämför working directory med senaste commit
git diff HEAD

# Jämför två commits tillbaka med nu
git diff HEAD~2 HEAD

# Visa en specifik commit
git show <commit-hash>
```

## Vanliga problem och lösningar

### Problem 1: "Please tell me who you are"

⚠️ **Felmeddelande:**
```
Please tell me who you are.
Run git config --global user.email "you@example.com"
```

**Lösning:**
```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
```

### Problem 2: "Not a git repository"

⚠️ **Felmeddelande:**
```
fatal: not a git repository (or any of the parent directories): .git
```

**Lösning:**
- Kontrollera att du är i rätt mapp: `pwd`
- Kör `git init` om du inte gjort det än
- Navigera till projektmappen: `cd mitt-git-projekt`

### Problem 3: "Nothing to commit"

⚠️ **Felmeddelande:**
```
nothing to commit, working tree clean
```

**Detta är faktiskt bra!** Det betyder:
- Alla ändringar är committade
- Git håller koll på allt
- Inga filer har ändrats sedan senaste commit

### Problem 4: Glömde commit-meddelande

⚠️ **Vad händer:**
Git öppnar en textredigerare (ofta vim eller nano)

**Lösning i vim:**
1. Tryck `i` för att komma i insert-mode
2. Skriv ditt commit-meddelande
3. Tryck `Esc`
4. Skriv `:wq` och tryck Enter

**Lösning för att undvika:**
Använd alltid `-m` flaggan:
```bash
git commit -m "Ditt meddelande här"
```

## Bästa praxis för commit-meddelanden

### Dåliga exempel

❌ **Vaga meddelanden:**
```bash
git commit -m "fix"
git commit -m "update" 
git commit -m "changes"
```

### Bra exempel

✅ **Beskrivande meddelanden:**
```bash
git commit -m "Fixade bug i inloggningsvalidering"
git commit -m "Lagt till responsiv design för mobila enheter"
git commit -m "Uppdaterat API-dokumentation med nya endpoints"
```

### Mall för bra commit-meddelanden

```plaintext
Kort sammanfattning (50 tecken eller mindre)

Mer detaljerad förklaring om nödvändigt. Förklara vad som 
ändrades och varför, inte hur. Radbryt vid 72 tecken.

- Bullet points är okej
- Använd bindestreck eller asterisker  
- Lämna en tom rad mellan titel och innehåll
```

## Sammanfattning - Vad du nu kan

📚 Efter att ha följt denna guide kan du:

- **Konfigurera Git** med ditt namn och email
- **Skapa ett nytt repository** från en vanlig mapp
- **Lägga till filer** till Git med `git add`
- **Committa ändringar** med beskrivande meddelanden
- **Se projekthistorik** och förstå vad som hänt
- **Hantera flera filer** och selektiva commits
- **Lösa vanliga problem** som uppstår för nybörjare

## Nästa steg

🎯 Nu när du behärskar grunderna, prova att:

1. **Experimentera** med ditt nya repository
2. **Skapa fler filer** och öva på commit-cykeln
3. **Läs om branches** för att lära dig hantera parallell utveckling
4. **Anslut till GitHub** för att backup och dela ditt projekt
5. **Jobba med en vän** på samma projekt

Kom ihåg: Det bästa sättet att lära sig Git är att **använda det regelbundet**. Börja använda Git för alla dina projekt, även små experiment!

## Användbara kommandon att komma ihåg

```bash
# Grundläggande workflow
git status          # Kolla läget
git add filnamn     # Förbered för commit
git add .           # Förbered alla filer
git commit -m "msg" # Commit med meddelande
git log --oneline   # Se historik

# Information och hjälp
git help            # Allmän hjälp
git help add        # Hjälp för specifikt kommando
git --version       # Vilken Git-version
```

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
