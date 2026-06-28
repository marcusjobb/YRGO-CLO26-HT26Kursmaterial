---

title: Enum Övning (lätt)
author: Marcus Ackre Medina
type: lecture
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Enum/Enum övning (lätt).docx"
description: "I den här övningen ska vi skapa ett projekt som visar upp veckodagar."
tags: ["(lätt).docx", "csharp", "datastrukturer", "enum", "git", "test", "övning"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Enum (lätt)

16 september 2021

Beskrivning:
I den här övningen ska vi skapa ett projekt som visar upp veckodagar.
Kursplanstermer som berörs av uppgiften:
Mål

För godkänt krävs

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Den studerande redogör för hur typer, variabler, uttryck,
villkorssatser och loopar används inom programmering.

Kunskap kring namngivning och kodstruktur av klasser,
metoder och variabler i objektorienterade program.

Den studerande redogör för hur namngivning och kodstruktur av
klasser, metoder och variabler i objektorienterade program används.

Förstå och använda sig av datastrukturer inom
programmering.

Den studerande använder sig av datastrukturer i sin
mjukvaruutveckling.

Termer för övningen:


Enum – lista med int värden som har fått ett namn, exempelvis Enum Veckodagar
{ Måndag=1, Tisdag, Onsdag, Torsdag, Fredag, Lördag, Söndag }

Psuedokod:
1.
2.
3.
4.

Fråga användaren om ett nummer mellan 1 och 7
Omvandla input från sträng till int
Omvandla talet från int till Veckodagar enum
Använd ToString för att skriva ut veckodagen

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
Ska ett enum för veckodagarna, för att göra det enklare för användarna så gör att den ska börja på
ett.
enum Veckodagar
{
Måndag = 1,
Tisdag,
Onsdag,
Torsdag,
Fredag,
Lördag,
Söndag
}

Enum skapas alltid utanför klassen, då det är en egen typ. Man kan skapa det innanför klassen med
iofs men i princip hamnar den utanför klassen, men inne i namespacet.
Fråga användaren om ett tal mellan 1-7
Kör int.TryParse för att få ett nummeriskt värde
Kör (Veckodag) på ditt int värde
och skriv ut resultatet

Testa att köra programmet och passa på att comitta ditt projekt till Git!

int page = 1;

Campus Mölndal .Net 21
Marcus Medina
När du är klar med projektet, pusha allting till Github!

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Att använda enum
2. Att omvandla mellan enum och tal

int page = 3;
