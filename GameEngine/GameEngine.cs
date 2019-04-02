using GameEngine.Interfaces;
using System;

namespace GameEngine
{
    public class GameEngine: IGameEngine
    {
        public int[,] FirstArray { get; set; }

        public int[,] SecondArray { get; set; }
        private bool ThreeOrTwoAliveNeighbors(int neighbours)
        {
            return (neighbours == 2 || neighbours == 3) ? true : false;
        }
        private bool ThreeAliveNeighbours(int neighbours)
        {
            return (neighbours == 3) ? true : false;
        }

        private int CountNeighbours(int[,] arr, int x, int y)
        {
            var arraySize = arr.GetLength(0);
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var row = (i + x + arraySize) % arraySize;
                    var col = (j + y + arraySize) % arraySize;
                    sum += arr[row, col];
                }
            }
            sum -= arr[x, y];

            return sum;
        }
        public bool WillCellSurvive(int[,] arr, int x, int y)
        {
            int neighbours = CountNeighbours(arr, x, y);

            int valueOfThis = arr[x, y];

            if (valueOfThis == 1)
            {
                return (ThreeOrTwoAliveNeighbors(neighbours)) ? true : false;
            }
            else
            {
                return (ThreeAliveNeighbours(neighbours)) ? true : false;
            }
        }

        public int[,] CreateArray(int arraySize)
        {
            return new int[arraySize, arraySize];
        }

        public int[,] GetNewGenerationArray(int[,] firstarr, int[,] secondArr)
        {
            var arraySize = firstarr.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (this.WillCellSurvive(firstarr, i, j))
                    {
                        secondArr[i, j] = 1;
                    }
                    else
                    {
                        secondArr[i, j] = 0;
                    }
                }
            }
            return secondArr;
        }

        public void InitializeArray(int[,] arr)
        {
            Random rand = new Random();
            int arraySize = arr.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    arr[i, j] = rand.Next(0, 2);
                }
            }
        }


        public int GetAllCellCount(int[,] gameArray)
        {
            return gameArray.Length;
        }

        public int GetAliveCellCount(int[,] gameArray)
        {
            int arrayLength = gameArray.GetLength(0);
            int aliveCells = 0;
            for (int i = 0; i < arrayLength; i++)
            {
                for (int j = 0; j < arrayLength; j++)
                {
                    if (gameArray[i, j] == 1)
                    {
                        aliveCells += 1;
                    }
                }
            }
            return aliveCells;
        }

        public int GetDeadCellCount(int allCellCount, int aliveCellCount)
        {
            return allCellCount - aliveCellCount;
        }
    }
}
