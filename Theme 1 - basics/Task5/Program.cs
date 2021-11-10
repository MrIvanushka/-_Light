using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            int gemPrice = 3;
            Console.Write("Cколько у вас золота? ");
            int goldCount = Convert.ToInt32(Console.ReadLine());
            Console.Write("Cколько кристаллов хотите купить? ");
            int gemCount = Convert.ToInt32(Console.ReadLine());
            goldCount -= gemCount * gemPrice;
            Console.WriteLine($"Кристаллов получено: {gemCount}");
            Console.WriteLine($"Золота осталось: {goldCount}");
        }
    }
}
