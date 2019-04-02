namespace GameOfLife
{
    public class GameEngine
    {
        private static FileOperations _fileStore = new FileOperations();
        private static GameStatistics _statistics = new GameStatistics();
        private static GameLogic _gameLogic = new GameLogic();
        private static Generations _generations = new Generations(_gameLogic);
        private static ConsoleManipulations _console = new ConsoleManipulations();
        private static Output _output = new Output(_generations, _console);

        private static Input _input = new Input(_generations, new UserInputValidate(),_output, _console);

         public static void Start()
         {
            SingleGame game = new SingleGame(_statistics, _generations, _fileStore, _output,_input);
            game.StartMenu();
         }
    }   
}
