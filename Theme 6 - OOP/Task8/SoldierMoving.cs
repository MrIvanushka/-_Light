using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class SoldierMoving
    {
        private Soldier _thisSoldier;
        public char Icon { get; private set; }
        public ConsoleColor SelfColor { get; private set; }
        public Vector2 Position { get; private set; }

        public SoldierMoving(Soldier thisSoldier, char icon, Vector2 position, ConsoleColor selfColor)
        {
            Icon = icon;
            Position = position;
            SelfColor = selfColor;
            _thisSoldier = thisSoldier;
        }

        public void DisableIcon(BattleGround battleGround)
        {
            Console.SetCursorPosition(Position.X, Position.Y);
            Console.Write(' ');
        }

        public void Move(BattleGround battleGround, in CellStatus[,] battleMap)
        {
            Vector2 nextPosition = GeneratePath(in battleMap);
            battleGround.RerenderCharacter(this, _thisSoldier.SelfStatus, nextPosition);
            Position = nextPosition;
        }

        private Vector2 GeneratePath(in CellStatus[,] map)
        {
            CellStatus targetStatus = CellStatus.BlueArmy;

            if (targetStatus == _thisSoldier.SelfStatus)
                targetStatus = CellStatus.YellowArmy;

            bool[,] used = new bool[map.GetLength(0), map.GetLength(1)];
            Vector2[,] parent = new Vector2[map.GetLength(0), map.GetLength(1)];

            for (int i = 0; i < map.GetLength(0); i++)
                for (int j = 0; j < map.GetLength(1); j++)
                    used[i, j] = false;

            used[Position.X, Position.Y] = true;
            Queue<Vector2> coordPool = new Queue<Vector2>();
            coordPool.Enqueue(Position);
            Vector2 startPosition = new Vector2(0, 0);
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
                                coordPool.Enqueue(new Vector2(x, y));
                                used[x, y] = true;
                                parent[x, y] = startPosition;
                            }
                            else if (map[x, y] == _thisSoldier.TargetStatus)
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


            if (enemyIsFound == false || parent[startPosition.X, startPosition.Y] == new Vector2(0, 0))
               return Position;

            while(parent[startPosition.X, startPosition.Y] != Position)
            {
                startPosition = parent[startPosition.X, startPosition.Y];
            }

            return startPosition;
        }
    }
}
