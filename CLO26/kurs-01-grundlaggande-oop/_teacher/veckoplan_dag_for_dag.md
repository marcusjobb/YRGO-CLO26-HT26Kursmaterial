# Veckoplan dag-för-dag — Kurs 01: OOP i C# Grund

**Kurs:** V36–V40 | 31 aug – 4 okt 2026  
**Schema:** Mån fm + Mån em + Tis fm + Tis em + Ons (online) = 5 undervisningspass/vecka  
**Tor:** Online handledning (ej schemalagd undervisning)  
**Fre:** Eget arbete

---

## Ämnen med tidsuppskattning

| Ämne | Tid | Nivå | Vecka |
|------|-----|------|-------|
| Kursintro + miljöinstallation (VS Code, .NET, Git Bash) | 1 pass | 🟢 | V36 |
| Git grund (init, add, commit, push, clone) | 1 pass | 🟢 | V36 |
| SSH + GitHub (ssh-keygen, klona, pusha) | 1 pass | 🟡 | V36 |
| Variabler och datatyper (int, double, string, bool, char) | 1 pass | 🟢 | V37 |
| Villkorssatser (if, else if, else, bool-operatorer) | ½ pass | 🟢 | V37 |
| Loopar (for, while, do-while, foreach) | ½ pass | 🟢 | V37 |
| Metoder (signatur, parametrar, returvärde) | ½ pass | 🟡 | V37 |
| Pseudokod + problemlösning (programmerarens tankesätt) | 1 pass | 🟡 | V37 |
| Story-driven code (träning på allt ovan) | 1 pass | 🟡 | V37 |
| UML-klassdiagram (rita innan koda) | ½ pass | 🟡 | V38 |
| Klasser och objekt (konstruktor, properties, fält) | 1 pass | 🟡 | V38 |
| Inkapsling (private/public/protected, get/set) | 1 pass | 🟡 | V38 |
| Övning + handledning klasser | 1 pass | 🟡 | V38 |
| List\<T\> (lägga till, ta bort, loopa, Count) | 1 pass | 🟢 | V39 |
| Enum (när och varför, syntax, användning) | ½ pass | 🟢 | V39 |
| Dictionary\<K,V\> (grundläggande, när man väljer vad) | ½ pass | 🟡 | V39 |
| Gruppuppgift: Skogsäventyret (design + UML) | 1 pass | 🔴 | V39 |
| Repetition inför tenta | 1 pass | — | V40 |
| Tenta | 1 pass | — | V40 |

**Totalt:** ~17 pass aktiv undervisning + 3 pass handledning/eget arbete fördelat på 5 veckor.

---

## Vecka 1 — Miljö, Git, variabler, if (V36, 1–5 sep)

**Mål:** Alla har fungerande miljö. Kan göra ett komplett git-flöde. Kan deklarera variabler och skriva if-satser med verkliga scenarion.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån 1 sep | — | Lära känna-aktivitet — ingen programmering | — |
| Tis 2 sep | campus | **Dator med.** Kursintro, förväntningar, kursplan. Installation: VS Code, .NET SDK, Git Bash, Windows Terminal | Installation |
| Ons 3 sep | online | SSH-nycklar, GitHub-konto, klona kursrepo, första push. Metropolitan Club-pussel (samarbetsövning) | Git grund + SSH |
| Tor 4 sep | campus | **Flödescheman + todo-listor** (penna + papper — tänka innan koda). Sedan: **variabler + datatyper.** Story-driven live-kod: `int pris=500; int saldo=400; int låna=250; int kvar=pris-saldo-låna;` — vad händer? Övningar inkl. **Den saknade kronan** (tre kompisar, pizza för 120 kr, felväxling — koda upp och fixa beräkningen) | Flödescheman + Variabler |
| Fre 5 sep | campus | **If-satser.** Story-driven: "Jag ska köpa en TV — är det billigare att köra till Varberg (NetOnNet) eller Sisjön (Elgiganten) när man räknar in bensinen?" Kod + övningar | If-satser |

**Nyckelord denna vecka:** `git`, `commit`, `push`, `clone`, `SSH`, `int`, `double`, `string`, `bool`, `variabel`, `deklaration`, `tilldelning`, `if`, `else`

**Inlämning:** Ingen.

---

## Vecka 2 — C# repetition + tänkverktyg (V37, 7–12 sep)

**Mål:** Varm i kläderna inför OOP. Förstår variabler, villkor, loopar och metoder. Kan tänka i pseudokod.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån 8 sep | fm | Variabler + datatyper: int, double, string, bool, char. Live-kod: "gissa vad som händer" | Variabler |
| Mån 8 sep | em | Villkorssatser (if/else) + loopar (for, while, foreach) + metoder. Live-kod + övningar | If / Loopar / Metoder |
| Tis 9 sep | fm | Pseudokod-övningen: "instruera roboten" — penna + papper, ingen dator | Problemlösning |
| Tis 9 sep | em | Rubber duck-intro (gummiankor delas ut 🦆) + klasser: första titt, vad är ett objekt? | Intro klasser |
| Ons 10 sep | online | Story-driven code: Grlub Trollet → Sherlock Holmes → Harry Potter. Level 1–3 | Repetition + flöde |

**Nyckelord denna vecka:** `variabel`, `datatyp`, `deklaration`, `tilldelning`, `metod`, `returvärde`, `pseudokod`

**Inlämning:** Ingen.

---

## Vecka 3 — Klasser och objekt (V38, 14–19 sep)

**Mål:** Kan skriva en klass med properties, konstruktor och metoder. Förstår private/public och varför inkapsling finns.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån 15 sep | fm | UML-klassdiagram: rita INNAN vi kodar. Whiteboard + penna. Modell: BankAccount | UML |
| Mån 15 sep | em | Klasser i C#: konstruktor, properties (private set), fält, metoder. Föreläsning: `BankAccount` | Klasser |
| Tis 16 sep | fm | Inkapsling: varför fält är privata. `private` vs `public` vs `protected`. Diskussion + kodsnuttar | Inkapsling |
| Tis 16 sep | em | Övning 1 (Car) + Övning 2 (Spaceship). Inlämning 1 (Spellistan) presenteras | Övning |
| Ons 17 sep | online | Handledning: studerande kodar, Marcus tillgänglig för frågor | Handledning |

**Nyckelord denna vecka:** `klass`, `objekt`, `instans`, `konstruktor`, `property`, `private`, `public`, `encapsulation`

**Inlämning 1:** Spellistan — deadline sön 21 sep 23:59

---

## Vecka 4 — Samlingar, datastrukturer, enums (V39, 21–26 sep)

**Mål:** Kan använda List och enum. Förstår när man väljer List vs Dictionary. Skogsäventyret igång.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån 22 sep | fm | `List<T>`: lägga till, ta bort, loopa, Count. Praktiska exempel | List |
| Mån 22 sep | em | `enum`: när och varför, syntax, hur det passar in i klasser | Enum |
| Tis 23 sep | fm | `Dictionary<K,V>`: grundläggande, när man väljer List vs Dictionary vs array | Dictionary |
| Tis 23 sep | em | Skogsäventyret presenteras: grupptilldelning (2–3 pers), design-diskussion, UML på whiteboard | Gruppuppgift |
| Ons 24 sep | online | Handledning: grupperna designar och börjar koda | Handledning |

**Nyckelord denna vecka:** `List`, `Dictionary`, `enum`, `generics`, `collection`, `arv`, `subklass`, `basklass`

**Skogsäventyret:** deadline sön 28 sep 23:59

---

## Vecka 5 — Tenta + ikapp (V40, 28 sep–4 okt)

**Mål:** Tentan visar förståelse. Fri tid för att koda ikapp och stärka projektet.

| Dag | Pass | Innehåll | Ämne |
|-----|------|----------|------|
| Mån 29 sep | fm+em | Repetition: alla ämnen på en rad. Tentaförberedelse. Handledning öppen | Repetition |
| Tis 30 sep | fm | **TENTA** 09:00–11:00 | Tenta |
| Ons 1 okt | online | Q&A om tentan. Genomgång av vanliga fel. Skogsäventyret sista handledning | Genomgång |

**Tenta täcker:** Git, variabler, if/else, loopar, metoder, klasser, inkapsling, List, enum  
**Betygsgräns:** G = 58p · VG = 82p (av 97p)

---

## Sammanfattning: svårighetsgrad och tid per ämne

```
Git grund          🟢 1 pass   — konkret och praktiskt, alla lyckas
SSH + GitHub       🟡 1 pass   — tekniskt, kräver felsökning per dator
Variabler          🟢 1 pass   — repetition för de flesta
If/else + loopar   🟢 ½ pass   — repetition, går snabbt
Metoder            🟡 ½ pass   — parametrar och returvärden tar tid att sätta
Pseudokod          🟡 1 pass   — tankesättet är nytt, värd tid
UML                🟡 ½ pass   — rita = förstå, investering som lönar sig
Klasser            🟡 1 pass   — kärnan i kursen, ta god tid
Inkapsling         🟡 1 pass   — "varför private?" behöver diskuteras
List<T>            🟢 1 pass   — logisk, snabbinlärd
Enum               🟢 ½ pass   — enkelt koncept
Dictionary         🟡 ½ pass   — key-value är nytt tänk
```

---

## Nästa kurs att planera

- [ ] Kurs 02: Databaser (SQL, Entity Framework)
- [ ] Kurs 03: Fördjupad OOP (arv, polymorfism, interface, design patterns)
- [ ] Kurs 04: Test och kvalitet
