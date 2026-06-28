---

title: Tdd Testcase Övningar
author: Marcus Ackre Medina
type: exercise
topic: testing
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/Material från Codic/C#/Övningar/TDD/TDD testcase övningar.docx"
description: "Ditt team arbetar med att utveckla ett CRM och du har fått i uppdrag att bygga till en importfunktion"
tags: ["csharp", "exercise", "tdd", "test", "testcase", "testing", "övningar.docx"]
week_fit: []
---
TDD – Småupdrag
Övning 1 - CRM
Ditt team arbetar med att utveckla ett CRM och du har fått i uppdrag att bygga till en importfunktion
som ska läsa in JSONfiler med namnlistor.
Exempelvis:
[{"guid":"f8bcfa4a-bd9f-4f92-bd3c-08b7e3543c74","age":39,"name":"Howard Moore","gender":"male","company":"LETPRO","email":"howardmoore@letpro.com","phone":"+1 (995) 4432936","address":"742 Colonial Road, Chapin, West Virginia, 1726"},{"guid":"f1b7cb47-a02b-4477-ab24-2e6a6040d473","age":26,"name":"Frazier
Riddle","gender":"male","company":"INFOTRIPS","email":"frazierriddle@infotrips.com","phone":"+1 (995) 423-2502","address":"729 Bayview Place, Jamestown, Massachusetts, 8838"},
{"guid":"456ba5f8-ab4f-4d94-b114-f0578a2f299c","age":39,"name":"Graves Boone","gender":"male","company":"ANIVET","email":"gravesboone@anivet.com","phone":"+1 (828) 5882527","address":"250 Pierrepont Place, Caroline, Ohio, 9563"},{"guid":"506cbb0e-6bb7-4348-bd91-5fadfd6e46ae","age":20,"name":"Iva
Morris","gender":"female","company":"ENORMO","email":"ivamorris@enormo.com","phone":"+1 (862) 487-2268","address":"342 Banker Street, Blanco, Missouri, 2806"},{"guid":"92fbe2727538-4481-83c6-fd8dc41af61d","age":25,"name":"Kirby Christian","gender":"male","company":"IMANT","email":"kirbychristian@imant.com","phone":"+1 (906) 420-2185","address":"420
Congress Street, Saranap, Puerto Rico, 7239"},{"guid":"8ff5885d-cbfa-4490-ab08-b68784a15d31","age":37,"name":"Atkins
Hicks","gender":"male","company":"XIIX","email":"atkinshicks@xiix.com","phone":"+1 (998) 512-2552","address":"857 Oriental Court, Leming, Oklahoma, 5234"},{"guid":"0b01d82581ca-4256-ad25-d8a6367f7482","age":21,"name":"Middleton Cooley","gender":"male","company":"FLEETMIX","email":"middletoncooley@fleetmix.com","phone":"+1 (970) 4273597","address":"172 Madison Street, Bellfountain, Indiana, 6674"}]

Importen ska gå till på två sätt.
1. En användare väljer fil att importera genom en dialogruta
2. En samling filer läggs i en specifik mapp som programmet läser av när den känner av när filer
lagts in
I båda fallen anropas importfunktionen som ska ta hand om datan.
Ditt jobb är nu att säkerställa att hela processen går rätt till.
Exempelvis, hur ska dubbletter av personer hanteras? Behöver man hantera dubbletter? Är tomma
dokument ett problem? Vad händer om JSON filen är felaktig? Vad händer om användaren ska välja fil
men trycker Avbryt? Det är många saker som kan gå fel. Skapa en lista på vad som behöver testas för att
vara säker på att importen går rätt till.
I denna uppgift behöver ni inte skriva någon kod, det räcker med att ni identifierar vad som kan gå fel
och vad som behöver kontrolleras.

Övning 2 - Profilverifiering
Ett företag har en söt liten community där man diskuterar allt från skräckfilmer till håriga och gosiga
djur. Då sidan funnits i flera år har många användare bytt emailadress med åren, men varit tvungna att
behålla samma adress på sidan, då sidan inte stödjer byte av email adress. Detta av den enkla orsaken
att det inte ska vara lätt att hijacka någons konto.
Nu har medlemmarnas rop om förändring blivit för stor så man beslutar sig för att lägga till den
funktionen. Hur försäkrar man sig om att kontot inte blir hijackat?
Det finns två sätt att byta emailadress:
1. Godkänna byte av mailadressen från sitt gamla konto (mail skickas)
2. Godkänna byte genom verifiering av användarens lösenord (inte lika säkert)
Positiv situation: Jenna vill byta från sitt gamla Jenna99@hotmail.com till jenna.karlsson@catlovers.com
och loggar in med sitt gamla konto, ändrar emailadress, verifierar från sitt gamla att mailbyte är OK.
Osäker men positiv situation: Peter har inte längre sitt gamla konto killer33@starraiders.org (eller så
har han glömt lösenordet) men vill byta till peter.johansson@gmail.com. Han ändrar sin emailadress och
väljer att verifiera bytet med sitt lösenord till sidan.
Hijacksförsök: Johan loggar in på ett cybercafé och glömmer att logga ut, en skojare bestämmer sig för
att ha kul på Johans bekostnad och ändrar emailadressen till sin egen. Johan får hem ett mail där han ska
verifiera bytet. Förhoppningsvis väljer han att inte göra det och då kommer skojaren inte att få tillgång
till kontot med sin emailadress.
Osis: Harald har glömt lösenordet till sitt konto (loggar in via lagrade cookies), han vill byta emailadress
men hans gamla emailadress är borta sedan länge. Han kan alltså inte beställa email för att verifiera och
han kan inte mata in sitt lösenord. Hur löser man detta?
I denna uppgift behöver ni inte skriva någon kod, det räcker med att ni identifierar vad som kan gå fel
och vad som behöver kontrolleras.

Mall för tester
Projekt
CRM
Modul
Import
Skapat av
Datum
2021-04-08
Kontroll av
Kontrolldatum
KolumnerScenarioId, Scenario, TestId, Test case, Steg, Testdata, Expected, Actual, Status,
Testare,Datum, Kommentar

Övrigt
För er som undrar hur man kollar om filer lagts till i en mapp, kolla här:
https://docs.microsoft.com/en-us/dotnet/api/system.io.filesystemwatcher?view=net-5.0
