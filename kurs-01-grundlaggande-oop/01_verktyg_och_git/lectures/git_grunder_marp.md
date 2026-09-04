---
marp: true
theme: nion-dark
paginate: true
---

# Git — Versionshantering
## Din tidsmaskin för kod

---

## Vad är Git?

Git håller koll på varje ändring du gör i dina filer.

- Aldrig mer "final_v2_RIKTIG_denna_gång.cs"
- Jobba flera personer på samma kod utan kaos
- Gå tillbaka till hur koden såg ut igår, förra veckan, när som helst

**Tänk på det som Ctrl+Z som aldrig tar slut — och som du kan dela med andra.**

---

## Tre områden du måste känna till

```
┌─────────────────┐    git add    ┌──────────────┐    git commit    ┌────────────┐
│ Working         │ ───────────►  │   Staging    │ ───────────────► │   Local    │
│ Directory       │               │   Area       │                  │   Repo     │
│ (dina filer)    │               │ (förberedd)  │                  │ (historik) │
└─────────────────┘               └──────────────┘                  └────────────┘
                                                                           │
                                                                      git push
                                                                           │
                                                                           ▼
                                                                    ┌────────────┐
                                                                    │   GitHub   │
                                                                    │  (remote)  │
                                                                    └────────────┘
```

---

## Git Bash — din nya bästa vän

Vi kör **alltid** Git Bash. Inte cmd. Inte PowerShell.

Git Bash följer med när du installerar Git for Windows.

```bash
ls          # lista filer i mappen
cd projekt/ # gå in i mappen "projekt"
pwd         # visa var du är
mkdir ny/   # skapa en ny mapp
```

**Varför?** Samma kommandon fungerar på Windows, Mac och Linux.
När du väl lärt dig kan du jobba på vilken dator som helst.

---

## Installera Git

1. Gå till **git-scm.com** och ladda ner
2. Under installationen — viktiga val:
   - ✅ **Git Bash** som standardterminal
   - ✅ Override default branch name: **main**
   - Allt annat kan vara default

Öppna **Git Bash** och verifiera:
```bash
git --version
```

---

## Konfigurera Git (görs en gång)

Öppna **Git Bash**:

```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
git config --global init.defaultBranch main
git config --global core.autocrlf false
```

`core.autocrlf false` — hindrar Windows från att förstöra radslut i filer.
Detta är viktigt. Glöm inte det.

---

## SSH-nyckel — säkrare än lösenord

```bash
# Generera en nyckel
ssh-keygen -t ed25519 -C "din@email.com"
# Tryck Enter tre gånger (default-inställningar)

# Visa nyckeln
cat ~/.ssh/id_ed25519.pub
```

Kopiera hela utskriften — den börjar med `ssh-ed25519`.

Gå till **GitHub → Settings → SSH and GPG keys → New SSH key**
Klistra in och spara.

---

## Testa att SSH fungerar

```bash
ssh -T git@github.com
```

Om det fungerar ser du:
```
Hi dittnamn! You've successfully authenticated,
but GitHub does not provide shell access.
```

Om du ser `Permission denied` — fråga läraren, det är fixbart.

---

## Dagliga Git-kommandon

```bash
git status              # vad har jag ändrat?
git add filnamn.cs      # förbered en fil
git add .               # förbered alla ändrade filer
git commit -m "meddelande"  # spara med beskrivning
git push                # skicka till GitHub
git pull                # hämta från GitHub
```

**Ritualen:** status → add → commit → push
Kör den varje gång du är klar med något.

---

## Bra commit-meddelanden

```bash
# Bra — beskriver VAD och VARFÖR
git commit -m "Lägg till inloggningsfunktion"
git commit -m "Fixa krasch när användaren skriver tomt"
git commit -m "Byt ut hardcodad URL mot konfiguration"

# Dåligt — säger ingenting
git commit -m "fix"
git commit -m "grejer"
git commit -m "aaa"
```

Commit-historiken är din dokumentation. Framtida du kommer tacka dig själv.

---

## .gitignore — vad som INTE ska committas

Skapa en `.gitignore`-fil i projektets rot:

```
# Bygg-filer (genereras automatiskt)
bin/
obj/

# IDE-filer
.vs/
*.user

# Aldrig, någonsin
.env
secrets.json
```

Lägg till `.gitignore` **innan** första commit. Annars är det jobbigt att fixa.

---

## Klona ett befintligt repo

```bash
# Hämta ett repo från GitHub
git clone git@github.com:användarnamn/repo-namn.git

# Gå in i mappen
cd repo-namn
```

SSH-adressen hittar du på GitHub under **Code → SSH**.

---

## Starta ett nytt repo

```bash
# Gå till din projektmapp
cd /c/Users/dittnamn/projekt

# Initiera git
git init

# Lägg till remote (skapa repot på GitHub först)
git remote add origin git@github.com:dittnamn/repo-namn.git

# Första commit
git add .
git commit -m "Initial commit"
git push -u origin main
```

---

## Clean Code — Git-historiken

> En git-historik är kod också. Den ska vara läsbar.

```bash
# ❌ Ingen aning om vad som hände
git commit -m "fix"
git commit -m "changes"
git commit -m "asdfgh"
git commit -m "nu funkar det"

# ✅ Historiken berättar en historia
git commit -m "Lägg till validering av e-postformat"
git commit -m "Fixa krasch vid tomt lösenordsfält"
git commit -m "Refaktorera inloggningslogik till egen metod"
```

**Reglerna:**
- En commit = en sak
- Presens, aktivt verb: "Lägg till", "Fixa", "Ta bort"
- Om du skriver "och" — dela upp commiten

---

## Vanliga problem

**"Permission denied (publickey)"**
→ SSH-nyckeln är inte kopplad till GitHub. Gå igenom SSH-stegen igen.

**"src refspec main does not match any"**
→ Du har inte gjort någon commit än. Kör `git add .` och `git commit` först.

**"Updates were rejected"**
→ GitHub har ändringar du inte har lokalt. Kör `git pull` först.

Kämpa inte ensam mer än 15 minuter — fråga!

---

## Sammanfattning

- Git spårar alla ändringar i din kod
- Vi använder alltid **Git Bash** som terminal
- Ritualen: `git add` → `git commit` → `git push`
- `.gitignore` ska alltid finnas
- Commit-meddelanden ska vara beskrivande

**Nästa:** Dags att testa allt i praktiken!
