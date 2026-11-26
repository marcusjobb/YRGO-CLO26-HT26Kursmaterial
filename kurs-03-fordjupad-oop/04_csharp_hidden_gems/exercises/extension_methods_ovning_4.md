# Stjärntecken från ett datum

**Frivillig.** Gör den om du vill träna mer. Den ingår inte i bedömningen.

**Tid:** ca 30 minuter.

**Förkunskap:** Extension methods och pattern matching med tuples.

Skriv en extension method som tar ett datum och returnerar stjärntecknet.

## Varför vill man ha den här?

Föreställ dig en horoskop-app, eller ett välkomstmejl från hotellet som skriver "Grattis på födelsedagen, kära Vädur!". Datumgränserna för stjärntecknen är knepiga. Ett tecken börjar i en månad och slutar i nästa, och Stenbocken går dessutom över nyår. Den logiken vill du skriva och testa en gång, inte kopiera.

När den finns som `födelsedag.ZodiacSign()` kan samma kod användas i konsolprogrammet, på en webbsida och i ett test.

Stjärntecknet är något ett datum *har*, inte något det *gör*. Därför passar det också bra som en **extension property** i C# 14: `födelsedag.ZodiacSign`, utan parenteser. Det provar du i varianten längst ner.

## Uppgift

1. Skriv en extension method `ZodiacSign(this DateOnly date)` som returnerar ett stjärntecken på svenska, till exempel "Vädur".
2. Läs in ett datum med `DateOnly.Parse(Console.ReadLine())`. Använd formatet `ÅÅÅÅ-MM-DD`.
3. Skriv ut stjärntecknet för datumet.
4. **🔴** Låt användaren skriva in flera datum, ett i taget, tills hen skriver `slut`.

Fyll i det som står `/* din kod */`.

```csharp
// 2–3. Ett datum
Console.Write("Ange ett datum (ÅÅÅÅ-MM-DD): ");
/* din kod 2–3:
   läs in en rad och gör om den till ett DateOnly
   skriv ut "Stjärntecken: " och stjärntecknet */

// 4. 🔴 Flera datum
/* din kod 4:
   upprepa:
     skriv "Datum (eller slut): "
     läs in en rad
     om raden är slut, avbryt
     annars gör om raden till ett datum och skriv ut stjärntecknet */

public static class DateOnlyExtensions
{
    public static string ZodiacSign(this DateOnly date)
    {
        /* din kod 1:
           titta på månad och dag tillsammans
           returnera tecknet vars datum passar:
             21/3–19/4   Vädur
             20/4–20/5   Oxen
             21/5–20/6   Tvillingarna
             21/6–22/7   Kräftan
             23/7–22/8   Lejonet
             23/8–22/9   Jungfrun
             23/9–22/10  Vågen
             23/10–21/11 Skorpionen
             22/11–21/12 Skytten
             22/12–19/1  Stenbocken
             20/1–18/2   Vattumannen
             annars      Fiskarna */
    }
}
```

Exempeloutput, när du skriver in `1990-05-01`, `2000-03-25`, `1985-12-24` och `slut`:

```text
Ange ett datum (ÅÅÅÅ-MM-DD): 1990-05-01
Stjärntecken: Oxen
Datum (eller slut): 2000-03-25
Vädur
Datum (eller slut): 1985-12-24
Stenbocken
Datum (eller slut): slut
```

<details markdown="1">
<summary>Tips 1: två värden på en gång</summary>

Ett stjärntecken beror på både månad och dag. Lägg ihop dem i en tuple, `(date.Month, date.Day)`, och gör ett switch-uttryck på den. Då blir det kort och tydligt.

</details>

<details markdown="1">
<summary>Tips 2: ett tecken spänner över två månader</summary>

Vädur är "dag 21 eller senare i mars" eller "dag 19 eller tidigare i april". I ett mönster skrivs det `(3, >= 21) or (4, <= 19)`. Fiskarna kan få `_`, eftersom det är det enda som blir kvar.

</details>

<details markdown="1">
<summary>Tips 3: läsa in och loopa</summary>

`Console.ReadLine()` kan returnera `null`, så skriv `Console.ReadLine()!` för att slippa varningen. För 🔴 passar en `while (true)`-loop. Kolla om raden är `"slut"` och använd `break` för att gå ur loopen. Annars anropar du `DateOnly.Parse(input!).ZodiacSign()`.

</details>

<details markdown="1">
<summary>Lösningsförslag</summary>

```csharp
// 2–3. Ett datum
Console.Write("Ange ett datum (ÅÅÅÅ-MM-DD): ");
DateOnly date = DateOnly.Parse(Console.ReadLine()!);
Console.WriteLine($"Stjärntecken: {date.ZodiacSign()}");

// 4. 🔴 Flera datum
while (true)
{
    Console.Write("Datum (eller slut): ");
    string? input = Console.ReadLine();
    if (input == "slut") break;
    Console.WriteLine(DateOnly.Parse(input!).ZodiacSign());
}

public static class DateOnlyExtensions
{
    public static string ZodiacSign(this DateOnly date)
    {
        return (date.Month, date.Day) switch
        {
            (3, >= 21) or (4, <= 19) => "Vädur",
            (4, >= 20) or (5, <= 20) => "Oxen",
            (5, >= 21) or (6, <= 20) => "Tvillingarna",
            (6, >= 21) or (7, <= 22) => "Kräftan",
            (7, >= 23) or (8, <= 22) => "Lejonet",
            (8, >= 23) or (9, <= 22) => "Jungfrun",
            (9, >= 23) or (10, <= 22) => "Vågen",
            (10, >= 23) or (11, <= 21) => "Skorpionen",
            (11, >= 22) or (12, <= 21) => "Skytten",
            (12, >= 22) or (1, <= 19) => "Stenbocken",
            (1, >= 20) or (2, <= 18) => "Vattumannen",
            _ => "Fiskarna"
        };
    }
}
```

</details>

## Variant: som egenskap

Från C# 14 (.NET 10) kan en extension också vara en **egenskap** (property), i stället för en metod. Egenskapen ska vara **get only**: stjärntecknet beräknas från datumet, så det finns inget att sätta.

1. Skriv om `ZodiacSign` till en get-only egenskap i ett `extension`-block.
2. Använd den som `date.ZodiacSign` (utan parenteser).
3. Försök att tilldela egenskapen. Vad säger kompilatorn?

Fyll i det som står `/* din kod */`.

```csharp
var date = new DateOnly(1990, 5, 1);
Console.WriteLine(date.ZodiacSign);

public static class DateOnlyExtensions
{
    /* din kod:
       skapa ett extension-block för DateOnly
       lägg en egenskap ZodiacSign i blocket, som bara går att läsa
       låt den ge samma svar som din metod ZodiacSign() */
}
```

Exempeloutput:

```text
Oxen
```

<details markdown="1">
<summary>Tips 1: hur ett extension-block ser ut</summary>

Blocket står inne i den statiska klassen och skrivs `extension(DateOnly date) { ... }`. Parametern `date` i parentesen gäller för allt i blocket. Därför behövs inget `this` längre.

</details>

<details markdown="1">
<summary>Tips 2: en egenskap som bara går att läsa</summary>

Skriv egenskapen med en pil: `public string ZodiacSign => ...;`. Då får den bara `get`, och ingen kan sätta den.

</details>

<details markdown="1">
<summary>Tips 3: återanvänd switchen</summary>

Switch-uttrycket på `(date.Month, date.Day)` från din metod kan flyttas in efter pilen nästan oförändrat. Det enda som skiljer är att `date` nu kommer från blocket, inte från metodens parameter.

</details>

<details markdown="1">
<summary>Lösningsförslag: egenskap</summary>

```csharp
var date = new DateOnly(1990, 5, 1);
Console.WriteLine(date.ZodiacSign);   // Oxen

public static class DateOnlyExtensions
{
    extension(DateOnly date)
    {
        public string ZodiacSign => (date.Month, date.Day) switch
        {
            (3, >= 21) or (4, <= 19) => "Vädur",
            (4, >= 20) or (5, <= 20) => "Oxen",
            (5, >= 21) or (6, <= 20) => "Tvillingarna",
            (6, >= 21) or (7, <= 22) => "Kräftan",
            (7, >= 23) or (8, <= 22) => "Lejonet",
            (8, >= 23) or (9, <= 22) => "Jungfrun",
            (9, >= 23) or (10, <= 22) => "Vågen",
            (10, >= 23) or (11, <= 21) => "Skorpionen",
            (11, >= 22) or (12, <= 21) => "Skytten",
            (12, >= 22) or (1, <= 19) => "Stenbocken",
            (1, >= 20) or (2, <= 18) => "Vattumannen",
            _ => "Fiskarna"
        };
    }
}
```

Steg 3: Att tilldela `date.ZodiacSign = "Vädur";` ger ett kompileringsfel (CS0200). Egenskapen saknar `set`, och det är meningen: stjärntecknet följer av datumet.

</details>

<!-- nion-copyright -->
<sub>© 2025–2026 Nion Education. Alla rättigheter förbehållna. Fritt att läsa, men får inte användas av andra skolor eller utbildningsanordnare utan skriftligt tillstånd från Nion Education. Delar bygger på källor som anges i respektive dokument.</sub>
