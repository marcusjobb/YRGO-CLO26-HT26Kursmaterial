---
marp: true
theme: nion-dark
paginate: true
---

<!-- _class: title -->

# Metoder

### Skriv en gång — kör hur många gånger du vill

_Kurs 01 · Vecka 4 · Nion Education_

---

## Problemet

Tänk dig att du ska hälsa på tre personer i ett program:

```csharp
Console.WriteLine("Hej, Anna!");
Console.WriteLine("Välkommen till kursen.");
Console.WriteLine("---");

Console.WriteLine("Hej, Björn!");
Console.WriteLine("Välkommen till kursen.");
Console.WriteLine("---");

Console.WriteLine("Hej, Camilla!");
Console.WriteLine("Välkommen till kursen.");
Console.WriteLine("---");
```

Fungerar — men tänk om hälsningsfrasen ska ändras. Du måste ändra på **tre ställen**.

> 💬 _"Vad händer om du glömmer ett av ställena?"_

---

## Lösningen: en metod

Skriv logiken en gång. Anropa den hur många gånger du vill.

```csharp
static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
    Console.WriteLine("Välkommen till kursen.");
    Console.WriteLine("---");
}

PrintGreeting("Anna");
PrintGreeting("Björn");
PrintGreeting("Camilla");
```

Vill du ändra hälsningsfrasen? Du ändrar på **ett ställe** — resten uppdateras automatiskt.

---

## Metodens anatomi

```csharp
static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
}
```

```
static   void          PrintGreeting  (string name)
  │        │                 │              │
  │      returtyp          namn         parameter
  │    (inget värde                    (indata)
  │     tillbaka)
  │
nyckelord — tillhör
klassen direkt
```

Allt innan `{ }` kallas **signaturen** — metodens kontrakt.

---

## void — gör något, returnerar ingenting

En `void`-metod utför en uppgift men skickar inget värde tillbaka.

```csharp
static void PrintGreeting(string name)
{
    Console.WriteLine("Hej, " + name + "!");
}
```

Du anropar den — den kör koden — sedan är det klart.

```csharp
PrintGreeting("Anna");   // kör metoden
// inget värde att spara
```

> 💬 _"Tänk på det som att trycka på en knapp — nåt händer, men du får inget tillbaka."_

---

## Returvärde — metoden beräknar något åt dig

När en metod ska beräkna eller ta fram ett värde anger du returtypen istället för `void`.

```csharp
static int Add(int a, int b)
{
    return a + b;
}
```

Du tar emot resultatet i en variabel:

```csharp
int sum = Add(3, 4);
Console.WriteLine("Summan är: " + sum);   // Summan är: 7
```

> 💬 _"Tänk på det som en miniräknare — du matar in siffror och får ett svar."_

---

## Parametrar och argument

Parametrar är de variabler metoden tar emot som indata.

```csharp
static int Add(int a, int b)   // a och b är PARAMETRAR
{
    return a + b;
}
```

Argument är de faktiska värden du skickar in vid anropet.

```csharp
int result = Add(3, 4);        // 3 och 4 är ARGUMENT
```

**Samma sak — olika tidpunkt:**
- Parameter = variabelnamnet när du **skriver** metoden
- Argument = det faktiska värdet när du **anropar** metoden

---

## Fler returtyper

Metoder kan returnera vilket värde som helst — string, bool, double…

```csharp
static string GetFullName(string firstName, string lastName)
{
    return firstName + " " + lastName;
}

static bool IsEven(int number)
{
    return number % 2 == 0;
}
```

```csharp
string name = GetFullName("Anna", "Svensson");
bool even   = IsEven(6);

Console.WriteLine(name);    // Anna Svensson
Console.WriteLine(even);    // True
```

---

## return — avsluta och skicka tillbaka

`return` gör två saker: skickar tillbaka ett värde **och** avslutar metoden direkt.

```csharp
static int Add(int a, int b)
{
    return a + b;   // skickar tillbaka summan — metoden är klar
    // Kod här körs ALDRIG
}
```

I en `void`-metod kan du använda `return;` (utan värde) för att avsluta tidigt:

```csharp
static void PrintPositive(int number)
{
    if (number <= 0)
        return;   // avsluta tidigt, skriv ingenting

    Console.WriteLine(number);
}
```

---

## Hur anropet flödar

```
static void Main(string[] args)          static int Add(int a, int b)
{                                        {
    int result = Add(3, 4);  ───────────►    return a + b;
    Console.WriteLine(result); ◄───────────  // returnerar 7
}                                        }
```

1. `Main` anropar `Add` med argumenten `3` och `4`
2. `Add` kör koden — beräknar `3 + 4`
3. `return` skickar tillbaka `7`
4. `Main` sparar `7` i `result` och fortsätter

> 💬 _"Programmet hoppar in i metoden, gör sitt jobb, hoppar tillbaka — exakt dit det kom ifrån."_

---

## Varför metoder?

| Utan metoder | Med metoder |
|---|---|
| Kopierar kod på flera ställen | Skriver logiken en gång |
| Ändrar på tre ställen om något ändras | Ändrar på ett ställe |
| Svårt att namnge vad kod gör | Metoden har ett beskrivande namn |
| Svårt att testa isolerat | Varje metod kan testas för sig |

En metod som gör **en enda sak** och har ett **bra namn** är lättare att läsa än hundra rader i `Main`.

> 💬 _"Vad heter metoden? Då vet du vad den gör — utan att läsa koden inuti."_

---

## Sammanfattning

```csharp
// Deklaration
static returtyp MetodNamn(typ param1, typ param2)
{
    // kropp
    return värde;   // om inte void
}

// Anrop
MetodNamn(argument1, argument2);
int result = MetodNamn(argument1, argument2);
```

Tre saker att komma ihåg:
1. `void` — gör något, returnerar ingenting
2. `return` — skickar tillbaka värdet och avslutar metoden
3. Parameter = i definitionen · Argument = vid anropet

---

<!-- _class: title -->

# Nu är det din tur

### Övningar finns i `exercises/`

🟢 Börja med en enkel `void`-metod — skriv ut ett meddelande  
🟡 Bygg vidare: lägg till parametrar och testa olika argument  
🔴 Skapa en metod som returnerar ett värde och använd det i `Main`

_Ta det steg för steg. Fråga om du fastnar._
