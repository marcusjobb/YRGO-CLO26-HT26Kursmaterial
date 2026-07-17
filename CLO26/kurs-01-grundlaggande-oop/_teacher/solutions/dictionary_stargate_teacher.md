# Lärarfacit — SG-1 personalregister

## Lösning

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

## Utmaning — LäggTillMedlem

```csharp
static void LäggTillMedlem(Dictionary<string, string> team, string namn, string specialitet)
{
    if (!team.ContainsKey(namn))
        team.Add(namn, specialitet);
    else
        Console.WriteLine($"{namn} är redan registrerad.");
}
```

## Pedagogisk poäng

Tre distinkta Dictionary-operationer i en övning:
- `Add` — lägga till
- `sg1["nyckel"]` — direktuppslag (kastar `KeyNotFoundException` om nyckeln saknas — bra att nämna)
- `ContainsKey` — säker kontroll utan risk för krasch

Visa skillnaden mellan `sg1["Apophis"]` (kraschar) och `ContainsKey("Apophis")` (returnerar false).
Det är ett av de vanligaste misstagen med Dictionary.

Utmaningen introducerar `ContainsKey` som grindvakt i en metod — ett mönster de kommer använda hela kursen.
