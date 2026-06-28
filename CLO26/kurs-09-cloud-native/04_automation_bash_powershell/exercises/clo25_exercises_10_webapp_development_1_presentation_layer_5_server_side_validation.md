---

title: 5. Server-Side Validation and Improved Feedback
author: Marcus Ackre Medina
type: exercise
topic: infrastructure
difficulty: 2
language: english
status: adapted
marcus_voice: true
source: "Old_courses/Coud_Development_CLO25/exercises/10-webapp-development/1-presentation-layer/5-server-side-validation.md"
description: "Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-"
tags: ["cloud", "docker", "dotnet", "exercise", "feedback", "improved", "infrastructure", "networking", "security", "server"]
week_fit: []
---

Navigation :
Getting StartedWeek by WeekIntro To Cloud DevelopmentInfrastructure FundamentalsExercises-
Server Foundation-
Network Foundation-
Deployment-
Cloud Databases-
Webapp Development--
1. Presentation Layer--- 1. Hello World--- 2. Create A Form With Basic HTML--- 3. Helper Tags and Model Binding--- 4. Data Annotations and Client-Side Validation--- 5. Server-Side Validation and Improved Feedback--- 6. Displaying a List of Subscribers--- 7. Implement Unsubscribe Functionality--- 8. Enhancing UI with Bootstrap--- 9. Landing Page with Hero Section--
2. Service Layer--
3. Data Layer--
4. Authentication and Authorization--
5. Identity and User Stores-
Code Collaboration-
DockerTutorials

# 5. Server-Side Validation and Improved Feedback

🟡


# Server-Side Validation and Improved Feedback

## Goal

Enhance the form by introducing **server-side validation**, managing a **list of subscribers**, and improving feedback handling using **TempData** and **model-level errors**.

## Learning Objectives

By the end of this exercise, you will:

* Implement **server-side validation** for better security.
* Store **subscribers in a list** within the controller.
* Use **regular expressions** to validate email format.
* Prevent **duplicate email signups**.
* Replace **ViewBag with TempData** for better feedback persistence.
* Use **ModelState.AddModelError** to improve error handling.

## Step-by-Step Instructions

### Step 1: Update the Model with Server-Side Validation

1. Open `Subscriber.cs` in the `Models` folder.
2. Improve **client-side email validation** using **regular expressions**.

> `Models/Subscriber.cs`

```
using System.ComponentModel.DataAnnotations;

namespace CloudSoft.Models;

public class Subscriber
{
    [Required]
    [StringLength(20, ErrorMessage = "Name cannot exceed 20 characters")]
    public string? Name { get; set; }

    [Required]
    [EmailAddress]
    [RegularExpression("^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\\.[a-zA-Z]{2,}$", ErrorMessage = "Missing top level domain")]
    public string? Email { get; set; }
}
```

#### **Information**

* The **`RegularExpression` attribute** enforces stricter email format validation.
* This prevents malformed or **suspicious email input**.

### Step 2: Update the Controller to Store Subscribers and Prevent Duplicates

1. Open `NewsletterController.cs`.
2. Modify the `Subscribe` method to check for **duplicate emails** and store data in memory.

> `Controllers/NewsletterController.cs`

```
using System.Text.RegularExpressions;
using CloudSoft.Models;
using Microsoft.AspNetCore.Mvc;

namespace CloudSoft.Controllers;

public class NewsletterController : Controller
{
    // Create a "database" of subscribers for demonstration purposes
    private static List<Subscriber> _subscribers = [];

    [HttpGet]
    public IActionResult Subscribe()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Subscribe(Subscriber subscriber)
    {
        // Validate the model
        if (!ModelState.IsValid)
        {
            return View(subscriber);
        }

        // Check if the email is already subscribed and return a general model level error
        if (_subscribers.Any(s => s.Email == subscriber.Email))
        {
            ModelState.AddModelError("Email", "The email is already subscribed. Please use a different email.");
            return View(subscriber);
        }

        // Add the subscriber to the list
        _subscribers.Add(subscriber);

        // Write to the console
        Console.WriteLine($"New subscription - Name: {subscriber.Name} Email: {subscriber.Email}");

        // Send a message to the user
        TempData["SuccessMessage"] = $"Thank you for subscribing, {subscriber.Name}! You will receive our newsletter at {subscriber.Email}";

        // Return the view (using the POST-REDIRECT-GET pattern)
        return RedirectToAction(nameof(Subscribe));  // use nameof() to find the action by name during compile time
    }
}
```

> **Information**
>
> * **`_subscribers` list** stores all registered users **in memory**.
> * **Checks if the email already exists** before allowing submission.
> * Uses **ModelState.AddModelError** for **field-specific error messages**.
> * **TempData replaces ViewBag** for better feedback persistence across redirects.

### Step 3: Update the View to Display Errors and TempData Feedback

1. Open `Views/Newsletter/Subscribe.cshtml`.
2. Modify the form to display **server-side validation errors** and **TempData messages**.

> `Views/Newsletter/Subscribe.cshtml`

```
@model CloudSoft.Models.Subscriber

@{
    ViewData["Title"] = "Newsletter Signup";
}

<h2>Newsletter Signup</h2>

<!-- Display validation summary for model-level errors -->
@if (!ViewData.ModelState.IsValid)
{
    <div class="alert alert-danger alert-dismissible fade show" role="alert">
        @Html.ValidationSummary(false, null, new { @class = "text-danger" })
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><i class="fas fa-times"></i></button>
    </div>
}

<!-- Display message from the TempData sent by the controller -->
@if (TempData["SuccessMessage"] != null)
{
    <div class="alert alert-success alert-dismissible fade show" role="alert">
        @TempData["SuccessMessage"]
        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"><i class="fas fa-times"></i></button>
    </div>
}

<form asp-action="Subscribe" method="post">
    <div class="form-group">
        <label asp-for="Name"></label>
        <input asp-for="Name" class="form-control" />
        <span asp-validation-for="Name" class="text-danger"></span>
    </div>
    <div class="form-group">
        <label asp-for="Email"></label>
        <input asp-for="Email" class="form-control" />
        <span asp-validation-for="Email" class="text-danger"></span>
    </div>
    <div class="mt-3">
        <button type="submit" class="btn btn-primary">Sign Up</button>
    </div>
</form>

@section Scripts {
    <partial name="_ValidationScriptsPartial" />
}
```

#### **Information**

* **TempData message** is displayed when **redirecting after a successful submission**.
* `asp-validation-for` **displays field-specific validation errors**.
* `_ValidationScriptsPartial` **enables client-side validation**.

## Final Tests

### Run the Application and Validate Your Work

1. Start the application:

   ```
   dotnet run
   ```
2. Open a browser and navigate to:

   ```
   http://localhost:5000/Newsletter/Subscribe
   ```
3. Try submitting **invalid or empty input** and confirm error messages.
4. Try **registering with the same email twice** and confirm the duplicate warning.
5. Submit valid data and check if feedback persists correctly using **TempData**.

## Shout Out!

Awesome work! You’ve implemented **server-side validation**, handled **duplicate entries**, and improved **feedback management**.
