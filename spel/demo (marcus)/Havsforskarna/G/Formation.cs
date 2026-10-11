namespace Havsforskarna_G;

class Formation
{
    public string        Namn      { get; }
    public List<(int r, int k)> Pos { get; } = [];
    public int           Storlek   { get; }

    public Formation(string namn, int storlek)
    {
        Namn   = namn;
        Storlek = storlek;
    }

    public bool ÄrTräffad((int r, int k) ruta) => Pos.Contains(ruta);
    public bool ÄrKartlagd()                    => Pos.All(p => _träffar.Contains(p));

    readonly HashSet<(int, int)> _träffar = [];

    public bool RegistreraTräff((int r, int k) ruta)
    {
        if (!Pos.Contains(ruta)) return false;
        _träffar.Add(ruta);
        return true;
    }
}
