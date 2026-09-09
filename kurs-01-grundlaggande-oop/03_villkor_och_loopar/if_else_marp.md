---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# if / else if / else

### Hur koden fattar beslut

_Kurs 01 · Vecka 3 · Nion Education_

---

## Vad är ett villkor?

Ett villkor är ett uttryck som antingen är **sant** eller **falskt**.

```csharp
bool harRåd = true;
bool ärVuxen = false;
```

I C# kallas det ett `bool`-värde — kort för *boolean*.

Villkor är grunden för all logik i kod. Utan dem gör programmet alltid samma sak, oavsett vad som händer.

> 💬 _"Du har redan träffat bool. Nu börjar vi använda den för riktiga beslut."_

---

## Enkel if

```csharp
int ålder = 20;

if (ålder >= 18)
    Console.WriteLine("Du får rösta!");
```

Koden inuti `if` körs **bara om villkoret är sant**.

```
Flöde:
         [ålder >= 18?]
               |
        Sant ──┘── Falskt
         |              |
  Skriv ut "Du        (hoppa över)
   får rösta!"
```

Om `ålder` är 17 — händer ingenting.

---

## if / else

```csharp
int ålder = 15;

if (ålder >= 18)
    Console.WriteLine("Du får rösta!");
else
    Console.WriteLine("Du är för ung.");
```

`else` körs när villkoret är **falskt**.

```
[ålder >= 18?]
      |
  Sant │ Falskt
      │       │
 "rösta!"  "för ung."
```

En av de två vägarna körs alltid — aldrig båda.

---

## if / else if / else — en kedja

```csharp
int poäng = 72;

if (poäng >= 90)
    Console.WriteLine("Du fick A — Utmärkt!");
else if (poäng >= 70)
    Console.WriteLine("Du fick B — Bra jobbat!");
else if (poäng >= 50)
    Console.WriteLine("Du fick C — Godkänt.");
else
    Console.WriteLine("Du fick F — Försök igen.");
```

C# testar varje villkor uppifrån och ner.
Det första som stämmer körs — resten hoppas över.

> 💬 _"Ordningen spelar roll. Prova att flytta runt raderna — vad händer?"_

---

## Jämförelseoperatorer

| Operator | Betyder | Exempel |
|----------|---------|---------|
| `==` | lika med | `poäng == 100` |
| `!=` | inte lika med | `namn != "admin"` |
| `<` | mindre än | `ålder < 18` |
| `>` | större än | `pris > 500` |
| `<=` | mindre eller lika | `hastighet <= 50` |
| `>=` | större eller lika | `poäng >= 70` |

Obs: `=` tilldelar ett värde. `==` jämför två värden.

---

## Logiska operatorer

Kombinera villkor med `&&`, `||` och `!`:

```csharp
// && — båda måste vara sanna
if (ålder >= 18 && harID)
    Console.WriteLine("Välkommen in!");

// || — minst ett måste vara sant
if (dag == "Lördag" || dag == "Söndag")
    Console.WriteLine("Det är helg!");

// ! — vänd om sant/falskt
if (!ärInloggad)
    Console.WriteLine("Logga in först.");
```

> 💬 _"`&&` är sträng — båda måste stämma. `||` är generös — en räcker."_

---

## Nästlade if — när det går snett

```csharp
// Svårt att följa — undvik djup nästling
if (ålder >= 18) {
    if (harID) {
        if (ärRegistrerad) {
            Console.WriteLine("Du får rösta!");
        }
    }
}
```

Tre nivåer in — och det är redan rörigt.

**Bättre:** kombinera med `&&`:

```csharp
if (ålder >= 18 && harID && ärRegistrerad)
    Console.WriteLine("Du får rösta!");
```

---

## Nästling — när det faktiskt är okej

Nästlade `if` kan vara vettiga när de hanterar *helt olika saker*:

```csharp
if (betalningGodkänd) {
    if (lagerstatus == "i lager")
        Console.WriteLine("Beställning bekräftad!");
    else
        Console.WriteLine("Betalning OK — väntar på lager.");
}
```

Regeln: om du är inne på nivå 3 — stanna upp och fundera.

> 💬 _"Koden ska vara läsbar för någon som aldrig sett den förut — inklusive du själv om tre månader."_

---

## Allt tillsammans

```csharp
string väder = "regn";
int temperatur = 8;

if (väder == "sol" && temperatur >= 20)
    Console.WriteLine("Ta på dig t-shirt!");
else if (väder == "regn")
    Console.WriteLine("Ta med ett paraply.");
else
    Console.WriteLine("Klä dig efter väder.");
```

Ett villkor kan vara hur komplext som helst — men håll det läsbart.

---

## Sammanfattning

```
if (villkor)          → körs om sant
else if (annat)       → testas om första var falskt
else                  → körs om inget annat stämde

==  !=  <  >  <=  >=  → jämför värden
&&  ||  !              → kombinera villkor
```

Tre saker att komma ihåg:
1. Ordningen i kedjan spelar roll
2. `=` tilldelar — `==` jämför
3. Djup nästling → kombinera med `&&` istället

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 `conditions/temperature_check.md` — temperaturkontroll med if/else  
🟡 `conditions/character_class.md` — klassifiera ett tecken med villkor

_Ta det steg för steg. Använd tipsen om du fastnar._
