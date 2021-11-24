using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            DataBase dataBase = new DataBase(new Criminal[] { 
                new Criminal("Джон", false, 160, 60, "бретонец"),
                new Criminal("Иван", true, 180, 85, "русский"),
                new Criminal("Борис", false, 190, 90, "русский"),
                new Criminal("Армен", false, 180, 90, "армянин")
            });

            do
            {
                dataBase.SearchCriminals();
            }
            while (dataBase.Enabled);
        }
    }

    class DataBase
    {
        private Criminal[] _criminals;

        public bool Enabled { get; private set; }

        public DataBase(Criminal[] criminals)
        {
            Enabled = true;
            _criminals = criminals;
        }

        public void SearchCriminals()
        {
            SearchData currentSearchingData = new SearchData();
            int height;
            string heightRaw = GetVariable("рост");

            if(int.TryParse(heightRaw, out height))
            {
                currentSearchingData.AddHeight(height);
            }
            else if(heightRaw != "missing")
            {
                if (heightRaw != "exit")
                {
                    Console.WriteLine("Невозможно прочитать высоту.");
                    Wait();
                }
                return;
            }
            int weight;
            string weightRaw = GetVariable("вес");

            if (int.TryParse(weightRaw, out weight))
            {
                currentSearchingData.AddWeight(weight);
            }
            else if (weightRaw != "missing")
            {
                if (weightRaw != "exit")
                {
                    Console.WriteLine("Невозможно прочитать вес.");
                    Wait();
                }
                return;
            }
            string nationality = GetVariable("национальность");

            if (nationality != "missing")
            {
                if (weightRaw == "exit")
                    return;
                else
                    currentSearchingData.AddNationality(nationality);
            }
            ShowSearchedCriminals(currentSearchingData);
        }

        private void ShowSearchedCriminals(SearchData currentSearchingData)
        {
            var filteredCriminals = from Criminal criminal in _criminals
                                    where currentSearchingData.CheckCriminalParameters(criminal)
                                    select criminal.Name;

            if (filteredCriminals.Count() > 0)
            {
                Console.WriteLine("Найденные преступники: ");

                foreach (var criminal in filteredCriminals)
                    Console.WriteLine(criminal);
            }
            else
            {
                Console.WriteLine("Преступники с данными параметрами не найдены.");
            }

            Wait();
        }

        private void Wait()
        {
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }

        private string GetVariable(string variableName)
        {
            Console.Write($"Укажите {variableName} (введите missing если хотите пропустить; exit для выхода): ");
            string message = Console.ReadLine();

            if (message == "exit")
                Enabled = false;

            return message;
        }
    }

    class SearchData
    {
        private bool _heightIsInserted;
        private bool _weightIsInserted;
        private bool _nationalityIsInserted;
        private int _height;
        private int _weight;
        private string _nationality;

        public SearchData()
        {
            _heightIsInserted = false;
            _weightIsInserted = false;
            _nationalityIsInserted = false;
        }

        public void AddHeight(int height)
        {
            if (_heightIsInserted == false)
            {
                _heightIsInserted = true;
                _height = height;
            }
        }

        public void AddWeight(int weight)
        {
            if (_weightIsInserted == false)
            {
                _weightIsInserted = true;
                _weight = weight;
            }
        }

        public void AddNationality(string nationality)
        {
            if (_nationalityIsInserted == false)
            {
                _nationalityIsInserted = true;
                _nationality = nationality;
            }
        }

        public bool CheckCriminalParameters(Criminal criminal)
        {
            if (_heightIsInserted == true && _height != criminal.Height ||
                _weightIsInserted == true && _weight != criminal.Weight ||
                _nationalityIsInserted == true && _nationality != criminal.Nationality ||
                criminal.IsInPrison)

                return false;
            else
                return true;
        }
    }
    
    class Criminal
    {
        public string Name { get; private set; }
        public bool IsInPrison { get; private set; }
        public int Height { get; private set; }
        public int Weight { get; private set; }
        public string Nationality { get; private set; }

        public Criminal(string name, bool isInPrison, int height, int weight, string nationality)
        {
            Name = name;
            IsInPrison = isInPrison;
            Height = height;
            Weight = weight;
            Nationality = nationality;
        }

        
    }
    

    
}
