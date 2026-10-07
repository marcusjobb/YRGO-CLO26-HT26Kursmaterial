---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Inkapsling

### Varför fält är privata

_Kurs 01 · Vecka 3 · Nion Education_

---

## Vad händer utan inkapsling?

Tänk dig ett bankkonto utan lås på dörren.

```csharp
// Utan inkapsling — alla kan göra vad de vill
class BankAccount
{
    public double saldo = 1000;   // publikt fält — farligt!
}

// Vem som helst kan sätta saldot till vad som helst
BankAccount konto = new BankAccount();
konto.saldo = -99999;             // ingen koll, ingen validering
```

Ingen kontroll. Inga spärrar. Vad som helst kan hända med dina data.

> 💬 _"Det här är precis vad som kan gå fel. Vi fixar det ihop."_

---

## Lösningen: stäng dörren med `private`

```csharp
class BankAccount
{
    private double saldo;   // privat — ingen utifrån kan röra det
}
```

Fältet `saldo` existerar fortfarande. Men nu är det inlåst.

Ingen kod **utanför** klassen kan läsa eller skriva direkt till det.

Klassen själv bestämmer vad som får hända med sin data.

---

## `private` och `public` — vad är vad?

| Nyckelord | Vem ser det? | Används till |
|-----------|-------------|--------------|
| `private` | Bara klassen själv | Fält, intern logik |
| `public`  | Alla utifrån | Konstruktorer, metoder, properties |

```csharp
class BankAccount
{
    private double saldo;           // bara klassen får röra det
    public string Ägare { get; private set; }   // alla kan läsa, bara klassen skriver
}
```

Enkelt: **privat = inuti, publik = utifrån.**

---

## Property med `private set` — det kontrollerade fönstret

**Gammal stil:**
```csharp
// Så här såg det ut — och så här ser det fortfarande ut i äldre kodbaser
private double _saldo;

public double Saldo
{
    get { return _saldo; }
    private set { _saldo = value; }
}
```

**Modern stil (C# 3+):**
```csharp
// Kortare, men gör exakt samma sak
public double Saldo { get; private set; }
```

Utifrån kan du läsa `konto.Saldo`. Du kan **inte** skriva `konto.Saldo = 999`.

---

## BankAccount — konstruktor och property

```csharp
class BankAccount
{
    public double Saldo { get; private set; }

    public BankAccount(double startSaldo)
    {
        Saldo = startSaldo;
    }
}
```

Konstruktorn är den enda ingångsporten när objektet skapas.

Inga genvägar. Inga bakdörrar.

> 💬 _"Klassen bestämmer själv hur den får skapas — det är designerns jobb."_

---

## Metoder med validering — SättIn och TaUt

```csharp
public void SättIn(double belopp)
{
    if (belopp > 0)
        Saldo += belopp;
}

public bool TaUt(double belopp)
{
    if (belopp > 0 && belopp <= Saldo)
    {
        Saldo -= belopp;
        return true;
    }
    return false;
}
```

Metoden kontrollerar att värdet är rimligt. Ogiltiga operationer avvisas tyst.

---

## Varför `private set` och inte bara `set`?

```csharp
// Med public set — vem som helst kan skriva
public double Saldo { get; set; }
konto.Saldo = 1000000;   // inga hinder alls

// Med private set — bara klassen skriver
public double Saldo { get; private set; }
konto.Saldo = 1000000;   // kompileringsfel — stoppas direkt
```

`private set` ger dig det bästa av två världar:
- Alla kan **läsa** saldot
- Bara klassen kan **ändra** det

---

## Hela BankAccount — alla delar på plats

```csharp
class BankAccount
{
    public double Saldo { get; private set; }

    public BankAccount(double startSaldo)
    {
        Saldo = startSaldo;
    }

    public void SättIn(double belopp)
    {
        if (belopp > 0)
            Saldo += belopp;
    }

    public bool TaUt(double belopp)
    {
        if (belopp > 0 && belopp <= Saldo)
        {
            Saldo -= belopp;
            return true;
        }
        return false;
    }
}
```

---

## Skapa ett objekt — och använd det

```csharp
BankAccount konto = new BankAccount(1000);

Console.WriteLine($"Saldo: {konto.Saldo} kr");   // 1000

konto.SättIn(500);
Console.WriteLine($"Saldo: {konto.Saldo} kr");   // 1500

bool lyckades = konto.TaUt(200);
Console.WriteLine($"Uttag lyckades: {lyckades}");
Console.WriteLine($"Saldo: {konto.Saldo} kr");   // 1300

bool misslyckades = konto.TaUt(9999);
Console.WriteLine($"Uttag lyckades: {misslyckades}");  // False
```

Allt sker via metoderna. Saldot kan aldrig hamna i ett ogiltigt tillstånd.

---

## Ditt jobb som klassdesigner

Du bestämmer vad omvärlden får se — och vad som är inlåst.

```
Fråga dig alltid:
  → Behöver kod utanför klassen läsa det här?   → public get
  → Behöver kod utanför klassen ändra det här?  → public set (sällan!)
  → Är det intern logik som ingen annan ska röra?→ private
```

Det är inte bara teknik. Det är design.

> 💬 _"Det här är hur jag tänker. Du hittar din egen rytm med tid — det viktiga är att du tänker på det alls."_

---

## Dags att kavla upp ärmarna!

**Övningar idag:**

🟢 `exercises/ovning_01_car.md` — bygg `Car` med privata fält och properties

🟡 `exercises/ovning_02_spaceship.md` — samma struktur, nytt rymdtema — lite mer att tänka på

---

**Inlämning 1 — Spellistan presenteras idag**

Skapa klassen `MusicArtist` — din favoritartist.

Privata fält: `name`, `genre`, `debutYear`, `country`, `isActive`
Properties med `private set` · Konstruktor · `Describe()` · `Perform()`
Minst 3 artister i `Main()`

**Deadline: Söndag 21 sep 23:59**

Mer info: `assignment/spellistan.md` — koda vilt! 🎵
