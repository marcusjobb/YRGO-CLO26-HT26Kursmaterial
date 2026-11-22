---
marp: true
theme: nion-dark
paginate: true
---

<!-- WOW-MOMENT: utan _ i switch-uttrycket varnar kompilatorn om fall som saknas -->

# Pattern matching

**Vecka 48 · Hellweek · Hidden gems**

---

## Typkontroll och hämtning i ett steg

```csharp
if (obj is int number && number > 10)
    Console.WriteLine($"Stort tal: {number}");
```

Du kontrollerar typen och får en variabel i samma rad.

---

## switch-uttryck

```csharp
string Category(int age) => age switch
{
    < 13 => "barn",
    < 20 => "tonåring",
    _ => "vuxen"
};
```

Mönstret läses uppifrån och ned. Den första träffen vinner.

---

## Wow: när du glömmer `_`

Utan `_` får du en varning om att fallet inte täcks. Kompilatorn kontrollerar att du tänkt på alla fall.

---

## Sammanfattning

- `is` kontrollerar typ och hämtar värdet.
- `switch`-uttryck gör en beslutstabell på några rader.
- `when` lägger till ett villkor på ett fall.
