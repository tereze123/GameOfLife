using Domain.Interfaces;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public int GetAllCellCountMultiGame(List<GameModelState> gameList)
        {
            int allCellCount = 0;
            ConcurrentBag<int> countOfAllCells = new ConcurrentBag<int>();
            Parallel.ForEach(gameList, (g) =>
            {
                countOfAllCells.Add(this.GetAllCellCount(g.GameField));
            });
            foreach (var aliveCell in countOfAllCells)
            {
                allCellCount += aliveCell;
            }
            return allCellCount;
        }

        public int GetAliveCellCountMultiGame(List<GameModelState> gameList)
        {
            int aliveCellCount = 0;
            ConcurrentBag<int> countOfAllCells = new ConcurrentBag<int>();
            Parallel.ForEach(gameList, (g) =>
            {
                countOfAllCells.Add(this.GetAliveCellCount(g.GameField));
            });
            foreach (var aliveCell in countOfAllCells)
            {
                aliveCellCount += aliveCell;
            }
            return aliveCellCount;
        }
    }
}
