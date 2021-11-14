using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Renderer renderer = new Renderer();
            Player player1 = new Player();
            Player player2 = new Player(3, 3, '#');
            renderer.Draw(player1);
            renderer.Draw(player2);
            Console.Read();
        }
    }

    class Renderer
    {
        public void Draw(Player player)
        {
            Console.SetCursorPosition(player.X, player.Y);
            Console.Write(player.Icon);
        }
    }

    class Player
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public char Icon { get; private set; }

        public Player() : this(0, 0)
        { }

        public Player(int x, int y) : this(x, y, '@')
        { }

        public Player(int x, int y, char icon)
        {
            X = x;
            Y = y;
            Icon = icon;
        }        
    }


}
