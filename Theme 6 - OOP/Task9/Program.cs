using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(10);
            string message = "";

            do
            {
                Console.Clear();
                aquarium.Update();
                Console.WriteLine("===================");
                Console.WriteLine("Чтобы выйти, напишите exit. \n" +
                    "Чтобы добавить рыбку напишите Add. \n" +
                    "Чтобы вытащить рыбку напишите Remove. \n" +
                    "Напишите любое сообщение, чтобы обновить аквариум.");
                Console.WriteLine("===================");
                message = Console.ReadLine();

                if(message == "Add")
                {
                    aquarium.AddFish();
                }
                else if (message == "Remove")
                {
                    Console.Write("Введите имя рыбки: ");
                    string name = Console.ReadLine();
                    aquarium.RemoveFish(name);
                }
            }
            while (message != "exit");
        }
    }

    class Aquarium
    {
        private List<Fish> _container;
        private int _capacity;

        public Aquarium(int capacity)
        {
            _container = new List<Fish>();
            _capacity = capacity;
        }

        public void Update()
        {
            List<Fish> deadFishes = new List<Fish>();

            if (_container.Count == 0)
                Console.WriteLine("Аквариум пуст");

            _container.ForEach(fish =>
            {
                fish.Grow();

                if (fish.IsAlive)
                    Console.WriteLine(fish);
                else
                    deadFishes.Add(fish);
            });

            deadFishes.ForEach(fish =>
            {
                Console.WriteLine(fish + " умерла");
                _container.Remove(fish);
            });
        }

        public void AddFish()
        {
            if (_container.Count >= _capacity)
            {
                Console.WriteLine("Ошибка: аквариум полон.");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
            {
                Console.Write("Введите имя рыбки: ");
                string name = Console.ReadLine();
                _container.Add(new Fish(name));
            }
        }

        public void RemoveFish(string name)
        {
            Fish target = null;
            _container.ForEach(fish => {
                if(fish.Name == name)
                    target = fish;
            });

            if (target == null)
            {
                Console.WriteLine("Ошибка: рыбка не найдена.");
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            else
                _container.Remove(target);
        }
    }

    class Fish
    {
        public readonly string Name;
        private int _age;
        private const int _maxAge = 10;

        public bool IsAlive => _age < _maxAge;

        public Fish(string name)
        {
            Name = name;
            _age = 1;
        }

        public void Grow()
        {
            _age += 1;
        }

        public override string ToString()
        {
            return Name + " возраст: " + _age;
        }
    }
}