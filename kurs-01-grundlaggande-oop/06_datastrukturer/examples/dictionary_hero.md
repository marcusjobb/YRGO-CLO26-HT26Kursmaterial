# Dictionary 2 — Hero med objektreferenser

Klassen `Hero` har en `Cousin`-egenskap som pekar på ett annat `Hero`-objekt — alltså en referens till sig själv som typ:

```cs
class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
    public Hero Cousin { get; set; }
}
```

Vi skapar två hjältar och låter dem peka på varandra. Ordningen spelar roll — vi måste skapa Superman först, sedan Supergirl med referensen, sedan sätta tillbaka Supermans `Cousin`:

```cs
Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };
Hero supergirl = new Hero() 
    { Name = "Kara Danvers", Alias = "Supergirl", Cousin =  superman };
superman.Cousin=supergirl;
```

Nu lägger vi dem i ett dictionary och hämtar via nyckel:

```cs
Dictionary<string, Hero> flying = new Dictionary<string, Hero>();
flying.Add("Superman", superman);
flying.Add("Supergirl", supergirl);

Console.WriteLine($"Supergirl: {flying["Supergirl"].Name}"); // Supergirl
Console.WriteLine($"Hennes kusin: {flying["Supergirl"].Cousin.Name}"); // Hennes kusin
```

Och här blir det lite skumt. Supergirls kusin är Superman, vars kusin är Supergirl, vars kusin är Superman... vi kan kedja hur länge som helst:

```cs
Console.WriteLine($"Kusinens kusin: {flying["Supergirl"].Cousin.Cousin.Name}"); // Hennes kusins kusin (hon själv)
Console.WriteLine($"Kusinens kusins kusin: {flying["Supergirl"].Cousin.Cousin.Cousin.Name}"); // Hennes kusins kusin (hon själv)
// Så lätt kan man skapa en rundgång i objekt 🫣
```

## Hela koden

```cs
// See https://aka.ms/new-console-template for more information

Console.WriteLine("Hello, World!");

Dictionary<string, Hero> flying = new Dictionary<string, Hero>();

Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };
Hero supergirl = new Hero() 
    { Name = "Kara Danvers", Alias = "Supergirl", Cousin =  superman };
superman.Cousin=supergirl;

flying.Add("Superman", superman);
flying.Add("Supergirl", supergirl);

Console.WriteLine($"Supergirl: {flying["Supergirl"].Name}"); // Supergirl
Console.WriteLine($"Hennes kusin: {flying["Supergirl"].Cousin.Name}"); // Hennes kusin
Console.WriteLine($"Kusinens kusin: {flying["Supergirl"].Cousin.Cousin.Name}"); // Hennes kusins kusin (hon själv)
Console.WriteLine($"Kusinens kusins kusin: {flying["Supergirl"].Cousin.Cousin.Cousin.Name}"); // Hennes kusins kusin (hon själv)
// Så lätt kan man skapa en rundgång i objekt 🫣

class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
    public Hero Cousin { get; set; }
}
```
