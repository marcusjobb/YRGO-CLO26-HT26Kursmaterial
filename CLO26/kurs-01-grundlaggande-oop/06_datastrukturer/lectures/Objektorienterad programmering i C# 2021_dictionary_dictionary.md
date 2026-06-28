---

title: Dictionary
author: Marcus Ackre Medina
type: lecture
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Dictionary/Dictionary.pdf"
description: "• En Dictionary är precis som det låter, en liten uppslagsbok."
tags: ["csharp", "datastrukturer", "dictionary.pdf"]
week_fit: []
---
.net21
Dictionary<>

Utbildningsledare
Annika Lund
annika.lund@molndal.se

Utbildare
Marcus Medina
marcus.medina@codic.se

Vad är en Dictionary
• En Dictionary är precis som det låter, en liten uppslagsbok.
• Vill man ha den sorterad får man använda en SortedList<> istället
• Vi använder värden för index istället för nummer.

Exempel
Dictionary<string, int> dic = new Dictionary<string, int>();

// Adult Animal, weight in kg
dic.Add("Blue whale", 136000);
dic.Add("Water buffalo", 725);
dic.Add("Walrus", 1013);

dic.Add("Moose", 386);
dic.Add("Tapir", 300);

Exempel – säker inmatning
// Adult Animal, weight in kg
if (!dic.TryAdd("Moose", 386))
Console.WriteLine("Moose already registered");

if (dic.TryAdd(“moose", 386))
Console.WriteLine(“Moose was added");

Exempel – säker avläsning
bool hasValue = dic.TryGetValue("Moose", out int value);

if (hasValue)
Console.WriteLine("Moose weight: "+ value);
else
Console.WriteLine("404: not found :(");
Alt.
if (dic.ContainsKey("Moose"))

Console.WriteLine("Moose weight: " + dic["Moose"]);

Exempel – sökning
if (dic.ContainsKey("Moose"))
Console.WriteLine("Moose weight: " + dic["Moose"]);

if (dic.ContainsValue(725))
{ // detta kan göras enklare med Linq (kommer i nästa kurs ;) )
foreach (var animal in dic)

{
if (animal.Value == 725)
{
Console.WriteLine(animal.Key + " weight: " + animal.Value);
break;
}
}
}

Sammanfattning
• Nyckeln kan vara av vilken typ som helst
• Värdet kan vara av vilken typ som helst
• Värdet kan vara en array eller Lista

• Söker nycklar med exakt samma värde
• Använder man strängar är det Case sensitive
• Kan inte söka på delar av ord

• Man kan söka på värde men det är svårare att hitta nyckeln till värdet
• Man kan använda det som en array med [index]
• Den kan inte sorteras direkt (utan att omvandlas något annat först)
