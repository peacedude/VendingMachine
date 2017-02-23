using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Food : Product
    {
        public int gram { get; set; }
        public int price { get; set; }
        public int personalStock { get; set; }
        public int storeStock { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Food(string _name, string _description, int _gram, int _price)
        {
            var rnd = new Random();
            gram = _gram;
            price = _price;
            name = _name;
            personalStock = 0;
            storeStock = rnd.Next(1, 3);
        }

        public void Use()
        {
            Console.WriteLine("You ate the {0}", name);
            personalStock--;
        }

        public void Buy()
        {
            Console.WriteLine("You bought the food {0} for the price {1:C}", name, price);
            personalStock++;
        }
    }
}
