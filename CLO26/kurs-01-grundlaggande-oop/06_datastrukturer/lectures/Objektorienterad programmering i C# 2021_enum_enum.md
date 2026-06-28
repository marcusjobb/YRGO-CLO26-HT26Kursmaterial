---

title: Enum
author: Marcus Ackre Medina
type: lecture
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Enum/Enum.pdf"
description: "• Enum är en lista med ord som får agera alias för nummeriska värden"
tags: ["csharp", "datastrukturer", "enum.pdf", "oop"]
week_fit: []
---
.net21
Enum

Utbildningsledare
Annika Lund
annika.lund@molndal.se

Utbildare
Marcus Medina
marcus.medina@codic.se

Enum
• Enum är en lista med ord som får agera alias för nummeriska värden
• Vi har innan använt oss av variabler och const för att ersätta magiska tal
med ord
• Enum är statisk (static) och konstant (const)
• Alla värden är int
• Räknar från 0 och upp
• Är egentligen en klass i miniformat
• Deklareras som enum, inte som klass dock
enum Colors
{
Red=0, Green, Blue=12, White, Orange, Brown=22, Black, Yellow
}

Exempel
const int max = 10;

const int min = 1;
int value = 5;
if (value < min) value = min;

if (value > max) value = max;

Exempel
enum BorderLimits

{
max = 10,
maxY = 20,
minX = 1,
minY = 1, // Fungerar men det är inte snyggt att göra så

}
int value = 5;
if (value < BorderLimits.minX) value = BorderLimits.minX;
if (value > BorderLimits.maxX) value = BorderLimits.maxX;

Omvandling
// enum till int
int limitX = (int)BorderLimits.maxX; // 10
// int till enum
BorderLimits limitY = (BorderLimits)20; // maxY
// enum till text
string miniText = BorderLimits.maxY.ToString(); // maxY
// text till enum
object? miniOut=null;
Enum.TryParse(typeof(BorderLimits),"minX", out miniOut); // minX
BorderLimits miniX = (BorderLimits)miniOut; //minX

Sammanfattning
Enums är listor med tal som får namn
• Man skriver alltid EnumNamn.Värde
• Kan hantera åäö men det är men det är inte Clean Code aktigt
• Dubbletter i värde kan förekomma men det är inte Clean Code aktigt
• Man kan omvandla från och till int och string
• Omvandla från string är kluddigt men det fungerar
• Används för att gruppera värden som hör ihop
• Ex. Månader, veckodagar, riktningar (NSEW)
