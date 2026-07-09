Kluriga kodsnuttar för kreativa kodare
Delegates och events

March 19, 2010
Authored by: Marcus Medina

Inledning ................................................................................................................................................................. 2
Treskiktslösningar ................................................................................................................................................... 3
Delegates................................................................................................................................................................. 5
Deklaration av delegates ..................................................................................................................................... 5
Exempel på hur man kan använda det ................................................................................................................ 6
Events ...................................................................................................................................................................... 7
Deklaration av Events.......................................................................................................................................... 7
Exempel på hur man kan använda det ................................................................................................................ 8
metoder att komma ihåg ........................................................................................................................................ 9
Andra tillämpningar ................................................................................................................................................ 9

1

INLEDNING
I detta häftet kommer vi att titta på delegates och events.
Delegates är ett sätt att få en metod att skickas runt som en variabel, eller exempelvis läggas in i en array. Det
används även för att hänvisa till specifika metoder vid behov.
Events använder man för att kommunicera med andra klasser vid speciella händelser.
Vi kommer även att smygtitta på timerevents.

2

TRESKIKTSLÖSNINGAR

User

GUI

Logik

Databas

Innan vi går in på Delegates och events så ska vi titta på treskiktsklösningar. Detta är det mest använda sättet
att programmera på och där delar man upp programmet i småbitar.
Regler:
A.
B.
C.
D.

Användaren kommincerar enbart med GUIt.
GUIt kommunicerar enbart med logiken och svarar användaren.
Logiken förser GUIt med information och hämtar data från DBn.
DBn returnerar data till Logiken.

Detta kan verka krångligt och onödigt, men det ger dig tryggheten att du vet precis var felet finns. Om det
grafiska gränssnittet dummar sig så vet du vilken del du ska ta hand om, om logiken inte fungerar så vet du
precis var du ska leta osv. Det gör också att flera programmerare kan arbeta samtidigt på samma program. En
sköter logiken, en GUIt och en DBn exempelvis.
Detta är grunden för bra programmering, använd det – alltid!
1.
2.
3.

Grafisk gränssnitt (GUI – Graphical User Interface)
Programlogik
Databas (DB)

Dessa kan i sin tur delas upp i småbitar ochså
1.

2.

3.

Grafisk gränssnitt (GUI – Graphical User Interface)
a. Grafiska objekt
b. Koden bakom
c. Övrig logik bakom
Programlogik
a. Databehandling
b. Datalagring
Databas
a. ODBC Dller
i. Databasmotor
b. Databasstrukur
c. Data

Men strunt i det, det viktiga är första listan. GUI, programlogik och databas. Det är de vanliga delarna man
använder.
3

Fördelen är att logiken i programmet kan användas i olika miljöer oberoende av det grafiska gränssnittet. Du
kan alltså göra en class som används i ett vanligt windows program, i en webbsida och i en pocketPC – utan att
ändra en rad kod i din class. GUIt tar hand om all kommunikation ut, den förändras från miljö till miljö däremot.
Detta medför vissa begränsningar. Classer som sköter logiken kan inte (och får inte) kommunicera med
användaren. Så fort man gör det blir programmet låst till en viss miljö!
Databasklassen ska inte sköta beräkningar eller logik, den ska bara kommunicera med databasen och returnera
resultatet.
Så hur löser vi det om vi vill att vid ett visst tillfälle (exempelvis om ett värde kommer in och det leder till att det
grafiska gränssnittet ska uppdateras) så ska logikklassen meddela GUIt om det. Ett sätt är att använda en Event
som triggas när något speciellt sker. Det kan GUIt fånga in och uppdatera informationen på skärmen. Det blir
snyggt och omärkbart för användaren.
Samma sak om användaren matar in felaktig data. Man kan inte visa felmeddelanden i den logiska delen av
koden, men man kan skicka en Event som talar om för GUIt att den ska skälla ut användaren.

Oops... Sorry
Jag ska genast skälla
på användaren!

GUI

Nämen hallå!
Du kan inte skicka hit
sådana värden!

Logik

Om man beställt information från databasen och nu ska resultatet presenteras, då skickar man en event till
GUIt med all data från databasen, så sköter den sitt arbete i lugn och ro.
Det kan bli lite segt om DBn skickar ett felmeddelande, då får Logiken skicka iväg ett felmeddelande med
samma information till GUIt som i sin tur skäller på användaren.
På detta sätt slipper vi blanda logik och GUI.
Om vi behöver att logiken ska anropa en viss metod i GUIt, exempelvis inne i formuläret. Så kan vi förse logiken
med en Delegate som använder den för att kommunicera med vår GUI. Detta ger oss fördelen att logiken bara
får tillgång till en specifik metod, inget annat!
Det är egentligen samma tanke bakom som när man gör privata variabler, man vill inte att vem som helst ska
pilla på våra metoder och variabler!

4

DELEGATES
Delegates är ett sätt att handskas med metoder som variabler. Det gör att vi kan skicka metoden som en
parameter till en annan metod. Rörigt eller hur?
Nja, det är inte så märkvärdigt. Man anväder det mycket inom Java, PHP, Javascript, Flash och andra sådana
språk.

Behandla den här
informationen. När du är
klar, anropa den här
metoden som jag har!
Gäsp...

GUI

Logik

Det fungerar ungefär såhär:
Vi kan också ha en metod som anropar olika metoder beroende på dess input. Man hade kunnat göra det med
if-satser, om alla metoder legat i samma class, men om de inte gör det? Hur kan man anropa en metod i en
class och sedan få den att anropa metoder i en annan class? Eller hur ska man få classen att anropa metoder i
formuläret man själv skapat?
Man kan skicka en referens till formuläret eller classen som innehåller metoden som man vill ska anropas, men
det gör att hela formuläret eller classen blir tillgänglig för andra, och det är inte alltid man vill det.
Dessa problem kan man lösa med Delegates.

DEKLARATION AV DELEGATES
En delegate deklareras såhär
public delegate string Beräkna(string Tema, int ArtikelID);
För att kunna använda den måste man skapa metoder som matchar dess beskrivning. Alltså metoder som
returnerar en bool och tar emot en string och en int.
public bool GenereraPHP(string Tema, int ArtikelID) {}
public bool GenereraHTML(string Tema, int ArtikelID) {}
osv

5

EXEMPEL PÅ HUR MAN KAN ANVÄNDA DET
I detta exempel ska vi skapa en klocka.
Vid programstart så kommer den att använda mallen av TimerCallback och tala om för den vilken metod
vi har som matchar den. Därefter ställs Timer in till att anropa metoden 1 gång per sekund. (1000
millisekunder = 1 sekund).
Man kan använda sådana anrop för att låta programmet arbeta i bakgrunden medan vi gör andra saker. Den
kan till exempel söka i hårddisken efter en viss fil, medan användaren tittar på någon annan information.
Multitaskning kallas det, eller i programmeringsterm kallas det för trådning. Men det kommer mer om trådning
i kursen om Systemprogrammering och kursen om Realtidsprogrammering.
Läs igenom koden så förstår du nog.
static void Main(string[] args)
{
//Skriv ut en kul överskrift
Console.WriteLine("Vad är klockan?");
//Skapa en timerCallback, alltså definiera vilken
//Delegat som ska anropas av Timern
TimerCallback tc = new TimerCallback(SkrivUt);
//Instansiera en timer
// * Ge den delegaten att anropa
// * Object state används för att skicka in parametrar i form av
//
en instans av en class
// * När ska den anropas första gången (efter 0 millisek)
// * När ska den anropas igen (efter 1000 millisek)
Timer Klocka = new Timer(tc, "Klockan är", 0, 1000);
//Vänta på att användaren ska ledsna
Console.ReadLine();
//Förstör klockan
Console.WriteLine("Stänger av klockan");
Klocka.Dispose();
}
//Metod som skriver ut klockan
static void SkrivUt(object state)
{
Console.WriteLine("{0} {1}",(string)state,
DateTime.Now.ToLongTimeString());
}
Detta kan man använda om man vill uppdatera viss information stup i kvarten, eller om man vill kontrollera ett
visst värde då och då. Det fina är att programmet fortsätter sin körning som vanligt, men metoden kommer att
anropas var sekund vare sig main ligger och sover eller inte. I exemplet ovan låter vi main vänta genom
Console.ReadLine();. I vanliga fall stannar programmet där, men det gör inte det nu!
Så roligt kan man ha med Delegater och Timers! Delegater är inte nödvändigtvis sådana att de kör i
bakgrunden, vill man ha den funktionaliteten måste man använda timers.

6

EVENTS
En event är en metod som anropar en given metod (Delegat) när en viss sak händer.
Som det förklarades i början så ska inte logikdelen av programmet kommunicera med användaren, men om
något gått fel så måste detta påpekas för användaren. Då skickar man alltså en signal till GUIt som sedan
informerar användaren.

DEKLARATION AV EVENTS
Klassen som ska skicka ut Events måste först definiera en Delegate för eventet. Denna delegate gäller för
metoder som inte returnerar något, men som tar emot en string. Delegaten kallas Error.
public delegate void Error(string Meddelande);
Sedan deklareras själva eventet. Den använder delegaten Error som typ och får namnet FelID.
public event Error FelID;
Classen som vill prenumerera på eventet måste i sin tur skriva in sig på en lista av prenummeranter av eventet.
Detta görs vanligen vid instansieringen av classen med eventet.
PersonLista pers = new PersonLista();
pers.FelID += new PersonLista.Error(Meddelande);
Först intansieras classen, sedan talar vi om att vi vill prenummerera på eventet genom att addera en delegat av
typen Error till själva eventet FelID.
När vår event sedan ska skickas ut så ska vi kontrollera att någon prenummererar på det. Annars är det rätt
meningslöst att skicka ut eventet. Det gör vi med en enkel if-sats.
if (FelID != null)
{
FelID("Personen finns inte");
}
Om FelID har några prenumeranter (inte är null alltså) så skicka iväg meddelandet genom eventet. Detta gör att
de ”Error” kompatibla metoderna som lagts till i event-listan kommer nu att anropas en och en, och få det
meddelande som skrevs in i argumentet.

7

EXEMPEL PÅ HUR MAN KAN ANVÄNDA DET
Tänk dig en personlista, OK denna är väldigt förenklad, men ändå.
class PersonLista
{
private List<string> Personer=new List<string>();
public delegate void Error(string Meddelande);
public event Error FelID;
public string HämtaPerson(int ID)
{
if (ID >= Personer.Count)
if (FelID != null)
{
FelID("Personen finns inte");
return "";
}
return Personer[ID];
}
}
Metoden hämta person kontrollerar om du angivit ett alldeles för högt värde, och i så fall skickar den ut en
event att personen inte finns. I detta fall fungerar eventet som en Exception, fast man slipper hemska error
meddelanden 
Metoden skulle kunna ändras (om Listan innehöll mer information), till att trigga eventet om personen
markerats som inaktiv, eller om personen tillhör FBIs lista ”Most Wanted criminals” eller något annat sådant.
I main skulle vi kunna lägga följande kod
PersonLista pers = new PersonLista();
pers.FelID += new PersonLista.Error(Meddelande);
Console.WriteLine(pers.HämtaPerson(100));
Och sedan skapa en metod som eventet ska anropa
static void Meddelande(string Meddelande)
{
Console.WriteLine(Meddelande);
}

8

METODER ATT KOMMA IHÅG
Metodanrop
public delegate void MinEvent(string
Meddelande);

Förklaring
Man skapar alltid en delegat som berättar hur
metoderna som ska ta emot eventet ska se ut.

public event MinEvent Eventet;

Sedan deklarerar man eventet. Eventet är egentligen
en lista som tar emot delegater, och som vid anrop
skickar information till alla delegaterna i listan
Man prenumererar på events genom att skriva
classens namn.eventets namn och addera dit en
delegat med parametern som pekat mot metoden
som ska anropas

MinClass.MinEvent += new
PersonLista.Error(MinEventfångare);

ANDRA TILLÄMPNINGAR
Du skulle kunna skapa en array av delegater, i den stoppar du in exempelvis Multiplicera(), dividera(), addera()
osv. Sedan låter du dessa delegat metoder arbeta mot dina properties i classen.
På detta sätt kan du påverka ordningen för hur beräkningarna ska ske.
Om du har talen 5 + 3 / 2 * 4 så kan du alltså kolla upp ordningen det ska räknas på (* / och slutligen +) och
sedan skicka in allting till din delegatarray, och därefter loopa igenom hela delegatarrayen och exekvera
delegaten så att beräkningarna utförs.
Delegater är också bra att ha vid pluginsystem. Man har en delegat som talar om hur pluginnet ska anropas,
och sedan får alla som vill bli anropade lägga sig i en lista av tillgängliga plugins. Man behöver inte veta något
om dem, mer än att deras delegat metod finns i listan på tillgängliga moduler.

9
