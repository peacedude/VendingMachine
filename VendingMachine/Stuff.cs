using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Stuff : Product
    {
        public int gram { get; set; }
        public int price { get; set; }
        public int personalStock { get; set; }
        public int storeStock { get; set; }
        public string name { get; set; }
        public string description { get; set; }

        public Stuff(string _name, string _description, int _gram, int _price)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            gram = _gram;
            price = _price;
            name = _name;
            description = _description;
            personalStock = 0;
            storeStock = rnd.Next(1, 5);
        }

        public void Buy()
        {
            Console.WriteLine("You bought the drink {0} for the price {1:C}", name, price);
            storeStock--;
            personalStock++;
        }

        public string Inspect()
        {
            return string.Format("\nName: {0}\nDescription: {1}\nWeight: {2}g\nPrice: {3}\nAvailable: {4}", name, description, gram, price, storeStock);
        }
    }
}
