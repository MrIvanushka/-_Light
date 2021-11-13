using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            string command = "";

            do
            {
                command = Console.ReadLine();
                int newElement;
                    
                if(int.TryParse(command, out newElement))
                {
                    list.Add(newElement);
                }
                else
                {
                    if (command == "sum")
                    {
                        int sum = 0;
                        list.ForEach(element => sum += element);
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
