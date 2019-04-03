namespace Domain.Interfaces
{
    public interface IGameField
    {
        bool[,] CreateArray(int arraySize);
        bool[,] GetNewGenerationArray(bool[,] initialArray, bool[,] nextGenerationArray);
        void InitializeArray(bool[,] arr);
    }
}