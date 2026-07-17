# Vecka 6 — Git fördjupning och Markdown

## Dagplan

### Dag 1 — Git-teori + grundövningar

**Genomgång:**
- `lectures/git_teori_presentation.md` — repo, commit, push, pull, tre-stegsmodellen
- `lectures/workflow.md` — add/commit/push-ritualen, branching, merge

**Övning:**
- `exercises/git_exercises.md` — grundkommandon (init, add, commit, log)
- `exercises/git_initiera_repository.md` — konfigurera Git lokalt, första repo

**Snabbtips:** Kör `git log --oneline --graph` live — se historiken växa efter varje commit. Det fastnar visuellt.

---

### Dag 2 — Git Flow i grupp

**Genomgång:**
- `lectures/github_registration.md` — GitHub-konto + Student Pack (om gruppen inte har det klart)
- Git Flow-konceptet: feature → develop → main, PR-flöde

**Övning (välj nivå):**
- `exercises/gitflow1.md` — Git Flow-flöde med feature branches och PR (🟡)
- `exercises/gitflow2.md` — Git Flow + Issues, stäng automatiskt via `Closes #X` (🟡)
- `exercises/git-flow-team-csharp.md` — bygg C#-klasser tillsammans via Git Flow (🟡)

**Utmaning:**
- `exercises/git_flow_exercise_1.md` — releases och hotfixes, tagging (🔴)

---

### Dag 3 — Handledning + code reviews

**Genomgång (kort):**
- `lectures/git_aterblick.md` — kommandoreferens, stash, morgon-/push-ritual

**Övning:**
- `exercises/gitflow3.md` — code reviews via GitHub: kommentera, approve, request changes (🔴)

**Handledning:** Lös blockeringar från dag 1–2. Extra fokus på konflikter — skapa en merge conflict med flit och lös den.

---

## Publish-manifest

```
publish:
  - 07_git_och_markdown/README.md
  - 07_git_och_markdown/lectures/git_teori_presentation.md
  - 07_git_och_markdown/lectures/workflow.md
  - 07_git_och_markdown/lectures/git_introduktion.md
  - 07_git_och_markdown/lectures/github_registration.md
  - 07_git_och_markdown/lectures/git_aterblick.md
  - 07_git_och_markdown/exercises/git_exercises.md
  - 07_git_och_markdown/exercises/git_initiera_repository.md
  - 07_git_och_markdown/exercises/gitflow1.md
  - 07_git_och_markdown/exercises/gitflow2.md
  - 07_git_och_markdown/exercises/git-flow-team-csharp.md
  - 07_git_och_markdown/exercises/git_flow_exercise_1.md
  - 07_git_och_markdown/exercises/gitflow3.md
```

## Notering

Modulen saknar separata lästexer (notes/). Föreläsningsmaterialet täcker genomgångarna. Om mer text behövs — `git_introduktion.md` och `git_aterblick.md` fungerar som läsning efter lektionen trots att de ligger i lectures/.

Git-kunskaperna i den här modulen examineras löpande genom alla inlämningar och i Skogsäventyret (modul 8) — inte med en separat inlämningsuppgift.
