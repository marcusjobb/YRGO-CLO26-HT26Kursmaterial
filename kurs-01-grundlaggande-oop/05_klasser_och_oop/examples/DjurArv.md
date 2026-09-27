# Exempel — Djur med arv

Föreläsningsexempel som visar `virtual`, `override`, `base()` och kedjat arv.  
Projektet delar upp klasserna i en egen mapp (`MinaDjur`) med matchande namespace.

---

## Djur.cs — basklassen

Den klass alla djur ärver från. Definierar gemensamma egenskaper och ett `virtual`-beteende.

```csharp
namespace Djur_arv.MinaDjur;

// Basklass — alla djur delar dessa egenskaper och beteenden
class Djur
{
    // Private backing fields — ingen utanför klassen kan sätta dem direkt
    private string _name = "Djur";
    private int _age = 1;

    // Encapsulate field — VS-refactor som genererar dessa automatiskt
    // get => läsbar utifrån | private set => skrivbar bara inifrån klassen
    public string Name { get => _name; private set => _name = value; }
    public int Age  { get => _age;  private set => _age  = value; }

    // Overloaded constructors — samma metodnamn, olika signaturer
    public Djur(string name)
    {
        _name = name;
    }

    public Djur(string name, int age)
    {
        _name = name;
        _age  = age;
    }

    public void Presentera()
    {
        Console.WriteLine($"Hej, jag heter {_name} och är {_age} år gammal");
    }

    // virtual = "denna metod FÅR skrivas om i en subklass"
    // Om ingen subklass skriver om den används denna version
    public virtual void Prata()
    {
        Console.WriteLine("AGGGHHH");
    }
}
```

---

## Hund.cs och Pudel.cs — subklasser

`Hund` ärver från `Djur`. `Pudel` ärver från `Hund` — kedjat arv.

```csharp
namespace Djur_arv.MinaDjur;

// Hund ärver allt från Djur — Name, Age, Presentera() ingår gratis
class Hund : Djur
{
    // : base(name) skickar argumentet vidare upp till Djur-konstruktorn
    public Hund(string name) : base(name) { }
    public Hund(string name, int age) : base(name, age) { }

    // override = ersätter Djurs virtual-metod med hundens version
    public override void Prata()
    {
        Console.WriteLine("Wooff woff");
    }
}

// Pudel ärver från Hund — som i sin tur ärver från Djur (kedjat arv)
class Pudel : Hund
{
    public Pudel(string name) : base(name) { }
    public Pudel(string name, int age) : base(name, age) { }

    public override void Prata()
    {
        base.Prata();  // anropar Hunds Prata() — skriver ut "Wooff woff" först
        Console.WriteLine("BjäbbBjäbbBjäbbBjäbb");
    }
}
```

---

## Orm.cs — ingen override

`Orm` ärver `Prata()` rakt av från `Djur` utan att skriva om den.

```csharp
namespace Djur_arv.MinaDjur;

class Orm : Djur
{
    public Orm(string name) : base(name) { }
    public Orm(string name, int age) : base(name, age) { }

    // ingen Prata() — basklassens "AGGGHHH" används automatiskt
}
```

---

## Program.cs — använd klasserna

```csharp
using Djur_arv.MinaDjur;  // importerar namespace där klasserna finns

Djur djur  = new("Animal");
Djur djur2 = new("Animal 2", 4);

djur.Presentera();   // Hej, jag heter Animal och är 1 år gammal
djur.Prata();        // AGGGHHH

djur2.Presentera();
djur2.Prata();       // AGGGHHH

Console.WriteLine();

Hund hund = new("Vovven", 3);
hund.Presentera();
hund.Prata();        // Wooff woff

Pudel pudel = new("Pudel", 2);
pudel.Presentera();
pudel.Prata();       // Wooff woff
                     // BjäbbBjäbbBjäbbBjäbb

Orm orm = new("Sir Väs", 45);
orm.Presentera();
orm.Prata();         // AGGGHHH — inget override, basklassen tar över
```

---

## Förväntad output

```
Hej, jag heter Animal och är 1 år gammal
AGGGHHH
Hej, jag heter Animal 2 och är 4 år gammal
AGGGHHH

Hej, jag heter Vovven och är 3 år gammal
Wooff woff
Hej, jag heter Pudel och är 2 år gammal
Wooff woff
BjäbbBjäbbBjäbbBjäbb
Hej, jag heter Sir Väs och är 45 år gammal
AGGGHHH
```
