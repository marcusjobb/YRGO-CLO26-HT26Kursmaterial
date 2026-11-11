# Spara ett objekt som JSON: termer

## JSON
Ett textformat för data. Ett objekt skrivs som `{ "nyckel": värde }`. Läsbart för både människor och program.

## Serialisering (Serialize)
Att göra om ett objekt till text, till exempel JSON. `JsonSerializer.Serialize(hero)` ger en sträng med heroens värden.

## Deserialisering (Deserialize)
Det omvända: att göra om text till ett objekt. `JsonSerializer.Deserialize<Hero>(text)` bygger ett nytt `Hero` av texten.

## System.Text.Json
.NET:s inbyggda bibliotek för JSON. Kräver `using System.Text.Json;`. Finns med i .NET, inget behöver installeras.

## Property
Ett fält med `get` och `set`: `public string Name { get; set; }`. `System.Text.Json` ser properties men ignorerar vanliga fält.

## Fält
En variabel direkt i klassen: `public string Name;`. Ger tom JSON (`{}`) vid serialisering.

## Nyckel och värde
En rad i JSON är `"Name": "Mira"`. `"Name"` är nyckeln, `"Mira"` är värdet.

## WriteIndented
En inställning (`JsonSerializerOptions`) som ger JSON med radbrytningar och indrag, så att den går att läsa.

## Fil
Data som sparas på disken och finns kvar när programmet stängts. `File.WriteAllText` skriver, `File.ReadAllText` läser.
