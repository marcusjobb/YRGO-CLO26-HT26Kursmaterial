---
marp: true
theme: nion-dark
paginate: true
---

# Polymorfism

**Kurs:** OOP i C# Grund
**Modul:** 05 — Klasser, properties och inkapsling

---

## Vad ska vi lära oss idag?

- Vad betyder "polymorfism" egentligen?
- Skillnaden på deklarerad typ och faktisk typ
- Hur en `List<Djur>` kan innehålla `Hund`, `Katt` och `Kanin` samtidigt
- Varför du sällan behöver `if`/`else if` för att skilja på typer

---

## Snabb repetition från förra passet

```csharp
class Djur
{
    public virtual void LåtaLjud() { }
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

`virtual` + `override` — det byggde vi förra gången. Idag: vad det ger oss.

---

## Ordet betyder "många former"

**Poly** = många, **morf** = form.

Samma anrop — `LåtaLjud()` — men olika resultat beroende på **vilket objekt** det faktiskt är.

Det är inte en ny syntax. Det är en **konsekvens** av `virtual`/`override`.

---

## Deklarerad typ vs faktisk typ

```csharp
Djur mittDjur = new Hund("Fido");
```

- **Deklarerad typ** (vad variabeln är typad som): `Djur`
- **Faktisk typ** (vad objektet verkligen är): `Hund`

C# håller reda på båda. När du anropar `mittDjur.LåtaLjud()` körs **Hunds** version — inte Djurs — trots att variabeln är typad som `Djur`.

---

## Testa det

```csharp
Djur mittDjur = new Hund("Fido");
mittDjur.LåtaLjud();   // Voff! — inte tomt, trots Djur-typen
```

Detta kallas **uppcastning** — en `Hund` behandlas som en `Djur`.
Funkar eftersom `Hund : Djur` — en Hund *är* en Djur.

---

## Var blir det här användbart?

Tänk dig att du vill hålla koll på **alla** djur på gården — hundar, katter, kaniner — i **en enda lista**.

Utan polymorfism: tre separata listor, tre separata loopar, mycket kod.

Med polymorfism: **en lista**, **en loop**.

---

## En lista, olika typer

```csharp
List<Djur> gården = new List<Djur>
{
    new Hund("Fido"),
    new Katt("Luna"),
    new Kanin("Snurre")
};

foreach (Djur djur in gården)
{
    djur.Presentera();
    djur.LåtaLjud();
}
```

---

## Vad händer i loopen?

```
Jag heter Fido.
Voff!
Jag heter Luna.
Mjau!
Jag heter Snurre.
Nöff!
```

Samma två rader kod (`Presentera()`, `LåtaLjud()`) — men varje varv i loopen kör **rätt** version, automatiskt.

`foreach` vet bara att det är `Djur`. Den bryr sig inte om vilken sorts djur.

---

## Utan polymorfism hade det sett ut så här

```csharp
foreach (Djur djur in gården)
{
    if (djur is Hund) Console.WriteLine("Voff!");
    else if (djur is Katt) Console.WriteLine("Mjau!");
    else if (djur is Kanin) Console.WriteLine("Nöff!");
}
```

Fungerar — men **varje ny djurtyp** kräver en ny `if`-gren här, i varenda loop, överallt i kodbasen.

---

## Med polymorfism

```csharp
foreach (Djur djur in gården)
{
    djur.LåtaLjud();
}
```

Lägg till `Orm : Djur` med sin egen `LåtaLjud()`. **Den här loopen ändras aldrig.**
Det är hela vinsten: ny funktionalitet utan att röra gammal kod.

---

## Öppen för arv, stängd för ändring — igen

Samma regel som förra lektionen, men nu ser du **varför** den är värd något:

- **Öppen för arv:** lägg till `Orm`, `Häst`, `Get` — hur många subklasser som helst
- **Stängd för ändring:** loopen, listan, anropen — allt som redan finns rör du inte

---

## Syntax på tavlan

```
List<Djur> gården = { Hund, Katt, Kanin }
                        │      │      │
                        ▼      ▼      ▼
                  foreach (Djur djur in gården)
                        │
                        ▼
              djur.LåtaLjud()  ← körs olika beroende på faktisk typ
```

Deklarerad typ (`Djur`) styr **vad du får skriva**.
Faktisk typ (`Hund`/`Katt`/`Kanin`) styr **vad som faktiskt körs**.

---

## Varför just nu — Skogsäventyret

I gruppuppgiften kommer ni ha samlingar som `List<Monster>` eller `List<Item>`.

Ett `Skelett` och en `Drake` är båda `Monster` — men attackerar olika.
Ett `Svärd` och en `Trollstav` är båda `Item` — men gör olika saker när de används.

Exakt samma mönster som `List<Djur>`: **en** bassklass, **en** loop, olika beteenden.

---

## Samma mönster, spel-exempel

```csharp
class Monster
{
    public string Namn { get; }
    public Monster(string namn) => Namn = namn;
    public virtual void Attackera() { }
}

class Skelett : Monster
{
    public Skelett(string namn) : base(namn) { }
    public override void Attackera() => Console.WriteLine($"{Namn} kastar ben!");
}

class Drake : Monster
{
    public Drake(string namn) : base(namn) { }
    public override void Attackera() => Console.WriteLine($"{Namn} andas eld!");
}
```

---

## Samma loop, spel-exempel

```csharp
List<Monster> fiender = new List<Monster>
{
    new Skelett("Ben-Bosse"),
    new Drake("Eldvinge")
};

foreach (Monster m in fiender)
{
    m.Attackera();
}
```

Lägg till en tredje monstertyp i er kod — den här loopen ändras fortfarande aldrig.

---

## Husdjurens tur — polymorfism

Öppna din `Hund`/`Katt`/`Kanin`-kod från förra passet.

1. Skapa en `List<Djur>` med minst tre olika djur
2. Loopa igenom listan med `foreach (Djur djur in ...)`
3. Anropa `Presentera()` och `LåtaLjud()` på varje djur i loopen
4. Lägg till ett fjärde djur (egen klass) — lägg **inte till** någon ny kod i loopen
5. Kör om — se att det nya djuret redan fungerar

---

## Klar? Testa dig själv

Utan att kolla:

1. Vad betyder orden "poly" och "morf" i "polymorfism"?
2. Om `Djur mittDjur = new Katt("Mia");` — vad är deklarerad typ, vad är faktisk typ?
3. Varför slipper du `if (djur is Hund) ...` när du använder polymorfism?
