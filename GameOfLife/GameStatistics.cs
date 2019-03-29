using System;
using System.Collections.Generic;
using System.Text;

namespace GameOfLife
{
    public class GameStatistics
    {
        public int ArraySize { get; set; }

        public int CellCount { get; set; }

        public int IterrationCount { get; set; }

        public int AliveCellCount { get; set; }

        public int DeadCellCount { get; set; }

        public void SetColorReadable()
        {
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }

        public void DrawStatistics()
        {
            SetColorReadable();
            Console.SetCursorPosition(0, ArraySize + 5);
            Console.WriteLine($"Iteration number: " + IterrationCount);
            Console.WriteLine($"Cell Count: " + CellCount);
            Console.WriteLine($"Alive Cell Count: " + AliveCellCount);
            Console.WriteLine($"Dead Cell Count: " + DeadCellCount);
        }

        public void CalculateStatistics(  int[,] gameArray)
        {
            this.CellCount = 0;
            AliveCellCount = 0;
            DeadCellCount = 0;

            ArraySize = gameArray.GetLength(0);
            for (int i = 0; i < ArraySize; i++)
            {
                for (int j = 0; j < ArraySize; j++)
                {
                    CellCount += 1;
                    if (gameArray[i, j] == 1)
                    {
                        AliveCellCount += 1;
                    }
                    else
                    {
                        DeadCellCount += 1;
                    }
                }
            }
        }
    }
}
