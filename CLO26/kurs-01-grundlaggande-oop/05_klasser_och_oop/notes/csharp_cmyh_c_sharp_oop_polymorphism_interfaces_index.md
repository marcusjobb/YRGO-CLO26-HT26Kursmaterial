---

title: Gränssnitt
author: Marcus Ackre Medina
type: article
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/csharp_cmyh/C-Sharp/oop/polymorphism/interfaces/index.md"
description: "Interfaces är ett kraftfullt verktyg i C-Sharp som ger oss möjlighet att skapa flexibla och"
tags: ["csharp", "git", "oop"]
week_fit: []
---

Interfaces är ett kraftfullt verktyg i C-Sharp som ger oss möjlighet att skapa flexibla och
återanvändbara komponenter i våra program. Genom att använda interfaces kan vi implementera
polymorfism och separera implementation och användning av komponenter.

## Beskrivning

Interfaces är en typ som definierar en uppsättning metoder, egenskaper och händelser som en klass
kan implementera. En klass som implementerar ett interface måste implementera alla dess medlemmar.
Därför säger man att Interfaces är kontrakt.

## Exempel

Låt oss titta på ett exempel där vi skapar ett interface som heter IAnimal:

```csharp

interface IAnimal
{
    string Name { get; set; }
    void Eat();
    void Sleep();
    void Shit();
}

```

I detta exempel har vi ett interface som heter IAnimal. Vi har också tre metoder och en property.
Observera att Interfaces inte kan ha fält, men de kan ha properties!

Observera att interfaces som regel alltid har ett namn som börjar på <b>I</b> och slutar helst på
<b>able</b>. I vårt fall slutar det inte på able för att IAnimalable låter bara dumt, men ett
interface som deklarerar en metod som heter Calc(int[]) skulle kunna heta ICalculable. Usch vilket
dåligt exempel, men ja, du förstår tanken.

Vi provar nu med att skapa en katt.

```csharp

class Cat : IAnimal
{
    public string Name { get; set; }

    public void Eat()
    {
        Console.WriteLine($"{Name} is eating.");
    }

    public void Sleep()
    {
        Console.WriteLine($"{Name} is sleeping.");
    }

    public void Shit()
    {
        Console.WriteLine($"{Name} is taking a shit.");
    }
}

```

I den här koden har vi skapat en klass Cat som implementerar IAnimal-gränssnittet. Klassen har en
egenskap Name som kan sättas och hämtas. Vi har också implementerat de tre metoderna Eat(), Sleep()
och Shit() enligt gränssnittets krav.

Om någon av metoderna saknas eller har en annan metodhuvud kommer programmet inte att kompileras då
den inte uppfyller kontraktet.

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
