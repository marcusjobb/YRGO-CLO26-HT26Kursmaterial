# Slutprojekt — Dungeon Crawler 🗝️

**Vecka:** 7–8  
**Deadline:** Söndag 27 sep 2026, 23:59  
**Presentation:** Onsdag 30 sep 2026 — förmiddag tenta, eftermiddag redovisning  
**Förlängd deadline:** Fredag 2 okt 2026, 23:59  
**Gruppstorlek:** max 4 studerande  
**Inlämning:** Google Classroom — zippad fil + GitHub-länk

---

## Bakgrunden

Du skriver ett kommando. Något händer.

Textäventyr är en av de äldsta formerna av datorspel — och fortfarande ett av de bästa sätten att träna objektorienterad design. En dörr är ett objekt. En nyckel är ett objekt. Rummet du befinner dig i är ett objekt som känner till sina grannar. Spelaren är ett objekt som bär på en lista med föremål.

Allt hänger ihop. Klasserna samarbetar. Det är precis det ni har tränat på.

---

## Temat är ert

Dungeon Crawler är ett standardtema — men ni behöver inte använda det.

Mekaniken är densamma oavsett: rum, föremål, navigation, ett mål att nå.
Det är **världen och berättelsen** ni äger.

Ni kan till exempel bygga:

- En rymdstation där luften håller på att ta slut
- En u-båt med låsta skott och läckande sektioner
- En spökad herrgård på 1800-talet
- En skola på fredagseftermiddag — nycklarna till grinden är någonstans

Ni får byta ut allt: klassnamn, rumsnamn, föremål, kommandon.
Så länge strukturen är där — `Spelare`, `Rum`, `Föremål`, navigationslogik, vinstvillkor — spelar temat ingen roll för betyget.

> Välj något ni tycker är roligt att bygga. Det märks i koden.

En liten tradition: i varje kull finns minst en grupp som lägger in **Marcus** som ett föremål man måste bära ut. Det är helt okej. Det uppskattas.

---

## Spelets flöde

### Uppstart
```
Välkommen till [ert spel]!
Skriv 'hjälp' för att se kommandon.

Du befinner dig i [startrum]. [beskrivning]
Utgångar: norr, öster
Föremål här: fackla
```

### Navigation
Spelaren skriver ett kommando i taget:

```
> gå norr
Du kliver in i [nästa rum]. [beskrivning]
Utgångar: syd, väster
Föremål här: nyckel

> ta nyckel
Du tar nyckeln.

> inventory
Du bär på: fackla, nyckel

> titta
[rummets beskrivning visas igen]

> hjälp
Kommandon: gå [riktning], ta [föremål], titta, inventory, hjälp, avsluta

> avsluta
Spelet avslutas. Hej då!
```

### Vinstvillkor
Ni bestämmer hur spelaren vinner — nå ett specifikt rum, hitta ett specifikt föremål, eller kombinera båda.

```
Du hittar den förlorade kristallen. Uppdraget är slutfört!
```

### Förlorvillkor (valfritt för G)
Spelaren kan förlora om de fastnar utan nyckel, löper ut ur tid, eller liknande.

---

## Krav för Godkänt (G)

*Som utbildare vill jag att ni kan...*
- [ ] bygga ett program med en tydlig spelloop driven av användarens input
- [ ] använda Dictionary för att koppla ihop rum via riktningar
- [ ] använda List för att hantera föremål i rum och inventory
- [ ] skriva klasser med konstruktorer, privata fält och properties
- [ ] ta emot och tolka textkommandon från användaren
- [ ] reflektera skriftligt kring era designval

---

### Teknisk grund

- [ ] Minst 3 klasser med privata fält (`private`) — data skyddas inuti klassen
- [ ] Properties med `{ get; private set; }` — ingen utomstående ändrar data direkt
- [ ] Konstruktorer med parametrar — objektet startar i korrekt tillstånd
- [ ] Minst en `List<T>` i aktiv användning — ex: `List<Föremål>` i rum och inventory
- [ ] Minst en basklass med minst **2 subklasser** som ärver från den

---

### Rum-klassen

- [ ] En `Rum`-klass med: namn, beskrivning
- [ ] `Dictionary<string, Rum> Utgångar` — kopplar riktning (ex: "norr") till nästa rum
- [ ] `List<Föremål> Föremål` — föremål som finns i rummet
- [ ] `string Beskriv()` — returnerar rummets namn, beskrivning, utgångar och föremål

### Föremål-klassen och arv

- [ ] En `Föremål`-basklass med: namn, beskrivning
- [ ] Konstruktor som tar namn och beskrivning
- [ ] Minst **2 subklasser** som ärver från `Föremål` (ex: `Nyckel`, `Vapen`, `Magisk`) — varje subklass har egna egenskaper eller beteenden (t.ex. en `Nyckel` har ett `LåserId`, ett `Vapen` har `AttackBonus`)

### Spelare-klassen

- [ ] En `Spelare`-klass med: `Rum AktuellRum`, `List<Föremål> Inventory`
- [ ] `FlyttaTill(Rum rum)` — uppdaterar aktuellt rum
- [ ] `TaFöremål(Föremål föremål)` — lägger föremålet i inventory, tar bort det från rummet
- [ ] `HarFöremål(string namn)` — returnerar `bool`
- [ ] `VisaInventory()` — skriver ut vad spelaren bär

### Spel-klassen

- [ ] En `Spel`-klass som ansvarar för spelloop och kommandotolkning
- [ ] `SkapaVärld()` — skapar alla rum och kopplar ihop dem
- [ ] `TolkaKommando(string input)` — hanterar vad spelaren skriver
- [ ] Felhantering: tydligt meddelande om riktningen inte finns eller föremålet saknas

### Världen

- [ ] Minst **5 rum** sammankopplade med `Utgångar`
- [ ] Rita kartan på papper INNAN ni kodar — koppla åt båda håll (norr/syd, öster/väster)
- [ ] Minst **3 föremål** utspridda i världen
- [ ] Ett tydligt vinstvillkor

### Kommandon som måste fungera

- [ ] `gå [riktning]` — flytta spelaren
- [ ] `ta [föremål]` — ta föremål från aktuellt rum
- [ ] `titta` — visa rummets beskrivning igen
- [ ] `inventory` — visa vad spelaren bär
- [ ] `hjälp` — lista tillgängliga kommandon
- [ ] `avsluta` — avsluta spelet

### Git

- [ ] Minst **8 commits** med beskrivande meddelanden — en per logisk del (Rum-klass klar, Spelare klar, spelloop funkar osv.)
- [ ] Alla gruppmedlemmar syns i commit-historiken

### Kodkvalitet

- [ ] Variabel-, klass- och metodnamn är självförklarande
- [ ] Kommentarer där logiken inte är uppenbar — förklara *varför*, inte *vad*

### gitignore

- [ ] `.gitignore` finns och filtrerar bort `bin/`, `obj/`, `.vs/`
- [ ] Saknas gitignore, eller filtrerar den inte bort dessa mappar → **IG**
- [ ] Bilder och mediafiler är undantagna från regeln — de får committas

> Tips: Använd [goblin.tools/ToDo](https://goblin.tools/Todo) för att bryta ner uppgiften i hanterbara steg.

### Reflektion (`REFLEKTION.md` i repot)

- [ ] Vad var svårast att lösa?
- [ ] Varför valde ni `Dictionary` för att koppla ihop rum — vad hade hänt med en `List`?
- [ ] Vad hade ni gjort annorlunda om ni fick börja om?
- [ ] Beskriv tre markdown-element ni använt i det här dokumentet och vad de gör

---

## Krav för Väl Godkänt (VG)

*Alla G-krav ska vara uppfyllda.*

VG kräver att ni **använder relevanta datastrukturer och kan motivera varför** — inte bara "för att det funkade".

Välj **minst ett** av följande:

### Låsta dörrar och nycklar

- [ ] Rum kan vara låsta — spelaren måste ha rätt föremål för att ta sig igenom
- [ ] Lägg till `ärLåst` och `KräverFöremål` (string) på `Rum`-klassen
- [ ] Felmeddelande om spelaren försöker gå igenom en låst dörr utan nyckel
- [ ] Motivera i reflektionen: varför räckte `string` som nyckelidentifierare? Hade `Föremål`-referens varit bättre?

### NPC — karaktärer att prata med

- [ ] En `NPC`-klass med: namn, `List<string> Dialog`
- [ ] Rum kan innehålla NPCer
- [ ] Kommando: `prata med [namn]` — visar nästa replik i listan, börjar om från början
- [ ] Minst 2 NPCer med 3 repliker var
- [ ] Motivera i reflektionen: varför `List<string>` för dialog — vad hade en `Dictionary<int, string>` gett er?

### Spara och ladda spelläge

- [ ] Kommando: `spara` — skriver aktuellt rum och inventory till fil
- [ ] Kommando: `ladda` — läser tillbaka och återställer spelläget
- [ ] Välj format (text, CSV, JSON-liknande) och motivera i reflektionen

### VG-reflektion (i samma `REFLEKTION.md`)

- [ ] Vilket VG-alternativ valde ni och varför?
- [ ] Vilken datastruktur är kärnan i er lösning och vad motiverade valet?
- [ ] Hade ni kunnat lösa det utan er valda datastruktur?

---

## Förslag på klassstruktur

Rita detta på whiteboard innan ni skriver en rad kod:

```
Spel
  ├── Spelare spelare
  ├── SkapaVärld()
  └── TolkaKommando(string input)

Spelare
  ├── Rum AktuellRum
  ├── List<Föremål> Inventory
  ├── FlyttaTill(Rum)
  ├── TaFöremål(Föremål)
  ├── HarFöremål(string) bool
  └── VisaInventory()

Rum
  ├── string Namn
  ├── string Beskrivning
  ├── Dictionary<string, Rum> Utgångar
  ├── List<Föremål> Föremål
  └── string Beskriv()

Föremål
  ├── string Namn
  └── string Beskrivning

NPC  (VG)
  ├── string Namn
  ├── List<string> Dialog
  └── string NästaReplik()
```

---

## Kom igång

1. **Rita rum-kartan på papper** — minst 5 rum, markera riktningar och var föremål finns
2. **Bygg `Rum` och `Föremål`** — inga andra klasser än
3. **Koppla ihop rummen** i `SkapaVärld()` — testa att `Utgångar` fungerar
4. **Bygg `Spelare`** — flytta spelaren manuellt, kolla att rummet byter sig
5. **Lägg till spelloop** — `Console.ReadLine()`, `TolkaKommando()`, en runda i taget

---

## Inlämning

**En i gruppen lämnar in:**
- Zippad fil av hela projektet (ladda ner från GitHub)
- GitHub-länk (bjud in `marcusjobb` om repot är privat)
- `RAPPORT.md` i repots rot
- Sin egen `REFLEKTION.md`

**Resten av gruppen lämnar in:**
- Sin egen `REFLEKTION.md` (individuell, obligatorisk)

`RAPPORT.md` ska innehålla:
- Lista med alla gruppmedlemmars namn
- Svar på G-frågorna med egna ord
- Länk till GitHub-repot
- VG-sektionen ifylld (lämna tom = G)

---

## Vad vi inte bedömer

- Hur snygg din terminal-output ser ut
- Om världen är perfekt balanserad eller pussel-designen är optimal
- Om klassnamnen är exakt som i uppgiften — byt gärna om temat kräver det

Vi bedömer om ni **förstår** det ni byggt och **kan motivera** era val.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback skickas per mail och via kommentar i Google Classroom.  
Commits efter deadline beaktas inte — spara er commit-hash.
