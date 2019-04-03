using GameEngine.Interfaces;
using System;

namespace GameEngine
{
    public class GameEngine: IGameEngine
    {
        private readonly GameModelState _gameModelState;
        public int[,] FirstArray { get; set; }
        public int[,] SecondArray { get; set; }
        public GameEngine(GameModelState gameModelState)
        {
            _gameModelState = gameModelState;
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
        public int[,] GetNewGenerationArray(int[,] initialArray, int[,] nextGenerationArray)
        {
            var arraySize = initialArray.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    SetCellStatusForNextGeneration(ref initialArray, ref nextGenerationArray, i, j);
                }
            }
            return nextGenerationArray;
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



        private void SetCellStatusForNextGeneration(ref int[,] initialArray, ref int[,] nextGenerationArray, int i, int j)
        {
            if (this.WillCellSurvive(initialArray, i, j))
            {
                nextGenerationArray[i, j] = 1;
            }
            else
            {
                nextGenerationArray[i, j] = 0;
            }
        }
        private bool ThreeOrTwoAliveNeighbors(int neighbours)
        {
            return (neighbours == 2 || neighbours == 3) ? true : false;
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
        private bool ThreeAliveNeighbours(int neighbours)
        {
            return (neighbours == 3) ? true : false;
        }
    }
}
