---

title: Clean code
author: Marcus Ackre Medina
type: lecture
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "/home/nionit/git/marcus-yh-claude-assistent/reference/lectures_to_spread_out/Clean code.md"
description: "- Nyckeln till bra programmering"
tags: ["api", "clean", "clean-code", "csharp", "oop"]
week_fit: []
---

## Slide 1: Clean code

- Nyckeln till bra programmering

---

## Slide 2: Vad är clean code

- Riktlinjer för hur man skapar koden.
- Det är inte mer än så, det är inte regler skrivna i sten, det är förslag och råd från programmerare.
- Följ riktlinjerna om du vill.
- I projekt bestämmer man innan man börjar vilken standard som ska följas, många av de reglarna har sin grund i vad som numera kallas ”Clean Code”.
- Clean code skiljer sig från framework till framework.

- 2021-02-15

---

## Slide 3: clean code i C#

- Vettiga namn
```csharp
Ge vettiga namn till dina classer, metoder och variablerInt v1,v2, v3; eller int Year, Month, Day;
```

- Följ grundregler för frameworken
- PascalCase för Metoder, Class variabler, Const värder, Enums
- camelCase för metodvariabler, metod argument
- Interfaces börjar alltid med I exempelvis IPerson, IRobot
- Ha en bra dialekt på din kod
```csharp
Movies.Add() säger klart och tydligt att classen Movies har metoden Add, det är onödigt att ha en beskrivande metod som AddANewMovie()
En POCO som inte är en lista eller array ska ha namn i singular (class Person{ }, class Animal{})
```

- En lista med POCOs ska ha POCO-namnet i plural (List<Person> People)

- 2021-02-15

---

## Slide 4: clean code i C#

- Kommentera på ett bra sätt
```csharp
// Den räknar siffror     vs     /// <Summary> Summerar alla värden i listan</Summary>
```

- Kommentera alltid dock inte till absurdum, skriv inte en novell av din kod, spara det till ReadMe.md
- Gör koden läsbar, Lamdakod är trevlig men ha hellre läsbar och förståelig kod
- Metodnamnen ska förklara vad koden i den gör och den ska inte göra något annat än det
- Gör metoderna små, om en metod är större än halva skärmlängden så är den för lång
- Håll raderna korta, om raden är längre än halva skärmlängden så är den för lång
- Skapa metoder istället för att upprepa kod
- Inget hårdkodande! Varken filnamn eller nummeriska värden,

- 2021-02-15

---

## Slide 5: clean code i C#

- Om du måste hårdkoda, använd Const variabler
- Inga magiska tal ( 10, -1, 32, 24) använd variabler vars namn förklarar talets mening
- Kontrollera alla objekt så de inte är null innan du använder dem
- Använd standardvärden på dina properties för att slippa NULL errors
- Använd Try Catch där det kan hände hemska saker
```csharp
Catch (exception ex) { throw ex; } är ett värdelöst sätt att hantera fel på
```

- Använd test där det är möjligt
- Refactor everything! Roslynerator och CodeCracker ger dig massor med tips
- APIs är bra men dubbelkolla licensavtal

- 2021-02-15

---

## Slide 6: clean code i C#

- Parkodning är alltid bra
- Kodutbyte för code review är alltid bra
- Spara ofta
- Använd Rubber Duck debugging
- Ta bort oanvända usings
- Ta bort oanvända metoder
- Ta bort bortkommenterad kod eller kommentera varför den finns kvar
- Återanvänd de metoder du har
- Samla hjälpmetoder i classer med namn som antyder användningsområdet
- Använd inte ”gamla” APIer eller gammaldags programmeringsstil

- 2021-02-15

---

## Slide 7: clean code i C#

- En tomrad mellan varje metod
```csharp
En tomrad efter } såvida inte det är en } efter
```

- Ha aldrig  flera tomrader efter varandra, det gör koden ful
- Se till att din kod alltid är snyggt strukturerad (tabulerad)
- I en klass ska metoderna vara i följande ordning
- Constructor
- Variabler
- Properties
- Publika metoder
- Privata metoder
- Statiska publika metoder
- Statiska privata metoder

- 2021-02-15

---

## Slide 8: clean code i C#

```csharp
Foreach hellre än for (int x; x<length; x++)
```

- Value as MyClass hellre än (MyClass)Value
```csharp
If (info is string) hellre än If (typeof(info)==string)
result = value==10?”Ten”:”Not ten” hellre än if (value==10)  result = ”Ten” else result = ”Not ten”
$”Hello {name}” hellre än ”Hello ”+ name
```

- Använd ”var” istället för variabeltyp om variabeltypen är självklar
- Använd mappar och namespaces för att dela upp din kod
- Använd #Region för att dela upp kodstycken

- 2021-02-15

---

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
