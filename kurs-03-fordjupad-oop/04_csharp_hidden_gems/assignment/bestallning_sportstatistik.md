# Beställning: Sportstatistik

**Till:** grupp, Inlämning 2 (grupparbete)
**Från:** S/K Ummis AB, Carlos Ash, beställare
**Typ:** kursuppgift. Företag och beställare är fiktiva.
**Bilaga:** [Inlämning 2: grupparbete](inlamning_2_grupparbete.md) (regler, inlämning, granskning, G- och VG-delen i reflektionen)

> Märk: **[G]** = krav för G. Övrigt är gemensamma krav. VG bedöms i reflektionen och i peer review, enligt uppgiften, inte i den här beställningen.

---

## 1. Uppgift

S/K Ummis AB sponsrar en liten korpserie i fotboll och vill ha ett enkelt program för den. Man ska kunna registrera lag, spelare och matcher, se seriens tabell för en säsong, och få en varning när ett lag närmar sig truppgränsen.

Vi ska använda programmet i serien, på riktigt, så det får gärna kännas som något som funkar, inte som en tentaövning.

## 2. Kravspec

### 2.1 Grundkrav

- Programmet körs i konsol, Blazor eller MVC. Ni väljer. **[G]**
- **Databasen väljer ni själva.** Motivera valet i reflektionen. ADO.NET är inte tillåtet. **[G]**
- Programmet skapar databasen och seedar den med exempeldata vid första start, om den är tom. **[G]**
- Connectionstring läses från user-secrets, under nyckeln `DB_CONNECTION`. Den finns aldrig i koden eller i repot. **[G]**
- Varje match har datum, hemmalag, bortalag, mål hemma, mål borta och en valfri anteckning. **[G]**
- Ett **Repository** står mellan programmet och databasen. **[G]**
- Minst ett av mönstren **Factory**, **Builder** eller **Observer** används. **[G]**
- Programmet är uppdelat i klasser med tydliga ansvar (SRP), och har minst ett interface. **[G]**
- Felaktig inmatning ger ett tydligt meddelande, inte en krasch. Ogiltigt antal mål, okänt datum och ett lag som möter sig självt ska hanteras. **[G]**

### 2.2 Menyer och minimal modell

Projektmappen (`Sportstatistik-Konsol`, `Sportstatistik-Mvc` eller `Sportstatistik-Blazor`) har en `README.md` med de menyval och den minimala modell som ni måste bygga ut. Det som står där är **minimum**. Ni lägger till fält och menyval för att uppfylla önskemålen nedan. Medlemmarna i README är den datamodell Carlos skickade från sitt gamla Excel-ark.

### 2.3 Funktioner

1. **Lag och spelare.** Ett lag har ett namn och spelare. En spelare har ett namn (förnamn räcker) och en position.
2. **Matcher.** Lägg till, visa och ta bort en match.
3. **Positioner.** Standardlistan är: Målvakt, Back, Mittfält, Forward, Övrigt. Användaren kan lägga till egna. **[G]**
4. **Tabell för en säsong.** Spelade matcher, vinster, oavgjorda, förluster, gjorda och insläppta mål, målskillnad och poäng för vald säsong. Vinst ger 3 poäng, oavgjort 1 och förlust 0. **[G]**
5. **Truppgräns per lag.** Sätt ett största antal spelare per lag och säsong. **[G]**
6. **Varning vid truppgränsen.** När ett lag når 80 % av truppgränsen ska användaren få ett meddelande, och när det når 100 % ett annat. **[G]**
7. **Sortering.** Tabellen sorteras på poäng, sedan målskillnad, sedan gjorda mål, sedan lagnamn. **[G]**

### 2.4 Önskemål från beställaren

Carlos har några specifika önskemål. De är inte svåra, men de ska synas i programmet.

**Önskemål 1: Resultat och målskillnad som går att läsa [G]**
Ett resultat visas som `2–1`. Målskillnad visas alltid med tecken, till exempel `+3`, `-2` och `0`.

**Önskemål 2: Säsongen följer kalenderåret [G]**
En säsong börjar den 1 januari och slutar den 31 december. En match den 31 december hör till den säsongen, även om användaren skriver in den den 2 januari.

**Önskemål 3: Positionen "Övrigt" är alltid kvar [G]**
Om en användare tar bort en egen position som har spelare ska spelarna flyttas till "Övrigt". De får inte försvinna.

**Önskemål 4: Varningen får inte spamma [G]**
Om laget redan har varnats för 80 % ska användaren inte få samma varning igen förrän säsongen byts.

**Önskemål 5: Ta bort en match, med bekräftelse [G]**
Innan en match tas bort frågar programmet "Är du säker? (j/n)". Svaret "n" lämnar allt oförändrat.

## 3. User stories (Gherkin)

Carlos vill att följande beteenden finns. Scenarierna är exempel. Ni kan lägga till fler.

### Story 1: Registrera en match [G]

```gherkin
Feature: Registrera en match

  Scenario: Hemmalaget vinner
    Given lagen "Ummis IF" och "Bryggeriet BK" finns
    When användaren registrerar matchen "Ummis IF" mot "Bryggeriet BK" den 12 november 2026 med resultatet 2–1
    Then matchen sparas i databasen
    And tabellen för säsong 2026 visar "Ummis IF" med 3 poäng och målskillnad "+1"
    And tabellen visar "Bryggeriet BK" med 0 poäng och målskillnad "-1"
```

### Story 2: Säsongens gränser [G]

```gherkin
Feature: Säsongens gränser

  Scenario: En match den sista dagen hör till rätt säsong
    Given lagen "Ummis IF" och "Bryggeriet BK" finns
    When användaren registrerar en match den 31 december 2026
    And användaren ber om tabellen för säsong 2026
    Then matchen räknas i säsong 2026
    And matchen räknas inte i säsong 2027
```

### Story 3: Varning vid 80 % [G]

```gherkin
Feature: Varning vid truppgräns

  Scenario: Användaren får en varning vid 80 %
    Given laget "Ummis IF" har truppgränsen 20 spelare för säsong 2026
    And laget har 15 spelare
    When användaren lägger till en spelare i "Ummis IF"
    Then användaren får meddelandet "Ummis IF är på 80 % av truppgränsen"

  Scenario: Samma varning visas inte två gånger
    Given användaren redan har fått varningen för "Ummis IF" i säsong 2026
    When användaren lägger till en spelare i "Ummis IF"
    Then ingen ny varning visas
```

### Story 4: Ta bort en position utan att förlora spelare [G]

```gherkin
Feature: Ta bort position

  Scenario: Spelare flyttas till Övrigt
    Given positionen "Libero" finns med två spelare
    When användaren tar bort positionen "Libero"
    Then de två spelarna finns kvar
    And de har positionen "Övrigt"
```

### Story 5: Bekräfta borttagning [G]

```gherkin
Feature: Bekräfta borttagning

  Scenario: Användaren avbryter
    Given en match finns
    When användaren väljer att ta bort den
    And användaren svarar "n" på frågan "Är du säker?"
    Then matchen finns kvar
```

### Story 6: Felaktig inmatning [G]

```gherkin
Feature: Felaktig inmatning

  Scenario: Ogiltigt antal mål
    When användaren försöker registrera en match där hemmalaget gjort "abc" mål
    Then programmet visar ett felmeddelande
    And ingen match sparas

  Scenario: Ett lag kan inte möta sig självt
    When användaren försöker registrera matchen "Ummis IF" mot "Ummis IF"
    Then programmet visar ett felmeddelande
    And ingen match sparas
```

## 4. Deadline

| Datum | Vad |
|---|---|
| Måndag 23 nov | Uppgiften delas ut |
| Onsdag 2 dec | Presentation (grupp, även om koden inte är klar) |
| Fredag 4 dec 23:59 | Peer review klar |
| Söndag 6 dec 23:59 | Inlämning (zip av projektet, reflektioner privat) |

Presentationen är inte en deadline för klar kod. Ni ska visa vad ni har, vad som fungerar och vad som återstår.

## 5. Definition of Done

Ett arbete är klart när alla punkter nedan stämmer:

- [ ] Programmet startar med `dotnet run` enligt README.
- [ ] Vid första start skapas databasen och seedas själv. Inga manuella steg utöver user-secrets.
- [ ] Alla [G]-önskemål och G-funktioner fungerar. Testat för hand, med minst ett scenario per Story.
- [ ] Data finns kvar efter att programmet stängts (gäller inte om ni valt en minnesdatabas).
- [ ] Ingen connectionstring finns i koden.
- [ ] Ingen riktig persondata finns i databasen. Använd påhittade namn och resultat.
- [ ] Avsnittet "Fält och data" i README är ifyllt, med motivering för varje fält.
- [ ] Repository, minst ett designmönster och minst ett interface finns, och det går att peka ut dem i koden.
- [ ] UML-klassdiagram (Mermaid) i README.
- [ ] Refactoring-logg med minst tre före/efter-exempel i README.
- [ ] `GRANSKNINGSNOTIS.md` är skriven och länkad från README.
- [ ] `GRUPP.md` är ifylld, med länk till kanban och bekräftad inbjudan till `marcusjobb`.
- [ ] AI-deklaration finns i README.
- [ ] Git: minst tre commits.
- [ ] Peer review är klar, och `review-feedback.md` är pushad.
- [ ] Zippen heter `<förnamn>-<repo-namn>.zip`, och varje person har lämnat in sin egen reflektion, privat, som `<förnamn>-reflektion.md`.

## 6. G- och VG-krav

### G-krav

1. Programmet körs i konsol, Blazor eller MVC.
2. Databasen är vald och motiverad i reflektionen. ADO.NET används inte.
3. Programmet skapar och seedar databasen vid första start.
4. Connectionstring från user-secrets (`DB_CONNECTION`).
5. Matcher har datum, hemmalag, bortalag, mål och anteckning.
6. Repository finns.
7. Minst ett av Factory, Builder eller Observer finns.
8. Klasser med tydliga ansvar och minst ett interface.
9. Felaktig inmatning hanteras.
10. Positioner: standardlistan och egna positioner.
11. Tabell för en säsong, sorterad enligt funktion 7.
12. Truppgräns per lag, och varningar vid 80 % och 100 % (Story 3).
13. Önskemål 1 till 5.
14. Refactoring-logg, granskningsnotis, UML och peer review.
15. Avsnittet "Fält och data" i README: varje fält i datamodellen är utvärderat och motiverat.

### VG-krav

VG-kraven i uppgiften gäller resonemanget, inte antalet funktioner. Se [Inlämning 2](inlamning_2_grupparbete.md), Del 4 och Del 5:

- Designmönster nyanserat diskuterade.
- Arkitektur och alternativa arkitekturer, med konsekvenser.
- Alternativ och beslut i den egna koden och i kodgranskningen.
- Dokumentation och struktur enligt clean code, med reflektion över val och alternativ.

---

Lycka till. Vi ses på presentationen.

**C.Ash**
Carlos Ash, beställare, S/K Ummis AB

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
