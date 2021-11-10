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

            uint minutesInHour = 60;
            uint waitingTimeInHours = (ladiesCount * singleAppointmentDuration) / minutesInHour;
            uint residuaryTimeInMinutes = (ladiesCount * singleAppointmentDuration) % minutesInHour;

            Console.WriteLine($"Вы должны отстоять в очереди {waitingTimeInHours} часа и {residuaryTimeInMinutes} минут.");
        }
    }
}
