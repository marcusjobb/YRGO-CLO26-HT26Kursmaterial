# Facit — Temperaturkollen

Motsvarar: `03_villkor_och_loopar/exercises/temperature_check/temperature_check.md`

---

## Lösning

### Steg 1 — grundläggande if/else if/else

```csharp
// TemperatureCheck.cs
// Avgör klädval utifrån temperatur

class TemperatureCheck
{
    static void Main()
    {
        int temperatur = 5;

        if (temperatur < 10)
            Console.WriteLine("Kallt (under 10°C) — ta på vinterjackan");
        else if (temperatur <= 20)
            Console.WriteLine("Lagom (10–20°C) — en hoodie räcker");
        else
            Console.WriteLine("Varmt (över 20°C) — t-shirt håller");
    }
}
```

### Steg 2 — kombinerat villkor med regn

```csharp
// TemperatureCheck.cs
// Avgör klädval utifrån temperatur och väder

class TemperatureCheck
{
    static void Main()
    {
        int temperatur = 5;
        bool regnar = true;

        if (temperatur < 10 && regnar)
            Console.WriteLine("Kallt och regnigt — ta regnjacka och mössa");
        else if (temperatur < 10)
            Console.WriteLine("Kallt (under 10°C) — ta på vinterjackan");
        else if (temperatur <= 20)
            Console.WriteLine("Lagom (10–20°C) — en hoodie räcker");
        else
            Console.WriteLine("Varmt (över 20°C) — t-shirt håller");
    }
}
```

---

## Vad tränar övningen

Övningen tränar grundläggande if/else if/else-struktur och att kombinera två villkor med `&&`. Ordningen på grenarna är avgörande — den mer specifika situationen (kallt OCH regn) måste kontrolleras före den mer generella (bara kallt).

## Vanliga misstag

- Lägger `temperatur < 10` före `temperatur < 10 && regnar` — den generella grenen fångar upp fallet och regn-grenen nås aldrig
- Skriver `regnar = true` (tilldelning) i villkoret i stället för `regnar == true` eller bara `regnar`
- Tror att `&&` "delar upp" i två separata kontroller — förstår inte att båda villkoren måste vara sanna samtidigt

## Alternativa lösningar

Steg 2 kan också lösas med nästlade if-satser inuti `temperatur < 10`-grenen. Det fungerar men är mer klottrat — `&&` i ett enda villkor är tydligare och bör lyftas som den bättre varianten.
