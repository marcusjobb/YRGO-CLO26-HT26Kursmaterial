---

title: Övning 1: SRP - Single Responsibility Principle
author: Marcus Ackre Medina
type: exercise
topic: clean-code
difficulty: 1
language: csharp
status: adapted
marcus_voice: true
source: "Old_courses/2025/3_advanced_oop/exercises/solid/solid_exercise_1.md"
description: "Du har fått en legacy 'God Object' klass som gör ALLT. Din uppgift är att refactora enligt SRP."
tags: ["clean-code", "csharp", "exercise", "principle", "responsibility", "single", "solid", "visual-studio", "övning"]
week_fit: []
---

# Övning 1: SRP - Single Responsibility Principle

🟢


## Scenario
Du har fått en legacy "God Object" klass som gör ALLT. Din uppgift är att refactora enligt SRP.

## Uppgift

### Del 1: Identifiera Problemet

Här är den dåliga koden:

```csharp

**15-minutersregeln:** Fastnar du i mer än 15 minuter — fråga klassen, sen AI, sen mig. I den ordningen.
public class UserManager
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public void SaveToDatabase()
    {
        // SQL kod för att spara user
        Console.WriteLine($"Saving {Name} to database...");
    }

    public void SendWelcomeEmail()
    {
        // SMTP kod för att skicka email
        Console.WriteLine($"Sending welcome email to {Email}...");
    }

    public bool ValidateEmail()
    {
        return Email.Contains("@") && Email.Contains(".");
    }

    public bool ValidatePassword()
    {
        return Password.Length >= 8;
    }

    public void LogActivity(string action)
    {
        Console.WriteLine($"[{DateTime.Now}] {Name}: {action}");
    }

    public string GenerateReport()
    {
        return $"User Report: {Name} ({Email})";
    }
}
```

**Fråga:** Hur många ansvarsområden har denna klass?

### Del 2: Refactora till SRP

Bryt ut klassen i separata ansvarsområden:

1. **User** - Bara data (properties)
2. **UserRepository** - Database operations
3. **EmailService** - Email sending
4. **UserValidator** - Validation logic
5. **ActivityLogger** - Logging
6. **ReportGenerator** - Report generation

### Del 3: Implementera

Skapa alla klasser och testa med följande scenario:

```csharp
var user = new User
{
    Name = "Luke Skywalker",
    Email = "luke@rebellion.com",
    Password = "usetheforce123"
};

var validator = new UserValidator();
var repo = new UserRepository();
var emailService = new EmailService();
var logger = new ActivityLogger();

if (validator.IsValid(user))
{
    repo.Save(user);
    emailService.SendWelcome(user);
    logger.Log(user.Name, "User created");
}
```

## Fördelar med SRP

Efter refactoring, lista:
- Vilka klasser kan du återanvända för andra entiteter (t.ex. Product)?
- Vilka klasser är lättare att testa?
- Vad händer om du vill byta från SQL till MongoDB?

<details>
<summary>Lösning</summary>

```csharp
// 1. Pure data model
public class User
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
}

// 2. Database responsibility
public class UserRepository
{
    public void Save(User user)
    {
        Console.WriteLine($"Saving {user.Name} to database...");
        // SQL logic here
    }

    public User? GetByEmail(string email)
    {
        Console.WriteLine($"Fetching user with email {email}...");
        return null; // Dummy
    }
}

// 3. Email responsibility
public class EmailService
{
    public void SendWelcome(User user)
    {
        Console.WriteLine($"Sending welcome email to {user.Email}...");
        // SMTP logic here
    }

    public void SendPasswordReset(User user)
    {
        Console.WriteLine($"Sending password reset to {user.Email}...");
    }
}

// 4. Validation responsibility
public class UserValidator
{
    public bool IsValid(User user)
    {
        return ValidateEmail(user.Email) && ValidatePassword(user.Password);
    }

    private bool ValidateEmail(string email)
    {
        return email.Contains("@") && email.Contains(".");
    }

    private bool ValidatePassword(string password)
    {
        return password.Length >= 8;
    }
}

// 5. Logging responsibility
public class ActivityLogger
{
    public void Log(string userName, string action)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss}] {userName}: {action}");
    }
}

// 6. Report responsibility
public class ReportGenerator
{
    public string GenerateUserReport(User user)
    {
        return $"User Report:\nName: {user.Name}\nEmail: {user.Email}";
    }
}

// Användning
var user = new User
{
    Name = "Luke Skywalker",
    Email = "luke@rebellion.com",
    Password = "usetheforce123"
};

var validator = new UserValidator();
var repo = new UserRepository();
var emailService = new EmailService();
var logger = new ActivityLogger();

if (validator.IsValid(user))
{
    repo.Save(user);
    emailService.SendWelcome(user);
    logger.Log(user.Name, "User created");
}
else
{
    Console.WriteLine("Invalid user data!");
}

// Fördelar:
// - EmailService kan användas för Product, Order, etc.
// - UserValidator lätt att testa (inga dependencies)
// - Byta repo? Skapa MongoUserRepository!
// - Varje klass har EN anledning att ändras
```

</details>

## Diskussion

**Parövning:**
- Diskutera: Kan man ta SRP för långt? När blir det över-engineering?
- Hitta exempel i era tidigare projekt där SRP saknas
- Rita UML Class Diagram av refactored solution

---
Och kom ihåg: allt vi gått igenom här är grunden. Resten bygger på det. Så var inte rädd att experimentera.
