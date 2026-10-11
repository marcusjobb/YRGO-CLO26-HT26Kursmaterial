# Reflektion — Slutprojekt

**Namn:** Filippa van der Kolk

**Kurs:** Grundläggande OOP i C#

**Projekt:** The Vampire Castle (Dungeon Crawler)

**Datum:** 2 Oktober 2026

---

## Vad var svårast att lösa?

Det svåraste att lösa tyckte jag var att få alla klasser att samarbeta utan att tappa bort sig i logiken. Jag fastnade flera gånger i hur rummen skulle kopplas ihop, hur NPC-systemet skulle fungera och hur input-hanteringen skulle byggas så att spelet inte kraschade vid fel input. Det tog längre tid än jag trodde att förstå hur `Game`-klassen skulle styra allt, men jag kom vidare genom att bryta ner problemet i mindre delar och testa funktionerna separat. När jag väl insåg hur `Player`, `Room` och `Vampire` hängde ihop blev allt mycket tydligare.

---

## Hur fungerade samarbetet i gruppen?

Samarbetet fungerade väldigt bra! Vi hade regelbundna möten och pratade nästan alltid via Discord där vi tog gemensamma beslut om struktur, funktioner och spelidé. Kodningen i sig skedde främst i Olivers Visual Studio (+ skärmdelning via Discord) men vi satt tillsammans och diskuterade varje del innan vi skrev den. Jag la själv främst till saker via GitHub genom att lägga till `.md`-filer, bilder och visuella idéer med hjälp av Copilot. Vi jobbade inte med branches och pull requests på ett avancerat sätt, utan mer som ett gemensamt utvecklingsflöde där vi hela tiden kommunicerade och tog beslut ihop.

---

## Om du fick göra om det — vad hade du gjort annorlunda?

Om jag fick göra om projektet hade jag velat börja med en ännu tydligare plan för hur alla klasser skulle se ut innan vi började koda, eftersom det nog hade sparat tid senare och gjort att vi slapp ändra strukturen flera gånger.

Jag tyckte om att vi satt i samma kodfil – det hjälpte oss undvika merge-konflikter och gjorde att båda kunde vara med och lära sig alla delar av koden, vilket jag tycker var av vikt eftersom det bara är vår andra inlämning och jag ville kunna förstå hela projektet.

Men i efterhand inser jag även att det hade varit bra att åtminstone testa att jobba i separata branches någon gång, just för att lära oss hantera merges och konflikter. Vi undvek det mest för att det kändes läskigt, men det är en färdighet man kommer behöva i framtiden, så det hade varit bra att prova det i liten skala.

---

## Valfritt — arv och datastrukturer

Vi använde inte arv eftersom våra klasser hade tydliga och separata ansvar. Däremot använde vi `List<T>` på flera ställen: för `Item` och `NPC` i `Room`, för inventory i `Player` och för dialoger i `NPC`-klassen. `List<T>` passade bra eftersom antalet objekt varierar och vi behövde kunna lägga till och ta bort element dynamiskt.

---

## VG — Motivering

### Dungeon Crawler

*Vilka VG-utbyggnader valde ni och varför? Motivera era datastrukturval (`string` som nyckel, `List<string>` för dialog, valt sparformat). Vad hade en annan struktur gett er?*

Vi valde NPC-utbyggnaden. Varje spelrum har en vampyr som ställer en mattefråga, och spelaren måste svara rätt för att få en nyckel och gå vidare. I sista spelrummet får man även en antidot som behövs för att rädda vännen och därmed vinna spelet. Vi valde detta eftersom det gav spelet en tydlig progression och ett moment av "risk" – fel svar betyder att du inte kan gå vidare. Vi la även till vanliga NPC:er (`Ghost` och `Prisoner`) för att skapa mer atmosfär och interaktion.

Vi använde `Dictionary<string, Room>` för utgångar eftersom det gör navigationen flexibel och lätt att utöka. Vi använde `List<Item>` för både rumsföremål och spelarens inventory eftersom antalet items varierar och `List<T>` gör det enkelt att lägga till och ta bort. För dialog använde vi `List<string>` eftersom NPC:erna ska kunna gå igenom sina repliker i ordning och loopa tillbaka när de når slutet. Vi valde att använda `string` som nyckel för riktningar (`north`, `south`) eftersom det är lätt att läsa och lätt att tolka i input-systemet.

Ett alternativ hade varit att använda `enum` för riktningar eller arv för NPC-typer, men jag anser att vår lösning blev enklare och tydligare, samt att den bättre passade projektets omfattning.
