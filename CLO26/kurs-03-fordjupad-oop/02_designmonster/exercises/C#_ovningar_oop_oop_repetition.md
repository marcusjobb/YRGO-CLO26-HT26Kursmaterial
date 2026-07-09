OOP REPETITION
MARCUS MEDINA, CODIC EDUCATION

CLASS
• Vad är en class
• Ungefär som lägerheter i ett
hyreshus

• Huset är en class för sig
• Varje lägenhet är en class för sig
• Varje hyresgäst kan man också se
som en class för sig

CLASSER I KOD
public class Person
{
}

Helt vanlig class.
Man använder classer till att dela upp program i småbitar, istället för att ha all kod i en
enda stor klump.
Det är till stor hjälp för att återanvända kod och gruppera metoder enligt
användsningsområde.

CLASSER I KOD
public class Person
{
public string Namn { get; set; }
public string Efternamn { get; set; }
}

Class som håller koll på personer, en så kallad “POCO (Plain Old CLR Object)”, även kallad
en “Plain old class/C#/C object”
Classen gör inte mycket annat än att spara två strängar… eller?

CLASSER I KOD
private string namn;
public string Namn
{
get { return namn; }
set { namn = value; }
}

En property {get;set;} är en förkortning av koden ovan

CLASSER I KOD
private string namn;
public string Namn
{
get { return namn; }
set { namn = value; }
}

Orsaken till att man använder dem är för att man kan ta emot värden och kontrollera dem
när de kommer in, det kan man inte göra med en public variabel.
GetSetters är standard i de flesta programmeringspråk, just för att man vet att de kan
kontrollers och för att man vet att de är enbart för information som ska delas till och från
classen.

CLASSER I KOD
public class Person
{
public string Namn { get; private set; }
public string Efternamn { get; private set; }
}

I detta exempel gör vi variabeln READ ONLY utanför classen, men fullt skrivbar innanför
classen, detta gäller dock bara de värden som skickas “by ref”, alltså “built in”-typer som
int, string, bool osv
Listor, classer och andra avancerade object kan inte göras readonly på detta sätt.

CLASSER I KOD
private string namn;
public string Namn
{
get { return namn; }
set { if (value.Length>5) namn = value; }
}

Vi kan kontrollera den input som kommer in

CLASSER I KOD
public class Person
{
public string Namn { get; set; }
public string Efternamn { get; set; }
public override string ToString() => $"{Namn} {Efternamn}";
}

När vi debugger är det skönt att kunna se värdet i classen istället för
{ConsoleApp1.Person}

Ett sätt att slippa det är att overrida ToString()

CLASSER I KOD
public class Person
{
[System.Diagnostics.DebuggerDisplay("{Namn} {Efternamn}")]
public string Namn { get; set; }
public string Efternamn { get; set; }
}

Ett annat sätt är att använda den inbyggda debuggern (System.Diagnostics)

Det fungerar lika bra, men ToString() blir snyggare om man använder Console.WriteLine(p)
eller Debug.WriteLine(p)

OBJECT CLASSEN
public class Person : Object
{
public string Namn { get; private set; }
public string Efternamn { get; private set; }
}

Alla classer ärver automatiskt från Object classen och där finns en del metoder som man
bör känna till

EQUALS()
public override bool Equals(object obj)
{
var cmp = (Person)obj;
return Namn == cmp.Namn && Efternamn == cmp.Efternamn;
}

Equals används för att jämföra innehåller i classer.
var p = new Person { Namn = "Jeff", Efternamn = "Goldblum" };
var p1 = new Person { Namn = ”Bill", Efternamn = ”Murray" };
Console.WriteLine(p.Equals(p1)); // False

OPERATOR ==
public static bool operator ==(Person personA, Person personB) { return (personA.Equals(personB)); }
public static bool operator !=(Person personA, Person personB) { return !(personA.Equals(personB)); }

== och != ska alltid deklareras tillsammans, deklarerar man den ena ska man deklarera den
andra med
• var p = new Person { Namn = "Jeff", Efternamn = "Goldblum" };

• var p1 = new Person { Namn = "Jeffrey", Efternamn = "Goldblum" };
• Console.WriteLine(p==p1);

== och != använder sig av Equals för att jämföra. Inte nödvändigtvis men det är smart att
göra det.

HASHCODE
public override int GetHashCode() { return (Namn,Efternamn).GetHashCode(); }

Hashkoden är en siffra som används för att identifiera classer, detta nummer används för
att jämföra classer med varandra, därav fungerar inte Equals och == när man jämför
classer trots att de ser likadana ut och har likada värden. Varje class får sin specifika värde.
För att overrida den är det enklast att använda sig av en Tuple som man sedan räknar ut
Hashvärdet på.
Så vad är en Tuple för något?
Det är en modernare variant av en Struct och kan användas som en vanlig variabel.

FALSKA PROPERTIES
public class Rectangle
{
public double Base { get; set; }
public double Height { get; set; }
public double Area { get { return Base * Height; } }
public double Circumference { get { return (Base * 2) + (Height * 2); } }
}

En property behöver inte vara enbart en variabel, man kan ha variabler som räknar ut
värden och returnerar dem som om de vore vanliga variabler.

COOLA INBYGGDA METODER
public class Rectangle
{
public double Base { get; set; }
public double Height { get; set; }
public double Area { get { return Base * Height; } }
public double Circumference { get { return (Base * 2) + (Height * 2); } }
public static bool operator ==(Rectangle r1, Rectangle r2) { return (r1.Equals(r2)); }
public static bool operator !=(Rectangle r1, Rectangle r2) { return !(r1.Equals(r2)); }
public override string ToString() { return $"Base:{Base} Height:{Height}"; }
}

Vi kan lägga till de vanliga overrides i denna class med, precis som vi gjorde innan men vi
kan också lägga till fler operatorer
Vi talar först om att det är en operator, och sedan vilken operator vi vill overrida. I övrigt
fungerar det som en vanlig metod, där vi talar om vad den ska returnera och vilka
parametrar den tar emot. Och koden inne i metoden fungerar precis som en i en vanlig
metod.

MER COOLA OPERATORER
public static Rectangle operator +(Rectangle r1, Rectangle r2)
{ return new Rectangle { Base = r1.Base + r2.Base, Height = r1.Height + r2.Height }; }
public static Rectangle operator -(Rectangle r1, Rectangle r2)
{ return new Rectangle { Base = r1.Base - r2.Base, Height = r1.Height - r2.Height }; }
public static Rectangle operator *(Rectangle r1, Rectangle r2)
{ return new Rectangle { Base = r1.Base * r2.Base, Height = r1.Height * r2.Height }; }
public static Rectangle operator /(Rectangle r1, Rectangle r2)
{ return new Rectangle { Base = r1.Base / r2.Base, Height = r1.Height / r2.Height }; }

Nu har vi även de fyra räknesätten, vi kan addera, multiplicera, substrahera och dividera
våra fyrkanter med varandra. Men oftast använder man inte de på detta sätt, vi behöver
andra sätt att addera och multiplicera på.
Samma regler för operatorer gäller här med skillnaden att vi returnerar en ny instans av
det som ska returneras. I detta fall en ”new Rectangle”.

TESTAR CLASSEN
var rectangleA = new Rectangle { Base = 10, Height = 15 };
var rectangleB = new Rectangle { Base = 2, Height = 5 };
Console.WriteLine(rectangleA); // Base:10 Height:15
rectangleA += rectangleB;
Console.WriteLine(rectangleA); // Base:12 Height: 20

Och i det här exemplet slår vi samman två rektanglar

MER COOLA OPERATORER
public static Rectangle operator +(Rectangle r1, int r2)
{ return new Rectangle { Base = r1.Base + r2, Height = r1.Height + r2}; }
public static Rectangle operator -(Rectangle r1, int r2)
{ return new Rectangle { Base = r1.Base - r2, Height = r1.Height - r2}; }
public static Rectangle operator *(Rectangle r1, int r2)
{ return new Rectangle { Base = r1.Base * r2, Height = r1.Height * r2}; }
public static Rectangle operator /(Rectangle r1, int r2)
{ return new Rectangle { Base = r1.Base / r2, Height = r1.Height / r2}; }

Vi kan ändra vilka typer som det räknas mot till exempel mot en int

TESTAR CLASSEN
var rectangleA = new Rectangle { Base = 10, Height = 15 };
Console.WriteLine(rectangleA); // Base:10 Height:15
rectangleA += 4;
Console.WriteLine(rectangleA); // Base:14 Height: 19

I exemplet här förstorar vi upp rektangeln med 4 enheter.

ARV
När vi ärver en class får vi tillgång till alla variabler och metoder
som är publika och protected.
Skillnaden är att public är tillgänglig för alla, protected är tillgänglig
enbart för de classer som ärver
Privata metoder och classer förblir privata för huvudclassen

INTERFACES
Ett interface är en mall för vilka metoder en class ska innehålla. De
metoder och properties som deklareras i interfacen är enbart
publika och icke statiska.
En class kan ärva en annan class, men den ”ärver” inte ett interface,
den inplementerar Interfaces.

INTERFACES ANVÄNDNINGSOMRÅDE
Grejen med interfaces är att man får en mall för att flera classer ska ha
samma uppbyggnad. Det kan man använda exempelvis när man arbetar
mot APIer och ska skicka objekt till den, eller om man gör ett program
som använder plugins.
Ett av de vanligaste användningsområden är dock att klumpa ihop classer
i en lista som tar emot alla objekt av en viss Interface eller en metod som
tar emot classer genom en parameter av en Interface-typ.

EXEMPEL PÅ INTERFACES
public interface IAnimal
{
int Legs { get; set; }
string Name { get; set; }
bool Cuddly { get; set; }
}
public interface IDog : IAnimal
{
void Bark();
void PlayFetch();
void FormPack();
}
public interface ICat : IAnimal
{
void Meow();
void Purr();
void Climb();
}

LIST<INTERFACE>
I förra sliden såg vi interfacen IAnimal, IDog och ICat.
Skapar man en class som instansierar ICat så måste den ha alla
egenskaper från ICat och IAnimal. Det betyder dock att vi kan samla
olika kattsorter (om vi skapar olika sådana) i en List<ICat> med
katter. Vi kan även skapa en lista med List<IAnimal> som kan
innehålla både katter och hundar.
Skillnaden är att ICat listan kan foreachas och då kan man anropa
Meow(); men i IAnimal listan kan vi bara se namn, antal ben och om
djuret är gosigt.

LIST<INTERFACE>
I kattlistan List<ICat> kan vi se alla egenskaper en vanlig katt har,
men vi kan inte se ifall en katt class har andra egenskaper och
metoder som inte anges i interfacet.
Samma sak gäller om vi skapar en lista med hundar List<IDog>,
självklart.

ABSTRACT CLASS
En abstract class är en class med metoder, properties och variabler
som inte går att instansiera – den måste ärvas.
Till vilken nytta?
Den används exempelvis om man vill att programmeraren som
använder vår kod ska tvunget skapa en viss sorts class, med
specifika funktioner som måste finnas och fungera på ett visst sätt.
Detta kan vi inte göra med interfaces. Den abstracta klassen kan
innehålla kod i sina metoder, men kan också ha tomma metoder.
Det är en mellanting mellan class-arv och interface.

STRUCT
struct Dot{
public int x;
public int y;
}

var dot = new Dot
{
x = 10,
y = 10
};

En struct är en typ som an definierar på ett liknande sätt till definierar en class. Oftast
används den för att hantera variabler, som en POCO. Den kan ha metoder och properties,
precis som en class – men det är inget man pratar högt om.

STRUCT
Då uppstår frågan, varför använder man inte den oftare?
Struct är by-value, detta innebär att varje gång man hänvisar till
den, exempelvis skickar den som parameter – så skapas en kopia av
den. Varje kopia lever för sig själv tills metoden avslutat. Vilket är
bra i många fall, men det har den stora nackdelen att ju mer kod
man stoppar in i en struct desto mer minne tar den upp.

TUPLE
Tuple<int, string, string> original = new Tuple<int, string, string>(1, "Steve", "Jobs");

var simplified = Tuple.Create(1, "Steve", "Jobs");

var cool = ("Steve", "Jobs");

En Tuple är som en Struct men den innehåller enbart properties, här kan man inte lägga in
kod. Detta gör att Tuples är mindre och enklare än Struct.
Det finns tre olika sätt att definiera dem på, som vi ser ovan. De olika sätten visar hur
Tuple objektet utvecklats mellan .Net versioner.
Bästa användningsområde för Tuples är när man behöver returnera mer än en variabel
från en metod.

EXPANDO
dynamic meow = new System.Dynamic.ExpandoObject();
meow.Type = "Cat";
meow.Name = "Misse Mjau";
Console.WriteLine(meow.Type);

Expando är en fantastisk men smått skrämmande objekt. I den skapar man properties
medan man programmerar. Detta innebär att objektet inte har properties förrän den
kompileras, och då skapas de properties vi skickat in. Precis som classer är Expando
ByValue.
Enda stora nackdelen är att Visual Studio kan inte korrigera om man skrivit fel,
ExpandoObjektet kommer att att acceptera om jag vill att den skriver ut meow.Type och
meow.type, enda skillnaden är att den senare kommer att vara NULL.

NÄR ANVÄNDER MAN VAD?
Objekt

När ska man ha det

Class

När man skapar kod för en viss funktionalitet, exempelvis kod som ska
hantera en viss filtyp eller en viss maskin. Man kan även skapa classer för
att gruppera metoder och funktioner som hör samman (exempelvis
databaskommunikation, beräkningar mm).

Arv

När man vill lägga till mer, eller annorlunda funktionalitet till en class

Interfaces

När man behöver en mall för vilka funktioner en class ska ha, och för att
kunna filtrera mellan class objekt i en lista (exempelvis enbart objekt av
typen IKatt får vara med)

Struct

När man behöver en mindre class som behållare för värden, bra att ha för
att kunna skicka och ta emot flera värden vid ett anrop (old fashion)

Tuple

När man behöver en mindre typ som behållare av värden, bra att ha för
att kunna skicka och ta emot flera värden vid ett anrop

Expando

När man läser in en strukturerad fil och vill återskapa dess struktur utan
att för den skull definiera en class. Exempelvis vid inläsning av JSON eller
XML filer utan modell-class.
