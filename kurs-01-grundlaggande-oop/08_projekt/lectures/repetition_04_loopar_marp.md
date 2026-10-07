---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Loopar

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: `for` · `while` · `do/while` · `foreach` · `break` · `continue`

---

## Fråga 1/7 — Vilken loop?

Vilken loop passar bäst?

1. Skriv ut talen 1–10
2. Fråga användaren tills hen skriver rätt lösenord
3. Gå igenom alla namn i en lista
4. Visa en meny — den ska visas **minst en gång**

---

## ✅ Svar 1/7

| Situation | Loop |
|-----------|------|
| Vet exakt antal varv | `for` |
| Kör tills ett villkor ändras | `while` |
| Går igenom en samling | `foreach` |
| Vill alltid köra minst en gång | `do/while` |

---

## Fråga 2/7 — Delarna i for

Vad heter ①–③, och i vilken ordning körs de?

```csharp
for (int i = 0; i < 5; i++)
//   ①          ②      ③
{
    Console.WriteLine(i);
}
```

---

## ✅ Svar 2/7

1. **Initiering** — körs **en gång** i början
2. **Villkor** — kollas före varje varv
3. **Uppdatering** — körs efter varje varv

Ordning: ① → ② → kropp → ③ → ② → kropp → ③ → … tills ② är falskt.

---

## Fråga 3/7 — Vad skrivs ut?

```csharp
for (int i = 0; i < 10; i += 3)
{
    Console.Write(i + " ");
}
```

---

## ✅ Svar 3/7

```text
0 3 6 9
```

`i` går 0 → 3 → 6 → 9 → 12. Då är `12 < 10` falskt och loopen stoppar.

---

## Fråga 4/7 — Evighetsloopen

Vad är fel, och hur fixar du det?

```csharp
int i = 0;
while (i < 5)
{
    Console.WriteLine(i);
}
```

---

## ✅ Svar 4/7

`i` ändras **aldrig**, så `i < 5` är alltid sant — en **infinite loop**.

```csharp
int i = 0;
while (i < 5)
{
    Console.WriteLine(i);
    i++;            // något måste få villkoret att bli falskt
}
```

---

## Fråga 5/7 — while vs do/while

Vad skrivs ut?

```csharp
int x = 10;

while (x < 5)
{
    Console.WriteLine("A");
}

do
{
    Console.WriteLine("B");
} while (x < 5);
```

---

## ✅ Svar 5/7

```text
B
```

- `while` kollar villkoret **före** kroppen → körs kanske aldrig
- `do/while` kollar **efter** → körs **alltid minst en gång**
- Glöm inte `;` efter `while (...)` i en do/while

---

## Fråga 6/7 — foreach

Varför kompilerar inte det här?

```csharp
string[] namn = { "Alex", "Sam", "Kim" };

foreach (string n in namn)
{
    n = n.ToUpper();
}
```

---

## ✅ Svar 6/7

`n` är en **skrivskyddad iterationsvariabel** — den går inte att tilldela.

Vill du ändra elementen, använd `for`:

```csharp
for (int i = 0; i < namn.Length; i++)
{
    namn[i] = namn[i].ToUpper();
}
```

`foreach` = enklast för att **läsa** en samling.

---

## Fråga 7/7 — break och continue

Vad skrivs ut?

```csharp
for (int i = 1; i <= 5; i++)
{
    if (i == 2) continue;
    if (i == 4) break;
    Console.Write(i + " ");
}
```

---

## ✅ Svar 7/7

```text
1 3
```

- `i == 2` → `continue` hoppar över resten av **det här varvet**
- `i == 4` → `break` **avslutar hela loopen**

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `loopar_marp.md`.
