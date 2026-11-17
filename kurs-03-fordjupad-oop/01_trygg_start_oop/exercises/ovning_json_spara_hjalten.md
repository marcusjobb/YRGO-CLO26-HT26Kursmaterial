# Övning: Spara hjälten som JSON

**Tid:** ca 45 minuter · **Gör den:** i par (en skriver, en läser högt, byt efter varje uppgift)

## Berättelsen

Mira har gått upp två nivåer under äventyret. Så stänger du programmet, och när du startar det igen är hon tillbaka på nivå 1. Allt hon har lärt sig är borta.

Idag ger vi Mira en sparfil.

## Innan du börjar

1. Skapa ett nytt konsolprojekt: `dotnet new console -n SparaHjalten`
2. Klassen `Hero` ska ha **properties** med `{ get; set; }`. Måndagens `Hero` hade fält, och `System.Text.Json` hoppar över fält. Då blir filen tom (`{}`).
3. Lägg klassen **under** koden i `Program.cs`. Står den ovanför får du felet CS8803.

```csharp
class Hero
{
    public string Name { get; set; } = "";
    public int Health { get; set; }
    public int Level { get; set; }
}
```

Överst i `Program.cs` behöver du `using System.Text.Json;`.

---

## Uppgift 1 🟢 Spara hjälten

1. Skapa en hjälte: Mira, 100 i hälsa, nivå 1.
2. Gör om objektet till JSON-text med `JsonSerializer.Serialize(...)`.
3. Skriv texten till filen `hjalte.json` med `File.WriteAllText(...)`.
4. Skriv ut `Sparade hjälten i hjalte.json` och sedan JSON-texten.

**Förväntad output:**
```
Sparade hjälten i hjalte.json
{
  "Name": "Mira",
  "Health": 100,
  "Level": 1
}
```

Ser du allt på en enda rad? Skicka med `new JsonSerializerOptions { WriteIndented = true }` som andra argument.

<details markdown="1">
<summary>Ledtråd</summary>

Du skapar en hjälte som vanligt, fast med måsvingar i stället för konstruktor:
`new Hero { Name = "Mira", Health = 100, Level = 1 }`.

Texten får du med `string json = JsonSerializer.Serialize(mira, options);`.

</details>

Filen ligger i projektmappen när du kör med `dotnet run`. Öppna den i VS Code och titta.

## Uppgift 2 🟢 Ladda hjälten

1. Läs filen med `File.ReadAllText("hjalte.json")`.
2. Gör om texten till ett objekt med `JsonSerializer.Deserialize<Hero>(...)`.
3. Skriv ut hjälten.
4. **Wow-testet:** ta bort raden som skapar Mira och kör programmet igen. Mira finns kvar, trots att koden inte längre skapar henne.

**Förväntad output:**
```
Laddade Mira: 100 hälsa, nivå 1.
```

<details markdown="1">
<summary>Ledtråd</summary>

`Deserialize` kan i teorin ge `null`. Lägg ett utropstecken sist om du vet att filen är giltig:
`Hero hero = JsonSerializer.Deserialize<Hero>(json)!;`

</details>

## Uppgift 3 🟡 Flera hjältar i en lista

1. Skapa en `List<Hero>` med tre hjältar: Mira (100, nivå 3), Tobbe (80, nivå 2) och Zelda (70, nivå 5).
2. Spara hela listan i `hjaltar.json`.
3. Ladda den igen med `Deserialize<List<Hero>>(...)`.
4. Skriv ut hur många som laddades och ett namn per rad.

**Förväntad output:**
```
Sparade 3 hjältar i hjaltar.json
Laddade 3 hjältar:
- Mira, nivå 3
- Tobbe, nivå 2
- Zelda, nivå 5
```

Öppna `hjaltar.json`. Vad är det som omger hjältarna?

<details markdown="1">
<summary>Ledtråd</summary>

Du serialiserar listan precis som du serialiserade en enda hjälte. Skriv `JsonSerializer.Serialize(party, options)`, där `party` är listan.

</details>

---

## Prata ihop innan ni är klara

- Vad hände med filen när ni körde uppgift 1 en andra gång?
- Vad står det i `hjaltar.json` runt hjältarna? Vad betyder tecknen?

Läxan till torsdag fortsätter där ni slutar.
