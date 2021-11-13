using System;
using System.Collections.Generic;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, string> thesaurus = new Dictionary<string, string>();
            thesaurus.Add("Массив", "Тип или структура данных в виде набора компонентов, расположенных в памяти непосредственно друг за другом.");
            thesaurus.Add("Список", "Тип или структура данных, представляющая собой упорядоченный набор значений. " +
                "\nВ отличие от массива, порядок определяется указателем на каждый объект.");
            string command = "";

            do
            {
                command = Console.ReadLine();

                if (thesaurus.ContainsKey(command))
                    Console.WriteLine(thesaurus[command]);
                else if(command != "exit")
                    Console.WriteLine("Данное слово отсутствует в словаре.");
            }
            while (command != "exit");

        }
    }
}
