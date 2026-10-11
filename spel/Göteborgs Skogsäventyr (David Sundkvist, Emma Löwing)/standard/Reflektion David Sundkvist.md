# Reflektion — Slutprojekt

**Namn:**  David Sundkvist
**Kurs:** Grundläggande OOP i C#  
**Projekt:** *(Skogsäventyret/GoteborgsSkogsAventyr)*  
**Datum:**  2026-10-02

---

> Den här reflektionen är obligatorisk och lämnas in **individuellt** — även om projektet gjordes i grupp.  
> Det finns inga rätta eller fel svar. Skriv vad du faktiskt tyckte och upplevde.  
> Kort och ärligt räcker — några meningar per fråga.

---

## Vad var svårast att lösa?

*Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*

> Det var många saker som var svåra att lösa, till exempel var det svårt att navigera i de olika klasserna för att få tag på en metod eller liknande som kopplades till andra klasser.
> polymorfism var svårt att få grepp om ibörjan med override och virtual. Det skedde också många gånger att jag skrev koden på fel ställe eller bara att den inte var tillräckligt bra i alla situvationer.
> 

---

## Hur fungerade samarbetet i gruppen?

*Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet? Hur använde ni Git tillsammans (branches, merge, pull requests)?*

> Det som fungerade bra var att båda hade ganska liknade arbetes rutin och vi hade kul tillsammans! sen var det själv klart svårt att få jobbet så effektivt som möjligt, vilket kan bero på att vi aldrig har jobbat på detta sätt förut och inte har något profffesionell arbets rutin. Vilket hade varit kul att lära sig framöver.
> vi bara delade upp classerna 50/50 med hjälp av Chatgpt som fick hjälpa oss fixa en detaljerad plan på vad som behövdes göras sen över lappade våra klasser mer än väntat så vi skrev ändå en hel del i varderas klasser vilket känns ganska rimligt. vi pull och pushade som fan och det blev 2 lite jobbiga merg conflicts som vi löste på udda sätt första gången var det lite risky men det funkade då vi gjorde en säkerhets kopia sen raderade vi en hel klass och klistrade in den gamla igen och då funkade det. andra gången löste vi det mer korrekt när markus hjälpte mig för det var typ samma problem då det inte gick att push pulla. Det löste sig genom att välja min nya kod en acceptera merg och sen något mer som jag inte kommer ihåg.

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

> Vi började ibland bygga olika delar innan vi helt hade bestämt hur de skulle kopplas ihop, vilket gjorde att vissa saker behövde ändras senare. Jag hade också testat och committat mindre delar oftare för att minska risken för merge conflicts. Vi gjorde också vår egena flödeschema och class planering själva för hand och jag känner att det är ett sådant bra tillfälle att ta mer hjälp av AI för att strukturerar speciellt eftersom det är vår första gång vi gör ett spel utan mall!  

---

## Valfritt — arv och datastrukturer

*Om ni använde arv: varför valde ni den strukturen? Vilken `List<T>` (eller annan datastruktur) använde ni, och varför just den?*

> Vi använde arv för våra monster eftersom alla monster har gemensamma egenskaper som HP, attack och defense, men kan bete sig olika. Vi hade därför en basklass Monster 0ch olika monster som ärver från den. Med virtual och override kunde monstren ha olika sätt att attackera. vi använde bland annat List<monster> föratt lagra flera olika monster tillsammans bland annat list<itmes> för spelar inventoy. Listor passade bra eftersom vi ville kunna lägga till och gå igenom objekten.

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.  
> Svara på frågorna som hör till **ditt** projekt och dina VG-val. Radera resten.
 

### Skogsäventyret

- Vilken datastruktur valde ni för vapensortimentet och varför?
- Hur sorterade ni monstren i arenan?
- Hade ni kunnat lösa arenan utan arv?

> Vi använder List<items> för vanliga föremål och List<Weapon> för vapnen i shoppen. Listorna gör att vi enkelt kan visa och köpa föremål med hjälp av loopar och index.
> Vi använde en list<Monster> där monstren läggs in i den ordning som de ska mötas i arenan. På så sätt kan vi gå igenom listan med en for-loop och hämta nästa monster med dess index. Vi valde alltså ordningen när vi skapade listan istället för att använda en separat sorteringsmetod.
> Det hade nog gått men vi hade fått upprepa mer kod och utan arv hade det nog blivit ännu svårare att strukturera och hitta i koden Så jag tror att arv och override sparade oss mycket huvudvärk.
Vi skapade även en basklass som heter `Weapon` och två subklasser som heter `Longsword` och `Greataxe`. Vi valde arv eftersom båda vapnen har gemensamma egenskaper som namn, pris och attackbonus, så vi slipper skriva samma kod flera gånger. Skillnaden är att basklassen innehåller det som är gemensamt medan subklasserna kan ha egna beteenden. Med `virtual` och `override` kunde vi göra så att Longsword ger sin attackbonus plus 2 medan Greataxe multiplicerar sin attackbonus med 2. En vanlig lista med siffror kan bara lagra värden, men med subklasser kan vi också skapa egna metoder som `Använd()` och låta vapnen fungera olika. Vi använder därför `List<Weapon>` för vapnen i shoppen och räknar med deras bonus när spelaren attackerar.
