using System;

namespace VendingMachine
{
    public class Food : Product, IUsable
    {
        public int Gram { get; set; }

        public Food(string name, string description, int price, int gram)
        {
            CreateProduct(name, description, price, gram);
        }

        protected void CreateProduct(string name, string description, int price, int gram)
        {
            base.CreateProduct(name, description, price);
            Gram = gram;
        }

        public void Use()
        {
            Console.WriteLine("\nYou ate the {0}", Name);
            PersonalStock--;
        }

        public new void Buy()
        {
            Console.WriteLine("You bought the food {0} for the price {1:C}", Name, Price);
            base.Buy();
        }

        public new string Inspect()
        {
                return $"{base.Inspect()}\nWeight: {Gram}g";
        }
    }
}
