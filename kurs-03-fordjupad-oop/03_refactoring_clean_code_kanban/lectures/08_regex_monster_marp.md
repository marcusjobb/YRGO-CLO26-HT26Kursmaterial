---
marp: true
theme: nion-dark
paginate: true
---

## Hjulet idag

En kund skriver in "XABC123Y". Appen släpper igenom det. Elin hittar felet på en eftermiddag.

---

# Regex: ankare, misstag och när det blir oläsligt

**Vecka 47 · Onsdag · Lektion 2 (10:00)**

---

## Ankare

```csharp
Regex.IsMatch("abc413 04xyz", @"\d{3} ?\d{2}")   // True!
Regex.IsMatch("abc413 04xyz", @"^\d{3} ?\d{2}$") // False
```

Utan `^` och `$` hittar mönstret en träff någonstans i texten.

---

## Vanliga misstag

- Glömt `@` före strängen.
- Glömt ankare.
- Försökt skriva ett komplett e-postmönster själv.

---

## Wow: samma svar, två sätt

- **Loop med villkor:** många rader.
- **Regex:** ett mönster.

**Live:** kör båda mot samma indata. Samma svar?

---

## När blir det oläsligt?

Om en kollega inte kan läsa mönstret på en minut, skriv en vanlig metod i stället, eller dela upp mönstret i namngivna delar.

---

## Sammanfattning

- Ankare gör att hela texten måste matcha.
- Testa med både bra och dåliga exempel.
