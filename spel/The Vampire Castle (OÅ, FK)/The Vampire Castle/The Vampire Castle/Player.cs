class Player
{
    public Room CurrentRoom { get; private set; }
    public List<Item> Inventory { get; private set; }

    public Player(Room startingRoom)
    {
        CurrentRoom = startingRoom;
        Inventory = new List<Item>();
    }

    public void MoveTo(Room room)
    {
        CurrentRoom = room;
    }

    public void TakeItem(Item item)
    {
        Inventory.Add(item);
        CurrentRoom.Items.Remove(item);
    }

    public bool HasItem(string name)
    {
        foreach (Item item in Inventory)
        {
            if (item.Name == name)
            {
                return true;
            }
        }

        return false;
    }

    public void ShowInventory()
    {
        if (Inventory.Count == 0)
        {
            Console.WriteLine("Your inventory is empty.");
        }
        else
        {
            Console.WriteLine("You are carrying:");

            foreach (Item item in Inventory)
            {
                Console.WriteLine("- " + item.Name);
            }
        }
    }
}