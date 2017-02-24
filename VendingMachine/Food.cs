using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    public class Food : IProduct, IUsable
    {
        public int Gram { get; set; }
        public int Price { get; set; }
        public int PersonalStock { get; set; }
        public int StoreStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public Food(string name, string description, int gram, int price)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            Gram = gram;
            Price = price;
            Name = name;
            Description = description;
            PersonalStock = 0;
            StoreStock = rnd.Next(1, 5);
        }

        public void Use()
        {
            Console.WriteLine("\nYou ate the {0}", Name);
            PersonalStock--;
        }

        public void Buy()
        {
            Console.WriteLine("You bought the food {0} for the price {1:C}", Name, Price);
            PersonalStock++;
        }

        public string Inspect()
        {
            return
                $"\nName: {Name}\nDescription: {Description}\nWeight: {Gram}g\nPrice: {Price}\nAvailable: {StoreStock}";
        }
    }
}
