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
                    if (_gameLogic.WillCellSurvive(initialArray, i, j))
                    {
                        nextGenerationArray[i, j] = 1;
                    }
                    else
                    {
                        nextGenerationArray[i, j] = 0;
                    }
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
    }
}
