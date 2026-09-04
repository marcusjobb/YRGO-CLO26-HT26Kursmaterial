# Vecka 1 — Verktyg, Git och Git Bash

**Tema:** Installation, versionshantering och terminalen
**Mål:** Alla studerande har fungerande miljö och kan grundläggande git-flöde

## Lärarens anteckningar

De studerande har förkunskapskrav i programmering men många har troligen glömt.
Vecka 1 handlar om att få alla på banan med rätt verktyg INNAN vi kodar.

### Hello World — editorgenomgång
Dag 2, efter installation: kör en Hello World live i IDE:n.
Visa hur man skapar projekt, hittar Program.cs, kör med F5/dotnet run.
Enkelt, snabbt, men de ser att IDE:n faktiskt fungerar och att de kan köra kod.
Det ger en liten dopaminboost och bekräftar att installationen lyckades.

```csharp
Console.WriteLine("Hej världen!");
Console.WriteLine("Kurs 1 är igång.");
```

### Klassens intressen — presentationsrundan
Under introt presenterar de studerande sig själva — namn, bakgrund och intressen.
Anteckna i `_teacher/klassens_intressen.md` medan de pratar.
Används för att anpassa teman i övningar och story-driven code under hela kursen.
Istället för alltid katter och bilar — använd det de faktiskt bryr sig om.

### Git Bash-strategin
Vi nämner aldrig cmd.exe eller PowerShell. Alla exempel använder Git Bash.
Installationsguiden säger "öppna Git Bash". Slides visar Git Bash. Övningarna
kräver Git Bash. Efter en vecka är det deras nya normal.

Git Bash följer med Git for Windows — de installerar det ändå.

## Publicera

```
publish:
  - 01_verktyg_och_git/README.md
  - 01_verktyg_och_git/lectures/kursintro_marp.md
  - 01_verktyg_och_git/lectures/om_utbildaren_marp.md
  - 01_verktyg_och_git/lectures/installationsguide_marp.md
  - 01_verktyg_och_git/lectures/git_grunder_marp.md
  - 01_verktyg_och_git/notes/installationsguide.md
  - 01_verktyg_och_git/notes/git_grunder.md
  - 01_verktyg_och_git/notes/git_konflikter.md
  - 01_verktyg_och_git/exercises/logik_01_metropolitanclub.md
  - 01_verktyg_och_git/exercises/diskutera_01_kassaapparaten.md
  - 01_verktyg_och_git/exercises/ovning_01_git_setup.md
  - 01_verktyg_och_git/exercises/klassovning_01_klass_repo.md
  - 01_verktyg_och_git/exercises/ovning_02_forsta_repot.md
  - 01_verktyg_och_git/exercises/ovning_03_hello_world.md
  - 01_verktyg_och_git/exercises/diskutera_03_kalle.md
  - 01_verktyg_och_git/exercises/training_vecka1.md
  - termer/git.md
  - termer/verktyg.md
  - termer/datatyper.md
  - termer/console.md
```

## Inlämning denna vecka

Ingen inlämning vecka 1.

## Checklista — alla studerande ska ha när veckan är slut

- [ ] Git installerat och konfigurerat (namn + email)
- [ ] Git Bash fungerar
- [ ] IDE installerad (VS Community eller Rider)
- [ ] VS Code installerat
- [ ] .NET SDK 10 installerat (`dotnet --version` fungerar i Git Bash)
- [ ] Obsidian installerat (rekommenderat)
- [ ] GitHub-konto skapat
- [ ] Kan klona ett repo, göra en ändring, commit och push
