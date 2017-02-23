using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Drinks : Product
    {
        public int centi { get; set; }
        public int price { get; set; }
        public int personalStock { get; set; }
        public int storeStock { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        readonly int[] maxMinCenti = new int[] { 33, 50 };

        public Drinks(string _name, string _description, int _price)
        {
            var rnd = new Random();
            centi = maxMinCenti[rnd.Next(0,1)];
            price = _price;
            name = _name;
            personalStock = 0;
            storeStock = rnd.Next(1, 3);
        }

        public void Use()
        {
            Console.WriteLine("You drank the {0}", name);
            personalStock--;
        }

        public void Buy()
        {
            Console.WriteLine("You bought the drink {0} for the price {1:C}", name, price);
            personalStock++;
        }
    }
}
