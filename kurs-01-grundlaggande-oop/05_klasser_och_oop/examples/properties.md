# Properties — livekodning 2026-09-23

Vi byggde upp properties i tre steg: från vanliga metoder, via en riktig property med egen kropp, till den korta auto-property-versionen. Målet var att visa **varför** syntaxen finns — inte bara hur man skriver den.

## Steg 1 — vanliga Get/Set-metoder

Så här löser man "läs och skriv ett fält utifrån" utan properties alls:

```csharp
class Person
{
    private string _name;

    public string GetName()
    {
        return _name;
    }

    public void SetName(string value)
    {
        _name = value;
    }
}
```

Fungerar. Men anropen blir klumpiga: `person.SetName("Kim")` istället för `person.Name = "Kim"`. Och ingenting hindrar att man glömmer parenteserna eller blandar ihop `Get`/`Set` med ett vanligt fält.

Man gör så i Java fortfarande om man inte använder [Lombok](https://codingnomads.com/intro-to-java-lombok-maven-gradle). C# har det inbyggt.

Alltså gör vi inte Get/Set metoder, såvida inte vi gör en [BuilderClass](https://refactoring.guru/design-patterns/builder/csharp/example#lang-features) men det är en helt annan historia.

## Steg 2 — en riktig property

C# har en syntax gjord exakt för det här mönstret:

```csharp
class Person
{
    private string _name;

    public string Name
    {
        get { return _name; }
        set { _name = value; }
    }
}
```

Samma sak som metoderna — en `get`-kropp som körs när man läser, en `set`-kropp som körs när man skriver — men nu kan man skriva `person.Name = "Kim"` och `person.Name` som om det vore ett vanligt fält. `value` är ett specialord som bara finns inuti `set` — det är det som skickades in.

## Steg 3 — auto-property

När `get`/`set` inte gör något extra (bara läser/skriver fältet rakt av), låter C# dig hoppa över backing-fältet helt:

```csharp
class Person
{
    public string Name { get; set; }
    public string LastName { get; set; }
}
```

Kompilatorn skapar ett dolt fält åt dig. Det här är koden vi landade i:

```csharp
namespace properties_live
{
    internal class Person
    {
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}
```

## Behöver man `private set`?

Inte alltid. `get; set;` räcker när vem som helst utifrån får sätta värdet rakt av — som `Name` här.

`private set` (eller en egen `set`-kropp) blir viktigt när du **bearbetar** värdet istället för att bara lagra det — till exempel ett banksaldo där du inte vill tillåta `konto.Saldo = -500`, utan bara låter klassen själv addera och subtrahera via egna metoder. Se `2026-09-14_bank.md` i samma mapp för ett fullständigt exempel på det.
