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

            List<int> coinQtys = ComputeChange(amount, coins, out decimal remainder);

            Console.WriteLine();
            Console.Write(coins.Zip(coinQtys, (coin, coinQty) => coin + ": " + coinQty + "\n").Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)));
            Console.WriteLine();
            Console.WriteLine("Remainder: " + remainder);
        }

        private static List<int> ComputeChange(decimal amount, List<decimal> coins, out decimal remainder)
        {
            decimal currentRemainder = amount;
            decimal smallestCoin = coins.Min();
            List<int> coinQtys = Enumerable.Repeat(0, coins.Count).ToList();
            while (currentRemainder >= smallestCoin)
            {
                List<decimal> allowedCoins = coins.FindAll(coin => coin <= currentRemainder);
                decimal largestAllowedCoin = allowedCoins.Max();

                int quotient = (int) Math.Floor(currentRemainder / largestAllowedCoin);

                int largestAllowedCoinIndex = coins.IndexOf(largestAllowedCoin);
                coinQtys[largestAllowedCoinIndex] += quotient; // TODO: Possibly replace with objects

                currentRemainder -= largestAllowedCoin * quotient;
            }

            remainder = currentRemainder;
            return coinQtys;
        }
    }
}