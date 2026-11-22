# Pattern matching

Ofta kontrollerar vi två saker på samma gång: vilken typ ett värde har, och vad värdet är. Med `if` och `else` blir det snabbt långt. Pattern matching låter dig skriva båda kontrollerna i samma uttryck.

## Typkontroll och värde i ett steg

```csharp
if (obj is int number && number > 10)
    Console.WriteLine($"Stort tal: {number}");
```

Läs det så här: om `obj` är ett heltal, lägg det i variabeln `number`, och om talet är större än tio, skriv ut det. Variabeln `number` finns bara inne i `if`-blocket, och det är avsiktligt.

## switch-uttryck

```csharp
string Category(int age) => age switch
{
    < 13 => "barn",
    < 20 => "tonåring",
    _ => "vuxen"
};
```

Ett `switch`-uttryck returnerar ett värde, och varje fall skrivs med `=>`. Mönstren läses uppifrån och ned, och den första träffen vinner. `_` betyder "allt annat".

## Ordningen spelar roll

Om du skriver `< 20` före `< 13` får alla barn fel svar, eftersom de också är mindre än tjugo. Skriv de snävaste fallen först, och det breda sist.

## Varför `_` är viktigt

Utan `_` varnar kompilatorn om att fallen inte täcker alla värden. Det är en hjälp: den ser när du har glömt ett fall. Låt inte varningen bli tyst med en tom standard. Fundera på vad det sista fallet ska betyda.

## Sammanfattning

- `is` kontrollerar typ och hämtar värdet i samma steg.
- `switch`-uttryck gör en beslutstabell på några rader.
- `when` lägger till ett villkor på ett fall.

## Prova själv

Gör `pattern_matching_ovning.md`, `pattern_matching_ovning_2.md` och `pattern_matching_ovning_3.md`.
