# Övning — Kvittot

🔴

Du ska bygga ett enkelt kvittosystem med hjälp av metoder. Varje metod har ett tydligt ansvar — beräkna, formatera eller skriva ut. Stegen bygger på varandra.

---

## Steg 1: Beräkna totalpriset

Skapa en metod `CalculateTotal` som tar emot ett pris (`double`) och ett antal (`int`) och returnerar det totala priset som `double`.

Testa med tre produkter och skriv ut resultaten:

| Produkt | Pris | Antal |
|---------|------|-------|
| Kaffe | 45.50 | 3 |
| Smörgås | 32.00 | 2 |
| Juice | 28.75 | 4 |

### Förväntad output
```plaintext
Totalt: 136,5
Totalt: 64
Totalt: 115
```

<details><summary>Tips: returtyp double, parametrar double och int</summary>

```csharp
static double CalculateTotal(double price, int quantity)
{
    return price * quantity;
}
```

```csharp
Console.WriteLine("Totalt: " + CalculateTotal(45.50, 3));
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Console.WriteLine("Totalt: " + CalculateTotal(45.50, 3));
Console.WriteLine("Totalt: " + CalculateTotal(32.00, 2));
Console.WriteLine("Totalt: " + CalculateTotal(28.75, 4));

static double CalculateTotal(double price, int quantity)
{
    return price * quantity;
}
```

</details>

---

## Steg 2: Formatera en kvitorad

Skapa en metod `FormatRow` som tar emot ett produktnamn (`string`), ett pris (`double`) och ett antal (`int`), och returnerar en färdigformaterad `string` som ser ut som en kvitorad.

### Förväntad output (när du anropar med samma tre produkter som ovan)
```plaintext
Kaffe        x3    136,50 kr
Smörgås      x2     64,00 kr
Juice        x4    115,00 kr
```

<details><summary>Tips: bygga strängar och anropa CalculateTotal inifrån FormatRow</summary>

Du kan anropa `CalculateTotal` inifrån `FormatRow` — metoder kan anropa andra metoder.

```csharp
static string FormatRow(string name, double price, int quantity)
{
    double total = CalculateTotal(price, quantity);
    return name + "   x" + quantity + "   " + total.ToString("F2") + " kr";
}
```

`ToString("F2")` formaterar ett `double`-värde med exakt två decimaler.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Console.WriteLine(FormatRow("Kaffe",   45.50, 3));
Console.WriteLine(FormatRow("Smörgås", 32.00, 2));
Console.WriteLine(FormatRow("Juice",   28.75, 4));

static double CalculateTotal(double price, int quantity)
{
    return price * quantity;
}

static string FormatRow(string name, double price, int quantity)
{
    double total = CalculateTotal(price, quantity);
    return $"{name,-12} x{quantity}  {total,8:F2} kr";
}
```

`{name,-12}` vänsterjusterar namnet i ett fält med 12 tecken. `{total,8:F2}` högerjusterar summan med två decimaler. Det ger en snygg tabell även om namnen är olika långa.

</details>

---

## Steg 3: Skriv ut hela kvittot

Skapa en metod `PrintReceipt` som tar emot tre produkter (namn, pris, antal) och skriver ut ett komplett kvitto med rubrik, rader och totalsumma.

### Förväntad output
```plaintext
========== KVITTO ==========
Kaffe        x3    136,50 kr
Smörgås      x2     64,00 kr
Juice        x4    115,00 kr
============================
Summa:             315,50 kr
```

<details><summary>Tips: beräkna summans totalt</summary>

Anropa `CalculateTotal` tre gånger och summera resultaten:

```csharp
double sum = CalculateTotal(p1, q1) + CalculateTotal(p2, q2) + CalculateTotal(p3, q3);
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
PrintReceipt(
    "Kaffe",   45.50, 3,
    "Smörgås", 32.00, 2,
    "Juice",   28.75, 4
);

static double CalculateTotal(double price, int quantity)
{
    return price * quantity;
}

static string FormatRow(string name, double price, int quantity)
{
    double total = CalculateTotal(price, quantity);
    return $"{name,-12} x{quantity}  {total,8:F2} kr";
}

static void PrintReceipt(
    string name1, double price1, int qty1,
    string name2, double price2, int qty2,
    string name3, double price3, int qty3)
{
    double sum = CalculateTotal(price1, qty1)
               + CalculateTotal(price2, qty2)
               + CalculateTotal(price3, qty3);

    Console.WriteLine("========== KVITTO ==========");
    Console.WriteLine(FormatRow(name1, price1, qty1));
    Console.WriteLine(FormatRow(name2, price2, qty2));
    Console.WriteLine(FormatRow(name3, price3, qty3));
    Console.WriteLine("============================");
    Console.WriteLine($"Summa:        {sum,12:F2} kr");
}
```

</details>
