HUSHÅLLSEKONOMI
KVALITETSSÄKRAT

Codic Education AB | marcus.medina@codic.se

GRUPPARBETE| 1

SAMMANFATTNING
Som grupparbete ska ni skapa backend till ett program som ska hjälpa till att
planera ekonomin beroende på den ekonomin vi har. Det ska bestå av
metoder som ska hjälpa användaren att planera sin ekonomi för månaden.
Ni ska arbeta i grupp och ha git som bas för koden.
Projektet ska kvalitetssäkras med Nunit, MSTestV2 eller Specflow.
Den ska ta emot värden som
 Elräkning 300: Hyra 5732: Netflix 89: Spotify 99: Mobilabonnemang 99: Bredband 199: Gym 450: Mat och förbrukningsvaror (schampo, tandkräm, etc.) 1000: Lön 14500: Sparkonto 10% ( = 10% av det som är kvar efter räkningar)
 Kläder 15%
Och då ska den lagra i en property hur mycket man har kvar att handla pizza
eller något annat mums för.

GRUPPARBETE| 2

INLEDNING
FUNKTIONALITET
Genom att ange värden för inkomster och förväntade utgifter för en vanlig
månad lägger vi alltså en budget, och vill då såklart få veta hur mycket
pengar vi förväntar oss att ha över att spendera på annat.
Det är enbart tester som ska generera någon slags output, så inga
Console.WriteLine(), felmeddelanden kan loggas i en textfil.

ANVÄNDARGRÄNSSNITT
Användargränssnittet kan implemmenteras av studenten/studenterna
efter avslutat projekt. Klassen ska testas genom att man skickar in listor till
kalkylatorn och sedan kontrollerar man hur mycket cash som finns kvar.
Dock är det OK att skapa ett consolprojekt med referens till classprojektet
för att lättare visualisera funktionaliteten.

KVALITETSSÄKRING
Tester ska utföras genom unit testing, antingen via nUnit, Xunit, MSTestV2
eller SpecFlow. Alla publika metoder ska testas. Testerna ska
dokumenteras som i första inlämningen, i ett excelblad.

GRUPPARBETE| 3

GRUPPARBETE
Här kommer förslag på hur ni kan planera arbetet i gruppen på ett optimalt sätt.
Den här avdelningen är en mix av lärdomar från projektarbete som konsult och
lärdomar från PPU (genom Hannah).
Genomgång
av gruppens
roller

Genomgång
av uppgiften

Grupp
uppstart

Kontroller
a

Koda

Planering

Feedback
till
varandra i
gruppen

Koda mer

(ex.
KanBan)

Reflektion
av egen
prestation

Inlämning

GENOMGÅNG AV UPPGIFTEN
Samlas i grupp och gå igenom hela projektbeskrivningen så att ni alla är överens
om vad som ska göras.

GRUPPUPPSTART
Vad kan ni komma överens om som viktiga element i ert kommande samarbete
som ni vill och behöver hantera/diskutera före, under och efter ert samarbete?
Vad behöver ni hantera redan idag? Gruppkontrakt? Korta dagliga
avstämningar? Vilken tid varje dag? Github konto? Var? Vem skapar det?
Ansvar? Milstolpar (tidslinje)? Samarbetsformer?
Använd varandras styrkor och ta hänsyn till varandras behov.

ROLLER
Vem ska leda gruppen? Ska gruppen ha en ledare? Vem kallar till mötena?
Vem ska hantera GitHub?
Vem gör vad?
Ska utbildare/handledare kallas in till möten för att agera moderator? (typ som
en Scrum master)

GRUPPARBETE| 4

TIDSLINJE
Fredag

Måndag

Torsdag

Tisdag

Onsdag

Onsdag

Tisdag

Torsdag

Måndag

Fredag

I alla projekt är det viktigt och essentiellt att utveckla en tidslinje. Det är
användbart när det gäller att organisera det arbete som ligger framför oss, men
det är också bra för att hålla alla individer och parter (i detta fall
gruppmedlemmar)) ansvariga för att slutföra arbetet när det behöver göras.
Alltför ofta klagar individer på att deras medarbetare/gruppmedlemmar inte gör
jobbet i rätt tid. Ibland beklagar dessa individer att de till följd av detta tar på sig
någon annans arbete och förbittrar si g över det påstådda ”samarbetet”.
Tidslinjetips
a) Börja med att skapa ett större mål för samarbetet och utveckla sedan
några mål som kan uppnås under mindre tidsperioder (dagar då ni har
bara två veckor).
b) Arbeta bakåt. Tilldela en deadline för det större syftet. Med det här
datumet i åtanke, tänk på de mindre målen och vad som behöver uppnås
för att de mindre prestationerna ska leda till övergripande framgång för
samarbetet. Tilldela datum för dessa mindre mål, och arbeta igen bakåt.
c) Diskutera som grupp vem som ansvarar för de specifika arbetena, vilka
leder till att de mindre målen uppnås. Arbetet kan delas upp på vilket
sätt som helst som är lämpligast för det givna samarbetet (detta kan till
exempel bero på avståndet mellan medarbetare).
d) Med dessa tidsfrister i åtanke, tilldela mötes- och incheckningstider. På
grund av den korta tiden ni har så är dagliga Scrum-aktiga standups att
rekommendera. Det här dagliga incheckningsmötet kan bara ta femton
minuter, men att veta att mötet kommer att äga rum är ett bra sätt att se
till att alla håller på med uppgiften. e. Utse en "uppgiftsmästare", någon
som kommer att hålla reda på tidslinjen och framstegen som görs. Om
någon är ansvarig för att hålla tidslinjen är det troligt att individer är mer
benägna att hålla sig på uppgiften

PLANERING – KANBAN
Kanban är ett enkelt sätt att planera. Trello (https://trello.com/) är ett bra och
gratis verktyg för planering med KanBan.
Man skriver en lista på allt man ska göra (backlog), allt man tänkt göra och allt
man kommer på i efterhand som man bör göra.
Från den listan kan vem som helst plocka ut ett kort och placera i ”Doing” vilket
betyder att den personen arbetar på det specifika kortet. När det är klart flyttar
man det till Test eller Done.
På så sätt vet alla vad som är på gång, vad som behövs göras och vem som gör
vad. Man vet även vem som gjort vad.
Ni behöver inte använda detta Trello eller KanBan, det är bara ett förslag.

KODNING
Nu ska agera enligt den planering ni har skrivit och kommit överens om.
Alla ska vara delaktiga i den här processen. Ni ska hjälpas åt med att definiera
tester, skapa koden, refaktorera, clean code kolla och säkra upp koden.

GRUPPARBETE | 5

REFLEKTION
När ni är klara för inlämning ska ni skriva reflektioner från era lärdomar av
grupparbetet.
Reflektion

Gruppen i sin helhet
Du själv
Gruppmedlemmarna

Vad kan man lära sig av projektet? Vad kunde ha gjorts bättre? Vad kunde du ha
gjort bättre.
Det finns många frågor och tankar som man kan ta i beaktning, men i
slutsumman är den viktigaste frågan ens egen roll i hela händelseförloppet.
Hur ger man feedback till andra, ska man vara snäll eller brutalt ärlig? Finns det
en väg mellan de två ytterligheterna? Ska feedbacken stärka, krossa, förbättra
eller bygga upp? Vilken sorts feedback vill du ha?

INLÄMNING
Alla ska lämna in till Classrooms.
Zip-filen ska innehålla följande
 Källkod till program
 Källkod till tester
 Lista på gruppens medlemmar
 Reflektion av eget arbete
 Reflektion av gruppen
 Feedback från gruppen
 Excel fil med dokumentation av tester

GRUPPARBETE | 6

KODBESKRIVNING
Extra pengar
Som student i karantäntider
behöver inte köpa busskort
och busskort kostar 875:- månaden
då sparar jag 875:- varje månad

Sparande
Som student med inkomst på 10500
Och räkningar på sammanlagt 7328
Och 15% sparar för att köpa en ny
dator. Detta innebär att varje månad
sparar jag 1099:-

HUVUDKLASSEN


Ni ska skapa en klass för beräkning av budget



Den ska ta emot en lista med alla inkomster



Den ska ta emot en lista med alla utgifter



Den ska ta emot en lista med alla beräknade utgifter
(där anger man inte värde utan procent som ska dras)



Den ska räkna ut summan av inkomst



Den ska räkna ut summan av utgifter



Den ska räkna ut summan av beräknade utgifter



Den ska tala om hur mycket cash man har över



Eventuella fel loggas till en textfil

Inkomster, utgifter och beräknade utgifter kan skickas till metoden i samma
lista, det behöver inte vara tre listor. Val och struktur på DTOs för att lagra
information om inkomster, utgifter och utgifter som ska beräknas, är upp till
programmeringsgruppen att designa.
(DTO = Data Transfer Object, klass som är enklare än en POCO, bara
properties och ingen logik)

Som student med inkomst på 10500
Och många räkningar
Vill jag kunna utveckla vidare
projektet till att hantera flera
månader och jämföra mot faktiska
resultat vid månadens slut.

BUDGET EXEMPEL
Post
Inkomst
Lön
Utgift
Hyra
Netflix
Mobilabonnemang
Bredband
Mat
Förbrukningsvaror
Bankavgift
PensionsSpar
Gym
Hemförsäkring/mån
Kalkylerade utgifter
Spara (10%)
Oanade utgifter (25%)
Cash kvar:

Inkomst

Utgift

Utgift %

Kvar

14,500
8900
89
99
199
1200
600
45
1000
350
75
10%
25%
1311,52

GRUPPARBETE | 7

KRAVSPEC
Cash över
Som student med inkomst på 10500
Och räkningar på sammanlagt 7328
Och 10% sparar för framtida bruk
Och 15% sparar för oplanerade utgifter
Efter att ha betalat allt
Har jag 2426 kvar

Sparande
Som student med inkomst på 10500
Och räkningar på sammanlagt 7328
Och jag sparar 10% för framtida bruk
Mitt sparande är alltså 312,20:- i
månaden

Skapa den ovan beskrivna applikationen som en lösning i Visual Studio, välj
projekttyp: Class Library (.Net Core).
Klasserna och funktionerna som beskrivs här ovanför ska alltså
implementeras som ett class library.
För att visa att funktionerna ger förväntat resultat (se
funktionsspecifikationen ovan) ska ni skriva tester för samtliga funktioner.
Testerna ska göras i ett eget projekt som är kopplad till samma solution
som huvudprojektet. Det är även OK att göra ännu ett projekt, en
consolapplikation som får agera som visuell presentation av class libraryn,
det dock inget krav.
När det gäller månadssparande är det upp till gruppen att bestämma om
sparande procent ska dras av inkomstsumman eller av inkomst minus
utgifter. Man kan exempelvis skapa en lista med bara sparande som
innehåller ”beräknade utgifter” typ ”Månadssparande 10%” och sen räkna
på det antingen från lönen eller från resultatet av in inkomst minus utgifter.
Om en uppgift överstiger summan pengar kvar ska utgiften inte dras av.

INLÄMNING
Pizza
Som student har jag 2426:- kvar efter
räkningar.
Och en pizza kostar 95:Då kan jag köpa 25 pizzor på en månad.

Projektet ska lagras på Github medan ni utvecklar det, men när den är klar
för inlämning ska den zippas och skickas in till Google Classroom.
I ZIP-filen ska det finnas källkod, Excel dokumentation och dokument som
berättar vilka som är med i gruppen.
Alla i gruppen ska lämna in samma ZIP-fil – för att det blir krångligt med
betygsättning annars. I samband med inlämning av ZIP-filen ska även ett
dokument med reflektion över det egna arbetet (PPU stil) skickas in.

GRUPPARBETE | 8

FÖR GODKÄNT










Programmet ska kunna räkna ut månadens budget
o Cash = Inkomst-utgifter-sparande-beräknade utgifter
Koden delas upp i två projekt
o Koden
o Tester
Alla publika metoder i ska testas
(DTOs behöver inte testas)
Alla tester ska gå igenom (grön, pass)
Dokumentation av testerna ska göras i Excel
Alla i gruppen ska dokumentera minst ett test (det ska stå på testet
vem som dokumenterat vad)
Alla klasser, metoder och properties ska XML kommenteras
Egen reflektion och utvärdering av sin insats i projektet (i PPU stil)
o Egen prestation
o Gruppens prestation
o Feedback till gruppen

FÖR VÄL GODKÄNT











Alla punkter på Godkänt-nivån ska vara OK
En publik List<string> ska ta emot alla felmeddelanden, den ska kunna
sparas i en textfil (vanlig text) genom en metod som kan anropas.
Den sammanlagda summan av procentdelarna får inte överstiga 100%,
detta error ska loggas i listan för felrapporter. Då ska inte beräknade
utgifter räknas med i avdragen.
Om pengarna tar slut medan räkningarna räknas ut, då ska räkningen
som övertrasserar inte dras av, detta ska loggas i listan för felrapporter.
Om pengarna tar slut medan kalkylerade räkningar räknas ut, då ska
den kalkylerade räkningen som övertrasserar inte dras av, detta ska
loggas i listan för felrapporter.
Programmet ska generera en rapport av budgeten i en textfil som
sparas på skrivbordet hos den inloggade användaren
Programmet ska skyddas mot felaktiga inmatningar (exempelvis
minusinkomst) och detta ska bevisas i testerna
Null errors får inte förekomma

EXTRA UTMANING


Gör så att programmet jämför två budget och genererar en rapport, för
att kunna se vad som inte gick som beräknat den specifika månaden.

GRUPPARBETE | 9

TIPS OCH TRICKS
1.
2.
3.
4.
5.
6.
7.
8.

”Det
svåraste
med en
uppgift är
att komma
igång”
– Brian Tracy

Tänk igenom vad som behövs och vad som ska göras
Gör en todo-lista av allt som ska göras
Dela ut uppgifter i gruppen
Sätt upp mål (”på onsdag ska alla dessa punkter vara klara”)
Skapa repository på GitHub
Bjud in alla i gruppen
Keep it simple, gör inte mer än vad kravspec kräver
När projektet är inlämnar är det upp till var och en om ni vill utveckla det
vidare.

KODTIPS
1. Definiera vad som ska testas först, så att koden anpassa till testerna
2. Fokusera främst på testerna, precis som med geometrin är koden inte så
stor del av projektet.
3. Skapa DTOs för all data som ska hanteras
4. Vill du testa en teori, använd en ny branch till det
– om det fungerar pusha, annars radera branchen
5. Använd Linq
6. Gör de svåraste bitarna först
7. Kommentera metoderna så fort de är klara
8. Kommentera klasser och properties när de skapas
9. Använd mappstruktur för att dela upp klasser

GRUPPARBETE | 10
