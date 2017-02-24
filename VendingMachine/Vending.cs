using System;
using System.Linq;



namespace VendingMachine
{
    public class Vending
    {

        private const int NumberOfDrinks = 4;
        private const int NumberOfFood = 5;
        private const int MaxMoney = 100000;
        private int Money { get; set; }
        private bool VendLoop { get; set; }
        private bool BuyLoop { get; set; }
        private bool WaitForEntry { get; set; }

        private readonly int[] _acceptedAmount = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };

        private readonly IProduct[] _stock;

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
            _stock = new IProduct[] { cola, fanta, pepsi, zingo, baguette, kycklingspett, rakburk, smorgastarta, paj, sten, diamant };
        }


        public void BuyMenu()
        {
            BuyLoop = true;
            while (BuyLoop)
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
            for (var i = 0; i < _stock.Length - NumberOfFood; i++)
            {
                Console.WriteLine("*{0}. {1,-20} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, _stock[i].Name, _stock[i].Price, _stock[i].StoreStock, _stock[i].PersonalStock);
            }
            Console.WriteLine("************************************************************************");
            for (var i = NumberOfDrinks; i < NumberOfFood + NumberOfDrinks; i++)
            {
                Console.WriteLine("*{0}. {1,-20} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, _stock[i].Name, _stock[i].Price, _stock[i].StoreStock, _stock[i].PersonalStock);
            }
            Console.WriteLine("************************************************************************");
            var n = NumberOfFood + NumberOfDrinks;
            for (var i = n; i < _stock.Length; i++)
            {
                Console.WriteLine("*{0}. {1,-19} Price: {2,-10:C0} Available: {3,-5} You have: {4}*", i + 1, _stock[i].Name, _stock[i].Price, _stock[i].StoreStock, _stock[i].PersonalStock);
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
            Console.Write("Enter your selection. Type '0' to leave: ");
            while (true)
            {
                int o;
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
                    BuyLoop = false;
                    GetChange();
                    break;
                }

                try
                {
                    o--;
                    _stock[o].Name = _stock[o].Name;
                }
                catch (Exception)
                {
                    Console.WriteLine("Enter a valid item number..");
                    break;
                }

                WaitForEntry = true;
                while (WaitForEntry)
                {
                    Console.WriteLine("\n1. Buy\n2. Inspect\n3. Don't buy");
                    var usable = _stock[o] as IUsable;
                    if (usable != null && _stock[o].PersonalStock > 0)
                    {
                        Console.WriteLine("4. Use");
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            if (_stock[o].StoreStock >= 1 && Money >= _stock[o].Price)
                            {
                                _stock[o].Buy();
                                Money -= _stock[o].Price;
                            }
                            else
                            {
                                Console.WriteLine("\nYou don't have enough money or {0} is not available", _stock[o].Name);
                            }
                            WaitForEntry = false;
                            break;
                        case ConsoleKey.D2:
                            Console.WriteLine(_stock[o].Inspect());
                            break;
                        case ConsoleKey.D3:
                            WaitForEntry = false;
                            break;
                        case ConsoleKey.D4:
                            if (usable != null && _stock[o].PersonalStock > 0)
                            {
                                usable.Use();
                                WaitForEntry = false;
                            }
                            break;
                    }
                }


                Console.ReadKey(true);
                break;

            }



        }

        public void Menu()
        {
            VendLoop = true;
            while (VendLoop)
            {
                Console.Clear();
                Console.WriteLine(GetMenuText());
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        BuyMenu();
                        break;
                    case ConsoleKey.D2:
                        if(Money < 100000)
                            InsertMoney();
                        else
                        {
                            Console.WriteLine("Can't insert more money. Limit: {0:C0}",MaxMoney);
                            Console.ReadKey(true);
                        }
                        break;
                    case ConsoleKey.D3:
                        VendLoop = false;
                        GetChange();
                        break;

                }
            }

        }

        public void InsertMoney()
        {
            Console.Write("Enter an amount: ");
            var amountString = Console.ReadLine();
            var amount = 0;
            try
            {
                amount = int.Parse(amountString);
            }
            catch (FormatException)
            {

            }
            if (Array.Exists(_acceptedAmount, element => element == amount))
            {
                Money += amount;
                Console.WriteLine("{0:C} was added to your balance", amount);
                Console.ReadKey(true);
            }
            else
            {
                Console.Write("Enter a correct amount. Valid amounts: ");
                var errorMessage = _acceptedAmount.Aggregate("", (current, c) => current + $"{c},");
                errorMessage = errorMessage.Remove(errorMessage.Length - 1);
                errorMessage += "\nPress any key to go back to the menu";
                Console.WriteLine(errorMessage);
                Console.ReadKey(true);

            }

        }

        public string GetMenuText()
        {
            return $"1.Buy\n2.Insert money\n3.Leave\n\nMoney: {Money}";
        }

        public void Buy(IProduct product)
        {
            product.Buy();
        }

    }
}
