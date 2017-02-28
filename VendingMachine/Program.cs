using System;
using System.Threading;

namespace VendingMachine
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var vendingLoop = true;

            Console.Write("Generating Vending Machines. Please wait ");
            for (var a = 5; a >= 1; a--)
            {
                Console.CursorLeft = 41;
                Console.Write("{0} seconds...", a); // Add space to make sure to override previous contents
                Thread.Sleep(1000);
            }

            var vendingFirst = new VendingFirst();
            var vendingGames = new VendingGames();
            var vendingEmpty = new VendingEmpty();

            while (vendingLoop)
            {
                Console.Clear();
                Console.WriteLine(
                    "1. Main Vending Machine\n2. Games Vending Machine\n3. Empty Vending Machine\n\n0. Quit");
                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.D1:
                        vendingFirst.Menu();
                        break;
                    case ConsoleKey.D2:
                        vendingGames.Menu();
                        break;
                    case ConsoleKey.D3:
                        vendingEmpty.Menu();
                        break;
                    case ConsoleKey.D0:
                        vendingLoop = false;
                        break;
                }
            }
        }
    }
}