using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Dictionary<string, string> employeePool = new Dictionary<string, string>();
            string command = "";

            do
            {
                command = GetVariable("<КАДРОВЫЙ УЧЁТ> ");

                try
                {
                    switch (command)
                    {
                        case "Add":
                            employeePool.Add(GetName(), GetJob());
                            break;
                        case "Remove":
                            RemoveEmployee(ref employeePool, GetName());
                            break;
                        case "PrintData":
                            PrintData(employeePool);
                            break;
                        case "Exit":
                            break;
                        case "Help":
                            Console.WriteLine("Add - добавление сотрудника \n" +
                                "Remove - удаление сотрудника по ФИО \n" +
                                "PrintData -получить список всех сотрудников \n" +
                                "Exit - выход");
                            break;
                        default:
                            throw new System.Exception("Неизвестная команда. Напишите Help для открытия списка всех команд.");
                            break;

                    }
                }
                catch (System.Exception exception)
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

        static void RemoveEmployee(ref Dictionary<string, string> employeePool, string kickName)
        {
            if(employeePool.ContainsKey(kickName))
                employeePool.Remove(kickName);
            else
                throw new System.Exception("Сотрудники с данными ФИО не найдены");
        }

        static void PrintData(Dictionary<string, string> employeePool)
        {
            if (employeePool.Count == 0)
                throw new System.Exception("База данных пуста.");

            foreach (var employee in employeePool)
                Console.WriteLine($"{employee.Key} - {employee.Value}");
        }
    }
}
