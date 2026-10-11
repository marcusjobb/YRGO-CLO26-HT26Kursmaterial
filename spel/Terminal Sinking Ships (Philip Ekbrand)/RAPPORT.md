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

>  försökte att trigga allt vi game, skapar dem nya objekten vi game. tycker också att det blir lättare att hålla koll på spelars bräde(board) när jag skapade upp det på detta sättet. (samma med ships sen!)

### Metoderna

>  många methoder, men dem största och viktigast är ju startGame och placment. Dem är väldigt simpla men också väldigt lätta att följa, vad är det som händer? 

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

> Jag la till att om det är en träff så kikar vi på dem närmsta cordinaterna!

### Varför vi löste det såhär

> Om vi träffat kallar vi på en method som retunerar en lista på tänkbara cordinater! Vi kikar dock så vi inte har skutit på dem innan. och vi fortsätter till skeppet har sjunkit eller det inte finns några target kvar. borde egntligen kikat på riktining etc om vi träffar två kordinater men skeppet inte har sjunkit men det får blir v2, och kanske att man kan möta någon riktigt spelare.

---

## Git-logg

Klistra in utskriften från `git log --oneline` här:

```
81735a7 (HEAD -> main, origin/main) working coordinats (Console.WriteLine). light clean up
6f34c9e working game, Bot v1.1
dac4fb8 Working game, bots board visual
bf362cc Working Missile logic, need to fix Console.Writeline(cordinates)
b9c936e First working logic of placing ships!
34bc6a4 First commit, classes need alot more work

```
