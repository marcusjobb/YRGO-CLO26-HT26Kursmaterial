# Kurskarta — OOP i C# Grund (kurs-01)

**Kalender:** V36–V40 | **31 aug – 4 okt 2026**  
**Schema:** Mån–tis campus, ons online, fre eget arbete

---

## Kursplan (officiell)

**YH-poäng:** 25 | **Längd:** 5 veckor  
**Examination:** Skriftligt prov + inlämningsuppgifter i programmering

### Lärandemål

| Mål | G | VG |
|-----|---|----|
| Redogöra för syntax, flöde och logik inom OOP i C# | uppnått | — |
| Redogöra för arbetsmetodik och versionshantering med Git | uppnått | — |
| Redogöra för namngivning och kodstruktur enligt clean code | uppnått | — |
| Beskriva funktionen i kod | uppnått | — |
| Redogöra för markdown-taggar och dess användning | uppnått | — |
| Använda Git korrekt i utvecklingsprojekt | uppnått | — |
| Använda datastrukturer i applikationsutveckling | uppnått | använder relevanta strukturer och motiverar val |
| Skriva korta och relevanta kodkommentarer | uppnått | — |
| Utveckla fristående OOP-applikationer med dokumentation | uppnått | välstrukturerad, clean code, reflektera över kodflöde |

---

## Veckorna på en rad

| Vecka | Datum | Tema | Inlämning |
|-------|-------|------|-----------|
| 1 | v36, 31 aug–5 sep | Git, verktyg, miljö | Nej |
| 2 | v37, 7–12 sep | C# repetition + tänkverktyg | Nej |
| 3 | v38, 14–19 sep | Klasser och objekt | **Inlämning 1** sön 20 sep |
| 4 | v39, 21–27 sep | Samlingar, datastrukturer, enums | **Skogsäventyret** sön 27 sep |
| 5 | v40, 28 sep–4 okt | Tenta + ikapp-kod | **Tenta** tis 29 sep |

---

## Vecka 1 — Git, verktyg, miljö

**Mål:** Alla har fungerande miljö och kan grundläggande git-flöde.

| Dag | Innehåll |
|-----|----------|
| Mån (introdag) | UL presenterar skolan — Marcus är med men kör inget eget |
| Tis | Kursintro, installation (VS Code, .NET, Git Bash, SSH) |
| Ons | SSH + GitHub, klona + pusha, Metropolitan Club-pussel |
| Tor | Online handledning |

**Publicera:**
- `01_verktyg_och_git/lectures/kursintro.md`
- `01_verktyg_och_git/lectures/installationsguide.md`
- `01_verktyg_och_git/lectures/git_grunder.md`
- `01_verktyg_och_git/exercises/ovning_01_git_setup.md`
- `01_verktyg_och_git/exercises/ovning_02_forsta_repot.md`

**Inlämning:** Ingen.

---

## Vecka 2 — C# repetition + tänkverktyg

**Mål:** Varm i kläderna inför OOP. Pseudokod och rubber duck introducerade.

| Dag | Innehåll |
|-----|----------|
| Mån fm | Variabler, datatyper, typfel — live-kod (gissa vad som händer) |
| Mån em | Villkor, loopar, metoder — live-kod + övningar |
| Tis fm | **Pseudokod-övningen** — "instruera roboten" (ingen dator, penna + papper) |
| Tis em | **Rubber duck-intro** + klasser intro — gummiankor delas ut 🦆 |
| Ons | Story-driven code: Grlub Trollet → Sherlock Holmes → Harry Potter |

**Story-driven code (vecka 2):**  
Flytta från `Old_courses/2025/1_oop/story_driven_code/` till `02_syntax_och_variabler/exercises/`.  
Level 1 (variabler) · Level 2 (loopar + metoder) · Level 3 (utmaning)

**Veckans nördiska:**  
`variabel` · `datatyp` · `deklaration` · `tilldelning` · `metod` · `returvärde` · `pseudokod`

**Inlämning:** Ingen.

---

## Vecka 3 — Klasser och objekt

**Mål:** Kan skriva en klass med properties, konstruktor och metoder. Förstår private/public.

| Dag | Innehåll |
|-----|----------|
| Mån fm | UML-klassdiagram — rita INNAN vi kodar (penna + whiteboard) |
| Mån em | Klasser i C# — konstruktor, properties, private/public — föreläsning: `BankAccount` |
| Tis fm | Inkapsling och varför fält är privata — diskussion + kodsnuttar |
| Tis em | Övning 1 (Car) + Övning 2 (Spaceship) + Inlämning 1 presenteras |
| Ons | Online handledning |

**Övningsteman:**  
Föreläsning: `BankAccount` (tydlig, neutral)  
Övning 1: `Car` — samma struktur, nytt tema (trygghet)  
Övning 2: `Spaceship` — identisk struktur, nytt tema (omedveten repetition)

**Veckans nördiska:**  
`klass` · `objekt` · `instans` · `konstruktor` · `property` · `private` · `public` · `encapsulation`

### Inlämning 1 — Spellistan (individuell)

**Tema:** Musikartister och låtar — Taylor Swift är exempel i föreläsningen, studerande väljer sin favoritartist

**Krav G:**
- [ ] `MusicArtist` med privata fält: name, genre, debutYear, country, isActive
- [ ] Properties med private set
- [ ] Konstruktor
- [ ] `Describe()` och `Perform()` metoder
- [ ] Minst 3 artister i Main()
- [ ] Reflektion: varför private? klass vs objekt?

**Krav VG:**
- [ ] `Song`-klass med: title, album, releaseYear, durationSeconds
- [ ] Property `DurationFormatted` → "3:45"-format
- [ ] `MusicArtist` har `List<Song>`, `AddSong()`, `ShowDiscography()`
- [ ] Minst 3 låtar skapas och läggs till

Se `05_klasser_och_oop/assignment/spellistan.md`

**Deadline:** Söndag 20 sep 23:59

---

## Vecka 4 — Samlingar, datastrukturer, enums

**Mål:** Kan använda List och enum. Förstår när man väljer vilken samling. Skogsäventyret igång.

| Dag | Innehåll |
|-----|----------|
| Mån fm | `List<T>` — lägga till, ta bort, loopa, Count |
| Mån em | `enum` — när och varför, hur det ser ut i kod |
| Tis fm | `Dictionary<K,V>` — kort intro, när man väljer vad |
| Tis em | **Skogsäventyret presenteras** — grupptilldelning, design-diskussion, UML på whiteboard |
| Ons | Online handledning |

**Notering:** Ingen individuell inlämning vecka 4. Skogsäventyret ÄR inlämningen.  
Grupper sätts ihop tisdag — 2–3 studerande per grupp.

**Veckans nördiska:**  
`List` · `Dictionary` · `enum` · `generics` · `collection` · `arv` · `subklass` · `basklass`

### Slutprojekt — Skogsäventyret (grupp)

Se `08_projekt/assignment/skogsaventyret.md`

**Deadline:** Söndag 27 sep 23:59

---

## Vecka 5 — Tenta + fri tid

**Mål:** Tentan visar förståelse. Fri tid = koda ikapp, stärka projektet.

| Dag | Innehåll |
|-----|----------|
| Mån | Repetition + tentaförberedelse + handledning |
| Tis | **TENTA** 09:00–11:00 |
| Ons | Online: Q&A om tentan, genomgång av vanliga fel |

**Tenta:** Se `_teacher/grading/tenta_kurs01.md`  
**Betygsgräns:** G = 58p · VG = 82p (av 97p)

---

## Kursbetyg

| Krav | Betyg |
|------|-------|
| Inlämning 1 G + Skogsäventyret G + Tenta G | **G** |
| Ovan + VG på minst 2 av 3 moment | **VG** |

---

## Inlämningsöversikt

| # | Namn | Typ | Deadline | Kursmoment |
|---|------|-----|----------|-----------|
| 1 | Spellistan | Individuell | Sön 20 sep | Klasser, properties, konstruktorer |
| 2 | Skogsäventyret | Grupp 2–3 | Sön 27 sep | Arv, List, enum, game loop |
| — | Tenta | Individuell | Tis 29 sep | Alla moment |

---

## Att göra

- [ ] Flytta story_driven_code/ → `02_syntax_och_variabler/exercises/`
- [ ] Skriv `05_klasser_och_oop/examples/bank_account.md` (föreläsningsexempel)
- [ ] Skriv `05_klasser_och_oop/exercises/ovning_car.md`
- [ ] Skriv `05_klasser_och_oop/exercises/ovning_spaceship.md`
- [x] Skriv `05_klasser_och_oop/assignment/spellistan.md`
- [x] Skriv `_teacher/grading/spellistan_bedomning.md`
- [ ] Skriv lektionsslides (Marp) för kap 05 Klasser
- [ ] Skriv lektionsslides (Marp) för kap 06 Datastrukturer
