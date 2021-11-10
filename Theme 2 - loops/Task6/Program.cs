using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            string name = Console.ReadLine();
            char frameSymbol = Convert.ToChar(Console.ReadLine());
            int frameWidth = name.Length + 4;

            for (int i = 0; i < frameWidth; i++)
                Console.Write(frameSymbol);
            Console.WriteLine($"\n{frameSymbol} {name} {frameSymbol}");
            for (int i = 0; i < frameWidth; i++)
                Console.Write(frameSymbol);
            Console.Write('\n');
        }
        
    }
}
