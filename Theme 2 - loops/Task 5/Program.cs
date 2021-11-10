using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            uint startValue = 7, difference = 7, maxValue = 100;

            for (uint i = startValue; i < maxValue; i += difference)
                Console.Write(i + " ");
        }
        
    }
}
