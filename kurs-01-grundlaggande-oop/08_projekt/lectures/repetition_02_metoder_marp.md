---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Metoder

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: uppbyggnad · åtkomst · returtyp · parametrar · scope · namngivning

---

## Fråga 1/7 — Metodens delar

Vad heter delarna ①–⑤?

```csharp
public static int Addera(int a, int b)
{
    return a + b;
}
//  ①      ②    ③    ④       ⑤
```

---

## ✅ Svar 1/7

| # | Del | Betyder |
|---|-----|---------|
| ① | **Åtkomst** | vem får anropa metoden (`public`, `private`…) |
| ② | `static` | tillhör klassen, inte ett objekt |
| ③ | **Returtyp** ("ut") | vilken typ som kommer tillbaka (`void` = inget) |
| ④ | **Namn** | PascalCase, börjar med verb |
| ⑤ | **Parametrar** ("in") | typ + namn, separerade med komma |

Sedan kommer **kroppen** i `{ }`.

---

## Fråga 2/7 — Åtkomst

Vad är skillnaden mellan `public` och `private` på en metod?

Och vad händer om du **inte** skriver någon åtkomstmodifierare på en metod i en klass?

---

## ✅ Svar 2/7

- `public` — kan anropas **utifrån**, från andra klasser
- `private` — kan bara anropas **inuti** den egna klassen
- Utelämnas den blir metoden **`private`**

Bonus: `protected` = den egna klassen **och** subklasser (kommer i arv).

> 💬 _"Börja privat. Öppna bara det andra behöver."_

---

## Fråga 3/7 — void eller returtyp?

Vilken returtyp ska stå på strecken?

```csharp
static _____ VisaMeny()                 // skriver ut en meny
static _____ ÄrJämnt(int tal)           // ja eller nej
static _____ BeräknaMoms(double pris)   // räknar ut 25 %
```

---

## ✅ Svar 3/7

```csharp
static void   VisaMeny()                // gör något, ger inget tillbaka
static bool   ÄrJämnt(int tal)          // svarar sant/falskt
static double BeräknaMoms(double pris)  // ger tillbaka ett decimaltal
```

- `void` = skrivare · returtyp = miniräknare
- Har metoden en returtyp **måste** den `return`:a ett värde av den typen

---

## Fråga 4/7 — Parameter eller argument?

```csharp
static int Addera(int a, int b)
{
    return a + b;
}

int summa = Addera(3, 5);
```

Vilka är **parametrar** och vilka är **argument**?

---

## ✅ Svar 4/7

- **Parametrar:** `a` och `b` — variablerna som **tar emot** i metoden
- **Argument:** `3` och `5` — de **faktiska värdena** du skickar in

Argumenten måste passa parametrarna i **antal, typ och ordning**.

---

## Fråga 5/7 — Vad skrivs ut?

```csharp
static int Dubbla(int x)
{
    return x * 2;
}

Console.WriteLine(Dubbla(Dubbla(3)));
```

---

## ✅ Svar 5/7

```text
12
```

1. Inre anropet först: `Dubbla(3)` → `6`
2. Yttre anropet: `Dubbla(6)` → `12`

`return` skickar tillbaka värdet **och avslutar metoden direkt**.

---

## Fråga 6/7 — Hitta felen

```csharp
static void SkrivHälsning(string namn)
{
    Console.WriteLine("Hej, " + namn);
    int längd = namn.Length;
}

static void Main()
{
    int svar = SkrivHälsning("Alex");   // (1)
    Console.WriteLine(längd);           // (2)
}
```

---

## ✅ Svar 6/7

**(1)** `void` returnerar inget — det går inte att spara i `int`.
Ta bort tilldelningen, eller ändra metodens returtyp.

**(2)** `längd` skapades **inuti** `SkrivHälsning` — den dör när metoden är klar (**scope**).
Vill du använda den i `Main`: `return` den och ta emot den.

---

## Fråga 7/7 — Bästa namnet

Metoden kollar om ett tal är ett primtal och svarar sant/falskt. Vilket namn är bäst?

- A) `primtal`
- B) `Kolla`
- C) `ÄrPrimtal`
- D) `metod1`

---

## ✅ Svar 7/7

**C) `ÄrPrimtal`**

- **PascalCase** + **verb**
- `Är` / `Har` för metoder som returnerar `bool`
- Namnet berättar vad metoden gör — utan att du läser kroppen

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `metoder_marp.md`.
