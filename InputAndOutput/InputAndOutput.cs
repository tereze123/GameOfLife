using InputAndOutput.Enums;
using InputAndOutput.Interfaces;
using System;

namespace InputAndOutput
{
    public class InputAndOutput: IInputAndOutput
    {
        public void SetColor(ColorEnum backgroundColor, ColorEnum foreGroundColor)
        {
            if (backgroundColor == ColorEnum.Black)
            {
                Console.BackgroundColor = ConsoleColor.Black;
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.White;
            }
            if (foreGroundColor == ColorEnum.White)
            {
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Black;
            }
        }

        private void DrawTopBorder()
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.White);
            Console.Write("===");
        }

        private void AliveCellOutput(int[,] arr, int x, int y)
        {
            this.SetColor(backgroundColor: ColorEnum.White, foreGroundColor: ColorEnum.White);
            Console.Write($" " + arr[x + 1, y + 1] + " ");
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.Black);
        }

        private void DeadCellOutput(int[,] arr, int x, int y)
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.Black);
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
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.White);
            Console.Write("===");
        }

        private void DrawBottomBorder(int x, int y, int cursorLeft, int cursorTop)
        {
            Console.SetCursorPosition(cursorLeft + ((y + 2) * 3), cursorTop + x + 2);
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.White);
            Console.Write("===");
        }

        public void DrawGameArrayOnScreen(
            int[,] arr, 
            int arraySize, 
            int cursorLeft = 0, 
            int cursorTop = 1)
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.Black);
            arraySize = arr.GetLength(0);

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
                this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.Black);
            }
        }

        public void DrawStatistics(
            int arraySize, 
            int iterationCount, 
            int cellCount, 
            int aliveCellCount, 
            int deadCellCount)
        {
            this.SetColor(backgroundColor: ColorEnum.Black, foreGroundColor: ColorEnum.White);
            Console.SetCursorPosition(0, arraySize + 5);

            Console.WriteLine($"Iteration number: " + iterationCount);
            Console.WriteLine($"Cell Count: " + cellCount);
            Console.WriteLine($"Alive Cell Count: " + aliveCellCount);
            Console.WriteLine($"Dead Cell Count: " + deadCellCount);
        }

        public int GetValidFieldSizeFromUser()
        {
            string userInput = "";
            do
            {
                Console.Clear();
                this.TextOutputForFieldSizeInput();
                userInput = Console.ReadLine();
            } while (!(this.ValidateFieldSizeUserInput(userInput)));
            return this.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForPausedGame(int[,] firstArray)
        {
            string userInput = "";
            do
            {
                Console.Clear();
                this.TextOutputForPausedGame();
                userInput = Console.ReadLine();
            } while (!(this.ValdiatePausedGameUserInput(userInput)));
            return this.ParseFromStringToInt(userInput);
        }

        public int GetValidUserInputForStartMenu()
        {
            string userInput = "";
            do
            {
                Console.Clear();
                this.TextOutputForStartMenuInput();
                userInput = Console.ReadLine();
            } while (!(this.ValidateStartMenuUserInput(userInput)));
            return this.ParseFromStringToInt(userInput);
        }

        public void ClearScreen()
        {
            Console.Clear();
        }

        public void TextOutputForFieldSizeInput()
        {
            this.ClearScreen();
            Console.WriteLine("Please set the size of field (10-50)");
        }

        public void TextOutputForPausedGame()
        {
            this.ClearScreen();
            Console.WriteLine("Game Paused... Want to Continue / Save / Exit? (1/2/3)");
        }

        public void TextOutputForStartMenuInput()
        {
            Console.Clear();
            Console.WriteLine(
                "------------------WELCOME TO THE GAME OF LIFE------------------------------",
                "Please Choose Your Next Action:",
                "1 Start New Game",
                "2 Load Game From File",
                "3 Multiple games"
                );
        }

        public int ParseFromStringToInt(string userInput)
        {
            return int.Parse(userInput);
        }

        private bool CanParseToInt(string userInput)
        {
            return int.TryParse(userInput, out int temp);
        }

        public bool ValidateStartMenuUserInput(string userInput)
        {
            // System.Enum.TryParse
            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                    return true;
                default: return false;
            }
        }

        public bool ValdiatePausedGameUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                case "2":
                case "3":
                    return true;
                default: return false;
            }
        }

        public bool ValidateFieldSizeUserInput(string userInputFieldSize)
        {
            return ((CanParseToInt(userInputFieldSize))) ? ValidateFieldSizeLessThan50MoreOrEqualTo10(int.Parse(userInputFieldSize)) : false;
        }

        private bool ValidateFieldSizeLessThan50MoreOrEqualTo10(int userInputFieldSize)
        {
            return (userInputFieldSize < 50 && userInputFieldSize >= 10) ? true : false;
        }
    }
}
