---
marp: true
theme: nion-dark
paginate: true
---

# Records

**Vecka 48 · Hellweek · Hidden gems**

---

## En rad, en klass

```csharp
public record Customer(string Name, int Age);

var a = new Customer("Ada", 36);
var b = new Customer("Ada", 36);
Console.WriteLine(a == b);          // True
```

Två objekt är lika om deras värden är lika, inte om de är samma objekt i minnet.

---

## Oföränderlig kopia med with

```csharp
var older = a with { Age = 37 };
Console.WriteLine(a.Age);           // 36. a är oförändrad.
```

---

## Wow: samma klass, två sätt att jämföra

En vanlig klass med `==` jämför referenser. En `record` jämför innehållet. Samma kod, olika svar.

---

## Sammanfattning

- En `record` är en klass för data.
- Den jämförs på innehåll.
- `with` gör en ändrad kopia.
