using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            List<string> firstArmy = new List<string>
                { "Иван", "Арсений", "Дмитрий", "Олег" };
            List<string> secondArmy = new List<string>
                { "Борис", "Сергей", "Богдан", "Николай" };

            List<string> transferredSoldiers = secondArmy.Where(soldier =>
            {
                if (soldier.ToUpper()[0] == 'Б')
                {
                    secondArmy.Remove(soldier);
                    return true;
                }
                return false;
            }).ToList();

            firstArmy = firstArmy.Union(transferredSoldiers).ToList();
            Console.WriteLine("Первый отряд после пополнения: ");

            foreach (var soldier in firstArmy)
                Console.WriteLine(soldier);

            Console.WriteLine("Остаток второго отряда: ");

            foreach (var soldier in secondArmy)
                Console.WriteLine(soldier);
        }
    }
}