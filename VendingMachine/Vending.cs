using System;
using System.Linq;



namespace VendingMachine
{
    public abstract class Vending
    { 
        protected const int MaxMoney = 100000;

        protected int NumberOfFirst { get; set; }
        protected int NumberOfSecond { get; set; }
        protected int NumberOfThird { get; set; }
        protected int Money { get; set; }
        protected bool VendLoop { get; set; }
        protected bool BuyLoop { get; set; }
        protected bool WaitForEntry { get; set; }

        protected readonly int[] AcceptedAmount = new int[] { 1, 5, 10, 20, 50, 100, 500, 1000 };

        protected IProduct[] Stock;

        protected void BuyMenu()
        {
            BuyLoop = true;
            while (BuyLoop)
            {
                StockMenu();
                SelectItem();
            }
        }

        protected void StockMenu()
        {
            Console.Clear();
            Console.WriteLine("***********************");
            Console.WriteLine($"*    You got {Money,-9:C0}*");
            Console.WriteLine("************************************************************************");
            for (var i = 0; i < NumberOfFirst; i++)
            {
                Console.WriteLine($"*{i + 1}. {Stock[i].Name,-20} Price: {Stock[i].Price,-10:C0} Available: {Stock[i].StoreStock,-5} You have: {Stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");
            for (var i = NumberOfFirst; i < NumberOfSecond + NumberOfFirst; i++)
            {
                Console.WriteLine($"*{i + 1}. {Stock[i].Name,-20} Price: {Stock[i].Price,-10:C0} Available: {Stock[i].StoreStock,-5} You have: {Stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");

            for (var i = NumberOfThird; i < Stock.Length; i++)
            {
                Console.WriteLine(
                    9 <= i
                        ? $"*{i + 1}. {Stock[i].Name,-19} Price: {Stock[i].Price,-10:C0} Available: {Stock[i].StoreStock,-5} You have: {Stock[i].PersonalStock}*"
                        : $"*{i + 1}. {Stock[i].Name,-20} Price: {Stock[i].Price,-10:C0} Available: {Stock[i].StoreStock,-5} You have: {Stock[i].PersonalStock}*");
            }
            Console.WriteLine("************************************************************************");
        }

        /// <summary>
        /// Prints how much money you had and sets current money to 0
        /// </summary>
        protected void GetChange()
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
        protected string GetBoughtItems()
        {

            var tempString = " ";
            var gotItem = false;
            foreach (var t in Stock)
            {
                if (t.PersonalStock <= 0) continue;
                tempString = "You left with";
                gotItem = true;
            }
            if (!gotItem) return tempString;
            tempString = Stock.Where(t => t.PersonalStock > 0).Aggregate(tempString, (current, t) => current + $" {t.PersonalStock} {t.Name},");
            foreach (var t in Stock)
            {
                t.PersonalStock = 0;
            }
            tempString = tempString.Remove(tempString.Length - 1);
            tempString += ".";

            return tempString;
        }

        /// <summary>
        /// Wait for selection of item from user.
        /// </summary>
        protected void SelectItem()
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
                    Stock[o].Name = Stock[o].Name;
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
                    var usable = Stock[o] as IUsable;
                    if (usable != null && Stock[o].PersonalStock > 0)
                    {
                        Console.WriteLine("4. Use");
                    }
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            if (Stock[o].StoreStock >= 1 && Money >= Stock[o].Price)
                            {
                                Buy(Stock[o]);
                            }
                            else
                            {
                                Console.WriteLine($"\nYou don't have enough money or {Stock[o].Name} is not available");
                            }
                            WaitForEntry = false;
                            break;
                        case ConsoleKey.D2:
                            if (Stock[o] is Food)
                            {
                                var inspect = Stock[o] as Food;
                                Console.WriteLine(inspect.Inspect());
                            }
                            else if (Stock[o] is Drinks)
                            {
                                var inspect = Stock[o] as Drinks;
                                Console.WriteLine(inspect.Inspect());
                            }
                            else if (Stock[o] is Stuff)
                            {
                                var inspect = Stock[o] as Stuff;
                                Console.WriteLine(inspect.Inspect());
                            }
                            else if (Stock[o] is Games)
                            {
                                var inspect = Stock[o] as Games;
                                Console.WriteLine(inspect.Inspect());
                            }
                            break;
                        case ConsoleKey.D3:
                            WaitForEntry = false;
                            break;
                        case ConsoleKey.D4:
                            if (usable != null && Stock[o].PersonalStock > 0)
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

        protected void InsertMoney()
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
            if (Array.Exists(AcceptedAmount, element => element == amount))
            {
                Money += amount;
                Console.WriteLine($"{amount:C} was added to your balance");
                Console.ReadKey(true);
            }
            else
            {
                Console.Write("Enter a correct amount. Valid amounts: ");
                var errorMessage = AcceptedAmount.Aggregate("", (current, c) => current + $"{c},");
                errorMessage = errorMessage.Remove(errorMessage.Length - 1);
                errorMessage += "\nPress any key to go back to the menu";
                Console.WriteLine(errorMessage);
                Console.ReadKey(true);

            }
        }

        protected string GetMenuText()
        {
            return $"1.Buy\n2.Insert money\n3.Leave\n\nMoney: {Money:C}";
        }

        protected void Buy(IProduct product)
        {
            var food = product as Food;
            food?.Buy();
            var prod = product as Drinks;
            prod?.Buy();
            var stuff = product as Stuff;
            stuff?.Buy();
            var game = product as Games;
            game?.Buy();
            Money -= product.Price;
        }
    }
}
