using System;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetInt());
        }

        static int GetInt()
        {
            string readValue = "";
            int exportValue;

            do
            {
                readValue = Console.ReadLine();
            }
            while (int.TryParse(readValue, out exportValue) == false);

            return exportValue;
        }
    }
}
