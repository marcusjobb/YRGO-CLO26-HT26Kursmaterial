# Övning — Piratskatten

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

🟡

Du styr en pirat som letar skatt på fyra platser. Programmet frågar efter en plats i taget — du väljer var du letar. Loopen fortsätter tills du hittar skatten eller har sökt igenom alla platser.

---

## Platser

Det finns fyra platser:

1. Stranden
2. Grottan
3. Skogen
4. Fyrtornet

Skatten är gömd på en av dem. Bestäm i koden vilken plats som har skatten — ändra värdet och kör om för att testa olika scenarion.

```csharp
int skattPlats = 3;   // ändra detta för att flytta skatten
```

---

## Uppgiften

Skriv ett program med en `while`-loop som:

1. Skriver ut vilka platser som finns kvar att söka
2. Frågar spelaren: `Välj plats (1-4): `
3. Läser in spelarens val med `Console.ReadLine()`
4. Skriver ut vad som händer på platsen
5. Fortsätter tills spelaren hittar skatten eller alla platser är sökta

Platser som redan sökts ska inte visas igen.

---

## Förväntad output (om spelaren väljer 1, sedan 3)

```plaintext
--- Piratskatten ---

Platser kvar att söka:
  1. Stranden
  2. Grottan
  3. Skogen
  4. Fyrtornet

Välj plats (1-4): 1
Du letar på Stranden... Inget här utom sand och krabbor.

Platser kvar att söka:
  2. Grottan
  3. Skogen
  4. Fyrtornet

Välj plats (1-4): 3
Du letar på Skogen... Du hittar skatten! Guldfylld kista under ett gammalt träd!

Du vann! Skatten var gömd i Skogen.
```

## Förväntad output (om spelaren söker igenom alla platser utan att hitta — skatt på plats 4)

```plaintext
Du letar på Stranden... Inget här utom sand och krabbor.
Du letar på Grottan... Bara fladdermus och mörker.
Du letar på Skogen... Täta träd men ingen skatt.
Du letar på Fyrtornet... Du hittar skatten! Gömd bakom en gammal lykta!

Du vann! Skatten var gömd i Fyrtornet.
```

---

## Kom igång: datastruktur

Representera platserna med en array av strängar och håll koll på vilka som redan sökts med en array av bool:

```csharp
string[] platser = { "Stranden", "Grottan", "Skogen", "Fyrtornet" };
bool[] sökt = { false, false, false, false };
int skattPlats = 3;   // index 3 = Fyrtornet (obs: index börjar på 0)
```

<details><summary>Tips: hur visar jag bara platser som inte sökts?</summary>

Loopa igenom arrayen och kontrollera `sökt`-arrayen:

```csharp
for (int i = 0; i < platser.Length; i++)
{
    if (!sökt[i])
    {
        Console.WriteLine($"  {i + 1}. {platser[i]}");
    }
}
```

`i + 1` används för att visa 1–4 för spelaren, men `sökt[i]` och `platser[i]` använder 0-baserat index.

</details>

<details><summary>Tips: hur kontrollerar jag om alla platser är sökta?</summary>

En enkel metod är att räkna hur många platser som sökts:

```csharp
int antalSökt = 0;
for (int i = 0; i < sökt.Length; i++)
{
    if (sökt[i])
    {
        antalSökt++;
    }
}
bool allaSökt = antalSökt == sökt.Length;
```

</details>

<details><summary>Tips: hur läser jag in och omvandlar spelarens val?</summary>

`Console.ReadLine()` returnerar en sträng. Omvandla den till `int` med `int.Parse()`:

```csharp
string inmatning = Console.ReadLine();
int val = int.Parse(inmatning);
```

Kom ihåg att spelaren skriver 1–4, men indexen i arrayen är 0–3. Dra bort 1 för att konvertera:

```csharp
int index = val - 1;
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
string[] platser = { "Stranden", "Grottan", "Skogen", "Fyrtornet" };
string[] beskrivningar =
{
    "Inget här utom sand och krabbor.",
    "Bara fladdermus och mörker.",
    "Täta träd men ingen skatt.",
    "Gömd bakom en gammal lykta!"
};
bool[] sökt = { false, false, false, false };
int skattPlats = 3;
bool hittadSkatt = false;

Console.WriteLine("--- Piratskatten ---");

while (!hittadSkatt)
{
    // Kontrollera om alla platser är sökta
    int antalSökt = 0;
    for (int i = 0; i < sökt.Length; i++)
    {
        if (sökt[i])
        {
            antalSökt++;
        }
    }

    if (antalSökt == sökt.Length)
    {
        Console.WriteLine("Du har sökt igenom alla platser utan att hitta skatten...");
        break;
    }

    // Visa platser kvar
    Console.WriteLine("\nPlatser kvar att söka:");
    for (int i = 0; i < platser.Length; i++)
    {
        if (!sökt[i])
        {
            Console.WriteLine($"  {i + 1}. {platser[i]}");
        }
    }

    // Läs in val
    Console.Write("\nVälj plats (1-4): ");
    string inmatning = Console.ReadLine();
    int val = int.Parse(inmatning);
    int index = val - 1;

    // Validera
    if (index < 0 || index >= platser.Length || sökt[index])
    {
        Console.WriteLine("Ogiltigt val — välj en plats som inte redan är sökt.");
        continue;
    }

    // Markera som sökt och skriv ut resultat
    sökt[index] = true;
    Console.Write($"Du letar på {platser[index]}... ");

    if (index == skattPlats)
    {
        Console.WriteLine($"Du hittar skatten! {beskrivningar[index]}");
        hittadSkatt = true;
        Console.WriteLine($"\nDu vann! Skatten var gömd i {platser[index]}.");
    }
    else
    {
        Console.WriteLine(beskrivningar[index]);
    }
}
```

</details>
