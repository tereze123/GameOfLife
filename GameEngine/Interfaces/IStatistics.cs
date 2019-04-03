namespace Domain.Interfaces
{
    public interface IStatistics
    {
            int GetAliveCellCount(int[,] gameArray);
            int GetAllCellCount(int[,] gameArray);
            int GetDeadCellCount(int allCellCount, int aliveCellCount);       
    }
}
