---
marp: true
theme: nion-dark
paginate: true
---

## Hjulet idag

Sara skriver registreringsnumret "ABC 123" med mellanslag. Appen måste förstå det.

---

# Regex: mönster i text

**Vecka 47 · Onsdag · Lektion 1 (09:00)**

---

## Ett första exempel

```csharp
bool isValid = Regex.IsMatch("413 04", @"^\d{3} ?\d{2}$");
```

Läs från vänster till höger: tre siffror, ett eventuellt mellanslag, två siffror.

---

## De vanligaste delarna

| Mönster | Betyder |
|---|---|
| `\d` | en siffra |
| `\w` | bokstav, siffra eller understreck |
| `*` `+` `?` | noll eller fler, en eller fler, noll eller en |
| `{3}` | exakt tre gånger |

---

## Hitta och ersätt

```csharp
var match = Regex.Match("Lektion 2026-11-18", @"(\d{4})-(\d{2})-(\d{2})");
string year = match.Groups[1].Value;   // 2026
```

Parenteser blir grupper. Ersättning döljer uppgifter, till exempel kortnummer i loggar.

---

## Sammanfattning

- Regex beskriver text kompakt.
- Mönstret läses från vänster till höger.
