---

title: Repetition Strängar
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Repetition strängar.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "exercise", "git", "methods", "oop", "repetition", "sql", "strängar.docx", "test"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Repetition strängar

Klicka eller tryck här för att ange datum.

Beskrivning:
I den här övningen ska vi leka lite med extension metoder och strängar, mest med strängar men
ändå…

Kursplanstermer som berörs av uppgiften:
Mål

Vad du ska lära dig

Kunskap om innehållet i .NET-biblioteket

Innehållet i .NET-biblioteket.

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Typer, variabler, uttryck, villkorssatser och loopar används
inom programmering.

Kunskap kring namngivning och kodstruktur av klasser,
metoder och variabler i objektorienterade program.

Namngivning och kodstruktur av klasser, metoder och variabler
i objektorienterade program används.

Lösa problem i ett datalogiskt sammanhang.

Lösa problem i datalogiska sammanhang genom att exempelvis
bryta ner problemen.

Versionshantering med hjälp av verktyg som exempelvis Git.

Versionshanteringsverktyg under projekt.

Utveckla program med en tydligt objektorienterad struktur.

Utveckla program med en tydligt objektorienterad struktur.

Förstå och använda sig av datastrukturer inom
programmering.

Att använda datastrukturer i sin mjukvaruutveckling.

Planera, designa och implementera gränssnitt utifrån
användaren.

Att planera, designa och implementera gränssnitt utifrån
användaren.

Utveckla felfria fristående program.

Att skapa felfria fristående program.

Termer för övningen:




Extension – metod som beter sig som en del av en klass
String.empty – samma sak som att skriva ””
String.split – delar upp strängar baserat på vilket tecken man vill använda som separator.
Exempelvis en CSV fil där man delar upp allt med semikolon.
Namn; Efternamn; Datum; Ort
Bruce; Banner; 1969-12-18; Dayton, Ohio

int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.

Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

Stringmetoder repetition
I den här övningen ska vi kolla på hur man arbetar med strängar. Det är mycket man kan göra med de
små liven och det är inte alltid man tänker på det. Strängar kan användas till mycket och de kan lagra
upp till 2gb data.
Exempel

// Instansiering av en sträng
var myString1 =new string();
var myString1 ="";
var myString2 = string.Empty;
var mystring = myObject.ToString();
// Vill man skapa en sträng som upprepar samma tecken
// x antal gånger skriver man såhär:
var lines= new string('-',100); // 100 streck på rad

Extension metoder är lite speciella men väldigt användbara!
Tips

// En extension-metod anropas på samma sätt som de metoder som klassen har
// som standard. Vi använder extensions för att bygga på redan existerande
// klasser. Extensions måste alltid ligga i statiska klasser och måste
// alltid ha this på sin första parameter. This bestämmer också vilken
// typ vår extension ska kopplas till. I detta fall är Scramble(this string)
// en metod som flyttar runt på bokstäverna
var passWord = "Katt".Scramble(); // tkta
// Metoden string.Split(char separator) är en söt metod som returnerar en
array, och använder
// det givna char som separator.
var text = "A rose by any other name would smell as sweet.
-Shakespeare".Split(' ');
foreach(var word in text);
{
Console.WriteLine(word);
}

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
Sträng till ord

Du ska nu skapa en extension metod som delar en text i antal ord, och skriver ett ord i taget, det
räcker med att splittra orden baserat på mellanslag. Punkter, frågetecken och andra tecken kan
skippas.
Words

internal static class Program
{
static void Main()
{
var text="Min katt är hungrig!".Words();
foreach(var word in text)
{
Console.WriteLine(word);
}
}
}
internal static class StringMetoderRepetition
{
public static string[] Words(this string text)
{
// Skriv din kod här
}
}

Förväntad output:
Min
katt
är
hungrig

int page = 3;

Campus Mölndal .Net 21
Marcus Medina

Flytta om bokstäverna
Tips

// Då strängar är en array av char, kan man alltid omvandla
// strängen till en array av chars.
var hello="Hello";
var chars=hello.ToCharArray();
// Man kan alltid läsa av specifika tecken från en sträng
// genom att ange arrayposition på stringen.
// Tyvärr kan man inte skriva över en char på det sättet.
Console.WriteLine(hello[2]); // l
// För att kunna skriva om ett tecken i sen string måste vi först
// omvandla det till en char array, ändra det vi vill och sedan
// omvandla det tillbaka till en string.
char[] allChars=hello.toCharArray();
allChars[0]='Y';
string yello=new string(allChars);

I den här övningen ska du skapa en extension metod som rör om alla bokstäver i ett ord, detta gör den
slumpmässigt så bokstäverna kommer att hamna i slumpvalda positioner.

internal static class Program
{
static void Main()
{
// Exemplet kommer inte att bli likadan då bokstävernas
// position ska slumpas
Console.WriteLine("Frank Sinatra".Scramble());
}
}
internal static class StringMetoderRepetition
{
public static string Scramble(this string word)
{
// Skriv din kod här
}
}

Förväntad output: (exakt så blir det inte då det är slumpvalt)
tSiraraaarStS

int page = 4;

Campus Mölndal .Net 21
Marcus Medina

Left
Tips

// Substring(int start, int length) är en trevlig funktion i C#. Man talar
// om var den ska börja
// läsa i strängen och hur långt det ska läsa. Om man inte anger längd
// på texten så läser den resten av texten med startpunkt från start
// parametern.

Du ska nu göra en metod som plockar ut alla tecken i en viss sträng från given startpunkt. Left är en
sträng-funktion som finns i Visual Basic som jag alltid saknar i C#. Därför ska vi göra den nu. Man
anger hur många tecken man vill ha från vänster sida av texten och så klipper den av texten enligt de
angivna parametrarna. Du ska nu göra en extension metod som gör just det som Visual Basic gör.
Left

internal static class Program
{
static void Main()
{
var sup="Superman".Left(5);
Console.WriteLine(sup);
}
}
internal static class StringMetoderRepetition
{
public static string Left(this string text, int length)
{
// Skriv din kod här
}
}

Förväntad output:
Super

int page = 5;

Campus Mölndal .Net 21
Marcus Medina

Right
Right är en sträng-funktion som finns i Visual Basic som jag alltid saknar i C#. Därför ska vi göra den
nu. Man anger hur många tecken man vill ha från höger sida av texten och så klipper den av texten
enligt de angivna parametrarna. Du ska nu göra en extension metod som gör just det som Visual
Basic gör.
Right

internal static class Program
{
static void Main()
{
// Då det är en Extension så skriver vi det som om det vore en
// del av string.
var super="Superman".Right(3);
Console.WriteLine(super);
}
}
internal static class StringMetoderRepetition
{
public static string Right(this string text, int length)
{
// Skriv din kod här
}
}

Förväntad output:
man

int page = 6;

Campus Mölndal .Net 21
Marcus Medina

Initialer
Skapa en extension metod som ska hämta ut initialerna ur ett namn.
Initials

internal static class Program
{
static void Main()
{
Console.WriteLine("John Ryes-Davis".Initials());
Console.WriteLine("John F Kennedy".Initials());
Console.WriteLine("Marcus Marcelo Medina".Initials());
}
}
internal static class StringMetoderRepetition
{
public static string Initials(this string wholeName)
{
// Skriv din kod här
}
}

Förväntad output:
JRD
JFK
MMM

int page = 7;

Campus Mölndal .Net 21
Marcus Medina

Addera ett tal som är i en sträng
Tips

Int.TryParse ger oss en bool som berättar för oss om texten är nummerisk eller
inte.
Console.WriteLine("IsNummeric:" + Int.TryParse("101",out int something)); //
True
Console.WriteLine("IsNummeric:" + Int.TryParse("Katt",out int something)); //
False

Nu ska vi ta en sträng och addera den med en int, det ska inte fungera men vi gör som så att vi kollar
om strängen är ett tal. Om den är det, omvandlar vi det till en int och adderar int-värdet vi fick, sedan
omvandlar vi summan till en sträng igen och returnerar det. Varför gör man en sådan sak? Tja...
varför inte? :D
Add

internal static class Program
{
static void Main()
{
Console.WriteLine("5".Add(100));
Console.WriteLine("3".Add(7));
}
}
internal static class StringMetoderRepetition
{
public static string Add(this string text, int number)
{
// Skriv din kod här
}
}

Förväntad output:
105
10

int page = 8;

Campus Mölndal .Net 21
Marcus Medina

ToInt - Sträng till en int on the fly.
Skapa en extension-metod som omvandlar din sträng till en int. Så du slipper skriva int.Parse eller
int.TryParse 50000 gånger medan du skriver Consol-applikationer. Det är en trevlig funktion att ha.
Det gör kodningen enklare. ToInt finns i namespacet System.Web.WebPages, men det är frågan om
man vill ta in ett helt bibliotek med funktioner för webben till en consolapplikation, bara för att
använda en funktion. Då är det kanske enklare att göra det själv.
ToInt

internal static class Program
{
static void Main()
{
var bender1="10101";
Console.WriteLine(bender1.ToInt()+1);
}
}
internal static class StringMetoderRepetition
{
public static int ToInt(this string text)
{
// Skriv din kod här
}
}

Förväntad output:
10102

int page = 9;

Campus Mölndal .Net 21
Marcus Medina

IsNummeric - Ifall man undrar.
Skapa en extension-metod som kontrollerar om din sträng är numerisk. Det finns många olika sätt att
göra detta på, men ibland är de enklaste sätten de bästa.
IsNummeric

internal static class Program
{
static void Main()
{
var bender2="10101";
Console.WriteLine(bender2.IsNummeric());
Console.WriteLine("Katt".IsNummeric());
}
}
internal static class StringMetoderRepetition
{
public static bool IsNummeric(this string text)
{
// Skriv din kod här
}
}

Förväntad output:
True
False

int page = 10;

Campus Mölndal .Net 21
Marcus Medina

Textmallar.
Tips

//Console.WriteLine använder sig av StringFormat, därför kan vi skriva såhär
Console.WriteLine("Hej {0}, hoppas du mår {1}", "Peter", "OK");
// Hej Peter, hoppas du mår OK.
// "Peter" är på position {0} och "OK" på position {1}.
// String format kan skapa specialgjorda strängar till oss
var name="Jeppe";
var mood="fantastiskt bra";
var greeting = ("Hej {0}, hoppas du mår {1}",name, mood);
Console.WriteLine(greeting);
// Ordningen saker skrivs ut är oviktigt, bara ordningen som de lagts i är
viktig, man kan även använda samma
// parameter flera gånger

Ibland behöver man textmallar som man bara ska fylla i oh vara nöjd med. Ett bra exempel är när
man ska generera massmail och skicka samma meddelande till alla, men ändå med en personlig
touch.
Bästa {förnamn},
Vi vill nu informera er om att ni hur {Resultat} 10.000:- på lotteriet.
{TrevligKommentar} era vänner på Nitlotteriet.
{Förnamn} kan ersättas med vilkar namn som helst
{Resultat} kan vara vunnit eller förlorat
{TrevligKommentar} kan vara ”Ha en trevlig helg”, ”Ha det bra”, ”Gratulationer” mm
C# hjälper oss med en funktion som heter String.Format, den ersätter vissa nyckelord med de värden
vi skickar in. Den använder sig av värdenas positioner i samma form som en array, så första
parametern är {0}, andra parametern är {1} osv.

int page = 11;

Campus Mölndal .Net 21
Marcus Medina
I metoden du ska skapa, använd dig av String.Format för att formatera hälsningen.
BondGreeting

internal static class Program
{
static void Main()
{
var name="James";
var lastName="Bond";
var greeting = StringMetoderRepetition.BondGreeting(name, lastName);
Console.WriteLine(greeting);
}
}
internal static class StringMetoderRepetition
{
public static string BondGreeting(string name, string lastName)
{
// Skriv din kod här
}
}

Förväntad output:
My name is Bond, James Bond.

int page = 12;

Campus Mölndal .Net 21
Marcus Medina

SträngInformation
Tips

// string.Length ger dig längden på en sträng
var batman="Batman";
Console.WriteLine(Batman.Length);
// Då strängen är en char Array kan du du få en fram alla tecken i en sträng
bara du
// Talar om vilken position du vill läsa av.
// [x] väljer position från början av din sträng/array (börjar från 0)
// [^x] väljer position från början av din sträng/array (börjar från 1)
Console.WriteLine("Albus"[2]+"Dumbledore"[^2]); // br

Nu ska du skriva en extension-metod som ger viss information om din sträng. String klassen har
många trevliga metoder inbyggda och de är väldigt praktiska.
PrintStringInformation

internal static class Program
{
static void Main()
{
var poem = @"Oh, promise me that someday you and I
Will take our love together to some sky
Where we can be alone and faith renew
And find the hollows where those flowers grew
Those first sweet violets of early spring
Which come in whispers, thrill us both, and sing
Of love unspeakable that is to be;
Oh, promise me!Oh, promise me!
- Scott, Clement and De Koven, Reginald";
StringMetoderRepetition.PrintStringInformation(poem);
}
}
internal static class StringMetoderRepetition
{
public static void PrintStringInformation(string text)
{
// Skriv din kod här
}
}

Förväntad output:
Den består av 69 ord och 8 rader.
Den är 366 tecken lång och börjar på 'O' och slutar med 'd'.
Första ordet är 'Oh' och sista ordet är 'Reginald'

int page = 13;

Campus Mölndal .Net 21
Marcus Medina

Facit: String metoder repetition
Flytta om bokstäverna
Man kan lösa detta genom att använda Substring eller omvandla stringen till
en StringBuilder som faktiskt kan hantera att man skriver över array positionen
Scramble

public static string Scramble(this string word)
{
Random random = new Random();
int times = 5;
char[] array = word.ToCharArray();
for (int rounds = 0; rounds < times; rounds++)
{
for (int letter = 0; letter < array.Length; letter++)
{
int pos = random.Next(0, word.Length);
var tmp = array[letter];
array[letter] = word[pos];
array[pos] = tmp;
}
}
return new string(array);
}

Sträng till ord
Du ska nu skapa en extension metod som delar en text i antal ord, och skriver ett ord i taget, det
räcker med att splittra orden baserat på mellanslag. Punkter, frågotecken och andra tecken kan
skippas.
Words

public static string[] Words(this string text)
{
var words = text.Replace('?', ' '). // ersätt frågotecken med mellanslag
Replace('!', ' '). // ersätt utropstecken med mellanslag
Replace('.', ' '). // ersätt punkt med mellanslag
Replace(',', ' '). // ersätt komma med mellanslag
Replace(':', ' '). // ersätt kolon med mellanslag
Replace('-', ' ') // ersätt minustecken med mellanslag
.Trim() // Ta bort onödiga mellanslag i början och slutet
.Split(' '); // Dela i ord
return words;
}

int page = 14;

Campus Mölndal .Net 21
Marcus Medina

Left
Du ska nu göra en metod som plockar ut alla tecken i en viss sträng från given startpunkt. Left är en
sträng-function som finns i Visual Basic som jag alltid saknar i C#. Därför ska vi göra den nu. Man
anger hur många tecken man vill ha från vänster sida av texten och så klipper den av texten enligt de
angivna parametrarna. Du ska nu göra en extension metod som gör just det som Visual Basic gör.
Left

public static string Left(this string text, int length)
{
return text.Substring(0, length);
}

Right
Right är en sträng-function som finns i Visual Basic som jag alltid saknar i C#. Därför ska vi göra den
nu. Man anger hur många tecken man vill ha från vänster sida av texten och så klipper den av texten
enligt de angivna parametrarna. Du ska nu göra en extension metod som gör just det som Visual
Basic gör.
Right

public static string Right(this string text, int length)
{
return text.Substring(text.Length - length);
}

int page = 15;

Campus Mölndal .Net 21
Marcus Medina

Initialer
Skapa en extension metod som ska hämta ut initialerna ur ett namn.
Det är mer eller mindre samma sak som innan när vi delade upp texten i ord, men den gär gången
plockar vi bara ut de första bokstäverna.
Initials

public static string Initials(this string wholeName)
{
var split = wholeName.Replace('-', ' ').Replace('.', ' ').Split(' ');
var initals = "";
foreach (var name in split)
{
initals += name[0];
}
return initals.ToUpper();
}

Addera ett tal som är i en sträng
Nu ska vi ta en sträng och addera den med en int, det ska inte fungera men vi gör som så att vi kollar
om strängen är ett tal. Om den är det, omvandlar vi det till en int och adderar int-värdet vi fick, sedan
omvandlar vi summan till en sträng igen och returnerar det. Varför gör man en sådan sak? Tja...
varför inte? :D
Add

public static string Add(this string text, int number)
{
var num = 0;
var isNum = int.TryParse(text, out num);
if (isNum)
{
return (num + number).ToString();
}
else
{
return text;
}
}

int page = 16;

Campus Mölndal .Net 21
Marcus Medina

ToInt - Sträng till en int on the fly.
Skapa en extension-metod som omvandlar din sträng till en int. Så du slipper skriva int.Parse eller
int.TryParse 50000 gånger medan du skriver Consol-applikationer. Det är en trevlig funktion att ha.
Det gör kodningen enklare. ToInt finns i namespacet System.Web.WebPages, men det är frågan om
man vill ta in ett helt bibliotek med funktioner för webben till en consolapplikation, bara för att
använda en funktion. Då är det kanske enklare att göra det själv.
ToInt

public static int ToInt(this string text)
{
int.TryParse(text, out int num);
return num;
}

IsNummeric - Ifall man undrar.
Skapa en extension-metod som kontrollerar om din sträng är nummerisk. Det finns många olika sätt
att göra detta på, men ibland är de enklaste sätten de bästa.
_ (underscore) talar om för Visual Studio att vi inte bryr oss om resultatet.
IsNummeric

public static bool IsNummeric(this string text)
{
return int.TryParse(text, out _);
}

Textmallar.
Ibland behöver man textmallar som man bara ska fylla i oh vara nöjd med. Ett bra exempel är när
man ska generera massmail och skicka samma meddelande till alla, men ändå med en personlig
touch.
C# hjälper oss med en funktion som heter String.Format, den ersätter vissa nyckelord med de värden
int page = 17;

Campus Mölndal .Net 21
Marcus Medina
vi skickar in. Den använder sig av värdenas positioner i samma form som en array, så första
parametern är {0}, andra parametern är {1} osv.
I metoden du ska skapa, använd dig av String.Format för att formattera hälsningen.
BondGreeting

public static string BondGreeting(string name, string lastName)
{
// Att skapa en metod för detta är lite overkill, men det var är bra sätt
att testa det.
return string.Format("My name is{1}, {0}{1}.", name, lastName);
}

SträngInformation
Nu ska du skriva en extension-metod som ger viss information om din sträng. String klassen har
många trevliga metoder inbyggda och de är väldigt praktiska.
PrintStringInformation

public static void PrintStringInformation(string text)
{
var rows = 0;
foreach (var letter in text)
{
if (letter == '\n') rows++;
}
var words = text.Words();
var sb = new StringBuilder();
sb.AppendLine($"Den består av {words.Length} ord och {rows} rader.");
sb.AppendLine($"Den är {text.Length} tecken lång och börjar på '{text[0]}'
och slutar med '{text[^1]}'.");
sb.AppendLine($"Första ordet är '{words[0]}' och sista ordet är
'{words[^1]}'");
Console.WriteLine(sb.ToString());
}

int page = 18;
