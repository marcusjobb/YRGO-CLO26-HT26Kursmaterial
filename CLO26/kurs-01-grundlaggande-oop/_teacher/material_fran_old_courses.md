# Inventering av gammalt kursmaterial — CLO26 Kurs-01

Genomförd: 2026-06-15
Källa: `/home/marcus/git/Old_courses/`
Syfte: Identifiera återanvändbart material för kurs-01-grundlaggande-oop (5 veckor, C#, CLO26)

---

## Använda sökvägar

- `2025/1_oop/` — primär källa, aktiv kurs 2025
- `2024/csharp/1_oop/` — aktiv kurs 2024, äldre upplägg
- `2023/csharp/Assignments/` — originalversion, delvis Java, delvis äldre C#
- `2022/` — äldst, sparsamt C# + Git
- `books/csharp_cmyh/C-Sharp/` — levande referensbok (Campus Mölndal)
- `Material från Codic/` — externa övningar/uppgifter i PDF/DOCX

---

## Stilbedömning

| Stil | Beskrivning |
|------|-------------|
| **Modern** | Marp-slides med mörkt tema, Mermaid-diagram, svenska kommentarer, CLO26-känsla |
| **Gammal** | Löptext utan slides, ibland Java-kod, äldre namngivning, inga diagram |
| **ChatGPT-stil** | Emojis i rubriker, "super cool", "rocks", "TL;DR", engelska buzzwords inblandade |

Notera: `books/csharp_cmyh/` är aktivt uppdaterad men skriven i ChatGPT-stil (emojis, "cooloare", "rocks") — behöver röstbyte till Marcus-stil innan den kan användas direkt.

---

## VECKA 1 — Git / GitHub / SSH

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/git/lectures/git-i-projekt.md` | Föreläsning (Marp) | — | Modern | Kan användas direkt |
| `2025/1_oop/git/exercises/git-exercises.md` | Övningar (3 steg, med scaffolding + förväntad output) | bash | Modern | Kan användas direkt |
| `2025/1_oop/git/workflow/workflow.md` | Arbetsflödesguide | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/git_basics_training.md` | Träningsfrågor (flerval, `<details>`-svar) | — | Modern | Kan användas direkt |
| `2023/csharp/Assignments/Git/stepbysteplocal.md` | Steg-för-steg guide (lokalt repo + push) | bash | Gammal men tydlig | Moderniseras (lättare) |
| `2023/csharp/Assignments/Git/stepbystepcloud.md` | Guide för GitHub-push | bash | Gammal | Moderniseras |
| `2023/csharp/Assignments/Git/about.md` | Konceptartikel Git | — | Gammal | Skriv om |
| `2023/csharp/Assignments/Git/glossary.md` | Ordlista Git | — | Gammal | Kan plockas + moderniseras |
| `2023/csharp/Assignments/Git/git flow/lecture.md` | Föreläsning Git flow | — | Gammal | Skriv om |
| `2022/Git/` (Checkout.md, Clone.md, Push.md m.fl.) | Individuella kommandofiler | bash | Gammal | Inte värt att modernisera — skriv nytt |
| `books/csharp_cmyh/campus/tools/installation/Gitinstallation.md` | Installationsguide Git | — | ChatGPT-stil | Röstbyte |

**Notera:** SSH-specifikt material saknas i alla källor. Behöver skapas från grunden.

---

## VECKA 2 — Variabler, datatyper, if/else, loopar, metoder, pseudokod

### Variabler & datatyper

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/variables/livecode/variables.md` | Livekod-exempel (värdekopiering, scope) | C# | Modern, med Mermaid-diagram | Kan användas direkt |
| `2025/1_oop/variables/livecode/parsing.md` | Livekod-exempel (parsing) | C# | Modern | Kan användas direkt |
| `2025/1_oop/variables/livecode/variables_slides.md` | Marp-slides variabler | C# | Modern | Kan användas direkt |
| `2025/1_oop/variables/exercises/` (10+ övningar) | Övningar variabler (hello-world, bool, speed-challenges m.fl.) | C# | Modern | Kan användas direkt |
| `2025/1_oop/begin_csharp/if-exempel.md` | Livekod if | C# | Modern | Kan användas direkt |
| `2025/1_oop/begin_csharp/print-exempel.md` | Livekod Console.WriteLine | C# | Modern | Kan användas direkt |
| `2025/1_oop/begin_csharp/loop-exempel.md` | Livekod loop | C# | Modern | Kan användas direkt |
| `books/csharp_cmyh/C-Sharp/variables_and_types/variables.md` | Referensartikel variabler | C# | ChatGPT-stil (Thor's hammer-metafor) | Röstbyte |
| `books/csharp_cmyh/C-Sharp/variables_and_types/types.md` | Referensartikel typer | C# | ChatGPT-stil | Röstbyte |
| `books/csharp_cmyh/C-Sharp/variables_and_types/parse.md` | Referens parsing | C# | ChatGPT-stil | Röstbyte |
| `2023/csharp/Assignments/OOP/excersises/variables/exercises1.md` | Övningar variabler | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/Cheatsheets/variabler.md` | Cheatsheet | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/Cheatsheets/datatyper.md` | Cheatsheet datatyper | C# | Gammal | Moderniseras |

### If/else

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/if/lecture/if_statements.md` | Föreläsning (Marp) | C# | Modern | Kan användas direkt |
| `2025/1_oop/if/lecture/if_and_variables.md` | Föreläsning kombinerat | C# | Modern | Kan användas direkt |
| `2025/1_oop/if/exercises/if-exempel.md` | Övning if-exempel | C# | Modern | Kan användas direkt |
| `2025/1_oop/if/exercises/decision-chaos.md` | Övning beslutslogik | C# | Modern | Kan användas direkt |
| `2025/1_oop/if/exercises/life-decisions.md` | Story-driven if-övning | C# | Modern | Kan användas direkt |
| `2025/1_oop/switch/lectures/switch_marp.md` | Marp-slides switch | C# | Modern | Kan användas direkt |
| `2025/1_oop/switch/exercises/` (3 övningar) | Switch-övningar | C# | Modern | Kan användas direkt |
| `2023/csharp/Assignments/OOP/excersises/Struktur/if.md` | Strukturövning if | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/OOP/information/operators/` | Konceptartiklar operatorer | C# | Gammal | Plocka logiken, modernisera |

### Loopar

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/loop/for/for.md` | Föreläsning for-loop | C# | Modern | Kan användas direkt |
| `2025/1_oop/loop/livecode/loop_live.md` | Livekod loopar | C# | Modern | Kan användas direkt |
| `2025/1_oop/loop/exercises/pirat-spel.md` | Story-driven loop (interaktivt spel) | C# | Modern, kreativ | Kan användas direkt |
| `2025/1_oop/loop/exercises/rymdfarja-start.md` | Story-driven loop | C# | Modern | Kan användas direkt |
| `2025/1_oop/loop/exercises/restaurang-allergi.md` | Loop-övning med kontext | C# | Modern | Kan användas direkt |
| `2025/1_oop/loop/exercises/loop-exempel.md` | Grundövning loop | C# | Modern | Kan användas direkt |
| `2025/1_oop/loop/exercises/kvitto-mysteriet.md` | Loop + logik | C# | Modern | Kan användas direkt |
| `2023/csharp/Assignments/OOP/excersises/loops/` (Avengers, Deadpool, Star_Trek m.fl.) | Story-driven loop-övningar | C# | Gammal stil, goda idéer | Moderniseras (berättelserna är bra) |
| `2023/csharp/Assignments/OOP/information/loops/` (while, for, do_while m.fl.) | Konceptartiklar loopar | C# | Gammal | Plocka innehållet, modernisera |
| `2023/csharp/Assignments/Cheatsheets/for.md` | Cheatsheet for-loop | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/Cheatsheets/while.md` | Cheatsheet while | C# | Gammal | Moderniseras |

### Metoder

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/methods/lecture.md` | Marp-slides metoder (med Mermaid) | C# | Modern | Kan användas direkt |
| `2025/1_oop/methods/exercises/calculator_methods.md` | Övning metoder kalkylator | C# | Modern | Kan användas direkt |
| `2025/1_oop/methods/exercises/temperature_converter.md` | Övning temperaturkonvertering | C# | Modern | Kan användas direkt |
| `2025/1_oop/methods/exercises/string_methods.md` | Övning strängmetoder | C# | Modern | Kan användas direkt |
| `2025/1_oop/methods/exercises/grade_calculator.md` | Övning betygsräknare | C# | Modern | Kan användas direkt |
| `2023/csharp/Assignments/OOP/information/methods/methods.md` | Konceptartikel metoder | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/OOP/excersises/methods/exercise1.md` | Metodövning 1 | C# | Gammal | Moderniseras |

### Pseudokod / problemlösning

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/problem_solving/task_decomposition.md` | Guide: uppgiftsdelning | — | Modern | Kan användas direkt |
| `2025/1_oop/problem_solving/programmer_mindset.md` | Tankesätt för programmerare | — | Modern | Kan användas direkt |
| `2025/1_oop/problem_solving/exercises/practical_task_decomposition.md` | Övning problemlösning | — | Modern | Kan användas direkt |
| `2023/csharp/Assignments/BionicReading/planering/programmermind.md` | Programmerartänk | — | Gammal | Plocka idéer |

---

## VECKA 2 — Story-driven code

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/story_driven_code/level_1_easy/harry_potter_variables_adventure.md` | Story-driven (variabler) | C# | Modern, med scaffolding + tips | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/star_wars_variables_adventure.md` | Story-driven (variabler) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/sherlock-holmes-mystery.md` | Story-driven (variabler) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/arsene-lupin-vault.md` | Story-driven (variabler) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/anakin_crystal_adventure.md` | Story-driven (variabler) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/troll.md` | Story-driven (enkel) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/harry-potter-zonkos-basement.md` | Story-driven (enkel) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/Kepler-442b.md` | Story-driven (sci-fi) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/level_1_easy/solutions/` (7 lösningar .cs) | Lösningsfiler | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/exercises/level_2_medium/lord_of_rings_methods_adventure.md` | Story-driven (metoder, medel) | C# | Modern | Kan användas direkt |
| `2025/1_oop/story_driven_code/exercises/level_2_medium/solutions/lord_of_rings_methods_adventure_solution.cs` | Lösning metod-äventyr | C# | Modern | Kan användas direkt |
| `2023/csharp/Assignments/OOP/excersises/loops/Jurassic_Park.md` | Story-driven (loopar, äldre) | C# | Gammal stil, god idé | Moderniseras |
| `2023/csharp/Assignments/OOP/excersises/loops/Avengers.md` | Story-driven (loopar) | C# | Gammal stil | Moderniseras |

**Notera:** Story-driven material är klart starkast i 2025. Stor rikedom på nivå 1, sparsamt på nivå 2.

---

## VECKA 3 — UML-klassdiagram, klasser, konstruktorer, properties, inkapsling

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/classes/lectures/classes_marp.md` | Marp-slides klasser (mörkt tema) | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/lectures/classes_advanced_marp.md` | Marp-slides avancerade klasser | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/lectures/class.lecture.marp.md` | Alternativ Marp-version | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/excersises/1_dice.md` | Övning klass (tärning, med skelett) | C# | Modern, välstrukturerad | Kan användas direkt |
| `2025/1_oop/classes/excersises/2_shopping.md` — `4_shopping3.md` | Progressiva övningar shopping | C# | Modern, progressiv | Kan användas direkt |
| `2025/1_oop/classes/excersises/5_clothes.md` — `7_coffee.md` | Övningar temaväxling | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/excersises/8_rpg_character.md` | Övning RPG-karaktär (klass) | C# | Modern, engagerande | Kan användas direkt |
| `2025/1_oop/classes/excersises/9_bank_account.md` | Övning bankkonto (inkapsling) | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/excersises/10_temperature.md` | Övning temperatur | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/excersises/11_weather_advisor.md` | Övning väderklass | C# | Modern | Kan användas direkt |
| `2025/1_oop/classes/excersises/12_code_review.md` | Code review-övning | C# | Modern | Kan användas direkt |
| `2024/csharp/1_oop/lectures/09_planning/2_classdiagram.md` | UML-klassdiagram (Mermaid, fullt) | — | Modern, med syntax + relationer | Kan användas direkt |
| `2024/csharp/1_oop/lectures/03_repetition_classes_objects/` (5 filer) | Klasser + konstruktorer (text-format) | C# | Halvmodern (ingen Marp) | Moderniseras till Marp |
| `books/csharp_cmyh/C-Sharp/oop/classes.md` | Referensartikel klasser | C# | ChatGPT-stil ("super cooloare") | Röstbyte |
| `books/csharp_cmyh/C-Sharp/oop/constructors.md` | Referensartikel konstruktorer | C# | ChatGPT-stil (stor tabell, för avancerad) | Röstbyte + nivåanpassning |
| `books/csharp_cmyh/C-Sharp/oop/properties.md` | Referensartikel properties | C# | ChatGPT-stil | Röstbyte |
| `books/csharp_cmyh/C-Sharp/oop/capsulation.md` | Referensartikel inkapsling | C# | ChatGPT-stil | Röstbyte |
| `books/csharp_cmyh/C-Sharp/oop/accessmodificators/` (5 filer) | Accessmodifierare (public, private m.fl.) | C# | ChatGPT-stil | Röstbyte |
| `2023/csharp/Assignments/OOP/information/class/info1.md` | Konceptartikel klasser (bilar) | Java/C# | Gammal, Java-kod i exemplet | Skriv om till C# |
| `2023/csharp/Assignments/OOP/excersises/classes/easy/` (movies, music, students, geometric) | Klassövningar lättare | C# | Gammal, god struktur | Moderniseras |
| `2023/csharp/Assignments/OOP/excersises/classes/medium/` (books, pets, names, measure) | Klassövningar medel | C# | Gammal | Moderniseras |
| `2022/Kodexempel/ClassIntro/` (.cs-filer: Property.cs, Dice.cs m.fl.) | Verkliga C#-filer (gamla men körbara) | C# | Gammal/oklar | Plocka logiken, skriv om |

---

## VECKA 4 — List&lt;T&gt;, enum, Dictionary

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/datastructures/lectures/lists.md` | Marp-slides List&lt;T&gt; | C# | Modern | Kan användas direkt |
| `2025/1_oop/datastructures/lectures/dictionaries.md` | Marp-slides Dictionary | C# | Modern | Kan användas direkt |
| `2025/1_oop/datastructures/lectures/advanced.md` | Avancerade datastrukturer | C# | Modern | Granska — kan vara för bred |
| `2025/1_oop/datastructures/exercises/list_basics.md` | Grundövning List | C# | Modern | Kan användas direkt |
| `2025/1_oop/datastructures/exercises/dictionary_phonebook.md` | Övning Dictionary (telefonkatalog, metoder) | C# | Modern, välstrukturerad | Kan användas direkt |
| `2025/1_oop/datastructures/exercises/inventory_management.md` | Övning inventariehantering | C# | Modern | Kan användas direkt |
| `2025/1_oop/datastructures/micro-exercises/lists.md` | Mikroövningar List | C# | Modern | Kan användas direkt |
| `2025/1_oop/datastructures/micro-exercises/dictionaries.md` | Mikroövningar Dictionary | C# | Modern | Kan användas direkt |
| `2025/1_oop/enums/articles/enums-comprehensive.md` | Referensartikel enum (Matrix, Gandalf-metaforer) | C# | Halvmodern (lite väl mycket emojis) | Röstbyte lite |
| `books/csharp_cmyh/C-Sharp/datastructures/list.md` | Referensartikel List | C# | Gammal struktur, ChatGPT-stil | Röstbyte |
| `books/csharp_cmyh/C-Sharp/datastructures/dictionary.md` | Referensartikel Dictionary | C# | ChatGPT-stil | Röstbyte |
| `books/csharp_cmyh/C-Sharp/variables_and_types/structured/enum.md` | Referensartikel enum | C# | ChatGPT-stil | Röstbyte |
| `2023/csharp/Assignments/Cheatsheets/list.md` | Cheatsheet List | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/Cheatsheets/dictionary.md` | Cheatsheet Dictionary | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/OOP/excersises/classes/easy/movies.md` | Övning List&lt;Film&gt; + klass | C# | Gammal men välupplagd | Moderniseras |

---

## VECKA 5 — Repetition + tenta

| Fil/Mapp | Typ | Språk | Kvalitet | Åtgärd |
|----------|-----|-------|----------|--------|
| `2025/1_oop/exam/STUDIEGUIDE_MED_LÄNKAR.md` | Studieguide (mappad till ämnen) | — | Modern | Kan användas som mall |
| `2025/1_oop/exam/git_basics_training.md` | Träningsfrågor Git (flerval) | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/variables_and_types_training.md` | Träningsfrågor variabler | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/if_statements_training.md` | Träningsfrågor if | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/loops_training.md` | Träningsfrågor loopar | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/data_structures_training.md` | Träningsfrågor datastrukturer | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/naming_conventions_training.md` | Träningsfrågor namngivning | — | Modern | Kan användas direkt |
| `2025/1_oop/exam/switch_statements_training.md` | Träningsfrågor switch | — | Modern | Kan användas direkt |
| `2023/csharp/Assignments/OOP/excersises/examprep/` (getsetters, variables m.fl.) | Tentaprep-övningar | C# | Gammal | Moderniseras |
| `2023/csharp/Assignments/OOP/information/tests/datatypes1.md` | Testfrågor datatyper | C# | Gammal | Moderniseras |

---

## Inlämningsuppgifter (referens)

| Fil/Mapp | Typ | Kvalitet | Kommentar |
|----------|-----|----------|-----------|
| `2025/1_oop/assignment/dungeon_crawler.md` | Inlämningsuppgift (textäventyr, klasser + Dictionary) | Modern | Stark uppgift, klar för CLO26 |
| `2025/1_oop/assignment/storage.md` | Inlämningsuppgift (lagersystem) | Modern | Alternativ uppgift |
| `2025/1_oop/assignment/sea_stuff.md` | Inlämningsuppgift (marint tema) | Modern | Alternativ uppgift |
| `2025/1_oop/assignment/teacher_matrix.md` | Bedömningsmatris | Modern | Kan användas direkt |
| `2024/csharp/1_oop/Assignments/Assignment1/` | Studentinlämningar (referens) | — | Inte kursinnehåll — visar vad 2024-studerande producerade |
| `Material från Codic/C#/Inlämningar/Tärningsspel (OOP)/` | Gammal inlämningsuppgift OOP | PDF/DOCX | Inte direkt användbar, kräver konvertering |

---

## Material som SAKNAS helt (behöver skapas från grunden)

| Ämne | Vad som saknas |
|------|----------------|
| **SSH** | Komplett guide: generera nyckel, lägg till GitHub, ssh-agent — finns ingenstans |
| **UML klassdiagram — slides** | Det finns förklaringstext (2024) men ingen dedikerad Marp-presentation för CLO26-stil |
| **Enum — övningar** | Enbart artikel, inga konkreta övningsfiler i 2025. Mikroövningar saknas |
| **Story-driven kod nivå 2** | Bara en fil (Lord of the Rings). Behövs 2–3 till för metoder + klasser |
| **Pseudokod-introduktion** | `programmer_mindset.md` finns men ingen strukturerad Marp-föreläsning om pseudokod |

---

## Sammanfattning

### Finns det gott om

- **Övningar på alla grundläggande ämnen (variabler, if, loop, metoder)** — 2025/1_oop är rikt, modernt och kan plockas nästan direkt
- **Story-driven kod nivå 1** — 8 filer med lösningar, varierande teman (Harry Potter, Star Wars, Sherlock Holmes, sci-fi, pirat)
- **Klassövningar** — 12 progressiva övningar i 2025/1_oop/classes, från tärning till bankkonto
- **Tentaprep-material** — 7 träningsfråge-filer med `<details>`-svar, redo att använda
- **Dictionary + List-övningar** — konkreta, välbyggda uppgifter
- **Referensboken** (`books/csharp_cmyh`) — täcker alla ämnen, men behöver röstbyte

### Finns det för lite av

- **SSH-material** — ingenstans i hela materialet
- **Story-driven kod nivå 2** (metoder + klasser) — bara 1 fil
- **Enum-övningar** — bara artikel, inga kodövningar
- **Marp-presentation om pseudokod** — saknas som dedikerat format
- **UML Marp-slides (CLO26-stil)** — texten finns men inte som slides

### Prioriterat att ta med direkt

1. Hela `2025/1_oop/story_driven_code/level_1_easy/` — använd direkt
2. `2025/1_oop/classes/excersises/` (1–12) — använd direkt, kärnan i vecka 3
3. `2025/1_oop/exam/` (alla 7 träningsfiler) — använd direkt till vecka 5
4. `2025/1_oop/datastructures/exercises/` — använd direkt till vecka 4
5. `2024/csharp/1_oop/lectures/09_planning/2_classdiagram.md` — UML-föreläsning, modernisera till Marp

### Prioriterat att modernisera

1. `2023/csharp/Assignments/Git/stepbysteplocal.md` + `stepbystepcloud.md` — bra struktur, behöver uppdateras
2. `books/csharp_cmyh/C-Sharp/oop/` (classes, constructors, properties) — allt content är rätt, bara röstbyte
3. `2023/csharp/Assignments/OOP/excersises/loops/` (Avengers, Jurassic Park m.fl.) — berättelserna är bra, formateringen gammal
