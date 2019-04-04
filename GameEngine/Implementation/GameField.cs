using System;
using Domain.Interfaces;

namespace Domain
{
    public class GameField : IGameField
    {
        private readonly IGameLogic _gameLogic;

        public GameField(IGameLogic gameLogic)
        {
            _gameLogic = gameLogic;
        }

        public bool[,] CreateArray(int arraySize)
        {
            return new bool[arraySize, arraySize];
        }

        public bool[,] GetNewGenerationArray(bool[,] initialArray)
        {
            var arraySize = initialArray.GetLength(0);
            bool[,] nextGenerationArray = new bool[arraySize, arraySize];

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    nextGenerationArray[i, j] = _gameLogic.CellStatusInNextGeneration(initialArray, i, j);
                }
            }
            return nextGenerationArray;
        }

        public void InitializeArray(bool[,] arr)
        {
            Random rand = new Random();
            int arraySize = arr.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    arr[i, j] = (rand.Next(0, 2) == 1) ? true : false;
                }
            }
        }
    }
}
