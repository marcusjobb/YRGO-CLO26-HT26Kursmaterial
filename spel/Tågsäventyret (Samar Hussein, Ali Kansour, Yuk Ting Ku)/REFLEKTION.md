# Reflektion — Slutprojekt

**Namn:** Samar Hussein
**Kurs:** Grundläggande OOP i C#  
**Projekt:** Tågäventyret/Skogsäventyret *(Skogsäventyret / Havsforskarna / Dungeon Crawler)*  
**Datum:**  2026-10-04

---

> Den här reflektionen är obligatorisk och lämnas in **individuellt** — även om projektet gjordes i grupp.  
> Det finns inga rätta eller fel svar. Skriv vad du faktiskt tyckte och upplevde.  
> Kort och ärligt räcker — några meningar per fråga.

---

## Vad var svårast att lösa?

*Var fastnade du? Vad tog längre tid än du trodde — och hur kom du vidare?*
 > Det svåraste var att få striderna och spelets olika delar att fungera tillsammans. Vi hade även en del problem med Git och GitHub när vi skulle pusha och uppdatera projektet, vilket ibland ledde till konflikter mellan våra ändringar. Vi löste problemen genom att hjälpa varandra och genom att låta en person i taget göra ändringar och pusha koden.

---

## Hur fungerade samarbetet i gruppen?

*Vad fungerade bra? Vad var svårt? Hur delade ni upp arbetet? Hur använde ni Git tillsammans (branches, merge, pull requests)?*

> Samarbetet fungerade bra och vi hjälpte varandra när någon fastnade. Vi delade upp arbete mellan oss och pratade med varandra om vad som behövdes göras. Vi kommunicerade med varandra både på Discord och under lektionstid. Samar gjorde Player, Yuk gjorde Zombie/Monster, både basklassern och subklasserna och Ali gjorde Battle. Vi hjälptes åt Main()/program.cs delen.

---

## Om du fick göra om det — vad hade du gjort annorlunda?

*Tänk på din lösning, din struktur, eller hur ni jobbade. Vad skulle du ändra?*

> Om vi fick göra om projekete hade vi börjat med att planera bättre. Vi hade kunnat gör ett UML-diagram och läsa genom alla krav noggrannare innan vi började koda. Vi hade även kunnat ha ett tydligare system för Git, till exempel bestämma vad som skulle pushas och när. Då hade vi kunnat undvika onödiga pushningar och konflikter.

---

## Valfritt — arv och datastrukturer

*Om ni använde arv: varför valde ni den strukturen? Vilken `List<T>` (eller annan datastruktur) använde ni, och varför just den?*

> Vi använde arv för att kunna skapa olika typer av Zombies med gemensamma egenskaper. Walker, Runner och Mutant ärver från Zombie, vilket gör att vi slipper skriva samma kod flera gånger. Vi använder List<Zombie> för att samla alla monster och sedan välja ett slumpmässigt monster.

---

## VG — Motivering

> Fyll i det här avsnittet om du siktar på VG. Lämna tomt = G-bedömning.  
> Svara på frågorna som hör till **ditt** projekt och dina VG-val. Radera resten.

### Skogsäventyret

- Vilken datastruktur valde ni för vapensortimentet och varför?
- Hur sorterade ni monstren i arenan?
- Hade ni kunnat lösa arenan utan arv?

> 

### Havsforskarna

- Vilken VG-utbyggnad valde ni och varför just den?
- Vad är den tekniskt svåraste delen av er lösning?
- Hade ni kunnat lösa det utan `List<T>`?

> 

### Dungeon Crawler

- Vilka VG-utbyggnader valde ni och varför?
- Motivera era datastrukturval (`string` som nyckel, `List<string>` för dialog, valt sparformat).
- Vad hade en annan struktur gett er?

> 
