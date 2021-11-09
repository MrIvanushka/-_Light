using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            float rubblesToDollarsCurrency = 70f;
            float dollarsToRubblesCurrency = 0.015f;
            float rubblesToEurosCurrency = 80f;
            float eurosToRubblesCurrency = 0.01f;
            Console.Write("Введите количество рублей: ");
            float rubbles = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество долларов: ");
            float dollars = Convert.ToInt32(Console.ReadLine());
            Console.Write("Введите количество евро: ");
            float euros = Convert.ToInt32(Console.ReadLine());

            try
            {
                do
                {
                    Console.WriteLine("Для окончания работы программы напишите любой символ.");
                    Console.WriteLine("Сколько долларов хотите купить на рубли? ");
                    Change(ref dollars, ref rubbles, rubblesToDollarsCurrency);
                    Console.WriteLine("Сколько евро хотите купить на рубли? ");
                    Change(ref euros, ref rubbles, rubblesToEurosCurrency);
                    Console.WriteLine("Сколько рублей хотите купить на доллары? ");
                    Change(ref rubbles, ref dollars, dollarsToRubblesCurrency);
                    Console.WriteLine("Сколько рублей хотите купить на евро? ");
                    Change(ref rubbles, ref euros, eurosToRubblesCurrency);
                }
                while (true);
            }
            catch(System.FormatException)
            { }
            Console.WriteLine($"Осталось рублей: {rubbles}");
            Console.WriteLine($"Осталось долларов: {dollars}");
            Console.WriteLine($"Осталось евро: {euros}");
            Console.Read();
        }
        static void Change(ref float product, ref float money, float price)
        {
            float buyValue = Convert.ToSingle(Console.ReadLine());
            if (money > buyValue * price)
            {
                product += buyValue;
                money -= buyValue * price;
            }
            else
                Console.WriteLine("У вас недостаточно средств.");
        }
    }
}
