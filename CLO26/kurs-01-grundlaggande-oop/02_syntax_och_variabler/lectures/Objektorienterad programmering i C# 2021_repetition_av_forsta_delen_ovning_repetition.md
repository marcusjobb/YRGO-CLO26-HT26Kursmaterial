---

title: Övning Repetition
author: Marcus Ackre Medina
type: lecture
topic: syntax
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/Objektorienterad programmering i C# 2021/Repetition av första delen/Övning repetition.docx"
description: "Klicka eller tryck här för att ange datum."
tags: ["csharp", "repetition.docx", "syntax", "övning"]
week_fit: []
---
Campus Mölndal .Net 21
Marcus Medina
Klicka eller tryck här för att ange datum.

Övning repetition
Beskrivning:

I den här övningen ska vi skapa projekt för att fräscha upp minnet om grundläggande delar i C#

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

Utveckla felfria fristående program.

Att skapa felfria fristående program.

Termer för övningen:


int page = 0;

Campus Mölndal .Net 21
Marcus Medina

Projektinstruktioner:
Detta projekt går utmärkt att göra tillsammans med andra, antingen i basgruppen eller i par. Välj en
klasskamrat du vill arbeta med och använd LiveShare eller Discordens Screen Share för att
samarbeta.

Kodning:
For-repetition
1.
2.
3.
4.

Skapa en for loop som räknar från 1 till 10
Skapa en for loop som räknar från 10 till 1
Omvandla for-looparna till While
Fråga användaren om ett namn och skriv det baklänges

ForEach-övningar
Skapa en array med 10 namn
1. Foreacha den och skriv ut alla namnen
2. Foreacha den och skriv ut listan baklänges (kan man göra det utan räknare)
Nästlad-loop övningar
1. Skapa en int variabel som sätts till noll. Gör en loop kallad minut, den ska gå från 0 till 59. I
loopen skapar du en loop kallad sekund och kör den från 1 till 59. Öka din variabel med 1.
När alla looparna är klara bör din variabel vara antal sekunder som finns på en timme.
2. Använd nästlade loopar för att rita en fyrkant som är 20 tecken bredd och 10 tecken hög

Parse-övningar
1. Skapa en sträng med värdet ”15.42” och gör om det till en lämplig nummerisk typ
2. Skapa en sträng med värdet ”1337” och gör om det till en lämplig nummerisk typ
3. Skapa en sträng med värdet ”katt” och gör om det till en lämplig nummerisk typ (Kan man
göra det?)

TryParse-övning
1. Skapa en sträng med värdet ”15.42” och gör om det till en lämplig nummerisk typ
2. Skapa en sträng med värdet ”1337” och gör om det till en lämplig nummerisk typ
3. Skapa en sträng med värdet ”katt” och gör om det till en lämplig nummerisk typ

While-Övningar
1. Gör en While loop som ska köra max 5 gånger. I den slumpar du fram tal mellan 1 och 100.
2. Gör en while loop som körs tills ett slumptal mellan 1 och 100 blir 55

DoWhile-övningar
1. Skapa en bool variabel kallad check, sätt den till true.
a. Skapa en Do While loop som körs så länge check är sant.
b. I Loopen ska du sätta check till att bli not checked, alltså checked=!checked;
c. Skriv ut checks värde varje runda loopen kör
2. Skriv ett spel där datorn ska gissa ett tal mellan 1 och 1000 som du tänker på.
a. Tipsar dig om gissningen är för hög eller för låg.
int page = 1;

Campus Mölndal .Net 21
Marcus Medina
b. Avsluta loopen när datorns gissning = ditt hemliga tal

Properties-övningar
1. Skapa en klass med propertyn Namn.
a. Gör den till en property med bakomliggande variabel. I set skriver du ut variabeln
och sedan ”har ändrats till ” och slutligen skriver du ut value. Sätt variabeln till att bli
value. Prova från main att ändra Namn propertyn flera gånger.
b. Skapa en property kallad IsAlive och ge den värdet true som standard
c. Skapa en property som heter Age. Kontrollera så att när variabeln sätt, att den inte
får vara minusvärde eller högre än 150. Om value överstiger 150 sätt isAlive till false.
2. Skapa en klass kallad MagicNumber.
a. I den har du en property kallad Value.
b. Om värdet överstiger 22 sätt den till 22
c. Om värdet understiger 0 sätt den till 0

Arv-övningar
1. Skapa en klass kallad Dog
a. Ge den vanliga properties som Name, Race, Age
2. Skapa en klass kallad Pitbull som ärver dog
a. Ge den bool propertyn Playful
3. Skapa en klass kallad Poodle som ärver dog
a. Ge den bool propertyn Barking
4. Från main skapa en pitbul
a. Ge den namn, ålder och sätt den till playful
5. Från main skapa en Poodle
a. Ge den namn, ålder och sätt den till playful (funkar det?)
b. Sätt Barking till true

Det kommer mera….

int page = 2;
