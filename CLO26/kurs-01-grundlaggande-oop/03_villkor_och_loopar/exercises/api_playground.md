---

title: 🌐 API Playground - Bygg Webservices! 🌐
author: Marcus Ackre Medina
type: exercise
topic: conditions
difficulty: 2
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/books/csharp_cmyh/C-Sharp/api/exercises/api-playground.md"
description: "Marcus demo:ar API:er i 10 min → NI BYGGER WEBSERVICES!** 🚀"
tags: ["api", "bygg", "conditions", "csharp", "exercise", "git", "installation", "playground", "ssh", "visual-studio"]
week_fit: []
---

# 🌐 API Playground - Bygg Webservices! 🌐

🟡


**Marcus demo:ar API:er i 10 min → NI BYGGER WEBSERVICES!** 🚀

15+ API-projekt från enkla endpoints till kompleta system! Koda loss! 🔥

## 📊 DATA MANAGEMENT APIS

### 4. Todo List API
**15 min:** Classic CRUD operations

```csharp

[ApiController]
[Route("api/todos")]
public class TodosController : ControllerBase
{
    private static readonly List<TodoItem> Todos = new();
    private static int nextId = 1;

    [HttpGet]
    public ActionResult<List<TodoItem>> GetTodos([FromQuery] bool? completed = null)
    {
        var filteredTodos = completed.HasValue
            ? Todos.Where(t => t.IsCompleted == completed.Value).ToList()
            : Todos;

        return Ok(filteredTodos);
    }

    [HttpGet("{id}")]
    public ActionResult<TodoItem> GetTodo(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        return todo == null ? NotFound() : Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoItem> CreateTodo(CreateTodoRequest request)
    {
        var todo = new TodoItem
        {
            Id = nextId++,
            Title = request.Title,
            Description = request.Description,
            IsCompleted = false,
            CreatedAt = DateTime.Now,
            Priority = request.Priority
        };

        Todos.Add(todo);
        return CreatedAtAction(nameof(GetTodo), new { id = todo.Id }, todo);
    }

    [HttpPut("{id}")]
    public ActionResult<TodoItem> UpdateTodo(int id, UpdateTodoRequest request)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        todo.Title = request.Title ?? todo.Title;
        todo.Description = request.Description ?? todo.Description;
        todo.IsCompleted = request.IsCompleted ?? todo.IsCompleted;
        todo.Priority = request.Priority ?? todo.Priority;
        todo.UpdatedAt = DateTime.Now;

        return Ok(todo);
    }

    [HttpDelete("{id}")]
    public ActionResult DeleteTodo(int id)
    {
        var todo = Todos.FirstOrDefault(t => t.Id == id);
        if (todo == null) return NotFound();

        Todos.Remove(todo);
        return NoContent();
    }

    [HttpGet("stats")]
    public ActionResult<object> GetStats()
    {
        return Ok(new
        {
            TotalTodos = Todos.Count,
            CompletedTodos = Todos.Count(t => t.IsCompleted),
            PendingTodos = Todos.Count(t => !t.IsCompleted),
            HighPriorityTodos = Todos.Count(t => t.Priority == "High"),
            CompletionRate = Todos.Count > 0 ? (double)Todos.Count(t => t.IsCompleted) / Todos.Count * 100 : 0
        });
    }
}

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public bool IsCompleted { get; set; }
    public string Priority { get; set; } = "Medium"; // Low, Medium, High
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}

```

### 5. Movie Collection API
**18 min:** Personal movie database

```csharp

[ApiController]
[Route("api/movies")]
public class MoviesController : ControllerBase
{
    private static readonly List<Movie> Movies = new();

    [HttpGet]
    public ActionResult<List<Movie>> GetMovies(
        [FromQuery] string? genre = null,
        [FromQuery] int? minYear = null,
        [FromQuery] double? minRating = null)
    {
        var filteredMovies = Movies.AsQueryable();

        if (!string.IsNullOrEmpty(genre))
            filteredMovies = filteredMovies.Where(m => m.Genre.Contains(genre, StringComparison.OrdinalIgnoreCase));

        if (minYear.HasValue)
            filteredMovies = filteredMovies.Where(m => m.ReleaseYear >= minYear.Value);

        if (minRating.HasValue)
            filteredMovies = filteredMovies.Where(m => m.Rating >= minRating.Value);

        return Ok(filteredMovies.ToList());
    }

    [HttpPost]
    public ActionResult<Movie> AddMovie(Movie movie)
    {
        movie.Id = Movies.Count + 1;
        movie.DateAdded = DateTime.Now;
        Movies.Add(movie);

        return CreatedAtAction(nameof(GetMovie), new { id = movie.Id }, movie);
    }

    [HttpGet("{id}")]
    public ActionResult<Movie> GetMovie(int id)
    {
        var movie = Movies.FirstOrDefault(m => m.Id == id);
        return movie == null ? NotFound() : Ok(movie);
    }

    [HttpGet("recommendations")]
    public ActionResult<List<Movie>> GetRecommendations()
    {
        // Simple recommendation: highest rated movies
        var recommendations = Movies
            .Where(m => m.Rating >= 4.0)
            .OrderByDescending(m => m.Rating)
            .Take(5)
            .ToList();

        return Ok(recommendations);
    }

    [HttpGet("stats")]
    public ActionResult<object> GetCollectionStats()
    {
        if (!Movies.Any())
            return Ok(new { Message = "No movies in collection" });

        return Ok(new
        {
            TotalMovies = Movies.Count,
            AverageRating = Movies.Average(m => m.Rating),
            FavoriteGenre = Movies.GroupBy(m => m.Genre)
                                 .OrderByDescending(g => g.Count())
                                 .First().Key,
            OldestMovie = Movies.Min(m => m.ReleaseYear),
            NewestMovie = Movies.Max(m => m.ReleaseYear)
        });
    }
}

public class Movie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string Genre { get; set; }
    public int ReleaseYear { get; set; }
    public double Rating { get; set; } // 1-5 stars
    public bool HasWatched { get; set; }
    public DateTime DateAdded { get; set; }
    public string Review { get; set; }
}

```

### 6. Contact Book API
**12 min:** Manage your contacts

```csharp

[ApiController]
[Route("api/contacts")]
public class ContactsController : ControllerBase
{
    private static readonly List<Contact> Contacts = new();

    [HttpGet]
    public ActionResult<List<Contact>> GetContacts([FromQuery] string? search = null)
    {
        if (string.IsNullOrEmpty(search))
            return Ok(Contacts);

        var filteredContacts = Contacts.Where(c =>
            c.FirstName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            c.LastName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
            c.Email.Contains(search, StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(filteredContacts);
    }

    [HttpPost]
    public ActionResult<Contact> AddContact(Contact contact)
    {
        contact.Id = Contacts.Count + 1;
        contact.CreatedAt = DateTime.Now;
        Contacts.Add(contact);

        return CreatedAtAction(nameof(GetContact), new { id = contact.Id }, contact);
    }

    [HttpGet("birthdays")]
    public ActionResult<List<Contact>> GetUpcomingBirthdays()
    {
        var today = DateTime.Today;
        var upcomingBirthdays = Contacts
            .Where(c => c.DateOfBirth.HasValue)
            .Where(c =>
            {
                var nextBirthday = new DateTime(today.Year, c.DateOfBirth.Value.Month, c.DateOfBirth.Value.Day);
                if (nextBirthday < today)
                    nextBirthday = nextBirthday.AddYears(1);
                return (nextBirthday - today).Days <= 30;
            })
            .ToList();

        return Ok(upcomingBirthdays);
    }
}

public class Contact
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Company { get; set; }
    public DateTime? DateOfBirth { get; set; }
    public DateTime CreatedAt { get; set; }
}

```

## 🎮 FUN APIS

### 9. Dad Joke API
**6 min:** Because every API needs humor

```csharp

[ApiController]
[Route("api/jokes")]
public class JokesController : ControllerBase
{
    private static readonly List<Joke> Jokes = new()
    {
        new("Varför gillar programmerare nature?", "För att det har så många bugs!"),
        new("Vad sa utvecklaren till compilern?", "Du fattar inte alls vad jag menar!"),
        new("Varför var JavaScript-utvecklaren arg?", "Han hade förlorat this!"),
        new("Hur många programmerare behövs för att byta en glödlampa?", "Ingen, det är ett hardware-problem!"),
        new("Varför använder programmerare mörka teman?", "För att ljuset attraherar bugs!")
    };

    [HttpGet("random")]
    public ActionResult<Joke> GetRandomJoke()
    {
        var random = new Random();
        var joke = Jokes[random.Next(Jokes.Count)];

        return Ok(new
        {
            Setup = joke.Setup,
            Punchline = joke.Punchline,
            Rating = "Dad-level",
            GeneratedAt = DateTime.Now
        });
    }

    [HttpGet("all")]
    public ActionResult<List<Joke>> GetAllJokes()
    {
        return Ok(Jokes);
    }

    [HttpPost("rate")]
    public ActionResult RateJoke(JokeRating rating)
    {
        // In a real app, store ratings in database
        return Ok(new { Message = $"Tack för din rating: {rating.Rating}/5!" });
    }
}

public class Joke
{
    public string Setup { get; set; }
    public string Punchline { get; set; }

    public Joke(string setup, string punchline)
    {
        Setup = setup;
        Punchline = punchline;
    }
}

public class JokeRating
{
    public int JokeId { get; set; }
    public int Rating { get; set; } // 1-5
    public string Comment { get; set; }
}

```

### 10. Fortune Cookie API
**8 min:** Digital wisdom cookies

```csharp

[ApiController]
[Route("api/fortune")]
public class FortuneController : ControllerBase
{
    private static readonly List<Fortune> Fortunes = new()
    {
        new("Code flows like water - sometimes smooth, sometimes it hits a rock."),
        new("Debug today, deploy tomorrow, maintain forever."),
        new("The best code is written when you're not trying to be clever."),
        new("Your next breakthrough is just one compile away."),
        new("In programming, as in life, the semicolon matters;")
    };

    [HttpGet]
    public ActionResult<object> GetFortune()
    {
        var random = new Random();
        var fortune = Fortunes[random.Next(Fortunes.Count)];

        return Ok(new
        {
            Fortune = fortune.Text,
            LuckyNumbers = GenerateLuckyNumbers(),
            Category = fortune.Category,
            GeneratedAt = DateTime.Now,
            Message = "May your code compile on the first try! 🥠"
        });
    }

    private int[] GenerateLuckyNumbers()
    {
        var random = new Random();
        return Enumerable.Range(0, 6)
            .Select(_ => random.Next(1, 100))
            .ToArray();
    }
}

public class Fortune
{
    public string Text { get; set; }
    public string Category { get; set; } = "Programming";

    public Fortune(string text)
    {
        Text = text;
    }
}

```

## 🚀 **EXPANSION IDEAS:**

### 🔐 **Security Features:**
- API key authentication
- Rate limiting per user
- Input validation and sanitization
- HTTPS enforcement

### 📊 **Analytics & Monitoring:**
- Request logging
- Performance metrics
- Error tracking
- Usage statistics

### 🌐 **Integration Features:**
- External API consumption
- Webhooks
- Real-time updates with SignalR
- Background job processing

### 📚 **Documentation:**
- Swagger/OpenAPI integration
- API versioning
- Response examples
- Interactive testing

---

## 💡 **API DEVELOPMENT TIPS:**

1. **Start Simple** - en endpoint i taget
2. **Use DTOs** - separera API models från internal models
3. **Validate Input** - förhindra bad data tidigt
4. **Error Handling** - returnera meaningful error messages
5. **Status Codes** - använd rätt HTTP status codes
6. **Testing** - testa dina endpoints med Postman/curl
7. **Documentation** - beskriv vad varje endpoint gör

**BYGG APIS SOM EN ROCKSTAR! 🎸🌐**

*Pro tip: Börja med GET endpoints, lägg till POST/PUT/DELETE gradvis!*

---
Det här är grunden. Öva på den, lek med koden, gör misstag. Det är så du lär dig.
