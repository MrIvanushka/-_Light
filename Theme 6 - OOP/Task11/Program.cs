﻿using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            CarService service = new CarService();
            Random random = new Random();

            do
            {
                service.RepairVehicle(new Client((Breakdown)random.Next(3), random.Next(20, 50)));
            }
            while (service.Enabled);
        }
    }
    
        enum Breakdown
        { 
            BrakeFailure, 
            BrokenHeadlight, 
            Scratch 
        }

        class CarService
        {
            private readonly ReadOnlyDictionary<Breakdown, string> _breakdownDefinitions;
            private readonly ReadOnlyDictionary<string, DetailKit> _allDetailKits;

            private readonly int _missingDetailFine;
            private readonly int _badWorkFine;
            private int _money;

            public bool Enabled { get; private set; }

            public CarService(int money = 100, int missingDetailFine = 10, int badWorkFine = 20)
            {
                Dictionary<Breakdown, string> breakdownDefinitions = new Dictionary<Breakdown, string>();
                breakdownDefinitions[Breakdown.BrakeFailure] = "Поломка тормозов";
                breakdownDefinitions[Breakdown.BrokenHeadlight] = "Сломана фара";
                breakdownDefinitions[Breakdown.Scratch] = "Царапина";
                _breakdownDefinitions = new ReadOnlyDictionary<Breakdown, string>(breakdownDefinitions);

                Dictionary<string, DetailKit> detailNames = new Dictionary<string, DetailKit>();
                detailNames["Колодки"] = new DetailKit(Breakdown.BrakeFailure, 3);
                detailNames["Фары"] = new DetailKit(Breakdown.BrokenHeadlight, 3);
                detailNames["Шпаклёвка"] = new DetailKit(Breakdown.Scratch, 3);
                _allDetailKits = new ReadOnlyDictionary<string, DetailKit>(detailNames);

                _money = money;
                _missingDetailFine = missingDetailFine;
                _badWorkFine = badWorkFine;
                Enabled = true;
            }

            public void RepairVehicle(Client client)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Поломка: " + _breakdownDefinitions[client.CurrentBreakdown]);
                Console.WriteLine("Ящики с деталями: ");

                foreach (var pair in _allDetailKits)
                    Console.Write(pair.Key + " ");

                Console.WriteLine("\nДенег в кассе: " + _money);
                bool workIsDone = false;

                do
                {
                    Console.Write("Выберите деталь для починки: ");
                    string fixingDetailName = Console.ReadLine();

                    if (_allDetailKits.ContainsKey(fixingDetailName) == false)
                    {
                        if (fixingDetailName == "Refuse")
                        {
                            Console.WriteLine("Клиенту отказано");
                            PayFine(_missingDetailFine);
                            workIsDone = true;
                        }
                        else if (fixingDetailName == "Exit")
                        {
                            Enabled = false;
                            workIsDone = true;
                        }
                        else
                        {
                            PrintErrorMessage("Таких деталей на складе нет.");
                        }
                    }
                    else if(_allDetailKits[fixingDetailName].HasDetails == false)
                    {
                        PrintErrorMessage("Подобные детали закончились.");
                    }
                    else if (_allDetailKits[fixingDetailName].Purpose  != client.CurrentBreakdown)
                    {
                        _allDetailKits[fixingDetailName].TakeDetail();
                        Console.WriteLine("Выбрана неверная деталь");
                        PayFine(_badWorkFine);
                        workIsDone = true;
                    }
                    else
                    {
                        _allDetailKits[fixingDetailName].TakeDetail();
                        Console.WriteLine("Машина починена");
                        workIsDone = true;
                        _money += client.Price;
                    }
                }
                while (workIsDone == false);

                WaitForTap();
            }

            private void WaitForTap()
            {
                Console.WriteLine("Нажмите любую клавишу...");
                Console.ReadKey();
            }

            private void PayFine(int fine)
            {
                if (_money < _missingDetailFine)
                {
                    Console.WriteLine("Деньги закончились");
                    Enabled = false;
                }
                else
                {
                    _money -= fine;
                    Console.WriteLine("Штраф оплачен. Денег осталось: " + _money);
                }
            }

            private void PrintErrorMessage(string message)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка: " + message);
                Console.WriteLine("Напишите Refuse, чтобы отказать клиенту. \n" +
                    "Напишите Exit для выхода");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        class DetailKit
        {
            public readonly Breakdown Purpose;
            private int _сount;

            public bool HasDetails => _сount > 0;

            public DetailKit(Breakdown purpose, int count)
            {
                Purpose = purpose;
                _сount = count;
            }

            public void TakeDetail()
            {
                if (_сount > 0)
                    _сount--;
                else
                    throw new Exception("Деталей не осталось");
            }
        }

        class Client
        {
            public Breakdown CurrentBreakdown { get; private set; }
            public int Price { get; private set; }

            public Client(Breakdown breakdown, int price)
            {
                CurrentBreakdown = breakdown;
                Price = price;
            }
        }
    

    
}
