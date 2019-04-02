using System.Threading;

namespace GameOfLife
{
    public class MultipleGames:Game
    {
        private readonly Generations _generations;
        private readonly Output _output;


        public MultipleGames(Generations _generations, Output output)
        {
            this._generations = _generations;
            this._output = output;

        }
         
        public void PlayMultiGame(params MultipleGames[] games)
        {
            int cursorXPosition = 0;

            this._output.ClearGameScreen();
            int iterationCounter = 0;
            for (int i = 0; i < 2; i++)
            {                
                games[i].FirstArray = this._generations.CreateArray(10);
                games[i].SecondArray = this._generations.CreateArray(10);
                this._generations.InitializeArray(games[i].FirstArray);
            }
            do
            {
                for (int i = 0; i < 9; i++)
                {
                    cursorXPosition = (((i % 2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
                    this._output.DrawGameArrayOnScreen(games[i].FirstArray, cursorXPosition, (i/2) * (Generations.GetArraySize(games[i].FirstArray)));
                    games[i].SecondArray = this._generations.GetNewGenerationArray(games[i].FirstArray, games[i].SecondArray);                   
                }
                for (int i = 0; i < 9; i++)
                {
                    cursorXPosition = (((i %2) + 1) * ((i % 2) + 1) * ((i % 2) + 1)) * (Generations.GetArraySize(games[i].FirstArray));
                    this._output.DrawGameArrayOnScreen(games[i].SecondArray, cursorXPosition, (i /2) * (Generations.GetArraySize(games[i].FirstArray)));
                    games[i].FirstArray = this._generations.GetNewGenerationArray(games[i].SecondArray, games[i].FirstArray);
                    iterationCounter++;
                }
                Thread.Sleep(1000);
            } while (!Input.EscapeKeyWasPressed());
        }
    }
}
