namespace Domain.Interfaces
{
    public interface IGameField
    {
        int[,] CreateArray(int arraySize);
        int[,] GetNewGenerationArray(int[,] initialArray, int[,] nextGenerationArray);
        void InitializeArray(int[,] arr);
    }
}