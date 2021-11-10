using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            float rublesToDollarsPrice = 70f;
            float dollarsToRublesPrice = 0.015f;
            float rublesToEurosPrice = 80f;
            float eurosToRublesPrice = 0.01f;
            Console.Write("Введите количество рублей: ");
            float rubles = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество долларов: ");
            float dollars = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество евро: ");
            float euros = Convert.ToInt32(Console.ReadLine());

            bool timeToExit = false;
            do
            {
                Console.Write("Для выхода напишите exit.");
                Console.Write("Введите валюту оплаты (rubles, dollars, euros): ");
                switch(Console.ReadLine())
                {
                    case "rubles":
                        Console.Write("Введите валюту покупки (dollars, euros): ");
                        switch (Console.ReadLine())
                        {
                            case "rubles":
                                Console.WriteLine("Рубли на рубли не обмениваем.");
                                break;
                            case "dollars":
                                Console.Write("Сколько долларов хотите купить? ");
                                float buyDollarsValue = Convert.ToSingle(Console.ReadLine());
                                if (rubles > buyDollarsValue * rublesToDollarsPrice)
                                {
                                    dollars += buyDollarsValue;
                                    rubles -= buyDollarsValue * rublesToDollarsPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "euros":
                                Console.Write("Сколько евро хотите купить? ");
                                float buyEurosValue = Convert.ToSingle(Console.ReadLine());
                                if (rubles > buyEurosValue * rublesToEurosPrice)
                                {
                                    euros += buyEurosValue;
                                    rubles -= buyEurosValue * rublesToEurosPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "exit":
                                timeToExit = true;
                                break;
                            default:
                                Console.WriteLine("Данную валюту не обмениваем.");
                                break;
                        }
                        break;

                    case "dollars":
                        Console.Write("Введите валюту покупки (rubles, euros): ");
                        switch (Console.ReadLine())
                        {
                            case "dollars":
                                Console.WriteLine("Доллары на доллары не обмениваем.");
                                break;
                            case "rubles":
                                Console.Write("Сколько рублей хотите купить? ");
                                float buyRublesValue = Convert.ToSingle(Console.ReadLine());
                                if (dollars > buyRublesValue * dollarsToRublesPrice)
                                {
                                    rubles += buyRublesValue;
                                    dollars -= buyRublesValue * dollarsToRublesPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "euros":
                                Console.Write("Сколько евро хотите купить? ");
                                float buyEurosValue = Convert.ToSingle(Console.ReadLine());
                                if (dollars > buyEurosValue * dollarsToRublesPrice * rublesToEurosPrice)
                                {
                                    euros += buyEurosValue;
                                    dollars -= buyEurosValue * dollarsToRublesPrice * rublesToEurosPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "exit":
                                timeToExit = true;
                                break;
                            default:
                                Console.WriteLine("Данную валюту не обмениваем.");
                                break;
                        }
                        break;

                    case "euros":
                        Console.Write("Введите валюту покупки (rubles, dollars): ");
                        switch (Console.ReadLine())
                        {
                            case "euros":
                                Console.WriteLine("Евро на евро не обмениваем.");
                                break;
                            case "rubles":
                                Console.Write("Сколько рублей хотите купить? ");
                                float buyRublesValue = Convert.ToSingle(Console.ReadLine());
                                if (euros > buyRublesValue * eurosToRublesPrice)
                                {
                                    rubles += buyRublesValue;
                                    euros -= buyRublesValue * eurosToRublesPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "dollars":
                                Console.Write("Сколько долларов хотите купить? ");
                                float buyDollarsValue = Convert.ToSingle(Console.ReadLine());
                                if (dollars > buyDollarsValue * rublesToDollarsPrice * eurosToRublesPrice)
                                {
                                    dollars += buyDollarsValue;
                                    euros -= buyDollarsValue * rublesToDollarsPrice * eurosToRublesPrice;
                                }
                                else
                                    Console.WriteLine("У вас недостаточно средств.");
                                break;
                            case "exit":
                                timeToExit = true;
                                break;
                            default:
                                Console.WriteLine("Данную валюту не обмениваем.");
                                break;
                        }
                        break;
                    case "exit":
                        timeToExit = true;
                        break;
                    default:
                        Console.WriteLine("Данную валюту не обмениваем.");
                        break;
                }
            }
            while (timeToExit == false);

            Console.WriteLine($"Осталось рублей: {rubles}");
            Console.WriteLine($"Осталось долларов: {dollars}");
            Console.WriteLine($"Осталось евро: {euros}");
            Console.Read();
        }
    }
}
