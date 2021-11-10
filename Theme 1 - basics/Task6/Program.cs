using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            uint singleAppointmentDuration = 10;
            Console.Write("Введите кол-во старушек: ");
            uint ladiesCount = Convert.ToUInt32(Console.ReadLine());

            uint waitingTimeInHours = (ladiesCount * singleAppointmentDuration) / 60;
            uint residuaryTimeInMinutes = (ladiesCount * singleAppointmentDuration) % 60;

            Console.WriteLine($"Вы должны отстоять в очереди {waitingTimeInHours} часа и {residuaryTimeInMinutes} минут.");
        }
    }
}
