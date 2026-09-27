# Git Flow med releases och hotfixes

🟡

**Scenario:** Ni levererar en produkt. Feature branches för nytt, release branches för att paketera, och hotfixes för akuta buggar i produktion.

## Steg för steg

### 1. Setup

```bash
mkdir superapp
cd superapp
git init
echo "# SuperApp" > README.md
git add .
git commit -m "Initial commit"

# Skapa develop
git checkout -b develop
echo "console.log('SuperApp redo');" > app.js
git add .
git commit -m "Struktur"
```

### 2. Feature — bygg inloggning

```bash
git checkout develop
git checkout -b feature/login

echo "function login(user) { return true; }" >> app.js
git add .
git commit -m "Lägg till login"

echo "function logout() {}" >> app.js
git add .
git commit -m "Lägg till logout"

# Merge till develop
git checkout develop
git merge feature/login --no-ff -m "Merge feature/login"
git branch -d feature/login
```

### 3. Release 1.0.0

```bash
git checkout -b release/1.0.0

echo "v1.0.0" > VERSION
git add .
git commit -m "Bump version to 1.0.0"

# Till main
git checkout main
git merge release/1.0.0 --no-ff -m "Release 1.0.0"
git tag -a v1.0.0 -m "Första releasen"

# Tillbaka till develop
git checkout develop
git merge release/1.0.0 --no-ff -m "Merge release 1.0.0"
git branch -d release/1.0.0
```

### 4. Hotfix — säkerhetsfix i produktion

```bash
git checkout main
git checkout -b hotfix/security-patch

echo "// Sanitize input" >> app.js
git add .
git commit -m "Säkerhetsfix"

# Till main
git checkout main
git merge hotfix/security-patch --no-ff -m "Hotfix"
git tag -a v1.0.1 -m "Säkerhetsfix"

# Till develop (annars försvinner fixen vid nästa release!)
git checkout develop
git merge hotfix/security-patch --no-ff -m "Merge hotfix"
git branch -d hotfix/security-patch
```

### 5. Visa historiken

```bash
git log --oneline --graph --all --decorate
```

Väntat resultat: main med två tags, develop med allt ihop, inga lösa branches.

---

<details>
<summary>💡 Tips – Varför --no-ff?</summary>

`--no-ff` tvingar Git att skapa en merge commit även om det går att göra fast-forward. Det gör historiken tydligare — du ser exakt när en feature/release/hotfix slogs ihop.

Utan `--no-ff`:
```
* Lägg till login
* Struktur
```

Med `--no-ff`:
```
*   Merge feature/login
|\
| * Lägg till login
| * Struktur
```

</details>

---

<details>
<summary>✅ Förslagslösning – hela flödet</summary>

```bash
# Setup
mkdir superapp && cd superapp
git init
echo "# SuperApp" > README.md
git add .
git commit -m "Initial commit"

git checkout -b develop
echo "console.log('SuperApp redo');" > app.js
git add .
git commit -m "Struktur"

# Feature
git checkout -b feature/login
echo "function login(user) { return true; }" >> app.js
git add .
git commit -m "Login"
echo "function logout() {}" >> app.js
git add .
git commit -m "Logout"
git checkout develop
git merge feature/login --no-ff -m "Merge feature/login"
git branch -d feature/login

# Release
git checkout -b release/1.0.0
echo "v1.0.0" > VERSION
git add .
git commit -m "Bump version"
git checkout main
git merge release/1.0.0 --no-ff
git tag -a v1.0.0 -m "Första releasen"
git checkout develop
git merge release/1.0.0 --no-ff
git branch -d release/1.0.0

# Hotfix
git checkout main
git checkout -b hotfix/security-patch
echo "// Sanitize" >> app.js
git add .
git commit -m "Säkerhetsfix"
git checkout main
git merge hotfix/security-patch --no-ff
git tag -a v1.0.1 -m "Säkerhetsfix"
git checkout develop
git merge hotfix/security-patch --no-ff
git branch -d hotfix/security-patch

# Kolla
git log --oneline --graph --all --decorate
```

</details>

---

**Förväntad output från `git log --oneline --graph --all --decorate`:**

```
*   Merge hotfix into develop
|\
| * Hotfix (tag: v1.0.1)
|/
*   Merge release 1.0.0 into develop
|\
| * Release (tag: v1.0.0)
|/
*   Merge feature/login into develop
|\
| * Logout
| * Login
|/
* Struktur
* Initial commit
```

---

## Reflektion

- Varför merge:ar man hotfix till både main och develop?
- Vad händer om hotfixen inte mergas till develop — och nästa release görs?
- När skulle ni INTE använda Git Flow? (Små projekt? Solo?)

---

_Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig._
