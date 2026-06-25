# Skrivregel: Marcus Medina-metoden

> Denna stilguide är baserad på Marcus Medinas undervisningsstil, utvecklad och förfinad
> genom år av praktisk undervisning i programmering på YRGO.
> Den dokumenterar hur Marcus skriver, förklarar och engagerar — så att allt material,
> oavsett om det skrivs av Marcus själv eller med hjälp av AI, håller hans röst och stil.
>
> **Upphovsman: NionIT**

Dessa regler gäller allt kursmaterial — lästext, slides, övningar och feedback.

---

## Ton och röst

**Prata med läsaren, inte åt dem.**

Använd alltid "du" och "vi". Skriv som om du förklarar för en kompis över en kopp kaffe, inte som om du skriver en lärobok.

Fraser som funkar:
- "Hängde du med?"
- "Tänk dig att..."
- "I min erfarenhet..."
- "Låt oss kika på det här tillsammans."

Avsluta alltid med en känsla, inte bara en summering. Ett "Du klarar det här" eller "Snyggt jobbat!" betyder mer än det låter.

---

## Struktur (ADHD-vänlig)

- **Korta stycken** — max 3–4 meningar. Sedan ny rad.
- **Variation** — ibland korta meningar. Ibland lite längre resonemang som bygger upp något. Aldrig alla meningar lika långa.
- **Underrubriker** — flitigt. De är andningspunkter för ögat.
- **Emojis** — sparsamt men medvetet. Markera innehållstyper konsekvent.
- **Upprepa viktiga koncept** — men på olika sätt: teori, analogi, pseudokod, kod, diagram.

---

## Metaforer och analogier

Fackspråk ska alltid ha en vardaglig mothängare. Tänk IKEA, verktygslådor, recept, brödrostar, LEGO.

> En for-loop är som ett recept du ger datorn: "börja räkna från 1, fortsätt så länge du är under 11, öka med 1 för varje varv." Start, stoppvillkor, nästa steg. Enkelt, eller hur?

När du vill ha en personlig historia — skriv en platshållare:
`[Marcus, infoga en kort historia här om när du kämpade med X]`

Använd max **två** metaforer per ämne. Utveckla minst en av dem ordentligt — nämn den inte bara en gång och gå vidare.

---

## Undvik AI-skrivfällor

Byt ut stelt mot mänskligt:

| ❌ Stelt | ✅ Mänskligt |
|---------|-------------|
| "It is important to note" | "Kom ihåg att..." |
| "Furthermore" / "In addition" | "och" / "dessutom" / "men" |
| "In conclusion" / "To summarize" | "Summan av kardemumman" / "Kortfattat" |
| "It is imperative" | "Det är viktigt" |
| "Utilize" | "använd" |
| "This suggests that" | "Det betyder att" |

Inga pompösa floskler. Ingen marknadsföringston. Inga generiska summeringar.

---

## Dopamine Design — bygg mot ett aha-ögonblick

Varje lektion och text ska ha en peak — ett moment som får läsaren att känna något.

1. **Hook** — börja med något som griper tag. En fråga, ett problem, en provocerande påstående.
2. **Bygg** — bygg förståelsen steg för steg.
3. **Peak** — leverera insikten. Det oväntade sambandet. Lösningen på det irriterande problemet.
4. **Landa** — befäst med tydliga takeaways.

Målet är inte bara att överföra information. Det är att skapa genuina ögonblick av förståelse och nyfikenhet.

---

## Pausch-filosofin (feedback och uppmuntran)

Inspirerad av Randy Pausch:

> "That was pretty good, but I know you can do better."

Sätt aldrig ett tak på potential. Om förväntningarna överträffas — höj ribban.

| Istället för... | Använd... |
|----------------|-----------|
| "Du saknar..." | "Till nästa gång, tänk på..." |
| "Det var dåligt att..." | "För att skärpa det här ytterligare..." |
| "Du behöver förbättra..." | "Grunden är stark — med X blir det riktigt bra" |

---

## Pedagogisk progression

Varje koncept ska följa denna ordning:

1. **Förklaring** — vad och varför. Koppla till verkliga tillämpningar.
2. **Pseudokod** — kärnan i vardagsspråk.
3. **MVP** — minimal, fungerande kod som visar grundkonceptet.
4. **Progression** — enkelt → avancerat → alternativa implementationer.
5. **Clean Code för konceptet** — se nedan.
6. **Vanliga misstag** — vad folk brukar göra fel och varför.
7. **Sammanfattning** — kärnpunkterna, kondenserade.
8. **Övningar** — från enkel till utmanande.

---

## Gammal stil → ny stil — visa alltid båda

När C# har en äldre och en modernare syntax för samma koncept ska **alltid båda visas** — i den ordningen. Gamla stilen först, nya stilen sen.

**Varför:**
- Studerande stöter på gammal kod i verkligheten. Den ska inte vara oläslig.
- Den som förstår vad `{ get { return _name; } set { _name = value; } }` gör förstår *varför* `{ get; set; }` är en förkortning — inte bara att det fungerar.
- Utan kontexten är modern syntax magi. Med den är det självklar förenkling.

**Format:**
```markdown
**Gammal stil:**
```csharp
// Så här såg det ut — och så här ser det fortfarande ut i äldre kodbaser
```

**Modern stil (C# X):**
```csharp
// Kortare, men gör exakt samma sak
```
```

**Exempel att alltid visa i par:**

| Koncept | Gammal stil | Modern stil |
|---------|-------------|-------------|
| Property | `get { return _a; }` `set { _a = value; }` | `{ get; set; }` |
| Logiska operatorer i if | `a > 3 && a < 10` | `a is > 3 and < 10` (C# 9) |
| Null-check | `if (x != null)` | `if (x is not null)` |
| Konstruktor med validering | Fullständig konstruktorkropp | Primary constructor (C# 12) |
| Strängkonkatenering | `"Hej " + namn + "!"` | `$"Hej {namn}!"` |
| Metodreferens | `delegate void MyDelegate(int x)` | `Action<int>` |
| Switch | `switch (x) { case 1: break; }` | `x switch { 1 => ..., _ => ... }` |

*Regeln gäller alla föreläsningar och lästext. Hoppa aldrig direkt till modern syntax utan att visa vad den härstammar från.*

---

## Clean Code integrerat i varje koncept

Clean code är **inte** ett separat kapitel. Det presenteras direkt efter varje nytt koncept — medan kontexten är färsk.

**Format:**
```markdown
### Clean Code — [konceptnamnet]

> Så här skriver du det rätt enligt Uncle Bob.

// ❌ Vanligt men dåligt
// ✅ Läsbart och tydligt

- Konkret regel för just det här konceptet
```

**Varför:**
En studerande som lärt sig for-loopar och direkt ser hur de skrivs rent förstår kopplingen. En studerande som lär sig for-loopar i vecka 2 och clean code i vecka 5 kopplar aldrig ihop dem.

**Exempel per koncept:**
- **for-loop:** beskrivande loop-variabelnamn, inte `i` i loopar som gör mer än indexera, inte för många rader inuti loopen
- **if-sats:** undvik dubbla negationer (`if (!isNotActive)`), extrahera komplexa villkor till namngivna booleans
- **klass:** ett ansvar, inga 500-raders klasser, konstruktor ska inte göra affärslogik
- **metod:** under 20 rader, ett verb som namn, returnera tidigt istället för nästlade if:ar

---

## Kodregler

**All kod skrivs på engelska. Kommentarer och output skrivs på svenska.**

```csharp
// Beräkna ålder baserat på födelseår
int age = currentYear - birthYear;
Console.WriteLine($"Du är {age} år gammal.");
```

Kodprinciper att följa:
- **Clean Code** — beskrivande namn, läsbar struktur
- **KISS** — börja alltid med den enklaste lösningen
- **SRP** — varje metod/klass har ett ansvar
- **DRY** — upprepa inte kod
- **YAGNI** — implementera bara det som behövs nu

Riktlinjer för kodexempel:
- Visa ett koncept i taget — inte allt på en gång
- Kommentera sparsamt men meningsfullt — förklara *varför*, inte *vad*
- Använd whitespace — luftig kod är lättläst kod
- Föredra string interpolation (`$"..."`) framför `+`-konkatenering

---

## Svårighetsmarkering i övningar

Märk alltid övningar tydligt:

- 🟢 **Grundnivå** — samma eller liknande som föreläsningsexemplet
- 🟡 **Mellannivå** — kräver att man kombinerar koncept
- 🔴 **Utmaning** — för de som vill ha mer

---

## Facit i övningar

Dölj alltid facit bakom en `<details>`-tagg:

```markdown
<details>
<summary>💡 Klicka här för ett lösningsförslag</summary>

Din lösning kan se annorlunda ut och ändå vara helt korrekt!

```csharp
// Fullständig, välkommenterad lösning
```

</details>
```

---

## Attribution — allt material

All material som produceras ska märkas med:

```
Av Marcus Ackre Medina · Nion Education · marcus.medina@nionit.com
```

- **Marp-slides:** hanteras automatiskt av `nion-dark`-temat
- **Markdown-dokument (ej studeranderiktat):** i frontmatter (se nedan)
- **Studeranderiktat material:** i filens sidfot eller i README

---

## Frontmatter — interna dokument

Alla markdown-filer som **inte** är studeranderiktat material ska ha frontmatter för enkel sökning och indexering:

```yaml
---
name: kort-kebab-case-namn
description: En mening som förklarar vad filen är
tags: [kurs-01, vecka-2, lektion, git]
author: Marcus Ackre Medina
---
```

**Gäller:** lärardokument, dagplaner, kursguider, lösningar, scripts, writing_rules, templates
**Gäller inte:** studeranderiktat material (övningar, lästext, slides) — de ska vara rena och enkla

---

## Marp-slides

Föreläsningen ska vara rapp — max 15 minuter. De studerande har redan läst, slides är repetition och fokus på det tyngsta.

**Tema:** `nion-dark` — CSS finns i `/res/nion_dark.css`

Frontmatter-mall för alla Marp-slides:

```yaml
---
marp: true
theme: nion-dark
paginate: true
---
```

Attribution visas automatiskt via CSS-temat. Titelsida: `<!-- _class: title -->` på första sliden.

- Börja med en stark hook — varför ska vi bry oss om det här?
- Använd Mermaid-diagram för att illustrera flöden och relationer
- Inkludera korta kodexempel (MVP/pseudo — inte fullständiga lösningar)
- Avsluta med: "Dags att kavla upp ärmarna!" och peka på övningarna

**Mermaid-formatering (kritiskt):**
```
(tom rad)
```mermaid
diagram här
```
(tom rad)
```
Felaktig formatering gör att diagrammet inte renderas.

---

## Övning 2 — kodbyte och felsökning

Övning 2 (och framåt) ska alltid ha en bonussektion:

> Byt kod med ett annat par. Era uppgifter:
> - Hårdkodade värden — finns det "magiska siffror" som borde vara variabler?
> - Namngivning — förstår du vad variablerna gör utan att läsa resten?
> - Felhantering — kraschar programmet vid oväntad input?
> - Kommentarer — finns det kommentarer som förklarar *varför*?

---

## Mitt sätt är inte det enda sättet — återkommande påminnelse

Detta är en kärnfilosofi som ska dyka upp **återkommande** i materialet, inte bara nämnas en gång och glömmas bort. Påminn om det med jämna mellanrum under kursens gång.

**Filosofin:**
Det Marcus visar är *ett* sätt att lösa ett problem — det sätt han är van vid. Det är inte den heliga bibeln. Det finns ofta flera bra lösningar, och din lösning kan vara lika bra eller bättre.

Målet är att de studerande ska:
- Hitta och utveckla **sin egen** kodstil
- Lära sig att **anpassa sig till andras** stil i projektarbete och senare på jobbet
- Förstå att "fungerar det och är det läsbart?" ofta är tillräckligt

Marcus säger till om en lösning är långsökt eller onödigt komplicerad. Annars — är det en bra lösning, varför krångla?

**Hur det ska synas i materialet:**
Lägg in påminnelser organiskt i lästext och slides, särskilt efter kodexempel:

> 💬 *"Det här är hur jag brukar lösa det. Har du en annan approach som funkar lika bra? Kör på den."*

> 💬 *"Min lösning är inte facit. Om din kod fungerar och är läsbar — bra jobbat."*

> 💬 *"I ett riktigt projekt kommer du jobba med folk som kodar annorlunda än dig. Det är bra träning att förstå andras kod, inte bara din egen."*

---

## Överkursmaterial

Varje vecka kan ha ett överkursdokument. Det följer dessa regler:

- **Börjar alltid med:** `> 🟣 Överkurs — inte krav, kommer inte på tentan.`
- **Ingen separat föreläsning** — lektionen är inbakad direkt i dokumentet
- **Självständigt** — studerande ska kunna läsa och förstå utan att fråga
- **Namnges:** `overkurs_NN_amne.md` i `exercises/`-mappen
- **Publiceras** tillsammans med veckans vanliga material

Mallen finns i `_templates/overkurs.md`.

Överkursmaterial ska aldrig kännas som ett krav som smugits in. Om det finns med i materialet ska det vara tydligt markerat och lätt att skippa.

---

## Marcus-ord att känna till

**"Koda vilt"** = Koda med fullt engagemang. Sätt igång och kör.
