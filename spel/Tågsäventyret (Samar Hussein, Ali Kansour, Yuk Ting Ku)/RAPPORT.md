# Rapport — Slutprojekt

**Kurs:** Grundläggande OOP i C#  

**Projekt:** *Tågsäventyret*  

**Grupp:** General Grievous 

**Datum:**  2026-10-04

**GitHub:**  https://github.com/SamarCumarHussein/Spelaventyr/tree/main

**Commit-hash vid inlämning:**  

---

## Gruppmedlemmar

*Fyll i alla som medverkat i gruppen:*

| Namn         | Lämnar in rapport? |
|------------- |--------------------|
|Samar Hussein | ✅ Ja (den här personen — lämnar in Zip + RAPPORT.md + REFLEKTION.md) |
|Yuk Ting Ku   | ❌ Nej (lämnar in endast REFLEKTION.md) |
|Ali Kansour   | ❌ Nej (lämnar in endast REFLEKTION.md) |

*(Lägg till eller ta bort rader efter behov)*

---

## Instruktion

Förklara kortfattat hur ni löste varje krav. En till tre meningar per punkt räcker — ni ska visa att ni **förstår** det ni byggt, inte skriva en uppsats. Skriv med egna ord och kopiera inte uppgiftsbeskrivningen.

Har ni inte fyllt i VG-delen → projektet bedöms som G.

---

## G — Godkänt

### Klasserna

*Vilka klasser skapade ni och vad ansvarar var och en för? Hur använde ni privata fält, properties och konstruktorer?*

 > Vi skapade flera klasser för att bygga upp spelet. Player ansvarar för spelarens stats, Zombie är basklassen för de de olika monsterna. Walker, Runner och Mutant är olika typer av zombies/monster. Battle ansvarar för striderna mellan spelaren och monstren. Vi använde properties med private set för att skydda värdena. Konstruktorerna används för att ge spelaren och monstren sina startvärden när de skapas.

### Arv och `List<T>`

*Var använde ni `List<T>`, och vad innehåller den? Använde ni arv — i så fall hur ser hierarkin ut och varför?*

> Vi använder List<Zombie> för att samla de olika monsterna/zombien och sedan välja ett monster slumpmässigt. Walker, Runner och Mutant är subklasserna och ärver från Zombie, de har gemensamma egenskaper men olika stats. 

### Spelloopen

*Hur är spelets huvudloop uppbyggd? Hur hanterar ni användarens input och felaktig input?*

> Spelets huvudloop forsätter så länge spelaren har HP-Kvar. Spelaren får välja vad de vi göra och if-satser används för att hantera valen. Om spelaren skriver ett felaktigt val få den föröska igen.

### UML

*Lägg in er klassdiagram (bild eller Mermaid) som ni ritade innan ni började koda. Skiljer sig slutresultatet från planen — hur och varför?*

> Vi gjorde ett flödesschema innan vi började koda för att planera hur spelet skulle fungera. Flödesschemat visar hur spelaren går genom tåget, möter zombies, slåss, får XP och till slut kan vinna spelet.
 Gå genom tåget
       ↓
Möt zombie
       ↓
      Strid
       ↓
Vinn? ── Nej → Game Over
  │
 Ja
  ↓
XP + Gold
  ↓
Level Up?
  ↓
Nästa tågvagn
  ↓
Nå slutstationen → Vinn

### Git

*Hur jobbade ni med Git? Branches, pull requests, vem gjorde vad?*
> Vi använde Git för att spara våra ändringar och hålla koll på alla versioner. Vi gjode commits och pushade sedan ändringan till GitHub. 

Klistra in utskriften från `git log --oneline`:

```
(7cb3355 koden är klar
c690953 Update project name to 'Tågsäventyret'
ad2b641 Update project details in RAPPORT.md
ebe8f3d Add GitHub link to RAPPORT.md
b42b17c Update group and date information in RAPPORT.md
5df466a Add files via upload
84dbe2e Add files via upload
d733f35 sista version
effc278 test
b2ae8ca Merge branch 'main' of https://github.com/SamarCumarHussein/Skogs-ventyret
6ab5c65 battle new
0ac8fa8 Merge branch 'main' of https://github.com/SamarCumarHussein/Skogs-ventyret
703055c zombie merge fix
64ab0f5 test1
c31ed03 test
fa0c4f5 test ändring
4e12749 ändrat för test
b062aa9 test ändra
eea0119 Merge branch 'main' of https://github.com/SamarCumarHussein/Skogs-ventyret
8e07cb0 Ändrat på battle samt klar med program.cs
76c0bb4 Merge branch 'main' of https://github.com/SamarCumarHussein/Skogs-ventyret
005d1dc a
57c87cf lagt till kommentarer
254768b aa
926567a Merge branch 'main' of https://github.com/SamarCumarHussein/Skogs-ventyret
)
```

### Kodkvalitet

*Nämn ett exempel på ett bra namn och en kommentar ni skrev som förklarar **varför**, inte *vad*.*

> Ett exempel på ett bra namn är StartBattle(), eftersom namnet tydligt visar vad metoden gör. Vi använde även kommentarer för att förklara varför vad en vissa delar gör eller ska göra.

---

## VG — Motivering

> Fyll i det här avsnittet om ni siktar på VG. Lämna tomt = G-bedömning.

### Vad vi lade till

*Vilka VG-delar byggde ni (vapen/shop/arena, VG-utbyggnad, låsta dörrar/NPC/spara m.m.)?*

> 

### Varför vi löste det såhär

*Vilka datastrukturer valde ni och varför? Vad var alternativen?*

> 
