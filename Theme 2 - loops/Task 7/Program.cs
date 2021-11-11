using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            string message = "!!Тайное послание!!";
            string rightPassword = "abcd";
            uint attemptsNumber = 3;

            for (uint i = 0; i < attemptsNumber; i++)
            {
                Console.Write("Введите пароль: ");
                string password = Console.ReadLine();
                
                if(password == rightPassword)
                {
                    Console.WriteLine("!!Тайное послание!!");
                    break;
                }
                else
                {
                    Console.WriteLine("Пароль неверный.");
                }
            }
            
        }
        
    }
}
