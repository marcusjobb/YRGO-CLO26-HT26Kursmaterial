using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GoteborgsSkogsAventyr
{
    public class Weapon
    {
        private string _name;
        private int _attackBonus;
        private int _price;

        public string Name { get { return _name; } }
        public int AttackBonus { get { return _attackBonus; } }
        public int Price { get { return _price; } }

        public Weapon (string name, int attackBonus, int price)
        { 
            _name = name;
            _attackBonus = attackBonus;
            _price = price;
        }
        public virtual int Använd()
        {
            return _attackBonus;
        }
    }
}
