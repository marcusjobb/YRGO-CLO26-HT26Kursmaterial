---

title: Strängar Och Unit Tests
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/Strängar och Unit tests.docx"
description: "Vi ska skapa några metoder och testa vad som kan gå fel"
tags: ["csharp", "exercise", "oop", "strängar", "test", "testing", "tests.docx", "unit"]
week_fit: []
---
Fredagsuppdrag
Vi ska skapa några metoder och testa vad som kan gå fel

Visual Basic strängkommandon
I visual basic finns två några bra kommandon som heter Left, Right och Mid.
var name = "The amazing spiderman";
Console.WriteLine(name.Left(6));

Detta kommer att returnera de 6 första tecknen i strängen: The am
var name = "The amazing spiderman";
Console.WriteLine(name.Right(6));

Detta kommer att returnera de 6 sista tecknen i strängen: derman
var name = "The amazing spiderman";
Console.WriteLine(name.Mid(6,4));

Detta kommer att returnera de 4 tecken från och med andra tecknet i strängen: zing
För att dessa metoder ska se ut som om de vore en del av stringen får vi göra om dem som extension.
Extensions måste alltid ligga i en statisk klass och första inparametern är alltid this. Ungefär såhär ser
det ut.
public static class StringExtensions
{
public static void Print(this string text)
{
Console.WriteLine(text);
}
}

Nu kommer vi att kunna anropa metoden genom alla strängar. Hur fungerar detta?
this string talar om för metoden att den alltid ska fungera med en string. I detta fall returnerar vi

ingenting, men vi kan returnera värden också, vi kan ta emot fler parametrar också, men bara den första
markeras med this.
var name = "The amazing spiderman";
name.Right(6).Print();

Vi kan också anropa metoden det vanliga sättetStringExtensions.Print(name);

Nu ska ni skapa metoderna Left, Right och Mid men för att spara tid får ni en färdig klass.

public static class StringExtensions
{
public static void Print(this string text)
{
Console.WriteLine(text);
}
public static string Left(this string text, int pos)
{
return text.Substring(0, pos);
}
public static string Right(this string text, int pos)
{
return text.Substring(text.Length - pos);
}
public static string Mid(this string text, int pos, int length)
{
return text.Substring(pos, length);
}
public static string Mid(this string text, int pos)
{
return text.Substring(pos);
}
}

Självklart måste dessa testas med följande villkor, vilket innebär att ni kommer att bli tvungna att ändra i
metoderna.

Följande regler gäller



Tom sträng returnerar tom sträng
Är positionen som anges för stor, anpassa den till längden på strängen

Scenarios: ”Catwoman”
Left (3) = Cat
Left(50) = Catwoman
Left(0) =””
Left(-3) =””
Right (3) = man
Right(50) = Catwoman
Right(0)=””
Mid (3,3) = two
Mid(3,9) = woman
Mid (15,9) = “”
Mid (0,5) =Catwo
Mid (-3,2) =””
Mid (3,-2) =at
Mid (5,-3) =two
Mid (2) = twoman

Mid (500) =””
Mid(0) = “”

Testa koden från main
När gruppen är klar med testandet och koden inte krashar med de givna exemplen, kör den här koden
static void Main(string[] args)
{
var code = "bälg är en alldaglig tradig sak, låt oss evakvera i all heder";
var nums = new (int, int)[] { (21, 2), (41, 2), (17, 3),(20,1),(56,2),(2,2)};
var builder = new System.Text.StringBuilder();
foreach (var item in nums)
{
(int start, int length) = item;
builder.Append(code.Mid(start, length));
}
(builder.ToString()+"!").Print();
}
