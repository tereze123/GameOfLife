using System;

namespace GameOfLife
{
    public class Output
    {
        private readonly Generations _generations;
        private readonly ConsoleManipulations _console;

        public Output(Generations _generations, ConsoleManipulations console)
        {
            this._generations = _generations;
            this._console = console;
        }

        public void TextOutputForFieldSizeImput()
        {
            this._console.ClearGameScreen();
            this._console.WriteToConsoleOneLine("Please set the size of field (10-50)");
        }

        public void TextOutputForPausedGame(int[,] firstArray)
        {
            var arraySize = Generations.GetArraySize(firstArray);
            this._console.SetCursorPosition(0, arraySize + 10);
            this._console.WriteToConsoleOneLine("Game Paused... Want to Continue / Save / Exit? (1/2/3)");
        }

        private void DrawTopBorder()
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            this._console.WriteToConsole("===");
        }

        public void TextOutputForStartMenuImput()
        {
            this._console.ClearGameScreen();
            this._console.WriteManyLinesToConsole(
                "------------------WELCOME TO THE GAME OF LIFE------------------------------",
                "Please Choose Your Next Action:",
                "1 Start New Game",
                "2 Load Game From File",
                "3 Multiple games");
        }

        private void AliveCellOutput(int[,] arr, int x, int y)
        {
            this.SetColor(backgroundColor: ColorEnum.White, foregroundColor: ColorEnum.White);
            Console.Write($" " + arr[x + 1, y + 1] + " ");
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
        }

        private void DeadCellOutput(int[,] arr, int x, int y)
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            this._console.WriteToConsole($" " + arr[x + 1, y + 1] + " ");
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
            this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 1);
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            this._console.WriteToConsole("===");
        }

        private void DrawBottomBorder(int x, int y, int cursorLeft, int cursorTop)
        {
            this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 2);
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            this._console.WriteToConsole("===");
        }

        public void DrawGameArrayOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            int arraySize = Generations.GetArraySize(arr);
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);

            for (int x = -1; x < arraySize - 1; x++)
            {
                for (int y = -1; y < arraySize - 1; y++)
                {
                    this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 1);

                    if (IsTopRow(x, y)) this.DrawTopBorder();
                    else
                    {
                        if (CellIsAlive(arr,x,y)) this.AliveCellOutput(arr,x,y);
                        else this.DeadCellOutput(arr, x, y);
                    }
                    if (IsLastColumn(y,arraySize))
                    {
                        DrawRightBorder(x, y, cursorLeft, cursorTop);
                    }
                    if (IsLastRow(x,arraySize))
                    {
                        DrawBottomBorder(x, y, cursorLeft, cursorTop);
                    }
                }
                this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.Black);
            }
        }

        public void DrawStatistics(int arraySize, int iterationCount, int cellCount, int aliveCellCount, int deadCellCount)
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foregroundColor: ColorEnum.White);
            this._console.SetCursorPosition(0, arraySize + 5);
            this._console.WriteManyLinesToConsole(
                $"Iteration number: " + iterationCount,
                $"Cell Count: " + cellCount,
                $"Alive Cell Count: " + aliveCellCount,
                $"Dead Cell Count: " + deadCellCount);
        }

        public void ClearGameScreen()
        {
            _console.ClearGameScreen();
        }

        void SetColor(ColorEnum backgroundColor, ColorEnum foregroundColor) { }
    }
}
