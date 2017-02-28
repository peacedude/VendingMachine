using System;

namespace VendingMachine
{
    public abstract class Product : IProduct
    {
        public int Price { get; set; }
        public int PersonalStock { get; set; }
        public int StoreStock { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public void Buy()
        {
            StoreStock--;
            PersonalStock++;
        }

        public string Inspect()
        {
            return $"\nName: {Name}\nDescription: {Description}\nPrice: {Price:C}\nAvailable: {StoreStock}";
        }


        protected void CreateProduct(string name, string description, int price)
        {
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            Price = price;
            Name = name;
            Description = description;
            PersonalStock = 0;
            StoreStock = rnd.Next(1, 6);
        }
    }
}