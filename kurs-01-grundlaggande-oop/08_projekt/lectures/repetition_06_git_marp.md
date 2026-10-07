---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Git

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: de tre områdena · stages · vanligaste kommandon

---

## Fråga 1/7 — De tre områdena

Vilka tre områden finns lokalt i Git?

Vilket kommando flyttar en ändring **mellan** dem?

Var ligger GitHub i bilden?

---

## ✅ Svar 1/7

```text
Working Directory --git add--> Staging Area --git commit--> Local Repo
                                                                |
                                                            git push
                                                                v
                                                     GitHub (remote)
```

- **Working Directory** — dina filer, där du kodar
- **Staging Area** — förberedelse inför nästa commit
- **Local Repo** — den sparade historiken
- **GitHub** är `remote` — nås med `push` / `pull`

---

## Fråga 2/7 — Filens tillstånd

En fil kan vara i fyra tillstånd. Vilka?

Vad skriver `git status` för dem?

---

## ✅ Svar 2/7

| Tillstånd | Betyder | `git status` visar |
|-----------|---------|--------------------|
| **Untracked** | ny fil Git inte känner till | `Untracked files` |
| **Modified** | ändrad, inte stagad | `Changes not staged for commit` |
| **Staged** | tillagd med `git add` | `Changes to be committed` |
| **Committed** | sparad i historiken | (inget — ren arbetskatalog) |

---

## Fråga 3/7 — Ritualen

Sätt kommandona i rätt ordning:

```bash
git push
git commit -m "Lägg till validering"
git add .
git status
```

---

## ✅ Svar 3/7

```bash
git status                      # 1. vad har jag ändrat?
git add .                       # 2. förbered
git commit -m "Lägg till validering"   # 3. spara lokalt
git push                        # 4. skicka till GitHub
```

**Ritualen:** status → add → commit → push

---

## Fråga 4/7 — commit vs push

Du har kört `git commit`, men din kompis ser ingenting på GitHub.

Varför? Och vilket kommando hämtar **hens** ändringar till dig?

---

## ✅ Svar 4/7

- `git commit` sparar **bara lokalt**
- Först `git push` skickar historiken till GitHub
- `git pull` hämtar andras ändringar från GitHub

> 💬 _"Commit är att spara. Push är att dela."_

---

## Fråga 5/7 — clone, init, pull

När använder du vad?

- `git clone <url>`
- `git init`
- `git pull`

---

## ✅ Svar 5/7

- `git clone <url>` — **kopierar ett befintligt repo** från GitHub till din dator
- `git init` — **startar ett nytt repo** i nuvarande mapp
- `git pull` — **hämtar och slår ihop** nya ändringar från remote

---

## Fråga 6/7 — Commit-meddelande och .gitignore

Vilket commit-meddelande är bäst?

- A) `fixade grejer`
- B) `asdf`
- C) `Lägg till validering av ålder i Person`

Och vad ska in i `.gitignore`?

---

## ✅ Svar 6/7

**C** — beskriver *vad* som ändrats, i presens/imperativ, så historiken går att läsa.

`.gitignore` = filer Git ska **strunta i**:

- byggmappar: `bin/`, `obj/`
- IDE-filer: `.vs/`
- **hemligheter**: lösenord, nycklar, `.env`

---

## Fråga 7/7 — Inspektera och ångra

Vilket kommando gör vad?

```bash
git diff
git log --oneline
git restore --staged fil.cs
git restore fil.cs
```

---

## ✅ Svar 7/7

- `git diff` — visar **exakt vad** du ändrat (ej stagat)
- `git log --oneline` — kort lista över commits
- `git restore --staged fil.cs` — **unstage**: tar bort från staging, ändringen finns kvar
- `git restore fil.cs` — **kastar ändringarna** i filen ⚠️ går inte att ångra

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `git_grunder_marp.md`.
