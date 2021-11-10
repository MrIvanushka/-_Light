using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            uint picturesInAlbum = 52;
            uint rowCapacity = 3;
            uint rowCount = picturesInAlbum / rowCapacity;
            uint freePicturesCount = picturesInAlbum % rowCapacity;
            Console.WriteLine($"Количество рядов: {rowCount}");
            Console.WriteLine($"Картинок сверх меры: {freePicturesCount}");
        }
    }
}
