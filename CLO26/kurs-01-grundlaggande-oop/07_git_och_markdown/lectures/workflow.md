---
marp: true
theme: nion-dark
---

# Git Workflow 🔄

## Ritualen som alla utvecklare måste behärska

---

# Vad är Git? 🤔

**Git** är som en tidsmaskin för din kod! ⏰

- **Versionshantering** - spara "snapshots" av kod
- **Collaboration** - jobba flera på samma projekt
- **History** - se vad som ändrades och när
- **Backup** - koden finns på GitHub/remote

```bash
# Grundläggande Git ritual
git add .                    # Förbered ändringar
git commit -m "Fix login"    # Spara med beskrivning
git push                     # Skicka till GitHub
```

---

# Git Repository Grunder 📁

```bash
# Skapa nytt projekt
mkdir MyProject
cd MyProject
git init                    # Gör mappen till Git repo

# Eller klona befintligt projekt
git clone https://github.com/user/repo.git
cd repo

# Se status - vad händer?
git status
```

**Repository** = projektmapp med Git "superkrafter"

- `.git/` mapp innehåller hela historian
- **Working Directory** = där du jobbar
- **Staging Area** = förberedelseområde

---

# Den Heliga Ritualen 🛐

## 1. ADD - Förbered ändringar 📦

```bash
git add .                   # Lägg till alla ändringar
git add filename.cs         # Lägg till specifik fil
git add *.cs               # Lägg till alla C# filer
```

## 2. COMMIT - Spara snapshot 📸

```bash
git commit -m "Add user login feature"
git commit -m "Fix bug in payment system"
git commit -m "Update README with setup instructions"
```

## 3. PUSH - Skicka till GitHub 🚀

```bash
git push                   # Skicka till remote repository
git push origin main       # Specificera branch och remote
```

---

# Vad händer när? 🔍

```bash
# Kolla vad som hänt
git status                 # Nuvarande läge
git log                    # Historia av commits
git log --oneline          # Kompakt historik

# Se skillnader
git diff                   # Ändringar som inte är staged
git diff --staged          # Ändringar som är staged
git diff HEAD~1            # Jämför med förra commit
```

**Workflow:**

1. **Edit** files → 2. **Add** to staging → 3. **Commit** snapshot → 4. **Push** to remote

---

# Branching - Parallell utveckling 🌳

```bash
# Se alla branches
git branch                 # Lista lokala branches
git branch -a              # Inkludera remote branches

# Skapa ny branch
git branch feature/login   # Skapa utan att växla
git checkout -b feature/login  # Skapa OCH växla

# Växla mellan branches
git checkout main          # Växla till main
git switch feature/login   # Moderne sättet att växla

# Radera branch
git branch -d feature/login    # Radera lokal branch
```

**Branch** = separat utvecklingslinje för features

---

# Merge - Kombinera ändringar 🤝

```bash
# Basic merge workflow
git checkout main          # Växla till main branch
git pull                   # Hämta senaste från remote
git merge feature/login    # Slå ihop feature med main
git push                   # Skicka upp merged ändringar

# Alternative: Merge via GitHub
# 1. Push feature branch to GitHub
git push origin feature/login
# 2. Skapa Pull Request på GitHub
# 3. Review och merge via web interface
```

**Merge** = kombinera två branches till en

---

# Collaboration - Teamwork 👥

```bash
# Hämta andras ändringar
git pull                   # Fetch + merge i ett steg
git fetch                  # Bara hämta, merge inte
git pull origin main       # Hämta från specifik branch

# Hantera merge conflicts
# 1. Git varnar om konflikt
# 2. Öppna filen och fixa konflikten
# 3. git add . && git commit
```

**Grundregel:** Alltid `git pull` innan du börjar jobba!

**Conflict-märken ser ut så här:**

```
<<<<<<< HEAD
Din kod här
=======
Annans kod
>>>>>>> branch-namn
```

---

# Praktisk Demo 💻

```bash
# Nytt projekt setup
mkdir SuperHeroApp
cd SuperHeroApp
git init
echo "# SuperHero App" > README.md

# Första commit
git add README.md
git commit -m "Initial commit with README"

# Koppla till GitHub
git remote add origin https://github.com/username/superheroapp.git
git push -u origin main

# Feature development
git checkout -b feature/hero-list
# ... gör ändringar i kod ...
git add .
git commit -m "Add hero list functionality"
git push origin feature/hero-list
```

---

# Git Best Practices 🎯

## Commit Messages ✍️

```bash
# BRA messages
git commit -m "Add user authentication system"
git commit -m "Fix bug in password validation"
git commit -m "Update dependencies to latest versions"

# DÅLIGA messages
git commit -m "stuff"
git commit -m "fix"
git commit -m "wip"
```

## Commit Frequency 📅

- **Commit ofta** - små, logiska ändringar
- **En feature** per commit
- **Fungerande kod** - commit inte trasig kod

---

# Emergency Commands 🚨

```bash
# Ångra senaste commit (behåll ändringar)
git reset HEAD~1

# Ångra senaste commit (kasta bort ändringar)
git reset --hard HEAD~1

# Se vem som ändrade vad
git blame filename.cs

# Stash - göm ändringar tillfälligt
git stash                  # Göm ändringar
git stash pop              # Ta tillbaka ändringar

# Hämta specifik commit
git checkout abc1234       # Hoppa till specifik commit
git checkout main          # Tillbaka till main
```

**VARNING:** `--hard` raderar ändringar permanent!

---

# Visual Git Workflow 🎨

```
Working Dir  →  Staging Area  →  Local Repo  →  Remote Repo
    📝             📦              📚            ☁️

git add    →   git commit   →   git push
           ←   git checkout ←   git pull
```

**Stages:**

1. **Working Directory** - där du editerar
2. **Staging Area** - förbereder för commit
3. **Local Repository** - lokala commits
4. **Remote Repository** - GitHub/GitLab

---

# Key Takeaways 🎓

1. **Git Ritual**: `add` → `commit` → `push`
2. **Commit ofta** med beskrivande meddelanden
3. **Branch för features** - håll main ren
4. **Pull före push** - hämta andras ändringar först
5. **Merge conflicts** är normala - fixa och fortsätt

## 🚀 Nästa steg:

- **Praktisera ritualen** dagligen
- **Skapa branches** för alla features
- **Läs Git log** för att förstå historik

---

**Git är din bästa vän som utvecklare! 🤝**

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
