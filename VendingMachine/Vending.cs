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

            var baguette = new Food("Baguette", "Långt bröd med ost", 20, 200);
            var kycklingspett = new Food("Kycklingspett", "Kyckligng på spett", 30, 120);
            var rakburk = new Food("Räkburk", "Burk med räkor", 500, 100);
            var smorgastarta = new Food("Smörgåstårta", "Smörgås i lager", 195, 1000);
            var paj = new Food("Paj", "Ost och skinkpaj", 30, 200);

            var sten = new Stuff("Sten", "Vanlig sten", 1, 20);
            var diamant = new Stuff("Diamant", "Dyr sten", 9999, 10);
            var skruv = new Stuff("Skruv", "Vanlig skruv", 1, 5);
            var borrmaskin = new Stuff("Borrmaskin", "Maskin att borra med", 595, 2052);

            _stock = new IProduct[] { cola, fanta, pepsi, zingo, baguette, kycklingspett, rakburk, smorgastarta, paj, sten, diamant, skruv, borrmaskin };
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
            Console.WriteLine($"*    You got {Money,-9:C0}*");
            Console.WriteLine("************************************************************************");
            for (var i = 0; i < NumberOfDrinks; i++)
            {
                Console.WriteLine($"*{i + 1}. {_stock[i].Name,-20} Price: {_stock[i].Price,-10:C0} Available: {_stock[i].StoreStock,-5} You have: {_stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");
            for (var i = NumberOfDrinks; i < NumberOfFood + NumberOfDrinks; i++)
            {
                Console.WriteLine($"*{i + 1}. {_stock[i].Name,-20} Price: {_stock[i].Price,-10:C0} Available: {_stock[i].StoreStock,-5} You have: {_stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");
            var n = NumberOfFood + NumberOfDrinks;
            for (var i = n; i < _stock.Length; i++)
            {
                Console.WriteLine($"*{i + 1}. {_stock[i].Name,-19} Price: {_stock[i].Price,-10:C0} Available: {_stock[i].StoreStock,-5} You have: {_stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");
        }

        /// <summary>
        /// Prints how much money you had and sets current money to 0
        /// </summary>
        private void GetChange()
        {
            if (Money <= 0) return;
            Console.WriteLine($"You got {Money:C} back in change");
            Money -= Money;
            Console.ReadKey(true);
        }

        /// <summary>
        /// Returns string of items you bought.
        /// </summary>
        /// <returns>Return string</returns>
        private string GetBoughtItems()
        {

            var tempString = " ";
            var gotItem = false;
            foreach (var t in _stock)
            {
                if (t.PersonalStock <= 0) continue;
                tempString = "You left with";
                gotItem = true;
            }
            if (!gotItem) return tempString;
            tempString = _stock.Where(t => t.PersonalStock > 0).Aggregate(tempString, (current, t) => current + $" {t.PersonalStock} {t.Name},");
            tempString = tempString.Remove(tempString.Length - 1);
            tempString += ".";

            return tempString;
        }

        /// <summary>
        /// Wait for selection of item from user.
        /// </summary>
        private void SelectItem()
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
                    Console.WriteLine(GetBoughtItems());
                    Console.ReadKey(true);
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
                                Buy(_stock[o]);
                            }
                            else
                            {
                                Console.WriteLine($"\nYou don't have enough money or {_stock[o].Name} is not available");
                            }
                            WaitForEntry = false;
                            break;
                        case ConsoleKey.D2:
                            if (_stock[o] is Food)
                            {
                                var inspect = _stock[o] as Food;
                                Console.WriteLine(inspect.Inspect());
                            }
                            else if (_stock[o] is Drinks)
                            {
                                var inspect = _stock[o] as Drinks;
                                Console.WriteLine(inspect.Inspect());
                            }
                            else if (_stock[o] is Stuff)
                            {
                                var inspect = _stock[o] as Stuff;
                                Console.WriteLine(inspect.Inspect());
                            }
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
                        if (Money < 100000)
                            InsertMoney();
                        else
                        {
                            Console.WriteLine($"Can't insert more money. Limit: {MaxMoney:C0}");
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

        private void InsertMoney()
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
                Console.WriteLine($"{amount:C} was added to your balance");
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

        private string GetMenuText()
        {
            return $"1.Buy\n2.Insert money\n3.Leave\n\nMoney: {Money:C}";
        }

        private void Buy(IProduct product)
        {
            if (product is Food)
            {
                var tempProd = (Food)product;
                tempProd.Buy();
            }
            if (product is Drinks)
            {
                var tempProd = (Drinks)product;
                tempProd.Buy();
            }
            if (product is Stuff)
            {
                var tempProd = (Stuff)product;
                tempProd.Buy();
            }

            Money -= product.Price;
        }
    }
}
