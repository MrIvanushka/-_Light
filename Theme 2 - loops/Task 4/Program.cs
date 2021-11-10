using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Console.Write("Введите имя: ");
            string name = Console.ReadLine();
            Console.Write("Введите пароль: ");
            string password = Console.ReadLine();

            bool timeToExit = false;
            do
            {
                Console.Write($"<{name}>: ");
                string commandRaw = Console.ReadLine();
                string[] commandList = commandRaw.Split(' ');

                switch (commandList[0])
                {
                    case "SwitchName":
                        if (commandList.Length > 2)
                            Console.WriteLine("Ошибка: имя не должно содержать пробелы.");
                        else if (commandList.Length < 2)
                            Console.WriteLine("Ошибка: Необходимо ввести новое имя.");
                        else
                            name = commandList[1];
                        break;
                    case "ChangePassword":
                        if (commandList.Length > 1)
                            Console.WriteLine("Ошибка: данная команда не должна иметь аргументов.");
                        else
                        {
                            Console.WriteLine("Введите старый пароль: ");
                            string oldPassword = Console.ReadLine();
                            if(oldPassword == password)
                            {
                                Console.WriteLine("Введите новый пароль: ");
                                password = Console.ReadLine();
                            }
                            else
                                Console.WriteLine("Ошибка: пароль неверно введён.");
                        }
                        break;
                    case "ChangeForegroundColor":
                        if (commandList.Length > 2)
                            Console.WriteLine("Ошибка: слишком много аргументов.");
                        else if (commandList.Length < 2)
                            Console.WriteLine("Ошибка: слишком мало аргументов.");
                        {
                            switch (commandList[1])
                            {
                                case "r":
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    break;
                                case "g":
                                    Console.ForegroundColor = ConsoleColor.Green;
                                    break;
                                case "w":
                                    Console.ForegroundColor = ConsoleColor.White;
                                    break;
                                default:
                                    Console.WriteLine("Ошибка: неизвестный аргумент. \nНапишите Help для просмотра списка команд");
                                    break;
                            }
                        }
                        break;
                    case "Reset":
                        if (commandList.Length > 1)
                            Console.WriteLine("Ошибка: данная команда не должна иметь аргументов.");
                        else
                            Console.Clear();
                        break;
                    case "Help":
                        if (commandList.Length > 1)
                            Console.WriteLine("Ошибка: данная команда не должна иметь аргументов.");
                        else
                        {
                            Console.WriteLine("SwitchName [новое имя] - меняет имя пользователя \n" +
                                "ChangePassword - меняет пароль \n " +
                                "Reset - очищает консоль \n " +
                                "ChangeForegroundColor [r/g/w] - меняет цвет текста на красный/зелёный/белый \n" +
                                "Esc - выход из программы");
                        }
                        break;
                    case "Esc":
                        if (commandList.Length > 1)
                            Console.WriteLine("Ошибка: данная команда не должна иметь аргументов.");
                        else
                            timeToExit = true;
                        break;
                    default:
                        Console.WriteLine("Неизвестная команда. Напишите Help для просмотра списка команд.");
                        break;
                }
            }
            while (timeToExit == false);
        }
    }
}
