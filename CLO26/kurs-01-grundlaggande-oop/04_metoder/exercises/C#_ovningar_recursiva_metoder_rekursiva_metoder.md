---

title: Rekursiva Metoder
author: Marcus Ackre Medina
type: exercise
topic: methods
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Recursiva metoder/Rekursiva metoder.docx"
description: "Rekursiva metoder är metoder som plockar fram information (antingen från ett objekt eller från en"
tags: ["csharp", "exercise", "git", "methods", "metoder.docx", "oop", "rekursiva", "sql"]
week_fit: []
---
Rekursiva metoder
Rekursiva metoder är metoder som plockar fram information (antingen från ett objekt eller från en
beräkning) och sedan anropar sig själv för att gå djupare in i spiralen av information.
Se Wikipedia :




https://sv.wikipedia.org/wiki/Rekursion
https://sv.wikipedia.org/wiki/Rekursiv_funktion
https://sv.wikipedia.org/wiki/Rekursiv_algoritm

Jag har använt detta för att lösa problem med kartor där man ska hitta den kortaste vägen att köra när
det finns flera olika vägar, som i sin tur går till andra vägar och i sin tur korsar varandra. Så jag gjorde en
rekursiv metod som sökte igenom en väg, när den hittade en avfart anropade sig själv med hela
historiken från vägen innan. På så sätt gick den igenom en labyrint av vägar och stoppade om vägen
korsade sig själv. Samma sak om den kom till en återvändsgränd eller ett vägslut som inte var målet. Till
slut fick jag en mängd med olika vägar och valde alltid den kortaste. När jag skulle göra uppgiften visste
jag inte hur jag skulle lösa det, men råkade läsa en artikel om hur mögel hittar vägen till mat. Den provar
bokstavligt talat alla vägar, där den fastnar eller springer på sig själv så dödar möglet sin ”arm” och
fortsätter att prova olika vägar tills den hittat maten. Alltså skrev jag kod som simulerade möglet och
löste problemet.
Lästips + video: https://www.scientificamerican.com/article/brainless-slime-molds
En annan visuell beskrivning av hur rekursivitet fungerar är filmen ”Next” med
Nicholas Cage där han spelar en man som kan se alla tänkbara möjligheter i
framtiden och välja den som fungerar bäst. (Filmen baserar sig på Novellen ”The
golden man” av Phillip K Dick och är helt annorlunda än filmen, förutom just delen
med att se alla tänkbara lösningar till en situation.)
IMDB länk: https://www.imdb.com/title/tt0435705/
Rekursiva metoder är bra att ha men de kan lätt bli komplicerade. Gör man fel kan
de låsa programmet och krascha i en ”Stack Overflow” som får processorn att gråta.

Familjeträd
Nu ska vi titta på ett familjeträd, det är ett typiskt fall där man kan använda rekursiva sökningar.
Namn
Fader
Moder
Shmi Skywalker
Anakin Skywalker
Shmi Skywalker
Leia Organa
Anakin Skywalker
Padmé Amidala
Luke Skywalker
Anakin Skywalker
Padmé Amidala
Vi utgår från en enkel class som lagrar namnet på personen och länk till föräldrarna, länken är ett objekt
av samma typ som classen, knasigt nog. Länkarna är som vägarna möglet följde.
class Person
{
public Person Father { get; set; }
public Person Mother { get; set; }
[DebuggerDisplay("{Name}")]
public string Name { get; set; }
}

När vi ska söka efter en specifik person kan vi använda Linq (istället för att loopa)
var find = n.Name.Contains(name, StringComparison.OrdinalIgnoreCase);

StringComparison.OrdinalIgnoreCase ser till att vi kan söka upper och lowercase blandat
för att söka efter flera personer kan vi göra såhär
Family.
Where(
n => n.Name.Contains(name, StringComparison.OrdinalIgnoreCase)
)
.OrderBy(n => n.Name)
.ToList()
.ForEach(
person => Console.WriteLine(person)
);

ToList() behövs för att Where och OrderBy returnerar en IEnummerable och dessa fungerar inte alltid så
bra med ForEach, därför måste vi omvandla objektet som returnerats till en Lista eller en Array
Men hela utmaningen den här gången är att hitta förfäderna och förmödrarna.
När vi tittar på person-classen ser vi att de har Person-objekt som med föräldrarnas data.
Så vi kan få fram namn på personen och föräldrarnas namn såhär
Console.WriteLine(person.Name);
Console.WriteLine(person.Mother.Name);
Console.WriteLine(person.Father.Name);

I teorin kan vi gå tillbaka ännu längre så länge inte något värde är null.
Console.WriteLine(person.Mother.Mother.Name); // Mormor
Console.WriteLine(person.Mother.Father.Name); // Morfar
Console.WriteLine(person.Father.Mother.Name); // Farmor
Console.WriteLine(person.Father.Father.Name); // Farfar

Konsten att vara en bra programmerare är att vara en lat
programmerare,
låt datorn göra jobbet åt dig!

Om vi för en loop exempelvis
LOOP
If mother is not null Print Mother.Name
If father is not null do Print Father.Name
END LOOP

Då får vi reda på föräldrarna för personen, loopen gör ingen större nytta där egentligen
Vi provar igen
START
If mother is not null
Print Mother.Name
Print Mother.Mother.Name
If father is not null do
Print Father.Name
Print Father.Father.Name
END

Nu är vi en generation bak i alla fall, men det ger inte hela historien
Hur många if och hur många gånger ska vi behöva upprepa koden? Rekursion kan lösa problemet, då vi
inte vet hur många steg bak vi ska eller kan gå.
START
FindAncestors(Person)
END

START FindAncestors
Print Person.Name
If mother is not null do FindAncestors(Mother)
If father is not null do FindAncestors(Father)
END

Vad händer nu?
Vi kallar på metoden FindAncestors och skickar in en person.
I FindAncestors skriver vi ut namet på personen och sen kollar vi



om personen har en mor och i så fall kallar vi på sig själv med moderns data, då kommer den att
skriva ut moderns namn och sedan söka efter information om moderns föräldrar osv
Samma sak med faderns data

Så har vi skapat en rekursiv algoritm, den anropar sig själv tills den inte har mer data att visa
private static void FindAncestors(Person person)
{
if (person != null)
{
Console.WriteLine(person.Name);
if (person.Mother!=null) FindAncestors(person.Mother, tab);
if (person.Father!=null) FindAncestors(person.Father, tab);
}
}

Detta kan orsaka en hel labyrint av vägar, och därför kan man inte riktigt göra samma sak med en loop,
vi kan kolla på familjeträdet

Bildkälla: https://www.reddit.com/r/StarWarsEU/comments/fsim9b/expanded_universe_skywalker_family_tree_included

Om vi tittar på Luke ser vi att hans föräldrar är Anakin och Padme, Anakins moder är Shmi och Padmes
föräldrar är Ruwee och Jobal, vi kan även se att Jobals mor är Ryoo. Tittar vi på hans son Ben Skywalker
så blir det genast fler steg att vandra i träder om man ska hitta alla förföräldrar.
Familjeträd är inte raka linjer, de sprider ut sig oavsett vilket håll man går, då är det inte så lätt att kolla
med en loop.
Man skulle förvisso kunna sortera eller filtrera på föräldrar och på så sätt få fram alla barnen i en familj,
men att sen stega bakåt i trädet blir komplicerat i en loop.
Fördelen med en rekursiv metod är att den är oftast enkel i sin form och den löser många problem,
nackdelen är att det blir väldigt komplicerat att debugga för att dess beteende kan verka kaotisk för oss.
Det blir ungefär som att försöka hitta den fysiska personen i ett rum med speglar

Man måste i alla fall ha en slutpunkt där den rekursiva metoden vet att den inte ska fortsätta med
rekursiviteten, annars riskerar man att gå slut på minner och få ”Stack Overflow”
Så för att en rekursiv metod ska fungera ska den kunna ta emot information som den kan bearbeta, och
skicka vidare till sig själv för att bearbeta nästa generation. Den behöver också veta när den ska sluta,
exempelvis om man talar om att den får högst gå x antal generationer bakåt. I fallet med familjeträdet så
slutar den när det inte finns far eller mor att undersöka.
void FindAncestors(Person person, int currentGenetation=0, int MaxGeneration=15)
{
if (currentGenetation<MaxGeneration)
{
… gör en massa roligt
FindAncestors(person.Mother, int currentGenetation+1, int MaxGeneration=15)
}
}

Oftast låter man den räkna ner mot noll med ett värde man skickar in. Men sätter man ett givet
gränsvärde kan man addera antal gånger man gått ner i listan. Men i fallet med familjeträdet letar vi
efter NULL objekt som signal för att sluta leta, så generationskontroll behövs inte.

Det är också vanligt att man använder sig av out variabler för att alla generationer ska påverkas av
samma variabel, exempelvis om man vill fylla på en sträng eller addera till en int vid varje iteration av
metoden.
private static void CollectNumbers(int start, out string AllNumber)
{
if (start>0)
{
AllNumber+=start.toString();
CollectNumbers(start - 1, out AllNumber);
}
}

Relativt meningslös rekursiv metod men den förklarar tanken i alla fall, den är kanske smartare om man
multiplicerar värdet på talet som kommer in med sitt nuvarande resultat och på så sätt få fram mer
vettig värde en en sträng med siffor…

Twelve days of Christmas
Vi tar ett bättre exempel på rekursivitet
[Verse 1]
On the first day of Christmas, my true love sent to me
A partridge in a pear tree
[Verse 2]
On the second day of Christmas, my true love sent to me
Two turtle doves, and
A partridge in a pear tree
[Verse 3]
On the third day of Christmas, my true love sent to me
Three french hens
Two turtle doves, and
A partridge in a pear tree

Och så fortsätter den till tolfte dagen
Hur fixar vi detta.
Först skapar vi självklart en array med alla dagar och en array med alla gåvor och en standard variabel
för första raden som vi kan använda med string.format.
string[] Days = new string[] { "first", "second", "third", "fourth", "fifth", "sixth", "seventh", "eighth ",
"ninth", "tenth", "eleventh", "twelfth" };
string[] Gifts = new string[] { "a partridge in a pear tree", "two turtle doves", "three French hens", "four
calling birds", "five gold rings", "six geese a-laying", "seven swans a-swimming", "eight maids amilking", "nine ladies dancing", "ten lords a-leaping", "eleven pipers piping", "twelve drummers
drumming" };
string Verse = "On the {0} day of Christmas my true love gave to me\n{1}";

Sedan kör vi en loop
for (int i = 0; i < 12; i++)
{
Console.WriteLine(
string.Format(Verse, Days[i], (i == 0 ? "\n" : "") + SayIt(i)));
Console.WriteLine();
}

Vi hämtar första raden i versen från variabeln Verse, sedan kör vi den genom String.Format för att få
snygare output.
i parameter {0} lägger vi in värdet från Days[loop] och sedan anropar vi vår rekursiva metod med värdet
på loopen för att lägga in den på parameter {1}. Slutligen lägger vi in en tomrad.
Det ser enkelt ut, eller hur? Det är enkelt faktisk… men det tog en stund att komma på hur jag skulle
göra :-/
private static string SayIt(int i)
{
if (i > 0)
{
var say = t.Gifts[i];
if (i == 1) say += "\nand "; else say += ",\n";
say += SayIt(i - 1);
return say;
}
else
{
return t.Gifts[i];
}
}

Observera att första delen av if-satsen kan skrivas såhär
return t.Gifts[i] + ((i == 1 ? "\nand " : ",\n")) + SayIt(i - 1);

eller så kan vi göra om hela if-satsen till
return i > 0 ? t.Gifts[i] + ((i == 1 ? "\nand " : ",\n")) + SayIt(i - 1) : t.Gifts[i];

men det blir oläsligt.
Metoden kollar om dagen som skickas in är större än 0. I så fall skapar den variabeln say och lägger in
”and” om det är näst sista gåvan i listan (egentligen första) eller kommatecken om det inte är näst sista.
Därefter anropar den sig själv och lägger resultatet i variabeln say som den sedan returnerar.
Om vi skickar in 0 får vi svaret ” a partridge in a pear tree” då ifsatsen kommer att skicka den till else
delen och i else delen returneras bara gåvans beskrivning.
Vi skickar in 1 och får svaret
two turtle doves
and a partridge in a pear tree

Då iterationen är ett och då kommer den att lägga in ”two turtle doves” i variabeln say. Den kommer
även att lägga till ”and” (då det är ett) och sedan anropa den sig själv med värdet (1-1). Då den nu får in
noll så returnerar den bara första gåvan och då hamnar vi tillbaka i första iterationen och där fortsätter
vi med variabeln say som lägger till gåvan i sitt värde och returnerar båda gåvorna.
Nu skickar vi in 2
three French hens,
two turtle doves
and a partridge in a pear tree

Då värdet är två kommer den att lägga in ”three french hens” i variabeln say, och då iterationens värde
int är 1 kommer att lägga till ett komma. Sedan anropar den sig själv med värdet (2-1). Då iterationen är
ett kommer den att lägga in ”two turtle doves” i variabeln say. Då iterationens värde är 1 kommer den
också att lägga till ”and” och sedan anropar den sig själv med värdet (1-1). Då den nu får in noll så
returnerar den bara första gåvan och då hamnar vi tillbaka i första iterationen och där fortsätter vi med
variabeln say som lägger till gåvan i sitt värde och returnerar båda gåvorna.
Osv.
Ganska coolt faktiskt!
Koden till familjeträdet och 12 days och Christmas kommer att dyka upp på
https://github.com/marcusjobb/Net20

Slutligen
Det vanligaste exemplet för rekursivt tänkande är Fibbonacciserien
TED-talk: http://www.ted.com/talks/arthur_benjamin_the_magic_of_fibonacci_numbers
Och kolla sedan in koden på
https://www.c-sharpcorner.com/UploadFile/d0e913/the-fibonacci-numbers/

Övningsexempel:



https://www.w3resource.com/csharp-exercises/recursion/index.php
https://www.c-sharpcorner.com/UploadFile/955025/C-Sharp-interview-questions-part4what-isa-recursive-function-in/

Videos:




https://www.youtube.com/watch?v=EpU0opEeP6g
https://www.youtube.com/watch?v=CcYi5eS3yqA
https://www.youtube.com/watch?v=NkKC8Vsf3rI
