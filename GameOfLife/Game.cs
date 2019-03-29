using System;
using System.Threading;

namespace GameOfLife
{
    public class Game
    {
        public int[,] FirstArray { get; set; }
        public int[,] SecondArray { get; set; }

        private readonly GameStatistics statistics;
        private readonly Generations generations;
        private readonly GameLogic gameLogic;
        private readonly FileStore fileStore;

        public Game(GameStatistics statistics, Generations generations, GameLogic gameLogic,FileStore fileStore)
        {
            this.statistics = statistics;
            this.generations = generations;
            this.gameLogic = gameLogic;
            this.fileStore = fileStore;          
        }

        public void StartMenu()
        {
            int usersChoice;
            Console.Clear();
            Console.WriteLine("------------------WELCOME TO THE GAME OF LIFE------------------------------");
            Console.WriteLine("Please Choose Your Next Action:");
            Console.WriteLine("1 Start New Game");
            Console.WriteLine("2 Load Game From File");
            Console.WriteLine("3 Multiple games");
            int.TryParse(Console.ReadLine(), out usersChoice);

            switch(usersChoice)
            {
                case 1:
                    StartNewGame();
                    break;

                case 2:
                    StartGameFromLoadedFile();
                    break;
                case 3:
                    MultipleGames games = new MultipleGames(generations, statistics);
                    games.PlayMultiGame(new Game(statistics, generations, gameLogic, fileStore), new Game(statistics, generations, gameLogic, fileStore));
                    break;
            }
        }

        public int GetFieldSizeFromUser()
        {

            bool resultOfParse = false;
            int sizeOfField = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Please set the size of field (10-50)");
                resultOfParse = int.TryParse((Console.ReadLine()), out sizeOfField);
            } while (sizeOfField > 50 || sizeOfField < 10 || resultOfParse == false);

            return sizeOfField;
        }

        public void SaveGame(  int[,] array)
        {
            this.fileStore.CreateFolderOnDesktop();
            this.fileStore.WriteTheArrayIntoFile(  array);
        }

        public void PauseGame(int continueGame,   int[,] firstArray,   int[,] secondArray, int currentArray)
        {
            Console.SetCursorPosition(0, this.statistics.ArraySize + 10);
            Console.WriteLine("Game Paused... Want to Continue / Save / Exit? (1/2/3)");
            int.TryParse(Console.ReadLine(), out continueGame);

            switch (continueGame)
            {
                case 1:
                    PlayGame(  firstArray,   secondArray);
                    break;
                case 2:
                    if (currentArray == 1)
                    {
                        SaveGame(firstArray);
                    }
                    else
                    {
                        SaveGame(secondArray);
                    }
                    
                    break;
                case 3:
                    Environment.Exit(0);
                    return;
                default:
                    return;
            }
        }

        public void PlayGame(  int[,] firstArray,   int[,] secondArray, int cursorLeft = 0, int cursorTop = 0)
        {
            int currentArray = 0;
            int continueGame = 0;
            int counter = 0;           
            do
            {
                this.statistics.CalculateStatistics(  firstArray);                
                Thread.Sleep(1000);                
                Draw.DrawOnScreen(firstArray, cursorLeft, cursorTop);
                currentArray = 1;
                this.statistics.DrawStatistics();
                secondArray = generations.GetNewGenerationArray(  firstArray,   secondArray);
                this.statistics.CalculateStatistics(  secondArray);
                this.statistics.IterrationCount += 1;
                Thread.Sleep(1000);
                Draw.DrawOnScreen(secondArray, cursorLeft, cursorTop);
                currentArray = 2;
                this.statistics.DrawStatistics();
                firstArray = generations.GetNewGenerationArray(  secondArray,   firstArray);

                counter++;
                this.statistics.IterrationCount += 1;

            } while ((!(Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Escape)));

            PauseGame(continueGame,   firstArray,   secondArray, currentArray);

        }

        public void StartGameFromLoadedFile()
        {
            int[,] firstArray = fileStore.ReturnSavedArrayFromFile();
            int arraySize = firstArray.GetLength(0);
            var secondArray = generations.CreateArray(arraySize);
            Console.Clear();
            PlayGame(  firstArray,   secondArray);
            Console.ReadLine();
        }

        public void StartNewGame(int cursorLeft = 0, int cursorTop = 0)
        {
            this.statistics.IterrationCount = 1;
            int sizeOfField = GetFieldSizeFromUser();
            FirstArray = generations.CreateArray(sizeOfField);
            SecondArray = generations.CreateArray(sizeOfField);
            generations.InitializeArray(FirstArray);
            Console.Clear();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);            
            Console.ReadLine();
        }

        public void StartNewMultiGame(int cursorLeft = 0, int cursorTop = 0)
        {
            this.statistics.IterrationCount = 1;
            int sizeOfField = GetFieldSizeFromUser();
            FirstArray = generations.CreateArray(sizeOfField);
            SecondArray = generations.CreateArray(sizeOfField);
            generations.InitializeArray(FirstArray);
            Console.Clear();
            PlayGame(FirstArray, SecondArray, cursorLeft, cursorTop);
            Console.ReadLine();
        }
    }
}
