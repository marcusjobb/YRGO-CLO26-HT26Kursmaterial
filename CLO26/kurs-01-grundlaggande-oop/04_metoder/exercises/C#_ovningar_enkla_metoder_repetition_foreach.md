---

title: Repetition Foreach
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Enkla metoder/Repetition foreach.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "exercise", "foreach.docx", "git", "methods", "repetition", "test"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina

Repetition foreach

Klicka eller tryck här för att ange datum.

Beskrivning:
I den här övningen ska vi skapa loopa igenom olika typer av listor. Även om de kan verka liknande så
beter de sig lite annorlunda.

Kursplanstermer som berörs av uppgiften:
Mål

Vad du ska lära dig

Kunskap om innehållet i .NET-biblioteket

Innehållet i .NET-biblioteket.

Kunskaper kring typer, variabler, operationer, uttryck,
villkorssatser och loopar inom programmering.

Typer, variabler, uttryck, villkorssatser och loopar används
inom programmering.

Kunskap kring namngivning och kodstruktur av klasser,
metoder och variabler i objektorienterade program.

Namngivning och kodstruktur av klasser, metoder och
variabler i objektorienterade program används.

Utveckla program med en tydligt objektorienterad struktur.

Utveckla program med en tydligt objektorienterad struktur.

Förstå och använda sig av datastrukturer inom programmering.

Att använda datastrukturer i sin mjukvaruutveckling.

Termer för övningen:






Array – samling av värden
List<> - en generisk lista med värden
Dictionary<,> - ett generiskt lexikon som håller koll på olika typer, man anger alltid en nyckel
(eller index) och ett värde. Dessa kan vara av vilka typer som helst egentligen.
Queue – En generisk FIFO lista
Stack – En generisk LIFO lista

int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Innehållsförteckning
Beskrivning:...............................................................................................................................................0
Kursplanstermer som berörs av uppgiften:..............................................................................................0
Termer för övningen:................................................................................................................................0
Projektinstruktioner:................................................................................................................................2
Kodning:....................................................................................................................................................2
For each loopar.....................................................................................................................................2
Skriv ut en namnlistan......................................................................................................................2
Skriv ut en namnlistan......................................................................................................................3
Skriv ut en aliaslistan........................................................................................................................4
En todolista.......................................................................................................................................5
En todolista 2....................................................................................................................................6
Facit: For each loopar...........................................................................................................................7
Skriv ut en namnlistan......................................................................................................................7
Skriv ut en aliaslistan........................................................................................................................8
En todolista.......................................................................................................................................8
En todolista 2....................................................................................................................................9

int page = 1;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.

Kodning:
Vi börjar med att skapa ett c#, .net konsolprojekt.
Skapa nu ett Github repo för ditt projekt (om du inte sparar i ett redan existerande repo)

For each loopar
I den här övningen ska vi fokusera på foreach-satser. Foreach är en enklare form av for-satser, de har
vissa fördelar och vissa nackdelar. Största fördelen är att koden blir snyggare, största nackdelen är
avsaknaden av räknare.
Exempel

// for-loop som fungerar som foreach
for(int counter=0; counter<list.Count; counter++)
{
var item=list[counter];
Console.WriteLine(item.Name);
}
// Som foreach skulle det se ut såhär
foreach(var item in lista)
{
Console.WriteLine(item.Name);
}

Skriv ut en namnlistan

Gör en metod som skriver ut namnlistan i listan som skickas in
PrintList

static void Main()
{
PrintList(new List<string>{"Batman","Robin","Batgirl","Alfred",
"Ace","Oracle","Huntress"});
}
public static void PrintList(List<string> namesList)
{
// Skriv din kod här

int page = 2;

Campus Mölndal .Net 21
Marcus Medina
PrintList

}

Förväntad output:
Batman
Robin
Batgirl
Alfred
Ace
Oracle
Huntress

Skriv ut en namnlistan

Gör en metod som skriver ut namnlistan i arrayen som skickas in.
PrintArray

static void Main()
{
PrintArray(new new string[]{"Batman","Robin","Batgirl","Alfred",
"Ace","Oracle","Huntress"});
}
public static void PrintArray(string[] namesList)
{
// Skriv din kod här
}

Förväntad output:
Batman
Robin
Batgirl
Alfred
Ace
Oracle
Huntress

int page = 3;

Campus Mölndal .Net 21
Marcus Medina
Skriv ut en aliaslistan
Tips

// En dictionary har två egenskaper, key som är första värdet vi ger den,
// och Value. Key får aldrig upprepas, medan Value får upprepas.
var numbers = new Dictionary<int, string>();
numbers.Add(1337, "Leet");

Gör en metod som skriver ut alias i dictionaryn som skickas in.
PrintDictionary

static void Main()
{
var alias = new Dictionary<string, string>();
alias.Add("Bruce Wayne", "Batman");
alias.Add("Clark Kent", "Superman");
alias.Add("Peter Quill", "Star-Lord");
alias.Add("Groot", "Groot");
PrintDictionary(alias);
}
public static void PrintDictionary(Dictionary<string, string> aliasList)
{
// Skriv din kod här
}

Förväntad output:
Bruce Wayne is Batman
Clark Kent is Superman
Peter Quill is Star-Lord
Groot is Groot

int page = 4;

Campus Mölndal .Net 21
Marcus Medina
En todolista
Tips

// Queue instansieras såhär:
var queue=new Queue<string>(); // Kan använda vilken typ som helst
// Queue använder sig av funktionen Enqueue för att lägga till saker i listan.
queue.Enqueue("Läs mailen");
// använder sig av funktionen Dequeue för att ta bort saker ur listan.
Console.WriteLine(queue.Dequeue());
// funktionen Peek visar vad som finns närmast i listan
Console.WriteLine(queue.Peek());

Nu ska vi skriva ut en Queue. Queues är lite annorlunda än andra listor, de har inte add och remove,
de har Enqueue och Dequeue. Det är en generisk typ av lista så vi kan välja själva vad vi vill lägga in i
vår Queue. Alla Queue är FIFO (First in, first out).
PrintQueue

static void Main()
{
var queue=new Queue<string>();
queue.Enqueue("9:00 Kolla mailen");
queue.Enqueue("9:30 Standup");
queue.Enqueue("9:45 Kaffe");
queue.Enqueue("10:00 programmering");
queue.Enqueue("11:15 Möte med kund");
queue.Enqueue("12:00 Lunch");
PrintQueue(queue);
}
public static void PrintQueue(Queue<string> todoList)
{
// Skriv din kod här
}

Förväntad output:
9:00 Kolla mailen
9:30 Standup
9:45 Kaffe
10:00 programmering
11:15 Möte med kund
12:00 Lunch

int page = 5;

Campus Mölndal .Net 21
Marcus Medina
En todolista 2
Tips

// Stack instansieras såhär:
var stack=new Stack<string>(); // Kan använda vilken typ som helst
// Stack använder sig av funktionen Push för att lägga till saker i listan.
stack.Push("Läs mailen");
// använder sig av funktionen Pop för att ta bort saker ur listan.
Console.WriteLine(stack.Pop());
// funktionen Peek visar vad som finns närmast i listan
Console.WriteLine(stack.Peek());

Nu ska vi skriva ut en Stack. Stack är lite annorlunda än andra listor, de har inte add och remove, de
har Push och Pop. Det är en generisk typ av lista så vi kan välja själva vad vi vill lägga in i vår stack.
Alla Stack är LIFO (last in, first out).
PrintStack

static void Main()
{
var stack=new Stack<string>();
stack.Push("9:00 Kolla mailen");
stack.Push("9:30 Standup");
stack.Push("9:45 Kaffe");
stack.Push("10:00 programmering");
stack.Push("11:15 Möte med kund");
stack.Push("12:00 Lunch");
PrintStack(stack);
}
public static void PrintStack(Stack<string> todoList)
{
// Skriv din kod här
}

Förväntad output:
12:00 Lunch
11:15 Möte med kund
10:00 programmering
9:45 Kaffe
9:30 Standup
9:00 Kolla mailen

int page = 6;

Campus Mölndal .Net 21
Marcus Medina

Facit: For each loopar
Skriv ut en namnlistan

Gör en metod som skriver ut namnlistan i arrayen som skickas in.
Piece of cake!
PrintList

public static void PrintList(List<string> namesList)
{
foreach (var name in namesList)
{
Console.WriteLine(name);
}
}

Skriv ut en namnlistan

Gör en metod som skriver ut namnlistan i listan som skickas in.
Piece of cake!
PrintArray

public static void PrintArray(string[] namesList)
{
foreach (var name in namesList)
{
Console.WriteLine(name);
}
}

int page = 7;

Campus Mölndal .Net 21
Marcus Medina
Skriv ut en aliaslistan

Gör en metod som skriver ut alias i dictionaryn som skickas in.
En dictionary har två egenskaper, key som är första värdet vi ger den, och value.
Key får aldrig upprepas, medan Value får upprepas. Value och key kan vara vilken typ som helst.
PrintDictionary

public static void PrintDictionary(Dictionary<string, string> aliasList)
{
foreach (var name in aliasList)
{
Console.WriteLine($"{name.Key} is {name.Value}");
}
}

En todolista

Nu ska vi skriva ut en Queue. Queues är lite annorlunda än andra listor, de har inte add och remove,
de har Enqueue och Dequeue. Det är en generisk typ av lista så vi kan välja själva vad vi vill lägga in i
vår Queue. Alla Queue är FIFO (First in, first out).
PrintQueue

public static void PrintQueue(Queue<string> todoList)
{
while (todoList.Count > 0)
{
Console.WriteLine(todoList.Dequeue());
}
}

int page = 8;

Campus Mölndal .Net 21
Marcus Medina
En todolista 2

Nu ska vi skriva ut en Stack. Stack är lite annorlunda än andra listor, de har inte add och remove, de
har Push och Pop. Det är en generisk typ av lista så vi kan välja själva vad vi vill lägga in i vår stack.
Alla Stack är LIFO (last in, first out)
PrintStack

public static void PrintStack(Stack<string> todoList)
{
while (todoList.Count > 0)
{
Console.WriteLine(todoList.Pop());
}
}

Testa att köra programmet och passa på att pusha ditt projekt till Git!
När du är klar med projektet, pusha allting till Github!

int page = 9;
