namespace DungeonCrawlerVG;

class Npc
{
    public string   Namn    { get; }
    readonly string[] _repliker;
    int _index = 0;

    public Npc(string namn, string[] repliker)
    {
        Namn      = namn;
        _repliker = repliker;
    }

    public string NästaReplik()
    {
        var replik = _repliker[_index];
        _index = (_index + 1) % _repliker.Length;
        return replik;
    }
}
