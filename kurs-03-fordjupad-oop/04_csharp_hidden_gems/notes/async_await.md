# async och await

När programmet väntar på något långsamt, till exempel en fil, en databas eller ett nätverksanrop, kan det antingen frysa eller göra något annat medan det väntar. `async` och `await` låter dig skriva det andra sättet utan att krångla till koden.

## Vänta utan att blockera

```csharp
async Task<string> FetchGreetingAsync()
{
    await Task.Delay(1000);       // Väntar en sekund utan att frysa tråden
    return "hej";
}

var greeting = await FetchGreetingAsync();
Console.WriteLine(greeting);
```

`async` märker metoden som asynkron. `await` säger: vänta här, men låt programmet göra andra saker under tiden. En metod som väntar returnerar en `Task<string>` i stället för en `string`, och det är därför `await` behövs när du anropar den.

## Vänta med await

```csharp
var greeting = await FetchGreetingAsync();
```

`await` väntar på resultatet utan att blockera tråden. Programmet kan göra andra saker under tiden, och när svaret finns fortsätter koden på nästa rad.

## Flera väntanden samtidigt

```csharp
await Task.WhenAll(
    Task.Delay(1000),
    Task.Delay(1000),
    Task.Delay(1000));
```

Tre väntetider på en sekund var som körs samtidigt tar en sekund sammanlagt, inte tre. `Task.WhenAll` väntar tills alla är klara.

## Fel i asynkron kod

Ett undantag som kastas i en `async`-metod kommer tillbaka när du `await`:ar den. Då kan du fånga det med `try` och `catch`, precis som vanligt.

## Sammanfattning

- `async` och `await` väntar utan att blockera.
- Använd `await` när du väntar på en `Task`.
- `Task.WhenAll` väntar på flera samtidigt.

## Prova själv

Gör `async_await_ovning.md`, `async_await_ovning_2.md` och `async_await_ovning_3.md`.
