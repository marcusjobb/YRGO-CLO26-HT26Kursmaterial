# Övning 1 — Git installation och konfiguration

🟢 Grundnivå

## Mål

Efter den här övningen har du:
- Git installerat och konfigurerat
- GitHub-konto kopplat till Git via webbläsaren
- Verifierat att allt fungerar i Git Bash

## Uppgift

Följ installationsguiden (`../notes/installationsguide.md`) och genomför alla steg.
Det här är ingen kodövning — det är din utvecklarmiljös grundsetup.

## Steg

### 1. Installera Git
Ladda ner från **git-scm.com/download/win** och installera med Git Bash som terminal.

```bash
git --version
```

### 2. Konfigurera Git
Öppna **Git Bash** och kör:

```bash
git config --global user.name "Ditt Namn"
git config --global user.email "din@email.com"
git config --global init.defaultBranch main
git config --global core.autocrlf false
```

### 3. Koppla Git till GitHub
När du pushar för första gången öppnas en webbläsare där du loggar in på GitHub.
Det är allt som krävs — Git kommer ihåg det.

Prova genom att klona ett repo:

```bash
git clone https://github.com/ditt-användarnamn/ett-repo.git
```

### 4. Installera IDE + .NET SDK

Verifiera:
```bash
dotnet --version
code --version
```

## Förväntat resultat

Alla dessa kommandon ger svar utan felmeddelanden:

```
$ git --version
git version 2.48.x

$ git config user.name
Ditt Namn

$ dotnet --version
10.0.x

$ code --version
1.x.x
```

## Tips

> Fastnar du? Kör `git config --list` för att se vad som är inställt.

<details>
<summary>Vanliga problem</summary>

**"git is not recognized"** — stäng och öppna Git Bash igen efter installationen.

**"dotnet is not found"** — stäng och öppna Git Bash igen efter .NET-installationen.

</details>

---

## Bonus — SSH-nycklar (gör detta om du behöver det)

Du behöver SSH om något av detta stämmer:

- Du har **två GitHub-konton** på samma dator (t.ex. privat + jobb) — med SSH slipper du logga in och ut
- En server eller tjänst **kräver SSH** för att ansluta (vanligt hos hostingbolag och i CI/CD-pipelines)
- Du vill ha ett extra lager säkerhet — allt fler tjänster kräver SSH + 2FA för att förhindra kapade konton

<details>
<summary>Sätt upp SSH-nyckel</summary>

```bash
ssh-keygen -t ed25519 -C "din@email.com"
cat ~/.ssh/id_ed25519.pub
```

Kopiera utskriften → GitHub → Settings → SSH keys → New SSH key.

Testa:
```bash
ssh -T git@github.com
```

Du ska se: `Hi dittnamn! You've successfully authenticated...`

> Fastnar du på SSH? Kör `ssh -vT git@github.com` för mer detaljer om vad som går fel.

</details>
