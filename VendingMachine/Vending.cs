using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace VendingMachine
{
    class Vending
    {
        private int Money { get; set; }
        private bool vendLoop { get; set; }

        int[] acceptedAmount = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };

        Product[] stock = new Product[8];

        public Vending()
        {
            var cola = new Drinks("Cola", "Brun dryck", 5);
            var fanta = new Drinks("Fanta", "Orange dryck", 5);
            var pepsi = new Drinks("Pepsi", "Brun dryck", 5);
            var zingo = new Drinks("Zingo", "Orange dryck", 5);

            var baguette = new Food("Baguette", "Långt bröd med ost", 200, 20);
            var kycklingspett = new Food("Kycklingspett", "Kyckligng på spett", 120, 30);
            var rakburk = new Food("Räkburk", "Burk med räkor", 500, 100);
            var smorgastarta = new Food("Smörgåstårta", "Smörgås i lager", 1000, 195);
            stock = new Product[] { cola, fanta, pepsi, zingo, baguette, kycklingspett, rakburk, smorgastarta };
        }

        public void StockMenu()
        {
            for(int i = 0; i < stock.Length; i++)
            {
                Console.WriteLine("{0}. {1,-20:10} Price: {2:C0} ",i + 1, stock[i].name, stock[i].price);
            }
        }


        public void Menu()
        {
            vendLoop = true;
            while (vendLoop == true)
            {
                Console.Clear();
                Console.WriteLine(GetMenuText());
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        break;
                    case ConsoleKey.D2:
                        InsertMoney();
                        break;
                    case ConsoleKey.D3:
                        vendLoop = false;
                        break;

                }
            }

        }

        public void InsertMoney()
        {
            Console.Write("Enter an amount: ");
            string amountString = Console.ReadLine();
            int amount = 0;
            try
            {
                amount = int.Parse(amountString);
            }
            catch (FormatException)
            {

            }
            if (Array.Exists(acceptedAmount, element => element == amount))
            {
                Money += amount;
                Console.WriteLine("{0:C} was added to your balance", amount);
                Console.ReadKey(true);
            }
            else
            {
                Console.Write("Enter a correct amount. Valid amounts: ");
                string errorMessage = "";
                foreach (int c in acceptedAmount)
                {
                    errorMessage += string.Format("{0},", c);
                }
                errorMessage = errorMessage.Remove(errorMessage.Length - 1);
                errorMessage += "\nPress any key to go back to the menu";
                Console.WriteLine(errorMessage);
                Console.ReadKey(true);

            }

        }

        public string GetMenuText()
        {
            return string.Format("1.Buy(TBD)\n2.Insert money\n3.Leave(TBD)\n\nMoney: {0}", Money);
        }

        public void Buy(Product _product)
        {
            _product.Buy();
        }

    }
}
