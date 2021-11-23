using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Zoo zoo = new Zoo();
            string command = "";

            do
            {
                Console.Clear();
                Console.WriteLine($"Есть {zoo.ContainerCount} вольера.\n" +
                    "Напишите номер вольера (с нуля), к которому хотите подойти.\n" +
                    "Напишите exit, чтобы выйти.");
                command = Console.ReadLine();
                int containerIndex;

                if (int.TryParse(command, out containerIndex))
                    zoo.ShowContainer(containerIndex);
                else if (command != "exit")
                    Console.WriteLine("Ошибка: неизвестная команда");
                else
                    continue;

                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }
            while (command != "exit");
        }
    }

    class Zoo
    {
        private AnimalContainer[] _animalContainers;

        public int ContainerCount => _animalContainers.Length;

        public Zoo()
        {
            _animalContainers = new AnimalContainer[]
            {
                new PigContainer(5),
                new BearContainer(2),
                new ParrotContainer(8),
                new LionContainer(3),
            };
        }

        public void ShowContainer(int index)
        {
            if (index >= _animalContainers.Length || index < 0)
                Console.WriteLine("Ошибка: нет вольера с таким номером.");
            else
                Console.WriteLine(_animalContainers[index]);
        }
    }

    abstract class AnimalContainer
    {
        private string _animalType;
        private int _count;

        public AnimalContainer(string animalType, int count)
        {
            _animalType = animalType;
            _count = count;
        }

        public override string ToString()
        {
            return $"Вольер с {_animalType}, {_count} особей. Звук " + Cry();
        }

        protected abstract string Cry();
    }

    class PigContainer : AnimalContainer
    {
        public PigContainer(int count) : base("свиньи", count)
        { }

        protected override string Cry()
        {
            return "Хрю";
        }
    }

    class BearContainer : AnimalContainer
    {
        public BearContainer(int count) : base("медведи", count)
        { }

        protected override string Cry()
        {
            return "<вой медведя>";
        }
    }

    class ParrotContainer : AnimalContainer
    {
        public ParrotContainer(int count) : base("попугаи", count)
        { }

        protected override string Cry()
        {
            return "<птичий крик>";
        }
    }

    class LionContainer : AnimalContainer
    {
        public LionContainer(int count) : base("львы", count)
        { }

        protected override string Cry()
        {
            return "Рррррр";
        }
    }
}