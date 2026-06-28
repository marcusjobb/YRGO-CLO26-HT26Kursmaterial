---

title: Events Och Delegates
author: Marcus Ackre Medina
type: exercise
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Events och Delegates/Events och Delegates.docx"
description: "Plötsligt händer det, som de säger…"
tags: ["csharp", "delegates.docx", "events", "exercise", "oop", "sql", "test"]
week_fit: []
---
Events
Plötsligt händer det, som de säger…

Innehåll
Timer..............................................................................................................................................................3
Delegate.........................................................................................................................................................4
Events.............................................................................................................................................................4
Kodexempel...................................................................................................................................................4
TLDR;..............................................................................................................................................................7

Vi ska titta på en annan del av OOP här. Den delen som gör att vi kan hålla koll på saker och ting utan att
behöva kontrollera properties och variabler stup i kvarten. Tänk dig att du har en lista på 5000 anställda
och vill veta när en av dem arbetat över 40 timmar på en vecka. Du kan självklart ha en if-sats där man
skriver in timmar, men tänk om man kan få den berörda personalklassen att rapportera själv när detta
inträffar? Eller om du har ett spel med olika små figurer, när spelaren skjuter och lasern träffar
motståndaren vill du veta att det hänt, utan att för den skull kontrollera varje gång din laserstråle
förflyttar sig på skärmen. Enklast är det då om själva karaktären i spelet meddelar ”Aj jag blev träffad”.
Kort och gott, istället för att fråga klasserna stup i kvarten om det hänt något specifikt, så låter vi dem
berätta för vår huvudklass att det har hänt något.
Har du 10 olika klasser att hålla reda på, så vill du gärna slippa loopa igenom alla för att kolla en viss
property gång på gång. Det är mycket enklare om dina klasser rapporterar till dig att något har hänt.
Hur gör vi detta?
Vi börjar med ett enkelt exempel för att förklara hur det fungerar… en timer!

Timer
Ett event i .Net är en metod som anropas när en specifik sak händer. Vanligaste exemplet är en timer
som anropar en metod i ett visst intervall. Men hur skapar man sådana, och hur använder man dem.
Det ska vi titta på nu!
var timerInfo = "Time has passed";
var timer = new Timer(callbackMetod, timerInfo, 0, 1000);

Här skapar vi en sträng med ett meddelande, det kan egentligen vara vilket objekt som helst eller null
om man vill.
Första parametern är callback metoden, den kan egentligen anropa vilken metod som helst, men timern
vill att metoden som anropas ska se ut såhär: void callbackMetod(object state). Namnet på metoden och
namnet på variabeln är oviktiga, om det är private, public, internal eller static också oviktigt.
private void callbackMetod(object state)
{
Console.WriteLine(state);
}

De parametrar som följer är ett objekt som ska skickas med när timern sker, detta kan vara en siffra, ett
objekt med properties, en sträng eller något annat.
Nästa parameter är hur många millisekunder (1000 = 1 sekund) timern ska vänta innan den kör igång
Den sista parametern är tiden i millisekunder.
Vad som händer sen är att en gång per sekund så kommer timern att anropa metoden som angetts och
skicka med ett objekt (som är en sträng) med texten ”Time has passed”.
En viktig detalj med timern är att den kräver ett specifikt mönster på metoden, och det är just det vi
bestämmer när vi skapar en delegate.

Delegate
En delegate är en variabeltyp som hanterar metoder. Vilket gör att vi kan skicka metoder som
parametrar till andra metoder, eller helt enkelt lagra en metod i en sträng. Detta ser man ofta i
Javascript, där man skickar med så kallade callback metoder vid metodanrop. Självklart kan vi göra detta
i C# med. En gång i tiden, i C skickade man helt enkelt en pekare till metoden. Nu slipper vi det, vi skickar
delegates i stället.
Vi deklarerar den såhär enkelt
public delegate void HasChanged(string aString);

Detta talar om att vår delegate kommer att heta HasChanged och kommer att kunna hänvisa till metoder
av typen void(string).
Nu kan vi alltså skapa variabler av typen
HasChanged metod = MinCoolaMetod;

Observera att när vi deklarerar delegate-variabler så använder vi inte new (metoden finns redan) och vi
använder inte metodens inparametrar.
Vi kan även använda delegates i metoder
public void DoSomething(HasChanged callback)
{
/* massor med kod */
callback?.Invoke("My work is done");
}

Detta kommer att göra att vi anropar vår speciella metod någonstans i DoSomething metoden genom
funktionen Invoke. Observera att delegaten används som en vanlig variabel, men det som skickas in vid
anrop är namnet på en metod som matchar delegaten. Det kan vara bra att kolla om den är null iofs, så
vi kör med ? innan själva anropet.

Events
Nästa del är events. Ett event är en klass man kan ”prenumerera” på genom att skicka in delegates till en
lista. Dessa delegates kommer att anropas när något specifikt händer i vår klass.

Kodexempel
Vi kollar på en färdig lösning för detta.
/*
Föst skapa en delegate som beskriver hur metoden som tar emot
notifikationen (event) ska se ut
*/
public delegate void HasChanged(string aString);

Först skapar vi en delegate som ska gälla för alla klasser, alltså deklarerar vi den utanför vår klass, rakt in
i namespacet.

static void Main()
{
var p = new Person();
/*
Prenumerera på eventet genom att välja namnet på den i objektets properties
och skriva += och namnet på metoden som ska ta emot eventet, tänk på att
metoden måste matcha din delegate. Vill du inte längre prenumerera på ett
event använder du -=
*/
p.NameChanged += PropertyNameHasChanged; // metod som matchar vår delegate
// Nu ska vi leka med propertyn
p.Name = "Marcus"; // Stoppa in ett namn i propertyn
p.Name = "Erik"; // Ändra namnet
p.Name = "Johan"; // Ändra namnet igen
}
/*
Exempel på metod som matchar din delegate
Vilka parametrar som skickas med bestämmer du själv.
Faktum är att det fungerar som ett vanligt metodanrop, så om
du ändrar i parametrar som är byref (exempelvis objekt) så
kommer de att ändras hos metoden som skickade eventet.
Det kan vara bra att ha ibland.
*/
private static void PropertyNameHasChanged(string name)
{
Console.WriteLine(name);
}

Nu skapar vi en klass vi kallar Person
class Person
{
/*
Sen deklarerar du ett event som med den delegate du skapat innan
*/
public event HasChanged NameChanged; //vår delegate
private string name; // sträng för propertyn
private string oldName = "Nobody"; // sträng för att komma ihåg ifall om den ändras
public string Name // vår property
{
get { return name; } // inget konstigt här
set
{
name = value; // sparar namnet som skickats in till propertyn
/*
för att skicka en avisering (event), kolla först om någon metod prenumererar
på den, antingen via if (NameChanged!=null) NameChanged.Invoce("Tralala");

eller använd ? som i exemplet nedan.
*/
NameChanged?.Invoke($"{Name} was changed from {oldName}"); // vår event
oldName = value; // sparar namnet för för att komma ihåg ifall det ändras
}
}
}

Om du testkör koden kommer du att få en avisering varenda gång namnet på personen ändras

Varför händer detta?
I vår main valde vi att prenumerera på själva eventet, och när eventet triggades skulle metoden
PropertyNameHasChanged(string name).

I vår klass Person definierade vi vårt event. I vår property Set metod triggar vi eventet varje gång
namnet ändras. Varje gång eventet triggas så anropas PropertyNameHasChanged(string name) som vi
valde. Flera klasser kan prenumerera på samma event och alla kommer att anropas i den ordning som
de skrev in sig som prenumeranter för eventet.
Tack och lov behöver vi inte tänka på hur många det är, allt vi behöver göra är att köra kommandot
Invoke på vår event. Glöm inte att kolla om den är null! Är den null så finns inga prenumeranter och då
behöver man inte trigga eventet.

TLDR;
Delegate bestämmer hur metoden ska se ut. Delegates kan också användas som variabler.
Events baseras på Delegates vid deklaration.
Events anropas såhär: myEvent?.Invoke(/* parametrar */)
Man prenumererar på eventet genom att skriva myEvent+=myMethod();
