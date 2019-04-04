using Presentation.Interfaces;
using Presentation.Enums;
using System;

namespace Presentation
{
    public class DrawField : IDrawField
    {
        private readonly IColorOfOutput _colorOfOutput;

        public DrawField(IColorOfOutput colorOfOutput)
        {
            _colorOfOutput = colorOfOutput;
        }
        public void DrawGameArrayOnScreen(bool[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            Console.CursorVisible = false;
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            var arraySize = arr.GetLength(0);

            for (int x = -1; x < arraySize - 1; x++)
            {
                for (int y = -1; y < arraySize - 1; y++)
                {
                    Console.SetCursorPosition(cursorLeft + ((y + 2) * 2), cursorTop + x + 1);

                    if (IsTopRow(x, y)) this.DrawTopBorder();
                    else
                    {
                        if (arr[x+1,y+1]) this.AliveCellOutput(arr, x, y);
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
        private void AliveCellOutput(bool[,] arr, int x, int y)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.White, foregroundColor: ColorEnum.White);
            Console.Write($"  ");
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
        }
        private void DeadCellOutput(bool[,] arr, int x, int y)
        {
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            Console.Write($"  ");
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
            if (cursorLeft < 0 || cursorTop < 0 || cursorLeft > Console.BufferWidth)
                throw new IndexOutOfRangeException();

            Console.SetCursorPosition(cursorLeft + ((y + 2) * 2), cursorTop + x + 1);
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.Write("===");
        }
        private void DrawBottomBorder(int x, int y, int cursorLeft, int cursorTop)
        {
            if (cursorLeft < 0 || cursorTop < 0 || cursorLeft > Console.BufferWidth)
                throw new IndexOutOfRangeException();

            Console.SetCursorPosition(cursorLeft + ((y + 2) * 2), cursorTop + x + 2);
            _colorOfOutput.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            Console.Write("===");
        }
    }
}
