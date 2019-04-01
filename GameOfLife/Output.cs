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
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
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
            this._console.SetColor(backgroundColor: ColorEnum.White, ForeGroundColor: ColorEnum.White);
            this._console.WriteToConsole($" " + arr[x + 1, y + 1] + " ");
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
        }

        private void DeadCellOutput(int[,] arr, int x, int y)
        {
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
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
        public void DrawGameArrayOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            int arraySize = Generations.GetArraySize(arr);
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);

            for (int x = -1; x < arraySize - 1; x++)
            {
                for (int y = -1; y < arraySize - 1; y++)
                {
                    this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 1);

                    if (IsTopRow(x, y))
                    {
                        this.DrawTopBorder();
                    }
                    else
                    {
                        if (CellIsAlive(arr,x+1,y+1))
                        {
                            this.AliveCellOutput(arr,x,y);
                        }
                        else
                        {
                            this.DeadCellOutput(arr, x, y);
                        }

                        if (y == arraySize - 2)
                        {
                            this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3) + 3, cursorTop + x + 1);
                            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
                            this._console.WriteToConsole("===");
                        }
                    }

                    if (x == arraySize - 2)
                    {
                        this._console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 2);
                        this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
                        this._console.WriteToConsole("===");
                    }
                }
                this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
            }
        }

        public void DrawStatistics(int arraySize, int iterationCount, int cellCount, int aliveCellCount, int deadCellCount)
        {
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
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
    }
}
