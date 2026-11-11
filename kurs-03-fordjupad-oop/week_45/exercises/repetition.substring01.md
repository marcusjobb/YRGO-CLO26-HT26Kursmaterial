# Repetition — Substring

`Substring` klipper ut en bit ur en sträng.

```csharp
string name = "Marcus";
//              012345   ← index (börjar alltid på 0)
```

Två varianter:
- `name.Substring(2)` — från index 2 till slutet → `"rcus"`
- `name.Substring(2, 3)` — från index 2, ta 3 tecken → `"rcu"`

---

🟢 Prova alla tre raderna och se att du förstår output.

```csharp
string text = "programmering";
//             0123456789...

Console.WriteLine(text.Substring(0, 5));   // ?
Console.WriteLine(text.Substring(7));       // ?
Console.WriteLine(text.Substring(3, 4));    // ?
```

**Förväntad output:**
```
progr
ering
gram
```

---

🟢 Plocka ut förkortningen. Strängen innehåller ett namn och en titel separerade med komma.

```csharp
string info = "Andersson, Anna";
// Plocka ut förnamnet "Anna" med Substring + IndexOf
// Tips: hitta var ", " slutar med IndexOf(", ") + 2
```

**Förväntad output:**
```
Anna
```

---

🟡 Dölj ett kortnummer. Visa bara de sista 4 siffrorna, resten ska vara `*`.

```csharp
string cardNumber = "1234567890123456";
// Hämta de sista 4 tecknen med Substring(cardNumber.Length - 4)
// Sätt ihop med "************" framför
```

**Förväntad output:**
```
************3456
```

---

🟡 En logg-rad ser ut så här: `"2026-11-04 ERROR Databasen svarar inte"`.
Plocka ut datum, nivå och meddelandet var för sig.

```csharp
string logLine = "2026-11-04 ERROR Databasen svarar inte";
// Datum = de första 10 tecknen
// Nivå  = tecken 11–15 (Substring(11, 5) och Trim())
// Meddelande = från index 17 till slutet
```

**Förväntad output:**
```
Datum: 2026-11-04
Nivå: ERROR
Meddelande: Databasen svarar inte
```
