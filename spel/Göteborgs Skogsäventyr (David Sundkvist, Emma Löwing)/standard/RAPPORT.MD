# Rapport — Slutprojekt

**Kurs:** Grundläggande OOP i C#  
**Projekt:** Skogsäventyret – Göteborgs Skogsäventyr  
**Grupp:** team emma the destroyer och david the inhilator 
**Datum:** 2026-10-04  
**GitHub:** https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-.git 
**Commit-hash vid inlämning:** a53a85e

---

## Gruppmedlemmar

| Namn | Lämnar in rapport? |
|------|--------------------|
| [David Sundkvist] | ✅ Ja (den här personen — lämnar in Zip + RAPPORT.md + REFLEKTION.md) |
| [Emma Löwing] | ❌ Nej (lämnar in endast REFLEKTION.md) |

---

## G — Godkänt

### Klasserna

Vi skapade bland annat klasserna `Spelare`, `Monster`, `shop` och `items`. Klasserna har olika ansvarsområden. `Spelare` håller exempelvis reda på spelarens HP, attack, defense, level, XP och guld, medan `Monster` ansvarar för monstrens egenskaper och beteenden.

Vi använder privata fält för att informationen inte ska kunna ändras hur som helst utanför klassen och properties för att andra delar av programmet ska kunna läsa värdena. Konstruktorerna används för att ge objekten sina startvärden när de skapas.

### Arv och `List<T>`

Vi använde arv genom att `Sealmonster`, `FullChalmersStudent` och `MarcusMedInlämning` ärver från basklassen `Monster`. De olika monstren kan därför behandlas som `Monster`, samtidigt som de kan ha egna versioner av metoden `AttackPlayer()` genom `override`.

Vi använder bland annat en `List<Monster>` som kan innehålla de olika monstertyperna. I vanliga strider används listan för att slumpa vilket monster spelaren möter. Vi använder även listor för bland annat föremålen i shoppen och spelarens inventory.

### Spelloopen

Spelets huvudloop är en `while`-loop som fortsätter så länge spelarens HP är större än 0. I huvudmenyn kan spelaren välja mellan olika aktiviteter, till exempel att slåss, vila, besöka Pressbyrån eller delta i Valborgsarenan.

Vi tar emot spelarens val med `Console.ReadLine()` och använder `switch` för att bestämma vad som ska hända. Vi använder `default` för att fånga upp ogiltiga val, så att spelaren får ett meddelande och sedan kan försöka igen.
När en ny dag börjar visas också hur många dagar spelaren har överlevt. Efter Game Over visas spelarens slutstatistik och spelaren får möjlighet att starta om spelet. Om spelaren väljer att starta om skapas ett nytt Spelare-objekt med spelets ursprungliga startvärden. Omstarts-funktionen använder en yttre while-loop runt själva spelomgången och en separat loop som kontrollerar att spelarens val är giltigt.

### UML

Innan vi började koda diskuterade vi muntligt hur klasserna skulle vara uppbyggda och vi skrev ner en lista, men vi ritade inte ner något UML-diagram. Diagrammet nedan är därför gjort i efterhand för att visa den ursprungliga plan som vi utgick från.


Grundstrukturen med `Spelare`, basklassen `Monster` och de olika monsterklasserna blev ungefär som vi hade planerat. Under arbetets gång lade vi bland annat till dagar överlevda och metoden `AddDay()`. Till en början fokuserade vi framför allt på G-kraven och planeringen av VG-delarna var därför mindre tydlig. Shop, items, inventory, guld och arena utvecklade och planerade vi vidare allt eftersom projektet växte.

#### Ursprunglig plan

```text
┌─────────────────────────────────┐
│             Spelare             │
├─────────────────────────────────┤
│ - _namn : string                │
│ - _hp : int                     │
│ - _maxhp : int                  │
│ - _attack : int                 │
│ - _defense : int                │
│ - _level : int                  │
│ - _xp : int                     │
├─────────────────────────────────┤
│ + TakeDamage(damage) : bool     │
│ + Heal() : void                 │
│ + GainXP(mängd) : void          │
│ + LevelUp() : void              │
└─────────────────────────────────┘


┌─────────────────────────────────┐
│             Monster             │
├─────────────────────────────────┤
│ - _name : string                │
│ - _hp : int                     │
│ - _maxhp : int                  │
│ - _attack : int                 │
│ - _defense : int                │
│ - _xpdropp : int                │
├─────────────────────────────────┤
│ + TakeDamage(damage) : bool     │
│ + AttackPlayer(spelare) : void  │
└─────────────────────────────────┘
                  ▲
                  │
             ärver från
        ┌─────────┼─────────┐
        │         │         │
 Sealmonster    Full-     Marcus med
                Chalmers- inlämning
                student
```

### Git

Vi arbetade tillsammans i samma GitHub-repository och använde Git för att spara och dela våra ändringar. Vi gjorde separata commits för olika delar av arbetet och använde `pull` för att hämta varandras senaste ändringar och `push` för att skicka upp våra egna.

Vi försökte också dela upp arbetet så att vi kunde arbeta med olika delar av programmet samtidigt och på så sätt minska risken för konflikter.

Klistra in utskriften från `git log --oneline`:

```text
a53a85e vapen bonus i arena/skog metod
a4849ef vapnets skada i program.cs
7685b83 EquipWeapon metod i spalar klass
ef4d951 by wepon metod
b55144a fixade till shopmenu flick lite hj├ñlp med att skriva ut case kompakt fr├Ñn ai
388a010 la till wepons list i shopen
ef90d11 gjorde 2 olika beteenden p├Ñ axe och sword axe har 2*5 och longsword har 10+2
249b974 Merge branch 'main' of https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-
fb5ce82 wepon class sub longsword och greateaxe
45c6ed2 Create reflection document for project overview
532cfd7 Create Emmas reflektion.md
87ce985 Update commit hash in RAPPORT.MD
d645946 Update RAPPORT.MD
17fed2c Update RAPPORT.MD
9a7c95f Update RAPPORT.MD
170998f Revise reflections on project design and data structures
2fcae61 While-loop som g├Âr att spelet kan startas om n├ñr man d├Âr
44fdd54 L├ñgg till kostnad f├Âr vila och bekr├ñftelse innan arenan
35362f1 Fixa monsterbalans, specialattacker, dagmeddelanden och mindre buggar
54d64f3 Update Reflektion David Sundkvist.md
7a0da26 Update Reflektion David Sundkvist.md
f09403a snuns till chalmers
665cf0c showinventory i spelarklass samt adda inventory i huvudmenyn i program cs
6d97ee7 Merge branch 'main' of https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-
bc58694 Update Reflektion David Sundkvist.md
c921573 Rename Reflektion David Sundkvist to Reflektion David Sundkvist.md
7803092 Rename Rapport ÔÇö Slutprojekt to RAPPORT.MD
fd0e818 Add reflection document for David Sundkvist
d6e1229 Merge branch 'main' of https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-
d261335 blabla
34d7baf Update UML section for clarity
094ff62 Create project report for Skogs├ñventyret
56f0a28 Clear project report template
db2f9f0 gjorde s├Ñ det visar hur mycke guld man har i spelet/gold dropp fr├Ñn monster
36c1abb Fix formatting issues in Rapport ÔÇö Slutprojekt
8fa0010 Create initial project report structure
8bcaf37 shopen classen borde va klar och la till pressbyr├Ñn rad 17 i program cs
efbd3c2 Merge branch 'main' of github.com:DavidSundkvist/GoteborgsSkogsAventyr-
3e84440 L├ñgg till prolog, guldstatus och f├Ârb├ñttrad menyhantering
000561a item list i shop
a71150f debuff p├Ñ marcus i monster
ad5f852 Merge branch 'main' of https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-
d6dc135 has item metod i spelare
9a2d125 L├ñgg till guld och XP-bel├Âning i arenan
855cee0 additem inventory i spelareklassen
8f4d810 Merge branch 'main' of github.com:DavidSundkvist/GoteborgsSkogsAventyr-
44eab98 L├ñgg till arenastrider med sju dagar och monster
aaa18db item class med privata f├ñlt och properties
e6f874a gold din status
fb18361 la till random golddropp i case 2 besrgrar monster
2ec7157 gold dropp metod i monster och lite i spelar klassen
6aaa837 gold i splelare
053e13f override plus virtual metod i marcus och chalmers
9b003e2 override och ny vertual metod i sealmonster
6537d9a L├ñgg till huvudmeny, stridsfl├Âde, vila och p├Ñb├Ârja arena
42948ec Skapat delar av metod StartCombat. Gjort f├ñrdigt case 2 i metoden
574b7f7 Skapat subklasser i monsterklassen
2be7ba1 test
d9b8699 sub klass sealmonster
cae4af3 L├Âst mergekonflikt
2398ac3 f├Ârs├Âk 1 av att l├Âsa merge
b6ecae1 Merge branch 'main' of https://github.com/DavidSundkvist/GoteborgsSkogsAventyr-
e018dd8 konstruktor
4c1a34c konstruktor monster
798218e konstruktor monster
291240f Klar med spelareklassen version 1
339d1e2 konstruktor monster halva
56fb8d4 Skapat metod Heal och GainXP i spelare.cs
ad9cf8a Skapat mkonstruktor samt metoden TakeDamage i spelare.cs
3e39e04 propertyn i monster
7b40945 P├Ñb├Ârjat privata f├ñlt och properties i spelarklass.cs & monsterklass.cs
8ab1af3 Skapat separata mappar f├Âr klasserna
8233d65 lagg till emma
e019b31 Initial commit


```

### Kodkvalitet

Ett exempel på ett tydligt variabelnamn är `damageWhenRunning`. Namnet visar vad variabeln innehåller, alltså mängden skada spelaren får när den försöker springa från en strid.
Under testningen gick vi även igenom och tog bort gammal och oanvänd kod samt förbättrade texter, stavning och hanteringen av ogiltiga menyval. Vi testspelade spelet flera gånger för att kontrollera att de olika delarna fungerade tillsammans. Vi justerade även monstrens HP, attack och defense för att få en bättre balans i striderna.

Ett exempel på en kommentar som förklarar varför vi har gjort på ett visst sätt är:

```csharp
// return används istället för break eftersom spelaren ska lämna hela striden, inte bara switchen.
return;
```

Kommentaren förklarar varför vi använder `return` när spelaren springer från ett monster. `break` hade bara lämnat vår `switch`, medan `return` gör att hela metoden `StartCombat()` avslutas och spelaren därmed lämnar striden.

---

## VG — Motivering

### Vad vi lade till

Vi byggde vidare på grundspelet med bland annat guld, föremål, inventory, en shop och en arena. Spelaren kan få guld genom att besegra monster och använda guldet för att köpa olika föremål på Pressbyrån.
Föremålen påverkar även spelarens stats och vissa har speciella funktioner mot olika monster. Snus har en särskild effekt mot FullChalmersStudent, medan spelaren behöver både Katt och Taylor Swift-skiva för att kunna besegra MarcusMedInlämning.

Vi lade även till ett vapensystem med Longsword och Greataxe som spelaren kan köpa i Pressbyrån. Vapnen kan utrustas och ger extra attack i både vanliga strider och arenan.

Vi skapade också Valborgsarenan. För att delta måste spelaren vara minst level 5. Arenan pågår under sju dagar och spelaren möter ett monster varje dag. Till skillnad från en vanlig strid går det inte att springa från arenan. Innan spelaren går in får den därför en varning och måste bekräfta sitt val. Om spelaren överlever alla sju dagar får den 50 XP i bonus.
Vi ändrade även vilan i Botaniska trädgården så att den kostar 2 guld. Tidigare var vilan gratis, vilket gjorde att spelaren kunde återställa sitt HP hur många gånger som helst utan någon nackdel. Genom att lägga till en kostnad behöver spelaren tänka mer på när det är värt att vila.


### Varför vi löste det såhär

Vi valde `List<Monster>` eftersom vi behöver kunna samla flera olika typer av monster och hantera dem tillsammans. Eftersom alla monstertyper ärver från `Monster` kan samma lista innehålla olika monster, samtidigt som varje monster kan ha sitt eget beteende genom polymorfism och `override`.

I Valborgsarenan använder vi också en `List<Monster>` för att bestämma vilka monster spelaren ska möta och i vilken ordning. Vi använder en `for`-loop eftersom vi redan från början vet hur många arenadagar som ska genomföras: exakt sju.

Vi valde att göra arenan som en egen metod, `StartArena()`, i stället för att lägga allt i den vanliga `StartCombat()`. Arenan har flera regler som skiljer sig från en vanlig strid, till exempel att den består av sju strider i följd och att spelaren inte kan springa därifrån.

För shoppen och inventory använder vi `List<items>`. En lista passar eftersom vi behöver kunna lägga till föremål i spelarens inventory och hålla reda på vilka föremål som finns tillgängliga i shoppen.
Vi skapade även basklassen `Weapon` med subklasserna `Longsword` och `Greataxe`. Vi valde arv eftersom vapnen har gemensamma egenskaper som namn, pris och attackbonus, vilket gör att vi slipper upprepa kod. Genom `virtual` och `override` kan varje vapen ha ett eget beteende i metoden `Använd()`, där Longsword lägger till 2 på sin attackbonus och Greataxe multiplicerar sin bonus med 2. Till skillnad från en vanlig lista med siffror kan subklasserna innehålla egna metoder och olika beteenden. Vi använder därför `List<Weapon>` i shoppen och låter menyn anpassas automatiskt efter listornas längd. När spelaren köper ett vapen utrustas det och dess attackbonus räknas med i striderna.
De speciella effekterna för vissa items kopplades till de monster som de påverkar. Det gör att monstren kan ha olika beteenden samtidigt som de fortfarande ärver den gemensamma strukturen från Monster. Genom override kan monstren ha egna versioner av AttackPlayer().
Under testningen märkte vi också att vissa delar behövde balanseras. Ett exempel var vilan, som från början var gratis. Vi ändrade därför vilan till att kosta 2 guld. En möjlig vidareutveckling hade varit att låta kostnaden öka beroende på spelarens level.
