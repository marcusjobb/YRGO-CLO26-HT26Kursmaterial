---

title: Inlämningsuppgift1 (uppdaterad)
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Inlämningar/Dörrlogg (SQL)/Inlämningsuppgift1 (uppdaterad).docx"
description: "Moderna bostäder för moderna människor"
tags: ["(uppdaterad).docx", "conditions", "csharp", "exercise", "inlämningsuppgift1", "oop", "sql", "test"]
week_fit: []
---
Husrum
Fastigheter AB

27 JUNI
NET20, Inlämningsuppgift 1
Författare: Marcus Medina

1

Husrum Fastigheter AB
Moderna bostäder för moderna människor
OM FÖRETAGET
HFAB specialiserar sig på att göra om gamla hyresrätter till trygga och moderna hem,
men bevarar den gamla touchen på gamla mysiga hem. Kunderna är främst mindre
familjer (max 3 barn) men det är även äldre par och ensamstående. De ensamstående
bor mest i enrumslägenheter.
Husen har moderniserats och sanerats från gamla tiders inte-så-hälsosamt material
som användes, väggarna har ljudisolerats så att man knappt kan höra grannarna.

”Vi vill ge våra hyresgäster trygghet och ro”

För att försäkra oss om våra hyresgästers trygghet så har vi ordnat så att alla dörrar
alltid är låsta. Speciellt dörrarna ut mot gatan kontrolleras mest. För att detta inte ska
vara ett problem för våra kunder har vi försett dem med taggar som de får trycka mot
en avläsare för att komma in eller ut ur huset. Även om det innebär lite krångel med att
plocka fram dem varenda gång så uppskattar våra kunder den trygghet det innebär.
Dörrar till tvättstuga, soprum och andra allmänna utrymmen som cykelparkering
öppnas också med taggar, och dörrarna stängs automatiskt.
Nästan allt är automatiserad i huset, hyran dras med autogiro, lampor tänds när någon
rör sig i trapphuset och släcks efter några minuter när ingen rör sig.
I det stora hela är allt som rör offentliga utrymmen är automatiserat, utom städningen
som skötts av ett företag vi anställer för detta.

2

OM HUSET
Huset som vi håller på att automatisera är redan renoverad, moderniserad och nu ska vi
automatisera allt.
Huset har tre våningar, i var och en av våningarna bor det två familjer, det är tre
rumslägenheter. I bottenvåningen finns två tvårumslägenheter och en etta.
Lägenheterna har fått ID-nummer enligt våning, position, väldigt enkelt
0101 står för bottenvåningen, första lägenheten,
0201 är första våningen första lägenheten (till vänster),
0202 är första våningen andra lägenheten (till höger).
Osv
Vaktmästaren kan gå in i alla lägenheter (med tillstånd) och även in i Vaktmästarens
rum, det är bara han som får gå in i det rummet.
Här kommer en förteckning av lägenheter, gäster och taggar
Lägenhetsnummer Person
Tagg
0101
Liam Jönsson
0101A
0102
Elias Petterson
0102A
0102
Wilma Johansson
0102B
0103
Alicia Sanchez
0103A
0103
Aaron Sanchez
0103B
0201
Olivia Erlander
0201A
0201
William Erlander
0201B
0201
Alexander Erlander
0201C
0201
Astrid Erlander
0201D
0202
Lucas Adolfsson
0202A
0202
Ebba Adolfsson
0202B
0202
Lilly Adolfsson
0202C
0301
Ella Ahlström
0301A
0301
Alma Alfredsson
0301B
0301
Elsa Ahlström
0301C
0301
Maja Ahlström
0301D
0302
Noah Almgren
0302A
0302
Adam Andersen
0302B
0302
Kattis Backman
0302C
0302
Oscar Chen
0302D
VAKT
Vaktmästare
VAKT01
3

Här är lite information om gästerna
•

•

•

•

•

•

•

Liam är en ung studerande kille som spenderar dagarna mest med att plugga,
relaxar på puben nån gång då och då (kommer hem vid 10 tiden när puben stängt
pga Covid-19).
Elias och Wilma är ett äldre par som gärna spenderar kvällarna i lugn och ro och
tittar på dokumentärer och drama på TV. Tyvärr är de båda äldre och kan ibland
ha TV på lite för högt ljud.
Alicia och Aaron är ett sydamerikanskt par som levt i Sverige sen 1970-talet. De
festar gärna med sina grannar men är också väldigt noga med att respektera alla
regler.
Olivia och William har två barn, Alexander och Astrid. Alexander går på andra året
i gymnasiet och Astrid är på sitt sista år i högstadiet. Föräldrarna arbetar heltid
och är hemma på kvällarna, barnen har ett rikt liv och många vänner.
Lucas och Ebba är ett ungt par som nyligen blivit stolta föräldrar till söta lilla Lilly.
Lucas arbetar som konsult, men arbetar mest hemifrån. Ebba arbetar som lärare
men har föräldraledigt.
Ella och Alma fann varandra efter Ellas skilsmässa, hon har två döttrar och Alma
har nu blivit deras extra mamma. Ella arbetar som fastighetsmäklare och Alma
arbetar som headhunter för konsulter inom IT. Då barnen är i tonåren klarar de
sig själva när Ella och Alma går ut på restaurang eller på teatern.
Noah, Adam, Kattis och Oscar är ett gäng pigga studenter som delar på en
lägenhet. De studerar med olika inriktningar. Även om alla har sin
bekantskapskrets separerad från gruppen så håller de ändå bra ihop tillsammans.
De har ofta besök av sina vänner.

4

För enkelhetens skull har alla dörrar fått ett ID också
Dörrbenämning
Förklaring
LGH(Lägenhetsnummer)
Dörr till lägenhet, exempelvis LGH0101
BLK(Lägenhetsnummer)
Dörr till altan/balkong exempelvis BLK0101
UT
Dörr ut mot gatan
SOPRUM
Dörr mot soprummet
TVÄTT
Dörr mot tvättstugan
VAKT
Dörr mot vaktmästarens rum

Lista på koder till dörrar som ska lagras (fler kan tillkomma)
KOD
Förklaring
DÖUT
Dörr har öppnats utifrån
DÖIN
Dörr har öppnats inifrån
FDIN
”Fel dörr” - Gäst har försökt öppna en
dörr utan tillstånd (exempelvis fel
lägenhet)
FDUT
”Fel dörr” – Person har försökt gå ut från
en dörr där taggen inte tillåter (borde
aldrig kunna ske)

5

UPPDRAGET
Uppdatering av uppdraget
 Metoderna som ska anropas i classen får vara statiska och i så fall anropas direkt
via classNamnet DoorEventsLog.Method() istället för att instansieras som i
exemplet på main med var events=new DoorEventsLog();
Båda alternativen fungerar.
 ListTenantsAt() ska returnera en DataTable (det hade inte angivits innnan)
 Bakgrundsinformation om Hyresgästerna ifall någon undrar vilka de är
Uppdragets grund
Det har förekommit en del klagomål då vissa grannar har slarvat med reglerna för
boendet, i nuläget har vi inte satt upp kameror och annan bevakning så vi kan inte veta
vem som bryter mot reglerna. De problem som rapporterats är följande.
 Personer som inte respekterar bokade tvättider
 Personer som slängt skräp i soprummet (exempelvis möbler) som borde ha körts
till återvinningen
 Grannar som kommit hem sent efter en pubkväll och varit högljudda, vilket har
stört andra grannar. (Klockan 10 ska det vara tyst enligt överenskommelsen)
Vi är måna om våra gästers privatliv och självklart önskar vi att de ska ha trevliga
pubkvällar, men då detta har fortsatt att upprepas trots uppmaningar till hyresgästerna
att visa hänsyn kommer vi nu att införa loggning av dörrarna.
Uppdraget är enligt följande.
Ni ska skapa ett program med databas för att logga vilka dörrar som öppnats av vem,
och vid vilken tid. Programmet kräver inget speciellt användargränssnitt för det kommer
att omvandlas till en API när vi sett att det fungerar, men det är inget ni behöver tänka
på just nu.
Till att börja med räcker det med en consolapplikation, dock ska all hanterande av
information ske via en class med publika metoder, detta för att förenkla övergången till
en API i framtida utveckling.
Även om det inte ska finnas användargränssnitt ska finnas en debug på consolfönstret.
6

7

Här nedan följer information om vad HFAB förväntar sig
Databasen ska lagra
 Hyresgästernas namn (för snyggare rapporter)
 Hyresgästens lägenhet
 Hyresgästernas taggar
 Hyresgästernas öppnande av dörrar
 Dörrarnas benämningar
 Tid för händelse
 Kod för händelse
Programmet ska kunna logga
 När en dörr öppnas
 Vilken tid det öppnats
 Vilken dörr som öppnats
 Om gästen har tillstånd att öppna dörren
 Av vem dörren öppnats
 Om det öppnats inifrån eller utifrån
Programmet ska kunna visa upp
 Sökning på en hyresgäst ska visa vilka dörrar som öppnats och när
 Sökning på en dörr ska visa vem som öppnat dörren
 Sökning på felrapporter ska visa om någon försökt öppna en dörr de inte har
tillgång till, exempelvis en gäst försökt gå in i vaktmästarens rum eller försökt
öppna grannens dörr
 Sökning på lägenhet ska visa vem som öppnat dörren och vid vilken tid
 Datumsökning ska visa alla dörrar som öppnats och av vem vid en viss dag
 Sökning på dörrkod ska visa senaste 20 rapporterade öppningar med den koden
 Sökning på tagg som ska visa de senaste 20 rapporterade öppningar
 Visa en lista på hyresgäster i en lägenhet

8

Följande metoder ska finnas publika i classen.
OBS att när det gäller loggning så behandlas Vaktmästaren som en hyresgäst och
allmänna utrymmen som tvättstuga behandlas som en lägenhet.
Classen ska heta DoorEventsLog.cs
Metod
FindEntriesByDoor

Förklaring
Sök de senaste loggar från en viss dörr, returnera en
DataTable
FindEntriesByDoor("LGH0302")
FindEntriesByEvent
Sök de senaste loggar med given kod, returnera en DataTable
FindEntriesByEvent("DÖIN")
FindEntriesByLocation Sök de senaste loggar från en viss lägenhet/rum, returnera
en DataTable
FindEntriesByLocation("TVÄTT")
FindEntriesByTag
Sök de senaste loggar från en viss tagg, returnera en
DataTable
FindEntriesByTag("0102A")
FindEntriesByTenant
Sök de senaste loggar från en viss hyresgäst, returnera en
DataTable
FindEntriesByTenant("Alexander Erlander")
eller
FindEntriesByTenant(”Erlander”,"Alexander")
ListTenantsAt
Söker hyresgäster från specifik lägenhet, returnera en
DataTable med deras namn och tagg-kod
ListTenantsAt("0201")
LogEntry
Informationen skickas in i textform och sparas i lämplig form
och i lämpliga tabeller i databasen, returnerar inget
Ex
LogEntry(”2020-10-23 10:23”, ”LGH0301”, ”DÖIN”,”0301A”)
Taggen används för att identifiera hyresgästen
Alla dessa metoder (utom ListTenantsAt) ska returnera
Datum, tid, Dörrkod, Tagkod, Händelsekod, Lägenhetskod, Tagkod, hyresgästens namn
Egenskaper
MaxEntries

Typ
Int

Förklaring
Maxantal poster som ska returneras vid en sökning, standard
är 20
9

Nyckelord som ska användas vid namngivning av variabler, tabeller och metoder
Nyckelord
Förklaring
Door
Dörr (inklusive dörr ut ur byggnaden)
Event
Händelse, exempelvis dörr öppnades (DÖIN eller DÖUT)
Location
Lägenhet eller rum (tvättstuga, soprum mm)
Tag
Kod till Taggen som används för att låsa upp dörrar
Tenant
Hyresgäst

Under första fasen av projektet är det önskvärt att en programmet själv visar hur
DoorEventLog Classen fungerar. Exempelvis output till Consolen vid körning av
projektet. Dock krävs inte att användaren själv ska mata in loggar, dessa kan skapas vid
initiering av databasen.
Output av loggen ska visa
Datum och tid, Dörrkod, händelsekod, Personens tagg, Personens namn och
textbeskrivning
2020-10-23 10:23, LGH0301, DÖIN, 0301A, Ella Ahlström Öppnade dörr till lägenhet 0301 inifrån
2010-10-23 12:32, TVÄTT, DÖUT, 0302C, Kattis Backman, Öppnade dörr till tvättstugan utifrån

OSV

10

Krav
För att projektet ska bli godkänd krävs
 Att projektet vid behov skapar databas, tabeller och testdata (detta får göras i
main men helst bara i form av ett anrop till en class som gör jobbet)
 Att databasen är uppdelad i tabeller (om det behövs) med en logisk koppling
mellan dem.
 Att Classen kan returnera den data som frågas om vid anropet
 Att mainclassen kan skriva ut resultatet från Classens funktioner
 Att parametrar används för sökningar och inmatning
 Att koden är kommenterad
 Att main-koden är minimal och att den enbart ska använda DoorEventLogs
classen (såvida inte det finns en class som genererar output till)
För att projektet ska bli väl godkänd krävs
 Att all output begränsas till de senaste 20 (eller vad som anges i Classegenskapen
MaxEntries) och ska visas i omvänd kronologisk ordning (senaste händelse ska
komma först)
 Att koden är uppdelad i flera Classer för hantering av exempelvis olika tabeller
och med ett klart och logiskt syfte, och att DoorEventLogs anropar dessa classer
för att få fram de nödvändiga resultaten.
Val av databas är fritt, SQLite, SQL-Server eller Mysql. Använd den som känns bäst
Koden ska vara kommenterad så att metoder och speciallösningar visar tydligt vad som
är tanken bakom, detta för att bättre kunna bedöma kunskap och förståelse av C# och
SQL.

11

Exempel på Main

Alternativt en statisk lösning

Det är allt main behöver göra, hämta de sökningar som angetts och sedan anropa en
metod som skriver ut allt. Observera att i main exemplet så är metoderna inte Statiska,
detta är inget krav dock, metoderna kan vara statiska.

12

I detta skede behövs inget grafiskt gränssnitt, det räcker med att skriva ut resultatet på
skärmen, exempelvis såhär

13

Exempel av Logg
I nuläget har vaktmästaren fått agera ”logg” när han varit på plats, absolut inte för
att spionera på grannarna, men för att få en bra överblick av hur det är. Den här
listan är enbart informativ, men den kan hjälpa till vid skapandet av programmet.
Loggen innehåller även rapporter från hyresgästerna. Detta är vad som hände den
23 okt 2020. Den här listan ska inte matas in i databasen, den ska bara fungera som
observationsmaterial för att kontrollera att loggarna är OK.
Tid
10:07
10:08
10:19
10:19
10:20
10:20
10:21
10:22
10:55
10:56
11:03
11:04

Händelse
William Erlander öppnade lägenhet 0201 inifrån
William Erlander in i lägenhet 0201
Noah Almgren öppnade lägenhet 0302 inifrån
Olivia Erlander öppnade lägenhet 0201 inifrån
Noah Almgren öppnade soprummet utifrån
Olivia Erlander öppnade dörren till byggnaden inifrån
Noah Almgren öppnade soprummet inifrån
Noah Almgren in i lägenhet 0302
Lucas Adolfsson öppnade lägenhet 0202 inifrån
Lucas Adolfsson in i lägenhet 0202
Maja Ahlström öppnade lägenhet 0301 inifrån
Maja Ahlström in i lägenhet 0301

14
