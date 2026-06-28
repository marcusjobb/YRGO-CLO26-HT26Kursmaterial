---

title: 🌿 Git Flow Cheatsheet - Minimal Workflow
author: Marcus Ackre Medina
type: assignment
topic: projekt
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/4_test_and_quality_assurance/assignment/cheatsheets/git_flow_cheatsheet.md"
description: "TL;DR:** Använd `main`, `develop`, och `feature/*` branches. Pusha ALDRIG direkt till main eller develop!"
tags: ["bash", "cheatsheet", "flow", "git", "installation", "minimal", "projekt", "ssh", "visual-studio", "workflow"]
week_fit: []
---

# 🌿 Git Flow Cheatsheet - Minimal Workflow

🟢


**TL;DR:** Använd `main`, `develop`, och `feature/*` branches. Pusha ALDRIG direkt till main eller develop!

---

## 🎯 Vad är Git Flow?

**Git Flow = Strukturerad branching-strategi**

```
main        ●────────────●────────────●  (Produktion, alltid deploybar)
             ╲          ╱            ╱
develop       ●────●───●────●───────●   (Nästa release, alltid testbar)
               ╲  ╱   ╲    ╱
feature/login   ●●     ╲  ╱
feature/api           ●●
```

**Varför?**

- ✅ Main är alltid stabil (_den som sabbar det får smisk!_)
- ✅ Develop samlar kommande features (_även kallad vilda västern_)
- ✅ Features utvecklas isolerat (_your circus, your monkeys!_)
- ✅ Konflikter upptäcks tidigt (via PR och CI/CD)
- ✅ CI/CD kan testa innan merge

---

## 📦 De Tre Branches

### 1. Main Branch (Produktion)

**Syfte:** Produktionskod, alltid deploybar

**Regler:**

- ❌ Pusha ALDRIG direkt
- ❌ Ingen experimentell kod
- ✅ Endast merges från develop (via PR)
- ✅ Tagga alla releases (`v1.0.0`, `v1.1.0`)

**När mergas till main?**

```
Sprint Review → Demo OK → CI/CD grön → Merge develop → main
```

---

### 2. Develop Branch (Development)

**Syfte:** Integration av features, nästa release

**Regler:**

- ❌ Pusha ALDRIG direkt
- ✅ Endast merges från feature-branches (via PR)
- ✅ Alltid testbar (alla tester gröna)
- ✅ Bas för alla nya features

**När mergas till develop?**

```
Feature klar → Tester gröna → Code review OK → Merge feature → develop
```

---

### 3. Feature Branches (Din arbetsyta)

**Syfte:** Utveckla en feature isolerat

**Namngivning:**

```bash
feature/user-login          ✅ Tydligt vad det är
feature/add-swagger         ✅ Specifikt
feature/api-endpoints       ✅ Beskrivande

feature/stuff               ❌ Vad är "stuff"?
feature/fix                 ❌ För vagt
my-branch                   ❌ Ingen kontext
pelles-branch               ❌ Säger inget om funktionalitet, Pelle ska ha smisk!
```

**Prefix-konventioner:**

- `feature/` - Ny funktion
- `bugfix/` - Buggfix
- `hotfix/` - Kritisk fix till main (direkt från main)
- `refactor/` - Kod-förbättring
- `test/` - Test-relaterade ändringar
- `docs/` - Dokumentation

---

## 🚀 Workflow Steg-för-Steg

### Steg 1: Starta En Ny Feature

```bash
# 1. Byt till develop och hämta senaste
git checkout develop
git pull origin develop

# 2. Skapa feature branch FRÅN develop
git checkout -b feature/user-authentication

# 3. Verifiera du är på rätt branch
git branch
# * feature/user-authentication
#   develop
#   main
```

**Pro-tip:** Kör ALLTID `git pull` innan du skapar ny branch!

---

### Steg 2: Utveckla & Commita

```bash
# Gör ändringar...

# Kör tester LOKALT först
dotnet test

# Om grönt → commit
git add .
git commit -m "feat: add login endpoint with JWT"

# Fortsätt arbeta...
git add .
git commit -m "test: add authentication integration tests"

# Fler commits...
git add .
git commit -m "refactor: extract token service to separate class"
```

**Pro-tip:** Många små commits > en jätte-commit!

**Commit Message Format:**

```
feat: add new feature
fix: fix bug in UserService
test: add tests for login
refactor: improve code structure
docs: update README
```

---

### Steg 3: Pusha Feature Branch

```bash
# Pusha till FEATURE-branch (INTE develop!)
git push origin feature/user-authentication

# Första gången (sätter upstream):
git push -u origin feature/user-authentication
```

**Viktigt:** Du pushar till `feature/user-authentication`, ALDRIG till `develop`!

**💡 Pro-tip: Testa merge INNAN du skapar PR!**

Så här säkerställer du att din kod kommer överleva CI/CD testerna i Pull Request:

```bash
# Hämta senaste develop
git fetch origin develop

# Merge develop in i din feature branch LOKALT
git merge origin/develop

# Lös eventuella konflikter NU (inte i PR!)
# ... fixa konflikter om det finns ...

# Kör tester lokalt
dotnet test

# Om allt grönt → pusha
git push origin feature/user-authentication
```

**Varför?** Om det blir konflikter eller testfel upptäcker du det NU istället för att CI/CD failar i PR! 🎯

---

### Steg 4: Skapa Pull Request

**På GitHub:**

1. Gå till repot → "Pull Requests" → "New Pull Request"
2. **Base:** `develop` ← **Compare:** `feature/user-authentication`
3. Fyll i beskrivning (se [PR Workflow Guide](../articles/pr_workflow_guide.md))
4. **Request reviewers** → Välj teammedlemmar
5. **Create Pull Request**

**CI/CD körs automatiskt:**

```
⏳ .NET CI / build-and-test - In progress...
✅ .NET CI / build-and-test - Success!
```

---

### Steg 5: Code Review & Feedback

**Teammedlem granskar:**

```
Alice reviewed:
✅ Approved
💬 "Bra kod! Bara en liten kommentar på rad 42..."
```

**Om ändringar behövs:**

```bash
# Fixa feedback lokalt
git add .
git commit -m "fix: address code review feedback"
git push

# CI/CD kör om automatiskt!
```

---

### Steg 6: Merge till Develop

**När:**

- ✅ CI/CD grön
- ✅ Minst 1 approval
- ✅ Inga konflikter

**Klicka "Merge pull request"**

```
✅ feature/user-authentication merged into develop
```

**Efter merge:**

```bash
# Byt till develop och uppdatera
git checkout develop
git pull origin develop

# (Optional) Radera feature branch
git branch -d feature/user-authentication
git push origin --delete feature/user-authentication
```

**GitHub kan auto-delete branch!** Aktivera under Settings → General → "Automatically delete head branches"

---

### Steg 7: Release till Main (Sprint Slutet)

**När en sprint är klar:**

```bash
# 1. Mergea develop → main (via PR!)
# GitHub: Base: main ← Compare: develop

# 2. Efter merge, tagga release
git checkout main
git pull origin main
git tag -a v1.0.0 -m "Release 1.0.0 - User Authentication"
git push origin v1.0.0
```

**Tagging Convention:**

- `v1.0.0` - Major release
- `v1.1.0` - Minor release (ny feature)
- `v1.1.1` - Patch release (buggfix)

---

## 🔥 Vanliga Scenarion

### Scenario 1: Develop Har Uppdaterats Medan Du Jobbade

**Problem:** Din feature branch är bakom develop

```bash
# På din feature branch
git checkout feature/my-feature

# Hämta senaste develop
git fetch origin develop

# Merge develop in i din feature
git merge origin/develop

# Lös eventuella konflikter
# ... fixa konflikter ...
git add .
git commit -m "merge: resolve conflicts with develop"

# Pusha uppdaterad branch
git push origin feature/my-feature
```

**Alternativ (Rebase - Mer avancerat):**

```bash
git rebase origin/develop
# Rebase gör historiken renare men är svårare
```

---

### Scenario 2: Jag Råkade Commita till Develop!

**OM DU INTE PUSHAT ÄN:**

```bash
# 1. Verifiera att du är på develop
git branch
# * develop

# 2. Backa commit (behåll ändringar)
git reset --soft HEAD~1

# 3. Skapa rätt feature branch
git checkout -b feature/my-feature

# 4. Commit igen
git add .
git commit -m "feat: add feature properly"
git push -u origin feature/my-feature
```

**OM DU REDAN PUSHAT:**

- Kontakta teamet OMEDELBART
- Använd `git revert` (säkrare än `git reset`)
- Be teamet hjälpa till att städa

---

### Scenario 3: Merge Conflict i Pull Request

**GitHub visar:**

```
❌ This branch has conflicts that must be resolved
```

**Lösning:**

```bash
# 1. Uppdatera din lokala branch
git checkout feature/my-feature
git pull origin feature/my-feature

# 2. Merge develop
git merge origin/develop

# 3. Lös konflikter i din editor
# Välj rätt version av koden:
<<<<<<< HEAD
// Din kod
=======
// Develop's kod
>>>>>>> origin/develop

# 4. Efter fix
git add .
git commit -m "merge: resolve conflicts with develop"
git push origin feature/my-feature

# PR uppdateras automatiskt!
```

---

### Scenario 4: Hotfix (Kritisk Bugg i Main)

**Akut fix behövs i produktion:**

```bash
# 1. Skapa hotfix FRÅN main (inte develop!)
git checkout main
git pull origin main
git checkout -b hotfix/critical-login-bug

# 2. Fixa buggen
# ... kod ...

# 3. Commit & pusha
git add .
git commit -m "hotfix: fix critical login vulnerability"
git push -u origin hotfix/critical-login-bug

# 4. Skapa PR till main
# Base: main ← Compare: hotfix/critical-login-bug

# 5. Efter merge till main, mergea också till develop!
# Base: develop ← Compare: hotfix/critical-login-bug
```

**Viktigt:** Hotfix måste mergas till BÅDE main OCH develop!

---

## 🎯 Best Practices

### 1. Feature Branches Ska Vara Små

**Dåligt:**

```
feature/implement-entire-api
Files changed: 47 files
+3,284 −1,847
```

→ Omöjligt att granska, tar dagar

**Bra:**

```
feature/add-login-endpoint
Files changed: 4 files
+142 −8
```

→ Enkel att granska, 15 min review

**Regel:** En feature branch = En User Story

---

### 2. Commita Ofta, Pusha Regelbundet

```bash
# Dåligt
git add .
git commit -m "did stuff"
# 500+ lines changed

# Bra
git add Controllers/LoginController.cs
git commit -m "feat: add login endpoint"

git add Services/TokenService.cs
git commit -m "feat: add JWT token generation"

git add Tests/LoginTests.cs
git commit -m "test: add login integration tests"
```

**Varför?**

- Lättare att hitta buggar (git bisect)
- Lättare att revertera specifika ändringar
- Tydligare historik

---

### 3. Pull Innan Du Pushar

```bash
# Alltid detta workflow:
git pull origin develop          # Hämta senaste
git checkout -b feature/new      # Skapa branch
# ... arbeta ...
git push -u origin feature/new   # Pusha

# INTE detta:
git checkout -b feature/new      # Branchar från gammal develop!
# ... arbeta i 3 dagar ...
git push                         # 💥 Konflikter!
```

---

### 4. Radera Gamla Feature Branches

```bash
# Efter merge, städa!
git branch -d feature/old-feature           # Lokalt
git push origin --delete feature/old-feature # Remote

# Lista alla lokala branches
git branch

# Lista alla remote branches
git branch -r
```

**Tips:** Aktivera "Auto-delete branches" på GitHub!

---

## 🔍 Användbara Git-Kommandon

### Status & Info

```bash
git status                    # Vad har ändrats?
git branch                    # Vilken branch är jag på?
git branch -a                 # Alla branches (lokal + remote)
git log --oneline --graph     # Commit-historik (visuell)
git diff                      # Vad har ändrats (unstaged)?
git diff --staged             # Vad ska commitas?
```

---

### Branching

```bash
git checkout -b feature/name  # Skapa och byt till ny branch
git checkout develop          # Byt till develop
git branch -d feature/name    # Radera branch (lokalt)
git push origin --delete feature/name  # Radera remote branch
```

---

### Syncing

```bash
git fetch origin              # Hämta remote changes (utan merge)
git pull origin develop       # Hämta OCH merge develop
git push origin feature/name  # Pusha till remote
git push -u origin feature/name  # Pusha och sätt upstream
```

---

### Undo Mistakes

```bash
git reset --soft HEAD~1       # Ångra senaste commit (behåll ändringar)
git reset --hard HEAD~1       # Ångra senaste commit (KASTA ändringar!)
git checkout -- file.cs       # Ångra ändringar i fil (unstaged)
git revert <commit-hash>      # Skapa ny commit som ångrar gammal
```

**Varning:** `git reset --hard` är PERMANENT! Använd med försiktighet!

---

## 🚨 Fel att UNDVIKA

### ❌ Fel #1: Pusha Direkt till Develop

```bash
# ALDRIG gör detta:
git checkout develop
git add .
git commit -m "add feature"
git push origin develop    # 💥 BAD!
```

**Varför fel?**

- Ingen code review
- CI/CD körs EFTER push (för sent!)
- Konflikter upptäcks efter merge

---

### ❌ Fel #2: Arbeta Direkt på Develop

```bash
# ALDRIG gör detta:
git checkout develop
# ... gör ändringar direkt ...
```

**Lösning:** ALLTID skapa feature branch först!

---

### ❌ Fel #3: Force Push till Develop/Main

```bash
# ALDRIG NÅGONSIN gör detta:
git push --force origin develop   # 💀 DÖDSSTÖT!
git push --force origin main      # 💀💀💀
```

**Varför?** Du kan radera andras arbete permanent!

**Lösning:** Branch protection på GitHub blockerar force-push!

---

### ❌ Fel #4: Merge Utan att Testa

```bash
# Dåligt:
git merge feature/untested
git push
# 💥 Develop är nu trasig!

# Bra:
dotnet test                    # Testa lokalt FÖRST
git merge feature/tested       # Merge EFTER test
dotnet test                    # Testa IGEN efter merge
git push
```

---

### ❌ Fel #5: Glömma att Pusha

```bash
# Du har committat lokalt men...
git commit -m "feat: add feature"
# ... och stänger laptopen

# Nästa dag:
Teammedlem: "Var är featuren?"
Du: "Jag commita den igår!"
# Men den finns bara på DIN dator!
```

**Lösning:** Commita OCH pusha innan du slutar för dagen!

```bash
git commit -m "feat: add feature"
git push origin feature/my-feature   # Nu är den säker!
```

---

## 📊 Visualisera Git Flow

### Enkel Feature

```
develop      ●────────────●──────────●
              ╲          ╱
feature/login  ●────●───●

Dag 1: Skapa feature branch
Dag 2-3: Utveckla & commita
Dag 3: Merge via PR
```

---

### Flera Parallella Features

```
develop          ●─────●───●─────●───●
                  ╲     ╲   ╲   ╱   ╱
feature/login      ●─●──●──●   ╱
feature/api             ●─●───●
feature/tests               ●─●

Alice jobbar på login
Bob jobbar på API
Charlie jobbar på tests
Alla mergas till develop via PR
```

---

### Hotfix Flow

```
main         ●─────────────●────●
              ╲           ╱    ╱
hotfix/bug     ●────●────●    ╱
                ╲            ╱
develop          ●──────────●

1. Hotfix från main
2. Merge till main
3. Merge till develop (viktigt!)
```

---

## ✅ Checklista

### Varje Gång Du Startar En Feature:

- [ ] Byt till develop: `git checkout develop`
- [ ] Pull senaste: `git pull origin develop`
- [ ] Skapa feature branch: `git checkout -b feature/namn`
- [ ] Verifiera branch: `git branch` (ska visa `* feature/namn`)

### Varje Gång Du Committar:

- [ ] Testa lokalt: `dotnet test`
- [ ] Kontrollera ändringar: `git status`
- [ ] Stagea filer: `git add .` eller specifika filer
- [ ] Commit med bra meddelande: `git commit -m "feat: beskrivning"`
- [ ] Pusha: `git push origin feature/namn`

### Varje Gång Du Skapar PR:

- [ ] CI/CD grön ✅
- [ ] Bra PR-beskrivning
- [ ] Request reviewers
- [ ] Vänta på approval
- [ ] Merge när allt OK

### Efter Merge:

- [ ] Byt till develop: `git checkout develop`
- [ ] Pull: `git pull origin develop`
- [ ] Radera feature branch: `git branch -d feature/namn`
- [ ] Radera remote: `git push origin --delete feature/namn`

---

## 🎓 Pro-Tips

**Tip 1: Alias för Vanliga Kommandon**

Lägg till i `~/.gitconfig`:

```
[alias]
    co = checkout
    br = branch
    ci = commit
    st = status
    lg = log --oneline --graph --all
```

Då kan du skriva:

```bash
git co develop          # istället för git checkout develop
git br                  # istället för git branch
git st                  # istället för git status
```

---

**Tip 2: Alltid Synka Innan Du Börjar**

```bash
# Morgonrutin:
git checkout develop
git pull origin develop
git checkout -b feature/dagens-arbete
```

---

**Tip 3: Använd `git stash` för Snabba Bytare**

```bash
# Du jobbar på feature/login men måste snabbt fixa nåt på develop
git stash                    # Spara ändringar tillfälligt
git checkout develop
# ... fixa saker ...
git checkout feature/login
git stash pop                # Återställ ändringar
```

---

**Tip 4: Feature Branch Lever Max 1-2 Dagar**

Om din feature branch lever längre:

- Bryt ner featuren i mindre delar
- Mergea ofta

**Dåligt:** Feature branch lever 2 veckor → Konflikter garanterade!
**Bra:** Feature branch lever 1 dag → Merge snabbt, mindre konflikter

---

## 🔗 Relaterade Guider

- [Pull Request Workflow](../articles/pr_workflow_guide.md) - Varför ALDRIG pusha direkt
- [CI/CD Setup](../articles/ci_cd_setup_github_actions.md) - Branch protection och automation
- [Scrum Cheatsheet](scrum_cheatsheet.md) - User Stories och sprint-planering

---

## 🎯 Sammanfattning

**Tre Gyllene Regler:**

1. **ALDRIG pusha direkt till `main` eller `develop`**
2. **ALLTID arbeta i feature branches**
3. **ALLTID skapa Pull Requests för merge**

**Grundläggande Flow:**

```bash
git checkout develop
git pull origin develop
git checkout -b feature/my-feature
# ... arbeta ...
git add .
git commit -m "feat: beskrivning"
git push origin feature/my-feature
# Skapa PR på GitHub
# Vänta på CI/CD + approval
# Merge!
```

**Det är så enkelt!** 🎉

---

_© Campus Mölndal 2026 - Test och Kvalitetssäkring CLO25_

**"Branch early, commit often, merge fast"** 🚀
