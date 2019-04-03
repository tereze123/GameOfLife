namespace Domain.Interfaces
{
    public interface IGameLogic
    {
        bool CellStatusInNextGeneration(bool[,] arr, int x, int y);
    }
}
