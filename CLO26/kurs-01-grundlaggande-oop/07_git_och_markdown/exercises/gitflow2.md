---
title: Git Flow + Issues – organisera arbetet
author: Marcus Ackre Medina
type: exercise
topic: git
difficulty: 1
language: bash
status: adapted
marcus_voice: true
source: "Old_courses/2025/csharp/Assignments/OOP/excersises/gitflow/gitflow2.md"
description: "Bygg vidare på Git Flow med issue tracking på GitHub — skapa issues, koppla branches och stäng automatiskt vid merge."
tags: ["bash", "git", "gitflow", "issues", "github", "pull-request"]
week_fit: []
---

# Git Flow + Issues – organisera arbetet

🟢

**Scenario:** Förra övningen funkade, men ni hade ingen koll på vad som skulle göras. Nu inför ni issues — varje uppgift blir en issue, varje branch kopplas till sin issue, och allt stängs automatiskt när PR:n mergas.

## Steg för steg

### 1. Repo + issues

Skapa ett repo, skydda `main` och `develop`. Gå sedan till Issues-fliken och skapa 3-4 issues, tex:

| Issue | Beskrivning |
|-------|-------------|
| #1 | Skapa inloggningsfunktion |
| #2 | Implementera sökfält |
| #3 | Visa användarprofil |

Tilldela varje issue till en gruppmedlem.

### 2. Feature branch från issue

```bash
git clone <repo-url>
cd <repo>
git checkout -b feature/inloggning
```

Jobba på featuren. När du committar, referera till issuen:

```bash
git add .
git commit -m "Lägg till login-formulär #1"
git push origin feature/inloggning
```

### 3. PR som stänger issuenn

Skapa PR på GitHub från `feature/inloggning` → `develop`. I PR-beskrivningen, skriv:

```
Closes #1
```

När PR:n mergas → issuenn stängs automatiskt.

### 4. Uppdatera och nästa issue

```bash
git checkout develop
git pull origin develop
git checkout -b feature/sokfalt
```

Jobba, committa med `#2`, pusha, PR med `Closes #2`.

### 5. Release

När alla issues är stängda → PR `develop` → `main`. Klart.

---

<details>
<summary>💡 Tips 1 – Commit-meddelanden</summary>

Skriv `#1` i commit-meddelandet för att länka till issuern:

```bash
git commit -m "Lägg till login-formulär #1"
```

Det syns i GitHub som en referens på issuern. Användaren ser direkt vilka commits som hör till uppgiften.

</details>

<details>
<summary>💡 Tips 2 – Closes vs Referens</summary>

- `Closes #1` i PR-beskrivning → stänger issuern vid merge
- `#1` i commit → länkar bara, stänger inte

Använd `Closes` på PR:n, inte i varje commit. Då stängs issuern först när koden är godkänd och mergad.

</details>

<details>
<summary>💡 Tips 3 – Milestones</summary>

Skapa en milestone (t.ex. "Sprint 1") och lägg issues på den. Då ser ni när allt är klart för release.

</details>

---

<details>
<summary>✅ Förslagslösning – hela flödet</summary>

```bash
# 1. Skapa repo + issues #1, #2, #3 på GitHub

# 2. Feature #1
git clone <repo-url>
cd <repo>
git checkout -b feature/inloggning
echo "<form>Login</form>" > login.html
git add .
git commit -m "Skapa login-formulär #1"
git push origin feature/inloggning
# → PR feature/inloggning → develop med "Closes #1"

# 3. Feature #2
git checkout develop
git pull origin develop
git checkout -b feature/sokfalt
echo "<input type='search' />" > search.html
git add .
git commit -m "Skapa sökfält #2"
git push origin feature/sokfalt
# → PR med "Closes #2"

# 4. Release
# → PR develop → main
```

</details>

---

## Reflektion

- Varför stänga issues via PR istället för manuellt?
- Vad händer om någon glömmer "Closes #X"?
- Hur kan ni använda milestones för att planera sprintar?

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
