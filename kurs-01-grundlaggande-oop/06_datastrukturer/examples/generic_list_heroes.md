# List\<T\> — Generisk lista med hjältar

Vi skapar en tom lista och lägger till hjältar på tre olika sätt — alla tre är korrekta C#, välj den du tycker är tydligast:

```cs
List<Hero> heroes = new();

Hero batman = new() { Name = "Bruce Wayne", Alias = "Batman" };
heroes.Add(batman);

Hero superman = new();
superman.Name = "Clark Kent";
superman.Alias = "Superman";
heroes.Add(superman);

Hero supergirl = new Hero()
{
    Name = "Kara Danvers",
    Alias = "Supergirl"
};
heroes.Add(supergirl);
```

Vi kan också lägga till direkt utan att spara i en variabel. `Insert` placerar på en specifik position — här index 0, dvs. längst fram:

```cs
heroes.Add(new Hero() { Alias = "Jokern", Name = "Jack Napier" });

heroes.Insert(0,new Hero(){ Alias="The flash", Name= "Barry Allen"});
heroes.Add(new Hero() { Name = "Arthur Curry", Alias = "Aquaman" });
heroes.Add(new Hero() { Name = "Diana Prince", Alias = "Wonder Woman" });
heroes.Add(new Hero() { Name = "Dick Grayson", Alias = "Nightwing" });
heroes.Add(new Hero() { Name = "Tim Drake", Alias = "Robin" });
```

`Sort` med en lambda sorterar listan på plats. `string.Compare` är det korrekta sättet att jämföra strängar för sortering:

```cs
// Krångelsort, vi fixar detta med Linq i näste kurs
heroes.Sort((a, b) => string.Compare(a.Name, b.Name));

Console.WriteLine("Heroes in the list");
Console.WriteLine(heroes.Count);
```

`ForEach` är en inbyggd metod på `List<T>` som tar en lambda. Pilen `=>` är lambda-symbolen — läs det som "för varje hero, gör det här":

```cs
//foreach (var hero in heroes)
//{
//    Console.WriteLine(hero.Name + " is " + hero.Alias);
//}

// foreach oneliner (funkar om det är bara en rad som loopas
// => pilen är lambda symbol 
// egentligen betyder det: kör metod (hero) { Console.WriteLine....};
// så parentesen säger vi baserar körningen på variabeln hero som ändras vid
// varje loop runda, och då körs metoden efter => pilen
heroes.ForEach(hero => Console.WriteLine(hero.Name + " is " + hero.Alias));
```

`Hero`-klassen behöver bara `Name` och `Alias`:

```cs
class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
}
```

## Hela koden

```cs
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, Generic list!");

List<Hero> heroes = new();

Hero batman = new() { Name = "Bruce Wayne", Alias = "Batman" };
heroes.Add(batman);

Hero superman = new();
superman.Name = "Clark Kent";
superman.Alias = "Superman";
heroes.Add(superman);

Hero supergirl = new Hero()
{
    Name = "Kara Danvers",
    Alias = "Supergirl"
};
heroes.Add(supergirl);

heroes.Add(new Hero() { Alias = "Jokern", Name = "Jack Napier" });

heroes.Insert(0,new Hero(){ Alias="The flash", Name= "Barry Allen"});
heroes.Add(new Hero() { Name = "Arthur Curry", Alias = "Aquaman" });
heroes.Add(new Hero() { Name = "Diana Prince", Alias = "Wonder Woman" });
heroes.Add(new Hero() { Name = "Dick Grayson", Alias = "Nightwing" });
heroes.Add(new Hero() { Name = "Tim Drake", Alias = "Robin" });

// Krångelsort, vi fixar detta med Linq i näste kurs
heroes.Sort((a, b) => string.Compare(a.Name, b.Name));

Console.WriteLine("Heroes in the list");
Console.WriteLine(heroes.Count);

//foreach (var hero in heroes)
//{
//    Console.WriteLine(hero.Name + " is " + hero.Alias);
//}

// foreach oneliner (funkar om det är bara en rad som loopas
// => pilen är lambda symbol 
// egentligen betyder det: kör metod (hero) { Console.WriteLine....};
// så parentesen säger vi baserar körningen på variabeln hero som ändras vid
// varje loop runda, och då körs metoden efter => pilen
heroes.ForEach(hero => Console.WriteLine(hero.Name + " is " + hero.Alias));

class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
}
```
