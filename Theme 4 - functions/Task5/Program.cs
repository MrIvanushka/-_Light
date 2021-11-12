using System;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] array = new int[20];
            Random random = new Random();
            Console.WriteLine("Исходный массив: ");

            for (int i = 0; i < array.Length; i++)
            {
                array[i] = random.Next(1000);
                Console.Write(array[i] + " ");
            }

            Shuffle(array);

            Console.WriteLine("\nПеремешанный массив: ");
            for (int i = 0; i < array.Length; i++)
            {
                Console.Write(array[i] + " ");
            }
        }

        static void Shuffle(int[] array)
        {
            Random random = new Random();

            for (int i = 0; i < array.Length; i++)
            {
                int randomIndex = 0;

                do
                    randomIndex = random.Next(array.Length);
                while (randomIndex == i);

                Swap(ref array[i], ref array[randomIndex]);
            }   
        }

        static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }
}
