# CLO26 — Backlog

Spårar vilket material som är klart, påbörjat eller saknas.
Uppdateras löpande. Facit och lösningar hör hemma i respektive kurs `_teacher/facit/`.

Varje modul behöver: README · lectures (Marp) · notes (lästext) · examples (.cs) · övning_01 · övning_02 · facit

---

## kurs-02 — Databaser: NuGet-paket att lära ut

Studerande måste känna igen och kunna installera dessa paket för databasarbete.

| Paket | Syfte | När |
|-------|-------|-----|
| `Microsoft.EntityFrameworkCore` | ORM — mappa klasser mot tabeller | Kärna |
| `Microsoft.EntityFrameworkCore.Sqlite` | SQLite-provider — perfekt för lärande (ingen server) | Intro |
| `Microsoft.EntityFrameworkCore.SqlServer` | SQL Server-provider — mer realistiskt | Fördjupning |
| `Microsoft.EntityFrameworkCore.Tools` | Migrations, scaffold — `dotnet ef` | Kärna |
| `MongoDB.Driver` | MongoDB — officiell C#-driver | Modul 05 MongoDB |
| `Dapper` | Lätt ORM — ren SQL med mappning | ⭐ Överkurs |

**Verktyg att installera:**
- **DB Browser for SQLite** (`sqlitebrowser`) — GUI för att bläddra i databasen visuellt
  - Studerande ser tabellerna, raderna och vad EF Core faktiskt gör i databasen
  - Gratis, cross-platform: https://sqlitebrowser.org

**Rekommenderad ordning:**
1. Börja med SQLite + EF Core + DB Browser (ingen server, direkt visuell feedback)
2. Introducera migrations tidigt — versionshantering av databasschemat
3. Byt till SQL Server när de förstår koncepten
4. MongoDB.Driver när de når NoSQL-modulen

**Överkurs:**
- Skriva en Facade som gör det enkelt att byta ut databas-providern (SQLite → SQL Server → MongoDB) utan att ändra affärslogiken
- Kopplar direkt till Facade-designmönstret i kurs-01 överkurs

---

## kurs-01 — Grundläggande OOP

| Modul | README | Lectures | Notes | Examples | Övning 1 | Övning 2 | Uppgift | Facit |
|-------|--------|----------|-------|----------|----------|----------|---------|-------|
| 01_verktyg_och_git     | ✅ | ✅ | ✅ | ⬜ | ✅ | ✅ | – | ⬜ |
| 02_syntax_och_variabler | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 03_villkor_och_loopar   | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 04_metoder              | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 05_klasser_och_oop      | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| 06_datastrukturer       | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| 07_git_och_markdown     | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 08_projekt              | ⬜ | ⬜ | ⬜ | ⬜ | – | – | ✅ | ⬜ |

**Utestående:** examples för modul 01, allt för modul 02–08

---

## kurs-02 — Databaser

| Modul | README | Lectures | Notes | Examples | Övning 1 | Övning 2 | Uppgift | Facit |
|-------|--------|----------|-------|----------|----------|----------|---------|-------|
| 01_intro_databaser      | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 02_sql_grunder          | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 03_sql_avancerat        | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 04_normalisering        | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| 05_mongodb              | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 06_sakerhet_och_backup  | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 07_uml_och_design       | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 08_projekt              | ⬜ | ⬜ | ⬜ | ⬜ | – | – | ⬜ | ⬜ |

**Kurs-README:** ⬜

---

## kurs-03 — Fördjupad OOP

| Modul | README | Lectures | Notes | Examples | Övning 1 | Övning 2 | Uppgift | Facit |
|-------|--------|----------|-------|----------|----------|----------|---------|-------|
| 01_databaskopplingar    | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 02_designmonster        | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 03_refactoring          | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 04_uml_och_planering    | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| 05_agila_metoder        | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 06_projekt              | ⬜ | ⬜ | ⬜ | ⬜ | – | – | ⬜ | ⬜ |

**Kurs-README:** ⬜

---

## kurs-04 — Test och kvalitet

| Modul | README | Lectures | Notes | Examples | Övning 1 | Övning 2 | Uppgift | Facit |
|-------|--------|----------|-------|----------|----------|----------|---------|-------|
| 01_clean_code           | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 02_unit_testning        | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 03_tdd                  | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 04_scrum_och_agilt      | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 05_user_stories_gherkin | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | – | ⬜ |
| 06_kodkvalitet          | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ | ⬜ |
| 07_projekt              | ⬜ | ⬜ | ⬜ | ⬜ | – | – | ⬜ | ⬜ |

**Kurs-README:** ⬜  
**Notering:** `exercises/diskutera_projekt_bonusen.md` ligger på rot-nivå — flytta eller ta bort.

---

## Infrastruktur / övrigt

- [ ] Exempelkod (`.cs`) för kurs-01 modul 01 saknas i `examples/`
- [ ] Kurs-README för kurs-02, kurs-03, kurs-04
- [ ] Flytta/radera `kurs-04/exercises/diskutera_projekt_bonusen.md`

---

## Övningar att bygga — Bubble sort med strumpor

### Intro (före kod och video)
Ge studerande en blandad lista — antingen utskrivna kort eller på skärmen:

```
["röd S", "blå XL", "grön M", "röd L", "blå S", "grön XL", "röd M"]
```

**Reglerna:** Du får bara jämföra *grannar*. Du får bara byta *grannar*. Sortera efter storlek (S→M→L→XL), sen färg inom samma storlek.

**Poängen:** De inser att det tar flera pass. De räknar antal byten. De frustreras lite — precis lagom.

**Kopplingen Marcus gör:** *"Det här är exakt vad jag gör när jag samlar ihop strumpor efter tvätten. Jag sorterar dem på storlek och färg, ett par i taget. Det har ett namn — bubble sort."*

**Sedan:** Visa AlgoRythmics Csángó-dansen. Nu ser de vad de precis gjorde.

**Sedan:** Skriv koden tillsammans.

### Progression i övningen
1. **Sortera siffror** — enklast, bara ett kriterium
2. **Sortera strängar** — jämföra `CompareTo`
3. **Sortera objekt** — `Sock`-klass med `Size` och `Color`, implementera `IComparable<Sock>`
4. **Överkurs:** Låt studerande skriva sin *egen* sorteringsalgoritm — behöver inte vara bubble sort

---

## Övningar att bygga — "Gissa ett tal"-serien

### Övning 1: Gissa ett tal (modul 03 — loopar + if)
Datorn tänker på ett tal 1–100. Du gissar. Datorn svarar: för högt / för lågt / rätt.
- Räknar antal försök
- Studerande spelar flera gånger med olika strategier
- **Reflektion:** vilken strategi var bäst? Varför?
- Facit: bästa möjliga strategi är alltid halvera → O(log n) utan att de vet det än

### Övning 2: Datorn gissar (modul 06 — innan binärsökning)
Du tänker på ett tal. Datorn gissar med binärsökning-strategi.
Datorn ska alltid hitta svaret på **max 7 gissningar** (för 1–100).
- Studerande svarar "högre" / "lägre" / "rätt"
- Visar hur binary search "halverar" problemet varje gång
- **Poängen:** de ser datorn göra det de *själva* insåg i övning 1

*Kopplar ihop modul 03 (loopar, if) → modul 06 (algoritmer) som en röd tråd.*

---

## Designbeslut — filformat-progression (modul 06)

Introducera datalagring naturligt i anslutning till `List<T>`:

| Steg | Format | Känsla | Syfte |
|------|--------|--------|-------|
| 1 | Textfil (rad för rad) | "krångligt och fult" | Visa problemet |
| 2 | CSV | "agghh" | Bättre men skört |
| 3 | JSON | "yay" | Rätt verktyg |
| 4 | XML | "are you kidding me" | Historik + varför JSON vann |

- [ ] Övning: spara en `List<Product>` som JSON med `System.Text.Json`
- [ ] Övning: läs tillbaka och deserialisera till listan
- [ ] Koppla till: `JsonSerializer.Serialize/Deserialize`, `File.WriteAllText/ReadAllText`
- [ ] Placering: modul 06 direkt efter `List<T>` — innan databaser tar över i kurs-02

### 🎬 JSON Statham-meme (obligatoriskt på JSON-lektionen)

> Gemini-prompt: "Jason Statham in a serious action pose, dark background, dark mode style.
> Top text: 'My name is Jason.' Bottom text: 'I always parse.'"

Alternativa texter:
- Top: "They said XML was fine." / Bottom: "I said nothing. I just parsed."
- Top: "Don't @ me." / Bottom: "I only accept valid JSON."
- Top: "Serialized. Deserialized." / Bottom: "No drama."
