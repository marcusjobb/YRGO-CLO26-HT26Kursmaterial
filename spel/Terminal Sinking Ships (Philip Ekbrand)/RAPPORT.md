# Rapport

**Kurs:** Grundläggande OOP i C#  
**Uppgift:**  Havsforskarna 
**Grupp:**  --
**Datum:**  2026-10-04
**GitHub:**  https://github.com/pekbrand/Terminal-Sinking-Ships/

---

## Gruppmedlemmar

| Namn | Lämnade in |
|------|-----------|
| Philip Ekbrand | Zip + RAPPORT.md + REFLEKTION.md |


---

## G — Hur vi löste uppgiften

> Förklara kortfattat hur du löste varje G-krav. En till tre meningar per punkt räcker.  
> Skriv med egna ord — kopiera inte uppgiftsbeskrivningen.

### Klasserna
> Game klasen ser till att vi har två spelar och ändrar status till placment.
>när spelarna skapas från game så skapar vi upp spelplanen för våra spelare.
>Spelplanen skapar upp skepp.


### Metoderna

>  många methoder, men dem största och viktigast är ju startGame och placment. 
> placment ser till att alla skeppen blir placerde, här kikar vi på vilken typ av spelare det är dvs bot eller human.
> Startgame håller koll på vems tur det är och + att här finns all logik för hur botten skall agera när vi träffar ett skepp.

### Main()

>  hålla det så städat som möjligt! startar upp spel objectet, se till att skeppen blir placerade och sen startar vi spelet tills alla skeppen är sjuktna/ upptäckta!

### Git

>  skapade upp den repo, hade kanske kunnat pusha lite mer ofta, men blir ju lite mindre när man är själv.

### Kodkvalitet

> Jag vet att kan bli bättre på namge varibler och methoder. Jag har även lagt till mycket som säkerställer att vi har valida inputs!

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.

### Vad vi lade till

> Jag valde att göra botten/AI smart. Detta pga av att spelet får lite mer verklighetshets känsla. En människa hade ju gissat på ungefär samma sätt när det väl blir en träff.


### Hade du kunnat lösa det utan List

> Det hade gått att lösa utan en lista med till exemple variabler eller en arrray.
> Men en lista är väldigt flexibel i detta fallet för det är enkelt att lägga till värden och även tömma listan när skeppt är sänkt.
> Hade jag istället valt en array hade det varit enkelt att uppdatera värderna men jag kan inte tömma eller ändra storlek på arrayen,
> Å andra sidan vet vi ju att det största skeppet är 5 i längd så det hade inte varit ett helt orimligt val.

### Varför vi löste det såhär

> Om vi träffat kallar vi på en method som retunerar en lista på tänkbara cordinater! Vi kikar dock så vi inte har skutit på dem innan.
> kordinaterna blir som ett kors, när vi väl träffar nästa kordinat, så jämför vi dem två kordinaterna för att veta om vi skall skjuta vågrätt eller lodrätt.
> vi gör en loop för att hitta min och max av dem träffade kordianterna, blir den en miss åt "min" ett håll så sjukter vi på "max" kordinaten istället.
> och vi fortsätter till skeppet har sjunkit eller det inte finns några target kvar. 


---

## Git-logg

Klistra in utskriften från `git log --oneline` här:

```
e4f09bc (HEAD -> main, origin/main) Bot v2, after 2 hits it finds direction
1473e6f Class Position was added
afbc829 New input handeler, row is now char
31b9c11 Winners is presented
9d884c7 Reflection and report v2
25dcad2 Reflections and report
81735a7 working coordinats (Console.WriteLine). light clean up
6f34c9e working game, Bot v1.1
dac4fb8 Working game, bots board visual
bf362cc Working Missile logic, need to fix Console.Writeline(cordinates)
b9c936e First working logic of placing ships!
34bc6a4 First commit, classes need alot more work


```
