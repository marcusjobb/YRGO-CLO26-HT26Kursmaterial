---
marp: true
theme: nion-dark
paginate: true
---

<!-- WOW-MOMENT: två variabler byter plats utan en temporär variabel -->

# Tuples

**Vecka 48 · Hellweek · Hidden gems**

---

## Två värden från en metod

```csharp
(decimal sum, decimal vat) Calculate(decimal price)
{
    return (price, price * 0.25m);
}

var (sum, vat) = Calculate(100m);
Console.WriteLine($"Summa: {sum}, moms: {vat}");
```

Ingen ny klass behövs för att skicka tillbaka två värden.

---

## Namn eller inte?

```csharp
var person = ("Ada", 36);
Console.WriteLine(person.Item1);   // Ada
```

Med namn blir koden tydligare än `Item1`. Använd alltid namn.

---

## När ska du INTE använda tuples?

Om värdena hör ihop och får egna regler, skapa en klass eller en `record`. Då har de ett namn och ett ansvar.

---

## Wow: byt plats utan temp

```csharp
int a = 1;
int b = 2;
(a, b) = (b, a);         // a är 2, b är 1. Ingen temp-variabel.
```

---

## Sammanfattning

- Tuples är lätta sätt att skicka tillbaka flera värden.
- Deconstruction plockar isär dem i variabler.
- Ge fälten namn.
