# Prompt: Generera Story-Driven Code

Kopiera och klistra in detta i Claude/ChatGPT. Fyll i de markerade fälten.

---

## FYLL I INNAN DU KÖR

```
C#-KONCEPT SOM TRÄNAS: [t.ex. for-loopar, klasser och objekt, if-satser, listor]
TEMA/STORY:            [t.ex. detektiv, fantasy, sci-fi, historia, matlagning]
SVÅRIGHETSGRAD:        [🟢 Grundnivå / 🟡 Mellannivå / 🔴 Utmaning]
ÖVNING 1 TEMA:         [t.ex. katter]
ÖVNING 2 TEMA:         [t.ex. bilar — samma struktur, annat tema]
```

---

## PROMPTEN

```
Du ska skapa ett story-driven code-paket för en YH-kurs i C# på YRGO.
Följ Marcus Medina-metoden (se stilregler nedan).

KONCEPT SOM TRÄNAS: [C#-KONCEPT]
TEMA: [TEMA/STORY]
SVÅRIGHETSGRAD: [SVÅRIGHETSGRAD]

Skapa tre filer:

---

### FIL 1: lecture_code.cs
Fullständig, körbar C#-kod som används i föreläsningen.
- Visar konceptet tydligt med [TEMA]-tema
- Välkommenterade på svenska (kommentarer förklarar varför, inte vad)
- Kod på engelska, kommentarer och output på svenska
- Följer Clean Code och KISS

---

### FIL 2: ovning_01_[ÖVNING1TEMA].md
Övning 1 — samma scenario som föreläsningskoden, lite guidning.

Struktur:
# [Äventyrstiteln]

## 🎯 Mål med övningen
- [Konkreta lärandemål kopplade till konceptet]

## 🧩 Mysteriet/Historien
[2-3 meningar som sätter scenen. Engagerande, inte akademisk.]

## 🚀 Kom igång
[Startkod med ledande kommentarer — inte lösning, men tydliga hints]
Varje steg är ett kodblock följt av en <details>-tagg med tipset.

## ✅ Förväntat resultat
[Exakt output som ska visas när programmet körs korrekt]

## 💡 Lärdomar
[Vad konceptet lärde dem — koppling till verkligheten]

---

### FIL 3: ovning_02_[ÖVNING2TEMA].md
Övning 2 — exakt samma struktur och krav som övning 1, men med [ÖVNING2TEMA] som tema.
Ingen extra guidning — de ska klara det själva nu.
Lägg till bonussektion för kodbyte och felsökning med en annan grupp.

---

## STILREGLER (Marcus Medina-metoden)

**Ton:**
- Prata med läsaren: "du", "vi", "låt oss"
- Engagerande och varm — inte akademisk
- Avsluta med uppmuntran: "Snyggt jobbat!" eller liknande
- Retoriska frågor: "Hängde du med?"

**Struktur:**
- Korta stycken, max 3-4 meningar
- Visuella pauser med emojis (sparsamt)
- Variera meningslängd — ibland korta. Ibland lite längre resonemang.

**Kod:**
- Engelska variabelnamn
- Svenska kommentarer och output
- Clean Code — beskrivande namn
- En sak i taget — visa inte allt på en gång
- Föredra string interpolation ($"...") framför +

**Undvik:**
- "It is important to note", "Furthermore", "In conclusion"
- Formella akademiska övergångar
- Generiska summeringar
- Varje mening lika lång

**Terminologi:**
- "Studerande" — aldrig "student" eller "elev"
- Kod på engelska, kommentarer och output på svenska

**Kom ihåg:**
Marcus sätt är inte det enda sättet. Lägg in en påminnelse om det i övning 2:
> 💬 "Det här är ett sätt att lösa det. Ditt sätt kan vara lika bra."
```

---

## EXEMPEL PÅ IFYLLT

```
C#-KONCEPT SOM TRÄNAS: for-loopar och ackumulatorer
TEMA/STORY:            Detektiv, 1800-talets London
SVÅRIGHETSGRAD:        🟢 Grundnivå
ÖVNING 1 TEMA:         sherlock (detektiv undersöker ledtrådar)
ÖVNING 2 TEMA:         indiana (arkeolog undersöker artefakter)
```
