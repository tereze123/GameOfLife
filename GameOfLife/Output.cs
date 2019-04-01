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

        public void DrawGameArrayOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            int arraySize = Generations.GetArraySize(arr);
            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
            for (int i = -1; i < arraySize - 1; i++)
            {
                for (int j = -1; j < arraySize - 1; j++)
                {
                    this._console.SetCursorPosition(cursorLeft + ((j + 2) * 3), cursorTop + i + 1);
                    if (i == -1 || j == -1)
                    {
                        this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
                        this._console.WriteToConsole("===");
                    }
                    else
                    {
                        if (arr[i + 1, j + 1] == 1)
                        {
                            this._console.SetColor(backgroundColor: ColorEnum.White, ForeGroundColor: ColorEnum.White);
                            this._console.WriteToConsole($" " + arr[i + 1, j + 1] + " ");
                            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
                        }
                        else
                        {
                            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.Black);
                            this._console.WriteToConsole($" " + arr[i + 1, j + 1] + " ");
                        }

                        if (j == arraySize - 2)
                        {
                            this._console.SetCursorPosition(cursorLeft + ((j + 2) * 3) + 3, cursorTop + i + 1);
                            this._console.SetColor(backgroundColor: ColorEnum.Black, ForeGroundColor: ColorEnum.White);
                            this._console.WriteToConsole("===");
                        }
                    }

                    if (i == arraySize - 2)
                    {
                        this._console.SetCursorPosition(cursorLeft + ((j + 2) * 3), cursorTop + i + 2);
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
