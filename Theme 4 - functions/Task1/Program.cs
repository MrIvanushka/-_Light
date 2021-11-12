using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            string[] names = new string[0];
            string[] jobs = new string[0];
            string command = "";

            do
            {
                command = GetVariable("<КАДРОВЫЙ УЧЁТ> ");

                try
                {
                    switch (command)
                    {
                        case "Add":
                            AddEmployee(ref names, ref jobs, GetName(), GetJob());
                            break;
                        case "Remove":
                            RemoveEmployee(ref names, ref jobs, GetName());
                            break;
                        case "Find":
                            int[] foundEmployees = FindEmployees(names, GetVariable("Введите фамилию: "));

                            foreach (int foundEmployee in foundEmployees)
                            {
                                PrintEmployee(names, jobs, foundEmployee);
                            }
                            break;
                        case "PrintData":
                            PrintData(names, jobs);
                            break;
                        case "Exit":
                            break;
                        case "Help":
                            Console.WriteLine("Add - добавление сотрудника \n" +
                                "Remove - удаление сотрудника по ФИО \n" +
                                "Find - поиск сотрудника по фамилии \n" +
                                "PrintData -получить список всех сотрудников \n" +
                                "Exit - выход");
                            break;
                        default:
                            throw new System.Exception("Неизвестная команда. Напишите Help для открытия списка всех команд.");
                            break;

                    }
                }
                catch(System.Exception exception)
                {
                    PrintErrorMessage(exception.Message);
                }
            }
            while (command != "Exit");
        }

        static string GetName()
        {
            return GetVariable("Введите ФИО: ");
        }

        static string GetJob()
        {
            return GetVariable("Введите должность: ");
        }

        static string GetVariable(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }

        static void PrintErrorMessage(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ошибка: " + message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void AddEmployee(ref string[] names, ref string[] jobs, string newName, string newJob)
        {
            Array.Resize(ref names, names.Length + 1);
            Array.Resize(ref jobs, jobs.Length + 1);
            names[names.Length - 1] = newName;
            jobs[names.Length - 1] = newJob;
        }

        static void RemoveEmployee(ref string[] names, ref string[] jobs, string kickName)
        {
            int[] employeesWithThisSurname = FindEmployees(names, kickName.Split(' ')[0]);

            int kickCount = 0;

            foreach (int employeeIndex in employeesWithThisSurname)
                if (names[employeeIndex] == kickName)
                {
                    RemoveEmployee(ref names, ref jobs, employeeIndex);
                    kickCount++;
                }

            if (kickCount == 0)
                throw new System.Exception("Сотрудники с данными ФИО не найдены");
        }

        static void RemoveEmployee(ref string[] names, ref string[] jobs, int kickIndex)
        {
            Console.WriteLine($"{names[kickIndex]} - {jobs[kickIndex]} удалён");

            for (int i = kickIndex; i < names.Length - 1; i++)
            {
                names[i] = names[i + 1];
                jobs[i] = jobs[i + 1];
            }
            Array.Resize(ref names, names.Length - 1);
            Array.Resize(ref jobs, jobs.Length - 1);
        }

        static int[] FindEmployees(string[] names, string searchName)
        {
            int[] foundNames = new int[0];

            for(int i = 0; i < names.Length; i++)
            {
                string surname = names[i].Split(' ')[0];

                if (surname == searchName)
                {
                    Array.Resize(ref foundNames, foundNames.Length + 1);
                    foundNames[foundNames.Length - 1] = i;
                }
            }
            
            if(foundNames.Length == 0)
                throw new Exception("Сотрудники с данной фамилией не найдены");

            return foundNames;
        }

        static void PrintEmployee(string[] names, string[] jobs, int employeeIndex)
        {
            Console.WriteLine($"{employeeIndex}. {names[employeeIndex]} - {jobs[employeeIndex]}");
        }

        static void PrintData(string[] names, string[] jobs)
        {
            if (names.Length == 0)
                throw new System.Exception("База данных пуста.");

            for (int i = 0; i < names.Length; i++)
            {
                PrintEmployee(names, jobs, i);
            }
        }
    }
}
