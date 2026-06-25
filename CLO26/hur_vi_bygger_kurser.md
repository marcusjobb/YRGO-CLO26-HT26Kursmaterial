# Hur vi bygger kurser — NionIT / YRGO

En guide för att sätta ihop nya kurser konsekvent.
Följ den här, så blir det rätt — oavsett vem som bygger.

---

## Mappstruktur

- `skola/program/kurs-NN-namn/` — en mapp per kurs
- Inuti varje kurs: `NN_veckonr-topic/` för varje ämnesmodul
- `_teacher/` innehåller allt som studerande aldrig ska se
- `_scripts/` innehåller lärarverktyg — dokumentera alltid i `_scripts/README.md`
- `_templates/` innehåller mallar för föreläsning, övning och inlämning

---

## Planering — bygg alltid bakåt

1. **Inlämningen** — vad ska de visa att de förstått?
2. **Övningarna** — vad tränar dem inför inlämningen?
3. **Lektionerna** — vad behöver de lära sig för övningarna?
4. **Veckans nördiska** — vilka termer dyker upp? (görs sist)

---

## Veckostruktur

- Första veckan: ingen inlämning
- Sista veckan: tenta + fri tid att koda ikapp, minimalt med nya moment
- Julveckor/helgveckor: grupparbete (YRGO kör under dessa veckor av CSN-skäl)
- Deadline inlämningar: söndag 23:59
- Rättning: måndag
- Missad deadline → förlängd till påföljande fredag

---

## Inlämningar

- Max 1 inlämning per vecka
- Minst en inlämning per kurs är **grupparbete** — helst den sista
- Inlämning = samma typ som övningarna men annat tema (katt-övning → hund-inlämning)
- Alltid med reflektion: vad var svårt/lätt, vad kan du använda det till, vad lärde du dig
- Spara alltid commit-hash vid rättning — studerande kan inte ändra efter deadline
- Slutinlämningen sammanfattar alla lärandemål från hela kursen

---

## Tentor

- Max 2 tentor per kurs
- Tentafrågor publiceras aldrig i förväg

---

## Betyg

- **G** = alla inlämningar godkända + godkänd tenta
- **VG** = G-kraven + VG-prestationer i inlämningar och/eller tenta

---

## Övningar

- Övning 1 = samma scenario som föreläsningens exempel (trygghet)
- Övning 2 = identisk struktur, annat tema — omedveten repetition
- Teman: djur, fordon, mat, sport, rymd — vad som är roligt och begripligt
- Alltid med förväntad output (exakt vad som skrivs ut)
- Facit i `_teacher/solutions/` — aldrig i studerandemappen
- Svårighetsgrad: 🟢 Grundnivå / 🟡 Mellannivå / 🔴 Utmaning
- **15-minutersregeln** syns i varje övning: fastnar du > 15 min → klass → AI → lärare
  > [Marcus, berätta att regeln kommer från konsultvärlden — det ger den trovärdighet och visar att det är så proffs faktiskt jobbar]

---

## Lektioner

- Varje ämne har två filer: `amne_marp.md` (lectures/) + `amne.md` (notes/)
- Exempelkod i `examples/` — samma kod används i lectures/ och övning 1
- Föreläsning max 15 minuter — de har läst, slides är repetition
- Dag 1: intro + demo + övningar
- Dag 2: genomgång + fördjupning + övningar
- Dag 3: online handledning (Discord/Meet)
- Lektioner är 45 minuter — schema 9–12 och 13–15

---

## Examination i AI-era

Traditionella kodtentor är problematiska i AI-era — verktyg finns som kan hjälpa de
studerande oavsett examinationsplattform. Frågan är inte om det händer, utan vad vi
faktiskt vill testa.

**Tentorna bör testa det AI inte kan göra åt dem:**
- Förklara *varför* ett designval gjordes
- Rita och förklara ett flödesschema eller UML-diagram
- Identifiera vad som saknas i en specifikation
- Granska och kritisera befintlig kod
- Planera en lösning utan att skriva koden

Syntaxfrågor ("skriv en for-loop") är inte längre meningsfulla på tenta.
Förståelsefrågor ("varför fungerar inte den här koden?") är det.

---

## Det stora skiftet — AI-era pedagogik

Världen har förändrats. AI skriver boilerplate på sekunder. Det är inte längre det viktigaste
att kunna syntax utantill. Det som gör en bra utvecklare idag är META-färdigheterna:

1. **Specificera för sig själv** — tänka tillräckligt klart för att veta vad man vill ha
2. **Faila i planeringen** — lär sig planera bättre genom att planera fel, inte koda fel
3. **Specificera för andra** — kommunikation, dokumentation, user stories, kravställning
4. **Flödesscheman** — visualisera ett system innan man bygger det
5. **Se hela bilden** — systemtänkande, arkitektur, konsekvenser av val
6. **Granska AI-output kritiskt** — är koden rätt? Löser den rätt problem?

AI:n är ett perfekt verktyg för den som kan specificera — och en kaosmaskin för den som inte kan.

**Vad det innebär för kurserna:**
- Mer fokus på planering, diagram och kravspecifikation
- Övningar där man måste ställa rätt frågor, inte bara skriva rätt kod
- Tankenötter och ambivalenta berättelser tränar detta (se nedan)
- Reflektion i inlämningar: "vad behövde du veta för att lösa det här?"
- Kod kan genereras med AI — förståelse och planering kan inte det

---

## Lärarknep som engagerar

### Antipattern — katten och fönstret på vintern
Passar perfekt när antipatterns introduceras i kurs 3.

> [Marcus, berätta om katten som alltid hoppade ut och in genom fönstret på första våningen —
> fungerade perfekt hela sommaren. Tills vintern kom. Kaboom. Stackars misse.
> Men efter 3-4 försök lärde sig katten — krafsade på rutan istället. Nytt, bättre mönster.
> Poängen: ett antipattern är att göra samma sak på samma sätt utan att ifrågasätta
> om det fortfarande passar. Och iteration + lärande är svaret — precis som i TDD.]

Koppla till AI: AI gör samma sak — kopierar lösningar som fungerat tidigare utan att
förstå om de passar just det här problemet. Därför måste ni förstå koden, inte bara köra den.

Passar i: Kurs 3 (antipatterns, designmönster), Kurs 4 (TDD — testa innan du deployar till vinter).

---

### TDD — tråkigt tills man var ensam i ett projekt
Perfekt öppning för TDD-introduktionen i kurs 4.

> [Marcus, berätta att TDD var himla tråkigt när du lärde dig det.
> Sen hamnade du ensam i ett projekt — inga kodgranskare, mystiska buggar dök upp.
> Du insåg värdet. Och gjorde vad varje programmerare utan AI skulle gjort:
> skapade ett program som läste din kod och *genererade* testkoden.
> Men för att göra det var du tvungen att förstå exakt vad ett test är.
> Genvägen var den längre vägen — och du lärde dig TDD på riktigt.]

Koppla till AI: AI gör nu det ditt program gjorde. Men principen är densamma —
förstår du inte vad ett bra test ser ut som, kan du inte avgöra om AI:ns tester är bra.

---

### 5 Whys — bok i huvudet (privat och professionellt)
Passar när man introducerar 5 Whys i TDD och rotorsaksanalys.

> [Marcus, berätta om dejten där du försökte säga något romantiskt men det kom ut
> som "du ska inte oroa din lilla söta hjärna" → bok i huvudet → skrattbryt → bra kväll.
> Poängen: 5 Whys-tekniken kan provocera om man inte är noga med hur man formulerar sig.
> Det gäller lika mycket med kunder och kollegor som på dejter.]

Passar också i: Kurs 7 (Konsultmässighet) — kommunikation och formulering.

---

### Quake-buggen — kompileringsfel och teknisk skuld
Perfekt anekdot när man introducerar kompileringsfel, felsökning och teknisk skuld.

> [Marcus, berätta om Quake-modden där en `}` saknades — du sökte länge, gav upp,
> satte `}` på första bästa ställe, koden kompilerade och spelet funkade.
> Buggen finns fortfarande någonstans i koden.]

Poängen: att koden kompilerar betyder inte att koden är rätt.
AI gör exakt samma sak — fixar symptomet, inte orsaken. Det är därför vi behöver
förstå vad vi skriver, inte bara få det att kompilera.

Passar i: kurs 1 (kompileringsfel), kurs 3 (refactoring/teknisk skuld), kurs 4 (TDD).

---

### Console.ForegroundColor — avsiktligt dålig smak
Visa kod med konsolfärger och välj medvetet fula/oläsliga kombinationer.
De studerande kan inte hålla sig — de rättar dig direkt och börjar experimentera med färger.
Bonus: perfekt introduktion till `ConsoleColor`-enumet. De lär sig enum-syntax
genom att peta med färger de faktiskt bryr sig om.

```csharp
Console.ForegroundColor = ConsoleColor.DarkYellow; // välj något hemskt
Console.BackgroundColor = ConsoleColor.DarkYellow; // ännu värre
Console.WriteLine("Kan ni se det här?");
Console.ResetColor();
```

> [Marcus, låt dem välja färger live — de tar över lektionen utan att märka det]

### SQL Injection — live-hack av en loginruta
Förbered ett enkelt loginformulär med en sårbar SQL-fråga.
Hacka det live inför klassen med `' OR '1'='1` och lista alla användare och emails.

Det brukar skrämma dem — på ett bra sätt. De förstår *varför* parameteriserade frågor
existerar när de ser det med egna ögon.

Gammalt material att utgå från:
`/home/marcus/git/Old_courses/2025/2_databases/exercises/ado/SQL_injection_1_Bobby_tables.md`
`/home/marcus/git/Old_courses/2025/2_databases/exercises/ado/SQL_injection_2_admin_hack.md`

Koppla till: säkerhet, behörigheter, och varför vi aldrig konkatenerar SQL-strängar.

---

## Icke-kodrelaterade inslag (återkommande)

- **Problemlösningspussel** — detektivgåtor, logikproblem, utan kod
  Tränar samma tankeprocess som programmering men utan syntaxbarriär

- **Ambivalenta berättelser** — berättelser utan uppenbar lösning, t.ex:
  *"En person går in i en butik och springer ut. Kassan var tömd. Någon skrek."*
  Vad hände? Man vet inte — man måste ställa rätt frågor.
  Kopplingen till programmering: kunden säger "jag vill ha en app som håller koll på saker" —
  vad menar de? Tränar studerande att identifiera vad de VET vs vad de ANTAR.

- **Veckans nördiska** — 5–8 termer de kommer möta under veckan, kort förklaring
  Görs alltid sist, när vi vet vilka ord som dyker upp i materialet

---

## UML — introduceras tidigt, återkommer alltid

UML introduceras som **första lektionen** i det relevanta momentet (kurs 3 vecka 1).
Därefter ska UML finnas med i **varje databasrelaterad inlämning** och på **tentan**.

Studerande behöver inte memorera UML — de behöver träna det tills det sitter.
Inbyggd repetition utan extra övningar: det är alltid en del av inlämningen.

---

## Kanban och grupparbete — startar tillsammans

Kanban introduceras med grupparbete som förutsättning — det är ett verktyg för team, inte individer.
Från det momentet (kurs 3 vecka 3) löper grupparbete hela vägen till kursens slut.

- Kanban-inlämningar = fiktiva uppgifter att planera och presentera som grupp
- Sista projektet i kursen = alltid grupparbete
- Presentationer hålls **efter tentan** (samma dag eller sista dagen)

---

## Planeringsmetoder — introduceras gradvis

Planeringstekniker från kurs 4 (TDD/SCRUM) fördelas ut tidigt så att kurs 4
är en fördjupning, inte en chock. Kopplar till AI-era-skiftet — allt handlar om
att tänka innan man kodar.

**Ordningen är viktig:** pseudokod lär dem tänka kod → rubber duck lär dem förklara kod →
båda är exakt AI-flödet (specifikation in, granskning ut).

| Teknik | Passar i | Varför just där |
|--------|----------|----------------|
| **Pseudokod** 🤖 | Kurs 1, v2 | Tänk ut vad du vill att koden ska göra *innan* du skriver den |
| **Rubber duck** 🦆 | Kurs 1, v2–3 | Förklara vad koden *faktiskt* gör när det inte fungerar |
| **Enkla User Stories** | Kurs 1, v3 | Innan de skriver klasser — vad ska klassen göra? |
| **Definition of Done** | Kurs 2, v1 | När är en inlämning faktiskt färdig? |
| **MoSCoW-prioritering** | Kurs 2 | Must/Should/Could/Won't för databasfeatures |
| **Six Thinking Hats** | Kurs 3, hellweek | Kräver tillräcklig mognad |
| **5 Whys** | Kurs 4 | Rotorsaksanalys — passar TDD naturligt |
| **Formell retrospektiv** | Kurs 4 | Fullt SCRUM-sammanhang |

> 🦆 Marcus delar ut fysiska gummiankor — en per studerande. Budget: kolla med skolan/NionIT.
> De använder dem faktiskt.

> 🤖 **Pseudokod-introduktion:** Marcus var 14 år. Dataläraren på 80-talet ställde frågan:
> *"Om jag vore en robot — hur skulle ni säga till mig att gå mot dörren?"*
> Någon sa "gå fram". Läraren: "Hur?"
> Efter diskussion: lyft benet lite, rör det framåt, sätt ner, nästa ben...
> Poängen: datorn (och AI:n) är den roboten. Den vet inte vad "gå" betyder.
> Du måste specificera tills varje steg är odelbart — annars hittar roboten på resten själv.
> Marcus var 14 och tyckte det var löjligt. De studerande är vuxna efter gymnasiet —
> de kommer tycka det är ännu löjligare. Perfekt. Ju löjligare, desto mer minns de det.
> Säg det högt: "Jag var 14 och tyckte min lärare var löjlig. Nu gör jag samma sak."
> Det tar bort skammen av att inte fatta direkt — och de känner igen sig.
> Använd den övningen live — låt de studerande försöka instruera dig som robot.

---

## Hellweek — näst sista veckan i kurs 3

Veckan före tentan i kurs 3 är **Hellweek** — en vecka dedikerad till C#-features som
normalt missas i grundkurser men som dyker upp överallt i professionell kod.

**Viktigt att säga högt:** *"Det här kommer inte på tentan. Det är för er skull."*
Det tar bort pressen och triggar nyfikenhet istället för panik.

Allt som inte hinns med under hellweek smygs in i **clean code**-veckan (kurs 3, vecka 3)
som "så här skriver du det snyggare" — inte som nytt ämne utan som förbättring av befintlig kod.

---

## C# Hidden Gems — måste finnas med innan testkursen

Dessa koncept missas lätt i grundkurser men dyker upp hela tiden i professionell kod
och i AI-genererad kod. Den studerande måste känna igen dem — annars blockeras de av
sin egen okunskap när de läser andras kod. Allt ska vara introducerat senast kurs 3.

| Koncept | Vävs in i | Varför viktigt |
|---------|-----------|---------------|
| Properties (get/set, auto, expression-bodied) | Klasser | Grunden i all OOP-kod |
| Extension methods | LINQ/samlingar | Dyker upp överallt i .NET |
| Operator overloading | Klasser | Ser ut som magi annars (`+` på en klass?) |
| Null coalescing (`??`, `??=`) | Variabler/klasser | Vardagligt i all modern C# |
| Null conditional (`?.`) | Klasser | Förhindrar NullReferenceException |
| Pattern matching (`is`, `switch` expressions) | Villkor | C# 8+ — AI skriver det hela tiden |
| Records | Databaskopplingar/DTOs | Vanligt i immutable data och DTOs |
| Tuples | Metoder | Snabb multi-return utan klass ❤️ |
| Object/collection initializers | Klasser | Renare kod, används överallt |
| String interpolation (`$"..."`) | Variabler | Alltid istället för `+` |
| `yield return` | Samlingar | Förstå vad IEnumerable egentligen gör |
| Named och optional parameters | Metoder | Vanligt i API-design |
| Indexers | Klasser | Gör egna klasser indexerbara som listor |
| `async` / `await` | Databaser/API | Krävs för all modern I/O — cloud-förberedelse |
| `dynamic` | Variabler | Som JavaScripts lösa typning — kan byta typ i runtime |
| `var` vs `dynamic` | Variabler | `var` = compiletyp, `dynamic` = runtime — viktigt att förstå skillnaden |
| Dependency Injection | **Databaskursen** | Krävs för cloud — introduceras senast i kurs 2 |

Dessa introduceras inte som egna lektioner utan **vävs in** i relevanta ämnen.
DI introduceras i databaskursen (kurs 2) — det är ett krav inför cloud.

---

## Språk och terminologi

- Alltid **"studerande"** — aldrig "studerande" eller "studerande"
- All kod skrivs på **engelska**
- Kommentarer och output skrivs på **svenska**
- Terminal = alltid **Git Bash** — aldrig cmd eller PowerShell i exempel

---

## Teknikstack

- Presentationer: **Marp** (markdown → slides)
- Diagram: **Mermaid** (inbäddat i markdown)
- C#: **.NET 10 / C# 14**
- Format: `.md` och `.cs` — inget annat utan särskilt skäl

---

## Marcus kodfilosofi — påminn regelbundet

Marcus sätt är ett sätt — inte det enda sättet. Påminn om det med jämna mellanrum, inte bara en gång. Studerande ska hitta sin egen stil och lära sig anpassa sig till andras i projekt och yrkesliv.

> "Fungerar koden och är den läsbar? Då är det en bra lösning."

---

## Skrivstil

Följ `writing_rules.md` i repots rot. Kortversion:
- Samtalston — prata med läsaren, inte åt dem
- Korta stycken, visuella pauser, emojis sparsamt
- Vardagliga metaforer och analogier
- Avsluta med en känsla, inte bara en summering
- Inga AI-floskler ("Furthermore", "It is important to note" osv.)

---

## Källkontroll

- `.gitignore` alltid innan första commit
- Pre-commit hook blockerar `.env`, nycklar och hemligheter
- Commit-meddelanden beskriver vad och varför — inte hur

---

## Slogans

- **"Koda vilt"** — på kodövningar. Kör igång med fullt engagemang.
- **"Diskutera mera!"** — på diskussions- och tankeövningar (Kassaapparaten, Metropolitan Club m.fl.)

## Återkommande karaktärer

| Karaktär | Roll | Används i |
|----------|------|-----------|
| **Jenny** | Juniorkonsult, nyfiken och modig | Diskutera mera-serien |
| **Kalle** | Kodare, noggrann, jobbar sent | Diskutera mera-serien |
| **Fredrik** | Senior fullstack, kommunicerar dåligt | Diskutera mera-serien |
| **Viktor Viber** | Vibe-kodaren — fixar med AI utan att förstå | AI-dilemma-arken |
| **Birger Bugg** | Personifikationen av buggen som uppstår | AI-dilemma-arken |
| **Copy-Conny** | Kopierar kod utan att förstå den | Sparad för senare övning |

**"Göra en Birger"** = leverera AI-genererad eller otesterad kod som *verkar* fungera men har en katastrofal bugg gömd inuti. Används som klassjargong från kurs 4 och framåt.

> Lärarfrågan som avslöjar det: *"Hur många veckor har ni planerat för testning?"*
> Om svaret är noll — de gör en Birger.

---

*Baserat på Marcus Medinas undervisningsstil och metodik, utvecklad sedan 2009.*
*Uppdatera den här filen när vi hittar bättre sätt att göra saker.*
