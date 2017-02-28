
using System;

namespace VendingMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            var vendingLoop = true;

//            Console.Write("Generating Vending Machines. Please wait... ");
//            for (int a = 5; a >= 1; a--)
//            {
//                Console.CursorLeft = 45;
//                Console.Write("{0} ", a);    // Add space to make sure to override previous contents
//                System.Threading.Thread.Sleep(1000);
//            }

            while (vendingLoop)
            {
                Console.Clear();
                Console.WriteLine("1. Main Vending\n2. Games Vending\n\n0. Quit");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        var vendingFirst = new VendingFirst();
                        vendingFirst.Menu();
                        break;
                    case ConsoleKey.D2:
                        var vendingGames = new VendingGames();
                        vendingGames.Menu();
                        break;
                    case ConsoleKey.D0:
                        vendingLoop = false;
                        break;
                }
            }
            
        }
    }
}
