---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Flödeskontroll

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: `if` / `else if` / `else` · `switch` · `case` · `break` · `default`

---

## Fråga 1/7 — if-kedjan

Vad skrivs ut?

```csharp
int poäng = 75;

if (poäng >= 90)      Console.WriteLine("A");
else if (poäng >= 70) Console.WriteLine("B");
else if (poäng >= 50) Console.WriteLine("C");
else                  Console.WriteLine("F");
```

---

## ✅ Svar 1/7

```text
B
```

Kedjan testas uppifrån. Den **första sanna grenen** körs — resten hoppas över, även om de också vore sanna (`75 >= 50`).

---

## Fråga 2/7 — Fel ordning

Poängen är `95`. Vad skrivs ut, och varför är det fel?

```csharp
if (poäng >= 50)      Console.WriteLine("G");
else if (poäng >= 90) Console.WriteLine("VG");
```

---

## ✅ Svar 2/7

```text
G
```

`95 >= 50` är sant först → `VG` nås **aldrig**.

```csharp
if (poäng >= 90)      Console.WriteLine("VG");   // strängaste först
else if (poäng >= 50) Console.WriteLine("G");
```

---

## Fråga 3/7 — = eller ==

Vad är skillnaden mellan `=`, `==` och `!=`?

Vad händer här?

```csharp
int x = 3;
if (x = 5) { Console.WriteLine("Fem"); }
```

---

## ✅ Svar 3/7

- `=` — **tilldelning** (lägg in värde)
- `==` — **jämförelse** (är de lika?)
- `!=` — **inte lika**

`if (x = 5)` ger **kompileringsfel**: ett `int` är inget `bool`. Skriv `if (x == 5)`.

---

## Fråga 4/7 — Logiska operatorer

Vad ger ①–③?

```csharp
int ålder = 20;
bool harKort = false;

Console.WriteLine(ålder >= 18 && harKort);   // ①
Console.WriteLine(ålder >= 18 || harKort);   // ②
Console.WriteLine(!harKort);                 // ③
```

---

## ✅ Svar 4/7

```text
False
True
True
```

- `&&` (OCH) — **båda** måste vara sanna
- `||` (ELLER) — **minst ett** måste vara sant
- `!` (INTE) — vänder på `bool`

---

## Fråga 5/7 — switch-uppbyggnad

Vad heter ①–④?

```csharp
switch (dag)                    // ①
{
    case 1:                     // ②
        Console.WriteLine("Måndag");
        break;                  // ③
    default:                    // ④
        Console.WriteLine("Okänd dag");
        break;
}
```

---

## ✅ Svar 5/7

| # | Del | Uppgift |
|---|-----|---------|
| ① | `switch (uttryck)` | värdet som jämförs |
| ② | `case värde:` | ett möjligt värde |
| ③ | `break` | avslutar grenen |
| ④ | `default` | körs om inget `case` matchar (som `else`) |

---

## Fråga 6/7 — Fall-through

Vilken av A och B är tillåten i C#?

```csharp
// A
case 6:
case 7:
    Console.WriteLine("Helg");
    break;

// B
case 1:
    Console.WriteLine("Måndag");
case 2:
    Console.WriteLine("Tisdag");
    break;
```

---

## ✅ Svar 6/7

**A är tillåten. B ger kompileringsfel.**

- A: tomma `case` som **grupperas** — det enda undantaget
- B: ett `case` med kod **måste** sluta med `break` (eller `return`)
- C# stoppar alltså det klassiska "fall-through"-misstaget

---

## Fråga 7/7 — if eller switch?

Vilket väljer du?

1. Betyg utifrån poäng (`>= 90`, `>= 70`…)
2. Menyval 1, 2, 3 eller 4
3. `ålder >= 18 && harLegitimation`
4. Veckodagens namn från siffra, som **uttryck**

---

## ✅ Svar 7/7

| Situation | Välj |
|-----------|------|
| Intervall | `if / else if` |
| Exakta, kända värden | `switch` |
| Komplext villkor med `&&` / `\|\|` | `if` |
| Kort uttryck som returnerar värde | `switch expression` |

```csharp
string namn = dag switch { 1 => "Måndag", 2 => "Tisdag", _ => "Okänd" };
```

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `if_else_marp.md` och `switch_marp.md`.
