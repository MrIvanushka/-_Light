using System;

namespace ConsoleApp3
{
    class Program
    {
        static void Main(string[] args)
        {
            uint picturesInAlbum = 52;
            uint setCapacity = 3;
            uint setCount = picturesInAlbum / setCapacity;
            uint freePicturesCount = picturesInAlbum % setCapacity;
            Console.WriteLine($"Количество рядов: {setCount}");
            Console.WriteLine($"Картинок сверх меры: {freePicturesCount}");
        }
    }
}
