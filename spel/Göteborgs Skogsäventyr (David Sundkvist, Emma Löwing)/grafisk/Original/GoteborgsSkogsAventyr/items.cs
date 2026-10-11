using System;
using System.Collections.Generic;
using System.Text;

namespace GoteborgsSkogsAventyr
{
    public class items
    {
        private string _itemName;
        private int _attackBoost;
        private int _defenseBoost;
        private int _hpBoost;
        private int _price;
        private bool _debuff;
        
        public items(string itemName, int attackBoost, int defenseBoost, int hpBoost, int price, bool debuff)
        {
            _itemName = itemName;
            _attackBoost = attackBoost;
            _defenseBoost = defenseBoost;
            _hpBoost = hpBoost;
            _price = price;
            _debuff = debuff;
        }

        public string ItemName
        {
            get { return _itemName; }
        }

        public int AttackBoost
        {
            get { return _attackBoost; }
        }

        public int DefenseBoost
        {
            get { return _defenseBoost; }
        }

        public int HpBoost
        {
            get { return _hpBoost; }
        }

        public int Price
        {
            get { return _price; }
        }

        public bool Debuff
        {
            get { return _debuff; }
        }
    }
    public class snus : items
    {
        public snus() : base("snus", 10, 5, 0, 20, false)
        {
        }
    }
    public class Katt : items
    {
        public Katt() : base("Katt", 25, 10, 100, 40, false)
        {
        }
    }
    public class TaylorSwiftSkiva : items
    {
        public TaylorSwiftSkiva() : base("Taylor Swift Skiva", 0, 0, 50, 70, true)
        {
        }
    }
}
