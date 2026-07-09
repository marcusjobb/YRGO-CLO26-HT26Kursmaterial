# 03 Tdd — Programmeringstermer

## TDD
Test-Driven Development. Skriv testet INNAN du skriver koden. Cykel: Red → Green → Refactor.

## Red-Green-Refactor
TDD-cykeln: 1) Skriv ett misslyckat test (rött). 2) Skriv minsta möjliga kod för att få det att fungera (grönt). 3) Förbättra koden (refaktorera).

## Triangulering
TDD-teknik där du lägger till fler testfall för att driva fram en generell lösning istället för en specifik.

## Fake It ('Til You Make It)
TDD-strategi: returnera först ett hårdkodat värde för att få testet grönt, ersätt sedan med riktig implementation.

## Transformation Priority Premise
Prioritetsordning för att välja enklaste implementation: konstant → variabel → selection → loop → datastruktur.

## Kent Beck
Skaparen av TDD (och Extreme Programming). Hans bok 'Test-Driven Development: By Example' är standardverket.

## Baby Steps
TDD-princip: gör så små förändringar som möjligt mellan varje test. Enklare att felsöka.

## Test List
Lista över tester du planerar att skriva. Hjälper dig att fokusera och se progression.

## Mutation Testing
Teknik där man medvetet introducerar buggar (mutationer) för att kontrollera om testerna upptäcker dem.

## Outside-In TDD
Börja med tester på högre nivå (integrationstest) och arbeta dig nedåt. Kontrast till Inside-Out (rena enhetstester först).

