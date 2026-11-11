# Övning — Restaurangmenyn

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Du jobbar på en restaurang och behöver ett program som tar ett menyval och skriver ut maträttens namn och pris.

Ingen inläsning från tangentbordet — byt värdet direkt i koden och kör om.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

Deklarera en variabel:

```csharp
int val = 1;
```

Skriv en `switch`-sats som matchar `val` mot menyn nedan och skriver ut rätt rätt och pris.

| Val | Maträtt | Pris |
|-----|---------|------|
| 1 | Pasta Carbonara | 135 kr |
| 2 | Laxfilé med dillsås | 175 kr |
| 3 | Vegetarisk lasagne | 125 kr |
| 4 | Kycklingwok | 145 kr |
| 5 | Dagens soppa | 95 kr |

Om valet inte finns i menyn ska programmet skriva ut: `Det valet finns inte på menyn.`

---

## Förväntad output för val = 2
```plaintext
Laxfilé med dillsås
Pris: 175 kr
```

## Förväntad output för val = 5
```plaintext
Dagens soppa
Pris: 95 kr
```

## Förväntad output för val = 9
```plaintext
Det valet finns inte på menyn.
```

---

<details><summary>Tips: två Console.WriteLine per case</summary>

Varje case behöver skriva ut två rader — namn och pris. Båda skrivs ut innan `break`.

```csharp
case 1:
    Console.WriteLine("Pasta Carbonara");
    Console.WriteLine("Pris: 135 kr");
    break;
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int val = 1;

switch (val)
{
    case 1:
        Console.WriteLine("Pasta Carbonara");
        Console.WriteLine("Pris: 135 kr");
        break;
    case 2:
        Console.WriteLine("Laxfilé med dillsås");
        Console.WriteLine("Pris: 175 kr");
        break;
    case 3:
        Console.WriteLine("Vegetarisk lasagne");
        Console.WriteLine("Pris: 125 kr");
        break;
    case 4:
        Console.WriteLine("Kycklingwok");
        Console.WriteLine("Pris: 145 kr");
        break;
    case 5:
        Console.WriteLine("Dagens soppa");
        Console.WriteLine("Pris: 95 kr");
        break;
    default:
        Console.WriteLine("Det valet finns inte på menyn.");
        break;
}
```

</details>
