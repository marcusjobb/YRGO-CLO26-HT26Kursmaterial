
Rapport

Kurs: Grundläggande OOP i C#

Uppgift: Skogsäventyret

Grupp: Team Wedge Antilles (Raman Tajerian och Martin Bäcklund)

Datum: 2026-09-30

GitHub: raman-tajerian och Martin-Backlund / Länk till repo: 
https://github.com/raman-tajerian/schaktet_fangelse_platform

[Spela Schaktet här](https://raman-tajerian.github.io/schaktet_fangelse_platform/)

Viktigt: Vi följde den förra mallen så vi har inte samma rubriker på frågorna och våran VG del finns i rapporten istället för reflektion. Hoppas ändå att vi förklarar det mesta och om något skulle fattas kan vi lägga till om du frågar efter det!

Klasserna

Vi har i detta projekt också delat upp arbetet med att skriva i olika klasser och sedan kommunicera till varandra hur vi gått till väga med våra lösningar och kod så att koden är konsekvent oavsett vem som skriver. Fange är spelarens klass och motsvarar Player i grundstrukturen. Denna klassen är fristående från dom andra då uppgiften kräver arv för motståndarna i spelet. Medfange är en abstract basklass för de motståndare som finns i spelet, dessa tre är monstrena, utsvulten, hamstraren och bödeln med egna fasta värden för HP attack, försvar, XP och guld. För enkelhetens skull skrev Raman fånge och Martin Medfånge och dess subklasser. 
För VG delen har vi även Redskap, en abstract basklass för vapen med likadant mönster och två subklasser till den, improviserad kniv och metallrör. Svarta marknaden är en fristående klass som håller sortimentet och hanterar köpen. Vårt main program parallell programmerade vi med varandra och där innehöll starpunkten och själva spelloopen.

Metoderna

I Fange finns TakeDamage som minskar HP och returnerar sant vid död, Heal återställer HP, GainXP lägger till XP och utlöser LevelUp med en while-loop så flera levels kan klaras på en gång och LevelUp höjer level, maxHP och attack. Vi la även till NyDag, LaggTillGuld, BetalaGuld och BytVapen eftersom dagar, guld och attack har private set och behöver deras egna metoder för att ändras.

I MedFange finns TakeDamage, Anfall och TappaGuld som slumpar guld mellan ett min- och maxvärde. 
I SvartaMarknaden finns VisaSortiment som skriver ut sortimentet med for-loop eftersom vi redan vet exakt hur många gånger den ska köras, en gång per vapen i listan.

I Arenan finns StartaArenan, som går igenom de sju motståndarna med en for loop och kör en strid mot var och en med en while loop som returnerar true om fången besegrar alla motståndare och false om fången dör.


Main()

I main skapas spelaren och kör huvudloopen while (fange.HP > 0). Väljer spelaren att äventyra byggs en ny monsterlista och ett monster slumpas fram, varpå en stridsloop körs med valen som är försvara, anfall och spring. Vila läker istället och räknar upp en dag och när spelaren dör avslutas loopen och poängtavlan skrivs ut.

Monsterlistan har vi gjort så att den byggs om inför varje strid, så att alla monster alltid har fullt HP. Skadan räknas som monstrets attack minus spelarens försvar och används både vid försvara som blir halverad och när monstret slår tillbaka efter misslyckat anfall.

Git

Git funkade bättre denna gången eftersom vi har erfarenhet att jobba med varandra från drömmatchen projektet. Vi använde separata branches för Fange och Medfange med dess subklasser så vi kunde jobba parallellt. I Program arbetade vi tillsammans och skrev varje gång vi pushade för att undvika konflikter. Vi committade löpande med tydligt beskrivande meddelanden och .gitignore kunde vi lägga till i Githubs mall direkt.

Kodkvalitet

Namnen försökte vi hålla så självförklarande som möjligt och konsekvent på svenska. Vi har kommenterat både för varandra, för läsaren och som minne för presentationen på de platser där de olika blocken av konstruktörer, metoder och annat används.

VG - Motivering

Vilken datastruktur valde ni för vapensortimentet och varför?

Vi använde en List av Redskap. Sortimentet bläddras bara igenom, alltså ingen sökning på namn behövs så en dictionary hade varit lite onödigt komplext.

Hur sorterade ni monstren i arenan?
I Arenan.cs använde vi en List som innehåller alla sju motståndare: Utsvulten,  Hamstraren, Bödeln, Vakthunden, Samurajen, Kannibalen och slutbossen Marcus. Majoriteten av dessa motståndare kommer man då endast möta i Arenan. Vi lade in dom i slumpmässig ordning med flit och sorterade dom sedan genom: motståndare.Sort((a,b) => a.Attack.Compareto(b.Attack)). CompareTo jämför två monsters attack och lägger den med lägst först. Sedan en for loop som går igenom listan från index 0, så möter man motståndarna från svagast till starkast.


Hade ni kunnat lösa arenan utan arv?
Ja eftersom att våra subklasser bara skiljer sig i värden inte i beteende. Alla använder samma TakeDamage och TappaGuld från medfange och skickar bara in sina egna siffror visa base(). VI hade därför kunnat göra medfange till en vanlig klass och skapa monstren direkt: new Medfange(“Bodeln”,35,14…..) och lägga alla i samma lista.

Varför vi löste det såhär

Private set valdes för att varje objekt ska kontrollera sin egen data, vilket krävde egna metoder för ändringar men gör klassen i sin tur säkrare att använda. Konstruktorn i Fange tar bara emot ett namn och resten är hårdkodat eftersom att alla spelare börjar med samma villkor och förutsättningar. While i GainXP används så flera levels kan avklaras i rad om man lyckas göra det. Monsterlistan byggs om varje strid var egentligen en bugglösning som vi hade men blev samtidigt något medvetet som garanterar fullt HP vid varje ny strid.

Vid testkörning hittade vi fyra problem som vi berättade om lite kort under presentationen. Den viktigaste att lösa och som tog längst tid var att fixa buggen med monster som kunde dyka upp igen med noll HP och det löste vi genom att bygga om listan varje strid.
Spring avslutade aldrig striden och löstes genom att avsluta den vid en lyckad flykt. 
Försvaret användes inte i skadeberäkningen, löstes med formeln attack minus försvar.
Guld sparades aldrig och löstes med en ny metod i Fange kodfilen. 

Git-logg

5fe8a32 (HEAD -> main, origin/main, origin/HEAD) metod GainXP skriver nu också TotalXP
c98ca2c Fixade Total XP i poängtavlan
657c1aa Fixat bugg, kan inte längre köpa kniv om och om igen
3184757 Lade till metod i Arena, samt sorterat lista av medfångar
2b638fa Lade till subklasser för mostståndare i Arenan, lade till Svarta Marknaden (Shop).
71c27ee Lagt grund för SvartaMarknaden som konstruktor och visa sortiment. Fixade namespace Improviseradkniv till ImproviseradKniv också
4238a55 Lade till konstruktor samt fält för Redskap basklassen.
39a9ca7 Skrivit i subklasserna Metallror och ImproviseradKniv
9991ec3 VG delen, lagt till klasserna redskap, improviserad kniv, metallror och svartamarknaden
e75c77f Lagt till guld i fange.cs
a12801c Fixat buggar i striden i Program.cs. Skapa nya monster varje strid så de har full hp, spelarens försvar minskar skadan, monstret slår inte tillbaka när den dör
1b67735 Tog bort return så den inte hoppar över poängtavlan direkt när man dör
9bedb60 lade till if sats för val 3,  lade till skada för fange vid anfall,
a2fdab2 Ändrat så monster inte kan få negativa HP
22d3f25 Val 2 och 3 tillagd i program
d146b5f Lagt till Ny dag i Fange cs
8253a98 Lade till loop för "striden"
d9ef404 Main program påbörjat, skapar spelare och lista av monstertyper
35ea6b4 Ändrade namespace så vi har matchande
efe0882 Merge branch 'main' of https://github.com/raman-tajerian/schaktet_fangelse_platform
9e0c164 Metoder har lagt till i Fange.cs
079ec44 Lade till konstruktor till Hamstaren (ärver ifrån Medfange).
803b880 Lade till konstruktor till Bodeln classen (ärver ifrån Medfange)
dc174f0 Merge branch 'main' of https://github.com/raman-tajerian/schaktet_fangelse_platform
c05bd04 Lade till konstrukor till Utsvulten class (ärver från Medfange)
a0e9124 Lagt till konstruktor
ce49fdb Lade till Anfall metod, samt slumpgenerator för Guld drops.
37e49ef Merge branch 'main' of https://github.com/raman-tajerian/schaktet_fangelse_platform
7109c78 Lade till relevanta kommentarer
a848905 Lagt till properties för Fange.cs
7db99a5 Lade till properties för Medfange samt konstrukor, lade till TakeDamage metod.
368e901 Lagt till klass för hamstraren och utsvulten
65e4efc Lade till Medfange.cs
d34e5c6 lade till Bodeln.cs
81e046d Lagt till fånge klass
51fcf17 Test3
0dc35e2 Test2
aaeb232 Test
3d22d7c Initial commit


