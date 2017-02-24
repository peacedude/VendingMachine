﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine
{
    class Drinks : Product, Usable
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
            var rnd = new Random(Guid.NewGuid().GetHashCode());
            centi = maxMinCenti[rnd.Next(0,1)];
            price = _price;
            name = _name;
            description = _description;
            personalStock = 0;
            storeStock = rnd.Next(1, 6);
        }

        public void Use()
        {
            Console.WriteLine("You drank the {0}", name);
            personalStock--;
        }

        public void Buy()
        {
            Console.WriteLine("You bought the drink {0} for the price {1:C}", name, price);
            storeStock--;
            personalStock++;
        }
        public string Inspect()
        {
            return string.Format("\nName: {0}\nDescription: {1}\nVolume: {2}cl\nPrice: {3}\nAvailable: {4}", name, description, centi, price, storeStock);
        }
    }
}
