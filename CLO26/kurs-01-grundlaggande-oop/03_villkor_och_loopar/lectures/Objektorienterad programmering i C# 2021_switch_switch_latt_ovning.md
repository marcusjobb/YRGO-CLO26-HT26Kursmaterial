---

title: Switch Lätt Övning
author: Marcus Ackre Medina
type: lecture
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Switch/Switch lätt övning.docx"
description: "I den här övningen ska vi skapa ett projekt som utvärderar en input genom Switch"
tags: ["conditions", "csharp", "git", "lätt", "switch", "test", "övning.docx"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Switch (lätt)

16 september 2021

Beskrivning:
I den här övningen ska vi skapa ett projekt som utvärderar en input genom Switch
Kursplanstermer som berörs av uppgiften:
Mål

För godkänt krävs

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Den studerande redogör för hur typer, variabler, uttryck,
villkorssatser och loopar används inom programmering.

Förstå och använda sig av datastrukturer inom
programmering.

Den studerande använder sig av datastrukturer i sin
mjukvaruutveckling.

Termer för övningen:


Switch – en snyggare form av if-else

Psuedokod:
1. Fråga användaren om ett tal mellan 1 och 50
2. Utvärdera talet
a. 11 ska ge svaret ”Talet är 11”
b. 22 ska ge svaret ”Talet är 22”
c. 33 ska ge svaret ”Talet är 33”
d. 44 ska ge svaret ”Talet är 44”
e. <1 ska ge svaret ”Talet är för litet”
f. <10 ska ge svaret ”Talet är mindre än 10”
g. <20 ska ge svaret ”Talet är mindre än 20”
h. <20 ska ge svaret ”Talet är mindre än 30”
i. <40 ska ge svaret ”Talet är mindre än 40”
j. >50 ska ge svaret ”Alldeles för högt tal!”

int page = 0;

Campus Mölndal .Net 21
Marcus Medina
Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.
Kodning:
Vi börjar med att skapa ett c#, .net core konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)
Kör den vanliga koden för att fråga om ett tal och omvandla det till en int
Kör en switch på din int
switch (mynumber)
{
case < 1: Console.WriteLine("Alldeles för litet tal"); break
default:
break;
}

Kör vidare med alla villkoren nämnda i pseudokoden
Testa att köra programmet och passa på att comitta ditt projekt till Git!

När du är klar med projektet, pusha allting till Github!

int page = 1;

Campus Mölndal .Net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Att använda Switch för att utvärdera specifika tal

int page = 2;
