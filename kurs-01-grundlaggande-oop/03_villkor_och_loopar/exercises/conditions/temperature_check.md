# Övning — Temperaturkollen

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟢

Du ska skriva ett program som avgör vad man ska ha på sig beroende på temperaturen. Steg 1 bygger grunden. Steg 2 utökar den med ett extra villkor.

---

## Steg 1: Vad ska jag ha på mig?

Deklarera en heltalsvariabel `temperatur` och sätt den till ett värde du väljer.

Skriv sedan en if/else if/else-sats som skriver ut ett av dessa tre meddelanden beroende på värdet:

- Under 10: `Kallt (under 10°C) — ta på vinterjackan`
- Mellan 10 och 20 (inklusive): `Lagom (10–20°C) — en hoodie räcker`
- Över 20: `Varmt (över 20°C) — t-shirt håller`

Börja med `temperatur = 5`.

### Förväntad output
```plaintext
Kallt (under 10°C) — ta på vinterjackan
```

Testa sedan med `temperatur = 15`.

### Förväntad output
```plaintext
Lagom (10–20°C) — en hoodie räcker
```

Testa sedan med `temperatur = 28`.

### Förväntad output
```plaintext
Varmt (över 20°C) — t-shirt håller
```

<details><summary>Hur bygger jag upp if/else if/else?</summary>

```csharp
int temperatur = 5;

if (temperatur < 10)
{
    // kallkoden här
}
else if (temperatur <= 20)
{
    // lagomkoden här
}
else
{
    // varmkoden här
}
```

Ordningen spelar roll. C# testar uppifrån och ned och kör den första grenen som är sann.

</details>

---

## Steg 2: Kombinera villkor

Lägg till en andra variabel:

```csharp
bool regnar = true;
```

Utöka nu din if-sats så att utskriften förändras när det regnar **och** är kallt. Kombinationsregeln:

- Kallt (`temperatur < 10`) **och** `regnar` är `true`: `Kallt och regnigt — ta regnjacka och mössa`
- Kallt men inte regn: `Kallt (under 10°C) — ta på vinterjackan`
- Övriga grader: som i steg 1

Testa med `temperatur = 5` och `regnar = true`.

### Förväntad output
```plaintext
Kallt och regnigt — ta regnjacka och mössa
```

Testa med `temperatur = 5` och `regnar = false`.

### Förväntad output
```plaintext
Kallt (under 10°C) — ta på vinterjackan
```

Testa med `temperatur = 23` och `regnar = true`.

### Förväntad output
```plaintext
Varmt (över 20°C) — t-shirt håller
```

<details><summary>Tips: hur kombinerar jag temperatur och regnar?</summary>

Använd `&&` (och-operatorn) för att kontrollera båda villkoren på en gång:

```csharp
if (temperatur < 10 && regnar)
{
    Console.WriteLine("Kallt och regnigt — ta regnjacka och mössa");
}
else if (temperatur < 10)
{
    Console.WriteLine("Kallt (under 10°C) — ta på vinterjackan");
}
```

Ordningen är viktig: det kombinerade villkoret måste komma **före** det ensamma `temperatur < 10` — annars fångas det aldrig upp.

</details>

<details><summary>Lösningsförslag</summary>

```csharp
int temperatur = 5;
bool regnar = true;

if (temperatur < 10 && regnar)
{
    Console.WriteLine("Kallt och regnigt — ta regnjacka och mössa");
}
else if (temperatur < 10)
{
    Console.WriteLine("Kallt (under 10°C) — ta på vinterjackan");
}
else if (temperatur <= 20)
{
    Console.WriteLine("Lagom (10–20°C) — en hoodie räcker");
}
else
{
    Console.WriteLine("Varmt (över 20°C) — t-shirt håller");
}
```

</details>
