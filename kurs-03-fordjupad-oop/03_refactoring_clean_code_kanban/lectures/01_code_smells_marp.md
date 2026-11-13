---
marp: true
theme: nion-dark
paginate: true
---

## Hjulet idag

Elin öppnar Nils kod för första gången. En metod, 200 rader. Hur många smells hittar ni?

---

# Code smells

**Vecka 47 · Måndag · Lektion 1 (09:00)**

---

## Tänk på en gammal bil

Den går fortfarande. Men den låter konstigt, och varje reparation tar längre tid.

Kod är likadan. Den kan fungera idag och kosta mycket imorgon.

---

## Tre frågor att ställa

1. Kan en ny kollega förstå den på 30 sekunder?
2. Måste jag läsa samma kod två gånger?
3. Vet jag vad varje metod gör utan att läsa hela den?

Om svaret är nej någon gång, har du ett code smell.

---

## Fem vanliga smells

- **Lång metod:** gör för många saker.
- **Duplicerad kod:** samma regel på två ställen.
- **Magiskt tal:** `0.25` utan förklaring.
- **Stor klass:** vet för mycket.
- **Lång parameterlista:** sju parametrar är svårt att hålla reda på.

---

## Exempel: lång metod

```csharp
void PrintReceipt(Order order)
{
    decimal sum = 0;
    foreach (var row in order.Rows)
        sum += row.Price * row.Quantity;
    decimal vat = sum * 0.25m;
    Console.WriteLine($"Summa: {sum}");
    Console.WriteLine($"Moms: {vat}");
    Console.WriteLine($"Att betala: {sum + vat}");
}
```

**Fråga till salen:** Hur många saker gör metoden?

---

## Sammanfattning

- Code smells är tecken, inte fel.
- Hitta dem genom att läsa koden högt.
