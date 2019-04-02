﻿namespace InputAndOutput.Interfaces
{
    public interface IDrawField
    {
        void DrawGameArrayOnScreen(int[,] arr, int arraySize, int cursorLeft = 0, int cursorTop = 1);
        void DrawStatistics(int arraySize, int iterationCount, int cellCount, int aliveCellCount, int deadCellCount);
    }
}