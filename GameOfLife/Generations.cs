using System;

namespace GameOfLife
{
    public class Generations
    {
        private readonly GameLogic gameLogic;

        public Generations(GameLogic gameLogic)
        {
            this.gameLogic = gameLogic;
        }

        public int[,] CreateArray(int arraySize) => new int[arraySize, arraySize];

        public void InitializeArray(  int[,] arr)
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

        public int[,] GetNewGenerationArray(  int[,] firstarr,   int[,] secondArr)
        {
            var arraySize = firstarr.GetLength(0);

            for (int i = 0; i < arraySize; i++)
            {
                for (int j = 0; j < arraySize; j++)
                {
                    if (gameLogic.WillSurvive(  firstarr, i, j))
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
    }
}
