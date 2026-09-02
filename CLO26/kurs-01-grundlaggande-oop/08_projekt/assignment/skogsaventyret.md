# Slutprojekt — Skogsäventyret 🌲

**Vecka:** 7–8  
**Deadline:** Söndag 27 sep 2026, 23:59  
**Presentation:** Onsdag 30 sep 2026 — förmiddag tenta, eftermiddag redovisning  
**Förlängd deadline:** Fredag 2 okt 2026, 23:59  
**Gruppstorlek:** 2–3 studerande  
**Inlämning:** Länk till er gemensamma fork i Google Classroom

---

## Bakgrunden

Det är kväll. Skogen är tyst.

Du kliver ut ur byn och in i mörkret. Det finns monster där ute — hur många och hur farliga vet du inte. Varje monster du dödar ger erfarenhet. Varje dag du överlever är en seger. Men ingenting varar för evigt.

Hur länge kan du hålla dig vid liv? Hur mycket XP kan du samla?

---

## Temat är ert

Skogsäventyret är ett standardtema — men ni behöver inte använda det.

Mekaniken är densamma oavsett: en spelare, motståndare, val, skada, XP, game over.
Det är **temat och berättelsen** ni äger.

Tidigare grupper har bland annat byggt:

- En Van Helsing som jagar monster i 1800-talets Europa
- En Sagan om Ringen-värld med orcher, alver och ringen som VG-item
- Ett spel där man klappar katter — de river ibland, men kurrar när de är besegrade 🐱

Det sista är faktiskt ett av de bästa spelen vi sett. Samma kod, helt annan känsla.

Ni får byta ut allt: namn på klasser, metoder, fiender, platser, items.
Så länge strukturen är där — `Player`, `Monster` med arv, spelloop, stridsval — spelar temat ingen roll för betyget.

> Välj något ni tycker är roligt att bygga. Det märks i koden.

En liten tradition: i varje kull finns minst en grupp som döper slutbossen till **Marcus**. Det är helt okej. Det uppskattas.

---

## Spelets flöde

### Uppstart
```
1) Välj spelarens namn
2) Börja spela
```

### Byn (varje dag)
```
Vad vill du göra idag?
1) Ut i skogen och äventyra   (1 dag)
2) Vila och hela dig           (1 dag)
3) Arenan                      (7 dagar, kräver hög level) ← VG
```

### Skogen
Du möter ett monster. Det anfaller.

```
1) Försvara dig       → skadan halveras
2) Anfall tillbaka    → monster.Attack - din försvarsstyrka
3) Spring             → du tar slumpmässig skada
```

- Monster dör → du får **10 XP** och kan välja: äventyra vidare eller gå tillbaka till byn
- Du dör (HP = 0) → **Game over**, poängtavla visas

### Mål
Överleva flest dagar med mest XP.

---

## Krav för Godkänt (G)

*Som utbildare vill jag att ni kan...*
- [ ] bygga ett program med en tydlig game loop
- [ ] använda if-satser och loopar för att hantera spellogik
- [ ] skriva klasser med konstruktorer, privata fält och properties
- [ ] använda arv för att bygga en hierarki av monster
- [ ] ta emot och hantera input från användaren
- [ ] reflektera skriftligt kring era designval

---

### Spelaren

- [ ] En `Player`-klass med: namn, HP, maxHP, attack, försvar, level, XP, dagar överlevda
- [ ] `TakeDamage(int skada)` — minskar HP, returnerar `true` om spelaren dör
- [ ] `Heal()` — återställer HP till maxHP (kostar en dag)
- [ ] `GainXP(int mängd)` — lägger till XP, utlöser `LevelUp()` vid tröskel
- [ ] `LevelUp()` — höjer level, ökar maxHP och attack, återställer HP

### Monstren

- [ ] En `Monster`-basklass med: namn, HP, attack, XP-belöning
- [ ] `TakeDamage(int skada)` — minskar HP, returnerar `true` om monstret dör
- [ ] `Attack(Player spelare)` — angriper spelaren
- [ ] Minst **3 subklasser** som ärver från `Monster` (ex: Goblin, Troll, Drake)
- [ ] Varje subklass har egna stats — en Goblin är inte ett Troll

### Striden

- [ ] Spelaren väljer 1, 2 eller 3 varje runda
- [ ] **Försvara:** skadan halveras (`monster.Attack / 2`)
- [ ] **Anfall:** monstret tar `player.Attack - monster.Forsvar` i skada (minimum 1)
- [ ] **Spring:** spelaren tar slumpmässig skada (`Random`, 1–monster.Attack)
- [ ] Tydlig utskrift varje runda: vad hände, HP kvar för båda

### Spelloopen

- [ ] Spelet körs tills spelaren dör
- [ ] Varje dag i skogen möter spelaren ett slumpmässigt monster
- [ ] Vila återställer HP och kostar en dag
- [ ] Poängtavla vid game over: dagar överlevda, level uppnådd, total XP

### Git

- [ ] Minst **8 commits** med beskrivande meddelanden — en per logisk del (Player klar, stridssystem klart osv.)
- [ ] Alla gruppmedlemmar syns i commit-historiken

### Kodkvalitet

- [ ] Variabel-, klass- och metodnamn är självförklarande
- [ ] Kommentarer där logiken inte är uppenbar — förklara *varför*, inte *vad*

### Reflektion (`REFLEKTION.md` i repot)

- [ ] Vad var svårast att lösa?
- [ ] Varför ärver era monster från en basklass — vad tjänar ni på det?
- [ ] Vad hade ni gjort annorlunda om ni fick börja om?
- [ ] Beskriv tre markdown-element ni använt i det här dokumentet och vad de gör

---

## Krav för Väl Godkänt (VG)

*Alla G-krav ska vara uppfyllda.*

VG kräver att ni **använder relevanta datastrukturer och kan motivera varför** — inte bara "för att det funkade".

### Vapen

- [ ] En `Weapon`-basklass med: namn, attackBonus, pris (i guldmynt)
- [ ] Minst **2 subklasser** (ex: Sword, Axe, Bow) med egna värden
- [ ] Monster tappar guldmynt när de dör (slumpmässigt, ex: 5–15 mynt)
- [ ] En `Shop` med ett sortiment av vapen — köp **mellan strider**, aldrig mitt i en strid
- [ ] Aktivt vapen adderas till spelarens attack
- [ ] Ni väljer datastruktur för sortimentet — `List<Weapon>` eller `Dictionary<string, Weapon>` — och motiverar valet i reflektionen

### Arenan

- [ ] Arenan tar **7 dagar** och kan inte avbrytas
- [ ] Spelaren möter **7 monster**, sorterade från svagast till starkast
- [ ] Om spelaren dör i arenan → game over direkt
- [ ] Om spelaren överlever alla 7 → stor XP-bonus och ärorik seger

### VG-reflektion (i samma `REFLEKTION.md`)

- [ ] Vilken datastruktur valde ni för vapensortimentet och varför?
- [ ] Hur sorterade ni monstren i arenan?
- [ ] Hade ni kunnat lösa arenan utan arv?

---

## Förslag på klassstruktur

Rita detta på whiteboard innan ni skriver en rad kod:

```
Player
  ├── namn, hp, maxHp, attack, forsvar, level, xp, dagar, guld
  ├── TakeDamage()
  ├── Heal()
  ├── GainXP()
  └── LevelUp()

Monster  (basklass)
  ├── namn, hp, attack, forsvar, xpBeloning
  ├── TakeDamage()
  └── Attack()
      ├── Goblin   : Monster
      ├── Troll    : Monster
      └── Drake    : Monster

Weapon   (basklass, VG)
  ├── namn, attackBonus, pris
      ├── Sword  : Weapon
      └── Axe    : Weapon

Shop     (VG)
  └── List<Weapon> sortiment
```

---

## Kom igång

1. Rita UML på papper — vilka klasser, vad ärver från vad
2. Bygg `Player` och `Monster` — ingen spelloop ännu
3. Skriv en enkel strid: välj handling, räkna skada, kolla om någon dör
4. Lägg till loopen: byn → skog → tillbaka
5. Lägg till level-systemet
6. VG: vapen, guld, shop, arena

---

## Vad vi inte bedömer

- Hur snygg din terminal-output ser ut
- Om variabelnamnen är exakt rätt
- Om monstren är perfekt balanserade — balans är svårt, det vet vi

Vi bedömer om ni **förstår** det ni byggt och **kan motivera** era val.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback skickas per mail och via kommentar i Google Classroom.  
Commits efter deadline beaktas inte — spara er commit-hash.
