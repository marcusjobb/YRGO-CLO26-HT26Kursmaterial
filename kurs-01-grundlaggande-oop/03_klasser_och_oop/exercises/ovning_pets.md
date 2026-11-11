# Övning — Husdjuren 🐾

🟡 Mellannivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Linnea driver ett litet dagis för husdjur. Hon vill ha ett program som kan presentera varje djur och låta dem "prata". Just nu har hon hundar och katter.

Du ska bygga det — och på vägen märka något viktigt.

---

## Del 1 — Bygg dem var för sig

Skapa **två separata klasser**: `Hund` och `Katt`.

Båda ska ha:

**Privat fält:**
- `_namn` (string)

**Konstruktor** som tar in ett namn

**Metoder:**
- `Presentera()` — skriver ut `"Jag heter [namn]."`
- `LåtaLjud()` — skriver ut djurets ljud

`Hund.LåtaLjud()` skriver ut `"Voff!"`, `Katt.LåtaLjud()` skriver ut `"Mjau!"`

**I `Main()`:**
- Skapa minst en hund och en katt
- Anropa `Presentera()` och `LåtaLjud()` på varje

---

## Exempeloutput

```
Jag heter Fido.
Voff!
Jag heter Luna.
Mjau!
```

---

## Tips

`private string _namn` — fältet är privat men konstruktorn sätter det. Utanför klassen kan ingen läsa eller skriva `_namn` direkt.

Om du vill lägga till fler djur later: skapa fler objekt i `Main()` — klassen behöver inte ändras.

---

## Del 2 — Titta på koden du just skrivit

Jämför din `Hund`-klass med din `Katt`-klass.

Svara på dessa frågor — skriv svaren som kommentarer i koden:

1. Hur många rader är identiska (eller nästan identiska) i de två klasserna?
2. Vad händer om Linnea vill lägga till ett fält `_ålder`? Hur många ställen behöver du ändra?
3. Vad händer om hon vill ha kaniner, papegojor och ormar också?

Det finns ett namn på det här problemet: **DRY-brott** — Do Not Repeat Yourself.  
Nästa vecka lär vi oss en lösning som heter **arv**.

---

## Klar snabbt? Utmaning 🔴

Linnea vill veta hur många djur som är registrerade.  
Lägg till en **statisk variabel** `AntalDjur` i varje klass som räknar upp i konstruktorn.  
Skriv ut totalen i `Main()`.

Notera att du nu behöver ändra **på två ställen**. Det är poängen.

---

## Lösningsförslag

<details>
<summary>Klicka för att visa lösningen</summary>

```csharp
class Hund
{
    private string _namn;

    public Hund(string namn)
    {
        _namn = namn;
    }

    public void Presentera()
    {
        Console.WriteLine($"Jag heter {_namn}.");
    }

    public void LåtaLjud()
    {
        Console.WriteLine("Voff!");
    }
}

class Katt
{
    private string _namn;

    public Katt(string namn)
    {
        _namn = namn;
    }

    public void Presentera()
    {
        Console.WriteLine($"Jag heter {_namn}.");
    }

    public void LåtaLjud()
    {
        Console.WriteLine("Mjau!");
    }
}

class Program
{
    static void Main()
    {
        Hund hund = new Hund("Fido");
        Katt katt = new Katt("Luna");

        hund.Presentera();
        hund.LåtaLjud();
        katt.Presentera();
        katt.LåtaLjud();
    }
}
```

Del 2 — svar att jämföra med:
1. `Presentera()` är identisk i båda klasserna — hela metoden, rad för rad
2. Två ställen: `Hund._ålder` och `Katt._ålder`
3. En ny klass per djurslag — fem djur = fem klasser med samma `Presentera()`

</details>

---

## Kika framåt — så här ser lösningen ut (en annan kurs)

Det här problemet löses med **arv** — ett koncept du möter i kurs 3.  
Ingen uppgift, bara en försmak:

```csharp
class Djur
{
    public string Namn { get; set; }

    public void Presentera()
    {
        Console.WriteLine($"Jag heter {Namn}.");
    }

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

`Presentera()` skrivs **en gång** i `Djur`. Hund och Katt ärver den.  
Lägger du till kanin behöver du bara skriva `LåtaLjud()` — inget mer.
