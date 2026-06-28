---

title: Repetition For Loopar
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Repetition For-Loopar.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "exercise", "git", "loopar.docx", "methods", "repetition"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Repetition av forloopar
Beskrivning:

I den här övningen ska vi skapa en masa metoder med olika loopar, bara för att öva på hur loopar
fungerar.

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

Utveckla program med en tydligt objektorienterad struktur.

Utveckla program med en tydligt objektorienterad struktur.

Utveckla felfria fristående program.

Att skapa felfria fristående program.

int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Översikt
Beskrivning:...............................................................................................................................................0
Kursplanstermer som berörs av uppgiften:..............................................................................................0
Termer för övningen:................................................................................................................................1
Projektinstruktioner:................................................................................................................................2
Kodning:....................................................................................................................................................2
Repetition av for loopar........................................................................................................................2
En loop av chars................................................................................................................................3
Down we go......................................................................................................................................4
Start och stopp..................................................................................................................................4
Start och stopp 2...............................................................................................................................5
Antal namn som börjar med given bokstav......................................................................................6
Antal namn som slutar med given bokstav......................................................................................7
Namn bokstav för bokstav, baklänges..............................................................................................8
Asc iivalues........................................................................................................................................8
Facit: Repetition av for loopar..............................................................................................................9
Down we go......................................................................................................................................9
Start och stopp...............................................................................................................................10
Start och stopp 2.............................................................................................................................10
Antal namn som börjar med given bokstav....................................................................................11
Antal namn som slutar med given bokstav....................................................................................11
Asc iivalues......................................................................................................................................12

Termer för övningen:


For loopar – enklaste formen av upprepning inom kod. Man anger startpunkt, villkor för
loopen och slutpunkt.
Char – char är en typ som agerar som en int, men dess värden representeras av AscII
symboler.

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.

Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

Repetition av for loopar
I den här övningen ska vi träna på For loopar rent allmänt du kommer att få skapa olika typer av
loopar, för att vara säker på att du kan använda loopar på olika sätt. En for-loop kör tills villkoret
uppfylls, oftast baserar man villkoret på räknaren, men det är inte en lag. Hur som helst, övningarna
går ut på att skriva for loopar fram och tillbaka, utom de övningar där man läser en namnlista, för där
kan du använda foreach med gott samvete.
Exempel

for (int counter = 0; counter < length; counter++)
{
// Massor med kod
}
// Räknaren inne i for-loopen
for (int counter = 0; counter < length; )
{
counter++; // knasigt men det funkar
}
// Variabeln definieras innan loopen
int counter = 0; // Weird men det funkar det med
for (; counter < length; )
{
counter++; // knasigt men det funkar
}
// Evig loop
for (; ; )
{
// men varför? Använd hellre while(true)
}

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
En loop av chars
Tips

// För att veta ett teckens ascii värde kan du fråga char typen
var a=(int)'a';
var b='a'+'b'; // När man addera två chars behandlas de som int, och
resultatet blir(int)195

Bokstaven a har ascii värdet 96. Bokstaven A har ascii värdet 65.
Det blev så då de första datorerna hade bara stora bokstäver. Sen fylldes Ascii tabellen på med andra
tecken. Skriv en loop som skriver ut bokstäverna från a-z.
CharsLoop

static void Main()
{
CharsLoop();
}
public static void CharsLoop()
{
// Skriv din kod här
}

Förväntad output:
abcdefghijklmnopqrstuvwxyz

int page = 3;

Campus Mölndal .Net 21
Marcus Medina
Down we go...

Skapa en loop som räknar från given inparameter till 0.
CountDown

static void Main()
{
CountDown(10);
}
public static void CountDown(int start)
{
// Skriv din kod här
}

Förväntad output:
10..9..8..7..6..5..4..3..2..1

Start och stopp

Skapa en loop som räknar baklänges från given inparameter start till given inparameter stop.
CountDownFrom

static void Main()
{
CountDownFrom(5,1);
}
public static void CountDownFrom(int start, int stop)
{
// Skriv din kod här
}

Förväntad output:
5..4..3..2..1

int page = 4;

Campus Mölndal .Net 21
Marcus Medina
Start och stopp 2

Skapa en loop som räknar från given inparameter "start" till given inparameter "stop".
CountFrom

static void Main()
{
CountFrom(10,15);
}
public static void CountFrom(int start, int stop)
{
// Skriv din kod här
}

Förväntad output:
10..11..12..13..14..15

int page = 5;

Campus Mölndal .Net 21
Marcus Medina
Antal namn som börjar med given bokstav
Tips

// Skillnaden mellan name[0] eller name.Startswith().
If (namn.StartsWith('x')) DoSomething();
If (namn[0]=='x') DoSomething();
// Skillnaden är att om namn är tom kommer [0] att krascha,
// medan StartsWith kommer att returnera false.
// TLDT;
// StartsWith kan hantera strängar,
// [0] kollar ett tecken åt gången

Skapa en metod som tar emot en char och en array av namn, den ska sedan skriva ut alla namn som
börjar med den bokstaven, och tala om hur många de är. Använd params för att skriva dem
kommaseparerade så slipper du skapa en array.
ListNamesThatStartsWith

static void Main()
{
ListNamesThatStartsWith('B',"Batman","Superman","Borat");
}
public static void ListNamesThatStartsWith(char firstLetter, params string[]
names)
{
// Skriv din kod här
}

Förväntad output:
Batman
Borat
2

int page = 6;

Campus Mölndal .Net 21
Marcus Medina
Antal namn som slutar med given bokstav
Tips

// En sträng är en Char array. För att hitta sista tecknet i strängen
kan du använda någon av följande alternativ
// Sträng längden - 1 ger dig positionen för sista tecknet
if (name[name.length-1]=='x']) DoSomething();
// ^ symbolen räknar baklänges i positionerna, så ^1 betyder längd - 1
if (name[^1]=='x']) DoSomething();
// Substring är en speciell och trevlig funktion. Första parametern talar
om // var man ska börja och andra parametern hur många tecken man vill ha
// från strängden, om andra parametern inte finns så tar den resten av
// strängen efter startpunkten
if (name.SubString(name.length-1)=="x"]) DoSomething();
// så finns det självklart också EndsWith
if (Name.EndsWith('x')) DoSomething();

Skapa en metod som tar emot en char och en array av namn, den ska sedan skriva ut alla namn som
börjar med den bokstaven, och tala om hur många de är. Använd params för att skriva dem
kommaseparerade så slipper du skapa en array.
ListNamesThatEndsWith

static void Main()
{
ListNamesThatStartsWith('h',"Hulk","Spiderman","Flash","Green Arrow");
}
public static void ListNamesThatEndsWith(char lastLetter, params string[]
names)
{
// Skriv din kod här
}

Förväntad output:
Flash
1

int page = 7;

Campus Mölndal .Net 21
Marcus Medina
Namn bokstav för bokstav, baklänges

Skapa en loop som skriver ut ett namn framlänges och sedan baklänges.
Namescrambler

static void Main()
{
Namescrambler("Marcus");
}
public static void Namescrambler(string text)
{
// Skriv din kod här
}

Förväntad output:
MarcussucraM

Asc iivalues

Skapa en metod som tar emot en sträng, strängen ska sedan skrivas ut. En bokstav i taget och vid
bokstaven ska det stå vad bokstaven har för AscII värde.
AscIIValues

static void Main()
{
AscIIValues("Bruce Wayne");
}
public static void AscIIValues(string name)
{
// Skriv din kod här
}

Förväntad output:
char B AscII value is 66
char r AscII value is 114
char u AscII value is 117
char c AscII value is 99
char e AscII value is 101
char AscII value is 32
char W AscII value is 87
char a AscII value is 97
char y AscII value is 121
char n AscII value is 110
char e AscII value is 101

int page = 8;

Campus Mölndal .Net 21
Marcus Medina

Facit: Repetition av for loopar
En loop av chars

Bokstaven a har ascii värdet 96. Bokstaven A har ascii värder 65 Det blev så då de första datorerna
hade bara stora bokstäver. Sen fylldes Ascii tabellen på med andra tecken. Skriv en loop som skriver
ut bokstäverna från a-z.
Då chars lätt kan omvandlas till int, kan vi använda dem i loopen med, dock måste värdet från loopen
omvandlas till char för att kunna skrivas ut (int)i. Men för att göra saker ännu enklare kan vi kör
loopen som en char loop, för att loop i sin natur har beter sig som en int.
CharsLoop

public static void CharsLoop()
{
for (char i = 'a'; i <= 'z'; i++)
{
Console.Write(i);
}
}

Down we go...

Skapa en loop som räknar från given inparameter till 0.
Det är lätt att glömma att man börjar från startvärde, såvida inte man ska loopa en array för då ska
man börja med array.length-1. Sen måste villkoret jämföra mot ett värde som är lägre än startvärdet
och större eller lika med och slutvärdet, slutligen ska räknaren minskas.
CountDown

public static void CountDown(int start)
{
for (var counter = start; counter >= 0; counter--)
{
Console.WriteLine(counter);
}
}

int page = 9;

Campus Mölndal .Net 21
Marcus Medina
Start och stopp

Skapa en loop som räknar baklänges från given inparameter start till given inparameter stop.
CountDownFrom

public static void CountDownFrom(int start, int stop)
{
for (var counter = start; counter >= stop; counter--)
{
Console.WriteLine(counter);
}
}

Start och stopp 2

Skapa en loop som räknar från given inparameter "start" till given inparameter "stop".
CountFrom

public static void CountFrom(int start, int stop)
{
for (var counter = start; counter <= stop; counter++)
{
Console.WriteLine(counter);
}
}

int page = 10;

Campus Mölndal .Net 21
Marcus Medina
Antal namn som börjar med given bokstav

Skapa en metod som tar emot en char och en array av namn, den ska sedan skriva ut alla namn som
börjar med den bokstaven, och tala om hur många de är. Använd params för att skriva dem
kommaseparerade så slipper du skapa en array.
string.StartsWith är najs!
ListNamesThatStartsWith

public static void ListNamesThatStartsWith(char firstLetter, params string[]
names)
{
var counter = 0;
foreach (var name in names)
{
if (name.StartsWith(firstLetter))
{
counter++;
Console.WriteLine(name);
}
}
Console.WriteLine(counter);
}

Antal namn som slutar med given bokstav

Skapa en metod som tar emot en char och en array av namn, den ska sedan skriva ut alla namn som
börjar med den bokstaven, och tala om hur många de är. Använd params för att skriva dem
kommaseparerade så slipper du skapa en array.
EndsWith är rätt najs! Dock är det alltid bra att lära sig att använda char-positionerna också!
ListNamesThatEndsWith

public static void ListNamesThatEndsWith(char lastLetter, params string[]
names)
{
var counter = 0;
foreach (var name in names)
{
if (name.EndsWith(lastLetter))
{
counter++;
Console.WriteLine(name);
}
}
Console.WriteLine(counter);
}

int page = 11;

Campus Mölndal .Net 21
Marcus Medina
ListNamesThatEndsWith

Namn bokstav för bokstav, baklänges

Skapa en loop som skriver ut ett namn framlänges och sedan baklänges.
Namescrambler

public static void Namescrambler(string text)
{
Console.Write(text); // Varför krångla till det? ;)
for (var letter = text.Length - 1; letter >= 0; letter--)
{
Console.Write(text[letter]);
}
Console.WriteLine();
}

Asc iivalues

Skapa en metod som tar emot en sträng, strängen ska sedan skrivas ut. En bokstav i taget och vid
bokstaven ska det stå vad bokstaven har för AscII värde.
AscIIValues

public static void AscIIValues(string name)
{
for (var i = 0; i < name.Length; i++)
{
var current = name[i];
System.Console.WriteLine("char " + current + " AscII value is " +
(int)current);
}
}

int page = 12;
