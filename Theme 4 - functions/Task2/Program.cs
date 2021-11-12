using System;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            RenderBar(3, 3, 0.32f);
        }

        static void RenderBar(int coordX, int coordY, float showingValue, int barLength = 10)
        {
            if (showingValue > 1 || showingValue < 0)
                throw new System.Exception("Шкала не может показывать значение больше 1 или меньне нуля.");

            Console.SetCursorPosition(coordX, coordY);

            int coloredBarLength = (int)Math.Round(barLength * showingValue);
            Console.Write("[");

            for (int i = 0; i < barLength; i++)
            {
                if (i < coloredBarLength)
                    Console.Write("#");
                else
                    Console.Write("_");
            }
            Console.Write("]");
        }
    }
}
