using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class Draw
    {
        public static void SetColorBlack()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Black;
        }

        public static void SetColorWhite()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void DrawOnScreen(int[,] arr, int cursorLeft = 0, int cursorTop = 0)
        {
            int arraySize = arr.GetLength(0);
            SetColorBlack();
            for (int i = 0; i < arraySize; i++)
            {
                Console.SetCursorPosition(cursorLeft, cursorTop + i);
                for (int j = 0; j < arraySize; j++)
                {
                    if (arr[i, j] == 1)
                    {
                        SetColorWhite();
                        Console.Write($" " + arr[i, j] + " ");
                        SetColorBlack();
                    }
                    else
                    {
                        SetColorBlack();
                        Console.Write($" " + arr[i, j] + " ");
                    }
                }
                SetColorBlack();
                //Console.WriteLine();
            }
        }
    }
}
