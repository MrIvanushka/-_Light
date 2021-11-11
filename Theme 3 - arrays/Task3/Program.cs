using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            int[] array = new int[30];
            int maxValue = 30;
            Random random = new Random();
            Console.Write("Исходный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(maxValue);
                Console.Write(array[i] + " ");
            }
            Console.Write("\nЛокальные максимумы: ");
            
            if(array[0] > array[1])
                Console.Write(array[0] + " ");

            for (int i = 1; i < array.Length - 1; i++)
                if(array[i] > array[i - 1] && array[i] > array[i + 1])
                    Console.Write(array[i] + " ");

            if (array[array.Length - 1] > array[array.Length - 2])
                Console.Write(array[array.Length - 1]);
        }
        
    }
}
