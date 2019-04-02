namespace GameEngine.Interfaces
{
    public interface IGameLoop
    {
        void LoopGame(int[,] firstArray, int[,] secondArray, int cursorLeft = 1, int cursorTop = 1, int iterationCount = 1);
    }
}
