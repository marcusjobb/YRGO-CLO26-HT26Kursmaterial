---
marp: true
theme: nion-dark
paginate: true
---

<!-- WOW-MOMENT: dynamic kompilerar men kraschar först när raden körs -->

# dynamic

**Vecka 48 · Hellweek · Hidden gems**

---

## Vad är dynamic?

En variabel som kompilatorn inte kontrollerar typen av. Typen avgörs först när programmet körs.

```csharp
dynamic value = 5;
value = "hej";           // Kompilerar. Typen ändras.
Console.WriteLine(value.Length);
```

---

## Wow: det kompilerar, men kraschar

```csharp
dynamic d = 3;
d.Foo();                 // Kompilerar. Kraschar när raden körs.
```

Inga röda streck. Inget IntelliSense. Felet syns först i körningen.

---

## När används det?

- Data från JSON när strukturen inte är känd i förväg
- COM-objekt och gamla API:er
- Reflektion, där man arbetar med objekt utan att veta deras typ

**Huvudregel:** Använd `var` eller en riktig typ om du kan. `dynamic` är ett undantag.

---

## Summan av kardemumman

- `dynamic` kontrolleras först i körningen.
- Det ger inga kompileringsfel men kraschar när något är fel.
- Använd det sparsamt.
