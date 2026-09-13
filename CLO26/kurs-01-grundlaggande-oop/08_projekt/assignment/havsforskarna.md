# Slutprojekt — Havsforskarna 🌊

**Vecka:** 6–8  
**Deadline:** Söndag 27 sep 2026, 23:59  
**Presentation:** Onsdag 30 sep 2026 — förmiddag tenta, eftermiddag redovisning  
**Förlängd deadline:** Fredag 2 okt 2026, 23:59  
**Gruppstorlek:** max 4 studerande  
**Inlämning:** Google Classroom — zippad fil + länk till GitHub-repo

---

## Bakgrunden

År 2031. Oceanografiska institutet i Göteborg har just fått finansiering för en ny expedition.

Uppdraget: kartlägga dolda bergformationer på Nordatlantens botten — formationer som kan dölja sällsynta mineral, unika ekosystem, och kanske något mer. En rivaliserade forskargrupp jobbar på samma koord­inater. Den som kartlägger allt först får publiceringsrätten.

Er ubåt är i vattnet. Sondern är laddad. Kartläggningstävlingen börjar nu.

---

## Temat är ert

Havsforskarna är ett standardtema — men ni behöver inte använda det.

Mekaniken är densamma oavsett: ett 10×10-rutnät, dolda objekt att hitta, turbaserad sökning, en vinnare.

Det är **temat och berättelsen** ni äger.

- Rymdutforskning: kartlägg asteroidfält mot en rivaliserande rymdagentur
- Arkeologi: hitta begravda tempelkomplex i ökensanden
- Detektivspel: lokalisera gömda ledtrådar i ett kvarter

Ni får byta ut allt: namn på klasser, metoder, formationer, koordinater.
Så länge strukturen är där — `OceanGrid`, `Formation`, turbaserat spel, vinstvillkor — spelar temat ingen roll för betyget.

> Välj något ni tycker är roligt att bygga. Det märks i koden.

---

## Spelets flöde

### Spelstart
```
1) Spelare placerar sina formationer på rutnätet
2) AI-motståndare placerar sina formationer slumpmässigt
3) Spelet börjar
```

### Turer
```
Vems tur? Spelarens.

Din kartläggningsyta:          Motståndarens yta (vad du sett):
  1 2 3 4 5 6 7 8 9 10          1 2 3 4 5 6 7 8 9 10
A ~ ~ ~ ~ ~ ~ ~ ~ ~ ~         A ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
B ~ F F ~ ~ ~ ~ ~ ~ ~         B ~ ~ X ~ ~ ~ ~ ~ ~ ~
...                            ...

Ange koordinat (ex: C5): 
```

- Träff (`X`) → du fortsätter
- Miss (`O`) → motståndarens tur
- En formation är helt kartlagd när alla dess rutor är träffade

### Vinstvillkor

Den som kartlägger alla motståndarens formationer först vinner.

---

## Kursmål

*Som utbildare vill jag att ni kan...*

- [ ] representera ett spelbräde med en 2D-datastruktur och visa det i terminalen
- [ ] använda klasser med privata fält, properties och konstruktorer
- [ ] använda `List<T>` för att hålla reda på formationer och positioner
- [ ] skriva en turbaserad spelloop med tydlig separation mellan spelarens och AI:ns logik
- [ ] validera användarinput och ge meningsfulla felmeddelanden
- [ ] reflektera skriftligt kring era designval

---

## Krav för Godkänt (G)

### Teknisk grund

- [ ] Minst 3 klasser med privata fält (`private`) — data skyddas inuti klassen
- [ ] Properties med `{ get; private set; }` — ingen utomstående ändrar data direkt
- [ ] Konstruktorer med parametrar — objektet startar i korrekt tillstånd
- [ ] Minst en `List<T>` i aktiv användning — ex: `List<Formation>` och `List<Position>`
- [ ] Minst en basklass med minst **2 subklasser** som ärver från den

---

### Rutnätet

- [ ] En `OceanGrid`-klass med ett 10×10-rutnät (tips: `char[,]` fungerar bra, men välj den struktur som känns logisk för er)
- [ ] Symboler: `~` okänt vatten, `F` din formation, `X` träff, `O` miss
- [ ] Metod för att visa rutnätet med koordinater (A–J, 1–10)
- [ ] Separata rutnät: ett som visar dina egna formationer, ett som visar vad du sett hos motståndaren

### Formationerna och arv

- [ ] En `Formation`-basklass med: namn, storlek (antal rutor), `List<Position>` positioner
- [ ] Metod `ÄrKartlagd()` som returnerar `true` när alla positioner är träffade
- [ ] En `Position`-klass (eller struct) med rad och kolumn
- [ ] Minst **2 subklasser** som ärver från `Formation` (ex: `Korallrev`, `Bergskedja`, `Vrak`) — varje subklass har egna stats eller beteenden (t.ex. olika defaultstorlek, eller en override av `ÄrKartlagd()`)

### Placering

- [ ] Spelaren placerar formationer manuellt: ange startkoordinat och riktning (H/V)
- [ ] Validering: formationen får inte gå utanför rutnätet och inte överlappa en annan
- [ ] AI-motståndaren placerar sina formationer slumpmässigt (med samma validering)
- [ ] Spelet har minst 3 formationer av olika storlek (ex: 5, 3, 3, 2, 2 rutor)

### Spellogiken

- [ ] Turbaserat: spelare skickar en sond, sedan AI:n
- [ ] Spelaren anger koordinat (ex: `C5`) — programmet svarar träff eller miss
- [ ] AI:n väljer en koordinat som inte redan testats
- [ ] Vinstvillkor: spelet slutar när en sidas alla formationer är kartlagda

### Presentation

- [ ] Båda rutnäten visas varje tur — eget och motståndarens utforskade yta
- [ ] Tydlig text om vad som hände: träff/miss, vilken formation som kartlagts, vem som vann

### Git

- [ ] Minst **8 commits** med beskrivande meddelanden — en per logisk del (Formation klar, rutnät klart, spelloop klar osv.)
- [ ] Alla gruppmedlemmar syns i commit-historiken

### Kodkvalitet

- [ ] Variabel-, klass- och metodnamn är självförklarande
- [ ] Kommentarer där logiken inte är uppenbar — förklara *varför*, inte *vad*

### gitignore

- [ ] `.gitignore` som filtrerar bort `bin/`, `obj/` och `.vs/`
- [ ] Saknas gitignore (eller filtrerar inte bort binärfiler) → **IG**

### Reflektion (`REFLEKTION.md` i repot)

- [ ] Vad var svårast att implementera?
- [ ] Hur valde ni att representera rutnätet — och varför?
- [ ] Hur fungerade gruppdynamiken? Vad hade ni gjort annorlunda?
- [ ] Beskriv tre markdown-element ni använt i det här dokumentet och vad de gör

---

## Krav för Väl Godkänt (VG)

*Alla G-krav ska vara uppfyllda.*

VG kräver att ni **väljer en av följande utbyggnader och motiverar valet i reflektionen**.

### Alternativ 1 — Smart AI

En AI som inte bara skjuter slumpmässigt, utan:

- [ ] Kommer ihåg var den träffat och söker systematiskt i närheten
- [ ] Byter till en annan strategi om grannarna är missar

### Alternativ 2 — Specialutrustning

- [ ] Implementera **sonarsvep** (avslöjar en hel rad eller kolumn) eller **multi-sond** (undersöker ett 2×2-område)
- [ ] Varje spelare har begränsat antal användningar (ex: 2 per spel)
- [ ] Motivera i reflektionen: vilken ni valde och varför

### Alternativ 3 — Spara och ladda

- [ ] Spelläget sparas till fil (JSON, CSV eller textformat ni definierar)
- [ ] Programmet kan ladda ett pågående spel och fortsätta därifrån
- [ ] Motivera i reflektionen: vilket format ni valde och varför

### VG-reflektion (i samma `REFLEKTION.md`)

- [ ] Vilken VG-utbyggnad valde ni och varför just den?
- [ ] Vad är den tekniskt svåraste delen av er lösning?
- [ ] Hade ni kunnat lösa det utan `List<T>`?

---

## Förslag på klassstruktur

Rita detta på whiteboard innan ni skriver en rad kod:

```
Position
  ├── Rad (int)
  └── Kolumn (int)

Formation
  ├── Namn (string)
  ├── Storlek (int)
  ├── List<Position> Positioner
  └── ÄrKartlagd() bool

OceanGrid
  ├── char[,] rutnät (10×10)
  ├── List<Formation> formationer
  ├── PlaceraFormation(Formation, startPos, riktning)
  ├── TaEmotSond(Position) → träff/miss
  └── VisaRutnät()

Spel
  ├── OceanGrid spelarensRutnät
  ├── OceanGrid motståndarnasRutnät
  ├── SpelarensRunda()
  ├── AIsRunda()
  └── SpelÄrSlut() bool
```

---

## Kom igång

1. Rita UML på papper — vilka klasser, vad håller de, vilka metoder behövs
2. Bygg `Position` och `Formation` — testa att `ÄrKartlagd()` fungerar
3. Bygg `OceanGrid` — visa ett tomt rutnät i terminalen
4. Lägg till placering och validering — testa manuellt med hårdkodade formationer
5. Bygg spelloopen: spelare skickar sond → AI svarar → byt tur

> Tips: [goblin.tools/ToDo](https://goblin.tools/ToDo) — klistra in uppgiftsbeskrivningen och låt den dela upp arbetet i steg åt er.

---

## Inlämning

**En person i gruppen lämnar in:**
- Zippad fil från GitHub
- Länk till GitHub-repo (bjud in `marcusjobb` om repot är privat)
- `RAPPORT.md` (se mall i `_templates/`)
- Egen `REFLEKTION.md`

**Övriga i gruppen lämnar in:**
- Endast sin egen `REFLEKTION.md`

**RAPPORT.md ska innehålla:**
- Lista på alla gruppmedlemmar (namn + vem som lämnade in vad)
- Förklaring av era designval (G-sektionen)
- VG-sektionen om ni siktar på VG (tom = G-bedömning)
- Git-logg: klistra in `git log --oneline`

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback skickas per mail och via kommentar i Google Classroom.  
Commits efter deadline beaktas inte — spara er commit-hash.
