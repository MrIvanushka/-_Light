using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            string message = "!!Тайное послание!!";
            string rightPassword = "abcd";
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();
            while(password != rightPassword)
            {
                Console.Write("Ошибка: пароль неверный. \nВведите пароль: ");
                password = Console.ReadLine();
            }
            Console.WriteLine("!!Тайное послание!!");
        }
        
    }
}
