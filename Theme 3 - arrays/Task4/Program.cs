using System;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            uint size = 0;
            int[] array = new int[0];
            string command = "";

            do
            {
                command = Console.ReadLine();
                
                try
                {
                    int newElement = Convert.ToInt32(command);
                    int[] biggerArray = new int[size + 1];
                    
                    for (int i = 0; i < size; i++)
                        biggerArray[i] = array[i];

                    array = biggerArray;
                    array[size] = newElement;
                    size++;
                }
                catch(FormatException)
                {
                    if (command == "sum")
                    {
                        int sum = 0;

                        for (int i = 0; i < size; i++)
                            sum += array[i];

                        Console.WriteLine(sum);
                    }
                    else if (command != "exit")
                    {
                        Console.WriteLine("Неизвестная команда.");
                    }
                }

            }
            while (command != "exit");
        }
        
    }
}
