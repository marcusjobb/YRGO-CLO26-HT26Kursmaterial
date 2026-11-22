# Dynamic

Det mesta i C# kontrolleras redan när du skriver koden. Kompilatorn vet vilken typ varje variabel har, och den säger till om du gör något som inte passar. `dynamic` är ett undantag. Där kontrolleras typen först när programmet körs.

Det låter som ett sätt att slippa krav, men det är ett sätt att flytta felen från tiden du skriver koden till tiden programmet körs. Därför ska du använda det sparsamt.

## Så fungerar det

```csharp
dynamic value = 5;
value = "hej";           // Kompilerar. Typen ändras.
Console.WriteLine(value.Length);
```

Kompilatorn tittar inte på `value` och accepterar allt. Först när raden körs får programmet veta att `value` är en sträng och att den har en `Length`.

## Det som gör ont

```csharp
dynamic d = 3;
d.Foo();                 // Kompilerar. Kraschar när raden körs.
```

Här finns inga röda streck och inget IntelliSense. Felet syns först när programmet körs, och då är det ofta för sent. Det är därför du ska föredra `var` eller en riktig typ när du kan.

## När det är rätt verktyg

- **JSON med okänd struktur**, där du inte vet vilka fält som kommer.
- **COM-objekt och äldre API:er** som inte har en tydlig typ.
- **Reflektion**, när du arbetar med objekt utan att veta deras typ i förväg.

I alla tre fallen finns det en orsak till att typen är okänd. Om orsaken inte finns, använd en riktig typ.

## Sammanfattning

`dynamic` kontrolleras först i körningen. Det ger inga kompileringsfel, men det kan ge körningsfel. Använd det bara när typen verkligen inte går att veta i förväg.

## Prova själv

Gör de frivilliga övningarna `dynamic_ovning.md`, `dynamic_ovning_2.md` och `dynamic_ovning_3.md`.
