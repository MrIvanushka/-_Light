using System;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            ConsoleApp thisApp = new ConsoleApp("TrainConfigurator");

            do
            {
                thisApp.Update();
            }
            while (thisApp.IsClosed == false);
        }
    }

    class ConsoleApp
    {
        public bool IsClosed { get; private set; }
        private string _title;
        private ConsolePanel topPanel;

        public ConsoleApp() : this("TrainConfigurator")
        { }

        public ConsoleApp(string title)
        {
            IsClosed = false;
            _title = title;
            topPanel = new ConsolePanel();
        }

        public void Update()
        {
            Console.Clear();
            topPanel.Render();
            string command = GetVariable($"<{_title}> ");

            try
            {
                switch (command)
                {
                    case "CreateVoyage":
                        string startCity = GetVariable("Введити точку отправки: ");
                        string finishCity = GetVariable("Введити точку назначения: ");
                        int peopleCount = GetInt("Введите число пассажиров: ");
                        Voyage voyage = new Voyage(startCity, finishCity, peopleCount);
                        topPanel.Add(voyage);
                        break;
                    case "Exit":
                        IsClosed = true;
                        break;
                    case "Help":
                        Console.WriteLine("CreateVoyage \n"+"Exit");
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
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private int GetInt(string message)
        {
            return Convert.ToInt32(GetVariable(message));
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

    class Train
    {
        private readonly int _railcarSize;
        public int RailcarCount { get; set; }

        public Train(int peopleCount)
        {
            _railcarSize = 60;
            RailcarCount = peopleCount / _railcarSize;

            if (peopleCount % _railcarSize == 0)
                RailcarCount += 1;
        }
    }

    class Voyage
    {
        public string StartCity { get; private set; }
        public string FinishCity { get; private set; }
        public Train ThisTrain { get; private set; }

        public Voyage(string startCity, string finishCity, int peopleCount)
        {
            StartCity = startCity;
            FinishCity = finishCity;
            ThisTrain = new Train(peopleCount);
        }

    }

    class ConsolePanel : List<Voyage>
    {
        private ConsoleColor _textColor;
        private readonly int[] labelSize;

        public ConsolePanel(ConsoleColor textColor = ConsoleColor.Yellow)
        {
            labelSize = new int[] { 14, 14, 16 };
            _textColor = textColor;
        }

        public void Render()
        {
            Console.ForegroundColor = _textColor;

            Console.WriteLine("| ГОРОД ОТПРАВКИ | ГОРОД ПРИБЫТИЯ | КОЛ-СТВО ВАГОНОВ |");

            if (Count == 0)
            {
                Console.WriteLine("[Нет объявленных рейсов]");
            }
            else
            {
                ForEach(voyage =>
                {
                    ResizeableString[] labels = new ResizeableString[] 
                    {   new ResizeableString(voyage.StartCity), 
                        new ResizeableString(voyage.FinishCity),
                        new ResizeableString(voyage.ThisTrain.RailcarCount.ToString()) };

                    for (int i = 0; i < labels.Length; i++)
                    {
                        labels[i].Resize(labelSize[i]);
                        Console.Write("| " + labels[i].Value + " ");
                    }
                    Console.Write("|\n");
                });
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        class ResizeableString
        {
            public string Value { get; private set; }

            public ResizeableString(string value)
            {
                Value = value;
            }

            public void Resize(int size)
            {
                if (Value.Length > size)
                {
                    Value = Value.Substring(0, size);
                }
                else
                {
                    int delta = size - Value.Length;
                    for (int i = 0; i < delta; i++)
                        Value += " ";
                }
            }
        }
    }
   
}