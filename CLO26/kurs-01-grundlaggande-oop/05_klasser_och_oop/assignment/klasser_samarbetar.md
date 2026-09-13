# Inlämningsuppgift 1 — Drömmatchen ⚽

**Vecka:** 3 (v38)  
**Deadline:** Söndag 21 sep 2026, 23:59  
**Förlängd deadline:** Fredag 25 sep 2026, 23:59  

### Inlämning via Google Classroom

**En person per grupp** lämnar in:
- Zippad fil av projektet (exporterad från GitHub — Marcus visar hur)
- GitHub-länk till repot (bjud in `marcusjobb` om repot är privat)
- `RAPPORT.md` (med allas namn ifyllda)
- Sin egen `REFLEKTION.md`

**Alla övriga** lämnar in:
- Sin egen `REFLEKTION.md`

Reflektionen är **obligatorisk** och lämnas in individuellt — även om ni jobbat tillsammans.

---

### ⚠️ Krav för att bli bedömd — `.gitignore`

Repot **måste** innehålla en `.gitignore` som filtrerar bort byggfiler (`bin/`, `obj/`, `.vs/`).

**Saknas `.gitignore`, eller filtrerar den inte bort byggfiler → IG (ej godkänd).**

> Bilder och andra mediafiler är tillåtna att committa — de räknas inte som byggfiler.

---

## Bakgrunden

Det är finalmatch. Arenans ljus är på. Kommentatorn är redo.

Men det här är ingen vanlig match.

Inga kontrakt. Inga tidszoner. Inga transferfönster. Inga tidseror. Du är manager, och du har fått sätta ihop din absoluta drömuppställning — oavsett klubb, land eller decennium. Pelé och Haaland i samma anfallspar. Maradona på tio. Din lokala idol på vänsterkanten. Den spelare du alltid tyckt var underskattad, äntligen på rätt plats.

Arenan är fylld. Klockan tickar mot avspark.

Det enda som saknas är koden som håller koll på vem som faktiskt levererar — vem som tar hem matchen, och vem som åker på bänken nästa gång.

---

## Kursmål

Den här inlämningen examinerar följande kursmål ur kursplanen:

- Redogöra för syntax, flöde och logik inom OOP i C#
- Redogöra för arbetsmetodik och versionshantering med Git
- Redogöra för namngivning och kodstruktur enligt clean code
- Beskriva funktionen i kod
- Redogöra för markdown-taggar och dess användning
- Använda Git korrekt i utvecklingsprojekt
- Skriva korta och relevanta kodkommentarer
- Utveckla fristående OOP-applikationer med dokumentation

---

## Självcheck

*Bocka av innan du lämnar in:*

- [ ] Kan skriva en klass med privata fält och properties
- [ ] Förstår vad en konstruktor gör och varför man använder den
- [ ] Kan skilja på `private` och `public` — och förklara varför
- [ ] En klass tar emot ett objekt av en annan klass som parameter
- [ ] Använder Git med meningsfulla commits under arbetets gång
- [ ] Skriver kod med tydliga namn och kommentarer där de behövs

---

## Krav för Godkänt (G)

### Klass 1 — `Spelare`

**Privata fält:**
- `_namn` (string)
- `_nummer` (int) — tröjnummer
- `_position` (string) — t.ex. `"Forward"`, `"Mittfältare"`, `"Målvakt"`

**Properties** (publik get, privat set):
- `Namn`
- `Nummer`
- `Position`

**Konstruktor** som tar in och sätter alla värden

---

### Klass 2 — `Match`

**Privata fält:**
- `_hemmalag` (string)
- `_bortalag` (string)
- `_datum` (string)

**Properties** (publik get, privat set):
- `Hemmalag`
- `Bortalag`
- `Datum`

**Konstruktor** som tar in och sätter alla värden

**Metoder:**
- `Presentera()` — skriver ut matchens lag och datum
- `AnnounceraMålskytt(Spelare spelare)` — skriver ut spelarens namn, nummer och position

---

### I `Main()`

- Skapa minst **2 spelare** och **1 match**
- Anropa `Presentera()` på matchen
- Anropa `AnnounceraMålskytt()` med båda spelarna

---

### Git

- [ ] Repot innehåller en `.gitignore` som filtrerar bort `bin/`, `obj/`, `.vs/`
- [ ] Minst **5 commits** med beskrivande meddelanden
- [ ] Commit-historiken ska spegla att du arbetat stegvis

---

### Kodkvalitet

- [ ] Variabel- och metodnamn är självförklarande
- [ ] Kommentarer där koden inte är uppenbar

---

### Rapport och reflektion

**`RAPPORT.md`** — fyll i mallen som finns i repot. Förklara kortfattat hur du löst varje G-krav. Har du siktat på VG — fyll i VG-delen också. Tomt VG-avsnitt = G.

**`REFLEKTION.md`** — fyll i reflektionsmallen individuellt. Obligatorisk. Lämnas in även om uppgiften gjorts i grupp.

---

## Krav för Väl Godkänt (VG)

*Alla G-krav måste vara uppfyllda.*

### Utökat `Spelare`

Lägg till:
- Privat fält `_mål` (int) — antal mål i matchen
- Property `Mål`
- Uppdatera konstruktorn

### Utökat `Match`

Lägg till metod:
- `ÄrMatchhjälte(Spelare spelare)` — returnerar `true` om spelaren gjort minst ett mål, annars `false`

### I `Main()` (VG-tillägg)

- Skriv ut resultatet av `ÄrMatchhjälte()` för varje spelare

### VG-rapport

Fyll i VG-avsnittet i `RAPPORT.md` — inklusive frågan om varför `ÄrMatchhjälte` returnerar `bool` istället för att skriva ut direkt.

---

## Exempeloutput (G)

```
Drömlagen FC vs Världselvan — 2026-09-14

MÅÅÅL! #10 Zlatan Ibrahimović (Forward)
MÅÅÅL! #7 Thierry Henry (Forward)
```

## Exempeloutput (VG-tillägg)

```
ÄrMatchhjälte — Zlatan Ibrahimović: True
ÄrMatchhjälte — Thierry Henry: False
```

---

## Tips — kom igång

Vet du inte var du ska börja? Prova [goblin.tools/ToDo](https://goblin.tools/ToDo) — klistra in uppgiftsbeskrivningen och låt den bryta ner den i lagom stora steg. Sedan kodar du ett steg i taget.

---

## Bedömning

Rättning sker veckan efter deadline.  
Feedback per mail och i Google Classroom.  
Commits efter deadline beaktas inte — spara commit-hashen du lämnar in på.
