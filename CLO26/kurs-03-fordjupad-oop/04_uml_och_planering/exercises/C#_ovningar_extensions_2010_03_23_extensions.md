---

title: 2010 03 23 Extensions
author: Marcus Ackre Medina
type: exercise
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Extensions/2010-03-23 Extensions.pdf"
description: "Kluriga kodsnuttar för kreativa kodare"
tags: ["csharp", "exercise", "extensions.pdf", "oop"]
week_fit: []
---
Kluriga kodsnuttar för kreativa kodare
Extensions

March 19, 2010
Authored by: Marcus Medina

Inledning .......................................................................................................................................................... 2
Nummerologi.................................................................................................................................................... 3
Programregler............................................................................................................................................... 3
programmet ................................................................................................................................................. 4
metoder att komma ihåg .................................................................................................................................. 8
Andra tillämpningar .......................................................................................................................................... 8
Ett annat förslag............................................................................................................................................ 9

1

INLEDNING
I det här häftet ska vi dyka ner i den ockulta pseudovetenskapens värld. Inte för att bli ockulta programmerare,
men för att leka med extensions.
Extensions är ett sätt att bygga ut .net så att den får funktioner som vi vill ha.
Detta är ett måste att förstå om man vill förstå vad LINQ är för något och vad man kan göra om det, mer om
LINQ kommer i ett annat häfte.

2

NUMMEROLOGI
Nummerologi är tron på att allt styrs av matematik. Ens personlighet och livsöde bestäms av namnet man har.
Konsonantena har en betydelse, vokalerna har en betydelse. Födelsedatan har också betydelse.
Detta har sitt ursprung i Judiska Kabbalan som fortfarande har många anhängare, av någon konstig orsak så är
Hollywood kändisar förtjusta i det. – exempelvis Elvis, Dan Aykroyd, Madonna och andra..
Varje bokstav har ett värde, alla värdena slås ihop och man får ut en summa. Den summan slår man ihop tills
man har ett värde som är mindre än 9, eller 11 eller 22.
Vare sig man vill tro på det eller inte så är det roligt att räkna ut det, speciellt med C#.

PROGRAMREGLER
Talet får inte överstiga 9, såvida inte det är 11 eller 22.
Varje slutsumma har en betydelse enligt tabellen nedan.
Värde
1
2
3
4
5
6
7
8
9
11
22

Tal
1
2
3
4
5
6
7
8
9
11
22

Tecken
1JSAÅÄ
2BT
3CLUÜ
4DM
5 N W SE
6FXOÖ
7GPY
8HQZ
9RI
K
V

Betydelse
Initiativrik, pionjär, stark personlighet
Diplomati, samarbete, följsam
Kommunikation, inspiration, optimism
Praktisk, uthållig, målmedveten, ärlig
Frihet, mänsklighet, livslust, förändring
Lojalitet, kärlek, hem och familj
Analys, djup, söker kunskap & det okända
Balans, harmoni, stark, effektiv
Konst, känslor, resor, vidsynt, ombytlig
Fantasi, ideal, visioner, ledare
Globalt inriktad ledare, helhet, kraft

I mitt fall skulle det bli
Karmavägen, eller kunskaper under livet:
1970 06 20 blir alltså 1+9+7+0+0+6+2+0 = 25 = 2+5 = 7 = Analys, djup, söker kunskap & det okända
3

Ser sig själv som (konsonanter)
Marcelo Medina = 27 = 9 = Konst, känslor, resor, vidsynt, ombytlig
Ses av andra som (vokaler)
Marcelo Medina = 32 = 5 = Frihet, mänsklighet, livslust, förändring
Personligen tycket inte jag att det stämmer, men det är beräkningen som är det roliga 

PROGRAMMET
I detta exempel ska vi använda Extensions. Det är ett sätt att anpassa .net till att fungera som vi vill.
String classen ska få en två metoder för att räkna ut värdet på namnet. En för vokaler och en för konsonanter.
Int klassen ska få en metod som omvandlar för stora tal till mindre, enligt nummerologins regler.
Nu skapar vi en class för våra extensions. Observera att varken klassen eller namespacet behöver heta
extension. Men vi gör det iallafall.
Vi gör programmet i consolfönstret för att slippa kludda med formulär. Skapa ett projekt kallat ”Nummerologi”
och lägg till en class kallad MyExtensions. Classen ska vara statisk och metoderna också!
static class MyExtensions
{
public static int Nummerologic(this int value)
{
//Omvandla talet till text
string val = value.ToString();
//Nollställ slutsumman
int total = 0;
for (int i = 0; i < val.Length; i++)
{
//Addera summan med varje siffra i talet
total += int.Parse(val[i].ToString());
}
//Om slutsumman är större än 9 och inte lika med 11 eller 22
//så räkna ihop det nya resultatet.
if (total > 9 && total!=11 && total!=22)
total = Nummerologic(total);
return total;
}
}
Det är allt som behövs. Nu kan skriva exempelvis
int Elvis = 19350108;
Console.WriteLine(Elvis.Nummerologic());
Vilket skulle ge oss värdet 9 = Konst, känslor, resor, vidsynt, ombytlig;

4

Nu ska vi räkna ut bokstävernas värden.
private static int Nummerologic(string[] värden, string value)
{
//Gör om namet till versaler
string upper = value.ToUpper();
//Nollställ summan
int summa = 0;
//Loopar igenom en array med bokstäver och siffror.
//När den hittar en matchning till den nuvaranden
//bokstaven i namnet så läggs värdet till i summan
for (int i = 0; i < upper.Length; i++)
{
for (int v = 0; v < värden.Length; v++)
{
if (värden[v].IndexOf(upper[i]) >= 0)
{
summa += v;
break;
}
}
}
//Se till att talet inte överstiger 9, 11 eller 22
return summa.Nummerologic();
}
Detta är en privat metod som kommer att anropas av de andra nummerologi metoderna.
Nu skapar vi två extension metoder som string kommer att utökas med
public static int ToNummerologicCons(this string value)
{
string[] värden = {
"", "1JS", "2BT", "3CL", "4DM",
"5NW", "6FX", "7GP", "8HQZ", "9R",
"", "K", "V" };
return Nummerologic(värden, value);
}
public static int ToNummerologicVocs(this string value)
{
string[] värden = {
"", "1AÅÄ", "2", "3UÜ", "4", "5E",
"6OÖ", "7Y", "8", "9I"};
return Nummerologic(värden, value);
}
Metoderna skapar en array med bokstäver som ska jämföras och räknas ut. Vissa strängar i arrayen är tomma,
för att de inte ska räknas in, men ändå ta upp en plats. Exempelvis position noll i arrayen är tom, för den ska
inte användas då det är meningslöst att addera med noll. (Loopen som räknar ut värdena adderas arrayradens
postition till summan, då den är samma som bokstavens värde.)

5

Nu kan vi ändra main() till detta
{
int Elvis = 19350108;
string ElvisP = "Elvis Presley";
Console.WriteLine(ElvisP);
Console.WriteLine("Karmaväg
: {0}", Elvis.Nummerologic());
Console.WriteLine("Ser sig som: {0}", ElvisP.ToNummerologicCons());
Console.WriteLine("Ses som
: {0}", ElvisP.ToNummerologicVocs());
}
Som ger oss

urskriften

Bra, nu ska vi bara tolka det också.
Vi gör ännu en extension till Int och i den lägger vi bara in en switch och returnerar en string baserad på talet
som skickades in. Lägg in följande metod i Extension classen.
public static string NummerologicTolk(this int value)
{
string tolkning = "";
switch (value)
{
case 1:
tolkning = "Initiativrik, pionjär, stark personlighet";
break;
case 2:
tolkning = "Diplomati, samarbete, följsam";
break;
case 3:
tolkning = "Kommunikation, inspiration, optimism";
break;
case 4:
tolkning = "Praktisk, uthållig, målmedveten, ärlig";
break;
case 5:
tolkning = "Frihet, mänsklighet, livslust, förändring";
break;
case 6:
tolkning = "Lojalitet, kärlek, hem och familj";
break;
case 7:
tolkning = "Analys, djup, söker kunskap & det okända";
break;
6

case 8:
tolkning = "Balans, harmoni, stark, effektiv";
break;
case 9:
tolkning = "Konst, känslor, resor, vidsynt, ombytlig";
break;
case 11:
tolkning = "Fantasi, ideal, visioner, ledare";
break;
case 22:
tolkning = "Globalt inriktad ledare, helhet, kraft";
break;
default:
tolkning = "??";
break;
}
return tolkning;
}
Detta gör att vi kan ändra main() till följande
int Elvis = 19350108;
string ElvisP = "Elvis Presley";
Console.WriteLine(ElvisP);
int karma = Elvis.Nummerologic();//Hämta födelsesiffrans värde
int ser = ElvisP.ToNummerologicCons(); //Hämta konsonanternas värde
int ses = ElvisP.ToNummerologicVocs(); //Hämta vokalernas värde
Console.WriteLine("Karmaväg
: {0} - {1}",
karma,karma.NummerologicTolk());
Console.WriteLine("Ser sig som: {0} - {1}", ser, ser.NummerologicTolk());
Console.WriteLine("Ses som
: {0} - {1}", ses, ses.NummerologicTolk());
Då får vi fram siffrorna och tolkningen.
Med extensions så slapp vi instansiera en class för att kunna nå de specifika metoderna, vi slapp även att
anropa metoder i någon statisk class genom att ange classnamn.metod(). Istället ökade vi på metoderna i redan
existerande typer och kunde därmed göra mainmetoden väldigt enkel och lätt att förstå.
Detta kunde ha gjorts med Arv men då skulle vi fått använda de nya klasserna istället för Int och String.

7

METODER ATT KOMMA IHÅG
Metodanrop
public static string Trixa(this int
value)
{
}
public static string Trixa(this
string value)
{
}

Förklaring
Metod som är en extension till int

Metod som är en extension till string

ANDRA TILLÄMPNINGAR
Med extensions kan man få snyggare kod och göra programmerandet enklare för sig själv. Man kan bygga ut
alla typer på detta sätt. Även om detta kan verka märkligt så är det ett ganska vanligt fenomen i Flash,
Javascript, PHP och andra sådana språk.
Om man sysslat med Visual Basic eller ASP Classic så känner man nog igen metoderna Left, Right och Mid som
man använder istället för substring. Vill man ha dem metoderna tillbaka kan man göra såhär
public static string Mid(this string value, int Start)
{ return Mid(value, Start, value.Length-Start); }
public static string Mid(this string value, int Start, int Length)
{return value.Substring(Start, Length);}
public static string Left(this string value, int Length)
{return value.Substring(0, Length);}
public static string Right(this string value, int Length)
{return value.Substring(value.Length-Length,Length);}

8

ETT ANNAT FÖRSLAG
Om du vill fortsätta med att programmera pseudovetenskap så kan du prova med astrologi. Nu när du sett hur
man räknar ut nummerologi, så borde Astrologi vara enkelt? Åtminstone soltecknet kan man räkna ut utan
planettabeller.
Soltecken
Väduren
Oxen
Tvillingarna
Kräftan
Lejonet
Jungfrun
Vågen
Skorpionen
Skytten
Stenbocken
Vattumannen
Fiskarna

Datum
21 mars - 20 april
20 april - 21 maj
21 maj - 22 juni
22 juni - 23 juli
23 juli - 23 augusti
23 augusti - 23 september
23 september - 23 oktober
23 oktober - 23 november
23 november - 22 december
22 december - 20 januari
20 januari - 19 februari
19 februari - 21 mars

Numerisk datum
0321 – 0420
0420 – 0521
0521 – 0622
0622 – 0723
0723 – 0823
0823 – 0923
0923 – 1023
1023 – 1123
1123 – 1222
1222 – 1231, 0101 – 0120
0120 – 0219
0219 – 0321

I den här sidan finner du mer information om tolkningen
http://www.astrologi.nu/horoskop/22-zodiaken.html

9
