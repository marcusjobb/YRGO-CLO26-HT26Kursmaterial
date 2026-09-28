---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Variabler

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: `int` · `double` · `string` · `char` · `bool`

---

## Fråga 1/7 — Välj datatyp

Vilken datatyp passar bäst för varje variabel?

```csharp
_____ antalÄpplen = 12;
_____ pris        = 19.90;
_____ namn        = "Alex";
_____ betyg       = 'A';
_____ ärInloggad  = true;
```

---

## ✅ Svar 1/7

```csharp
int    antalÄpplen = 12;
double pris        = 19.90;
string namn        = "Alex";
char   betyg       = 'A';
bool   ärInloggad  = true;
```

- `int` — heltal · `double` — decimaltal · `string` — text
- `char` — **ett** tecken · `bool` — bara `true` eller `false`

---

## Fråga 2/7 — string eller char?

Vilka rader kompilerar?

```csharp
char   a = 'A';     // (a)
char   b = "A";     // (b)
string c = 'A';     // (c)
string d = "A";     // (d)
```

---

## ✅ Svar 2/7

**(a) och (d)** kompilerar.

- `char` = ett tecken, **enkla** citattecken `'A'`
- `string` = text, **dubbla** citattecken `"A"`
- `"A"` är en string med ett tecken — **inte** samma typ som `'A'`

> 💬 _"Enkla citat för ett tecken, dubbla för text."_

---

## Fråga 3/7 — Vad skrivs ut?

```csharp
int a = 7;
int b = 2;
Console.WriteLine(a / b);
Console.WriteLine(7.0 / b);
```

---

## ✅ Svar 3/7

```text
3
3.5
```

- `int / int` = **heltalsdivision** — decimalerna kapas bort
- Så fort ett tal är `double` (`7.0`) blir resultatet ett decimaltal

---

## Fråga 4/7 — Hitta felen

Fyra rader, fyra fel. Vad är fel och hur fixar du dem?

```csharp
int pi          = 3.14;
char bokstav    = "a";
string hälsning = 'hej';
bool klar       = "true";
```

---

## ✅ Svar 4/7

```csharp
double pi       = 3.14;     // int rymmer inte decimaler
char bokstav    = 'a';      // char = enkla citat
string hälsning = "hej";    // string = dubbla citat
bool klar       = true;     // inga citat runt true/false
```

> 💡 Kompilatorn är en noggrann kassörska: rätt sak i rätt låda.

---

## Fråga 5/7 — Deklaration och tilldelning

Vad kallas de tre raderna?

```csharp
int poäng;         // (1)
poäng = 10;        // (2)
int nivå = 1;      // (3)
```

---

## ✅ Svar 5/7

1. **Deklaration** — skapar variabeln (typ + namn), inget värde än
2. **Tilldelning** — lägger in ett värde med `=`
3. **Deklaration + tilldelning** i ett steg (initiering)

Läser du `poäng` innan den fått ett värde får du ett **kompileringsfel** (unassigned variable).

---

## Fråga 6/7 — Giltiga namn

Vilka variabelnamn är giltiga i C#?

```text
2spelare      mitt namn      _temp
antalRader    class
```

---

## ✅ Svar 6/7

**Giltiga:** `_temp`, `antalRader`

- `2spelare` — får inte börja med siffra
- `mitt namn` — inga mellanslag
- `class` — reserverat nyckelord

Konvention för lokala variabler: **camelCase** (`antalRader`).

---

## Fråga 7/7 — Plus med text och tal

```csharp
string a = "5";  string b = "3";
int c = 5;       int d = 3;

Console.WriteLine(a + b);
Console.WriteLine(c + d);
Console.WriteLine("Summa: " + c + d);
```

---

## ✅ Svar 7/7

```text
53
8
Summa: 53
```

- `string + string` **slår ihop** texten
- `int + int` **räknar**
- `"Summa: " + c + d` — läses vänster till höger, så `d` läggs på som text

Fix: `"Summa: " + (c + d)` → `Summa: 8`

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `variabler_marp.md`.
