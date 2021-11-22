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
        private CellStatus[,] _mapToRead;
        private int _emptyCellCount;

        public CellStatus[,] GetBattleMap()
        {
            SyncronizeMapCollections();
            return _mapToRead;
        }

        public BattleGround(int sizeX, int sizeY)
        {
            do
            {
                _emptyCellCount = 0;
                GenerateMap(sizeX, sizeY);
            }
            while (_emptyCellCount < 0.5f * sizeX * sizeY);
            _mapToRead = new CellStatus[sizeX, sizeY];

            RenderScene();
        }

        private void SyncronizeMapCollections()
        {
            for (int i = 0; i < _map.GetLength(0); i++)
                for (int j = 0; j < _map.GetLength(1); j++)
                    _mapToRead[i, j] = _map[i, j];
        }

        private void GenerateMap(int sizeX = 50, int sizeY = 30)
        {
            _map = new CellStatus[sizeX, sizeY];
            Random random = new Random();
            int endWallThickness = 2;
            int randomFactor = 2;

            for (int x = endWallThickness; x < sizeX - endWallThickness; x++)
                for (int y = endWallThickness; y < sizeY - endWallThickness; y++)
                {
                    int randomValue = random.Next(randomFactor);

                    if (randomValue > 0)
                    {
                        _map[x, y] = CellStatus.Empty;
                        _emptyCellCount++;
                    }
                }

            UseCellAutomat(_map);
        }

        private void UseCellAutomat(CellStatus[,] map)
        {
            int minimalNeighboursToSurvive = 4;


            for (int x = 1; x < map.GetLength(0) - 1; x++)
                for (int y = 1; y < map.GetLength(1) - 1; y++)
                {
                    int neighbourCount = ScoreNeighbours(map, x, y);

                    if (neighbourCount > minimalNeighboursToSurvive)
                    {
                        map[x, y] = CellStatus.Empty;
                        _emptyCellCount++;
                    }
                    else if (neighbourCount < minimalNeighboursToSurvive)
                    {
                        map[x, y] = CellStatus.Wall;
                        _emptyCellCount--;
                    }
                }
        }
        private int ScoreNeighbours(CellStatus[,] map, int coordX, int coordY)
        {
            int neighbourCount = 0;

            for (int i = coordX - 1; i < coordX + 2; i++)
                for (int j = coordY - 1; j < coordY + 2; j++)
                    if ((i != coordX || j != coordY)&&map[i, j] == CellStatus.Empty)
                        neighbourCount += 1;

            return neighbourCount;
        }


        private void RenderScene()
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
                }
                Console.Write('\n');
            }
        }

        public void ClearCell(Vector2 position)
        {
            _map[position.X,position.Y] = CellStatus.Empty;
            _mapToRead[position.X, position.Y] = CellStatus.Empty;
            Console.SetCursorPosition(position.X, position.Y);
            Console.Write(' ');
        }

        public void RerenderCharacter(SoldierMoving soldier, CellStatus soldierStatus, Vector2 nextPosition)
        {
            _map[soldier.Position.X, soldier.Position.Y] = CellStatus.Empty;
            _mapToRead[soldier.Position.X, soldier.Position.Y] = CellStatus.Empty;
            _map[nextPosition.X, nextPosition.Y] = soldierStatus;
            _mapToRead[nextPosition.X, nextPosition.Y] = soldierStatus;
            Console.SetCursorPosition(nextPosition.X, nextPosition.Y);
            Console.ForegroundColor = soldier.SelfColor;
            Console.Write(soldier.Icon);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public Army[] CreateArmies(int soilderCount, int rankLength)
        {
            Army[] armies = new Army[2];
            List<Vector2> blueArmyPositions = new List<Vector2>();
            List<Vector2> yellowArmyPositions = new List<Vector2>();
            int rankCount = soilderCount / rankLength;
            int armyLeftOffsetX = (_map.GetLength(0) - rankLength) / 2;
            int armyRightOffsetX = armyLeftOffsetX + rankLength;
            int armyOffsetY = 3;

            for (int x = armyLeftOffsetX; x <= armyRightOffsetX; x++)
            {
                for (int y = armyOffsetY; y <= armyOffsetY + rankCount; y++)
                {
                    blueArmyPositions.Add(new Vector2(x, y));
                    _map[x, y] = CellStatus.BlueArmy;

                    yellowArmyPositions.Add(new Vector2(x, _map.GetLength(1) - y));
                    _map[x, y] = CellStatus.YellowArmy;
                }

            }
            Console.SetCursorPosition(0, 50);
            armies[0] = new Army(this, "Жёлтая армия",ConsoleColor.Yellow, yellowArmyPositions, CellStatus.YellowArmy);
            armies[1] = new Army(this, "Синяя армия", ConsoleColor.Blue, blueArmyPositions, CellStatus.BlueArmy);
            return armies;
        }
       
    }
}

