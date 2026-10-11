class Ship
{
    public int length {get; private set;}
    public int hits {get; private set;}
    public string name {get; private set;}
    public Direction direction {get;private set;}
    public List<Position> positions = new List<Position>();

    public bool hit()
    {
        hits ++;
        if(length == hits)
        {
            Console.WriteLine("ship sunk");
            return true;
        }
        else
         return false;
    }


    public void ShipCordinats(int x, int y)
    {
        Position position = new Position(x, y);
        positions.Add(position);
    }
    

    public Ship(string name,int length )
    {
        this.name = name;
        this.length = length;
        this.hits = 0;
    
    }


}
    

enum Direction
{
    Horizontal,
    Vertical
}

