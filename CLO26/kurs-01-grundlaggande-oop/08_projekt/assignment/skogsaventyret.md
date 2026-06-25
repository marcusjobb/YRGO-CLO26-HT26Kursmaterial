# Slutprojekt — Skogsäventyret 🌲

**Vecka:** 4–5 (v39–v40)  
**Deadline:** Söndag 4 okt 2026, 23:59  
**Förlängd deadline:** Fredag 9 okt 2026, 23:59  
**Gruppstorlek:** 2–3 studerande  
**Inlämning:** Länk till er gemensamma fork i Google Classroom

---

## Bakgrunden

Det är kväll. Skogen är tyst.

Din gubbe kliver ut ur byn och in i mörkret. Han vet att det finns monster där ute — men han vet inte hur många, och han vet inte hur farliga de är. Varje monster han dödar ger honom erfarenhet. Varje level gör honom starkare. Men när han når nästa level återställs hans hälsa — kroppen anpassar sig, men det kostar.

Hur länge kan han överleva? Hur många monster kan han ta?

Det är er uppgift att bygga spelet och ta reda på det.

---

## Vad gäller för den här inlämningen

*Som utbildare vill jag att ni...*

- [ ] kan använda arv för att bygga en hierarki av monstertyper
- [ ] förstår skillnaden mellan basklass och subklass, och varför man väljer det ena framför det andra
- [ ] kan skriva klasser med properties och privata fält
- [ ] kan bygga ett enkelt spellopp med game loop, input och output
- [ ] kan reflektera kring era designbeslut: varför ärver Troll från Monster?

*Bocka av dem själva innan ni lämnar in.*

---

## Krav för Godkänt (G)

**Karaktären**
- [ ] En `Character`-klass med: namn, HP, maxHP, attack, level, XP, poäng
- [ ] HP nollställs **inte** vid level-up — den ökar (se nedan)
- [ ] Level-up när XP når tröskel — ni bestämmer tröskeln

**Monstren**
- [ ] En `Monster`-basklass med: namn, HP, attack, XP-belöning
- [ ] Minst **2 subklasser** (ex: Goblin, Troll, Drake) som ärver från `Monster`
- [ ] Varje subklass har egna värden — en Goblin är inte ett Troll

**Level-systemet**
- [ ] XP ökar när ett monster dödas
- [ ] Vid level-up: HP *återställs till maxHP* och maxHP ökar
- [ ] Attack ökar vid level-up
- [ ] Poängtavla visas när gubben dör

**Spelloopen**
- [ ] Spelet körs i en loop tills gubben dör
- [ ] Varje runda: ett monster dyker upp slumpmässigt
- [ ] Strid sker automatiskt (turbaserad eller direkt — ni väljer)
- [ ] Tydlig output: vad hände, hur mycket HP har gubben kvar, vilken level

**Reflektion** *(lämnas in som `REFLEKTION.md` i repot)*
- [ ] Vad var svårast att lösa?
- [ ] Varför valde ni att ärva monster på det sättet ni gjorde?
- [ ] Vad skulle ni göra annorlunda om ni fick börja om?

---

## Krav för Väl Godkänt (VG)

*Alla G-krav ska vara uppfyllda. VG-kraven är ett tillägg, inte en ersättning.*

**Vapenshopen**
- [ ] En `Weapon`-basklass med: namn, attack-bonus, pris
- [ ] Minst **2 subklasser** (ex: Sword, Axe, Bow) som ärver från `Weapon`
- [ ] En `Shop`-klass där gubben kan köpa vapen med poäng
- [ ] Aktivt vapen påverkar gubbens attack i striden
- [ ] Shopen visas **mellan strider** — inte mitt i en strid

**VG-reflektionen** *(i samma `REFLEKTION.md`)*
- [ ] Hur valde ni att lösa vapnarvet? Hade ni kunnat göra det utan arv?
- [ ] Vad är skillnaden mellan en `Sword` och en `Weapon` i er kod?

---

## Hur ni kommer igång

Ni bestämmer själva upplägg — men ett förslag:

1. Börja med att rita ett UML-diagram på ett papper. Vilka klasser? Vilka ärver från vilka?
2. Skapa `Character` och `Monster` — ingen spelloop ännu
3. Skriv en enkel strid: gubben attackerar, monstret attackerar, kolla om någon dör
4. Lägg till loop och level-system
5. VG: Lägg till vapen och shop

Koda vilt. 🌲

---

## Vad vi **inte** bedömer

- Hur snygg din output ser ut i terminalen
- Om du har exakt rätt variabelnamn
- Om monstren är rimligt balanserade (det är ett spel, balans är svårt)

Vi bedömer om ni **förstår** det ni har byggt och **kan motivera** era val.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback skickas per mail och via kommentar i Google Classroom.  
Commits efter deadline beaktas inte.

Spara commit-hashen ni lämnar in på — ni kan inte ändra i efterhand.
