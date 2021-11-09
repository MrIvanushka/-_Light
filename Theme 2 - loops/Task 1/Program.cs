using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите сообщение: ");
            string message = Console.ReadLine();
            Console.Write("Введите количество повторений: ");
            uint repeatCount = Convert.ToUInt32(Console.ReadLine());

            for (uint i = 0; i < repeatCount; i++)
                Console.WriteLine(message);
        }
    }
}
