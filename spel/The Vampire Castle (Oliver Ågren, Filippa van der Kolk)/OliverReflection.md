# Reflektion — Slutprojekt

**Namn:** Oliver Ågren
**Kurs:** Grundläggande OOP i C#
**Projekt:** The Vampire Castle (Dungeon Crawler)
**Datum:** 2 oktober 2026

---

## Vad var svårast att lösa?

*Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*

Jag fastande generellt på alla loopar, skelett av en loop är lätt men vad det skall innehålla och få det att bli logiskt kan bli väldigt rörigt efter ett tag.

---

## Hur fungerade samarbetet i gruppen?

*Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet? Hur använde ni Git tillsammans (branches, `merge`, `pull requests`)?*

Dynamiken var toppen, vi hade struktur i hur vi ville arbeta, vi var flexibla med plugg tillfällen och känslan överlag kändes bra i grupp. Detta gjorde också att vi alltid kändes produktiva.

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

Jag tror att vi över komplicerade saker i början och tänkte att projektet skulle vara en mycket större grej än vad det egentligen var.
När vi såg projektet för vad det var så kändes det bättre

---

## Valfritt — arv och datastrukturer

*Om ni använde arv: varför valde ni den strukturen? Vilken `List<T>` (eller annan datastruktur) använde ni, och varför just den?*

Vi valde list främst för att det är lättare att ändra under spelets gång, vi behöver alltså loopa och igenom och lägga till och ta bort.
En array hade till exemple varit för stel för detta projekt

---

## VG — Motivering

### Dungeon Crawler

**Vilka VG-utbyggnader valde ni och varför?**

Vi valde NPC för att vi tyckte att det passade bäst för detta projekt, vi såg det också som en rolig del.

**Motivera era datastrukturval (`string` som nyckel, `List<string>` för dialog, valt sparformat).**

- `string` Key som vi hade i detta fall gör det lättare för oss att tyda koden och komma ihåg saker.
- Så att använda `string` gör det lättare att slå ihop rätt objekt utan att loopa genom allt.
- `List<string>` använde vi för att replikerna kommer i en bestämd ordning och antalet KAN variera mellan olika karaktärer. Den är mer flexibel

**Vad hade en annan struktur gett er?**

- Hade vi till exempel gjort Key med `int` så hade koden blivit svårare att läsa just för att man måste komma ihåg vad varje siffra betyder.
- En array istället för `List<string>` hade gett oss en fast storlek, vilket hade gjort det krångligare att lägga till eller ändra repliker utan att skriva om koden.
