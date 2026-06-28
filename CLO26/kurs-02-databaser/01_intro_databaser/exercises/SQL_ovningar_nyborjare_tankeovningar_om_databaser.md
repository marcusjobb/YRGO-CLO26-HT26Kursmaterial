---

title: Tankeövningar Om Databaser
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 1
language: sql
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/SQL/Övningar/Nybörjare/Tankeövningar om databaser.docx"
description: "Av Marcus Medina, Codic Education"
tags: ["databaser", "databaser.docx", "exercise", "sql", "tankeövningar"]
week_fit: []
---
Tankeövningar om databaser

Av Marcus Medina, Codic Education

Här kommer några databasövningar du kan göra på egen hand eller i grupp. I slutet av detta dokument
finns facit.

Djursjukhuset
En veterinär ska ta över pappans djursjukhus och inser att pappan fört alla sina anteckningar på papper.
Nu måste den stackarn snabbt komma på ett sätt att få över informationen till en databas för att
snabbare kunna hitta information om sina klienter.
Pappans anteckningar ser ut ungefär såhär
Klient
Ras
Ägare
Bunne
Kanin
Elisa Johansson
Vovsing
Hund
Johan Woff
Klösofräs
Katt
Kattarina Meow
Osv. I mapparna hittar man följande information

Telefonnummer
070-992436
079-115533
072-223344

Mapp
EJ992
JW115
KW223

KW223
Datum
2006-02-13
2006-03-04
2006-07-21

Symptom

Inflammation, V
ben

Behandling
Kontroll
Kastrering
Skar upp blåsan,
rengjorde från
var, sydde ihop
blåsan.
Desinficerade.

Hur splittrar man bäst informationen till tabeller?

Anteckning
Nyadopterad katt, ej kastrerad, frisk
Kastrering av (mestadels) innekatt
Troligen är det grannens katts klo som
fastnat i benet och orsakat en
infektion. Bör desinfekteras och
plåstras om dagligen i en veckas tid.
Om var fortsätter att bildas, återkom.

Dagbok
Du ska göra en app som fungerar som dagbok. Till att börja med ska den bara innehålla det mest
nödvändiga
Id
Måste finnas

Datum
Måste finnas

Tid
Kan läggas
tillsammans
med datum

Titel
Bra för
sökning

Anteckning
Allt som
behöver
skrivas

Taggar
Nyckelord för
att förenkla
sökningen

Hur skulle du lagra informationen? Behöver den här tabellen delas upp?

Familjeträd
En nybliven fantast av Genealogi påbörjade ett familjeträd för att få en överblick på sin familj, än så
länge saknar han väldigt mycket information och bestämde sig därmed för att inte köpa en färdig tjänst
på nätet. Medan han samlar information tänker han göra en enkel databas. Hur kan du hjälpa denne.
Information som finns är






Tilltalsnamn
Namn
Efternamn
Efternamn innan
giftermål
Födelsedatum








Födelseort
Dödsdatum
Dödsort
Mor
Far
Syskon *





Makar *
Datum för giftermål
Ort för giftermål

Hur splittrar man upp detta på ett bra sätt? Observera att stjärnmarkerade fält innebär att det kan vara
flera som kopplas, exempelvis kan Syskon vara mer än en.

Facit
Djursjukhuset
Nästan all information är föränderlig, vilket gör att det blir meningslöst att skapa en massa tabeller även
om det är mycket information. Den information som kan upprepas är Djurras och mapp. Dessa kan
brytas ut ur tabellen. Resten kan ligga som det är.

Dagbok
Den klarar sig fint med bara en tabell, då ingen av informationen
kommer att upprepa sig.
Även om du sparar exempelvis väderförhållanden skulle inte det heller
kräva en annan tabell.
Taggarna kan kommasepareras och användas i sökningar som helt
vanlig text.
Vill man optimera det så kan man bryta ut taggarna, men det är ej
nödvändigt.

Familjeträd
Den här är lite klurig för den hänvisar till sig själv många gånger
Person hänvisar till sig själv på





MorId
FarId
Makar (kan vara flera)
Syskon (Kan vara flera)

Och den hänvisar till olika platser, dock samma tabell




Födelseort
Dödsort
Ort för giftermål

Det blir många LEFT JOIN och AS att använda här… Men det klarar sig i alla fall med bara två tabeller
Vill man köra överkurs kan man splittra ut Namn, Tilltalsnamn och Efternamn och få två tabeller till men
det är att ta i lite.
