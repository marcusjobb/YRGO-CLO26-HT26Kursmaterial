---
marp: true
theme: nion-dark
paginate: true
---

## Hjulet idag

Klassen `BookingManager` gör tre jobb. Elin delar upp den. Nils undrar varför det behövs tre klasser för en verkstad.

---

# SOLID, DRY och KISS

**Vecka 47 · Måndag · Lektion 3 (13:00)**

---

## DRY: varje kunskap på ett ställe

```csharp
const decimal VatRate = 0.25m;

decimal CalculateVat(decimal sum) => sum * VatRate;
```

Ändrar du momssatsen på ett ställe följer både kvitton och fakturor med.

---

## KISS: keep it simple, students

Den enklaste lösningen som fungerar är oftast den bästa.

Fråga dig: kan en ny kollega förstå det här på en minut?

---

## SRP: en klass, ett skäl att ändras

En klass som både räknar och skriver ut ändras av två skäl. Dela upp den, så ändras de separat.

---

## SoC: gränssnitt, logik och data

- Gränssnitt: visar kvitton
- Logik: räknar moms
- Data: sparar kvitton

Ett ändrat gränssnitt ska inte kunna förstöra logiken.

---

## Sammanfattning

- DRY: en sanning, en plats.
- KISS: enklast som fungerar.
- SRP och SoC: ansvar hålls isär.
