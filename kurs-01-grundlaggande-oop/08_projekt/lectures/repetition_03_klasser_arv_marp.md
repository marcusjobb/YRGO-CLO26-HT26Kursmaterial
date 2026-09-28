---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Klasser och arv

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: klass · objekt · konstruktor · inkapsling · arv · `base`

---

## Fråga 1/7 — Klass vs objekt

Vad är skillnaden mellan en klass och ett objekt?

```csharp
class Hund { }

Hund h1 = new Hund();
Hund h2 = new Hund();
```

Hur många objekt finns här, och hur många klasser?

---

## ✅ Svar 1/7

- **Klassen** är ritningen/mallen — här **1** klass
- **Objektet** är en konkret instans skapad med `new` — här **2** objekt

Från en klass kan du skapa hur många objekt du vill.

---

## Fråga 2/7 — Klassens uppbyggnad

Vad kallas ①–④?

```csharp
class Hund
{
    private string _namn;                    // ①
    public int Ålder { get; set; }           // ②

    public Hund(string namn)                 // ③
    {
        _namn = namn;
    }

    public void Skälla()                     // ④
    {
        Console.WriteLine(_namn + " säger Voff!");
    }
}
```

---

## ✅ Svar 2/7

| # | Del | Uppgift |
|---|-----|---------|
| ① | **Fält** | lagrar objektets data (oftast `private`) |
| ② | **Property** | kontrollerad åtkomst till data (`get` / `set`) |
| ③ | **Konstruktor** | sätter startläget när objektet skapas |
| ④ | **Metod** | vad objektet kan göra |

---

## Fråga 3/7 — Konstruktorn

Ge **tre kännetecken** för en konstruktor.

Vad händer när raden körs?

```csharp
Hund h = new Hund("Fido");
```

---

## ✅ Svar 3/7

1. **Samma namn** som klassen
2. **Ingen returtyp** — inte ens `void`
3. Körs **automatiskt** vid `new`

`new Hund("Fido")` → minne reserveras, konstruktorn körs, `_namn` får värdet `"Fido"`.

Du kan ha flera konstruktorer med olika parametrar (**överlagring**).

---

## Fråga 4/7 — Inkapsling

Vad är problemet här, och hur löser du det?

```csharp
class Konto
{
    public decimal Saldo;
}

konto.Saldo = -5000;
```

---

## ✅ Svar 4/7

Vem som helst kan sätta ett **ogiltigt värde**. Klassen kan inte skydda sig själv.

```csharp
class Konto
{
    private decimal _saldo;

    public decimal Saldo
    {
        get => _saldo;
        set { if (value >= 0) _saldo = value; }
    }
}
```

**Inkapsling** = privat data + kontrollerad åtkomst.

---

## Fråga 5/7 — Arvssyntax

Hur skriver du att `Hund` ärver från `Djur`?

Vad kallas de två klasserna?

Kan en klass i C# ärva från **två** klasser?

---

## ✅ Svar 5/7

```csharp
class Hund : Djur { }
```

- `Djur` = **basklass** (förälder)
- `Hund` = **subklass** (barn)
- Subklassen ärver `public` och `protected` medlemmar — inte `private`
- C# tillåter **bara en** basklass

---

## Fråga 6/7 — Konstruktorkedjan

Varför kompilerar inte det här?

```csharp
class Djur
{
    public Djur(string namn) { /* ... */ }
}

class Hund : Djur
{
    public Hund(string namn) { }
}
```

---

## ✅ Svar 6/7

`Djur` har **ingen parameterlös konstruktor**, och `Hund` anropar ingen explicit.
Basklassens konstruktor måste köras — och den behöver ett `namn`.

```csharp
class Hund : Djur
{
    public Hund(string namn) : base(namn) { }
}
```

`: base(...)` skickar argumenten vidare till basklassen.

---

## Fråga 7/7 — Är-en eller har-en?

Vilka av paren ska vara **arv**?

- `Hund` – `Djur`
- `Bil` – `Motor`
- `Student` – `Person`
- `Lärare` – `Klassrum`

---

## ✅ Svar 7/7

**Arv:** `Hund`–`Djur` och `Student`–`Person`

- Testet: **"X är ett Y"** → arv
- `Bil` **har en** `Motor`, `Lärare` **har ett** `Klassrum` → en **medlem** i klassen, inte arv

```csharp
class Bil { private Motor _motor; }
```

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `klasser_och_objekt_marp.md` och `arv_marp.md`.
