# Rapport — Slutprojekt

**Kurs:** Grundläggande OOP i C#  
**Projekt:** *(Skogsäventyret / Havsforskarna / Dungeon Crawler)*  
**Grupp:**  Aysha-Luvinda-Anton-Group
**Datum:**  4/10- 26
**GitHub:**  https://github.com/Aysha-Nihal/Skogsaventyret
**Commit-hash vid inlämning:**  346c8b4

---

## Gruppmedlemmar

*Fyll i alla som medverkat i gruppen:*

| Namn | Lämnar in rapport? |
|------|--------------------|
|Anton | ✅ Ja (den här personen — lämnar in Zip + RAPPORT.md + REFLEKTION.md) |
|Aysha | ❌ Nej (lämnar in endast REFLEKTION.md) |
| Luvinda| ❌ Nej (lämnar in endast REFLEKTION.md) |

*(Lägg till eller ta bort rader efter behov)*

---

## Instruktion

Förklara kortfattat hur ni löste varje krav. En till tre meningar per punkt räcker — ni ska visa att ni **förstår** det ni byggt, inte skriva en uppsats. Skriv med egna ord och kopiera inte uppgiftsbeskrivningen.

Har ni inte fyllt i VG-delen → projektet bedöms som G.

---

## G — Godkänt

### Klasserna

Vi skapade klasserna Program.cs (mainklassen) som låter användaren loopa igenom spelet för att ta sig fram i sitt äventyr. Player.cs för spelarklass där fält för egenskaper och metoder som låter användaren utföra aktioner och översyn. Monster.cs för monster och dess egenskaper samt subklasser till Monster.cs (basklass) för att få med olika varianter av Monster - GiantTeddyBear.cs, GrumpyTeddyBear.cs och TeddyBear.cs (arv).
> 

### Arv och `List<T>`

*Vi har Monster som en Basklass som beskriver vad ett Monster är och kan utföra. Subklassern står under Monster i sin Hierarki. Vi har använt List i vår mainklass för att skapa olika Monster och lagra dem i en lista för att kunna ta fram dem i spelloopen.

> 

### Spelloopen

Spelar får börja med att ange ett namn, felaktigt namn skickar spelaren tillbaka till namninput genom ett lambda. Spelar får 5 olika alterativ att välja bland, ta sig genom skogen för att hitta en utväg, vila sig, sätta sig i arenan, se sina insatser eller avsluta spelet. Vi har inte hanterat en try-catch eller liknande.
> 

### UML

Vi målade en enkelt bild som vi lika snabbt skrotade. Flödesschema saknas.
> 

### Git

Vi arbetade för det mesta remote, alltså var och en hemma för sig. Agilt så gott det gick men kunde varit bättre. Försökte att arbeta via egen branch vid de flesta tillfällen och pusha ofta. Inga större mergekonflikter uppstod men vid de få tillfällen så löste det sig hyggligt. 
Klistra in utskriften från `git log --oneline`:

```
a791b15 (HEAD -> main, origin/main, origin/HEAD) corrected days not couting in mainclass
4cdd78b fix: fixa Monster skada
707e1b8 (anton) conflict solved
2f1948d changed takedamage and new output in terminal
1f733c5 Increase teddy bear monster HP
42a4bbf feat: update Player and Program
257088d Merge pull request #3 from Aysha-Nihal/aysha
b34b3c0 Removed the unnecessary Console.WriteLine lines I had added
9aad91b Merge pull request #2 from Aysha-Nihal/aysha
112cc33 Add Rapport and Reflektion files
e0263d4 Merge pull request #1 from Aysha-Nihal/aysha
07c90e0 add a line in Program.cs
19cbc02 feat: uppdatera Program
4d536df feat: implementera skogsäventyret
0f3ac6f Add Teddybear monster types
2ee8448 (origin/anton) added user input
dc6f633 Merge branch 'anton'
97a548f added switch to main and static ReadChoice metod
726b8eb added while loop
0bae969 added monster constructor
89f4ae1 changed constructor
5255ce3 added heal function

```

### Kodkvalitet

*Nämn ett exempel på ett bra namn och en kommentar ni skrev som förklarar **varför**, inte *vad*.*

> //feel free to change, might need to place some code into other classes and refactor code


    string inputName;

Genom att kommentarer tagit bort allt eftersom kod har ändrat var det svårt att hitta en "bra" kommentar, men detta är väl det närmsta "bra" man kan komma.
Variabelnamn för användaren alternativ att få välja namn.

---

## VG — Motivering

> Fyll i det här avsnittet om ni siktar på VG. Lämna tomt = G-bedömning.

### Vad vi lade till

*Vilka VG-delar byggde ni (vapen/shop/arena, VG-utbyggnad, låsta dörrar/NPC/spara m.m.)?*

> 

### Varför vi löste det såhär

*Vilka datastrukturer valde ni och varför? Vad var alternativen?*

> 
