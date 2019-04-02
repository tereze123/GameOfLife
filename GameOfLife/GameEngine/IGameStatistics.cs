namespace GameOfLife
{
    public interface IGameStatistics
    {
        int GetAliveCellCount(int[,] gameArray);
        int GetAllCellCount(int[,] gameArray);
        int GetDeadCellCount(int allCellCount, int aliveCellCount);
    }
}