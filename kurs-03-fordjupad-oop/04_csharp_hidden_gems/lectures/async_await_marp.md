---
marp: true
theme: nion-dark
paginate: true
footer: "© 2025–2026 Nion Education. Får inte användas av andra skolor utan tillstånd."
---

# async och await

**Vecka 48 · Hellweek · Hidden gems**

---

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

---

## Vänta med await

```csharp
var greeting = await FetchGreetingAsync();
```

`await` väntar utan att blockera tråden.

---

## Wow: tre sekunder blir en

```csharp
await Task.WhenAll(
    Task.Delay(1000),
    Task.Delay(1000),
    Task.Delay(1000));
```

Tre väntetider som körs samtidigt tar en sekund, inte tre.

---

## Sammanfattning

- `async` och `await` väntar utan att blockera.
- Använd `await` när du väntar på en `Task`.
- `Task.WhenAll` väntar på flera samtidigt.
