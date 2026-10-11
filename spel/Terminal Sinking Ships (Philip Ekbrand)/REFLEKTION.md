# Reflektion

**Namn:**Philip Ekbrand  
**Kurs:**kurs-01-grundlaggande-oop
**Uppgift:** Havsforskarna 
**Datum:**  2026-10-04

---

> Den här reflektionen är obligatorisk och lämnas in individuellt — även om uppgiften gjordes i grupp.  
> Det finns inga rätta eller fel svar. Skriv vad du faktiskt tyckte och upplevde.

---

## Vad var svårast att lösa?

*Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*

Fortfarande är det lite jobbigt med klasser, men det börjat flyta på bättre och bättre.
Jag lät chatgpt göra själva funktionen "BuildBoard(bool myView)", gjorde den i vanlig konsol först men sen la jag till "using Spectre.Console;", kände att vi tillfället var det inte dag att för djupa sig utan ville bara ha en snyggare spelplan. i min egna lösning fungerade men raderna skönk ihop, och kännde att det var onödigt att lägga tid på det just där och då!.

"BuildBoard(bool myView)" i klassen board skapar upp ett table.

först så skapar ramen dvs column 1 till 10 och sen för varje rad så skriver vi ut vilken bokstav raden har.
detta görs ju i loop med en loop i, vi använder detta för att kika i arrayen, vilka rutor är toma, vilka har blivit träffade, vart finns skeppen?
efter som värden i arrayen in är samma om vi faktiskt visar så använder vi en if sats för att skriva ut i tabel vad vi faktiskt vill visas.
Vi tar även med oss en parameter in i methoden för att veta om vi skall visa skeppen i tablet.
Våran ""Spectre.Console;"" tolkar även stil på elementen så t ex X blir röd eller att columner visas som bold.


Vi kallar på methoden i både placment och startgame, för varje tur så behöver vi kalla på methoden så vi kan får en
uppdaterad spelplan.

Eftersom vi vill skriva ut både tabels i consolen så skapar vi en var som innehåller båda tabel, så räcker det att vi ber 
AnsiConsole.Write() skriver ut vår var.
((AnsiConsole.Write(boards);))

Sen fick man ju tänk lite gällande placeringen av skepp, man får testa alla codinater ett skepp skulle ha innan man placerade ut det, men gick relativt smidigt.

> 

---

## Hur fungerade samarbetet i gruppen?

*Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet?*  
*(Individuell uppgift? Hoppa över den här.)*

> 

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

hmm, denna uppgiften gick väldigt smärtfritt, har fastnar fortfarande mycket i små syntax errors. Jag ställde mycket frågor till ai i början av projektet, men nu ser man det lättare själv, och kan använda sig av felmeddelanden från terminalen! testar också av mer, dvs starta programet när man har gjort mindre förändingar så slipper man felsöka så mycket senare.

Återigen så borde jag målupp vägen framåt innan jag startar. hade en bild i huvudet om vad jag skulle göra när jag satte igång, men tappade mycket under resans gång, hade nog kunna effektivisera min process om bilden var glasklar innan. Hade tänkt muiliplay och förbereda för det, men tappade det under resans gång. (gjorde det lite mer dynamiskt senare men men.)

Borde även ha läst igenom uppgiften lite mer noggrant innan jag startade men blev väldigt taggad på att sätta igång direkt!

> 
