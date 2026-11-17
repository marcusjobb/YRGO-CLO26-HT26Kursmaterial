---
marp: true
theme: nion-dark
paginate: true
---

# Designmönster: snälla verktyg

**Del 3 av 3: Observer, och ett mönster ni sett i Windows Forms**

---

## Mira tar skada. Vem behöver veta?

- hälsostapeln på skärmen
- ljudet som varnar vid låg hälsa
- loggen
- kanske en prestation: "Överlevde med 1 hälsa!"

Ska `Hero` känna till alla dessa? Då måste `Hero` ändras varje gång någon ny vill veta.

---

## Idén: den som vill veta, anmäler sig

Som att prenumerera på en YouTube-kanal.
Kanalen vet inte vem du är. Den säger bara till alla prenumeranter när en ny video kommer.

```csharp
interface IHealthObserver
{
    void OnHealthChanged(string heroName, int health);
}
```

---

## Två som vill veta

```csharp
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
```

---

## Hero säger till alla

```csharp
class Hero
{
    // ... Name, Health, konstruktor
    private List<IHealthObserver> observers = new List<IHealthObserver>();

    public void Subscribe(IHealthObserver observer)
    {
        observers.Add(observer);
    }

    public void TakeDamage(int amount)
    {
        Health = Math.Max(0, Health - amount);   // Aldrig under 0

        foreach (IHealthObserver observer in observers)
        {
            observer.OnHealthChanged(Name, Health);
        }
    }
}
```

---

## Kör

```csharp
Hero mira = new Hero("Mira", 100);
mira.Subscribe(new HealthBar());
mira.Subscribe(new SoundPlayer());

mira.TakeDamage(30);
mira.TakeDamage(50);
```

```
[#######   ] Mira: 70
[##        ] Mira: 20
♪ Varningsljud: låg hälsa!
```

En ny lyssnare? En ny klass och en `Subscribe`. `Hero` rörs inte.

---

## Ni har sett Observer i Windows Forms

Vi såg det när vi tittade på Windows Forms. C# har mönstret inbyggt, och det heter `event`.

```csharp
mira.HealthChanged += health => Console.WriteLine($"Hälsostapeln visar {health}.");
```

`+=` betyder "jag vill också veta när det händer".
En knapp med `Click` i ett Windows Forms-fönster fungerar exakt så.

---

## Vilket mönster passar?

1. Spotify spelar en lista i ordning eller blandat.
2. När ett paket skickas får kunden ett sms och lagret uppdateras.
3. Ett spel läser `"goblin,troll"` ur en fil och skapar monster.

Skriv ert svar. Vi går igenom tillsammans.

---

## Läxa

`exercises/lara_d_mira_och_verktygsladan.md`

Mira hittar en verktygslåda med tre verktyg. Ni känner igen dem alla.
