using System;

namespace VendingMachine
{
    internal class Games : Product
    {
        public Games(string name, string description, int price)
        {
            CreateProduct(name, description, price);
        }

        public double Rating { get; set; }

        public new void CreateProduct(string name, string description, int price)
        {
            base.CreateProduct(name, description, price);
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            float temp1 = rnd.Next(10, 30);
            float temp2 = rnd.Next(3, 6);
            Rating = Math.Round(temp1 / temp2, 1);
        }


        public new void Buy()
        {
            Console.WriteLine("\nYou bought the game {0} for the price {1:C}.", Name, Price);
            base.Buy();
        }

        public new string Inspect()
        {
            return $"{base.Inspect()}\nRating: {Rating}/10";
        }
    }
}