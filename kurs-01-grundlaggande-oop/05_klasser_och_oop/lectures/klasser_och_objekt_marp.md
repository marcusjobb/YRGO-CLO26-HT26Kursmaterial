---
marp: true
theme: nion-dark
paginate: true
---

# Klasser och Objekt

**Kurs:** OOP i C# Grund
**Modul:** 05 — Klasser, properties och inkapsling

---

## Vad ska vi lära oss idag?

- Vad är skillnaden mellan en klass och ett objekt?
- Hur skriver man en klass med fält, properties och konstruktor?
- Varför är fält privata — och vad händer om de inte är det?

---

## Klassen är ritningen. Objektet är huset.

Du kan ha en ritning för ett hus.  
Ritningen *är inte* ett hus.  
Men från en ritning kan du bygga hur många hus som helst.

```
Klass  = BankAccount    ← ritningen
Objekt = konto1, konto2 ← de faktiska husen
```

---

## Så här ser det ut på tavlan

```
┌─────────────────────────────┐
│         BankAccount         │
├─────────────────────────────┤
│ - ägare : string            │
│ - saldo : double            │
│ - ärAktivt : bool           │
├─────────────────────────────┤
│ + BankAccount(ägare,        │
│     startSaldo)             │
│ + SättIn(belopp)            │
│ + TaUt(belopp) : bool       │
│ + Presentera()              │
└─────────────────────────────┘
```

`-` = private &nbsp;&nbsp; `+` = public

---

## Koden — steg för steg

```csharp
public class BankAccount
{
    private string ägare;
    private double saldo;
    private bool ärAktivt;

    public BankAccount(string ägare, double startSaldo)
    {
        this.ägare = ägare;
        this.saldo = startSaldo;
        this.ärAktivt = true;
    }
}
```

*Vi kodar resten live. Häng med.*
*Imorgon skriver vi om det här med properties och `private set`.*

---

## Properties — det kontrollerade fönstret

```csharp
// ❌ Publikt fält — ingen kontroll
public double saldo;

// ✅ Property — vi bestämmer vad som syns utåt
public double Saldo { get; private set; }
```

Utifrån kan du läsa `Balance`.  
Utifrån kan du **inte** skriva `konto.Balance = 1000000`.  
Bara klassen själv får ändra den.

---

## Varför spelar det roll?

```csharp
// Om saldo vore publikt:
konto.saldo = -999999;    // inga hinder
konto.saldo = 0.000001;   // fortfarande inga hinder

// Med private + TaUt():
bool lyckades = konto.TaUt(500);
// TaUt() kontrollerar att pengarna finns
// Ogiltiga värden stoppas inuti klassen
```

*Klassen skyddar sig själv. Det är poängen.*

---

## Konstruktorn — startpistolen

```csharp
// new = bygg ett objekt från ritningen
BankAccount konto1 = new BankAccount("Alex", 1000);
BankAccount konto2 = new BankAccount("Sam", 500);
```

`konto1` och `konto2` är **separata objekt**.  
De delar ritning — men inte data.

---

## Vanliga misstag

- Glömmer `private` → allt är publikt → inkapsling försvinner
- Sätter `this.ägare = ägare` i fel ordning → parametern skriver över sig själv
- Gör properties som både `get` och `set` publika → ingen kontroll

---

## Nu kodar vi

Häng med. Ställ frågor direkt — inte efter.

Vi bygger `BankAccount` från noll.  
Sedan testar ni `Car` på egen hand.

---

## Sammanfattning

- ✅ Klass = ritning. Objekt = instans av ritningen.
- ✅ Fält är privata — data skyddas inuti klassen
- ✅ Properties styr vad som syns utifrån
- ✅ Konstruktorn sätter startvärden
- ➡️ Imorgon: inkapsling djupare + övning Car + Spaceship
- ➡️ Tisdag em: **Spellistan presenteras**
