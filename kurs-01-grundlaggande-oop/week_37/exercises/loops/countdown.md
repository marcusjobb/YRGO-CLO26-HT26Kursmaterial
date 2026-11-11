# Övning — Nedräkning

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

Två korta loopar. Steg 1 räknar ned med en `for`-loop. Steg 2 räknar jämna tal med en `while`-loop.

---

## Steg 1: Nedräkning med for

Skriv en `for`-loop som räknar ned från 10 till och med 0. Skriv ut varje tal på en egen rad. När loopen är klar, skriv ut: `Dags att börja!`

### Förväntad output
```plaintext
10
9
8
7
6
5
4
3
2
1
0
Dags att börja!
```

<details><summary>Hur stegar man bakåt i en for-loop?</summary>

Startvärdet är 10, loopen ska köra så länge `i >= 0`, och steget ska minska:

```csharp
for (int i = 10; i >= 0; i--)
{
    Console.WriteLine(i);
}
```

`i--` minskar `i` med 1 efter varje varv — precis som `i++` ökar, fast åt andra hållet.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
for (int i = 10; i >= 0; i--)
{
    Console.WriteLine(i);
}

Console.WriteLine("Dags att börja!");
```

</details>

---

## Steg 2: Jämna tal med while

Skriv en `while`-loop som skriver ut alla jämna tal mellan 1 och 20 (inklusive 2 och 20).

### Förväntad output
```plaintext
2
4
6
8
10
12
14
16
18
20
```

<details><summary>Tips: hur vet jag om ett tal är jämnt?</summary>

Modulo-operatorn `%` ger dig resten vid heltalsdivision. Ett tal är jämnt om resten är noll:

```csharp
if (tal % 2 == 0)
{
    // tal är jämnt
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int tal = 1;

while (tal <= 20)
{
    if (tal % 2 == 0)
    {
        Console.WriteLine(tal);
    }
    tal++;
}
```

Alternativt — stega med 2 direkt:

```csharp
int tal = 2;

while (tal <= 20)
{
    Console.WriteLine(tal);
    tal += 2;
}
```

Båda ger samma output. Den andra varianten är lite effektivare — den hoppar direkt till nästa jämna tal utan att behöva kontrollera alla udda tal på vägen.

</details>
