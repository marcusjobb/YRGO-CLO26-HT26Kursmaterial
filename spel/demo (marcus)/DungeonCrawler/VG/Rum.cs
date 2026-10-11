namespace DungeonCrawlerVG;

class Rum
{
    public string                    Namn         { get; }
    public string                    Beskrivning  { get; }
    public Dictionary<string,string> Utgångar     { get; }
    public List<string>              Föremål      { get; }
    public Dictionary<string,string> LåstaDörrar  { get; }   // riktning → nyckel-id

    public Rum(
        string namn,
        string beskrivning,
        Dictionary<string,string> utgångar,
        List<string> föremål,
        Dictionary<string,string>? låstaDörrar = null)
    {
        Namn        = namn;
        Beskrivning = beskrivning;
        Utgångar    = utgångar;
        Föremål     = föremål;
        LåstaDörrar = låstaDörrar ?? new();
    }
}
