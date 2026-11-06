# Observer: den som vill veta, anmäler sig

Läsversion av tredje passet om designmönster. Förra delen: `factory.md`. Övning: `exercises/ovning_observer.md`.

**Efter det här kan du:** känna igen Observer, och låta flera delar av ett program reagera på en händelse utan att den som orsakar händelsen känner till dem.

## Problemet

När Mira tar skada vill flera saker veta det: hälsostapeln, varningsljudet, loggen.
Om `Hero` själv anropar alla dessa måste `Hero` ändras varje gång något nytt vill veta.

## Lösningen: prenumerera

Det fungerar som att prenumerera på en YouTube-kanal. Kanalen vet inte vem du är.
När en ny video kommer säger den till alla som prenumererar.

Den som vill veta (en *observer*) håller ett löfte i form av ett `interface`. `Hero` har en lista med observers och säger till alla när hälsan ändras.

```csharp
// Hela programmet: Observer, flera får veta när Mira tar skada
Hero mira = new Hero("Mira", 100);
mira.Subscribe(new HealthBar());
mira.Subscribe(new SoundPlayer());

mira.TakeDamage(30);
mira.TakeDamage(50);

interface IHealthObserver
{
    void OnHealthChanged(string heroName, int health);
}

class HealthBar : IHealthObserver
{
    public void OnHealthChanged(string heroName, int health)
    {
        // En # per 10 hälsa
        string bar = new string('#', health / 10);
        Console.WriteLine($"[{bar,-10}] {heroName}: {health}");
    }
}

class SoundPlayer : IHealthObserver
{
    public void OnHealthChanged(string heroName, int health)
    {
        if (health < 30)
        {
            Console.WriteLine("♪ Varningsljud: låg hälsa!");
        }
    }
}

class Hero
{
    public string Name;
    public int Health;
    private List<IHealthObserver> observers = new List<IHealthObserver>();

    public Hero(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void Subscribe(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);   // Aldrig under 0

        // Hero vet inte vilka som lyssnar, bara att de vill veta
        foreach (IHealthObserver observer in observers)
        {
            observer.OnHealthChanged(Name, Health);
        }
    }
}
```

```
[#######   ] Mira: 70
[##        ] Mira: 20
♪ Varningsljud: låg hälsa!
```

`{bar,-10}` betyder "skriv `bar` och fyll ut till 10 tecken", så att stapeln alltid är lika bred.

Lägg märke till att `Hero` aldrig nämner `HealthBar` eller `SoundPlayer`. En ny lyssnare är en ny klass och ett anrop till `Subscribe`.

## C# har Observer inbyggt: event

Mönstret är så vanligt att C# har ett eget ord för det: `event`. Du har redan använt det om du har klickat på en knapp i en app.
Vi skriver inte våra egna events i den här kursen. Det räcker att du känner igen dem.

```csharp
// Hela programmet: C# har Observer inbyggt, det heter event
Hero mira = new Hero("Mira", 100);

// += betyder "jag vill också veta när det händer"
mira.HealthChanged += health => Console.WriteLine($"Hälsostapeln visar {health}.");
mira.HealthChanged += health => Console.WriteLine($"Loggen skriver: hälsa {health}.");

mira.TakeDamage(25);

class Hero
{
    public string Name;
    public int Health;
    public event Action<int>? HealthChanged;

    public Hero(string name, int health)
    {
        Name = name;
        Health = health;
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);
        HealthChanged?.Invoke(Health);   // Säg till alla som lyssnar, om någon gör det
    }
}
```

```
Hälsostapeln visar 75.
Loggen skriver: hälsa 75.
```

## Mönster du kommer att känna igen

| Mönster | Du har sett det i |
|---------|-------------------|
| **Iterator** | `foreach`: gå igenom en samling utan att veta hur den är byggd |
| **Repository** | databaskopplingen: en klass som sköter allt mot databasen |
| **Singleton** | bara *ett* exemplar av något i hela appen, till exempel en databasanslutning |
| **Strategy** | flera sätt att göra samma sak, som går att byta |
| **Factory** | objekt skapas på ett ställe |
| **Observer** | flera får veta när något händer |

## Så känner du igen Observer

> **Något händer**, och **flera andra** ska reagera, utan att den som orsakar händelsen behöver känna till dem.

Exempel: notiser när en video kommer, sms och lageruppdatering när ett paket skickas, knappar i en app.

## Testa dig själv

Vilket mönster passar? Svaren finns längst ned.

1. Spotify spelar en lista i ordning eller blandat.
2. När ett paket skickas får kunden ett sms och lagret uppdateras.
3. Ett spel läser `"goblin,troll"` ur en fil och skapar monster.

<details markdown="1">
<summary>Svar</summary>

1. **Strategy:** samma uppgift (spela listan), flera sätt.
2. **Observer:** en händelse, flera som reagerar.
3. **Factory:** text blir objekt på ett ställe.

</details>
