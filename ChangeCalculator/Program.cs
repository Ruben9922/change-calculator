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
            Console.WriteLine("Change Calculator");
            Console.WriteLine("-----------------");
            
            decimal amount = ConsoleUtilities.ReadDecimal("Enter amount: ", 0);
            List<decimal> coins = new List<decimal> {2, 1, 0.5m, 0.2m, 0.1m, 0.05m, 0.02m, 0.01m};

            List<int> coinQtys = ComputeChange(amount, coins);

            Console.WriteLine();
            Console.Write(coins.Zip(coinQtys, (coin, coinQty) => coin + ": " + coinQty + "\n").Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)));
        }

        private static List<int> ComputeChange(decimal amount, List<decimal> coins)
        {
            decimal remainder = amount;
            decimal smallestCoin = coins.Min();
            List<int> coinQtys = Enumerable.Repeat(0, coins.Count).ToList();
            while (remainder >= smallestCoin)
            {
                List<decimal> allowedCoins = coins.FindAll(coin => coin <= remainder);
                decimal largestAllowedCoin = allowedCoins.Max();
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