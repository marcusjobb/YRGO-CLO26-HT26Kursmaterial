---

title: Tågresa
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Tågresa.docx"
description: "Exempel på hur man räknar tid i timmar."
tags: ["csharp", "exercise", "git", "methods", "test", "tågresa.docx"]
week_fit: []
---
.net 21 – Marcus Medina

Tågresa
Beskrivning:
Exempel på hur man räknar tid i timmar.

Termer för övningen:




DateTime – DateTime klassen innehåller metoder för hantering och beräkning av tid och
datum. Vi kommer att använda
o Now() = dagens datum och tid, innehåller variabler för Month och Day
o Constructor (År, månad, dag, timme, minut, sekund) för att skapa en specifik
tidspunkt
TimeSpan
o Objekt som innehåller skillnaden mellan två DateTime
o Den har properties för
 Year
 Month
 Day
 Hour
 Minute
 Second

Problembeskrivning:
Ett tåg från Göteborg till Stockholm startar 8:24 och är framme 11:33. Hur lång tid tar resan
egentligen.

int page = 0;

.net 21 – Marcus Medina
Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)
Vi kan räkna ut detta på två sätt. Vi tar det fula sättet först.
// Definiera värden
int startTimme = 8;
int startMinut = 24;
int frammeTimme = 11;
int frammeMinut = 33;
// Dra av timmar först
int resTimmar = frammeTimme - startTimme;
// Dra av minuter sen
int resMinuter = frammeMinut - startMinut;
// Om minuter är mindre än noll, öka minuter med 60
// och minska timmen med 1
while (resMinuter < 0)
{
resMinuter += 60;
resTimmar--;
}
// Skriv ut resultatet
Console.WriteLine($"Resan tar {resTimmar:00}:{resMinuter:00}");

Och ett snyggare sätt
// Definiera värden
DateTime idag = DateTime.Now;
int startTimme = 8;
int startMinut = 24;
int frammeTimme = 11;
int frammeMinut = 33;
DateTime start = new DateTime(idag.Year, idag.Month, idag.Day, startTimme,startMinut, 00);
DateTime framme = new DateTime(idag.Year, idag.Month, idag.Day, frammeTimme,frammeMi
nut, 00);
TimeSpan resTid = framme - start;
// Skriv ut resultatet
Console.WriteLine($"Resan tar {resTid.Hours:00}:{resTid.Minutes:00}");

Din uppgift nu är att göra en snyggare version av detta. Skapa en metod som tar emot nödvändiga
parametrar och returnerar tiden det tar att resa.
Testa att köra programmet och passa på att att comitta ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 1;

.net 21 – Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1.
2.
3.
4.
5.
6.

Välj alltid det snygga sättet att koda.
Att jämföra tid är enklare med DateTime..
Jämförelse av DateTime ger oss ett TimeSpan-objekt
TimeSpan liknar DateTime men är inte ett DateTime.
:00 efter en variabel ger oss nollor framför ental.
Om en if-sats kan behöva upprepas, gör om det till en while.

int page = 2;
