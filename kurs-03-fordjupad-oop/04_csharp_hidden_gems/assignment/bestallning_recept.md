# Beställning: Recept

**Till:** grupp, Inlämning 2 (grupparbete)
**Från:** S/K Ummis AB, Carlos Ash, beställare
**Typ:** kursuppgift. Företag och beställare är fiktiva.
**Bilaga:** [Inlämning 2: grupparbete](inlamning_2_grupparbete.md) (regler, inlämning, granskning, G- och VG-delen i reflektionen)

> Märk: **[G]** = krav för G. Övrigt är gemensamma krav. VG bedöms i reflektionen och i peer review, enligt uppgiften, inte i den här beställningen.

---

## 1. Uppgift

S/K Ummis AB vill ha ett enkelt receptprogram. Ett hushåll ska kunna spara recept med ingredienser, planera veckans måltider, få en inköpslista för veckan, och få en varning när en receptkategori används för ofta under en vecka.

Vi ska använda programmet hemma, i vardagen, så det får gärna kännas som något som funkar, inte som en tentaövning.

## 2. Kravspec

### 2.1 Grundkrav

- Programmet körs i konsol, Blazor eller MVC. Ni väljer. **[G]**
- **Databasen väljer ni själva.** Motivera valet i reflektionen. ADO.NET är inte tillåtet. **[G]**
- Programmet skapar databasen och seedar den med exempeldata vid första start, om den är tom. **[G]**
- Connectionstring läses från user-secrets, under nyckeln `DB_CONNECTION`. Den finns aldrig i koden eller i repot. **[G]**
- Varje recept har namn, antal portioner, kategori, ingredienser och en valfri anteckning. Varje ingrediens har namn, mängd och enhet. **[G]**
- Ett **Repository** står mellan programmet och databasen. **[G]**
- Minst ett av mönstren **Factory**, **Builder** eller **Observer** används. **[G]**
- Programmet är uppdelat i klasser med tydliga ansvar (SRP), och har minst ett interface. **[G]**
- Felaktig inmatning ger ett tydligt meddelande, inte en krasch. Ogiltig mängd, okänt datum och tom kategori ska hanteras. **[G]**

### 2.2 Menyer och minimal modell

Projektmappen (`Recept-Konsol`, `Recept-Mvc` eller `Recept-Blazor`) har en `README.md` med de menyval och den minimala modell som ni måste bygga ut. Det som står där är **minimum**. Ni lägger till fält och menyval för att uppfylla önskemålen nedan. Medlemmarna i README är den datamodell Carlos skickade från sitt gamla Excel-ark.

### 2.3 Funktioner

1. **Kockar.** En kock är den som har skapat receptet, och har ett namn (förnamn räcker). Programmet ska klara 1–5 kockar.
2. **Recept.** Lägg till, visa och ta bort ett recept. Sök recept på ingrediens.
3. **Kategorier.** Standardlistan är: Frukost, Lunch, Middag, Efterrätt, Bakning, Övrigt. Användaren kan lägga till egna. **[G]**
4. **Veckoplan och inköpslista.** Koppla recept till dagar i en vecka. Visa veckoplanen för vald vecka, och en inköpslista där samma ingrediens med samma enhet är summerad. **[G]**
5. **Gräns per kategori.** Sätt hur många gånger per vecka en kategori högst får förekomma i veckoplanen, till exempel Efterrätt högst 2 gånger. **[G]**
6. **Varning vid gränsen.** När en kategori når 80 % av gränsen ska användaren få ett meddelande, och när den når 100 % ett annat. **[G]**
7. **Portioner.** När ett recept visas för ett annat antal portioner än det är skrivet för, räknas mängderna om. **[G]**

### 2.4 Önskemål från beställaren

Carlos har några specifika önskemål. De är inte svåra, men de ska synas i programmet.

**Önskemål 1: Mängder som går att läsa [G]**
Mängder visas med högst två decimaler och utan onödiga nollor, följt av enheten. Till exempel `1,5 dl`, `2 st` och `0,25 tsk`, inte `1,50 dl` eller `2,00 st`.

**Önskemål 2: Veckan börjar på måndag [G]**
Veckoplanen börjar alltid på måndag och slutar på söndag. En middag som planeras på söndag hör till den veckan, även om användaren lägger in den på måndag morgon.

**Önskemål 3: Kategorin "Övrigt" är alltid kvar [G]**
Om en användare tar bort en egen kategori som har recept ska recepten flyttas till "Övrigt". De får inte försvinna.

**Önskemål 4: Varningen får inte spamma [G]**
Om kategorin redan har varnats för 80 % ska användaren inte få samma varning igen förrän veckan byts.

**Önskemål 5: Ta bort ett recept, med bekräftelse [G]**
Innan ett recept tas bort frågar programmet "Är du säker? (j/n)". Svaret "n" lämnar allt oförändrat.

## 3. User stories (Gherkin)

Carlos vill att följande beteenden finns. Scenarierna är exempel. Ni kan lägga till fler.

### Story 1: Lägga till ett recept [G]

```gherkin
Feature: Lägga till ett recept

  Scenario: Lägga till pannkakor
    Given kocken "Kim" finns
    And kategorin "Efterrätt" finns
    When användaren lägger till receptet "Pannkakor" för 4 portioner med 2,5 dl vetemjöl och 6 dl mjölk
    Then receptet sparas i databasen
    And receptet visar "2,5 dl vetemjöl" för 4 portioner
    And receptet visar "1,25 dl vetemjöl" för 2 portioner
```

### Story 2: Veckans gränser [G]

```gherkin
Feature: Veckans gränser

  Scenario: En middag på söndagen hör till rätt vecka
    Given receptet "Pannkakor" finns
    When användaren lägger in "Pannkakor" söndag 15 november 2026
    And användaren ber om veckoplanen för vecka 46
    Then "Pannkakor" finns med i vecka 46
    And "Pannkakor" finns inte med i vecka 47
```

### Story 3: Varning vid 80 % [G]

```gherkin
Feature: Varning vid gräns per kategori

  Scenario: Användaren får en varning vid 80 %
    Given kategorin "Efterrätt" har gränsen 5 gånger för vecka 47
    And veckoplanen har "Efterrätt" 3 gånger
    When användaren lägger in ett recept i kategorin "Efterrätt"
    Then användaren får meddelandet "Efterrätt är på 80 % av gränsen"

  Scenario: Samma varning visas inte två gånger
    Given användaren redan har fått varningen för "Efterrätt" i vecka 47
    When användaren lägger in ett till recept i kategorin "Efterrätt"
    Then ingen ny varning för 80 % visas
```

### Story 4: Ta bort en kategori utan att förlora recept [G]

```gherkin
Feature: Ta bort kategori

  Scenario: Recept flyttas till Övrigt
    Given kategorin "Grill" finns med två recept
    When användaren tar bort kategorin "Grill"
    Then de två recepten finns kvar
    And de tillhör kategorin "Övrigt"
```

### Story 5: Bekräfta borttagning [G]

```gherkin
Feature: Bekräfta borttagning

  Scenario: Användaren avbryter
    Given ett recept finns
    When användaren väljer att ta bort det
    And användaren svarar "n" på frågan "Är du säker?"
    Then receptet finns kvar
```

### Story 6: Felaktig inmatning [G]

```gherkin
Feature: Felaktig inmatning

  Scenario: Ogiltig mängd
    When användaren försöker lägga till en ingrediens med mängden "abc"
    Then programmet visar ett felmeddelande
    And ingen ingrediens sparas
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
- [ ] Ingen riktig persondata finns i databasen. Använd påhittade namn och recept.
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
5. Recept har namn, portioner, kategori, ingredienser och anteckning. Ingredienser har namn, mängd och enhet.
6. Repository finns.
7. Minst ett av Factory, Builder eller Observer finns.
8. Klasser med tydliga ansvar och minst ett interface.
9. Felaktig inmatning hanteras.
10. Kategorier: standardlistan och egna kategorier.
11. Veckoplan och inköpslista för vald vecka.
12. Gräns per kategori, och varningar vid 80 % och 100 % (Story 3).
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
