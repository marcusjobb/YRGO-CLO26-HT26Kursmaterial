# Inventering v2 — Gammalt kursmaterial + böcker
# CLO26 Kurs-01 Grundläggande OOP i C#

Genomförd: 2026-06-15
Föregående version: `material_fran_old_courses.md` (2026-06-15, fokus vecka 1–5)
Tillägg i v2: böckerna, story-driven scenarios (komplett), tenta-analys, Java OOP-övningar, Marp-konverteringsstatus

---

## Böcker — kapitelöversikt och relevans

### `csharp_cmyh` — Campus Mölndals referensbok (nyaste, mest komplett)

**Sökväg:** `/home/marcus/git/Old_courses/books/csharp_cmyh/`
**Typ:** Jekyll-webbplats. Senast uppdaterad 2025-10-23.
**Bedömning:** Täcker alla kurs-01-ämnen utom pseudokod. Innehållet är korrekt men skrivet i ChatGPT-stil (emojis, "cooloare", "rocks", Thor's hammer-metaforer). Behöver röstbyte till Marcus-stil.

| Kapitelgrupp | Relevanta filer | Kurs-01-ämne | Röstbyte? |
|---|---|---|---|
| `begin_csharp/` | `variabler-och-typer.md`, `if-exempel.md`, `if_and_loops.md`, `loop-exempel.md`, `klasser-och-properties.md`, `metoder-och-syntax.md` | Variabler, if, loopar, metoder | Ja |
| `variables_and_types/` | `variables.md`, `types.md`, `numbers/` (int, float, decimal m.fl.), `text/` (string, char), `logic/` (bool), `structured/enum.md` | Variabler + datatyper + enum | Ja |
| `control_structures/` | `if/` (else, elseif, guard-clauses, ternaryif), `loop/` (for, while, dowhile, nestedloop, recursion), `switch/` | If/else, loopar, switch | Ja |
| `oop/` | `classes.md`, `constructors.md`, `properties.md`, `capsulation.md`, `inheritance.md` | Klasser, konstruktorer, properties, inkapsling | Ja |
| `datastructures/` | `list.md`, `dictionary.md`, `arrays.md`, `hashset.md` | List\<T\>, Dictionary | Ja |
| `databases/` | `uml_database_design.md` | UML-klassdiagram | Ja |

**Saknar:** pseudokod, SSH.

---

### `devbook` — Java/C# parallellbok (äldre)

**Sökväg:** `/home/marcus/git/Old_courses/books/devbook/`
**Typ:** Jekyll, dubbel struktur — `Docs/csharp/` och `Docs/java/`.
**Bedömning:** Partiell täckning. Saknar metoder, konstruktorer, properties, enum, UML. Inte primär källa.

| Täcker | Saknar |
|---|---|
| Variabler, if, loopar, klasser, inkapsling, arv, List, Dictionary, arrays | Metoder, konstruktorer, properties, enum, UML |

---

### `csharp_bgu` — BGU-boken (skelett/utkast)

**Sökväg:** `/home/marcus/git/Old_courses/books/csharp_bgu/`
**Bedömning:** Inte användbar. Innehåller bara `book/index.md`, `book/variabler/index.md` (ytlig lista) och `book/metoder/index.md` (tom). Hoppa över.

---

### `CLO22` — Campus Mölndal, äldre version

**Sökväg:** `/home/marcus/git/Old_courses/books/CLO22/`
**Bedömning:** Nästan identisk med `devbook` (C#-delen). Täcker variabler, if, loopar, klasser, inkapsling, List, Dictionary. Saknar metoder, konstruktorer, properties, enum, UML. Inte primär källa — `csharp_cmyh` är alltid nyare och rikare.

---

## Bokkapitel → Marp-lektioner

Mappning: vilket kapitel i `csharp_cmyh` kan bli vilken Marp-föreläsning, och i vilken vecka.

Förutsättning: röstbyte från ChatGPT-stil till Marcus-stil krävs för alla.

| Bokkapitel (sökväg i `csharp_cmyh/C-Sharp/`) | Marp-lektions titel | Vecka | Åtgärd |
|---|---|---|---|
| `variables_and_types/variables.md` + `types.md` | Variabler och datatyper i C# | V1 | Röstbyte + lägg till Stack/Heap-Mermaid |
| `variables_and_types/numbers/` (int, float, decimal) | Taltyper och precision | V1 | Röstbyte + komprimera till 1 slide-set |
| `variables_and_types/text/` (string, char) | Strängar och tecken | V1 | Röstbyte |
| `variables_and_types/logic/` (bool) | Boolesk logik | V1 | Röstbyte, kombinera med if |
| `control_structures/if/` (else, elseif, guard-clauses, ternaryif) | If-satser och beslutslogik | V1 | Röstbyte — `variables/livecode/variables_slides.md` är redan Marp-klar, komplettera med guard-clauses |
| `control_structures/loop/` (for, while, dowhile) | Loopar — for, while, do-while | V2 | Röstbyte, komprimera — `loop/for/for.md` finns redan |
| `control_structures/switch/` | Switch-satser | V2 | Röstbyte — `switch/lectures/switch_marp.md` är redan Marp-klar, boken ger referensinnehåll |
| `begin_csharp/metoder-och-syntax.md` | Metoder — struktur och syntax | V2 | Röstbyte — `methods/lecture.md` är redan Marp-klar, boken ger kompletterande innehåll |
| `oop/classes.md` + `constructors.md` | Klasser och konstruktorer | V3 | Röstbyte — `classes/lectures/classes_marp.md` är redan Marp-klar |
| `oop/properties.md` + `capsulation.md` | Properties och inkapsling | V3 | Röstbyte — `classes/lectures/classes_advanced_marp.md` täcker redan |
| `oop/accessmodificators/` (public, private, protected) | Accessmodifierare | V3 | Röstbyte, bygg ny Marp-slide |
| `variables_and_types/structured/enum.md` | Enum — varför och hur | V4 | Röstbyte + bygg Marp-slides (inga klara från 2025) |
| `datastructures/list.md` | List\<T\> — samlingar i C# | V4 | Röstbyte — `datastructures/lectures/lists.md` är redan Marp-klar |
| `datastructures/dictionary.md` | Dictionary — nyckel och värde | V4 | Röstbyte — `datastructures/lectures/dictionaries.md` är redan Marp-klar |
| `databases/uml_database_design.md` | UML-klassdiagram | V3 | Röstbyte + bygg Marp-slides (texten finns, slides saknas) |

**Slutsats:** `csharp_cmyh` är bäst som referensinput för att komplettera och berika befintliga Marp-filer. Den ersätter inte de redan Marp-klara filerna från 2025 — den tillför detaljer och exempel som saknas där.

---

## Story-driven scenarios — komplett lista

### Level 1 — Easy

Sökväg: `/home/marcus/git/Old_courses/2025/1_oop/story_driven_code/level_1_easy/`

| # | Fil | Tema | C#-koncept som övas | Svårighetsgrad | Lösning |
|---|---|---|---|---|---|
| 1 | `harry_potter_variables_adventure.md` | Hogwarts, Harry Potter | `string`, `int`, string interpolation, grundläggande variabler | Grundläggande | Ja |
| 2 | `star_wars_variables_adventure.md` | Star Wars, Darth Vader | `string`, `int`, string interpolation | Grundläggande | Stub (TODO) |
| 3 | `troll.md` | Fantasy — Grlub trollet slåss | `string`, `int`, aritmetik (`*`, `+=`, `-=`), string concatenation | Grundläggande |Ja |
| 4 | `anakin_crystal_adventure.md` | Star Wars — Anakins ljussabelkristall | `string`, `int`, `double`, blandade typer, `.ToLower()` | Grundläggande–Medel | Ja |
| 5 | `Kepler-442b.md` | Sci-fi — rymdforskaren Commander Zara Nova | `string`, `int`, `double`, resurshantering, division, precision | Medel | Ja |
| 6 | `sherlock-holmes-mystery.md` | Klassisk detektiv — Baker Street 221B | `for`-loop, accumulator, `if/else if/else` inuti loop, type casting, procent | Medel–Svår | Ja |
| 7 | `arsene-lupin-vault.md` | Gentlemannatjuven Lupin, bankvalv | Modulo (`%`), type casting `(int)`, digital root, stora decimaltal | Svår | Ja |
| 8 | `harry-potter-zonkos-basement.md` | Harry Potter — Trams-O-Matic 3000 | `switch`, `while(true)`, `break`, `continue`, `int.TryParse`, input validation | Svår | Saknas |

**Notering:** `star_wars_variables_adventure_solution.cs` är en stub med `// TODO` — lösningen behöver skrivas klart. `harry-potter-zonkos-basement.md` saknar lösningsfil helt.

---

### Level 2 — Medium

Sökväg: `/home/marcus/git/Old_courses/2025/1_oop/story_driven_code/exercises/level_2_medium/`

| # | Fil | Tema | C#-koncept | Svårighetsgrad | Lösning |
|---|---|---|---|---|---|
| 1 | `lord_of_rings_methods_adventure.md` | Frodo förbereder festmåltid i Shire | Metoder med parameter, lokala variabler, string interpolation | Grundläggande–Medel | Ja |

**Notering:** Bara ett scenario på nivå 2. Behövs 2–3 till för klasser och metoder (se "Saknas" i slutet av rapporten).

---

### Level 3 — Master Coder

Sökväg: `/home/marcus/git/Old_courses/2025/1_oop/story_driven_code/level_3_master_coder/`

Enbart `readme.md` — inga äventyrsfiler. Nivån är tom.

---

### Story-driven material från 2023 (Java, moderniseringskandidater)

Sökväg: `/home/marcus/git/Old_courses/2023/java/OOP/excersises/loops/`

| Fil | Tema | C#-koncept (efter konvertering) | Åtgärd |
|---|---|---|---|
| `Avengers.md` | Marvel — Avengers | for, for-each, while, do-while | Konvertera Java → C#, modernisera stil |
| `Deadpool.md` | Marvel — Deadpool | Loopar | Konvertera + modernisera |
| `Jurassic_Park.md` | Film — Jurassic Park | Loopar | Konvertera + modernisera |
| `Star_Trek.md` | Sci-fi | Loopar | Konvertera + modernisera |
| `Fredag_13e.md` | Skräck | Loopar | Konvertera + modernisera |

---

## Tenta-analys

**Sökväg:** `/home/marcus/git/Old_courses/2025/1_oop/exam/`

**Viktigt:** Det finns ingen faktisk tenta-fil i katalogen — allt är träningsfrågor inför tentan. `STUDIEGUIDE_MED_LÄNKAR.md` avslöjar att den riktiga tentan har 30 blandade frågor.

### Träningsfiler — oversikt

| Fil | Antal frågor | Ämnesområde | Format | G/VG-mix |
|---|---|---|---|---|
| `variables_and_types_training.md` | 20 | int/string/double/bool, Parse/TryParse, var, const, null-coalescing | Multiple choice, `<details>`-svar | Blandat |
| `if_statements_training.md` | 20 | if/else if/else, `&&`/`||`/`!`, scope utan `{}`, ternary, operatorprecedens | Multiple choice, `<details>`-svar | Blandat |
| `loops_training.md` | 20 | for, while, do-while, foreach (read-only!), break/continue, LINQ Sum() | Multiple choice, `<details>`-svar | Blandat |
| `switch_statements_training.md` | 20 | Klassisk switch + C# 8 switch expressions, pattern matching, when guard | Multiple choice, `<details>`-svar | Frågorna 7, 12, 14, 17 är VG-nivå |
| `naming_conventions_training.md` | 20 | camelCase/PascalCase/snake_case, C#-konventioner, boolean Is/Has/Can | Multiple choice, `<details>`-svar | Blandat |
| `git_basics_training.md` | 20 | git add/commit/push/pull, .gitignore, konflikter, commit-meddelanden | Multiple choice, `<details>`-svar | Blandat |
| `data_structures_training.md` | 19 | Array, List, Dictionary, Stack, Queue, HashSet, SortedSet, 2D-array, LINQ | Multiple choice, `<details>`-svar | Mest avancerade filen |

**Totalt:** ~139 träningsfrågor med expanderbara förklaringar. Används direkt till kurs-01 vecka 6 (repetition + tentaprep).

**Notering om stilformat:** Träningsfrågorna har humoristiska felalternativ ("coffee", "bigfoot") och avslappnad ton — stämmer bra med Marcus-stil.

---

## Variables/Lectures — Marp-konverteringsstatus

Sökväg: `/home/marcus/git/Old_courses/2025/1_oop/variables/`

| Fil | Marp-klar? | Vad behöver göras | Noteringar |
|---|---|---|---|
| `livecode/variables_slides.md` | Ja — har `marp: true`, `paginate: true`, `---`-separatorer | Rensa duplicerat `<style>`-block (copy-paste-bugg, upprepas ~6 ggr) | 8 slides: variabler som lådor, Stack/Heap-diagram (Mermaid), värde- vs referenstyper |
| `livecode/variables.md` | Nej — ren lästext | Strukturera om till slides | Tema: djur/hund, Mermaid-diagram finns |
| `livecode/parsing.md` | Nej — ren lästext | Strukturera om till slides | Tema: födelseår/åldersberäkning, `int.TryParse`, gammalt klassformat |
| `exercises/variables_exercise.md` | Nej — övningsfil | Behåll som övning, konvertera ej | — |
| `exercises/bool-basic.md`, `bool-logical.md` m.fl. | Nej — övningsfiler | Behåll som övningar | — |

**CSS-bugg:** Samma felaktiga copy-paste av `<style>`-blocket finns i `variables_slides.md` och `if/lecture/if_statements.md`. Ta inte med buggen in i nytt material.

---

## If-övningar — komplett lista

Sökväg: `/home/marcus/git/Old_courses/2025/1_oop/if/`

### Föreläsningar

| Fil | Marp-klar? | Innehåll | Tema |
|---|---|---|---|
| `lecture/if_statements.md` | Ja — Marp-frontmatter + `---` | If som vägkorsning (Mermaid), jämförelseoperatorer (tabell), logiska operatorer, nästlade if-problem, tidiga returner. 8 slides. | Generellt / vardagsbeslut |
| `lecture/if_and_variables.md` | Nej — lästext + livekod | Ålder/körkort/moppe, C64-stil, gammalt klassformat, Mermaid-diagram | Ålderskontroll |

### Övningar

| Fil | Tema | Svårighetsgrad | Struktur |
|---|---|---|---|
| `exercises/if-exempel.md` | Vardagsbeslut — fem kompletta kodexempel | Grundläggande | Inga facit — bara exempelkod |
| `exercises/decision-chaos.md` | Morgonrutin, gaming, ekonomi, social, mat, karriär | Grundläggande → Utmaning (3 nivåer) | 30+ scenarios med kodskelett |
| `exercises/life-decisions.md` | Kaffe, middag, Star Wars, investeringar, RPG-karaktär | Grundläggande → Komplex | 5 övningar med exempelkod + utökningsuppgifter |

---

## Java OOP-övningar — komplett lista

Sökväg: `/home/marcus/git/Old_courses/2023/java/OOP/excersises/`

Alla övningar följer samma struktur: beskrivning → pseudokod → tips i `<details>` (planering, kodsamarbete, klassförslag, namngivning, ingen kod) → lösning.
För C#-konvertering: ersätt `Scanner` med `Console.ReadLine`, `ArrayList` med `List<T>`, `@Override` med `override`.

### classes/easy/

| Fil | Tema | OOP-koncept | Svårighetsgrad |
|---|---|---|---|
| `movies.md` | Film-lista | Klass, getter, ArrayList, for-each | Grundläggande |
| `music.md` | Musiksmak | Klass, getter, ArrayList, for-each | Grundläggande |
| `geometric.md` | Geometri (cirkel, kvadrat) | Två klasser, beräkningsmetoder | Grundläggande |
| `pets.md` | Husdjur (katt/hund) | Två klasser, ArrayList, full CRUD | Grundläggande–Medel |

### classes/medium/

| Fil | Tema | OOP-koncept | Svårighetsgrad |
|---|---|---|---|
| `books.md` | Bibliotek | Två relaterade klasser (Bok + Författare), sökning | Medel |
| `students.md` | Studenthantering | Klass + manager-klass, sökning på betyg | Medel |
| `measure.md` | Enhetsomvandling (km/m/cm/dm) | 4 klasser, metodkedja | Medel |
| `names.md` | Namnhantering | Klass + sökning | Medel |

### inheritance/easy/

| Fil | Tema | OOP-koncept | Svårighetsgrad |
|---|---|---|---|
| `easy1.md` | Rymdfarkoster | Basklass Spacecraft, subklasser Spaceship + Satellite, `@Override displayInfo()` | Grundläggande |
| `easy2.md` | Rymdfärd | Variabler, if-sats, Scanner-inmatning | Grundläggande |
| `easy3.md` | Fordon (bil/cykel/båt) | Basklass Vehicle, 3 subklasser, polymorfism | Grundläggande |

### loops/ (6 st, popkulturteman)

| Fil | Tema | Anmärkning |
|---|---|---|
| `Avengers.md` | Marvel — täcker for, for-each, while, do-while | Stark pedagogisk struktur |
| `Deadpool.md` | Marvel — Deadpool | — |
| `Jurassic_Park.md` | Film — Jurassic Park | — |
| `Star_Trek.md` | Sci-fi | — |
| `Fredag_13e.md` | Skräck | — |
| (extra) | — | En sjätte fil kan finnas — katalogen listades med 6 |

### variables/ (8 st)

| Fil | Tema |
|---|---|
| `exercises1.md` | Star Wars — Darth Vader, Luke, R2-D2, Leia m.fl. |
| `darkmard.md` | Skräck |
| `Kodad_skrack.md` | Skräck |
| `Kodad_skrack_v2.md` | Skräck |
| `Koden_fran_rymden.md` | Sci-fi |
| `Spoprogrammeraren.md` | — |
| `Terminatorns_kod.md` | Sci-fi — Terminator |
| (extra) | — |

### examprep/ (6 st)

Repetitionsövningar: getters/setters, string-hantering (vokaler), variabler/datatyper, geometri (trekant/kvadrat). Användbara som tentaprep-underlag om de konverteras till C#.

---

## Hela `2025/1_oop/`-katalogen — strukturöversikt

Referens för vad som finns. Detaljinventering per modul gjordes i `material_fran_old_courses.md`.

```
1_oop/
├── arrays/            lecture/ + exercises/
├── assignment/        dungeon_crawler, sea_stuff, storage, storage_csv
├── begin_csharp/      livecode/ + 21 separata övningsfiler (BeraknaMoms, BMICalculator, BoolTester m.fl.)
├── classes/           lectures/ (3 Marp-klara) + exercises/ (12 progressiva)
├── clean_code/        lectures/ + exercises/
├── datastructures/    lectures/ (lists, dictionaries, advanced) + exercises/ + micro-exercises/
├── enums/             articles/enums-comprehensive.md (artikel, inga övningar)
├── exam/              7 träningsfiler + STUDIEGUIDE
├── git/               exercises/ + lectures/ + workflow/
├── if/                (se If-övningar ovan)
├── loop/              exercises/ + for/ + livecode/
├── markdown/          cheatsheet
├── methods/           lecture.md + exercises/ (4 övningar)
├── problem_solving/   exercises/ + self_test/
├── story_driven_code/ (se Story-driven scenarios ovan)
├── switch/            lectures/switch_marp.md (Marp-klar) + exercises/ (3 st)
└── variables/         (se Variables/Lectures ovan)
```

---

## `2023/csharp/Assignments/` — kompletterande fynd

### Cheatsheets (19 korta referensdokument)
Sökväg: `/home/marcus/git/Old_courses/2023/csharp/Assignments/Cheatsheets/`
arrays, bool, datatyper, dictionary, for, git, if, list, string, variabler, while m.fl.
Direkt användbara som referensmaterial till studerande om de moderniseras.

### ELI5-filer (5 st)
Sökväg: `/home/marcus/git/Old_courses/2023/csharp/Assignments/ELI5/`
klasser, CRUD, dice, properties — pedagogiska "explain like I'm 5"-filer.

### C#-övningar (speglar Java-övningarna)
Sökväg: `/home/marcus/git/Old_courses/2023/csharp/Assignments/OOP/excersises/`
movies, music, geometric, students, books, measure, pets — nästan identisk struktur som Java-versionen, men i C#.

---

## Marp-klara filer — sammanfattningslista

Dessa filer kan plockas direkt (efter eventuell CSS-bugg-rensning):

| Fil | Ämne | CSS-bugg? |
|---|---|---|
| `2025/1_oop/variables/livecode/variables_slides.md` | Variabler | Ja — rensa |
| `2025/1_oop/if/lecture/if_statements.md` | If-satser | Ja — rensa |
| `2025/1_oop/classes/lectures/classes_marp.md` | Klasser | Nej |
| `2025/1_oop/classes/lectures/classes_advanced_marp.md` | Klasser avancerat | Nej |
| `2025/1_oop/classes/lectures/class.lecture.marp.md` | Klasser (alt. version) | Okänt |
| `2025/1_oop/switch/lectures/switch_marp.md` | Switch | Nej |

---

## Saknas — behöver skapas från grunden

| Ämne | Vad som saknas | Prioritet |
|---|---|---|
| **SSH** | Guide: generera nyckel, lägg till GitHub, ssh-agent — finns ingenstans | Hög (vecka 1) |
| **Story-driven nivå 2** | Bara 1 fil (Lord of the Rings/metoder). Behövs 2–3 scenarier för metoder + klasser | Hög (vecka 2–3) |
| **Enum-övningar** | Bara en artikel (`enums-comprehensive.md`), inga kodövningar | Medel (vecka 4) |
| **UML-slides (Marp)** | Textinnehåll finns (`2024/csharp/1_oop/lectures/09_planning/2_classdiagram.md`) men inga Marp-slides för CLO26-stil | Medel (vecka 3) |
| **Pseudokod-föreläsning** | `problem_solving/programmer_mindset.md` finns men ingen strukturerad Marp-presentation om pseudokod specifikt | Låg (kan integreras i annan föreläsning) |
