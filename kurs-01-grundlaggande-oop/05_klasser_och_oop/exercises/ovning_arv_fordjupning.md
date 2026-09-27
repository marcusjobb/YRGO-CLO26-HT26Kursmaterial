# Övning — Arv fördjupning

🟡 Mellannivå → 🔴 Utmaning

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Del 1 — base.Metod() i praktiken

Utgå från din `Djur`-klass från förmiddagen.

Gör `Presentera()` virtual i `Djur`. Låt `Hund` overrida den och:
1. Anropa `base.Presentera()` — Djur-versionen körs först
2. Lägg till en rad: `"Svans: alltid igång."`

`Katt` gör samma sak men skriver `"Svans: enligt humör."`

**Exempeloutput:**
```
Jag heter Fido och är 5 år gammal.
Svans: alltid igång.
Jag heter Luna och är 5 år gammal.
Svans: enligt humör.
```

---

## Del 2 — ToString() override

Lägg till `ToString()` i `Djur`:

```csharp
public override string ToString()
{
    return $"{GetType().Name}({Namn}, {ÅlderIÅr} år)";
}
```

`GetType().Name` returnerar klassens namn — `"Hund"` eller `"Katt"` — utan att du behöver skriva det manuellt.

Testa:
```csharp
List<Djur> djur = new List<Djur>
{
    new Hund("Fido", 5),
    new Katt("Luna", 5),
    new Kanin("Nisse", 3)
};

foreach (var d in djur)
{
    Console.WriteLine(d);
}
```

**Exempeloutput:**
```
Hund(Fido, 5 år)
Katt(Luna, 5 år)
Kanin(Nisse, 3 år)
```

---

## Del 3 — Overloading på Presentera

Lägg till en **overloadad** version av `Presentera` i `Djur` som tar en bool `visa_ålder`:

- `Presentera(true)` — skriver ut namn och ålder
- `Presentera(false)` — skriver bara ut namn

```csharp
djur.Presentera(true);   // "Jag heter Fido och är 5 år gammal."
djur.Presentera(false);  // "Jag heter Fido."
```

Tips: du har redan `Presentera()` — nu lägger du till en version med en parameter.  
De kan leva sida vid sida i samma klass.

---

## Frågor

Svara som kommentarer i koden:

1. Vad är skillnaden mellan att anropa `base.Presentera()` och att kopiera koden från basklassen?
2. Varför returnerar `GetType().Name` rätt klassnamn (`"Hund"`, `"Katt"`) när `ToString()` är definierad i `Djur`?
3. Är `Presentera()` och `Presentera(bool)` overloading eller override — och vad är skillnaden?

---

## Utmaning 🔴 — Djursjukhuset

Beatrice vill ha ett journalsystem. Bygg en klass `Journal` med:

**Privat fält:**
- `_poster` : `List<string>`

**Metoder:**
```csharp
// Overloading — två sätt att lägga till
public void LäggTill(Djur djur)          // lägger till djurets ToString()
public void LäggTill(string anteckning)  // lägger till fritext

// Skriver ut alla poster
public void SkrivUt()
```

**Testa:**
```csharp
Journal journal = new Journal();
journal.LäggTill(new Hund("Fido", 5));
journal.LäggTill(new Katt("Luna", 3));
journal.LäggTill("Fido vaccinerades 2026-09-16.");
journal.SkrivUt();
```

**Exempeloutput:**
```
Hund(Fido, 5 år)
Katt(Luna, 3 år)
Fido vaccinerades 2026-09-16.
```

---

<details>
<summary>Lösningsförslag — Del 1</summary>

```csharp
class Djur
{
    public string Namn { get; private set; }
    public int ÅlderIÅr { get; private set; }

    public Djur(string namn, int ålder) { Namn = namn; ÅlderIÅr = ålder; }

    public virtual void Presentera()
    {
        Console.WriteLine($"Jag heter {Namn} och är {ÅlderIÅr} år gammal.");
    }
}

class Hund : Djur
{
    public Hund(string namn, int ålder) : base(namn, ålder) { }

    public override void Presentera()
    {
        base.Presentera();
        Console.WriteLine("Svans: alltid igång.");
    }
}
```

</details>

<details>
<summary>Lösningsförslag — Del 2 (ToString)</summary>

```csharp
public override string ToString()
{
    return $"{GetType().Name}({Namn}, {ÅlderIÅr} år)";
}
```

</details>

<details>
<summary>Lösningsförslag — Del 3 (overloading)</summary>

```csharp
// I Djur — två versioner lever sida vid sida
public virtual void Presentera()
{
    Console.WriteLine($"Jag heter {Namn} och är {ÅlderIÅr} år gammal.");
}

public void Presentera(bool visaÅlder)
{
    if (visaÅlder)
        Console.WriteLine($"Jag heter {Namn} och är {ÅlderIÅr} år gammal.");
    else
        Console.WriteLine($"Jag heter {Namn}.");
}
```

</details>
