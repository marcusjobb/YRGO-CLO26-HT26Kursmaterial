class Room
{
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Dictionary<string, Room> Exits { get; private set; }
    public List<Item> Items { get; private set; }
    public List<NPC> Npcs { get; private set; }

    public Vampire Guard { get; private set; }
    public bool GuardDefeated { get; private set; }

    public Room(string name, string description, Vampire guard)
    {
        Name = name;
        Description = description;
        Guard = guard;

        Exits = new Dictionary<string, Room>();
        Items = new List<Item>();
        Npcs = new List<NPC>();

        GuardDefeated = false;
    }

    public void AddExit(string direction, Room destination)
    {
        Exits.Add(direction, destination);
    }

    public void AddItem(Item item)
    {
        Items.Add(item);
    }

    public void AddNpc(NPC npc)
    {
        Npcs.Add(npc);
    }

    public void DefeatGuard()
    {
        GuardDefeated = true;
    }
}