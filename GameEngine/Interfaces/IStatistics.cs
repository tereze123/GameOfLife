using System.Collections.Generic;

namespace Domain.Interfaces
{
    public interface IStatistics
    {
            int GetAliveCellCount(bool[,] gameArray);
            int GetAllCellCount(bool[,] gameArray);
            int GetDeadCellCount(int allCellCount, int aliveCellCount);
            int GetAllCellCountMultiGame(List<GameModelState> gameList);
        int GetAliveCellCountMultiGame(List<GameModelState> gameList);
    }
}
