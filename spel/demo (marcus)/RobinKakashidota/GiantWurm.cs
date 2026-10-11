namespace RobinKakashidota;

// Specifikt monster som ärver Monster — precis som uppgiften krävde för G
class GiantWurm : Monster
{
    private static readonly Random _rnd = new();
    public override int Hp { get; protected set; }

    public GiantWurm() : base("Giant Wurm")
    {
        Hp = _rnd.Next(30, 60);
    }

    public override int Attack() => _rnd.Next(5, 15);
    public override int GiveExp() => _rnd.Next(25, 50);
    public void TakeDamage(int damage) => Hp -= damage;
}
