class Shop
{
    private List<Weapon> inventory;

    public Shop()
    {
        // Weapons sortiment sorted by cheapest/least attack bonus to most expensive/most attack bonus
        inventory = new List<Weapon>
        {
            new Dagger(),
            new Sword(),
            new Greatsword()
        };
    }
    public void ShowShop(Player player)
    {
        bool shopping = true;
        while (shopping)
        {
            Console.WriteLine($"---------- SHOP -----------");
            Console.WriteLine($"--------- Gold:{player.Gold} ----------");
            for (int i = 0; i < inventory.Count; i++)
            {
                Weapon weapon = inventory[i];
                Console.WriteLine($"{i+1}) {weapon.WeaponName} - +{weapon.AttackBonus} Attack - {weapon.Price} gold.");
            }
            Console.WriteLine("0) Leave Shop");

            string choice = Console.ReadLine();

            if (choice == "0")
            {
                shopping = false;
            }
            // TryParse checks if input is a number before using it as an index. otherwise it's an invalid input.
            else if (int.TryParse(choice, out int index) && index >= 1 && index <= inventory.Count)
            {
                Weapon chosenWeapon = inventory[index - 1]; // -1 as it's an index starting from 0, but player sees 1-3

                if (player.Gold >= chosenWeapon.Price)
                {
                    player.SpendGold(chosenWeapon.Price); 
                    player.EquipWeapon(chosenWeapon); // Equips chosen weapon to player, replacing previous weapon.
                    Console.WriteLine($"You bought {chosenWeapon.WeaponName}.");
                }

                else
                {
                    Console.WriteLine("You're too poor."); 

                }

            }
            else
            {
                Console.WriteLine("Invalid input. Please try again.");
            }   
        }
    }
}