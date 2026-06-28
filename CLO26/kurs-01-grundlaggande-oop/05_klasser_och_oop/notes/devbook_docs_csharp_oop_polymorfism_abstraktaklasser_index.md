---

title: Abstrakta klasser
author: Marcus Ackre Medina
type: article
topic: oop
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/books/devbook/Docs/csharp/oop/polymorfism/abstraktaklasser/index.md"
description: "Abstrakta klasser är klasser som innehåller både kod och abstrakta metoder. Som en slags mellanting mellan en interface och en klass."
tags: ["abstrakta", "csharp", "git", "klasser", "oop"]
week_fit: []
---

# Abstrakta klasser

🟢


Abstrakta klasser är klasser som innehåller både kod och abstrakta metoder. Som en slags mellanting mellan en interface och en klass.
<details open markdown="block">
  <summary>
    Innehållsförteckning
  </summary>
  {: .text-delta }

1. TOC
{:toc}

</details>

## Beskrivning

En abstrakt klass är bara till för att andra klasser ska kunna arva från den. Det går inte att skapa objekt av en abstrakt klass. Det är bara till för att andra klasser ska kunna arva från den.

## Exempel

```csharp
public abstract class Shape
{
    public abstract double Area();
}
public class Circle : Shape
{
    public double Radius { get; set; }
    public override double Area()
    {
        return Math.PI * Radius * Radius;
    }
}
public class Rectangle : Shape
{
    public double Width { get; set; }
    public double Height { get; set; }
    public override double Area()
    {
        return Width * Height;
    }
}
```

## Referenser

- [Abstrakta klasser](https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/abstract-classes)
- [Abstract class W3Schools](https://www.w3schools.com/cs/cs_abstract.php)

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
