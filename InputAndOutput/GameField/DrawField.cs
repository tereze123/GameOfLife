using InputAndOutput.Interfaces;
using InputAndOutput.Enums;
using System;

namespace InputAndOutput
{
    public class DrawField : IDrawField
    {
        private readonly IColorOfOutput _colorOfOutput;

        public DrawField(IColorOfOutput colorOfOutput)
        {
            _colorOfOutput = colorOfOutput;
        }
        public void DrawGameArrayOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            var arraySize = arr.GetLength(0);

            for (int x = -1; x < arraySize - 1; x++)
            {
                for (int y = -1; y < arraySize - 1; y++)
                {
                    Console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 1);

                    if (IsTopRow(x, y)) this.DrawTopBorder();
                    else
                    {
                        if (CellIsAlive(arr, x, y)) this.AliveCellOutput(arr, x, y);
                        else this.DeadCellOutput(arr, x, y);
                    }
                    if (IsLastColumn(y, arraySize))
                    {
                        DrawRightBorder(x, y, cursorLeft, cursorTop);
                    }
                    if (IsLastRow(x, arraySize))
                    {
                        DrawBottomBorder(x, y, cursorLeft, cursorTop);
                    }
                }
                _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            }
        }
        public void DrawStatistics(
            int arraySize, 
            int iterationCount, 
            int cellCount, 
            int aliveCellCount, 
            int deadCellCount)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.SetCursorPosition(0, arraySize + 5);
            Console.WriteLine($"Iteration number: " + iterationCount);
            Console.WriteLine($"Cell Count: " + cellCount);
            Console.WriteLine($"Alive Cell Count: " + aliveCellCount);
            Console.WriteLine($"Dead Cell Count: " + deadCellCount);
        }

        private void DrawTopBorder()
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.Write("===");
        }
        private void AliveCellOutput(int[,] arr, int x, int y)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.White, foregroundColor: ColorEnum.White);
            Console.Write($" " + arr[x + 1, y + 1] + " ");
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
        }
        private void DeadCellOutput(int[,] arr, int x, int y)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            Console.Write($" " + arr[x + 1, y + 1] + " ");
        }
        private bool CellIsAlive(int[,] arr, int x, int y)
        {
            return arr[x + 1, y + 1] == 1 ? true : false;
        }
        private bool IsTopRow(int x, int y)
        {
            return (x == -1 || y == -1) ? true : false;
        }
        private bool IsLastColumn(int y, int arraySize)
        {
            return (y == arraySize - 2) ? true : false;
        }
        private bool IsLastRow(int x, int arraySize)
        {
            return (x == arraySize - 2) ? true : false;
        }
        private void DrawRightBorder(int x, int y, int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 1);
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.Write("===");
        }
        private void DrawBottomBorder(int x, int y, int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 2);
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.Write("===");
        }
    }
}
