# Rapport — Slutprojekt

**Kurs:** Grundläggande OOP i C#

**Projekt:** The Vampire Castle (Dungeon Crawler)

**Grupp:** Oliver Ågren & Filippa van der Kolk

**Datum:** 2 Oktober 2026

**GitHub:** @oliver-agg & @filippa-kolk

**Commit-hash vid inlämning:** 7c82ae4

---

## Gruppmedlemmar

| Namn | Lämnar in rapport? |
|------|---------------------|
| Oliver Ågren | ✅ Ja (den här personen — lämnar in Zip + `Report.md` + `OliverReflection.md`) |
| Filippa van der Kolk | ❌ Nej (lämnar in endast `FilippaReflection.md`) |

---

## G — Godkänt

### Klasserna
*Vilka klasser skapade ni och vad ansvarar var och en för? Hur använde ni privata fält, properties och konstruktorer?*

Vi skapade sju klasser: `Game`, `Item`, `NPC`, `Player`, `Program`, `Room` och `Vampire`.

`Game` ansvarar för hela spelets logik, att skapa spelvärlden, hantera kommandon och driva spelloopen. `Item` representerar föremål som nycklar och antidoten, med namn och beskrivningar. `NPC` håller dialogrepliker och kan ge nästa replik vid interaktion. `Player` håller reda på vilket rum spelaren befinner sig och deras inventory, samt metoder för att plocka upp föremål. `Program` innehåller `Main`-metoden som startar spelet. `Room` representerar varje rum i slottet och innehåller namn, beskrivning, utgångar, föremål och NPC:er. `Vampire` representerar mattefråge-vampyrerna och hanterar frågor + korrekt svar.

Vi använde privata fält och properties för att skydda data och konstruktorer för att initiera alla objekt på ett tydligt sätt.

### Arv och List&lt;T&gt;
*Var använde ni `List<T>`, och vad innehåller den? Använde ni arv — i så fall hur ser hierarkin ut och varför?*

Vi använde `List<T>` på flera ställen:
- `Room` har en lista med `Item` och en lista med `NPC`.
- `Player` har en lista med `Item` i sitt inventory.

`List<T>` valdes eftersom antalet objekt kan variera och vi behöver kunna lägga till och ta bort element dynamiskt. Vi använde oss aldrig av arv, då våra klasser hade separata och tydliga ansvar och det fanns ingen naturlig hierarki som motiverade arv.

### Spelloopen
*Hur är spelets huvudloop uppbyggd? Hur hanterar ni användarens input och felaktig input?*

Spelets huvudloop ligger i `Game.Start()` och körs så länge variabeln `_playing` är `true`.

I varje varv visas rummet, eventuella vampyrfrågor hanteras, vinstvillkoret kontrolleras och sedan läses spelarens input.

Input tolkas av `InterpretCommand()`, som avgör vilket kommando spelaren försöker använda och anropar rätt metod. Felaktig input hanteras genom att vi skriver ut ett felmeddelande och låter spelaren försöka igen.

### UML
*Lägg in ert klassdiagram som ni ritade innan ni började koda. Skiljer sig slutresultatet från planen — hur och varför?*

Vi ritade både en flödeskarta och en ASCII-karta (vänligen se `FlowChart.png` och `ASCII-Chart.md` i GitHub) innan vi började koda, som visade relationerna mellan `Game`, `Player`, `Room`, `Item`, `NPC` och `Vampire`.

Slutresultatet blev nästan som planen, men vi lade till vissa fält (som `Guard` och `GuardDefeated` i `Room`) när vi insåg att vi behövde hantera vampyrfrågor direkt i rummet. Vi lade också till en separat NPC-lista i `Room` för vanliga NPC:er, eftersom vi ville ha både dialog-NPC:er och mattefråge-vampyrer.

### Git
*Hur jobbade ni med Git? Branches, pull requests, vem gjorde vad?*

Vi arbetade tillsammans genom regelbundna möten under projektets gång och samarbetade främst via Discord-samtal där vi gemensamt bestämde vad som skulle skrivas ut i kod och hur spelet skulle fungera.

Den faktiska kodningen skedde huvudsakligen i Olivers Visual Studio-miljö, där vi satt tillsammans och tog beslut om struktur, logik och funktioner. Detta för att båda skulle kunna tycka och tänka om beslut, undvika merge-konflikter, men framförallt för att båda fortsatt skulle kunna lära sig alla delar (fortsatt användning av det vi känner oss trygga i och lära oss nya saker) av projektet.

Vid sidan av detta använde Filippa även GitHub för att lägga till kompletterande filer, som `.md`-dokument och bilder/visualisering av speliden med hjälp av Copilot.

Git användes alltså främst för versionlagring och dokumentation, medan kodningen skedde i ett gemensamt utvecklingsflöde där båda deltog i skapandet av spelet och designbeslut.

**Filippas `git log --oneline`:**

```
264af37 (HEAD -> main, origin/main, origin/HEAD) Update reflection document with project details
2279e2d Format and expand reflection document sections
76dac0b Create project report for The Vampire Castle
2316ce7 Create reflection template for final project
87544ca Create FilippaReflection.md for project insights
01ef38e Create FilippaReflection.md
d40e90f Create OliverReflection.md
5f00643 Create Report.md
b9f20d0 made a mistake w .md
4812a1d added .md files for report and reflektions
54334d4 update room and vampire descriptions
c625c67 Merge branch 'main' of https://github.com/Oliver-agg/The-Vampire-Castle # Please enter a commit message to explain why this merge is necessary, # especially if it merges an updated upstream into a topic branch. # # Lines starting with '#' will be ignored, and an empty message aborts # the commit.
28a945f Changes to all classes and main
b2b0fbe Fix formatting inconsistencies in ASCII Chart
5c21493 Add files via upload
d1371ab Created class for Rewards
b2e1ec4 Added classes Rewards and Rooms
058f279 Rename Flow chart Image 23 sep. 2026 16_20_27.png to FlowChart.png
7353378 Rename ChatGPT Image 23 sep. 2026 16_20_27.png to Flow chart Image 23 sep. 2026 16_20_27.png
34b374b Add files via upload
bd942ec Delete TheVampireCastleGameChart.png
e376502 Update and rename Game concept.md to GameConcept.md
7c82ae4 Add files via upload
```

**Olivers `git log --oneline`:**

```
342e759 Added visuals to the game
264af37 Update reflection document with project details
2279e2d Format and expand reflection document sections
76dac0b Create project report for The Vampire Castle
2316ce7 Create reflection template for final project
87544ca Create FilippaReflection.md for project insights
01ef38e Create FilippaReflection.md
d40e90f Create OliverReflection.md
5f00643 Create Report.md
b9f20d0 made a mistake w .md
4812a1d added .md files for report and reflektions
54334d4 update room and vampire descriptions
c625c67 Merge branch 'main' of https://github.com/Oliver-agg/The-Vampire-Castle # Please enter a commit message to explain why this merge is necessary, # especially if it merges an updated upstream into a topic branch. # # Lines starting with '#' will be ignored, and an empty message aborts # the commit.
28a945f Changes to all classes and main
b2b0fbe Fix formatting inconsistencies in ASCII Chart
5c21493 Add files via upload
d1371ab Created class for Rewards
b2e1ec4 Added classes Rewards and Rooms
058f279 Rename Flow chart Image 23 sep. 2026 16_20_27.png to FlowChart.png
```

### Kodkvalitet
*Nämn ett exempel på ett bra namn och en kommentar ni skrev som förklarar varför, inte vad.*

Ett exempel på ett bra namn är `GuardDefeated`, eftersom det tydligt beskriver varför den finns — att markera om vampyren i rummet är besegrad istället för att bara beskriva vad den är.

En bra kommentar vi skrev förklarade varför vi kontrollerar vinstvillkoret direkt efter varje kommando, för att undvika att spelaren kan fortsätta spela trots att spelet egentligen skulle kunna vara avslutat.

---

## VG — Motivering: Vad vi lade till & varför vi löste det såhär

Vi valde VG-delen med NPC-utbyggnad.

Varje spelrum (förutom `Hallway` och `Rescue Room`) har en vampyr-NPC som ställer en mattefråga. Spelaren måste besvara frågan korrekt för att få en nyckel och kunna fortsätta till nästa rum.

I det sista spelrummet får spelaren både en nyckel och en antidot som behövs för att rädda vännen, och därmed vinna spelet. Vi la även till vanliga NPC:er (`Ghost` & `Prisoner`) som ger dialog och atmosfär, vilket utökar spelets interaktivitet.

Vi valde att implementera NPC-systemet med en separat `Vampire`-klass för mattefrågor och en `NPC`-klass för dialog. Det gav en tydlig separation mellan funktionella NPC:er, som påverkar spelmekaniken, och atmosfäriska NPC:er.

Vi använde:
- `List<NPC>` i `Room` för att kunna ha flera NPC:er i samma rum.
- `Dictionary<string, Room>` för utgångar, eftersom det gör navigationen flexibel och lätt att utöka.
- Item-listor i både `Room` och `Player` för att hantera rewards och inventory dynamiskt.

Alternativet hade varit att koda varje rum med fasta fält, medan `Dictionary` gav en mycket renare, tydligare och skalbar lösning. Vi valde även att låta `Game`-klassen hantera all input och spelstyrning, vilket gör koden lättare att följa och utöka.
