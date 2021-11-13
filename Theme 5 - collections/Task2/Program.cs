using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Queue<int> purchasePool = new Queue<int>();
            int purchaseCount = 5;
            Random random = new Random();

            for (int i = 0; i < purchaseCount; i++)
                purchasePool.Enqueue(random.Next(20));

            int accountMoney = 0;

            while(purchasePool.Count > 0)
            {
                accountMoney += purchasePool.Peek();
                Console.WriteLine($"Покупка {purchasePool.Dequeue()}$, на счёте {accountMoney}$");
                Console.ReadKey();
                Console.Clear();
            }
            Console.WriteLine($"Покупки окончены. На счёте {accountMoney}$");
        }
    }
}
