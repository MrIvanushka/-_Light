using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            uint startValue = 7;
            uint difference = 7;
            for (uint i = startValue; i < 100; i += difference)
                Console.Write(i + " ");
        }
        
    }
}
