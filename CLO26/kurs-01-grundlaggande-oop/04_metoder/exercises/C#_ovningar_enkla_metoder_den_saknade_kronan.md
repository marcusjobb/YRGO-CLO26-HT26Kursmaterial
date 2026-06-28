---

title: Den Saknade Kronan
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Den saknade kronan.docx"
description: "I den här övningen ska du få kod som inte fungerar, och du ska få nöjet att korrigera det. Det är inte"
tags: ["csharp", "den", "exercise", "git", "kronan.docx", "methods", "saknade", "test"]
week_fit: []
---
Campus Mölndal – .net 21
Marcus Medina

Den saknade kronan
Beskrivning:
I den här övningen ska du få kod som inte fungerar, och du ska få nöjet att korrigera det. Det är inte
så enkelt som att det är felskriven kod, det är snarare så att logiken är felaktig.

Psuedokod:
1. Definiera värden
2. Beräkna betalningen
3. Bekräfta att det stämmer

int page = 0;

Campus Mölndal – .net 21
Marcus Medina
Problembeskrivning:
Kalle, Pelle och Tjalle ska dela på en påse bullar. Den kostar 25 kronor. De har var sin 10 kronors mynt
och ingen swish eller kort tillgänglig.
De betalar och får 5 kronor tillbaka. De delar upp det så att var och en får en krona och de två
resterande bestämmer de sig för att donera till Röda Korsets sparbössa som finns i butiken.
Senare när de ätit upp bullarna räknar de på mynten.
De betalade 10 var, fick 1 krona tillbaka och två kronor skänktes bort.
De betalade alltså 10-1=9 och donerade två kronor.
9 x 3 = 27 + 2 = 29
Vad hände med den sista kronan?
Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)
Vi skapar variabler för de viktiga delarna.
int bullar = 25; // pris
int kalle = 10; // Cash
int pelle = 10; // Cash
int tjalle = 10;// Cash
int rödaKorset = 0; // Donation

Skriv ut informationen till Consolen
Console.WriteLine($"Bullarna kostar {bullar}");
Console.WriteLine($"Kalle har {kalle}");
Console.WriteLine($"Pelle har {pelle}");
Console.WriteLine($"Tjalle har {tjalle}");
Console.WriteLine();

Nu har vi grundinformationen, så vi påbörjar beräknandet.
Testa att köra programmet och passa på att att comitta ditt projekt till Git!
Console.WriteLine($"De betalar {kalle + pelle + tjalle}");
int kvarEfterKöp = (kalle + pelle + tjalle) - bullar;
kalle -= 10;
pelle -= 10;
tjalle -= 10;
Console.WriteLine($"Kalle har nu {kalle}");
Console.WriteLine($"Pelle har nu {pelle}");
Console.WriteLine($"Tjalle har nu {tjalle}");
Console.WriteLine($"Och får tillbaka {kvarEfterKöp}");
Console.WriteLine();

int page = 1;

Campus Mölndal – .net 21
Marcus Medina
Inget konstigt här… men testa att köra programmet och passa på att att comitta ditt projekt till Git!
Console.WriteLine($"De delar så att de får en krona var");
kalle++;
pelle++;
tjalle++;
kvarEfterKöp -= 3;
rödaKorset += kvarEfterKöp;
Console.WriteLine($"Kalle har nu {kalle}");
Console.WriteLine($"Pelle har nu {pelle}");
Console.WriteLine($"Tjalle har nu {tjalle}");
Console.WriteLine($"Och donerar {kvarEfterKöp} till Röda korset");
kvarEfterKöp = 0;
Console.WriteLine();

Nu börjar det bli intressant. Testa att köra programmet och passa på att att comitta ditt projekt till
Git!
I sista biten så räknar vi ihop kronorna
Console.WriteLine($"Summa summarum:");
int utlägg = (10 - 1) * 3;
Console.WriteLine($"De betalade 10 - 1 kronor var, alltså 9*3 = {utlägg} kronor");
Console.WriteLine($"och donerade 2 kronor");
utlägg += 2;
Console.WriteLine($"Summan blir då: {utlägg}");
If (utlägg!=30) Console.WriteLine($"Error 404: Krona not found");

Testa att köra programmet och passa på att att comitta ditt projekt till Git!
Var försvann kronan.
Gå igenom koden och korrigera felberäkningen. Diskutera problematiken i din basgrupp.
När du är klar med projektet, pusha allting till Github!

int page = 2;

Campus Mölndal – .net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Även om koden fungerar så kan logiken vara kass

Vad kan göras bättre?
1. Planera koden och beräkningar innan du börjar koda

int page = 3;
