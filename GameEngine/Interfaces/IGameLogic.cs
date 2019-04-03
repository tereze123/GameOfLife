namespace Domain.Interfaces
{
    public interface IGameLogic
    {
        bool WillCellSurvive(int[,] arr, int x, int y);
    }
}
