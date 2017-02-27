using System;

namespace VendingMachine
{
    public class Drinks : Product, IUsable
    {
        public int Centi { get; set; }

        private readonly int[] _maxMinCenti = new int[] { 33, 50 };

        public Drinks(string name, string description, int price)
        {
            CreateProduct(name, description, price);
        }

        protected new void CreateProduct(string name, string description, int price)
        {
            base.CreateProduct(name, description, price);
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            Centi = _maxMinCenti[rnd.Next(0, 1)];
        }

        public void Use()
        {
            Console.WriteLine("You drank the {0}", Name);
            PersonalStock--;
        }

        public new void Buy()
        {
            Console.WriteLine("You bought the drink {0} for the price {1:C}", Name, Price);
            base.Buy();
        }
        public new string Inspect()
        {
            return $"{base.Inspect()}\nVolume: {Centi}cl";
        }
    }
}
