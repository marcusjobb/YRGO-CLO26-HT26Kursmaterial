# Övning — SG-1 personalregister

🟢 Grundnivå

> Fastnar du i mer än 15 minuter? Fråga klassen → AI → Marcus. I den ordningen.

---

## Bakgrunden

Stargate Command behöver ett system för att hålla reda på SG-1-teamets specialområden.
General Hammond vill kunna slå upp vem som kan vad — snabbt, utan att bläddra i papper.

En array duger inte här. Vi vet inte i förväg vilken ordning vi vill söka på, och vi vill söka
på *namn*, inte på index. Det är precis vad ett `Dictionary` är till för.

---

## Vad gäller för den här övningen

- [ ] Kan skapa ett `Dictionary<string, string>`
- [ ] Kan lägga till poster med `Add`
- [ ] Kan slå upp ett värde med en nyckel
- [ ] Kan kontrollera om en nyckel finns med `ContainsKey`
- [ ] Kan loopa igenom alla poster med `foreach`

---

## Uppgift

Skapa ett `Dictionary<string, string>` där nyckeln är teammedlemmens namn och värdet är deras roll/specialitet.

**Lägg till de fyra originalmedlemmarna:**

| Namn | Specialitet |
|------|-------------|
| Jack O'Neill | Taktik och ledarskap |
| Samantha Carter | Astrofysik och teknik |
| Daniel Jackson | Arkeologi och språk |
| Teal'c | Jaffa-strid och goa'uld-kunskap |

**Skriv sedan kod som:**
1. Skriver ut alla teammedlemmar och deras specialiteter
2. Slår upp och skriver ut Samantha Carters specialitet
3. Kontrollerar om `"Apophis"` finns i teamet (spoiler: nej)
4. Kontrollerar om `"Teal'c"` finns i teamet (spoiler: ja)

---

## Exempeloutput

```
SG-1 teamet:
Jack O'Neill — Taktik och ledarskap
Samantha Carter — Astrofysik och teknik
Daniel Jackson — Arkeologi och språk
Teal'c — Jaffa-strid och goa'uld-kunskap

Carters specialitet: Astrofysik och teknik

Är Apophis med i teamet? Nej.
Är Teal'c med i teamet? Ja.
```

---

## Klar snabbt? Utmaning 🔴

General Hammond vill kunna lägga till nya teammedlemmar under körning.
Lägg till en metod `LäggTillMedlem(Dictionary<string, string> team, string namn, string specialitet)`
som kontrollerar att namnet inte redan finns innan det läggs till.

<details>
<summary>Tips — ContainsKey som grindvakt</summary>

```csharp
if (!team.ContainsKey(namn))
    team.Add(namn, specialitet);
else
    Console.WriteLine($"{namn} är redan registrerad.");
```

</details>

<details>
<summary>Lösningsförslag</summary>

```csharp
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        Dictionary<string, string> sg1 = new Dictionary<string, string>();

        sg1.Add("Jack O'Neill", "Taktik och ledarskap");
        sg1.Add("Samantha Carter", "Astrofysik och teknik");
        sg1.Add("Daniel Jackson", "Arkeologi och språk");
        sg1.Add("Teal'c", "Jaffa-strid och goa'uld-kunskap");

        Console.WriteLine("SG-1 teamet:");
        foreach (var medlem in sg1)
            Console.WriteLine($"{medlem.Key} — {medlem.Value}");

        Console.WriteLine();
        Console.WriteLine($"Carters specialitet: {sg1["Samantha Carter"]}");

        Console.WriteLine();
        Console.WriteLine($"Är Apophis med i teamet? {(sg1.ContainsKey("Apophis") ? "Ja." : "Nej.")}");
        Console.WriteLine($"Är Teal'c med i teamet? {(sg1.ContainsKey("Teal'c") ? "Ja." : "Nej.")}");
    }
}
```

</details>
