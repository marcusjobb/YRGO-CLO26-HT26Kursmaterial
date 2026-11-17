# Övning — Väderappen

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

SMHI har anlitat dig för att bygga en enkel väderapp.
Appen tar ett väderläge och ger ett konkret klädråd — ingen ska behöva frysa för att de trodde det var soligare än det var.
Stavfel i strängar som `"snoe"` eller `"snö "` (med mellanslag) ska inte vara möjliga.

---

## Vad gäller för den här övningen

- [ ] Kan deklarera en enum med flera värden
- [ ] Kan skriva en metod som returnerar en sträng baserat på enum-värde
- [ ] Förstår att en switch på enum ger kompilatorfel om man skriver fel värde

---

## Uppgift

Skapa enumen `Vader` utanför klassen:

```csharp
enum Vader { Soligt, Molnigt, Regn, Snö, Åska }
```

**Metod `string KladRad(Vader vader)`**  
Returnerar ett klädråd som sträng:

- `Soligt` → `"T-shirt räcker fint!"`
- `Molnigt` → `"Ta med en tunn jacka."`
- `Regn` → `"Ta regnjacka och stövlar."`
- `Snö` → `"Mössa, vantar och vinterjacka."`
- `Åska` → `"Stanna inomhus om du kan."`

I `Main()`: anropa `KladRad()` för minst tre väderlägen och skriv ut resultatet med `Console.WriteLine()`.

---

## Exempeloutput

```
Soligt: T-shirt räcker fint!
Regn: Ta regnjacka och stövlar.
Snö: Mössa, vantar och vinterjacka.
```

---

## Klar snabbt? Utmaning 🔴

Lägg till `double temperatur` som extra parameter i `KladRad()`.  
Om `temperatur < 0` och vädret är `Soligt` — lägg till meningen `"Men det är kallt — ta jacka ändå."` på en ny rad.

<details>
<summary>Tips</summary>

Bygg den vanliga strängen först med switch, spara den i en variabel. Kolla sedan temperaturen och lägg till extra text om villkoret stämmer. Returnera den samlade strängen.

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
enum Vader { Soligt, Molnigt, Regn, Snö, Åska }

class Program
{
    static void Main()
    {
        Console.WriteLine($"Soligt: {KladRad(Vader.Soligt)}");
        Console.WriteLine($"Regn: {KladRad(Vader.Regn)}");
        Console.WriteLine($"Snö: {KladRad(Vader.Snö)}");
    }

    static string KladRad(Vader vader)
    {
        switch (vader)
        {
            case Vader.Soligt:  return "T-shirt räcker fint!";
            case Vader.Molnigt: return "Ta med en tunn jacka.";
            case Vader.Regn:    return "Ta regnjacka och stövlar.";
            case Vader.Snö:     return "Mössa, vantar och vinterjacka.";
            case Vader.Åska:    return "Stanna inomhus om du kan.";
            default:            return "Okänt väderläge.";
        }
    }
}
```

</details>
