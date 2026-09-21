# Enum — labyrinten

Den här artikeln bygger på live-demo från lektion v39.  
Vi använde `enum` för att representera riktningar, konverterade en sträng från användaren till ett enum-värde och kastade enum till `int`.

---

## Vad är ett enum?

En enum är en lista med namngivna konstanter. Istället för att använda strängar som `"Norr"` eller siffror som `2` ger vi möjliga värden tydliga namn:

```csharp
enum Riktning
{
    Upp,
    Ner,
    Norr,
    Söder,
    Öst,
    Väst,
}
```

Nu kan vi skriva `Riktning.Norr` i koden. Kompilatorn vet vilka värden som är tillåtna — stavfel ger ett fel, inte ett felaktigt beteende.

---

## Enum.Parse — från sträng till enum

Användaren skriver in en text. Vi vill omvandla den till ett `Riktning`-värde:

```csharp
string input = Console.ReadLine();
Riktning val = Enum.Parse<Riktning>(input, true);
```

`true` som andra argument gör parsningen skiftlägesokänslig — "norr", "NORR" och "Norr" tolkas alla som `Riktning.Norr`.

Om användaren skriver något som inte finns i enumen kastas ett `ArgumentException`. I produktionskod skyddar man sig med `Enum.TryParse` istället.

---

## Jämföra enum-värden

Med ett enum-värde i handen kan vi jämföra direkt:

```csharp
if (val == Riktning.Söder)
    Console.WriteLine("Du kommer till labyrintens mitt");
else if (val == Riktning.Norr)
    Console.WriteLine("Du vandrar norrut och hittar fler gångar");
else if (val == Riktning.Väst)
    Console.WriteLine("Du vandrar västerut och hittar en fontän");
else if (val == Riktning.Öst)
    Console.WriteLine("Du vandrar österut och hittar en grotta");
```

Hade vi använt strängar direkt hade ett stavfel som `"Norrr"` glidit igenom tyst. Med enum fångar kompilatorn det.

---

## Enum och heltal

Varje enum-värde har ett underliggande heltal. Som standard börjar räkningen på 0:

```csharp
Console.WriteLine(Riktning.Norr + " är " + (int)Riktning.Norr);
Console.WriteLine(Riktning.Söder + " är " + (int)Riktning.Söder);
Console.WriteLine(Riktning.Öst + " är " + (int)Riktning.Öst);
Console.WriteLine(Riktning.Väst + " är " + (int)Riktning.Väst);
```

Utskrift:

```
Norr är 2
Söder är 3
Öst är 4
Väst är 5
```

`Norr` är inte 0 — det beror på att `Upp` och `Ner` definierades först. Ordningen i enum-deklarationen avgör heltalen.

Man kan sätta värden manuellt om det spelar roll:

```csharp
enum Riktning
{
    Norr = 0,
    Söder = 1,
    Öst = 2,
    Väst = 3,
}
```

---

## Enum.TryParse — det säkra alternativet

`Enum.Parse` kraschar om användaren skriver något oväntat. I en riktig app vill vi hantera det snyggt:

```csharp
if (Enum.TryParse<Riktning>(input, true, out Riktning val))
{
    // Parsningen lyckades — val är ett giltigt Riktning-värde
    Console.WriteLine("Du valde att gå " + val);
}
else
{
    // Strängen matchade inget enum-värde
    Console.WriteLine("Okänd riktning: " + input);
}
```

`TryParse` returnerar `true` eller `false` och lägger resultatet i `out`-variabeln `val`. Ingen krasch, inget undantag — programmet bestämmer själv vad som ska hända.

**Tumregel:** Använd `Parse` när du vet att indata är korrekt (t.ex. i en switch-meny du kontrollerar). Använd `TryParse` när användaren skriver fritt.

---

## Den kommenterade raden

I demo-koden finns en rad som är kommenterad bort:

```csharp
// input = input.ToUpper()[0] + input.ToLower().Substring(1);
```

Det här är ett manuellt sätt att göra om `"norr"` till `"Norr"` — stor första bokstav, resten små. Det behövs **inte** när vi använder `Enum.Parse` med `true` som argument, eftersom den redan hanterar skiftläge. Raden visades för att illustrera string-manipulation, men används inte i den färdiga koden.

---

## Hela koden från lektionen

```csharp
Console.WriteLine("Hello, Player!");
Console.WriteLine("Du befinner dig i en labyrint");
Console.WriteLine("Du kan gå Nord, Söder, Öst, Väst");
Console.WriteLine("Vart vill du gå");

string input = Console.ReadLine();
Console.WriteLine("Du valde att gå " + input);

Riktning val = Enum.Parse<Riktning>(input, true);

if (val == Riktning.Söder)
    Console.WriteLine("Du kommer till labyrintens mitt");
else if (val == Riktning.Norr)
    Console.WriteLine("Du vandrar norrut och hittar fler gångar");
else if (val == Riktning.Väst)
    Console.WriteLine("Du vandrar västerut och hittar en fontän");
else if (val == Riktning.Öst)
    Console.WriteLine("Du vandrar österut och hittar en grotta");

Console.WriteLine(Riktning.Norr + " är " + (int)Riktning.Norr);
Console.WriteLine(Riktning.Söder + " är " + (int)Riktning.Söder);
Console.WriteLine(Riktning.Öst + " är " + (int)Riktning.Öst);
Console.WriteLine(Riktning.Väst + " är " + (int)Riktning.Väst);

enum Riktning
{
    Upp,
    Ner,
    Norr,
    Söder,
    Öst,
    Väst,
}
```
