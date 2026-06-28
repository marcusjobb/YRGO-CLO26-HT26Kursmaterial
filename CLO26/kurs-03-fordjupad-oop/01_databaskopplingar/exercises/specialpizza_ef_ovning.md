---

title: Specialpizza (EF övning)
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Specialpizza (EF övning).md"
description: "Den lokala pizzerian går lite dåligt och behöver inspiration för nya pizzor. Du kommer på en alldeles utmärkt idé. Med hjälp av en databas och lite äventyrlighet ska du skapa receptet för en pizza. Då"
tags: ["csharp", "databaser", "entity-framework", "exercise", "linq", "random", "seeder", "specialpizza", "version"]
week_fit: []
---

# Specialpizza EF version

🟡


Den lokala pizzerian går lite dåligt och behöver inspiration för nya pizzor. Du kommer på en alldeles utmärkt idé. Med hjälp av en databas och lite äventyrlighet ska du skapa receptet för en pizza. Då den inte ska vara för dyr räcker det med ett par ingredienser.

"Dagens pizza"

Med hjälp av en databas kommer du nu att slumpa fram olika recept. För att det inte ska bli kaotiskt har du flera typer.

- Degtyp (tunn, tjock, ostkanter a´la Pizza hut)

- Grönsaker (paprika, tomat, lök mm)

- Kött (kebabkött, skinka, kyckling mm)

- Vego (Tofu, Omph mm)

- Top (olika ost typer)

- Vego top (olika vego ost typer)

Skapa modellerna och en Seeder som genererar de olika ingredienser i dem.

Nu ska du med hjälp av Random objektet skapa en värden mellan 0 och List.Count(), en för köttätare och en för vegetarianer/veganer. Det är inget krav att de ska ha samma uppsättning av deg och grönsaker. (glutenfri deg finns tillgängligt)

Kodexempel
private static Random Rnd = new Random();

```


public Vegan GetVeganToping()
{
var pos = Rnd.Next(0, VeganToppings.Count());
return VeganToppings.ToArray()[pos];
```

}
alternativt

```


public Vegan GetVeganTopping()
{
return VeganToppings.OrderBy(q => Guid.NewGuid()).First();
}
```


En standard pizza kommer att bli ungefär

| Carnivor | Vegetarian |
| --- | --- |
| Paprika | Tomat |
| Skinka | Omph |
| Västerbotten | Go Vegan Mozarella |
| Tunn botten | Ostkanter |


En lyxvrariant skulle kunna blanda in fler sorters grönsaker

Metod 2 kan du anpassa till att returnera en lista med fler objekt, då får du en pizza mer fler smaker


```


public List<Vegan> GetVeganTopping(int max=4)
{
return VeganToppings.OrderBy(q => Guid.NewGuid()).Take(max);
}
```


Dela med dig av dina fantastiska pizzarecept på den allmänna chatten

Inspiration: How to Generate (Almost) Anything, Episode 2: Human-AI Collaborated Pizza (Longer Version) - YouTube

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
