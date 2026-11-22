---
marp: true
theme: nion-dark
paginate: true
---

# Null-operatorerna

**Vecka 48 · Hellweek · Hidden gems**

---

## Tre operatorer

```csharp
string? name = null;

int length = name?.Length ?? 0;   // 0: inget objekt, då 0
name ??= "Gäst";                  // sätter "Gäst" om name är null
```

- `?.` går bara vidare om objektet inte är `null`.
- `??` ger ett reservvärde om värdet är `null`.
- `??=` sätter ett värde om variabeln är `null`.

---

## Samma sak, längre väg

```csharp
int length = 0;
if (name != null)
    length = name.Length;
```

Samma beteende, med fler rader och fler chanser att glömma något.

---

## Wow: en rad kan ersätta fem

Ta en metod med fem `if (x != null)`-kontroller. Skriv om den med `?.` och `??`. Samma utskrift, halva koden.

---

## Sammanfattning

- `?.` hoppar över `null` i stället för att krascha.
- `??` ger ett reservvärde.
- `??=` fyller i om det är tomt.
