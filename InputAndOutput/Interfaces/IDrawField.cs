namespace Presentation.Interfaces
{
    public interface IDrawField
    {
        void DrawGameArrayOnScreen(bool[,] arr, int cursorLeft = 0, int cursorTop = 1);
        void DrawStatistics(int arraySize, 
            int iterationCount, 
            int cellCount,
            int aliveCellCount,
            int deadCellCount, 
            int gameCount = 1,
            int visibleGameCount = 1);

    }
}