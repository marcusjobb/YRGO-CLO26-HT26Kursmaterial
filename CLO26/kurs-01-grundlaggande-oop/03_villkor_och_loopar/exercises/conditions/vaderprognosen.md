# Övning — Väderprognosen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

En väderapp visar en väderkod (1–5) och du ska skriva ut en beskrivning av vädret och ett klädråd.

Ingen inläsning från tangentbordet — byt värdet direkt i koden och kör om.

> Sitter du fast i mer än 15 minuter? Be om hjälp — det är inte ett tecken på att du inte kan, det är precis vad 15-minutersregeln är till för.

---

## Uppgiften

Deklarera en variabel:

```csharp
int vaderkod = 1;
```

Skriv en `switch`-sats som matchar `vaderkod` mot tabellen nedan och skriver ut väder och klädråd.

| Kod | Väder | Klädråd |
|-----|-------|---------|
| 1 | Soligt och varmt | T-shirt och solglasögon |
| 2 | Molnigt men torrt | En lätt jacka räcker |
| 3 | Regn | Ta med paraply |
| 4 | Snö | Varma kläder och halksäkra skor |
| 5 | Åska | Stanna inomhus om du kan |

Om koden inte är 1–5 ska programmet skriva ut: `Okänd väderkod.`

---

## Förväntad output för vaderkod = 1
```plaintext
Väder: Soligt och varmt
Tips: T-shirt och solglasögon
```

## Förväntad output för vaderkod = 3
```plaintext
Väder: Regn
Tips: Ta med paraply
```

## Förväntad output för vaderkod = 7
```plaintext
Okänd väderkod.
```

---

<details><summary>Tips: variabler eller direkt utskrift?</summary>

Du kan antingen skriva ut direkt i varje case, eller spara i variabler utanför switch och skriva ut efteråt — precis som i KaraktärsSkapare-exemplet. Prova båda sätten och se vad du föredrar.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int vaderkod = 1;

switch (vaderkod)
{
    case 1:
        Console.WriteLine("Väder: Soligt och varmt");
        Console.WriteLine("Tips: T-shirt och solglasögon");
        break;
    case 2:
        Console.WriteLine("Väder: Molnigt men torrt");
        Console.WriteLine("Tips: En lätt jacka räcker");
        break;
    case 3:
        Console.WriteLine("Väder: Regn");
        Console.WriteLine("Tips: Ta med paraply");
        break;
    case 4:
        Console.WriteLine("Väder: Snö");
        Console.WriteLine("Tips: Varma kläder och halksäkra skor");
        break;
    case 5:
        Console.WriteLine("Väder: Åska");
        Console.WriteLine("Tips: Stanna inomhus om du kan");
        break;
    default:
        Console.WriteLine("Okänd väderkod.");
        break;
}
```

</details>
