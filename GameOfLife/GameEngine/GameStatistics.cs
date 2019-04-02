namespace GameOfLife
{
    public class GameStatistics : IGameStatistics
    {
        public int GetAllCellCount(int[,] gameArray)
        {
            return gameArray.Length;
        }

        public int GetAliveCellCount(int[,] gameArray)
        {
            int arrayLength = Generations.GetArraySize(gameArray);
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
