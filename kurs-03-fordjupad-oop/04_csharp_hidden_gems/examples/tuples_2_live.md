# Tuples: namngivna värden, byte och villkor

En tuple är ett par eller fler värden som vi kan skicka runt utan att skapa en klass. I det här exemplet ser vi tre sätt att använda dem.

## Övningen

Fyll i de tre ställena där det står `/* din kod */`.

```csharp
var person = ("Maria", 25);
/* din kod: skriv ut namnet */

var person2 = (name: "James Sunderland", age: 27);
/* din kod: skriv ut åldern, med fältets namn */

int a = 10;
int b = 5;
/* din kod: byt plats på a och b om a är större än b, utan en extra variabel */
Console.WriteLine($"{a} {b}");
```

Exempeloutput:

```text
Maria
27
5 10
```

<details markdown="1">
<summary>Tips 1: en tuple utan namn</summary>

Har fälten inga namn heter de `Item1`, `Item2` och så vidare. Namnet är det första värdet i `person`.

</details>

<details markdown="1">
<summary>Tips 2: en tuple med namn</summary>

I `person2` har fälten fått namnen `name` och `age`. Då skriver du `person2.age` i stället för `person2.Item2`.

</details>

<details markdown="1">
<summary>Tips 3: byta plats</summary>

Bygg en ny tuple med värdena i omvänd ordning, `(b, a)`, och tilldela den till `(a, b)`. Båda sidor räknas ut innan något skrivs över, så du behöver ingen `tmp`.

</details>

<details markdown="1">
<summary>Tips 4: bara när det behövs</summary>

Bytet ska bara ske när `a` är större än `b`. Lägg det i en `if`. Prova sedan med `a = 3` och se att ordningen inte ändras.

</details>

## Genomgång från lektionen

Läs vidare när du har försökt själv. Här är lösningen vi skrev tillsammans.

Först en tuple utan namn. Elementen får namnen `Item1` och `Item2`:

```csharp
var person = ("Maria", 25);
Console.WriteLine(person.Item1);   // Ada
```

Utskriften blir `Maria`. Kommentaren i koden säger `Ada`, och det stämmer inte. `Item1` är det första värdet, alltså `"Maria"`.

Nu en tuple med namn. Då läser vi värdena med namnen i stället för `Item1` och `Item2`:

```csharp
// Mer lättanvänt
var person2 = (name:"James Sunderland", age:27);
Console.WriteLine(person2.age);
```

Det är lättare att läsa, eftersom `person2.age` säger vad värdet är. Utskriften blir `27`.

Nu byter vi två värden. Först med en tillfällig variabel, `tmp`, som vi sett förut:

```csharp
int value1 = 10;
int value2 = 5;

// Towers of Hanoi
// 10 (tom) 5
//      5   10
// 5 (tom) 10
int tmp = value1;
value1 = value2;
value2 = tmp;
// default swap algoritm
Console.WriteLine($"{value1} {value2}");
```

Tre rader och en extra variabel. Utskriften blir `5 10`.

Samma sak med en tuple. Vi skriver båda värdena i en rad, utan `tmp`:

```csharp
int a = 10;
int b = 5;

if (a > b)
{
    (a, b) = (b, a);
}

Console.WriteLine($"{a} {b}");
```

`(b, a)` bygger en tuple med de två värdena i omvänd ordning. Den tilldelas sedan till `(a, b)`. Villkoret `if (a > b)` gör att bytet bara sker när de står i fel ordning. Utskriften blir `5 10`.

## Varför har vi nytta av detta?

Vi slipper skapa en klass för två värden som bara används på ett ställe. Ett byte med en tuple är dessutom kortare och tydligare än med en tillfällig variabel.

## När kan vi använda detta?

Det används när en metod behöver ge tillbaka två värden som hör ihop, och när vi vill byta två värden eller sätta flera variabler i en rad. Vi kan också ge fälten namn, så att den som läser koden förstår vad värdena betyder.

## Hela koden

```csharp
Console.WriteLine("Hello, World!");

var person = ("Maria", 25);
Console.WriteLine(person.Item1);   // Ada

// Mer lättanvänt
var person2 = (name:"James Sunderland", age:27);
Console.WriteLine(person2.age);

// -------------------------

// Swap algoritmen
// Vi två värden där vi måste ha den lägsta (eller högsta)
// i första variabeln, och det högra i andra variabeln
int value1 = 10;
int value2 = 5;

// Towers of Hanoi
// 10 (tom) 5
//      5   10
// 5 (tom) 10
int tmp = value1;
value1 = value2;
value2 = tmp;
// default swap algoritm
Console.WriteLine($"{value1} {value2}");

int a = 10;
int b = 5;

if (a > b)
{
    (a, b) = (b, a);
}

Console.WriteLine($"{a} {b}");
```
