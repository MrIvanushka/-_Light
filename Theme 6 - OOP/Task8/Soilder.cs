using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Soilder
    {
        private Army _selfArmy;
        public SoilderCombat Combat { get; private set; }
        public SoilderMoving Graphics { get; private set; }
        public bool IsAlive { get; private set; }

        public Soilder(Army selfArmy, int soilderClass, Coord2 position, ConsoleColor color, CellStatus status)
        {
            char icon = 'S';
            switch (soilderClass)
            {
                case 0:
                    Combat = new Warrior(this, 120, 10);
                    icon = 'W';
                    break;
                case 1:
                    Combat = new Wizard(this, 40, 20);
                    icon = 'M';
                    break;
                case 2:
                    Combat = new Warlock(this, 60, 100);
                    icon = 'L';
                    break;
                case 3:
                    Combat = new Prist(this, 50, 10);
                    icon = 'P';
                    break;
                case 4:
                    Combat = new FrostMage(this, 60, 15);
                    icon = 'F';
                    break;
            }
            Graphics = new SoilderMoving(icon, position, color, status);
            _selfArmy = selfArmy;
        }

        public List<Soilder> GetNeighbourAllies(CellStatus[,] map)
        {
            Coord2 position = Graphics.Position;
            List<Soilder> neighbourAllies = new List<Soilder>();

            _selfArmy.SoilderPool.ForEach(soilder => {
                int deltaPosX = Math.Abs(soilder.Graphics.Position.X - Graphics.Position.X);
                int deltaPosY = Math.Abs(soilder.Graphics.Position.Y - Graphics.Position.Y);
                if (deltaPosX <= 1 && deltaPosY <= 1)
                    neighbourAllies.Add(soilder);
            });

            return neighbourAllies;
        }
    }
}
