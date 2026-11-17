
class MonsterSpawner
{

    public Monster SpawnRandomMonster()
    {
        Random random = new Random(); // Creates random monsters
        int index = random.Next(3);

        switch (index) // Creates new monsters each round
        {
            case 0: return new Goblin();
            case 1: return new Orc();
            default: return new Ogre();
        }
    }

}