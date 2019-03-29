using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace GameOfLife
{
    public class MultipleGames
    {
        private readonly Generations generations;
        private readonly GameStatistics statistics;

        public MultipleGames(Generations generations,GameStatistics statistics)
        {
            this.generations = generations;
            this.statistics = statistics;
        }

        public void PlayMultiGame(params Game[] games)
        {
            Console.Clear();
            int currentArray = 0;
            int counter = 0;
            for (int i = 0; i < 2; i++)
            {
                games[i].FirstArray = generations.CreateArray(10);
                games[i].SecondArray = generations.CreateArray(10);
                generations.InitializeArray(games[i].FirstArray);
            }
            do
            {
                for (int i = 0; i < 2; i++)
                {
                    Draw.DrawOnScreen(games[i].FirstArray, i + (((i+1) * (i+1) * (i + 1)) *(games[i].FirstArray.GetLength(0))), 0);
                    currentArray = 1;
                    games[i].SecondArray = generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);
                    
                }
                Thread.Sleep(1000);
                for (int i = 0; i < 2; i++)
                {
                    Draw.DrawOnScreen(games[i].SecondArray, (((i + 1) * (i + 1) * (i + 1)) * (games[i].SecondArray.GetLength(0))), 0);
                    currentArray = 2;
                    games[i].FirstArray = generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
                    counter++;
                }
                Thread.Sleep(1000);
            } while ((!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)));
        }
    }
}
