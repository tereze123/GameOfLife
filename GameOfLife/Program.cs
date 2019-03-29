using System;
using System.Threading;

namespace GameOfLife
{
    class Program
    {               
        static void Main(string[] args)
        {
            FileStore fileStore = new FileStore();
            GameStatistics statistics = new GameStatistics();
            GameLogic gameLogic = new GameLogic();
            Generations generations = new Generations(gameLogic);

            Game game = new Game(statistics, generations, gameLogic, fileStore);

            game.StartMenu();
            Console.ReadLine();
        }
    }
}
