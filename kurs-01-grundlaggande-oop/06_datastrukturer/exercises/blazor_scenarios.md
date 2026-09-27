# Blazor — Scenarios

> Ingår inte i examinationen. Syftet är att utforska hur C# fungerar som webbapp.
> Skapa ett nytt **Blazor Web App (.NET 10)**-projekt i Visual Studio.
> Välj Interactive render mode: **Server**, Interactivity location: **Global**.

Varje scenario är en ny `.razor`-fil i mappen `Components/Pages/`.

---

## Scenario 1 🟢 — Favoritlista

Skapa sidan `/favoriter` med en lista du kan lägga till saker i.

### Krav

- En `<input>` och en **Lägg till**-knapp
- Det du skrivit läggs till i en lista och visas på sidan
- En **Rensa**-knapp tömmer hela listan
- Om listan är tom visas texten `"Listan är tom."`

### Kom ihåg

```razor
@page "/favoriter"
@rendermode InteractiveServer
```

### Tips

- `@bind="minVariabel"` kopplar en input till en C#-variabel
- `@onclick="MinMetod"` kopplar en knapp till en C#-metod
- `List<string> saker = new();` — initialisera alltid listan
- `saker.Clear();` — tömmer listan

### Förväntad känsla

Du skriver "Pizza", klickar Lägg till, "Pizza" dyker upp i listan direkt — utan att sidan laddas om.

---

## Scenario 2 🟡 — Röstningsapp

Skapa sidan `/rostning` där man kan rösta på ett av tre alternativ.

### Krav

- Tre alternativ (välj tema själv — djur, mat, teknik, vad du vill)
- Varje alternativ har en **Rösta**-knapp och visar röstantalet
- En **Återställ**-knapp nollställer alla röster
- Det alternativ med flest röster visas som vinnare (om minst en röst lagts)

### Tips

- Använd en `Dictionary<string, int>` för att lagra röster per alternativ
- `@foreach` fungerar på en Dictionary
- Hitta vinnaren med LINQ: `roster.MaxBy(kv => kv.Value).Key`
  (eller en vanlig loop om du inte vill använda LINQ)

### Förväntad output

```
🐶 Hund     [Rösta]   7 röster
🐱 Katt     [Rösta]   3 röster
🐾 Hamster  [Rösta]   1 röst

Vinnare: 🐶 Hund!
```
