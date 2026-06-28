---

title: Switch Svår Övning
author: Marcus Ackre Medina
type: lecture
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Switch/Switch svår övning.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["conditions", "csharp", "git", "svår", "switch", "test", "övning.docx"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Switch (svår)
Beskrivning:

I den här övningen ska vi skapa ett projekt som utvärderar en input genom Switch men som
använder text för att generera informationen som Switch behöver.
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


Switch – en snyggare form av if-else
Omvandla sträng till enum är inte det enklaste (se svår enum övning)

Psuedokod:
1.
2.
3.
4.
5.
6.

Skapa en enum med alla månader
Fråga användaren om en månad
Omvandla input texten till en månads-enum
Kör en switch på ditt enum
Skriv ut helg och högtidsdagar för given månad
För tips kolla in: https://www.holidayscalendar.com/categories/weird/

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
Skapa en enum för månader
enum Months
{
Januari, Februari, Mars,
April, May, Juni, Juli,
Augusti, September, Oktober,
November, December
}

Kör en input för att få reda på vilken månad användaren vill se
Dubbelkolla att det är rätt stavat och stor bokstav i början (om du har stor bokstav på ditt enum)
Omvandla din sträng till enum värde
Kör nu en switch på ditt enum värde
switch (month)
{
case Months.Januari:
Console.WriteLine(" 1 - Nyår!");
break;
case Months.Februari:
break;
case Months.Mars:
Console.WriteLine("14 - Pi dagen! 3.14");
break;
case Months.April:
break;
case Months.May:
break;
case Months.Juni:
break;
case Months.Juli:
Console.WriteLine(" 1 - Nu är det juli igen!");
Console.WriteLine("20 - Marcus blir ännu äldre!");
break;
case Months.Augusti:
break;
case Months.September:
break;

int page = 1;

Campus Mölndal .Net 21
Marcus Medina
case Months.Oktober:
Console.WriteLine("31 - Jul oktalt (oct 31 = 25 dec)");
break;
case Months.November:
break;
case Months.December:
Console.WriteLine("23 – Handla julklappar i sista minuten dagen");
Console.WriteLine("24 - Julafton");
break;
default:
break;
}

Ta inspiration av sidan https://www.holidayscalendar.com/categories/weird/ och skriv in lite
högtider för varje månad.
Testa att köra programmet och passa på att comitta ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Att switch är roligt att använda tillsammans med enums
2. Att omvandla till enums från text är krångligt men värt besväret
3. Att det finns massvis med helgalna högtider att fira varje dag!

int page = 3;
