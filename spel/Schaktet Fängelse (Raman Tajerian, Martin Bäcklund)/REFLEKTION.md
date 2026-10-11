
Namn: Raman Tajerian

Kurs: Grundläggande OOP i C# 

Uppgift: Skogsäventyret

Datum: 2026-10-01

Viktigt: Vi följde den förra mallen så vi har inte samma rubriker på frågorna och våran VG del finns i rapporten istället för reflektion. Hoppas ändå att vi förklarar det mesta och om något skulle fattas kan vi lägga till om du frågar efter det!

Vad var svårast att lösa?

Det som kändes svårast var antalet kodfiler som behövdes och i kombination med det jobba i grupp. Det kändes som det var mycket att hålla på som att basklasser och subklasser hänger ihop, alla värden stämmer överens för att annars kommer koden inte kompilera alls. 
Två saker som nödvändigtvis inte var svårast men som jag ändå behövde söka djupare förklaring för att få bättre förståelse var att förstå varför en metod inte ska ta emot klassens egna properties som parametrar, eftersom det "skuggar" objektets riktiga data istället för att ändra den. Jag gjorde det misstaget i TakeDamage innan jag förstod att en metod på Fange redan har tillgång till sina egna properties utan att de behöver skickas in. 

Vad tog längre tid än du trodde — och hur kom du vidare? 

Att hitta och fixa buggarna i Program.cs tog betydligt längre tid än att skriva grundkoden. Flera saker som såg korrekta ut vid genomläsning fungerade inte som tänkt när vi väl spelade igenom spelet, till exempel att besegra monster kunde dyka upp igen, att man inte kunde fly från en strid, och att vårt försvarsvärde inte gjorde någon skillnad i striden.
Jag kom vidare genom att faktiskt köra spelet upprepade gånger och läsa utskrifterna noga rad för rad, istället för att bara läsa koden och anta att den gjorde rätt. Det var först när jag såg ett monster med noll HP bli "besegrat" utan strid som jag förstod att vi återanvände samma objekt istället för att skapa nya.

Hur fungerade samarbetet i gruppen?

Vad fungerade bra?

Överlag fungerade allt inom arbetet väldigt bra, vi hade störst fördel av att vi jobbade med varandra i det tidigare projektet så vi tog lärdom från hur det gick tidigare och kunde förbättra det denna gången. Vi hade en bra tidsplan och bra kommunikation och uppgiften kändes jämnt uppdelad för oss båda. Vi diskuterade även vårt program när vi sågs under lektionerna och gick igenom vad vi ska göra härnäst och vad vi behöver förbättra i vår kod.

Vad var svårt?

Jag kände för det mesta att inget var riktigt svårt mellan oss som grupp men om jag ändå var tvungen att välja var det nog när våra klasser möttes i Program.cs Det kunde vara olikheter i våra namespaces och liknande men även att skapa konflikter så vi fick anpassa vårt arbetssätt och jobba tätare tillsammans genom att skriva varje gång vi pushar eller pullar.

Hur delade ni upp arbetet

Som beskrivet i rapporten delade vi upp arbetet i olika kodfiler vilket fungerade bra och utan större problem. Rapporten skrev vi tillsammans i ett delat Google Docs som vi båda kunde redigera parallellt.

Om du fick göra om det - vad hade du gjort annorlunda?

Jag hade testat koden löpande medan jag skrev den, istället för att vänta tills hela klassen var klar. Flera av buggarna vi hittade i efterhand hade förmodligen synts tidigare om vi kört spelet oftare under byggandet, inte bara i slutet. Även kanske jobbat tydligare med kraven genom att skriva ner dem, för att även om jag förstod uppgiften helt var det enkelt att komma bort sig och när man skrivit en del kod glömma bort att monster till exempel inte kan komma tillbaka med 0 HP.
Jag hade också lagt till kommentarer direkt när jag skrev koden, istället för att gå tillbaka och lägga till dem efteråt. Det gjorde att jag fick tänka igenom samma logik två gånger.

