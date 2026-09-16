# Exempel — Override av ToString()

Alla klasser i C# ärver från `object` — även om du inte skriver det.  
`object` har ett antal metoder, varav `ToString()` är `virtual` och kan skrivas om.

> Används bland annat för debug — `Console.WriteLine(obj)` anropar `ToString()` automatiskt.

---

## Program.cs

```csharp
Console.WriteLine("Hello, World!");

// Allt utom siffror är Object i C#
// Object-klassen har egna metoder — bland annat ToString()

Dummy d = new Dummy();
Console.WriteLine(d.ToString());  // "Dummy" — standardsvaret från object

Meow m = new Meow();
Console.WriteLine(m.ToString());  // "Meow" — ingen override, object svarar med klassnamnet

Person p = new Person();
Console.WriteLine(p.ToString());  // "Personen heter Kalle" — override används
```

---

## Klasserna

```csharp
class Dummy
{
    // Tom klass — ingen ToString()
    // object.ToString() svarar med fullt klassnamn: "Dummy"
}

class Meow
{
    // Också tom — samma sak
}

class Person
{
    public string Name { get; set; } = "Kalle";

    // override ersätter object.ToString() med vår egen version
    // object.ToString() var virtual — det är därför vi får lov att override:a den
    public override string ToString()
    {
        return "Personen heter " + Name;
    }
}
```

---

## Förväntad output

```
Hello, World!
Dummy
Meow
Personen heter Kalle
```

---

## Varför är det här användbart?

`Console.WriteLine(obj)` anropar `ToString()` under huven — du behöver inte skriva `.ToString()` explicit:

```csharp
Person p = new Person();
Console.WriteLine(p);  // samma sak — skriver ut "Personen heter Kalle"
```

Det gör det enkelt att debugga: definiera `ToString()` i dina klasser så ser du direkt vad ett objekt innehåller när du skriver ut det.

---

## Arvskedjan

```
object          ← alla klasser ärver härifrån automatiskt
  └─ Dummy      ← ingen override → object.ToString() → "Dummy"
  └─ Meow       ← ingen override → object.ToString() → "Meow"
  └─ Person     ← override → "Personen heter Kalle"
```
