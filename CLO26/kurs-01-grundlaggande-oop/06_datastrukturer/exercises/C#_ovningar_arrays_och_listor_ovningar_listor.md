---

title: Övningar Listor
author: Marcus Ackre Medina
type: exercise
topic: datastrukturer
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/Arrays och listor/Övningar - Listor.docx"
description: "För tips: Kolla https://www.tutorialsteacher.com/csharp/csharp-collection"
tags: ["csharp", "datastrukturer", "exercise", "git", "listor.docx", "övningar"]
week_fit: []
---
Övningar – Lista
För tips: Kolla https://www.tutorialsteacher.com/csharp/csharp-collection

Övning 1: List<Int>
1.
2.
3.
4.
5.
6.
7.

Skapa en lista av typen List<int>
Lägg till 10 slumpmässiga värden
Summera alla värden
Räkna ut medelvärdet
Skriv ut talen
Skriv ut summan
Skriv ut medelvärdet

Övning 2: List<double>
1.
2.
3.
4.
5.
6.

Skapa en lista av typen List<double>
Fråga användaren mått för en rätvinklig triangel
Lägg alla sidorna i din lista
Använd lista.Sort() för att sortera din lista
Räkna ut omkretsen av triangeln
Räkna ut arean av triangeln
Då talen är sorterade kommer det högsta värdet att hamna i slutet, och i en rätvinklig
triangel är det de första och mindre talen som utgör basen och höjden.
7. Skriv ut resultatet

Övning 3: List<string>
1.
2.
3.
4.

Skapa en List<string>
Lägg till flera namn i listan
Använd .Sort() metoden för att sortera namnen
Skriv ut listan

Övning 4: StringList
1. Använd samma kod som förra exemplet men ersätt List<string> med StringList
2. Diskutera skillnaden

Övning 5: ArrayList
1.
2.
3.
4.

Skapa en ArrayList ( ArrayList minLista = New ArrayList(); )
Lägg till flera namn i listan
Lägg till flera siffror i listan
Skriv ut listan
a. Om det är en siffra som kommet ut, multiplicera den med 2

Övning 6: Dictionary
1. Skapa en Dictionary<string,string>
2. Lägg till
a. ”Hej” och ”Hejsan!”
b. ”Hur mår du” och ”Bra, tack och du”
c. ”Bra” och ”Det gör mig glad”
d. ”Dåligt” och ”Det gör mig ledsen”
e. ”inte bra” och ”Attans!”
f. ”Vad heter du” och ”Pladderbot”
3. Loopa tills användaren skriver sluta
a. Fråga nu användaren om ett kommando
b. Om din lista innehåller nyckeln för det som användaren skrev
i. If (dictionary.ContainsKey(input))
c. Skriv ut värdet från din dictionary dictionary[input]

Övning 7: Sök i listor
1.
2.
3.
4.

Skapa en List<string> eller en StringArray
Lägg till ett tiotal namn
Fråga användaren om ett sökord
Loopa igenom listan
a. Kolla om något värde innehåller sökordet
b. minVariabel.IndexOf(sökord)
i. Noll och större talar om positionen var det finns
ii. Minus ett betyder att den inte hittat något
c. Skriv ut det som matchar

Övning 7: Sök nummer i listor
1.
2.
3.
4.

Skapa en List<int>
Lägg till ett tiotal siffor
Fråga användaren om en siffra
Loopa igenom listan
a. Kolla om något värde innehåller sök-siffran
b. Skriv ut det som matchar

Övning 8: Sök större nummer i listor
1.
2.
3.
4.

Skapa en List<int>
Lägg till ett tiotal siffor
Fråga användaren om en siffra
Loopa igenom listan
a. Kolla om något värde som är större än det som användaren angav
b. Skriv ut det som matchar

Övning 9: SortedList
1. Skapa en SortedList<string, string>
2. Lägg in nycklar och värden i det, på samma sätt som du la till värden i din dictionary
a. 4
Fyra
b. 3
Tre
c. 0
Noll
d. 2
Två
e. 1
Ett
f. 9
Nio
g. 7
Sju
h. 5
Fem
i. 8
Åtta
j. 6
Sex
3. Skriv ut listan
4. Fråga användaren om ett nummer
5. Loopa igenom strängen med det du fick av användaren
a. Om tecken i strängen finns i din sorterade lista
i. Skriv ut värdet
Exempel:
 152 ger resultatet EttFemTvå
 322 ger resultatet TreTvåTvå
 1337 ger resultatet EttTreTreSju

Övning 10: Stack
1.
2.
3.
4.
5.
6.
7.
8.

Skapa en Stack<int>
Lägg till värden från 1 till 20 använd stack.Push(x)
Skriv ut resultatet av stack.Pop();
Skriv ut resultatet av stack.Pop();
Skriv ut resultatet av stack.Pop();
Skriv ut resultatet av stack.Peek();
Skriv ut resultatet av stack.Peek();
Skriv ut resultatet av stack.Peek();

Övning 11: Queue
1. Skapa en Queue<string>
2. Lägg till med kommandot queue.Enqueue(x)
a. Borsta tänderna
b. Duscha
c. Torka sig
d. Torka av golvet
e. Klä på dig
f. Gå till skolan
g. Plugga massor
3. Skriv ut listan med en while
a. While (queue.Count>0)

i. Skriv ut queue.Dequeue();

Övning 12: Diskussion i gruppen
a. Vad kan man ha de olika listorna till
b. Ge förslag på program för var och en av listorna
