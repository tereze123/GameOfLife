namespace Domain.Interfaces
{
    public interface IGameLogic
    {
        bool WillCellSurvive(bool[,] arr, int x, int y);
    }
}
