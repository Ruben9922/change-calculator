using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RubenDougall.Utilities;

namespace ChangeCalculator
{
    class Program
    {
        private static void Main(string[] args)
        {
            double amount = ConsoleUtilities.ReadDouble("Enter amount: ", 0);
            List<double> coins = new List<double> {2, 1, 0.5, 0.2, 0.1, 0.05, 0.02, 0.01};

            List<int> coinQtys = ComputeChange(amount, coins);

            Console.WriteLine();
            Console.Write(coins.Zip(coinQtys, (coin, coinQty) => coin + ": " + coinQty + "\n").Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)));
        }

        private static List<int> ComputeChange(double amount, List<double> coins)
        {
            double remainder = amount;
            double smallestCoin = coins.Min();
            List<int> coinQtys = Enumerable.Repeat(0, coins.Count).ToList();
            while (remainder >= smallestCoin)
            {
                List<double> allowedCoins = coins.FindAll(coin => coin <= remainder);
                double largestAllowedCoin = allowedCoins.Max();
                int largestAllowedCoinIndex = coins.IndexOf(largestAllowedCoin);
                
                Console.WriteLine("r"+remainder);
                Console.WriteLine("c"+largestAllowedCoin);

                coinQtys[largestAllowedCoinIndex]++; // TODO: Possibly replace with objects

                remainder -= largestAllowedCoin;
            }

            return coinQtys;
        }
    }
}