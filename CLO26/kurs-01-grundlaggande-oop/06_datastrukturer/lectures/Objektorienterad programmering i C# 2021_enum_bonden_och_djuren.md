---

title: Bonden Och Djuren
author: Marcus Ackre Medina
type: lecture
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Enum/Bonden och djuren.docx"
description: "I den här övningen ska du vägleda en bonde med som ska föra över en varg, en get och ett"
tags: ["bonden", "csharp", "datastrukturer", "djuren.docx", "git"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Bonden och djuren

15 september 2021

Beskrivning:
I den här övningen ska du vägleda en bonde med som ska föra över en varg, en get och ett
salladsblad till en ö. Inte riktigt som originalhistorien från 1200 talet men nära nog.
Programmet är färdigskrivet men saknar ordningen som saker ska överföras i. Gör man saker i fel
ordning så kommer programmet att tala om vad som gått fel.

Kursplanstermer som berörs av uppgiften:
Mål

För godkänt krävs

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Den studerande redogör för hur typer, variabler, uttryck, villkorssatser
och loopar används inom programmering.

Kunskap kring namngivning och kodstruktur av
klasser, metoder och variabler i objektorienterade
program.

Den studerande redogör för hur namngivning och kodstruktur av
klasser, metoder och variabler i objektorienterade program används.

Versionshantering med hjälp av verktyg som
exempelvis Git.

Den studerande använder sig av versionshanteringsverktyg under
projekt.

Termer för övningen:


Enum – En enum är en lista med statiska värden (som inte kan ändras medan programet kör).
Dessa värden är namngivna för att lättare kunna hanteras i programmet.

int page = 0;

Campus Mölndal .Net 21
Marcus Medina
Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.
Kodning:
Koden till grund-delen av programmet finns här:
https://github.com/marcusjobb/net21/tree/main/OOP%20Grund/Farmer
Hämta hem projektet och öppna den.
I Main metoden finns förklaringar på hur du ska lösa problemet.
I korthet ska du använda dig enbart av metoden MoveThingy och i den ska du ange vad som ska
flyttas, varifrån det ska flyttas och var det ska hamna.
När man arbetar med enum så anger man alltid namnet på enum punkt och sedan värdet man vill
använda.
enum Thingies
{
Farmer,
Goat,
Lettuce,
Wolf,
}

Vill vi använda oss av Goat så skriver vi Thingies.Lettuce, vilket för kompilatorn kommer att betyda
samma sak som nummer 1. Enums räknar alltid från noll och varje nytt ord i listan får nästa nummer.
enum Thingies
{
Farmer = 0,
Goat = 1,
Lettuce = 2,
Wolf = 3,
}

Tack o lov behöver vi inte skriva siffrorna, den räknar det själv.

int page = 1;

Campus Mölndal .Net 21
Marcus Medina
Sammanfattning:
Vad har vi lärt oss av detta exempel?
Om enums
1.
2.
3.
4.

En enum kan omvandlas till en int genom vanlig casting int farmer = (int)Thingies.Farmer
En int kan omvandlas till enum Thingies lettuce = (Thingies)2; // lettuce
Enums är enbart för programmerarnas nöje, det omvandlas till tal av kompilatorn
Enums är bra medel mot magiska tal

Om projektet
Vad det gäller själva projektet så kan vi konstatera
1. Det räcker inte med att koden kompilerar och är körbar – den ska agera rätt också
2. Även om koden fungerar betyder inte att den gör saker på rätt sätt.
3. Alltid bra att hålla användaren informerad om vad som händer i koden
Vad kan göras bättre?
1. Kanske en version där användaren ska välja genom inputs om vad som ska flyttas och vart?
2. Kanske lite ascii bilder?
något i den stilen kanske? (i matchande storlekar dock)
Art by Elissa Potier
////\\\\
|
|
@ O O @
| ~ |
\__
\ -- /
|\ |
___| |___
| \|
/
\
/|__|
/
\ //
/ /| . . |\ \ / /
/ /|
| \ \/ /
< < |
| \ /
\ \ | . | \_/
\ \|______|
\_|______|
|
|
| | |
| | |
|__|___|
| | |
( ( |
| | |
| | |
_| | |
cccC_Cccc___)

,--._,--.
,' ,' ,-`.
(`-.__ / ,' /
`. `--'
\__,--'-.
`--/
,-. ______/
(o-. ,o- /
`. ;
\
|:
\
,'`
, \
(o o , --' :
\--','.
;
`;; :
/
-hrr- ;' ; ,' ,'
,',' : '
\\ :
`

, ,
|\---/|
/ ,,|
__.-'| / \ /
__ ___.-'
._O|
.-' '
:
_/
/, .
. |
: ; :
: _/
| | .' __: /
| : /'----'| \ |
\ |\ |
| /| |
'.'| /
|| \ |
| /|.'
'.l \\_
snd || ||
'-'

.-~~~~-.
/ (('\
|() ) |
\)'} //
(` \ , / ~)
`-.`\/_.-'
jgs
`""

Ascii hämtad från https://www.asciiart.eu/ och https://ascii.co.uk/

int page = 2;
