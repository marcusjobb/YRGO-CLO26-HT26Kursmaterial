# Övning — Räkna med metoder

🟡

Du ska skriva metoder som tar emot tal och returnerar ett beräknat värde. Fokus är på returtyper — `int` och `double`.

---

## Steg 1: Kvadraten av ett tal (int)

Skapa en metod `Square` som tar emot ett heltal och returnerar talets kvadrat (talet gånger sig självt).

Anropa den med `4`, `7` och `10` och skriv ut resultaten.

### Förväntad output
```plaintext
4 i kvadrat är 16
7 i kvadrat är 49
10 i kvadrat är 100
```

<details><summary>Tips: hur returnerar en metod ett int-värde?</summary>

```csharp
static int Square(int number)
{
    return number * number;
}
```

Ta emot resultatet i en variabel eller använd det direkt i `Console.WriteLine`:

```csharp
int result = Square(4);
Console.WriteLine("4 i kvadrat är " + result);

// eller direkt:
Console.WriteLine("4 i kvadrat är " + Square(4));
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Console.WriteLine("4 i kvadrat är " + Square(4));
Console.WriteLine("7 i kvadrat är " + Square(7));
Console.WriteLine("10 i kvadrat är " + Square(10));

static int Square(int number)
{
    return number * number;
}
```

</details>

---

## Steg 2: Cirkelns area (double)

Skapa en metod `CircleArea` som tar emot radien som `double` och returnerar cirkelns area. Formeln är: `area = π * r * r`

I C# heter π `Math.PI`.

Anropa metoden med radien `3.0`, `5.0` och `10.0`. Runda av till två decimaler med `Math.Round(värde, 2)`.

### Förväntad output
```plaintext
Radien 3 ger arean 28,27
Radien 5 ger arean 78,54
Radien 10 ger arean 314,16
```

<details><summary>Tips: Math.PI och Math.Round</summary>

```csharp
static double CircleArea(double radius)
{
    return Math.PI * radius * radius;
}
```

Runda av resultatet när du skriver ut:

```csharp
double area = CircleArea(3.0);
Console.WriteLine("Radien 3 ger arean " + Math.Round(area, 2));
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Console.WriteLine("Radien 3 ger arean " + Math.Round(CircleArea(3.0), 2));
Console.WriteLine("Radien 5 ger arean " + Math.Round(CircleArea(5.0), 2));
Console.WriteLine("Radien 10 ger arean " + Math.Round(CircleArea(10.0), 2));

static double CircleArea(double radius)
{
    return Math.PI * radius * radius;
}
```

</details>

---

## Steg 3: Celsius till Fahrenheit (double)

Skapa en metod `ToFahrenheit` som tar emot en temperatur i Celsius och returnerar den omvandlad till Fahrenheit. Formeln är: `F = C * 9 / 5 + 32`

Anropa metoden med `0`, `20` och `100` grader Celsius.

### Förväntad output
```plaintext
0°C = 32°F
20°C = 68°F
100°C = 212°F
```

<details><summary>Tips: tänk på datatypen</summary>

Om du skriver `9 / 5` i C# får du `1` — heltalsdivision. Skriv `9.0 / 5` istället för att få ett decimaltal.

```csharp
static double ToFahrenheit(double celsius)
{
    return celsius * 9.0 / 5 + 32;
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Console.WriteLine("0°C = " + ToFahrenheit(0) + "°F");
Console.WriteLine("20°C = " + ToFahrenheit(20) + "°F");
Console.WriteLine("100°C = " + ToFahrenheit(100) + "°F");

static double ToFahrenheit(double celsius)
{
    return celsius * 9.0 / 5 + 32;
}
```

</details>
