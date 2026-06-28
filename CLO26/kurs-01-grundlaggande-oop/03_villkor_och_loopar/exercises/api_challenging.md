---

title: Övning - Skapa data med POST
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/books/csharp_cmyh/C-Sharp/api/exercises/api-challenging.md"
description: "- Att skapa ett C-Sharp-objekt för att skicka som data."
tags: ["api", "challenging", "conditions", "csharp", "data", "exercise", "git", "post", "skapa", "visual-studio"]
week_fit: []
---

# Övning - Skapa data med POST

🟢


## 🎯 Vad du kommer lära dig

- Att skapa ett C-Sharp-objekt för att skicka som data.
- Att serialisera ett C-Sharp-objekt till en JSON-sträng.
- Att använda `PostAsync` för att skicka data till ett API.
- Att läsa och hantera svaret från ett POST-anrop.

## 🧩 Uppgift

Din uppgift är att skapa ett nytt "album" på JSONPlaceholder. Du ska skicka en titel och ett
`userId` till API:et. När servern har skapat albumet kommer den att svara med det objekt du
skickade, plus ett nytt `id` som den har genererat.

Du ska sedan läsa detta svar och skriva ut det nya ID:t.

Adressen (endpoint) för att skapa album är: `https://jsonplaceholder.typicode.com/albums`

JSON-datan du skickar ska se ut ungefär så här:

```json

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

{
  "userId": 15,
  "title": "Min nya skiva"
}

```

## 🚀 Steg-för-steg-guide

1.  **Skapa projektet**: Skapa en ny C\# konsolapplikation.
2.  **Skapa en DTO-klass**: Skapa en klass `Album`. Den ska ha egenskaperna `UserId` (int), `Title`
(string) och `Id` (int?). `Id` ska vara en nullable int (`int?`) eftersom vi inte har något ID när
vi skickar iväg datan, men vi förväntar oss ett i svaret.
3.  **Skapa objektet**: I `Main`, skapa en ny instans av din `Album`-klass. Sätt `UserId` till
valfritt nummer och `Title` till en valfri text.
4.  **Serialisera & Paketera**: Använd `JsonSerializer.Serialize` för att göra om ditt objekt till
en JSON-sträng. Skapa sedan ett `StringContent`-objekt från strängen.
5.  **Skicka datan**: Använd `client.PostAsync` för att skicka ditt `StringContent` till URL:en.
6.  **Hantera svaret**: Kontrollera att `response.IsSuccessStatusCode` är `true`. Läs sedan
innehållet från svaret med `response.Content.ReadAsStringAsync()`.
7.  **Presentera resultatet**: Deserialisera svars-strängen tillbaka till ett `Album`-objekt och
skriv ut det nya `Id` som servern genererade.

## 💡 Tips

- Glöm inte att ange `Encoding.UTF8` och `"application/json"` när du skapar ditt `StringContent`-objekt. Servern behöver veta vad du skickar för något\!

<details markdown="1">
<summary>💡 Klicka här för facit</summary>

```csharp

using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

public class Album
{
    public int UserId { get; set; }
    public string Title { get; set; }
    public int? Id { get; set; }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        using var client = new HttpClient();

        var newAlbum = new Album
        {
            UserId = 22,
            Title = "Resan till C-Sharp-Land"
        };

        string jsonData = JsonSerializer.Serialize(newAlbum);
        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

        string url = "https://jsonplaceholder.typicode.com/albums";

        try
        {
            HttpResponseMessage response = await client.PostAsync(url, content);

            if (response.IsSuccessStatusCode)
            {
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine("Album skapat! Svar från servern:");
                Console.WriteLine(responseBody);

                Album? createdAlbum = JsonSerializer.Deserialize<Album>(responseBody);
                if (createdAlbum?.Id is not null)
                {
                    Console.WriteLine($"\nAlbumet fick ID: {createdAlbum.Id}");
                }
            }
            else
            {
                Console.WriteLine($"Misslyckades! Statuskod: {response.StatusCode}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Nätverksfel: {e.Message}");
        }
    }
}

```

</details>

---
Nu har du verktygen. Använd dem, missbruka dem, lär dig av misstagen. Det är vägen.
