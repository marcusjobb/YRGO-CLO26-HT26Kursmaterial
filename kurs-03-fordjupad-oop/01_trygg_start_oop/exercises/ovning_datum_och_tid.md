# Övning: Datum och tid i Brf Solgläntan

**Tid:** ca 30 minuter. 🟢 först, sedan 🟡, sist 🔴.
Termer finns i `termer/datum_och_tid.md`.

---

## Berättelsen

Styrelsen i Brf Solgläntan har många datum att hålla koll på: julfesten, kursen för nya styrelsemedlemmar, och hur länge folk har stått i parkeringskön. Du hjälper dem att räkna.

---

## Startkod

Skapa ett nytt konsolprojekt och klistra in det här. Datumen är fasta, så att alla får samma svar oavsett vilken dag du gör övningen. Skriv dina lösningar under raderna.

```cs
var today = new DateOnly(2026, 11, 5);        // "idag"
var christmas = new DateOnly(2026, 12, 24);   // julafton
var clock = new TimeOnly(9, 30);              // klockan just nu
var courseStart = new DateOnly(2026, 11, 2);  // kursstart
var exam = new DateOnly(2026, 12, 2);         // tentadagen
var now = new DateTime(2026, 11, 5, 12, 0, 0);        // nu: datum och tid tillsammans
var examStart = new DateTime(2026, 12, 2, 9, 0, 0);   // tentan börjar 09:00
var queueDate = new DateOnly(2026, 1, 10);    // dagen Johannes ställde sig i kön
var taylor_travis_marriage = new DateTime(2026, 7, 3, 17, 30, 0);   // bröllopet: datum och tid (New York tid)
```

### Vad matas in

Det finns inga egna klasser i den här övningen, bara färdiga typer, så det blir inget UML-diagram. Så här läser du raderna:

| Du skriver | Betyder | Exempel |
|---|---|---|
| `new DateOnly(år, månad, dag)` | Ett datum | `new DateOnly(2026, 12, 24)` är 24 december 2026 |
| `new TimeOnly(timme, minut)` | Ett klockslag | `new TimeOnly(9, 30)` är 09:30 |
| `new DateTime(år, månad, dag, timme, minut, sekund)` | Datum och klockslag | `new DateTime(2026, 12, 2, 9, 0, 0)` är 2 december kl 09:00 |

---

## 🟢 Del 1: Räkna med datum

### Uppgift 1: Dagar till jul
Hur många dagar är det kvar till julafton?

Förväntad output:
```
Dagar till jul: 49
```

<details>
<summary>Ledtråd</summary>

Du kan inte dra ifrån ett `DateOnly` från ett annat direkt. Varje datum har ett `DayNumber`, ett löpnummer för dagen. Dra ifrån dem i stället.
</details>

### Uppgift 2: Om två veckor
Vilket datum är det om två veckor (14 dagar)? Skriv ut det som `ÅÅÅÅ-MM-DD`.

Förväntad output:
```
Om två veckor: 2026-11-19
```

<details>
<summary>Ledtråd</summary>

`AddDays(14)` ger ett nytt datum. Formatera det med `ToString("yyyy-MM-dd")`.
</details>

### Uppgift 3: Veckodag
Vilken veckodag är det idag?

Förväntad output:
```
Veckodag idag: Thursday
```

### Uppgift 4: Klockan om 15 timmar
Klockan är 09:30. Vad är klockan om 15 timmar?

Förväntad output:
```
Om 15 timmar: 00:30
```

Fundera: vilken dag är det då? Vet `TimeOnly` det? Vilken typ skulle veta det?

---

## 🟡 Del 2: Mellan två tidpunkter

### Uppgift 5: Från kursstart till tenta
Hur många dagar är det mellan kursstarten och tentan? Är tentan i framtiden jämfört med idag?

Förväntad output:
```
Dagar mellan kursstart och tenta: 30
Tentan är i framtiden: True
```

<details>
<summary>Ledtråd</summary>

Två `DateOnly` kan du jämföra med `>` och `<`. Antal dagar får du med `DayNumber`, som i uppgift 1.
</details>

### Uppgift 6: Tid kvar till tentan
Nu är det den 5 november klockan 12:00 (`now`). Tentan börjar 2 december klockan 09:00 (`examStart`). Hur lång tid är det kvar, och när slutar tentan om den pågår i tre timmar?

Förväntad output:
```
Kvar till tenta: 26 dagar och 21 timmar
Tentan slutar: 12:00
```

<details>
<summary>Ledtråd</summary>

Dra ifrån två `DateTime`. Svaret är ett `TimeSpan`, som har `Days` och `Hours`. Tentans sluttid får du med `AddHours(3)` och `ToString("HH:mm")`.
</details>

### Uppgift 7: Plocka ut datum och klockslag
`now` är ett `DateTime`, alltså datum och klockslag tillsammans. Plocka ut **bara datumet** som ett `DateOnly` och **bara klockslaget** som ett `TimeOnly`, och skriv ut dem.

I riktig kod hade du skrivit `DateTime.Now` i stället för det fasta `now`, och då får du alltid dagens datum och klockslag. Här använder vi det fasta värdet, så att alla får samma svar.

Förväntad output:
```
Datum: 2026-11-05
Klockslag: 12:00
```

<details>
<summary>Ledtråd</summary>

`DateOnly.FromDateTime(...)` och `TimeOnly.FromDateTime(...)` plockar ut varsin del. Formatera med `ToString("yyyy-MM-dd")` och `ToString("HH:mm")`.
</details>

Fundera: kan du sätta ihop datumet och klockslaget till ett `DateTime` igen? Leta efter `ToDateTime` på ditt `DateOnly`.

---

## 🔴 Del 3: Utmaningen

### Uppgift 8: Hur länge har Johannes väntat?
Johannes ställde sig i kön den 10 januari (`queueDate`). Hur många dagar har han väntat, och hur många hela veckor och överblivna dagar är det?

Förväntad output:
```
Johannes förfrågan är 299 dagar gammal
299 dagar = 42 veckor och 5 dagar
```

<details>
<summary>Ledtråd</summary>

Hela veckor får du med heltalsdivision (`/ 7`). Överblivna dagar får du med resten (`% 7`).
</details>

---

## Till handledningen

1. I uppgift 4: vad händer med `TimeOnly` när klockan går förbi midnatt, och varför är det ett problem om du också vill veta datumet?
2. Varför ändrar `AddDays` inte originalet `today`?
3. Tänk på inlämningen: en bokning har en incheckning och en utcheckning. Vilken typ skulle du välja, `DateOnly`, `DateTime` eller något annat? Vad är viktigast att kunna räkna ut?
