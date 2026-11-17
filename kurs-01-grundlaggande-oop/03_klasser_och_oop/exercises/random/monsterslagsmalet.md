# Övning — Monsterfight ⚔️

🔴

> 🗺️ **Rita ett flödesschema innan du kodar.** Skissa upp programflödet på papper — vilka steg tas? Vilka beslut fattas? Rita klart, lägg ner pennan, öppna sedan VS Code.

Du kombinerar karaktärsskapandet från *Karaktärsklass* med tärningslogiken från *Tärningsspelet* — och lägger ihop dem till en kamparena.

Spelaren väljer två monster. De slåss. Tärningen avgör.

**Inga metoder.** Bara variabler, Random, if/else, switch och loopar.

---

## Monstren

| Val | Monster | HP | Attack |
|-----|---------|-----|--------|
| 1 | Troll | 20 | 3 |
| 2 | Goblin | 12 | 5 |
| 3 | Drage | 30 | 2 |
| 4 | Varg | 15 | 4 |

**HP** är hur mycket livskraft monstret har.  
**Attack** är bonusen som läggs till tärningsslaget varje runda.

---

## Regler

Varje runda rullar båda monstren var sin tärning (1–6). Attackskada beräknas som:

```
skada = tärningsslag + attackbonus
```

Skadan dras av från motståndarens HP. Den som når 0 HP (eller lägre) förlorar.

---

## Uppgiften

Skriv ett program som:

1. **Väljer monster 1** — skriver ut menyn, läser in val `1`–`4` med `Console.ReadLine()`, sätter namn, HP och attack med `switch`
2. **Väljer monster 2** — samma sak
3. Skriver ut: `--- Kampen börjar! [Monster1] vs [Monster2] ---`
4. Loopar tills ett monster har noll HP eller lägre:
   - Rullar tärning för monster 1: `slump.Next(1, 7)`
   - Beräknar skada och drar av från monster 2:s HP
   - Rullar tärning för monster 2
   - Beräknar skada och drar av från monster 1:s HP
   - Skriver ut rundens resultat (vad varje monster slog, skadan, HP kvar)
   - Om ett monster är nere på 0 eller lägre — avsluta loopen
5. Skriver ut vinnaren

---

## Förväntad output (exempel)

```plaintext
Välj Monster 1:
1. Troll    (HP: 20, Attack: 3)
2. Goblin   (HP: 12, Attack: 5)
3. Drage    (HP: 30, Attack: 2)
4. Varg     (HP: 15, Attack: 4)
Ditt val: 1

Välj Monster 2:
1. Troll    (HP: 20, Attack: 3)
2. Goblin   (HP: 12, Attack: 5)
3. Drage    (HP: 30, Attack: 2)
4. Varg     (HP: 15, Attack: 4)
Ditt val: 2

--- Kampen börjar! Troll vs Goblin ---

Runda 1:
  Troll slår 4 + 3 = 7 i skada → Goblin har 5 HP kvar
  Goblin slår 2 + 5 = 7 i skada → Troll har 13 HP kvar

Runda 2:
  Troll slår 6 + 3 = 9 i skada → Goblin har -4 HP kvar
  Goblin besegrades!

⚔️ Troll vinner!
```

---

## Utmanande frågor

1. Goblin har lägre HP men högre attack — är det ett balanserat matchup? Hur kan du justera siffrorna för att göra en rättvisare kamp?
2. Vad händer om båda monstren når 0 HP i samma runda? Hanterar ditt program det som oavgjort?
3. Hur lägger du till en tredje spelare — ett "publikens monster" som väljs slumpmässigt med Random?

---

> 💡 Fler slump-exempel: [marcusjobb/UsborneBooks](https://github.com/marcusjobb/UsborneBooks) — gamla Usborne-spel från 80-talet övertatta till C#.

<details><summary>Tips: hur deklarerar jag monstrets stats?</summary>

Varje monster behöver tre variabler: namn, HP och attack. Läs in valet och sätt dem med switch:

```csharp
string monster1Namn = "";
int monster1Hp = 0;
int monster1Attack = 0;

Console.Write("Ditt val: ");
int val1 = int.Parse(Console.ReadLine());

switch (val1)
{
    case 1:
        monster1Namn = "Troll";
        monster1Hp = 20;
        monster1Attack = 3;
        break;
    case 2:
        monster1Namn = "Goblin";
        monster1Hp = 12;
        monster1Attack = 5;
        break;
    // ...
}
```

Gör samma sak för monster 2 med egna variabler: `monster2Namn`, `monster2Hp`, `monster2Attack`.

</details>

<details><summary>Tips: hur strukturerar jag kamprundan?</summary>

Loopen fortsätter så länge båda monstren har mer än 0 HP:

```csharp
int runda = 1;

while (monster1Hp > 0 && monster2Hp > 0)
{
    Console.WriteLine($"\nRunda {runda}:");

    // Monster 1 attackerar
    int slag1 = slump.Next(1, 7);
    int skada1 = slag1 + monster1Attack;
    monster2Hp -= skada1;
    Console.WriteLine($"  {monster1Namn} slår {slag1} + {monster1Attack} = {skada1} i skada → {monster2Namn} har {monster2Hp} HP kvar");

    // Kontrollera om monster 2 är nere innan monster 2 attackerar
    if (monster2Hp <= 0)
    {
        Console.WriteLine($"  {monster2Namn} besegrades!");
        break;
    }

    // Monster 2 attackerar
    int slag2 = slump.Next(1, 7);
    int skada2 = slag2 + monster2Attack;
    monster1Hp -= skada2;
    Console.WriteLine($"  {monster2Namn} slår {slag2} + {monster2Attack} = {skada2} i skada → {monster1Namn} har {monster1Hp} HP kvar");

    runda++;
}
```

</details>

<details><summary>Tips: hur skriver jag ut vinnaren?</summary>

Efter loopen — kontrollera vilket monster som fortfarande har HP kvar:

```csharp
Console.WriteLine();
if (monster1Hp > 0)
{
    Console.WriteLine($"⚔️ {monster1Namn} vinner!");
}
else if (monster2Hp > 0)
{
    Console.WriteLine($"⚔️ {monster2Namn} vinner!");
}
else
{
    Console.WriteLine("Oavgjort — båda föll i samma runda!");
}
```

</details>

<details><summary>Lösningsförslag</summary>

```csharp
Random slump = new Random();

// Välj monster 1
Console.WriteLine("Välj Monster 1:");
Console.WriteLine("1. Troll    (HP: 20, Attack: 3)");
Console.WriteLine("2. Goblin   (HP: 12, Attack: 5)");
Console.WriteLine("3. Drage    (HP: 30, Attack: 2)");
Console.WriteLine("4. Varg     (HP: 15, Attack: 4)");
Console.Write("Ditt val: ");
int val1 = int.Parse(Console.ReadLine());

string monster1Namn = "";
int monster1Hp = 0;
int monster1Attack = 0;

switch (val1)
{
    case 1: monster1Namn = "Troll";  monster1Hp = 20; monster1Attack = 3; break;
    case 2: monster1Namn = "Goblin"; monster1Hp = 12; monster1Attack = 5; break;
    case 3: monster1Namn = "Drage";  monster1Hp = 30; monster1Attack = 2; break;
    default: monster1Namn = "Varg";  monster1Hp = 15; monster1Attack = 4; break;
}

// Välj monster 2
Console.WriteLine("\nVälj Monster 2:");
Console.WriteLine("1. Troll    (HP: 20, Attack: 3)");
Console.WriteLine("2. Goblin   (HP: 12, Attack: 5)");
Console.WriteLine("3. Drage    (HP: 30, Attack: 2)");
Console.WriteLine("4. Varg     (HP: 15, Attack: 4)");
Console.Write("Ditt val: ");
int val2 = int.Parse(Console.ReadLine());

string monster2Namn = "";
int monster2Hp = 0;
int monster2Attack = 0;

switch (val2)
{
    case 1: monster2Namn = "Troll";  monster2Hp = 20; monster2Attack = 3; break;
    case 2: monster2Namn = "Goblin"; monster2Hp = 12; monster2Attack = 5; break;
    case 3: monster2Namn = "Drage";  monster2Hp = 30; monster2Attack = 2; break;
    default: monster2Namn = "Varg";  monster2Hp = 15; monster2Attack = 4; break;
}

Console.WriteLine($"\n--- Kampen börjar! {monster1Namn} vs {monster2Namn} ---");

int runda = 1;

while (monster1Hp > 0 && monster2Hp > 0)
{
    Console.WriteLine($"\nRunda {runda}:");

    int slag1 = slump.Next(1, 7);
    int skada1 = slag1 + monster1Attack;
    monster2Hp -= skada1;
    Console.WriteLine($"  {monster1Namn} slår {slag1} + {monster1Attack} = {skada1} i skada → {monster2Namn} har {monster2Hp} HP kvar");

    if (monster2Hp <= 0)
    {
        Console.WriteLine($"  {monster2Namn} besegrades!");
        break;
    }

    int slag2 = slump.Next(1, 7);
    int skada2 = slag2 + monster2Attack;
    monster1Hp -= skada2;
    Console.WriteLine($"  {monster2Namn} slår {slag2} + {monster2Attack} = {skada2} i skada → {monster1Namn} har {monster1Hp} HP kvar");

    runda++;
}

Console.WriteLine();
if (monster1Hp > 0)
{
    Console.WriteLine($"⚔️ {monster1Namn} vinner!");
}
else if (monster2Hp > 0)
{
    Console.WriteLine($"⚔️ {monster2Namn} vinner!");
}
else
{
    Console.WriteLine("Oavgjort — båda föll i samma runda!");
}
```

</details>
