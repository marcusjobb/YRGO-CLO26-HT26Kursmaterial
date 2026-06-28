---

title: Switch
author: Marcus Ackre Medina
type: lecture
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Switch/Switch.pdf"
description: "• Switch använder man för att göra val, precis som if-else"
tags: ["conditions", "csharp", "switch.pdf"]
week_fit: []
---
.net21
Switch

Utbildningsledare
Annika Lund
annika.lund@molndal.se

Utbildare
Marcus Medina
marcus.medina@codic.se

Switch
• Switch använder man för att göra val, precis som if-else
• Den härstammar från den gamla goda tiden i C så den har lite
konstigheter för sig
• Varje ”case” avslutas med break; (har inte samma betydelse som i loopar)
• Man kan ha klammer runt varje case men det är inte nödvändigt

If else exempel
if (animal == Animals.Cat)
{
Console.WriteLine("Mjau");
}
else if (animal == Animals.Dog)
{
Console.WriteLine("Wooff");
}
else if (animal == Animals.Horse)
{
Console.WriteLine("Gnägg");
}
else
{
Console.WriteLine("Meh!");
}

Switch case
switch (animal)
{
case Animals.Cat:
{
Console.WriteLine("Mjau");
break;
}
case Animals.Dog:
{
Console.WriteLine("Wooff");
break;
}
case Animals.Horse:
{
Console.WriteLine("Gnägg");
break;
}
default:
{
Console.WriteLine("Meh!");
break;
}
}

Switch case
switch (animal)
{
case Animals.Cat:
Console.WriteLine("Mjau");
break;
case Animals.Dog:
Console.WriteLine("Wooff");
break;
case Animals.Horse:
Console.WriteLine("Gnägg");
break;
default:
Console.WriteLine("Meh!");
break;
}

Switch case med flera likadana resultat
switch (food)
{
case Animals.Cat:
case Animals.Dog:
Console.WriteLine("Kött");
break;
case Animals.Horse:
Console.WriteLine("Gräs");
break;
default:
Console.WriteLine("Allt annat");
break;
}

Switch case expression
string sound = animal switch

{
Animals.Cat => "Mjau",
Animals.Dog => "Wooff",
Animals.Horse => "Gnägg",

_ => "Meh!",
};
Console.WriteLine(sound);

Switch case expression
string sound = animal switch

{
Animals.Cat => "Mjau",
Animals.Dog => "Wooff",
Animals.Horse => "Gnägg",

_ => "Meh!",
};
Console.WriteLine(sound);

Switch case expression med likadana resultat
string animalFood = food switch
{
Animals.Cat or Animals.Dog => ("Kött"),
Animals.Horse => ("Gräs"),
_ => ("Allt annat"),
};

Switch med jämförelser
int x = 0;
string num = "";
switch (x)
{
case 1: num = "ett"; break;
case < 4: num = "mindre än 4"; break;
case > 5: num = " Större än 5"; break;
default:
num = "Whatever";
break;
};
Console.WriteLine(num);

Switch expression med jämförelser
int x = 0;
string num = x switch
{
1 => "ett",
< 4 => "mindre än 4",
> 5 => " Större än 5",
_ => "Whatever",
};
Console.WriteLine(num);

Sammanfattning
• Switch gör koden snyggare än if else
• Sista else på en switch heter default:
• Alla case avslutas med break;
• En tom case utan break ger samma resultat som följande case
• Cases på en rad (ekl break) kan ersättas med case expression
• Man kan göra enkla jämförelser i switch
• För många case gör koden oläsbar
• Det är snyggt att ha metoder som bara innehåller Switch case och
returnerar resultatet
