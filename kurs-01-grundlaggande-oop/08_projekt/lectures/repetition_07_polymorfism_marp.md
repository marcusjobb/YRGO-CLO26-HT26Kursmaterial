---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Repetition: Polymorfism

### 7 frågor — frågan på en slide, svaret på nästa

_Kurs 01 · Repetition inför tentan · Nion Education_

---

## Så funkar quizet

- Frågan visas på en slide
- **Tänk själv först** — säg svaret högt eller skriv ner det
- Nästa slide visar svaret och förklaringen

Ämne: `virtual` · `override` · deklarerad vs faktisk typ · `List<Djur>`

---

## Förutsättning — klasserna

Alla frågor utgår från de här klasserna:

```csharp
class Djur
{
    public string Namn { get; }
    public Djur(string namn) { Namn = namn; }
    public virtual void LåtaLjud() => Console.WriteLine("Djuret låter...");
}
class Hund : Djur
{
    public Hund(string n) : base(n) { }
    public override void LåtaLjud() => Console.WriteLine("Voff!");
}
class Katt : Djur
{
    public Katt(string n) : base(n) { }
    public override void LåtaLjud() => Console.WriteLine("Mjau!");
}
class Kanin : Djur { public Kanin(string n) : base(n) { } }
```

---

## Fråga 1/7 — Vad betyder ordet?

Vad betyder **polymorfism**?

Vad är "samma" och vad är "olika" i ett polymorft anrop?

---

## ✅ Svar 1/7

**Poly** = många, **morf** = form → "många former".

- **Samma** anrop: `LåtaLjud()`
- **Olika** resultat — beroende på vilket objekt det faktiskt är

Det är ingen ny syntax — det är en **konsekvens** av `virtual` / `override`.

---

## Fråga 2/7 — Deklarerad vs faktisk typ

Vad skrivs ut? Vilken är den **deklarerade** och vilken den **faktiska** typen?

```csharp
Djur d = new Hund("Fido");
d.LåtaLjud();
```

---

## ✅ Svar 2/7

```text
Voff!
```

- **Deklarerad typ:** `Djur` (vad variabeln är)
- **Faktisk typ:** `Hund` (vad objektet är)
- Vid körning väljs metoden efter den **faktiska** typen

---

## Fråga 3/7 — virtual och override

Vilka nyckelord krävs, och var?

Vad händer om du skriver `override` i `Hund` men **glömmer** `virtual` i `Djur`?

---

## ✅ Svar 3/7

- `virtual` i **basklassen** — "den här metoden får skrivas över"
- `override` i **subklassen** — "jag skriver över den"

Glömmer du `virtual` → **kompileringsfel**: det går inte att `override`:a en icke-virtuell metod.

Glömmer du bara `override` körs basens version via en `Djur`-variabel.

---

## Fråga 4/7 — Lista med olika djur

Vad skrivs ut?

```csharp
List<Djur> djur = new List<Djur>
{
    new Hund("Fido"),
    new Katt("Misse"),
    new Kanin("Kalle")
};

foreach (Djur d in djur)
{
    d.LåtaLjud();
}
```

---

## ✅ Svar 4/7

```text
Voff!
Mjau!
Djuret låter...
```

- En `List<Djur>` kan hålla alla subtyper
- `Kanin` har **ingen** `override` → basklassens `LåtaLjud()` körs

---

## Fråga 5/7 — Varför polymorfism?

Vad är problemet med den här koden — och hur löser polymorfism det?

```csharp
foreach (Djur d in djur)
{
    if (d is Hund)      Console.WriteLine("Voff!");
    else if (d is Katt) Console.WriteLine("Mjau!");
}
```

---

## ✅ Svar 5/7

Varje **nytt djur** kräver att du ändrar loopen — lätt att glömma, lätt att göra fel.

Med polymorfism räcker `d.LåtaLjud();`. Ny subklass = ny `override`, **loopen ändras aldrig**.

> 💬 _"Öppen för utökning, stängd för ändring."_

---

## Fråga 6/7 — Kompileringsfel

Varför kompilerar inte det här? (`Apportera()` finns bara i `Hund`.)

```csharp
Djur d = new Hund("Fido");
d.Apportera();
```

---

## ✅ Svar 6/7

Kompilatorn tittar på den **deklarerade** typen (`Djur`), och `Djur` har ingen `Apportera()`.

```csharp
if (d is Hund h)
{
    h.Apportera();
}
```

Testa typen med `is` och använd sedan den mer specifika variabeln.

---

## Fråga 7/7 — base i override

`Hund` ser nu ut så här. Vad skrivs ut?

```csharp
class Hund : Djur
{
    public Hund(string n) : base(n) { }

    public override void LåtaLjud()
    {
        base.LåtaLjud();
        Console.WriteLine("Voff!");
    }
}

Djur d = new Hund("Fido");
d.LåtaLjud();
```

---

## ✅ Svar 7/7

```text
Djuret låter...
Voff!
```

- `override` körs (faktisk typ = `Hund`)
- `base.LåtaLjud()` anropar först **basklassens** version
- Sedan fortsätter `Hund` med sitt eget

---

<!-- _class: title -->

# Klart! 🎉

Missade du någon fråga? Läs om den i `polymorfism_marp.md`.
