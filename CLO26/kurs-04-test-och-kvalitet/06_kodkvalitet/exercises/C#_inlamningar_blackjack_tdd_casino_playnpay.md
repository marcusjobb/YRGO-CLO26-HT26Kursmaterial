---

title: Casino Playnpay
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 3
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Inlämningar/Blackjack (TDD)/Casino PlayNPay.docx"
description: "Casinot ”Play’n’Pay” behöver ett backend för blackjack-spelmaskiner"
tags: ["casino", "csharp", "exercise", "git", "playnpay.docx", "test", "testing"]
week_fit: []
---
Casino Play’n’Pay!
Casinot ”Play’n’Pay” behöver ett backend för blackjack-spelmaskiner
Ni ska alltså skapa en backend för spelet
Den behöver inte någon fancy frontend, men det ska gå att spela via consolen med en enkel
textbaserad-gui
Alla publika metoder ska testas och test-cases ska dokumenteras
Test scenario
Starta spel
Beskrivning
Nollställ kortleken när spelet startar
Förberedelse Inget

Steg
1

Handling
Starta spel

2
3

Sätt namn på spelare
Blanda kort

Test steg
Expected

””
Kortlek

Test ID

Spelstart 1

Prio

Mellan

Actual

Resultat
Kortlek
initieras
”Namn”

”Namn”
Kortlek i
annan
ordning

Testare
Marcus

Exempel på hur man skriver Test cases : https://www.youtube.com/watch?v=0iPRgJWJRcs
Projektet behöver inte vara SOLID eller MVC men ska delas upp i klasser och mappar enligt funktion,
tänk på att spelet ska senare kopplas till ett MVC projekt som visar upp dess funktionalitet grafiskt. Detta
är dock ett annat projekt som ni inte ska göra.
Projektet ska ha en public klass kallad ”Blackjack” som ska innehålla publika metoder för att spela.
Man ska kunna ange antal spelare (1-7 + huset) och antal kortlekar (1-x)
Kort delas ut till spelarna och vinnaren kollas
För spelregler se:
https://www.johnslots.com/sv/blackjack/hur-spelar-du-blackjack/
(källa till bilderna på kommande sidor)

Källa: https://www.johnslots.com/sv/blackjack/hur-spelar-du-blackjack/

Källa: https://www.johnslots.com/sv/blackjack/hur-spelar-du-blackjack/

För Godkänt krävs












Att spelet kan blanda korten
Att spelet kan räkna kortens värde
Att spelet kan hålla koll på upp till 5 spelare i en runda
Att spelet reagerar om en spelare vinner
Att spelet reagerar om huset vinner
Att alla publika metoder testas
Att alla tester dokumenteras i ett excelblad
Att metoderna dokumenteras med vem som skrivit dem
Klasser och metoder ska vara delade enligt funktion
Clean Code
Välkommenterade metoder (XML kommentarer)

För VG krävs








Att alla godkända delar ska vara uppfyllda
Att det ska gå att spela spelet
Att man ska kunna satsa fiktiva pengar
Hålla koll på spelarnas kassa
Datorbaserade spelare eller hjälpare som kommer med förslag
o Hjälparen räknar inte kort
 ”Du borde nog stanna”
 ”Jag skulle nog chansa på ett kort till”
 osv
Även datorspelarna ska testas med TDD och dokumenteras

Även om det är en inlämning får detta göras i grupp
I projektinlämning ska det finnas dokumenterat vem som gjort vad. Även om gruppens projekt är väl
godkänt, så kan enskilda som gjort väldigt lite få G eller i värsta fall IG om de inte gjort något.
Alla medverkande ska programmera.
För enkelhetens skull ska alla medlemmarna i gruppen lämna in koden (även om det är samma zipfil
med kod och dokument). Zipfilen ska döpas enligt basgrup för att veta vilka som deltagit i vilket projekt.
