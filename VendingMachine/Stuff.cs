using System;

namespace VendingMachine
{
    public class Stuff : Product
    {
        public int Gram { get; set; }

        public Stuff(string name, string description, int price, int gram)
        {
            CreateProduct(name, description, price, gram);
        }

        protected void CreateProduct(string name, string description, int price, int gram)
        {
            base.CreateProduct(name, description, price);
            Gram = gram;
        }

        public new void Buy()
        {
            Console.WriteLine("\nYou bought the item {0} for the price {1:C}", Name, Price);
            base.Buy();
        }

        public new string Inspect()
        {
            return $"{base.Inspect()}\nWeight: {Gram}g";
        }
    }
}
