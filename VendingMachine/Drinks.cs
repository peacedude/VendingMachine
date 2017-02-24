using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Drinks : IProduct, IUsable
    {
        public int Centi { get; set; }
        public int Price { get; set; }
        public int PersonalStock { get; set; }
        public int StoreStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        private readonly int[] _maxMinCenti = new int[] { 33, 50 };

        public Drinks(string name, string description, int price)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            Centi = _maxMinCenti[rnd.Next(0,1)];
            Price = price;
            Name = name;
            Description = description;
            PersonalStock = 0;
            StoreStock = rnd.Next(1, 6);
        }

        public void Use()
        {
            Console.WriteLine("You drank the {0}", Name);
            PersonalStock--;
        }

        public void Buy()
        {
            Console.WriteLine("You bought the drink {0} for the price {1:C}", Name, Price);
            StoreStock--;
            PersonalStock++;
        }
        public string Inspect()
        {
            return
                $"\nName: {Name}\nDescription: {Description}\nVolume: {Centi}cl\nPrice: {Price}\nAvailable: {StoreStock}";
        }

        
    }
}
