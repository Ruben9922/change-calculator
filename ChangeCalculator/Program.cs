using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MoreLinq;
using RubenDougall.Utilities;

namespace ChangeCalculator
{
    // ReSharper disable once ClassNeverInstantiated.Global
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Change Calculator");
            Console.WriteLine("-----------------");

            decimal amount = ConsoleUtilities.ReadDecimal("Enter amount: ", 0);
            List<Coin> coins = new List<Coin>
            {
                new Coin {Value = 2},
                new Coin {Value = 1},
                new Coin {Value = 0.5m},
                new Coin {Value = 0.2m},
                new Coin {Value = 0.1m},
                new Coin {Value = 0.05m},
                new Coin {Value = 0.02m},
                new Coin {Value = 0.01m}
            };

            ComputeChange(amount, coins, out decimal remainder);

            Console.WriteLine();
            Console.Write(coins.Select(coin => coin.Value + ": " + coin.QtyUsed + "\n").Aggregate(new StringBuilder(), (sb, s) => sb.Append(s)));
            Console.WriteLine();
            Console.WriteLine("Remainder: " + remainder);
        }

        private static void ComputeChange(decimal amount, List<Coin> coins, out decimal remainder)
        {
            decimal currentRemainder = amount;
            Coin smallestCoin = coins.MinBy(coin => coin.Value).First();
            while (currentRemainder >= smallestCoin.Value)
            {
                IEnumerable<Coin> allowedCoins = coins.Where(coin => coin.Value <= currentRemainder);
                Coin largestAllowedCoin = allowedCoins.MaxBy(coin => coin.Value).First();

                int quotient = (int) Math.Floor(currentRemainder / largestAllowedCoin.Value);

                largestAllowedCoin.QtyUsed += quotient;
                
                currentRemainder -= largestAllowedCoin.Value * quotient;
            }

            remainder = currentRemainder;
        }
    }
}