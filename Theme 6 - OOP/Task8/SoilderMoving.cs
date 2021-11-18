using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class SoilderMoving
    {
        

        public char Icon { get; private set; }
        public ConsoleColor SelfColor { get; private set; }
        public CellStatus SelfStatus { get; private set; }
        public CellStatus TargetStatus { get; private set; }
        public Coord2 Position { get; private set; }

        public SoilderMoving(char icon, Coord2 position, ConsoleColor selfColor, CellStatus status)
        {
            Icon = icon;
            Position = position;
            SelfColor = selfColor;
            SelfStatus = status;
            TargetStatus = CellStatus.BlueArmy;
            
            if (TargetStatus == SelfStatus)
                TargetStatus = CellStatus.YellowArmy;

        }

        public void DisableIcon(BattleGround battleGround)
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(' ');
        }

        public void Move(BattleGround battleGround)
        {
            Coord2 nextPosition = GeneratePath(battleGround.Map);
            battleGround.RerenderCharacter(this, nextPosition);
            Position = nextPosition;
        }

        private Coord2 GeneratePath(CellStatus[,] map)
        {
            CellStatus targetStatus = CellStatus.BlueArmy;

            if (targetStatus == SelfStatus)
                targetStatus = CellStatus.YellowArmy;

            bool[,] used = new bool[map.GetLength(0), map.GetLength(1)];
            Coord2[,] parent = new Coord2[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    used[i, j] = false;

            used[Position.X, Position.Y] = true;
            Queue<Coord2> coordPool = new Queue<Coord2>();
            coordPool.Enqueue(Position);
            Coord2 startPosition = new Coord2(0, 0);
            bool enemyIsFound = false;

            while (coordPool.Count > 0)
            {
                startPosition = coordPool.Dequeue();

                for (int x = startPosition.X - 1; x <= startPosition.X + 1; x++)
                {
                    for (int y = startPosition.Y - 1; y <= startPosition.Y + 1; y++)
                    {
                        if (x != startPosition.X || y != startPosition.Y)
                        {
                            if (used[x, y] == false && map[x, y] == CellStatus.Empty)
                            {
                                coordPool.Enqueue(new Coord2(x, y));
                                used[x, y] = true;
                                parent[x, y] = startPosition;
                            }
                            else if (map[x, y] == TargetStatus)
                            {
                                enemyIsFound = true;
                                break;
                            }
                        }
                    }
                    if (enemyIsFound)
                        break;
                }
                if (enemyIsFound)
                    break;
            }


            if (enemyIsFound == false || parent[startPosition.X, startPosition.Y] == new Coord2(0, 0))
               return Position;

            while(parent[startPosition.X, startPosition.Y] != Position)
            {
                startPosition = parent[startPosition.X, startPosition.Y];
            }

            return startPosition;
        }
    }
}
