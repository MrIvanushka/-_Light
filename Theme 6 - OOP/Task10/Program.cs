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

        public Zoo(int animalContainerCount = 4)
        {
            Random random = new Random();
            _animalContainers = new AnimalContainer[animalContainerCount];

            for (int i = 0; i < _animalContainers.Length; i++)
                _animalContainers[i] = new AnimalContainer();
        }

        public void ShowContainer(int index)
        {
            if (index >= _animalContainers.Length || index < 0)
                Console.WriteLine("Ошибка: нет вольера с таким номером.");
            else
                Console.Write(_animalContainers[index]);
        }
    }

    class AnimalContainer
    {
        private Animal[] _animals;

        public AnimalContainer()
        {
            Random random = new Random();
            int animalCount = random.Next(4, 12);
            _animals = new Animal[animalCount];

            for(int i = 0; i < animalCount; i++)
            {
                Gender gender = (Gender)random.Next(2);
                int animalType = random.Next(4);

                switch(animalType)
                {
                    case 0:
                        _animals[i] = new Pig(gender);
                        break;
                    case 1:
                        _animals[i] = new Bear(gender);
                        break;
                    case 2:
                        _animals[i] = new Parrot(gender);
                        break;
                    case 3:
                        _animals[i] = new Lion(gender);
                        break;
                }
            }
        }

        public override string ToString()
        {
            string letter = $"Животных : {_animals.Length} \n";

            foreach(var animal in _animals)
            {
                letter += animal + "\n";
            }
            return letter;
        }
    }

    enum Gender
    { 
        Male, 
        Female 
    }

    abstract class Animal
    {
        private string _animalType;
        private Gender _gender;

        public Animal(string animalType, Gender gender)
        {
            _animalType = animalType;
            _gender = gender;
        }

        public override string ToString()
        {
            string gender = "";

            switch(_gender)
            {
                case Gender.Female:
                    gender = "самка";
                    break;
                case Gender.Male:
                    gender = "самец";
                    break;
            }
            return $"{_animalType}, {gender}. Звук " + Cry();
        }

        protected abstract string Cry();
    }

    class Pig : Animal
    {
        public Pig(Gender gender) : base("свинья", gender)
        { }

        protected override string Cry()
        {
            return "Хрю";
        }
    }

    class Bear : Animal
    {
        public Bear(Gender gender) : base("медведь", gender)
        { }

        protected override string Cry()
        {
            return "<вой медведя>";
        }
    }

    class Parrot : Animal
    {
        public Parrot(Gender gender) : base("попугай", gender)
        { }

        protected override string Cry()
        {
            return "<птичий крик>";
        }
    }

    class Lion: Animal
    {
        public Lion(Gender gender) : base("лев", gender)
        { }

        protected override string Cry()
        {
            return "Рррррр";
        }
    }
}
