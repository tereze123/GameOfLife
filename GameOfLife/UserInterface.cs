﻿using System;

namespace GameOfLife
{
    public class UserInterFace
    {
        public void ClearGameScreen()
        {
            Console.Clear();
        }

        private void TextOutputForFieldSizeImput()
        {
            Console.Clear();
            Console.WriteLine("Please set the size of field (10-50)");
        }

        public int GetFieldSizeFromUser()
        {
            bool resultOfParse = false;
            int sizeOfField = 0;
            do
            {
                TextOutputForFieldSizeImput();
                resultOfParse = int.TryParse((Console.ReadLine()), out sizeOfField);
            } while (sizeOfField > 50 || sizeOfField < 10 || resultOfParse == false);

            return sizeOfField;
        }

        private void TextOutputForPausedGame(int[,] firstArray)
        {
            var arraySize = firstArray.GetLength(0);
            Console.SetCursorPosition(0, arraySize + 10);
            Console.WriteLine("Game Paused... Want to Continue / Save / Exit? (1/2/3)");
        }

        public int GetUserInputForPausedGame(int[,] firstArray)
        {
            int continueGame;

            TextOutputForPausedGame(firstArray);

            int.TryParse(Console.ReadLine(), out continueGame);

            return continueGame;
        }

        private void TextOutputForStartMenuImput()
        {
            Console.Clear();
            Console.WriteLine("------------------WELCOME TO THE GAME OF LIFE------------------------------");
            Console.WriteLine("Please Choose Your Next Action:");
            Console.WriteLine("1 Start New Game");
            Console.WriteLine("2 Load Game From File");
            Console.WriteLine("3 Multiple games");
        }

        public int GetUserInputForStartMenu()
        {
            int usersChoice;
            TextOutputForStartMenuImput();
            int.TryParse(Console.ReadLine(), out usersChoice);
            return usersChoice;
        }

        private void SetCursorAndBackgroundColorBlack()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        private void SetCursorAndBackgroundColorWhite()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void SetBackgroundBlackAndWhiteText()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DrawGameArrayOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 1)
        {
            int arraySize = arr.GetLength(0);
            SetCursorAndBackgroundColorBlack();
            for (int i = -1; i < arraySize-1; i++)
            {
                for (int j = -1; j < arraySize-1 ; j++)
                {
                    Console.SetCursorPosition(cursorLeft+((j+2)*3), cursorTop + i + 1);
                    if (i == -1 || j == -1)
                    {
                        SetBackgroundBlackAndWhiteText();

                        Console.Write("===");
                    }
                    else
                    {
                        if (arr[i + 1, j+1] == 1)
                        {
                            SetCursorAndBackgroundColorWhite();
                            Console.Write($" " + arr[i + 1, j+1] + " ");
                            SetCursorAndBackgroundColorBlack();
                        }
                        else
                        {

                            SetCursorAndBackgroundColorBlack();
                            Console.Write($" " + arr[i + 1, j+1] + " ");
                        }

                        if (j == arraySize - 2)
                        {
                            Console.SetCursorPosition(cursorLeft + ((j + 2) * 3) +3, cursorTop + i + 1);
                            SetBackgroundBlackAndWhiteText();

                            Console.Write("===");
                        }
                    }

                if (i == arraySize - 2)
                {
                        Console.SetCursorPosition(cursorLeft + ((j + 2) * 3), cursorTop + i + 2);
                        SetBackgroundBlackAndWhiteText();

                    Console.Write("===");
                }
                }
                SetCursorAndBackgroundColorBlack();
            }
        }

        public void DrawStatistics(int arraySize, int iterationCount, int cellCount, int aliveCellCount, int deadCellCount)
        {
            SetBackgroundBlackAndWhiteText();
            Console.SetCursorPosition(0, arraySize + 5);
            Console.WriteLine($"Iteration number: " + iterationCount);
            Console.WriteLine($"Cell Count: " + cellCount);
            Console.WriteLine($"Alive Cell Count: " + aliveCellCount);
            Console.WriteLine($"Dead Cell Count: " + deadCellCount);
        }
    }
}
