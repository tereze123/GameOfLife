namespace GameOfLife
{
    public class GameEngine
    {
        private static FileOperations fileStore = new FileOperations();
        private static GameStatistics statistics = new GameStatistics();
        private static GameLogic gameLogic = new GameLogic();
        private static UserInterFace userInterFace = new UserInterFace();
        private static Generations generations = new Generations(gameLogic);

         public static void Start()
         {
            SingleGame game = new SingleGame(statistics, generations, fileStore, userInterFace);
            game.StartMenu();
         }
    }   
}
