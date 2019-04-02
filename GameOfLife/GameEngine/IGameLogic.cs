namespace GameOfLife
{
    public interface IGameLogic
    {
        bool WillSurvive(int[,] arr, int x, int y);
    }
}