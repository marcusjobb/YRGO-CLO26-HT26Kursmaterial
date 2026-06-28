---

title: Repetition Switch
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Repetition Switch.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "exercise", "git", "methods", "repetition", "switch.docx", "test"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Repetition Switch
Beskrivning:

I den här övningen ska vi repetera lite switch satser. De är fina switcharna och de gör koden mycket
enklare att läsa.

Kursplanstermer som berörs av uppgiften:
Mål

Vad du ska lära dig

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

Termer för övningen:


Switch – en snyggare form av en if-sats.

Pseudokod:
Switch(villkor)
{
Så länge villkoret är sann kommer den att köras
}

Innehållsförteckning
Beskrivning:...............................................................................................................................................0
Kursplanstermer som berörs av uppgiften:..............................................................................................0
Termer för övningen:................................................................................................................................0
Pseudokod:...............................................................................................................................................0
Projektinstruktioner:................................................................................................................................1
Kodning:....................................................................................................................................................1
Switch övningar....................................................................................................................................1
Swedish dayname.............................................................................................................................2
Facit: Switch övningar...........................................................................................................................2
Swedish month name.......................................................................................................................2
Swedish dayname.............................................................................................................................3
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

Switch övningar
If i all ära, men switch gör koden så mycket snyggare. Så nu ska vi kolla på switchar!
Exempel

switch (number)
{
case 1: Console.WriteLine("Ett"); break;
case 2: Console.WriteLine("Två"); break;
case 3: Console.WriteLine("Tre"); break;
default: : Console.WriteLine("Nåt annat"); break;
}

Swedish month name

Nu ska du skriva en metod som omvandlar din int (1-12) till en månad i textform
SwedishMonthName

static void Main()
{
SwedishMonthName(8);
SwedishMonthName(10);
}
public static void SwedishMonthName(int month)
{
// Skriv din kod här
}

Förväntad output:
Augusti
Oktober

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

Swedish dayname

Nu ska du skriva en metod som omvandlar din int (1-7) till en dag i textform
SwedishDayname

static void Main()
{
SwedishDayName(2);
SwedishDayName(4);
}
public static void SwedishDayName(int day)
{
// Skriv din kod här
}

Förväntad output:
Tisdag
Torsdag

Facit: Switch övningar
Swedish month name

Nu ska du skriva en metod som omvandlar din int (1-12) till en månad i textform. Du kan också prova
med att göra om det till en Switch expression ;)
SwedishMonthName

public static void SwedishMonthName(int month)
{
string monthName;
switch (month)
{
case 1: monthName = "Januari"; break;
case 2: monthName = "Februari"; break;
case 3: monthName = "Mars"; break;
case 4: monthName = "April"; break;
case 5: monthName = "Maj"; break;
case 6: monthName = "Juni"; break;
case 7: monthName = "Juli"; break; // Nu är det juli igen!
case 8: monthName = "Augusti"; break;
case 9: monthName = "September"; break;
case 10: monthName = "Oktober"; break;

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
SwedishMonthName

case 11: monthName = "November"; break;
case 12: monthName = "December"; break;
default: monthName = "Uh?"; break;
}
Console.WriteLine(monthName);
}

Swedish dayname

Nu ska du skriva en metod som omvandlar din int (1-7) till en dag i textform.
Du kan också prova med att göra om det till en Switch expression ;)
SwedishDayname

public static void SwedishDayName(int day)
{
string dayName;
switch (day)
{
case 1: dayName = "Måndag"; break;
case 2: dayName = "Tisdag"; break;
case 3: dayName = "Onsdag"; break;
case 4: dayName = "Torsdag"; break;
case 5: dayName = "Fredag"; break;
case 6: dayName = "Lördag"; break;
case 7: dayName = "Söndag"; break;
default: dayName = "Uh?"; break;
}
Console.WriteLine(dayName);
}

Testa att köra programmet och passa på att pusha ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 3;
