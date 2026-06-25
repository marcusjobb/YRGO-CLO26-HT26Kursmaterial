# Inventering av gammalt kursmaterial — CLO26 kurs-04

**Genomförd:** 2026-06-15
**Kurslängd:** 6 veckor
**Ämnesområden:** xUnit/TDD/AAA, katas, refactoring, mocking/DI, integrationstester, SCRUM/BDD/Gherkin/SpecFlow, CI/CD, avancerade tester, OWASP, E2E

---

## Källmappar som genomsökts

| Källa | Innehåll i korthet |
|-------|-------------------|
| `/home/marcus/git/Old_courses/2025/4_test_and_quality_assurance/` | **Primärkälla.** Komplett kurs körd CLO25 (dec 2025–jan 2026). Marp-slides, katas, exam, inlämningsuppgift, quiz. |
| `/home/marcus/git/Old_courses/2024/csharp/4_test/` | Äldre C#-kurs med 11 moduler. Teori-tunga textlektioner, MSTestV2 (inte xUnit), Java-förorenat material. |
| `/home/marcus/git/Old_courses/2024/java/4_test/` | Java/JUnit5-kurs. Struktur i linje med 2024 C#-kursen men Java-specifik — används enbart för pedagogisk struktur. |
| `/home/marcus/git/Old_courses/2023/csharp/TDDGithub/` | Litet livekod-repo: Calculator med xUnit + GitHub Actions workflow. |
| `/home/marcus/git/Old_courses/2022/TDD/` | Två konsolprojekt: Account-tester (MSTest), MoqDemo (Moq-biblioteket, inte NSubstitute). |
| `/home/marcus/git/Old_courses/2022/Kodkvalite/` | Textlektioner om Clean Code (namngivning, metoder, klasser, kommentarer, refaktorering, testning). |
| `/home/marcus/git/Old_courses/Material från Codic/` | Primärt .docx/.pptx/.pdf. TDD-inlämningar (Blackjack, Game of Life, Geometri, Hushållsekonomi). Ej direkt återanvändbart. |
| `/home/marcus/git/Old_courses/JinTDDProject/` | Studentexempel (Java). Minimalt. |

---

## Vecka 1 — xUnit, AAA-mönstret, testtyper, TDD-intro & katas

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/xunit_intro/xunit_intro_marp.md` | Föreläsning (Marp) | Modern — mörkt tema, xUnit, C# 14-stil | Kan användas direkt |
| `2025/lectures/aaa_pattern/aaa_pattern_marp.md` | Föreläsning (Marp) | Modern — tydlig struktur, xUnit-exempel | Kan användas direkt |
| `2025/lectures/tdd_intro/tdd_intro_marp.md` | Föreläsning (Marp) | Modern — kortfattad, Red-Green-Refactor tydligt | Kan användas direkt |
| `2025/lectures/test_types/test_types_marp.md` | Föreläsning (Marp) | Modern | Kan användas direkt |
| `2025/lectures/test_pyramid/test_pyramid.md` | Artikel/läsning | Modern | Kan användas direkt |
| `2025/lectures/test_naming_conventions/test_naming_marp.md` | Föreläsning (Marp) | Modern | Kan användas direkt |
| `2025/exercises/calculator_kata.md` | Kata (övning) | Modern — AAA, xUnit, steg-för-steg med Red-Green | Kan användas direkt |
| `2025/exercises/string_validator.md` | Kata (övning) | Modern — Theory/InlineData, edge cases | Kan användas direkt |
| `2025/exercises/shopping_cart.md` | Kata (övning) | Modern — komplex TDD, collections | Kan användas direkt |
| `2025/exercises/buggy_code_kata.md` | Kata (övning) | Modern — hitta och fixa buggar | Kan användas direkt |
| `2025/exercises/buggy_code_kata_2.md` | Kata (övning) | Modern | Kan användas direkt |
| `2025/quiz/xunit_basics_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/aaa_pattern_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/tdd_basic_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/tdd_phases_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/TDD_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/Testtyper_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/testnamn_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/test_pyramiden_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/unit_tests_training.md` | Quiz | Modern | Kan användas direkt |
| `2023/csharp/TDDGithub/` | Livekod-projekt (Calculator + xUnit) | Modern — xUnit, riktig .cs-kod | Kan användas som exempelkod |
| `2024/csharp/4_test/lectures/01_introduktion_xunit/1_marp.md` | Föreläsning (Marp) | Gammal stil — generisk, teori-tung, MSTestV2-tendenser | Behöver skrivas om |
| `2024/csharp/4_test/lectures/03_tdd/1_introduktion_till_testdriven_utveckling_tdd.md` | Teori-text | Gammal stil — lång, Java-influerad, pratar om JUnit5 | Kan användas som referens — ej direkt |
| `2022/Kodkvalite/Testning.md` | Artikel | Gammal stil — MSTestV2, Console-mocking-exempel | Behöver skrivas om |

---

## Vecka 2A — Refactoring med TDD, Red-Green-Refactor

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/exercises/christmas_manager_kata.md` | Kata (övning) | Modern — refactoring med TDD-skyddstest | Kan användas direkt (byte av jultema till neutralt) |
| `2025/exercises/christmas_weather_kata.md` | Kata (övning) | Modern | Kan användas direkt (tema-byte) |
| `2024/csharp/4_test/lectures/03_tdd/2_red-green-refactor_cycle.md` | Teori-text | Gammal stil — lång, Java-referens | Kan användas som referens |
| `2022/Kodkvalite/Refaktorering.md` | Artikel | Gammal stil — kortfattad, ingen xUnit | Behöver skrivas om |
| `2022/Kodkvalite/CleanCode/CleanCode-Namngivning.md` | Artikel | Gammal men tydlig stil | Kan användas som referens |
| `2022/Kodkvalite/CleanCode/CleanCode-Metoder.md` | Artikel | Gammal men tydlig stil | Kan användas som referens |
| `2022/Kodkvalite/CleanCode/CleanCode-Klasser.md` | Artikel | Gammal men tydlig stil | Kan användas som referens |
| `2022/Kodkvalite/CleanCode/CleanCode-Kommentarer.md` | Artikel | Gammal men tydlig stil | Kan användas som referens |
| `2022/Kodkvalite/CodeReview.md` | Artikel | Gammal | Kan användas som referens |

---

## Vecka 2B — Mocking med NSubstitute & Dependency Injection

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/mocking_nsubstitute/mocking_nsubstitute_marp.md` | Föreläsning (Marp) | Modern — NSubstitute, C#, jultema (tomte-API) | Kan användas direkt (tema-byte valbart) |
| `2025/lectures/dependency_injection/dependency_injection_marp.md` | Föreläsning (Marp) | Modern | Kan användas direkt |
| `2025/exercises/christmas_wishlist_api_kata.md` | Kata (övning) | Modern — mocking + DI | Kan användas direkt (tema-byte) |
| `2025/quiz/Mocking_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/dependency_injection_training.md` | Quiz | Modern | Kan användas direkt |
| `2024/csharp/4_test/lectures/04_mockning_integrationstestning/2_nsubstitute_fr_mockning_i_c_.md` | Teori-text | Gammal stil — mycket text, Mermaid-diagram, bra djup | Kan användas som referens |
| `2024/csharp/4_test/lectures/04_mockning_integrationstestning/1_introduktion_till_mockning.md` | Teori-text | Gammal stil | Kan användas som referens |
| `2022/TDD/MoqDemo/` | Livekod-projekt | Gammal — Moq (inte NSubstitute), interface-mönster OK | Behöver moderniseras (byt Moq → NSubstitute) |

---

## Vecka 2C — Integration tests, InMemory-databas, Repository pattern

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/integration_tests/integration_tests_marp.md` | Föreläsning (Marp) | Modern — EF Core InMemory, xUnit | Kan användas direkt |
| `2025/lectures/repository_pattern/repository_pattern_marp.md` | Föreläsning (Marp) | Modern — Interface, IRepository, DI | Kan användas direkt |
| `2025/exercises/projects/ChristmasWishlistAPI/` | Livekod-projekt (.cs) | Modern — komplett API med Repository, InMemory-db, tester | Kan användas direkt (tema-byte) |
| `2025/exercises/projects/BankAccount/` | Livekod-projekt (.cs) | Modern — enkel BankAccount med xUnit-tester | Kan användas direkt |
| `2025/exercises/projects/MetricConverter/` | Livekod-projekt (.cs) | Modern — konverterare med tester | Kan användas direkt |
| `2025/quiz/testverktyg_training.md` | Quiz | Modern | Kan användas direkt |
| `2024/csharp/4_test/lectures/04_mockning_integrationstestning/3_integrationstestning_med_entity_framework_core.md` | Teori-text | Gammal stil — lång men korrekt EF Core InMemory-innehåll | Kan användas som referens |

---

## Vecka 3A — SCRUM, User Stories, Gherkin/BDD, SpecFlow

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/scrum_intro/scrum_intro_marp.md` | Föreläsning (Marp) | Modern — SCRUM-roller, events, Planning Poker | Kan användas direkt |
| `2025/lectures/scrum_intro/scrum_detailed_guide.md` | Läsmaterial | Modern — fullständig guide | Kan användas direkt |
| `2025/lectures/scrum_intro/scrum_exercises.md` | Övningar | Modern | Kan användas direkt |
| `2025/lectures/scrum_intro/planning_poker_guide.md` | Guide | Modern | Kan användas direkt |
| `2025/lectures/scrum_intro/TEACHER_NOTES.md` | Lärarguide | Modern | Kan användas direkt |
| `2025/lectures/scrum_intro/templates.md` | Mallar | Modern | Kan användas direkt |
| `2025/exercises/templates/user_story_template.md` | Mall | Modern | Kan användas direkt |
| `2025/exercises/templates/sprint_planning_template.md` | Mall | Modern | Kan användas direkt |
| `2025/exercises/templates/sprint_review_template.md` | Mall | Modern | Kan användas direkt |
| `2025/exercises/templates/sprint_retrospective_template.md` | Mall | Modern | Kan användas direkt |
| `2025/exercises/tuesday_sprint_planning_exercise.md` | Övning | Modern | Kan användas direkt |
| `2025/exercises/wednesday_sprint_simulation.md` | Övning | Modern — simulerar en hel sprint | Kan användas direkt |
| `2025/lectures/bdd_gherkin/presentation.md` | Föreläsning (Marp) | Modern — Given-When-Then, koppling till AAA | Kan användas direkt |
| `2025/lectures/bdd_gherkin/LIVEKODNING_CSHARP.md` | Livekodningsguide | Modern — C#, xUnit + Gherkin-mönster | Kan användas direkt |
| `2025/lectures/bdd_gherkin/LIVEKODNING.md` | Livekodningsguide | Modern | Kan användas direkt |
| `2025/exercises/gherkin_tdd_kata.md` | Kata (övning) | Modern — Gherkin → xUnit-tester → TDD | Kan användas direkt |
| `2025/quiz/Gherkin_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/user_stories_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/SCRUM_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/scrum_events_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/scrum_roller_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/scrum_practice_questions.md` | Quiz | Modern | Kan användas direkt |
| `2025/assignment/themes/*.md` (6 teman) | User Stories per tema | Modern — 6 fullständiga temapaket | Kan användas direkt |
| `2025/assignment/cheatsheets/scrum_cheatsheet.md` | Cheatsheet | Modern | Kan användas direkt |
| `2024/csharp/4_test/lectures/08_user_story_and_acceptance_criteria/3_automatisering_av_acceptanstester_med_specflow.md` | Teori-text | Gammal stil men **SpecFlow-specifik**, c#-kod, MSTest | Behöver moderniseras (byt MSTest → xUnit) |
| `2024/csharp/4_test/lectures/08_user_story_and_acceptance_criteria/2_introduktion_till_bdd_och_gherkin-syntax.md` | Teori-text | Gammal stil | Kan användas som referens |
| `2024/csharp/4_test/lectures/07_agile_methods_and_scrum/1_introduktion_till_agila_metoder.md` | Teori-text | Gammal stil — lång, Java-influenser | Kan användas som referens |

---

## Vecka 3B — GitHub Actions CI/CD

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/cicd_intro/cicd_intro_marp.md` | Föreläsning (Marp) | Modern — GitHub Actions, .NET-pipeline | Kan användas direkt |
| `2025/lectures/cicd_intro/yaml_grunder.md` | Läsmaterial | Modern | Kan användas direkt |
| `2025/exercises/github_actions_cicd_setup.md` | Övning | Modern — steg-för-steg setup | Kan användas direkt |
| `2025/exercises/example_workflow_dotnet10.yml` | YAML-fil | Modern — .NET 10 workflow | Kan användas direkt |
| `2025/exercises/monster_arena_cicd_guide.md` | Övning | Modern — CI/CD med befintligt projekt | Kan användas direkt |
| `2025/assignment/articles/ci_cd_setup_github_actions.md` | Artikel | Modern | Kan användas direkt |
| `2025/assignment/cheatsheets/git_flow_cheatsheet.md` | Cheatsheet | Modern | Kan användas direkt |
| `2025/quiz/cicd_training.md` | Quiz | Modern | Kan användas direkt |
| `2023/csharp/TDDGithub/.github/workflows/dotnet-desktop.yml` | Workflow-fil | Gammal — .NET Desktop-mall, behöver uppdateras | Behöver moderniseras |
| `Material från Codic/CICD/Övningar/Testning på Github.pdf` | PDF-övning | Gammal — Python/PyTest | Ej relevant (Python) |

---

## Vecka 4A — Avancerade tester: Theory/InlineData, async, flaky tests

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/advanced_testing/advanced_testing_marp.md` | Föreläsning (Marp) | Modern — async, Theory, InlineData, flaky tests | Kan användas direkt |
| `2025/exercises/theory_inlinedata_kata.md` | Kata (övning) | Modern — Theory, InlineData, steg-för-steg | Kan användas direkt |
| `2025/exercises/async_api_testing_kata.md` | Kata (övning) | Modern — SWAPI, HttpClient, async tester | Kan användas direkt |
| `2025/exercises/flaky_test_hunt_kata.md` | Kata (övning) | Modern — Race conditions, timing-problem, fix-övning | Kan användas direkt |
| `2025/quiz/theory_inlinedata_training.md` | Quiz | Modern | Kan användas direkt |
| `2025/quiz/async_testing_training.md` | Quiz | Modern | Kan användas direkt |

---

## Vecka 4B — OWASP Top 10 & security testing

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/lectures/security_testing/security_testing_marp.md` | Föreläsning (Marp) | Modern — OWASP Top 10, C#-demo-kod | Kan användas direkt |
| `2025/lectures/security_testing/OWASP_TOP_10_QUICK_REFERENCE.md` | Referensguide | Modern — komprimerad snabbreferens | Kan användas direkt |
| `2025/lectures/security_testing/demo_vulnerable_code/README.md` | Demo-projekt | Modern — sårbar kod att säkra | Kan användas direkt |
| `2025/lectures/security_testing/monster_arena_skeleton/` | Startprojekt | Modern — skeleton för säkerhetsövning | Kan användas direkt |
| `2025/exercises/santa_password_manager_kata.md` | Kata (övning) | Modern — säkerhetsmönster, lösenordshantering | Kan användas direkt (tema-byte) |
| `2025/quiz/owasp_training.md` | Quiz | Modern | Kan användas direkt |
| `2024/java/7_system_integration/14_secutity_testing/1_.md` | Teori-text | Gammal stil — Java, Spring Security-fokus | Ej relevant (Java/Spring) |

---

## Vecka 4C — E2E med Playwright

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/quiz/e2e_training.md` | Quiz | Modern — E2E-begrepp | Kan användas direkt |
| *(inget annat Playwright-material hittades)* | — | — | **Saknas helt — måste skapas från scratch** |

---

## Examination & inlämningsuppgift

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/exam/tenta_test_kvalitet_2026-01-16.md` | Tenta (studentversion) | Modern — 60p, G/VG-gränser tydliga, täcker alla ämnen | Kan användas direkt (datumjustering) |
| `2025/exam/tenta_test_kvalitet_2026-01-16_facit.md` | Tentafacit | Modern | Kan användas direkt |
| `2025/exam/ovningstenta.md` | Övningstenta | Modern | Kan användas direkt |
| `2025/exam/tentafragor_test_och_kvalitet.md` | Frågebank | Modern — stor pool med extra frågor | Kan användas direkt |
| `2025/assignment/README.md` | Projektinlämning | Modern — 2-veckors grupprojekt, SCRUM+TDD+CI/CD | Kan användas direkt (tema-byte och datumanpassning) |
| `2025/assignment/INLAMNING.md` | Inlämningsinstruktioner | Modern | Kan användas direkt |
| `2025/assignment/QUICKSTART.md` | Guide | Modern | Kan användas direkt |
| `2025/assignment/reflection-mall.md` | Reflektionsmall | Modern | Kan användas direkt |
| `2025/assignment/teacher_matrix.md` | Bedömningsmatris | Modern | Kan användas direkt (anpassa mot CLO26-kursplan) |
| `2025/assignment/teacher_instructions.md` | Lärarinstruktioner | Modern | Kan användas direkt |
| `Material från Codic/C#/Inlämningar/Blackjack (TDD)/` | Inlämningsuppgift (docx) | Gammal — TDD-projekt, Blackjack-domän | Kan tjäna som idé-källa |
| `Material från Codic/C#/Inlämningar/Game of life (TDD)/` | Inlämningsuppgift (docx) | Gammal — Conway's Game of Life | Kan tjäna som idé-källa |

---

## Kursplanering & dagschema (lärarmaterial)

| Fil/Mapp | Typ | Kvalitet | Åtgärd |
|---|---|---|---|
| `2025/kursplanering.md` | Kursplanering (lärare) | Modern — veckovis dagschema, "Dopamin-Driven Development" | Läs som mall för CLO26 |
| `2025/resurser_per_dag.md` | Daglig resurslista | Modern | Kan användas som referens |
| `2025/exercises/dagschema_2025-12-18.md` | Dagschema | Modern | Mall för CLO26 |
| `2025/exercises/dagschema_2026-01-08.md` | Dagschema | Modern | Mall för CLO26 |
| `2025/exercises/code_review_workflow.md` | Workflow-guide | Modern | Kan användas direkt |
| `2025/exercises/daily_standup_guide.md` | Guide | Modern | Kan användas direkt |

---

## Notiser om 2024-kursen (C#)

2024-kursen (`/home/marcus/git/Old_courses/2024/csharp/4_test/`) är strukturellt rik — 11 moduler med README, Marp, teori-text, quiz, övningar, exempelkod per modul. Den täcker också ämnen som **MSTestV2**, **mutations- och property-based testing**, och **statisk kodanalys** som faller utanför CLO26-scope. 

Teori-texterna är välskrivna men i gammal stil: långa, pratar om JUnit5 i mitten av C#-material, och är klart AI-genererade utan Marcuskänslan. De passar som referensmaterial men ska inte kopieras rakt av.

---

## Sammanfattning

### Det finns gott om

- **xUnit, AAA, TDD-intro, Red-Green-Refactor** — 2025-kursen har allt: Marp-slides, katas (Calculator, String Validator, Shopping Cart, Buggy Code), quizar och livekodprojekt. Kvaliteten är hög och materialet är klart.
- **Katas** — 10+ övningsuppgifter i modern stil. Alla följer Red-Green-Refactor och AAA konsekvent.
- **Mocking med NSubstitute** — komplett föreläsning + kata + quiz. Använder rätt bibliotek.
- **Integration tests + Repository pattern** — föreläsning, livekodprojekt med InMemory-databas, xUnit.
- **SCRUM + User Stories + Gherkin/BDD** — 2025-kursen har hela ekosystemet: slides, mallar, övningar, simuleringsdagar, User Stories per tema (6 stycken).
- **CI/CD med GitHub Actions** — föreläsning, steg-för-steg-övning, YAML-fil för .NET 10.
- **Avancerade tester** — Theory/InlineData, async-tester (SWAPI), flaky tests. Allt samlat i en föreläsning och tre separata katas.
- **OWASP Top 10 + security testing** — föreläsning, snabbreferens, demo-projekt, kata.
- **Examination** — fullständig tenta med facit, övningstenta, frågebank och inlämningsuppgift med projektformat.
- **Quizar** — 35+ quizfiler täcker praktiskt taget varje ämne i kursen.

### Det saknas helt eller nästan helt

- **Playwright / E2E-testning** — finns bara ett quiz (`e2e_training.md`) med begrepp. Ingen föreläsning, ingen kata, ingen livekodguide. Måste skapas från scratch.
- **SpecFlow (automatiserade BDD-tester)** — 2024-kursen har en teori-text om SpecFlow med MSTest, men ingen xUnit-version och ingen färdig kata/övning. Texten behöver moderniseras och kompletteras med en praktisk övning.
- **Refactoring-fokusad föreläsning** — det finns katas som innehåller refactoring-moment, men ingen renodlad föreläsning om refactoring-tekniker (Extract Method, Rename, strangler fig pattern etc.) anpassad för kurs-04.

### Jultema-beroenden att byta ut

Flera av 2025-materialen har jultema (tomten, önskelista, jul) eftersom kursen kördes i december. Teman att byta vid CLO26:
- `mocking_nsubstitute_marp.md` — "tomtens API"
- `integration_tests_marp.md` — "önskeliste-API"
- `repository_pattern_marp.md` — jultema
- `christmas_manager_kata.md`, `christmas_weather_kata.md`, `christmas_wishlist_api_kata.md` — katas
- `santa_password_manager_kata.md` — security kata

Innehållet är utmärkt — bara temat behöver anpassas.
