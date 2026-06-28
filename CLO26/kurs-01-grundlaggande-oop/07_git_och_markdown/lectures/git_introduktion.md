---

title: Versionskontroll med Git
author: Marcus Ackre Medina
type: lecture
topic: git
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/0_installation/install_git/lecture/git-introduktion.md"
description: "📘 Git är som ett 'magiskt filsystem' som kommer ihåg allt du någonsin gjort med dina filer. Tänk dig Git som en superkraftig 'undo'-knapp som inte bara kan ångra det senaste du gjorde, utan kan hoppa "
tags: ["git", "installation", "versionshantering", "versionskontroll"]
week_fit: []
---

# Versionskontroll med Git

🟢


## Vad är Git?

📘 Git är som ett "magiskt filsystem" som kommer ihåg allt du någonsin gjort med dina filer. Tänk dig Git som en superkraftig "undo"-knapp som inte bara kan ångra det senaste du gjorde, utan kan hoppa tillbaka till vilken punkt som helst i ditt projekts historia.

Git är ett **versionshanteringssystem** som hjälper utvecklare att:
- Spåra ändringar i kod över tid
- Samarbeta med andra utvecklare utan att skriva över varandras arbete
- Hoppa tillbaka till tidigare versioner av koden
- Förstå vem som ändrade vad och när

Föreställ dig att du skriver en uppsats. Istället för att ha filer som heter "uppsats_v1.docx", "uppsats_v2.docx", "uppsats_FINAL.docx", "uppsats_FINAL_FINAL.docx" - så håller Git reda på alla versioner åt dig på ett smart sätt.

## Grundläggande koncept

### Repository (repo)

Ett **repository** är som en mapp som innehåller ditt projekt plus hela dess historia. Det är där Git lagrar all information om dina filer och alla ändringar som gjorts.

### Commit

En **commit** är som en ögonblicksbild av ditt projekt vid en specifik tidpunkt. Varje commit har:
- Ett unikt ID (kallas hash)
- Ett meddelande som beskriver vad som ändrats
- Information om vem som gjorde ändringen och när

### Working Directory, Staging Area och Repository

Git har tre "områden" där dina filer kan befinna sig:

1. **Working Directory** - Här jobbar du med dina filer
2. **Staging Area** - Här "förbereder" du filer innan du committar dem
3. **Repository** - Här lagras alla dina commits permanent

## Så här fungerar Git i praktiken

### Tänk så här

💬 Så här kan vi tänka kring Git-processen i vardagstermer:

```plaintext
1. Du arbetar med dina filer (Working Directory)
2. När du är nöjd väljer du vilka ändringar som ska sparas (git add)
3. Du skapar en "ögonblicksbild" av dessa ändringar (git commit)
4. Din ögonblicksbild sparas permanent i projektets historia
```

### Grundläggande workflow

Här är den typiska arbetsprocessen med Git:

```bash
# 1. Skapa eller klona ett repository
git init                    # Skapar nytt repo
git clone <url>            # Kopierar befintligt repo

# 2. Arbeta med filer
# (Redigera, skapa, ta bort filer som vanligt)

# 3. Kolla status på dina ändringar
git status

# 4. Lägg till filer till staging area
git add filnamn.txt        # Lägger till specifik fil
git add .                  # Lägger till alla ändrade filer

# 5. Commit dina ändringar
git commit -m "Beskrivning av vad jag ändrat"

# 6. Se historik över commits
git log
```

## Varför är Git så viktigt?

### 1. Säkerhet mot förlust

Git är som en försäkring för din kod. Även om du råkar ta bort filer eller göra misstag, kan du alltid återställa tidigare versioner.

### 2. Samarbete utan kaos

Flera personer kan jobba på samma projekt samtidigt utan att skriva över varandras arbete. Git hjälper till att slå ihop allas ändringar på ett smart sätt.

### 3. Experimentering utan risk

Du kan skapa **branches** (grenar) för att testa nya idéer utan att påverka huvudkoden. Om experimentet lyckas - slå ihop det. Om inte - kasta bort grenen.

### 4. Historik och ansvar

Git kommer ihåg vem som ändrade vad och när. Detta hjälper vid:
- Felsökning (när introducerades detta problem?)
- Kodgranskning (vad tänkte utvecklaren här?)
- Projektplanering (hur snabbt utvecklas olika delar?)

## Stegvis utveckling av Git-kunskaper

### Nivå 1: Grunderna (lokal utveckling)

```bash
# Skapa nytt repository
git init

# Lägg till filer och commit
git add .
git commit -m "Initial commit"

# Kolla vad som hänt
git status
git log
```

### Nivå 2: Ångra och navigera

```bash
# Se skillnader
git diff                   # Ändringar som inte committs än
git diff HEAD~1           # Jämför med förra committen

# Återställ filer
git checkout HEAD filnamn  # Återställ fil till senaste commit
git reset --hard HEAD~1    # Gå tillbaka en commit (VARNING: raderar ändringar)

# Navigera i historiken
git log --oneline          # Kompakt log
git show <commit-hash>     # Visa specifik commit
```

### Nivå 3: Samarbete (remotes)

```bash
# Anslut till remote repository (t.ex. GitHub)
git remote add origin <url>

# Ladda upp dina ändringar
git push origin main

# Ladda ner andras ändringar
git pull origin main

# Se remote-information
git remote -v
```

### Nivå 4: Branching (avancerat)

```bash
# Skapa och byt till ny branch
git checkout -b feature-ny-funktion

# Arbeta på branchen, commit som vanligt
git add .
git commit -m "Implementerat ny funktion"

# Byt tillbaka till main
git checkout main

# Slå ihop din branch
git merge feature-ny-funktion
```

## Vanliga misstag att undvika

⚠️ Här är några vanliga fallgropar:

### 1. Glömma att commit ofta
Gör små, frekventa commits istället för stora sällsynta. Det är lättare att förstå och återställa små ändringar.

### 2. Dåliga commit-meddelanden
Skriv beskrivande meddelanden:
```bash
# Dåligt
git commit -m "fix"

# Bra
git commit -m "Fixade bug där användare inte kunde logga in med specialtecken"
```

### 3. Inte kolla status innan commit
Kör alltid `git status` för att se vad som kommer att committas.

### 4. Rädda för att experimentera
Git är byggt för att låta dig experimentera säkert. Använd branches och var inte rädd för att prova saker.

## Sammanfattning

📚 De viktigaste sakerna att komma ihåg om Git:

- **Git spårar ändringar** - det kommer ihåg allt du gjort med dina filer
- **Commit tidigt och ofta** - små ändringar är lättare att förstå och hantera  
- **Använd beskrivande meddelanden** - framtida du kommer tacka dig
- **Git är säkert** - du kan experimentera utan rädsla för att förstöra något
- **Samarbete blir enkelt** - flera kan jobba på samma projekt utan konflikt

Git kan kännas överväldigande först, men det är som att lära sig köra bil - när du väl behärskar grunderna blir det naturligt och du kan inte föreställa dig att jobba utan det.

## Nästa steg

🎯 För att börja med Git praktiskt:

1. **Installera Git** på din dator
2. **Skapa ditt första repository** med `git init`
3. **Gör din första commit** med några enkla filer
4. **Experimentera** med att ändra filer och committa igen
5. **Utforska historiken** med `git log`

Kom ihåg: Det bästa sättet att lära sig Git är att börja använda det för riktiga projekt. Börja enkelt och bygg upp kunskaperna steg för steg!

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
