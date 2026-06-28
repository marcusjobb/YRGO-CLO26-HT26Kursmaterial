---

title: Repetition While
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Repetition While.docx"
description: "I den här övningen ska vi testa lite while loopar."
tags: ["csharp", "exercise", "git", "methods", "repetition", "test", "while.docx"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Repetition While
Beskrivning:
I den här övningen ska vi testa lite while loopar.

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

Namngivning och kodstruktur av klasser, metoder och
variabler i objektorienterade program används.

Termer för övningen:


Nästlad loop – Det är en loop i en annan loop. Om ytter loopen upprepar sig 10 gånger
kommer innerloopen att köras 10 gånger, och innerloopen i sin tur kommer att köra de
gånger den ska. Så koden i den inre loopen körs 10*x antal gånger.

Innehållsförteckning
Beskrivning:...............................................................................................................................................0
Kursplanstermer som berörs av uppgiften:..............................................................................................0
Termer för övningen:................................................................................................................................0
Projektinstruktioner:................................................................................................................................1
Kodning:....................................................................................................................................................1
While loopar.........................................................................................................................................1
Gissa ett tal mellan 1 och 10.............................................................................................................2
Nästlad loop......................................................................................................................................3
Count up or down.............................................................................................................................4
Facit: While loopar................................................................................................................................5
Gissa ett tal mellan 1 och 10.............................................................................................................5
Nästlad loop......................................................................................................................................5
Count up or down.............................................................................................................................6

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

While loopar
Nu ska vi kolla lite på While Loopar.
Exempel

// While loopas tills villkoret uppfylls, den behöver ingen räknare. Bra att
ha när man inte vet hur många gånger man ska loopa.
// I stort sett fungerar while loopar som for loopar.
for (int counter=0; counter<10; counter++)
{
DoSomething();
}
int counter=0;
while (something<10)
{
DoSomething();
counter++;
}
bool keepGoing=true;
while (keepGoing)
{
// Massor med kod
if (something is as it should be) keepGoing=false;
// break; fungerar också bra
}

int page = 1;

Campus Mölndal .Net 21
Marcus Medina
Gissa ett tal mellan 1 och 10
Tips

// Glöm inte att initiera din slumpgenerator
var random = new Random(); // Instansiera generatorn
var randNumber = random.Next(1, max + 1); // Hämta ett slumptal mellan 1 och
max.

Slumpa fram ett tal mellan ett och tio och låt användaren gissa det. Fråga om en gissning och
omvandla input till en int, som du sedan jämför med slumptalet som valdes i början av programmet.
På metodhuvudet står det (int max=10). Det betyder att om metoden anropas utan int parameter, så
kommer int parametern att bli 10.
Man kan anropa metoden med eller utan inparametrar.
GuessMyNumber

static void Main()
{
GuessMyNumber();
GuessMyNumber(15);
}
public static void GuessMyNumber(int max = 10)
{
// Skriv din kod här
}

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
Nästlad loop

Nu ska du skapa en nästlad while loop och skriva ut värdena i looparna.
Loopa mellan 1 och max i båda looparna.
NestledLoops

static void Main()
{
NestledLoops(3);
}
public static void NestledLoops(int max = 3)
{
// Skriv din kod här
}

Förväntad output:
Loop värde: 1:1
Loop värde: 1:2
Loop värde: 1:3
Loop värde: 2:1
Loop värde: 2:2
Loop värde: 2:3
Loop värde: 3:1
Loop värde: 3:2
Loop värde: 3:3

int page = 3;

Campus Mölndal .Net 21
Marcus Medina
Count up or down

Skapa en metod som tar emot två parametrar, start och stopp. Sedan ska du loopa från start till
stopp. Om start är större än stopp ska du självklart loopa baklänges, om start är mindre ska du loopa
framlänges.
Tips! Du kan göra allt med en while sats och två if-satser
CountUpOrDown

static void Main()
{
test.CountUpOrDown(4, 7);
test.CountUpOrDown(6, 2);
}
public static void CountUpOrDown(int start, int stop)
{
// Skriv din kod här
}

Förväntad output:
Loop: 4 - 7
Counter = 4
Counter = 5
Counter = 6
Counter = 7
Loop: 6 - 2
Counter = 6
Counter = 5
Counter = 4
Counter = 3
Counter = 2

int page = 4;

Campus Mölndal .Net 21
Marcus Medina

Facit: While loopar
Gissa ett tal mellan 1 och 10

Slumpa fram ett tal mellan ett och tio och låt användaren gissa det. På metodhuvudet står det (int
max=10). Det betyder att om metoden anropas utan int parameter, så kommer int parametern att bli
10. Man kan anropa metoden med eller utan inparametrar.
Lägg till att den ska tala om ifall man gissat för högt eller för lågt,
så har du ett roligt spel ;)
GuessMyNumber

public static void GuessMyNumber(int max = 10)
{
var random = new Random();
var randNumber = random.Next(1, max + 1);
var guess = 0;
while (guess != randNumber)
{
Console.WriteLine($"Gissa ett tal mellan 1 och {max}");
var input = Console.ReadLine();
_ = int.TryParse(input, out guess);
}
}

Nästlad loop

Nu ska du skapa en nästlad while loop och skriva ut värdena i looparna. Loopa mellan 1 och max i
båda looparna.
NestledLoops

public static void NestledLoops(int max = 3)
{
int counterA = 0;
while (counterA++ < max)
{
int counterB = 0;
while (counterB++ < max)
{
Console.WriteLine($"Loop värde: {counterA}:{counterB}");
}
}
}

int page = 5;

Campus Mölndal .Net 21
Marcus Medina

Count up or down

Skapa en metod som tar emot två parametrar, start och stopp. Sedan ska du loopa från start till
stopp. Om start är större än stopp ska du självklart loopa baklänges, om start är mindre ska du loopa
framlänges.
Tips! Du kan göra allt med en while sats och två if-satser
CountUpOrDown

public static void CountUpOrDown(int start, int stop)
{
int diff = 1;
if (start > stop) diff = -1;
int counter = start;
Console.WriteLine($"Loop: {start} - {stop}");
while (true)
{
Console.WriteLine("Counter = " + counter);
counter += diff;
if (diff == 1 && counter > stop) break;
if (diff == -1 && counter < stop) break;
}
Console.WriteLine();
}

int page = 6;
