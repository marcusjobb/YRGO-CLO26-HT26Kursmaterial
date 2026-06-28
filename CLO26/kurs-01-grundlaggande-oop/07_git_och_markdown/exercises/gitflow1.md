---
title: Git Flow – gruppövning
author: Marcus Ackre Medina
type: exercise
topic: git
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/Assignments/OOP/excersises/gitflow/gitflow1.md"
description: "En grundläggande Git Flow-övning där ni jobbar i grupp med feature branches, pull requests och merge till dev och main."
tags: ["bash", "git", "gitflow", "grupparbete", "pull-request"]
week_fit: []
---

# Git Flow – gruppövning

🟢

**Scenario:** Ni är ett team som bygger ett gemensamt projekt. Alla jobbar på varsin feature, och ingen ska kliva på någon annans tår. Lösningen heter Git Flow.

## Steg för steg

### 1. Skapa repo + branch-regler

En person skapar ett repo på GitHub med README. Gå till Settings → Branches → Add rule. Skydda `main` och `develop` — kräv pull request för merge.

### 2. Bjud in gruppen

Settings → Collaborators → Add people. Alla måste ha tillgång för att kunna pusha och göra PR.

### 3. Klona och skapa feature branch

```bash
git clone <repo-url>
cd <repo-namn>
git checkout -b feature/ditt-namn
```

Skapa en fil, `ditt-namn.md`, med en programmeringsfråga.

```bash
git add .
git commit -m "Lägg till ditt-namn.md med fråga"
git push origin feature/ditt-namn
```

### 4. Pull request

Gå till GitHub, skapa PR från din feature branch → `develop`. Låt en gruppkamrat granska och merga.

### 5. Uppdatera local + ny branch

```bash
git checkout develop
git pull origin develop
git checkout -b feature/ditt-namn-svar
```

Svara på någon annans fråga i deras fil. Committa, pusha, PR igen.

### 6. Merge-konflikt

När två personer ändrat samma fil kan det bli konflikt:

```bash
git checkout develop
git pull origin develop
git checkout feature/ditt-namn-svar
git merge develop
```

Lös konflikten i filen, spara, add + commit + push.

### 7. Klart

När alla frågor är besvarade → PR från `develop` till `main`. Sen PR från `main` tillbaka — klart!

---

<details>
<summary>💡 Tips 1 – Branch-namn</summary>

Använd `feature/` prefix för allt nytt. Det håller historiken ren och gör det lätt att se vad varje branch gör.

```bash
git checkout -b feature/user-login
# istället för
git checkout -b user-login
```

</details>

<details>
<summary>💡 Tips 2 – När konflikten kommer</summary>

Merge-konflikter är normalt. Öppna filen, leta efter:

```
<<<<<<< HEAD
Din kod
=======
Annans kod
>>>>>>> branch-namn
```

Behåll det ni vill ha, ta bort markörerna, spara.

</details>

<details>
<summary>💡 Tips 3 – Håll utkik</summary>

Kör `git fetch -p` regelbundet för att städa bort gamla branch-referenser. Och `git log --oneline --graph` för att se exakt vad som hänt.

</details>

---

<details>
<summary>✅ Förslagslösning – hela flödet</summary>

```bash
# === SETUP ===
# Skapa repo på GitHub, skydda main och develop

# === FEATURE — skapa fråga ===
git clone <repo-url>
cd <repo>
git checkout -b feature/anna
echo "Vad är en delegate i C#?" > anna.md
git add .
git commit -m "Lägg till anna.md med fråga"
git push origin feature/anna
# → PR på GitHub → merge till develop

# === FEATURE — svara ===
git checkout develop
git pull origin develop
git checkout -b feature/anna-svar
echo "En delegate är en typ som pekar på en metod." >> anna.md
git add .
git commit -m "Svara på anna.md"
git push origin feature/anna-svar
# → PR → merge till develop

# === KLART ===
# PR develop → main
```

</details>

---

**Förväntat resultat:** Alla i gruppen har skapat och mergat minst en feature branch. Ni har stött på (och löst) en merge-konflikt. Repot har en ren historia med feature/ → develop → main.

---

## Reflektion

- Vad händer om två personer gör PR till develop samtidigt?
- Varför skydda main med branch-regler istället för att pusha direkt?
- Hur märker ni att Git Flow hjälper (eller stjälper) i ett litet team?

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
