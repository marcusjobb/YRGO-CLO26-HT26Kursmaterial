# Rapport

**Kurs:** Grundläggande OOP i C#  
**Uppgift:**  Slutuppgift Skogsäventyret
**Grupp:**  Team Jango Fett
**Datum:**  2026-09-28
**GitHub:**  https://github.com/DaniRushton/Skogs-ventyret.git

---

## Gruppmedlemmar

| Namn | Lämnade in |
|------|-----------|
| [Daniella] | Zip + RAPPORT.md + REFLEKTION.md |
| [Lazo] | REFLEKTION.md |


---

## G — Hur vi löste uppgiften

> Förklara kortfattat hur du löste varje G-krav. En till tre meningar per punkt räcker.  
> Skriv med egna ord — kopiera inte uppgiftsbeskrivningen.

### Klasserna

> Monster-basklassen har privata fält och auto-properties { get; private set; }, i dessa har vi satt i monstrets
  namn, hp, attack, xp och defense enligt krav. Efter basklassen för Monster var gjord kunde vi gå över till att
  skapa självaste monstrena där vi skapade tre olika med sina egna startvärden i konstruktorn för att slippa
  skapa nya klasser för varje nytt monster.

### Metoderna

> TakeDamage(int damage) minskar hp och ser till att hp:n aldrig går under 0. i det fall att hp:n blir 0 så
  returnar den att objektet är dött och anropar koden som säger att striden är över.
 
> AttackPlayer(Player player) låter monstret attackera spelaren och skriver ut vem som attackerar vem och hur
  mycket skada det blir. Anropar också player.TakeDamage() för att skadan faktiskt skall ske
 
> MonsterSpawner.SpawnRandomMonster() använder Random för att slumpa fram ett nytt objekt (monstertyp) varje 
  runda istället för att koden skall återanvända samma potentiellt döda monster runda efter runda. 
 
> BattleSystem.RunBattle() bröts ut till en egen klass så att skog och arena skulle kunna använda samma stridslogik så slipper vi 
  skriva om koden till båda filerna. Den returnar true ifall spelaren överlever striden och false om spelaren dog - där avslutas 
  spelet också. 
 
> Arena.RunArena() kör 7 fasta strider i rad mot monster i stigande svårighetsgrad genom att anropa BAttleSystem.RunBattle() för 
  varje mosnter i listan. Dör spelaren avbryts arenan och funktionen returnar false.

### Main()

> I Main() så efterfrågas spelaren sitt namn. Sedan körs en while-loop som representerar varje enskild dag. Spelaren väljer mellan 
  att 1) gå ut i skogen, 2) vila eller 3) gå till arenan. Väljer spelaren 1 så slumpas ett nytt monster fram med hjälp av 
  MonsterSpawner och självaste striden hanterar BattleSystem, där spelaren kan välja mellan att 1) attackera mosntret 2) försvara 
  sig eller 3) springa. Vilar spelaren sig så återställs spelarens HP, och väljer spelaren arenan så körs 7 strider med mosnter 
  sorterade i svårighetsgrad. Dör spelaren så avslutas spelet och visar en poängtavla med antal dagar överlevda, level och Xp, samt 
  en lista över besegrade mosnter.

### Git

> Vi har skapat ett gemensamt repo på GitHub som vi arbetat i tillsammans i våra egna commits för att kunna 
  tilldela ansvar av olika delar av koden som Monster eller Player- hierarkierna till varandra. Vi har använt 
  git pull för att 'ladda ner' varandras ändringar som finns på GitHub lokalt och sedan commitat och pushat 
  vår del av koden till GitHub, allt för att undvika att vår kod skriver över någon annans.

### Kodkvalitet

> Vi har gett variabler och metoder tydliga och beskrivande namn för att tydliggöra vad de gör, exempelvis
  SpawnRandomMonster som spawnar ett random monster. Kommentarer har också satts där koden inte är tydlig
  vad den gör, exempelvis vid skadeberäkningen för varje stridsval.

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.

### Vad vi lade till

> Vi lade till en Arena funktion där spelaren slåss mot 7 monster i stigande svårighetsgrad. Spelaren kan inte avbryta striden, 
  och vinner hen ges en stor XP-bonus. Eftersom spelaren inte kan läka sig/vila så krävs det att spelaren överlevt minst 7 dagar innan 
  besöksmomentet.

  Vi lade till en vapenshop som spelaren kan gå välja att gå till mellan strider. Där kan spelaren välja att köpa tre olika vapen 
  med olika attackvärden och olika priser som spelaren kan tjäna upp till genom att besegra monster i skogen. För att spelaren 
  skulle kunna köpa något i shoppen så behövde jag lägga till en Weapon-klass där jag strukturerade bas och sub-klasserna lite som
  jag gjorde i Monster klassen; en basklass med tre subklasser. I detta fall tre olika vapen med sina egna värden som subklasser.
  

### Varför vi löste det såhär

> Jag valde att bryta ut stridslogiken till en separat BattleSystem-klass så att vi skulle slippa behöva skriva samma kod två gånger 
  - en för skogen och en för arenan. Detta gör koden lättare att underhålla då man bara behöver ändra koden på ett ställe i det fall 
  att det behövs. Arena monstrena sattas i en fast ordning i en List<Monster> eftersom spelar alltid ska möta samma sju monster 
  i samma sorterade ordning (enklast till svårast).


---

## Git-logg

Klistra in utskriften från `git log --oneline` här:

```
43dfed9 (HEAD -> main, origin/main, origin/HEAD) Added Weapons and Shop
b41a291 updaterat rapport
6e3005f Made an arena, updated reflektion.md
f4e95d7 Implement Player class with attributes and methods
1a2eb50 Tagit bort player stub
dd8057a Flyttade striden till egen fil
1a2a8a3 Spelloopen och striden klar
023c2a1 Flyttar filsökväg och tar bort tillfällig Player-stub
9f7542d Added README, RAPPORT, REFLEKTION. Gjort en spelloop.
494c24a Added MonsterSpawner & Types
e221bb5 Ändrade klassnamn
4a960fc Skapat class Monster
e15c28d Added class Monster
a7fb4e8 Adderat gitignore


```