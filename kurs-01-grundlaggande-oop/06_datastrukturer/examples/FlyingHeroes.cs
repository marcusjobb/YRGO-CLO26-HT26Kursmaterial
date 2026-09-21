// Demo: Dictionary med objekt som värde + cirkulärreferens
// Visar: Dictionary<string, T>, objektinitierare, korsrefererade objekt
// Körs i: konsollapp (.NET 10, top-level statements)

Console.WriteLine("Hello, World!");

// Nyckeln är aliaset (det namn som används för uppslagning),
// värdet är ett fullt Hero-objekt med namn, alias och kusin.
Dictionary<string, Hero> flying = new Dictionary<string, Hero>();

Hero superman = new Hero() { Name = "Clark Kent", Alias = "Superman" };

// supergirl skapas med objektinitierare och pekar direkt på superman som kusin.
// superman.Cousin sätts separat efteråt — de kan inte peka på varandra i samma rad.
Hero supergirl = new Hero()
    { Name = "Kara Danvers", Alias = "Supergirl", Cousin = superman };
superman.Cousin = supergirl;

flying.Add("Superman", superman);
flying.Add("Supergirl", supergirl);

// Vanlig uppslagning via nyckel → hämtar objektets Name-property
Console.WriteLine($"Supergirl: {flying["Supergirl"].Name}");

// .Cousin är en Hero-referens, inte en kopia — vi följer pekaren till ett annat objekt
Console.WriteLine($"Hennes kusin: {flying["Supergirl"].Cousin.Name}");

// supergirl.Cousin = superman, superman.Cousin = supergirl
// Kedjan vänder tillbaka till supergirl — vi är tillbaka på start
Console.WriteLine($"Kusinens kusin: {flying["Supergirl"].Cousin.Cousin.Name}");

// Och ett steg till: supergirl → superman → supergirl → superman
// Man kan följa kedjan hur länge som helst — det finns ingen stoppregel
Console.WriteLine($"Kusinens kusins kusin: {flying["Supergirl"].Cousin.Cousin.Cousin.Name}");

// Poängen: objekt i C# är referenser, inte kopior.
// Två objekt kan peka på varandra — en s.k. cirkulärreferens.
// Det är helt lagligt, men kräver att man sätter minst en av pekarna efter att båda objekten existerar. 🫣

class Hero
{
    public string Name { get; set; }
    public string Alias { get; set; }
    public Hero Cousin { get; set; } // Referens till ett annat Hero-objekt — kan vara null
}
