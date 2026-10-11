using System;
using System.Collections.Generic;
using System.Text;
namespace GoteborgsSkogsAventyr
{
    internal class shop
    {
        private List<items> _items;
        private List<Weapon> _weapons;

        public shop()
        {
            _items = new List<items>();
            _items.Add(new snus());
            _weapons = new List<Weapon>();
            _weapons.Add(new Longsword());
            _weapons.Add(new Greataxe());
            _items.Add(new Katt());
            _items.Add(new TaylorSwiftSkiva());
        }
        public void ShowItems()
        {
            Console.WriteLine("välkommen till linneparkens pressbyrå!");
            for (int i = 0; i < _items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {_items[i].ItemName} - Pris: {_items[i].Price} guld");
            }
            for (int i = 0; i < _weapons.Count; i++)
            {
                Console.WriteLine($"{_items.Count + i + 1}. {_weapons[i].Name} - Pris: {_weapons[i].Price} guld");
            }
        }
        public void ShopMenu(Spelare spelare)
        {
            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine();
                Console.WriteLine($"Du har {spelare.Gold} guld.");
                ShowItems();
                Console.WriteLine($"{_items.Count + _weapons.Count + 1}. Lämna Pressbyrån");
                Console.WriteLine();
                Console.WriteLine("Vad vill du göra?");

                string choice = Console.ReadLine();
                if (int.TryParse(choice, out int val))
                {
                    switch (val)
                    {
                        case int n when n >= 1 && n <= _items.Count:
                            BuyItem(spelare, _items[n - 1]);
                            break;

                        case int n when n > _items.Count &&
                                        n <= _items.Count + _weapons.Count:
                            BuyWeapon(spelare, _weapons[n - _items.Count - 1]);
                            break;

                        case int n when n == _items.Count + _weapons.Count + 1:
                            shopping = false;
                            Console.WriteLine("Du lämnar Pressbyrån.");
                            break;

                        default:
                            Console.WriteLine("Ogiltigt val.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Skriv ett giltigt nummer.");
                }
            }
        }   
        public void BuyItem(Spelare spelare,items item)
        {
            if (_items.Contains(item))
            {
                if (spelare.SpendGold(item.Price))
                {
                    spelare.AddItem(item);

                    Console.WriteLine($"du har köpt {item.ItemName} för {item.Price} guld!");
                }
                else 
                {
                    Console.WriteLine("du har inte tillräckligt med guld för att köpa detta föremål.");
                }
            }
            else
            {
                Console.WriteLine("denna item finns inte på pressbyrån");
            }
        }
        public void BuyWeapon(Spelare spelare, Weapon weapon)
        {
            if (_weapons.Contains(weapon))
            {
                if (spelare.SpendGold(weapon.Price))
                {
                    spelare.EquipWeapon(weapon);
                    Console.WriteLine($"Du har köpt {weapon.Name} för {weapon.Price} guld!");
                }
                else
                {
                    Console.WriteLine("Du har inte tillräckligt med guld.");
                }
            }
        }

    }
}
