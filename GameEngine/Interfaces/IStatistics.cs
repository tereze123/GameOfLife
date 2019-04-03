namespace Domain.Interfaces
{
    public interface IStatistics
    {
            int GetAliveCellCount(bool[,] gameArray);
            int GetAllCellCount(bool[,] gameArray);
            int GetDeadCellCount(int allCellCount, int aliveCellCount);       
    }
}
