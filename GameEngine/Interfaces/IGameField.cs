namespace GameEngine.Interfaces
{
    public interface IGameField
    {
        int[,] CreateArray(int arraySize);
        int[,] GetNewGenerationArray(int[,] firstarr, int[,] secondArr);
        void InitializeArray(int[,] arr);
    }
}