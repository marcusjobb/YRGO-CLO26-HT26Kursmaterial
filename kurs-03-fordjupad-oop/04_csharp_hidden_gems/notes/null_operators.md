# Null-operatorerna

Ett av de vanligaste felen i C# är `NullReferenceException`. Det händer när du använder ett objekt som är `null`. Null-operatorerna gör det enklare att skriva kod som tål `null`, utan att du behöver kontrollera allt manuellt.

## Tre operatorer

```csharp
string? name = null;

int length = name?.Length ?? 0;   // 0, eftersom name är null
name ??= "Gäst";                  // Sätter "Gäst", eftersom name är null
```

- **`?.`** går vidare bara om objektet inte är `null`. Om det är `null` blir hela uttrycket `null`.
- **`??`** ger ett reservvärde om värdet är `null`.
- **`??=`** tilldelar bara om variabeln är `null`.

## Samma sak, längre väg

```csharp
int length = 0;
if (name != null)
    length = name.Length;
```

Det ger samma resultat, men med fler rader. Varje extra kontroll är en chans att glömma något.

## Kedjor

```csharp
string city = customer?.Address?.City ?? "okänd";
```

Om någon länk i kedjan är `null` får du reservvärdet. Det är särskilt användbart när data kommer från en databas eller ett API, där delar kan saknas.

## Gränser

Null-operatorerna löser inte alla problem. Om ett värde *alltid* ska finnas, ska du inte dölja att det saknas. Då är ett undantag ofta bättre, eftersom det visar var felet uppstod.

## Sammanfattning

- `?.` hoppar över `null` i stället för att krascha.
- `??` ger ett reservvärde.
- `??=` fyller i bara om det är tomt.

## Prova själv

Gör `null_operators_ovning.md`, `null_operators_ovning_2.md` och `null_operators_ovning_3.md`.
