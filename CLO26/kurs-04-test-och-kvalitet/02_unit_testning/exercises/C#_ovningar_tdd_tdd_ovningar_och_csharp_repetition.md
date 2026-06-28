---

title: Tdd Övningar Och Csharp Repetition
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/TDD övningar och CSharp repetition.pdf"
description: "En positiv sak med att öva på Unit testing är att man får världens chans att öva på grundläggande C#, så"
tags: ["csharp", "exercise", "oop", "repetition.pdf", "tdd", "test", "testing", "övningar"]
week_fit: []
---
TDD övningar och C# repetition
En positiv sak med att öva på Unit testing är att man får världens chans att öva på grundläggande C#, så
det ska vi göra. Vi börjar med en söt liten enkel klass som med constructor som ska innehålla
information om en person.
Vi ska titta lite på constructor, strings och DateTime.
Vi börjar med att skapa klassen Person.cs
public class Person
{
public string Name { get; set}
public int Age { get; set; }
public Person(string name, int age)
{
Name = name;
Age = age;
}
}

Enkel kod som knappt gör något, men som vi kan testa.
[TestMethod()]
public void PersonTest_Constructor()
{
var person = new Person("Pelle",25);
Assert.AreEqual("Pelle", person.Name);
Assert.AreEqual(25, person.Age);
}

Vi verifierar att properties sätts rätt. Vad händer om vi skickar in null som namn?
[TestMethod()]
public void PersonTest_Constructor_Null_Name()
{
var person = new Person(null, 32);
Assert.IsNull(person.Name);
Assert.AreEqual(32, person.Age);
}

Det funkar alldeles utmärkt, men nu har vi verifierat att det går att göra utan att det kraschar, är det bra
eller dåligt? Om inte annat vet vi nu med säkerhet att en string kan vara null.
Nu ska vi titta lite på DateTime-beräkningar. Vi ska räkna ut åldern på personen. Så vi ändrar vår
constructor till att ta emot en DateTime istället för en int.
public Person(string name, DateTime birthDate)
{
Name = name;
Age = GetAge(birthDate);
}

Detta innebär att vi måste lägga till en property.
public DateTime BirthDate { get; set; }

I Constructorn kallade vi på en metod för att beräkna ålder, så vi skapar den nu.
Visual Studio talar faktiskt om för oss att metoden inte finns och erbjuder sig att skapa den till oss, det är
bara att klicka på glödlampan som dyker upp och välja ”Generate method”.

Vi kör på det och vi får följande metod
private int GetAge(DateTime birthDate)
{
throw new NotImplementedException();
}

Så nu lägger vi till kod för beräkning där.
return (int)((DateTime.Now - birthDate).TotalDays / 365.2425);

Kort och enkelt, vi tar reda på antal dagar som passerat och dividerar med 365.2425 för att få fram antal
år, skottår inräknat.
Najs! Nu kan vi kolla på några saker man kan kontrollera med datum. VI behöver inte skriva kontroller
för alla reglerna nedan om vi inte vill, men det är en kul grej att göra.
Ålder
11
13
14
15
15
15
15
16
16

Lag
Får se vissa filmer på bio i vuxet sällskap
Får utföra lättare arbete
Får övningsköra moped
Straffmyndig
Får köra moped
Får se barnförbjudna filmer
Kan cykla utan hjälp
Kan arbeta 40 timmars vecka
Kan starta ett företag

Property (bool & {get;})
CanGoToTheMovies
CanWorkSmallerJobs
CanTestDriveMoped
CanBePunishedByLaw
CanDriveMoped
CanSeeAdultMovies
CanRideBikeWithoutHelmet
CanWork40HoursAWeek
CanOwnACompany

16
16
16
16
18
18
18
18
18
18
20
21
25
47
55
57
62
68

Får köra lätt motorcykel
Får köra traktor
Får övningsköra bil
Rösta i kyrkoval
Är myndig
Får gifta sig
Får rösta
Får köpa cigaretter och snus
Får köpa alkohol
Får ta körkort för bil och MC
Får köpa alkohol på systembolaget
Får köra medelstor buss
Kan få alkoholreklam
Studielån begränsas
Får ta ut tjänstepension
Får inte ta studielån
Får ta ålderspension
Får avskedas på grund av ålder

CanDriveScooter
CanDriveTractor
CanTestDriveCar
CanVoteChurchElection
IsMature
CanGetMarried
CanVote
CanbuyCigarettes
CanBuyAlcoholAtRestaurants
CanGetDriversLicense
CanBuyAlcoholInStore
CanDriveMediumSizeBus
CanGetAlcoholAdsOnFacebook
LimitedStudentLoans
CanRetireFromWork
DeniedStudentLoans
CanRetireByAge
CanBeFiredDueToAge

För att kunna testa ska vi först ändra vår constructor
public Person(string name, DateTime birthDate)
{
Name = name;
Age = GetAge(birthDate);
}

Och vi lägger till propertyn
public DateTime BirthDate { get; set; }

Nu pajjade våra första test

så vi får ändra dem

[TestMethod()]
public void PersonTest_Constructor()
{
var person = new Person("Elvis", new DateTime(1935, 1, 8));
Assert.AreEqual("Elvis", person.Name);
Assert.AreEqual(86, person.Age);
}
[TestMethod()]
public void PersonTest_Constructor_Null_Name()
{
var person = new Person(null, new DateTime(2021,6,26));
Assert.IsNull(person.Name);
Assert.AreEqual(0, person.Age);
}

Nu funkar testerna igen!

Vi gör några enkla properties för att se om en person får gifta sig och en för att se när man kan börja
pensionera sig. Denna property är skrivskyddad då den räknas ut automatiskt.
public bool CanGetMarried { get { return Age >= 18; } }
public bool CanRetireByAge { get { return Age >= 62; } }

Nu skriver vi två tester, en för var property.
Vi börjar med Pelle som är född 2005, detta innebär att i år borde Pelle vara 16 år, och oavsett hur
mycket han älskar sin flickvän så får han inte gifta sig med henne. Vi bekräftar det med det här testet.
[TestMethod()]
public void CanGetMarried_Test_Age_16()
{
var birthDate = new DateTime(2005, 1, 1);
var person = new Person("Pelle", birthDate);
Assert.IsFalse(person.CanGetMarried);
Assert.AreEqual(16, person.Age);
}
[TestMethod()]
public void CanGetMarried_Test_Age_18()
{
var birthDate = new DateTime(2003, 1, 1);
var person = new Person("Johan", birthDate);
Assert.IsTrue(person.CanGetMarried);
Assert.AreEqual(18, person.Age);
}

Allright, då kan vi fortsätta med att skriva properties och tester. Inga problem… eller?
Vad händer när vi kör samma tester om ett år?
Då kommer Johan att vara 17 år, även om han fortfarande inte gifta sig med sin flickvän så stämmer inte
del två av testet där vi jämför åldern. Alla tester med datum kommer att misslyckas. För att det har gått
ett år och deras ålder har förändrats.
Det är inte fel på koden, eller logiken, men testerna tar inte hänsyn till att omständigheter (i detta fall,
datum) förändras.
Hur fixar vi detta? Vi måste kunna testa för att se att årtal räknas rätt. Vi skapar en metod för detta som
drar av det antal år vi behöver för att testa, från dagens datum.
private DateTime GetDateForAge(int years)
{
return DateTime.Now.AddYears(-years);
}

Så nu korrigerar vi alla metoder som använder sig av DateTime till att anropa denna metod, som i
exemplet med Elvis.
public void PersonTest_Constructor()
{
var person = new Person("Elvis", GetDateForAge(41));
Assert.AreEqual("Elvis", person.Name);
Assert.AreEqual(41, person.Age);
}

I testandets värld kommer Elvis alltid att vara 41 år, ett år innan han dog.
Vila i frid The King!
Hur som helst, nu kan vi testa att åldern räknas ut rätt och då kan vi testa olika åldrar mot olika
properties för att veta vad de får göra beroende på vilken ålder de har.
För de här testerna har vi använt
Assert.IsNull för att kontrollera om ett värde är NULL
Assert.AreEqual för att kontrollera om ett två värden är likadana
Assert.IsTrue för att kolla om en bool har svarat med true
Assert.IsFalse för att kolla om en bool har svarat med false
