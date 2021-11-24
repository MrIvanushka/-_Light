using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            User[] allUsers = new User[] {
                new User("Джон", 15, 100),
                new User("Иван", 30, 80),
                new User("Борис", 43, 120),
                new User("Армен", 28, 20),
                new User("Андрей", 15, 48),
                new User("Дима", 31, 34),
                new User("Катя", 27, 58),
                new User("Ислам", 40, 34),
                new User("Артём", 12, 2),
                new User("Денис", 34, 43),
                new User("Даша", 33, 43),
                new User("Сергей", 68, 76)
            };
            int topUsersCount = 3;

            User[] levelLeaders = allUsers.OrderByDescending(user => user.Level).Take(topUsersCount).ToArray();
            User[] powerLeaders = allUsers.OrderByDescending(user => user.Power).Take(topUsersCount).ToArray();

            Console.WriteLine($"Топ {topUsersCount} игроков по уровню:");

            for(int i = 0; i < levelLeaders.Length; i++)
                Console.WriteLine(i+1 + ". " + levelLeaders[i].Name);

            Console.WriteLine($"Топ {topUsersCount} игроков по силе:");

            for (int i = 0; i < powerLeaders.Length; i++)
                Console.WriteLine(i+1 + ". " + powerLeaders[i].Name);
        }
    }

    class User
    {
        public string Name { get; private set; }
        public int Level { get; private set; }
        public int Power { get; private set; }

        public User(string name, int level, int power)
        {
            Name = name;
            Level = level;
            Power = power;
        }

        public override string ToString()
        {
            return $"{Name} Уровень {Level} Сила {Power}";
        }
    }
    

    
}
