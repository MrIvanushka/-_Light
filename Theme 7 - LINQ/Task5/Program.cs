using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            CannedMeat[] allCans = new CannedMeat[] {
                new CannedMeat("Барс", 2019, 3),
                new CannedMeat("Совок", 2015, 3),
                new CannedMeat("Гродфуд", 2018, 6),
                new CannedMeat("Русский изыск", 2016, 2),
                new CannedMeat("Агрокомбинат СНОВ", 2019, 2),
                new CannedMeat("ОВА", 2017, 5)
            };
            int currentYear = 2021;
            var expiredCans = allCans.Where(can => can.IsExpired(currentYear) == true);

            if (expiredCans.Count() > 0)
            {
                Console.WriteLine("Просроченные банки: ");

                foreach (var can in expiredCans)
                    Console.WriteLine(can.Name);
            }
            else
            {
                Console.WriteLine("Просрочки нет.");
            }
        }
    }

    class CannedMeat
    {
        public string Name { get; private set; }
        private int _yearOfManufacture;
        private int _storageDuration;

        public CannedMeat(string name, int yearOfManufacture, int storageDuration)
        {
            Name = name;
            _yearOfManufacture = yearOfManufacture;
            _storageDuration = storageDuration;
        }

        public bool IsExpired(int currentYear)
        {
            return _yearOfManufacture + _storageDuration < currentYear;
        }
    }
    

    
}
