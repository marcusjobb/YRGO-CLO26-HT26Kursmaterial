---

title: Linq Och Dess Magiska Värld
author: Marcus Ackre Medina
type: exercise
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Linq/Linq och dess magiska värld.docx"
description: "Linq är en förkortning av Language Intergrated Query och är en slags SQL-aktig fluent språk för att skapa"
tags: ["csharp", "datastrukturer", "dess", "exercise", "linq", "magiska", "oop", "sql", "värld.docx"]
week_fit: []
---
Linq och dess magiska värld
Linq är en förkortning av Language Intergrated Query och är en slags SQL-aktig fluent språk för att skapa
sökningar i koden. Exempelvis om man har en lista med namn och vill veta alla namn som börjar på ”M”.
För att det ska fungera behöver vi lägga till
using System.Linq;

i vår kod.
var Namn = new List<string> { "Marcus", "Peter", "Linda", "Ronja", "Sören" };
var filtrerade = new List<string>();
foreach (var item in Namn)
{
if (item.StartsWith("M"))
filtrerade.Add(item);
}
foreach (var item in filtrerade)
{
Console.WriteLine(item);
}

Med Linq kan vi skriva om det såhär
Namn.Where(n => n.StartsWith('M')).ForEach(x => Console.WriteLine(x));

De två kodsnuttarna gör precis samma sak.
Med Linq anger vi at vi vill söka (Where funktionen) sedan anger vi en temporär variabel att arbeta med,
i detta fall ”n”. För varje rad i listan kollar den om ”n” börjar med bokstaven ”M” när den är klar kommer
läggs allting i en lista med kommandot .ToList(). Alla listor som berörs av Linq har funktionen ForEach
som man kan köra direkt på objektet. Rörigt? Jo, lite faktiskt.
Det är inte ett kommando vi kör, det är flera som länkas till varandra. Om man delar dem i rader så är
det enklare att se.
Namn.
Where(
n => n.StartsWith('M')
)
.ForEach(
x => Console.WriteLine(x)
);

Alternativt kan man skriva koden såhär
Så i sin enklaste och mest läsbara form
var filter = Namn.Where(n => n.StartsWith('M'));
foreach (var item in filter)

{
Console.WriteLine(item);
}

Bara för att man kan göra allt i en enda rad betyder inte att man måste göra så.
Dock finns det en sak till man ska förstå om Linq innan man kan börja leka på allvar… den är inte lite
extra funktioner till redan existerande typer… den är mer än så.
var filter = from name in Namn
where name.StartsWith("M")
select name;

Den här koden gör precis samma sak som den förra på en rad, men denna är mycket mer läsbar… och
den påminner om SQL. Hur bra som helst. Använd den som känns mer naturlig för dig.

Så, vad mer kan man göra med Linq då?
Vi ska kolla på några exempel.
// En massa siffror
var tal = new int[] { 1, 23, 23, 423, 423, 23, 23, 4, 23, 23, 4, 24, 23, 4, 24, 2, 42, 4, 2, 23, 43 };
// sortera min array eller lista
var sorteradeTal = tal.OrderBy(x => x); //<-- knasig format men så är det
// Högsta värdet
var högstaVärdet = tal.Max();
// Minsta värdet
var minstaVärdet = tal.Min();
// summera alla talen
var summa = tal.Sum();
// Räkna ut medelvärdet
var medel = tal.Average();
// Räkna ut summan av alla tal större än 100
var SumTalMerÄn100 = tal.Where(x => x > 100).Sum();
// Räkna ut summan av alla tal större än 100 men mindre än 200
var SumTalMerÄn100MindreÄn200 = tal.Where(x => x > 100 && x<200).Sum();

Nu har vi tittar på String och Int, men hur fungerar det med objekt?
Precis lika bra!

Linq och objekt
Vi börjar med att skapa en class
class Person
{
public string Name { get; set; }
public int Age { get; set; }
}

Och sedan populerar vi den med populära personer.
var people = new Person[]
{
new Person{Name="Jack Nicholson",Age=83},
new Person{Name="Nicholas Cage",Age=56},
new Person{Name="Bruce Willis",Age=65},
};

Vi kan sortera dem enligt ålder
var peopleByAge = from person in people
orderby person.Age
select person;
eller så kan vi skriva det såhär
var peopleByAge = people.OrderBy(p => p.Age);

och vi skriver ut allt såhär
foreach (var item in peopleByAge)
{
Console.WriteLine($"{item.Name} {item.Age}");
}

Vill vi ha medelåldern på dem skriver vi
var medel = people.Average(x => x.Age);

Så vi kan använda classens properties för att filtrera och söka. Exempelvis om vi vill hitta alla namn som
har Nic i sig.
var HasNic = from actor in people
where actor.Name.Contains("Nic")
select actor;

eller
var HasNic = people.Where(actor => actor.Name.Contains("Nic"));

Då får vi Nicholas Cage och Jack Nicholson.

Sortering
En lurig sak med Linq är att när man vill sortera på flera kriterier så kan man få problem
Detta fungerar inte
var sortedActors = people.OrderBy(actor => actor.Name && actor.Age);

Detta fungerar
var sortedActors = people.OrderBy(actor => actor.Name).OrderBy(actor=>actor.Age);

Men den ger fel resultat. Det är nämligen så att vi städar listan efter skådisarnas namn, och sedan
sorterar vi listan efter skådisarnas ålder. Så listan kommer att bli enbart sorterad på ålder.
Vi måste ändra vår kod…
var sortedActors = people.OrderBy(actor => actor.Name).ThenBy(actor=>actor.Age);

Nu kommer den att fungera.
Vill vi sortera dem på namn men sedan på äldsta först, så använder vi ThenByDescending
var sortedActors = people.OrderBy(actor => actor.Name).ThenByDescending(actor=>actor.Age);

Join classes
När vi ändå använder classer som tabeller i Entity Framework, hur gör man joins?
Jag lånar ett exempel från Microsoft
class Person
{
public string FirstName { get; set; }
public string LastName { get; set; }
}
class Pet
{
public string Name { get; set; }
public Person Owner { get; set; }
}

I main classen kan vi skriva in följande kod
Person magnus = new Person { FirstName = "Magnus", LastName = "Hedlund" };
Person terry = new Person { FirstName = "Terry", LastName = "Adams" };
Person charlotte = new Person { FirstName = "Charlotte", LastName = "Weiss" };
Person arlene = new Person { FirstName = "Arlene", LastName = "Huff" };
Person rui = new Person { FirstName = "Rui", LastName = "Raposo" };
Pet barley = new Pet { Name = "Barley", Owner = terry };
Pet boots = new Pet { Name = "Boots", Owner = terry };

Pet whiskers = new Pet { Name = "Whiskers", Owner = charlotte };
Pet bluemoon = new Pet { Name = "Blue Moon", Owner = rui };
Pet daisy = new Pet { Name = "Daisy", Owner = magnus };
// Create two lists.
List<Person> people = new List<Person> { magnus, terry, charlotte, arlene, rui };
List<Pet> pets = new List<Pet> { barley, boots, whiskers, bluemoon, daisy };
// Create a collection of person-pet pairs. Each element in the collection
// is an anonymous type containing both the person's name and their pet's name.
var query = from person in people
join pet in pets on person equals pet.Owner
select new { OwnerName = person.FirstName, PetName = pet.Name };
foreach (var ownerAndPet in query)
{
Console.WriteLine($"\"{ownerAndPet.PetName}\" is owned by {ownerAndPet.OwnerName}");
}
// "Daisy" is owned by Magnus
// "Barley" is owned by Terry
// "Boots" is owned by Terry
// "Whiskers" is owned by Charlotte
// "Blue Moon" is owned by Rui

Precis som i SQL kan vi göra flera joins i rad efter varandra. Själv queryn fungerat såhär
Kodsnutt
from person in people
join pet in pets on person equals pet.Owner

Select new
{
OwnerName = person.FirstName,
PetName = pet.Name
}

Förklaring
Skapar variabeln person som baserar sig på vår
lista med people
Join resultater kommer att läggas in variabeln
pet, och vi kopplar ihop listan pets med variabeln
pets.owner (som är en instans av person classen)
Skapar en ny typ med den informationen vi ger
den
Den nya typens egenskaper.
Det är ungefär som att skapa en ny class med
dessa egenskaper, men med skillnaden att vi inte
skriver koden, det fixar kompilatorn till oss.

Varför kan Linq men inte jag?
OK, nu när du vet hur cool Linq är så vill du självklart skapa dina egna funktioner. Det finns några tips för
det med…
Låt oss säga att du skapar den här metoden
private static int Add(int x, int y) { return x + y; }

då kan du tala om för Linq att den ska använda den, genom att använda aggregate metoden, som du kan
se i koden så talar vi om för den att den ska köra en specifik funktion för varje rad i listan
var numbers = new List<int> { 6, 2, 8, 3 };
int sum = numbers.Aggregate(func: Add);

Men nja… ok det var nog inte så vi menade med att skapa egna metoder… Tänk om vi vill ha en metod
som ger oss en IMDB länk till skådisen. Det kan inte Linq göra, men det kan vi.
Om vi inte kan eller vill ändra i classen Person så kan vi ändå lägga till metoder i den, då använder vi en
Extension. Vi skapar en statisk class och ger den ett vettigt namn
public static class ActorExtension
{
public static string GetIMDBUrl(this Person actor)
{
return "https://www.imdb.com/find?s=all&q=" + actor.Name.Replace(" ", "+");
}
}

Classen heter ActorExtension och den innehåller ”Extensions”, en slags förlängningn för just skådislistan.
Nu när vi använder objekt av typen Person kommer GetIMDBUrl att föreslås av visual Studio som
funktion till den classen.
Du kan göra Extensions för alla objekt i Visual Studio. Det finns några regler som är viktiga att komma
ihåg.
1) Mata dem inte efter midnatt
2) Utsätt dem inte för vatten
3) De tål inte stark ljus
Nej!
De reglerna gäller för bara för Gremlings.
För Extensions är reglerna följande
1) Classen måste vara statisk
2) Metoden ska vara statisk
3) Och första parametern i metoden ska föregås av this.
This talar nämligen om för metoden att den ska koppla sig till den sortens objekt.
This string kopplar sig till string objekt, this int[] kopplar sig till int array objekt osv, sen gör man vad man

vill med det objekt man fått in i sin metod och returnerar resultater. Det blir som om den vore en del av
objektet och ingen kommer att märka någon skillnad.

Exempel på Extensions
Säg att du vill kunna splittra alla string till en array om de innehåller mellanslag. Jag döper classen till
StringExtension för att förtydliga att jag förbättrar stringobjektet här
public static class StringExtension
{
public static string[] SplitBySpace(this string text)
{
return text.Split(' ');
}
public static string JoinWithSpace(this string[] text)
{
return string.Join(' ', text);
}
}

Nu kommer stringobjektet att ha de metoderna tillgängliga för mig, så jag lätt kan få en array av ord
delade med mellanslag och om jag har en array med ord kan jag lätt slå ihop dem med bara ett
kommando.
Prova själv!

Bra länkar:



https://www.tutorialsteacher.com/linq/linq-tutorials
https://www.tutorialsteacher.com/csharp/csharp-extension-method

Disclaimer
OK, jag medger att Extensions är lite offtopic från Linq men det är ändå kul att kunna skapa sina egna
metoder till Visual Studio. Vi får titta på lite mer avencerade Func funktioner en annan gång…
