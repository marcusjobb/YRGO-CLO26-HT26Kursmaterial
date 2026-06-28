---

title: Arrays Och Listor
author: Marcus Ackre Medina
type: lecture
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Arrays och listor/Arrays och listor.pptx"
description: "• Det är som en hylla där vi placerar våra burkar med variabler"
tags: ["arrays", "csharp", "datastrukturer", "listor.pptx"]
week_fit: []
---
.net21
Arrays och Listor

Utbildningsledare
Annika Lund annika.lund@m
olndal.se

Utbildare
Marcus Medina marcus.med
ina@codic.se

Vad är en array?
• Det är som en hylla där vi placerar våra burkar med variabler

Vad är tvådimensionella arrayer
• Det är helt enkelt fler hyllor
• Det blir som ett rutnät, med x och y positioner

Vad är multidimensionella arrayer
• Det är helt enkelt fler hyllor
• Det blir som ett rutnät, med x och y positioner

Arrays
• När vi deklarerar en array gör vi det på samma sätt som vi deklarerar
variabler
• Dock anger vi tomma klammer för att markera att det är en array
• int[] myNumbers;

• När vi instansierar den skriver vi antal minnesplatser den ska
innehålla, innanför klammer
•

myNumbers = new int[10]; // Array med 10 värden

Arrays - exempel
• Vi vill lagra 3 namn i vår array
names[0] = "Marcus";
names[1] = "Marcelo";
names[2] = "Medina";

Vi leker lite med arrayen live istället!

Arrays – snyggare initiering
string[] names = new string[]
{
"Marcus",
"Marcelo",
"Medina"
};
// Det är alltid enklare att använda klammer för att initiera arrayer och
listor

Arrays – övning (15 mins i basgruppen)
• Skapa en array som tar emot tre strängar
• Skapa en array som tar emot tre double
• I strängarrayen tilldelar du följande värden

• Mjölk, 3 Mjau - Noga Utvalt Kyckling i Sås, Bananer

• I double arrayen tilldelar du följande värden
• 17.30, 20, 26.95

• Gör nu en loop som

• skriver ut innehållet i string arrayen
• Skriver ut innehållet i double arrayen efter strängen
• Summerar innehåller i double arrayen
• Skriv ut summan när loopen är klar

Listor
• En lista är som en array, men med lite mer möjligheter att leka
• Det finns olika listor i C#
• List<> är en typ av lista som anpassar sig till olika typer
• Plus: Den är snabbare än de flesta andra listor
• Minus: Generiska listor har få funktioner (men man klarar sig bra med dem)

• ArrayList är en lista som tar emot allt
• Plus: Den är lätt att mata in data i 
• Minus: Den är svårt att hantera det man får ut ur den 

• StringList: Utbyggnad av List<string> med fler funktioner
• Plus: Lätt att använda
• Minus: Möjligen lite långsammare (nån bitcykel) än List<string>

Listor
• Stack är en LIFO list
• Plus: bra för köhantering i omvänd kronologisk ordning
• Minus: lätt att radera punkter om man använder Pop istället för Peek

• Queue är en FIFO list
• Plus: bra för köhantering i kronologisk ordning
• Minus: lätt att radera punkter om man använder Dequeue istället för Peek
