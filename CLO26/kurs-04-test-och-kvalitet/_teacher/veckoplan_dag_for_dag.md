# Veckoplan dag-för-dag — Kurs 04: Test och kvalitetssäkring

**Kurs:** 6 veckor | 30 YH-poäng  
**Schema:** Mån (online) + Ons fm + Ons em + Tor fm + Tor em = 5 pass/vecka  
**Tis + Fre:** Eget arbete  
**Examination:** Skriftligt prov + inlämningsprojekt (grupp)

**Filosofi:** Dopamin-driven development — grön test inom 15 min dag 1. Praktik före teori. Max 30 min föreläsning per pass, resten hands-on.

---

## Ämnen med tidsuppskattning

| Ämne | Tid | Nivå | Vecka |
|------|-----|------|-------|
| xUnit installation + första gröna test | ½ pass | 🟢 | V1 |
| AAA-mönstret (Arrange-Act-Assert) | ½ pass | 🟢 | V1 |
| Testtyper: unit, integration, UAT, E2E | ½ pass | 🟢 | V1 |
| TDD Calculator Kata | ½ pass | 🟡 | V1 |
| Testnamnskonventioner | ¼ pass | 🟢 | V1 |
| String Validator TDD | ½ pass | 🟡 | V1 |
| Code coverage (Coverlet) | ½ pass | 🟡 | V1 |
| SonarLint + complexity metrics | ½ pass | 🟡 | V1 |
| Shopping Cart TDD | ½ pass | 🟡 | V1 |
| Test-pyramiden | ¼ pass | 🟢 | V1 |
| Refactoring med TDD (legacy rescue) | 1 pass | 🟡 | V2 |
| Red-Green-Refactor cykeln | ½ pass | 🟡 | V2 |
| Mocking (NSubstitute) | 1 pass | 🔴 | V2 |
| Dependency Injection (testbarhet) | ½ pass | 🔴 | V2 |
| Integration tests + InMemory-databas | 1 pass | 🔴 | V2 |
| Repository pattern | ½ pass | 🟡 | V2 |
| SCRUM: sprint, backlog, standup, retro | 1 pass | 🟡 | V3 |
| User Stories + INVEST-principen | ½ pass | 🟢 | V3 |
| Gherkin + BDD (Given-When-Then) | 1 pass | 🟡 | V3 |
| SpecFlow (Gherkin i C#) | ½ pass | 🟡 | V3 |
| Kanban board + Planning Poker | ½ pass | 🟢 | V3 |
| GitHub Actions (CI/CD, auto-test) | 1 pass | 🟡 | V3 |
| Avancerade tester (async, Theory/InlineData) | 1 pass | 🔴 | V4 |
| API-testning (Postman / HttpClient) | ½ pass | 🟡 | V4 |
| OWASP Top 10 + security testing | 1 pass | 🟡 | V4 |
| E2E-testning (Playwright) | ½ pass | 🟡 | V4 |
| CI/CD pipeline (avancerad) + badges | ½ pass | 🟡 | V4 |
| Tenta-prep + repetition | 1 pass | — | V5 |
| Tenta | 1 pass | — | V5 |
| Demo Day + projektinlämning | 1 pass | — | V6 |

**Totalt:** ~21 pass aktiv undervisning + 9 pass projektarbete/handledning

---

## Vecka 1 — "Min första gröna test!"

**Mål:** Grön test inom 15 minuter dag ett. Kan skriva unit tests med xUnit. Förstår coverage och testtyper.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | **Livekod (inte föreläsning):** Installera xUnit, skapa första testet TILLSAMMANS. 🎯 Grön test inom 15 min. Förklara AAA EFTER att vi sett det. Testtyper: unit, integration, UAT (visa pyramiden) | xUnit + AAA + testtyper |
| Ons | fm | **TDD Calculator Kata:** Add, Subtract, Multiply, Divide. Alla kodar med. Lektion (20 min): testnamnskonventioner + MSTest vs xUnit-jämförelse | TDD kata + namnkonventioner |
| Ons | em | **String Validator TDD:** email-validator, telefonvalidator, lösenordsstyrka. Lektion (15 min): edge cases och boundary testing | String Validator TDD |
| Tor | fm | **Shopping Cart TDD:** AddItem, RemoveItem, CalculateTotal, ApplyDiscount. Kör coverage — sikta på 80%+. Installera Coverlet live. Lektion (20 min): SonarLint + complexity metrics | Shopping Cart + coverage |
| Tor | em | Avancera Shopping Cart: membership discount, buy-X-get-Y. Refactor när tester blir röda. Lektion (15 min): test-pyramiden (nu förstår de varför!) | Advanced cart + pyramid |

**Nyckelord denna vecka:** `xUnit`, `[Fact]`, `AAA`, `Arrange-Act-Assert`, `Assert`, `unit test`, `code coverage`, `SonarLint`, `testnamn`, `TDD`

**Show & Tell ons + tor 15:30:** Studerande visar sin lösning på storskärm.

**Inlämning:** Ingen.

---

## Vecka 2 — "Clean code som är testbar!"

**Mål:** Kan refaktorera ful kod med TDD. Förstår mocking och DI. Skriver integration tests.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | **"Rescue Mission":** Ta legacy-kod (ful, utan tester). Skriv tester FÖRST, sedan refactor. 🎯 Ful kod blir ren, tester är gröna. Clean Code-principer förklaras MEDAN vi refactorar | Refactoring med TDD |
| Ons | fm | **Red-Green-Refactor visuellt på tavlan.** TDD Password Manager: spara, hämta, generera, kontrollera styrka. Skriv test FÖRST — se det rött — implementera — grönt — refactor. Lektion (20 min): mocking — varför? NSubstitute vs Moq | TDD full cykel + mocking intro |
| Ons | em | **Weather Service med mockat API:** introducera NSubstitute, mocka HTTP-anrop. Lektion (15 min): DI gör koden testbar — visa i deras kod | NSubstitute + DI |
| Tor | fm | **Integration tests + InMemory-databas.** Todo API: unit tests för business logic, integration tests för endpoints, InMemory SQLite. Lektion (20 min): Repository pattern — när och varför | Integration tests + Repository |
| Tor | em | Fortsätt Todo API: lägg till användare + kategorier, refaktorera till Repository pattern. Tester ska förbli gröna under refactoring! Lektion (15 min): testable architecture | Repository pattern + clean arch |

**Nyckelord denna vecka:** `mocking`, `NSubstitute`, `[Fact]`, `dependency injection`, `InMemory`, `integration test`, `Repository pattern`, `refactoring`, `Red-Green-Refactor`

**Show & Tell tor 15:30:** Grupper visar sitt API live med integration tests på storskärm.

**Inlämning:** Ingen.

---

## Vecka 3 — "Vi jobbar som ett riktigt team!"

**Mål:** Kan köra en sprint. Skriver User Stories + Gherkin. Har en CI/CD-pipeline som triggas vid commit.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | **Sprint Planning live:** dela upp i teams (2–3 pers). "Kundförfrågan" ges ut (ex: bokningssystem). 🎯 Teams skriver User Stories inom 30 min. Marcus spelar Product Owner. Planning Poker för estimering. SCRUM-roller spelas live | SCRUM + User Stories |
| Ons | fm | **Sprint Simulation:** teams bygger en feature från sin story. Börja med Gherkin (Given-When-Then). 🎯 Acceptance criteria som tester. Daily standup kl 10:30. Lektion (15 min): BDD + SpecFlow | Gherkin + BDD + SpecFlow |
| Ons | em | Fortsätt sprint: implementera feature med TDD. Skriv UAT-planer. 🎯 Fungerande feature + UAT-plan klar. Mini-standup (10 min) | Sprint + UAT |
| Tor | fm | **Projektstart:** rösta på projekttyp (todo/quiz/booking/inventory/blog). Bilda teams, brainstorma features, skapa GitHub Kanban board, skriv User Stories, Planning Poker. Lektion: Kanban board | Projektstart + Kanban |
| Tor | em | Börja koda! Setup: projekt, tester, CI/CD. Implementera första feature med TDD. Lektion (20 min): GitHub Actions — sätt upp pipeline live. 🎯 Commit → auto-test → grön badge | GitHub Actions CI/CD |

**Nyckelord denna vecka:** `SCRUM`, `sprint`, `backlog`, `standup`, `retrospective`, `User Story`, `INVEST`, `Gherkin`, `Given-When-Then`, `BDD`, `SpecFlow`, `Kanban`, `GitHub Actions`

**Projektspec delas ut:** Tor i slutet.

**Inlämning:** Projekt kickas denna vecka. Deadline vecka 6.

---

## Vecka 4 — "Testa ALLT!"

**Mål:** Skriver async/parametriserade tester. Förstår OWASP. Har en fullständig CI/CD-pipeline.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | **Livekod advanced testing:** async/await i tester, `[Theory]` + `[InlineData]` (skriv 1 test — kör 100 scenarios). Flaky tests — orsaka dem + fixa dem. Teams testar varandras API:er över video | Avancerade tester |
| Ons | fm | **API-testning med Postman/HttpClient.** Teams testar sitt eget API. Integration tests för alla endpoints. 🎯 Alla endpoints har minst 1 test. Lektion (20 min): OWASP Top 10 — SQL injection + XSS live-demo | API-testning + OWASP |
| Ons | em | **"Security Quest":** hacka en sårbar app (lärarmaterial), hitta 5 brister, skriv tester som förhindrar dem. Lektion (15 min): E2E-testning med Playwright — live-demo | Security testing + E2E |
| Tor | fm | **Pipeline Day:** pimpa GitHub Actions — automatisera ALL testning, badge i README. 🎯 Commit → auto-test → grön badge. Lektion (20 min): test metrics + coverage reports | CI/CD + badges |
| Tor | em | Teams kodar på projekten. Pair programming (byt partner varje timme). Code reviews. Mini-standup + vecko-review | Projektarbete |

**Nyckelord denna vecka:** `[Theory]`, `[InlineData]`, `async`, `flaky test`, `OWASP`, `SQL injection`, `XSS`, `Playwright`, `E2E`, `GitHub Actions`, `coverage badge`

**Inlämning:** Ingen ny — projektarbete pågår.

---

## Vecka 5 — "Finish line!"

**Mål:** Tentan klarad. Projektet MVP-klart. UAT genomförd.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | **Tenta-prep Kahoot + livekod.** Alla kunskapsmål går igenom. Kodningsutmaningar live: TDD, mocking, refactoring. Q&A | Tenta-prep |
| Ons | fm | **Sprint Finale:** teams slutför MVP. UAT-genomförande: testa varandras projekt. 🎯 MVP som funkar + feedback från UAT. Mini-workshop (15 min): README och dokumentation | Sprint finale + UAT |
| Ons | em | Code reviews mellan teams. Fixa feedback från UAT. Dokumentation + reflektionsdokument. Snabb repetition (20 min): viktigaste tenta-koncept | Polish + review |
| Tor | fm | Sista handledning: finputsa dokumentation, sista bugfixar. Code Review Showcase: bästa testerna + kreativaste lösningen | Handledning + showcase |
| Tor | em | Tenta-workshop: genomgång av alla kunskapsmål, "highest-risk" områden. Individuell repetition. Frågestund | Tenta-workshop |
| Fre | — | **TENTA** 09:00–12:00 | Tenta |

**Tenta täcker:** TDD (Red-Green-Refactor), xUnit-syntax, AAA, testtyper, mocking (NSubstitute), DI, SCRUM-terminologi, User Stories, Gherkin, Clean Code/SOLID, coverage

**Betygsgräns:** sätts per kurs.

---

## Vecka 6 — "Show Time!"

**Mål:** Projektdemo genomförd. Omtenta för de som behöver. Kurs avslutad.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån | online | Projektarbete: finputsning, förbered presentation | Projektarbete |
| Ons | fm | Projektarbete + handledning: sista bugfixar, UAT, README | Projektarbete |
| Ons | em | Handledning: förbered demos | Handledning |
| Tor | fm | **Demo Day:** varje team presenterar 30 min — live-demo, gröna tester, CI/CD pipeline, Q&A | Demo Day |
| Tor | em | "Oscars": rösta Best Test / Cleanest Code / Most Creative. Kurs-retrospective. Omtenta-prep | Ceremoni + omtenta-prep |
| Fre | — | **OMTENTA** 09:00–12:00 | Omtenta |

**Projektdeadline:** Sön vecka 6 23:59

**Projektkrav (kort):**
- ASP.NET Core Web API, minst 5 endpoints
- xUnit unit tests — minst 80% coverage
- Integration tests
- UAT-plan
- SCRUM-dokumentation (User Stories, backlog, standup-logg, Kanban)
- GitHub Actions CI/CD
- Clean Code + SOLID
- VG: E2E, security tests, refaktorering med motivering, UAT med riktiga testpersoner

---

## Sammanfattning: svårighetsgrad och tid per ämne

```
xUnit + AAA + första gröna test  🟢 ½ pass  — snabb win, sätter tonen
Testnamnskonventioner            🟢 ¼ pass  — visa 3 stilar, välj en
TDD Calculator kata              🟡 ½ pass  — konkret, alla lyckas
String Validator TDD             🟡 ½ pass  — real-world direkt
Code coverage + SonarLint        🟡 ½ pass  — visuellt — se siffran stiga
Test-pyramiden                   🟢 ¼ pass  — lätt när de redan skrivit tester
Refactoring med TDD              🟡 1 pass  — rescue-mission-formatet funkar
Red-Green-Refactor               🟡 ½ pass  — viktigt att se cykeln explicit
Mocking (NSubstitute)            🔴 1 pass  — abstrakt, visa tydlig use-case
Dependency Injection             🔴 ½ pass  — länka till testbarhet, inte DI i sig
Integration tests                🔴 1 pass  — InMemory-databas är nyckeln
Repository pattern               🟡 ½ pass  — koppla till testbarhet
SCRUM + User Stories             🟡 1 pass  — rollspela — inte powerpointas
Gherkin + BDD                    🟡 1 pass  — Given-When-Then klickar snabbt
SpecFlow                         🟡 ½ pass  — syntax är lätt när Gherkin sitter
Kanban                           🟢 ½ pass  — GitHub Projects räcker
GitHub Actions CI/CD             🟡 1 pass  — visa minsta tänkbara pipeline
Avancerade tester (Theory)       🔴 1 pass  — InlineData är snyggt, async är nytt tänk
OWASP + security testing         🟡 1 pass  — live-demo av SQL injection sitter
E2E (Playwright)                 🟡 ½ pass  — demo, inte djupdyk
```

---

## Teknisk stack

- C# (.NET 10), ASP.NET Core Web API
- xUnit (primärt), MSTest (intro)
- NSubstitute (mocking)
- Entity Framework Core, SQLite/InMemory
- SonarLint, Coverlet (coverage)
- SpecFlow (BDD)
- GitHub Actions (CI/CD)
- Playwright (E2E, demo)
- GitHub Projects (Kanban)
