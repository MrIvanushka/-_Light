using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    class Soldier
    {
        private Army _selfArmy;
        public CellStatus SelfStatus { get; private set; }
        public CellStatus TargetStatus { get; private set; }
        public SoldierCombat Combat { get; private set; }
        public SoldierMoving Graphics { get; private set; }
        public bool IsAlive { get; private set; }

        public Soldier(Army selfArmy, int soilderClass, Vector2 position, ConsoleColor color, CellStatus status)
        {
            char icon = 'S';
            switch (soilderClass)
            {
                case 0:
                    Combat = new WarriorCombat(this, 120, 10);
                    icon = 'W';
                    break;
                case 1:
                    Combat = new WizardCombat(this, 40, 20);
                    icon = 'M';
                    break;
                case 2:
                    Combat = new WarlockCombat(this, 60, 100);
                    icon = 'L';
                    break;
                case 3:
                    Combat = new PristCombat(this, 50, 10);
                    icon = 'P';
                    break;
                case 4:
                    Combat = new FrostMageCombat(this, 60, 15);
                    icon = 'F';
                    break;
            }
            SelfStatus = status;

            TargetStatus = CellStatus.BlueArmy;

            if (TargetStatus == SelfStatus)
                TargetStatus = CellStatus.YellowArmy;

            Graphics = new SoldierMoving(this, icon, position, color);
            _selfArmy = selfArmy;
        }

        public List<Soldier> GetNeighbourAllies(CellStatus[,] map)
        {
            return _selfArmy.GetNeighbourAllies(map, Graphics.Position);
        }
    }
}
