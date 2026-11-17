---
marp: true
theme: nion-dark
paginate: true
---

# Arv + överlagring — tips och tricks

**Kurs:** OOP i C# Grund
**Modul:** 05 — Klasser, properties och inkapsling

---

## Vad tar vi upp nu?

1. Metodöverlagring — samma namn, olika parametrar
2. Konstruktoröverlagring + `this(...)` — samma sak fast för konstruktorer
3. `base.MetodNamn()` — bygg vidare på basklassens implementation
4. `ToString()` — den metod alla klasser ärver men sällan skriver om

---

## Metodöverlagring — samma namn, olika parametrar

En metod kan ha samma namn men ta emot olika saker:

```csharp
class Hälsning
{
    public string Hälsa(string namn)
    {
        return $"Hej, {namn}!";
    }

    public string Hälsa(string namn, string titel)
    {
        return $"God dag, {titel} {namn}!";
    }

    public string Hälsa()
    {
        return "Hej på dig!";
    }
}
```

C# väljer rätt version baserat på vad du skickar in.  
Det kallas **överlagring** — overloading.

---

## Metodöverlagring — i praktiken

```csharp
Hälsning h = new Hälsning();
Console.WriteLine(h.Hälsa("Alex"));
Console.WriteLine(h.Hälsa("Medina", "doktor"));
Console.WriteLine(h.Hälsa());
```

Output:
```
Hej, Alex!
God dag, doktor Medina!
Hej på dig!
```

Samma metodnamn. Tre varianter. C# väljer rätt automatiskt.

---

## Konstruktoröverlagring — samma sak, för konstruktorer

En klass kan ha **flera konstruktorer** med olika parametrar:

```csharp
class Bil
{
    public string Märke { get; private set; }
    public int Dörrar { get; private set; }

    public Bil(string märke, int dörrar)   // full konstruktor
    {
        Märke = märke;
        Dörrar = dörrar;
    }

    public Bil(string märke)               // förenklad — 4 dörrar som standard
        : this(märke, 4) { }
}
```

`this(märke, 4)` anropar den **fulla konstruktorn i samma klass**.  
Du slipper skriva samma initiering på två ställen.

---

## this(...) vs base(...)

| | `this(...)` | `base(...)` |
|---|---|---|
| **Anropar** | Konstruktor i **samma klass** | Konstruktor i **basklassen** |
| **Används för** | Konstruktoröverlagring | Arv |

```csharp
class Bil
{
    public Bil(string märke, int dörrar) { ... }
    public Bil(string märke) : this(märke, 4) { }  // this
}

class Elbil : Bil
{
    public Elbil(string märke) : base(märke, 4) { }  // base
}
```

`this` = annan konstruktor i mig själv  
`base` = konstruktorn hos min förälder

---

## base.MetodNamn() — bygg vidare istället för att ersätta

Ibland vill du inte **ersätta** basklassens implementation — du vill **utöka** den.

```csharp
class Djur
{
    public string Namn { get; private set; }

    public Djur(string namn) { Namn = namn; }

    public virtual void Presentera()
    {
        Console.WriteLine($"Jag heter {Namn}.");
    }
}

class Hund : Djur
{
    public Hund(string namn) : base(namn) { }

    public override void Presentera()
    {
        base.Presentera();                         // kör Djurs version först
        Console.WriteLine("Och jag är en hund!");  // lägg till mer
    }
}
```

Output: `Jag heter Fido.` → `Och jag är en hund!`

---

## Varför base.Metod()?

Utan `base.Presentera()` måste du skriva om hela presentationen i varje subklass.  
Med den håller du logiken på ett ställe — och lägger bara till det som är specifikt.

```
Djur.Presentera()   → "Jag heter [namn]."
Hund.Presentera()   → base + "Och jag är en hund!"
Katt.Presentera()   → base + "Och jag är en katt!"
```

Ändrar du basklassen? Alla subklasser uppdateras automatiskt.

---

## Method overloading — samma namn, olika parametrar

En metod kan ha samma namn men ta emot olika typer eller antal parametrar:

```csharp
class Hälsning
{
    public string Hälsa(string namn)
    {
        return $"Hej, {namn}!";
    }

    public string Hälsa(string namn, string titel)
    {
        return $"God dag, {titel} {namn}!";
    }

    public string Hälsa()
    {
        return "Hej på dig!";
    }
}
```

```csharp
Hälsning h = new Hälsning();
Console.WriteLine(h.Hälsa("Alex"));
Console.WriteLine(h.Hälsa("Medina", "doktor"));
Console.WriteLine(h.Hälsa());
```

---

## Overloading vs override — olika saker

| | Overloading | Override |
|---|---|---|
| **Vad?** | Samma namn, olika parametrar | Samma signatur, ny implementation |
| **Var?** | Samma klass | Subklass |
| **Nyckelord** | Inget extra | `virtual` + `override` |

Overloading = "metoden kan ta emot olika saker"  
Override = "subklassen gör det annorlunda"

---

## ToString() — den dolda metoden

Alla klasser i C# ärver från `object` — och `object` har en metod `ToString()`.

Som standard skriver den ut klassens namn: `CLO26.Hund`  
Men du kan skriva om den:

```csharp
class Hund : Djur
{
    public Hund(string namn) : base(namn) { }

    public override string ToString()
    {
        return $"Hund({Namn})";
    }
}
```

```csharp
Hund h = new Hund("Fido");
Console.WriteLine(h);          // anropar ToString() automatiskt
Console.WriteLine($"Djuret: {h}");  // string interpolation gör samma sak
```

Output: `Hund(Fido)`

---

## ToString() — varför är det användbart?

`Console.WriteLine(objekt)` anropar `ToString()` automatiskt.  
`$"...{objekt}..."` gör samma sak.

```csharp
List<Djur> djur = new List<Djur> { new Hund("Fido"), new Katt("Luna") };

foreach (var d in djur)
{
    Console.WriteLine(d);  // ToString() på varje objekt
}
```

Output:
```
Hund(Fido)
Katt(Luna)
```

En liten metod — stor nytta vid debugging och utskrift.

---

## Tre saker att ta med sig

```csharp
// 1. base.Metod() — bygg vidare på basklassens implementation
public override void Presentera()
{
    base.Presentera();
    Console.WriteLine("Extra info här.");
}

// 2. Overloading — samma namn, olika parametrar
public void Logga(string text) { ... }
public void Logga(string text, int nivå) { ... }

// 3. ToString() override — objekt som skriver ut sig själva
public override string ToString() => $"Hund({Namn})";
```

---

## Dags att prova!

Öppna eftermiddagsövningarna — `ovning_arv_fordjupning.md`

---
