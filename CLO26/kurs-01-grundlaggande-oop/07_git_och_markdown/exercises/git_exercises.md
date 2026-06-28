---

title: Git Workflow Övningar 🔄
author: Marcus Ackre Medina
type: exercise
topic: git
difficulty: 3
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/git/exercises/git-exercises.md"
description: "Följ instruktionerna nedan för att skapa ditt första Git repository och genomföra den grundläggande Git-ritualen."
tags: ["bash", "exercise", "git", "installation", "visual-studio", "workflow", "övningar"]
week_fit: []
---

# Git Workflow Övningar 🔄

🔴


## Övning 1: Grundläggande Git Workflow (Lätt)

Följ instruktionerna nedan för att skapa ditt första Git repository och genomföra den grundläggande Git-ritualen.

### Kommandoskelett:
```bash

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
# TODO: Skapa ny katalog för ditt projekt
mkdir "MinProjektNamn"

# TODO: Navigera in i katalogen
cd "MinProjektNamn"

# TODO: Initiera Git repository
git init

# TODO: Skapa en README.md fil
echo "# Mitt Första Git Projekt" > README.md

# TODO: Kontrollera Git status
git status

# TODO: Lägg till filen till staging area
git add README.md

# TODO: Skapa första commit
git commit -m "Initial commit with README"

# TODO: Visa commit-historik
git log --oneline
```

### Förväntad Output:
```
Initialized empty Git repository in /path/to/MinProjektNamn/.git/
On branch main
Untracked files:
  (use "git add <file>..." to include in what will be committed)
        README.md

[main (root-commit) abc1234] Initial commit with README
 1 file changed, 1 insertion(+)
 create mode 100644 README.md

abc1234 Initial commit with README
```

<details>
<summary>💡 Tips 1: Kataloghantering</summary>

- Använd `mkdir` för att skapa ny katalog
- `cd` för att navigera in i katalogen
- Kontrollera var du är med `pwd` (optional)

</details>

<details>
<summary>💡 Tips 2: Git Initialisering</summary>

- `git init` skapar `.git/` mappen med hela Git-infrastrukturen
- `git status` visar nuvarande läge av ditt repository
- "Untracked files" betyder att Git inte följer filerna ännu

</details>

<details>
<summary>💡 Tips 3: Add och Commit</summary>

- `git add` flyttar filer till staging area (förberedelse för commit)
- Commit-meddelandet ska beskriva vad som ändrats
- `git log --oneline` visar kompakt commit-historik

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```bash
# Skapa ny katalog för ditt projekt
mkdir "MinProjektNamn"

# Navigera in i katalogen
cd "MinProjektNamn"

# Initiera Git repository
git init

# Skapa en README.md fil
echo "# Mitt Första Git Projekt" > README.md

# Kontrollera Git status
git status

# Lägg till filen till staging area
git add README.md

# Skapa första commit
git commit -m "Initial commit with README"

# Visa commit-historik
git log --oneline
```

</details>

---

## Övning 2: Branching och Merging (Medium)

Komplettera Git-kommandona för att skapa en ny feature branch, göra ändringar, och merge tillbaka till main.

### Kommandoskelett:
```bash
# Startposition: Du är i ditt Git repository från övning 1

# TODO: Kontrollera nuvarande branch
git branch

# TODO: Skapa och växla till ny branch "feature/add-description"
git checkout -b "feature/add-description"

# TODO: Skapa en beskrivning.txt fil
echo "Detta är en beskrivning av mitt projekt." > beskrivning.txt

# TODO: Lägg till och committa den nya filen
git add beskrivning.txt
git commit -m "Add project description"

# TODO: Visa alla branches
git branch

# TODO: Växla tillbaka till main branch
git checkout main

# TODO: Merge feature branch in i main
git merge "feature/add-description"

# TODO: Ta bort feature branch (städa upp)
git branch -d "feature/add-description"

# TODO: Visa slutgiltig historik
git log --oneline --graph
```

### Förväntad Output:
```
* main
Switched to a new branch 'feature/add-description'
[feature/add-description 1234567] Add project description
 1 file changed, 1 insertion(+)
 create mode 100644 beskrivning.txt
  feature/add-description
* main
Switched to branch 'main'
Updating abc1234..1234567
Fast-forward
 beskrivning.txt | 1 +
 1 file changed, 1 insertion(+)
 create mode 100644 beskrivning.txt
Deleted branch feature/add-description (was 1234567).
*   1234567 Add project description
*   abc1234 Initial commit with README
```

<details>
<summary>💡 Tips 1: Branch Operations</summary>

- `git branch` visar alla branches, `*` markerar aktiv branch
- `git checkout -b "branchname"` skapar och växlar till ny branch samtidigt
- Branch-namn kan innehålla `/` för organisation (feature/, bugfix/, etc.)

</details>

<details>
<summary>💡 Tips 2: Arbetsgång på Branch</summary>

- Jobba normalt med add och commit på din feature branch
- `git checkout main` växlar tillbaka till main branch
- Observera att din fil inte finns på main förrän efter merge

</details>

<details>
<summary>💡 Tips 3: Merge och Städning</summary>

- `git merge branchname` slår ihop ändringar från branchen
- `git branch -d branchname` tar bort branch efter lyckad merge
- `--graph` flaggan visar en visuell representation av commits

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```bash
# Kontrollera nuvarande branch
git branch

# Skapa och växla till ny branch "feature/add-description"
git checkout -b "feature/add-description"

# Skapa en beskrivning.txt fil
echo "Detta är en beskrivning av mitt projekt." > beskrivning.txt

# Lägg till och committa den nya filen
git add beskrivning.txt
git commit -m "Add project description"

# Visa alla branches
git branch

# Växla tillbaka till main branch
git checkout main

# Merge feature branch in i main
git merge "feature/add-description"

# Ta bort feature branch (städa upp)
git branch -d "feature/add-description"

# Visa slutgiltig historik
git log --oneline --graph
```

</details>

---

## Övning 3: Simulera Team Collaboration med Konflikt (Svår)

Denna övning simulerar arbete i team där konflikter uppstår. Du kommer att skapa två branches med ändringar i samma fil och lösa merge-konflikten.

### Kommandoskelett:
```bash
# Startposition: Du är i main branch från föregående övning

# TODO: Skapa och växla till branch "feature/update-readme"
git checkout -b "feature/update-readme"

# TODO: Modifiera README.md (lägg till en rad)
echo "## Beskrivning" >> README.md
echo "Detta projekt demonstrerar Git workflow." >> README.md

# TODO: Committa ändringarna
git add README.md
git commit -m "Update README with description section"

# TODO: Växla tillbaka till main
git checkout main

# TODO: Skapa och växla till en annan branch "feature/update-readme-alt"
git checkout -b "feature/update-readme-alt"

# TODO: Modifiera README.md på ett annat sätt (ersätt innehåll)
echo "# Mitt Fantastiska Git Projekt" > README.md
echo "## Installation" >> README.md
echo "Kör git clone för att ladda ner." >> README.md

# TODO: Committa ändringarna
git add README.md
git commit -m "Update README with installation section"

# TODO: Växla tillbaka till main och merge första branchen
git checkout main
git merge "feature/update-readme"

# TODO: Försök merge den andra branchen (detta skapar konflikt!)
git merge "feature/update-readme-alt"

# TODO: Visa konfliktens status
git status

# TODO: Öppna README.md och lös konflikten manuellt
# (Du måste redigera filen och ta bort conflict markers)

# TODO: Efter att ha löst konflikten, lägg till och committa
git add README.md
git commit -m "Resolve merge conflict in README.md"

# TODO: Städa upp branches
git branch -d "feature/update-readme"
git branch -d "feature/update-readme-alt"

# TODO: Visa final historik
git log --oneline --graph
```

### Förväntad Output:
```bash
Switched to a new branch 'feature/update-readme'
[feature/update-readme 2345678] Update README with description section
 1 file changed, 2 insertions(+)
Switched to branch 'main'
Switched to a new branch 'feature/update-readme-alt'
[feature/update-readme-alt 3456789] Update README with installation section
 1 file changed, 3 insertions(+)
Updating abc1234..2345678
Fast-forward
 README.md | 2 ++
 1 file changed, 2 insertions(+)
Auto-merging README.md
CONFLICT (content): Merge conflict in README.md
Automatic merge failed; fix conflicts and then commit the result.

On branch main
You have unmerged paths.
  (fix conflicts and run "git commit")
  (use "git merge --abort" to abort the merge)

Unmerged paths:
  (use "git add <file>..." to mark resolution)
        both modified:   README.md

[main 4567890] Resolve merge conflict in README.md
```

<details>
<summary>💡 Tips 1: Konfliktsituation</summary>

- Två branches ändrar samma fil = potentiell konflikt
- Git kan inte automatiskt avgöra vilken ändring som ska behållas
- Detta är normalt i team-utveckling!

</details>

<details>
<summary>💡 Tips 2: Identifiera Konflikt</summary>

- `git status` visar "both modified" för filer med konflikt
- Öppna filen och leta efter conflict markers:
  ```
  Kod från branch som mergas
  ```

</details>

<details>
<summary>💡 Tips 3: Lösa Konflikt</summary>

- Redigera filen och behåll vad du vill ha
- Spara filen, `git add`, och `git commit`
- Du kan också använda `git merge --abort` för att avbryta

</details>

<details>
<summary>✅ Lösningsförslag</summary>

```bash
# Skapa och växla till branch "feature/update-readme"
git checkout -b "feature/update-readme"

# Modifiera README.md (lägg till en rad)
echo "## Beskrivning" >> README.md
echo "Detta projekt demonstrerar Git workflow." >> README.md

# Committa ändringarna
git add README.md
git commit -m "Update README with description section"

# Växla tillbaka till main
git checkout main

# Skapa och växla till en annan branch "feature/update-readme-alt"
git checkout -b "feature/update-readme-alt"

# Modifiera README.md på ett annat sätt (ersätt innehåll)
echo "# Mitt Fantastiska Git Projekt" > README.md
echo "## Installation" >> README.md
echo "Kör git clone för att ladda ner." >> README.md

# Committa ändringarna
git add README.md
git commit -m "Update README with installation section"

# Växla tillbaka till main och merge första branchen
git checkout main
git merge "feature/update-readme"

# Försök merge den andra branchen (detta skapar konflikt!)
git merge "feature/update-readme-alt"

# Visa konfliktens status
git status

# Redigera README.md manuellt för att lösa konflikten
# Exempel på lösning:
cat > README.md << EOF
# Mitt Fantastiska Git Projekt

## Beskrivning
Detta projekt demonstrerar Git workflow.

## Installation
Kör git clone för att ladda ner.
EOF

# Efter att ha löst konflikten, lägg till och committa
git add README.md
git commit -m "Resolve merge conflict in README.md"

# Städa upp branches
git branch -d "feature/update-readme"
git branch -d "feature/update-readme-alt"

# Visa final historik
git log --oneline --graph
```

</details>

---

## Bonusövning: Remote Repository Simulation (Avancerad)

Simulera arbete med remote repository genom att skapa två lokala "kloner" av samma projekt.

### Förberedelser:
```bash
# TODO: Skapa en "server" katalog som simulerar GitHub
mkdir ../git-server
cd ../git-server
git init --bare shared-project.git

# TODO: Gå tillbaka till ditt projekt och lägg till remote
cd ../MinProjektNamn
git remote add origin ../git-server/shared-project.git

# TODO: Pusha ditt projekt till "servern"
git push -u origin main

# TODO: Skapa en andra "utvecklares" klon
cd ..
git clone git-server/shared-project.git developer2-project
cd developer2-project

# TODO: Gör ändringar som "utvecklare 2"
echo "Ändring från utvecklare 2" >> README.md
git add README.md
git commit -m "Changes from developer 2"
git push origin main

# TODO: Gå tillbaka till original projekt och hämta ändringar
cd ../MinProjektNamn
git pull origin main
```

### Förväntad Output:
```
Bare repository successfully created
[main 4567890] Resolve merge conflict in README.md
Branch 'main' set up to track remote branch 'main' from 'origin'.
Cloning into 'developer2-project'...
done.
[main 5678901] Changes from developer 2
Updating 4567890..5678901
Fast-forward
 README.md | 1 +
 1 file changed, 1 insertion(+)
```

<details>
<summary>💡 Tips: Remote Repository Koncept</summary>

- `--bare` repository har ingen working directory, bara Git data
- `git remote add origin URL` länkar ditt lokala repo till remote
- `git push -u origin main` pushar och sätter upp tracking
- `git pull` = `git fetch` + `git merge` i ett kommando

</details>

<details>
<summary>✅ Komplett Lösning</summary>

Se kommandosekvensen ovan - denna övning demonstrerar hela remote workflow som används i verkliga projekt med GitHub/GitLab.
</details>

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
