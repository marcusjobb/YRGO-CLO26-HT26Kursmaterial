# Havsforskarna — hur planerar man en Sänka Skepp-variant?

Innan du skriver kod: rita klasserna och rutnätet på papper eller whiteboard. Det här är en checklista för vad du behöver ha koll på.

---

## 1. Rutnätet

Spelet har **två** 10×10-rutnät per spelare: ett som visar dina egna formationer, ett som visar vad du sett hos motståndaren.

```
     Ditt rutnät (egna formationer)          Motståndarens rutnät (vad du sett)
      1 2 3 4 5 6 7 8 9 10                    1 2 3 4 5 6 7 8 9 10
   A  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~                  A  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
   B  ~ F F ~ ~ ~ ~ ~ ~ ~                  B  ~ ~ X ~ ~ ~ ~ ~ ~ ~
   C  ~ ~ ~ ~ ~ F ~ ~ ~ ~                  C  ~ ~ ~ O ~ ~ ~ ~ ~ ~
   D  ~ ~ ~ ~ ~ F ~ ~ ~ ~                  D  ~ ~ ~ ~ ~ ~ ~ ~ ~ ~
   ...                                     ...
```

**Symboler:** `~` okänt vatten · `F` din formation · `X` träff · `O` miss

**Koordinater:** rad = bokstav (A–J), kolumn = siffra (1–10). `C5` = rad C, kolumn 5.

---

## 2. Klasserna

Tre byggklossar som äger varandra (inte arv den här gången — det är **komposition**):

```
                     ┌───────────────────┐
                     │        Spel         │
                     │  (styr turerna)      │
                     └──────────┬──────────┘
                                │ äger två
                     ┌──────────▼──────────┐
                     │     OceanGrid         │
                     │ (rutnät, formationer) │
                     └──────────┬──────────┘
                                │ äger en lista av
                     ┌──────────▼──────────┐
                     │      Formation         │
                     │ (namn, storlek)        │
                     └──────────┬──────────┘
                                │ äger en lista av
                     ┌──────────▼──────────┐
                     │       Position         │
                     │   (rad, kolumn)        │
                     └───────────────────┘
```

**Skillnaden mot arv:** `Formation` **är inte** en `Position` — den **har** en lista av `Position`. Fråga dig alltid: "är det en...?" (arv) eller "har det en...?" (komposition).

> 💡 Vill ni ha VG-vänlig polymorfism: låt `Formation` vara basklass för olika formationstyper (t.ex. `Korallrev`, `Bergskedja`) — samma mönster som monster i Skogsäventyret, men frivilligt här.

---

## 3. Spelflödet

```csharp
// 1. Placering
spelare.PlaceraFormationer();          // manuellt, med koordinat + riktning
motståndare.PlaceraFormationerSlumpat(); // AI, samma valideringsregler

// 2. Spelloop — turbaserat
while (!spelÄrSlut)
{
    VisaBådaRutnät();

    Position sond = LäsSpelarensSond();       // t.ex. "C5"
    bool träff = motståndaresRutnät.TaEmotSond(sond);
    Console.WriteLine(träff ? "Träff!" : "Miss!");

    if (!spelÄrSlut)
    {
        Position aiSond = AI.VäljSond();
        spelarensRutnät.TaEmotSond(aiSond);
    }

    spelÄrSlut = motståndaresRutnät.AllaFormationerKartlagda()
              || spelarensRutnät.AllaFormationerKartlagda();
}
```

---

## 4. Kodmässigt

Det här är **pseudokod** — strukturen är rätt, men du fyller i detaljerna själv.

### Position och Formation

```csharp
class Position
{
    public int Rad;
    public int Kolumn;
}

class Formation
{
    public string Namn;
    public int Storlek;
    public List<Position> Positioner = new();

    public bool ÄrKartlagd()
    {
        // sant när ALLA positioner i Positioner är träffade
        return false;
    }
}
```

### OceanGrid

```csharp
class OceanGrid
{
    private char[,] rutnät = new char[10, 10];   // rad, kolumn
    private List<Formation> formationer = new();

    public OceanGrid()
    {
        for (int rad = 0; rad < 10; rad++)
            for (int kolumn = 0; kolumn < 10; kolumn++)
                rutnät[rad, kolumn] = '~';
    }

    public bool PlaceraFormation(Formation formation, Position start, char riktning)
    {
        // kolla: ryms formationen innanför rutnätet?
        // kolla: krockar den med en annan formation?
        // om ok: fyll rutnät[...] med 'F' för varje position, returnera true
        return false;
    }

    public bool TaEmotSond(Position sond)
    {
        // kolla om sond.Rad/sond.Kolumn träffar en formation
        // uppdatera rutnät[...] till 'X' (träff) eller 'O' (miss)
        return false;
    }

    public void VisaRutnät()
    {
        // skriv ut rutnät med kolumnrubriker (1–10) och radbokstäver (A–J)
    }
}
```

### Bokstav till index

Koordinaten `C5` är text — du behöver räkna om bokstaven till ett radindex:

```csharp
char radBokstav = 'C';
int radIndex = radBokstav - 'A';   // 'C' - 'A' = 2 → tredje raden (0-indexerat)
```

---

## Vanliga buggar att se upp för

- `char[,] rutnät = new char[10, 10];` — **rad, kolumn**, i den ordningen. Blanda inte ihop `rutnät[rad, kolumn]` med `rutnät[kolumn, rad]`, då hamnar allt i fel ruta.
- Rutnätet är **0-indexerat** i koden (`0`–`9`) men **1-indexerat** för spelaren (`1`–`10`). `kolumn - 1` när du läser input, `kolumn + 1` när du visar rutnätet.
- Radbokstav → index: `radBokstav - 'A'` funkar bara om bokstaven redan är versal. Gör `ToUpper()` på input innan du räknar om den.
- Glöm inte validera **båda** riktningarna vid placering — en formation som pekar öster kan gå utanför högerkanten, en som pekar söder kan gå utanför nederkanten.
- `ÄrKartlagd()` måste kolla **alla** positioner i formationen — en enda otestad ruta räcker för att den inte ska räknas som kartlagd.

---

## Checklista innan du börjar koda

- [ ] Rutnätet är ritat på papper — vet ni vad `rutnät[rad, kolumn]` innebär?
- [ ] `Position`, `Formation` och `OceanGrid` finns som klasser, med rätt fält
- [ ] Ni har bestämt hur radbokstav (A–J) blir ett index (0–9)
- [ ] Placeringsvalidering: inom rutnätet, ingen överlappning
- [ ] `TaEmotSond` uppdaterar rätt ruta och returnerar träff/miss korrekt
- [ ] `ÄrKartlagd()` är testad med både en helt kartlagd och en delvis kartlagd formation
