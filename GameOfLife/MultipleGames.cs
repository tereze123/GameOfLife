using System;
using System.Threading;

namespace GameOfLife
{
    public class MultipleGames:Game
    {
        private readonly Generations _generations;
        private readonly UserInterFace _userInterFace;

        public MultipleGames(Generations generations, UserInterFace userInterFace)
        {
            this._generations = generations;
            this._userInterFace = userInterFace;
        }
         
        public void PlayMultiGame(params MultipleGames[] games)
        {
            this._userInterFace.ClearGameScreen();
            int iterationCounter = 0;
            for (int i = 0; i < 2; i++)
            {                
                games[i].FirstArray = this._generations.CreateArray(10);
                games[i].SecondArray = this._generations.CreateArray(10);
                this._generations.InitializeArray(games[i].FirstArray);
            }
            do
            {
                for (int i = 0; i < 2; i++)
                {
                    this._userInterFace.DrawGameArrayOnScreen(games[i].FirstArray, i + (((i+1) * (i+1) * (i + 1)) *(games[i].FirstArray.GetLength(0))), 0);
                    games[i].SecondArray = this._generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);
                    
                }
                Thread.Sleep(1000);
                for (int i = 0; i < 2; i++)
                {
                    this._userInterFace.DrawGameArrayOnScreen(games[i].SecondArray, (((i + 1) * (i + 1) * (i + 1)) * (games[i].SecondArray.GetLength(0))), 0);
                    games[i].FirstArray = this._generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
                    iterationCounter++;
                }
                Thread.Sleep(1000);
            } while ((!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)));
        }
    }
}
