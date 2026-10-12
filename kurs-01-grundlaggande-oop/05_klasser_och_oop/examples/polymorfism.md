# Polymorfism — livekodning 2026-09-23

Samma Djur/Hund/Katt som i `arv`-lektionen, men nu bygger vi vidare: vad vinner vi egentligen på att `Hund` och `Katt` ärver från `Djur`?

## Klasserna vi utgick från

```csharp
class Djur
{
    public string Namn { get; set; }
    public virtual void LåtaLjud() { }
}

class Hund : Djur
{
    public override void LåtaLjud() => Console.WriteLine("Voff!");

    public void Bit()
    {
        Console.WriteLine($"{Namn} bet dig");
    }
}

class Katt : Djur
{
    public override void LåtaLjud() => Console.WriteLine("Mjau!");
    public void Kurr()
    {
        Console.WriteLine($"{Namn} kurrar glatt");
    }
}
```

Lägg märke till att `Bit()` och `Kurr()` **inte** finns på `Djur` — de är unika för respektive subklass. Det blir viktigt lite längre ner.

## Fyra djur

```csharp
Hund vovve = new() { Namn = "Vovve" };
Hund byracka = new() { Namn = "Byracka" };
Katt meow = new() { Namn = "Meow" };
Katt klös = new() { Namn = "klös" };
```

## Utan polymorfism — separata listor

Så här hade vi tvingats göra om vi bara tänkte i `Hund` och `Katt`, var för sig:

```csharp
List<Hund> jycken = new() { vovve, byracka };
List<Katt> hårbollar = new() { meow, klös };

foreach(Hund hund in jycken)
    hund.LåtaLjud();

foreach (Katt katt in hårbollar)
    katt.LåtaLjud();
```

Fungerar, men kräver en lista och en loop **per djurtyp**. Lägg till en tredje sort och du skriver samma loop en gång till.

## Med polymorfism — en lista

```csharp
// alla är Djur i grunden, så alla kan vara med i listan
List<Djur> djur = new() { meow, vovve, byracka, klös };

foreach (Djur d in djur)
{
    Console.WriteLine(d.Namn);
    d.LåtaLjud();
}
```

En lista, en loop, oavsett hur många djurtyper som finns. `d.LåtaLjud()` kör automatiskt rätt `override` beroende på vad `d` faktiskt är — det är hela vinsten.

## När du behöver mer än basklassen erbjuder

`LåtaLjud()` finns på `Djur`, så den är alltid tillgänglig i loopen. Men `Bit()` och `Kurr()` finns bara på subklasserna — `d` är deklarerad som `Djur`, så `d.Bit()` kompilerar inte rakt av. Då behöver du kolla typen och **casta**:

```csharp
foreach (Djur d in djur)
{
    Console.WriteLine(d.Namn);
    d.LåtaLjud();
    if (d is Katt)
        ((Katt)d).Kurr();
    if (d is Hund)
        ((Hund)d).Bit();
}
```

`d is Katt` kollar faktisk typ vid körning. `(Katt)d` är själva castningen — "lita på mig, det här är faktiskt en Katt" — och ger dig tillgång till `Kurr()`.

**Tumregel:** om metoden finns på basklassen (`virtual`/`override`), behöver du aldrig `is`/cast — polymorfismen sköter det. Först när du vill åt något som **bara** finns på subklassen behöver du fråga vilken typ det faktiskt är.

## Explicit upcast och downcast

```csharp
Djur vov = new Hund() { Namn = "Doggie" };
Hund vov_v2 = (Hund)vov;
vov_v2.Bit();
```

`Djur vov = new Hund()` — en `Hund` sparas i en `Djur`-variabel (uppcastning, alltid tillåten eftersom Hund är en Djur). `Hund vov_v2 = (Hund)vov` — vi vill tillbaka till `Hund` för att komma åt `Bit()`, så vi castar explicit nedåt.

