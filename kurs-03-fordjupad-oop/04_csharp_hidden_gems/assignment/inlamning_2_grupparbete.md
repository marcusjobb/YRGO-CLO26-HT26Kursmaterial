# Inlämning 2: Grupparbete

**Grupp om två. Projektet lämnas in av en person. Reflektionen lämnas in individuellt och privat.**

---

## Syfte

Ni bygger ett litet, modulärt program som använder en databas, och dokumenterar era val.
Uppgiften täcker kursens mål om databaskopplingar, designmönster, UML, refactoring, kodgranskning och reflektion.

## Tidsplan

- **Uppgiften delas ut:** måndag 23 nov
- **Presentation:** onsdag 2 dec. Ni visar projektet och svarar på frågor. Kan du inte vara med, skriver du om det i din reflektion.
- **Peer review klar:** fredag 4 dec 23:59
- **Inlämning:** söndag 6 dec 23:59

---

## Beställningar

Alla fyra projekt har samma beställare och samma typ av krav. Läs beställningen för det projekt ni väljer:

- [Hushållsbudget](bestallning_hushallsbudget.md)
- [Träningsbok](bestallning_traningsbok.md)
- [Sportstatistik](bestallning_sportstatistik.md)
- [Recept](bestallning_recept.md)

---

## Så börjar ni

Länken till startrepot står i Classroom-inlägget för Inlämning 2. Gör så här, i den här ordningen:

1. **Välj ett projekt och ett gränssnitt.** Det finns fyra projekt (Hushållsbudget, Träningsbok, Sportstatistik och Recept) i tre gränssnitt (konsol, MVC och Blazor). Det är tolv mallar. Behåll en, och radera de elva andra, både mappen och raden i `Projekt.slnx`.
2. **Forka repot.** Klicka på **Fork** uppe till höger på repots GitHub-sida och välj ert eget konto. Klona sedan er fork, inte originalrepot.
3. **Bjud in `marcusjobb`** som medarbetare: **Settings → Collaborators → Add people**.
4. **Sätt upp en kanban** för gruppen, i Trello eller GitHub Projects. Bjud in läraren så att hen kan se den: i GitHub Projects med `marcusjobb`, i Trello med det namn eller den e-postadress som står i Classroom. Kanban ska ha aktivitet under hela perioden, inte bara i slutet.
5. **Fyll i `GRUPP.md`** i repots rot: gruppens namn, medlemmar, valt projekt, valt databas, länken till kanban och era gruppregler.

---

## Vad lämnas in

| Vad | Hur | Vem |
|---|---|---|
| Projektet (zip från er fork, på branchen `main`, omdöpt till `<förnamn>-<repo-namn>.zip`) | Classroom, en zip per grupp | En person i gruppen |
| `GRUPP.md` ifylld | Ingår i zippen | En person i gruppen |
| `review-feedback.md` från granskningen | Pushad till den granskade gruppens repo | Granskaren |
| `peer_review_log.md` (ifylld) | Ingår i zippen | En person i gruppen, men alla i gruppen är ansvariga för innehållet |
| Reflektion (`<förnamn>-reflektion.md`) | Classroom, **privat** | Varje person för sig |

**Zippens namn:** Zippen heter `<förnamn>-<repo-namn>.zip`. Förnamnet är ditt, i gemener utan å, ä och ö. Repo-namnet är namnet på er fork på GitHub. GitHub döper filen till `<repo-namn>-main.zip`, så byt namn på den innan du lämnar in. Exempel: `marcus-clo26-inlamning-2-start.zip`.

**Reflektionen ligger inte i repot.** Den skrivs i en egen fil, utifrån `reflektion_mall.md`. Namnet är ditt förnamn i gemener utan å, ä och ö, till exempel `marcus-reflektion.md`. Överst i filen skriver du ditt namn, din grupp och datum.

---

## Del 1: Program (G)

### Val som ni gör själva

- **Gränssnitt:** konsol, Blazor eller MVC.
- **Databas:** fritt val: SQLite, MySQL, SQL Server, PostgreSQL, MongoDB eller en minnesdatabas (till exempel EF Core InMemory). Rena JSON- eller textfiler räknas inte som databas. Vill ni använda något annat, fråga läraren först. Ni motiverar valet i reflektionen (se Del 4).
- **"Det funkar hos mig" räknas inte.** Läraren kör er kod på en annan dator, mot en tom databas som läraren startar själv. Er kod måste bygga hela databasen och lägga in exempeldata själv, oavsett vilken databas ni valt.
- **Databastypen ska vara en annan än i Inlämning 1.** Kursen vill att ni har arbetat med fildatabas, minnesdatabas och databasserver. Använde ni en fildatabas (till exempel SQLite) i Inlämning 1, väljer ni nu en databasserver (till exempel MongoDB). Använde ni en databasserver (till exempel LocalDB), väljer ni nu en fildatabas. Har ni använt olika typer i Inlämning 1, kontakta läraren.
- **Ramverk för databaskopplingen:** EF Core eller MongoDB.Driver. **ADO.NET är inte tillåtet**, och det gäller även bibliotek som bygger direkt på det (till exempel `Microsoft.Data.Sqlite`, `MySqlConnector`, `SqlClient` och Dapper). Vill ni använda något annat, fråga läraren först.

### Krav på programmet

- **Menyer och modeller:** varje projektmapp har en `README.md` med de menyval och den minimala modell som ni **måste** bygga ut. Ni får lägga till fler menyval och fält.
- **Programmet bygger databasen själv.** Vid första start skapar programmet databas och tabeller/collections, och lägger in exempeldata (seed) om databasen är tom. Väljer ni en minnesdatabas görs det vid varje start. Ingen manuell SQL eller ny migrering av hand för att komma igång.
- **Connectionstring läses från user-secrets, under nyckeln `DB_CONNECTION`.** Den finns aldrig i koden, i repot eller i zippen. Läraren kör er kod mot sin egen Docker-server och sätter connectionstringen själv. Ni lämnar ingen connectionstring, så er kod måste bygga allt själv.
- **Repository** finns (obligatoriskt). Gränssnittet och menyn känner bara till repositoryt.
- **Minst ett** av mönstren **Factory**, **Builder** eller **Observer** används, och det ska gå att peka ut i koden.
- **Klasser med tydliga ansvar** (SRP), och **minst ett interface**.
- **Er egen kod räknas.** Startrepot innehåller ett färdigt gränssnitt, en låtsasdatabas (`InMemory…Repository`) och några interface. Det som fanns från början räknas inte när ni pekar ut Repository, designmönster och interface. Det ska finnas i kod som ni själva har skrivit.
- **Språk i koden:** namn på klasser, metoder, variabler och tabeller eller collections är på **engelska**. Utskrifter och text till användaren är på **svenska**, och kommentarerna är på **svenska**.
- **Underhållbar, återanvändbar och välkommenterad kod:** ingen kopierad logik (DRY), tydliga namn, och kommentarer på svenska som förklarar *varför*, inte vad.
- **Fält och data:** utvärdera fälten i den datamodell ni fått. Vilka behöver programmet, och vilka behövs inte? Skriv en tabell i `README.md` (avsnittet "Fält och data") med varje fält, om ni behåller eller tar bort det, och varför.
- **Felaktig inmatning** ger ett meddelande, inte en krasch.
- **Git:** minst tre commits. `bin/`, `obj/` och `.vs/` ligger inte i repot.

---

## Del 2: UML och gränssnittsplan (G)

**Gränssnittsplan:** en enkel skiss eller ett flöde över menyer eller sidor i `README.md` (ASCII, bild eller Mermaid-flödesdiagram). Den ska visa vad användaren gör, utifrån beställningen.

**UML:** ett klassdiagram i `README.md` (Mermaid) över era entiteter, egenskaper och relationer.
Skriv en kort förklaring: vad är en klass, vad är en relation, och varför ser diagrammet ut som det gör?

## Del 3: Refactoring och granskning (G)

Skriv en **refactoring-logg** i `README.md`:

- Peka ut **minst tre** saker i er egen kod som ni förbättrat (före/efter-kodutdrag)
- För varje: vad var problemet, vad ändrade ni, varför blev det bättre?

Skriv en **granskningsnotis** i `GRANSKNINGSNOTIS.md`, länkad från README.
Välj ett område i er egen kod som ni tycker är svagt. Förklara vad som behöver ändras och varför ni ännu inte ändrat det (eller vad ni gjort).

---

## Del 4: Resonemang och val (G och VG)

Reflektionen är **individuell**. Alla i gruppen svarar på alla frågor, med egna ord, i sin egen fil.

### G-delen i reflektionen

- **Databasval:** vilken databas ni valde, varför, och vad som skiljer en fildatabas, en minnesdatabas och en databasserver åt. Nämn minst en annan situation där ni hade valt en annan typ av databas, och varför.
- **Designmönster:** vilka mönster ni använde, och vilket problem vart och ett löser i er kod.
- **Fält och data:** vilka fält du behöll och tog bort, och vilka regler och principer för personuppgifter som ligger bakom dina val, med egna ord och kopplade till konkreta fält i er app.

### VG-delen i reflektionen

- **Designmönster (nyanserat):** vad är styrkor och svagheter med mönstren ni använde? När hade ni valt bort dem?
- **Arkitektur:** resonera kring er valda arkitektur och minst en alternativ arkitektur. Vilka konsekvenser får ert val, och varför valde ni det?
- **Beslut och alternativ:** för varje stort beslut i koden, vilka alternativ fanns och varför föll de bort?
- **Kodgranskning:** vilka beslut tog ni i er egen granskning och i peer review? Vilka konsekvenser fick de?
- **Dokumentation och struktur:** hur hänger kodens namn, struktur och dokumentation ihop med clean code? Vad skulle ni göra annorlunda med mer tid?

### AI-stöd (G, i reflektionen)

- Hur använde ni AI, och vilken del var er egen?
- Vilket förslag från AI sa du nej till, och varför?
- Hur kontrollerade du att AI:ns påståenden stämde?

Om du inte använde AI: skriv det, och hoppa över frågorna ovan. Att köra kodkontrollen i startrepot räknas som AI-stöd och ska stå i deklarationen.

**AI-deklaration** ska finnas i `README.md` (tabell med verktyg, berörda delar och vad som ändrats). Har ingen AI använts: skriv det.

---

## Del 5: Peer review (F6, C3)

**Granskningen gör en annan grupp.** Ni väljer själva vilken grupp som granskar er. Två grupper granskar inte varandra. Granskaren ska inte vara medlem i den granskade gruppen.

- **Bjud in** granskaren till ert repo.
- **Testa själva först.** All testning ni gör, inklusive det som krånglar, loggas i `peer_review_log.md` i ert repo.
- **Loggen följer med granskningen.** Granskaren skriver in det hen hittar i samma fil. Ni skriver in hur varje sak lösts. Vi vill se hela kedjan: vad som hände, vad som ändrades och vem som kontrollerade.
- Granskaren **kör code review** och **kommenterar** koden.
- Granskaren **pushar** `review-feedback.md` till den granskades repo.
- Den granskade gör de ändringar som begärs och **godkänner** granskningen.
- **Hittar ni ingen granskare, eller levererar inte granskaren?** Kontakta läraren i god tid. Det är en sista utväg, inte det normala.
- Läraren verifierar att de begärda ändringarna är gjorda innan bedömning.

**Vänta inte till deadline.** Granska så snart ni kan. Fredag 4 dec 23:59 är sista dag, inte måldag.

### Checklista för granskaren

1. **Körbarhet:** bygger och kör programmet utan fel. Stämmer README?
2. **Varningar:** finns varningar i build-utskriften eller i editorn? Vilka, och varför?
3. **Clean code:** namngivning, metodlängd, SRP, magic numbers, dubbelkod
4. **Struktur:** har klasser och interface tydliga ansvar? Är designmönstret motiverat?
5. **Databas:** fungerar kopplingen? Finns felhantering där fil, JSON eller databas kan fallera? Finns en motivering i README för varje fält i datamodellen?
6. **Förståelse:** kan du förklara koden för någon annan efter en genomläsning?
7. **Git:** meningsfulla commits, minst tre.
8. **Kommentarer:** på svenska, förklarar varför snarare än vad.

### Mall: `review-feedback.md`

```markdown
# Peer review

**Reviewed by:** [förnamn efternamn eller initialer]
**Approved by:** [den granskade gruppens namn, fylls i när de godkänt]
**Granskad grupp:** [gruppnamn]

## Styrkor (tre, med exempel från koden)

1.
2.
3.

## Förbättringsförslag (tre, med konkret förslag)

1.
2.
3.

## Beslut

- [ ] Godkänd
- [ ] Ändring begärd (punktlista ovan)
```

**Obs:** granskaren sätter inte betyg. Läraren bedömer.

---

## Bedömning

- **Betyget sätts per person.** Koden är gemensam, men varje person ska visa sin egen måluppfyllelse genom reflektionen och sina bidrag i git.
- **G** kräver att alla G-krav i Del 1 till Del 5 är uppfyllda, och att lärandemålen som examineras i inlämningen är uppnådda.
- **VG** kräver att G är uppfyllt, och att VG-kriterierna i Del 4 och Del 5 är uppfyllda. VG bedöms enbart i inlämningsuppgifterna.
- Tentan examinerar bara G-nivå.
