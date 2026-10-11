//--------------------------------------- Vapen -----------------------------------
// Basklassen för alla vapen. Den är abstract så att man inte kan skapa ett
// "vanligt" Weapon – man måste välja en av subklasserna nedanför.
//
// Vapnen kan både köpas i skolcafeterian och droppas av monster:
//   Blyertspenna  +1  – 12 poletter  –  Robert      5 % drop
//   Laptop        +2  – 25 poletter  –  Foliehatt  15 % drop
//   HDMI-kabel    +3  – 45 poletter  –  Karen      50 % drop

abstract class Weapon {
    public string Namn { get; private set; }
    public int AttackBonus { get; private set; }
    public int Pris { get; private set; }            // pris i kaffepoletter
    public ConsoleColor Färg { get; protected set; } = ConsoleColor.White;

    public Weapon(string namn, int attackBonus, int pris) {
        Namn = namn;
        AttackBonus = attackBonus;
        Pris = pris;
    }

    // Varje vapen har ett eget verb när du anfaller, t.ex. "Du piskar Karen"
    public virtual string Anfallsverb => "slår";

    // Kort beskrivning som visas i shoppen
    public virtual string Beskrivning => "Ett vapen.";
}

//--------------------------------------- Sub Vapen -----------------------------------

class Blyertspenna : Weapon // droppas av Robert (5 %)
{
    public Blyertspenna() : base("Blyertspenna", 1, 12) {
        Färg = ConsoleColor.Gray;
    }
    public override string Anfallsverb => "petar";
    public override string Beskrivning => "Nyvässad. Robert lånade den och lämnade aldrig tillbaka den.";
}

class Laptop : Weapon // droppas av Foliehatt (15 %)
{
    public Laptop() : base("Laptop", 2, 25) {
        Färg = ConsoleColor.Cyan;
    }
    public override string Anfallsverb => "dunkar";
    public override string Beskrivning => "Väger 3 kg. Foliehatten har klistrat tejp över kameran.";
}

class HdmiKabel : Weapon // droppas av Karen (50 %)
{
    public HdmiKabel() : base("HDMI-kabel", 3, 45) {
        Färg = ConsoleColor.Magenta;
    }
    public override string Anfallsverb => "piskar";
    public override string Beskrivning => "Den enda som faktiskt passar projektorn. Karen vaktade den med sitt liv.";
}