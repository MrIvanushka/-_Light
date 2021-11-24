using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Visitor[] visitors = new Visitor[] {
                new Visitor("Джон", 15, "Бронхит"),
                new Visitor("Иван", 30, "Гаймарит"),
                new Visitor("Борис", 43, "Коронавирус"),
                new Visitor("Армен", 28, "Бронхит"),
                new Visitor("Андрей", 15, "Астма"),
                new Visitor("Дима", 31, "Коронавирус"),
                new Visitor("Катя", 27, "Коронавирус"),
                new Visitor("Ислам", 40, "Простуда"),
                new Visitor("Артём", 12, "Ангина"),
                new Visitor("Денис", 34, "Ветрянка"),
                new Visitor("Даша", 33, "Коронавирус"),
                new Visitor("Сергей", 68, "Простуда")
            };
            string command;

            do
            {
                Console.Write("<больница>");
                command = Console.ReadLine();

                switch(command)
                {
                    case "SortByName":
                        visitors = visitors.OrderBy(visitor => visitor.Name).ToArray();
                        Console.WriteLine("Список больных отсортирован по ФИО");
                        break;
                    case "SortByAge":
                        visitors = visitors.OrderBy(visitor => visitor.Age).ToArray();
                        Console.WriteLine("Список больных отсортирован по возрасту");
                        break;
                    case "PrintData":
                        foreach (var visitor in visitors)
                            Console.WriteLine(visitor);
                        break;
                    case "Help":
                        Console.WriteLine("SortByName - сортировка списка больных по ФИО \n" +
                            "SortByAge - сортировка списка больных по ФИО \n" +
                            "FindWith <название болезни> - вывести всех с данной болезнью \n" +
                            "PrintData - вывести всех больных \n" +
                            "Exit - выход из программы");
                        break;
                    case "Exit":
                        break;
                    default:
                        string[] commandList = command.Split(' ');

                        if(commandList[0] == "FindWith")
                        {
                            if (commandList.Length == 1)
                            {
                                Console.WriteLine("Ошибка: FindWith должна принимать аргументы");
                            }
                            else
                            {
                                var filteredVisitors = visitors.Where(visitor => visitor.Disease == commandList[1]);

                                if(filteredVisitors.Count() > 0)
                                    foreach (var visitor in filteredVisitors)
                                        Console.WriteLine(visitor);
                                else
                                    Console.WriteLine("Пациентов с подобной болезнью не найдено");
                            }
                        }    
                        else
                        {
                            Console.WriteLine("Ошибка: неизвестная команда. Напишите Help для просмотра всех команд.");
                        }
                        break;

                }
            }
            while (command != "Exit");
        }
    }

    class Visitor
    {
        public string Name { get; private set; }
        public string Disease { get; private set; }
        public int Age { get; private set; }

        public Visitor(string name, int age, string disease)
        {
            Name = name;
            Age = age;
            Disease = disease;
        }

        public override string ToString()
        {
            return $"{Name}, {Age} лет. {Disease}";
        }
    }
    

    
}
