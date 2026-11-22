# Beställning: Hushållsbudget

**Till:** grupp, Inlämning 2 (grupparbete)
**Från:** S/K Ummis AB, Carlos Ash, beställare
**Typ:** kursuppgift. Företag och beställare är fiktiva.
**Bilaga:** [Inlämning 2: grupparbete](inlamning_2_grupparbete.md) (regler, inlämning, granskning, G- och VG-delen i reflektionen)

> Märk: **[G]** = krav för G. Övrigt är gemensamma krav. VG bedöms i reflektionen och i peer review, enligt uppgiften, inte i den här beställningen.

---

## 1. Uppgift

S/K Ummis AB vill ha ett enkelt program för hushållsbudget. Ett hushåll ska kunna registrera inkomster och utgifter, se hur pengarna fördelar sig per månad, och få en varning när en kategori går över sin budget.

Vi ska använda programmet hemma, i vardagen, så det får gärna kännas som något som funkar, inte som en tentaövning.

## 2. Kravspec

### 2.1 Grundkrav

- Programmet körs i konsol, Blazor eller MVC. Ni väljer. **[G]**
- **Databasen väljer ni själva.** Motivera valet i reflektionen. ADO.NET är inte tillåtet. **[G]**
- Programmet skapar databasen och seedar den med exempeldata vid första start, om den är tom. **[G]**
- Connectionstring läses från user-secrets, under nyckeln `DB_CONNECTION`. Den finns aldrig i koden eller i repot. **[G]**
- Varje transaktion har belopp, datum, kategori och en valfri anteckning. **[G]**
- Ett **Repository** står mellan programmet och databasen. **[G]**
- Minst ett av mönstren **Factory**, **Builder** eller **Observer** används. **[G]**
- Programmet är uppdelat i klasser med tydliga ansvar (SRP), och har minst ett interface. **[G]**
- Felaktig inmatning ger ett tydligt meddelande, inte en krasch. Ogiltigt belopp, okänt datum och tom kategori ska hanteras. **[G]**

### 2.2 Menyer och minimal modell

Projektmappen (`Hushallsbudget-Konsol`, `Hushallsbudget-Mvc` eller `Hushallsbudget-Blazor`) har en `README.md` med de menyval och den minimala modell som ni måste bygga ut. Det som står där är **minimum**. Ni lägger till fält och menyval för att uppfylla önskemålen nedan. Medlemmarna i README är den datamodell Carlos skickade från sitt gamla Excel-ark.

### 2.3 Funktioner

1. **Hushåll.** Ett hushåll har ett namn och 1–5 medlemmar (förnamn räcker).
2. **Transaktioner.** Lägg till, visa och ta bort en transaktion.
3. **Kategorier.** Standardlistan är: Boende, Mat, Transport, El och internet, Nöje, Hälsa, Övrigt. Användaren kan lägga till egna. **[G]**
4. **Månadsöversikt.** Summa inkomster, summa utgifter, saldo, och summa per kategori för vald månad. **[G]**
5. **Budget per kategori.** Sätt en månadsbudget per kategori. **[G]**
6. **Varning vid överskridning.** När en kategori går över 80 % av budgeten ska användaren få ett meddelande, och när den går över 100 % ett annat. **[G]**

### 2.4 Önskemål från beställaren

Carlos har några specifika önskemål. De är inte svåra, men de ska synas i programmet.

**Önskemål 1: Belopp i kronor [G]**
Alla belopp visas med två decimaler och "kr", till exempel `1 249,50 kr`. En utgift visas som ett positivt belopp under Utgifter, inte med minustecken.

**Önskemål 2: Månadens första dag [G]**
Månadsöversikten börjar alltid på den första i månaden, och slutar på den sista. Transaktioner den 31 januari hör till januari, även om användaren skriver in dem den 1 februari.

**Önskemål 3: Kategorin "Övrigt" är alltid kvar [G]**
Om en användare tar bort en egen kategori som har transaktioner ska transaktionerna flyttas till "Övrigt". De får inte försvinna.

**Önskemål 4: Varning får inte spamma [G]**
Om kategorin redan har varnats för 80 % ska användaren inte få samma varning igen förrän månaden byts.

**Önskemål 5: Ta bort en transaktion, med bekräftelse [G]**
Innan en transaktion tas bort frågar programmet "Är du säker? (j/n)". Svaret "n" lämnar allt oförändrat.

## 3. User stories (Gherkin)

Carlos vill att följande beteenden finns. Scenarierna är exempel. Ni kan lägga till fler.

### Story 1: Registrera en utgift [G]

```gherkin
Feature: Registrera en utgift

  Scenario: Lägga till en matvara
    Given hushållet "Familjen Berg" finns
    And kategorin "Mat" finns
    When användaren registrerar en utgift på 249,5 kr den 12 november 2026 med anteckningen "ICA"
    Then transaktionen sparas i databasen
    And översikten visar "Mat: 249,50 kr" för november
```

### Story 2: Månadens gränser [G]

```gherkin
Feature: Månadens gränser

  Scenario: En transaktion den sista dagen hör till rätt månad
    Given hushållet "Familjen Berg" finns
    When användaren registrerar en utgift på 100 kr den 31 januari 2026
    And användaren ber om översikten för januari 2026
    Then utgiften finns med i januari
    And utgiften finns inte med i februari
```

### Story 3: Varning vid 80 % [G]

```gherkin
Feature: Varning vid budget

  Scenario: Användaren får en varning vid 80 %
    Given kategorin "Nöje" har en budget på 1 000 kr för november 2026
    And användaren har registrerat utgifter på 790 kr i "Nöje"
    When användaren registrerar en utgift på 20 kr i "Nöje"
    Then användaren får meddelandet "Nöje är på 81 % av budgeten"

  Scenario: Samma varning visas inte två gånger
    Given användaren redan har fått varningen för "Nöje" i november 2026
    When användaren registrerar en utgift på 10 kr i "Nöje"
    Then ingen ny varning visas
```

### Story 4: Ta bort en kategori utan att förlora pengar [G]

```gherkin
Feature: Ta bort kategori

  Scenario: Transaktioner flyttas till Övrigt
    Given kategorin "Bil" finns med två transaktioner
    When användaren tar bort kategorin "Bil"
    Then de två transaktionerna finns kvar
    And de tillhör kategorin "Övrigt"
```

### Story 5: Bekräfta borttagning [G]

```gherkin
Feature: Bekräfta borttagning

  Scenario: Användaren avbryter
    Given en transaktion finns
    When användaren väljer att ta bort den
    And användaren svarar "n" på frågan "Är du säker?"
    Then transaktionen finns kvar
```

### Story 6: Felaktig inmatning [G]

```gherkin
Feature: Felaktig inmatning

  Scenario: Ogiltigt belopp
    When användaren försöker registrera en utgift med beloppet "abc"
    Then programmet visar ett felmeddelande
    And ingen transaktion sparas
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
- [ ] Ingen riktig persondata finns i databasen. Använd påhittade namn och belopp.
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
5. Transaktioner har belopp, datum, kategori och anteckning.
6. Repository finns.
7. Minst ett av Factory, Builder eller Observer finns.
8. Klasser med tydliga ansvar och minst ett interface.
9. Felaktig inmatning hanteras.
10. Kategorier: standardlistan och egna kategorier.
11. Månadsöversikt med summor och per kategori.
12. Budget per kategori, och varningar vid 80 % och 100 % (Story 3).
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
