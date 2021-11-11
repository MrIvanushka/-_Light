using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            int[,] array = new int[5, 6];

            for (int i = 0; i < array.GetLength(0); i++)
                for (int j = 0; j < array.GetLength(1); j++)
                    array[i, j] = (i + 1) * (j + 1);

            int productOf1stColumn = 1;
            int sumOf2ndLine = 0;

            for (int i = 0; i < array.GetLength(0); i++)
                sumOf2ndLine += array[i, 2];

            for (int j = 0; j < array.GetLength(1); j++)
                productOf1stColumn *= array[1, j];

            Console.WriteLine("Исходный массив:");

            for (int j = 0; j < array.GetLength(1); j++)
            {
                for (int i = 0; i < array.GetLength(0); i++)
                    Console.Write(array[i, j] + " ");

                Console.Write('\n');
            }
            Console.WriteLine("Произведение элементов 1 столбца: " + productOf1stColumn);
            Console.WriteLine("Сумма элементов 2 строки: " + sumOf2ndLine);
        }
        
    }
}
