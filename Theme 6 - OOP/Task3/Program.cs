using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleApp thisApp = new ConsoleApp("DataBase");

            do
            {
                thisApp.Update();
            }
            while (thisApp.IsClosed == false);
        }
    }

    class ConsoleApp
    {
        public bool IsClosed { get; private set; } = false;
        private string _title;
        private DataBase _dataBase;

        public ConsoleApp() : this("New app")
        { }

        public ConsoleApp(string title)
        {
            _title = title;
            _dataBase = new DataBase();
        }

        public void Update()
        {
            string command = GetVariable($"<{_title}> ");

            try
            {
                switch (command)
                {
                    case "Add":
                        _dataBase.Add(GetName(), GetLevel());
                        break;
                    case "Remove":
                        _dataBase.Remove(GetID());
                        break;
                    case "Ban":
                        ChangeAdmission(true);
                        break;
                    case "Unban":
                        ChangeAdmission(false);
                        break;
                    case "PrintData":
                        PrintData();
                        break;
                    case "Exit":
                        IsClosed = true;
                        break;
                    case "Help":
                        Console.WriteLine("Add - добавление игрока \n" +
                            "Remove - удаление игрока по id \n" +
                            "Ban - забанить игрока по id \n" +
                            "Unban - разбанить игрока по id \n" +
                            "PrintData -получить список всех игроков \n" +
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

        private void ChangeAdmission(bool isBanned)
        {
            int id = GetID();
            _dataBase.ChangeAdmission(GetID(), isBanned);
            Console.WriteLine(_dataBase[id]);
        }

        private void PrintData()
        {
            if (_dataBase.Count == 0)
                throw new System.Exception("База данных пуста.");

            foreach (var player in _dataBase)
                Console.WriteLine(player.Value);
        }

        private string GetName()
        {
            return GetVariable("Введите ник игрока: ");
        }

        private int GetLevel()
        {
            string message = GetVariable("Введите уровень игрока: ");
            return Convert.ToInt32(message);
        }

        private int GetID()
        {
            string message = GetVariable("Введите ID игрока: ");
            return Convert.ToInt32(message);
        }

        private string GetVariable(string message)
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
    }

    class DataBase : Dictionary<int, Player>
    {
        private int maxDictionaryKey = 0;
        
        public void Add(string name, int level)
        {
            Add(maxDictionaryKey, new Player(name, level, maxDictionaryKey));
            maxDictionaryKey++;
        }

        public void ChangeAdmission(int playerID, bool isBanned)
        {
            if (ContainsKey(playerID) == true)
                this[playerID].СhangeAdmission(isBanned);
            else
                throw new Exception("Игрок с данным ID не найден.");
        }
    }

    class Player
    {
        private string _name;
        private int _level;
        private int _id;
        private bool _isBanned = false;

        public Player(string name, int level, int id)
        {
            _name = name;
            _level = level;
            _id = id;
        }

        public void СhangeAdmission(bool isBanned)
        {
            if(_isBanned == isBanned)
            {
                if (isBanned == true)
                    throw new Exception("Этот игрок уже был забанен");
                else
                    throw new Exception("Этот игрок уже был разбанен");
            }
            _isBanned = isBanned;
        }

        public override string ToString()
        {
            string status = "Разбанен";

            if (_isBanned == true)
                status = "Забанен";
            
            return $"ID: {_id} Name: {_name} Level: {_level} {status}";
        }
    }
}