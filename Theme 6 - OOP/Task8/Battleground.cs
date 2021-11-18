using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp4
{
    enum CellStatus
        {
            Wall, Empty, BlueArmy, YellowArmy
        }

    class BattleGround
    {
        private CellStatus[,] _map;

        public CellStatus[,] Map => _map;

        public BattleGround(int sizeX, int sizeY)
        {
            GenerateMap(sizeX, sizeY);
            RenderScene();
        }

        private void GenerateMap(int sizeX = 50, int sizeY = 30)
        {
            _map = new CellStatus[sizeX, sizeY];
            Random random = new Random();
            int endWallThickness = 2;
            int randomFactor = 6;

            for (int x = endWallThickness; x < sizeX - endWallThickness; x++)
                for (int y = endWallThickness; y < sizeY - endWallThickness; y++)
                {
                    int randomValue = random.Next(randomFactor);

                    if (randomValue > 0)
                        _map[x, y] = CellStatus.Empty;
                }
        }

        public void RenderScene()
        {
            Console.Clear();

            for (int y = 0; y < _map.GetLength(1); y++)
            {
                for (int x = 0; x < _map.GetLength(0); x++)
                {
                    if (_map[x, y] == CellStatus.Empty)
                        Console.Write(' ');
                    else if (_map[x, y] == CellStatus.Wall)
                        Console.Write('#');
                    else if (_map[x, y] == CellStatus.BlueArmy)
                        Console.Write('B');
                    else if (_map[x, y] == CellStatus.YellowArmy)
                        Console.Write('Y');
                }
                Console.Write('\n');
            }
        }

        public void ClearCell(Coord2 position)
        {
            Map[position.X,position.Y] = CellStatus.Empty;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(' ');
        }

        public void RerenderCharacter(SoilderMoving soilder, Coord2 nextPosition)
        {
            Map[soilder.Position.X, soilder.Position.Y] = CellStatus.Empty;
            Map[nextPosition.X, nextPosition.Y] = soilder.SelfStatus;
            Console.SetCursorPosition(nextPosition.X, nextPosition.Y);
            Console.ForegroundColor = soilder.SelfColor;
            Console.Write(soilder.Icon);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Army[] CreateArmies(int soilderCount, int rankLength)
        {
            Army[] armies = new Army[2];
            List<Coord2> blueArmyPositions = new List<Coord2>();
            List<Coord2> yellowArmyPositions = new List<Coord2>();
            int rankCount = soilderCount / rankLength;
            int armyLeftOffsetX = (_map.GetLength(0) - rankLength) / 2;
            int armyRightOffsetX = armyLeftOffsetX + rankLength;
            int armyOffsetY = 3;

            for (int x = armyLeftOffsetX; x <= armyRightOffsetX; x++)
            {
                for (int y = armyOffsetY; y <= armyOffsetY + rankCount; y++)
                {
                    blueArmyPositions.Add(new Coord2(x, y));
                    Map[x, y] = CellStatus.BlueArmy;

                    yellowArmyPositions.Add(new Coord2(x, _map.GetLength(1) - y));
                    Map[x, y] = CellStatus.YellowArmy;
                }

            }
            Console.SetCursorPosition(0, 50);
            armies[0] = new Army(this, "Жёлтая армия",ConsoleColor.Yellow, yellowArmyPositions, CellStatus.YellowArmy);
            armies[1] = new Army(this, "Синяя армия", ConsoleColor.Blue, blueArmyPositions, CellStatus.BlueArmy);
            return armies;
        }
       
    }

    struct Coord2
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Coord2(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Coord2 operator +(Coord2 a, Coord2 b)
        {
            return new Coord2(a.X + b.X, a.Y + b.Y);
        }

        public static bool operator ==(Coord2 a, Coord2 b)
        {
            return a.X == b.X && a.Y == b.Y;
        }

        public static bool operator !=(Coord2 a, Coord2 b)
        {
            return a.X != b.X || a.Y != b.Y;
        }
    }
}

