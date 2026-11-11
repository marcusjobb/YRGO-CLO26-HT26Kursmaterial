---
marp: true
theme: nion-dark
paginate: true
---

# Arv

**Kurs:** OOP i C# Grund
**Modul:** 05 — Klasser, properties och inkapsling

---

## Vad ska vi lära oss idag?

- Vad är arv och varför behövs det?
- Hur skriver man en basklass och en subklass i C#?
- Vad är `virtual` och `override`?
- Hur anropar man basklassens konstruktor med `base(...)`?

---

## Kom ihåg Husdjuren?

Du byggde `Hund` och `Katt` var för sig:

```csharp
class Hund
{
    private string _namn;
    public Hund(string namn) { _namn = namn; }
    public void Presentera() => Console.WriteLine($"Jag heter {_namn}.");
    public void LåtaLjud()  => Console.WriteLine("Voff!");
}
```

```csharp
class Katt
{
    private string _namn;
    public Katt(string namn) { _namn = namn; }
    public void Presentera() => Console.WriteLine($"Jag heter {_namn}.");
    public void LåtaLjud()  => Console.WriteLine("Mjau!");
}
```

---

## Problemet med det

`Presentera()` är **exakt samma** i båda klasserna — rad för rad.

Lägg till `_ålder`? Ändra på **två ställen**.  
Vill Linnea ha kaniner också? En klass till med samma `Presentera()`.

Det här kallas ett **DRY-brott** — Don't Repeat Yourself.  
Arv är lösningen.

---

## Lösningen: en basklass

Flytta det som är gemensamt till en **basklass**:

Basklass är en helt vanlig klass, men den använder sig av speciella markörer för metoder och variabler.

**private** : Bara basklassen har tillgång till det
**protected** : Som private men tillgänglig för subklasser
**public** : Alla har tillgång till det

---

## Basklassen

```csharp
class Djur
{
    public string Namn { get; private set; }
    public Djur(string namn)
    {
        Namn = namn;
    }
    public void Presentera()
    {
        Console.WriteLine($"Jag heter {Namn}.");
    }
    public virtual void LåtaLjud() { }
}
```

`Presentera()` skrivs **en gång**.  
`virtual` markerar att subklasser får skriva sin egen version av `LåtaLjud`.

---

## Subklasserna ärver

En subklass är ett "barn" till basklassen — den ärver allt som inte är `private`.

`Presentera()` finns i `Djur`. `Hund` och `Katt` får den gratis — ingen kopiering.

---

## Virtual — öppen för arv, stängd för ändringar

Regeln är "Öppen för arv, stängd för ändringar"

**Scenario:** Vi har djur som har olika beteenden, samma grund, men de agerar annorlunda. För att slippa en massa if-satser i basklassen så skapar vi olika klasser av djur, som ärver basklassen.

---

## Tillbaka till djuren

```csharp
class Djur
{
    public virtual void LåtaLjud() { }   // vet inte — gör ingenting
}

class Hund : Djur
{
    public override void LåtaLjud() => Console.WriteLine("Voff!");
}

class Katt : Djur
{
    public override void LåtaLjud() => Console.WriteLine("Mjau!");
}
```

---

## Voffsing

```csharp
class Hund : Djur
{
    public Hund(string namn) : base(namn) { }
    public override void LåtaLjud()
    {
        Console.WriteLine("Voff!");
    }
}
```

`: Djur` — Hund är en Djur.  
`: base(namn)` — anropar basklassens konstruktor.  
`override` — skriver över basklassens `LåtaLjud`.

---

## Meow

```csharp
class Katt : Djur
{
    public Katt(string namn) : base(namn) { }
    public override void LåtaLjud()
    {
        Console.WriteLine("Mjau!");
    }
}
```

`: Djur` — Katt är en Djur.  
`: base(namn)` — anropar basklassens konstruktor.  
`override` — skriver över basklassens `LåtaLjud`.

---

## base(...) — konstruktorkedjan

Varje subklass måste sätta i gång basklassens konstruktor:

```csharp
class Djur
{
    public string Namn { get; private set; }

    public Djur(string namn)
    {
        Namn = namn;
    }
}

class Hund : Djur
{
    public Hund(string namn) : base(namn) { }
}
```

`: base(namn)` skickar `namn` upp till `Djur`.  
Utan det vet inte `Djur` vad `Namn` ska vara.

---

## Syntax på tavlan

```
┌──────────────────────────────┐
│           Djur               │  ← basklass
├──────────────────────────────┤
│ + Namn : string              │
├──────────────────────────────┤
│ + Djur(namn)                 │
│ + Presentera()               │
│ + virtual LåtaLjud()         │
└──────────────────────────────┘
         ▲           ▲
         │           │
┌────────────┐  ┌────────────┐
│    Hund    │  │    Katt    │  ← subklasser
├────────────┤  ├────────────┤
│ override   │  │ override   │
│ LåtaLjud   │  │ LåtaLjud   │
└────────────┘  └────────────┘
```

Pilen pekar uppåt — subklassen ärver från basklassen.

---

## Sätt ihop det i Main

```csharp
Hund hund = new Hund("Fido");
Katt katt = new Katt("Luna");

hund.Presentera();   // ärvd från Djur
hund.LåtaLjud();    // Hunds egen override

katt.Presentera();   // ärvd från Djur
katt.LåtaLjud();    // Katts egen override
```

Output:
```
Jag heter Fido.
Voff!
Jag heter Luna.
Mjau!
```

---

## Lat kodare

Resaultatet är att vi får olika djur utan att behöva skriva en massa kod, 

**Yay!**

---

## Lägg till ett nytt djur — en rad

```csharp
class Kanin : Djur
{
    public Kanin(string namn) : base(namn) { }

    public override void LåtaLjud()
    {
        Console.WriteLine("Nöff!");
    }
}
```

`Presentera()` fungerar direkt — ingen ändring någonstans.  
Det är poängen med arv.

---

## virtual vs override

| Nyckelord | Var? | Vad gör det? |
|-----------|------|--------------|
| `virtual` | Basklassen | "Subklasser får skriva sin egen version" |
| `override` | Subklassen | "Jag skriver min egen version" |

Om du glömmer `virtual` i basklassen → `override` fungerar inte.  
Om du glömmer `override` i subklassen → basklassens version körs.

---

## Tre nyckelord att komma ihåg

```csharp
class Hund : Djur           // arv — Hund är en Djur
{
    public Hund(string namn)
        : base(namn) { }    // kedja konstruktorer

    public override void LåtaLjud()  // skriv över virtual-metod
    {
        Console.WriteLine("Voff!");
    }
}
```

`: Djur` · `: base(...)` · `override`

---

## Husdjurens tur — nu med arv

Öppna `ovning_pets.md` och läs "Kika framåt" längst ner.

Ändra din kod:
1. Skapa klassen `Djur` som basklass
2. Låt `Hund` och `Katt` ärva från `Djur`
3. Flytta `Presentera()` till `Djur`
4. Gör `LåtaLjud()` virtual i `Djur`, override i subklasserna
5. Lägg till en `Kanin` — se hur lite kod du behöver

---

## Klar? Testa dig själv

Utan att kolla:

1. Vad heter nyckelordet som markerar att subklasser får skriva sin egen version?
2. Vad gör `: base(namn)` i konstruktorn?
3. Vad händer om du lägger till `class Orm : Djur` men inte skriver `override LåtaLjud()`?
