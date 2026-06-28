---

title: Specialpizza (SQL övning)
author: Marcus Ackre Medina
type: exercise
topic: databaser
difficulty: 2
language: sql
status: adapted
marcus_voice: true
source: "exercises_to_spread_out/Specialpizza_SQL_ovning.md"
description: "Den lokala pizzerian går lite dåligt och behöver inspiration för nya pizzor. Du kommer på en alldeles utmärkt idé. Med hjälp av en databas och lite äventyrlighet ska du skapa receptet för en pizza. Då"
tags: ["cross-join", "databaser", "exercise", "newid", "random", "specialpizza", "sql"]
week_fit: []
---

# Specialpizza

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

Skapa tabellerna och INSERTA olika ingredienser i dem.

Nu ska du med hjälp av cross join och top 1 slumpa fram två pizzor, en för köttätare och en för vegetarianer/veganer. Det är inget krav att de ska ha samma uppsättning av deg och grönsaker. (glutenfri deg finns tillgängligt)

En standard pizza kommer att bli ungefär

| Carnivor | Vegetarian |
| --- | --- |
| Paprika | Tomat |
| Skinka | Omph |
| Västerbotten | Go Vegan Mozarella |
| Tunn botten | Ostkanter |


En lyxvrariant skulle kunna blanda in fler sorters grönsaker

Det finns olika sätt att lösa det på.

- Cross joina alla tabeller (skilj dock på kött och vego)

- Läs av en tabell i taget

- Läs av en tabell i taget och slumpa igenom det i C#

I SQL servern kan man skapa Unique identifiers, dessa är slumpvalda nycklar som aldrig återskapas. Men vi kan missbruka dessa till att få slumpmässiga resultat om vi sorterar tabellen på funktionen NewId().
Select Top 1 * from people Order By NewId() – kommer att välja en slumpvald person. Vi kan alltså föra samma sak med ingredienser.

Dela med dig av dina fantastiska pizza recept på den allmänna chatten

Inspiration:

How to Generate (Almost) Anything, Episode 2: Human-AI Collaborated Pizza (Longer Version) - YouTube

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
