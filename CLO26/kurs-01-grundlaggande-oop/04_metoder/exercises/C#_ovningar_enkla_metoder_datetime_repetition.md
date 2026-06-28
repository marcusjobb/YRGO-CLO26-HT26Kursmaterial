---

title: Datetime Repetition
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/DateTime repetition.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "datetime", "exercise", "git", "methods", "repetition.docx", "test"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

DateTime repetition

Klicka eller tryck här för att ange datum.

Beskrivning:
I den här övningen ska vi leka lite med DateTime, det finns en del roligt man kan göra med dem och
det är bra att lära sig hur man använder dem.

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

Förstå och använda sig av datastrukturer inom programmering.

Att använda datastrukturer i sin mjukvaruutveckling.

Termer för övningen:



DateTime – Objekt som används för att hålla koll på datum och tid, den håller även reda på
dagens datum och klockslag (från systemet).
TimeSpan – Hjälpobjekt till DateTime som håller koll på tid som förflutit mellan två
tidsperioder.

Innehållsförteckning
Beskrivning:...............................................................................................................................................0
Kursplanstermer som berörs av uppgiften:..............................................................................................0
Termer för övningen:................................................................................................................................0
Projektinstruktioner:................................................................................................................................1
Kodning:....................................................................................................................................................1
Date time övningar...............................................................................................................................1
Date in one week..............................................................................................................................2
Halloween weekday..........................................................................................................................2
Concepcion day.................................................................................................................................3
Information om idag.........................................................................................................................4
Facit: Date time övningar.....................................................................................................................5
Date in one week..............................................................................................................................5
Halloween weekday..........................................................................................................................5
Concepcion day.................................................................................................................................5
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

Date time övningar
I den här övningen ska vi kolla på DateTime. DateTime kan man använda till mycket.
Exempel

// Du instansierar en DateTime såhär
var date=new DateTime(2021,12,24); // Midnatt till Julafton!
var today = DateTime.Now(); // Dagens datum och klockslaget just nu
var bday = new DateTime("Sep 25, 1919");
// DateTime har också trevliga egenskaper som DayOfWeek som ger dig
// en enum med veckodagen
var XmasWeekday = new DateTime(DateTime.Now.Year, 12, 25).DayOfWeek;
// Du kan också få årets dagnummer med DayOfYear
var daysTillXmas = new DateTime(DateTime.Now.Year, 12, 25).DayOfYear;
// DateTime har en speciell ToString() som kan ta emot parametrar
// som talar om hur datumet ska skrivas ut
// yyyy-MM-dd = år-månad-dag = 2021-10-17
// dd MMMM yyyy = dag månad i textform år = 17 okt 2021
// Experimentera själv...
// Du kan lägga till dagar och år på ditt datum
var nextMonth = DateTime.Now().AddMonths(1);
var nextWeek = DateTime.Now.AddDays(7);
var LastWeek = DateTime.Now.AddDays(-7);
var nextHour = DateTime.Now.AddHours(7);
var TenMinutesAgo = DateTime.Now.AddMinutes(-10);
var InHalfAnHour = DateTime.Now.AddMinutes(30);
// Du kan jämföra datum såhär, när du jämför datum får du en TimeSpan
// och den har egenskaper som
// TodalDays, TotalHours, TotalMinutes, TotalSeconds
var daysTillXmas = (new DateTime(DateTime.Now.Year,12,24)DateTime.Now).TodalDays;

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
Date in one week

Skapa en metod som skriver ut dagens datum en vecka fram.
DateInOneWeek

static void Main()
{
DateInOneWeek()
}
public static void DateInOneWeek()
{
// Skriv din kod här
}

Förväntad output: (om idag är 17 okt 2021)
24 oktober 2021

Halloween weekday

Skapa en metod som skriver ut vilken veckodag Halloween blir i år.
HalloweenWeekday

static void Main()
{
HalloweenWeekday()
}
public static void HalloweenWeekday()
{
// Skriv din kod här
}

Förväntad output: (om i år är 2021)
Sunday

int page = 3;

Campus Mölndal .Net 21
Marcus Medina
Concepcion day

Skapa en metod som tar emot födelsedata och skriver ut datumet minus 9 månader.
Vi tar Veronica Maggios födelsedata som hon angav i låten "Kära mamma".
ConcepcionDay

static void Main()
{
// Mamma ville ta en taxi. Men pappa var för snål.
// Så de fick värma sig i varenda trapp.
// 1981, det var fredag.
// Men det skulle snart bli lördagen den 15 mars.
var maggio=new DateTime(1981,3,15);
ConcepcionDay(maggio);
}
public static void ConcepcionDay(DateTime birthday)
{
// Skriv din kod här
}

Förväntad output:
Född
: 15 Mar 1981
Befruktningsdatum : 15 Jun 1980

int page = 4;

Campus Mölndal .Net 21
Marcus Medina
Information om idag
Tips

//IsDaylightSavingTime() returnerar en bool som är true om det är sommartid
if(Datetime.Today.IsDaylightSavingTime());
{
Console.WriteLine("Sommartid");
}
else
{
Console.WriteLine("Vintertid");
}

Skriv en metod som ger information om dagens datum.
TodayInfo

static void Main()
{
TodayInfo();
}
public static void TodayInfo()
{
// Skriv din kod här
}

Förväntad output:
Datum
: 17 Oct 2021
Nummer
: 290
Veckodag : Sunday
Sommartid : True

int page = 5;

Campus Mölndal .Net 21
Marcus Medina

Facit: Date time övningar
Date in one week

Skapa en metod som skriver ut datum för idag om sju dagar.
DateInOneWeek

public static void DateInOneWeek()
{
var nextWeek = DateTime.Now.AddDays(7);
Console.WriteLine(nextWeek.ToString("dd MMMM yyyy"));
}

Halloween weekday

Skapa en metod som skriver ut vilken veckodag Halloween blir iår.
HalloweenWeekday

public static void HalloweenWeekday()
{
var halloween = new DateTime(DateTime.Now.Year, 10, 31).DayOfWeek;
Console.WriteLine(halloween);
}

Concepcion day

Skapa en metod som tar emot födelsedata och skriver ut datumet minus 9 månader. Vi tar Veronica
Maggios födelsedata som hon angav i låten "Kära mamma".
Om hon inte bars i exakt 9 månader så är det nog så att hon blev till på midsommarafton, då den
inträffade den 19e juni.
ConcepcionDay

public static void ConcepcionDay(DateTime birthday)
{
var concepcion = birthday.AddMonths(-9);
Console.WriteLine("Född
: " + birthday.ToString("dd MMM
yyyy"));
Console.WriteLine("Befruktningsdatum : " + concepcion.ToString("dd MMM
yyyy"));
}

int page = 6;

Campus Mölndal .Net 21
Marcus Medina
ConcepcionDay

Information om idag

Skriv en metod som ger information om dagens datum.
TodayInfo

public static void TodayInfo()
{
var today = DateTime.Now;
Console.WriteLine("Datum
: " + today.ToString("dd MMM yyyy"));
Console.WriteLine("Nummer
: " + today.DayOfYear);
Console.WriteLine("Veckodag : " + today.DayOfWeek.ToString());
Console.WriteLine("Sommartid : " + today.IsDaylightSavingTime());
}

Testa att köra programmet och passa på att pusha ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 7;
