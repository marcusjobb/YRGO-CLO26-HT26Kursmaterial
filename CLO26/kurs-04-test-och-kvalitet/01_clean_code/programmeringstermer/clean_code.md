# 01 Clean Code — Programmeringstermer

## Clean Code
Kod som är lätt att läsa, förstå och underhålla. Namngivning, struktur och enkelhet prioriteras.

## Namngivning
Variabel-, metod- och klassnamn ska beskriva vad de gör. Undvik förkortningar. Exempel: `CalculateTotal()` inte `CalcTot()`.

## Single Responsibility
En klass/metod ska ha en och endast en anledning att ändras. Gör en sak och gör den bra.

## DRY
Don't Repeat Yourself. Undvik duplicering av kod. Använd metoder, loopar och arv för att återanvända.

## KISS
Keep It Simple, Stupid. Enklaste möjliga lösning är oftast bäst. Undvik onödig komplexitet.

## YAGNI
You Ain't Gonna Need It. Lägg inte till funktionalitet förrän du faktiskt behöver den.

## Magic Number
Hårdkodad siffra utan förklaring. Byt till namngiven konstant: `const int MaxRetries = 3;`.

## Kommentarer
Förklara VARFOR, inte VAD. Koden ska vara självdokumenterande. Ta bort utkommenterad kod.

## Coding Standards
Gemensamma regler för kodstil: indentering, namnkonventioner, filstruktur. Minskar friktion i team.

## Code Review
Systematisk granskning av kollegors kod för att hitta buggar och förbättra kvalitet.

