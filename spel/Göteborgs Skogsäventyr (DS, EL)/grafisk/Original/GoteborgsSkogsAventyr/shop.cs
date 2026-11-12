using System;
using System.Collections.Generic;
using System.Text;
namespace GoteborgsSkogsAventyr
{
    internal class shop
    {
        private List<items> _items;

        public shop()
        {
            _items = new List<items>();
            _items.Add(new snus());
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
        }
        public void ShopMenu(Spelare spelare)
        {
            bool shopping = true;

            while (shopping)
            {
                Console.WriteLine();
                Console.WriteLine($"Du har {spelare.Gold} guld.");
                ShowItems();
                Console.WriteLine("4. Lämna Pressbyrån");
                Console.WriteLine();
                Console.WriteLine("Vad vill du göra?");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        BuyItem(spelare, _items[0]);
                        break;

                    case "2":
                        BuyItem(spelare, _items[1]);
                        break;

                    case "3":
                        BuyItem(spelare, _items[2]);
                        break;

                    case "4":
                        shopping = false;
                        Console.WriteLine("Du lämnar Pressbyrån.");
                        break;

                    default:
                        Console.WriteLine("Ogiltigt val. Välj 1, 2, 3 eller 4.");
                        break;
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

    }
}
