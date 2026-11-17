namespace Havsforskarna_VG;

class Formation
{
    public string             Namn     { get; }
    public List<(int r, int k)> Pos   { get; } = [];
    public int                Storlek  { get; }
    public bool               Sjunken  { get; private set; }

    readonly HashSet<(int, int)> _träffar = [];

    public Formation(string namn, int storlek)
    {
        Namn    = namn;
        Storlek = storlek;
    }

    public bool ÄrTräffad((int r, int k) ruta) => Pos.Contains(ruta);

    public bool RegistreraTräff((int r, int k) ruta)
    {
        if (!Pos.Contains(ruta)) return false;
        _träffar.Add(ruta);
        if (_träffar.Count == Pos.Count) Sjunken = true;
        return true;
    }

    public bool ÄrKartlagd() => Sjunken;
}
