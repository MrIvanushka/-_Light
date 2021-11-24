using System;
using System.Linq;
using System.Collections.Generic;

namespace ConsoleApp3
{
    class Program
    {

        static void Main(string[] args)
        {
            Prisoner[] prisoners = new Prisoner[] {
                new Prisoner("Джон", "Воровство"),
                new Prisoner("Иван", "Антиправительственное"),
                new Prisoner("Борис", "Убийство"),
                new Prisoner("Армен", "Антиправительственное")
            };
            Console.WriteLine("До амнистии: ");

            foreach (var prisoner in prisoners)
                Console.WriteLine(prisoner.Name);

            string annuledCrime = "Антиправительственное";
            var remainingPrisoners = prisoners.Where(prisoner => prisoner.Crime != annuledCrime);
            Console.WriteLine("После амнистии: ");

            foreach (var prisoner in remainingPrisoners)
                Console.WriteLine(prisoner.Name);
        }
    }

    class Prisoner
    {
        public string Name { get; private set; }
        public string Crime { get; private set; }

        public Prisoner(string name, string crime)
        {
            Name = name;
            Crime = crime;
        }
    }
    

    
}
