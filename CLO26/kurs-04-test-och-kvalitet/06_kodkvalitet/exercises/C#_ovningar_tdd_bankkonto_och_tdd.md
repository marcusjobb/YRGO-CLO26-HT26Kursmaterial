---

title: Bankkonto Och Tdd
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/Bankkonto och TDD.docx"
description: "I dagens övning ska vi göra en klass som ska representera ett bankkonto"
tags: ["bankkonto", "csharp", "exercise", "tdd.docx", "test", "testing"]
week_fit: []
---
Bankkonto och TDD
I dagens övning ska vi göra en klass som ska representera ett bankkonto
Gruppens uppgift blir att se till att bankkontot skyddas mot eventuella problem eller logiska fel som kan
uppstå.

Kontroller
1.
2.
3.
4.
5.
6.

Kontrollera att insättning alltid är positiv (får inte göra insättning med minusvärden)
Kontrollera att uttag aldrig är mer än saldot
Om kontot har kredit, kontrollera att uttag aldrig får överskrida krediten
Om swish är kopplad kontrollera att uttagsgränsen (standard 3000/vecka) inte överskrids
Kontrollera att flytt från konto till ett annat konto aldrig är mer än vad som finns på kontot
Vid insättning betalas krediten först

Kontroller (avancerat)
1. Om insättning görs för mer än 15000:- ska kontot flaggas för misstanke om pengatvätt
2. Butikskortläsare kan ibland dröja med sina transaktioner, detta innebär att de kan ta ut pengar
även om kontot är på noll, för att det kan ha funnits pengar vid själva köpet. Detta gäller dock
om transaktionen är mer än en halv dag försenad.
3. Speciella sparkonton är låsta till max 5 uttag per år
4. Investeringskonton kostar 1% att ta ut pengar eller flytta till annat konto

Scenarios (avancerat)
1. Herr M.Ysko kommer till banken och gör en insättning på 14500:-, några timmar senare kommer
han tillbaka och gör en insättning på 12500:2. Pelle, 14 år, köper en begagnad elscooter av sin kompis för 2950:-, några timmar senare får han
för sig att köpa Gamerheadset för 890 av en kompis. I båda fallen swishar han och hans Swish
konto har standardgräns.
3. Gunnar Ambler har spelar bort månadens lön på online poker, det är helt OK tänker han för att
han och hans fru har ändå ett konto med 20000:- i kredit som han kan använda för månadens
utgifter. Nu visar det sig att han redan använt upp 15000:- tidigare månader. Han behöver nu
betala av en räkning på 4900:- Godkänner banken betalningen?
4. Gunnars frun inser vad maken ställt till med och försöker nu rädda familjens ekonomi genom att
överföra 25000 från sitt investeringskonto (hon har 50000:- ) på kontot. Kan hon göra den
överföringen? Hur mycket finns på familjens kreditkonto nu? Och hur mycket finns på
investeringskontot?
Vilka andra scenarios kan ni komma på? Diskutera mera!
