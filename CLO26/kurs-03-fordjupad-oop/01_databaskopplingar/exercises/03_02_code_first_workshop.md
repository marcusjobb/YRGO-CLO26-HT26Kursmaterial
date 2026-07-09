# Workshop: Code First-magi med EF Core

🟢


## NuGet-paket att ha installerade

- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.Tools`
- `Microsoft.Extensions.Logging.Console`

> Bonus: Aktivera loggning i `Program.cs` för att se genererade SQL-frågor under arbetet.

## Scenario

Vi tar appen från vecka 1 och byter ut manuell SQL/ADO.NET mot Entity Framework Core. Målet är att använda modellerna och DbContext från Pre-Flight checklistan och bygga vidare med LINQ, relationer och migrationer.

## Del 1 – Rensa bort gammal dataåtkomst (60 min)

1. Ta bort (eller kommentera ut) klasser som hanterar `SqlConnection`, `SqlCommand` etc.
2. Lägg till `builder.Services.AddDbContext<SchoolContext>()` (om du använder minimal API/ASP.NET).
3. Injicera `SchoolContext` där du tidigare använde repository-klasser.
4. Kör `dotnet build` – det ska kompilera även om datan ännu inte hämtas.

## Del 2 – CRUD med LINQ (90 min)

Nu bygger vi en `StudentService` som ersätter all gammal SQL-kod med LINQ och EF Core.

### Steg 1: Skapa service-klassen

Börja med grundstrukturen:

```csharp
public class StudentService
{
    private readonly SchoolContext _context;

    public StudentService(SchoolContext context)
    {
        _context = context;
    }
}
```

### Steg 2: Hämta alla studenter med kurser

Här är nyckeln att använda `Include()` för att ladda relationer:

```csharp
public List<Student> GetAll()
{
    return _context.Students
        .Include(s => s.Courses)
        .ToList();
}
```

**Varför `Include()`?** Utan den får du bara studenter – navigation property `Courses` blir tom. EF Core laddar inte relationer automatiskt (kallas "lazy loading" och är avstängt by default).

### Steg 3: Hitta en specifik student

Två vanliga sätt:

```csharp
public Student? GetById(int id)
{
    return _context.Students
        .Include(s => s.Courses)
        .FirstOrDefault(s => s.Id == id);
}
```

**Varför `FirstOrDefault`?** Returnerar `null` om studenten inte finns – säkrare än att krascha.

**Alternativ:** `Find()` är snabbare om du bara vill ha Id utan filter:
```csharp
var student = _context.Students.Find(id);
```

Men `Find()` laddar inte relationer automatiskt, så du måste explicit ladda dem:
```csharp
_context.Entry(student).Collection(s => s.Courses).Load();
```

### Steg 4: Lägg till ny student

Skapa objekt, lägg till i DbSet, spara:

```csharp
public void Add(string name, string email)
{
    var student = new Student
    {
        Name = name,
        Email = email
    };

    _context.Students.Add(student);
    _context.SaveChanges();
}
```

**Viktigt:** Ingenting sparas förrän du anropar `SaveChanges()`! EF Core håller koll på ändringar i sin "change tracker" tills du säger åt den att skriva till databasen.

### Steg 5: Koppla student till kurs

Detta är many-to-many relation – så vi jobbar med navigation properties:

```csharp
public void AssignToCourse(int studentId, int courseId)
{
    var student = _context.Students
        .Include(s => s.Courses)
        .FirstOrDefault(s => s.Id == studentId);

    var course = _context.Courses.Find(courseId);

    if (student != null && course != null)
    {
        student.Courses.Add(course);
        _context.SaveChanges();
    }
}
```

**Varför `Include()` här?** Vi måste ladda `Courses`-listan först, annars vet inte EF Core om student redan är kopplad till kursen.

### Steg 6: Ta bort student

```csharp
public void Remove(int id)
{
    var student = _context.Students.Find(id);
    if (student != null)
    {
        _context.Students.Remove(student);
        _context.SaveChanges();
    }
}
```

**Vad händer med relationer?** Beror på din `OnDelete`-konfiguration i DbContext. Default är oftast `Cascade` för många-till-många, vilket betyder kopplingar raderas automatiskt.

### Testa din service

Ersätt gamla SQL-anrop med service-metoderna:

```csharp
var service = new StudentService(context);

// Lägg till student
service.Add("Ada Lovelace", "ada@school.com");

// Hämta alla
var students = service.GetAll();
foreach (var s in students)
{
    Console.WriteLine($"{s.Name} går {s.Courses.Count} kurser");
}

// Koppla till kurs
service.AssignToCourse(1, 1);

// Ta bort
service.Remove(99);
```

<details>
<summary>💡 Visa komplett StudentService</summary>

```csharp
using Microsoft.EntityFrameworkCore;

public class StudentService
{
    private readonly SchoolContext _context;

    public StudentService(SchoolContext context)
    {
        _context = context;
    }

    public List<Student> GetAll()
    {
        return _context.Students
            .Include(s => s.Courses)
            .ToList();
    }

    public Student? GetById(int id)
    {
        return _context.Students
            .Include(s => s.Courses)
            .FirstOrDefault(s => s.Id == id);
    }

    public void Add(string name, string email)
    {
        var student = new Student
        {
            Name = name,
            Email = email
        };

        _context.Students.Add(student);
        _context.SaveChanges();
    }

    public void AssignToCourse(int studentId, int courseId)
    {
        var student = _context.Students
            .Include(s => s.Courses)
            .FirstOrDefault(s => s.Id == studentId);

        var course = _context.Courses.Find(courseId);

        if (student != null && course != null)
        {
            student.Courses.Add(course);
            _context.SaveChanges();
        }
    }

    public void Remove(int id)
    {
        var student = _context.Students.Find(id);
        if (student != null)
        {
            _context.Students.Remove(student);
            _context.SaveChanges();
        }
    }
}
```

</details>

## Del 3 – Migrationer & Seeding (60 min)

Nu lägger vi till testdata direkt i databasen via seeding. Detta är perfekt för utveckling och demo.

### Varför seeding?

- Varje gång du nollställer databasen (`dotnet ef database update`) får du samma data
- Teamet jobbar med identiska dataset
- Demos och tester blir förutsägbara

### Steg 1: Lägg till seed-data i OnModelCreating

Öppna `SchoolContext.cs` och lägg till i `OnModelCreating`:

```csharp
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // Konfigurera många-till-många (om inte redan gjort)
    modelBuilder.Entity<Student>()
        .HasMany(s => s.Courses)
        .WithMany(c => c.Students)
        .UsingEntity(j => j.ToTable("StudentCourses"));

    // Seed students
    modelBuilder.Entity<Student>().HasData(
        new Student { Id = 1, Name = "Ada Lovelace", Email = "ada@school.com" },
        new Student { Id = 2, Name = "Grace Hopper", Email = "grace@school.com" },
        new Student { Id = 3, Name = "Margaret Hamilton", Email = "margaret@school.com" }
    );

    // Seed courses
    modelBuilder.Entity<Course>().HasData(
        new Course { Id = 1, Name = "Mathematics", Credits = 5 },
        new Course { Id = 2, Name = "Physics", Credits = 5 },
        new Course { Id = 3, Name = "Computer Science", Credits = 7 }
    );

    // Seed kopplingar (viktigt: använd exakt StudentsId och CoursesId!)
    modelBuilder.Entity("StudentCourses").HasData(
        new { StudentsId = 1, CoursesId = 1 },  // Ada - Math
        new { StudentsId = 1, CoursesId = 3 },  // Ada - CS
        new { StudentsId = 2, CoursesId = 1 },  // Grace - Math
        new { StudentsId = 2, CoursesId = 2 },  // Grace - Physics
        new { StudentsId = 3, CoursesId = 3 }   // Margaret - CS
    );
}
```

### Steg 2: Skapa migration och uppdatera

```bash
dotnet ef migrations add AddSeedData
dotnet ef database update
```

### Steg 3: Verifiera

Kör din app och lista studenter – ska se seed-data direkt!

```csharp
using var context = new SchoolContext();
var students = context.Students.Include(s => s.Courses).ToList();

foreach (var student in students)
{
    Console.WriteLine($"{student.Name} går {student.Courses.Count} kurser:");
    foreach (var course in student.Courses)
    {
        Console.WriteLine($"  - {course.Name} ({course.Credits}hp)");
    }
}
```

**Förväntad output:**
```
Ada Lovelace går 2 kurser:
  - Mathematics (5hp)
  - Computer Science (7hp)
Grace Hopper går 2 kurser:
  - Mathematics (5hp)
  - Physics (5hp)
Margaret Hamilton går 1 kurser:
  - Computer Science (7hp)
```

<details>
<summary>🔥 Vanliga fel vid seeding</summary>

**Problem 1: Glömde ange Id**
```csharp
// FEL - EF Core kan inte generera Id vid seeding
new Student { Name = "Ada", Email = "ada@school.com" }

// RÄTT - Måste ange Id manuellt
new Student { Id = 1, Name = "Ada", Email = "ada@school.com" }
```

**Problem 2: Fel kolumnnamn i kopplingstabellen**
```csharp
// FEL - EF Core förväntar StudentsId och CoursesId (plural!)
new { StudentId = 1, CourseId = 1 }

// RÄTT
new { StudentsId = 1, CoursesId = 1 }
```

**Problem 3: Id-konflikt**
Om du kör migrationer flera gånger kan samma Id försöka läggas till igen. Lösning:
```bash
dotnet ef database drop
dotnet ef database update
```

</details>

## Del 4 – Bonus: Validering (30 min)

Lägg till Data Annotations för att säkerställa datakvalitet:

```csharp
public class Student
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Namn krävs")]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(150)]
    public string Email { get; set; } = string.Empty;

    public ICollection<Course> Courses { get; set; } = new List<Course>();
}

public class Course
{
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;

    [Range(1, 10, ErrorMessage = "Poäng måste vara 1-10")]
    public int Credits { get; set; }

    public ICollection<Student> Students { get; set; } = new List<Student>();
}
```

Skapa migration:
```bash
dotnet ef migrations add AddValidation
dotnet ef database update
```

Testa validering:
```csharp
try
{
    var invalidStudent = new Student { Name = "", Email = "not-an-email" };
    context.Students.Add(invalidStudent);
    context.SaveChanges();  // Kommer kasta exception!
}
catch (DbUpdateException ex)
{
    Console.WriteLine($"Valideringsfel: {ex.Message}");
}
```

## Avstämning

- ✅ Alla tidigare SQL-kedjor är ersatta av DbContext + LINQ
- ✅ Migrationer används för att hantera schemaändringar
- ✅ Seed-data gör utveckling förutsägbar
- ✅ Validering säkerställer datakvalitet

Ditt team kan nu jobba vidare utan att röra SQL-strängar. I morgon kopplar vi upp oss mot MySQL!

🎉 Bra jobbat!

---
Sådärja. Nu har du koll på det här. Nästa steg — testa själv. Det är då det fastnar.
