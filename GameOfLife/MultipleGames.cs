using System;
using System.Threading;

namespace GameOfLife
{
    public class MultipleGames:Game
    {
        private readonly Generations generations;
        private readonly GameStatistics statistics;
        private readonly UserInterFace userInterFace;

        public MultipleGames(Generations generations,GameStatistics statistics, UserInterFace userInterFace)
        {
            this.generations = generations;
            this.statistics = statistics;
            this.userInterFace = userInterFace;
        }
         
        public void PlayMultiGame(params MultipleGames[] games)
        {
            userInterFace.ClearGameScreen();
            int iterationCounter = 0;
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
                    userInterFace.DrawGameArrayOnScreen(games[i].FirstArray, i + (((i+1) * (i+1) * (i + 1)) *(games[i].FirstArray.GetLength(0))), 0);
                    games[i].SecondArray = generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);
                    
                }
                Thread.Sleep(1000);
                for (int i = 0; i < 2; i++)
                {
                    userInterFace.DrawGameArrayOnScreen(games[i].SecondArray, (((i + 1) * (i + 1) * (i + 1)) * (games[i].SecondArray.GetLength(0))), 0);
                    games[i].FirstArray = generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
                    iterationCounter++;
                }
                Thread.Sleep(1000);
            } while ((!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)));
        }
    }
}
