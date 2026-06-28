---

title: Övning - Enkel GET-förfrågan
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/books/csharp_cmyh/C-Sharp/api/exercises/api-basic.md"
description: "- Att använda `HttpClient` för att göra ett enkelt GET-anrop."
tags: ["api", "basic", "conditions", "csharp", "enkel", "exercise", "get-förfrågan", "git", "visual-studio", "övning"]
week_fit: []
---

# Övning - Enkel GET-förfrågan

🟢


## 🎯 Vad du kommer lära dig

- Att använda `HttpClient` för att göra ett enkelt GET-anrop.
- Att skapa en C-Sharp-klass som matchar en JSON-struktur.
- Att deserialisera ett JSON-svar till ett C-Sharp-objekt.
- Att presentera data som hämtats från ett externt API.

## 🧩 Uppgift

Din uppgift är att skriva en konsolapplikation som hämtar en specifik "to-do" från test-API:et
JSONPlaceholder och skriver ut dess titel och om den är avklarad eller inte.

Specifikt ska du hämta "to-do" med **ID 4**.

Adressen (endpoint) för detta är: `https://jsonplaceholder.typicode.com/todos/4`

Exempel på hur JSON-svaret ser ut:

```json

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.

{
  "userId": 1,
  "id": 4,
  "title": "et porro tempora",
  "completed": true
}

```

## 🚀 Steg-för-steg-guide

1.  **Skapa projektet**: Skapa ett nytt C\# konsolapplikations-projekt.
2.  **Skapa en DTO-klass**: Skapa en klass, döp den till `Todo`. Den ska ha egenskaperna `Title`
(string) och `Completed` (bool). Du kan även ta med `Id` och `UserId` om du vill.
3.  **Hämta datan**: I din `Main`-metod, skapa en `HttpClient`. Använd `GetStringAsync` för att anropa URL:en ovan och spara JSON-svaret i en sträng.
4.  **Omvandla datan**: Använd `JsonSerializer.Deserialize` för att omvandla JSON-strängen till ett
`Todo`-objekt.
5.  **Presentera resultatet**: Skriv ut en snygg text till konsolen, till exempel:
    `Uppgift: et porro tempora`
    `Avklarad: Ja`

## 💡 Tips

- Kom ihåg att din `Main`-metod måste vara `async Task` för att du ska kunna använda `await`.
- Om du vill visa "Ja" eller "Nej" istället för `true` eller `false`, kan du använda en enkel if-sats eller en ternary operator: `todo.Completed ? "Ja" : "Nej"`.

<details markdown="1">
<summary>💡 Klicka här för facit</summary>

```csharp

using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

// DTO-klass som matchar JSON-datan
public class Todo
{
    public string Title { get; set; }
    public bool Completed { get; set; }
}

public class Program
{
    public static async Task Main(string[] args)
    {
        using var client = new HttpClient(); // Skapa en HttpClient för att göra anrop
        string url = "https://jsonplaceholder.typicode.com/todos/4"; // URL för att hämta todo med ID 4

        try
        {
            string jsonResponse = await client.GetStringAsync(url); // Hämta JSON-svaret
            Todo? todo = JsonSerializer.Deserialize<Todo>(jsonResponse); // Deserialisera JSON till ett Todo-objekt

            if (todo is not null) // Kontrollera att vi fått svar (alltså inte null)
            {
                string status = todo.Completed ? "Ja" : "Nej";
                Console.WriteLine($"Uppgift: {todo.Title}");
                Console.WriteLine($"Avklarad: {status}");
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Ett fel inträffade: {e.Message}");
        }
    }
}

```

</details>

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
