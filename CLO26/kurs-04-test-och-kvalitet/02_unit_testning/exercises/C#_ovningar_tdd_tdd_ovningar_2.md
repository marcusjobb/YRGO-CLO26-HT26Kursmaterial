---

title: Tdd Övningar 2
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/TDD övningar 2.pdf"
description: "Här kommer en uppsättning med fler enkla metoder som kan vara bra att testa"
tags: ["2.pdf", "csharp", "exercise", "tdd", "test", "testing", "övningar"]
week_fit: []
---
TDD övningar
Här kommer en uppsättning med fler enkla metoder som kan vara bra att testa

Palindrom
public bool IsPalindrome(string text) { /* Massor med kod */ }

Testa att skicka in olika texter, den ska vända på din string och returnera true om texten ser likadan ut (
(i lowercase) efter att man har vänt på det, exempelvis Will Ferell blir llerreF lliW (false, vilket är samma
svar på frågan om jag tycker att han är rolig), men Tacocat blir tacocaT (true, vilket är sant).
Vad händer om stängen är tom, eller null? Försök att tänka ut bra tester innan du skriver koden.

Födelsedata
Du ska göra några metoder som ska fungera med DateTime(), dessa ska du testa mot orealistiska datum.
public int Age (DateTime birthDay) – räkna ut en persons ålder
public int BirthdayWeekDay(DateTime birthDay) – Veckodagen personen födes
public int BirthdaySeason(DateTime birthDay) – Årstid personen födes på
public int BirthdayHoliday(DateTime birthDay) – Om personen födes vid en högtid
public int AgeDays (DateTime birthDay) – räkna ut antalet dagar personen levt
public int SleepYears (DateTime birthDay) räkna ut antalet sömnår personen levt (1/3 av levnadsår)
public int SleepDays (DateTime birthDay) räkna ut antalet dagar en personen sovit (1/3 av levnadsdagar)
public int ConceiveDate (DateTime birthDay) – Personens födelsedag minus 9 månader
public int NearestConceiveHoliday (DateTime birthDay) – Personens födelsedag – 9 och jämför månad
och dag mot standard högtider (Nyår, Jul, Midsommar, Fabrikssemester, Halloween, Alla hjärtans mm),
räkna plus/minus två veckor.

Skottår
Testa den här metoden för att veta om ett visst år är skottår eller inte, dubbelchecka med Google
om det inte stämmer. Hittar du felaktigheter, anpassa koden. När du är klar kör refactoring på det. Kan
du få ner koden till en rad kanske?
public bool IsLeapyear(int year)
{
if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
{
return true;
}
else
{
return false;
}
}

Geometri
Testa dessa metoder och se vad som händer när man skickar in orealistiska värden. Kör refactoring
också för att snygga upp koden, den är inte snygg som den är nu!
public int TriangleArea(int length, int width)
{
return length * width;
}
public (int, int) SquareAreaAndPerimeter(int height)
{
int area, perimeter;
area = height * height;
perimeter = 4 * height;
return (area, perimeter);
}
public (double perimeter, double area) CircleArea(double radius)
{
double perimeter, area;
perimeter = 2 * 3.14 * radius;
area = 3.14 * Math.Pow(radius, 2); //area = 3.14 * r * r;
return (perimeter, area);
}
