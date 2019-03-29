using System;

namespace GameOfLife
{
    public class Program
    {               
        static void Main(string[] args)
        {
            FileOperations fileStore = new FileOperations();
            GameStatistics statistics = new GameStatistics();
            GameLogic gameLogic = new GameLogic();
            UserInterFace userInterFace = new UserInterFace();
            Generations generations = new Generations(gameLogic);

            SingleGame game = new SingleGame(statistics, generations, fileStore, userInterFace);
            game.StartMenu();
            Console.ReadLine();
        }
    }
}
