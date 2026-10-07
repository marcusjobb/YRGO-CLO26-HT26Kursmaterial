# Installationsguide — Gör detta innan dag 2

Den här guiden är din **hemläxa från dag 1**. Följ varje steg noga.
På dag 2 börjar vi koda direkt — då måste allt fungera.

**15-minutersregeln:** Fastnar du i mer än 15 minuter — be om hjälp.
1. Fråga klassen (Discord/Teams)
2. Fråga en AI
3. Fråga läraren

Kämpa inte ensam längre än så.

---

## Vad du ska installera

1. **Git + Git Bash** ← börja här
2. **En IDE** — välj en av: Visual Studio Community, JetBrains Rider, VS Code
3. **.NET SDK 10**
4. **GitHub-konto + SSH-nyckel**
5. **VS Code** (oavsett vilken IDE du väljer — används för annat)
6. **Obsidian** (rekommenderas)

---

## 1. Git och Git Bash

Git Bash är terminalen vi använder i hela kursen. Installera den först.

### Installera

1. Gå till **git-scm.com/download/win**
2. Ladda ner och kör installationsfilen

Under installationen — dessa inställningar är viktiga:

| Steg | Välj |
|------|------|
| Default editor | Use Visual Studio Code as Git's default editor |
| Branch name | Override → skriv **main** |
| PATH environment | Git from the command line and also from 3rd-party software |
| SSH executable | Use bundled OpenSSH |
| HTTPS transport | Use the OpenSSL library |
| Line endings | **Checkout as-is, commit as-is** |
| Terminal emulator | **Use MinTTY** (Git Bash) |

Allt annat kan vara default.

### Verifiera

Öppna **Git Bash** (sök efter det i startmenyn) och kör:

```bash
git --version
```

Du ska se något som `git version 2.47.0` eller nyare.

### Konfigurera (en gång per dator)

```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
git config --global init.defaultBranch main
git config --global core.autocrlf false
```

Byt ut `"Ditt Namn"` och `"din@email.com"` mot dina riktiga uppgifter.

Verifiera:
```bash
git config --list
```

---

## 2. Välj din IDE

Du behöver en av dessa tre. Välj den du är bekväm med — eller fråga läraren om du är osäker.

---

<details>
<summary>Visual Studio Community — gratis, Windows (rekommenderas för nybörjare)</summary>

1. Gå till **visualstudio.microsoft.com/vs/community/**
2. Ladda ner och kör installationsfilen
3. I **Workloads**-steget — markera:
   - ✅ **.NET desktop development**
   - ✅ **ASP.NET and web development**
   - ✅ **Data storage and processing**
4. Klicka **Install** — det tar en stund (5–10 GB)

**Verifiera:** Starta Visual Studio → File → New → Project → Console App (.NET)
Om projektet skapas utan fel är allt klart.

**Var hamnar dina projekt?**
`Documents\Source\Repos\`

</details>

---

<details>
<summary>JetBrains Rider — gratis med studentlicens</summary>

#### Steg 1: Skaffa studentlicens

1. Gå till **jetbrains.com/community/education/#students**
2. Klicka **Apply now**
3. Fyll i med din skolmail (eller ladda upp studentbevis)
4. Du får ett mail med aktiveringslänk — kan ta några dagar

#### Steg 2: Installera Rider

1. Gå till **jetbrains.com/rider/download/**
2. Ladda ner och installera
3. Vid första start — logga in med ditt JetBrains-konto
4. Aktivera Rider via studentlicensen

**Verifiera:** New Solution → Console Application → skapa projektet.
Om det fungerar är allt klart.

**Var hamnar dina projekt?**
`C:\Users\dittnamn\IdeaProjects\`

</details>

---

<details>
<summary>VS Code — för den som är bekväm med terminalen</summary>

1. Gå till **code.visualstudio.com**
2. Ladda ner och installera
3. Installera dessa extensions (Ctrl+Shift+X):
   - **C# Dev Kit** (Microsoft)
   - **GitLens**

Du skapar projekt via Git Bash + `dotnet new` — se övning 2 för instruktioner.

</details>

---

## 3. .NET SDK 10

1. Gå till **dot.net** och ladda ner .NET 10 SDK
2. Kör installationsfilen

**Verifiera i Git Bash:**
```bash
dotnet --version
```

Du ska se `10.x.x`. Om ingenting händer — stäng och öppna Git Bash igen.

---

## 4. GitHub-konto

### Skapa konto

1. Gå till **github.com/signup**
2. Välj ett professionellt användarnamn — det syns hos framtida arbetsgivare
   - ✅ `anna-svensson`
   - ❌ `xXgamer2007Xx`
3. Verifiera din e-postadress

### Koppla Git till GitHub

När du pushar kod för första gången behöver Git veta att det är du.
Det enklaste sättet är att logga in via webbläsaren när Git frågar.

Prova att klona ett repo så triggas inloggningen:

```bash
git clone https://github.com/ditt-användarnamn/ett-repo.git
```

En webbläsare öppnas och du loggar in på GitHub. Klart — Git kommer ihåg det.

> SSH-nycklar tar vi senare i kursen — de behövs när man hanterar
> flera GitHub-konton på samma dator.

### GitHub Student Developer Pack (valfritt)

Gratis tillgång till GitHub Pro, Copilot, Azure-krediter, JetBrains-verktyg m.m.
Helt frivilligt, men rekommenderat.

**Viktigt:** Skapa **inte** ett nytt GitHub-konto för det här. Koppla din
skol-epost (`@skola.goteborg.se`) till kontot du redan skapade ovan:

1. Logga in på ditt GitHub-konto → **Settings → Emails → Add email address**
2. Lägg till din `@skola.goteborg.se`-adress och verifiera den (kolla skolmailen för länken)
3. Gå till **education.github.com/pack**
4. Klicka **"Get student benefits"** — logga in med samma GitHub-konto
5. Verifiera med skol-eposten — det brukar gå igenom snabbare än att ladda upp studiebevis

Kan ta några dagar om GitHub ändå vill granska manuellt. Fungerar utan detta också — det är en bonus, inte ett krav.

---

## 5. VS Code (oavsett IDE-val)

VS Code används för markdown, config-filer och allt utanför C#-projekt.

Ladda ner från **code.visualstudio.com** om du inte redan gjort det.

Extensions att installera:
- **Markdown Preview Enhanced**
- **GitLens**

---

## 6. Obsidian (rekommenderas, inte krav)

Ladda ner från **obsidian.md**

Anteckningsverktyg i markdown. Bra för att hålla koll på det vi går igenom.

---

## Slutkontroll

Öppna **Git Bash** och kör:

```bash
git --version
git config user.name
git config user.email
dotnet --version
```

Alla ska ge svar utan felmeddelanden.

Fungerar något inte — **15-minutersregeln gäller**:
1. Fråga klassen (Discord/Teams) med: vilket steg + screenshot på felmeddelandet
2. Fråga en AI
3. Fråga läraren

Vi löser det tillsammans på dag 2!
