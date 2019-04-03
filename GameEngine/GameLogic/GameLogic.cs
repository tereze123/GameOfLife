using Domain.Interfaces;

namespace Domain
{
    public class GameLogic:IGameLogic
    {
        public bool WillCellSurvive(bool[,] arr, int x, int y)
        {
            int neighbours = CountNeighbours(arr, x, y);

            bool valueOfThis = arr[x, y];

            if (valueOfThis == true)
            {
                return (ThreeOrTwoAliveNeighbors(neighbours)) ? true : false;
            }
            else
            {
                return (ThreeAliveNeighbours(neighbours)) ? true : false;
            }
        }

        private bool ThreeOrTwoAliveNeighbors(int neighbours)
        {
            return (neighbours == 2 || neighbours == 3) ? true : false;
        }
        private bool ThreeAliveNeighbours(int neighbours)
        {
            return (neighbours == 3) ? true : false;
        }
        private int CountNeighbours(bool[,] arr, int x, int y)
        {
            var arraySize = arr.GetLength(0);
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    var row = (i + x + arraySize) % arraySize;
                    var col = (j + y + arraySize) % arraySize;
                    sum += (arr[row, col] == true) ? 1 : 0;
                }
            }
            sum -= (arr[x, y] == true) ? 1 : 0;

            return sum;
        }
    }
}
