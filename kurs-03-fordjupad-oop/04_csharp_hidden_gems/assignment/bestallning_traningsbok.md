# Beställning: Träningsbok

**Till:** grupp, Inlämning 2 (grupparbete)
**Från:** S/K Ummis AB, Carlos Ash, beställare
**Typ:** kursuppgift. Företag och beställare är fiktiva.
**Bilaga:** [Inlämning 2: grupparbete](inlamning_2_grupparbete.md) (regler, inlämning, granskning, G- och VG-delen i reflektionen)

> Märk: **[G]** = krav för G. Övrigt är gemensamma krav. VG bedöms i reflektionen och i peer review, enligt uppgiften, inte i den här beställningen.

---

## 1. Uppgift

S/K Ummis AB vill ha en enkel träningsbok. En utövare ska kunna registrera träningspass och övningar, se hur träningen fördelar sig per vecka, och få ett meddelande när ett veckomål för en träningstyp närmar sig eller är nått.

Vi ska använda programmet själva, i vardagen, så det får gärna kännas som något som funkar, inte som en tentaövning.

## 2. Kravspec

### 2.1 Grundkrav

- Programmet körs i konsol, Blazor eller MVC. Ni väljer. **[G]**
- **Databasen väljer ni själva.** Motivera valet i reflektionen. ADO.NET är inte tillåtet. **[G]**
- Programmet skapar databasen och seedar den med exempeldata vid första start, om den är tom. **[G]**
- Connectionstring läses från user-secrets, under nyckeln `DB_CONNECTION`. Den finns aldrig i koden eller i repot. **[G]**
- Varje pass har datum, träningstyp, längd i minuter och en valfri anteckning. Varje övning har namn, set, reps och vikt. **[G]**
- Ett **Repository** står mellan programmet och databasen. **[G]**
- Minst ett av mönstren **Factory**, **Builder** eller **Observer** används. **[G]**
- Programmet är uppdelat i klasser med tydliga ansvar (SRP), och har minst ett interface. **[G]**
- Felaktig inmatning ger ett tydligt meddelande, inte en krasch. Ogiltig vikt, okänt datum och tom träningstyp ska hanteras. **[G]**

### 2.2 Menyer och minimal modell

Projektmappen (`Traningsbok-Konsol`, `Traningsbok-Mvc` eller `Traningsbok-Blazor`) har en `README.md` med de menyval och den minimala modell som ni måste bygga ut. Det som står där är **minimum**. Ni lägger till fält och menyval för att uppfylla önskemålen nedan. Medlemmarna i README är den datamodell Carlos skickade från sitt gamla Excel-ark.

### 2.3 Funktioner

1. **Utövare.** En utövare har ett namn (förnamn räcker). Programmet ska klara 1–5 utövare.
2. **Pass och övningar.** Lägg till, visa och ta bort ett pass. Lägg till övningar till ett pass.
3. **Träningstyper.** Standardlistan är: Styrka, Kondition, Rörlighet, Bollsport, Övrigt. Användaren kan lägga till egna. **[G]**
4. **Veckoöversikt.** Antal pass, total träningstid och träningstid per träningstyp för vald vecka. **[G]**
5. **Veckomål per träningstyp.** Sätt ett veckomål i minuter per träningstyp. **[G]**
6. **Meddelande vid målet.** När en träningstyp når 80 % av veckomålet ska användaren få ett meddelande, och när den når 100 % ett annat. **[G]**
7. **Statistik för en övning.** Högsta vikt och totalt antal set för en vald övning. **[G]**

### 2.4 Önskemål från beställaren

Carlos har några specifika önskemål. De är inte svåra, men de ska synas i programmet.

**Önskemål 1: Vikt och tid som går att läsa [G]**
Vikt visas med en decimal och "kg", till exempel `62,5 kg`. Träningstid visas i timmar och minuter, till exempel `1 h 05 min`. Under en timme visas bara minuter, till exempel `45 min`.

**Önskemål 2: Veckan börjar på måndag [G]**
Veckoöversikten börjar alltid på måndag och slutar på söndag. Ett pass på söndag kväll hör till den veckan, även om användaren skriver in det på måndag morgon.

**Önskemål 3: Träningstypen "Övrigt" är alltid kvar [G]**
Om en användare tar bort en egen träningstyp som har pass ska passen flyttas till "Övrigt". De får inte försvinna.

**Önskemål 4: Meddelandet får inte spamma [G]**
Om träningstypen redan har fått meddelandet för 80 % ska användaren inte få samma meddelande igen förrän veckan byts.

**Önskemål 5: Ta bort ett pass, med bekräftelse [G]**
Innan ett pass tas bort frågar programmet "Är du säker? (j/n)". Svaret "n" lämnar allt oförändrat.

## 3. User stories (Gherkin)

Carlos vill att följande beteenden finns. Scenarierna är exempel. Ni kan lägga till fler.

### Story 1: Registrera ett pass [G]

```gherkin
Feature: Registrera ett pass

  Scenario: Lägga till ett styrkepass
    Given utövaren "Kim" finns
    And träningstypen "Styrka" finns
    When användaren registrerar ett pass på 65 minuter den 12 november 2026 med anteckningen "Ben"
    And lägger till övningen "Knäböj" med 3 set, 8 reps och 62,5 kg
    Then passet sparas i databasen
    And översikten visar "Styrka: 1 h 05 min" för vecka 46
```

### Story 2: Veckans gränser [G]

```gherkin
Feature: Veckans gränser

  Scenario: Ett pass på söndagen hör till rätt vecka
    Given utövaren "Kim" finns
    When användaren registrerar ett pass på 30 minuter söndag 15 november 2026
    And användaren ber om översikten för vecka 46
    Then passet finns med i vecka 46
    And passet finns inte med i vecka 47
```

### Story 3: Meddelande vid 80 % [G]

```gherkin
Feature: Meddelande vid veckomål

  Scenario: Användaren får ett meddelande vid 80 %
    Given träningstypen "Kondition" har ett veckomål på 100 minuter för vecka 47
    And användaren har registrerat pass på 75 minuter i "Kondition"
    When användaren registrerar ett pass på 6 minuter i "Kondition"
    Then användaren får meddelandet "Kondition är på 81 % av veckomålet"

  Scenario: Samma meddelande visas inte två gånger
    Given användaren redan har fått meddelandet för "Kondition" i vecka 47
    When användaren registrerar ett pass på 5 minuter i "Kondition"
    Then inget nytt meddelande visas
```

### Story 4: Ta bort en träningstyp utan att förlora pass [G]

```gherkin
Feature: Ta bort träningstyp

  Scenario: Pass flyttas till Övrigt
    Given träningstypen "Klättring" finns med två pass
    When användaren tar bort träningstypen "Klättring"
    Then de två passen finns kvar
    And de tillhör träningstypen "Övrigt"
```

### Story 5: Bekräfta borttagning [G]

```gherkin
Feature: Bekräfta borttagning

  Scenario: Användaren avbryter
    Given ett pass finns
    When användaren väljer att ta bort det
    And användaren svarar "n" på frågan "Är du säker?"
    Then passet finns kvar
```

### Story 6: Felaktig inmatning [G]

```gherkin
Feature: Felaktig inmatning

  Scenario: Ogiltig vikt
    When användaren försöker lägga till en övning med vikten "abc"
    Then programmet visar ett felmeddelande
    And ingen övning sparas
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
- [ ] Ingen riktig persondata finns i databasen. Använd påhittade namn och värden.
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
5. Pass har datum, träningstyp, längd och anteckning. Övningar har namn, set, reps och vikt.
6. Repository finns.
7. Minst ett av Factory, Builder eller Observer finns.
8. Klasser med tydliga ansvar och minst ett interface.
9. Felaktig inmatning hanteras.
10. Träningstyper: standardlistan och egna träningstyper.
11. Veckoöversikt med antal pass, total tid och tid per träningstyp.
12. Veckomål per träningstyp, och meddelanden vid 80 % och 100 % (Story 3).
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
