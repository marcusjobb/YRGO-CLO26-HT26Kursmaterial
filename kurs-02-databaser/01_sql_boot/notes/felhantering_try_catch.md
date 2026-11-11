# Felhantering med try-catch

🟢

Databasanslutningar kan misslyckas. Nätverket krånglar, lösenordet är fel, servern är nere. Om du inte hanterar det kraschar programmet med ett rött felmeddelande.

`try-catch` låter dig fånga felet och göra något vettigt med det.

---

## Grundmönstret

```csharp
try
{
    // Kod som kan krascha
    using var connection = new MySqlConnection(connectionString);
    connection.Open();

    // ... databasoperationer ...
}
catch (MySqlException ex)
{
    // Databasspecifika fel — fel lösenord, server nere, etc.
    Console.WriteLine($"Databasfel: {ex.Message}");
}
catch (Exception ex)
{
    // Allt annat oväntat
    Console.WriteLine($"Fel: {ex.Message}");
}
```

`MySqlException` fångas **före** `Exception` — mer specifikt undantag går alltid först.

---

## Varför två catch-block?

`MySqlException` ger bättre felmeddelanden för databasspecifika problem:

```
Databasfel: Unable to connect to any of the specified MySQL hosts.
```

`Exception` fångar allt annat — till exempel om connection string-variabeln råkar vara `null`.

---

## Med felkoder

`MySqlException` har ett `Number`-fält med MySQL:s felkod:

```csharp
catch (MySqlException ex)
{
    switch (ex.Number)
    {
        case 1045:
            Console.WriteLine("Fel användarnamn eller lösenord.");
            break;
        case 1049:
            Console.WriteLine("Databasen finns inte.");
            break;
        default:
            Console.WriteLine($"Databasfel ({ex.Number}): {ex.Message}");
            break;
    }
}
```

Vanliga MySQL-felkoder:

| Kod | Betydelse |
|-----|-----------|
| 1045 | Access denied — fel lösenord |
| 1049 | Unknown database |
| 1146 | Table doesn't exist |
| 2002 | Can't connect to server |

---

## finally — körs alltid

```csharp
try
{
    using var connection = new MySqlConnection(connectionString);
    connection.Open();
    // ...
}
catch (MySqlException ex)
{
    Console.WriteLine($"Databasfel: {ex.Message}");
}
finally
{
    // Körs oavsett om det gick bra eller inte
    Console.WriteLine("Programmet avslutas.");
}
```

`using`-satsen stänger anslutningen automatiskt — du behöver sällan `finally` för databasobjekt, men det är bra att känna till mönstret.

---

## Tumregel

Allt som pratar med omvärlden — databaser, API:er, filer — ska vara insvept i `try-catch`. Din kod vet inte om nätverket fungerar, om servern är uppe, eller om filen existerar.

```csharp
// ❌ Kraschar om databasen är nere
connection.Open();

// ✅ Hanterar felet snyggt
try { connection.Open(); }
catch (MySqlException ex) { Console.WriteLine($"Kunde inte ansluta: {ex.Message}"); }
```

---

Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
