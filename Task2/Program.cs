using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Как вас зовут? \n");
            string name = Console.ReadLine();
            Console.Write("Сколько вам лет? \n");
            int age = Convert.ToInt32(Console.ReadLine());
            Console.Write("Какой ваш знак зодиака? \n");
            string zodiacSign = Console.ReadLine();
            Console.Write("Кем вы работаете? \n");
            string job = Console.ReadLine();
            Console.Write($"Вас зовут {name}, вам {age} лет, вы {zodiacSign} и работаете {job}.");
        }
    }
}
