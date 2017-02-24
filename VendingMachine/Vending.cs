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

        private const int NUMBER_OF_DRINKS = 4;
        private const int NUMBER_OF_FOOD = 5;
        private int Money { get; set; }
        private bool vendLoop { get; set; }
        private bool buyLoop { get; set; }
        private bool waitForEntry { get; set; }

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
            var paj = new Food("Paj", "Ost och skinkpaj", 200, 30);

            var sten = new Stuff("Sten", "Vanlig sten", 20, 1);
            var diamant = new Stuff("Diamant", "Dyr sten", 10, 9999);
            stock = new Product[] { cola, fanta, pepsi, zingo, baguette, kycklingspett, rakburk, smorgastarta, paj, sten, diamant };
        }


        public void BuyMenu()
        {
            buyLoop = true;
            while (buyLoop == true)
            {
                StockMenu();
                SelectItem();
            }
        }

        public void StockMenu()
        {
            Console.Clear();
            Console.WriteLine("***********************");
            Console.WriteLine("*    You got {0,-9:C0}*", Money);
            Console.WriteLine("************************************************************************");
            for (int i = 0; i < stock.Length - NUMBER_OF_FOOD; i++)
            {
                Console.WriteLine("*{0}. {1,-20} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, stock[i].name, stock[i].price, stock[i].storeStock, stock[i].personalStock);
            }
            Console.WriteLine("************************************************************************");
            for (int i = NUMBER_OF_DRINKS; i < NUMBER_OF_FOOD + NUMBER_OF_DRINKS; i++)
            {
                Console.WriteLine("*{0}. {1,-20} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, stock[i].name, stock[i].price, stock[i].storeStock, stock[i].personalStock);
            }
            Console.WriteLine("************************************************************************");
            int n = NUMBER_OF_FOOD + NUMBER_OF_DRINKS;
            for (int i = n; i < stock.Length; i++)
            {
                Console.WriteLine("*{0}. {1,-19} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, stock[i].name, stock[i].price, stock[i].storeStock, stock[i].personalStock);
            }
            Console.WriteLine("************************************************************************");
        }

        public void GetChange()
        {
            if (Money > 0)
            {
                Console.WriteLine("You got {0:C} back in change", Money);
                Money -= Money;
                Console.ReadKey(true);
            }
        }

        public void SelectItem()
        {
            int o = -1;
            Console.Write("Enter your selection. Type '0' to leave: ");

            while (true)
            {
                try
                {
                    o = int.Parse(Console.ReadLine());

                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a valid item number..");
                    break;
                }
                if (o == 0)
                {
                    buyLoop = false;
                    GetChange();
                    break;
                }

                try
                {
                    o--;
                    string temp = stock[o].name;
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a valid item number..");
                    break;
                }

                waitForEntry = true;
                while (waitForEntry == true)
                {
                    Console.WriteLine("\n1. Buy\n2. Inspect\n3. Don't buy");
                    Usable usable = stock[o] as Usable;
                    if (usable != null && stock[o].personalStock > 0)
                    {
                        Console.WriteLine("4. Use");
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            if (stock[o].storeStock >= 1 && Money >= stock[o].price)
                            {
                                stock[o].Buy();
                                Money -= stock[o].price;
                            }
                            else
                            {
                                Console.WriteLine("\nYou don't have enough money or {0} is not available", stock[o].name);
                            }
                            waitForEntry = false;
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine(stock[o].Inspect());
                            break;
                        case ConsoleKey.D3:
                            waitForEntry = false;
                            break;
                        case ConsoleKey.D4:
                            if (usable != null && stock[o].personalStock > 0)
                            {
                                usable.Use();
                                waitForEntry = false;
                            }
                            break;
                    }
                }


                Console.ReadKey(true);
                break;

            }



        }
        private string InspectItem(int o)
        {
            return string.Format("\n{0}:\n{1}\nPrice: {2}\nAvailable: {3}", stock[o].name, stock[o].description, stock[o].price, stock[o].storeStock);
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
                        BuyMenu();
                        break;
                    case ConsoleKey.D2:
                        InsertMoney();
                        break;
                    case ConsoleKey.D3:
                        vendLoop = false;
                        GetChange();
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
            return string.Format("1.Buy\n2.Insert money\n3.Leave\n\nMoney: {0}", Money);
        }

        public void Buy(Product _product)
        {
            _product.Buy();
        }

    }
}
