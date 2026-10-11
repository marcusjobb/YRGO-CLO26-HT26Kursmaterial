Reflektion — Slutprojekt

Namn:  Emma Löwing
Kurs: Grundläggande OOP i C#  
Projekt: (Skogsäventyret / Havsforskarna / Dungeon Crawler)*  
Datum: 04-10-26  

---

> Den här reflektionen är obligatorisk och lämnas in **individuellt** — även om projektet gjordes i grupp.  
> Det finns inga rätta eller fel svar. Skriv vad du faktiskt tyckte och upplevde.  
> Kort och ärligt räcker — några meningar per fråga.

---

Vad var svårast att lösa?

Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?

> Jag tyckte att det svåraste var att förstå och se helheten. Jag är inte så van vid spel och då var helhetsbilden lite svår. Det som tog längst tid var nog att gå tillbaka och ändra saker när vi väl hade testspelat. Jag tycker alltid att det är lite utmanande att gå tillbaka och ändra i “gammal” kod för jag är alltid rädd att sabotera något som jag redan har gjort. Jag kom vidare genom att helt enkelt prova mig fram. I och med att jag missade genomgången av spelet så kollade jag extra noga när klasskamraterna gick igenom sina spel, detta för att förstå helheten. Och när vårt spel hade en första version så testade jag det väldigt mycket och felsökte väldigt mycket. 

---

Hur fungerade samarbetet i gruppen?

Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet? Hur använde ni Git tillsammans (branches, merge, pull requests)?

>Vi stötte på några merge konflikter och de löste vi helt enkelt genom att stanna upp, göra en kopia på respektive kod och sedan metodiskt felsöka. Som helhet tycker jag att samarbetet fungerade väldigt bra. Vi kommunicerade bra och stämmde av ofta. Vi började med att jag gjorde spelar klassen och David gjorde Monsterklassen. Jag blev klar lite snabbare i och med att spelareklassen inte innehåller arv så då hjälpte jag till med monsterklassen. Sedan gick jag vidare och jobbade i program cs medan David skapade shop.cs och items.cs. Mot slutet tyckte jag att det var lite svårt för då kom jag till en punkt där jag inte riktigt kunde fortsätta förens David var klar med allt sitt. Detta skapade lite schema problem men vi löste det. När David var klar med allt sitt färdigställde jag det sista i koden och sedan testa spelade vi båda och felsökte.
---

Om du fick göra om det — vad hade du gjort annorlunda?

Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*
Om jag hade fått göra det igen så hade jag gjort en overlod istället för 
> Om jag hade gjort om projektet hade jag velat använda overloading istället för att ha två separata metoder för striderna. Vi har nu en metod för vanliga strider och en separat för striderna i Valborgsarenan, trots att mycket av koden är likadan. Med overload hade metoderna kunnat ha samma namn men olika parametrar beroende på vilken typ av strid som skulle köras. Det hade kunnat minska mängden upprepad kod och gjort strukturen tydligare. Jag skulle även göra så att monstren attackerade mer ju högre level man är i. Nu tar man inte så mycket skada om man är på en högre level. Alternativt skapa fler starkare monster som dyker upp efter ex level 10.

---

Valfritt — arv och datastrukturer

Om ni använde arv: varför valde ni den strukturen? Vilken `List<T>` (eller annan datastruktur) använde ni, och varför just den?*

> Vi använde arv för monstren. Vi skapade en basklass Monster med sådant som alla monster har gemensamt, till exempel HP, attack, defense och XP. Sedan fick de olika monstertyperna ärva från Monster. Det gjorde att vi slapp skriva samma grundläggande kod flera gånger.
Vi använde bland annat en List<Monster> i Valborgsarenan. Där kunde vi lägga alla monster som spelaren skulle möta i den ordning de skulle komma. Eftersom de olika monsterklasserna ärver från Monster kunde alla ligga i samma lista. Vi använde också List<items> för inventory, eftersom spelaren kan få fler föremål under spelets gång och listan därför behöver kunna förändras.

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.  
> Svara på frågorna som hör till **ditt** projekt och dina VG-val. Radera resten.

### Skogsäventyret

- Vilken datastruktur valde ni för vapensortimentet och varför?
>Vi använde List <items> på sortimentet i shoppen. Det passade eftersom vi hade flera olika items som skulle kunna lagras tillsammans och gås igenom. Det gjorde det också enkelt att lägga till fler items om vi hade velat utveckla spelet vidare. 

- Hur sorterade ni monstren i arenan?
>Vi använde en List <Monster> där monstren lades in i den ordningen spelaren skulle möta dem. Först kommer tre Sealmonster, sedan tre Full Chalmers Student och slutligen, the final boss, Marcus med inlämning. Vi går sedan igenom listan med en for-loop. På så sätt blir motståndarna svårare under arenans gång. 

- Hade ni kunnat lösa arenan utan arv?
>Arenan hade gått att lösa utan arv men det hade blivit mer komplicerat och inneburit mer upprepad kos. I och med att alla våra monstersubklasser ärver från basklassen Monster kan vi lägga dem i samma List<Monster> och behandla dem som monster, trots att de är olika typer. Alltså gör arv lösningen enklare och mer strukturerad. 


