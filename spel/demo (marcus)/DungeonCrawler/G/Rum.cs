namespace DungeonCrawlerG;

class Rum
{
    public string                    Namn       { get; }
    public string                    Beskrivning { get; }
    public Dictionary<string,string> Utgångar   { get; }
    public List<string>              Föremål    { get; }

    public Rum(string namn, string beskrivning, Dictionary<string,string> utgångar, List<string> föremål)
    {
        Namn        = namn;
        Beskrivning = beskrivning;
        Utgångar    = utgångar;
        Föremål     = föremål;
    }
}
