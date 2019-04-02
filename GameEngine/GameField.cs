using System;
using GameEngine.Interfaces;

namespace GameEngine
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

        public int[,] GetNewGenerationArray(int[,] firstarr, int[,] secondArr)
        {
            var arraySize = firstarr.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (_gameLogic.WillCellSurvive(firstarr, i, j))
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
    }
}
