# Övning — Veckodagen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

Du får ett tal (1–7) och ska skriva ut rätt veckodag på svenska.

Ingen inläsning från tangentbordet — byt värdet direkt i koden och kör om.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

Deklarera en variabel:

```csharp
int dag = 1;
```

Skriv en `switch`-sats som skriver ut rätt veckodag för värdet.

| Värde | Dag |
|-------|-----|
| 1 | Måndag |
| 2 | Tisdag |
| 3 | Onsdag |
| 4 | Torsdag |
| 5 | Fredag |
| 6 | Lördag |
| 7 | Söndag |

Om värdet inte är 1–7 ska programmet skriva ut: `Ogiltigt dagsnummer.`

---

## Förväntad output för dag = 3
```plaintext
Onsdag
```

## Förväntad output för dag = 5
```plaintext
Fredag
```

## Förväntad output för dag = 9
```plaintext
Ogiltigt dagsnummer.
```

---

<details><summary>Tips: grundstruktur</summary>

```csharp
int dag = 1;

switch (dag)
{
    case 1:
        Console.WriteLine("Måndag");
        break;
    case 2:
        // fortsätt...
        break;
    default:
        Console.WriteLine("Ogiltigt dagsnummer.");
        break;
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int dag = 1;

switch (dag)
{
    case 1:
        Console.WriteLine("Måndag");
        break;
    case 2:
        Console.WriteLine("Tisdag");
        break;
    case 3:
        Console.WriteLine("Onsdag");
        break;
    case 4:
        Console.WriteLine("Torsdag");
        break;
    case 5:
        Console.WriteLine("Fredag");
        break;
    case 6:
        Console.WriteLine("Lördag");
        break;
    case 7:
        Console.WriteLine("Söndag");
        break;
    default:
        Console.WriteLine("Ogiltigt dagsnummer.");
        break;
}
```

</details>
