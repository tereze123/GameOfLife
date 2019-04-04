namespace Domain.Interfaces
{
    public interface IGameField
    {
        bool[,] CreateArray(int arraySize);
        bool[,] GetNewGenerationArray(bool[,] initialArray);
        void InitializeArray(bool[,] arr);
    }
}