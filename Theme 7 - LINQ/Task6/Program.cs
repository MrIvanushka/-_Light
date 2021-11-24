using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Soldier[] allSoldiers = new Soldier[] {
                new Soldier("Джон", "Автомат", "Сержант", 10),
                new Soldier("Иван", "Автомат", "Ефрейтор", 5),
                new Soldier("Борис", "Автомат", "Старший сержант", 12),
                new Soldier("Армен", "Автомат", "Младший сержант", 8),
                new Soldier("Андрей", "Автомат", "Рядовой", 3),
                new Soldier("Дима", "Автомат", "Рядовой", 4)
            };

            var soldierNames = from Soldier soldier in allSoldiers select new
            {
                Name = soldier.Name,
                Rank = soldier.Rank 
             };

            foreach (var soldier in soldierNames)
                Console.WriteLine(soldier.Rank + " " + soldier.Name);
        }
    }

    class Soldier
    {
        public string Name { get; private set; }
        public string Weapon { get; private set; }
        public string Rank { get; private set; }
        public int TourTimeInMonthes { get; private set; }

        public Soldier(string name, string weapon, string rank, int tourTimeInMonthes)
        {
            Name = name;
            Weapon = weapon;
            Rank = rank;
            TourTimeInMonthes = tourTimeInMonthes;
        }
    }
    

    
}
