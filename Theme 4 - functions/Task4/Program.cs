using System;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] map = GenerateMap();

            int playerCoordX = map.GetLength(0) / 2;
            int playerCoordY = map.GetLength(1) / 2;
            map[playerCoordX, playerCoordY] = 1;
            ConsoleKeyInfo readKey;
            RenderScene(map);
            DrawPlayer(playerCoordX, playerCoordY);

            do
            {
                readKey = Console.ReadKey();

                switch(readKey.Key)
                {
                    case ConsoleKey.UpArrow:
                        MovePlayer(map, ref playerCoordX, ref playerCoordY, 0, -1);
                        break;
                    case ConsoleKey.LeftArrow:
                        MovePlayer(map, ref playerCoordX, ref playerCoordY, -1, 0);
                        break;
                    case ConsoleKey.DownArrow:
                        MovePlayer(map, ref playerCoordX, ref playerCoordY, 0, 1);
                        break;
                    case ConsoleKey.RightArrow:
                        MovePlayer(map, ref playerCoordX, ref playerCoordY, 1, 0);
                        break;
                }
            }
            while (readKey.Key != ConsoleKey.Escape);

            
        }

        static int[,] GenerateMap(int sizeX = 50, int sizeY = 30)
        {
           int[,] map = new int[sizeX, sizeY];
            Random random = new Random();
            int endWallThickness = 2;
            int randomFactor = 6;

            for (int x = endWallThickness; x < sizeX - endWallThickness; x++)
                for (int y = endWallThickness; y < sizeY - endWallThickness; y++)
                    map[x, y] = random.Next(randomFactor);

            UseCellAutomat(map);

            return map;
        }

        static void UseCellAutomat(int[,] map)
        {
            int minimalNeighboursToSurvive = 4;


            for(int x = 1; x < map.GetLength(0) - 1; x++)
                for (int y = 1; y < map.GetLength(1) - 1; y++)
                {
                    int neighbourCount = ScoreNeighbours(map, x, y);

                    if (neighbourCount > minimalNeighboursToSurvive)
                        map[x, y] = 1;
                    else if(neighbourCount < minimalNeighboursToSurvive)
                        map[x, y] = 0;
                }
        }

        static int ScoreNeighbours(int[,] map, int coordX, int coordY)
        {
            int neighbourCount = 0;

            for (int i = coordX - 1; i < coordX + 2; i++)
                for (int j = coordY - 1; j < coordY + 2; j++)
                    if (i != coordX && j != coordY)
                        neighbourCount += map[i, j];

            return neighbourCount;
        }

        static void RenderScene(int[,] map)
        {
            Console.Clear();

            for (int y = 0; y < map.GetLength(1); y++)
            {
                for (int x = 0; x < map.GetLength(0); x++)
                {
                    if (map[x, y] == 1)
                        Console.Write(' ');
                    else
                        Console.Write('#');
                }
                Console.Write('\n');
            }
        }

        static void MovePlayer(int[,] map, ref int playerX, ref int playerY, int dx, int dy)
        {
            if (map[playerX + dx, playerY + dy] == 1)
            {
                Console.SetCursorPosition(playerX, playerY);
                Console.Write(' ');

                playerX += dx;
                playerY += dy;

                DrawPlayer(playerX, playerY);
            }
        }

        static void DrawPlayer(int playerX, int playerY)
        {
            Console.SetCursorPosition(playerX, playerY);
            Console.Write('P');
        }
    }
}
