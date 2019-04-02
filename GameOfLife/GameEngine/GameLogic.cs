namespace GameOfLife
{
    public class GameLogic : IGameLogic
    {
        private bool ThreeOrTwoAliveNeighbors(int neighbours)
        {
           return (neighbours == 2 || neighbours == 3) ? true : false;
        }
        private bool ThreeAliveNeighbours(int neighbours)
        {
            return (neighbours == 3) ? true : false;
        }

        private int CountNeighbours(  int[,] arr, int x, int y)
        {
            var arraySize = Generations.GetArraySize(arr);
            int sum = 0;
            for (int i = - 1; i < 2; i++)
            {
                for (int j = - 1; j < 2; j++)
                {
                    var row = (i + x + arraySize) % arraySize;
                    var col = (j + y + arraySize) % arraySize;
                    sum += arr[row, col];
                }
            }
            sum -= arr[x, y];

            return sum;
        }
        public bool WillSurvive(  int[,] arr, int x, int y)
        {
            int neighbours = CountNeighbours(arr,x,y);

            int valueOfThis = arr[x, y];

            if(valueOfThis == 1)
            {
                return (ThreeOrTwoAliveNeighbors(neighbours)) ? true : false;
            }
            else
            {
                return (ThreeAliveNeighbours(neighbours)) ? true : false;
            }
        }
    }
}
