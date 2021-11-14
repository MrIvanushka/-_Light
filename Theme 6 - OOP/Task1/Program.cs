using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player("Steve");
            Console.WriteLine("Player1: " + player1);
            Console.WriteLine("Player2: " + player2);
        }
    }


    class Player
    {
        private string _name;
        private float _hp;
        private float _maxHP = 100;

        public Player() : this("Username")
        { }

        public Player(string name)
        {
            _name = name;
            _hp = _maxHP;
        }

        public override string ToString()
        {
            return $"[{_name}: {_hp} HP.]";
        }
    }


}
