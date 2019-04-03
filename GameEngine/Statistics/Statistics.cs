using Domain.Interfaces;

namespace Domain.Statistics
{
    public class Statistics :IStatistics
    {
        public int GetAllCellCount(bool[,] gameArray)
        {
            return gameArray.Length;
        }
        public int GetAliveCellCount(bool[,] gameArray)
        {
            int arrayLength = gameArray.GetLength(0);
            int aliveCells = 0;
            for (int i = 0; i < arrayLength; i++)
            {
                for (int j = 0; j < arrayLength; j++)
                {
                    if (gameArray[i, j] == true)
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
