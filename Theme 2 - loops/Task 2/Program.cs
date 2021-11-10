using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите exit чтобы выйти: ");
            string command = Console.ReadLine();
            while(command != "exit")
            {
                Console.Write("Ввод неверный. \nВведите exit чтобы выйти: ");
                command = Console.ReadLine();
            }
        }
    }
}
