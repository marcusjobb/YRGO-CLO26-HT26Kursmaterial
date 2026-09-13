# Lästext — Klasser och objekt

## Vad är en klass?

Innan du kan förstå hur en klass fungerar i kod behöver du förstå varför den finns.

Tänk dig att du ska bygga ett program som hanterar bankkonton. Du behöver hålla reda på ett kontos ägare, saldo och om kontot är aktivt. Du behöver också kunna sätta in pengar, ta ut pengar och visa kontoinformation. Om du bara har tre variabler och tre lösa metoder fungerar det — men vad händer när du har hundra konton? Variablerna blandas ihop, metoderna vet inte vilket konto de arbetar med, och koden blir omöjlig att följa.

En klass löser det här. Den är en **ritning** — en mall som beskriver vad ett objekt ska innehålla och vad det ska kunna göra. Ur ritningen skapar du sedan verkliga objekt.

```mermaid
flowchart LR
    K["Klass: BankAccount\n(ritning)"] --> O1["Objekt: konto1\nÄgare: Alex\nSaldo: 1000"]
    K --> O2["Objekt: konto2\nÄgare: Sam\nSaldo: 500"]
    K --> O3["Objekt: konto3\nÄgare: Nour\nSaldo: 250"]
```

Klassen skapas en gång. Objekt kan du skapa hur många som helst ur samma klass — varje med sina egna värden, men samma struktur och beteende.

**Klass** = ritningen. **Objekt** = det verkliga exemplaret byggt från ritningen.

---

## Skapa en klass

En klass i C# har tre byggstenar: **fält** (eller properties) som lagrar data, en **konstruktor** som körs när objektet skapas, och **metoder** som beskriver vad objektet kan göra.

Här är ett komplett exempel på klassen `BankAccount`:

```csharp
class BankAccount
{
    // Properties — lagrar data, skrivbara bara inifrån klassen
    public double Saldo { get; private set; }
    public string Ägare { get; private set; }
    public bool ÄrAktivt { get; private set; }

    // Konstruktor — körs en gång när objektet skapas
    public BankAccount(string ägare, double startSaldo)
    {
        Ägare = ägare;
        Saldo = startSaldo;
        ÄrAktivt = true;
    }

    // Metod — sätter in pengar på kontot
    public void SättIn(double belopp)
    {
        if (belopp <= 0)
        {
            Console.WriteLine("Beloppet måste vara positivt.");
            return;
        }
        Saldo += belopp;
        Console.WriteLine($"{Ägare} satte in {belopp} kr. Nytt saldo: {Saldo} kr.");
    }

    // Metod — tar ut pengar, returnerar true om det gick
    public bool TaUt(double belopp)
    {
        if (belopp <= 0 || belopp > Saldo)
        {
            Console.WriteLine("Uttag nekat — otillräckligt saldo.");
            return false;
        }
        Saldo -= belopp;
        Console.WriteLine($"{Ägare} tog ut {belopp} kr. Nytt saldo: {Saldo} kr.");
        return true;
    }

    // Metod — skriver ut en presentation av kontot
    public void Presentera()
    {
        string status = ÄrAktivt ? "Aktivt" : "Inaktivt";
        Console.WriteLine($"Konto: {Ägare} | Saldo: {Saldo} kr | Status: {status}");
    }
}
```

Lägg märke till att klassen bara beskriver strukturen. Inget händer förrän du skapar ett objekt ur den.

---

## Konstruktorn

Konstruktorn är en speciell metod som körs automatiskt när ett objekt skapas. Den har alltid samma namn som klassen och returnerar inget.

**Varför behövs den?** Utan en konstruktor skulle ett nyskapat objekt sakna värden — `Ägare` skulle vara `null` och `Saldo` skulle vara `0`. Konstruktorn är platsen där du garanterar att objektet startar i ett korrekt tillstånd.

```csharp
public BankAccount(string ägare, double startSaldo)
{
    Ägare = ägare;
    Saldo = startSaldo;
    ÄrAktivt = true;
}
```

Konstruktorn tar emot parametrar precis som en vanlig metod. När du skriver `new BankAccount("Alex", 1000)` skickas `"Alex"` och `1000` in i konstruktorn, som sedan tilldelar dem till objektets properties.

```mermaid
flowchart LR
    A["new BankAccount(\"Alex\", 1000)"] --> B["Konstruktorn körs\nÄgare = \"Alex\"\nSaldo = 1000\nÄrAktivt = true"]
    B --> C["Objektet är klart\noch kan användas"]
```

---

## Private och public

En klass kan hålla på hemligheter. Det är faktiskt meningen.

`public` betyder att något är tillgängligt för alla — kod utanför klassen kan läsa och ändra det. `private` betyder att något bara är tillgängligt inifrån klassen själv.

Varför vill vi dölja något? Tänk på `Saldo`. Om det vore en vanlig `public` variabel skulle vem som helst kunna skriva `konto.Saldo = 999999` direkt — utan att gå via `SättIn` eller `TaUt`. All logik om giltiga belopp och felmeddelanden skulle kringgås helt.

Det här principen kallas **inkapsling**: du döljer interndetaljer och erbjuder istället ett kontrollerat gränssnitt utåt.

```csharp
// Utanför klassen — detta fungerar INTE:
konto.Saldo = 999999;  // Fel! Saldo har private set

// Det här fungerar däremot:
konto.SättIn(999999);  // Går via metoden som validerar beloppet
```

Tumregeln är enkel: **data är privat, beteende är publikt**. Metoder är klassens API mot omvärlden.

---

## Inkapsling

Det finns ett namn på det vi precis pratade om: **inkapsling** (encapsulation).

Inkapsling innebär att ett objekt äger sin data och bestämmer själv vem som får röra den — och hur. Ingenting utifrån kan komma åt interndetaljerna direkt. Istället exponerar klassen ett kontrollerat gränssnitt via properties och metoder.

En bra analogi: tänk på en bil. Du kan trycka på gaspedalen, vrida på ratten, växla. Men du kan inte sträcka in handen och justera bränsleinsprutningen direkt. Bilen döljer det komplexa innanverket och ger dig ett enkelt gränssnitt. Det är inkapsling i verkligheten.

I kod ser det ut så här:

```csharp
class BankAccount
{
    private double _saldo;  // ingen utifrån kan röra detta

    public bool TaUt(double belopp)
    {
        if (belopp <= 0 || belopp > _saldo) return false;
        _saldo -= belopp;
        return true;
    }
}
```

`_saldo` är privat. Ingen kan skriva `konto._saldo = -999` utifrån. Den enda vägen in är via `TaUt()` — som validerar beloppet innan den gör något.

> 📖 Se även: [Inkapsling — programmeringstermer](../termer/oop.md#inkapsling)

---

## Properties

I exemplet ovan används `{ get; private set; }` — det kallas en **property**. En property ser ut som en variabel utifrån men beter sig som en kontrollpunkt.

**Varför inte bara en vanlig variabel?** En `public double saldo;` kan läsas och ändras av vem som helst. En property med `private set` låter omvärlden läsa värdet men inte ändra det direkt — bara metoderna inuti klassen kan sätta ett nytt värde.

```csharp
// Property — läsbar utifrån, skrivbar bara inifrån
public double Saldo { get; private set; }

// Utanför klassen kan man göra:
Console.WriteLine(konto.Saldo);  // Fungerar — läsning är public

// Men inte:
konto.Saldo = 500;  // Kompileringsfel — set är private
```

Det ger dig en tydlig kontroll: vem får läsa? Vem får skriva?

<details>
<summary>Djupare: property med logik</summary>

En property kan också innehålla egen logik i `get` och `set`. Det är användbart när du vill validera eller beräkna ett värde:

```csharp
private double _saldo;

public double Saldo
{
    get { return _saldo; }
    private set
    {
        if (value < 0)
        {
            Console.WriteLine("Saldo kan inte bli negativt.");
            return;
        }
        _saldo = value;
    }
}
```

Här lagras det verkliga värdet i det privata fältet `_saldo`. Propertyn `Saldo` är gränssnittet utåt — och `set`-delen kan kontrollera vad som händer när värdet ändras. För de flesta enkla klasser räcker `{ get; private set; }`, men det är bra att veta att properties kan växa med behoven.

</details>

---

## Skapa ett objekt

Nu när klassen är definierad kan du skapa objekt ur den. Det gör du med nyckelordet `new`.

```csharp
BankAccount konto1 = new BankAccount("Alex", 1000);
BankAccount konto2 = new BankAccount("Sam", 500);
```

Varje `new`-anrop skapar ett **eget objekt** med egna värden. `konto1` och `konto2` är oberoende av varandra — ändrar du `konto1.Saldo` påverkar det inte `konto2`.

Variabeltypen till vänster (`BankAccount`) berättar vad för slags objekt variabeln pekar på. Det är viktigt: du kan bara använda det som klassen erbjuder via sitt publika gränssnitt.

---

## Metodanrop på objekt

När du har ett objekt kallar du dess metoder med punktnotation: `objekt.Metod(argument)`.

```csharp
BankAccount konto1 = new BankAccount("Alex", 1000);
BankAccount konto2 = new BankAccount("Sam", 500);

konto1.Presentera();
konto2.Presentera();

konto1.SättIn(500);
konto1.TaUt(200);
konto2.TaUt(600);   // misslyckas — otillräckligt saldo

konto1.Presentera();
konto2.Presentera();
```

Utskrift:

```
Konto: Alex | Saldo: 1000 kr | Status: Aktivt
Konto: Sam | Saldo: 500 kr | Status: Aktivt
Alex satte in 500 kr. Nytt saldo: 1500 kr.
Alex tog ut 200 kr. Nytt saldo: 1300 kr.
Uttag nekat — otillräckligt saldo.
Konto: Alex | Saldo: 1300 kr | Status: Aktivt
Konto: Sam | Saldo: 500 kr | Status: Aktivt
```

Punkten är inte bara syntax — den är en signal om ägande. `konto1.SättIn(500)` betyder: "be objektet `konto1` att utföra sin `SättIn`-metod med argumentet 500". Objektet vet vem det är och arbetar med sin egen data.

---

## UML-klassdiagram

När man pratar om klasser ritar man ofta ett **UML-klassdiagram** — ett standardiserat sätt att visa en klass struktur utan att skriva kod. Det är ett gemensamt språk mellan utvecklare.

```mermaid
classDiagram
  class BankAccount {
    -double Saldo
    -string Ägare
    -bool ÄrAktivt
    +BankAccount(ägare, startSaldo)
    +SättIn(belopp)
    +TaUt(belopp) bool
    +Presentera()
  }
```

Tecknen framför namnen har en betydelse:

| Tecken | Betyder |
|--------|---------|
| `-` | private |
| `+` | public |

Det du ser i diagrammet är precis samma klass som vi har kodat — bara ritad istället för skriven. UML hjälper dig planera och kommunicera design innan du börjar koda.

<details>
<summary>Djupare: vad händer i minnet när new anropas?</summary>

När du skriver `new BankAccount("Alex", 1000)` händer det här bakom kulisserna:

1. **Minne allokeras på heapen.** .NET reserverar ett utrymme i minnet tillräckligt stort för att hålla alla objektets data — `Saldo`, `Ägare` och `ÄrAktivt`.

2. **Konstruktorn körs.** Värdena `"Alex"` och `1000` skickas in och tilldelas till objektets properties.

3. **En referens returneras.** Variabeln `konto1` innehåller inte objektet direkt — den innehåller en **referens**, ungefär som en adress, som pekar till var i minnet objektet finns.

Det har en praktisk konsekvens:

```csharp
BankAccount konto1 = new BankAccount("Alex", 1000);
BankAccount kopia = konto1;  // kopia pekar på SAMMA objekt

kopia.SättIn(500);
konto1.Presentera();  // visar 1500 — inte 1000!
```

`konto1` och `kopia` är två variabler men ett och samma objekt. Det är ett vanligt misstag att tro att man kopierat ett objekt när man egentligen bara kopierat referensen till det. Om du vill ha ett äkta nytt objekt med samma värden måste du skapa det med `new`.

</details>

---

**Se även:** [programmeringstermer/klasser.md](../programmeringstermer/klasser.md), [programmeringstermer/oop.md](../programmeringstermer/oop.md)
