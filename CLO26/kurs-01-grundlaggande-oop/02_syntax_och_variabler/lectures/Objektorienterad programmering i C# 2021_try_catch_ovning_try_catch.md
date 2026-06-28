---

title: Övning Try Catch
author: Marcus Ackre Medina
type: lecture
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Try Catch/Övning try catch.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["catch.docx", "csharp", "oop", "syntax", "test", "try", "övning"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Övning try catch
Beskrivning:

I den här övningen ska vi skapa ett consolprojekt och leka lite med Try Catch

Kursplanstermer som berörs av uppgiften:
Mål

För godkänt krävs

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Den studerande redogör för hur typer, variabler, uttryck,
villkorssatser och loopar används inom programmering.

Utveckla felfria fristående program.

Den studerande skapar felfria fristående program.

Termer för övningen:





Try
Catch
Finally
Exception

Psuedokod:
Try
Skapa en hopplös situation för att testa
Catch
Fånga upp krasch meddelandet
logga meddelandet
Finally
Förstör objekt och annat som kan skapa minnesläckor

int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Diskutera gärna med medlemmarna i din basgrupp

Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Vi börjar med en klassiker, vi dividerar med noll
Om vi skriver
int x = 1/0;

Kommer kompilatorn att varna att det inte går att göra så, men vi kan forcera fram det genom att
lura kompilatorn.
int num1 = 4;
int num2 = 0;
int x = num1 / num2;

Kör vi den koden kommer programmet att krascha. Det är självklart att vi inte ska skriva kod på detta
sätt, men det kan hända att vi får in felaktig information, eller att kommunikationen med en databas
eller API avbryts. Det kan även hända att vi försöker läsa av ett värde ur en array och det visar sig att
det värdet ligger utanför arrayens ramar. För att skydda oss från sådana överraskningar som vi inte
kan förutse med if-satser, kan vi använda en try-catch block.
try
{
int num1 = 4;
int num2 = 0;
int x = num1 / num2;
}
catch (Exception ex)
{
Console.WriteLine(ex.Message);
}

I catch delen fångar vi upp felet som uppstått och information om vad som orsakat det, i en variabel
av typen Exception. Exception är moderklassen för just exceptiontyper.
ArrayTypeMismatchException
DivideByZeroException
IndexOutOfRangeException
InvalidCastException
IO.IOException
NullReferenceException
OutOfMemoryException
StackOverflowException
… och många fler.

När man skickar in fel typ av data till en array
När man försöker dividera med 0
När man angett ett index som är utanför listan eller arrayens
storlek
När man gör fel vid omvandling mellan typer
När det uppstår filhanteringsproblem
När man försöker bearbeta ett objekt som är null
När man mot förmodan lyckats fylla hela minnet
När man anger ett alldeles för stort värde

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

Person person=null;
try
{
person.Name = "Jack Sparrow";
}
catch (System.NullReferenceException ex)
{
Console.WriteLine(ex.Message);
}

Vi provar ett annat exempel
class Person
{
public int Id { get; set; }
public string Name { get; set; }
}
class Program
{
static void Main(string[] args)
{
Person person = null;
try
{
person.Name = "Jack Sparrow";
}
// triggas av null error
catch (System.NullReferenceException ex)
{
// Hantera vad som nu gått fel
}
// triggas av alla andra error
catch (System.Exception ex)
{
Console.WriteLine(ex.Message);
}
// ifall minnet behövs städas upp
finally
{
person = null;
}
}
}

int page = 2;

Campus Mölndal .Net 21
Marcus Medina

Sammanfattning:
Vad har vi lärt oss av detta exempel?
1. Try Catch fångar upp fel vi inte kan förutse
2. Om du kommunicerar med databaser, APIer, hårdvara eller annat du inte kan kontrollera från din
kod, använd Try Catch för att försäkra dig om att ditt program inte dör på grund av externa
felaktigheter.
3. Ha minimalt med kod i din Try Catch, så att du lätt kan identifiera vad som gått fel. Ha hellre flera
try catch efter varandra om det behövs än att ha en massa kod i en enda try catch.

Värt att tänka på
1. Try Catch är långsammare än if-satser, så använd if-satser hellre för att stoppa så många fel du
kan komma på.
2. Try Catch kan hantera olika typer av fel, filtrerar man dessa fel kan man hantera problemen mer
effektivt.

int page = 3;
