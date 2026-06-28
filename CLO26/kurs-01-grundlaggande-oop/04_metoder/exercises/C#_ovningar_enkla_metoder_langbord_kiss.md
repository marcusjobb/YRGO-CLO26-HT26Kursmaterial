---

title: Långbord (kiss)
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Långbord (KISS).docx"
description: "I den här övningen ska vi skapa titta på olika sätt att lösa ett problem på"
tags: ["(kiss).docx", "csharp", "exercise", "git", "långbord", "methods"]
week_fit: []
---
Campus Mölndal – .net 21
Marcus Medina

Långbord
Beskrivning:
I den här övningen ska vi skapa titta på olika sätt att lösa ett problem på
En familj med fyra personer har ett fyrkantigt bord. Fyra personer får plats runt bordet när de sitter
på varsin sida. Till midsommar ställer familjen ihop sju sådana bord efter varandra till ett enda långt
bord. Hur många personer får plats runt detta långbord?
Termer för övningen:


KISS = Keep It Simple, Stupid!

Kodning:
Skriv kod som löser problemet innan du läser vidare på min kodlösning. Här är lite kodledtrådar
Normalt:
int PersonerPerBord = 4;
int antalBord = 1;

Fest:
int PersonerPerBord = 2;
int antalKantplatser = 2;
int antalBord = 7;

Hur många personer får plats runt detta långbord?

int page = 0;

Campus Mölndal – .net 21
Marcus Medina

Mitt sätt att räkna på
Sju bord gånger 4 personer – 2 platser gånger sju, plus två vid kanterna.
int PersonerPerBord = 4;
int antalBord = 7;
int totalPlatser = PersonerPerBord * antalBord;
int platserFörsvinner = PersonerPerBord / 2 * antalBord;
int antalKantplatser = 2;
int antalSittplatser = totalPlatser - platserFörsvinner + antalKantplatser;
Console.WriteLine($"Platser att sitta på när man har {antalBord} bord: {antalSittplatser}");

Min sambos sätt att lösa problemet
Sju bord i rad gånger 2 personer per bord, och två personer i kanterna. Alltså:
int antalBord = 7;
int antalSidoplatser = 2;
int antalKantplatser = 2;
int antalSittplatser = antalBord * antalSidoplatser + antalKantplatser;
Console.WriteLine($"Platser att sitta på när man har {antalBord} bord: {antalSittplatser}");

Hennes sätt var enklare och lättare att förstå sig på.
KISS (Keep It Simple, Stupid!) regeln gäller alltid! Krånga inte till saker mer än nödvändigt.
Din uppgift nu är att göra en snyggare version av detta. Skapa en metod som tar emot nödvändiga
parametrar och returnerar mängden sittplatser.
När du är klar med projektet, pusha allting till Github!

int page = 1;

Campus Mölndal – .net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Keep it simple.
2. Tänk igenomproblemlösningen innan du skriver koden
3. Använd vettiga variabelnamn

int page = 2;
