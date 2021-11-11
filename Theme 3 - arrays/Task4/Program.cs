using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            uint size = 0;
            uint capacity = 2;
            int[] array = new int[capacity];
            int sum = 0;
            string command = "";

            do
            {
                command = Console.ReadLine();
                
                try
                {
                    int newElement = Convert.ToInt32(command);
                    
                    if(size == capacity)
                    {
                        int[] biggerArray = new int[2 * capacity];

                        for (int i = 0; i < capacity; i++)
                            biggerArray[i] = array[i];

                        array = biggerArray;
                        capacity *= 2;
                    }
                    array[size] = newElement;
                    sum += newElement;
                    size++;
                }
                catch(FormatException)
                {
                    if (command == "sum")
                        Console.WriteLine(sum);
                    else if (command != "exit")
                        Console.WriteLine("Неизвестная команда.");
                }

            }
            while (command != "exit");
        }
        
    }
}
