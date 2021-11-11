using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            int[,] array = new int[10, 10];
            int maxRandomValue = 30;
            int maxElement = int.MinValue;
            Random random = new Random();

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = random.Next(1, maxRandomValue);

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    if (array[i, j] > maxElement)
                        maxElement = array[i, j];

            Console.WriteLine("Наибольший элемент: " + maxElement);
            Console.WriteLine("Исходная матрица: ");

            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                    Console.Write(array[i, j] + " ");

                Console.Write('\n');
            }

            Console.WriteLine("Изменённая матрица: ");

            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    if (array[i, j] == maxElement)
                        array[i, j] = 0;

                    Console.Write(array[i, j] + " ");
                }
                Console.Write('\n');
            }
            Console.Read();
        }

    }
}
