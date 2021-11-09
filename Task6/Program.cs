using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            uint singleDuration = 10;
            Console.Write("Введите кол-во старушек: ");
            uint ladyCount = Convert.ToUInt32(Console.ReadLine());

            uint waitingTimeInHours = (ladyCount * singleDuration) / 60;
            uint residuaryTime = (ladyCount * singleDuration) % 60;

            Console.WriteLine($"Вы должны отстоять в очереди {waitingTimeInHours} часа и {residuaryTime} минут.");
        }
    }
}
