---
marp: true
theme: nion-dark
paginate: true
---

# Installationsguide
## Allt du behöver för att komma igång

---

## Installationsordning

1. **Git** ← börja här, allt annat beror på det
2. **GitHub-konto**
3. **IDE** (Visual Studio eller Rider)
4. **.NET SDK 10**
5. **VS Code**
6. **Obsidian** (rekommenderas)

---

## 1. Git

Ladda ner från **git-scm.com**

Under installationen:
- Välj **"Git Bash"** som standardterminal
- Allt annat kan vara default

Verifiera i **Git Bash**:
```bash
git --version
```

---

## Vad är Git Bash?

En riktig terminal som fungerar likadant på alla datorer.

Vi använder **alltid** Git Bash — inte cmd, inte PowerShell.

```bash
# Navigera
ls          # lista filer
cd mapp/    # gå in i mapp
pwd         # var är jag?
```

---

## Konfigurera Git

Öppna **Git Bash** och kör:

```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
git config --global core.autocrlf false
```

Verifiera:
```bash
git config --list
```

---

## 2. GitHub-konto

Gå till **github.com** och skapa ett konto.

- Använd en email du kommer ihåg
- Välj ett professionellt användarnamn
  - ✅ `anna-svensson` eller `annasv`
  - ❌ `xXgamer2007Xx`

Vi kopplar kontot till skolan senare under kursen.

---

## 3. IDE — välj en

**Visual Studio Community** (gratis)
- windows.microsoft.com → sök "Visual Studio Community"
- Välj dessa workloads:
  - ✅ **.NET desktop development**
  - ✅ **ASP.NET and web development**
  - ✅ **Data storage and processing**

**JetBrains Rider** (betald, men gratis med studentlicens)
- jetbrains.com/rider
- Aktivera med din skolmail på jetbrains.com/studerande

Osäker? Ta **Visual Studio** — det är vanligast i branschen.

---

## 4. .NET SDK 10

Ladda ner från **dot.net**

Verifiera i **Git Bash**:
```bash
dotnet --version
# ska visa 10.x.x
```

Om det inte fungerar — stäng och öppna Git Bash igen.

---

## 5. VS Code

Ladda ner från **code.visualstudio.com**

Installera extensions:
- **C# Dev Kit** (Microsoft)
- **Markdown Preview Enhanced**
- **GitLens**

VS Code används för markdown, config-filer och allt som inte är C#-projekt.

---

## 6. Obsidian (rekommenderas)

Ladda ner från **obsidian.md**

Ett kraftfullt anteckningsverktyg som använder markdown.
Perfekt för att hålla koll på det vi går igenom.

Inte ett krav — men du kommer att tacka dig själv senare.

---

## Checklista

Öppna **Git Bash** och kör:

```bash
git --version       # Git fungerar
dotnet --version    # .NET SDK 10
code --version      # VS Code
```

Alla tre ska ge ett versionsnummer — inte ett felmeddelande.

---

## Problem?

1. Stäng och öppna Git Bash igen
2. Starta om datorn
3. Fråga en klasskamrat
4. Fråga läraren

Kämpa inte ensam i mer än 15 minuter.
